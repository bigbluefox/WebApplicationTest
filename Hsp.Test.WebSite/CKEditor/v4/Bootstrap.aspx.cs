using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

public partial class CKEditor_v4_Bootstrap : PageBase
{
    /// <summary>
    /// 分页数据
    /// </summary>
    protected List<Article> List { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    protected string StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    protected string EndDate { get; set; }

    /// <summary>
    /// 文章服务
    /// </summary>
    internal readonly IArticleService ArticleService = new ArticleService();

    #region 页面加载

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //var pageIndex = Request.QueryString["page"] ?? "1";
        //MapName = Request.QueryString["mapName"] ?? "";

        //var defalutPageSize = ConfigurationManager.AppSettings["DefalutPageSize"] ?? "20";
        //PageSize = int.Parse(defalutPageSize);
        //PageIndex = int.Parse(pageIndex);

        //StartDate = Request.QueryString["sdate"] ?? "";
        //EndDate = Request.QueryString["edate"] ?? "";

        //if (Page.IsPostBack) return;
        //PageDataProcess(PageIndex, PageSize);
    }

    #endregion

    #region 分页数据处理

    /// <summary>
    /// 分页数据处理
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">分页大小</param>
    protected void PageDataProcess(int pageIndex, int pageSize)
    {
        var paramList = new Dictionary<string, string> 
        {
            {"pageSize", pageSize.ToString()},
            {"pageIndex", pageIndex.ToString()}
        };

        List = ArticleService.GetArticleList(paramList);

        //List = bll.PageStandardList(paramList);
        if (List.Count == 0) return;

        //PagerString = GetPagerString(List[0].Count, pageIndex, pageSize);
        //PagerString = GetPagerString(809, pageIndex, pageSize);
    }

    #endregion
}