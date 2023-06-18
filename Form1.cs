using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace books
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DL dl = new DL();
            dl.ShowDialog();
        }

        private void 注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZC zl = new ZC();
            zl.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CHPWD ch1 = new CHPWD();
            ch1.ShowDialog();
        }

        private void 类型表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing.CHX chl = new leixing.CHX();
            chl.ShowDialog();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tushu.CHX CHl = new tushu.CHX();
            CHl.ShowDialog();
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing.ADD al = new leixing.ADD();
            al.ShowDialog();
        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tushu.ADD al = new tushu.ADD();
            al.ShowDialog();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing.Change cl = new leixing.Change();
            cl.ShowDialog();
        }

        private void 查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            employee.CHX chl = new employee.CHX();
            chl.ShowDialog();
        }

        private void 查询ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            kehu.CHX chl = new kehu.CHX();
            chl.ShowDialog();
        }

        private void 修改ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            kehu.Change cl = new kehu.Change();
            cl.ShowDialog();
        }

        private void 添加ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            kehu.ADD al = new kehu.ADD();
            al.ShowDialog();
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tushu.Change cl = new tushu.Change();
            cl.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing.delete dl = new leixing.delete();
            dl.ShowDialog();
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            kehu.delete d2 = new kehu.delete();
            d2.ShowDialog();
        }

        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tushu.delete d3 = new tushu.delete();
            d3.ShowDialog();
        }

        private void 添加ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            employee.ADD al = new employee.ADD();
            al.ShowDialog();
        }

        private void 修改ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            employee.Change cl = new employee.Change();
            cl.ShowDialog();
        }

        private void 删除ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            employee.delete el = new employee.delete();
            el.ShowDialog();
        }

        private void 图书借还多选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tushu.check_chx hd = new tushu.check_chx();
            hd.ShowDialog();
        }

        private void 类型图书单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leixing.dep dl = new leixing.dep();
            dl.ShowDialog();
        }

        private void 查询ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            jiehuan.CHX cc = new jiehuan.CHX();
            cc.ShowDialog();
        }

        private void 图书详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tushu.chx2 cw = new tushu.chx2();
            cw.ShowDialog();
        }

        private void 添加ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            jiehuan.ADD ac = new jiehuan.ADD();
            ac.ShowDialog();
        }

        private void 修改ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            jiehuan.Change ca = new jiehuan.Change();
            ca.ShowDialog();
        }

        private void 查询ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            cuihuan.CHX chx = new cuihuan.CHX();
            chx.ShowDialog();
        }

        private void 添加ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            cuihuan.ADD cA = new cuihuan.ADD();
            cA.ShowDialog();
        }
    }
}
