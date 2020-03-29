using System;
using System.Collections.Generic;
using System.Text;
using kernel;
using kernel.models;

namespace coder
{
    class NetworkApiBodyWriter : FileBase
    {
        private string classPath;
        private string fileName;
        private ProjectInfo project;

        public NetworkApiBodyWriter(string classPath, ProjectInfo project)
        {
            this.classPath = classPath;
            this.project = project;
            fileName = "NetworkApi";
            base.SetFilePath(classPath + "\\NetworkApi.cpp");
        }

        public override void WriteContent()
        {
            a("#include \"").a(fileName).al(".h\"");
            al("#include \"network\\HttpClient.h\"");
            al("#include \"network\\HttpRequest.h\"");
            al("#include \"network\\HttpResponse.h\"");
            e();
            for (int i = 0; i < project.Requests.Count; i++)
            {
                kernel.models.Request request = project.Requests[i];
                a("#include \"").a(mainName(request.Name)).a("Result.h\"").e();
            }
            e();
            al("using namespace cocos2d::network;");
            e();

            a("std::string ").a(fileName).a("::BASEURL = \"http://127.0.0.1:8080\";").e().e();
            a(fileName).a("::").a(fileName).al("(){");
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            al("}").e();

            for (int i = 0; i < project.Requests.Count; i++)
            {
                kernel.models.Request request = project.Requests[i];
                a("void ").a(fileName).a("::send").a(mainName(request.Name)).a("(");
                if (request.AddToken)
                    a("std::string token");
                for (int j = 0; j < request.Params.Count; j++)
                {
                    Param param = request.Params[j];
                    if (request.AddToken || j > 0)
                        a(",");
                    a(RequestTools.changeParamType(param.Type)).a(" ").a(param.Name);
                }
                if (request.AddToken || request.Params.Count > 0)
                {
                    a(",");
                }

                a("NetworkCallback *callback) {").e();
                t().a("auto request = new HttpRequest();").e();
                t().a("std::string REQUEST = \"").a(request.Url).a("/").a(request.Name).a("\";").e();
                if (request.AddToken)
                {
                    t().a("std::vector < std::string> headers;").e();
                    t().a("headers.push_back(\"X-Token:\" + token);").e();
                    t().a("request->setHeaders(headers);").e();

                }
                t().a("std::string DATA = \"\";").e();
                for(int j = 0; j < request.Params.Count; j++)
                {
                    Param param = request.Params[j];
                    if (j == 0)
                    {
                        t().a("DATA = DATA + \"").a(param.Name).a("=\" + ").a(RequestTools.changeParamToString(param)).a(";").e();
                    } else
                    {
                        t().a("DATA = DATA + \"&").a(param.Name).a("=\" + ").a(RequestTools.changeParamToString(param)).a(";").e();
                    }
                }
                
                if(request.Type == "Get")
                {
                    t().a("request->setUrl(BASEURL + REQUEST + \"&\"+DATA);").e();
                } else
                {
                    t().a("request->setUrl(BASEURL + REQUEST);").e();
                    if(request.Params.Count >0)
                        t().a("request->setRequestData(DATA.c_str(), DATA.size());").e();
                }
                
                t().a("request->setRequestType(HttpRequest::Type::").a(request.Type.ToUpper()).a(");").e();
                t().a("request->setResponseCallback([=](HttpClient * client, HttpResponse * response) {").e();
                t().t().a("if (response->isSucceed() == true) {").e();
                t().t().t().a("std::vector<char>* data = response->getResponseData();").e();
                t().t().t().a("std::string resp(data->begin(), data->end());").e();
                t().t().t().a(mainName(request.Name)).a("Result result;").e();
                t().t().t().a("if(result.parseJson(resp)) {").e();
                t().t().t().t().a("if(callback != NULL)").e();
                t().t().t().t().t().a("callback->onReceiveSuccess(REQUEST,&result);").e();
                t().t().t().a("} else {").e();
                t().t().t().t().a("if(callback != NULL)").e();
                t().t().t().t().t().a("callback->onReceiveFailed(REQUEST);").e();
                t().t().t().a("}").e();
                t().t().a("} else {").e();
                t().t().t().a("if(callback != NULL)").e();
                t().t().t().t().a("callback->onReceiveFailed(REQUEST);").e();
                t().t().a("}").e();
                t().a("});").e();
                t().a("HttpClient::getInstance()->send(request);").e();
                t().a("request->release();").e();
            
            
            a("}").e();
            }
        }
    }
}
