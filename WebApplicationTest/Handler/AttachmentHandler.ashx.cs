using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

namespace WebApplicationTest.Handler
{
    /// <summary>
    ///     AttachmentHandler 的摘要说明
    /// </summary>
    public class AttachmentHandler : IHttpHandler, IRequiresSessionState
    {
        #region ProcessRequest

        /// <summary>
        ///     ProcessRequest
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
                // 获取附件列表信息
                case "ATTACHMENTLIST":
                    GetFileList(context);
                    break;

                // 删除附件
                case "DELETEFILE":
                    DeleteFile(context);
                    break;

                // 附件上传
                case "UPLOAD":
                    AttachmentAdd(context);
                    break;

                // 附件下载
                case "DOWNLOAD":
                    AttachmentDownload(context);
                    break;

                // 附件批量下载
                case "BATCHDOWNLOAD":
                    BatchDownload(context);
                    break;

                default:
                    // 在线附件保存
                    AttachmentEdit(context);
                    break;
            }
        }

        #endregion

        #region IsReusable

        /// <summary>
        ///     IsReusable
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 获取附件列表信息

        /// <summary>
        ///     获取附件列表信息
        /// </summary>
        /// <param name="context"></param>
        private void GetFileList(HttpContext context)
        {
            var strFileId = context.Request.Params["FID"] ?? "";
            var strGroupId = context.Request.Params["GID"] ?? "";
            var strTypeId = context.Request.Params["TID"] ?? "";

            if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) &&
                string.IsNullOrWhiteSpace(strTypeId)) return;

            var paramList = new Dictionary<string, string>
            {
                {"FID", strFileId},
                {"GID", strGroupId},
                {"TID", strTypeId}
            };

            var list = AttachmentService.GetFileList(paramList);
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 删除附件

        /// <summary>
        ///     删除附件
        /// </summary>
        /// PATH
        /// <param name="context"></param>
        private void DeleteFile(HttpContext context)
        {
            // 附件文件ID
            var strFileId = context.Request.Params["FID"] ?? "";

            // 附件分组ID
            var strGroupId = context.Request.Params["GID"] ?? "";

            // 附件类型ID
            var strTypeId = context.Request.Params["TID"] ?? "";

            var strFilePath = context.Request.Params["PATH"].Trim();
            //var strRootPath = System.Configuration.ConfigurationManager.AppSettings["RootPath"].Trim();
            //var VirtualDirectory =
            //    System.Configuration.ConfigurationManager.AppSettings["VirtualDirectory"].Trim();

            VirtualDirectory = VirtualDirectory.StartsWith("\\")
                ? VirtualDirectory
                : "\\" + VirtualDirectory;
            VirtualDirectory = VirtualDirectory.EndsWith("\\")
                ? VirtualDirectory
                : VirtualDirectory + "\\";

            if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) &&
                string.IsNullOrWhiteSpace(strTypeId))
            {
                return;
            }

            var paramList = new Dictionary<string, string>
            {
                {"FID", strFileId},
                {"GID", strGroupId},
                {"TID", strTypeId}
            };

            var rst = "";

            //从数据库中删除附件
            var deleteFile = AttachmentService.GetFileModel(strFileId);
            var i = AttachmentService.DeleteFile(paramList);
            if (i > 0)
            {
                rst = "{\"IsSuccess\":true,\"Message\": \"附件删除成功！\"}";

                if (string.IsNullOrEmpty(strFilePath)) return;

                // 删除上传附件
                strFilePath = VirtualDirectory + HttpUtility.UrlDecode(strFilePath);

                try
                {
                    strFilePath = HttpContext.Current.Server.MapPath(strFilePath);

                    if (File.Exists(strFilePath))
                    {
                        File.Delete(strFilePath);

                        #region 记录附件操作日志

                        //FileLog log = new FileLog();

                        //log.LogId = Guid.NewGuid().ToString();
                        //log.GroupId = deleteFile.GroupId;
                        //log.TypeId = deleteFile.TypeId;
                        //log.Operation = "删除附件，文件名称：" + deleteFile.FileName + deleteFile.FileExt + "，文件ID：" + strFileId;
                        //log.ClientInfo = String.Format("IP: {0}, 浏览器：{1}", context.Request.UserHostAddress,
                        //    context.Request.UserAgent);
                        //log.Creator = CurrentUser.Userno.ToString();
                        //log.CreatorName = CurrentUser.Username;
                        //log.CreateTime = DateTime.Now;

                        //AttachmentService.AttachmentLog(log);

                        #endregion
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
                //context.Response.Write("附件删除失败！");
                rst = "{\"IsSuccess\":false,\"Message\": \"附件删除失败！\"}";
            }

            context.Response.Write(rst);
        }

        #endregion

        #region 附件下载

        /// <summary>
        ///     附件下载
        /// </summary>
        /// <param name="context"></param>
        private void AttachmentDownload(HttpContext context)
        {
            //Request.Browser.MajorVersion.ToString();//获取客户端浏览器的（主）版本号 

            //Request.Browser.Version.ToString();//获取客户端浏览器的完整版本号 
            //Request.Browser.Platform.ToString();//获取客户端使用平台的名字 
            //Request.UserHostAddress.ToString(); //获取远程客户端主机IP

            //HttpRequest.Url 获取有关当前请求的URL的信息。 
            //HttpRequest.UrlReferrer 获取有关客户端上次请求的URL的信息,该请求链接到当前的URL。 
            //HttpRequest.UserAgent 获取客户端浏览器的原始用户代理信息。 
            //HttpRequest.UserHostAddress 获取远程客户端的 IP 主机地址。 
            //HttpRequest.UserHostName 获取远程客户端的 DNS 名称。 
            //HttpRequest.UserLanguages 获取客户端语言首选项的排序字符串数组。

            //System.Text.StringBuilder strLabel = new System.Text.StringBuilder();
            //   HttpBrowserCapabilities bc = context.Request.Browser;
            //   //strLabel.Append("您的浏览器的分辨率为：");
            //   //strLabel.Append(context.Request.Form["WidthPixel"]);
            //   //strLabel.Append("×");
            //   //strLabel.Append(context.Request.Form["HeightPixel"]);
            //   //strLabel.Append("");
            //   strLabel.Append("浏览器基本信息：");
            //   strLabel.Append("Type = " + bc.Type + System.Environment.NewLine);
            //   strLabel.Append("Name = " + bc.Browser + System.Environment.NewLine);
            //   strLabel.Append("Version = " + bc.Version + System.Environment.NewLine);
            //   strLabel.Append("Major Version = " + bc.MajorVersion + System.Environment.NewLine);
            //   strLabel.Append("Minor Version = " + bc.MinorVersion + System.Environment.NewLine);
            //   strLabel.Append("Platform = " + bc.Platform + System.Environment.NewLine);
            //   strLabel.Append("Is Beta = " + bc.Beta + System.Environment.NewLine);
            //   strLabel.Append("Is Crawler = " + bc.Crawler + System.Environment.NewLine);
            //   strLabel.Append("Is AOL = " + bc.AOL + System.Environment.NewLine);
            //   strLabel.Append("Is Win16 = " + bc.Win16 + System.Environment.NewLine);
            //   strLabel.Append("Is Win32 = " + bc.Win32 + System.Environment.NewLine);
            //   strLabel.Append("支持 Frames = " + bc.Frames + System.Environment.NewLine);
            //   strLabel.Append("支持 Tables = " + bc.Tables + System.Environment.NewLine);
            //   strLabel.Append("支持 Cookies = " + bc.Cookies + System.Environment.NewLine);
            //   strLabel.Append("支持 VB Script = " + bc.VBScript + System.Environment.NewLine);
            //   strLabel.Append("支持 JavaScript = " + bc.JavaScript + System.Environment.NewLine);
            //   strLabel.Append("支持 Java Applets = " + bc.JavaApplets + System.Environment.NewLine);
            //   strLabel.Append("支持 ActiveX Controls = " + bc.ActiveXControls + System.Environment.NewLine);
            //   strLabel.Append("CDF = " + bc.CDF + System.Environment.NewLine);
            //   strLabel.Append("W3CDomVersion = " + bc.W3CDomVersion.ToString() + System.Environment.NewLine);
            //   strLabel.Append("UserAgent = " + context.Request.UserAgent + System.Environment.NewLine);
            //   strLabel.Append("UserLanguages = " + context.Request.UserLanguages[0].ToString() + System.Environment.NewLine);
            //   strLabel.Append(System.Environment.NewLine);
            //   strLabel.Append("客户端计算机基本配置：");
            //   strLabel.Append("UserHostName = " + context.Request.UserHostName + System.Environment.NewLine);
            //   strLabel.Append("UserHostAddress = " + context.Request.UserHostAddress + System.Environment.NewLine);
            //   strLabel.Append("PDF 6.0 插件是否安装 = " + context.Request.Form["PDF"] + System.Environment.NewLine);
            //   //Label1.Text = strLabel.ToString();

            //   var lbl = strLabel.ToString();

            try
            {
                var strFileId = context.Request.Params["FID"];
                if (string.IsNullOrWhiteSpace(strFileId))
                {
                    context.Response.StatusCode = 400; /* Bad Request */
                    return;
                }

                VirtualDirectory = VirtualDirectory.StartsWith("\\")
                    ? VirtualDirectory
                    : "\\" + VirtualDirectory;
                VirtualDirectory = VirtualDirectory.EndsWith("\\")
                    ? VirtualDirectory
                    : VirtualDirectory + "\\";

                var file = AttachmentService.GetFileModel(strFileId);

                if (file != null)
                {
                    var fileName = file.FileName + file.FileExt;
                    var filePath =
                        HttpContext.Current.Server.MapPath(VirtualDirectory + HttpUtility.UrlDecode(file.FilePath));
                    FileHelper.DownLoadold(filePath, fileName, file.FileExt.Trim('.'), BrowserName());
                }

                context.Response.StatusCode = 200; /* OK */
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400; /* Bad Request */
            }
        }

        #endregion

        #region 附件批量下载

        /// <summary>
        ///     附件批量下载
        /// </summary>
        /// <param name="context"></param>
        private void BatchDownload(HttpContext context)
        {
            var rst = "";

            try
            {
                var strFileId = context.Request.Params["FID"] ?? "";
                var strGroupId = context.Request.Params["GID"] ?? "";
                var strTypeId = context.Request.Params["TID"] ?? "";

                if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) &&
                    string.IsNullOrWhiteSpace(strTypeId))
                {
                    return;
                }

                VirtualDirectory = VirtualDirectory.StartsWith("\\")
                    ? VirtualDirectory
                    : "\\" + VirtualDirectory;
                VirtualDirectory = VirtualDirectory.EndsWith("\\")
                    ? VirtualDirectory
                    : VirtualDirectory + "\\";

                // 站点根目录
                var rootPath = HttpContext.Current.Server.MapPath("/");

                // 需要压缩的文件的临时目录
                var tempPath = VirtualDirectory + "Temp" + "\\" + strGroupId;

                // 压缩后的文件路径（绝对路径）
                var ZipedPath = @"D:\MSO\Processist.MSO-v3.0\Processist.MSO.Website\FileService\Attachment";
                ZipedPath = HttpContext.Current.Server.MapPath(VirtualDirectory);
                //ZipedPath = ZipedPath.EndsWith("\\")
                //    ? ZipedPath
                //    : ZipedPath + "\\";

                // 检查附件目录
                FileHelper.FilePathCheck(ZipedPath);

                // 需要压缩的文件夹（绝对路径）
                var DirectoryToZip = @"D:\MSO\Processist.MSO-v3.0\Processist.MSO.Website\FileService\Attachment\2017";
                DirectoryToZip = HttpContext.Current.Server.MapPath(tempPath);
                DirectoryToZip = DirectoryToZip.EndsWith("\\")
                    ? DirectoryToZip
                    : DirectoryToZip + "\\";

                // 检查附件目录
                FileHelper.FilePathCheck(DirectoryToZip);

                // 压缩后的文件名称（文件名，默认 同源文件夹同名）
                var ZipedFileName = strGroupId;

                // 处理文件

                var paramList = new Dictionary<string, string>
                {
                    {"FID", strFileId},
                    {"GID", strGroupId},
                    {"TID", strTypeId}
                };
                var list = AttachmentService.GetFileList(paramList);

                if (list.Count > 0)
                {
                    foreach (var file in list)
                    {
                        var sourcePath =
                            HttpContext.Current.Server.MapPath(VirtualDirectory + HttpUtility.UrlDecode(file.FilePath));
                        var fileName = file.FileName + "_" + file.CreatorName + file.FileExt;
                        var targetPath = DirectoryToZip + fileName;

                        var isrewrite = true; // true=覆盖已存在的同名文件,false则反之

                        // 检查只有存在的文件才处理
                        if (File.Exists(sourcePath))
                        {
                            File.Copy(sourcePath, targetPath, isrewrite);
                        }
                    }

                    // 压缩文件目录
                    FileHelper.ZipDirectory(DirectoryToZip, ZipedPath, ZipedFileName);

                    // 文件名称（默认同源文件名称相同）
                    var ZipFileName = string.IsNullOrEmpty(ZipedFileName)
                        ? ZipedPath + new DirectoryInfo(DirectoryToZip).Name + ".zip"
                        : ZipedPath + ZipedFileName + ".zip";

                    var url = ZipFileName.Replace(rootPath, "").Replace("\\", "/");
                    url = url.StartsWith("/") ? url : "/" + url;

                    Random ra = new Random(10);
                    url = url + "?rnd=" + ra.Next();
                    rst = "{\"IsSuccess\":true,\"Message\": \"附件批量下载成功！\", \"Url\":\"" + url + "\"}";
                }
                else
                {
                    rst = "{\"IsSuccess\":false,\"Message\": \"没有查找到需要下载的文件！\"}";
                }
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
            }

            context.Response.Write(rst);
        }

        #endregion

        #region 验证附件类型是否是有效附件

        /// <summary>
        ///     验证附件类型是否是有效附件
        ///     *.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.jpg;*.zip;*.rar;*.txt;*.wps;*.et;*.dps;*.rtf;
        /// </summary>
        /// <param name="filetype"></param>
        /// <returns></returns>
        public bool IsValidType(string filetype)
        {
            filetype = filetype.Trim('.');
            return ValidFileType.IndexOf("." + filetype + ";", StringComparison.Ordinal) > -1;

            //filetype == "doc" || filetype == "docx" || filetype == "xls" || filetype == "xlsx" ||
            //   filetype == "pdf" ||
            //   filetype == "ppt" || filetype == "pptx" || filetype == "ceb" || filetype == "wps" || filetype == "ceb" ||
            //   filetype == "dps" ||
            //   filetype == "et" || filetype == "jpg" || filetype == "png" || filetype == "txt" || filetype == "rar" ||
            //   filetype == "zip";
        }

        #endregion

        #region 如果目录不存在，建立

        /// <summary>
        ///     如果目录不存在，建立
        /// </summary>
        /// <param name="dirName">目录名称</param>
        /// <summary>
        ///     当前登录的用户对象
        /// </summary>
        //public AUTHUSER CurrentUser
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["SESSION_CURRENT_USER"] != null)
        //        {
        //            return HttpContext.Current.Session["SESSION_CURRENT_USER"] as AUTHUSER;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    set { HttpContext.Current.Session["SESSION_CURRENT_USER"] = value; }
        //}

        #endregion

        #region 浏览器名称

        /// <summary>
        /// 浏览器名称
        /// </summary>
        /// <returns></returns>
        public string BrowserName()
        {
            var bc = HttpContext.Current.Request.Browser;
            return bc.Browser;
        }

        #endregion

        #region 参数配置

        /// <summary>
        ///     附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        internal string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"].Trim();

        /// <summary>
        ///     有效文件类型
        /// </summary>
        internal string ValidFileType = ConfigurationManager.AppSettings["UpFileType"].Trim();

        #endregion

        #region 附件上传

        /// <summary>
        ///     附件上传(添加)
        /// </summary>
        /// <param name="context"></param>
        private void AttachmentAdd(HttpContext context)
        {
            try
            {
                //var strUpFileTypes = System.Configuration.ConfigurationManager.AppSettings["UpFileType"].Trim();
                //var strRootPath = System.Configuration.ConfigurationManager.AppSettings["RootPath"].Trim();

                //var id = context.Request.Form["ID"]; // 传递变量
                //var gid = context.Request.Form["GID"];
                //var tid = context.Request.Form["TID"];

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

                var strUserId = context.Request["UID"] ?? "";
                //if (string.IsNullOrEmpty(strUserId)) strUserId = CurrentUser.Userno.ToString();

                var strUserName = HttpUtility.UrlDecode(context.Request["UName"]);
                //if (string.IsNullOrEmpty(strUserName)) strUserName = CurrentUser.Username;

                var strDate = DateTime.Now.ToString("yyyy-MM-dd");
                var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为附件子目录

                var strParams = context.Request.Params["Params"];
                var filename = context.Request.Form["filename"];

                var file = context.Request.Files["fileData"];

                if (file != null)
                {
                    var fileLength = file.ContentLength; // 附件长度
                    var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                    filename = string.IsNullOrEmpty(filename) ? file.FileName : filename; // 附件名称 D:\新建文件夹 (4)\99A坦克.jpg


                    if (filename.LastIndexOf("\\", StringComparison.Ordinal) > -1)
                    {
                        filename = filename.Substring(filename.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    }


                    // 附件类型
                    var strFileExt = "." +
                                     file.FileName.Substring(
                                         file.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                                         .ToLower();

                    // 检查附件类型是否合法
                    if (IsValidType(strFileExt))
                    {
                        // 保存附件
                        var strSaveName = string.IsNullOrEmpty(strFileExt)
                            ? strFileId
                            : strFileId + strFileExt;

                        VirtualDirectory = VirtualDirectory.StartsWith("\\")
                            ? VirtualDirectory
                            : "\\" + VirtualDirectory;
                        VirtualDirectory = VirtualDirectory.EndsWith("\\")
                            ? VirtualDirectory
                            : VirtualDirectory + "\\";

                        strSubdirectories = strSubdirectories.EndsWith("\\")
                            ? strSubdirectories
                            : strSubdirectories + "\\";

                        // 检查附件目录
                        FileHelper.FilePathCheck(HttpContext.Current.Server.MapPath(VirtualDirectory + strSubdirectories));

                        // 保存附件
                        var fullFileName = strSubdirectories + strSaveName;
                        file.SaveAs(context.Server.MapPath(VirtualDirectory + fullFileName));

                        var strUrl = "/" + strSubdirectories.Replace("\\", "/") + strSaveName;
                        var strFileName = filename.Replace(strFileExt, "").Trim('.');

                        var strFilePath = HttpUtility.UrlEncode(strSubdirectories + strSaveName);

                        //Utility.WriteLog("附件目录：",
                        //    fullFileName + "，附件大小：" + fileLength + "，附件类型：" + strFileExt + "，附件名称：" +
                        //    strFileName + "，URL：" + strUrl + "，上传人：" + strUserName);

                        // 保存附件信息
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

                        var rst = AttachmentService.AddFile(model);

                        if (rst > 0)
                        {
                            #region 记录附件操作日志

                            //FileLog log = new FileLog();

                            //log.LogId = Guid.NewGuid().ToString();
                            //log.GroupId = strGroupId;
                            //log.TypeId = strTypeId;
                            //log.Operation = "添加附件，文件名称：" + strFileName + strFileExt + "，文件ID：" + model.FileId;
                            //log.ClientInfo = String.Format("IP: {0}, 浏览器：{1}", context.Request.UserHostAddress,
                            //    context.Request.UserAgent);
                            //log.Creator = CurrentUser.Userno.ToString();
                            //log.CreatorName = CurrentUser.Username;
                            //log.CreateTime = DateTime.Now;

                            //AttachmentService.AttachmentLog(log);

                            #endregion
                        }

                        context.Response.Write("1*上传附件成功");
                        //context.Response.Write("{\"IsSuccess\":\"true\",\"msg\":\"上传附件成功!\"}");
                    }
                    else
                    {
                        context.Response.Write("0*不支持该附件类型");
                        //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传附件类型错误（*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.ceb;*.wps;*.dps;*.et)\"}");
                    }
                }
                else
                {
                    context.Response.Write("0*附件不存在");
                    //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传失败，附件不存在！\"}");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("0*" + ex.Message.Replace('"', '\"'));
                //context.Response.Write("{\"IsSuccess\":\"false\",\"msg\":\"上传失败，异常信息为：" + ex.Message.Replace('"', '\"') + "！\"}");
            }
        }

        /// <summary>
        ///     在线附件保存(编辑)
        /// </summary>
        /// <param name="context"></param>
        private void AttachmentEdit(HttpContext context)
        {
        }

        #endregion
    }
}