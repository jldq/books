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
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
        }
        public static string emp_id;
        void showALL()
        {
            DB.GrtCn();
            string str = "select * from yuangongbiao";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (rdr.Read())
            {
                int index = dataGridView1.Rows.Add();
                for (int i = 0; i <= 7; i++)
                {
                    dataGridView1.Rows[index].Cells[i].Value = rdr[i];
                }
            }
            rdr.Close();
        }

        private void delete_Load(object sender, EventArgs e)
        {
            if (DL.Alog == false)
            {
                this.Close();
                MessageBox.Show("请以管理员的身份登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                showALL();
                label2.Text = DL.name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> list_emp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Column9"].EditedFormattedValue.ToString()=="True")
                {
                    string x = (string)dataGridView1.Rows[i].Cells["Column1"].Value;
                    list_emp.Add(x);
                }
            }
            foreach (string y in list_emp)
            {
                emp_id += y + "','";
            }
            emp_id = emp_id.Substring(0, emp_id.Length - 3);

            string str = "select * from jiehuanbiao where y_id in('" + emp_id + "')";
            DataTable dt = DB.GetDataSet(str);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("此信息不能删除,请先删除借还记录表中相关信息");
            }
            else
            {
                DialogResult dr = MessageBox.Show("你确定要删除吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    string sdr = "delete from yuangongbiao where y_id  in('" + emp_id + "')";
                    DB.sqlEx(sdr);
                    MessageBox.Show("删除成功!");
                    showALL();

                    string sdt = "insert into Log(username,type,action_date,action_table) values ('" + DL.name + "','删除','" + DateTime.Now + "','员工表')";
                    DB.sqlEx(sdt);
                }
            }
            DB.cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
