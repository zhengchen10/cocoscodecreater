using kernel.models;
using kernel.plist;
using kernel.property;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms.Design;

namespace kernel
{
    public class ProjectInfo  : IResourceManager
    {
        public ProjectInfo()
        {
            Scenes = new List<Scene>();
            Components = new List<models.Component>();
            Images = new List<Resource>();
            Fonts = new List<Resource>();
            Sounds = new List<Resource>();
            Plists = new List<PListInfo>();
            Requests = new List<Request>();
        }
        [Browsable(false)]
        public String Name { get; set; }
        [Browsable(false)]
        public String WorkPath { get; set; }
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor)), CategoryAttribute("基础信息")]
        public String OutputPath { get; set; }
        [Browsable(false)]
        public List<Scene> Scenes { get; set; }
        [Browsable(false)]
        public List<models.Component> Components { get; set; }
        [Browsable(false)]
        public List<Resource> Images { get; set; }
        [Browsable(false)]
        public List<Resource> Fonts { get; set; }
        [Browsable(false)]
        public List<Resource> Sounds { get; set; }
        [Browsable(false)]
        public List<PListInfo> Plists { get; set; }
        [Browsable(false)]
        public List<Request> Requests { get; set; }

        public void RemoveSceneByUUID(string uuid)
        {
            for(int i=0;i< Scenes.Count; i++)
            {
                if(Scenes[i].UUID == uuid)
                {
                    Scenes.RemoveAt(i);
                    break;
                }
            }
        }

        public Scene GetScene(String name)
        {
            for (int i = 0; i < Scenes.Count; i++)
            {
                if (Scenes[i].Name == name)
                {
                    return Scenes[i];
                }
            }
            return null;
        }

        public Resource GetImage(String name)
        {
            if (name == null)
                return null;
            for (int i = 0; i < Images.Count; i++)
            {
                if (Images[i].Name.EndsWith(name))
                {
                    return Images[i];
                }else if(Images[i].WorkPath+ Images[i].Name == name)
                {
                    return Images[i];
                }
            }
            return null;
        }

        public void RemoveImageByUUID(string uuid)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (Images[i].UUID == uuid)
                {
                    Images.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemoveFontByUUID(string uuid)
        {
            for (int i = 0; i < Fonts.Count; i++)
            {
                if (Fonts[i].UUID == uuid)
                {
                    Fonts.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveSoundByUUID(string uuid)
        {
            for (int i = 0; i < Sounds.Count; i++)
            {
                if (Sounds[i].UUID == uuid)
                {
                    Sounds.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemovePlistByUUID(string uuid)
        {
            for (int i = 0; i < Plists.Count; i++)
            {
                if (Plists[i].UUID == uuid)
                {
                    Plists.RemoveAt(i);
                    break;
                }
            }
        }


        public void RemoveComponentByUUID(string uuid)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].UUID == uuid)
                {
                    Components.RemoveAt(i);
                    break;
                }
            }
        }

        public models.Component GetComponent(String name)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Name == name)
                {
                    return Components[i];

                }
            }
            return null;
        }

        public string[] GetImages()
        {
            string[] result = new string[Images.Count];
            for(int i = 0; i < result.Length; i++)
            {
                Resource res = Images[i];
                if (res.WorkPath == null ||res.WorkPath.Length == 0)
                    result[i] = res.Name;
                else
                    result[i] = res.WorkPath+res.Name;
            }
            return result;
        }

        public string[] GetFonts()
        {
            string[] result = new string[Fonts.Count];
            for (int i = 0; i < result.Length; i++)
            {
                Resource res = Fonts[i];
                result[i] = res.Name;
            }
            return result;
        }

        public  models.Component getComponentByRef(ComponentRef node)
        {
            models.Component component = null;
            for (int i = 0; i < this.Components.Count; i++)
            {
                if (this.Components[i].UUID == node.RefUUID)
                {
                    component = this.Components[i];
                    break;
                }
            }
            return component;
        }

        public string[] GetScenes()
        {
            string[] result = new string[Scenes.Count];
            for (int i = 0; i < Scenes.Count; i++)
            {
                Scene res = Scenes[i];
                result[i] = res.Name;
            }
            return result;
        }
    }
}
