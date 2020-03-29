using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class ActionItem
    {
        public ActionItem(String type)
        {
            UUID = System.Guid.NewGuid().ToString("N");
            SubActions = new List<ActionItem>();
            Type = type;
        }
        [Browsable(false)]
        public String Type { get; set; }

        [Browsable(false)]
        public List<ActionItem> SubActions { get; set; }

        [Browsable(false)]
        public String UUID { get; set; }

        [Browsable(false)]
        public bool NeedCallback { get; set; }

        [Browsable(false)]
        public float Duration { get; set; }

        [Browsable(false)]
        public float ValueX { get; set; }

        [Browsable(false)]
        public float ValueY { get; set; }

        [Browsable(false)]
        public float ValueZ { get; set; }

        [Browsable(false)]
        public int ValueInt { get; set; }

        [Browsable(false)]
        public string Description { get; set; }

        public string createCode(HashSet<string> context,String paramName)
        {
            if(Type == "sequence")
            {
                return new SequenceAction(this).CreateCode(context, paramName);
            } else if(Type == "callFunc")
            {
                return new CallFuncAction(this).CreateCode(context, paramName);
            }
            else if (Type == "delayTime")
            {
                return new DelayTimeAction(this).CreateCode(context, paramName);
            }
            else if (Type == "fadeIn")
            {
                return new FadeInAction(this).CreateCode(context, paramName);
            }
            else if (Type == "fadeOut")
            {
                return new FadeOutAction(this).CreateCode(context, paramName);
            }
            else if (Type == "moveBy")
            {
                return new MoveByAction(this).CreateCode(context, paramName);
            }
            else if (Type == "moveTo")
            {
                return new MoveToAction(this).CreateCode(context, paramName);
            }
            else if (Type == "repeat")
            {
                return new RepeatAction(this).CreateCode(context, paramName);
            }
            else if (Type == "repeatForever")
            {
                return new RepeatForeverAction(this).CreateCode(context, paramName);
            }
            else if (Type == "rotateBy")
            {
                return new RotateByAction(this).CreateCode(context, paramName);
            }
            else if (Type == "rotateTo")
            {
                return new RotateToAction(this).CreateCode(context, paramName);
            }
            else if (Type == "scaleBy")
            {
                return new ScaleByAction(this).CreateCode(context, paramName);
            }
            else if (Type == "scaleTo")
            {
                return new ScaleToAction(this).CreateCode(context, paramName);
            }
            else if (Type == "spawn")
            {
                return new SpawnAction(this).CreateCode(context, paramName);
            }
            return null;
        }
    }
}
