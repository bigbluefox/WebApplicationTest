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

namespace HSP.MediaRetrieve
{
    public partial class frmThreading : Form
    {
        public frmThreading()
        {
            InitializeComponent();

            rtbThreadingMsg.Text += @"开始一个新的线程，名为次线程" + @"时间：" + DateTime.Now + Environment.NewLine;

            //Console.WriteLine("开始一个新的线程，名为次线程");
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
            for (int i = 0; i < 4; i++)
            {
                //Console.WriteLine("主线程：" + i);
                rtbThreadingMsg.Text += @"主线程，编号为：" + i + "；" + @"时间：" + DateTime.Now + Environment.NewLine;
                Thread.Sleep(1000);
            }
            //Console.WriteLine("调用Join函数等待次线程结束");
            rtbThreadingMsg.Text += @"开始一个新的线程，名为次线程，编号未定。" + @"时间：" + DateTime.Now + Environment.NewLine;
            //当次线程执行完毕后,Join阻塞调用线程，直到某个线程终止为止，本例为次线程
            t.Join();
            //Console.WriteLine("线程执行完毕");
            rtbThreadingMsg.Text += @"线程执行完毕！" + @"时间：" + DateTime.Now + Environment.NewLine;
        }

        public void ThreadProc()
        {
            for (int i = 0; i < 10; i++)
            {
                //Console.WriteLine("ThreadPorc:{0}", i);
                rtbThreadingMsg.Text += @"开始一个新的线程，名为次线程，编号为：" + i + "；" + @"时间：" + DateTime.Now + Environment.NewLine;

                Thread.Sleep(1000);//将当前进程阻塞指定的毫秒数
            }
        }
    }
}
