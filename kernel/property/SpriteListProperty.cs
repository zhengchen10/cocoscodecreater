using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kernel.property
{
    public class SpriteListProperty : StringConverter
    {
        public static IResourceManager manager;
        //public static List<String> spriteList = new List<string>();
        public static void ReplaceResource(String src,String dest)
        {

        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            //@! 编辑下拉框中的items
            return new StandardValuesCollection(manager.GetImages());
        }
        //@! true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
