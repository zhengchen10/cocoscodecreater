using kernel.extends;
using kernel.models;
using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public partial class SceneExplorer : ToolWindow, IPropertyListener
    {
        private Global global;
        private NodeContainer rootContainer;
        public Scene Scene { get { if(rootContainer is Scene)
                    return (Scene)rootContainer;
                return null;
            } set { reloadScene(value,null); } }
        public kernel.models.Component Component {
            get
            {
                if (rootContainer is kernel.models.Component)
                    return (kernel.models.Component)rootContainer;
                return null;
            }
            set { reloadComponent(value,null); } }
        private TreeNode copyNode = null;
        private TreeNode currentNode;
        private TreeNode root;
        private void reloadScene(Scene value,string selectNode)
        {
            List<string> nodeList = new List<string>();
            copyNode = null;
            rootContainer = value;
            treeView1.Nodes.Clear();
            root = new TreeNode(value.Name);
            root.Tag = rootContainer;
            treeView1.Nodes.Add(root);
            for(int i = 0; i < rootContainer.Children.Count; i++)
            {
                Node node = rootContainer.FindNodeByUUID(rootContainer.Children[i]);
                nodeList.Add(node.Name);
                TreeNode n = new TreeNode(node.Name);
                n.Tag = node.UUID;
                if(node.UUID == selectNode)
                {
                    currentNode = n;
                }
                root.Nodes.Add(n);
                if(node.Type == "Layer")
                {
                    //loadLayer(n, (Layer)node,selectNode);
                }
            }
            root.ExpandAll();
            if (selectNode != null)
                treeView1.SelectedNode = currentNode;

            NodeListProperty.nodeItems = nodeList.ToArray();
            initComponentMenu();
        }

        private void initComponentMenu()
        {
            this.addComponentToolStripMenuItem.DropDownItems.Clear();
            if(this.global.Project.Components.Count > 0)
            {
                System.Windows.Forms.ToolStripItem[] subItems = new System.Windows.Forms.ToolStripItem[this.global.Project.Components.Count];
                for (int i = 0; i < this.global.Project.Components.Count; i++)
                {
                    ToolStripMenuItem toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    toolStripMenuItem.Tag = this.global.Project.Components[i];
                    toolStripMenuItem.Name = this.global.Project.Components[i].Name;
                    toolStripMenuItem.Size = new System.Drawing.Size(180, 22);
                    toolStripMenuItem.Text = this.global.Project.Components[i].Name;
                    subItems[i] = toolStripMenuItem;
                    toolStripMenuItem.Click += ToolStripMenuItem_Click;
                }
                this.addComponentToolStripMenuItem.DropDownItems.AddRange(subItems);
            }            
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kernel.models.Component component = (kernel.models.Component)((ToolStripMenuItem)sender).Tag;
            String name = getNodeName(component.Name);
            TreeNode n = new TreeNode(name);
            kernel.models.ComponentRef s = new kernel.models.ComponentRef(name);
            s.Size = new Size(component.Size.Width,component.Size.Height);
            s.RefName = component.Name;
            s.RefUUID = component.UUID;
            currentNode.Nodes.Add(n);
            currentNode.ExpandAll();
            if (currentNode.Tag == rootContainer)
            {
                rootContainer.AddChildren(s);
            }
            else
            {
                Node layer = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                if (layer.Type == "Layer")
                {
                    ((Layer)layer).AddChildren(s);
                }
                else
                {
                    rootContainer.AddChildren(s);
                }
            }
            n.Tag = s.UUID;
        }

        public void RemoveTreeNode(String uuid)
        {
            RemoveTreeNode(root, uuid);
        }
        private bool RemoveTreeNode(TreeNode node,String uuid)
        {
            for(int i = 0; i < node.Nodes.Count; i++)
            {
                if(node.Nodes[i].Tag == uuid)
                {
                    node.Nodes.RemoveAt(i);
                    treeView1.SelectedNode = null;
                    return true;
                }
                if(node.Nodes[i].Nodes.Count > 0)
                {
                    bool result = RemoveTreeNode(node.Nodes[i], uuid);
                    if (result)
                        return true;
                }
            }
            return false;
        }

        private void reloadComponent(kernel.models.Component value, string selectNode)
        {
            List<string> nodeList = new List<string>();
            rootContainer = value;
            treeView1.Nodes.Clear();
            root = new TreeNode(value.Name);
            root.Tag = rootContainer;
            treeView1.Nodes.Add(root);
            for (int i = 0; i < rootContainer.Children.Count; i++)
            {
                Node node = rootContainer.FindNodeByUUID(rootContainer.Children[i]);
                nodeList.Add(node.Name);
                TreeNode n = new TreeNode(node.Name);
                n.Tag = node.UUID;
                if (node.UUID == selectNode)
                {
                    currentNode = n;
                }
                root.Nodes.Add(n);
            }
            root.ExpandAll();
            NodeListProperty.nodeItems = nodeList.ToArray();
            if (selectNode != null)
                treeView1.SelectedNode = currentNode;
        }

        private void loadLayer(TreeNode root, Layer layer, string selectNode)
        {
            for (int i = 0; i < layer.Children.Count; i++)
            {
                Node node = layer.FindNodeByUUID(layer.Children[i]);
                TreeNode n = new TreeNode(node.Name);
                n.Tag = node.UUID;
                if (node.UUID == selectNode)
                {
                    currentNode = n;
                }
                root.Nodes.Add(n);
            }
        }

        public SceneExplorer(Global global)
        {
            this.global = global;
            InitializeComponent();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == null) return;
                //treeView1.SelectedNode = e.Node;
                if(rootContainer is Scene)
                    this.contextMenuStrip.Show(treeView1, e.X, e.Y);
                else
                    this.contextMenuStrip1.Show(treeView1, e.X, e.Y);
            } else
            {
                treeView1.SelectedNode = e.Node;
                TreeNode node = e.Node;
                if(e.Node.Tag == rootContainer)
                {
                    global.PropertyGrid.SelectedObject = rootContainer;
                }
                else
                {
                    if (node.Tag != null)
                    {
                        global.PropertyGrid.SelectedObject = rootContainer.FindNodeByUUID((String)node.Tag);
                        global.SceneExplorerListener.onSelectItem(global.PropertyGrid.SelectedObject);
                    }
                }
            }
        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = getNodeName("Layer");
            TreeNode n = new TreeNode(name);
            Layer s = new Layer(name);
            currentNode.Nodes.Add(n);
            currentNode.ExpandAll();
            // scene.Sprites.Add(s);
            rootContainer.AddChildren(s);
            n.Tag = s.UUID;
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = getNodeName("Sprite");
            TreeNode n = new TreeNode(name);
            Sprite s = new Sprite(name);
            currentNode.Nodes.Add(n);
            currentNode.ExpandAll();
            if(currentNode.Tag == rootContainer)
            {
                rootContainer.AddChildren(s);
            } else
            {
                Node layer = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                if(layer.Type == "Layer")
                {
                    ((Layer)layer).AddChildren(s);
                } else
                {
                    rootContainer.AddChildren(s);
                }
            }
           // scene.Sprites.Add(s);
            
            n.Tag = s.UUID;
        }
        private void addSpriteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addSpriteToolStripMenuItem_Click(sender, e);
        }

        private void addLabelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addLabelToolStripMenuItem_Click(sender, e);
        }

        private void addLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = getNodeName("Label");
            TreeNode n = new TreeNode(name);
            kernel.models.Label s = new kernel.models.Label(name);
            currentNode.Nodes.Add(n);
            currentNode.ExpandAll();
            if (currentNode.Tag == rootContainer)
            {
                rootContainer.AddChildren(s);
            }
            else
            {
                Node layer = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                if (layer.Type == "Layer")
                {
                    ((Layer)layer).AddChildren(s);
                }
                else
                {
                    rootContainer.AddChildren(s);
                }
            }
            n.Tag = s.UUID;
        }

        private string getNodeName(string v)
        {
            int index = 1;
            while (true)
            {
                String name = v + index;
                Node node = rootContainer.FindNodeByName(name);
                if(node == null)
                {
                    return name;
                }
                
                index++;
            }
        }

        private void addSpriteOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addSpriteScale9ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void addButtonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addButtonToolStripMenuItem_Click(sender, e);
        }

        private void addButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = getNodeName("Button");
            TreeNode n = new TreeNode(name);
            kernel.models.Button s = new kernel.models.Button(name);
            currentNode.Nodes.Add(n);
            currentNode.ExpandAll();
            if (currentNode.Tag == rootContainer)
            {
                rootContainer.AddChildren(s);
            }
            else
            {
                Node layer = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                if (layer.Type == "Layer")
                {
                    ((Layer)layer).AddChildren(s);
                }
                else
                {
                    rootContainer.AddChildren(s);
                }
            }
            // scene.Sprites.Add(s);
            n.Tag = s.UUID;
        }

        public void onPropertyChanged(object obj, string property, object oldValue, object newValue)
        {
            if (obj == rootContainer)
            {
                if (property == "Name")
                {
                    root.Text = (String)newValue;
                }
            }
            else if (obj is kernel.models.Sprite)
            {
                if (property == "SpriteFrame")
                {
                    Resource r = global.Project.GetImage((String)newValue);
                    System.Drawing.Image image = System.Drawing.Image.FromFile(r.Path);
                    kernel.models.Sprite sprite = (kernel.models.Sprite)obj;
                    sprite.Size = new Size(image.Width, image.Height);
                    image.Dispose();
                }
                global.Document.Refresh();
            }
            else if (obj is kernel.models.Button)
            {
                if (property == "NormalSprite")
                {
                    Resource r = global.Project.GetImage((String)newValue);
                    System.Drawing.Image image = System.Drawing.Image.FromFile(r.Path);
                    kernel.models.Button button = (kernel.models.Button)obj;
                    if (button.Size.Width == 0)
                        button.Size = new Size(image.Width, image.Height);
                    if (button.PressedSprite == "")
                        button.PressedSprite = (String)newValue;
                    if (button.DisableSprite == "")
                        button.DisableSprite = (String)newValue;
                    image.Dispose();
                }
                global.Document.Refresh();
            }
            else if (obj is MoveObject)
            {
                if (property == "NormalSprite")
                {
                    
                }
                    
            }

            if (currentNode != null )
            {
                if(obj is Node && currentNode.Tag == ((Node)obj).UUID)
                {
                    if (property == "Name")
                    {
                        currentNode.Text = (string)newValue;
                    }
                }
            }
            //throw new NotImplementedException();
        }

        private void SceneExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            global.Main.OnExplorerClosed("Scene");
            e.Cancel = true;
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (currentNode != null && currentNode.Tag != rootContainer)
                {
                    String message = "确认要删除 " + treeView1.SelectedNode.Text + " 吗?";
                    if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Node node = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                        rootContainer.RemoveNode(node);
                        currentNode.Parent.Nodes.Remove(currentNode);
                        currentNode = null;
                        global.Document.Refresh();
                    }
                }
            } else if (e.Control && e.KeyCode == Keys.Up)
            {
                if (currentNode != null && currentNode.Tag != rootContainer)
                {
                    if (rootContainer.MoveUp((string)currentNode.Tag))
                    {
                        if (rootContainer is Scene)
                        {
                            reloadScene((Scene)rootContainer, (string)currentNode.Tag);
                        }
                        else
                        {
                            reloadComponent((kernel.models.Component)rootContainer, (string)currentNode.Tag);
                        }
                    }
                    global.Document.Refresh();
                }
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                if (currentNode != null && currentNode.Tag != rootContainer)
                {
                    if (rootContainer.MoveDown((string)currentNode.Tag))
                    {
                        if (rootContainer is Scene)
                        {
                            reloadScene((Scene)rootContainer, (string)currentNode.Tag);
                        }
                        else
                        {
                            reloadComponent((kernel.models.Component)rootContainer, (string)currentNode.Tag);
                        }
                    }
                    global.Document.Refresh();
                }
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                if(currentNode != root)
                {
                    copyNode = currentNode;
                }
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                if(copyNode != null)
                {
                    String uuid = (String)copyNode.Tag;
                    Node node = rootContainer.FindNodeByUUID(uuid);
                    if(node.Type == "Sprite")
                    {
                        Sprite sprite = new Sprite(node.Name);
                        sprite.clone(node);
                        addNode(copyNode.Parent, sprite);
                    } else if(node.Type == "Label")
                    {
                        kernel.models.Label sprite = new kernel.models.Label(node.Name);
                        sprite.clone(node);
                        addNode(copyNode.Parent, sprite);
                    }
                    else if (node.Type == "Button")
                    {
                        kernel.models.Button sprite = new kernel.models.Button(node.Name);
                        sprite.clone(node);
                        addNode(copyNode.Parent, sprite);
                    }
                    else if(node.Type == "ComponentRef")
                    {
                        ComponentRef sprite = new ComponentRef(node.Name);
                        sprite.clone(node);
                        addNode(copyNode.Parent,sprite);
                    }
                }
            }
            else
            {
                /*if(global.Document.Editer != null)
                {
                    global.Document.Editer.OnKeyDown(e);
                }*/
                if (global.Document != null)
                {
                    global.Document.DoKeyDown(e);
                }
            }
        }
        private void addNode(TreeNode parent,Node node)
        {
            TreeNode n = new TreeNode(node.Name);
            n.Tag = node.UUID;
            parent.Nodes.Add(n);
            if (parent.Tag == rootContainer)
            {
                rootContainer.AddChildren(node);
            }
            else
            {
                Node layer = rootContainer.FindNodeByUUID((String)currentNode.Tag);
                if (layer.Type == "Layer")
                {
                    ((Layer)layer).AddChildren(node);
                }
                else
                {
                    rootContainer.AddChildren(node);
                }
            }
        }
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        
    }
}
