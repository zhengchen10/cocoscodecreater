using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kernel.models;

namespace CocosCodeCreater
{
    public class DrawTools
    {
        private static Brush backBrush = new SolidBrush(Color.FromArgb(51, 51, 51));
        private static Pen pen1 = new Pen(Color.FromArgb(77, 77, 77), 2);
        private static Pen pen2 = new Pen(Color.FromArgb(69, 69, 69), 1);
        private static Pen pen3 = new Pen(Color.FromArgb(59, 59, 59), 1);
        private static Font drawFont = new Font("Arial", 12);
        private static SolidBrush drawBrush = new SolidBrush(Color.FromArgb(90, 90, 90));

        public static void DrawGrid(Graphics g,  SceneEditInfos seInfo)
        {
            Rectangle rect = seInfo.Rectangle;
            float x0 = TransformTools.XD2R(rect.Left, seInfo);
            float x1 = TransformTools.XD2R(rect.Right, seInfo);
            float y1 = TransformTools.YD2R(rect.Top, seInfo);
            float y0 = TransformTools.YD2R(rect.Bottom, seInfo);

            int bigRange=100, midRange= 20,smallRange = 10;

            int range = (int)(x1 - x0) / 5;
            if (range < 30)
            {
                smallRange = 1;
            } else if (range < 50)
            {
                smallRange = 2;
            } else if (range < 100)
            {
                smallRange = 5;
            } else if (range < 200)
            {
                smallRange = 10;
            }
            else if (range < 400)
            {
                smallRange = 20;
            }else
            {
                smallRange = 50;
            }
            midRange = smallRange * 2;
            bigRange = smallRange * 10;

            int startX = (int)(x0 - smallRange) / smallRange * smallRange;
            int endX = (int)(x1 + smallRange ) / smallRange * smallRange;
            int startY;
            int endY;
            if (seInfo.InvertedYAxis)
            {
                startY = (int)(y0 - smallRange) / smallRange * smallRange;
                endY = (int)(y1 + smallRange) / smallRange * smallRange;
            }
            else
            {
                startY = (int)(y1 - smallRange) / smallRange * smallRange;
                endY = (int)(y0 + smallRange) / smallRange * smallRange;
            }
            for (int i = startX; i <= endX; i += smallRange)
            {
                if (i % bigRange == 0)
                {
                    DrawTools.DrawLine(g,  pen1, i, startY, i, endY, seInfo);
                    if(seInfo.InvertedYAxis)
                        DrawTools.DrawStringX(g,  i + "", drawFont, drawBrush, i, rect.Height - 20, seInfo);
                    else
                        DrawTools.DrawStringX(g, i + "", drawFont, drawBrush, i,  20, seInfo);
                }
                else if (i % midRange == 0)
                    DrawTools.DrawLine(g,  pen2, i, startY, i, endY, seInfo);
                else
                    DrawTools.DrawLine(g,  pen3, i, startY, i, endY, seInfo);
            }
            if(startY > endY)
            {
                int tmp = startY;
                startY = endY;
                endY = tmp;
            }
            for (int i = startY; i <= endY; i += smallRange)
            {
                if (i % bigRange == 0)
                {
                    DrawTools.DrawLine(g,  pen1, startX, i, endX, i, seInfo);
                    DrawTools.DrawStringY(g,  i + "", drawFont, drawBrush, 3, i, seInfo);
                }
                else if (i % midRange == 0)
                    DrawTools.DrawLine(g,  pen2, startX, i, endX, i, seInfo);
                else
                    DrawTools.DrawLine(g,  pen3, startX, i, endX, i, seInfo);
            }



        }
        private static void DrawLine(Graphics g, Pen pen,float x0,float y0,float x1,float y1,SceneEditInfos seInfo)
        {
            x0 = TransformTools.XR2D(x0, seInfo);
            x1 = TransformTools.XR2D(x1, seInfo);

            y0 = TransformTools.YR2D(y0, seInfo);
            y1 = TransformTools.YR2D(y1, seInfo);

            g.DrawLine(pen, x0, y0, x1, y1);
        }

        

