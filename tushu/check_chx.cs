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
    public partial class check_chx : Form
    {
        public check_chx()
        {
            InitializeComponent();
        }
        public static string ts_i;
        SqlDataAdapter dats;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from tushubiao";
            dats = new SqlDataAdapter(str, DB.cn);
            dats.Fill(ds,"ts_info");
        }
        void showALL()
        {
            DataView dats = new DataView(ds.Tables["ts_info"]);
            dataGridView1.DataSource = dats;
        }
        void header()
        {
            dataGridView1.Columns[0].HeaderText = "ISBN";
            dataGridView1.Columns[1].HeaderText = "书名";
            dataGridView1.Columns[2].HeaderText = "类型名称";
            dataGridView1.Columns[3].HeaderText = "作者";
            dataGridView1.Columns[4].HeaderText = "出版社";
            dataGridView1.Columns[5].HeaderText = "出版日期";
            dataGridView1.Columns[6].HeaderText = "单价";
            dataGridView1.Columns[7].HeaderText = "状态";
        }
        void showXZ()
        {
            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode.HeaderText = "选择";
            dataGridView1.Columns.Add(acCode);
        }

        private void check_chx_Load(object sender, EventArgs e)
        {
            if (DL.log==false)
            {
                this.Close();
                MessageBox.Show("请先登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                init();
                showALL();
                header();
                showXZ();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> List_ts = new List<string>();
            if (dataGridView1.Rows.Count>0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
                    {
                        string x = (string)dataGridView1.Rows[i].Cells["ISBN"].Value;
                        List_ts.Add(x);
                    }
                }
                foreach (string y in List_ts)
                {
                    ts_i += y + "','";
                }
                ts_i = ts_i.Substring(0, ts_i.Length - 3);

                tushu.check_em el = new check_em();
                el.ShowDialog();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
