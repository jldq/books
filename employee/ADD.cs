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

namespace books.employee
{
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }
        SqlDataAdapter daemp;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GrtCn();
            string str = "select * from yuangongbiao";
            daemp = new SqlDataAdapter(str, DB.cn);
            daemp.Fill(ds, "emp_info");
        }
        void showALL()
        {
            DataView dvemp = new DataView(ds.Tables["emp_info"]);
            dataGridView1.DataSource = dvemp;
        }
        void dgvHeader()
        {
            dataGridView1.Columns[0].HeaderText = "员工编号";
            dataGridView1.Columns[1].HeaderText = "员工姓名";
            dataGridView1.Columns[2].HeaderText = "性别";
            dataGridView1.Columns[3].HeaderText = "入职日期";
            dataGridView1.Columns[4].HeaderText = "电话";
            dataGridView1.Columns[5].HeaderText = "生日";
            dataGridView1.Columns[6].HeaderText = "地址";
            dataGridView1.Columns[7].HeaderText = "工资";
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
                //return;
                init();
                showALL();
                dgvHeader();
                label10.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("员工编号或姓名不能为空");
            }
            else
            {
                DB.GrtCn();
                string str = "select * from yuangongbiao where y_id='" + textBox1.Text + "'";
                string sdr = "select * from yuangongbiao where y_xingming='" + textBox2.Text + "'";
                DataTable dt1 = DB.GetDataSet(str);
                DataTable dt2 = DB.GetDataSet(sdr);
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("该员工编号已经存在,请重新输入");
                }
                else if (dt2.Rows.Count > 0)
                {
                    MessageBox.Show("该姓名已经存在,请重新输入");
                }
                else
                {
                    //方法3
                    DataRow empRow = ds.Tables["emp_info"].NewRow();
                    empRow["y_id"] = textBox1.Text;
                    empRow["y_xingming"] = textBox2.Text;
                    empRow["y_xingbie"] = comboBox1.Text;
                    empRow["y_rzriqi"] = dateTimePicker1.Value;
                    empRow["y_dianhua"] = textBox3.Text;
                    empRow["y_shengri"] = dateTimePicker2.Value;
                    empRow["y_dizhi"] = textBox4.Text;
                    empRow["y_gongzi"] = textBox5.Text;
                    ds.Tables["emp_info"].Rows.Add(empRow);

                    //使用存储过程添加日志
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.VarChar));

                    cmd.Parameters["username"].Value = DL.name;
                    cmd.Parameters["type"].Value = "添加";
                    cmd.Parameters["action_date"].Value = DateTime.Now;
                    cmd.Parameters["action_table"].Value = "员工表";
                    try
                    {
                        SqlCommandBuilder dbkh = new SqlCommandBuilder(daemp);
                        daemp.Update(ds, "emp_info");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("添加成功!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("ex.Message");
                    }
                    DB.cn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showALL();
        }
    }
}
