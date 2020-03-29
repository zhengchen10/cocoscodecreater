using System;
using System.Collections.Generic;
using System.Text;
using kernel.models;

namespace coder
{
    class RequestTools
    {
        public static String changeParamType(String type)
        {
            if(type == "Integer")
            {
                return "int";
            } else if (type == "Boolean")
            {
                return "bool";
            } else
            {
                return "std::string";
            }
        }

        public static String changeFuncType(String type)
        {
            if (type == "Integer")
            {
                return "Int";
            }
            else if (type == "Boolean")
            {
                return "Bool";
            }
            else
            {
                return "String";
            }
        }

        public  static String changeParamDefaultValue(string type)
        {
            if (type == "Integer")
            {
                return "0";
            }
            else if (type == "Boolean")
            {
                return "false";
            }
            else
            {
                return "\"\"";
            }
        }

        public static String changeParamToString(Param param)
        {
            if (param.Type == "Integer")
            {
                return "std::to_string("+param.Name+")";
            }
            else if (param.Type == "Boolean")
            {
                return "std::to_string(" + param.Name + ")";
            }
            else
            {
                return param.Name;
            }
        }
    }
}
