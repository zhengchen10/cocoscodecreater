using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kernel.property
{
    [TypeConverter(typeof(LocationPropertyConvert))]
    public class LocationProperty
    {
        private int x = 0;
        private int y = 0;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }

    public class LocationPropertyConvert : TypeConverter
    {
        //String -> MyLocation
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            { return true; }
            else
            { return false; }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null || value.ToString().Length == 0)
                return new LocationProperty();
            char spliter = culture.TextInfo.ListSeparator[0]; // 得到字符串的分隔符
            string[] ss = ((string)value).Split(spliter);
            LocationProperty myLoc = new LocationProperty();
            int leftIdx = 0;
            myLoc.X = int.Parse(ss[leftIdx]);
            myLoc.Y = int.Parse(ss[leftIdx + 1]);
            return myLoc;
        }

        //MyLocation -> Str
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(String))
            { return true; }
            else
            { return false; }
        }

        // 在Property窗口中显示为string类型。
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null) return "0,0";
            if (destinationType == typeof(string))
            {
                LocationProperty myLoc = (LocationProperty)value;
                char spliter = culture.TextInfo.ListSeparator[0]; // 得到字符串的分隔符

                String[] locs = new string[2];
                locs[0] = myLoc.X.ToString();
                locs[1] = myLoc.Y.ToString();
                return string.Join(spliter.ToString(), locs);
            }
            return "0,0";
        }


        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            String[] names = new String[] { "X", "Y" };
            return (TypeDescriptor.GetProperties(value.GetType()).Sort(names));
        }

    }
}
