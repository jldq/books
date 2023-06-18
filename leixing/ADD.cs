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

namespace books.leixing
{
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }

        SqlDataAdapter dalx;
        DataSet ds = new DataSet();
        
        void init()
        {
            DB.GrtCn();
            string s_dep = "select * from leixingbiao";
            dalx = new SqlDataAdapter(s_dep, DB.cn);
            dalx.Fill(ds, "leixingbiao_info");
        }
        private void ADD_load(object sender, EventArgs e)
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
                showALLleixing();
                dgvHead();
                label5.Text = DL.name;
            }
        }
        void showALLleixing()
        {
            DataView dvdep = new DataView(ds.Tables["leixingbiao_info"]);
            dataGridView1.DataSource = dvdep;
            dataGridView1.Columns[2].Width = 500;
        }
        void dgvHead()
        {
            dataGridView1.Columns[0].HeaderText = "类型名称";
            dataGridView1.Columns[1].HeaderText = "借阅期限";
            dataGridView1.Columns[2].HeaderText = "类型描述";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("类型名称或借阅期限不能为空");
            }
            else
            {
                DB.GrtCn();
                string str = "select * from leixingbiao where l_leixing='" + textBox1.Text + "'";
                string sdr = "select * from leixingbiao where l_qixian='" + textBox2.Text + "'";
                DataTable dt1 = DB.GetDataSet(str);
                DataTable dt2 = DB.GetDataSet(sdr);
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("该类型名称已经存在,请重新输入");
                }
                else if (dt2.Rows.Count > 0)
                {
                    MessageBox.Show("该类型名称已经存在,请重新输入");
                }
                else
                {
                    //方法3
                    DataRow depRow = ds.Tables["leixingbiao_info"].NewRow();
                    depRow["l_leixing"] = textBox1.Text;
                    depRow["l_qixian"] = textBox2.Text;
                    depRow["l_miaoshu"] = textBox3.Text;
                    ds.Tables["leixingbiao_info"].Rows.Add(depRow);

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
                    cmd.Parameters["action_table"].Value = "类型表";
                    try
                    {
                        SqlCommandBuilder dblx = new SqlCommandBuilder(dalx);
                        dalx.Update(ds, "leixingbiao_info");
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
            showALLleixing();
        }
    }
}
