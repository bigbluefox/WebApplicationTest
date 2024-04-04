using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hsp.Test.AI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Thread t1 = new Thread(new ThreadStart(Thread1));
            //Thread t2 = new Thread(new ThreadStart(Thread2));

            //t1.Priority = ThreadPriority.BelowNormal;
            //t2.Priority = ThreadPriority.Lowest;
            //t1.Start();
            //t2.Start();

        }

        public static void Thread1()
        {
            for (int i = 1; i < 1000; i++)
            {//每运行一个循环就写一个“1”
                dosth();
                Console.Write("1");
            }
        }

        public static void Thread2()
        {
            for (int i = 0; i < 1000; i++)
            {//每运行一个循环就写一个“2”
                dosth();
                Console.Write("2");
            }
        }

        public static void dosth()
        {//用来模拟复杂运算
            for (int j = 0; j < 10000000; j++)
            {
                int a = 15;
                a = a * a * a * a;
            }
        }


    }
}
