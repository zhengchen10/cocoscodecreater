using kernel.extends;
using kernel.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = kernel.models.Button;

namespace CocosCodeCreater
{
    public class ComponentEditor : IEditer, IRefreshDocument
    {

        static Hashtable sceneInfo = new Hashtable();

        float[] scales = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 10 / 9f, 1.25f, 1.5f, 1.75f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f };
        Brush backBrush = new SolidBrush(Color.FromArgb(51, 51, 51));
        Pen pen1 = new Pen(Color.FromArgb(255, 0, 0), 2);
        //String sceneName;
        Component component;
        //Rectangle clientRect;
        Global global;
        bool startMove = false;
        Point startPoint;
        double dStartOffsetX, dStartOffsetY;
        SceneEditInfos seInfo = null;
        IAction currentAction = null;
        List<Node> selectNodes = new List<Node>();
        bool isControl = false;
        public ComponentEditor(Component obj, Global global)
        {
            this.global = global;
            component = obj;// sceneName = obj.Name;
            seInfo = (SceneEditInfos)sceneInfo[obj.Name];
            if (seInfo == null)
            {
                seInfo = new SceneEditInfos();
                seInfo.OffsetX = (int)100;
                seInfo.OffsetY = (int)100;
                seInfo.Scale = 1.0;
                seInfo.AnchorX = (int)0;
                seInfo.AnchorY = (int)0;
                sceneInfo[obj.Name] = seInfo;
            }
        }

