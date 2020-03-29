using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class ComponentHeaderWriter : FileBase
    {
        private String fileName;
        private Component component;
        private ProjectInfo project;
        public ComponentHeaderWriter(Component component, String path, ProjectInfo project,String fileName)
        {
            this.component = component;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path + "\\" + fileName + ".h");
        }
        public override void WriteContent()
        {
            al("#pragma once");
            al("#include \"cocos2d.h\"");
            al("#include \"cocos-ext.h\"");
            al("#include \"ui/CocosGUI.h\"");
            al("#include \"NodesContainer.h\"");
            al("USING_NS_CC;");
            al("using namespace ui;");
            e();
            CodeCreater.addInclude(this,project, component);
            e();
            a("class ").a(fileName).al("Logic;");
            a("class ").a(fileName).al(" : public Node ,NodesContainer");
            al("{");
            al("public:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");
            t().al("static Node* createNode(NodesContainer *owner);");
            e();
            al("public:");
            t().a("CREATE_FUNC(").a(fileName).al(");");
            t().al("virtual bool init();");
            t().al("virtual void onTouchNode(Node* node,int subTag, Touch* touch, Event* event);");
            t().al("void setStatus(int status);");
            t().al("int getStatus(){ return status; }");
            t().al("NodesContainer * getOwner() { return m_owner; }");
            al("public:");
            CodeCreater.addActionHeaders(this, project, component);
            al("private:");
            t().a(fileName).a("Logic * m_").a(fileName).a("Logic;").e();
            t().al("NodesContainer *m_owner;");
            t().al("int status;");
            if (component.Touchable)
            {
                //t().al("bool onNodeTouchBegin(Touch* tTouch, Event* eEvent);");
                //t().al("void onNodeTouchEnded(Touch* tTouch, Event* eEvent);");
            }
            CodeCreater.addTouchMethodHeaders(this, component);
            CodeCreater.addParams(this, component);

            al("};");
        }
    }
}
