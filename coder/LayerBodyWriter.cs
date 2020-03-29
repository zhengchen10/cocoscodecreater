using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class LayerBodyWriter : FileBase
    {
        String fileName;
        Layer layer;
        ProjectInfo project;
        public LayerBodyWriter(Layer layer, String path, ProjectInfo project, String fileName)
        {
            this.layer = layer;
            this.fileName = fileName;
            this.project = project;
            base.SetFilePath(path + "\\" + fileName + ".cpp");
        }
        public override void WriteContent()
        {
            a("#include \"").a(fileName).al(".h\"");
            al("USING_NS_CC;");
            e();
            a(fileName).a("::").a(fileName).al("(){");
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            al("}").e();

            a("bool ").a(fileName).al("::init() {");
            t().a("if (!BaseLayer::init()) {").e();
            t().t().a("return false;").e();
            t().a("}").e();
            CodeCreater.addInit(this, project,fileName,layer);
            t().a("return true;").e();
            al("}").e();
        }
    }
}
