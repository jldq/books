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
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }
        SqlDataAdapter dajh, dakh,dats;
        DataSet ds = new DataSet();
        void init()
        {

            DB.GrtCn();
            string str = "select * from jiehuanbiao";
            //string sdr = "select * from yuangongbiao";
            string sdv = "select * from kehubiao";
            string sts = "select * from tushubiao";
            //daemp = new SqlDataAdapter(sdr, DB.cn);
            dajh = new SqlDataAdapter(str, DB.cn);
            dakh = new SqlDataAdapter(sdv, DB.cn);
            dats = new SqlDataAdapter(sts, DB.cn);
            dats.Fill(ds, "ts_info");
            dakh.Fill(ds, "kh_info");
            dajh.Fill(ds, "jh_info");
            //daemp.Fill(ds, "emp_info");
        }
        void showALL()
        {
            DataView dejh = new DataView(ds.Tables["jh_info"]);
            dataGridView1.DataSource = dejh;
        }
        void Header()
        {
            dataGridView1.Columns[0].HeaderText = "借还编号";
            dataGridView1.Columns[1].HeaderText = "ISBN";
            dataGridView1.Columns[2].HeaderText = "客户编号";
            dataGridView1.Columns[3].HeaderText = "借员工编号";
            dataGridView1.Columns[4].HeaderText = "借出日期";
            dataGridView1.Columns[5].HeaderText = "还代理人";
            dataGridView1.Columns[6].HeaderText = "归还日期";
            dataGridView1.Columns[7].HeaderText = "备注";
        }
        //void showEmp()
        //{
        //    DataView em = new DataView(ds.Tables["emp_info"]);
        //    comboBox1.DisplayMember = "y_xingming";
        //    comboBox1.ValueMember = "y_id";
        //    comboBox1.DataSource = em;
        //}

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
            //comboBox4.DataSource = ts;
            comboBox4.DisplayMember = "s_shuming";
            comboBox4.ValueMember = "ISBN";
            comboBox4.DataSource = ts;
            //string tss = "select * from tushubiao where ISBN >0";
            List<string> strList = new List<string>();
            foreach (var i in ts)
            {
                strList.Add(i.ToString());
            }
            comboBox4.DisplayMember = "s_shuming";
            comboBox4.ValueMember = "ISBN";
            Cursor = Cursors.Default;
            string s = comboBox4.Text;
            List<string[]> list_book = new List<string[]>();
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            foreach (var item in strList)
            {
                if (item.Contains(s))
                {
                    list_book.Add(new string[] { "", item });
                }
            }
            if (list_book.Count >= 1)
            {
                comboBox4.Items.AddRange(list_book.ToArray());
                comboBox4.DataSource = strList;
            }
            comboBox4.SelectionStart = comboBox4.Text.Length;
            comboBox4.DroppedDown = true;
            Cursor.Current = Cursors.Default;
            comboBox4.MaxDropDownItems = 8;
        }
        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataView emName = new DataView(ds.Tables["jh_info"]);
        //    emName.RowFilter = "y_id='" + comboBox1.SelectedValue.ToString() + "'";
        //    //dataGridView1.DataSource = emName;
        //}

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBox4.Text;
            DataView khName = new DataView(ds.Tables["jh_info"]);
            khName.RowFilter = "k_id='" + comboBox3.SelectedValue.ToString() + "'";
            comboBox4.FindString(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static string[] GetItem(string[] item)
        {
            return item;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showALL();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string s = comboBox4.Text;
            DataView tsName = new DataView(ds.Tables["jh_info"]);
            tsName.RowFilter = "ISBN='" + comboBox4.SelectedValue.ToString() + "'";
            //comboBox4.DataSource = "select * from tushubiao where s_shuming LIKE'"+ s +"'" ;
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
                showTS();
                showALL();
                Header();
                label10.Text = DL.name;
                label1.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DB.GrtCn();
           string str = "update tushubiao set  s_zhangtai='借出' where ISBN='" + comboBox4.SelectedValue + "'";
           DB.sqlEx(str);

           //方法3
           DataRow jhRow = ds.Tables["jh_info"].NewRow();
           jhRow["ISBN"] = comboBox4.SelectedValue;
           jhRow["k_id"] = comboBox3.SelectedValue;
           jhRow["y_id"] =label1.Text;
           jhRow["j_jcriqi"] = dateTimePicker1.Value;
           ds.Tables["jh_info"].Rows.Add(jhRow);

            //使用存储过程添加日志
           DB.GrtCn();
           SqlCommand cmd = new SqlCommand("add_log", DB.cn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
           cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
           cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
           cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

           cmd.Parameters["username"].Value = DL.name;
           cmd.Parameters["type"].Value = "借书";
           cmd.Parameters["action_date"].Value = DateTime.Now;
           cmd.Parameters["action_table"].Value = "借还表";
           try
           {
               SqlCommandBuilder dbjh = new SqlCommandBuilder(dajh);
               dajh.Update(ds, "jh_info");
               cmd.ExecuteNonQuery();
               MessageBox.Show("借书成功!");
           }
           catch (SqlException ex)
           {
               MessageBox.Show("ex.Message");
           }
           DB.cn.Close();
        }
    }
}
