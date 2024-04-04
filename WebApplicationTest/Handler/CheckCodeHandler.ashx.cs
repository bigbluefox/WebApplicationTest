using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// CheckCodeHandler 的摘要说明
    /// </summary>
    public class CheckCodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string checkCode;
            MemoryStream ms = CheckCodeMethod.CreateCheckCodeImageMemoryStream(out checkCode);

            //清除缓冲区流中的所有输出 
            context.Response.ClearContent();
            //输出流的Http Mime类型设置为"image/Png" 
            context.Response.ContentType = "image/Png";
            //输出图片的二进制流 
            context.Response.BinaryWrite(ms.ToArray());
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