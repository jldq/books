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
    public partial class CHX : Form
    {
        public CHX()
        {
            InitializeComponent();
        }
        SqlDataAdapter daleixing;
        DataSet ds = new DataSet();
        Font f1 = new Font("UTF-8", 10);
        //public static string dep_id;
        void init()
        {
            DB.GrtCn();
            string str = "select * from leixingbiao";
            daleixing = new SqlDataAdapter(str, DB.cn);
            daleixing.Fill(ds, "leixingbiao_info");
            dataGV.Font = f1;
        }
        void showALLdaleixing()
        {
            DataView daleixing = new DataView(ds.Tables["leixingbiao_info"]);
            dataGV.DataSource = daleixing;
            dataGV.Columns[2].Width = 450;
        }
        void dgvHeader()
        {
            dataGV.Columns[0].HeaderText = "类型名称";
            dataGV.Columns[1].HeaderText = "借阅期限";
            dataGV.Columns[2].HeaderText = "类型描述";     
        }
        //void showXZ()
        //{
        //    DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
        //    acCode.Name = "acCode";
        //    acCode.HeaderText = "选择";
        //    dataGV.Columns.Add(acCode);
        //}
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
                dgvdaleixingName();
                dgvdaleixingQixian();
                showALLdaleixing();
                dgvHeader();
                //showXZ();
            }
        }
        void dgvdaleixingName()
        {
            DataView daleixingName = new DataView(ds.Tables["leixingbiao_info"]);
            comboBox1.DisplayMember = "l_leixing";
            comboBox1.ValueMember = "l_leixing";
            comboBox1.DataSource = daleixingName;
        }
        void dgvdaleixingQixian()
        {
            DataView dvleixingQixian = new DataView(ds.Tables["leixingbiao_info"]);
            comboBox2.DisplayMember = "l_qixian";
            comboBox2.ValueMember = "l_leixing";
            comboBox2.DataSource = dvleixingQixian;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvName = new DataView(ds.Tables["leixingbiao_info"]);
            dvName.RowFilter = "l_leixing='" + comboBox1.SelectedValue.ToString() + "'";
            dataGV.DataSource = dvName;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvQixian = new DataView(ds.Tables["leixingbiao_info"]);
            dvQixian.RowFilter = "l_leixing='" + comboBox2.SelectedValue.ToString() + "'";
            dataGV.DataSource = dvQixian;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showALLdaleixing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (dataGV.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataGV.Rows.Count; i++)
        //        {
        //            dataGV.Rows[i].Cells["acCode"].EditedFormattedValue.ToString();
        //            string _selectedValue = dataGV.Rows[i].Cells["acCode"].EditedFormattedValue.ToString();
        //            if (_selectedValue == "True")
        //            {
        //                dep_id = dataGV.Rows[i].Cells["l_leixing"].Value.ToString();
        //            }
        //        }
        //    }
        //    leixing.emp e1 = new emp();
        //    e1.ShowDialog();
        //}

        //private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGV.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataGV.Rows.Count; i++)
        //        {
        //            DataGridViewCheckBoxCell ck = dataGV.Rows[i].Cells["acCode"] as DataGridViewCheckBoxCell;
        //            if (i != e.RowIndex)
        //            {
        //                ck.Value = false;
        //            }
        //            else
        //            {
        //                ck.Value = true;
        //            }
        //        }
        //    }
        //}
    }
}
