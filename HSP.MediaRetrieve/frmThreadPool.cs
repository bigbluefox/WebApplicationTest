using System;
using System.Threading;
using System.Windows.Forms;
using Amib.Threading;

namespace HSP.MediaRetrieve
{
    public partial class frmThreadPool : Form
    {
        public frmThreadPool()
        {
            InitializeComponent();

            //WaitCallback w = new WaitCallback(delegate (Object obj) {});
            ////下面启动四个线程,计算四个直径下的圆周长
            //ThreadPool.QueueUserWorkItem(w, 1.0);
            //ThreadPool.QueueUserWorkItem(w, 2.0);
            //ThreadPool.QueueUserWorkItem(w, 3.0);
            //ThreadPool.QueueUserWorkItem(w, 4.0);


            // 创建一个线程池
            var smartThreadPool = new SmartThreadPool();

            // 执行任务
            smartThreadPool.QueueWorkItem(() =>
            {
                //Console.WriteLine("Hello World!");
                rtbThreadPoolMsg.Text += "Hello World!" + Environment.NewLine;
            });


            //带返回值的任务：

            // 创建一个线程池
            smartThreadPool = new SmartThreadPool();

            // 执行任务
            var result = smartThreadPool.QueueWorkItem(() =>
            {
                var sum = 0;
                for (var i = 0; i < 10; i++)
                    sum += i;

                return sum;
            });

            // 输出计算结果
            //Console.WriteLine(result.Result);

            rtbThreadPoolMsg.Text += result.Result + Environment.NewLine;


            //等待多个任务执行完成：
            smartThreadPool = new SmartThreadPool();

            // 执行任务
            var result1 = smartThreadPool.QueueWorkItem(() =>
            {
                //模拟计算较长时间
                Thread.Sleep(5000);

                return 3;
            });

            var result2 = smartThreadPool.QueueWorkItem(() =>
            {
                //模拟计算较长时间
                Thread.Sleep(3000);

                return 5;
            });

            var success = SmartThreadPool.WaitAll(new IWaitableResult[] {result1, result2});

            if (success)
            {
                // 输出结果
                //Console.WriteLine(result1.Result);
                //Console.WriteLine(result2.Result);

                rtbThreadPoolMsg.Text += result1.Result + Environment.NewLine;
                rtbThreadPoolMsg.Text += result2.Result + Environment.NewLine;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Calculate(double diameter)
        {
            //return diameter * Math.PI;
        }
    }
}

//https://www.codeproject.com/Articles/7933/Smart-Thread-Pool
//开源线程池组件SmartThreadPool 

//1、为什么需要使用线程池(Thread Pool）
//减少线程间上下文切换。线程执行一定的时间片后，系统会自动把cpu切换给另一个线程使用，这时还需要保存当前的线程上下文状态，并加载新线程的上下文状态。当程序中有大量的线程时，每个线程分得的时间片会越来越少，可能会出现线程未处理多少操作，就需要切换到另一线程，这样频繁的线程间上下文切换会花费大量的cpu时间。
//减少内存占用。系统每创建一条物理线程，需要大概花费1MB的内存空间，许多程序喜欢先创建多条物理线程，并周期轮询来处理各自的任务，这样既消耗了线程上下文切换的时间，还浪费了内存。这些任务可能只需要一条线程就能满足要求。假如某一任务需要执行较长的周期，线程池还可以自动增加线程，并在空闲时，销毁线程，释放占用的内存。
//2、为什么不使用.Net默认的线程池
//.Net默认的线程池(ThreadPool)是一个静态类，所以是没办法自己创建一个新的程序池的。默认的线程池与应用程序域(AppDomain)挂钩，一个AppDomain只有一个线程池。假如在线程池中执行了一个周期较长的任务，一直占用着其中一个线程，可能就会影响到应用程序域中的其他程序的性能。例如，假如在Asp.Net的线程池中执行一个周期较长的任务，就会影响请求的并发处理能力（线程池默认有个最大线程数）。 3、SmartThreadPool特性和优点
//SmartThreadPool特性如下：
//池中的线程数量会根据负载自动增减
//任务异步执行后可以返回值
//处于任务队列中未执行的任务可以取消
//回调函数可以等待多个任务都执行完成后再触发
//任务可以有优先级(priority)
//任务可以分组
//支持泛型Action<T> 和 Func<T>
//有性能监测机制
//4、使用示例 最简单的使用方法：

//5、结论 使用SmartThreadPool可以简单就实现支持多线程的程序，由线程池来管理线程，可以减少死锁的出现。
//SmartThreadPool还支持简单的生产者-消费者模式，当不需要对任务进行持久化时，还是很好用的。 
//6、扩展阅读 http://www.codeproject.com/KB/threads/smartthreadpool.aspx 
//http://smartthreadpool.codeplex.com/ 
//http://www.albahari.com/threading/