using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Hsp.Test.Common
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
