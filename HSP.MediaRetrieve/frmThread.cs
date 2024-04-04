using System;
using System.Threading;
using System.Windows.Forms;

namespace HSP.MediaRetrieve
{
    public partial class frmThread : Form
    {
        //线程同步的方法有很多很多种volatile、Lock、InterLock、Monitor、Mutex、ReadWriteLock...
        //这里用lock说明问题：在哪里同步，用什么同步，同步谁？

        static object obj = new object();//同步用
        static int balance = 500;

        //多线程调用带参函数还有一种更简单的方式：
        //Thread thead = new Thread(new ThreadStart(delegate() {
        ////调用方法(参数1，参数2……);
        //}));
        //thead.Start();



        public frmThread()
        {
            InitializeComponent();

            ThreadStart threadStart = Calculate; //通过ThreadStart委托告诉子线程讲执行什么方法,这里执行一个计算圆周长的方法
            var thread = new Thread(threadStart);
            thread.Start(); //启动新线程

            rtbThreadMsg.Text += string.Format("主线程开始");
            Thread t = new Thread(new ThreadStart(ShowTime));//注意ThreadStart委托的定义形式
            t.Start();//线程开始，控制权返回Main线程
            rtbThreadMsg.Text += string.Format("主线程继续执行");
            //while (t.IsAlive == true) ;
            Thread.Sleep(1000);
            t.Abort();
            t.Join();//阻塞Main线程,直到t终止
            rtbThreadMsg.Text += string.Format("--------------");


            rtbThreadMsg.Text += string.Format("Main线程开始");
            t = new Thread(new ParameterizedThreadStart(DoSomething));//注意ParameterizedThreadStart委托的定义形式
            t.Start(new string[] { "Hello", "World" });
            rtbThreadMsg.Text += string.Format("Main线程继续执行");

            Thread.Sleep(1000);
            t.Abort();
            t.Join();//阻塞Main线程,直到t终止




            Guest guest = new Guest()
            {
                Name = "Hello",
                Age = 99
            };
            t = new Thread(new ThreadStart(guest.DoSomething));//注意ThreadStart委托的定义形式
            t.Start();

            Thread.Sleep(1000);
            t.Abort();
            t.Join();//阻塞Main线程,直到t终止


            Thread t1 = new Thread(new ThreadStart(Credit));
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(Debit));
            t2.Start();
        }

        public void Calculate()
        {
            var Diameter = 0.5;
            //Console.Write("The perimeter Of Circle with a Diameter of {0} is {1}"
            //Diameter,
            //Diameter*Math.PI)
            //;

            rtbThreadMsg.Text +=
                string.Format("The perimeter Of Circle with a Diameter of {0} is {1}" + Environment.NewLine, Diameter,
                    Diameter*Math.PI);

        }

        void ShowTime()
        {
            while (true)
            {
                rtbThreadMsg.Text += string.Format(DateTime.Now.ToString());
            }
        }

        void DoSomething(object s)
        {
            string[] strs = s as string[];
            while (true)
            {
                rtbThreadMsg.Text += string.Format("{0}--{1}", strs[0], strs[1]);
            }
        }

        void Credit()
        {
            for (int i = 0; i < 15; i++)
            {
                lock (obj)
                {
                    balance += 100;
                    rtbThreadMsg.Text += string.Format("After crediting,balance is {0}", balance);
                }
            }
        }
        void Debit()
        {
            for (int i = 0; i < 15; i++)
            {
                lock (obj)
                {
                    balance -= 100;
                    rtbThreadMsg.Text += string.Format("After debiting,balance is {0}", balance);
                }
            }
        }




        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class Guest
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void DoSomething()
        {
            while (true)
            {
                
                //.Text += string.Format("{0}--{1}", Name, Age);
            }
        }
    }

}




//函数、匿名函数、Lambda表达式用法比较

//using System.Threading.Tasks;
 
//namespace ParallelFor
//{
//    using System.Threading.Tasks;
//    class Test
//    {
//        static int N = 1000;
 
//        static void TestMethod()
//        {
//            // Using a named method.
//            Parallel.For(0, N, Method2);
 
//            // Using an anonymous method.
//            Parallel.For(0, N, delegate(int i)
//            {
//                // Do Work.
//            });
 
//            // Using a lambda expression.
//            Parallel.For(0, N, i =>
//            {
//                // Do Work.
//            });
//        }
 
//        static void Method2(int i)
//        {
//            // Do work.
//        }
//    }
//}