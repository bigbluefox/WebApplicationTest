<%@ WebHandler Language="C#" Class="AudioHandler" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Hsp.Test.IService;
using Hsp.Test.Service;

public class AudioHandler : IHttpHandler
{

    /// <summary>
    ///   媒体服务
    /// </summary>
    internal readonly IMediaService MediaService = new MediaService();

    /// <summary>
    ///   音频服务
    /// </summary>
    internal readonly IAudioService AudioService = new AudioService();

    #region ProcessRequest

    /// <summary>
    /// ProcessRequest
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();
        var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"] ?? "";

        switch (strOperation.ToUpper())
        {
            //// 音频信息检索
            //case "RETRIEVAL":
            //    AudioRetrieval(context);
            //    break;

            // 音频标题修改
            case "SAVE":
                AudioSave(context);
                break;

            // 清空数据表
            case "EMPTYING":
                EmptyingTable(context);
                break;

            // 音频列表信息
            case "LIST":
                GetAudioList(context);
                break;

            // 单个删除音频
            case "DELETE":
                DeleteAudio(context);
                break;

            // 批量删除音频
            case "BATCHDELETE":
                BatchDelete(context);
                break;

            // 音频列表信息
            case "PAGELIST":
                GetPageList(context);
                break;


            default:
                break;
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion


    #region 获取音频列表信息

    /// <summary>
    ///     获取音频列表信息
    /// </summary>
    /// <param name="context"></param>
    private void GetAudioList(HttpContext context)
    {
        var strTitle = context.Request.Params["Title"] ?? "";
        var strType = context.Request.Params["Type"] ?? "";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim();

        //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
        //{
        //    return;
        //}

        //得到客户端传递的页码和每页记录数，并转换成int类型  
        //int pageSize = Integer.parseInt(request.getParameter("pageSize"));
        //int pageNumber = Integer.parseInt(request.getParameter("pageNumber"));
        //String orderNum = request.getParameter("orderNum");          

        var paramList = new Dictionary<string, string>
            {
                {"Title", strTitle},
                {"Type", strType}
            };

        var list = AudioService.GetAudioList(paramList);
        var js = new JavaScriptSerializer().Serialize(list);
        context.Response.Write(js);
    }

    #endregion

    #region 获取音频分页列表信息

    /// <summary>
    ///     获取音频分页列表信息
    /// </summary>
    /// <param name="context"></param>
    private void GetPageList(HttpContext context)
    {
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
        //}

        //var a = s;
        var s = "";

        foreach (var name in context.Request.Params)
        {
            s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        }

        var b = s; // 1235

        var strTitle = context.Request.Params["Title"] ?? "";
        var strType = context.Request.Params["Type"] ?? "";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim();

        //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
        //{
        //    return;
        //}

        var pageSize = context.Request.Params["pageSize"] ?? "10";
        var pageNumber = context.Request.Params["pageNumber"] ?? "1";

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
                {"PageSize", pageSize},
                {"PageIndex", pageNumber}
            };

        var list = AudioService.GetAudioList(paramList);
        var js = list.Count > 0 ? new JavaScriptSerializer().Serialize(list) : "";
        var total = list.Count > 0 ? list[0].Count : 0;

        //需要返回的数据有总记录数和行数据  
        var json = "{\"total\":" + total + ",\"rows\":" + js + "}";

        context.Response.Write(json);
    }

    #endregion

    #region 清空音频数据

    /// <summary>
    /// 清空音频数据
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
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 音频标题修改

    /// <summary>
    /// 音频标题修改
    /// </summary>
    /// <param name="context"></param>
    private void AudioSave(HttpContext context)
    {
        var rst = "";

        try
        {
            var id = context.Request.Params["id"] ?? "0";
            var title = context.Request.Params["title"] ?? "";
            var ext = context.Request.Params["ext"] ?? "";
            var dir = context.Request.Params["dir"] ?? "";
            var fullname = context.Request.Params["fullname"] ?? "";

            if (!string.IsNullOrEmpty(title)) title = HttpUtility.UrlDecode(title);
            if (!string.IsNullOrEmpty(dir)) dir = HttpUtility.UrlDecode(dir);
            if (!string.IsNullOrEmpty(fullname)) fullname = HttpUtility.UrlDecode(fullname);

            var strNewFileName = Path.Combine(dir + "\\" + title + ext);

            if (File.Exists(fullname))
            {
                FileInfo fileInfo = new FileInfo(fullname);
                fileInfo.MoveTo(strNewFileName);
            }

            var count = AudioService.AudioRename(int.Parse(id), title, strNewFileName);
            rst = "{\"success\":true,\"Message\":\"音频文件名称修改成功！\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion


    #region 删除音频

    /// <summary>
    ///     删除音频
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void DeleteAudio(HttpContext context)
    {
        var id = context.Request.Params["ID"] ?? "0"; // 音频文件ID
        if (id.Length > 0) id = id.Trim();
        var rst = "";
        var audio = AudioService.GetAudioById(int.Parse(id));
        var i = AudioService.DelAudioById(int.Parse(id)); // 从数据库中删除音频
        if (i > 0)
        {
            rst = "{\"success\":true,\"Message\": \"音频删除成功！\"}";

            if (string.IsNullOrEmpty(audio.FullName)) return;

            try
            {
                if (File.Exists(audio.FullName))
                {
                    File.Delete(audio.FullName);
                }
            }
            catch (Exception ex)
            {
                rst = "{\"success\":false,\"Message\": \"" + ex.Message + "\"}";
            }
        }
        else
        {
            rst = "{\"success\":false,\"Message\": \"音频数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 批量删除音频

    /// <summary>
    ///     批量删除音频
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void BatchDelete(HttpContext context)
    {
        // 音频文件ID
        var ids = context.Request.Params["IDs"] ?? "";
        if (ids.Length > 0) ids = ids.Trim().TrimEnd(',');
        var rst = "";
        var mediaList = AudioService.GetAudioByIds(ids);
        var i = AudioService.DelAudioByIds(ids); // 从数据库中批量删除音频
        if (i > 0)
        {
            var strMessage = "音频数据删除成功！";

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
                    strMessage += "音频文件" + media.Title + "删除失败，原因：" + ex.Message.Replace('"', '\"');
                }
            }

            rst = "{\"success\":true,\"Message\": \"" + strMessage + "\"}";
        }
        else
        {
            rst = "{\"success\":false,\"Message\": \"音频数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion


}