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

namespace books.jiehuan
{
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        SqlDataAdapter dajh;
        DataSet ds = new DataSet();
        void init()
        {

            DB.GrtCn();
            string str = "select * from jiehuanbiao";
            //string sdr = "select * from yuangongbiao";
            //daemp = new SqlDataAdapter(sdr, DB.cn);
            dajh = new SqlDataAdapter(str, DB.cn);
            dajh.Fill(ds, "jh_info");
            //daemp.Fill(ds, "emp_info");
        }
        void showALL()
        {
            DataView dejh = new DataView(ds.Tables["jh_info"]);
            dataGridView1.DataSource = dejh;
        }
        void Header()
        {
            dataGridView1.Columns[0].HeaderText = "借还编号";
            dataGridView1.Columns[1].HeaderText = "ISBN";
            dataGridView1.Columns[2].HeaderText = "客户姓名";
            dataGridView1.Columns[3].HeaderText = "借员工姓名";
            dataGridView1.Columns[4].HeaderText = "借出日期";
            dataGridView1.Columns[5].HeaderText = "还代理人";
            dataGridView1.Columns[6].HeaderText = "归还日期";
            dataGridView1.Columns[7].HeaderText = "备注";
        }
        //void showEmp()
        //{
        //    DataView em = new DataView(ds.Tables["emp_info"]);
        //    comboBox3.DisplayMember = "y_xingming";
        //    comboBox3.ValueMember = "y_xingming";
        //    comboBox3.DataSource = em;
        //}

        //private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataView jhName = new DataView(ds.Tables["jh_info"]);
        //    jhName.RowFilter = "j_huandlr='" + comboBox3.SelectedValue.ToString() + "'";
        //    dataGridView1.DataSource = jhName;
        //}

        private void Change_Load(object sender, EventArgs e)
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
                init();
                //showEmp();
                showALL();
                Header();
                label10.Text = DL.name;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            textBox2.Text = dataGridView1.Rows[n].Cells["j_id"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showALL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //方法3
            int a = dataGridView1.CurrentRow.Index;
            string str = dataGridView1.Rows[a].Cells["j_id"].Value.ToString();
            DataRow[] drjh = ds.Tables["jh_info"].Select("j_id='" + str + "'");
            drjh[0]["j_beizhu"] = textBox1.Text;
            drjh[0]["j_id"] = textBox2.Text;
           
            //ds.Tables["jh_info"].Rows.Add(drjh);

            //使用存储过程添加日志
            DB.GrtCn();
            SqlCommand cmd = new SqlCommand("add_log", DB.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
            cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

            cmd.Parameters["username"].Value = DL.name;
            cmd.Parameters["type"].Value = "修改";
            cmd.Parameters["action_date"].Value = DateTime.Now;
            cmd.Parameters["action_table"].Value = "借还表";
            try
            {
                SqlCommandBuilder dbjh = new SqlCommandBuilder(dajh);
                dajh.Update(ds, "jh_info");
                cmd.ExecuteNonQuery();
                MessageBox.Show("修改成功!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("ex.Message");
            }
            DB.cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入查询关键字");
            }
            else
            {
                DataView dvkh = new DataView(ds.Tables["jh_info"]);
                dvkh.RowFilter = "kehu LIKE '%" + textBox3.Text + "%'";
                if (dvkh.Count == 0)
                {
                    MessageBox.Show("没有查询到与之相配的结果");
                }
                else
                {
                    dataGridView1.DataSource = dvkh;
                }
            }
        }
    }
}

