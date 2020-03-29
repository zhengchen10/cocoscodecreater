using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace CocosCodeCreater
{
    public partial class PropertyExplorer : ToolWindow
    {
        public PropertyGrid PropertyGrid { get { return this.propertyGrid1; } }
        public IPropertyListener Listener { get; set; }
        public PropertyExplorer()
        {
            InitializeComponent();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if(Listener != null)
            {
                Listener.onPropertyChanged(PropertyGrid.SelectedObject, e.ChangedItem.PropertyDescriptor.Name, e.OldValue, e.ChangedItem.Value);
            }
        }
    }
}
