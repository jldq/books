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
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }
        SqlDataAdapter dats;
        SqlDataAdapter dalx;
        DataSet ds = new DataSet();

        void init()
        {

            DB.GrtCn();
            string str = "select * from tushubiao";
            string sdr = "select * from leixingbiao";
            dalx = new SqlDataAdapter(sdr, DB.cn);
            dats = new SqlDataAdapter(str, DB.cn);
            dats.Fill(ds, "tushubiao_info");
            dalx.Fill(ds, "leixingbiao_info");
        }
        void showALLtushu()
        {
            DataView detushu = new DataView(ds.Tables["tushubiao_info"]);
            dataGridView1.DataSource = detushu;
        }
        void dgvHeader()
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
        void showleixing()
        {
            DataView daleixingName = new DataView(ds.Tables["leixingbiao_info"]);
            comboBox1.DisplayMember = "l_leixing";
            comboBox1.ValueMember = "l_leixing";
            comboBox1.DataSource = daleixingName;
        }
        private void ADD_Load(object sender, EventArgs e)
        {
            if (DL.Alog == false)
            {
                this.Close();
                MessageBox.Show("请以管理员身份登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                //return;
                init();
                showleixing();
                showALLtushu();
                dgvHeader();
                label10.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("ISBN或书名不能为空");
            }
            else
            {
                DB.GrtCn();
                string str = "select * from tushubiao where ISBN='" + textBox1.Text + "'";
                DataTable dt1 = DB.GetDataSet(str);
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("该ISBN已经存在,请重新输入");
                }
           
                else
                {
                    //方法3
                    DataRow tsRow = ds.Tables["tushubiao_info"].NewRow();
                    tsRow["ISBN"] = textBox1.Text;
                    tsRow["s_shuming"] = textBox2.Text;
                    tsRow["l_leixing"] =comboBox1.SelectedValue;
                    tsRow["s_zuozhe"] = textBox4.Text;
                    tsRow["s_chubanshe"] = textBox5.Text;
                    tsRow["s_cbriqi"] = dateTimePicker1.Value;
                    tsRow["s_danjia"] = textBox6.Text;
                    tsRow["s_zhangtai"] = comboBox2.Text;
                    ds.Tables["tushubiao_info"].Rows.Add(tsRow);

                    //使用存储过程添加日志
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

                    cmd.Parameters["username"].Value = DL.name;
                    cmd.Parameters["type"].Value = "添加";
                    cmd.Parameters["action_date"].Value = DateTime.Now;
                    cmd.Parameters["action_table"].Value = "图书表";
                    try
                    {
                        SqlCommandBuilder dbts = new SqlCommandBuilder(dats);
                        dats.Update(ds, "tushubiao_info");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("添加成功!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("ex.Message");
                    }
                    DB.cn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showALLtushu();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvName = new DataView(ds.Tables["tushubiao_info"]);
            dvName.RowFilter = "l_leixing='" + comboBox1.SelectedValue.ToString() + "'";
            dataGridView1.DataSource = dvName;
        }
    }
}
