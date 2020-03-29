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
    public class ImageSelectAction : IAction
    {
        Pen pen1 = new Pen(Color.FromArgb(0, 0, 255), 1);
        private ImageInfo image;
        private SceneEditInfos seInfo;
        public ImageSelectAction(ImageInfo image, SceneEditInfos seInfo)
        {
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
            DrawTools.DrawRect(g, pen1, new Rectangle(image.Frame.Left, image.Frame.Top, image.Frame.Width, image.Frame.Height), seInfo);
        }
    }
}
