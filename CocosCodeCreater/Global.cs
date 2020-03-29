using kernel;
using kernel.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public class Global : ISolutionListener , IPropertyListener, IExplorerListener
    {
        private Main main;
        public Global(Main main)
        {
            this.main = main;
        }
        public Main Main { get { return main; } }
        public MainDocument Document { get; set; }
        public ProjectInfo Project { get; set; }
        public PropertyGrid PropertyGrid { get; set; }
        public PropertyExplorer PropertyExplorer { get; set; }
        public SolutionExplorer SolutionExplorer { get; set; }
        public SceneExplorer SceneExplorer { get; set; }
        public PlistExplorer PlistExplorer { get; set; }
        public ApiExplorer ApiExplorer { get; set; }
        public ActionExplorer ActionExplorer { get; set; }
        public ISolutionListener SolutionListener{ get{ return this; } }
        public IPropertyListener PropertyListener { get { return this; } }
        public IExplorerListener SceneExplorerListener { get { return this; } }
        public IExplorerListener PListExplorerListener { get { return this; } }


        public void onAddComponent(string name)
        {
            //throw new NotImplementedException();
        }

        public void onAddFont(string name, string path)
        {
            //throw new NotImplementedException();
        }

        public void onAddImage(string name, string path)
        {
            //throw new NotImplementedException();
        }

        public void onAddPlist(string name, string path)
        {
            //throw new NotImplementedException();
        }

        public void onAddResource(string name, string path)
        {
            //throw new NotImplementedException();
        }

        public void onAddScene(string name)
        {
            //throw new NotImplementedException();
        }

        public void onAddSound(string name, string path)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteComponent(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteFont(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteImage(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeletePlist(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteResource(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteScene(string name)
        {
            //throw new NotImplementedException();
        }

        public void onDeleteSound(string name)
        {
            //throw new NotImplementedException();
        }

        public void onPropertyChanged(object obj, string property, object oldValue, object newValue)
        {
            SolutionExplorer.onPropertyChanged(obj, property, oldValue, newValue);
            SceneExplorer.onPropertyChanged(obj, property, oldValue, newValue);
            ActionExplorer.onPropertyChanged(obj, property, oldValue, newValue);
            if (obj is Node)
            {
                RedoManager.PushRedo(new RedoItem((Node)obj, property, oldValue, newValue));
                Document.Refresh();
            }
            if(obj is Request || obj is Param)
            {
                ApiExplorer.onPropertyChanged(obj, property, oldValue, newValue);
            }
            //throw new NotImplementedException();
        }

        public void onSelectItem(object obj)
        {
            if(Document.Editer!= null)
            {
                Document.Editer.OnSelectObject(obj);
                Document.Refresh();
            }
        }
    }
}
