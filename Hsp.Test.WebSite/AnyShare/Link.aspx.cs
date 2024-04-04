using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Link : PageBase
{
    /// <summary>
    ///     根目录编号
    /// </summary>
    public string RootDocId = "gns://7F45B54D47A242FC837EB31BAD9940D1";

    /// <summary>
    ///     爱数服务地址
    /// </summary>
    public string AnyShareServer = (ConfigurationManager.AppSettings["AnyShareServer"] ?? "").Trim();

    /// <summary>
    ///     爱数服务地址
    /// </summary>
    public string Url = (ConfigurationManager.AppSettings["AnyShareUrl"] ?? "").Trim();

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

    #region 检查服务器是否在线

    /// <summary>
    ///     检查服务器是否在线
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPing_Click(object sender, EventArgs e)
    {
        var httpUtility = new AnyShareHelper();
        try
        {
            var url = UrlPre + ":9998/v1/ping";
            var res = httpUtility.HttpPost(url, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            return;
        }

        lblMsg.Text = "服务器在线";
    }

    #endregion

    #region 用户登录

    /// <summary>
    ///     用户登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLoginTest_Click(object sender, EventArgs e)
    {
        lblMsg.Text = AnyShareLogin(UrlPre);

        txtUserId.Text = AnyShare.LoginUser.UserId;
        txtToken.Text = AnyShare.LoginUser.TokenId;

        return;

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

            Session.Timeout = int.Parse(expires) / 60;

            var user = new AnyShareAuth();
            user.UserId = UserId;
            user.TokenId = TokenId;
            user.Expires = int.Parse(expires);

            AnyShare.LoginUser = user;

            lblMsg.Text = "用户登录成功，有效时间：" + expires + "秒";
        }
    }

    #endregion

    #region 7.1. 获取外链开启信息

    /// <summary>
    ///     7.1. 获取外链开启信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLinkInfo_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = txtLinkDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.LinkInfo(UrlPre, UserId, TokenId, docId); // 外链开启信息

        if (rst == null) return;

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
        else
        {
            var jobj = JObject.Parse(rst);
            var linkId = jobj.GetValue("link").ToString(); // 外链唯一标识，如FC5E038D38A57032085441E7FE7010B0
            var password = jobj.GetValue("password").ToString(); // 密码，空表示没有
            var endtime = jobj.GetValue("endtime").ToString(); // 到期时间，如果为-1，表示无限期，表示从1970-01-01,00-00-00至今的时间
            var perm = jobj.GetValue("perm").ToString(); // 按位存储的权限值（返回的是十进制），获取该值后，需要转化成二级制，检查对应的位码是否被设置。
            var limittimes = jobj.GetValue("limittimes").ToString(); // 外链使用次数。-1为无限制
            //accesscode

            lblResult.Text = "外链唯一标识：" + linkId;
        }
    }

    #endregion

    #region 7.2. 开启外链

    /// <summary>
    ///     开启外链
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetLink_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = txtLinkDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.SetLink(UrlPre, UserId, TokenId, docId); // 开启外链信息

        if (rst == null) return;

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
        else
        {
            var jobj = JObject.Parse(rst);
            var linkId = jobj.GetValue("link").ToString(); // 外链唯一标识，如FC5E038D38A57032085441E7FE7010B0
            var password = jobj.GetValue("password").ToString(); // 密码，空表示没有
            var endtime = jobj.GetValue("endtime").ToString(); // 到期时间，如果为-1，表示无限期，表示从1970-01-01,00-00-00至今的时间
            var perm = jobj.GetValue("perm").ToString(); // 按位存储的权限值（返回的是十进制），获取该值后，需要转化成二级制，检查对应的位码是否被设置。
            var limittimes = jobj.GetValue("limittimes").ToString(); // 外链使用次数。-1为无限制
            var result = jobj.GetValue("result").ToString(); // 0，请求已生效，返回为最新信息；1，请求正在审核，返回为创建前信息

            lblResult.Text = "外链唯一标识：" + linkId; // 96153D76D76C8620C28034C85EEB5153
        }
    }

    #endregion

    #region 7.5. 我的外链

    /// <summary>
    ///     7.5. 我的外链
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetLinked_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.Linked(UrlPre, UserId, TokenId); // 我的外链信息

        if (rst == null) return;

        if (rst.StartsWith("["))
        {
            var jsonArray = JArray.Parse(rst);

            var linkList = "";

            foreach (var link in jsonArray)
            {
                linkList += link["docid"] + " * " + link["namepath"] + " * " + link["size"] + "<br/>" +
                            Environment.NewLine;
            }
            lblResult.Text = "我的外链：<br/>" + linkList;
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
            }
        }
    }

    #endregion
}