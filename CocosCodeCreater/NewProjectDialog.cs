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
    public partial class NewProjectDialog : Form
    {
     
        public string ProjectName
        {
            get
            {
                return projectName.Text;
            }
        }

        public string ProjectPath
        {
            get
            {
                return projectPath.Text;
            }
        }
        public NewProjectDialog()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if(projectName.Text.Length == 0)
            {
                return;
            }
            if (projectPath.Text.Length == 0)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void select_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                projectPath.Text = dialog.SelectedPath;
            }
        }

        private void projectPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
