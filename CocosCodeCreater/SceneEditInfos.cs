using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class SceneEditInfos
    {
        public SceneEditInfos()
        {
            InvertedYAxis = true;
        }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double Scale { get; set; }
        public double AnchorX { get; set; }
        public double AnchorY { get; set; }
        public bool InvertedYAxis;
        public Rectangle Rectangle { get; set; }
    }
}
