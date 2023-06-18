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
    public partial class ZC : Form
    {
        public ZC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入用户名和密码!");
            }
            else
            {
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("两次密码不一致,请重新输入!");
                }
                else
                {
                    string str = "select * from buser where username='" + textBox1.Text + "'";
                    DB.GrtCn();
                    DataTable dt = DB.GetDataSet(str);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("此用户已存在!");
                    }
                    else
                    {
                        string md5 = GetMD5Hash.MD5(textBox2.Text);
                        string sdr = "insert into buser values('" + textBox1.Text + "','" + md5 + "',1)";
                        DB.sqlEx(sdr);
                        MessageBox.Show("注册成功!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
