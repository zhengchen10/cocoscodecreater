using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.property
{
    public class RequestTypeProperty : StringConverter
    {
        //public static IResourceManager manager;
        //public static List<String> spriteList = new List<string>();
        public static void ReplaceResource(String src, String dest)
        {

        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            String[] items = new string[] { "Get", "Post", "Put","Delete" };
            return new StandardValuesCollection(items);
        }
        //@! true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
