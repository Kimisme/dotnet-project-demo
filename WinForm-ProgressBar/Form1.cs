using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_ProgressBar
{
    public partial class Form1 : Form
    {
        //添加一个委托
        private delegate void SetPos(int total, int finish);
        public Form1()
        {
            InitializeComponent();
        }

        #region 进度条 - SetTextPos(int total, int finish)
        private void SetTextPos(int total, int finish)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextPos);
                this.Invoke(setpos, new object[] { total, finish });
            }
            else
            {
                this.lbNum.Text = string.Format("进度：{0}/{1} ", finish, total);
                this.pgb.Value = Convert.ToInt32(finish * 100 / total);

                if (this.pgb.Value == 100)
                {
                    this.lbNum.Visible = true;
                    this.pgb.Visible = true;
                    this.btnStart.Enabled = true;
                }
                else
                {
                    this.lbNum.Visible = true;
                    this.pgb.Visible = true;
                    this.btnStart.Enabled = false;
                }
            }
        }
        #endregion

        #region 初始化数据 - InitData()
        private List<int> InitData()
        {
            var list = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(0);
            }
            return list;
        }
        #endregion

        #region 处理数据 - DealData()
        private void DealData()
        {
            var list = InitData();
            for (int i = 0; i < list.Count; i++)
            {
                //处理数据
                SetPos sp = new SetPos(SetTextPos);
                sp(list.Count, i + 1);
            }
            MessageBox.Show("执行完成");
        }
        #endregion

        # region 开始方法 - btnStart_Click(object sender, EventArgs e)
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("点击按钮开始执行进度条", "请确认信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Thread thread = new Thread(() =>
                {
                    DealData();
                });
                thread.Start();
            }
        }
        #endregion
    }
}
