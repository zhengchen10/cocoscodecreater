using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kernel.property
{
    public class FolderNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            FolderBrowser browser = new FolderBrowser();
            if (value != null)
            {
                browser.DirectoryPath = string.Format("{0}", value);
            }

            if (browser.ShowDialog(null) == DialogResult.OK)
                return browser.DirectoryPath;

            return value;
        }
    }


}
