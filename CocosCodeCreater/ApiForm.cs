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
    public partial class ApiForm : Form
    {
        public ApiForm()
        {
            InitializeComponent();
            ApiUrl = "/api";
        }

        public String ApiName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public String ApiUrl
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ApiName == "")
            {
                MessageBox.Show("请输入名称");
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            
        }
    }
}
