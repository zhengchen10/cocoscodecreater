
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
    public partial class PlistExplorer : ToolWindow
    {
        Global global;
        private PListInfo plistInfo;
        public PListInfo PListInfo { get { return plistInfo; } set { reloadPListInfo(value); } }
        private TreeNode currentNode;
        private TreeNode root;
        private void reloadPListInfo(PListInfo value)
        {
            this.plistInfo = value;
            treeView1.Nodes.Clear();
            root = new TreeNode(value.Name);
            root.Tag = value;
            treeView1.Nodes.Add(root);
            for (int i = 0; i < plistInfo.Frames.Count; i++)
            {
                TreeNode n = new TreeNode(plistInfo.Frames[i].Name);
                n.Tag = plistInfo.Frames[i];
                //n.Tag = scene.FindObject(scene.Children[i]);
                root.Nodes.Add(n);
            }
            root.ExpandAll();
        }

        public PlistExplorer(Global global)
        {
            this.global = global;
            InitializeComponent();
        }

        private void PlistExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.global.Main.OnExplorerClosed("Plist");
            e.Cancel = true;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == null) return;
                //treeView1.SelectedNode = e.Node;
                if(PListInfo.WorkPath != null && PListInfo.WorkPath.Length >0)
                    this.contextMenuStrip.Show(treeView1, e.X, e.Y);
            }
            else
            {
                treeView1.SelectedNode = e.Node;
                TreeNode node = e.Node;
                if (node.Tag != null)
                {
                    global.PropertyGrid.SelectedObject = node.Tag;
                    global.PListExplorerListener.onSelectItem(node.Tag);
                }
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Image Files|*.jpeg;*.jpg;*.png|" +
                            "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphics (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for(int i = 0; i < dialog.FileNames.Length; i++)
                {
                    FileInfo fi = new FileInfo(dialog.FileNames[i]);

                    TreeNode resNode = new TreeNode(fi.Name);
                    resNode.Tag = dialog.FileNames[i];
                    root.Nodes.Add(resNode);
                    ImageInfo imageInfo = new ImageInfo();
                    imageInfo.SourceFile = dialog.FileNames[i];
                    imageInfo.Name = fi.Name;
                    System.Drawing.Image image = System.Drawing.Image.FromFile(dialog.FileNames[i]);
                    imageInfo.Offset = new Point(0, 0);
                    imageInfo.Rotated = false;
                    //imageInfo.Frame =  new Rectangle(0, 0, image.Width, image.Height);
                    imageInfo.Frame = PListTools.getNextPosition(plistInfo, image.Width, image.Height);
                    imageInfo.SourceColorRect = new Rectangle(0, 0, image.Width, image.Height);
                    imageInfo.SourceSize = new Size(image.Width, image.Height);
                    image.Dispose();

                    resNode.Tag = imageInfo;
                    plistInfo.Frames.Add(imageInfo);
                    global.Document.Refresh();
                }
                
                
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (global.Document != null)
            {
                global.Document.DoKeyDown(e);
            }
        }
    }
}
