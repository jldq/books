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

namespace books.employee
{
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter dem;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from yuangongbiao";
            dem = new SqlDataAdapter(str, DB.cn);
            dem.Fill(ds, "yuangongbiao_info");
        }
        void showALL()
        {
            DataView dem = new DataView(ds.Tables["yuangongbiao_info"]);
            dataGridView1.DataSource = dem;
        }
        void dgvHeader()
        {
            dataGridView1.Columns[0].HeaderText = "员工编号";
            dataGridView1.Columns[1].HeaderText = "员工姓名";
            dataGridView1.Columns[2].HeaderText = "性别";
            dataGridView1.Columns[3].HeaderText = "入职日期";
            dataGridView1.Columns[4].HeaderText = "电话";
            dataGridView1.Columns[5].HeaderText = "生日";
            dataGridView1.Columns[6].HeaderText = "地址";
            dataGridView1.Columns[7].HeaderText = "工资";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入查询关键字");
            }
            else
            {
                DataView dvEn = new DataView(ds.Tables["yuangongbiao_info"]);
                dvEn.RowFilter = "y_xingming LIKE '%" + textBox1.Text + "%'";
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

        private void CHX_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvEm1 = new DataView(ds.Tables["yuangongbiao_info"]);
                    dvEm1.RowFilter = "y_shengri>='" + dateTimePicker1.Value + "'and y_shengri<='" + dateTimePicker2.Value + "'";
                    dataGridView1.DataSource = dvEm1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value >= dateTimePicker4.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvEm2 = new DataView(ds.Tables["yuangongbiao_info"]);
                    dvEm2.RowFilter = "y_rzriqi>='" + dateTimePicker3.Value + "'and y_rzriqi<='" + dateTimePicker4.Value + "'";
                    dataGridView1.DataSource = dvEm2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvGZ = new DataView(ds.Tables["yuangongbiao_info"]);
            int i = comboBox1.SelectedIndex;
            switch (i)
            {
                case 0:
                    dvGZ.RowFilter = "y_gongzi<=4000";
                    break;
                case 1:
                    dvGZ.RowFilter = "y_gongzi>4000  and  y_gongzi<=4300";
                    break;
                case 2:
                    dvGZ.RowFilter = "y_gongzi>4300  and  y_gongzi<=4600";
                    break;
                case 3:
                    dvGZ.RowFilter = "y_gongzi>4600  and  y_gongzi<=5000";
                    break;
                default:
                    dvGZ.RowFilter = "y_gongzi>=5000";
                    break;
            }
            dataGridView1.DataSource = dvGZ;
            DB.cn.Close();
        }
    }
}
