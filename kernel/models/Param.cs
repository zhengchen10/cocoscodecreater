using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.models
{
    public class Param
    {
        [DisplayNameAttribute("Name"),
        CategoryAttribute("基本信息"),
        DescriptionAttribute("Name")
        ]
        public string Name { get; set; }
        [DisplayNameAttribute("Type"),
      CategoryAttribute("基本信息"),
      DescriptionAttribute("Type"),
      TypeConverter(typeof(ParamTypeProperty)),
      ]
        public string Type { get; set; }

        [DisplayNameAttribute("TestValue"),
      CategoryAttribute("基本信息"),
      DescriptionAttribute("TestValue")
      
      ]
        public string TestValue { get; set; }

        [Browsable(false)]
        public bool IsData { get; set; }
    }
}
