using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            passwordRTB.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  Form1 naviagte = new Form1();
            string username = "nabin";
            string password = "nabin";
           
            if (username == usernameRTB.Text && password == passwordRTB.Text)
            {
                Form2 d = new Form2();
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                DialogResult result = MessageBox.Show("Login Successful", "Message", buttons);
               if(result == DialogResult.OK)
                {
                    d.ShowDialog();
                }
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;

                DialogResult result = MessageBox.Show("Wrong username & password", "Message", buttons);

            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            VisitorsDashboard dashboard = new VisitorsDashboard();
            dashboard.ShowDialog();
        }
    }
}
