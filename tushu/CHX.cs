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
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter deleixing;
        SqlDataAdapter detushu;
        DataSet ds = new DataSet();
        private string x;

        void init()
        {
            DB.GrtCn();
            string str = "select * from tushubiao";
            string sdr = "select * from leixingbiao";
            deleixing = new SqlDataAdapter(sdr, DB.cn);
            detushu = new SqlDataAdapter(str, DB.cn);
            detushu.Fill(ds, "tushubiao_info");
            deleixing.Fill(ds, "leixingbiao_info");
        }
        void showALLtushu()
        {
            DataView detushu = new DataView(ds.Tables["tushubiao_info"]);
            dataGridView1.DataSource = detushu;
        }
        void dgvHeader()
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
        void showleixing()
        {
            DataView daleixingName = new DataView(ds.Tables["leixingbiao_info"]);
            comboBox1.DisplayMember = "l_leixing";
            comboBox1.ValueMember = "l_leixing";
            comboBox1.DataSource = daleixingName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvName = new DataView(ds.Tables["tushubiao_info"]);
            dvName.RowFilter = "l_leixing='" + comboBox1.SelectedValue.ToString() + "'";
            dataGridView1.DataSource = dvName;
        }

        private void CHX_load(object sender, EventArgs e)
        {
            if (DL.log == false)
            {
                this.Close();
                MessageBox.Show("请先登录");
                DL dl = new DL();
                dl.ShowDialog();
            }
            else
            {
                init();
                showleixing();
                showALLtushu();
                dgvHeader();
                showXZ();
            }
        }
        void showXZ()
        {
            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode.HeaderText = "选择";
            dataGridView1.Columns.Add(acCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showALLtushu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入查询关键字");
            }
            else
            {
                DataView dvEn = new DataView(ds.Tables["tushubiao_info"]);
                dvEn.RowFilter = "s_shuming LIKE '%" + textBox1.Text + "%'";
                if (dvEn.Count == 0)
                {
                    MessageBox.Show("没有查询到与之相配的结果");
                }
                else
                {
                    dataGridView1.DataSource = dvEn;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("开始日期不能晚于结束日期");
            }
            else
            {
                try
                {
                    DataView dvEm1 = new DataView(ds.Tables["tushubiao_info"]);
                    dvEm1.RowFilter = "s_cbriqi>='" + dateTimePicker1.Value + "'and s_cbriqi<='" + dateTimePicker2.Value + "'";
                    dataGridView1.DataSource = dvEm1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvDanjia = new DataView(ds.Tables["tushubiao_info"]);
            int i = comboBox2.SelectedIndex;
            switch (i)
            {
                case 0:
                    dvDanjia.RowFilter = "s_danjia<=45";
                    break;
                case 1:
                    dvDanjia.RowFilter = "s_danjia>45  and  s_danjia<=55";
                    break;
                case 2:
                    dvDanjia.RowFilter = "s_danjia>55  and  s_danjia<=65";
                    break;
                case 3:
                    dvDanjia.RowFilter = "s_danjia>65  and  s_danjia<=75";
                    break;
                default:
                    dvDanjia.RowFilter = "s_danjia>=75";
                    break;
            }
            dataGridView1.DataSource = dvDanjia;
            DB.cn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB.GrtCn();
            int n = dataGridView1.CurrentRow.Index;          
            if (dataGridView1.Rows[n].Cells["acCode"].EditedFormattedValue.ToString() == "False")
            {
                MessageBox.Show("您未选择书籍,请先选择需借还的书籍");
            }
            else
            {
                string x = dataGridView1.Rows[n].Cells["ISBN"].Value.ToString();

                string str = "update tushubiao set  s_zhangtai='借出' where ISBN='" + x + "'";
                DB.sqlEx(str);

                DB.GrtCn();
                if (DL.Alog == true)
                {
                    string sdt = "insert into jiehuanbiao(ISBN,jieyg,j_jcriqi) values ('" + x + "','" + DL.name + "','" + DateTime.Now + "')";
                    DB.sqlEx(sdt);

                    DB.GrtCn();
                    string svt = "insert into Log(username,type,action_date,action_table) values ('" + DL.name + "','借书','" + DateTime.Now + "','借还表')";
                    DB.sqlEx(svt);
                }
                else
                {
                    string sdt = "insert into jiehuanbiao(ISBN,kehu,j_jcriqi) values ('" + x + "','" + DL.name + "','" + DateTime.Now + "')";
                    DB.sqlEx(sdt);
                }
                MessageBox.Show("借书成功!");
            }
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        if (dataGridView1.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
            //        {
            //            string x = (string)dataGridView1.Rows[i].Cells["ISBN"].Value;
            //        }
            //    }
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DB.GrtCn();
            int n = dataGridView1.CurrentRow.Index;
            if (dataGridView1.Rows[n].Cells["acCode"].EditedFormattedValue.ToString() == "False")
            {
                MessageBox.Show("您未选择书籍,请先选择需借还的书籍");
            }
            else
            {
                string x = dataGridView1.Rows[n].Cells["ISBN"].Value.ToString();

                string str = "update tushubiao set  s_zhangtai='库存' where ISBN='" + x + "'";
                DB.sqlEx(str);

                DB.GrtCn();
                if (DL.Alog == true)
                {
                    string sdt = "update jiehuanbiao set j_huandlr='" + DL.name + "',j_hriqi='" + DateTime.Now + "' where ISBN='" + x + "'";
                    DB.sqlEx(sdt);

                    DB.GrtCn();
                    string svt = "insert into Log(username,type,action_date,action_table) values ('" + DL.name + "','还书','" + DateTime.Now + "','借还表')";
                    DB.sqlEx(svt);
                }
                else
                {
                    string sdt = "update jiehuanbiao set j_huandlr='" + DL.name + "',j_hriqi='" + DateTime.Now + "' where ISBN='" + x + "'";
                    DB.sqlEx(sdt);
                }
                MessageBox.Show("还书成功!");
                DB.cn.Close();
            }
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        if (dataGridView1.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
            //        {
            //            string x = (string)dataGridView1.Rows[i].Cells["ISBN"].Value;
            //        }
            //    }
            //}         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells["acCode"] as DataGridViewCheckBoxCell;
                    if (i != e.RowIndex)
                    {
                        ck.Value = false;
                    }
                    else
                    {
                        ck.Value = true;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExcelService es = new ExcelService();
            es.DataGirdViewExportToExcel(ds, "导出信息");
        }
    }
}
