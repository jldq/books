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
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        void showALL()
        {
            DB.GrtCn();
            string str = "select * from kehubiao";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int index = dataGridView1.Rows.Add();
                for (int i = 0; i <= 3; i++)
                {
                    dataGridView1.Rows[index].Cells[i].Value = rdr[i];
                }
            }
            rdr.Close();
            //DB.cn.Close();
        }

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
                showALL();
                label6.Text = DL.name;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells["Column5"] as DataGridViewCheckBoxCell;
                    if (i != e.RowIndex)
                    {
                        ck.Value = false;
                    }
                    else
                    {
                        ck.Value = true;
                    }
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string _selectValue = dataGridView1.Rows[i].Cells["Column5"].EditedFormattedValue.ToString();
                    if (_selectValue == "True")
                    {
                        textBox1.Text = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[i].Cells["Column2"].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                        textBox4.Text = dataGridView1.Rows[i].Cells["Column4"].Value.ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("客户名称不能为空!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要修改吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    DB.GrtCn();
                    string str = "update kehubiao set k_xingming='" + textBox2.Text + "',k_shoujihao='" + textBox3.Text
                     + "',k_Email='" + textBox4.Text + "'where k_id='" + textBox1.Text + "'";
                    DB.sqlEx(str);
                    MessageBox.Show("修改成功!");
                    dataGridView1.Rows.Clear();
                    showALL();

                    //使用存储过程添加日志
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.NVarChar));

                    cmd.Parameters["username"].Value = DL.name;
                    cmd.Parameters["type"].Value = "修改";
                    cmd.Parameters["action_date"].Value = DateTime.Now;
                    cmd.Parameters["action_table"].Value = "客户表";

                    try
                    {
                        cmd.ExecuteNonQuery();
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string _selectValue = dataGridView1.Rows[i].Cells["Column5"].EditedFormattedValue.ToString();
                if (_selectValue == "True")
                {
                    textBox1.Text = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[i].Cells["Column2"].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[i].Cells["Column4"].Value.ToString();
                }
            }
        }
    }
}
