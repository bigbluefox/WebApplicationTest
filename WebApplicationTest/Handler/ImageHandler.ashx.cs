using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Hsp.Test.IService;
using Hsp.Test.Service;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// ImageHandler 的摘要说明
    /// 图片处理
    /// </summary>
    public class ImageHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        ///     图片服务
        /// </summary>
        internal readonly IImageService ImageService = new ImageService();

        #region ProcessRequest

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            var strOperation = context.Request.Params["OPERATION"] ?? "";
            if (string.IsNullOrEmpty(strOperation)) strOperation = context.Request.Form["OPERATION"];

            switch (strOperation.ToUpper())
            {
                // 获取图片列表信息
                case "LIST":
                    GetImageList(context);
                    break;

                // 删除图片信息
                case "DELETE":
                    DeleteImage(context);
                    break;

                // 获取重复图片列表
                case "DUPLICATE":
                    GetDuplicateImageList(context);
                    break;


                // 图片上传
                case "UPLOAD":
                    //AttachmentAdd(context);
                    break;

                // 图片下载
                case "DOWNLOAD":
                    //AttachmentDownload(context);
                    break;


                default:
                    // 在线图片保存
                    //AttachmentEdit(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 获取图片列表信息

        /// <summary>
        ///     获取图片列表信息
        /// </summary>
        /// <param name="context"></param>
        private void GetImageList(HttpContext context)
        {
            //var strFileId = context.Request.Params["FID"] ?? "";
            //var strGroupId = context.Request.Params["GID"] ?? "";
            //var strTypeId = context.Request.Params["TID"] ?? "";

            //if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) &&
            //    string.IsNullOrWhiteSpace(strTypeId))
            //{
            //    return;
            //}

            //var paramList = new Dictionary<string, string>
            //{
            //    {"FID", strFileId},
            //    {"GID", strGroupId},
            //    {"TID", strTypeId}
            //};

            var list = ImageService.GetImageList();
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 获取重复图片列表

        /// <summary>
        ///     获取重复图片列表
        /// </summary>
        /// <param name="context"></param>
        private void GetDuplicateImageList(HttpContext context)
        {
            var list = ImageService.GetDuplicateImageList();
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 删除图片

        /// <summary>
        ///     删除图片
        /// </summary>
        /// <param name="context"></param>
        private void DeleteImage(HttpContext context)
        {
            // 图片文件编号
            var strFileId = context.Request.Params["ID"] ?? "0";

            var rst = "";
            var image = ImageService.GetImageById(int.Parse(strFileId));
            var i = ImageService.DelImageById(int.Parse(strFileId)); // 从数据库中删除图片
            if (i > 0)
            {
                rst = "{\"IsSuccess\":true,\"Message\": \"图片删除成功！\"}";

                if (string.IsNullOrEmpty(image.FullName)) return;

                try
                {
                    if (File.Exists(image.FullName))
                    {
                        File.Delete(image.FullName);
                    }
                }
                catch (Exception ex)
                {
                    rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                }
            }
            else
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"图片数据删除失败！\"}";
            }

            context.Response.Write(rst);
        }

        #endregion
    }
}