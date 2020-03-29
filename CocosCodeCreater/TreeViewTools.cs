using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public class TreeViewTools
    {
        public static TreeNode FindNode(TreeNode node, string name)
        {
            //接受返回的节点
            TreeNode ret = null;
            //循环查找
            foreach (TreeNode temp in node.Nodes)
            {
                //是否有子节点
                if (temp.Nodes.Count != 0)
                {
                    //如果找到
                    if ((ret = FindNode(temp, name)) != null)
                    {
                        //MessageBox.Show(string.Format("找到，深度{0},索引{1}", ret.Level, ret.Index));
                        return ret;
                    }
                }
                //如果找到
                if (string.Equals(temp.Text, name))
                {
                    return temp;
                }
            }
            return ret;
        }

        public static TreeNode FindNodeByTag(TreeNode node,object obj)
        {
            TreeNode ret = null;
            //循环查找
            foreach (TreeNode temp in node.Nodes)
            {
                //是否有子节点
                if (temp.Nodes.Count != 0)
                {
                    //如果找到
                    if ((ret = FindNodeByTag(temp, obj)) != null)
                    {
                        return ret;
                    }
                }
                //如果找到
                if (temp.Tag == obj)
                {
                    return temp;
                }
            }
            return ret;
        }
        public static TreeNode FindNode(TreeNode node, string name,Type type)
        {
            //接受返回的节点
            TreeNode ret = null;
            //循环查找
            foreach (TreeNode temp in node.Nodes)
            {
                //是否有子节点
                if (temp.Nodes.Count != 0)
                {
                    //如果找到
                    if ((ret = FindNode(temp, name,type)) != null)
                    {
                        //MessageBox.Show(string.Format("找到，深度{0},索引{1}", ret.Level, ret.Index));
                        return ret;
                    }
                }
                //如果找到
                if (string.Equals(temp.Text, name) && temp.Tag.GetType() == type)
                {
                    return temp;
                }
            }
            return ret;
        }
    }
}
