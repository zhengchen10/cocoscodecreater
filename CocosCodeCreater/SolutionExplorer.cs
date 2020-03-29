using kernel;
using kernel.models;
using kernel.plist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public partial class SolutionExplorer : ToolWindow,IPropertyListener
    {
        private ProjectInfo project;
        public ProjectInfo Project { get { return project; } set { project = value; if (global != null) global.Project = value; } }
        public String FileName { get; set; }
        public String FilePath { get; set; }
        public TreeView TreeView { get { return treeView1; } }
        public ISolutionListener Listener { get; set; }
        private Global global;
        public SolutionExplorer(Global global)
        {
            InitializeComponent();
            Project = null;
            Listener = null;
            this.global = global;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == null) return;
                //treeView1.SelectedNode = e.Node;
                mainMenuStrip.Show(treeView1, e.X, e.Y);
            } else
            {
                treeView1.SelectedNode = e.Node;
                TreeNode node = e.Node;
                if(node.Parent == null)
                {
                    global.PropertyGrid.SelectedObject = Project;
                } else if(node.Parent.Text == "Scenes")
                {
                    Scene scene = (Scene)node.Tag;
                    global.PropertyGrid.SelectedObject = scene;
                    global.Main.SelectObject = scene;
                    global.SceneExplorer.Scene = scene;
                    global.ActionExplorer.Scene = scene;
                    //global.PropertyExplorer.Listener = this;
                } else if (node.Parent.Text == "Components")
                {
                    kernel.models.Component component = (kernel.models.Component)node.Tag;
                    global.PropertyGrid.SelectedObject = component;
                    global.Main.SelectObject = component;
                    global.SceneExplorer.Component = component;
                    global.ActionExplorer.Component = component;
                    //global.PropertyExplorer.Listener = this;
                } else if(node.Tag is PListInfo)
                {
                    global.Main.SelectObject = node.Tag;
                    global.PlistExplorer.PListInfo = (PListInfo)node.Tag;
                } else if(node.Tag is Resource)
                {
                    global.PropertyGrid.SelectedObject = node.Tag;
                } else if(node.Tag is Request)
                {
                    global.Main.SelectObject = (Request)node.Tag;
                    global.ApiExplorer.Request = (Request)node.Tag;
                    global.PropertyGrid.SelectedObject = node.Tag;
                }
            }
        }

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = 1;
            TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Scenes");
            if(node != null)
            {
                while (true)
                {
                    TreeNode n = TreeViewTools.FindNode(node, "Scene" + index);
                    if(n != null)
                    {
                        index++;
                    } else
                    {
                        n = new TreeNode("Scene" + index);
                        node.Nodes.Add(n);
                        node.ExpandAll();
                        Scene s = new kernel.models.Scene("Scene" + index);
                        n.Tag = s;
                        Project.Scenes.Add(s);
                        if (Listener != null)
                            Listener.onAddScene("Scene" + index);
                        break;
                    }
                }
            }
        }

        private void newComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = 1;
            TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Components");
            if (node != null)
            {
                while (true)
                {
                    TreeNode n = TreeViewTools.FindNode(node, "Component" + index);
                    if (n != null)
                    {
                        index++;
                    }
                    else
                    {
                        n = new TreeNode("Component" + index);
                        
                        node.Nodes.Add(n);
                        node.ExpandAll();
                        kernel.models.Component c = new kernel.models.Component("Component" + index);
                        n.Tag = c;
                        Project.Components.Add(c);
                        if (Listener != null)
                            Listener.onAddComponent("Component" + index);
                        break;
                    }
                }
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(treeView1.SelectedNode != null)
                {
                    TreeNode n = treeView1.SelectedNode.Parent;
                    if(n.Text == "Scenes")
                    {
                        String message = "确认要删除场景 " + treeView1.SelectedNode.Text +" 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            Scene scene = (Scene)treeView1.SelectedNode.Tag;
                            Project.RemoveSceneByUUID(scene.UUID);
                            if (Listener != null)
                                Listener.onDeleteScene(scene.UUID);
                            String name = treeView1.SelectedNode.Text;
                            n.Nodes.Remove(treeView1.SelectedNode);

                        }
                    } else if(n.Text == "Components")
                    {
                        String message = "确认要删除组件 " + treeView1.SelectedNode.Text + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            kernel.models.Component component = (kernel.models.Component)treeView1.SelectedNode.Tag;
                            Project.RemoveComponentByUUID(component.UUID);
                            if (Listener != null)
                                Listener.onDeleteComponent(component.UUID);
                            String name = treeView1.SelectedNode.Text;
                            n.Nodes.Remove(treeView1.SelectedNode);
                        }
                    }
                    else if (n.Text == "Images")
                    {
                        String message = "确认要删除资源 " + treeView1.SelectedNode.Text + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            kernel.models.Resource res = (kernel.models.Resource)treeView1.SelectedNode.Tag;
                            Project.RemoveImageByUUID(res.UUID);
                            if (Listener != null)
                                Listener.onDeleteImage(res.UUID);

                            String name = treeView1.SelectedNode.Text;
                            n.Nodes.Remove(treeView1.SelectedNode);

                        }
                    }
                    else if (n.Text == "Sounds")
                    {
                        String message = "确认要删除音频 " + treeView1.SelectedNode.Text + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            kernel.models.Resource res = (kernel.models.Resource)treeView1.SelectedNode.Tag;
                            Project.RemoveSoundByUUID(res.UUID);
                            if (Listener != null)
                                Listener.onDeleteSound(res.UUID);
                            String name = treeView1.SelectedNode.Text;
                            n.Nodes.Remove(treeView1.SelectedNode);
                        }
                    }
                    else if (n.Text == "Plists")
                    {
                        String message = "确认要删除 " + treeView1.SelectedNode.Text + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            kernel.models.Resource res = (kernel.models.Resource)treeView1.SelectedNode.Tag;
                            Project.RemovePlistByUUID(res.UUID);
                            if (Listener != null)
                                Listener.onDeletePlist(res.UUID);
                            String name = treeView1.SelectedNode.Text;
                            n.Nodes.Remove(treeView1.SelectedNode);

                        }
                    }
                }
            }
            else
            {
                if (global.Document != null)
                {
                    global.Document.DoKeyDown(e);
                }
            }
        }

        public void onPropertyChanged(object obj, string property, object oldValue, object newValue)
        {
            if (obj is Scene)
            {
                if (property == "Name")
                {
                    TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], (string)oldValue);
                    node.Text = (string)newValue;
                }
                if (property == "ScreenType")
                {

                }
            }
            else if (obj is kernel.models.ComponentRef)
            {
            }
            else if (obj is kernel.models.Component)
            {
                if (property == "Name")
                {
                    TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], (string)oldValue);
                    node.Text = (string)newValue;
                }
            }
            else if(obj is Request)
            {
                if (property == "Name")
                {
                    TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], (string)oldValue,obj.GetType());
                    node.Text = (string)newValue;
                }
            }
        }

   

        private void newResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpeg;*.jpg;*.png|" +
                            "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphics (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Resources");
                if(node != null)
                {
                    TreeNode resNode = new TreeNode(fi.Name);
                    resNode.Tag = dialog.FileName;
                    node.Nodes.Add(resNode);
                    node.ExpandAll();
                    if (Listener != null)
                        Listener.onAddResource(fi.Name, dialog.FileName);
                    Resource r = new Resource(dialog.FileName);
                    Project.Resources.Add(r);
                    resNode.Tag = r;
                }
            }*/
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpeg;*.jpg;*.png|" +
                            "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphics (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Images");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(fi.Name);
                    resNode.Tag = dialog.FileName;
                    node.Nodes.Add(resNode);
                    node.ExpandAll();
                    if (Listener != null)
                        Listener.onAddImage(fi.Name, dialog.FileName);
                    Resource r = new Resource(dialog.FileName);
                    r.Type = "Image";
                    Project.Images.Add(r);
                    resNode.Tag = r;
                }
            }
        }

        private void addFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Font Files|*.ttf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Fonts");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(fi.Name);
                    resNode.Tag = dialog.FileName;
                    node.Nodes.Add(resNode);
                    node.ExpandAll();
                    if (Listener != null)
                        Listener.onAddFont(fi.Name, dialog.FileName);
                    Resource r = new Resource(dialog.FileName);
                    r.Type = "Font";
                    Project.Fonts.Add(r);
                    resNode.Tag = r;
                }
            }
        }

        private void addSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Mp3 Files|*.mp3";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Sounds");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(fi.Name);
                    resNode.Tag = dialog.FileName;
                    node.Nodes.Add(resNode);
                    node.ExpandAll();
                    if (Listener != null)
                        Listener.onAddSound(fi.Name, dialog.FileName);
                    Resource r = new Resource(dialog.FileName);
                    r.Type = "Sound";
                    Project.Sounds.Add(r);
                    resNode.Tag = r;
                }
            }
        }

        private void addPlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Plist Files|*.plist";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Plists");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(fi.Name);
                   
                    node.Nodes.Add(resNode);
                    node.ExpandAll();
                    if (Listener != null)
                        Listener.onAddPlist(fi.Name, dialog.FileName);
                    PListInfo pListInfo = PListTools.readPlist(dialog.FileName);
                    //Resource r = new Resource(dialog.FileName);
                    pListInfo.Type = "Plist";
                    Project.Plists.Add(pListInfo);
                    resNode.Tag = pListInfo;
                }
            }
        }

        private void newPlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPlistDialog dialog = new NewPlistDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Plists");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(dialog.PlistName);
                    node.Nodes.Add(resNode);
                    if (Listener != null)
                        Listener.onAddPlist(dialog.PlistName, global.Project.WorkPath +"\\temp\\"+ dialog.PlistName);
                    PListInfo pListInfo = PListTools.newPlist(global.Project.WorkPath + "\\temp\\" + dialog.PlistName,dialog.PlistWidth,dialog.PlistHeight);
                    pListInfo.WorkPath = global.Project.WorkPath + "\\temp\\";
                    Project.Plists.Add(pListInfo);
                    resNode.Tag = pListInfo;
                }
            }
        }

        private void 添加APIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApiForm apiForm = new ApiForm();
            if(apiForm.ShowDialog() == DialogResult.OK)
            {
                TreeNode node = TreeViewTools.FindNode(treeView1.Nodes[0], "Apis");
                if (node != null)
                {
                    TreeNode resNode = new TreeNode(apiForm.ApiName);
                    node.Nodes.Add(resNode);
                    Request request = new Request(apiForm.ApiName, apiForm.ApiUrl);
                    global.Project.Requests.Add(request);
                    resNode.Tag = request;
                }
                
            }
        }
    }
}
