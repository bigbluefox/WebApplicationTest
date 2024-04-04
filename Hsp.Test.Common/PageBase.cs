using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Hsp.Test.Common
{
    /// <summary>
    ///     页面基础类
    /// </summary>
    public class PageBase : Page
    {
        #region 公共属性

        /// <summary>
        ///     程序相对路径
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 栏目路径
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        public string PagerString { get; set; }

        #endregion

        #region 获取客户端IP

        /// <summary>
        ///     获取客户端IP
        /// </summary>
        /// <value>The IP.</value>
        public string IP
        {
            get
            {
                return Request.ServerVariables["REMOTE_ADDR"] != ""
                    ? Request.ServerVariables["REMOTE_ADDR"]
                    : Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }

        #endregion

        #region 当前客户端的IP地址

        /// <summary>
        ///     当前客户端的IP地址(该属性只读 get)
        /// </summary>
        public static string LocalClientIP { get; set; }

        #endregion

        #region 当前登录的用户对象

        /// <summary>
        ///     当前登录的用户对象
        /// </summary>
        //public AuthUser CurrentUser
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Session["SESSION_CURRENT_USER"] as AuthUser;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        //    set { Session["SESSION_CURRENT_USER"] = value; }
        //}

        #endregion

        #region 获取客户端IP地址

        /// <summary>
        ///     获取客户端IP地址
        /// </summary>
        /// <returns>IP</returns>
        public static string GetClientIp()
        {
            var strResult = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(strResult))
            {
                strResult = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(strResult))
            {
                strResult = HttpContext.Current.Request.UserHostAddress;
            }
            LocalClientIP = strResult;
            return strResult;
        }

        #endregion

        #region 清除HTML标签

        /// <summary>
        ///     清除HTML标签
        /// </summary>
        /// <param name="strHtml">The HTML.</param>
        /// <returns></returns>
        public static string StripHtml(string strHtml)
        {
            var strOutput = strHtml;

            var scriptRegExp = new Regex("<scr" + "ipt[^>.]*>[\\s\\S]*?</sc" + "ript>",
                RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline &
                RegexOptions.ExplicitCapture);
            strOutput = scriptRegExp.Replace(strOutput, "");

            var styleRegex = new Regex("<style[^>.]*>[\\s\\S]*?</style>",
                RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline &
                RegexOptions.ExplicitCapture);
            strOutput = styleRegex.Replace(strOutput, "");

            var objRegExp = new Regex("<(.|\\n)+?>",
                RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline);
            strOutput = objRegExp.Replace(strOutput, "");

            objRegExp = new Regex("<[^>]+>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline);
            strOutput = objRegExp.Replace(strOutput, "");

            strOutput = strOutput.Replace("&lt;", "<");
            strOutput = strOutput.Replace("&gt;", ">");
            strOutput = strOutput.Replace("&nbsp;", " ");

            return strOutput;
        }

        #endregion

        #region 替换禁用词汇

        /// <summary>
        ///     替换禁用词汇
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="BadWords">The bad words.</param>
        /// <param name="FixWord">The fix word.</param>
        /// <returns></returns>
        public static string StripBadWord(string Content, string BadWords, string FixWord)
        {
            var sqlExp = new Regex(string.Format("({0})", BadWords));
            return sqlExp.Replace(Content, FixWord);
        }

        #endregion

        #region 过滤禁用词汇

        /// <summary>
        ///     过滤禁用词汇
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="DenyWords">The deny words.</param>
        /// <returns></returns>
        public static string StripDenyWord(string Content, string DenyWords)
        {
            var sqlExp = new Regex(string.Format(".*({0}).*", DenyWords));
            return sqlExp.Replace(Content, "");
        }

        #endregion

        #region  防SQL注入

        /// <summary>
        ///     防SQL注入
        /// </summary>
        /// <param name="inStr">The in STR.</param>
        /// <returns></returns>
        //public static string MASK(string inStr)
        //{
        //    var sqlExp =
        //        new Regex(
        //            @"(and |exec |insert |select |delete |update | count|drop |table|%| chr| mid| master|truncate | char|declare |'|--|xp_)");
        //    return sqlExp.Replace(inStr, "");
        //}

        #endregion

        #region GetMd5Value

        public string GetMd5Value(string inString)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(inString))).Replace("-", "");
        }

        #endregion

        #region MD5加密

        /// <summary>
        ///     MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMd5String(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //value += "Processist9%";
            var data = Encoding.Default.GetBytes(value);
            var result = md5.ComputeHash(data);
            var ret = "";
            for (var i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        #endregion

        #region GUIDs

        /// <summary>
        ///     GUIDs this instance.
        /// </summary>
        /// <returns></returns>
        public static string GUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        #endregion

        #region Nested type: HashTable

        public abstract class TestHashTable
        {
            private static Hashtable _instance; //<-------首先，我们来定义一个静态的，本类的对象instancec

            public static Hashtable Instance() //<----好啦，我们来定义一个方法返回的是本类对像的实例。
            {
                if (_instance == null) //<-----判断实例在内存中是否存在
                {
                    _instance = new Hashtable(); //<-----如果不存在，我们构造一个新的实例。
                }
                return _instance; //<------返回本类的实例。
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        ///     构造函数
        /// </summary>
        public PageBase()
        {
            Load += PageBase_Load;
        }

        /// <summary>
        ///     Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PageBase_Load(object sender, EventArgs e)
        {
            PageId = HttpContext.Current.Request.Path; // 相对路径

            //if (CurrentUser == null)
            //{
            //    Response.Redirect("/LogOut.aspx", true);
            //}
        }

        #endregion

        #region 用户缓存数据

        /// <summary>
        ///     清除会话
        /// </summary>
        public void ClearSession()
        {
            Context.Session.Clear(); //清除会话状态中的所有值   
            Context.Session.Abandon(); //取消当前会话
        }

        /// <summary>
        ///     创建用户Session
        /// </summary>
        //public void BuildLoginUserSession(AuthUser user)
        //{
        //    Session["SESSION_CURRENT_USER"] = user;
        //}

        /// <summary>
        ///     获取用户Session
        /// </summary>
        //public AuthUser GetLoginUserSession()
        //{
        //    AuthUser user;
        //    if (Session["SESSION_CURRENT_USER"] != null)
        //    {
        //        user = (AuthUser)Session["SESSION_CURRENT_USER"];
        //    }
        //    else
        //    {
        //        user = null;
        //    }
        //    return user;
        //}

        #endregion

        #region 获取分页控件数据

        /// <summary>
        /// 获取分页控件数据
        /// </summary>
        /// <param name="total">记录总数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public string GetPagerString(int total, int pageIndex, int pageSize)
        {
            #region 分页参数

            //var recordCount = total; // 记录总数
            var pageCount = int.Parse(Math.Ceiling((total * 1.0) / (pageSize * 1.0)).ToString("####"));//total/pageSize + 1; // 页码数量，向上取整
            if (pageCount == 1) return ""; // 当页码数量为1时不显示页码条

            var barSize = 10; // 分页条包含页码数量
            var barCount = int.Parse(Math.Ceiling((pageCount * 1.0) / (barSize * 1.0)).ToString("####"));// pageCount/barSize + 1; // 分页条数量，向上取整
            var barIndex = int.Parse(Math.Ceiling((pageIndex * 1.0) / (barSize * 1.0)).ToString("####")); //pageIndex / barSize + 1; // 当前分页条，向上取整

            #endregion

            #region 属性参数

            var btnSize = " btn-lg"; // 按钮大小：-lg(大)，空值(默认)，-sm(小)，-xs(超小)，
            var preDisabled = barIndex == 1 ? " disabled" : ""; // 前页区禁用
            var lastDisabled = barIndex == barCount ? " disabled" : ""; // 后页区禁用
            var url = Request.RawUrl.Trim();

            #region 页面Url处理

            if (url.IndexOf("page=", StringComparison.Ordinal) == -1)
            {
                url += (url.IndexOf("?", StringComparison.Ordinal) == -1) ? "?page=" : "&page=";
            }
            else
            {
                url = url.Substring(0, url.LastIndexOf('=') + 1);
            }

            #endregion

            btnSize = "";

            #endregion

            #region 前页区

            var previousPage = (barIndex - 2) * barSize + 1;

            var sb = new StringBuilder();
            sb.Append("<div class=\"btn-toolbar\" role=\"toolbar\" aria-label=\"Toolbar with button groups\">");
            if (barIndex > 1)
            {
                sb.Append("	<div class=\"btn-group\" role=\"group\" aria-label=\"First group\">");
                sb.AppendFormat(
                    "		<button type=\"button\" class=\"btn btn-default{2}{3}\" aria-label=\"Left Align\" title=\"首页\" onclick=\"javascript:Page('{0}{1}');\">",
                    url, 1, btnSize, preDisabled);
                sb.Append("			<span class=\"glyphicon glyphicon-step-backward\" aria-hidden=\"true\"></span>");
                sb.Append("		</button>");
                sb.AppendFormat(
                    "		<button type=\"button\" class=\"btn btn-default{2}{3}\" aria-label=\"Left Align\" title=\"上一页\" onclick=\"javascript:Page('{0}{1}');\">",
                    url, previousPage, btnSize, preDisabled);
                sb.Append("			<span class=\"glyphicon glyphicon-triangle-left\" aria-hidden=\"true\"></span>");
                sb.Append("		</button>");
                sb.Append("	</div>");
            }

            #endregion

            #region 分页区

            var barStart = (barIndex - 1) * barSize + 1; // 分页条起始页码
            var barEnd = barIndex == barCount ? pageCount + 1 : barIndex * barSize + 1; // 分页条结束页码

            sb.Append("	<div class=\"btn-group\" role=\"group\" aria-label=\"Second group\">");
            for (int i = barStart; i < barEnd; i++)
            {
                var active = i == pageIndex ? " active" : ""; // 当前页码按钮
                var disabled = i > pageCount ? " disabled" : ""; // 分页区禁用，当页码大于最大页码时候启用(即页码条严格显示十个页码)
                sb.AppendFormat(
                    "		<button type=\"button\" class=\"btn btn-default{2}{3}\" onclick=\"javascript:Page('{0}{1}');\">{1}</button>",
                    url, i, btnSize, active, disabled);
            }
            sb.Append("	</div>");

            #endregion

            #region 后页区

            if (barIndex != barCount)
            {
                var nextPage = barIndex * barSize + 1;

                sb.Append("	<div class=\"btn-group\" role=\"group\" aria-label=\"Last group\">");
                sb.AppendFormat(
                    "		<button type=\"button\" class=\"btn btn-default{2}{3}\" aria-label=\"Left Align\" title=\"下一页\" onclick=\"javascript:Page('{0}{1}');\">",
                    url, nextPage, btnSize, lastDisabled);
                sb.Append("			<span class=\"glyphicon glyphicon-triangle-right\" aria-hidden=\"true\"></span>");
                sb.Append("		</button>");
                sb.AppendFormat(
                    "		<button type=\"button\" class=\"btn btn-default{2}{3}\" aria-label=\"Left Align\" title=\"尾页\" onclick=\"javascript:Page('{0}{1}');\">",
                    url, pageCount, btnSize, lastDisabled);
                sb.Append("			<span class=\"glyphicon glyphicon-step-forward\" aria-hidden=\"true\"></span>");
                sb.Append("		</button>");
                sb.Append("	</div>");
            }

            var totalString = pageIndex > 1000 ? "" : " (" + total + ")"; // 当页码超过4位数，则不显示总数
            sb.AppendFormat("	<div style=\"float: right; padding-top: 5px; color: #337ab7;\">{0}/{1}{2}</div>", pageIndex, pageCount, totalString);
            sb.Append("</div>");

            #endregion

            return sb.ToString();
        }

        #endregion
    }
}