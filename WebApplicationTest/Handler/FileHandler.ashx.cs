using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using WebApplicationTest.AppCodes;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler
    {
        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        internal string VirtualDirectory = (ConfigurationManager.AppSettings["VirtualDirectory"] ?? "").Trim();

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

            string strOperation = context.Request.Params["OPERATION"] ?? "";

            switch (strOperation.Trim().ToUpper())
            {
                // 目录文件数据处理
                case "FILEPROCESS":
                    FileProcess(context);
                    break;

                // 获取职责体系统计数据
                case "ZTREEDATA":
                    ZTreeProcess(context);
                    break;

                // 标准文件检索
                case "SEARCH":
                    StandardSearch(context);
                    break;

                //// 获取指标体系统计数据
                //case "INDICATORCOUNT":
                //    GetIndicatorSystemData(context);
                //    break;

                //// 获取标准制度体系统计数据
                //case "STANDARDCOUNT":
                //    GetStandardSystemData(context);
                //    break;

                //// 查询首页我的手册数据
                //case "MYMANUAL":
                //    GetMyManualData(context);
                //    break;

                default:
                    FileUpload(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 文件上传测试

        /// <summary>
        /// 文件上传测试
        /// </summary>
        /// <param name="context"></param>
        private void FileUpload(HttpContext context)
        {
            var strDate = DateTime.Now.ToString("yyyy-MM-dd");
            var strSubdirectories = strDate.Replace("-", "\\") + "\\"; // 以日期为文件子目录

            HttpPostedFile file = context.Request.Files["fileData"];

            if (file != null)
            {
                //var fileLength = file.ContentLength; // 文件长度
                //var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型
                //var filename = file.FileName; // 文件名称

                // 附件ID
                var strFileId = Guid.NewGuid().ToString().ToUpper();

                // 文件类型
                string strFileExt = "." +
                                    file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                                        .ToLower();

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

                // 检查文件目录
                FilePathCheck(HttpContext.Current.Server.MapPath(VirtualDirectory + strSubdirectories));

                // 保存文件
                string fullFileName = strSubdirectories + strSaveName;

                try
                {
                    var filePath = context.Server.MapPath(VirtualDirectory + fullFileName);
                    file.SaveAs(filePath);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    throw;
                }
            }
        }

        #endregion

        #region 如果目录不存在，建立

        /// <summary>
        /// 如果目录不存在，建立
        /// </summary>
        /// <param name="dirName">目录名称</param>
        public static void FilePathCheck(string dirName)
        {
            var directoryName = Path.GetDirectoryName(dirName);
            if (directoryName == null) return;
            String path = directoryName.TrimEnd('\\');
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

        #region 目录文件数据处理

        /// <summary>
        /// 目录文件数据处理
        /// </summary>
        /// <param name="context"></param>
        private static void FileProcess(HttpContext context)
        {
            var rootPath = ConfigurationManager.AppSettings["BatchSystemFilePath"];
            if (!string.IsNullOrEmpty(context.Request.Params["rootPath"]))
            {
                rootPath = context.Request.Params["rootPath"];
            }

            if (string.IsNullOrEmpty(rootPath)) return;

            if (!Directory.Exists(rootPath))
            {
                //lblFilePath.Text = "设定目录不存在";
                return;
            }

            //var user = new AUTHUSER();
            //if (HttpContext.Current.Session["SESSION_CURRENT_USER"] != null)
            //{
            //    user = HttpContext.Current.Session["SESSION_CURRENT_USER"] as AUTHUSER;
            //}
            //IHomeService homeService = new HomeService();
            //var list = homeService.GetProcessStatiscalData();

            //IDataCacheService dataCache = new DataCacheService();
            //if (user == null) return;

            var list = new List<FileAttribute>();
            var di = new DirectoryInfo(rootPath);

            //遍历文件夹
            foreach (var folder in di.GetDirectories())
            {
                FileAttribute fileAttribute = new FileAttribute();
                fileAttribute.FileName = folder.Name;
                fileAttribute.FileSize = 0;
                fileAttribute.FileType = 0; // "文件夹";

                fileAttribute.Extension = "";
                fileAttribute.FullName = folder.FullName;
                fileAttribute.DirectoryName = "";
                fileAttribute.CreationTime = folder.CreationTime;

                list.Add(fileAttribute);
            }

            //遍历文件
            foreach (var file in di.GetFiles())
            {
                FileAttribute fileAttribute = new FileAttribute();
                fileAttribute.FileName = file.Name;
                fileAttribute.FileSize = file.Length;
                fileAttribute.FileType = 1; // "文件";

                fileAttribute.Extension = file.Extension;
                fileAttribute.FullName = file.FullName;
                fileAttribute.DirectoryName = file.DirectoryName;
                fileAttribute.CreationTime = file.CreationTime;

                list.Add(fileAttribute);
            }

            //dataCache.GetProcessStatiscalData(user.EnterpriseID);
            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 目录文件数据处理

        /// <summary>
        /// 目录文件数据ZTree处理
        /// </summary>
        /// <param name="context"></param>
        private static void ZTreeProcess(HttpContext context)
        {
            var rootPath = ConfigurationManager.AppSettings["BatchSystemFilePath"];
            if (!string.IsNullOrEmpty(context.Request.Params["id"]))
            {
                rootPath = context.Request.Params["id"];
            }

            var id = context.Request.Form["id"];
            var name = context.Request.Form["name"];
            var level = context.Request.Form["level"];

            if (string.IsNullOrEmpty(id)) id = "0";
            if (string.IsNullOrEmpty(level)) level = "0";

            if (string.IsNullOrEmpty(rootPath)) return;

            if (!Directory.Exists(rootPath))
            {
                return;
            }

            var list = new List<zTreeModel>();
            var di = new DirectoryInfo(rootPath);

            //遍历文件夹
            foreach (var folder in di.GetDirectories())
            {
                zTreeModel model = new zTreeModel();
                model.id = folder.FullName;
                model.name = folder.Name;
                model.pId = rootPath;
                model.isParent = true;
                //model.icon = "";
                list.Add(model);
            }

            //遍历文件
            foreach (var file in di.GetFiles())
            {
                zTreeModel model = new zTreeModel();
                model.id = file.FullName;
                model.name = file.Name;
                model.pId = rootPath;
                model.isParent = false;
                //model.icon = "";
                list.Add(model);
            }

            var js = new JavaScriptSerializer().Serialize(list);
            context.Response.Write(js);
        }

        #endregion

        #region 标准文件检索

        /// <summary>
        /// 标准文件检索
        /// </summary>
        /// <param name="context"></param>
        private static void StandardSearch(HttpContext context)
        {
        }

        #endregion

    }
}