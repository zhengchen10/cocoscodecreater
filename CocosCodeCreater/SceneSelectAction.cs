using kernel.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public class SceneSelectAction : IAction
    {
        Pen pen1 = new Pen(Color.FromArgb(0, 0, 255), 1);
        SceneEditInfos seInfo;
        Global global;
        public SceneSelectAction(Global global,List<Node> selectNode, SceneEditInfos seInfo)
        {
            this.global = global;
            this.ActiveNodes = selectNode;
            this.seInfo = seInfo;
        }

        public List<Node> ActiveNodes { get; set ; }

        public bool OnMouseClick(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseDown(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseMove(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseUp(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseWheel(MouseEventArgs e)
        {
            return false;
        }

        public void OnPaint(Graphics g, Rectangle rect)
        {
            //Rectangle nodeRect = NodeTools.getAllNodeRect(global, ActiveNodes);// ActiveNode.getNodeDisplayRect();
            //if(ActiveNode.Type == "ComponentRef")
            //{
            //    nodeRect = ActiveNode.getCompentShowRect(global.Project);
            //}
            //DrawTools.DrawRect(g, pen1, new Rectangle(nodeRect.X,nodeRect.Y, nodeRect.Width, nodeRect.Height),  seInfo);
            for (int i = 0; i < ActiveNodes.Count; i++)
            {
                kernel.models.Node node = ActiveNodes[i];
                Rectangle r = ActiveNodes[i].getNodeDisplayRect();// new RectangleF(ActiveNode.Location.X, ActiveNode.Location.Y, ActiveNode.Size.Width, ActiveNode.Size.Height);
                if (ActiveNodes[i].Type == "ComponentRef")
                {
                    r = ActiveNodes[i].getCompentShowRect(global.Project);
                }
                DrawTools.DrawRect(g, pen1, new Rectangle(r.X, r.Y, r.Width, r.Height), seInfo);
            }
        }

    }
}
