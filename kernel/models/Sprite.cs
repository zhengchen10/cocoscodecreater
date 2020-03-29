using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace kernel.models
{
    public class Sprite : Node
    {
        public Sprite(string name)
        {
            Name = name;
            Type = "Sprite";
            Touchable = false;
            NotifyTouchEvent = false;
        }


        [DisplayNameAttribute("Sprite Frame"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("Sprite Frame"),
        TypeConverter(typeof(SpriteListProperty)),
        ]
        public String SpriteFrame { get; set; }

        [DisplayNameAttribute("Touchable"),
       CategoryAttribute("操作信息"),
       DescriptionAttribute("Touchable")
       ]
        public bool Touchable { get; set; }

        [DisplayNameAttribute("NotifyTouchEvent"),
      CategoryAttribute("操作信息"),
      DescriptionAttribute("NotifyTouchEvent")
      ]
        public bool NotifyTouchEvent { get; set; }



        public virtual void clone(Node node)
        {
            base.clone(node);
            Sprite s = (Sprite)node;
            this.SpriteFrame = (string)s.SpriteFrame.Clone();
            this.Touchable = s.Touchable;
            this.NotifyTouchEvent = s.NotifyTouchEvent;
        }
    }
}
