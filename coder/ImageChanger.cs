using System;
using System.Collections.Generic;
using System.Text;

namespace coder
{
    public interface ImageChanger
    {
        void changeImage(String src, String dest, int width, int height,float scale);
    }
}
