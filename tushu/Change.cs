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
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        SqlDataAdapter dalx, dats, dalog;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from tushubiao";
            string sdr = "select * from leixingbiao";
            string sor = "select * from Log";
            dats = new SqlDataAdapter(str, DB.GrtCn());
            dalx = new SqlDataAdapter(sdr, DB.GrtCn());
            dalog = new SqlDataAdapter(sor, DB.GrtCn());
            dats.Fill(ds, "ts_info");
            dalx.Fill(ds, "lx_info");
            dalog.Fill(ds, "log_info");
        }
        void showALL()
        {
            DataView dvts = new DataView(ds.Tables["ts_info"]);
            dataGridView1.DataSource = dvts;
        }

        private void Change_Load(object sender, EventArgs e)
        {
            if (DL.Alog == false)
            {
                this.Close();
                MessageBox.Show("请以管理员的身份登录!");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                init();
                showALL();
                head();
                showlx();
                label10.Text = DL.name;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            DataRow[] drlx = ds.Tables["lx_info"].Select("l_leixing='" + dataGridView1.Rows[n].Cells["l_leixing"].Value.ToString() + "'");
            textBox1.Text = dataGridView1.Rows[n].Cells["ISBN"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells["s_shuming"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells["l_leixing"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[n].Cells["s_zuozhe"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[n].Cells["s_chubanshe"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["s_cbriqi"].Value.ToString());
            textBox5.Text = dataGridView1.Rows[n].Cells["s_danjia"].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[n].Cells["s_zhangtai"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("书名不能为空!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要修改吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    int a = dataGridView1.CurrentRow.Index;
                    DataRow[] drlx = ds.Tables["lx_info"].Select("l_leixing='" + comboBox1.Text + "'");
                    string str = dataGridView1.Rows[a].Cells["ISBN"].Value.ToString();
                    DataRow[] drts = ds.Tables["ts_info"].Select("ISBN='" + str + "'");
                    drts[0]["s_shuming"] = textBox2.Text;
                    drts[0]["s_zuozhe"] = textBox3.Text;
                    drts[0]["s_chubanshe"] = textBox4.Text;
                    drts[0]["s_danjia"] = decimal.Parse(textBox5.Text);
                    drts[0]["l_leixing"] = drlx[0]["l_leixing"].ToString();
                    drts[0]["s_zhangtai"] = comboBox2.Text;
                    drts[0]["s_cbriqi"] = dateTimePicker1.Value;

                    //添加日志
                    DataRow drlog = ds.Tables["log_info"].NewRow();
                    drlog["username"] = DL.name;
                    drlog["type"] = "修改";
                    drlog["action_date"] = DateTime.Now;
                    drlog["action_table"] = "图书表";
                    ds.Tables["log_info"].Rows.Add(drlog);

                    MessageBox.Show("修改成功!");
                    UpdateDB();
                }
            }
        }
        void UpdateDB()
        {
            try
            {
                SqlCommandBuilder dbts = new SqlCommandBuilder(dats);
                SqlCommandBuilder dblog = new SqlCommandBuilder(dalog);
                dats.Update(ds, "ts_info");
                dalog.Update(ds, "log_info");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            DataRow[] drlx = ds.Tables["lx_info"].Select("l_leixing='" + dataGridView1.Rows[n].Cells["l_leixing"].Value.ToString() + "'");
            textBox1.Text = dataGridView1.Rows[n].Cells["ISBN"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells["s_shuming"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells["l_leixing"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[n].Cells["s_zuozhe"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[n].Cells["s_chubanshe"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["s_cbriqi"].Value.ToString());
            textBox5.Text = dataGridView1.Rows[n].Cells["s_danjia"].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[n].Cells["s_zhangtai"].Value.ToString();
        }

        void head()
        {
            dataGridView1.Columns[0].HeaderText = "ISBN";
            dataGridView1.Columns[1].HeaderText = "书名";
            dataGridView1.Columns[2].HeaderText = "类型名称";
            dataGridView1.Columns[3].HeaderText = "作者";
            dataGridView1.Columns[4].HeaderText = "出版社";
            dataGridView1.Columns[5].HeaderText = "出版日期";
            dataGridView1.Columns[6].HeaderText = "单价";
            dataGridView1.Columns[7].HeaderText = "状态";
        }
        void showlx()
        {
            DataView dxName = new DataView(ds.Tables["lx_info"]);
            comboBox1.DisplayMember = "l_leixing";
            comboBox1.ValueMember = "l_leixing";
            comboBox1.DataSource = dxName;
        }
    }
}
