using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;
using Hsp.Test.Service;
using Hsp.Test.IService;
using Hsp.Test.Model;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// 文件上传处理程序
    /// </summary>
    public class UploadHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        // Define a destination
        private string targetFolder = "/Uploads/";
        // Relative to the root and should match the upload folder in the uploader script

        /// <summary>
        /// 附件虚拟目录
        /// </summary>
        internal string VirtualDirectory = (ConfigurationManager.AppSettings["VirtualDirectory"] ?? "").Trim();

        /// <summary>
        /// 有效文件类型
        /// </summary>
        internal string ValidFileType = (ConfigurationManager.AppSettings["UpFileType"] ?? "").Trim();

        /// <summary>
        /// 有效图片类型
        /// </summary>
        internal string ValidImageType = (ConfigurationManager.AppSettings["UpImageType"] ?? "").Trim();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            //context.Response.Write("Hello World");

            try
            {
                var validFileType = ValidFileType;

                if (string.IsNullOrEmpty(validFileType))
                {
                    validFileType = "*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;";
                }

                //var VirtualDirectory =
                //    System.Configuration.ConfigurationManager.AppSettings["VirtualDirectory"].Trim();
                //var strUpFileTypes = System.Configuration.ConfigurationManager.AppSettings["UpFileType"].Trim();
                //var strRootPath = System.Configuration.ConfigurationManager.AppSettings["RootPath"].Trim();

                //var id = context.Request.Form["ID"]; // 传递变量
                //var gid = context.Request.Form["GID"];
                //var tid = context.Request.Form["TID"];

                // 是否新文件
                //var isNew = string.IsNullOrEmpty(context.Request.Form["ID"]) && string.IsNullOrEmpty(context.Request["ID"]) ? true : false;

                // 附件ID
                var strFileId = !string.IsNullOrEmpty(context.Request.Form["ID"])
                    ? context.Request.Form["ID"]
                    : !string.IsNullOrEmpty(context.Request["ID"])
                        ? context.Request["ID"]
                        : Guid.NewGuid().ToString().ToUpper();

                // 附件类型ID
                var strTypeId = !string.IsNullOrEmpty(context.Request.Form["TID"])
                    ? context.Request.Form["TID"]
                    : context.Request["TID"];

                // 附件分组ID
                var strGroupId = !string.IsNullOrEmpty(context.Request.Form["GID"])
                    ? context.Request.Form["GID"]
                    : context.Request["GID"];

                var strUserId = context.Request["UID"];
                var strUserName = HttpUtility.UrlDecode(context.Request["UName"]);

                var strDate = DateTime.Now.ToString("yyyy-MM-dd");
                var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为文件子目录
                //var strUploadPath = strRootPath + strSubdirectories; Params

                var strParams = context.Request.Params["Params"];

                var filename = context.Request.Form["filename"];
                HttpPostedFile file = context.Request.Files["fileData"];
                file = file ?? context.Request.Files["file_data"];

                if (file != null)
                {
                    var fileLength = file.ContentLength; // 文件长度
                    var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                    filename = string.IsNullOrEmpty(filename) ? file.FileName : filename; // 文件名称

                    if (filename.LastIndexOf("\\", StringComparison.Ordinal) > -1)
                    {
                        filename = filename.Substring(filename.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    }

                    // application/octet-stream 
                    // application/vnd.openxmlformats-officedocument.wordprocessingml.document

                    // 文件类型
                    string strFileExt = "." +
                        file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                            .ToLower();

                    // 检查文件类型是否合法
                    if (IsValidType(validFileType, strFileExt))
                    {
                        // 保存文件
                        //string strFilePath = targetFolder;
                        //string strFilePath = strSubdirectories + strSaveName;

                        var strSaveName = string.IsNullOrEmpty(strFileExt)
                            ? strFileId
                            : strFileId + strFileExt;

                        VirtualDirectory = VirtualDirectory.StartsWith("\\")
                            ? VirtualDirectory
                            : "\\" + VirtualDirectory;
                        VirtualDirectory = VirtualDirectory.EndsWith("\\")
                            ? VirtualDirectory
                            : VirtualDirectory + "\\";

                        strSubdirectories = (strSubdirectories.EndsWith("\\")
                            ? strSubdirectories
                            : strSubdirectories + "\\");

                        //strSaveName = filename;

                        // 检查文件目录
                        FileHelper.FilePathCheck(HttpContext.Current.Server.MapPath(VirtualDirectory + strSubdirectories));

                        // 保存文件
                        string fullFileName = strSubdirectories + strSaveName;
                        file.SaveAs(context.Server.MapPath(VirtualDirectory + fullFileName));

                        //context.Session["ExtName"] = strFileExt;
                        //context.Session["FileLength"] = fileLength;
                        //context.Session["CurrentFileUrl"] = fullFileName;
                        //context.Session["CurrentFileName"] = file.FileName;
                        //context.Session["CurrentFile"] = StreamToBytes(file.InputStream);
                        //context.Session["FullFileName"] = context.Server.MapPath(fullFileName);

                        //SELECT     TOP (200) FileId, TypeId, GroupId, FileName, FileDesc, FileExt, FileSize, ContentType, FileUrl, FilePath, Creator, CreatorName, CreateTime
                        //FROM         Attachments

                        //strFileName:4
                        //strUrl:/Files/2017/02/27/A166A427-5067-4072-9950-F7347739933C.txt // 应该去掉Files
                        //FilePath：2017\02\27\A166A427-5067-4072-9950-F7347739933C.txt //加码

                        var strUrl = "/" + strSubdirectories.Replace("\\", "/") + strSaveName;
                        var strFileName = filename.Replace(strFileExt, "").Trim('.');

                        var strFilePath = HttpUtility.UrlEncode(strSubdirectories + strSaveName);

                        //Utility.WriteLog("文件目录：",
                        //    fullFileName + "，文件大小：" + fileLength + "，文件类型：" + strFileExt + "，文件名称：" +
                        //    strFileName + "，URL：" + strUrl + "，上传人：" + strUserName);

                        // 保存文件信息
                        var model = new FileModel();
                        model.FileId = strFileId;
                        model.TypeId = strTypeId;
                        model.GroupId = strGroupId;
                        model.FileName = strFileName;
                        model.FileDesc = "";
                        model.FileExt = strFileExt;
                        model.FileSize = fileLength;
                        model.ContentType = contentType;
                        model.FileUrl = strUrl;
                        model.FilePath = strFilePath;
                        model.Params = strParams;
                        model.Creator = strUserId;
                        model.CreatorName = strUserName;

                        AttachmentService.AddFile(model);

                        context.Response.Write("1*" + HttpUtility.UrlEncode(strUrl));
                        //context.Response.Write("{\"IsSuccess\":\"true\",\"msg\":\"上传文件成功!\"}");
                    }
                    else
                    {
                        context.Response.Write("0*不支持该文件类型");
                        //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传文件类型错误（*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.ceb;*.wps;*.dps;*.et)\"}");
                    }
                }
                else
                {
                    context.Response.Write("0*文件不存在");
                    //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传失败，文件不存在！\"}");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("0*" + ex.Message.Replace('"', '\"'));
                //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传失败，异常信息为：" + ex.Message.Replace('"', '\"') + "！\"}");
            }
        }

        #region 将 Stream 转成 byte[]

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        #region 验证附件类型是否是有效附件

        /// <summary>
        /// 验证附件类型是否是有效附件
        /// *.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.jpg;*.zip;*.rar;*.txt;*.wps;*.et;*.dps;*.rtf;
        /// *.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;
        /// </summary>
        /// <param name="validType">有效文件类型</param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        public bool IsValidType(string validType, string filetype)
        {
            filetype = filetype.Trim('.');
            //var idx = validType.IndexOf("." + filetype + ";", StringComparison.Ordinal) > -1;
            return validType.IndexOf("." + filetype + ";", StringComparison.Ordinal) > -1;
        }

        public string BrowserName()
        {
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            return bc.Browser;
        }

        #endregion

        #region 如果目录不存在，建立

        /// <summary>
        /// 如果目录不存在，建立
        /// </summary>
        /// <param name="dirName">目录名称</param>
        //public static void FilePathCheck(string dirName)
        //{
        //    var directoryName = Path.GetDirectoryName(dirName);
        //    if (directoryName == null) return;
        //    String path = directoryName.TrimEnd('\\');
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //}

        #endregion

        #region IsReusable

        /// <summary>
        /// IsReusable
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
}