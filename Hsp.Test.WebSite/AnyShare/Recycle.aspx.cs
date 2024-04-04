using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Recycle : PageBase
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
    }


    #endregion

    #region 6.1. 浏览回收站资源协议

    /// <summary>
    /// 6.1. 浏览回收站资源协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRecycleList_Click(object sender, EventArgs e)
    {
        var rst = this.ListRecycle(UrlPre, UserId, TokenId, RootDocId);
        if (rst == null) return;

        var dirList = "";
        var jsonObj = JObject.Parse(rst);

        var count = 0;

        var dirs = jsonObj["dirs"];
        var files = jsonObj["files"];

        dirList += "查询结果：<br/>" + Environment.NewLine;
        dirList += "<ul>";

        foreach (var dir in dirs)
        {
            count++;
            dirList += "<li onclick='CheckThis(this);' title='" + dir["name"] + "'>" + dir["docid"] + " * " + dir["name"] + " * " + dir["size"] + "</li>" + Environment.NewLine;
        }

        foreach (var file in files)
        {
            count++;
            dirList += "<li onclick='CheckThis(this);' title='" + file["name"] + "'>" + file["docid"] + " * " + file["name"] + " * " + file["size"] + "</li>" + Environment.NewLine;
        }

        dirList += "</ul>";

        dirList += "总计：" + count + "个对象！<br/>" + Environment.NewLine;

        lblResult.Text = dirList;
    }

    /// <summary>
    ///     6.1. 浏览回收站资源协议
    /// </summary>
    /// <param name="urlBase">基础地址</param>
    /// <param name="userId">用户标识</param>
    /// <param name="tokenId">验证令牌</param> 
    /// <param name="docid">gns路径</param>
    /// <returns></returns>
    public string ListRecycle(string urlBase, string userId, string tokenId, string docid)
    {
        var httpUtility = new AnyShareHelper();
        try
        {
            var url = urlBase + ":9123/v1/recycle?method=list&userid=" + userId + "&tokenid=" + tokenId;
            var json = "{\"docid\":\"" + docid + "\"}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion

    #region 6.2. 还原回收站资源协议

    /// <summary>
    /// 6.2. 还原回收站资源协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRestore_Click(object sender, EventArgs e)
    {
        var docId = txtRestoreDocId.Text;

        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写要还原的资源编号！";
            return;
        }

        var rst = this.RestorRecycle(UrlPre, UserId, TokenId, docId);
        if (rst == null) return;

        var jsonObj = JObject.Parse(rst);
        rst = jsonObj.GetValue("docid").ToString();

        rst = "资源（" + rst + ")还原成功！";

        lblResult.Text = rst;

        txtDelDocId.Text = "";
        txtRestoreDocId.Text = "";
    }

    /// <summary>
    ///     6.2. 还原回收站资源协议
    /// </summary>
    /// <param name="urlBase">基础地址</param>
    /// <param name="userId">用户标识</param>
    /// <param name="tokenId">验证令牌</param> 
    /// <param name="docid">gns路径</param>
    /// <returns></returns>
    public string RestorRecycle(string urlBase, string userId, string tokenId, string docid)
    {
        var httpUtility = new AnyShareHelper();
        try
        {
            var url = urlBase + ":9123/v1/recycle?method=restore&userid=" + userId + "&tokenid=" + tokenId;
            var json = "{\"docid\":\"" + docid + "\", \"ondup\": \"2\"" + "}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion

    #region 6.3. 删除回收站资源协议

    /// <summary>
    /// 6.3. 删除回收站资源协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        var docId = txtDelDocId.Text;

        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写要删除的资源编号！";
            return;
        }

        var rst = this.DeleteRecycle(UrlPre, UserId, TokenId, docId);
        //if (rst == null) return;

        if (string.IsNullOrEmpty(rst)) rst = "回收站资源删除成功！";

        lblResult.Text = rst;

        txtDelDocId.Text = "";
        txtRestoreDocId.Text = "";
    }

    /// <summary>
    ///     6.3. 删除回收站资源协议
    /// </summary>
    /// <param name="urlBase">基础地址</param>
    /// <param name="userId">用户标识</param>
    /// <param name="tokenId">验证令牌</param> 
    /// <param name="docid">gns路径</param>
    /// <returns></returns>
    public string DeleteRecycle(string urlBase, string userId, string tokenId, string docid)
    {
        var httpUtility = new AnyShareHelper();
        try
        {
            var url = urlBase + ":9123/v1/recycle?method=delete&userid=" + userId + "&tokenid=" + tokenId;
            var json = "{\"docid\":\"" + docid + "\"}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion

}