using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
   public class MoveByAction : AbstractAction
    {
        public MoveByAction(ActionItem item)
        {
            Action = item;
        }
        public MoveByAction(ActionItem item,float duration,float positionX, float positionY, float positionZ)
        {
            Action = item;
            Duration = duration;
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
        }

        [DisplayNameAttribute("时长"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("时长")]
        public float Duration { get { return Action.Duration; } set { Action.Duration = value; } }

        [DisplayNameAttribute("PositionX"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("PositionX")]
        public float PositionX { get { return Action.ValueX; } set { Action.ValueX = value; } }

        [DisplayNameAttribute("PositionY"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("PositionY")]
        public float PositionY { get { return Action.ValueY; } set { Action.ValueY = value; } }

        [DisplayNameAttribute("PositionZ"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("PositionZ")]
        public float PositionZ { get { return Action.ValueZ; } set { Action.ValueZ = value; } }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            addDescription(sb, paramName);
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" = CCMoveBy::create(").Append(Duration).Append(",Vec3(");
            sb.Append(PositionX).Append(",").Append(PositionY).Append(",").Append(PositionZ).Append("));\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "moveBy(" + Duration + ",(" + PositionX + "," + PositionY + "," + PositionZ + "))";
        }
    }
}
