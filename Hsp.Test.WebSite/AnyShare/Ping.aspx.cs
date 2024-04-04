using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Ping : PageBase
{
    /// <summary>
    ///     爱数服务地址
    /// </summary>
    public string AnyShareServer = (ConfigurationManager.AppSettings["AnyShareServer"] ?? "").Trim();

    /// <summary>
    ///     根目录编号
    /// </summary>
    public string RootDocId = "gns://7F45B54D47A242FC837EB31BAD9940D1";

    /// <summary>
    ///     爱数服务地址
    /// </summary>
    /// <summary>
    /// </summary>
    public string UrlPre { get; set; }

    /// <summary>
    ///     登录账号
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// </summary>
    public string TokenId { get; set; }

    /// <summary>
    ///     Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        UrlPre = string.Format("http://{0}", AnyShareServer);

        UserId = AnyShare.LoginUser == null ? "" : AnyShare.LoginUser.UserId;
        TokenId = AnyShare.LoginUser == null ? "" : AnyShare.LoginUser.TokenId;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "用户未登录或者Session过期";
        }
        else
        {
            lblMsg.Text = "用户已登录!";

            txtUserId.Text = AnyShare.LoginUser.UserId;
            txtToken.Text = AnyShare.LoginUser.TokenId;
        }
    }

    #region 检查服务器是否在线 (OK)

    /// <summary>
    ///     4.1.	检查服务器是否在线
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPing_Click(object sender, EventArgs e)
    {
        //var url = Url + "ping";

        //// 获取凭证验证结果
        //var rst = HttpPost(url, "", Encoding.UTF8, "application/json;charset=UTF-8");

        //var ss = rst;

        //if (string.IsNullOrEmpty(rst))
        //{
        //    lblMsg.Text = "服务器在线";
        //}

        UrlPre = string.Format("http://{0}", AnyShareServer);
        var httpUtility = new AnyShareHelper();
        var res = httpUtility.Ping(UrlPre);

        lblMsg.Text = string.IsNullOrEmpty(res) ? "服务器在线" : "服务器不在线";
    }

    #endregion

    #region 登录测试 (OK)

    /// <summary>
    ///     登录测试
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLoginTest_Click(object sender, EventArgs e)
    {
        lblMsg.Text = AnyShareLogin(UrlPre);

        txtUserId.Text = AnyShare.LoginUser.UserId;
        txtToken.Text = AnyShare.LoginUser.TokenId;

        return;

        //var account = "interface_test";
        //var password = "cecep@123";

        var account = (ConfigurationManager.AppSettings["AnyShareAccount"] ?? "").Trim();
        var password = (ConfigurationManager.AppSettings["AnySharePassword"] ?? "").Trim();

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.Login(UrlPre, account, password); // 爱数账号登录

        if (rst == null) return;

        var rstJson = JObject.Parse(rst);

        var hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

            AnyShare.LoginUser = null;
        }
        else
        {
            TokenId = rstJson.GetValue("tokenid").ToString();
            UserId = rstJson.GetValue("userid").ToString();
            var expires = rstJson.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒

            txtUserId.Text = UserId;
            txtToken.Text = TokenId;

            Session.Timeout = int.Parse(expires)/60;

            var user = new AnyShareAuth();
            user.UserId = UserId;
            user.TokenId = TokenId;
            user.Expires = int.Parse(expires);

            AnyShare.LoginUser = user;

            lblMsg.Text = "用户登录成功，有效时间：" + expires + "秒";
        }
    }

    #endregion

    #region 5.10. 刷新身份凭证有效期

    /// <summary>
    ///     5.10. 刷新身份凭证有效期
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefreshToken_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString(): UserId ;
        //TokenId = string.IsNullOrEmpty(TokenId) ?  (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.RefreshToken(UrlPre, UserId, TokenId, 1); // 刷新身份凭证有效期

        if (rst == null) return;

        var rstJson = JObject.Parse(rst);

        var hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

            AnyShare.LoginUser = null;
            AnyShare.AnyShareErrorProcess(errcode);
        }
        else
        {
            var expires = rstJson.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒
            Session.Timeout = int.Parse(expires)/60;

            var user = AnyShare.LoginUser;
            user.Expires = int.Parse(expires);

            AnyShare.LoginUser = user;

            lblMsg.Text = "刷新以后token的有效期：" + expires + "秒";
        }
    }

    #endregion

    #region 5.11. 回收身份凭证

    /// <summary>
    ///     5.11. 回收身份凭证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRevokeToken_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "用户没有登录";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.RevokeToken(UrlPre, TokenId); // 回收身份凭证

        if (rst == null) return;

        if (string.IsNullOrEmpty(rst))
        {
            lblMsg.Text = "回收身份凭证成功，Session已经销毁！";

            AnyShare.LoginUser = null; // 销毁Session对象
        }
        else
        {
            var rstJson = JObject.Parse(rst);

            var hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
            if (hasErr) // 错误处理
            {
                var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
                var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
                var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
                lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

                AnyShare.AnyShareErrorProcess(errcode);
            }
        }
    }

    #endregion

    #region 5.13. 登出

    /// <summary>
    ///     5.13. 登出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "用户没有登录";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.Logout(UrlPre, UserId, TokenId); // 登出

        if (rst == null) return;

        if (string.IsNullOrEmpty(rst))
        {
            lblMsg.Text = "登出成功，Session已经销毁！";
            AnyShare.LoginUser = null; // 销毁Session对象
        }
        else
        {
            var rstJson = JObject.Parse(rst);

            var hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
            if (hasErr) // 错误处理
            {
                var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
                var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
                var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
                lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;

                AnyShare.AnyShareErrorProcess(errcode);
            }
        }
    }

    #endregion

    #region RSA生成公私钥测试

    /// <summary>
    ///     RSA生成公私钥测试
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateKey_Click(object sender, EventArgs e)
    {
        string privateKey = string.Empty, publicKey = string.Empty;

        RsaHelper.CreateKey(out privateKey, out publicKey);

        txtPrivateKey.Text = privateKey;
        txtPublicKey.Text = publicKey;
    }

    #endregion

    #region 5.2. 获取OAuth信息

    /// <summary>
    ///     5.2. 获取OAuth信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOAuth_Click(object sender, EventArgs e)
    {
        UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var url = UrlPre + "auth1?method=getoauthinfo";

        // 获取凭证验证结果
        var rst = HttpPost(url, "", Encoding.UTF8, "application/json;charset=UTF-8");

        var ss = rst; // {"authserver":"","authurl":"","isenabled":false,"redirectserver":""}

        if (string.IsNullOrEmpty(rst))
        {
            lblMsg.Text = "服务器在线";
        }
    }

    #endregion

    #region 5.1. 获取服务器配置信息

    /// <summary>
    ///     5.1. 获取服务器配置信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfig_Click(object sender, EventArgs e)
    {
        UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var url = UrlPre + "auth1?method=getconfig";

        // 获取凭证验证结果
        var rst = HttpPost(url, "", Encoding.UTF8, "application/json;charset=UTF-8");

        var ss = rst;
        // {"auto_lock_remind":true,"csf_level_enum":{"内部":6,"机密":8,"秘密":7,"绝密":9,"非密":5},"enable_doc_comment":true,"enable_invitation_share":false,"enable_limit_rate":true,"enable_link_access_code":false,"enable_secret_mode":false,"entrydoc_view_config":1,"extapp":{"enable_chaojibiaoge":false,"enable_qhdj":false},"forbid_ostype":"0","https":false,"internal_link_prefix":"AnyShare:\/\/","oemconfig":{"allowauthlowcsfuser":true,"allowowner":true,"cadpreview":false,"clearcache":false,"clientlogouttime":-1,"defaultpermexpireddays":-1,"enableclientmanuallogin":true,"enablecsflevel":false,"enablefiletransferlimit":false,"enableonedrive":false,"enableshareaudit":false,"enableuseragreement":false,"hidecachesetting":false,"indefiniteperm":true,"maxpassexpireddays":-1,"owasurl":"","rememberpass":true,"wopiurl":""},"server_version":"5.0.20-20180104-7324","show_knowledge_page":0,"tag_max_num":30,"third_pwd_modify_url":"","vcode_login_config":{"isenable":false,"passwderrcnt":0},"windows_ad_sso":{"is_enabled":false}}

        if (!string.IsNullOrEmpty(rst))
        {
            lblMsg.Text = rst;
        }

        //{"auto_lock_remind":true,
        //"csf_level_enum":{"内部":6,"机密":8,"秘密":7,"绝密":9,"非密":5},
        //"enable_doc_comment":true,
        //"enable_invitation_share":false,
        //"enable_limit_rate":true,
        //"enable_link_access_code":false,
        //"enable_secret_mode":false,
        //"entrydoc_view_config":1,
        //"extapp":{"enable_chaojibiaoge":false,"enable_qhdj":false},
        //"forbid_ostype":"0",
        //"https":false,
        //"internal_link_prefix":"AnyShare:\/\/",
        //"oemconfig":{"allowauthlowcsfuser":true,
        //"allowowner":true,"cadpreview":false,
        //"clearcache":false,"clientlogouttime":-1,"defaultpermexpireddays":-1,
        //"enableclientmanuallogin":true,
        //"enablecsflevel":false,
        //"enablefiletransferlimit":false,
        //"enableonedrive":false,
        //"enableshareaudit":false,
        //"enableuseragreement":false,
        //"hidecachesetting":false,
        //"indefiniteperm":true,
        //"maxpassexpireddays":-1,
        //"owasurl":"","rememberpass":true,
        //"wopiurl":""},
        //"server_version":"5.0.20-20180104-7324",
        //"show_knowledge_page":0,
        //"tag_max_num":30,
        //"third_pwd_modify_url":"",
        //"vcode_login_config":{"isenable":false,"passwderrcnt":0},
        //"windows_ad_sso":{"is_enabled":false}}
    }

    #endregion

    #region Post数据接口

    /// <summary>
    ///     Post数据接口
    /// </summary>
    /// <param name="postUrl">接口地址</param>
    /// <param name="paramData">提交json数据</param>
    /// <param name="dataEncode">编码方式</param>
    /// <param name="contentType">内容类型</param>
    /// <returns></returns>
    public static string HttpPost(string postUrl, string paramData, Encoding dataEncode, string contentType)
    {
        var ret = string.Empty;
        try
        {
            var byteArray = dataEncode.GetBytes(paramData); //转化
            var webReq = (HttpWebRequest) WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            //webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentType = contentType;
            webReq.ContentLength = byteArray.Length;
            var dataStream = webReq.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length); //写入参数
            dataStream.Close();
            var response = (HttpWebResponse) webReq.GetResponse();
            var sr = new StreamReader(response.GetResponseStream(), dataEncode);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            dataStream.Close();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return ret;
    }

    /// <summary>
    ///     Post数据接口 (Json)
    /// </summary>
    /// <param name="url"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static string HttpPost(string url, string param)
    {
        var strURL = url;
        HttpWebRequest request;
        request = (HttpWebRequest) WebRequest.Create(strURL);
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        var paraUrlCoded = param;
        byte[] payload;
        payload = Encoding.UTF8.GetBytes(paraUrlCoded);
        request.ContentLength = payload.Length;
        var writer = request.GetRequestStream();
        writer.Write(payload, 0, payload.Length);
        writer.Close();
        HttpWebResponse response;
        response = (HttpWebResponse) request.GetResponse();
        Stream s;
        s = response.GetResponseStream();
        var StrDate = "";
        var strValue = "";
        var Reader = new StreamReader(s, Encoding.UTF8);
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        return strValue;
    }

    #endregion
}