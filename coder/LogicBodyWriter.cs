using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class LogicBodyWriter : FileBase
    {
        private NodeContainer parent;
        private String fileName;
        private ProjectInfo project;
        public LogicBodyWriter(NodeContainer parent, String path, ProjectInfo project, String fileName)
        {
            this.parent = parent;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path + "\\" + fileName + ".cpp");
        }

        public override void WriteContent()
        {
            a("#include \"").a(fileName).al(".h\"");
            al("#include \"ActionTools.h\"");
            al("#include \"NetworkApi.h\"");

            e();
            e();
            a(fileName).a("::").a(fileName).al("(){");
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            al("}").e();

            a("void ").a(fileName).a("::onInit(").a(parent.Name).a("* ").a(objName(parent.Name)).a(") { ").e().e();
            t().a("this->").a(objName(parent.Name)).a(" = ").a(objName(parent.Name)).e();
            a("}").e().e();

            a("void ").a(fileName).a("::onReceiveSuccess(std::string request, BaseResult * result) { ").e().e();
            a("}").e().e();

            a("void ").a(fileName).a("::onReceiveFailed(std::string request) { ").e().e();
            a("}").e().e();

            if (parent.Type == "Component")
            {
                a("void ").a(fileName).a("::onSetStatus(int status) { ").e().e();
                a("}").e().e();

            }

            for (int i = 0; i < parent.Children.Count; i++)
            {
                Node node = parent.FindNodeByUUID(parent.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite s = (Sprite)node;
                    if (s.Touchable)
                    {
                        if (s.NotifyTouchEvent)
                        {
                            a("void ").a(fileName).a("::on").a(mainName(node.Name)).a("TouchBegin(Node *node, Touch* touch, Event* event) {").e().e();

                            a("}").e().e();

                            a("void ").a(fileName).a("::on").a(mainName(node.Name)).a("TouchMove(Node *node, Touch* touch, Event* event) {").e().e();

                            a("}").e().e();
                        }
                        a("void ").a(fileName).a("::on").a(mainName(node.Name)).a("Touched(Node *node, Touch* touch, Event* event) {").e().e();

                        a("}").e().e();
                    }
                }

                if (node.Type == "ComponentRef")
                {
                    Component component = project.getComponentByRef((ComponentRef)node);
                    if (component.Touchable)
                    {
                        a("void ").a(fileName).a("::on").a(mainName(node.Name)).a("Touched(Node *node,int subTag, Touch* touch, Event* event){").e().e();
                        a("}").e().e();
                    }
                }
                if (node.Type == "Button")
                {
                    Button button = (Button)node;
                    if (button.ActionType == "NavigateTo")
                    {
                    }
                    else if (button.ActionType == "NavigateBack")
                    {
                    }
                    else
                    {
                        a("void ").a(fileName).a("::on").a(mainName(node.Name)).a("Touched() {").e();
                        a("}").e().e();
                    }
                }
            }

        }

        private static String mainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }
        private static String objName(String name)
        {
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }
    }
}
