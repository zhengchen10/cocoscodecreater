using kernel.models;
using kernel.plist;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public class ImageMoveAction : IAction
    {
        Pen pen1 = new Pen(Color.FromArgb(0, 0, 255), 1);
        ImageInfo image;
        PListInfo listInfo;
        SceneEditInfos seInfo;
        Global global;
        Point selectPoint;
        PointF startPoint;
        bool isSelect = false;
        public ImageMoveAction(Global global, PListInfo listInfo,ImageInfo image, SceneEditInfos seInfo)
        {
            this.listInfo = listInfo;
            this.global = global;
            this.image = image;
            this.seInfo = seInfo;
        }

        public List<Node> ActiveNodes { get ; set; }

        public bool OnMouseClick(MouseEventArgs e)
        {
            return false;
        }

        public bool OnMouseDown(MouseEventArgs e)
        {
            RectangleF rect = new RectangleF(image.DisplayFrame.Left, image.DisplayFrame.Top, image.DisplayFrame.Width, image.DisplayFrame.Height);
            PointF pt = TransformTools.D2R(e.Location, seInfo);
            if (rect.Contains(pt))
            {
                selectPoint = e.Location;
                startPoint = TransformTools.R2D(new PointF(image.Frame.Left, image.Frame.Top), seInfo);

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
                PointF newLocation = new PointF(e.Location.X - selectPoint.X + startPoint.X,
                     e.Location.Y - selectPoint.Y + startPoint.Y);

                PointF offset = TransformTools.D2R(newLocation, seInfo);
                if (offset.X < 0)
                    offset.X = 0;
                if (offset.Y < 0)
                    offset.Y = 0;
                if (offset.X + image.Frame.Width > listInfo.Size.Width)
                    offset.X = listInfo.Size.Width - image.Frame.Width;

                if (offset.Y + image.Frame.Height > listInfo.Size.Height)
                    offset.Y = listInfo.Size.Height - image.Frame.Height;


                Rectangle rect = new Rectangle((int)offset.X, (int)offset.Y, image.DisplayFrame.Width, image.DisplayFrame.Height);
                for(int i = 0; i < listInfo.Frames.Count; i++)
                {
                    if (image == listInfo.Frames[i])
                        continue;
                    Rectangle frame = listInfo.Frames[i].DisplayFrame;
                    Rectangle dest = new Rectangle(frame.Left - 1, frame.Top - 1, frame.Width + 2, frame.Height + 2);
                    if (dest.IntersectsWith(rect))
                        return false;
                }
                image.Frame = new Rectangle((int)offset.X, (int)offset.Y, image.Frame.Width, image.Frame.Height);
                global.Document.Refresh();

                return true;
            }
            return false;
        }

        public bool OnMouseUp(MouseEventArgs e)
        {
            if (isSelect)
            {
                global.PropertyGrid.SelectedObject = image;
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
            
        }
    }
}
