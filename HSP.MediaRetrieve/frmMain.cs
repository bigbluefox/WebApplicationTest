using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace HSP.MediaRetrieve
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检索图片文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImage_Click(object sender, EventArgs e)
        {
            var frm = new frmImage();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 检索音频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudio_Click(object sender, EventArgs e)
        {
            var frm = new frmAudio();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 检索视频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVideo_Click(object sender, EventArgs e)
        {
            var frm = new frmVideo();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowMusic_Click(object sender, EventArgs e)
        {
            var frm = new frmMusic();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnMediaInfo_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnGetMediaInfo_Click(object sender, EventArgs e)
        {
            var frm = new frmMediaInfo();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private int type = 0;

        /// <summary>
        /// 按钮背景图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttach_Click(object sender, EventArgs e)
        {
            //this.btnAttach.BackgroundImage = global::HSP.MediaRetrieve.Properties.Resources.attach11;

            Button bt = (Button)sender;

            if (type == 0)
            {
                type = 1;
                this.btnAttach.BackgroundImage = global::HSP.MediaRetrieve.Properties.Resources.document161;
            }
            else
            {
                type = 0;
                this.btnAttach.BackgroundImage = global::HSP.MediaRetrieve.Properties.Resources.attach11;
            }
            //this.btnAttach.Enabled = false;
        }

        /// <summary>
        /// 线程测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThreadingTest_Click(object sender, EventArgs e)
        {
            var frm = new frmThreading();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 线程测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmThreading();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        //static void CountNumber(int cnt)
        //{
        //    for (int i = 0; i < cnt; i++)
        //    {

        //        Thread.Sleep(500);
        //        Console.WriteLine(string.Format(" {0}    打印 {1,11} 数字", Thread.CurrentThread.Name, i.ToString("N0")));

        //    }

        //}

        //static void Count(object cnt)
        //{
        //    CountNumber((int)cnt);
        //}
        //static void PrintNumber(int num)
        //{

        //    Console.WriteLine(string.Format(" {0} 打印 {1,11} 数字", Thread.CurrentThread.Name, num.ToString("N0")));

        //}

        /// <summary>
        /// 統計代碼行數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 統計代碼行數ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmCodeLines();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 使用Thread类 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmThread();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 使用Delegate.BeginInvoke 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginInvokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmThreadDelegate();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        /// <summary>
        /// 使用ThreadPool.QueueworkItem 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmThreadPool();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }



    }
}
