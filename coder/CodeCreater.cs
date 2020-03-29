using kernel;
using kernel.actions;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace coder
{
    public class CodeCreater
    {
        public static ImageChanger imageChanger;

        public static void addInclude(FileBase file, ProjectInfo project,NodeContainer container)
        {
            for(int i = 0; i < container.Layers.Count; i++)
            {
                file.a("#include \"").a(container.Layers[i].Name).al(".h\"");
            }
            HashSet<String> set = new HashSet<String>();
            for (int i = 0; i < container.Components.Count; i++)
            {
                ComponentRef c = (ComponentRef)container.Components[i];
                for(int j = 0; j < project.Components.Count; j++)
                {
                    if(project.Components[j].UUID == c.RefUUID)
                    {
                        if (!set.Contains(c.RefUUID))
                        {
                            file.a("#include \"").a(project.Components[j].Name).al(".h\"");
                            set.Add(c.RefUUID);
                        }
                        break;
                    }
                }
            }
        }

        public static void addBodyInclude(FileBase file, ProjectInfo project, NodeContainer container)
        {
            HashSet<String> set = new HashSet<String>();
            for (int i = 0; i < container.Buttons.Count; i++)
            {
                Button c = (Button)container.Buttons[i];
                if (c.ActionType == "NavigateTo")
                {
                    String sceneName = c.DestScene;
                    if (!set.Contains(sceneName))
                    {
                        file.a("#include \"").a(sceneName).al(".h\"");
                        set.Add(sceneName);
                    }
                }
            }
        }

        public static void addParams(FileBase file, NodeContainer container)
        {
            for (int i = 0; i < container.Layers.Count; i++)
            {
                String name = container.Layers[i].Name;
                //file.t().a("Layer* m_").a(name).a(";").e();
                file.t().a("CC_SYNTHESIZE(Layer*,m_").a(name).a(",").a(mainName(name)).al(");");    
            }
            for (int i = 0; i < container.Sprites.Count; i++)
            {
                String name = container.Sprites[i].Name;
                //file.t().a("Sprite* m_").a(name).a(";").e();
                if (container.Sprites[i].Touchable)
                {
                    file.t().a("CC_SYNTHESIZE(TouchableSprite*,m_").a(name).a(",").a(mainName(name)).al(");");
                } else
                {
                    file.t().a("CC_SYNTHESIZE(Sprite*,m_").a(name).a(",").a(mainName(name)).al(");");
                }
            }
            for (int i = 0; i < container.Labels.Count; i++)
            {
                String name = container.Labels[i].Name;
                //file.t().a("Label* m_").a(name).a(";").e();
                file.t().a("CC_SYNTHESIZE(Label*,m_").a(name).a(",").a(mainName(name)).al(");");
            }
            for (int i = 0; i < container.Buttons.Count; i++)
            {
                String name = container.Buttons[i].Name;
                //file.t().a("Button* m_").a(name).a(";").e();
                file.t().a("CC_SYNTHESIZE(Button*,m_").a(name).a(",").a(mainName(name)).al(");");
            }
            for (int i = 0; i < container.Components.Count; i++)
            {
                String name = container.Components[i].Name;
                //file.t().a("Button* m_").a(name).a(";").e();
                file.t().a("CC_SYNTHESIZE(Node*,m_").a(name).a(",").a(mainName(name)).al(");");
            }
        }

        public static void addTouchTAGS(FileBase file, ProjectInfo project, NodeContainer container)
        {
            int index = 1;
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite s = (Sprite)node;
                    if (s.Touchable)
                    {
                        file.a("#define TOUCH_TAG_").a(node.Name.ToUpper()).a(" ").a(index).e();
                        index++;
                    }
                }
                if(node.Type == "ComponentRef")
                {
                    Component component = getComponentByRef(project, (ComponentRef)node);
                    if (component.Touchable)
                    {
                        file.a("#define TOUCH_TAG_").a(node.Name.ToUpper()).a(" ").a(index).e();
                        index++;
                    }
                }
            }
        }

        public static void addTouchMethodBodys(FileBase file, ProjectInfo project, String fileName, NodeContainer container)
        {
            bool notifyTouchEvent = false;
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite s = (Sprite)node;
                    if (s.Touchable && s.NotifyTouchEvent && !notifyTouchEvent)
                    {
                        notifyTouchEvent = true;
                        break;
                        //file.t().a("virtual void onTouchBegin(Node* node, int tag, Touch* touch, Event* event);");
                        //file.t().a("virtual void onTouchMove(Node* node, int tag, Touch* touch, Event* event)");
                    }
                }
            }
            if (notifyTouchEvent)
            {
                file.a("void ").a(fileName).a(":: onTouchBegin(Node* node,int subTag, Touch* touch, Event* event) {").e();
                for (int i = 0; i < container.Children.Count; i++)
                {
                    Node node = container.FindNodeByUUID(container.Children[i]);
                    if (node.Type == "Sprite")
                    {
                        Sprite s = (Sprite)node;
                        if (s.Touchable && s.NotifyTouchEvent )
                        {
                            file.t().a("if(node->getTag() == TOUCH_TAG_").a(node.Name.ToUpper()).a("){").e();
                            file.t().t().a("m_").a(container.Name).a("Logic->on").a(mainName(node.Name)).a("TouchBegin(node,touch,event);").e();
                            file.t().a("}").e().e();
                        }
                    }
                }
                file.a("}").e().e();

                file.a("void ").a(fileName).a(":: onTouchMove(Node* node,int subTag, Touch* touch, Event* event) {").e();
                for (int i = 0; i < container.Children.Count; i++)
                {
                    Node node = container.FindNodeByUUID(container.Children[i]);
                    if (node.Type == "Sprite")
                    {
                        Sprite s = (Sprite)node;
                        if (s.Touchable && s.NotifyTouchEvent)
                        {
                            file.t().a("if(node->getTag() == TOUCH_TAG_").a(node.Name.ToUpper()).a("){").e();
                            file.t().t().a("m_").a(container.Name).a("Logic->on").a(mainName(node.Name)).a("TouchMove(node,touch,event);").e();
                            file.t().a("}").e().e();
                        }
                    }
                }
                file.a("}").e().e();
            }

            file.a("void ").a(fileName).a(":: onTouchNode(Node* node,int subTag, Touch* touch, Event* event) {").e();

            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite s = (Sprite)node;
                    if (s.Touchable)
                    {
                        file.t().a("if(node->getTag() == TOUCH_TAG_").a(node.Name.ToUpper()).a("){").e();
                        file.t().t().a("m_").a(container.Name).a("Logic->on").a(mainName(node.Name)).a("Touched(node,touch,event);").e();
                        file.t().a("}").e().e();
                    }
                }

                if (node.Type == "ComponentRef")
                {
                    Component component = getComponentByRef(project, (ComponentRef)node);
                    if (component.Touchable)
                    {
                        file.t().a("if(node->getTag() == TOUCH_TAG_").a(node.Name.ToUpper()).a("){").e();
                        file.t().t().a("m_").a(container.Name).a("Logic->on").a(mainName(node.Name)).a("Touched(node,subTag,touch,event);").e();
                        file.t().a("}").e().e();


                    }
                }
            }
            file.a("}").e().e();


            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Button")
                {
                    file.a("void ").a(fileName).a(":: on").a(mainName(node.Name)).a("Touched(cocos2d::Ref* pSender, cocos2d::ui::Widget::TouchEventType type) {").e();
                    file.t().a("if (type == Widget::TouchEventType::ENDED){").e();
                    Button button = (Button)node;
                    if(button.ActionType == "NavigateTo")
                    {
                        file.t().t().a("Director::getInstance()->pushScene(").a(button.DestScene).a("::createScene());").e();
                    } else if(button.ActionType == "NavigateBack")
                    {
                        file.t().t().a("Director::getInstance()->popScene();").e();
                    } else 
                    {
                        file.t().t().a("m_").a(container.Name).a("Logic->on").a(mainName(node.Name)).a("Touched();").e();
                    }
                    file.t().a("}").e();
                    file.a("}").e().e();
                }
            }
        }

        public static void addTouchMethodHeaders(FileBase file, NodeContainer container)
        {
            bool notifyTouchEvent = false;
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite s = (Sprite)node;
                    if (s.Touchable&&s.NotifyTouchEvent&&!notifyTouchEvent)
                    {
                        notifyTouchEvent = true;
                        file.t().a("virtual void onTouchBegin(Node* node, int tag, Touch* touch, Event* event);").e();
                        file.t().a("virtual void onTouchMove(Node* node, int tag, Touch* touch, Event* event);").e();
                    }
                }
            }
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Button")
                {
                    Button s = (Button)node;
                    file.t().a("void on").a(mainName(node.Name)).al("Touched(cocos2d::Ref* pSender, cocos2d::ui::Widget::TouchEventType type);");
                }
            }
        }

        public static void addInit(FileBase file,ProjectInfo project, String fileName,NodeContainer container)
        {
            file.t().al("auto* chnStrings = Dictionary::createWithContentsOfFile(\"config.xml\");");



            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Layer")
                {
                    createLayerInit(file, (Layer)node);
                }
                else if (node.Type == "Button")
                {
                    createButtonInit(file, project, fileName,(Button)node, "this");
                }
                else if (node.Type == "Sprite")
                {
                    createSpriteInit(file, project,(Sprite)node, "this");
                }
                else if (node.Type == "Label")
                {
                    createLabelInit(file, (kernel.models.Label)node, "this");
                }
                else if(node.Type == "ComponentRef")
                {
                    createComponentInit(file,project, (ComponentRef)node, "this");
                }
            }

            /*
            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite sprite = (Sprite)node;
                    if (sprite.Touchable)
                    {
                        String paramName = sprite.Name + "TouchListener";
                        file.t().a("auto ").a(paramName).a("= EventListenerTouchOneByOne::create();").e();
                        file.t().a(paramName).a("->setSwallowTouches(true);").e();
                        file.t().a(paramName).a("->onTouchBegan =  CC_CALLBACK_2(").a(fileName).a("::onTouch").a(mainName(sprite.Name)).a("Begin,this);").e();
                        file.t().a(paramName).a("->onTouchEnded =  CC_CALLBACK_2(").a(fileName).a("::onTouch").a(mainName(sprite.Name)).a("Ended,this);").e();
                        file.t().a("getEventDispatcher()->addEventListenerWithSceneGraphPriority(").a(paramName).a(", this->m_").a(sprite.Name).a(");").e();
                    }
                }
            }*/
                   
            /*
            file.t().al("touchListener->onTouchBegan = [](Touch * touch, Event * event) {");
            file.t().t().al("auto target = static_cast<Node*>(event->getCurrentTarget());");
            file.t().t().al("Size targetSize = target->getContentSize();");
            file.t().t().al("Point locationInNode = target->convertToNodeSpace(touch->getLocation());");
            file.t().t().al("Size s = target->getContentSize();");
            file.t().t().al("Rect rect = Rect(0, 0, s.width, s.height);");
            file.t().t().al("if (rect.containsPoint(locationInNode)){");
            file.t().t().t().al("ActionTools::startClick(target);");
            file.t().t().t().al("return true;");
            file.t().t().al("}");
            file.t().t().al("return false;");
            file.t().al("};").e();

            file.t().al("touchListener->onTouchMoved = [] (Touch* touch, Event* event) {");
            file.t().t().al("auto target = static_cast<Node*>(event->getCurrentTarget());");
            file.t().al("};").e();


            file.t().al("touchListener->onTouchEnded = [=](Touch* touch, Event* event) {");
            file.t().t().al("auto target = static_cast<Node*>(event->getCurrentTarget());");
            file.t().t().al("ActionTools::endClick(target);");

            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite sprite = (Sprite)node;
                    if (sprite.Touchable)
                    {
                        file.t().t().a("if(target->getTag() == TOUCH_TAG_").a(sprite.Name.ToUpper()).al(") {");
                        file.t().t().t().a("onTouch").a(mainName(sprite.Name)).al("();");
                        file.t().t().al("}");
                    }
                }
                 
            }
            file.t().al("};");

            for (int i = 0; i < container.Children.Count; i++)
            {
                Node node = container.FindNodeByUUID(container.Children[i]);
                if (node.Type == "Sprite")
                {
                    Sprite sprite = (Sprite)node;
                    if(sprite.Touchable)
                        file.t().a("getEventDispatcher()->addEventListenerWithSceneGraphPriority(touchListener, this->m_").a(sprite.Name).a(");").e();
                    
                }
            }*/
        }

        private static void createButtonInit(FileBase file, ProjectInfo project, String fileName,Button node, string parent)
        {
            file.t().a("// init m_").a(node.Name).e();

            file.t().a("m_").a(node.Name).a(" = Button::create(\"").a(getSpriteImage(project, node.NormalSprite,node.Size)).a("\",\"").a(getPressedImage(project, node.PressedSprite,node.Size,"0.9"));
            file.a("\",\"").a(getSpriteImage(project, node.DisableSprite,node.Size)).a("\",Widget::TextureResType::LOCAL);").e();

            file.t().a(parent).a("->addChild(m_").a(node.Name).al(");");
            //file.t().a("m_").a(node.Name).a("->setPosition(Vec2(").a(node.Location.X).a(",").a(node.Location.Y).a("));").e();
            //file.t().a("m_").a(node.Name).a("->setAnchorPoint(Vec2(").a(getAPValue(node.AnchorPoint.X)).a(",").a(getAPValue(node.AnchorPoint.Y)).a("));").e();
            //file.t().a("m_").a(node.Name).a("->setContentSize(Size(").a(node.Size.Width).a(",").a(node.Size.Height).a("));").e();
            Rectangle r = node.getNodeDisplayRect();
            file.t().a("ScaleTools::setNodeRectInCenter(m_").a(node.Name).a(",Rect(").a(r.Left).a(",").a(r.Top).a(",").a(r.Width).a(",").a(r.Height).a("));").e();
            //->setContentSize(Size(").a(node.Size.Width).a(",").a(node.Size.Height).a("));").e();
            
            file.t().a("m_").a(node.Name).a("->addTouchEventListener(CC_CALLBACK_2(").a(fileName).a("::on").a(mainName(node.Name)).a("Touched, this));").e();
            if (node.Label.Length > 0)
            {
                if (node.LoadTextFromConfig)
                {
                    file.t().a("const char* ").a(node.Label).a(" = ((String*)chnStrings->objectForKey(\"").a(node.Label).a("\"))->getCString();").e();
                    file.t().a("auto ").a(node.Name).a("Label = Label::createWithTTF(").a(node.Label).a(", \"fonts/").a(node.FontName).a("\",").a(node.FontSize).a(", Size::ZERO, TextHAlignment::CENTER, TextVAlignment::CENTER);").e();
                }
                else
                {
                    file.t().a("auto").a(node.Name).a("Label = Label::createWithTTF(\"").a(node.Label).a("\", \"fonts/").a(node.FontName).a("\",").a(node.FontSize).a(", Size::ZERO, TextHAlignment::CENTER, TextVAlignment::CENTER);").e();
                }
                file.t().a("").a(node.Name).a("Label->setTextColor(Color4B(").a((int)node.Color.R).a(",").a((int)node.Color.G).a(",").a((int)node.Color.B).a(",").a((int)node.Color.A).a("));").e();
                if (node.Shadow.X != 0 && node.Shadow.Y != 0)
                {
                    file.t().a("").a(node.Name).a("Label->enableShadow(Color4B(").a((int)node.ShadowColor.R).a(",").a((int)node.ShadowColor.G).a(",").a((int)node.ShadowColor.B).a(",").a((int)node.ShadowColor.A).a("), Size(").a(node.Shadow.X).a(",").a(node.Shadow.Y).al("));");
                }
                file.t().a("").a(node.Name).a("Label->setPosition(Vec2(").a((r.Left + r.Right)/2).a(",").a((r.Top+r.Bottom)/2).a("));").e();
                file.t().a("").a(node.Name).a("Label->setAnchorPoint(Vec2(0.5,0.5));").e();
                file.t().a("this->addChild(").a(node.Name).a("Label);").e();
            }
            file.e();
        }

        private static String getAPValue(int i)
        {
            if (i == 0)
                return "0";
            else if (i == 2)
                return "1";
            else
                return "0.5";
        }
        private static void createLabelInit(FileBase file, Label node, string parent)
        {
            file.t().a("// init m_").a(node.Name).e();
            
            if (node.LoadFromConfig)
            {
                file.t().a("const char* ").a(node.Value).a(" = ((String*)chnStrings->objectForKey(\"").a(node.Value).a("\"))->getCString();").e();
                file.t().a("m_").a(node.Name).a(" = Label::createWithTTF(").a(node.Value).a(", \"fonts/").a(node.FontName).a("\",").a(node.FontSize).a(", Size::ZERO, TextHAlignment::CENTER, TextVAlignment::CENTER);").e();
            } else
            {
                file.t().a("m_").a(node.Name).a(" = Label::createWithTTF(\"").a(node.Value).a("\", \"fonts/").a(node.FontName).a("\",").a(node.FontSize).a(", Size::ZERO, TextHAlignment::CENTER, TextVAlignment::CENTER);").e();
            }
            file.t().a(parent).a("->addChild(m_").a(node.Name).al(");");
            file.t().a("m_").a(node.Name).a("->setTextColor(Color4B(").a((int)node.Color.R).a(",").a((int)node.Color.G).a(",").a((int)node.Color.B).a(",").a((int)node.Color.A).a("));").e();
            if (node.Shadow.X != 0 && node.Shadow.Y != 0)
            {
                file.t().a("m_").a(node.Name).a("->enableShadow(Color4B(").a((int)node.ShadowColor.R).a(",").a((int)node.ShadowColor.G).a(",").a((int)node.ShadowColor.B).a(",").a((int)node.ShadowColor.A).a("), Size(").a(node.Shadow.X).a(",").a(node.Shadow.Y).al("));");
            }
            file.t().a("m_").a(node.Name).a("->setPosition(Vec2(").a(node.Location.X).a(",").a(node.Location.Y).a("));").e();
            file.t().a("m_").a(node.Name).a("->setAnchorPoint(Vec2(").a(getAPValue(node.AnchorPoint.X)).a(",").a(getAPValue(node.AnchorPoint.Y)).a("));").e();
            if (node.Size.Width != 0)
                file.t().a("m_").a(node.Name).a("->setWidth(").a(node.Size.Width).a(");").e();
            if (node.Size.Height != 0)
                file.t().a("m_").a(node.Name).a("->setHeight(").a(node.Size.Height).a(");").e();
            
            file.e();
        }

        private static void createSpriteInit(FileBase file, ProjectInfo project,Sprite node,String parent)
        {
            file.t().a("// init m_").a(node.Name).e();
            if (node.Touchable)
            {
                file.t().a("m_").a(node.Name).a(" = TouchableSprite::create(\"").a(getSpriteImage(project,node.SpriteFrame,node.Size)).a("\",this,TOUCH_TAG_").a(node.Name.ToUpper()).a(");").e();
            } else
            {
                file.t().a("m_").a(node.Name).a(" = Sprite::create(\"").a(getSpriteImage(project, node.SpriteFrame,node.Size)).al("\");");
                
            }
            file.t().a(parent).a("->addChild(m_").a(node.Name).al(");");
            Rectangle rect = node.getNodeDisplayRect();
            //file.t().a("ScaleTools::setNodeRectInCenter(").a("m_").a(node.Name).a(",Rect(").a(rect.X).a(",").a(rect.Y).a(",").a(rect.Width).a(",").a(rect.Height).al("));");
            file.t().a("m_").a(node.Name).a("->setAnchorPoint(Vec2(").a(getAPValue(node.AnchorPoint.X)).a(",").a(getAPValue(node.AnchorPoint.Y)).a("));").e();
            file.t().a("m_").a(node.Name).a("->setPosition(Vec2(").a(node.Location.X).a(",").a(node.Location.Y).a("));").e();
            file.t().a("m_").a(node.Name).a("->setContentSize(Size(").a(node.Size.Width).a(",").a(node.Size.Height).a("));").e();
            file.t().al("");

            file.e();
        }

        

        private static void createComponentInit(FileBase file, ProjectInfo project, ComponentRef node,  string parent)
        {
            Component component = getComponentByRef(project, node);
            file.t().a("// init m_").a(node.Name).e();
            file.t().a("m_").a(node.Name).a(" = ").a(component.Name).a("::createNode(this);").e();
            file.t().a(parent).a("->addChild(m_").a(node.Name).al(");");
            Rectangle rect = node.getNodeDisplayRect();
            if (node.Touchable)
            {
                file.t().a("m_").a(node.Name).a("->setTag(TOUCH_TAG_").a(node.Name.ToUpper()).a(");").e();
            }
            file.t().a("m_").a(node.Name).a("->setPosition(Vec2(").a(node.Location.X).a(",").a(node.Location.Y).a("));").e();
            //file.t().a("m_").a(node.Name).a("->setAnchorPoint(Vec2(").a(getAPValue(node.AnchorPoint.X)).a(",").a(getAPValue(node.AnchorPoint.Y)).a("));").e();
            file.t().a("m_").a(node.Name).a("->setAnchorPoint(Vec2(").a(getAPValue(node.AnchorPoint.X)).a(",").a(getAPValue(node.AnchorPoint.Y)).a("));").e();
            file.t().a("m_").a(node.Name).a("->setContentSize(Size(").a(node.Size.Width).a(",").a(node.Size.Height).a("));").e();


            file.t().al("");

            file.e();
        }

        private static Component getComponentByRef(ProjectInfo project,  ComponentRef node)
        {
            Component component = null;
            for (int i = 0; i < project.Components.Count; i++)
            {
                if (project.Components[i].UUID == node.RefUUID)
                {
                    component = project.Components[i];
                    break;
                }
            }
            return component;
        }

        private static void createLayerInit(FileBase file, Layer node)
        {
            file.t().a("// init m_").a(node.Name).e();
            file.e();
        }

        private static String mainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

        private static string getSpriteImage(ProjectInfo project,String spriteName,Size size)
        {
            String path = project.OutputPath + "//Resources//res//";
            Resource res = project.GetImage(spriteName);
            if(res == null)
            {
                return "";
            }
            FileInfo file = new FileInfo(res.Path);
            int pos = file.Name.LastIndexOf(".");
            String fileName = file.Name.Substring(0, pos);
            String ext = file.Name.Substring(pos + 1);
            String destFileName = fileName + "_" + size.Width + "x" + size.Height +"." + ext;
            FileInfo destFile = new FileInfo(path + destFileName);
            if (!destFile.Exists)
            {
                imageChanger.changeImage(res.Path, destFile.FullName, size.Width, size.Height,1);
                //Bitmap bufferImage = new Bitmap(sprite.Size.Width, sprite.Size.Height);
                //Graphics bufferGraph = Graphics.FromImage(bufferImage);
                //bufferGraph.Clear(Color.Transparent);
            }
            return "res/"+ destFileName;
        }

        private static string getPressedImage(ProjectInfo project, String spriteName, Size size,String scale)
        {
            String path = project.OutputPath + "//Resources//res//";
            Resource res = project.GetImage(spriteName);
            if (res == null)
            {
                return "";
            }
            FileInfo file = new FileInfo(res.Path);
            int pos = file.Name.LastIndexOf(".");
            String fileName = file.Name.Substring(0, pos);
            String ext = file.Name.Substring(pos + 1);
            String destFileName = fileName + "_" + scale+  "_" + size.Width + "x" + size.Height+ "." + ext;
            FileInfo destFile = new FileInfo(path + destFileName);
            if (!destFile.Exists)
            {
                float s = float.Parse(scale);
                imageChanger.changeImage(res.Path, destFile.FullName, size.Width, size.Height, s);
                //Bitmap bufferImage = new Bitmap(sprite.Size.Width, sprite.Size.Height);
                //Graphics bufferGraph = Graphics.FromImage(bufferImage);
                //bufferGraph.Clear(Color.Transparent);
            }
            return "res/" + destFileName;
        }

        public static void addActionHeaders(FileBase file, ProjectInfo project, NodeContainer container)
        {
            for(int i = 0; i < container.Actions.Count; i++)
            {
                ActionOwner action = container.Actions[i];
                
                if(container.FindNodeByUUID(action.ActionNodeUUID)==null)
                    file.t().a("void run").a(mainName(action.Name)).a("Action(Node *actionNode);").e();
                else
                    file.t().a("void run").a(mainName(action.Name)).a("Action();").e();
            }
        }

        public static void addActionBodys(FileBase file, ProjectInfo project, String fileName, NodeContainer container)
        {
            for (int i = 0; i < container.Actions.Count; i++)
            {
                ActionOwner action = container.Actions[i];
                Node node = container.FindNodeByUUID(action.ActionNodeUUID);
                if(node == null)
                    file.a("void ").a(fileName).a("::run").a(mainName(action.Name)).a("Action(Node *actionNode){").e();
                else
                    file.a("void ").a(fileName).a("::run").a(mainName(action.Name)).a("Action(){").e();
                HashSet<string> context = new HashSet<string>();
                if(action.Action != null)
                {
                    string param = getParams(context, action.Action.Type);
                    string code = action.Action.createCode(context,param);
                    file.a(code);
                    if (node == null)
                        file.t().a("actionNode->runAction(").a(param).a(");").e();
                    else
                        file.t().a("m_").a(action.ActionNode).a("->runAction(").a(param).a(");").e();
                }
                file.a("}").e().e();
            }
        }

        public static String getParams(HashSet<string> context,String prefix)
        {
            int index = 1;
            String key = prefix + index;
            while (context.Contains(key))
            {
                index++;
                key = prefix + index;
            }
            context.Add(key);
            return key;
        }
    }
}
