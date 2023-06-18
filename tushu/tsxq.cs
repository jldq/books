using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace books.tushu
{
    public partial class tsxq : Form
    {
        public tsxq()
        {
            InitializeComponent();
        }

        private void tsxq_Load(object sender, EventArgs e)
        {
            DB.GrtCn();
            string str="select * from tushubiao where ISBN ='"+tushu.chx2.ts_isbn + "'";
            //DataTable dt = DB.GetDataSet(str);
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                MessageBox.Show("没有相关查询结果");
            }
            else
            {
                while (rdr.Read())
                {
                    label9.Text = rdr[0].ToString();
                    label10.Text = rdr[1].ToString();
                    label11.Text = rdr[2].ToString();
                    label12.Text = rdr[3].ToString();
                    label13.Text = rdr[4].ToString();
                    label14.Text = rdr[5].ToString();
                    label15.Text = rdr[6].ToString();
                    label16.Text = rdr[7].ToString();
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "" + rdr[8].ToString() + ""); //加载图片
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; 
                }
                rdr.Close();
            }
            DB.cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
