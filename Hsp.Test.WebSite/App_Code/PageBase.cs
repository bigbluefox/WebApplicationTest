using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

/// <summary>
/// PageBase 的摘要说明
/// </summary>
public class PageBase : Hsp.Test.Common.PageBase
{

	public PageBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region AnyShareLogin

    /// <summary>
    /// AnyShareLogin
    /// </summary>
    /// <param name="urlBase"></param>
    /// <returns></returns>
    public string AnyShareLogin(string urlBase)
    {
        var account = (ConfigurationManager.AppSettings["AnyShareAccount"] ?? "").Trim();
        var password = (ConfigurationManager.AppSettings["AnySharePassword"] ?? "").Trim();

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.Login(urlBase, account, password); // 爱数账号登录

        if (rst == null) return "";

        var rstJson = JObject.Parse(rst);

        var hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            //lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

            AnyShare.LoginUser = null;

            return "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;
        }
        else
        {
            var tokenId = rstJson.GetValue("tokenid").ToString();
            var userId = rstJson.GetValue("userid").ToString();
            var expires = rstJson.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒



            Session.Timeout = int.Parse(expires)/60;

            var user = new AnyShareAuth();
            user.UserId = userId;
            user.TokenId = tokenId;
            user.Expires = int.Parse(expires);

            AnyShare.LoginUser = user;

            return "用户登录成功，有效时间：" + expires + "秒";
        }
    }

    #endregion

}