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
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        SqlDataAdapter dalx;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from leixingbiao";
            dalx = new SqlDataAdapter(str, DB.cn);
            dalx.Fill(ds, "leixingbiao_info");
        }

        void showALL()
        {
            DataView dalx = new DataView(ds.Tables["leixingbiao_info"]);
            dataGridView1.DataSource = dalx;
            dataGridView1.Columns[2].Width = 500;
        }

        void dgvHead()
        {
            dataGridView1.Columns[0].HeaderText = "类型名称";
            dataGridView1.Columns[1].HeaderText = "借阅期限";
            dataGridView1.Columns[2].HeaderText = "类型描述";
        }

        private void Change_Load(object sender, EventArgs e)
        {
            if (DL.Alog==false)
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
                dgvHead();
                label5.Text = DL.name;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["l_leixing"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["l_qixian"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["l_miaoshu"].Value.ToString();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text=="")
            {
                MessageBox.Show("借阅期限不能为空!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要修改吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr==DialogResult.OK)
                {
                    int a = dataGridView1.CurrentRow.Index;
                    string str = dataGridView1.Rows[a].Cells["l_leixing"].Value.ToString();
                    DataRow[] deRows = ds.Tables["leixingbiao_info"].Select("l_leixing='" + str + "'");
                    deRows[0]["l_qixian"] = textBox2.Text;
                    deRows[0]["l_miaoshu"] = textBox3.Text;
                    //MessageBox.Show("修改成功");
                    //updateDB();

                    //使用存储过程添加日志
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

                    cmd.Parameters["username"].Value = DL.name;
                    cmd.Parameters["type"].Value = "修改";
                    cmd.Parameters["action_date"].Value = DateTime.Now;
                    cmd.Parameters["action_table"].Value = "类型表";

                    try
                    {
                        SqlCommandBuilder dblx = new SqlCommandBuilder(dalx);
                        dalx.Update(ds, "leixingbiao_info");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("修改成功!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("ex.Message");
                    }
                    DB.cn.Close();
                }
                else
                {
                    return;
                }
            }
        }
        //void updateDB()
        //{
        //    try { 
        //        SqlCommandBuilder dblx = new SqlCommandBuilder(dalx);
        //        dalx.Update(ds, "leixingbiao_info");
        //    }
        //    catch(SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["l_leixing"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["l_qixian"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["l_miaoshu"].Value.ToString();
        }
    }
}
