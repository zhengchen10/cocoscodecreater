using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.models
{
    public class Label : Node
    {
        public Label(string name)
        {
            Name = name;
            Type = "Label";
            FontSize = 12;
            Value = name;
            Color = Color.Black;
            Shadow = new Point(0, 0);
            ShadowColor = Color.Black;
            AnchorPoint = new Point(1, 1);
        }

        [DisplayNameAttribute("Font Name"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("字体名称"),
        TypeConverter(typeof(FontNameProperty)),
        ]
        public string FontName { get; set; }
        [CategoryAttribute("显示信息")]
        public int FontSize { get; set; }
        [CategoryAttribute("显示信息")]
        public string Value { get; set; }
        [CategoryAttribute("显示信息")]
        public Color Color { get; set; }

        [DisplayNameAttribute("Shadow"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Shadow")
        ]
        public Point Shadow { get; set; }

        [CategoryAttribute("显示信息")]
        public Color ShadowColor { get; set; }
        [CategoryAttribute("控制信息")]
        public bool LoadFromConfig { get; set; }

        public virtual void clone(Node node)
        {
            base.clone(node);
            Label s = (Label)node;
            this.FontName = (string)s.FontName.Clone();
            this.FontSize = s.FontSize;
            this.Value = (string)s.Value.Clone();
            this.Color =  Color.FromArgb(s.Color.R, s.Color.G, s.Color.B, s.Color.A);
            this.Shadow = new Point(s.Shadow.X, s.Shadow.Y);
            this.ShadowColor = Color.FromArgb(s.ShadowColor.R, s.ShadowColor.G, s.ShadowColor.B, s.ShadowColor.A);
            this.LoadFromConfig = s.LoadFromConfig;
        }
    }
}
