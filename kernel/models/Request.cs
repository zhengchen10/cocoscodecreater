using kernel.property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.models
{
    public class Request
    {
        [DisplayNameAttribute("Name"),
       CategoryAttribute("基本信息"),
       DescriptionAttribute("Name")
       ]
        public string Name { get; set; }
        [DisplayNameAttribute("Url"),
       CategoryAttribute("基本信息"),
       DescriptionAttribute("Url")
       ]

        public string Url { get; set; }

        [DisplayNameAttribute("AddToken"),
       CategoryAttribute("基本信息"),
       DescriptionAttribute("AddToken")
       ]

        public bool AddToken { get; set; }


        [DisplayNameAttribute("Type"),
      CategoryAttribute("基本信息"),
      DescriptionAttribute("Type"),
      TypeConverter(typeof(RequestTypeProperty)),
      ]

        public string Type {get;set;}
        [Browsable(false)]
        public List<Param> Params { get; set; }
        [Browsable(false)]
        public List<Param> Results { get; set; }
        [Browsable(false)]
        public int ResultType { get; set; }
        public Request(string apiName, string apiUrl)
        {
            this.Name = apiName;
            this.Url = apiUrl;
            this.Type = "Get";
            this.Params = new List<Param>();
            this.Results = new List<Param>();
            this.ResultType = 0;
        }
    }
}
