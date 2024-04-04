using System;
using System.Web;
using System.Web.UI;

namespace WebApplicationTest
{
    public partial class ClientInfo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = string.Empty;

            var userAgent = Request.UserAgent ?? "无";
            if (IsPostBack) return;

            lbHoverTreeInfo.Items.Add("您的系统信息为：");
            lbHoverTreeInfo.Items.Add("客户端IP[Page.Request.UserHostAddress]：" + Page.Request.UserHostAddress);
            lbHoverTreeInfo.Items.Add("浏览器类型[Request.Browser.Browser]：" + Request.Browser.Browser);
            lbHoverTreeInfo.Items.Add("浏览器标识[Request.Browser.Id]：" + Request.Browser.Id);
            lbHoverTreeInfo.Items.Add("浏览器版本号[Request.Browser.Version]：" + Request.Browser.Version);
            lbHoverTreeInfo.Items.Add("浏览器是不是测试版本[Request.Browser.Beta]：" + Request.Browser.Beta);
            lbHoverTreeInfo.Items.Add("浏览器类型[Request.Browser.Type]：" + Request.Browser.Type);
            lbHoverTreeInfo.Items.Add("是否支持框架网页[Request.Browser.Frames]：" + Request.Browser.Frames);
            lbHoverTreeInfo.Items.Add("是否支持Cookie[Request.Browser.Cookies]：" + Request.Browser.Cookies);
            lbHoverTreeInfo.Items.Add("浏览器JScript版本[Request.Browser.JScriptVersion]：" + Request.Browser.JScriptVersion);
            //lbHoverTreeInfo.Items.Add("屏幕分辨率宽[System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width ]]：" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width);
            //lbHoverTreeInfo.Items.Add("屏幕分辨率高[System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height ]]：" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            lbHoverTreeInfo.Items.Add("浏览器类型[Request.Browser.Type]：" + Request.Browser.Type);
            lbHoverTreeInfo.Items.Add("客户端IP[GetHoverTreeIp()]：" + GetHoverTreeIp());
            lbHoverTreeInfo.Items.Add("客户端的操作系统[Request.Browser.Platform]：" + Request.Browser.Platform);
            lbHoverTreeInfo.Items.Add("客户端的操作系统[GetHoverTreeOSName(userAgent)]：" + GetHoverTreeOSName(userAgent));
            lbHoverTreeInfo.Items.Add("是不是win16系统[Request.Browser.Win16]：" + Request.Browser.Win16);
            lbHoverTreeInfo.Items.Add("是不是win32系统[Request.Browser.Win32]：" + Request.Browser.Win32);
            lbHoverTreeInfo.Items.Add("客户端.NET Framework版本：Request.Browser.ClrVersion]：" + Request.Browser.ClrVersion);
            lbHoverTreeInfo.Items.Add("是否支持Java[Request.Browser.JavaApplets]：" + Request.Browser.JavaApplets);

            if (Request.ServerVariables["HTTP_UA_CPU"] == null)
                lbHoverTreeInfo.Items.Add("CPU 类型[Request.ServerVariables[\"HTTP_UA_CPU\"]]:未知");
            else
                lbHoverTreeInfo.Items.Add("CPU 类型[Request.ServerVariables[\"HTTP_UA_CPU\"]]:" +
                                          Request.ServerVariables["HTTP_UA_CPU"]);

            lbHoverTreeInfo.Items.Add("UserAgent信息[Request.UserAgent]：" + userAgent);
            //lbHoverTreeInfo.Items.Add("By 何问起工具 http://tool.hovertree.com/info/client/");

            lbHoverTreeInfo.Visible = false;


            s += "您的系统信息为：<br />";
            s += "客户端IP[Page.Request.UserHostAddress]：" + Page.Request.UserHostAddress + "<br />";
            s += "浏览器类型[Request.Browser.Browser]：" + Request.Browser.Browser + "<br />";
            s += "浏览器标识[Request.Browser.Id]：" + Request.Browser.Id + "<br />";
            s += "浏览器版本号[Request.Browser.Version]：" + Request.Browser.Version + "<br />";
            s += "浏览器是不是测试版本[Request.Browser.Beta]：" + Request.Browser.Beta + "<br />";
            s += "浏览器类型[Request.Browser.Type]：" + Request.Browser.Type + "<br />";
            s += "是否支持框架网页[Request.Browser.Frames]：" + Request.Browser.Frames + "<br />";
            s += "是否支持Cookie[Request.Browser.Cookies]：" + Request.Browser.Cookies + "<br />";
            s += "浏览器JScript版本[Request.Browser.JScriptVersion]：" + Request.Browser.JScriptVersion + "<br />";
            //s+="屏幕分辨率宽[System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width ]]：" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width  + "<br />";
            //s+="屏幕分辨率高[System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height ]]：" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height  + "<br />";
            s += "浏览器类型[Request.Browser.Type]：" + Request.Browser.Type + "<br />";
            s += "客户端IP[GetHoverTreeIp()]：" + GetHoverTreeIp() + "<br />";
            s += "客户端的操作系统[Request.Browser.Platform]：" + Request.Browser.Platform + "<br />";
            s += "客户端的操作系统[GetHoverTreeOSName(userAgent)]：" + GetHoverTreeOSName(userAgent) + "<br />";
            s += "是不是win16系统[Request.Browser.Win16]：" + Request.Browser.Win16 + "<br />";
            s += "是不是win32系统[Request.Browser.Win32]：" + Request.Browser.Win32 + "<br />";
            s += "客户端.NET Framework版本：Request.Browser.ClrVersion]：" + Request.Browser.ClrVersion + "<br />";
            s += "是否支持Java[Request.Browser.JavaApplets]：" + Request.Browser.JavaApplets + "<br />";

            if (Request.ServerVariables["HTTP_UA_CPU"] == null)
                s += "CPU 类型[Request.ServerVariables[\"HTTP_UA_CPU\"]]:未知<br />";
            else
                s += "CPU 类型[Request.ServerVariables[\"HTTP_UA_CPU\"]]:" + Request.ServerVariables["HTTP_UA_CPU"] +
                     "<br />";

            s += "UserAgent信息[Request.UserAgent]：" + userAgent + "<br />";

            lblClientInfo.Text = s;
        }


        /// <summary>
        ///     获取真实IP
        /// </summary>
        /// <returns></returns>
        public string GetHoverTreeIp()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == string.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == string.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>
        ///     根据 User Agent 获取操作系统名称
        /// </summary>
        private string GetHoverTreeOSName(string userAgent)
        {
            var m_hvtOsVersion = "未知";
            if (userAgent.Contains("NT 6.4"))
            {
                m_hvtOsVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                m_hvtOsVersion = "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                m_hvtOsVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                m_hvtOsVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                m_hvtOsVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                m_hvtOsVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                m_hvtOsVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                m_hvtOsVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                m_hvtOsVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                m_hvtOsVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                m_hvtOsVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                m_hvtOsVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                m_hvtOsVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                m_hvtOsVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                m_hvtOsVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                m_hvtOsVersion = "SunOS";
            }
            return m_hvtOsVersion;
        }
    }
}