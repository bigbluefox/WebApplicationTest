using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Video : PageBase
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
    /// 检查服务器是否在线
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPing_Click(object sender, EventArgs e)
    {
        UrlPre = string.Format("http://{0}", AnyShareServer);

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
    /// 用户登录
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

        UrlPre = string.Format("http://{0}", AnyShareServer);
        Session["UrlPre"] = UrlPre;

        var json = "{ \"account\": " + "\"" + account + "\"," + "\"password" + "\":\"" + RSAEncrypt(password) + "\"}";
        var httpUtility = new AnyShareHelper();
        try
        {
            var url = UrlPre + ":9998/v1/auth1?method=getnew";
            var res = httpUtility.HttpPost(url, json);
            var jobj = JObject.Parse(res);
            TokenId = jobj.GetValue("tokenid").ToString();
            UserId = jobj.GetValue("userid").ToString();
            //tabControl1.SelectedIndex = 1;

            //var needmodifypassword = jobj.GetValue("needmodifypassword").ToString();
            // True表示该用户需要修改密码后才能登陆，False表示该用户不需要修改密码

            var expires = jobj.GetValue("expires").ToString(); // 获取到的token的有效期，单位为秒

            txtUserId.Text = UserId;
            txtToken.Text = TokenId;

            Session.Timeout = int.Parse(expires) / 60;

            Session["UserId"] = UserId;
            Session["TokenId"] = TokenId;

            var msg = string.IsNullOrEmpty(lblMsg.Text) ? "用户登录成功" : "，用户登录成功";
            lblMsg.Text = msg + "，有效时间：" + expires + "秒";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #endregion

    #region RSA加密

    /// <summary>
    ///     RSA加密
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string RSAEncrypt(string content)
    {
        var publickey =
            @"<RSAKeyValue><Modulus>uyS9A3GjFB7pknYcV08aogAQQgxEYUSSLADwfvs8dSDYEhCjxm3sQ7daI3DQHNHyPhv8k7kHIB9RFvKaLIFJ4tJnExOgp45FW7/CC4ArocvuHrvtpQKQ8EDw/U6+ifJNtUbrtrFleWdVUbkBahpv3Ob2kzkBOVRTiFz1U2mtuZk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publickey);

        byte[] cipherbytes;
        cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

        // 由于windows 使用的\r\n对 base64进行换行，而服务器rsa只支持 \n换行的base64 串，故将\r\n替换成 \n
        return Convert.ToBase64String(cipherbytes, Base64FormattingOptions.InsertLineBreaks).Replace("\r\n", "\n");
    }

    #endregion

    #region 5.18. 在线播放请求协议

    /// <summary>
    /// 5.18.	在线播放请求协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPlayInfo_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = this.txtPlayInfoDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var rst = PlayInfo(docId); // 在线播放请求协议

        if (rst == null) return;

        var jobj = JObject.Parse(rst);
        var status = jobj.GetValue("status").ToString(); // 转码状态: 0 未开始转码；1 正在转码；2转码完成
        var docid = jobj.GetValue("docid").ToString(); // 转码文件的唯一标识id

        this.txtPlayDocId.Text = docid;
        lblResult.Text = "在线播放请求协议：" + docid + "，转码状态：" + status + "[0 未开始转码；1 正在转码；2转码完成]";
    }

    /// <summary>
    /// 在线播放请求协议
    /// </summary>
    /// <param name="docid"></param>
    /// <returns></returns>
    public string PlayInfo(string docid)
    {
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var httpUtility = new AnyShareHelper();
        try
        {
            var url = UrlPre + ":9123/v1/file?method=playinfo&userid=" + UserId + "&tokenid=" + TokenId;
            var json = "{\"docid\":\"" + docid + "\"}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message; return null;
        }
    }

    #endregion

    #region 5.19. 在线播放协议

    /// <summary>
    /// 5.19. 在线播放协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPlay_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = this.txtPlayDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var rst = Play(docId); // 在线播放协议

        if (rst == null) return;


        //在将文本写入文件前，处理文本行
        //StreamWriter一个参数默认覆盖
        //StreamWriter第二个参数为false覆盖现有文件，为true则把文本追加到文件末尾

        var filePath = "/AnyShare/Test.m3u8";
        filePath = Server.MapPath(filePath);

        System.IO.File.WriteAllText(filePath, rst, Encoding.UTF8);


        //using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        //{

        //    foreach (string line in lines)
        //    {

        //        if (!line.Contains("second"))
        //        {

        //            file.Write(line);//直接追加文件末尾，不换行

        //            file.WriteLine(line);// 直接追加文件末尾，换行   

        //        }

        //    }

        //}

        //var jobj = JObject.Parse(rst);
        //var aa = jobj;

        //var status = jobj.GetValue("status").ToString(); // 转码状态: 0 未开始转码；1 正在转码；2转码完成
        //var docid = jobj.GetValue("docid").ToString(); // 转码文件的唯一标识id

        //lblResult.Text = "在线播放协议：" + docid + "，转码状态：" + status + "[0 未开始转码；1 正在转码；2转码完成]";
    }

    /// <summary>
    /// 在线播放协议
    /// </summary>
    /// <param name="docid"></param>
    /// <returns></returns>
    public string Play(string docid)
    {
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var httpUtility = new AnyShareHelper();
        try
        {
            var url = UrlPre + ":9123/v1/file?method=play&userid=" + UserId + "&tokenid=" + TokenId;
            var json = "{\"docid\":\"" + docid + "\"}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message; return null;
        }
    }

    #endregion

    #region 5.20. 获取视频缩略图协议

    /// <summary>
    /// 5.20. 获取视频缩略图协议，棒棒的
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnThumbnail_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = this.txtPlayInfoDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var response = httpUtility.Thumbnail(UrlPre, UserId, TokenId, docId); // 获取视频缩略图协议

        if (response == null) return;

        Stream imgstream = response.GetResponseStream();

        if (imgstream == null) return;

        System.Drawing.Image img = System.Drawing.Image.FromStream(imgstream);

        string savePath = Server.MapPath("/Upload/");

        //检查服务器上是否存在这个物理路径，如果不存在则创建
        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
        savePath = savePath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

        //MemoryStream ms = new MemoryStream();
        //img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //Response.ClearContent(); //需要输出图象信息 要修改HTTP头 
        //Response.ContentType = "image/jpg";
        //Response.BinaryWrite(ms.ToArray());
    }

    /// <summary>
    /// 二进制转图片
    /// </summary>
    /// <param name="streamByte"></param>
    /// <returns></returns>
    //public System.Drawing.Image ReturnPhoto(byte[] streamByte)
    //{
    //    System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
    //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
    //    return img;
    //}

    ///// <summary>
    ///// 获取视频缩略图协议
    ///// </summary>
    ///// <param name="docid"></param>
    ///// <returns></returns>
    //public HttpWebResponse Thumbnail(string docid)
    //{
    //    UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

    //    var httpUtility = new AnyShareHelper();
    //    try
    //    {
    //        var url = UrlPre + ":9123/v1/file?method=playthumbnail&userid=" + UserId + "&tokenid=" + TokenId;
    //        var json = "{\"docid\":\"" + docid + "\"}";
    //        //var res = httpUtility.HttpPost(url, json);

    //        HttpWebResponse response = httpUtility.GetHttpPostResponse(url, json);


    //        return response;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = ex.Message; return null;
    //    }
    //}

    #endregion

}