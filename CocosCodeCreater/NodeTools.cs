using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class NodeTools
    {
        public static Rectangle getAllNodeRect(Global global,List<kernel.models.Node> ActiveNodes)
        {
            int left = 0;
            int right = 0;
            int top = 0;
            int bottom = 0;
            for (int i = 0; i < ActiveNodes.Count; i++)
            {
                kernel.models.Node node = ActiveNodes[i];
                Rectangle rect = ActiveNodes[i].getNodeDisplayRect();// new RectangleF(ActiveNode.Location.X, ActiveNode.Location.Y, ActiveNode.Size.Width, ActiveNode.Size.Height);
                if (ActiveNodes[i].Type == "ComponentRef")
                {
                    rect = ActiveNodes[i].getCompentShowRect(global.Project);
                }
                if (i == 0)
                {
                    left = rect.Left;
                    top = rect.Top;
                    right = rect.Right;
                    bottom = rect.Bottom;
                }
                else
                {
                    if (left > rect.Left) left = rect.Left;
                    if (top > rect.Top) top = rect.Top;
                    if (right < rect.Right) right = rect.Right;
                    if (bottom < rect.Bottom) bottom = rect.Bottom;
                }
            }
            return new Rectangle(left, top, right - left, bottom - top);
        }
    }
}
