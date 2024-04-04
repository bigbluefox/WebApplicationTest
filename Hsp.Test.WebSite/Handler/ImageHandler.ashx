<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;

public class ImageHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();
        var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            //// 图像信息检索
            //case "RETRIEVAL":
            //    AudioRetrieval(context);
            //    break;

            //// 图像标题修改
            //case "SAVE":
            //    AudioSave(context);
            //    break;

            //// 清空数据表
            //case "EMPTYING":
            //    EmptyingTable(context);
            //    break;

            //// 图像列表信息
            //case "LIST":
            //    GetAudioList(context);
            //    break;

            //// 单个删除图像
            //case "DELETE":
            //    DeleteAudio(context);
            //    break;

            //// 批量删除图像
            //case "BATCHDELETE":
            //    BatchDelete(context);
            //    break;

            //// 图像列表信息
            //case "PAGELIST":
            //    GetPageList(context);
            //    break;

            default:
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}