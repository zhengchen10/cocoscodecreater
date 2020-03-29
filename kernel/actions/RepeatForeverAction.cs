using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class RepeatForeverAction : AbstractAction
    {
        public RepeatForeverAction(ActionItem item)
        {
            Action = item;
        }

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
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" =  CCRepeatForever::create(");
            for (int i = 0; i < paramList.Count; i++)
            {
                if(i==0)
                    sb.Append(paramList[i]);
                else
                    sb.Append(", ").Append(paramList[i]);
            }
            sb.Append(");\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "repeatForever()";
        }
    }
}
