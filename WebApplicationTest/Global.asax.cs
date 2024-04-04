using System;
using System.IO;
using System.Timers;
using System.Web;
using Quartz;
using Quartz.Impl;
using WebApplicationTest.App_Code;

namespace WebApplicationTest
{
    public class Global : HttpApplication
    {
        ////调度器
        //IScheduler scheduler;
        ////调度器工厂
        //ISchedulerFactory factory;

        protected void Application_Start(object sender, EventArgs e)
        {
            //配置log4
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));


            

            // 在应用程序启动时运行的代码  

            //var myTimer = new Timer(10000);
            //myTimer.Elapsed += OnTimedEvent;
            //myTimer.Interval = 10000;
            //myTimer.Enabled = true;


            ////1、创建一个调度器
            //factory = new StdSchedulerFactory();
            //scheduler = factory.GetScheduler();
            //scheduler.Start();

            ////2、创建一个任务
            //IJobDetail job = JobBuilder.Create<TimeJob>().WithIdentity("job1", "group1").Build();

            ////3、创建一个触发器
            ////DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("trigger1", "group1")
            //    .WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
            //    //.StartAt(runTime)
            //    .Build();

            ////4、将任务与触发器添加到调度器中
            //scheduler.ScheduleJob(job, trigger);

            ////5、开始执行
            //scheduler.Start();


            AppTest.SayHello();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码  
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码  
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。  
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为  
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer  
            // 或 SQLServer，则不会引发该事件。
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码  
            //if (scheduler != null)
            //{
            //    scheduler.Shutdown(true);
            //}
        }


        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //间隔时间执行某动作，指定日志文件的目录  

            var fileLogPath = AppDomain.CurrentDomain.BaseDirectory + "SystemLog\\";
            var fileLogName = "SoftPrj_CN_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_log.txt";

            //定义文件信息对象  
            var finfo = new FileInfo(fileLogPath + fileLogName);

            //创建只写文件流  
            using (var fs = finfo.OpenWrite())
            {
                //根据上面创建的文件流创建写数据流  
                var strwriter = new StreamWriter(fs);

                //设置写数据流的起始位置为文件流的末尾  
                strwriter.BaseStream.Seek(0, SeekOrigin.End);

                //写入错误发生时间  
                strwriter.WriteLine("发生时间: " + DateTime.Now);

                //写入日志内容并换行  
                //strwriter.WriteLine("错误内容: " + message);  

                strwriter.WriteLine("错误内容: ");

                //写入间隔符  
                strwriter.WriteLine("---------------------------------------------");

                strwriter.WriteLine();

                //清空缓冲区内容，并把缓冲区内容写入基础流  
                strwriter.Flush();

                //关闭写数据流  
                strwriter.Close();

                fs.Close();
            }
        }
    }
}