using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class RequestHeaderWriter : FileBase
    {
        private String fileName;
        private Request request;
        public RequestHeaderWriter(Request request, String path,String fileName)
        {
            this.fileName = fileName;
            this.request = request;
            base.SetFilePath(path + "\\" + fileName + ".h");
        }
        public override void WriteContent()
        {
            al("#pragma once").e();

            al("#include<string>");
            al("#include \"cocos2d.h\"");
            al("#include \"json\\rapidjson.h\"");
            al("#include \"json\\document.h\"");
            al("#include \"BaseResult.h\"");
            
            al("USING_NS_CC;");
            a("class ").a(fileName).al(" : public BaseResult");
            al("{");
            al("public:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");
            al("public:");
            t().a("bool parseJson(std::string json);").e();
            t().a(fileName).a(" & operator= (const ").a(fileName).a("&result);").e();
            al("protected:").e();
            t().a("void parseData(rapidjson::Value& data);").e();
            for (int i = 0; i < request.Results.Count; i++)
            {
                Param param = request.Results[i];
                if (!param.IsData && ((param.Name == "success") || (param.Name == "errorCode") || (param.Name == "message")))
                {
                    continue;
                }
                if (request.ResultType == 2 && param.IsData)
                {
                    t().a("CC_ARRAYLIST(").a(RequestTools.changeParamType(param.Type)).a(",m_").a(param.Name).a(",").a(mainName(param.Name)).al(");");
                }
                else { 
                    t().a("CC_SYNTHESIZE(").a(RequestTools.changeParamType(param.Type)).a(",m_").a(param.Name).a(",").a(mainName(param.Name)).al(");");
                }
            }
            a("};");
        }
        private static String mainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

    }
   
}
