using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace WebApplicationTest.AppCodes
{
    public class TimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //向c:\Quartz.txt写入当前时间并换行
            System.IO.File.AppendAllText(@"c:\Quartz.txt", DateTime.Now + Environment.NewLine);
        }



    }
}