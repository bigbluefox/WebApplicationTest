using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.Model;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Dirs : PageBase
{
    /// <summary>
    ///     根目录编号
    /// </summary>
    public string RootDocId = (ConfigurationManager.AppSettings["AnyShareRootDocId"] ?? "").Trim();

    /// <summary>
    ///     爱数回收站基础目录编号
    /// </summary>
    public static string RecycleRootId = (ConfigurationManager.AppSettings["ASRecycleRootDocId"] ?? "").Trim();

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

    /// <summary>
    /// Page_Load
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
       lblMsg.Text =  AnyShareLogin(UrlPre);

       txtUserId.Text = AnyShare.LoginUser.UserId;
       txtToken.Text = AnyShare.LoginUser.TokenId;
    }


    #endregion

    #region 根目录浏览 (OK)

    /// <summary>
    /// 根目录浏览
    /// </summary>
    /// <returns></returns>
    public void GetEntryDoc()
    {

        //var httpUtility = new AnyShareHelper();
        try
        {
            //var url = UrlPre + ":9998/v1/entrydoc?method=get&userid=" + UserId + "&tokenid=" + TokenId;
            //var res = httpUtility.HttpPost(url, "");

            var httpUtility = new AnyShareHelper();
            var rst = httpUtility.RootDir(UrlPre, UserId, TokenId);

            var count = 0;
            var dirList = "";
            var jsonInfos = JObject.Parse(rst);

            dirList += "查询结果：<br/>" + Environment.NewLine;
            dirList += "<ul>";

            for (var i = 0; i < jsonInfos["docinfos"].Count(); ++i)
            {
                count++;
                var entrydocid = jsonInfos["docinfos"][i]["docid"].ToString();
                var entrydocname = jsonInfos["docinfos"][i]["docname"].ToString();
                var entrysize = jsonInfos["docinfos"][i]["size"].ToString();

                dirList += "<li onclick='CheckThis(this);' title='" + entrydocname + "'>" + entrydocid + " * " + entrydocname + " * " + entrysize + "</li>" + Environment.NewLine;

                //TreeNode node = new TreeNode();
                //node.Value = entrydocid;
                //node.Text = entrydocname + "(" + entrytypename + ")";
                if ((int)jsonInfos["docinfos"][i]["size"] >= 0) // 文件
                {
                    //node.Tag = "file";
                    //node.ImageIndex = 1;
                    //node.SelectedImageIndex = 1;
                }
                //treeView1.Nodes.Add(node);
            }

            dirList += "</ul>";
            dirList += "总计：" + count + "个对象！<br/>" + Environment.NewLine;

            lblResult.Text = dirList;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #endregion

    #region 3.1. 创建目录协议 (OK)

    /// <summary>
    ///     3.1. 创建目录协议 (OK)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateDir_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        //if (string.IsNullOrEmpty(UserId))
        //{
        //    UserId = txtUserId.Text;
        //}
        //if (string.IsNullOrEmpty(TokenId))
        //{
        //    TokenId = txtToken.Text;
        //}

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var dirName = txtDirName.Text;

        if (string.IsNullOrEmpty(dirName))
        {
            lblMsg.Text = "请填写要添加的目录名称";
            return;
        }
        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.CreateDir(UrlPre, UserId, TokenId, RootDocId, dirName);

        if (rst == null) return;

        var rstJson = JObject.Parse(rst);

        bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;
        }
        else
        {
            var docid = rstJson.GetValue("docid").ToString(); // 创建的目录的gns路径
            var rev = rstJson.GetValue("rev").ToString(); // 数据变化标识

            //var modified = jobj.GetValue("modified").ToString(); // 创建时间，UTC时间，此为服务器时间
            //var createTime = jobj.GetValue("create_time").ToString(); // 目录创建的服务端时间
            //var creator = jobj.GetValue("creator").ToString(); // 目录创建者
            //var editor = jobj.GetValue("目录修改者").ToString(); // 目录创建的服务端时间

            lblResult.Text = "目录“" + dirName + "”，编号：" + docid + "创建成功！";
        }
        //{"docid":"gns:\/\/7F45B54D47A242FC837EB31BAD9940D1\/E0BB1985CD64421E8262B66037E9524F","modified":1534137164360787,"rev":"e0e57b19-e92d-497c-9018-14cf67baeb35"}

    }

    #endregion

    #region 3.3. 删除目录协议 (OK)

    /// <summary>
    /// 3.3. 删除目录或文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeleteDir_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var docId = this.txtDocId.Text.Trim();
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件或目录编号";
            return;
        }

        var type = rblType.SelectedValue;

        var httpUtility = new AnyShareHelper();
        var rst = string.Empty;

        if (type == "1")
        {
            rst = httpUtility.DeleteDir(UrlPre, UserId, TokenId, docId); // 删除目录
        }
        else
        {
            rst = httpUtility.DeleteFile(UrlPre, UserId, TokenId, docId); // 删除文件

            if (string.IsNullOrEmpty(rst))
            {
                #region 删除回收站文件

                //if (type == "0") // 文件//{}

                var fileIdArr = docId.Split('/');
                if (fileIdArr.Length > 0)
                {
                    RecycleRootId = RecycleRootId.EndsWith("/") ? RecycleRootId : RecycleRootId + "/";
                    var fileId = fileIdArr[fileIdArr.Length - 1];
                    var fileRecycleId = RecycleRootId + fileId;
                    rst = httpUtility.RecycleDelete(UrlPre, UserId, TokenId, fileRecycleId); // 删除回收站文件
                }

                #endregion

                lblResult.Text = "文件删除成功，并已经删除回收站文件！";
                return;
            }
        }

        if (rst == null) return;

        var rstJson = JObject.Parse(rst);

        bool hasErr = rstJson.Properties().Any(p => p.Name == "errcode"); // 判断是否有错误信息
        if (hasErr) // 错误处理
        {
            var causemsg = rstJson.GetValue("causemsg").ToString(); // causemsg
            var errcode = rstJson.GetValue("errcode").ToString(); // 错误代码
            var errmsg = rstJson.GetValue("errmsg").ToString(); // 错误信息
            lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;
        }
        else
        {
            var isdirexist = rstJson.GetValue("isdirexist").ToString(); // 当前文件夹最后是否还存在 （用于前端判断刷新界面）
            var typeName = type == "1" ? "目录" : "文件";
            lblResult.Text = typeName + "删除成功！";
        }
    }

    #endregion

    #region 3.5. 浏览目录协议 (OK)

    /// <summary>
    ///     3.5. 浏览目录协议 (OK)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetDirList_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.ListDir(UrlPre, UserId, TokenId, RootDocId);
        if (rst == null) return;

        var dirList = "";
        var count = 0;
        var jsonInfos = JObject.Parse(rst);

        var dirs = jsonInfos["dirs"];
        var files = jsonInfos["files"];

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

        //gns://7F45B54D47A242FC837EB31BAD9940D1/76A40A6379CD4844B859BB23DBF9F9ED * 安全生产概论 * -1 
        //gns://7F45B54D47A242FC837EB31BAD9940D1/E0BB1985CD64421E8262B66037E9524F * 生产安全概论 * -1 
        //gns://7F45B54D47A242FC837EB31BAD9940D1/CD8F078765894A76A5F04D8F5A5C8F18 * 个人旅行物品清单.pdf * 507642 

        lblResult.Text = dirList;
    }

    #endregion

    #region 3.5. 浏览目录协议 (OK)

    /// <summary>
    /// 3.5. 浏览目录协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFileList_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var dirId = this.txtDirId.Text.Trim();
        if (string.IsNullOrEmpty(dirId))
        {
            lblMsg.Text = "请填写目录编号";
            return;
        }

        var httpUtility = new AnyShareHelper();
        var rst = httpUtility.ListDir(UrlPre, UserId, TokenId, dirId);
        if (rst == null) return;

        var count = 0;
        var dirList = "";
        var jsonInfos = JObject.Parse(rst);

        var dirs = jsonInfos["dirs"];
        var files = jsonInfos["files"];

        dirList += "查询结果：<br/>" + Environment.NewLine;
        dirList += "<ul>";

        foreach (var dir in dirs)
        {
            count++;
            dirList += "<li onclick='CheckThis(this);'>" + dir["docid"] + " * " + dir["name"] + " * " + dir["size"] + "</li>" + Environment.NewLine;
        }

        foreach (var file in files)
        {
            count++;
            dirList += "<li onclick='CheckThis(this);'>" + file["docid"] + " * " + file["name"] + " * " + file["size"] + "</li>" + Environment.NewLine;
        }

        dirList += "</ul>";

        dirList += "总计：" + count + "个对象！<br/>" + Environment.NewLine;

        lblResult.Text = dirList;

        //gns://7F45B54D47A242FC837EB31BAD9940D1/797CEEEE29E84811B0A940A6EC5BC8F7/D35E76AC778B4BB5B9577719F27AA937 * 最全ASCII码对照表.doc * 37890 
    }

    #endregion

    #region 根目录列表浏览 (OK)

    /// <summary>
    /// 根目录列表浏览
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetEntryDoc_Click(object sender, EventArgs e)
    {
        GetEntryDoc();
    }

    #endregion

    #region 文件预览，效果不理想

    /// <summary>
    /// 文件预览，效果不理想
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var docId = this.txtPreviewId.Text;
        if (string.IsNullOrEmpty(docId))
        {
            lblMsg.Text = "请填写文件编号";
            return;
        }

        var rst = PreView(docId.Trim());
        if (rst == null) return;

        var jobj = JObject.Parse(rst);
        var size = jobj.GetValue("size").ToString(); // 所预览文件的大小
        var url = jobj.GetValue("url").ToString(); // 下载转换后文件的url(15分钟过期)
        

        Response.Redirect(url, true);

        //https://zhdoc.cecep.cn:9029/anyshares3accesstestbucket/088CF110E09640D8B103F24A2D031552/C5A23DA26EA34F2DA1D90664E3C51569-0?x-eoss-length=74239&userid=AKIAI6IFWLK557WYM23A&Expires=1534147593&Signature=y97bzId25Eu6z9EDT4CR6FycaQM%3d&x-as-userid=16d72478-9c3a-11e8-8e0a-6c92bf4484f6
        
    }


    /// <summary>
    ///     预览文件，有“你的电脑不信任此网站的安全证书。错误代码: DLG_FLAGS_INVALID_CA”问题
    /// </summary>
    /// <param name="docid"></param>
    /// <returns></returns>
    public string PreView(string docid)
    {
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        var httpUtility = new AnyShareHelper();
        try
        {
            var url = UrlPre + ":9123/v1/file?method=previewoss&userid=" + UserId + "&tokenid=" + TokenId;
            var json = "{\"docid\":\"" + docid + "\"}";
            var res = httpUtility.HttpPost(url, json);
            return res;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message; // 请求API错误，调用了不支持的API
            //{"causemsg":"已提交文档转换(gns:\/\/7F45B54D47A242FC837EB31BAD9940D1\/797CEEEE29E84811B0A940A6EC5BC8F7\/D35E76AC778B4BB5B9577719F27AA937\/38CDC20472DC4494BA0674C5C43B3479)。（错误提供者：EVFS，错误值：16779009，错误位置：\/var\/JFR\/workspace\/C_EVFS\/MY_OS_FULL\/CentOS_All_x64\/svnrepo\/DataEngine\/EFAST\/EApp\/EVFS\/src\/evfs\/preview\/ncEVFSDocProcessMgr.cpp:577）","errcode":503002,"errmsg":"预览失败，服务端正在进行文档转换。"} 
            return null;
        }
    }

    #endregion

    #region 上传文件

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //UserId = string.IsNullOrEmpty(UserId) ? (Session["UserId"] ?? "").ToString() : UserId;
        //TokenId = string.IsNullOrEmpty(TokenId) ? (Session["TokenId"] ?? "").ToString() : UserId;
        //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(TokenId))
        {
            lblMsg.Text = "请首先登录用户";
            return;
        }

        var dirId = this.txtUploadDir.Text;
        if (string.IsNullOrEmpty(dirId))
        {
            lblMsg.Text = "请填写文件或目录编号";
            return;
        }

        var httpUtility = new AnyShareHelper();
        try
        {
            //判断是否上传了文件

            if (FileUpload1.HasFile)
            {
                //指定上传文件在服务器上的保存路径
                string savePath = Server.MapPath("/Upload/");

                //检查服务器上是否存在这个物理路径，如果不存在则创建
                if (!Directory.Exists(savePath))
                {
                    //需要注意的是，需要对这个物理路径有足够的权限，否则会报错
                    //另外，这个路径应该是在网站之下，而将网站部署在C盘却把文件保存在D盘

                    Directory.CreateDirectory(savePath);
                }

                savePath = savePath + "\\" + FileUpload1.FileName;

                FileUpload1.SaveAs(savePath); //保存文件

                var retDocId = "";

                using (FileStream fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    string urlbase = UrlPre + ":9123";
                    httpUtility.PostLargeFileBlock(urlbase, UserId, TokenId, dirId, FileUpload1.FileName, fileStream, out retDocId);

                    lblResult.Text = "文件[" + retDocId + "]上传成功！";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }

    #endregion
}