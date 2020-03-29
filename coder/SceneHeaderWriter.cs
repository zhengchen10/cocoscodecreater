using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class SceneHeaderWriter : FileBase
    {
        private String fileName;
        private Scene scene;
        private ProjectInfo project;
        public SceneHeaderWriter(Scene scene,String path, ProjectInfo project, String fileName) 
        {
            this.scene = scene;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path +"\\"+ fileName+".h");
        }
        public override void WriteContent()
        {
            al("#pragma once");
            al("#include \"cocos2d.h\"");
            al("#include \"cocos-ext.h\"");
            al("#include \"ui/CocosGUI.h\"");

            al("#include \"BaseScene.h\"");
            al("#include \"NodesContainer.h\"");

            al("USING_NS_CC;");
            al("using namespace ui;");
            e();
            CodeCreater.addInclude(this, project, scene);
            e();
            a("class ").a(fileName).al("Logic;");
            a("class ").a(fileName).al(" : public BaseScene,NodesContainer");
            al("{");
            al("public:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");
            e();
            t().al("static Scene* createScene();");
            e();
            al("public:");
            t().a("CREATE_FUNC(").a(fileName).al(");");
            t().al("virtual bool init();");
            t().al("virtual void onTouchNode(Node* node,int subTag,Touch* touch, Event* event);");
            t().al("virtual std::vector<int> getRegisterMessages();");
            t().al("virtual void onReceiveMessage(BaseMessage* message);");
            al("public:");
            CodeCreater.addActionHeaders(this, project, scene);
            al("private:");
            t().a(fileName).a("Logic * m_").a(fileName).a("Logic;").e();
            CodeCreater.addTouchMethodHeaders(this, scene);
            CodeCreater.addParams(this, scene);
            
            al("};");
        }
    }
}
