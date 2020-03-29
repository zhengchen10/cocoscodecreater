using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.property
{

    public class FontNameProperty : StringConverter
    {
        public static IResourceManager manager;
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
            //@! 编辑下拉框中的items
            return new StandardValuesCollection(manager.GetFonts());
        }
        //@! true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
