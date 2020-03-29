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
using kernel.actions;

namespace CocosCodeCreater
{
    public partial class ActionExplorer : ToolWindow, IPropertyListener
    {
        private Global global;
        private NodeContainer rootContainer;

        private TreeNode currentNode;
        private TreeNode root;

        public Scene Scene
        {
            get
            {
                if (rootContainer is Scene)
                    return (Scene)rootContainer;
                return null;
            }
            set { reload(value, null); }
        }
        public kernel.models.Component Component
        {
            get
            {
                if (rootContainer is kernel.models.Component)
                    return (kernel.models.Component)rootContainer;
                return null;
            }
            set { reload(value, null); }
        }

        public ActionExplorer(Global global)
        {
            this.global = global;
            InitializeComponent();
        }

        
        private void reload(NodeContainer value, string selectNode)
        {
            if(rootContainer != value)
            {
                rootContainer = value;
                treeView1.Nodes.Clear();
                root = new TreeNode(value.Name);
                root.Tag = rootContainer;
                treeView1.Nodes.Add(root);
                for(int i = 0; i < value.Actions.Count; i++)
                {
                    ActionOwner action = value.Actions[i];
                    Node n = rootContainer.FindNodeByUUID(action.ActionNodeUUID);
                    if(n != null)
                    {
                        if (n.Name != action.ActionNode)
                            action.ActionNode = n.Name;
                    } else
                    {
                        action.ActionNode = null;
                        action.ActionNodeUUID = "";
                    }
                    TreeNode node = new TreeNode(action.Name);
                    node.Tag = action;
                    root.Nodes.Add(node);
                    if(action.Action != null)
                    {
                        AbstractAction aa = createAction(action.Action);
                        if(aa != null)
                        {
                            TreeNode actionNode = new TreeNode(aa.GetName());
                            actionNode.Tag = aa;
                            node.Nodes.Add(actionNode);
                            loadActionItems(actionNode, action.Action);
                        }
                    }
                }
                root.ExpandAll();
            }
            if (selectNode != null)
                treeView1.SelectedNode = currentNode;
        }
        private AbstractAction createAction(ActionItem item)
        {
            if(item.Type == "sequence")
            {
                return new SequenceAction(item);
            } else if(item.Type == "delayTime")
            {
                return new DelayTimeAction(item);
            }
            else if (item.Type == "moveBy")
            {
                return new MoveByAction(item);
            }
            else if(item.Type == "moveTo")
            {
                return new MoveToAction(item);
            } else if(item.Type == "callFunc")
            {
                return new CallFuncAction(item);
            }
            else if (item.Type == "fadeIn")
            {
                return new FadeInAction(item);
            }
            else if (item.Type == "fadeOut")
            {
                return new FadeOutAction(item);
            }
            else if (item.Type == "repeat")
            {
                return new RepeatAction(item);
            }
            else if (item.Type == "repeatForever")
            {
                return new RepeatForeverAction(item);
            }
            else if (item.Type == "rotateBy")
            {
                return new RotateByAction(item);
            }
            else if (item.Type == "rotateTo")
            {
                return new RotateToAction(item);
            }
            else if (item.Type == "scaleBy")
            {
                return new ScaleByAction(item);
            }
            else if (item.Type == "scaleTo")
            {
                return new ScaleToAction(item);
            }
            else if (item.Type == "spawn")
            {
                return new SpawnAction(item);
            }
            return null;
        }
        private void loadActionItems(TreeNode actionNode, ActionItem action)
        {
            for(int i = 0; i < action.SubActions.Count; i++)
            {
                AbstractAction aa = createAction(action.SubActions[i]);
                if(aa != null)
                {
                    TreeNode node = new TreeNode(aa.GetName());
                    node.Tag = aa;
                    actionNode.Nodes.Add(node);
                    loadActionItems(node, action.SubActions[i]);
                }
            }
        }

        private void addActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionOwner action = new ActionOwner();
            action.Name = getNodeName("action");
            rootContainer.Actions.Add(action);
            TreeNode n = new TreeNode(action.Name);
            n.Tag = action;
            root.Nodes.Add(n);
            root.ExpandAll();
        }

