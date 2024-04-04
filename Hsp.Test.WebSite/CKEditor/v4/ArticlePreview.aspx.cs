using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

public partial class CKEditor_v4_ArticlePreview : PageBase
{
    /// <summary>
    /// 文章服务
    /// </summary>
    internal readonly IArticleService ArticleService = new ArticleService();

    /// <summary>
    /// 文章实体
    /// </summary>
    internal Article Article { get; set; }

    /// <summary>
    /// 返回地址
    /// </summary>
    internal string ReturnUrl { get; set; }

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Request.QueryString["id"] ?? "";
        if (string.IsNullOrEmpty(id)) return;
        Article = ArticleService.GetArticleById(int.Parse(id));

        if (Request.UrlReferrer != null) ReturnUrl = Request.UrlReferrer.AbsolutePath;

        if (Page.IsPostBack) return;
    }
}