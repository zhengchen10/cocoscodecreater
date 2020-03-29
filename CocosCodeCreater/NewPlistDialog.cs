using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocosCodeCreater
{
    public partial class NewPlistDialog : Form
    {
        public NewPlistDialog()
        {
            InitializeComponent();
        }

        public String PlistName
        {
            get {
                if (plistName.Text.EndsWith(".plist"))
                    return plistName.Text;
                else
                    return plistName.Text + ".plist";
            }
        }

        public int PlistWidth
        {
            get
            {
                return int.Parse(plistWidth.Text);
            }
        }

        public int PlistHeight
        {
            get
            {
                return int.Parse(plistHeight.Text);
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (plistName.Text.Length == 0)
            {
                return;
            }
            if (plistWidth.Text.Length == 0)
            {
                return;
            }
            if (plistHeight.Text.Length == 0)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
