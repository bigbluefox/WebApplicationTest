using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSP.MediaRetrieve
{
    public partial class frmThreadDelegate : Form
    {
        delegate double CalculateMethod(double Diameter); //申明一个委托，表明需要在子线程上执行的方法的函数签名
        static CalculateMethod calcMethod = new CalculateMethod(Calculate);//把委托和具体的方法关联起来



        public frmThreadDelegate()
        {
            InitializeComponent();

            //此处开始异步执行,并且可以给出一个回调函数(如果不需要执行什么后续操作也可以不使用回调)
            calcMethod.BeginInvoke(5, new AsyncCallback(TaskFinished), null);
            //Console.ReadLine();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //线程调用的函数,给出直径作为参数,计算周长
        public static double Calculate(double diameter)
        {
            return diameter * Math.PI;
        }

        //线程完成之后回调的函数
        public void TaskFinished(IAsyncResult result)
        {
            double re = 0;
            re = calcMethod.EndInvoke(result);
            //Console.WriteLine(re);

            rtxThreadingMsg.Text += string.Format("周长为： {0};" + Environment.NewLine, re);
        }
    }
}
