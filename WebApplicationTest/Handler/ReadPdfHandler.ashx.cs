using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// ReadPdfHandler 的摘要说明
    /// </summary>
    public class ReadPdfHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            var pdf = @"\PDF\PdfShow\QSDYY 201.001-2018 上海上电电力运营有限公司规划管理标准.pdf";
            var filePath = HttpContext.Current.Server.MapPath(pdf);

            FileHelper.DownLoadold(filePath, "QSDYY 201.001-2018 上海上电电力运营有限公司规划管理标准", "pdf", HttpContext.Current.Request.Browser.Browser);



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}