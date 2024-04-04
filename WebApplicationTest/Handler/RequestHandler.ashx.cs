using System.Text;
using System.Web;

namespace WebApplicationTest.Handler
{
    /// <summary>
    ///     RequestHandler 的摘要说明
    /// </summary>
    public class RequestHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var sb = new StringBuilder();
            var req = context.Request;

            sb.AppendLine("Params（最为通用）：<br />");

            for (var i = 0; i < req.Params.Count; i++)
                sb.AppendLine(req.Params.Keys[i] + " = " + req.Params[i] + "<br />");

            sb.AppendLine("<br /><br />");
            sb.AppendLine("Form：<br />");

            for (var i = 0; i < req.Form.Count; i++)
                sb.AppendLine(req.Form.Keys[i] + " = " + req.Form[i] + "<br />");

            sb.AppendLine("<br /><br />");
            sb.AppendLine("QueryString：<br />");

            for (var i = 0; i < req.QueryString.Count; i++)
                sb.AppendLine(req.QueryString.Keys[i] + " = " + req.QueryString[i] + "<br />");

            sb.AppendLine("<br /><br />");
            sb.AppendLine("Cookies：<br />");

            for (var i = 0; i < req.Cookies.Count; i++)
            {
                var cookie = req.Cookies[i];
                if (cookie != null)
                    sb.AppendLine(req.Cookies.Keys[i] + " = " + cookie + "<br />");
            }

            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}