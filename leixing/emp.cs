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
    public partial class emp : Form
    {
        public emp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emp_load(object sender, EventArgs e)
        {
            DB.GrtCn();
            string str = "select * from tushubiao where l_leixing='" + leixing.dep.dep_id + "'";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                MessageBox.Show("没有查询到与之相配的结果");
            }
            else
            {
                while (rdr.Read())
                {
                    int index = dataGridView1.Rows.Add();
                    for (int i = 0; i <= 7; i++)
                    {
                        dataGridView1.Rows[index].Cells[i].Value = rdr[i];
                    }
                }
                rdr.Close();
            }
            DB.cn.Close();
            leixing.dep.dep_id = "";
        }
    }
}
