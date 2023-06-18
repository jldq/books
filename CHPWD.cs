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
    public partial class CHPWD : Form
    {
        public CHPWD()
        {
            InitializeComponent();
        }

        private void CHADD_load(object sender, EventArgs e)
        {
            if (DL.log == false)
            {
                MessageBox.Show("请先登录!");
                this.Close();
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                textBox1.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "select * from buser where username='" + textBox1.Text + "'and pwd='" + textBox2.Text + "'";
            string sdr = "update buser set pwd='" + textBox3.Text + "'where username='" + textBox1.Text + "'";
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入原密码");
            }
            else
            {
                DB.GrtCn();
                DataTable dt = DB.GetDataSet(str);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("原密码错误,请重新输入");
                }
                else
                {
                    if (textBox3.Text == "" || textBox4.Text == "")
                    {
                        MessageBox.Show("请输入新密码");
                    }
                    else
                    {
                        if (textBox3.Text != textBox4.Text)
                        {
                            MessageBox.Show("两次密码不一致,请重新输入");
                        }
                        else
                        {
                            DB.sqlEx(sdr);
                            MessageBox.Show("修改成功!");
                        }
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
