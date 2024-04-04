using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// FileInputHandler 的摘要说明
    /// </summary>
    public class FileInputHandler : IHttpHandler, IRequiresSessionState
    {

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

            var s = "";
            foreach (var name in context.Request.Form)
            {
                s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
            }

            var a = s;
            s = "";

            foreach (var name in context.Request.Params)
            {
                s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
            }

            var b = s; // 1235

            HttpPostedFile file = context.Request.Files["file_data"];

            var filename = DateTime.Now.ToString("yyyyMMddmmHHss");
            //var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为附件子目录

            if (file != null)
            {
                var fileLength = file.ContentLength; // 附件长度
                var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                filename = string.IsNullOrEmpty(filename) ? file.FileName : filename; // 附件名称 D:\新建文件夹 (4)\99A坦克.jpg
                //filename = file.FileName;

                // 附件类型
                var strFileExt = "." +
                                 file.FileName.Substring(
                                     file.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                                     .ToLower();


                // 保存附件
                var strSaveName = filename + strFileExt;

                VirtualDirectory = VirtualDirectory.StartsWith("\\")
                    ? VirtualDirectory
                    : "\\" + VirtualDirectory;
                VirtualDirectory = VirtualDirectory.EndsWith("\\")
                    ? VirtualDirectory
                    : VirtualDirectory + "\\";

                // 检查附件目录
                var filePath = HttpContext.Current.Server.MapPath(VirtualDirectory);
                FileHelper.FilePathCheck(filePath);

                // 保存附件
                file.SaveAs(filePath + strSaveName);



                context.Response.Write("{\"id\": \"23213123！\"}");




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

// Form Post

//ALL_HTTP = HTTP_CACHE_CONTROL:no-cache
//HTTP_CONNECTION:Keep-Alive
//HTTP_CONTENT_LENGTH:50908
//HTTP_CONTENT_TYPE:multipart/form-data; boundary=---------------------------7e2443b41136
//HTTP_ACCEPT:text/html, application/xhtml+xml, image/jxr, */*
//HTTP_ACCEPT_ENCODING:gzip, deflate
//HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//HTTP_HOST:localhost:8926
//HTTP_REFERER:http://localhost:8926/Bootstrap/FileInput.html?input-b1=E%3A%5CVideos%5C100%E9%83%A8%E6%A0%A1%E5%9B%AD%E7%94%B5%E5%BD%B1.doc
//HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko

//ALL_RAW = Cache-Control: no-cache
//Connection: Keep-Alive
//Content-Length: 50908
//Content-Type: multipart/form-data; boundary=---------------------------7e2443b41136
//Accept: text/html, application/xhtml+xml, image/jxr, */*
//Accept-Encoding: gzip, deflate
//Accept-Language: en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//Host: localhost:8926
//Referer: http://localhost:8926/Bootstrap/FileInput.html?input-b1=E%3A%5CVideos%5C100%E9%83%A8%E6%A0%A1%E5%9B%AD%E7%94%B5%E5%BD%B1.doc
//User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko

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
//CONTENT_LENGTH = 50908
//CONTENT_TYPE = multipart/form-data; boundary=---------------------------7e2443b41136
//GATEWAY_INTERFACE = CGI/1.1
//HTTPS = off
//HTTPS_KEYSIZE = 
//HTTPS_SECRETKEYSIZE = 
//HTTPS_SERVER_ISSUER = 
//HTTPS_SERVER_SUBJECT = 
//INSTANCE_ID = 3
//INSTANCE_META_PATH = /LM/W3SVC/3
//LOCAL_ADDR = ::1
//PATH_INFO = /Handler/FileInputHandler.ashx
//PATH_TRANSLATED = e:\my documents\visual studio 2012\Projects\WebApplicationTest\WebApplicationTest\Handler\FileInputHandler.ashx
//QUERY_STRING = 
//REMOTE_ADDR = ::1
//REMOTE_HOST = ::1
//REMOTE_PORT = 33677
//REQUEST_METHOD = POST
//SCRIPT_NAME = /Handler/FileInputHandler.ashx
//SERVER_NAME = localhost
//SERVER_PORT = 8926
//SERVER_PORT_SECURE = 0
//SERVER_PROTOCOL = HTTP/1.1
//SERVER_SOFTWARE = Microsoft-IIS/8.0
//URL = /Handler/FileInputHandler.ashx
//HTTP_CACHE_CONTROL = no-cache
//HTTP_CONNECTION = Keep-Alive
//HTTP_CONTENT_LENGTH = 50908
//HTTP_CONTENT_TYPE = multipart/form-data; boundary=---------------------------7e2443b41136
//HTTP_ACCEPT = text/html, application/xhtml+xml, image/jxr, */*
//HTTP_ACCEPT_ENCODING = gzip, deflate
//HTTP_ACCEPT_LANGUAGE = en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3
//HTTP_HOST = localhost:8926
//HTTP_REFERER = http://localhost:8926/Bootstrap/FileInput.html?input-b1=E%3A%5CVideos%5C100%E9%83%A8%E6%A0%A1%E5%9B%AD%E7%94%B5%E5%BD%B1.doc
//HTTP_USER_AGENT = Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko









//Ajax Uploads
//You need to setup the server methods to parse and return the right response via AJAX. You can setup uploads in asynchronous OR synchronous modes as described below.

//Asynchronous Uploads
//This is the default mode, whereby the uploadAsync property is set to true. When uploading multiple files, the asynchronous mode allows triggering parallel server calls for each file upload. You can control the maximum number of files allowed at a time to be uploaded by setting the maxFileCount property. In asynchronous mode, progress of each thumbnail in the preview is validated and updated.

//Receiving Data (on server)
//Your server method as set in uploadUrl receives the following data from the plugin
//file data: This data is sent to the server in a format very similar to the form file input. For example in PHP you can read this data as $_FILES['input-name'], where input-name is the name attribute of your input. If you do not set a name attribute for your input, the name is defaulted to file_data. Note that multiple file uploads require that you set multiple property to true for your input. So in PHP you would receive the file data as $_FILES['file_data']
//extra data: The plugin can send additional data to your server method. This can be done by setting uploadExtraData as an associative array object in key value pairs. So if you have setup uploadExtraData={id:'kv-1'}, in PHP you can read this data as $_POST['id'].
//Note
//In asynchronous mode, you will ALWAYS receive a single FILE on your server action that processes the ajax upload. Basically the plugin will trigger parallel ajax calls for every file selected for upload. You need to write your server upload logic accordingly so that you always read and upload ONE file. Similarly, in the sending data section below, you must return an initialPreview that reflects data only for the single file received. 



//Sending Data (from server)
//Your server method as set in uploadUrl must send data back as a json encoded object. For example, the server could return a JSON object like below:

//// example JSON response from server
//{
//    error: 'An error exception message if applicable',
//    initialPreview: [
//        // initial preview thumbnails for server uploaded files if you want it displayed immediately after upload
//    ],
//    initialPreviewConfig: [
//        // configuration for each item in initial preview 
//    ],
//    initialPreviewThumbTags: [
//        // initial preview thumbnail tags configuration that will be replaced dynamically while rendering
//    ],
//    append: true // whether to append content to the initial preview (or set false to overwrite)
//}