using System;
using System.Collections.Generic;
using System.Text;
using kernel;
using kernel.models;

namespace coder
{
    class NetworkApiHeaderWriter : FileBase
    {
        private string classPath;
        private ProjectInfo project;
        private string fileName;
        public NetworkApiHeaderWriter(string classPath, ProjectInfo project)
        {
            this.classPath = classPath;
            this.project = project;
            fileName = "NetworkApi";
            base.SetFilePath(classPath + "\\NetworkApi.h");
        }

        public override void WriteContent()
        {
            al("#pragma once");
            al("#include <string>");
            al("#include \"cocos2d.h\"");
            al("#include \"NetworkCallback.h\"");

            al("USING_NS_CC;");
            e();
            a("class ").a(fileName).al(" {");
            al("protected:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");

            al("protected:");
            t().a("static std::string BASEURL;").e();
            

            al("public:");
            for(int i = 0; i < project.Requests.Count; i++)
            {
                kernel.models.Request request = project.Requests[i];
                t().a("static void send").a(mainName(request.Name)).a("(");
                if (request.AddToken)
                    a("std::string token");
                for(int j = 0; j < request.Params.Count; j++)
                {
                    Param param = request.Params[j];
                    if (request.AddToken || j > 0)
                        a(",");
                    a(RequestTools.changeParamType(param.Type)).a(" ").a(param.Name);
                }
                if(request.AddToken || request.Params.Count > 0)
                {
                    a(",");
                }
                
                a("NetworkCallback *callback);").e();
            }
            a("};");
        }
    }
}
