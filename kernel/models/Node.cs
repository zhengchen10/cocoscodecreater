using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace kernel.models
{
    public class Node
    {
        public Node()
        {
            UUID = System.Guid.NewGuid().ToString("N");
            Children = new List<String>();
            Location = new Point();
            Size = new Size();
            Visible = true;
            AnchorPoint = new Point(1, 1);
        }
        [CategoryAttribute("基本信息"), DescriptionAttribute("名称")]
        public String Name { get; set; }
        [Browsable(false)]
        public String Type { get; set; }
        [Browsable(false)]
        public List<String> Children { get; set; }
        [Browsable(false)]
        public String UUID { get; set; }

        [DisplayNameAttribute("Location"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Location")
        ]
        public Point Location { get; set; }

        [DisplayNameAttribute("Size"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Size")
        ]
        public Size Size { get; set; }

        [DisplayNameAttribute("AnchorPoint"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("AnchorPoint")
        ]
        public Point AnchorPoint { get; set; }

        [DisplayNameAttribute("Visible"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Visible")
        ]
        public bool Visible { get; set; }


        public Rectangle getNodeDisplayRect()
        {
            int x;
            int y;
            if(AnchorPoint.X == 0)
            {
                x = this.Location.X;
            } else if(AnchorPoint.X == 2){
                x = this.Location.X - this.Size.Width;
            } else
            {
                x = this.Location.X - this.Size.Width/2;
            }

            if (AnchorPoint.Y == 0)
            {
                y = this.Location.Y;
            }
            else if (AnchorPoint.Y == 2)
            {
                y = this.Location.Y - this.Size.Height;
            }
            else
            {
                y = this.Location.Y - this.Size.Height / 2;
            }

            return new Rectangle(x, y, this.Size.Width, this.Size.Height);
        }

        public Rectangle getCompentShowRect(ProjectInfo project)
        {
            Rectangle rect = getNodeDisplayRect();
            if(Type == "ComponentRef")
            {
                Component component = project.getComponentByRef((ComponentRef)this);
                Rectangle r = component.getNodeDisplayRect();
                return new Rectangle(rect.X + r.Left, rect.Y + r.Top, r.Width, r.Height);
            }
            return rect;
        }

        public void clone(Node node)
        {
            this.Name = (string)node.Name.Clone();
            this.Type = node.Type;

            this.AnchorPoint = new Point(node.AnchorPoint.X, node.AnchorPoint.Y);
            this.Location = new Point(node.Location.X, node.Location.Y);
            this.Size = new Size(node.Size.Width, node.Size.Height);
            this.Visible = node.Visible;

            UUID = System.Guid.NewGuid().ToString("N");
        }
        /*public virtual Node FindChildren(string v)
        {
            return null;
        }*/
        /*
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].UUID == v)
                    return Children[i];
                if(Children[i].Type == "Scene" || Children[i].Type == "Layer")
                {
                    Node result = Children[i].FindChildren(v);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }*/
    }
}
