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

namespace books.jiehuan
{
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter jh, log;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GrtCn();
            string str = "select * from jiehuanbiao";
           
            string sdv = "select * from Log";
            jh = new SqlDataAdapter(str, DB.cn);
           
            log = new SqlDataAdapter(sdv, DB.cn);
            jh.Fill(ds, "jh_info");
            
            log.Fill(ds, "log_info");
        }
        void showALL()
        {
            DataView dvjh = new DataView(ds.Tables["jh_info"]);
            dataGridView1.DataSource = dvjh;
        }
        void head()
        {
            dataGridView1.Columns[0].HeaderText = "借还编号";
            dataGridView1.Columns[1].HeaderText = "ISBN";
            dataGridView1.Columns[2].HeaderText = "客户姓名";
            dataGridView1.Columns[3].HeaderText = "借员工姓名";
            dataGridView1.Columns[4].HeaderText = "借出日期";
            dataGridView1.Columns[5].HeaderText = "还代理人";
            dataGridView1.Columns[6].HeaderText = "归还日期";
            dataGridView1.Columns[7].HeaderText = "备注";
        }
        //void showemp()
        //{
        //    DataView em = new DataView(ds.Tables["emp_info"]);
        //    comboBox1.DisplayMember = "y_xingming";
        //    comboBox1.ValueMember = "y_id";
        //    comboBox1.DataSource = em;
        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataView emName = new DataView(ds.Tables["jh_info"]);
        //    emName.RowFilter = "y_id='" + comboBox1.SelectedValue.ToString() + "'";
        //    dataGridView1.DataSource = emName;
        //}

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
                //showemp();
                showALL();
                head();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value>=dateTimePicker2.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvjc = new DataView(ds.Tables["jh_info"]);
                    dvjc.RowFilter = "j_jcriqi>='" + dateTimePicker1.Value + "' and j_jcriqi<='" + dateTimePicker2.Value+"'";
                    dataGridView1.DataSource = dvjc;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value >= dateTimePicker4.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvgh = new DataView(ds.Tables["jh_info"]);
                    dvgh.RowFilter = "j_hriqi>='" + dateTimePicker3.Value + "' and j_hriqi<='" + dateTimePicker4.Value + "'";
                    dataGridView1.DataSource = dvgh;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showALL();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvsf = new DataView(ds.Tables["jh_info"]);
            int i = comboBox2.SelectedIndex;
            //int a = dataGridView1.CurrentRow.Index;
            //int x = dataGridView1.Rows[a].Cells["j_huandlr"].Value.ToString().Length;
            //int y = dataGridView1.Rows[a].Cells["j_hriqi"].Value.ToString().Length;
            switch (i)
            {
                case 0:
                    dvsf.RowFilter = "j_huandlr is null";
                    break;
                default:
                    dvsf.RowFilter = "j_huandlr>'0'";
                    break;
            }
            dataGridView1.DataSource = dvsf;
            DB.cn.Close();
        }
    }
}
