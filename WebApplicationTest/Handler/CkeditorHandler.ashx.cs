using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// CkeditorHandler 的摘要说明
    /// </summary>
    public class CkeditorHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            var strContent = context.Request.Params["content"] ?? "";

            var cc = strContent;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /*
        检测到有潜在危险的 Request.Form 值 

        这种问题是因为你提交的Form中有HTML字符串，例如你在TextBox中输入了html标签，或者在页面中使用了HtmlEditor组件等，解决办法是禁用validateRequest。
        如果你是.net 4.0或更高版本，一定要看方法3。
        此方法在asp.net webForm和MVC中均适用
        方法1：
        在.aspx文件头中加入这句：
        <%@ Page validateRequest="false"  %>
 
        方法2：
        修改web.config文件:
        <configuration>
            <system.web>
                <pages validateRequest="false" />
            </system.web>
        </configuration>

        因为validateRequest默认值为true。只要设为false即可。
  
        方法3：
        web.config里面加上
        <system.web>
            <httpRuntime requestValidationMode="2.0" />
        </system.web>

        因为4.0的验证在HTTP的BeginRequest前启用，因此，请求的验证适用于所有ASP.NET资源，aspx页面,ashx页面,Web服务和一些HTTP处理程序等.
        */

    }
}