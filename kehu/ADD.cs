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
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }
        SqlDataAdapter dakh;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GrtCn();
            string str = "select * from kehubiao";
            dakh = new SqlDataAdapter(str, DB.cn);
            dakh.Fill(ds, "kh_info");
        }
        void showALL()
        {
            DataView dvkh = new DataView(ds.Tables["kh_info"]);
            dataGridView1.DataSource = dvkh;
        }
        void dgvHead()
        {
            dataGridView1.Columns[0].HeaderText = "客户ID";
            dataGridView1.Columns[1].HeaderText = "客户姓名";
            dataGridView1.Columns[2].HeaderText = "手机号";
            dataGridView1.Columns[3].HeaderText = "Email";
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
                showALL();
                dgvHead();
                label6.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("客户编号或客户姓名不能为空");
            }
            else
            {
                DB.GrtCn();
                string str = "select * from kehubiao where k_id='" + textBox1.Text + "'";
                string sdr = "select * from kehubiao where k_xingming='" + textBox2.Text + "'";
                DataTable dt1 = DB.GetDataSet(str);
                DataTable dt2 = DB.GetDataSet(sdr);
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("该客户编号已经存在,请重新输入");
                }
                else if (dt2.Rows.Count > 0)
                {
                    MessageBox.Show("该客户姓名已经存在,请重新输入");
                }
                else
                {
                    //方法3
                    DataRow khRow = ds.Tables["kh_info"].NewRow();
                    khRow["k_id"] = textBox1.Text;
                    khRow["k_xingming"] = textBox2.Text;
                    khRow["k_shoujihao"] = textBox3.Text;
                    khRow["k_Email"] = textBox4.Text;
                    ds.Tables["kh_info"].Rows.Add(khRow);

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
                    cmd.Parameters["action_table"].Value = "客户表";
                    try
                    {
                        SqlCommandBuilder dbkh = new SqlCommandBuilder(dakh);
                        dakh.Update(ds, "kh_info");
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showALL();
        }
    }
}
