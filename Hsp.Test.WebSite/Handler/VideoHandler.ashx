<%@ WebHandler Language="C#" Class="VideoHandler" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Hsp.Test.IService;
using Hsp.Test.Service;

public class VideoHandler : IHttpHandler, IRequiresSessionState
{
    /// <summary>
    ///   媒体服务
    /// </summary>
    internal readonly IMediaService MediaService = new MediaService();
    
    /// <summary>
    ///   视频服务
    /// </summary>
    internal readonly IVideoService VideoService = new VideoService();

    #region ProcessRequest

    /// <summary>
    /// ProcessRequest
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();
        var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            //// 视频信息检索
            //case "RETRIEVAL":
            //    MediaRetrieval(context);
            //    break;

            //// 获取视频信息检索结果数量
            //case "COUNT":
            //    GetCount(context);
            //    break;

            // 清空数据表
            case "EMPTYING":
                EmptyingTable(context);
                break;

            //// 视频列表信息
            //case "LIST":
            //    GetVideoList(context);
            //    break;

            // 单个删除视频
            case "DELETE":
                DeleteVideo(context);
                break;

            // 批量删除视频
            case "BATCHDELETE":
                BatchDelete(context);
                break;

            // 视频列表信息
            case "PAGELIST":
                GetPageList(context);
                break;

            //// 视频归档操作
            //case "ARCHIVING":
            //    Archiving(context);
            //    break;

            default:
                break;
        }

    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion



    #region 获取视频分页列表信息

    /// <summary>
    ///     获取视频分页列表信息
    /// </summary>
    /// <param name="context"></param>
    private void GetPageList(HttpContext context)
    {
        var strTitle = context.Request.Params["Title"] ?? "";
        var strType = context.Request.Params["Type"] ?? "0";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim();

        //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
        //{
        //    return;
        //}

        var pageSize = context.Request.Params["pageSize"] ?? "0";
        var pageNumber = context.Request.Params["pageNumber"] ?? "0";

        if (string.IsNullOrEmpty(pageSize)) pageSize = "10";
        if (string.IsNullOrEmpty(pageNumber)) pageNumber = "1";

        //得到客户端传递的页码和每页记录数，并转换成int类型  
        //int pageSize = Integer.parseInt(request.getParameter("pageSize"));
        //int pageNumber = Integer.parseInt(request.getParameter("pageNumber"));
        //String orderNum = request.getParameter("orderNum");  

        ////分页查找商品销售记录，需判断是否有带查询条件  
        //List<SimsSellRecord> sellRecordList = null;
        //sellRecordList = sellRecordService.querySellRecordByPage(pageNumber, pageSize, orderNum);

        ////将商品销售记录转换成json字符串  
        //String sellRecordJson = sellRecordService.getSellRecordJson(sellRecordList);
        ////得到总记录数  
        //int total = sellRecordService.countSellRecord(orderNum);

        var paramList = new Dictionary<string, string>
            {
                {"Title", strTitle},
                {"Type", strType},
                {"PageIndex", pageNumber},
                {"PageSize", pageSize}               
            };

        var list = VideoService.GetVideoList(paramList);
        var js = new JavaScriptSerializer().Serialize(list);

        //需要返回的数据有总记录数和行数据  
        var json = "{\"total\":" + list[0].Count + ",\"rows\":" + js + "}";

        context.Response.Write(json);
    }

    #endregion

    #region 清空视频数据

    /// <summary>
    /// 清空视频数据
    /// </summary>
    /// <param name="context"></param>
    private void EmptyingTable(HttpContext context)
    {
        var rst = "";

        try
        {
            var name = context.Request.Params["name"];
            var count = MediaService.EmptyingTable(name);
            rst = "{\"success\":true,\"Message\":\"表" + name + "数据已经清空！\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion    
    

    #region 删除视频

    /// <summary>
    ///     删除视频
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void DeleteVideo(HttpContext context)
    {
        // 视频文件ID
        var id = context.Request.Params["ID"] ?? "0";
        if (id.Length > 0) id = id.Trim();
        var rst = "";
        var media = VideoService.GetVideoById(int.Parse(id));
        var i = VideoService.DelVideoById(int.Parse(id)); // 从数据库中删除视频
        if (i > 0)
        {
            rst = "{\"success\":true,\"Message\": \"视频删除成功！\"}";

            if (string.IsNullOrEmpty(media.FullName)) return;

            try
            {
                if (File.Exists(media.FullName))
                {
                    File.Delete(media.FullName);
                }
            }
            catch (Exception ex)
            {
                rst = "{\"success\":false,\"Message\": \"" + ex.Message.Replace('"', '\"') + "\"}";
            }
        }
        else
        {
            rst = "{\"success\":false,\"Message\": \"视频数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 批量删除视频

    /// <summary>
    ///     批量删除视频
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void BatchDelete(HttpContext context)
    {
        // 视频文件ID
        var ids = context.Request.Params["IDs"] ?? "";
        if (ids.Length > 0) ids = ids.Trim().TrimEnd(',');
        var rst = "";
        var mediaList = VideoService.GetVideoByIds(ids);
        var i = VideoService.DelVideoByIds(ids); // 从数据库中批量删除视频
        if (i > 0)
        {
            var strMessage = "视频数据删除成功！";

            foreach (var media in mediaList)
            {
                if (string.IsNullOrEmpty(media.FullName)) return;

                try
                {
                    if (File.Exists(media.FullName))
                    {
                        File.Delete(media.FullName);
                    }
                }
                catch (Exception ex)
                {
                    strMessage += "视频文件" + media.Title + "删除失败，原因：" + ex.Message.Replace('"', '\"');
                }
            }

            rst = "{\"success\":true,\"Message\": \"" + strMessage + "\"}";
        }
        else
        {
            rst = "{\"success\":false,\"Message\": \"视频数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion
    
    
}