using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public class ComponentBodyWriter : FileBase
    {
        String fileName;
        Component component;
        ProjectInfo project;
        public ComponentBodyWriter(Component component, String path, ProjectInfo project, String fileName)
        {
            this.component = component;
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
            CodeCreater.addBodyInclude(this, project, component);
            e();
            CodeCreater.addTouchTAGS(this, project, component);
            e();
            a(fileName).a("::").a(fileName).al("(){");
            t().a("m_").a(fileName).a("Logic = new ").a(fileName).a("Logic();").e();
            al("}").e();
            a(fileName).a("::~").a(fileName).al("(){");
            t().a("delete m_").a(fileName).a("Logic;").e();
            al("}").e();

            a("Node* ").a(fileName).al("::createNode(NodesContainer * owner) {");
            t().a("Node* ret = ").a(fileName).a("::create();").e();
            t().a("((").a(fileName).a("*)ret)->m_owner = owner;").e();
            t().a("return ret;").e();
            al("}").e();

            a("bool ").a(fileName).al("::init() {");
            t().a("if (!Node::init()) {").e();
            t().t().a("return false;").e();
            t().a("}").e();
            t().al(" auto director = Director::getInstance();").e();

            CodeCreater.addInit(this, project,fileName,component);
            if (component.Touchable)
            {
                // t().a("auto touchListener = EventListenerTouchOneByOne::create();").e();
                // t().a("touchListener->setSwallowTouches(true);").e();
                // t().a("touchListener->onTouchBegan = CC_CALLBACK_2(").a(fileName).a("::onNodeTouchBegin, this);").e();
                // t().a("touchListener->onTouchEnded = CC_CALLBACK_2(").a(fileName).a("::onNodeTouchEnded, this);").e();
                // t().a(" getEventDispatcher()->addEventListenerWithSceneGraphPriority(touchListener, this);").e();
            }
            t().a("m_").a(fileName).a("Logic->onInit(this);").e();
            t().a("return true;").e();
            al("}").e();

            a("void ").a(fileName).al("::setStatus(int status) {");
            t().a("this->status = status;").e();
            t().a("m_").a(fileName).a("Logic->onSetStatus(status);").e();
            al("}").e();
            CodeCreater.addActionBodys(this, project, fileName, component);
            CodeCreater.addTouchMethodBodys(this,project, fileName,component);
            if (component.Touchable)
            {
               /* a("bool ").a(fileName).a("::onNodeTouchBegin(Touch * tTouch, Event * eEvent) {").e();
                t().a("return ActionTools::checkClick(tTouch,eEvent,false);").e();
                a("}").e().e();

                a("void ").a(fileName).a("::onNodeTouchEnded(Touch * tTouch, Event * eEvent) {").e();
                a("}").e().e();
                */
            }
        }
    }
}
