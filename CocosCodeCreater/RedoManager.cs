using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class RedoManager
    {
        private static List<RedoItem> redoItems = new List<RedoItem>();
        private static List<RedoItem> undoItems = new List<RedoItem>();
        public static void Redo() // Ctrl + Y
        {
            if (redoItems.Count > 0)
            {
                RedoItem item = redoItems[redoItems.Count - 1];
                redoItems.RemoveAt(redoItems.Count - 1);
                undoItems.Add(item);
                redo(item);
            }
        }

        

        public static void Undo() // Ctrl + Z
        {
            if(undoItems.Count > 0)
            {
                RedoItem item = undoItems[undoItems.Count - 1];
                undoItems.RemoveAt(undoItems.Count - 1);
                redoItems.Add(item);
                undo(item);
            }
        }

        private static void undo(RedoItem item)
        {
            if(item.Property == "Location")
            {
                item.Node.Location = (Point)item.OldValue;
            }   
            
        }

        private static void redo(RedoItem item)
        {
            if (item.Property == "Location")
            {
                item.Node.Location = (Point)item.NewValue;
            }
        }

        public static void PushRedo(RedoItem item)
        {
            if(undoItems.Count == 0)
            {
                undoItems.Add(item);
            } else
            {
                RedoItem last = undoItems[undoItems.Count - 1];
                if(last.Node.UUID == item.Node.UUID && last.Property == item.Property)
                {
                    undoItems[undoItems.Count - 1].NewValue = item.NewValue;
                } else
                {
                    undoItems.Add(item);
                }
            }
            
            redoItems.Clear();
        }
    }
}
