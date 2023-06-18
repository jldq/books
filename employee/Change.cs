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
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        SqlDataAdapter daemp, dalog;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GrtCn();
            string str = "select * from yuangongbiao";
            daemp = new SqlDataAdapter(str, DB.cn);
            daemp.Fill(ds, "emp_info");
            string sdr = "select * from  Log";
            dalog = new SqlDataAdapter(sdr, DB.cn);
            dalog.Fill(ds, "log_info");
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


        private void Change_Load(object sender, EventArgs e)
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
                showALL();
                dgvHeader();
                label10.Text = DL.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("性名不能为空!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要修改吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    int a = dataGridView1.CurrentRow.Index;
                    string str = dataGridView1.Rows[a].Cells["y_id"].Value.ToString();
                    DataRow[] dremp = ds.Tables["emp_info"].Select("y_id='" + str + "'");
                    dremp[0]["y_xingming"] = textBox2.Text;
                    dremp[0]["y_dianhua"] = textBox3.Text;
                    dremp[0]["y_dizhi"] = textBox4.Text;
                    dremp[0]["y_gongzi"] = decimal.Parse(textBox5.Text);
                    dremp[0]["y_xingbie"] = comboBox1.Text;
                    dremp[0]["y_rzriqi"] = dateTimePicker1.Value;
                    dremp[0]["y_shengri"] = dateTimePicker1.Value;

                    //添加日志
                    DataRow drlog = ds.Tables["log_info"].NewRow();
                    drlog["username"] = DL.name;
                    drlog["type"] = "修改";
                    drlog["action_date"] = DateTime.Now;
                    drlog["action_table"] = "员工表";
                    ds.Tables["log_info"].Rows.Add(drlog);

                    MessageBox.Show("修改成功!");
                    UpdateDB();
                }
            }
        }
        void UpdateDB()
        {
            try
            {
                SqlCommandBuilder dbts = new SqlCommandBuilder(daemp);
                SqlCommandBuilder dblog = new SqlCommandBuilder(dalog);
                daemp.Update(ds, "emp_info");
                dalog.Update(ds, "log_info");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[n].Cells["y_id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells["y_xingming"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells["y_xingbie"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["y_rzriqi"].Value.ToString());
            textBox3.Text = dataGridView1.Rows[n].Cells["y_dianhua"].Value.ToString();
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["y_shengri"].Value.ToString());
            textBox4.Text = dataGridView1.Rows[n].Cells["y_dizhi"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[n].Cells["y_gongzi"].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[n].Cells["y_id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells["y_xingming"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells["y_xingbie"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["y_rzriqi"].Value.ToString());
            textBox3.Text = dataGridView1.Rows[n].Cells["y_dianhua"].Value.ToString();
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[n].Cells["y_shengri"].Value.ToString());
            textBox4.Text = dataGridView1.Rows[n].Cells["y_dizhi"].Value.ToString(); 
            textBox5.Text = dataGridView1.Rows[n].Cells["y_gongzi"].Value.ToString();
        }
    }
}
