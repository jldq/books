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
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
        }
        SqlDataAdapter dalx, dalog;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GrtCn();
            string str = "select * from leixingbiao";
            string sdr = "select * from Log";
            dalx = new SqlDataAdapter(str, DB.cn);
            dalog = new SqlDataAdapter(sdr, DB.cn);
            dalx.Fill(ds,"lx_info");
            dalog.Fill(ds, "log_info");
        }
        void showALL()
        {
            DataView dalx = new DataView(ds.Tables["lx_info"]);
            dataGridView1.DataSource = dalx;
            dataGridView1.Columns[2].Width = 500;
        }
        void dgvHeader()
        {
            dataGridView1.Columns[0].HeaderText = "类型名称";
            dataGridView1.Columns[1].HeaderText = "借阅期限";
            dataGridView1.Columns[2].HeaderText = "类型描述";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            string lxname = dataGridView1.Rows[n].Cells["l_leixing"].Value.ToString();
            string str = "select * from tushubiao where l_leixing='" + lxname + "'";
            DataTable dt = DB.GetDataSet(str); 
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("不能删除此纪录,请先删除图书表中的相关信息");
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
                    drlog["action_table"] = "类型表";
                    ds.Tables["log_info"].Rows.Add(drlog);

                    UpdateDB();
                }
            }
        }
        void UpdateDB()
        {
            try
            {
                SqlCommandBuilder dblx = new SqlCommandBuilder(dalx);
                SqlCommandBuilder dblog = new SqlCommandBuilder(dalog);
                dalx.Update(ds, "lx_info");
                dalog.Update(ds, "log_info");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                dgvHeader();
                label2.Text = DL.name;
            }
        }
    }
}
