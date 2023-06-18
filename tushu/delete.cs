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
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
        }
        SqlDataAdapter  dats, dalog;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from tushubiao";
            string sor = "select * from Log";
            dats = new SqlDataAdapter(str, DB.GrtCn());       
            dalog = new SqlDataAdapter(sor, DB.GrtCn());
            dats.Fill(ds, "ts_info");
            dalog.Fill(ds, "log_info");
        }
        void showALL()
        {
            DataView dvts = new DataView(ds.Tables["ts_info"]);
            dataGridView1.DataSource = dvts;
        }
        void head()
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

        private void delete_Load(object sender, EventArgs e)
        {
            if (DL.Alog == false)
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
                head();
                label2.Text = DL.name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            string ISBN= dataGridView1.Rows[n].Cells["ISBN"].Value.ToString();
            string str = "select * from jiehuanbiao where ISBN='" + ISBN + "'";
            DataTable dt = DB.GetDataSet(str);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("不能删除此纪录,请先删除借还记录表中的相关信息");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要删除吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    dataGridView1.Rows.RemoveAt(n);
                    MessageBox.Show("删除成功!");

                    //添加日志
                    DataRow drlog = ds.Tables["log_info"].NewRow();
                    drlog["username"] = DL.name;
                    drlog["type"] = "删除";
                    drlog["action_date"] = DateTime.Now;
                    drlog["action_table"] = "图书表";
                    ds.Tables["log_info"].Rows.Add(drlog);

                    UpdateDB();
                }
            }
            void UpdateDB()
            {
                try
                {
                    SqlCommandBuilder dblx = new SqlCommandBuilder(dats);
                    SqlCommandBuilder dblog = new SqlCommandBuilder(dalog);
                    dats.Update(ds, "ts_info");
                    dalog.Update(ds, "log_info");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
