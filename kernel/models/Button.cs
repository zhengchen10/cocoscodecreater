using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace kernel.models
{
    public class Button : Node
    {
        public Button(string name)
        {
            Name = name;
            Type = "Button";

            PressedSprite = "";
            NormalSprite = "";
            DisableSprite = "";
            FontSize = 12;
            Label = "";
            Color = Color.Black;
            Shadow = new Point(0, 0);
            ShadowColor = Color.Black;
            AnchorPoint = new Point(1, 1);
        }

        [DisplayNameAttribute("NormalSprite"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("NormalSprite"),
        TypeConverter(typeof(SpriteListProperty)),
        ]
        public String NormalSprite { get; set; }
        [DisplayNameAttribute("PressedSprite"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("PressedSprite"),
        TypeConverter(typeof(SpriteListProperty)),
        ]
        public String PressedSprite { get; set; }
        [DisplayNameAttribute("DisableSprite"),
       CategoryAttribute("显示信息"),
       DescriptionAttribute("DisableSprite"),
       TypeConverter(typeof(SpriteListProperty)),
       ]
        public String DisableSprite { get; set; }

        [DisplayNameAttribute("Font Name"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("字体名称"),
        TypeConverter(typeof(FontNameProperty)),
        ]
        public string FontName { get; set; }
        [CategoryAttribute("显示信息")]
        public int FontSize { get; set; }
        [CategoryAttribute("显示信息")]
        public string Label { get; set; }
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
        public bool LoadTextFromConfig { get; set; }

        [DisplayNameAttribute("ActionType"),
       CategoryAttribute("控制信息"),
       DescriptionAttribute("ActionType"),
       TypeConverter(typeof(NavigateActionTypeProperty)),
       ]
        public String ActionType { get; set; }

        [DisplayNameAttribute("DestScene"),
       CategoryAttribute("控制信息"),
       DescriptionAttribute("DestScene"),
       TypeConverter(typeof(SceneListProperty)),
       ]
        public String DestScene { get; set; }


        public virtual void clone(Node node)
        {
            base.clone(node);
            Button s = (Button)node;
            this.NormalSprite = (string)s.NormalSprite.Clone();
            this.PressedSprite = (string)s.PressedSprite.Clone();
            this.DisableSprite = (string)s.DisableSprite.Clone();

            this.FontName = (string)s.FontName.Clone();
            this.FontSize = s.FontSize;
            this.Label = (string)s.Label.Clone();
            this.Color = Color.FromArgb(s.Color.R, s.Color.G, s.Color.B, s.Color.A);
            this.Shadow = new Point(s.Shadow.X, s.Shadow.Y);
            this.ShadowColor = Color.FromArgb(s.ShadowColor.R, s.ShadowColor.G, s.ShadowColor.B, s.ShadowColor.A);
            this.LoadTextFromConfig = s.LoadTextFromConfig;

            this.ActionType = (string)s.ActionType.Clone();
            this.DestScene = (string)s.DestScene.Clone();
        }
    }
}
