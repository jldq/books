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
    public partial class chx2 : Form
    {
        public chx2()
        {
            InitializeComponent();
        }
        public static string ts_isbn;
        void showALL()
        {
            DB.GrtCn();
            string str = "select * from tushubiao";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[0].Value = rdr[0];
                dataGridView1.Rows[index].Cells[1].Value = rdr[1];
                dataGridView1.Rows[index].Cells[2].Value = rdr[2];
                dataGridView1.Rows[index].Cells[3].Value = rdr[3];
            }
            rdr.Close();
        }

        private void chx2_Load(object sender, EventArgs e)
        {
            if (DL.log == false)
            {
                this.Close();
                MessageBox.Show("请先登录!");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                showALL();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells["Column5"] as DataGridViewCheckBoxCell;
                    if (i!=e.RowIndex)
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
            if (dataGridView1.Rows.Count>0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string _select = dataGridView1.Rows[i].Cells["Column5"].EditedFormattedValue.ToString();
                    if (_select=="True")
                    {
                        ts_isbn = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                    }
                }
            }
            tushu.tsxq ch2 = new tsxq();
            ch2.ShowDialog();
        }
    }
}
