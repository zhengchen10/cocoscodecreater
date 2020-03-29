using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class SpawnAction : AbstractAction
    {
        public SpawnAction(ActionItem item)
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
            sb.Append("    CCActionInterval * ").Append(paramName).Append(" =  CCSpawn::create(");
            for (int i = 0; i < paramList.Count; i++)
            {
                sb.Append(paramList[i]).Append(", ");
            }
            sb.Append("NULL);\n");
            return sb.ToString();
        }

        public override string GetName()
        {
            return "spawn()";
        }
    }
}
