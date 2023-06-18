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
    public partial class check_em : Form
    {
        public check_em()
        {
            InitializeComponent();
        }
        private void check_em_Load(object sender, EventArgs e)
        {
            DB.GrtCn();
            string str = "select * from jiehuanbiao where ISBN in ('" + tushu.check_chx.ts_i + "')";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                MessageBox.Show("没有查到与之相配的结果");
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
            tushu.check_chx.ts_i = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