        public bool OnMouseClick(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseDown(MouseEventArgs e)
        {
            if (currentAction != null && currentAction.OnMouseDown(e))
            {
                return true;
            }
            Node node = HitTest(e.Location);
            if (node != null)
            {
                if (!isControl)
                    selectNodes.Clear();
                selectNodes.Add(node);
                currentAction = new SceneSelectAction(global, selectNodes, seInfo);
                if (selectNodes.Count == 1)
                    global.PropertyGrid.SelectedObject = node;
                else
                    global.PropertyGrid.SelectedObject = new MoveObject(this,selectNodes);
                return true;
            }
            else
            {
                selectNodes.Clear();
                currentAction = null;
            }
            startMove = true;
            seInfo.AnchorX = (e.X - seInfo.OffsetX) / seInfo.Scale + seInfo.AnchorX;
            seInfo.AnchorY = ((seInfo.Rectangle.Height - e.Y) - seInfo.OffsetY) / seInfo.Scale + seInfo.AnchorY;
            seInfo.OffsetX = e.X;
            seInfo.OffsetY = (seInfo.Rectangle.Height - e.Y);

            startPoint = e.Location;
            dStartOffsetX = seInfo.OffsetX;
            dStartOffsetY = seInfo.OffsetY;
            return true;

        }

        private Node HitTest(Point location)
        {
            PointF p = TransformTools.D2R(location, seInfo);
            // TODO:
            for (int i = component.Children.Count-1; i >=0; i--)
            {
                Node node = component.FindNodeByUUID(component.Children[i]);
                if (node.Visible == false)
                    continue;
                RectangleF rect = node.getNodeDisplayRect();
                if (node.Type == "ComponentRef")
                {
                    rect = node.getCompentShowRect(global.Project);
                }
                //new RectangleF(node.Location.X, node.Location.Y, node.Size.Width, node.Size.Height);
                if (rect.Contains(p))
                {
                    return node;
                }
            }
            return null;
        }

        public bool OnMouseMove(MouseEventArgs e)
        {
            if (currentAction != null)
            {
                return currentAction.OnMouseMove(e);
            }
            if (startMove)
            {
                seInfo.OffsetX = dStartOffsetX + e.X - startPoint.X;
                seInfo.OffsetY = dStartOffsetY + startPoint.Y - e.Y;
                sceneInfo[component.Name] = seInfo;
                return true;
            }
            return false;
        }

        public bool OnMouseUp(MouseEventArgs e)
        {
            if (currentAction != null)
            {
                return currentAction.OnMouseUp(e);
            }
            startMove = false;
            return false;
        }

        public bool OnMouseWheel(MouseEventArgs e)
        {
            double newScale;
            double lastScale = seInfo.Scale;
            if (e.Delta > 0)
            {
                newScale = lastScale * 9 / 10;
                if (newScale < 0.3) newScale = 0.3;
            }
            else
            {
                newScale = lastScale * 10 / 9;
                if (newScale > 10) newScale = 10;
            }
            seInfo.Scale = newScale;

            int x = (int)(e.X);
            int y = (int)((seInfo.Rectangle.Height - e.Y));
            double ox = seInfo.OffsetX, oy = seInfo.OffsetY;
            if (lastScale != newScale)
            {
                seInfo.AnchorX = (e.X - seInfo.OffsetX) / lastScale + seInfo.AnchorX;
                seInfo.AnchorY = ((seInfo.Rectangle.Height - e.Y) - seInfo.OffsetY) / lastScale + seInfo.AnchorY;
                seInfo.OffsetX = e.X;
                seInfo.OffsetY = (seInfo.Rectangle.Height - e.Y);
                //Console.WriteLine("Offset X:" + ox + " zoom(" + newScale + ") to " + dOffsetX +" e.X " + e.X);
                //Console.WriteLine("Offset Y:" + oy + " zoom(" + newScale + ") to " + dOffsetY);
                seInfo.Scale = newScale;
                sceneInfo[component.Name] = seInfo;
                return true;
            }
            return false;
        }

        public void OnPaint(Graphics g, Rectangle rect)
        {
            seInfo.Rectangle = rect;
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(90, 90, 90));// Create point for upper-left corner of drawing.

            g.FillRectangle(backBrush, rect);

            DrawTools.DrawGrid(g, seInfo);

            // TODO:
            DrawTools.DrawContainer(global, component, g, new Point(0, 0), component.Size, seInfo);
            /*
            for (int i = 0; i < component.Children.Count; i++)
            {
                Node node = component.FindNodeByUUID(component.Children[i]);

                if (node.Type == "Sprite")
                {
                    Sprite sprite = (Sprite)node;
                    Resource res = global.Project.GetImage(sprite.SpriteFrame);
                    if (res != null && sprite.Visible)
                    {
                        if (sprite.Visible)
                        {
                            Rectangle r = sprite.getNodeDisplayRect();
                            DrawTools.DrawImage(g, res.Path, r.X, r.Y, r.Width, r.Height, seInfo);
                        }

                    }
                }
                if (node.Type == "Label")
                {
                    kernel.models.Label label = (kernel.models.Label)node;
                    if (label.Visible)
                    {
                        Rectangle r = label.getNodeDisplayRect();
                        DrawTools.DrawString(g, label.Value, new Font("宋体", (int)(label.FontSize * seInfo.Scale)), label.Color, (r.Right + r.Left - label.Size.Width) / 2, (r.Bottom + r.Top - label.Size.Height) / 2, label.Size.Width, label.Size.Height, seInfo);
                        // DrawTools.DrawString(g, label.Value, new Font("宋体", (int)(label.FontSize * seInfo.Scale)), label.Color, label.Location.X- label.Size.Width/2, label.Location.Y-label.Size.Height/2, label.Size.Width, label.Size.Height, seInfo);
                    }
                }
                if (node.Type == "Button")
                {
                    Button button = (Button)node;
                    Resource res = global.Project.GetImage(button.NormalSprite);
                    if (res != null && button.Visible)
                    {
                        Rectangle r = button.getNodeDisplayRect();
                        DrawTools.DrawImage(g, res.Path, r.X, r.Y, r.Width, r.Height, seInfo);
                        if (button.Label != null)
                        {
                            DrawTools.DrawString(g, button.Label, new Font("宋体", (int)(button.FontSize * seInfo.Scale)), button.Color, (r.Right + r.Left - button.Size.Width) / 2, (r.Bottom + r.Top - button.Size.Height) / 2, button.Size.Width, button.Size.Height, seInfo);
                        }
                    }
                }
            }*/
            Rectangle r1 = component.getNodeDisplayRect();
            DrawTools.DrawRect(g, pen1, r1, seInfo);
            if (currentAction != null)
            {
                currentAction.OnPaint(g, rect);
            }
        }
        private void Refresh()
        {
            global.Document.Refresh();
        }
        public bool OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                isControl = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                selectNodes.Clear();
                currentAction = null;
                Refresh();
            }
            if (e.Control && e.KeyCode == Keys.M && selectNodes.Count>0)
            {
                currentAction = new SceneMoveAction(global, selectNodes, seInfo);
                Refresh();
            }
            return false;
        }

        public bool OnKeyUp(KeyEventArgs e)
        {
            isControl = false;
            return false;
        }

        public void OnSelectObject(object obj)
        {
            if (obj is Node)
            {
                selectNodes.Clear();
                selectNodes.Add((Node)obj);
                currentAction = new SceneSelectAction(global, selectNodes, seInfo);
            }
        }

        public void RefreshDocument()
        {
            global.Document.Refresh();
        }
    }
}