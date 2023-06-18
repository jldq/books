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

namespace books.kehu
{
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter dekh;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from  kehubiao";
            dekh = new SqlDataAdapter(str, DB.cn);
            dekh.Fill(ds, "kehubiao_info");
        }
        void showALL()
        {
            DataView dekh = new DataView(ds.Tables["kehubiao_info"]);
            dataGridView1.DataSource = dekh;
        }
        void dgvHeader()
        {
            dataGridView1.Columns[0].HeaderText = "客户ID";
            dataGridView1.Columns[1].HeaderText = "客户姓名";
            dataGridView1.Columns[2].HeaderText = "手机号";
            dataGridView1.Columns[3].HeaderText = "Email";
        }
        private void CHX_Load_1(object sender, EventArgs e)
        {

            if (DL.log == false)
            {
                this.Close();
                MessageBox.Show("请先登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                init();
                showALL();
                dgvHeader();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入查询关键字");
            }
            else
            {
                DataView dvEn = new DataView(ds.Tables["kehubiao_info"]);
                dvEn.RowFilter = "k_xingming LIKE '%" + textBox1.Text + "%'";
                if (dvEn.Count == 0)
                {
                    MessageBox.Show("没有查询到与之相配的结果");
                }
                else
                {
                    dataGridView1.DataSource = dvEn;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showALL();
        }
    }
}
