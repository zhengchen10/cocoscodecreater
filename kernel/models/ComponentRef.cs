using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.models
{
    public class ComponentRef : Component
    {
        public ComponentRef(string name):base(name)
        {
            Name = name;
            Type = "ComponentRef";
        }
        [Browsable(false)]
        public String RefUUID { get; set; }
        [Browsable(false)]
        public String RefName { get; set; }

        public virtual void clone(Node node)
        {
            base.clone(node);
            ComponentRef s = (ComponentRef)node;
            this.RefUUID = (string)s.RefUUID.Clone();
            this.RefName = s.RefName;
            
        }
    }
}
