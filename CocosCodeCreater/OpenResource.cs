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
    public partial class OpenResource : Form
    {
        public OpenResource()
        {
            InitializeComponent();
        }

        private void select_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpeg;*.jpg;*.png|" +  
                            "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphics (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                resourcePath.Text = dialog.FileName;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (resourcePath.Text.Length == 0)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
