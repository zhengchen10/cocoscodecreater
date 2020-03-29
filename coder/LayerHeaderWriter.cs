using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class LayerHeaderWriter : FileBase
    {
        private String fileName;
        private Layer layer;
        public LayerHeaderWriter(Layer layer, String path, String fileName)
        {
            this.layer = layer;
            this.fileName = fileName;
            base.SetFilePath(path + "\\" + fileName + ".h");
        }
        public override void WriteContent()
        {
            al("#pragma once");
            al("#include \"cocos2d.h\"");
            al("#include \"BaseLayer.h\"");

            e();
            a("class ").a(fileName).al(" : BaseLayer");
            al("{");
            al("public:");
            t().a(fileName).al("();");
            t().a("~").a(fileName).al("();");
            e();
            al("public:");
            t().a("CREATE_FUNC(").a(fileName).al(");");
            t().al(" virtual bool init();");

            al("private:");
            CodeCreater.addParams(this, layer);
            al("};");
        }
    }
}