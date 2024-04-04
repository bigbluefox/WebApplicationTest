using System;
using System.Configuration;
using System.IO;
using System.Web;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    ///     WebUploaderHandler 的摘要说明
    /// </summary>
    public class WebUploaderHandler : IHttpHandler
    {
        // Define a destination
        private string targetFolder = "/Uploads/";

        /// <summary>
        ///     有效文件类型
        /// </summary>
        internal string ValidFileType = ConfigurationManager.AppSettings["UpFileType"].Trim();

        // Relative to the root and should match the upload folder in the uploader script


        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        internal string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"].Trim();


        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            var strOperation = context.Request.Params["OPERATION"] ?? "";
            if (string.IsNullOrEmpty(strOperation)) strOperation = context.Request.Form["OPERATION"];

// Settings
// $targetDir = ini_get("upload_tmp_dir") . DIRECTORY_SEPARATOR . "plupload";
            var targetDir = "upload_tmp";
            var uploadDir = "upload";

            var cleanupTargetDir = true; // Remove old files
            var maxFileAge = 5*3600; // Temp file age in seconds


            //var s = "";
            //foreach (var name in context.Request.Form)
            //{
            //    s += name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
            //}

            //var a = s;
            //s = "";

            //foreach (var name in context.Request.Params)
            //{
            //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
            //}

            //var b = s; // 1235


//uid = 123
//id = WU_FILE_0
//name = 8803863_1.jpg
//type = image/jpeg
//lastModifiedDate = Sun Feb 05 2017 21:32:53 GMT+0800 (中国标准时间)
//size = 110966


//ALL_HTTP = HTTP_CACHE_CONTROL:no-cache
//HTTP_CONNECTION:Keep-Alive
//HTTP_CONTENT_LENGTH:111827
//HTTP_CONTENT_TYPE:multipart/form-data; boundary=---------------------------7e13be23161a18
//HTTP_ACCEPT:*/*
//HTTP_ACCEPT_ENCODING:gzip, deflate
//HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//HTTP_HOST:localhost:8926
//HTTP_REFERER:http://localhost:8926/WebUploader/WebUploader.aspx
//HTTP_USER_AGENT:Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; InfoPath.3; Tablet PC 2.0)

//ALL_RAW = Cache-Control: no-cache
//Connection: Keep-Alive
//Content-Length: 111827
//Content-Type: multipart/form-data; boundary=---------------------------7e13be23161a18
//Accept: */*
//Accept-Encoding: gzip, deflate
//Accept-Language: en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//Host: localhost:8926
//Referer: http://localhost:8926/WebUploader/WebUploader.aspx
//User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; InfoPath.3; Tablet PC 2.0)

//APPL_MD_PATH = /LM/W3SVC/3/ROOT
//APPL_PHYSICAL_PATH = e:\my documents\visual studio 2012\Projects\WebApplicationTest\WebApplicationTest\
//AUTH_TYPE = 
//AUTH_USER = 
//AUTH_PASSWORD = 
//LOGON_USER = 
//REMOTE_USER = 
//CERT_COOKIE = 
//CERT_FLAGS = 
//CERT_ISSUER = 
//CERT_KEYSIZE = 
//CERT_SECRETKEYSIZE = 
//CERT_SERIALNUMBER = 
//CERT_SERVER_ISSUER = 
//CERT_SERVER_SUBJECT = 
//CERT_SUBJECT = 
//CONTENT_LENGTH = 111827
//CONTENT_TYPE = multipart/form-data; boundary=---------------------------7e13be23161a18
//GATEWAY_INTERFACE = CGI/1.1
//HTTPS = off
//HTTPS_KEYSIZE = 
//HTTPS_SECRETKEYSIZE = 
//HTTPS_SERVER_ISSUER = 
//HTTPS_SERVER_SUBJECT = 
//INSTANCE_ID = 3
//INSTANCE_META_PATH = /LM/W3SVC/3
//LOCAL_ADDR = ::1
//PATH_INFO = /Handler/WebUploaderHandler.ashx
//PATH_TRANSLATED = e:\my documents\visual studio 2012\Projects\WebApplicationTest\WebApplicationTest\Handler\WebUploaderHandler.ashx
//QUERY_STRING = 
//REMOTE_ADDR = ::1
//REMOTE_HOST = ::1
//REMOTE_PORT = 18736
//REQUEST_METHOD = POST
//SCRIPT_NAME = /Handler/WebUploaderHandler.ashx
//SERVER_NAME = localhost
//SERVER_PORT = 8926
//SERVER_PORT_SECURE = 0
//SERVER_PROTOCOL = HTTP/1.1
//SERVER_SOFTWARE = Microsoft-IIS/8.0
//URL = /Handler/WebUploaderHandler.ashx
//HTTP_CACHE_CONTROL = no-cache
//HTTP_CONNECTION = Keep-Alive
//HTTP_CONTENT_LENGTH = 111827
//HTTP_CONTENT_TYPE = multipart/form-data; boundary=---------------------------7e13be23161a18
//HTTP_ACCEPT = */*
//HTTP_ACCEPT_ENCODING = gzip, deflate
//HTTP_ACCEPT_LANGUAGE = en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//HTTP_HOST = localhost:8926
//HTTP_REFERER = http://localhost:8926/WebUploader/WebUploader.aspx
//HTTP_USER_AGENT = Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; InfoPath.3; Tablet PC 2.0)


            //uid = 123
            //id = WU_FILE_0
            //name = 8803863_1.jpg
            //type = image/jpeg
            //lastModifiedDate = Sun Feb 05 2017 21:32:53 GMT+0800 (中国标准时间)
            //size = 110966

            try
            {
                // 附件ID
                var strFileId = !string.IsNullOrEmpty(context.Request.Form["ID"])
                    ? context.Request.Form["ID"]
                    : !string.IsNullOrEmpty(context.Request["ID"])
                        ? context.Request["ID"]
                        : Guid.NewGuid().ToString().ToUpper();

                strFileId = Guid.NewGuid().ToString().ToUpper();

                var strDate = DateTime.Now.ToString("yyyy-MM-dd");
                var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为附件子目录

                var filename = context.Request.Params["name"];
                var type = context.Request.Params["type"];
                var size = context.Request.Params["size"];

                var file = context.Request.Files["file"];

                if (file != null)
                {
                    var fileLength = file.ContentLength; // 附件长度
                    var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                    filename = string.IsNullOrEmpty(filename) ? file.FileName : filename; // 附件名称

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
                        var saveFilePath = context.Server.MapPath(VirtualDirectory + fullFileName);
                        file.SaveAs(saveFilePath);

                        //byte[] data = new byte[fileLength]; 
                        //if (File.Exists(saveFilePath))
                        //{
                        //    // {"无法将类型为“System.Web.HttpInputStream”的对象强制转换为类型“System.IO.FileStream”。"}
                        //    FileStream fs = (FileStream) file.InputStream;
                        //        //= new FileStream(saveFilePath, FileMode.Open);

                        //    //获取文件大小
                        //    //long fileSize = fs.Length;

                        //    //data = new byte[fileSize];

                        //    //将文件读到byte数组中
                        //    fs.Read(data, 0, fileLength);

                        //    fs.Close();
                        //}

                        //var ms = new MemoryStream(data);
                        //var hashMd5 = HashHelper.ComputeMD5(ms);

                        var hashMd51 = HashHelper.ComputeMD5(saveFilePath);

                        byte[] array = new byte[fileLength];
                        file.InputStream.Read(array, 0, fileLength);
                        var ms = new System.IO.MemoryStream(array);
                        var hashMd52 = HashHelper.ComputeMD5(ms);

                        var strUrl = "/" + strSubdirectories.Replace("\\", "/") + strSaveName;
                        var strFileName = filename.Replace(strFileExt, "").Trim('.');

                        var strFilePath = HttpUtility.UrlEncode(strSubdirectories + strSaveName);

                        //Utility.WriteLog("附件目录：",
                        //    fullFileName + "，附件大小：" + fileLength + "，附件类型：" + strFileExt + "，附件名称：" +
                        //    strFileName + "，URL：" + strUrl + "，上传人：" + strUserName);

                        // 保存附件信息
                        //var model = new FileModel();
                        //model.FileId = strFileId;
                        //model.TypeId = strTypeId;
                        //model.GroupId = strGroupId;
                        //model.FileName = strFileName;
                        //model.FileDesc = "";
                        //model.FileExt = strFileExt;
                        //model.FileSize = fileLength;
                        //model.ContentType = contentType;
                        //model.FileUrl = strUrl;
                        //model.FilePath = strFilePath;
                        //model.Params = strParams;
                        //model.Creator = strUserId;
                        //model.CreatorName = strUserName;

                        //var rst = AttachmentService.AddFile(model);

                        //if (rst > 0)
                        //{
                        //    #region 记录附件操作日志

                        //    //FileLog log = new FileLog();

                        //    //log.LogId = Guid.NewGuid().ToString();
                        //    //log.GroupId = strGroupId;
                        //    //log.TypeId = strTypeId;
                        //    //log.Operation = "添加附件，文件名称：" + strFileName + strFileExt + "，文件ID：" + model.FileId;
                        //    //log.ClientInfo = String.Format("IP: {0}, 浏览器：{1}", context.Request.UserHostAddress,
                        //    //    context.Request.UserAgent);
                        //    //log.Creator = CurrentUser.Userno.ToString();
                        //    //log.CreatorName = CurrentUser.Username;
                        //    //log.CreateTime = DateTime.Now;

                        //    //AttachmentService.AttachmentLog(log);

                        //    #endregion
                        //}

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

        public bool IsReusable
        {
            get { return false; }
        }

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
    }
}