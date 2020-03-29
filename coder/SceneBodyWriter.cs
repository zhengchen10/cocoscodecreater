using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace coder
{
    public class SceneBodyWriter : FileBase
    {
        String fileName;
        Scene scene;
        ProjectInfo project;
        public SceneBodyWriter(Scene scene, String path, ProjectInfo project, String fileName) 
        {
            this.scene = scene;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path + "\\" + fileName + ".cpp");
        }
        public override void WriteContent()
        {
            a("#include \"").a(fileName).al(".h\"");
            a("#include \"").a(fileName).al("Logic.h\"");
            al("#include \"ScaleTools.h\"");
            al("#include \"ActionTools.h\"");


            CodeCreater.addBodyInclude(this, project, scene);
            e();
            CodeCreater.addTouchTAGS(this,project, scene);
            e();
            a(fileName).a("::").a(fileName).al("(){");
            t().a("m_").a(fileName).a("Logic = new ").a(fileName).a("Logic();").e();
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            t().a("delete m_").a(fileName).a("Logic;").e();
            al("}").e();

            a("Scene* ").a(fileName).al("::createScene() {");
            t().a("return ").a(fileName).al("::create();");
            al("}").e();

            a("std::vector<int> ").a(fileName).al("::getRegisterMessages() {");
            t().a("std::vector<int> messages;").e();
            t().a("return messages;").e();
            al("}").e();
            a("void ").a(fileName).al("::onReceiveMessage(BaseMessage * message) {");
            al("}").e();

            a("bool ").a(fileName).al("::init() {");
            t().a("if (!BaseScene::init()) {").e();
            t().t().a("return false;").e();
            t().a("}").e();
            CodeCreater.addInit(this, project, fileName, scene);
            t().a("m_").a(fileName).a("Logic->onInit(this);").e();
            t().a("return true;").e();
            al("}").e();
            CodeCreater.addActionBodys(this, project,fileName, scene);
            CodeCreater.addTouchMethodBodys(this,project, fileName, scene);
        }
    }
}
