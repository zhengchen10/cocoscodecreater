using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kernel.models
{
    public class Layer :  NodeContainer
    {
        public Layer(string name)
        {
            Name = name;
            Type = "Layer";
            //Sprites = new List<Sprite>();
        }
        //[Browsable(false)]
        //public List<Sprite> Sprites { get; set; }
    }
}