        private static void DrawStringX(Graphics g,  string label, Font drawFont, SolidBrush drawBrush, float x0, float v2, SceneEditInfos seInfo)
        {
            x0 = TransformTools.XR2D(x0, seInfo);
            g.DrawString(label, drawFont, drawBrush, x0, v2);
        }

        private static void DrawStringY(Graphics g, string label, Font drawFont, SolidBrush drawBrush, float v2, float y0, SceneEditInfos seInfo)
        {
            y0 = TransformTools.YR2D(y0, seInfo);
            g.DrawString(label, drawFont, drawBrush, v2,y0);
        }

        internal static void DrawImage(Graphics g, string name, int x, int y, int width, int height, SceneEditInfos seInfo)
        {
            float x0 = TransformTools.XR2D(x, seInfo);
            float x1 = TransformTools.XR2D(x+width, seInfo);

            float y0 = TransformTools.YR2D(y, seInfo);
            float y1 = TransformTools.YR2D(y+height, seInfo);

            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(name);
                if (seInfo.InvertedYAxis)
                    g.DrawImage(img, new RectangleF(x0, y1, x1 - x0, y0 - y1));
                else
                    g.DrawImage(img, new RectangleF(x0, y0, x1 - x0, y1 - y0));
                img.Dispose();
            }catch(Exception exp)
            {

            }
            
        }

        internal static void DrawImage(Graphics g, Image img, int x, int y, int width, int height, SceneEditInfos seInfo)
        {
            float x0 = TransformTools.XR2D(x, seInfo);
            float x1 = TransformTools.XR2D(x + width, seInfo);

            float y0 = TransformTools.YR2D(y, seInfo);
            float y1 = TransformTools.YR2D(y + height, seInfo);

            if (seInfo.InvertedYAxis)
                g.DrawImage(img, new RectangleF(x0, y1, x1 - x0, y0 - y1));
            else
                g.DrawImage(img, new RectangleF(x0, y0, x1 - x0, y1 - y0));
        }


        internal static void DrawImage(Graphics g, Image img, int srcX, int srcY, int srcWidth, int srcHeight, int destX, int destY, int destWidth, int destHeight, SceneEditInfos seInfo)
        {
            float x0 = TransformTools.XR2D(destX, seInfo);
            float x1 = TransformTools.XR2D(destX + destWidth, seInfo);

            float y0 = TransformTools.YR2D(destY, seInfo);
            float y1 = TransformTools.YR2D(destY + destHeight, seInfo);

           
            if(seInfo.InvertedYAxis)
                g.DrawImage(img, new RectangleF(x0, y1, x1 - x0, y0 - y1),new RectangleF(srcX,srcY,srcWidth,srcHeight),GraphicsUnit.Pixel);
            else
                g.DrawImage(img, new RectangleF(x0, y0, x1 - x0, y1 - y0), new RectangleF(srcX, srcY, srcWidth, srcHeight), GraphicsUnit.Pixel);
        }

        internal static void DrawString(Graphics g, string value,Font font, Color color, int x, int y, int width, int height, SceneEditInfos seInfo)
        {
            float x0 = TransformTools.XR2D(x, seInfo);
            float x1 = TransformTools.XR2D(x + width, seInfo);

            float y0 = TransformTools.YR2D(y, seInfo);
            float y1 = TransformTools.YR2D(y + height, seInfo);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;  
            format.Alignment = StringAlignment.Center;
            if (seInfo.InvertedYAxis)
            {
                g.DrawString(value, font, new SolidBrush(color), new RectangleF(x0, y1, x1 - x0, y0 - y1), format);
            } else
            {
                g.DrawString(value, font, new SolidBrush(color), new RectangleF(x0, y0, x1 - x0, y1 - y0), format);
            }
        }

