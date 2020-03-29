using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public interface IEditer
    {
        void OnPaint(Graphics g, Rectangle rect);
        bool OnMouseClick(MouseEventArgs e);
        bool OnMouseDown(MouseEventArgs e);
        bool OnMouseMove(MouseEventArgs e);
        bool OnMouseUp(MouseEventArgs e);
        bool OnMouseWheel(MouseEventArgs e);
        bool OnKeyDown(KeyEventArgs e);
        bool OnKeyUp(KeyEventArgs e);
        void OnSelectObject(Object obj);
    }
}
