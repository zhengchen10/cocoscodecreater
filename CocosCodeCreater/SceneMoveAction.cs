using kernel.extends;
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
    public class SceneMoveAction : IAction, IRefreshDocument
    {
        Pen pen1 = new Pen(Color.FromArgb(0, 0, 255), 1);
        //Node node;
        SceneEditInfos seInfo;
        Global global;
        Point selectPoint;
        List<PointF> startPoints;
        List<Point> startLocations;
        bool isSelect = false;

        public SceneMoveAction(Global global, List<Node> node,SceneEditInfos seInfo)
        {
            this.global = global;
            this.ActiveNodes = node;
            this.seInfo = seInfo;
        }

        public List<Node> ActiveNodes { get; set ; }

        public bool OnMouseClick(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseDown(MouseEventArgs e)
        {
            //RectangleF rect = ActiveNode.getNodeDisplayRect();// new RectangleF(ActiveNode.Location.X, ActiveNode.Location.Y, ActiveNode.Size.Width, ActiveNode.Size.Height);
            //if(ActiveNode.Type == "ComponentRef")
            //{
            //    rect = ActiveNode.getCompentShowRect(global.Project);
            //}
            RectangleF rect = NodeTools.getAllNodeRect(global, ActiveNodes);
            PointF pt = TransformTools.D2R(e.Location, seInfo);
            if (rect.Contains(pt))
            {
                selectPoint = e.Location;
                startLocations = new List<Point>();
                startPoints = new List<PointF>();
                for (int i=0;i< ActiveNodes.Count; i++)
                {
                    startLocations.Add(ActiveNodes[i].Location);
                    startPoints.Add(TransformTools.R2D(new PointF(ActiveNodes[i].Location.X, ActiveNodes[i].Location.Y), seInfo));
                }
                
                

                isSelect = true;
                return true;
            }
            isSelect = false;
            return false;
        }

        

        public bool OnMouseMove(MouseEventArgs e)
        {
            if (isSelect)
            {
                
                //PointF newLocation = new PointF(e.Location.X - selectPoint.X + startPoint.X,
                //     e.Location.Y - selectPoint.Y + startPoint.Y);
                
                //PointF offset = TransformTools.D2R(newLocation, seInfo);
                //ActiveNode.Location = new Point((int)offset.X,(int)offset.Y);

                for(int i = 0; i < ActiveNodes.Count; i++)
                {
                    PointF newLocation = new PointF(e.Location.X - selectPoint.X + startPoints[i].X,
                         e.Location.Y - selectPoint.Y + startPoints[i].Y);

                    PointF offset = TransformTools.D2R(newLocation, seInfo);
                    ActiveNodes[i].Location = new Point((int)offset.X,(int)offset.Y);
                }

                global.Document.Refresh();
               
                return true;
            }
            //RectangleF rect = ActiveNode.getNodeDisplayRect();// new RectangleF(ActiveNode.Location.X, ActiveNode.Location.Y, ActiveNode.Size.Width, ActiveNode.Size.Height);
            RectangleF rect = NodeTools.getAllNodeRect(global, ActiveNodes);
            PointF pt = TransformTools.D2R(e.Location,seInfo);
            if (rect.Contains(pt))
            {
                global.Document.Cursor = Cursors.SizeAll;
                return true;
            } else
            {
                global.Document.Cursor = Cursors.Arrow;
                return false;
            }
            
        }

        public bool OnMouseUp(MouseEventArgs e)
        {
            if (isSelect)
            {
                if (ActiveNodes.Count == 1)
                    global.PropertyGrid.SelectedObject = ActiveNodes[0];
                else
                    global.PropertyGrid.SelectedObject = new MoveObject(this,ActiveNodes);
                //RedoManager.PushRedo(new RedoItem(ActiveNode, "Location", startLocation, ActiveNode.Location));
            }
            isSelect = false;
            return false;
        }

        public bool OnMouseWheel(MouseEventArgs e)
        {
            return false;
        }

        public void OnPaint(Graphics g, Rectangle rect)
        {
            // Rectangle r = NodeTools.getAllNodeRect(global, ActiveNodes);// ActiveNode.getNodeDisplayRect();
            // DrawTools.DrawRect(g, pen1, new Rectangle(r.X, r.Y, r.Width, r.Height),  seInfo);
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

        public void RefreshDocument()
        {
            global.Document.Refresh();
        }
    }
}
