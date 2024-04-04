<%@ WebHandler Language="C#" Class="AnyShareHandler" %>

using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

/// <summary>
/// AnyShare处理程序
/// </summary>
public class AnyShareHandler : IHttpHandler, IRequiresSessionState
{
    /// <summary>
    ///     爱数服务地址
    /// </summary>
    public string AnyShareServer = (ConfigurationManager.AppSettings["AnyShareServer"] ?? "").Trim();

    /// <summary>
    ///     爱数基础目录编号
    /// </summary>
    public string AnyShareRootDocId = (ConfigurationManager.AppSettings["AnyShareRootDocId"] ?? "").Trim();

    /// <summary>
    /// 爱数基础地址
    /// </summary>
    public string AnyShareUrlPre { get; set; }

    /// <summary>
    ///     爱数登录账号
    /// </summary>
    public string AnyShareUserId { get; set; }

    /// <summary>
    /// 爱数验证令牌
    /// </summary>
    public string AnyShareTokenId { get; set; }
    
    #region ProcessRequest

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();

        AnyShareUrlPre = string.Format("http://{0}", AnyShareServer);

        var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            // 检查服务器是否在线
            case "PING":
                Ping(context);
                break;

            // 登录
            case "LOGIN":
                Login(context);
                break;

            // 刷新身份凭证有效期
            case "REFRESHTOKEN":
                RefreshToken(context);
                break;

            // 登出
            case "LOGOUT":
                Logout(context);
                break;

            //// 单个删除视频
            //case "DELETE":
            //    DeleteVideo(context);
            //    break;

            //// 批量删除视频
            //case "BATCHDELETE":
            //    BatchDelete(context);
            //    break;

            //// 视频列表信息
            //case "PAGELIST":
            //    GetPageList(context);
            //    break;

            // JsonTest
            case "TEST":
                JsonTest(context);
                break;

            default:
                break;
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion

    #region JsonTest

    /// <summary>
    /// JsonTest
    /// </summary>
    /// <param name="context"></param>
    private void JsonTest(HttpContext context)
    {
        var rst = "";

        try
        {
            var ret =
                "{\"causemsg\":\"存在同类型的同名文件(gns:\\/\\/7F45B54D47A242FC837EB31BAD9940D1,宪法学)（错误提供者：EVFS，错误值：16777229，错误位置：\\/var\\/JFR\\/workspace\\/C_EVFS\\/MY_OS_FULL\\/CentOS_All_x64\\/svnrepo\\/DataEngine\\/EFAST\\/EApp\\/EVFS\\/src\\/evfs\\/util\\/ncEVFSSameNameUtil.cpp:117）\",\"errcode\":403039,\"errmsg\":\"存在同类型的同名文件名。\"}";

            var obj = JObject.Parse(ret);

            var b = obj;

            var causemsg = obj.GetValue("causemsg").ToString();

            var startIdx = causemsg.IndexOf("gns:", StringComparison.Ordinal);

            var gns = causemsg.Substring(startIdx, 38);

            var bb = gns;
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion 


    #region 检查服务器是否在线

    /// <summary>
    /// 检查服务器是否在线
    /// </summary>
    /// <param name="context"></param>
    private void Ping(HttpContext context)
    {
        var rst = "";

        try
        {
            var httpUtility = new AnyShareHelper();
            rst = httpUtility.Ping(AnyShareUrlPre);
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion 
    
    #region 登录

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="context"></param>
    private void Login(HttpContext context)
    {
        var rst = "";

        try
        {
            var account = (ConfigurationManager.AppSettings["AnyShareAccount"] ?? "").Trim();
            var password = (ConfigurationManager.AppSettings["AnySharePassword"] ?? "").Trim();

            var httpUtility = new AnyShareHelper();
            rst = httpUtility.Login(AnyShareUrlPre, account, password); // 爱数账号登录

            if (rst == null) return;

            var rstJson = JObject.Parse(rst);

            bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
            if (hasErr) // 错误处理
            {
                var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
                //var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
                //var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
                //lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

                rst = "{\"success\":false, \"Message\":\"" + causemsg + "\"}";
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

                AnyShareUser = user;

                HttpContext.Current.Session.Timeout = int.Parse(expires) / 60;
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
        
    }

    #endregion 
    
    #region 刷新身份凭证有效期

    /// <summary>
    /// 刷新身份凭证有效期
    /// </summary>
    /// <param name="context"></param>
    private void RefreshToken(HttpContext context)
    {
        var rst = "";

        try
        {
            var userId = AnyShareUser == null ? "" : AnyShareUser.UserId;
            var tokenId = AnyShareUser == null ? "" : AnyShareUser.TokenId;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(tokenId))
            {
                rst = "{\"success\":false, \"Message\":\"" + "用户未登录！" + "\"}";
                context.Response.Write(rst);
                
                return;
            }
            
            var httpUtility = new AnyShareHelper();
            rst = httpUtility.RefreshToken(AnyShareUrlPre, userId, tokenId, 1); // 刷新身份凭证有效期

            if (rst == null) return;

            var rstJson = JObject.Parse(rst);

            bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
            if (hasErr) // 错误处理
            {
                var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
                //var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
                //var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
                //lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

                rst = "{\"success\":false, \"Message\":\"" + causemsg + "\"}";
            }
            else
            {
                var expires = rstJson.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒

                AnyShareAuth user = AnyShareUser;
                if (user != null)
                {
                    user.Expires = int.Parse(expires);
                    AnyShareUser = user;

                    HttpContext.Current.Session.Timeout = int.Parse(expires) / 60;
                }
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion 
    
    #region 登出

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="context"></param>
    private void Logout(HttpContext context)
    {
        var rst = "";

        try
        {
            var userId = AnyShareUser == null ? "" : AnyShareUser.UserId;
            var tokenId = AnyShareUser == null ? "" : AnyShareUser.TokenId;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(tokenId))
            {
                rst = "{\"success\":false, \"Message\":\"" + "用户未登录！" + "\"}";
                context.Response.Write(rst);

                return;
            }
            
            var httpUtility = new AnyShareHelper();
            rst = httpUtility.Logout(AnyShareUrlPre, userId, tokenId); // 登出

            if (rst == null) return;

            if (string.IsNullOrEmpty(rst))
            {
                AnyShareUser = null; // 销毁Session对象
                rst = "{\"success\":false, \"Message\":\"" + "登出成功，Session已经销毁！" + "\"}";
            }
            else
            {
                var rstJson = JObject.Parse(rst);

                bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
                if (hasErr) // 错误处理
                {
                    var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
                    //var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
                    //var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
                    rst = "{\"success\":false, \"Message\":\"" + causemsg + "\"}";
                }
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion 
    
    
    #region 当前登录的用户对象

    /// <summary>
    /// 当前登录的用户对象
    /// </summary>
    public AnyShareAuth AnyShareUser
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