using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using kernel;
using kernel.models;
using kernel.property;
using kernel.plist;
using coder;
using System.Drawing.Imaging;

namespace CocosCodeCreater
{
    public partial class Main : Form,ImageChanger
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private SolutionExplorer m_solutionExplorer;
        private PropertyExplorer m_propertyExplorer;
        private SceneExplorer m_sceneExplorer;
        private PlistExplorer m_plistExplorer;
        private ApiExplorer m_apiExplorer;
        private ActionExplorer m_actionExplorer;

        private readonly ToolStripRenderer _toolStripProfessionalRenderer = new ToolStripProfessionalRenderer();
        private Global global;
        private MainDocument mainDocument;
        
        public Main()
        {
            InitializeComponent();
            this.dockPanel.Theme = this.vS2015LightTheme;
            global = new Global(this);
            AutoScaleMode = AutoScaleMode.Dpi;
            CreateStandardControls();
            m_solutionExplorer.RightToLeftLayout = RightToLeftLayout;
            m_propertyExplorer.RightToLeftLayout = RightToLeftLayout;
            m_sceneExplorer.RightToLeftLayout = RightToLeftLayout;
            m_plistExplorer.RightToLeftLayout = RightToLeftLayout;
            m_apiExplorer.RightToLeftLayout = RightToLeftLayout;
            m_actionExplorer.RightToLeftLayout = RightToLeftLayout;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            vsToolStripExtender1.DefaultRenderer = _toolStripProfessionalRenderer;
            
            //m_solutionExplorer.Show(dockPanel, DockState.DockRight);
            //dockPanel.ResumeLayout(true, true);
            mainDocument = new MainDocument(global);
            global.Document = mainDocument;
        }

        private void CreateStandardControls()
        {
            m_solutionExplorer = new SolutionExplorer(global);
            m_solutionExplorer.FormClosing += M_solutionExplorer_FormClosing;
            m_propertyExplorer = new PropertyExplorer();
            m_propertyExplorer.FormClosing += M_propertyExplorer_FormClosing;
            m_sceneExplorer = new SceneExplorer(global);
            m_sceneExplorer.FormClosing += M_sceneExplorer_FormClosing;
            m_plistExplorer = new PlistExplorer(global);
            m_plistExplorer.FormClosing += M_plistExplorer_FormClosing;
            m_apiExplorer = new ApiExplorer(global);
            m_apiExplorer.FormClosing += M_apiExplorer_FormClosing;
            m_actionExplorer = new ActionExplorer(global);
            m_actionExplorer.FormClosing += M_actionExplorer_FormClosing;
            global.Project = m_solutionExplorer.Project;
            global.PropertyGrid = m_propertyExplorer.PropertyGrid;
            global.SolutionExplorer = m_solutionExplorer;
            global.SolutionExplorer.Listener = global;
            global.PropertyExplorer = m_propertyExplorer;
            global.PropertyExplorer.Listener = global;
            global.SceneExplorer = m_sceneExplorer;
            global.PlistExplorer = m_plistExplorer;
            global.ApiExplorer = m_apiExplorer;
            global.ActionExplorer = m_actionExplorer;
        }

