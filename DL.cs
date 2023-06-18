using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace books
{
    public partial class DL : Form
    {
        public DL()
        {
            InitializeComponent();
        }
        public static string name;
        public static bool log = false;
        public static bool Alog = false;

        private void button1_Click(object sender, EventArgs e)
        {
            Userservice us = new Userservice();
            if (radioButton1.Checked)
            {
                if (us.checkUsernameAndPwd(textBox1.Text, textBox2.Text, 1) == true)
                {
                    MessageBox.Show("登录成功");
                    name = textBox1.Text;
                    log = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }
            else
            {
                if (us.checkUsernameAndPwd(textBox1.Text, textBox2.Text, 2) == true)
                {
                    MessageBox.Show("登录成功");
                    name = textBox1.Text;
                    log = true;
                    Alog = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
