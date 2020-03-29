using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kernel.models
{
    public class Component : NodeContainer
    {
        public Component(string name)
        {
            Name = name;
            Type = "Component";
            Touchable = false;
           // this.AnchorPoint = new System.Drawing.Point(0, 0);
        }

        [DisplayNameAttribute("Touchable"),
       CategoryAttribute("操作信息"),
       DescriptionAttribute("Touchable")
       ]
        public bool Touchable { get; set; }
    }
}
