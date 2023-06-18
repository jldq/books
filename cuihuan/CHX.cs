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

namespace books.cuihuan
{
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter ch, log;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GrtCn();
            string str = "select * from cuihuanbiao";
            string sdv = "select * from Log";
            ch = new SqlDataAdapter(str, DB.cn);
            log = new SqlDataAdapter(sdv, DB.cn);
            ch.Fill(ds, "ch_info");
            log.Fill(ds, "log_info");
        }
        void showALL()
        {
            DataView dvch = new DataView(ds.Tables["ch_info"]);
            dataGridView1.DataSource = dvch;
        }
        void head()
        {
            dataGridView1.Columns[0].HeaderText = "催还编号";
            dataGridView1.Columns[1].HeaderText = "ISBN";
            dataGridView1.Columns[2].HeaderText = "客户编号";
            dataGridView1.Columns[3].HeaderText = "催员工编号";
            dataGridView1.Columns[4].HeaderText = "催还日期";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvch = new DataView(ds.Tables["ch_info"]);
                    dvch.RowFilter = "c_hriqi>='" + dateTimePicker1.Value + "' and c_hriqi<='" + dateTimePicker2.Value + "'";
                    dataGridView1.DataSource = dvch;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void CHX_Load(object sender, EventArgs e)
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
                init();
                showALL();
                head();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showALL();
        }
    }
}
