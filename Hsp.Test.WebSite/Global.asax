<%@ Application Language="C#" %>
<%@ Import Namespace="Quartz" %>
<%@ Import Namespace="Quartz.Impl" %>

<script runat="server">

    ////调度器
    //IScheduler scheduler;
    ////调度器工厂
    //ISchedulerFactory factory;

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码

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
        
        
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码
        //if (scheduler != null)
        //{
        //    scheduler.Shutdown(true);
        //}

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

        AnyShare.SessionLogin(); // AnyShare Session 用户登录
    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }
       
</script>
