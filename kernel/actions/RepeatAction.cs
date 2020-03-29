using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class RepeatAction : AbstractAction
    {
        public RepeatAction(ActionItem item)
        {
            Action = item;
        }

        public RepeatAction(ActionItem item,int times)
        {
            Action = item;
            Times = times;
        }

        [DisplayNameAttribute("Times"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("Times")]
        public int Times { get { return Action.ValueInt; } set { Action.ValueInt = value; } }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            List<String> paramList = new List<string>();
            
            for (int i = 0; i < Action.SubActions.Count; i++)
            {
                String param = getParams(context, Action.SubActions[i].Type);
                sb.Append(Action.SubActions[i].createCode(context, param));
                paramList.Add(param);
            }
            addDescription(sb, paramName);
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" =  CCRepeat::create(");
            for (int i = 0; i < paramList.Count; i++)
            {
                sb.Append(paramList[i]).Append(", ");
            }
            sb.Append(Times).Append(");\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "repeat("+Times+")";
        }

    }
}
