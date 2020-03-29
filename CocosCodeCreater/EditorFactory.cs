using kernel.models;
using kernel.plist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class EditorFactory
    {
        public static IEditer getEditer(Object obj,Global global)
        {
            if(obj is Scene)
                return new SceneEditor((Scene)obj,global);
            if (obj is PListInfo)
                return new PlistEditor((PListInfo)obj, global);
            if (obj is Component)
                return new ComponentEditor((Component)obj, global);
            return null;
        }

    }
}
