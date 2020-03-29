using kernel.actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel.models
{
    public class NodeContainer :  Node
    {
        public NodeContainer()
        {
            Sprites = new List<Sprite>();
            Layers = new List<Layer>();
            Labels = new List<Label>();
            Buttons = new List<Button>();
            Components = new List<ComponentRef>();
            Actions = new List<ActionOwner>();
        }
        public ActionOwner FindActionByName(String name)
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                if (Actions[i].Name == name)
                    return Actions[i];
            }
            return null;
        }
        public void RemoveActionByName(string name)
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                if (Actions[i].Name == name) {
                    Actions.RemoveAt(i);
                    return;
                }
            }
        }

        public Node FindNodeByName(String name)
        {
            for(int i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i].Name == name)
                    return Sprites[i];
            }
            for (int i = 0; i < Labels.Count; i++)
            {
                if (Labels[i].Name == name)
                    return Labels[i];
            }
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].Name == name)
                    return Buttons[i];
            }
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Name == name)
                    return Layers[i];
            }
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Name == name)
                    return Components[i];
            }
            return null;
        }

        public Node FindNodeByUUID(String uuid)
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i].UUID == uuid)
                    return Sprites[i];
            }
            for (int i = 0; i < Labels.Count; i++)
            {
                if (Labels[i].UUID == uuid)
                    return Labels[i];
            }
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].UUID == uuid)
                    return Buttons[i];
            }
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].UUID == uuid)
                    return Layers[i];
            }
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].UUID == uuid)
                    return Components[i];
            }
            return null;
        }

        public void AddChildren(Node node)
        {
            if(node.Type == "Layer")
            {
                Layers.Add((Layer)node);
                Children.Add(node.UUID);
            }
            if (node.Type == "Sprite")
            {
                Sprites.Add((Sprite)node);
                Children.Add(node.UUID);
            }
            if (node.Type == "Label")
            {
                Labels.Add((Label)node);
                Children.Add(node.UUID);
            }
            if (node.Type == "Button")
            {
                Buttons.Add((Button)node);
                Children.Add(node.UUID);
            }
            if(node.Type == "ComponentRef")
            {
                Components.Add((ComponentRef)node);
                Children.Add(node.UUID);
            }
        }
        

        public bool RemoveNode(Node node)
        {
            bool delete = false;
            if (!delete)
            {
                for (int i = 0; i < Sprites.Count; i++)
                {
                    if (Sprites[i].UUID == node.UUID)
                    {
                        delete = true;
                        Sprites.RemoveAt(i);
                        break;
                    }
                }
            }
            if (!delete)
            {
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if (Buttons[i].UUID == node.UUID)
                    {
                        delete = true;
                        Buttons.RemoveAt(i);
                        break;
                    }
                }
            }
            if (!delete)
            {
                for (int i = 0; i < Labels.Count; i++)
                {
                    if (Labels[i].UUID == node.UUID)
                    {
                        delete = true;
                        Labels.RemoveAt(i);
                        break;
                    }
                }
            }
            if (!delete)
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    if (Components[i].UUID == node.UUID)
                    {
                        delete = true;
                        Components.RemoveAt(i);
                        break;
                    }
                }
            }
            if (!delete)
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    delete = Layers[i].RemoveNode(node);
                    if (delete)
                    {
                        break;
                    }
                    if(Layers[i].UUID == node.UUID)
                    {
                        delete = true;
                        Layers.RemoveAt(i);
                        break;
                    }
                }
            }
            

            for (int i = 1; i < Children.Count; i++)
            {
                if (Children[i] == node.UUID)
                {
                    Children.RemoveAt(i);
                    break;
                }
            }
            return delete;
        }

        [Browsable(false)]
        public List<Sprite> Sprites { get; }

        [Browsable(false)]
        public List<Layer> Layers { get; }

        [Browsable(false)]
        public List<Label> Labels { get; }

        [Browsable(false)]
        public List<Button> Buttons { get; }

        [Browsable(false)]
        public List<ComponentRef> Components { get; }

        [Browsable(false)]
        public List<ActionOwner> Actions { get; }


        public bool MoveUp(string uuid)
        {
            for(int i = 1; i < Children.Count; i++)
            {
                if(Children[i] == uuid)
                {
                    Children.RemoveAt(i);
                    Children.Insert(i - 1, uuid);
                    return true;
                }
            }
            for(int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].MoveUp(uuid))
                    return true;
            }
            return false;
        }

        public bool MoveDown(string uuid)
        {
            for (int i = 0; i < Children.Count-1; i++)
            {
                if (Children[i] == uuid)
                {
                    Children.RemoveAt(i);
                    Children.Insert(i + 1, uuid);
                    return true;
                }
            }
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].MoveDown(uuid))
                    return true;
            }
            return false;
        }

        public bool RemoveActionItemByUUID(string UUID)
        {
            for(int i = 0; i < Actions.Count; i++)
            {
                ActionOwner owner = Actions[i];
                if(owner.Action != null)
                {
                    if(owner.Action.UUID == UUID)
                    {
                        owner.Action = null;
                        return true;
                    }
                    if (RemoveSubActionByUUID(owner.Action, UUID))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool RemoveSubActionByUUID(ActionItem action, string uUID)
        {
            if (action.SubActions.Count == 0)
                return false;
            for(int i = 0; i < action.SubActions.Count; i++)
            {
                ActionItem item = action.SubActions[i];
                if(item.UUID == uUID)
                {
                    action.SubActions.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