        internal static void DrawContainer(Global global,NodeContainer container, Graphics g, Point location, Size size,SceneEditInfos seInfo)
        {
            //Bitmap bufferImage = new Bitmap(container.Size.Width, container.Size.Height);
            //Graphics bufferGraph = Graphics.FromImage(bufferImage);
            //bufferGraph.Clear(Color.Transparent);

            // SceneEditInfos sinfo = new SceneEditInfos();
            // sinfo.AnchorX = 0;
            // sinfo.AnchorY = 0;
            // sinfo.OffsetX =  container.Size.Width/2;
            // sinfo.OffsetY =  container.Size.Height/2;
            // sinfo.Rectangle = new Rectangle(0,0,container.Size.Width,container.Size.Height);
            // sinfo.Scale = 1;
            //Rectangle r1 = container.getNodeDisplayRect();
            //int offsetX = location.X + r1.X;
            //int offsetY = location.Y + r1.Y;
            int offsetX = location.X ;
            int offsetY = location.Y ;
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);

                if (node.Type == "Sprite")
                {
                    Sprite sprite = (Sprite)node;
                    Resource res = global.Project.GetImage(sprite.SpriteFrame);
                    if (res != null && sprite.Visible)
                    {
                        Rectangle rect = sprite.getNodeDisplayRect();
                        DrawTools.DrawImage(g, res.Path, offsetX + rect.X, offsetY + rect.Y,rect.Width,rect.Height, seInfo);
                    }

                }
                if (node.Type == "Button")
                {
                    Button button = (Button)node;
                    Resource res = global.Project.GetImage(button.NormalSprite);
                    if (res != null && button.Visible)
                    {
                        Rectangle rect = button.getNodeDisplayRect();
                        DrawTools.DrawImage(g,  res.Path, offsetX + rect.X, offsetY + rect.Y, rect.Width, rect.Height, seInfo);
                        if(button.Label.Length > 0)
                        {
                            DrawTools.DrawString(g, button.Label, new Font("宋体", (int)(button.FontSize * seInfo.Scale)), button.Color, offsetX + button.Location.X - button.Size.Width / 2, offsetY + button.Location.Y - button.Size.Height / 2, button.Size.Width, button.Size.Height, seInfo);
                        }
                    }

                }
                if (node.Type == "Label")
                {
                    kernel.models.Label label = (kernel.models.Label)node;
                    if (label.Visible)
                    {
                        Rectangle rect = label.getNodeDisplayRect();
                        DrawTools.DrawString(g, label.Value, new Font("宋体", (int)(label.FontSize * seInfo.Scale)), label.Color, offsetX + label.Location.X - label.Size.Width / 2, offsetY + label.Location.Y - label.Size.Height / 2, label.Size.Width, label.Size.Height, seInfo);
                    }
                }
                if (node.Type == "ComponentRef")
                {
                    ComponentRef component = (ComponentRef)node;
                    for (int j = 0; j < global.Project.Components.Count; j++)
                    {
                        if (global.Project.Components[j].UUID == component.RefUUID)
                        {
                            DrawTools.DrawContainer(global, global.Project.Components[j], g, new Point(offsetX + node.Location.X, offsetY + node.Location.Y), component.Size, seInfo);
                        }
                    }
                }
            }
            //DrawTools.DrawImage(g, bufferImage, location.X, location.Y, size.Width, size.Height, seInfo);
        }

        public static void DrawRect(Graphics g, Pen pen, Rectangle rectangle,SceneEditInfos seInfo)
        {
            float x0 = TransformTools.XR2D(rectangle.Left, seInfo);
            float x1 = TransformTools.XR2D(rectangle.Right, seInfo);

            float y0 = TransformTools.YR2D(rectangle.Top, seInfo);
            float y1 = TransformTools.YR2D(rectangle.Bottom, seInfo);
            g.DrawLine(pen, x0, y0, x1, y0);
            g.DrawLine(pen, x1, y0, x1, y1);
            g.DrawLine(pen, x1, y1, x0, y1);
            g.DrawLine(pen, x0, y1, x0, y0);
        }
    }
}
