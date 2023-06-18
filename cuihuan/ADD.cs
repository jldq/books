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
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }
        SqlDataAdapter dach, daemp, dakh, dats;
        DataSet ds = new DataSet();
        void init()
        {

            DB.GrtCn();
            string str = "select * from cuihuanbiao";
            string sdr = "select * from yuangongbiao";
            string sdv = "select * from kehubiao";
            string sts = "select * from tushubiao";
            daemp = new SqlDataAdapter(sdr, DB.cn);
            dach = new SqlDataAdapter(str, DB.cn);
            dakh = new SqlDataAdapter(sdv, DB.cn);
            dats = new SqlDataAdapter(sts, DB.cn);
            dats.Fill(ds, "ts_info");
            dakh.Fill(ds, "kh_info");
            dach.Fill(ds, "ch_info");
            daemp.Fill(ds, "emp_info");
        }
        void showALL()
        {
            DataView dejh = new DataView(ds.Tables["ch_info"]);
            dataGridView1.DataSource = dejh;
        }
        void Header()
        {
            dataGridView1.Columns[0].HeaderText = "催还编号";
            dataGridView1.Columns[1].HeaderText = "ISBN";
            dataGridView1.Columns[2].HeaderText = "客户编号";
            dataGridView1.Columns[3].HeaderText = "催员工编号";
            dataGridView1.Columns[4].HeaderText = "催还日期";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.GrtCn();

            //方法3
            DataRow chRow = ds.Tables["ch_info"].NewRow();
            chRow["ISBN"] = comboBox4.SelectedValue;
            chRow["k_id"] = comboBox3.SelectedValue;
            chRow["y_id"] = comboBox1.SelectedValue;
            chRow["c_hriqi"] = dateTimePicker1.Value;
            ds.Tables["ch_info"].Rows.Add(chRow);

            //使用存储过程添加日志
            SqlCommand cmd = new SqlCommand("add_log", DB.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
            cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

            cmd.Parameters["username"].Value = DL.name;
            cmd.Parameters["type"].Value = "催还";
            cmd.Parameters["action_date"].Value = DateTime.Now;
            cmd.Parameters["action_table"].Value = "催还表";
            try
            {
                SqlCommandBuilder dbch = new SqlCommandBuilder(dach);
                dach.Update(ds, "ch_info");
                cmd.ExecuteNonQuery();
                MessageBox.Show("添加成功!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("ex.Message");
            }
            DB.cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showALL();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView khName = new DataView(ds.Tables["ch_info"]);
            khName.RowFilter = "k_id='" + comboBox3.SelectedValue.ToString() + "'";
            dataGridView1.DataSource = khName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView emName = new DataView(ds.Tables["ch_info"]);
            emName.RowFilter = "y_id='" + comboBox1.SelectedValue.ToString() + "'";
            dataGridView1.DataSource = emName;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView tsName = new DataView(ds.Tables["ch_info"]);
            tsName.RowFilter = "ISBN='" + comboBox4.SelectedValue.ToString() + "'";
            dataGridView1.DataSource = tsName;
        }

        private void ADD_Load(object sender, EventArgs e)
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
                init();
                showKH();
                showEmp();
                showTS();
                showALL();
                Header();
                label10.Text = DL.name;
            }
        }

        void showEmp()
        {
            DataView em = new DataView(ds.Tables["emp_info"]);
            comboBox1.DisplayMember = "y_xingming";
            comboBox1.ValueMember = "y_id";
            comboBox1.DataSource = em;
        }

        void showKH()
        {
            DataView kh = new DataView(ds.Tables["kh_info"]);
            comboBox3.DisplayMember = "k_xingming";
            comboBox3.ValueMember = "k_id";
            comboBox3.DataSource = kh;
        }
        void showTS()
        {
            DataView ts = new DataView(ds.Tables["ts_info"]);
            comboBox4.DisplayMember = "s_shuming";
            comboBox4.ValueMember = "ISBN";
            comboBox4.DataSource = ts;
        }
       
    }
}
