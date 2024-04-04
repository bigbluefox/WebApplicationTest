using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class Browser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder strLabel = new System.Text.StringBuilder();
            HttpBrowserCapabilities bc = Request.Browser;
            strLabel.Append("您的浏览器的分辨率为：");
            strLabel.Append(bc.ScreenPixelsWidth + "X" + bc.ScreenPixelsHeight);
            strLabel.Append(" * ");
            strLabel.Append(bc.ScreenCharactersWidth + "X" + bc.ScreenCharactersHeight);
            strLabel.Append("<br />");
            strLabel.Append("浏览器基本信息：");
            strLabel.Append("Type = " + bc.Type + "<br />");
            strLabel.Append("Name = " + bc.Browser + "<br />");
            strLabel.Append("Version = " + bc.Version + "<br />");
            strLabel.Append("Major Version = " + bc.MajorVersion + "<br />");
            strLabel.Append("Minor Version = " + bc.MinorVersion + "<br />");
            strLabel.Append("Platform = " + bc.Platform + "<br />");
            strLabel.Append("Is Beta = " + bc.Beta + "<br />");
            strLabel.Append("Is Crawler = " + bc.Crawler + "<br />");
            strLabel.Append("Is AOL = " + bc.AOL + "<br />");
            strLabel.Append("Is Win16 = " + bc.Win16 + "<br />");
            strLabel.Append("Is Win32 = " + bc.Win32 + "<br />");
            strLabel.Append("支持 Frames = " + bc.Frames + "<br />");
            strLabel.Append("支持 Tables = " + bc.Tables + "<br />");
            strLabel.Append("支持 Cookies = " + bc.Cookies + "<br />");
            strLabel.Append("支持 VB Script = " + bc.VBScript + "<br />");
            strLabel.Append("支持 JavaScript = " + bc.JavaScript + "<br />");
            strLabel.Append("支持 Java Applets = " + bc.JavaApplets + "<br />");
            strLabel.Append("支持 ActiveX Controls = " + bc.ActiveXControls + "<br />");
            strLabel.Append("CDF = " + bc.CDF + "<br />");
            strLabel.Append("W3CDomVersion = " + bc.W3CDomVersion.ToString() + "<br />");
            strLabel.Append("UserAgent = " + Request.UserAgent + "<br />");
            strLabel.Append("UserLanguages = " + Request.UserLanguages[0].ToString() + "<br />");
            strLabel.Append("<br />");
            strLabel.Append("客户端计算机基本配置：");
            strLabel.Append("UserHostName = " + Request.UserHostName + "<br />");
            strLabel.Append("UserHostAddress = " + Request.UserHostAddress + "<br />");
            strLabel.Append("PDF 6.0 插件是否安装 = " + Request.Form["PDF"] + "<br />");

            strLabel.Append("原始URL = " + Request.RawUrl + "<br />");
            strLabel.Append("当前请求的URL = " + Request.Url + "<br />");


            var ipv4 = GetLocalIpv4().Aggregate("", (current, ip) => current + ip.ToString() + ",");
            strLabel.Append("IPV4 = " + GetRemoteIP() + "<br />");

            Label1.Text = strLabel.ToString();

            var lbl = strLabel.ToString();
        }

        string GetRemoteIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "0.0.0.0";
            }

            return result;
        }


        string[] GetLocalIpv4()
        {
            //事先不知道ip的个数，数组长度未知，因此用StringCollection储存  
            var localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection ipCollection = new StringCollection();
            foreach (IPAddress ip in localIPs)
            {
                //根据AddressFamily判断是否为ipv4,如果是InterNetWorkV6则为ipv6  
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipCollection.Add(ip.ToString());
            }
            string[] ipArray = new string[ipCollection.Count];
            ipCollection.CopyTo(ipArray, 0);
            return ipArray;
        }
    }
}