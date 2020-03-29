using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class RotateByAction : AbstractAction
    {
        public RotateByAction(ActionItem item)
        {
            Action = item;
        }
        public RotateByAction(ActionItem item,float duration,float rotateX,float rotateY,float rotateZ)
        {
            Action = item;
            Duration = duration;
            RotateX = rotateX;
            RotateY = rotateY;
            RotateZ = rotateZ;
        }
        [DisplayNameAttribute("时长"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("时长")]
        public float Duration { get { return Action.Duration; } set { Action.Duration = value; } }

        [DisplayNameAttribute("RotateX"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("RotateX")]
        public float RotateX { get { return Action.ValueX; } set { Action.ValueX = value; } }

        [DisplayNameAttribute("RotateY"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("RotateY")]
        public float RotateY { get { return Action.ValueY; } set { Action.ValueY = value; } }

        [DisplayNameAttribute("RotateZ"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("RotateZ")]
        public float RotateZ { get { return Action.ValueZ; } set { Action.ValueZ = value; } }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            addDescription(sb, paramName);
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" = CCRotateBy::create(").Append(Duration).Append(",Vec3(");
            sb.Append(RotateX).Append(",").Append(RotateY).Append(",").Append(RotateZ).Append("));\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "rotateBy(" + Duration + ",(" + RotateX + "," + RotateY + "," + RotateZ + "))";
        }
    }
}