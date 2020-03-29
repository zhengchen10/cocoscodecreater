using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class LogicHeaderWriter : FileBase
    {
        private NodeContainer parent;
        private String fileName;
        private ProjectInfo project;
        public LogicHeaderWriter(NodeContainer parent,String path, ProjectInfo project, String fileName)
        {
            this.parent = parent;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path + "\\" + fileName + ".h");
        }

        public override void WriteContent()
        {
            al("#pragma once");
            al("#include \"cocos2d.h\"");
            al("USING_NS_CC;");
            a("#include \"").a(parent.Name).a(".h\"").e();
            a("#include \"NetworkCallback.h\"").e().e();

            a("class ").a(fileName).al(" : public NetworkCallback{");
            al("public:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");

            al("public:");
            t().a("virtual void onReceiveSuccess(std::string request, BaseResult* result);").e();
            t().a("virtual void onReceiveFailed(std::string request);").e();
        
            t().a("void onInit(").a(parent.Name).a("* parent);").e();
            if (parent.Type == "Component")
            {
                t().a("void onSetStatus(int status);").e();
                //t().a("static bool hitTest(").a(parent.Name).a("* ").a(objName(parent.Name)).a(",Vec2 location);").e();
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
                            t().a("void on").a(mainName(node.Name)).a("TouchBegin(Node *node, Touch* touch, Event* event);").e();
                            t().a("void on").a(mainName(node.Name)).a("TouchMove(Node *node, Touch* touch, Event* event);").e();
                        }
                        t().a("void on").a(mainName(node.Name)).a("Touched(Node *node, Touch* touch, Event* event);").e();

                    }
                }

                if (node.Type == "ComponentRef")
                {
                    Component component = project.getComponentByRef((ComponentRef)node);
                    if (component.Touchable)
                    {
                        t().a("void on").a(mainName(node.Name)).a("Touched(Node *node,int subTag, Touch* touch, Event* event);").e();
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
                        t().a("void on").a(mainName(node.Name)).a("Touched();").e();
                    }
                }
            }

            al("private:");
            t().a(fileName.Substring(0,fileName.Length - 5)).a("* ").a(objName(parent.Name)).a(";").e();
            al("};");
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
