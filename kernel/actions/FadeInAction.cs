using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class FadeInAction : AbstractAction
    {
        public FadeInAction(ActionItem item)
        {
            Action = item;
        }
        public FadeInAction(ActionItem item,float duration)
        {
            Action = item;
            Duration = duration;
        }
        [DisplayNameAttribute("时长"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("时长")
        ]
        public float Duration { get { return Action.Duration; } set { Action.Duration = value; } }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            addDescription(sb, paramName);
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" = CCFadeIn::create(").Append(Duration).Append(");\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "fadeIn(" + Duration + ")";
        }
    }
}
