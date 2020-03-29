using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class RequestBodyWriter : FileBase
    {
        private String fileName;
        private Request request;
        public RequestBodyWriter(Request request, String path, String fileName)
        {
            this.fileName = fileName;
            this.request = request;
            base.SetFilePath(path + "\\" + fileName + ".cpp");
        }

        public override void WriteContent()
        {
            a("#include \"").a(fileName).al(".h\"");
            a(fileName).a("::").a(fileName).al("(){");
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            al("}").e();

            a("bool ").a(fileName).a("::parseJson(std::string json){").e();
            t().a("rapidjson::Document doc;").e();
            t().a("doc.Parse <0> (json.c_str());").e();
            t().a("if (doc.HasParseError()) {").e();
            t().t().a("return false;").e();
            t().a("}").e();

            for(int i = 0; i < request.Results.Count; i++)
            {
                Param param = request.Results[i];
                if (!param.IsData)
                {
                    t().a("if (doc.HasMember(\"").a(param.Name).a("\")) {").e();
                    t().t().a("set").a(mainName(param.Name)).a("(doc[\"").a(param.Name).a("\"].Get").a(RequestTools.changeFuncType(param.Type)).a("());").e();
                    t().a("}").e();
                }
            }
            if (request.ResultType > 0)
            {
                t().a("rapidjson::Value & data = doc[\"data\"];").e();
                t().a("parseData(data);").e();
            }
            t().a("return true;").e();
            al("}").e();

            a("void ").a(fileName).a("::parseData(rapidjson::Value& data){").e();
            if (request.ResultType == 2)
            {
                t().a("if (!data.IsArray()) return;").e();
                t().a("for (int i = 0; i < data.Size(); i++) {").e();
                t().t().a("const rapidjson::Value& item = data[i];").e().e();
                for (int i = 0; i < request.Results.Count; i++)
                {
                    Param param = request.Results[i];
                    if (param.IsData)
                    {
                        t().t().a("if (item.HasMember(\"").a(param.Name).a("\")) {").e();
                        t().t().t().a("m_").a(param.Name).a(".push_back(item[\"").a(param.Name).a("\"].Get").a(RequestTools.changeFuncType(param.Type)).a("());").e();
                        t().t().a("} else {").e();
                        t().t().t().a("m_").a(param.Name).a(".push_back(").a(RequestTools.changeParamDefaultValue(param.Type)).a(");").e();
                        t().t().a("} ").e().e();
                    }
                }

                t().a("}").e();
            } else
            {
                for (int i = 0; i < request.Results.Count; i++)
                {
                    Param param = request.Results[i];
                    if (param.IsData)
                    {
                        t().a("if (data.HasMember(\"").a(param.Name).a("\")) {").e();
                        t().t().a("set").a(mainName(param.Name)).a("(data[\"").a(param.Name).a("\"].Get").a(RequestTools.changeFuncType(param.Type)).a("());").e();
                        t().a("}").e();
                    }
                }
            }
            
            al("}").e();

            a(fileName).a(" & ").a(fileName).a("::operator= (const ").a(fileName).a("&result) {").e();
            for (int i = 0; i < request.Results.Count; i++)
            {
                Param param = request.Results[i];
                if(request.ResultType == 2 && param.IsData)
                {
                    t().a("m_").a(param.Name).a(".assign(result.m_").a(param.Name).a(".begin(), result.m_").a(param.Name).a(".end());").e();
                }else
                {
                    t().a("set").a(mainName(param.Name)).a("(result.get").a(mainName(param.Name)).a("());").e();
                }
            }
            t().a("return *this;").e();
            al("}").e();
        }

        private static String mainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }
    }
}
