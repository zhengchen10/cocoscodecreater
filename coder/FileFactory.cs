using kernel;
using kernel.models;
using kernel.plist;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class FileFactory
    {
        public static void createScene(String workPath, ProjectInfo project, Scene scene)
        {
            SceneHeaderWriter header=new SceneHeaderWriter(scene, workPath, project,scene.Name);
            header.writeCode();
            SceneBodyWriter body = new SceneBodyWriter(scene, workPath, project,scene.Name);
            body.writeCode();

            LogicHeaderWriter logic = new LogicHeaderWriter(scene, workPath+ "//Logics", project, scene.Name + "Logic");
            logic.writeCode();

            LogicBodyWriter logicbody = new LogicBodyWriter(scene, workPath + "//Logics", project, scene.Name + "Logic");
            logicbody.writeCode();
        }

        public static void createLayer(String workPath, ProjectInfo project, Layer layer)
        {
            LayerHeaderWriter header = new LayerHeaderWriter(layer, workPath, layer.Name);
            header.writeCode();
            LayerBodyWriter body = new LayerBodyWriter(layer, workPath,project, layer.Name);
            body.writeCode();
        }


        public static void createPlist(String workPath, ProjectInfo project, PListInfo resource)
        {
            resource.SaveAtPath(workPath);
        }

        public static void createComponent(string workPath,ProjectInfo project, Component component)
        {
            ComponentHeaderWriter header = new ComponentHeaderWriter(component, workPath, project,component.Name);
            header.writeCode();
            ComponentBodyWriter body = new ComponentBodyWriter(component, workPath,project, component.Name);
            body.writeCode();
            LogicHeaderWriter logic = new LogicHeaderWriter(component, workPath + "//Logics", project, component.Name + "Logic");
            logic.writeCode();

            LogicBodyWriter logicbody = new LogicBodyWriter(component, workPath + "//Logics", project, component.Name + "Logic");
            logicbody.writeCode();
        }

        public static void createRequest(string workPath,ProjectInfo project,Request request)
        {
            RequestHeaderWriter header = new RequestHeaderWriter(request, workPath + "//Messages", getMainName(request.Name)+"Result");
            header.writeCode();
            RequestBodyWriter body = new RequestBodyWriter(request, workPath + "//Messages", getMainName(request.Name) + "Result");
            body.writeCode();
        }

        public static string getMainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

        public static void createNetworkApi(string classPath, ProjectInfo project)
        {
            NetworkApiHeaderWriter header = new NetworkApiHeaderWriter(classPath, project);
            header.writeCode();
            NetworkApiBodyWriter body = new NetworkApiBodyWriter(classPath, project);
            body.writeCode();
        }
    }
}
