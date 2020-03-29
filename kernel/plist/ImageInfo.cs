using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace kernel.plist
{
    public class ImageInfo
    {
        private Rectangle frame = new Rectangle();
        private bool rotated =false;
        //[ReadOnly(true)]
        public String Name { get; set; }
        public Rectangle Frame { get { return frame; } set { frame = value;
                if (rotated)
                    DisplayFrame = new Rectangle(value.Left, value.Top, value.Height, value.Width);
                else
                    DisplayFrame = new Rectangle(value.Left, value.Top, value.Width, value.Height);
            } }
        public Rectangle DisplayFrame { get; set; }
        public Point Offset { get; set; }
        public Boolean Rotated { get { return rotated; } set {rotated = value;
                if (rotated)
                    DisplayFrame = new Rectangle(frame.Left, frame.Top, frame.Height, frame.Width);
                else
                    DisplayFrame = new Rectangle(frame.Left, frame.Top, frame.Width, frame.Height);
            } }
        public Rectangle SourceColorRect { get; set; }
        
        public Size SourceSize { get; set; }
        [Browsable(false)]
        public String SourceFile { get; set; }
    }
}
