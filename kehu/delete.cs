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
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
        }
        void showALL()
        {
            DB.GrtCn();
            string str = "select * from kehubiao";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (rdr.Read())
            {
                int index = dataGridView1.Rows.Add();
                for (int i = 0; i <= 3; i++)
                {
                    dataGridView1.Rows[index].Cells[i].Value = rdr[i];
                }
            }
            rdr.Close();
        }
        private void delete_Load(object sender, EventArgs e)
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
                label2.Text = DL.name;
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            string k_id = dataGridView1.Rows[n].Cells["Column1"].Value.ToString();
            string str = "select * from jiehuanbiao where k_id='" + k_id + "'";
            DataTable dt = DB.GetDataSet(str);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("此信息不能删除,请先删除借还记录表中相关信息");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要删除吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    DB.GrtCn();
                    string sdr = "delete from kehubiao where k_id ='" + k_id + "'";
                    DB.sqlEx(sdr);
                    MessageBox.Show("删除成功!");
                    showALL();

                    //使用存储过程添加日志
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.NVarChar));

                    cmd.Parameters["username"].Value = DL.name;
                    cmd.Parameters["type"].Value = "删除";
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
