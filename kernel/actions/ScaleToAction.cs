using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class ScaleToAction : AbstractAction
    {
        public ScaleToAction(ActionItem item)
        {
            Action = item;
        }
        public ScaleToAction(ActionItem item,float duration,float scaleX,float scaleY,float scaleZ)
        {
            Action = item;
            Duration = duration;
            ScaleX = scaleX;
            ScaleY = scaleY;
            ScaleZ = scaleZ;
        }
        [DisplayNameAttribute("时长"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("时长")]
        public float Duration { get { return Action.Duration; } set { Action.Duration = value; } }

        [DisplayNameAttribute("ScaleX"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("ScaleX")]
        public float ScaleX { get { return Action.ValueX; } set { Action.ValueX = value; } }

        [DisplayNameAttribute("ScaleY"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("ScaleY")]
        public float ScaleY { get { return Action.ValueY; } set { Action.ValueY = value; } }

        [DisplayNameAttribute("ScaleZ"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("ScaleZ")]
        public float ScaleZ { get { return Action.ValueZ; } set { Action.ValueZ = value; } }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            addDescription(sb, paramName);
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" = CCScaleTo::create(").Append(Duration).Append(",");
            sb.Append(ScaleX).Append(",").Append(ScaleY).Append(",").Append(ScaleZ).Append(");\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "scaleTo(" + Duration + ",(" + ScaleX + "," + ScaleY + "," + ScaleZ + "))";
        }
    }
}
