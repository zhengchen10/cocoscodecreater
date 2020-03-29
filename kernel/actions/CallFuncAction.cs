using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class CallFuncAction : AbstractAction
    {
        public CallFuncAction(ActionItem item)
        {
            Action = item;
        }

        public override string CreateCode(HashSet<string> context, string paramName)
        {
            return "";
        }

        public override string GetName()
        {
            return "callFunc()";
        }
    }
}