        private void M_solutionExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_solutionExplorer.Hide();
            solutionToolStripMenuItem.Checked = false;
        }

        private void M_propertyExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_propertyExplorer.Hide();
            propertyToolStripMenuItem.Checked = false;
        }

        private void M_sceneExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_sceneExplorer.Hide();
            sceneToolStripMenuItem.Checked = false;
        }

        private void M_plistExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_plistExplorer.Hide();
            plistToolStripMenuItem.Checked = false;
        }

        private void M_apiExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_apiExplorer.Hide();
            requestEditorToolStripMenuItem.Checked = false;
        }

        private void M_actionExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_actionExplorer.Hide();
            actionToolStripMenuItem.Checked = false;
        }

        public void OnExplorerClosed(string v)
        {
            if(v == "Scene")
            {
                sceneToolStripMenuItem.Checked = false;
                m_sceneExplorer.Hide();
            } else if(v == "Plist")
            {
                plistToolStripMenuItem.Checked = false;
                m_plistExplorer.Hide();
            }
        }
        public void ShowPlistExplorer(bool show)
        {
            if(plistToolStripMenuItem.Checked != show)
            {
                plistEditerToolStripMenuItem_Click(this, null);
            }
            if (show)
            {
                m_plistExplorer.Show(dockPanel, DockState.DockRight);
            }
        }

        public void ShowSceneExplorer(bool show)
        {
            if(sceneToolStripMenuItem.Checked != show)
            {
                sceneToolStripMenuItem_Click(this, null);
            } else
            {
                if (show)
                {
                    m_sceneExplorer.Show(dockPanel, DockState.DockRight);
                }
            }
        }

        public void ShowActionExplorer(bool show)
        {
            if (actionToolStripMenuItem.Checked != show)
            {
                actionToolStripMenuItem_Click(this, null);
            }
            else
            {
                if (show)
                {
                    m_actionExplorer.Show(dockPanel, DockState.DockRight);
                }
            }
        }

        public void ShowApiExplorer(bool show)
        {
            if (requestEditorToolStripMenuItem.Checked != show)
            {
                requestEditorToolStripMenuItem_Click(this, null);
            }
            else
            {
                if (show)
                {
                    m_apiExplorer.Show(dockPanel, DockState.DockRight);
                }
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(SolutionExplorer).ToString())
            {
                solutionToolStripMenuItem.Checked = true;
                return m_solutionExplorer;
            }
            if (persistString == typeof(PropertyExplorer).ToString())
            {
                propertyToolStripMenuItem.Checked = true;
                return m_propertyExplorer;
            }
            if (persistString == typeof(SceneExplorer).ToString())
            {
                sceneToolStripMenuItem.Checked = true;
                return m_sceneExplorer;
            }
            if (persistString == typeof(PlistExplorer).ToString())
            {
                plistToolStripMenuItem.Checked = true;
                return m_plistExplorer;
            }
            if (persistString == typeof(ApiExplorer).ToString())
            {
                requestEditorToolStripMenuItem.Checked = true;
                return m_apiExplorer;
            }

            if (persistString == typeof(ActionExplorer).ToString())
            {
                actionToolStripMenuItem.Checked = true;
                return m_actionExplorer;
            }

            return null;
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int y = this.menuStrip1.Location.Y + this.menuStrip1.Size.Height+this.toolStrip1.Size.Height;
            this.dockPanel.Location = new Point(0, y);
            this.dockPanel.Size = new Size(this.Size.Width,this.Size.Height - this.dockPanel.Location.Y);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //SetSchema(this.menuItemSchemaVS2013Blue, null);

            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CocosCodeCreater.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                //m_solutionExplorer.Show(dockPanel, DockState.DockRight);
                //dockPanel.ResumeLayout(true, true);
            }
            this.dockPanel.Theme = this.vS2015LightTheme;
            mainDocument.Show(dockPanel);
        }

        

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CocosCodeCreater.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private void solutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (solutionToolStripMenuItem.Checked)
            {
                m_solutionExplorer.Hide();
            } else
            {
                m_solutionExplorer.Show(dockPanel, DockState.DockRight);
            }
            solutionToolStripMenuItem.Checked = !solutionToolStripMenuItem.Checked;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetSchema(object sender, System.EventArgs e)
        {
            // Persist settings when rebuilding UI
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CocosCodeCreater.temp.config");

            dockPanel.SaveAsXml(configFile);
            CloseAllContents();

            if (sender == this.styleBlueToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015BlueTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015BlueTheme);
            }
            else if (sender == this.styleLightToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015LightTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015LightTheme);
            }
            else if (sender == this.styleDarkToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015DarkTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015DarkTheme);
            }

            styleLightToolStripMenuItem.Checked = (sender == styleLightToolStripMenuItem);
            styleBlueToolStripMenuItem.Checked = (sender == styleBlueToolStripMenuItem);
            styleDarkToolStripMenuItem.Checked = (sender == styleDarkToolStripMenuItem);
            
            if (dockPanel.Theme.ColorPalette != null)
            {
                //statusBar.BackColor = dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
            }

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            //vsToolStripExtender1.SetStyle(menuStrip1, version, theme);
            //vsToolStripExtender1.SetStyle(toolStrip1, version, theme);
            //vsToolStripExtender1.SetStyle(statusStrip1, version, theme);
        }

        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_solutionExplorer.DockPanel = null;
            m_propertyExplorer.DockPanel = null;
            m_sceneExplorer.DockPanel = null;
            m_plistExplorer.DockPanel = null;
            m_apiExplorer.DockPanel = null;
            m_actionExplorer.DockPanel = null;
            foreach (var window in dockPanel.FloatWindows.ToList())
                window.Dispose();

            
            System.Diagnostics.Debug.Assert(dockPanel.Panes.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.Contents.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.FloatWindows.Count == 0);
        }

        private void styleLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSchema(sender, e);
        }

        private void styleBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSchema(sender, e);
        }

        private void styleDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSchema(sender, e);
        }

        private void newSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectDialog dialog = new NewProjectDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                if (!ProjectTools.createProejct(dialog.ProjectPath, dialog.ProjectName))
                {
                    MessageBox.Show("项目已存在,创建失败!");
                    return;
                }
                m_solutionExplorer.Project = new ProjectInfo();
                SpriteListProperty.manager = m_solutionExplorer.Project;
                FontNameProperty.manager = m_solutionExplorer.Project;
                SceneListProperty.manager = m_solutionExplorer.Project;
                m_solutionExplorer.Project.Name = dialog.ProjectName;
                
                m_solutionExplorer.FileName = dialog.ProjectName + ".apj";
                m_solutionExplorer.FilePath = dialog.ProjectPath + "\\" + dialog.ProjectName;
                m_solutionExplorer.Project.WorkPath = m_solutionExplorer.FilePath ;
                m_solutionExplorer.Project.OutputPath = dialog.ProjectPath + "\\" + dialog.ProjectName + "\\Output";
                m_solutionExplorer.TreeView.Nodes.Clear();
                TreeNode root = new TreeNode(dialog.ProjectName);

                TreeNode scenes = new TreeNode("Scenes");
                scenes.Tag = "Scenes";
                root.Nodes.Add(scenes);
                TreeNode components = new TreeNode("Components");
                components.Tag = "Components";
                root.Nodes.Add(components);

                TreeNode apis = new TreeNode("Apis");
                apis.Tag = "Apis";
                root.Nodes.Add(apis);

                TreeNode resources = new TreeNode("Resources");
                resources.Tag = "Resources";
                root.Nodes.Add(resources);


                TreeNode images = new TreeNode("Images");
                images.Tag = "Images";
                resources.Nodes.Add(images);

                TreeNode fonts = new TreeNode("Fonts");
                fonts.Tag = "Fonts";
                resources.Nodes.Add(fonts);

                TreeNode sounds = new TreeNode("Sounds");
                sounds.Tag = "Sounds";
                resources.Nodes.Add(sounds);

                TreeNode plists = new TreeNode("Plists");
                plists.Tag = "Plists";
                resources.Nodes.Add(plists);


                m_solutionExplorer.TreeView.Nodes.Add(root);
                m_solutionExplorer.TreeView.ExpandAll();
            }
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (propertyToolStripMenuItem.Checked)
            {
                m_propertyExplorer.Hide();
            }
            else
            {
                m_propertyExplorer.Show(dockPanel, DockState.DockRight);
            }
            propertyToolStripMenuItem.Checked = !propertyToolStripMenuItem.Checked;
        }
        private void requestEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(requestEditorToolStripMenuItem.Checked)
            {
                m_apiExplorer.Hide();
            } else
            {
                m_apiExplorer.Show(dockPanel, DockState.DockRight);
            }
            requestEditorToolStripMenuItem.Checked = !requestEditorToolStripMenuItem.Checked;
        }


        private void sceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sceneToolStripMenuItem.Checked)
            {
                m_sceneExplorer.Hide();
            }
            else
            {
                m_sceneExplorer.Show(dockPanel, DockState.DockRight);
            }
            sceneToolStripMenuItem.Checked = !sceneToolStripMenuItem.Checked;
        }

        private void saveSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_solutionExplorer.Project != null)
            {
                ProjectTools.saveProejct(m_solutionExplorer.FilePath, m_solutionExplorer.FileName, m_solutionExplorer.Project);
            }
        }

        private void openSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "项目文件(*.apj)|*.apj";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                m_solutionExplorer.Project = ProjectTools.openProject(dialog.FileName);
                SpriteListProperty.manager = m_solutionExplorer.Project;
                FontNameProperty.manager = m_solutionExplorer.Project;
                SceneListProperty.manager = m_solutionExplorer.Project;
                FileInfo file =  new FileInfo(dialog.FileName);
                m_solutionExplorer.FileName = file.Name;
                m_solutionExplorer.FilePath = file.Directory.FullName;
                m_solutionExplorer.TreeView.Nodes.Clear();
                
                TreeNode root = new TreeNode(m_solutionExplorer.Project.Name);
                TreeNode scenes = new TreeNode("Scenes");
                scenes.Tag = "Scenes";
                root.Nodes.Add(scenes);
                for(int i = 0; i < m_solutionExplorer.Project.Scenes.Count; i++)
                {
                    TreeNode scene = new TreeNode(m_solutionExplorer.Project.Scenes[i].Name);
                    scene.Tag = m_solutionExplorer.Project.Scenes[i];
                    scenes.Nodes.Add(scene);
                }
                TreeNode components = new TreeNode("Components");
                components.Tag = "Components";
                root.Nodes.Add(components);
                for (int i = 0; i < m_solutionExplorer.Project.Components.Count; i++)
                {
                    TreeNode component = new TreeNode(m_solutionExplorer.Project.Components[i].Name);
                    component.Tag = m_solutionExplorer.Project.Components[i];
                    components.Nodes.Add(component);
                }

                TreeNode apis = new TreeNode("Apis");
                apis.Tag = "Apis";
                root.Nodes.Add(apis);
                for (int i = 0; i < m_solutionExplorer.Project.Requests.Count; i++)
                {
                    TreeNode component = new TreeNode(m_solutionExplorer.Project.Requests[i].Name);
                    component.Tag = m_solutionExplorer.Project.Requests[i];
                    apis.Nodes.Add(component);
                }


                TreeNode resources = new TreeNode("Resources");
                resources.Tag = "Resources";
                root.Nodes.Add(resources);
                TreeNode images = new TreeNode("Images");
                images.Tag = "Images";
                resources.Nodes.Add(images);

                for (int i = 0; i < m_solutionExplorer.Project.Images.Count; i++)
                {
                    TreeNode resource = new TreeNode(m_solutionExplorer.Project.Images[i].Name);
                    resource.Tag = m_solutionExplorer.Project.Images[i];
                    m_solutionExplorer.Project.Images[i].Type = "Image";
                    images.Nodes.Add(resource);
                }

                TreeNode fonts = new TreeNode("Fonts");
                fonts.Tag = "Fonts";
                resources.Nodes.Add(fonts);
                for (int i = 0; i < m_solutionExplorer.Project.Fonts.Count; i++)
                {
                    //FileInfo fi = new FileInfo(m_solutionExplorer.Project.Resources[i].Name);
                    TreeNode resource = new TreeNode(m_solutionExplorer.Project.Fonts[i].Name);
                    resource.Tag = m_solutionExplorer.Project.Fonts[i];
                    m_solutionExplorer.Project.Fonts[i].Type = "Font";
                    fonts.Nodes.Add(resource);
                }

                TreeNode sounds = new TreeNode("Sounds");
                sounds.Tag = "Sounds";
                resources.Nodes.Add(sounds);
                for (int i = 0; i < m_solutionExplorer.Project.Sounds.Count; i++)
                {
                    TreeNode resource = new TreeNode(m_solutionExplorer.Project.Sounds[i].Name);
                    resource.Tag = m_solutionExplorer.Project.Sounds[i];
                    m_solutionExplorer.Project.Sounds[i].Type = "Sound";
                    sounds.Nodes.Add(resource);
                }

                TreeNode plists = new TreeNode("Plists");
                plists.Tag = "Plists";
                resources.Nodes.Add(plists);
                for (int i = 0; i < m_solutionExplorer.Project.Plists.Count; i++)
                {
                    TreeNode resource = new TreeNode(m_solutionExplorer.Project.Plists[i].Name);
                    resource.Tag = m_solutionExplorer.Project.Plists[i];
                    m_solutionExplorer.Project.Plists[i].Type = "Plist";
                    plists.Nodes.Add(resource);
                }

                m_solutionExplorer.TreeView.Nodes.Add(root);
                m_solutionExplorer.TreeView.ExpandAll();
                //this.txtboxPath.SelectedText = dialog.FileName;
            }

        }
        public Object SelectObject { set {
                mainDocument.Editer = null;
                if (value is Scene)
                {
                    mainDocument.Editer = EditorFactory.getEditer(value,global);
                    ShowActionExplorer(true);
                    ShowSceneExplorer(true);
                    //ShowPlistExplorer(false);
                } else if(value is PListInfo)
                {
                    mainDocument.Editer = EditorFactory.getEditer(value, global);
                    ShowPlistExplorer(true);
                    //ShowSceneExplorer(false);
                } else if(value is kernel.models.Component)
                {
                    mainDocument.Editer = EditorFactory.getEditer(value, global);
                    ShowActionExplorer(true);
                    ShowSceneExplorer(true);
                    //ShowPlistExplorer(false);
                } else if(value is kernel.models.Request)
                {
                    ShowApiExplorer(true);
                }
                mainDocument.Refresh();
            }
        }

        private void plistEditerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (plistToolStripMenuItem.Checked)
            {
                m_plistExplorer.Hide();
            }
            else
            {
                m_plistExplorer.Show(dockPanel, DockState.DockRight);
            }
            plistToolStripMenuItem.Checked = !plistToolStripMenuItem.Checked;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String basepath = global.Project.OutputPath;
            if (Directory.Exists(basepath))
            {
                DeleteDir(basepath);
            }

            String rPath = basepath + "//Resources";
            String classPath = basepath + "//Classes";
            String fontPath = basepath + "//Resources//fonts";
            String resPath = basepath + "//Resources//res";
            Directory.CreateDirectory(basepath);
            Directory.CreateDirectory(rPath);
            Directory.CreateDirectory(classPath);
            Directory.CreateDirectory(classPath+"//Logics");
            Directory.CreateDirectory(classPath + "//Messages");
            Directory.CreateDirectory(fontPath);
            Directory.CreateDirectory(resPath);

            CodeCreater.imageChanger = this;
            for (int i = 0; i < global.Project.Scenes.Count; i++)
            {
                FileFactory.createScene(classPath, global.Project, global.Project.Scenes[i]);
                for(int j=0;j< global.Project.Scenes[i].Layers.Count; j++)
                {
                    FileFactory.createLayer(classPath, global.Project,(Layer)global.Project.Scenes[i].Layers[j]);
                    
                }
            }
            for(int i = 0; i < global.Project.Components.Count; i++)
            {
                FileFactory.createComponent(classPath, global.Project,global.Project.Components[i]);
            }
            for (int i = 0; i < global.Project.Requests.Count; i++)
            {
                Request req = global.Project.Requests[i];
                FileFactory.createRequest(classPath, global.Project, req);
            }
            FileFactory.createNetworkApi(classPath, global.Project);

            for (int i = 0; i < global.Project.Plists.Count; i++)
            {
                FileFactory.createPlist(resPath, global.Project, global.Project.Plists[i]);
            }

            for(int i = 0; i < global.Project.Fonts.Count; i++)
            {
                Resource font = global.Project.Fonts[i];
                FileInfo file = new FileInfo(font.Path);
                file.CopyTo(Path.Combine(fontPath, file.Name));
            }
            

            for (int i = 0; i < global.Project.Images.Count; i++)
            {
                //Resource image = global.Project.Images[i];
                //FileInfo file = new FileInfo(image.Path);
                //file.CopyTo(Path.Combine(resPath, file.Name));
            }
            MessageBox.Show("生成完成");
        }

        public void changeImage(string src, string dest, int width, int height,float scale)
        {
            Bitmap bufferImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bufferImage);
            g.Clear(Color.Transparent);
            try
            {
                Image image = Image.FromFile(src);
                if(scale != 1)
                {
                    int w = (int)(width * scale);
                    int h = (int)(height * scale);
                    g.DrawImage(image, new Rectangle((width - w)/2, (height - h)/2, w, h), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                } else {
                    g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                }
                
                if (src.EndsWith(".jpg"))
                    bufferImage.Save(dest, ImageFormat.Jpeg);
                else if (src.EndsWith(".png"))
                    bufferImage.Save(dest, ImageFormat.Png);
            } catch(Exception exp)
            {

            }
            
        }

        public static void DeleteDir(string file)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }
                    //删除空文件夹
                    Directory.Delete(file);
                }

            }
            catch (Exception ex) // 异常处理
            {
                Console.WriteLine(ex.Message.ToString());// 异常信息
            }
        }

        private void actionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (actionToolStripMenuItem.Checked)
            {
                m_actionExplorer.Hide();
            }
            else
            {
                m_actionExplorer.Show(dockPanel, DockState.DockRight);
            }
            actionToolStripMenuItem.Checked = !actionToolStripMenuItem.Checked;
        }
    }
}
