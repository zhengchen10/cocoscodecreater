using kernel.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class RedoItem
    {
        public RedoItem(Node node, string property, object oldValue, object newValue)
        {
            Node = node;
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public Node Node {get;set;}
        public String Property { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