        private string getNodeName(string v)
        {
            int index = 1;
            while (true)
            {
                String name = v + index;
                Node node = rootContainer.FindNodeByName(name);
                if (node == null)
                {
                    return name;
                }

                index++;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == null) return;
                if(e.Node.Tag == rootContainer)
                {
                    this.contextMenuStrip1.Show(treeView1, e.X, e.Y);
                } else
                {
                    if (e.Node.Tag is ActionOwner) {
                        if (((ActionOwner)e.Node.Tag).Action == null)
                            this.contextMenuStrip2.Show(treeView1, e.X, e.Y);
                    }
                    else if(e.Node.Tag is AbstractAction)
                    {
                        if(e.Node.Tag is RepeatAction || e.Node.Tag is RepeatForeverAction)
                        {
                            AbstractAction aa = (AbstractAction)e.Node.Tag;
                            if(aa.Action.SubActions.Count == 0)
                            {
                                this.contextMenuStrip3.Show(treeView1, e.X, e.Y);
                            }
                        }
                        else if(e.Node.Tag is SequenceAction || e.Node.Tag is SpawnAction)
                        {
                            this.contextMenuStrip2.Show(treeView1, e.X, e.Y);
                        }
                    }
                }
            }
            else
            {
                treeView1.SelectedNode = e.Node;
                TreeNode node = e.Node;
                if(global.PropertyGrid.SelectedObject != e.Node.Tag)
                {
                    global.PropertyGrid.SelectedObject = e.Node.Tag;
                }
                
                /*if (e.Node.Tag == rootContainer)
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
                }*/
            }
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
            else if (obj is ActionOwner)
            {
                if (property == "Name")
                {
                    for(int i = 0; i < root.Nodes.Count; i++)
                    {
                        TreeNode node = root.Nodes[i];
                        if(node.Tag == obj)
                        {
                            node.Text = (String)newValue;
                        }
                    }
                }else if(property == "ActionNode")
                {
                    Node node = rootContainer.FindNodeByName((String)newValue);
                    ActionOwner owner = (ActionOwner)obj;
                    owner.ActionNodeUUID = node.UUID;
                }
            } else if(obj is AbstractAction)
            {
                TreeNode node = TreeViewTools.FindNodeByTag(root, obj);
                node.Text = ((AbstractAction)obj).GetName();
            }
        }

        private void addSubAction(AbstractAction actionItem)
        {
            if (currentNode.Tag is ActionOwner)
            {
                ActionOwner action = (ActionOwner)currentNode.Tag;
                action.Action = actionItem.Action;

            }
            else if (currentNode.Tag is AbstractAction)
            {
                AbstractAction item = (AbstractAction)currentNode.Tag;
                item.Action.SubActions.Add(actionItem.Action);
            }
            TreeNode node = new TreeNode(actionItem.GetName());
            node.Tag = actionItem;
            currentNode.Nodes.Add(node);
            currentNode.Expand();
        }

        private void sequenceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addSubAction( new SequenceAction(new ActionItem("sequence")));
        }

        private void delayTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new DelayTimeAction(new ActionItem("delayTime"),1.0f));
        }
        private void moveByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new MoveByAction(new ActionItem("moveBy"),1.0f,0.0f,0.0f,0.0f));
        }

        private void moveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new MoveToAction(new ActionItem("moveTo"), 1.0f, 0.0f, 0.0f, 0.0f));
        }

        private void scaleToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new ScaleToAction(new ActionItem("scaleTo"),1.0f,1.0f,1.0f,1.0f));
        }

        private void scaleByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new ScaleByAction(new ActionItem("scaleBy"), 1.0f, 1.0f, 1.0f, 1.0f));
        }
        private void rotateByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new RotateByAction(new ActionItem("rotateBy"), 1.0f, 0.0f, 0.0f, 0.0f));
        }
        private void rotateToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new RotateToAction(new ActionItem("rotateTo"), 1.0f, 0.0f, 0.0f, 0.0f));
        }

        private void fadeInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new FadeInAction(new ActionItem("fadeIn"),1.0f));
        }

        private void fadeOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new FadeOutAction(new ActionItem("fadeOut"), 1.0f));
        }

        private void callFuncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new CallFuncAction(new ActionItem("callFunc")));
        }

        private void spawnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addSubAction(new SpawnAction(new ActionItem("spawn")));
        }

        private void repeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new RepeatAction(new ActionItem("repeat"),5));
        }
        private void repeatForeverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new RepeatForeverAction(new ActionItem("repeatForever")));
        }

       
        

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (currentNode != null && currentNode.Tag != rootContainer)
                {
                    if(currentNode.Tag is ActionOwner)
                    {
                        ActionOwner owner = (ActionOwner)currentNode.Tag;
                        String message = "确认要删除 " + treeView1.SelectedNode.Text + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            rootContainer.RemoveActionByName(owner.Name);
                            currentNode.Parent.Nodes.Remove(currentNode);
                            currentNode = null;
                            global.PropertyGrid.SelectedObject = null;
                            //global.Document.Refresh();
                        }
                    } else if(currentNode.Tag is AbstractAction)
                    {
                        AbstractAction owner = (AbstractAction)currentNode.Tag;
                        String message = "确认要删除 " + owner.GetName() + " 吗?";
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            AbstractAction action = (AbstractAction)currentNode.Tag;
                            rootContainer.RemoveActionItemByUUID(action.Action.UUID);
                            currentNode.Parent.Nodes.Remove(currentNode);
                            currentNode = null;
                            global.PropertyGrid.SelectedObject = null;
                        }
                    }
                    
                }
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {

            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {

            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                if (currentNode != root)
                {
                //    copyNode = currentNode;
                }
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                //if (copyNode != null)
                {

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

        private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new SequenceAction(new ActionItem("sequence")));
        }

        private void spawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSubAction(new SpawnAction(new ActionItem("spawn")));
        }
    }
}
