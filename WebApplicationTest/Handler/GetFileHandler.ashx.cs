using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// GetFileHandler 的摘要说明
    /// </summary>
    public class GetFileHandler : IHttpHandler
    {
        /// <summary>
        /// 附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        #region ProcessRequest

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //与输出内容类型无关
            context.Response.ContentType = "application/json";
            //context.Response.ContentType = "text/javascript";
            //context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string strOperation = context.Request.Params["OPERATION"].Trim();

            switch (strOperation.ToUpper())
            {
                // 获取文件列表信息
                case "GETFILELIST":
                    GetFileList(context);
                    break;

                // 删除文件
                case "DELETEFILE":
                    DeleteFile(context);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 获取文件列表信息

        /// <summary>
        /// 获取文件列表信息
        /// </summary>
        /// <param name="context"></param>
        private void GetFileList(HttpContext context)
        {
            var strFileId = context.Request.Params["FID"];
            var strGroupId = context.Request.Params["GID"];
            var strTypeId = context.Request.Params["TID"];

            if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) && string.IsNullOrWhiteSpace(strTypeId))
            {
                return;
            }
            var paramList = new Dictionary<string, string>
            {
                {"FID", strFileId},
                {"GID", strGroupId},
                {"TID", strTypeId}
            };

            List<FileModel> list = AttachmentService.GetFileList(paramList);
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 删除文件
        /// </summary>PATH
        /// <param name="context"></param>
        private void DeleteFile(HttpContext context)
        {
            var strFilePath = context.Request.Params["PATH"].Trim();
            //var strRootPath = System.Configuration.ConfigurationManager.AppSettings["RootPath"].Trim();
            var strVirtualDirectory =
                System.Configuration.ConfigurationManager.AppSettings["VirtualDirectory"].Trim();

            strVirtualDirectory = strVirtualDirectory.StartsWith("\\")
                ? strVirtualDirectory
                : "\\" + strVirtualDirectory;
            strVirtualDirectory = strVirtualDirectory.EndsWith("\\")
                ? strVirtualDirectory
                : strVirtualDirectory + "\\";

            var paramList = new Dictionary<string, string>
            {
                {"FID", context.Request.Params["FID"]},
                {"GID", context.Request.Params["GID"]},
                {"TID", context.Request.Params["TID"]}
            };

            var rst = "";

            //从数据库中删除文件
            var i = AttachmentService.DeleteFile(paramList);
            if (i > 0)
            {
                rst = "{\"IsSuccess\":true,\"Message\": \"文件删除成功！\"}";

                if (string.IsNullOrEmpty(strFilePath)) return;

                // 删除上传文件
                strFilePath = strVirtualDirectory + HttpUtility.UrlDecode(strFilePath);

                try
                {
                    strFilePath = HttpContext.Current.Server.MapPath(strFilePath);

                    if (File.Exists(strFilePath))
                    {
                        File.Delete(strFilePath);
                    }
                }
                catch (Exception ex)
                {
                    rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                    //throw;
                }
            }
            else
            {
                //context.Response.Write("文件删除失败！");
                rst = "{\"IsSuccess\":false,\"Message\": \"文件删除失败！\"}";
            }

            context.Response.Write(rst);
        }

        #endregion

        public bool IsReusable
        {
            get { return false; }
        }
    }
}