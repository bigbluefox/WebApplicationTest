using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine("开始一个新的线程，名为次线程");
            //Thread t = new Thread(new ThreadStart(ThreadProc));
            //t.Start();
            //for (int i = 0; i < 10; i++)
            //{
            //    System.Console.WriteLine("主线程：" + i);
            //    Thread.Sleep(1000);
            //}
            //System.Console.WriteLine("调用Join函数等待次线程结束");
            ////当次线程执行完毕后,Join阻塞调用线程，直到某个线程终止为止，本例为次线程
            //t.Join();
            //System.Console.WriteLine("线程执行完毕");

            //Thread t1 = new Thread(new ThreadStart(TestMethod));
            //Thread t2 = new Thread(new ParameterizedThreadStart(TestMethod));
            //t1.IsBackground = true;
            //t2.IsBackground = true;
            //t1.Start();
            //t2.Start("hello");
            //System.Console.ReadKey();


            //Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 10000);
            //t.Start();
            //t.Wait();
            //System.Console.WriteLine(t.Result);
            //System.Console.ReadKey();

            //Thread t = new Thread(WriteY);          // Kick off a new thread
            //t.Start();                               // running WriteY()

            //// Simultaneously, do something on the main thread.
            //for (int i = 0; i < 10000; i++) System.Console.Write("x");
            //System.Console.ReadKey();


            //Thread.CurrentThread.Name = "main";
            //Thread worker = new Thread(Go);
            //worker.Name = "worker";
            //worker.Start();
            //Go();
            //System.Console.ReadKey();


            //ThreadPool.QueueUserWorkItem(Go);
            //ThreadPool.QueueUserWorkItem(Go, 123);
            //System.Console.ReadLine();

            ////Asynchronous delegates
            //Func<string, int> method = Work;
            //method.BeginInvoke("test", Done, method);
            //method.BeginInvoke("Hlsdjksdj", Done, method);
            //System.Console.ReadKey();

            //for (int i = 0; i < 300; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(Calculate);
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}

            {
                /*
                 * 
                //FileStream：写入大文件的最佳缓冲区大小，默认缓冲区大小为4 KiB。

                Stopwatch sw = new Stopwatch();
                Random rand = new Random();  // seed a random number generator
                int numberOfBytes = 2 << 22; //8,192KB File
                byte nextByte;
                for (int i = 1; i <= 28; i++) //Limited loop to 28 to prevent out of memory
                {
                    sw.Start();
                    using (FileStream fs = new FileStream(
                        String.Format(@"D:\Text\TEST{0}.DAT", i),  // name of file
                        FileMode.Create,    // create or overwrite existing file
                        FileAccess.Write,   // write-only access
                        FileShare.None,     // no sharing
                        2 << i,             // block transfer of i=18 -> size = 256 KB
                        FileOptions.None))
                    {
                        for (int j = 0; j < numberOfBytes; j++)
                        {
                            nextByte = (byte)(rand.Next() % 256); // generate a random byte
                            fs.WriteByte(nextByte);               // write it
                        }
                    }
                    sw.Stop();
                    System.Console.WriteLine(String.Format("Buffer is 2 << {0} Elapsed: {1}，{2}", i, sw.Elapsed, (2 << i) / 1024));

                    sw.Reset();
                }


                // E盘最优为512K，D盘最优为 128K，1024K

                */
            }

            {
                //var id = 5;

                //using (var connection = new SqliteConnection("Data Source=hello.db"))
                //{
                //    connection.Open();

                //    var command = connection.CreateCommand();
                //    command.CommandText =
                //    @"
                //        SELECT name
                //        FROM user
                //        WHERE id = $id
                //    ";
                //    command.Parameters.AddWithValue("$id", id);

                //    using (var reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            var name = reader.GetString(0);

                //            System.Console.WriteLine($"Hello, {name}!");
                //        }
                //    }
                //}
            }

            {

            }








            Thread.Sleep(2000);
            System.Console.ReadKey();



        }




        public static void Calculate()
        {
            DateTime time = DateTime.Now;//得到当前时间
            Random ra = new Random();//随机数对象
            Thread.Sleep(ra.Next(10, 100));//随机休眠一段时间
            System.Console.WriteLine(time.Minute + ":" + time.Millisecond);
        }




        static int Work(string s) { return s.Length; }

        static void Done(IAsyncResult cookie)
        {
            var target = (Func<string, int>)cookie.AsyncState;
            int result = target.EndInvoke(cookie);
            System.Console.WriteLine("String length is: " + result);
        }

        static void Go(object data)   // data will be null with the first call.
        {
            System.Console.WriteLine("Hello from the thread pool! " + data);
        }

        //static void Go()
        //{
        //    System.Console.WriteLine("Hello from " + Thread.CurrentThread.Name);
        //}

        static void WriteY()
        {
            for (int i = 0; i < 10000; i++) System.Console.Write("y");
        }


        public static void ThreadProc()
        {
            for (int i = 0; i < 20; i++)
            {
                System.Console.WriteLine("ThreadPorc:{0}", i);
                Thread.Sleep(1000);//将当前进程阻塞指定的毫秒数
            }
        }

        public static void TestMethod()
        {
            System.Console.WriteLine("不带参数的线程函数");
        }

        public static void TestMethod(object data)
        {
            string datastr = data as string;
            System.Console.WriteLine("带参数的线程函数，参数为：{0}", datastr);
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; --n)
                checked { sum += n; } //结果太大，抛出异常
            return sum;
        }




    }
}
