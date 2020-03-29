using System;
using System.Collections.Generic;
using System.Text;

namespace kernel
{
    public interface IResourceManager
    {
        String[] GetScenes();
        String[] GetImages();
        String[] GetFonts();
    }
}
