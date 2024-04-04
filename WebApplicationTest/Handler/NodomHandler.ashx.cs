using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// NodomHandler 的摘要说明
    /// </summary>
    public class NodomHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var strMsg = context.Request.Params["MSG"] ?? context.Request.Form["MSG"];
            context.Response.Write(strMsg + "，Hello World");
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