using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public interface ISolutionListener
    {
        void onAddScene(String name);
        void onDeleteScene(String uuid);
        void onAddComponent(String name);
        void onDeleteComponent(String uuid);
        void onAddImage(String name,String path);
        void onDeleteImage(String uuid);

        void onAddFont(String name, String path);
        void onDeleteFont(String uuid);
        void onAddSound(String name, String path);
        void onDeleteSound(String uuid);
        void onAddPlist(String name, String path);
        void onDeletePlist(String uuid);
    }
}
