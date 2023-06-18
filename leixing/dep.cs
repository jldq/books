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
    public partial class dep : Form
    {
        public dep()
        {
            InitializeComponent();
        }
        public static string dep_id;
        void showALL()
        {
            DB.GrtCn();
            string str = "select * from leixingbiao";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            dataGV.Rows.Clear();
            while (rdr.Read())
            {
                int index = dataGV.Rows.Add();
                for (int i = 0; i <= 2; i++)
                {
                    dataGV.Rows[index].Cells[i].Value = rdr[i];
                }
            }
            rdr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGV.Rows.Count > 0)
            {
                for (int i = 0; i < dataGV.Rows.Count; i++)
                {
                    string _selectedValue = dataGV.Rows[i].Cells["Column4"].EditedFormattedValue.ToString();
                    if (_selectedValue == "True")
                    {
                        dep_id = dataGV.Rows[i].Cells["Column1"].Value.ToString();
                    }
                }
            }
            leixing.emp e1 = new emp();
            e1.ShowDialog();
        }

        private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGV.Rows.Count > 0)
            {
                for (int i = 0; i < dataGV.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGV.Rows[i].Cells["Column4"] as DataGridViewCheckBoxCell;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dep_Load(object sender, EventArgs e)
        {
            if (DL.log == false)
            {
                this.Close();
                MessageBox.Show("请先登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                showALL();
            }
        }
    }
}
