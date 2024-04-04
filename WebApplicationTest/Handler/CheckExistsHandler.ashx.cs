using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// CheckExistsHandler 的摘要说明
    /// 用来检查上传目标文件夹里是否存在相同文件
    /// </summary>
    public class CheckExistsHandler : IHttpHandler
    {
        // Define a destination
        private string targetFolder = "/Uploads/"; // Relative to the root and should match the upload folder in the uploader script

        //if (file_exists($_SERVER['DOCUMENT_ROOT'] . $targetFolder . '/' . $_POST['filename'])) {
        //    echo 1;
        //} else {
        //    echo 0;
        //}

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
                var s = "";
                for (int i = 0; i < context.Request.Form.Count; i++)
                {
                    if (context.Request.Form.Keys[i].ToString().Substring(0, 1) != "_")
                        s += context.Request.Form.Keys[i].ToString() + " = " + context.Request.Form[i].ToString();
                }

                //var b = s;
                //ID = GID = 1234TID = 64C64907-F110-4A41-9DAF-5532A5A135FBfilename = WeChat_20171125201459.mp4

                var filename = context.Request.Form["filename"];
                //var filename1 = filename.Substring(0, filename.LastIndexOf(".", StringComparison.Ordinal));
                //var p = filename1;
                var strSubdirectories = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "\\") + "\\"; // 以日期为文件子目录

                VirtualDirectory = VirtualDirectory.StartsWith("\\")
                    ? VirtualDirectory
                    : "\\" + VirtualDirectory;
                VirtualDirectory = VirtualDirectory.EndsWith("\\")
                    ? VirtualDirectory
                    : VirtualDirectory + "\\";

                strSubdirectories = (strSubdirectories.EndsWith("\\")
                    ? strSubdirectories
                    : strSubdirectories + "\\");

                targetFolder = VirtualDirectory;

                var strFilePath = HttpContext.Current.Server.MapPath(targetFolder);

                strFilePath += strSubdirectories;
                FileHelper.FilePathCheck(strFilePath);
                string fullFileName = strFilePath + filename;

                var rst = File.Exists(fullFileName) ? "1" : "0";

                context.Response.Write(rst);
            }
            catch (Exception ex)
            {
                context.Response.Write("0");
            }
        }

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