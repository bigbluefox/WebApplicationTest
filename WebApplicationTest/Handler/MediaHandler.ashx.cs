using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using Hsp.Test.IService;
using Hsp.Test.Service;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// MediaHandler 的摘要说明
    /// 媒体处理程序
    /// </summary>
    public class MediaHandler : IHttpHandler
    {
        /// <summary>
        ///     媒体服务
        /// </summary>
        internal readonly IMediaService MediaService = new MediaService();


        #region ProcessRequest

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            var strOperation = context.Request.Params["OPERATION"] ?? "";
            if (string.IsNullOrEmpty(strOperation)) strOperation = context.Request.Form["OPERATION"];

            switch (strOperation.ToUpper())
            {
                // 媒体列表信息
                case "LIST":
                    GetMediaList(context);
                    break;

                // 单个删除媒体
                case "DELETE":
                    DeleteMedia(context);
                    break;

                // 批量删除媒体
                case "BATCHDELETE":
                    BatchDelete(context);
                    break;

                //// 媒体下载
                //case "DOWNLOAD":
                //    AttachmentDownload(context);
                //    break;

                //// 媒体批量下载
                //case "BATCHDOWNLOAD":
                //    BatchDownload(context);
                //    break;

                default:
                    // 在线媒体保存
                    //AttachmentEdit(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 获取媒体列表信息

        /// <summary>
        ///     获取媒体列表信息
        /// </summary>
        /// <param name="context"></param>
        private void GetMediaList(HttpContext context)
        {
            var strTitle = context.Request.Params["Title"] ?? "";
            var strType = context.Request.Params["Type[]"] ?? "";
            if (strTitle.Length > 0) strTitle = strTitle.Trim();
            if (strType.Length > 0) strType = strType.Trim();

            //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
            //{
            //    return;
            //}

            var paramList = new Dictionary<string, string>
            {
                {"Title", strTitle},
                {"Type", strType}
            };

            var list = MediaService.GetMediaList(paramList);
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 删除媒体

        /// <summary>
        ///     删除媒体
        /// </summary>
        /// PATH
        /// <param name="context"></param>
        private void DeleteMedia(HttpContext context)
        {
            // 媒体文件ID
            var strMediaId = context.Request.Params["ID"] ?? "0";
            if (strMediaId.Length > 0) strMediaId = strMediaId.Trim();
            var rst = "";
            var media = MediaService.GetMediaById(int.Parse(strMediaId));
            var i = MediaService.DelMediaById(int.Parse(strMediaId)); // 从数据库中删除媒体
            if (i > 0)
            {
                rst = "{\"IsSuccess\":true,\"Message\": \"媒体删除成功！\"}";

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
                    rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                }
            }
            else
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"媒体数据删除失败！\"}";
            }

            context.Response.Write(rst);
        }

        #endregion

        #region 批量删除媒体

        /// <summary>
        ///     批量删除媒体
        /// </summary>
        /// PATH
        /// <param name="context"></param>
        private void BatchDelete(HttpContext context)
        {
            // 媒体文件ID
            var strMediaIds = context.Request.Params["IDs"] ?? "";
            if (strMediaIds.Length > 0) strMediaIds = strMediaIds.Trim().TrimEnd(',');
            var rst = "";
            var mediaList = MediaService.GetMediaByIds(strMediaIds);
            var i = MediaService.DelMediaByIds(strMediaIds); // 从数据库中批量删除媒体
            if (i > 0)
            {
                var strMessage = "媒体数据删除成功！";

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
                        strMessage += "媒体文件" + media.Title + "删除失败，原因：" + ex.Message.Replace('"', '\"');
                    }
                }

                rst = "{\"IsSuccess\":true,\"Message\": \"" + strMessage + "\"}";
            }
            else
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"媒体数据删除失败！\"}";
            }

            context.Response.Write(rst);
        }

        #endregion
    }
}