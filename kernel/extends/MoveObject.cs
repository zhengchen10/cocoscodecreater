using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kernel.models;

namespace kernel.extends
{
    public class MoveObject
    {
        [Browsable(false)]
        public List<Node> SelectNodes { get; set; }
        [Browsable(false)]
        private IRefreshDocument document;
        public MoveObject(IRefreshDocument document,List<Node> selectNodes)
        {
            this.document = document;
            this.SelectNodes = selectNodes;
            Node node = selectNodes[0];
            left = node.Location.X;
            top = node.Location.Y;
            width = node.Size.Width;
            height = node.Size.Height;
            right = left + width;
            bottom = top + height;
            anchorPoint = node.AnchorPoint;
            visible = node.Visible;
        }



        private int left;
        [DisplayNameAttribute("Left"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Left")
        ]
        public int Left { get { return left; } set { left = value;changeValue("left"); } }

        private int top;
        [DisplayNameAttribute("Top"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Top")
        ]
        public int Top { get { return top; } set { top = value; changeValue("top"); } }

        private int right;
        [DisplayNameAttribute("Right"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Right")
        ]
        public int Right { get { return right; } set { right = value; changeValue("right"); } }

        private int bottom;
        [DisplayNameAttribute("Bottom"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Bottom")
        ]
        public int Bottom { get { return bottom; } set { bottom = value; changeValue("bottom"); } }


        private int width;
        [DisplayNameAttribute("Width"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Width")
        ]
        public int Width { get { return width; } set { width = value; changeValue("width"); } }

        private int height;
        [DisplayNameAttribute("Height"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Height")
        ]
        public int Height { get { return height; } set { height = value; changeValue("height"); } }

        private Point anchorPoint;
            
        [DisplayNameAttribute("AnchorPoint"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("AnchorPoint")
        ]
        public Point AnchorPoint { get { return anchorPoint; } set { anchorPoint = value;changeValue("anchorPoint"); } }

        private bool visible;
        [DisplayNameAttribute("Visible"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Visible")
        ]
        public bool Visible { get { return visible; } set { visible = value; changeValue("visible"); } }

        
        private void changeValue(String type)
        {
            if(type == "anchorPoint")
            {
                for(int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].AnchorPoint = AnchorPoint;
                }
            }
            if (type == "visible")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Visible = Visible;
                }
            }
            if (type == "left")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Location = new Point(Left, SelectNodes[i].Location.Y);
                }
            }
            if (type == "right")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Location = new Point(Right - SelectNodes[i].Size.Height, SelectNodes[i].Location.Y);
                }
            }
            if (type == "top")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Location = new Point( SelectNodes[i].Location.X ,Top);
                }
            }
            if (type == "bottom")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Location = new Point(SelectNodes[i].Location.X, Bottom - SelectNodes[i].Size.Height);
                }
            }
            if (type == "width")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Size = new Size(width,SelectNodes[i].Size.Height);
                }
            }
            if (type == "height")
            {
                for (int i = 0; i < SelectNodes.Count; i++)
                {
                    SelectNodes[i].Size = new Size(SelectNodes[i].Size.Width,height);
                }
            }
            document.RefreshDocument();
        }
    }
}
