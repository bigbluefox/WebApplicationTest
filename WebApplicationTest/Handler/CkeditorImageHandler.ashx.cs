using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// CkeditorImageHandler 的摘要说明
    /// </summary>
    public class CkeditorImageHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 图片文件存储根目录
        /// </summary>
        internal readonly string TargetFolder = "\\Uploads\\";

        /// <summary>
        /// context
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            // 新版Ckeditor需要返回Json数据
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            //context.Response.Write("Hello World");

            try
            {
                var strDate = DateTime.Now.ToString("yyyy-MM-dd");
                var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为文件子目录
                HttpPostedFile upload = context.Request.Files["upload"];

                if (upload != null)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    var name = fileName;

                    // 附件类型

                    var strFileExt = "." +
                                     fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                                         .ToLower();
                    fileName = Guid.NewGuid().ToString().ToUpper() + strFileExt;

                    var filePhysicalPath = context.Server.MapPath(TargetFolder + strSubdirectories + fileName);
                        //我把它保存在网站根目录的 upload 文件夹

                    FileHelper.FilePathCheck(filePhysicalPath); // 检查附件目录

                    upload.SaveAs(filePhysicalPath);

                    var url = TargetFolder.Replace("\\", "/") + strSubdirectories.Replace("\\", "/") + fileName;

                    //var ckEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
                    //context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" + url +
                    //    "\");</script>");

                    context.Response.Write("{\"fileName\":\"" + name + "\",\"uploaded\": 1,\"url\": \"" + url + "\"}");
                }
                else
                {
                    context.Response.Write("{\"uploaded\": 0,\"error\": {\"message\": \"上传文件为空！\"}}");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"uploaded\": 0,\"error\": {\"message\": \"" + ex.Message + "\"}}");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}