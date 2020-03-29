using kernel.property;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kernel.models
{
    public class Scene :  NodeContainer
    {
        public Scene(string name)
        {
            Name = name;
            Type = "Scene";
            //Sprites = new List<Sprite>();
        }

        [DisplayNameAttribute("横竖屏"),
        CategoryAttribute("显示信息"),
        DescriptionAttribute("横屏 竖屏"),
        TypeConverter(typeof(ScreenTypeProperty)),
        ]
        public string ScreenType{get;set;}

        /*[Browsable(false)]
        [JsonIgnore]
        public List<Sprite> Sprites { get; }

        [Browsable(false)]
        [JsonIgnore]
        public List<Layer> Layers { get;  }
        */

        

        /*public void ReplaceObjectName(string oldValue, string newValue)
        {
            for(int i = 0; i < Children.Count; i++)
            {
                if (Children[i] == oldValue)
                    Children[i] = newValue;
            }
        }*/
    }
}
