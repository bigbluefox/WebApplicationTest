using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.IService;
using Hsp.Test.Model.Media;
using Hsp.Test.Service;

public partial class Medias_Archiving : System.Web.UI.Page
{
    /// <summary>
    /// 媒体分页数据
    /// </summary>
    protected List<Medias> List { get; set; }

    /// <summary>
    ///   媒体服务
    /// </summary>
    internal readonly IMediaService MediaService = new MediaService();

    protected void Page_Load(object sender, EventArgs e)
    {
        //MapName = Request.QueryString["mapName"] ?? "";
        //var pageIndex = Request.QueryString["page"] ?? "1";
        //var defalutPageSize = ConfigurationManager.AppSettings["DefalutPageSize"] ?? "20";
        //PageSize = int.Parse(defalutPageSize); //PageSize = 10;
        //PageIndex = int.Parse(pageIndex);

        //StartDate = Request.QueryString["sdate"] ?? "";
        //EndDate = Request.QueryString["edate"] ?? "";
        //QueryName = Request.QueryString["queryname"] ?? "";

        var strTitle = Request.QueryString["Title"] ?? "";
        var strType = Request.QueryString["Type"] ?? "";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim();

        if (Page.IsPostBack) return;
        PageLogDataProcess(strTitle, strType);
    }


    #region 媒体数据处理

    /// <summary>
    /// 媒体数据处理
    /// </summary>
    /// <param name="title"></param>
    /// <param name="type"></param>
    protected void PageLogDataProcess(string title, string type)
    {

        //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
        //{
        //    return;
        //}

        var paramList = new Dictionary<string, string>
            {
                {"Title", title},
                {"Type", type}
            };

        List = MediaService.GetMediaList(paramList);

        if (List.Count == 0) return;

        //PagerString = GetPagerString(List[0].RecordCount, pageIndex, pageSize);
        //PagerString = GetPagerString(89809, pageIndex, pageSize);
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileSize"></param>
    /// <returns></returns>
    protected string FileSizeProcess(long fileSize)
    {
        var s = "";
        var rst = 0.0;
        if (fileSize < 102.4)
        {
            s = "0.10K";
        }
        else if (fileSize < 1024 * 1024)
        {
            rst = Math.Round(fileSize/1024.0, 2, MidpointRounding.AwayFromZero);
            s = rst.ToString("##.00") + "K";
        }
        else if (fileSize < 1024 * 1024 * 1024)
        {
            rst = Math.Round(fileSize/1024.0/1024.0, 2, MidpointRounding.AwayFromZero);
            s = rst.ToString("##.00") + "M";
        } 
        else
        {
            rst = Math.Round(fileSize/1024.0/1024.0/1024.0, 2, MidpointRounding.AwayFromZero);
            s = rst.ToString("##.00") + "G";
        }

        return s;
    }
}

//http://issues.wenzhixin.net.cn/bootstrap-table/index.html

//<tbody>
//<% if (List != null && List.Count > 0)
//   {
//       foreach (var m in List)
//       { %>
//        <tr>
//            <td style="text-align: center;"><% = m.Id %></td>
//            <td style="">
//                <input type="checkbox" name="cbxId" value="<% = m.Id %>"/>
//            </td>
//            <td style=""><% = m.Type %></td>
//            <td style="display: none;"><% = m.Name %></td>
//            <td style="" title="<% = m.Title %>"><% = m.Title %></td>
//            <td style="" title="<% = m.DirectoryName %>"><% = m.DirectoryName %></td>
//            <td style=""><% = m.Extension %></td>
//            <td style="text-align: right;" title="<% = m.Size %>"><% = FileSizeProcess(m.Size) %></td>
//            <td style="display: none; text-align: right;"><% = m.Width %></td>
//            <td style="display: none; text-align: right;"><% = m.Height %></td>
//            <td style=""><% = m.MD5 %></td>
//            <td style="text-align: center;">
//                <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" title="编辑" data-toggle="modal" data-id="" data-target="#progressModal"></span>--%>
//                <span class="glyphicon glyphicon-remove" aria-hidden="true" title="删除" onclick="javascript:DelMediaById(this);"></span>
//                <%--<span class="glyphicon glyphicon-download-alt" aria-hidden="true" title="下载" onclick="javascript:StandardDownload('<% = m.Id %>');"></span>--%>
//            </td>
//        </tr>
//<% }
//   } %>
//</tbody>
