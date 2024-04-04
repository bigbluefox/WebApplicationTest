using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

/// <summary>
/// TimeJob 的摘要说明
/// </summary>
public class TimeJob : IJob
{
	public TimeJob()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public void Execute(IJobExecutionContext context)
    {
        //向c:\Quartz.txt写入当前时间并换行
        System.IO.File.AppendAllText(@"c:\Quartz.txt", DateTime.Now + Environment.NewLine);
    }

}