using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public abstract class AbstractAction
    {
        //[Browsable(false)]
        //public String Name { get; set; }
        public abstract string GetName();
        public abstract string CreateCode(HashSet<string> context,String paramName);

        [Browsable(false)]
        public ActionItem Action { get; set; }

        [DisplayNameAttribute("Description"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("Description")]
        public string Description { get { return Action.Description; } set { Action.Description = value; } }

        public void addDescription(StringBuilder sb, String paramName)
        {
            if (Description == null || Description.Length == 0)
                sb.Append("    // init " + paramName).Append("\n");
            else
                sb.Append("    // " + Description).Append("\n");
        }
        public static String getParams(HashSet<string> context, String prefix)
        {
            int index = 1;
            String key = prefix + index;
            while (context.Contains(key))
            {
                index++;
                key = prefix + index;
            }
            context.Add(key);
            return key;
        }
    }
}
