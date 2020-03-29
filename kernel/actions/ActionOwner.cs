using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.actions
{
    public class ActionOwner
    {
        public ActionOwner()
        {
            Action = null;
        }
        [DisplayNameAttribute("Name"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("Name")]
        public String Name { get; set; }
        [DisplayNameAttribute("NeedCallback"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("NeedCallback")]
        public bool NeedCallback { get; set; }

        [DisplayNameAttribute("ActionNode"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("ActionNode"),
        TypeConverter(typeof(NodeListProperty))]
        public String ActionNode { get; set; }
        [Browsable(false)]
        public String ActionNodeUUID { get; set; }

        [Browsable(false)]
        public ActionItem Action { get; set; }
    }
}
