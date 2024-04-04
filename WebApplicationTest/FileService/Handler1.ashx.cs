using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.FileService
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            foreach (string fileDate in context.Request.Files.AllKeys)
            {
                HttpPostedFile f = context.Request.Files[fileDate];
                if (f != null && f.FileName != "")
                {
                    string title = context.Request.Form["title"];
                    f.SaveAs(HttpContext.Current.Server.MapPath("\\Uploads\\" + f.FileName));
                }
            }
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