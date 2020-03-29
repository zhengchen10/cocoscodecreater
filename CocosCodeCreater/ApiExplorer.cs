using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kernel.models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CocosCodeCreater
{
    public partial class ApiExplorer : ToolWindow, IPropertyListener
    {
        TreeNode root;
        TreeNode input;
        TreeNode output;
        Global global;
        public ApiExplorer(Global global)
        {
            this.global = global;
            InitializeComponent();
        }
        private Request request = null;
        public Request Request
        {
            get
            {
                return request;
            }
            set { reloadRequest(value); }
        }

        public void onPropertyChanged(object obj, string property, object oldValue, object newValue)
        {
            if(obj is Request)
            {
                if(property == "Name")
                {
                    root.Text = (String)newValue;
                }
            } else if(obj is Param)
            {
                if (property == "Name")
                {
                    TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], (string)oldValue, obj.GetType());
                    node.Text = (String)newValue;
                }
            }
            //throw new NotImplementedException();
        }

        private void reloadRequest(Request request)
        {
            this.request = request;
            treeView1.Nodes.Clear();
            root = new TreeNode(request.Name);
            root.Tag = request;
            treeView1.Nodes.Add(root);
            input = new TreeNode("input");
            output = new TreeNode("output");
            root.Nodes.Add(input);
            for(int i = 0; i < request.Params.Count; i++)
            {
                Param param = request.Params[i];
                TreeNode p = new TreeNode(param.Name);
                p.Tag = param;
                input.Nodes.Add(p);
            }
            root.Nodes.Add(output);
            TreeNode data = null;
            for(int i = 0; i < request.Results.Count; i++)
            {
                Param param = request.Results[i];
                TreeNode p = new TreeNode(param.Name);
                p.Tag = param;
                if (param.IsData)
                {
                    if(data == null)
                    {
                        data = new TreeNode("data");
                        output.Nodes.Add(data);
                    }
                    data.Nodes.Add(p);
                } else
                {
                    output.Nodes.Add(p);
                }
            }
            root.ExpandAll();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == null) return;
                this.contextMenuStrip1.Show(treeView1, e.X, e.Y);
            } else
            {
                if(e.Node != null)
                {
                    if(e.Node.Tag is Request)
                    {
                        global.PropertyGrid.SelectedObject = e.Node.Tag;
                    } else if(e.Node.Tag is Param)
                    {
                        global.PropertyGrid.SelectedObject = e.Node.Tag;
                    }
                }
            }
        }

        private void addParamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Param p = new Param();
            p.Name = "param";
            p.Type = "String";
            request.Params.Add(p);
            TreeNode n = new TreeNode(p.Name);
            n.Tag = p;
            input.Nodes.Add(n);
            input.ExpandAll();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(request != null)
            {
                String url = "http://127.0.0.1:8080" + request.Url+"/"+request.Name;
                String data = "";
                for(int i = 0; i < request.Params.Count; i++)
                {
                    Param param = request.Params[i];
                    if (i > 0)
                        data = data + "&";
                    data = data + param.Name + "=" + param.TestValue;
                }
                String response = "";
                try
                {
                    if (request.Type == "Get")
                    {
                        url = url + "?" + data;
                        response = HttpRequest(url, "", "GET");
                    }
                    else
                    {
                        response = HttpRequest(url, data, request.Type.ToUpper());
                    }
                    JObject jo = JObject.Parse(response);
                    parseResult(jo, output);
                } catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                
            }
        }
        private void parseResult(JObject obj,TreeNode output)
        {
            output.Nodes.Clear();
            request.Results.Clear();
            JToken token = obj.First;
            output.Nodes.Clear();
            TreeNode data = null;
            while (token != null)
            {
                var reader = token.CreateReader();
                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            continue;
                        }
                        System.Console.WriteLine(reader.Path + ":" + reader.TokenType);
                        TreeNode node = new TreeNode(reader.Path);
                        if (reader.Path.StartsWith("data["))
                        {
                            if (data == null)
                            {
                                data = new TreeNode("data");
                                output.Nodes.Add(data);
                                
                            }
                            int pos = reader.Path.IndexOf(".");
                            String name = reader.Path.Substring(pos + 1);
                            if (TreeViewTools.FindNode(output, name) == null)
                            {
                                TreeNode sub = new TreeNode(name);
                                data.Nodes.Add(sub);

                                Param param = new Param();
                                param.Name = name;
                                param.Type = reader.TokenType.ToString();
                                sub.Tag = param;
                                param.IsData = true;
                                request.Results.Add(param);
                                request.ResultType = 2;
                            }
                        } else if (reader.Path.StartsWith("data.")){
                            if (data == null)
                            {
                                data = new TreeNode("data");
                                output.Nodes.Add(data);
                            }
                            int pos = reader.Path.IndexOf(".");
                            TreeNode sub = new TreeNode(reader.Path.Substring(pos + 1));
                            data.Nodes.Add(sub);

                            Param param = new Param();
                            param.Name = reader.Path.Substring(pos + 1);
                            param.Type = reader.TokenType.ToString();
                            sub.Tag = param;
                            param.IsData = true;
                            request.Results.Add(param);
                            request.ResultType = 1;
                        }
                        else {
                            output.Nodes.Add(node);
                            Param param = new Param();
                            param.Name = reader.Path;
                            param.Type = reader.TokenType.ToString();
                            node.Tag = param;
                            param.IsData = false;
                            request.Results.Add(param);
                        }
                    }
                }


                TreeNode n = new TreeNode();
                Param p = new Param();
                p.Name = token.Path;

                token = token.Next;
            }
            output.ExpandAll();
        }

        public static string HttpRequest(string url, string data, string method = "PUT", string contentType = "application/x-www-form-urlencoded", Encoding encoding = null)
        {

            byte[] datas = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);//data可以直接传字节类型 byte[] data,然后这一段就可以去掉
            if (encoding == null)
                encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = method;
            request.Timeout = 150000;
            request.AllowAutoRedirect = false;
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }

            Stream requestStream = null;
            string responseStr = null;
            try
            {
                if (datas != null&&datas.Length >0)
                {
                    request.ContentLength = datas.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(datas, 0, datas.Length);
                    requestStream.Close();
                }
                else
                {
                    request.ContentLength = 0;
                }
                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    Stream getStream = webResponse.GetResponseStream();
                    byte[] outBytes = ReadFully(getStream);
                    getStream.Close();
                    responseStr = Encoding.UTF8.GetString(outBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
            }
            return responseStr;
        }

        public static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[512];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

    }
}
