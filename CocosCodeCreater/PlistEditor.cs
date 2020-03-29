using kernel.plist;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public class PlistEditor : IEditer
    {
        static Hashtable sceneInfo = new Hashtable();
        private PListInfo info;
        SceneEditInfos seInfo = null;
        ImageInfo activeImage = null;
        ImageInfo selectImage = null;
        Point startPoint;
        double dStartOffsetX, dStartOffsetY;
        bool startMove;
        Global global;
        IAction currentAction = null;
        
        public PlistEditor(PListInfo obj, Global global)
        {
            this.global = global;
            seInfo = new SceneEditInfos();
            seInfo.InvertedYAxis = false;
            seInfo.Scale = 1;
            seInfo.OffsetX = 20;
            seInfo.OffsetY = 20;
            seInfo.AnchorX = 0;
            seInfo.AnchorY = 0;
            this.info = obj;
           
            //this.global = global;
        }
        public bool OnKeyDown(KeyEventArgs e)
        {
            return false;
        }

        public bool OnKeyUp(KeyEventArgs e)
        {
            return false;
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
            ImageInfo image = HitTest(e.Location);
            if (image != null)
            {
                selectImage = image;
                currentAction = new ImageMoveAction(global, info, image, seInfo);
                currentAction.OnMouseDown(e);
                global.PropertyGrid.SelectedObject = image;
                return true;
            }
            else
            {
                selectImage = null;
                currentAction = null;
            }
            startMove = true;
            seInfo.AnchorX = (e.X - seInfo.OffsetX) / seInfo.Scale + seInfo.AnchorX;
            seInfo.AnchorY = (e.Y - seInfo.OffsetY) / seInfo.Scale + seInfo.AnchorY;
            seInfo.OffsetX = e.X;
            seInfo.OffsetY = e.Y;

            startPoint = e.Location;
            dStartOffsetX = seInfo.OffsetX;
            dStartOffsetY = seInfo.OffsetY;
            return true;
        }

        private ImageInfo HitTest(Point location)
        {
            PointF p = TransformTools.D2R(location, seInfo);
            for (int i = 0; i < info.Frames.Count; i++)
            {
                ImageInfo imageInfo = info.Frames[i];
                RectangleF rect = new RectangleF(imageInfo.DisplayFrame.Left, imageInfo.DisplayFrame.Top, imageInfo.DisplayFrame.Width, imageInfo.DisplayFrame.Height);
                if (rect.Contains(p))
                {
                    return imageInfo;
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
                seInfo.OffsetY = dStartOffsetY + e.Y - startPoint.Y;
                //sceneInfo[scene.Name] = seInfo;
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
                seInfo.AnchorY = (e.Y - seInfo.OffsetY) / lastScale + seInfo.AnchorY;
                seInfo.OffsetX = e.X;
                seInfo.OffsetY = e.Y;
                seInfo.Scale = newScale;
                //sceneInfo[scene.Name] = seInfo;
                return true;
            }
            return false;
        }

        public void OnPaint(Graphics g, Rectangle rect)
        {
            seInfo.Rectangle = rect;
            //SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(51, 51, 51));
            g.FillRectangle(drawBrush, rect);

            DrawTools.DrawGrid(g, seInfo);
            String imagePath = info.Path.Replace(".plist", ".png");
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
                for (int i = 0; i < info.Frames.Count; i++)
                {
                    ImageInfo imageInfo = info.Frames[i];
                    if (imageInfo.SourceFile != null&&imageInfo.SourceFile.Length>0)
                    {
                        System.Drawing.Image imgItem = System.Drawing.Image.FromFile(imageInfo.SourceFile);
                        if (imageInfo.Rotated)
                        {
                            Image rotate = PListTools.RotateImage90(imgItem);
                            DrawTools.DrawImage(g, rotate, imageInfo.DisplayFrame.Left, imageInfo.DisplayFrame.Top, imageInfo.DisplayFrame.Width, imageInfo.DisplayFrame.Height, seInfo);
                            rotate.Dispose();
                        }
                        else
                        {
                            DrawTools.DrawImage(g, imgItem, imageInfo.DisplayFrame.Left, imageInfo.DisplayFrame.Top, imageInfo.DisplayFrame.Width, imageInfo.DisplayFrame.Height, seInfo);
                        }
                        imgItem.Dispose();
                    }
                    else
                    {
                        if (imageInfo.Rotated)
                        {
                            DrawTools.DrawImage(g, img, imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Height, imageInfo.Frame.Width,
                                imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Height, imageInfo.Frame.Width, seInfo);
                        }
                        else
                        {
                            DrawTools.DrawImage(g, img, imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Width, imageInfo.Frame.Height,
                                imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Width, imageInfo.Frame.Height, seInfo);
                        }
                    }
                }

                img.Dispose();
            }catch(Exception exp)
            {

            }
            

            for (int i = 0; i < info.Frames.Count; i++)
            {
                ImageInfo imageInfo = info.Frames[i];
                if (imageInfo.SourceFile != null && imageInfo.SourceFile.Length > 0)
                {
                    if (imageInfo.Rotated)
                    {
                        DrawTools.DrawRect(g, Pens.Red, new Rectangle(imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Height, imageInfo.Frame.Width), seInfo);
                    }
                    else
                    {
                        DrawTools.DrawRect(g, Pens.Red, imageInfo.Frame, seInfo);
                    }
                }
                else
                {
                    if (imageInfo.Rotated)
                    {
                        DrawTools.DrawRect(g, Pens.Red, new Rectangle(imageInfo.Frame.Left, imageInfo.Frame.Top, imageInfo.Frame.Height, imageInfo.Frame.Width), seInfo);
                    }
                    else
                    {
                        DrawTools.DrawRect(g, Pens.Red, imageInfo.Frame, seInfo);
                    }
                }
            }
            if(activeImage != null)
            {
                if (activeImage.Rotated)
                {
                    DrawTools.DrawRect(g, Pens.Blue, new Rectangle(activeImage.Frame.Left, activeImage.Frame.Top, activeImage.Frame.Height, activeImage.Frame.Width), seInfo);
                }
                else
                {
                    DrawTools.DrawRect(g, Pens.Blue, activeImage.Frame, seInfo);
                }
            }
        }
        private void Refresh()
        {
            global.Document.Refresh();
        }

        public void OnSelectObject(object obj)
        {
            if(obj is ImageInfo)
            {
                activeImage = (ImageInfo)obj;
                Refresh();
            }
        }
    }
}
