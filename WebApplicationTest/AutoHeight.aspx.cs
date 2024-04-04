using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using Hsp.Test.Common;

namespace WebApplicationTest
{
    public partial class AutoHeight : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var now = DateTime.Now;

            //var d = now.Day;
            //var dd = (int) now.DayOfWeek;


            //var date = DateTime.Parse("2020-9-1");
            //dd = (int)date.DayOfWeek;

            //Response.Write(dd);


            //var date = DateTime.Parse("2020-9");

            //var start = date;
            //var end = date.AddMonths(1);

            //var yearMonth = "99999-14";

            //DateTime date;
            //DateTime.TryParse(yearMonth, out date);

            //var dd = date;

            //if (!date.is.IsValidInTimezone(TimeZoneInfo.Local))
            //{
            //    // the time is not valid
            //}


            string type = Request.QueryString["type"];

            var abc = type;

            return;


            var dateString = "2020-08-20 05:56";
            DateTime date = DateTime.Parse(dateString);
            var h = date.Hour;
            var m = date.Minute;

            var hm = date.ToString("HH:mm");

            return;
            






            Response.Write("<ul>");

            for (int i = 0; i < Request.Params.Count; i++)
                Response.Write("<li>" + Request.Params.Keys[i].ToString() + " = " + Request.Params[i].ToString());

            Response.Write("<hr>");
            for (int i = 0; i < Request.Form.Count; i++)
                Response.Write("<li>" + Request.Form.Keys[i].ToString() + " = " + Request.Form[i].ToString());

            Response.Write("<hr>");
            for (int i = 0; i < Request.QueryString.Count; i++)
                Response.Write("<li>" + Request.QueryString.Keys[i].ToString() + " = " + Request.QueryString[i].ToString());

            Response.Write("<hr>");
            for (int i = 0; i < Request.Cookies.Count; i++)
                Response.Write("<li>" + Request.Cookies.Keys[i].ToString() + " = " + Request.Cookies[i].ToString());

            Response.Write("</ul>");

            //var aaa = "000195";

            //var vb = aaa.TrimStart('0');

            //var c = vb;

            //Image albumArt;

            //var strDate = "1970-1-1";

            //var dt = DateTime.Parse(strDate);

            //var rnd = new Random().Next();

            //Label1.Text = dt.ToString("yyyy-MM-dd hh:mm:ss");

            //Label1.Text = rnd.ToString();

            //var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //Label1.Text = version;

            ////var allowDownload =   bool.Parse(ConfigurationManager.AppSettings["AllowStandardFileDownload"] ?? "true");
            ////Label1.Text = allowDownload.ToString();

            //var path = @"D:\MSO\SVN\Processist.MSO-ZH\Processist.MSO.Website\FilePath\StandardSystem\基础标准体系\001  标准化工作导则\标准化工作导则  第1部分：标准的结构和编写.pdf";
            //FileHelper.FilePathCheck(path);

            //var exist = false;
            //try
            //{
            //    exist = System.IO.File.Exists(path);
            //    var p = exist;
            //}
            //catch (Exception ex)
            //{
            //    exist = false;
            //    throw;
            //}

            //var dd = exist;

            //int i = 2;
            //DateTime dt = DateTime.Now;

            //var type = dt.GetType();

            //var t = GetTypeName(typeof (DateTime?));

            //var tt = GetTypeName(typeof (DateTime));

            //string value = dt.GetType().FullName;


            //var webRootPath = HttpContext.Current.Server.MapPath("/");

            //var path = webRootPath;

            //var alertString = "13:25";
            //DateTime alertTime = Convert.ToDateTime(alertString);

            //var nowTime = DateTime.Now;

            //var a = false;
            //if (alertTime > nowTime)
            //{
            //    a = true;
            //}
            //else
            //{
            //    a = false;
            //}

            //var b = a;


//<a href="Files/X-办安环〔2018〕305号%20关于印发《班组安全建设指导意见》的通知.pdf">Files/X-办安环〔2018〕305号 关于印发《班组安全建设指导意见》的通知.pdf</a>

            var url = "/Files/X-办安环〔2018〕305号%20关于印发《班组安全建设指导意见》的通知.pdf";

            Response.Write(System.Web.HttpUtility.UrlEncode(url));

            //var p = url.Replace()

            //var c = ConvertUtil.MillimeterToPoint()




            StringBuilder sb = new StringBuilder();

            sb.Append("");

            sb.AppendLine("");

            var ss = sb.ToString();

            //GetIP();

        }


        private static string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://www.ip138.com/ips138.asp");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("您的IP地址是：[") + 9;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return tempip;
        }

        public static string GetTypeName(Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);

            bool isNullableType = nullableType != null;

            if (isNullableType)
                return nullableType.Name;
            else
                return type.Name;
        }
    }
}