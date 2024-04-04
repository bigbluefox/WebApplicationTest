using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

/// <summary>
/// AnyShare 的摘要说明
/// </summary>
public class AnyShare
{
    /// <summary>
    ///     爱数服务地址
    /// </summary>
    public static string AnyShareServer = (ConfigurationManager.AppSettings["AnyShareServer"] ?? "").Trim();

	public AnyShare()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region AnyShare Session 用户登录

    /// <summary>
    /// AnyShare Session 用户登录
    /// </summary>
    /// <returns></returns>
    public static void SessionLogin()
    {
        // 往OnlineUser表增加一条记录            
        //context.Application.Lock();

        //Hashtable list = context.Application.Get("GLOBAL_USER_LIST") as Hashtable ?? new Hashtable();
        //if (!list.ContainsKey(user.UserCode))
        //{
        //    list.Add(user.UserCode, context.Request.UserHostAddress);
        //}
        //context.Application.Add("GLOBAL_USER_LIST", list);
        //context.Application.UnLock();


        // AnyShare用户登录会话
        if (LoginUser != null) return;

        var urlPre = string.Format("http://{0}", AnyShareServer);
        var account = (ConfigurationManager.AppSettings["AnyShareAccount"] ?? "").Trim();
        var password = (ConfigurationManager.AppSettings["AnySharePassword"] ?? "").Trim();

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.Login(urlPre, account, password); // 爱数账号登录

        if (rst == null) return;

        var rstJson = JObject.Parse(rst);

        bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            //var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            //var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            //var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            //lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

            //rst = "{\"success\":false, \"Message\":\"" + causemsg + "\"}";

            LoginUser = null;
        }
        else
        {
            var tokenId = rstJson.GetValue("tokenid").ToString();
            var userId = rstJson.GetValue("userid").ToString();
            var expires = rstJson.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒

            AnyShareAuth user = new AnyShareAuth();
            user.UserId = userId;
            user.TokenId = tokenId;
            user.Expires = int.Parse(expires);

            LoginUser = user;

            HttpContext.Current.Session.Timeout = int.Parse(expires)/60; // Session过期时间，单位分钟
        }
    }

    #endregion

    #region AnyShare Access Token无效或已过期 错误处理

    /// <summary>
    /// AnyShare Access Token无效或已过期 错误处理
    /// </summary>
    /// <param name="errcode"></param>
    public static void AnyShareErrorProcess(string errcode)
    {
        if (errcode == "401001")
        {
            SessionLogin();
        }
    }

    #endregion

    #region AnyShare登录的用户对象

    /// <summary>
    /// AnyShare登录的用户对象
    /// </summary>
    public static AnyShareAuth LoginUser
    {
        get
        {
            if (HttpContext.Current.Session["SESSION_ANYSHARE_USER"] != null)
            {
                return HttpContext.Current.Session["SESSION_ANYSHARE_USER"] as AnyShareAuth;
            }
            return null;
        }

        set { HttpContext.Current.Session["SESSION_ANYSHARE_USER"] = value; }
    }

    #endregion

}