using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

namespace WebApplicationTest
{
    public partial class DocWatch : System.Web.UI.Page
    {
        #region 参数

        /// <summary>
        /// 附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        /// <summary>
        /// 附件虚拟目录
        /// </summary>
        public string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"];

        public string key = "";
        public string FromURL;

        #endregion

        #region 页面加载

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            VirtualDirectory = VirtualDirectory.StartsWith("/")
                ? VirtualDirectory
                : "/" + VirtualDirectory;

            if (Request.QueryString["key"] != null)
            {
                key = Request.QueryString["key"];
            }

            if (Request.QueryString["FID"] == null) return;
            var strFileId = Request.QueryString["FID"];

            //var paramList = new Dictionary<string, string>
            //{
            //    {"FID", strFileId},
            //    {"GID", ""},
            //    {"TID", ""}
            //};
            //List<FileModel> list = AttachmentService.GetFileList(paramList);
            //if (list.Count > 0){}
            //    FileModel model = list.FirstOrDefault();

            var model = AttachmentService.GetFileModel(strFileId);
            if (model != null) FromURL = model.FileUrl;

            if (!string.IsNullOrEmpty(FromURL))
            {
                FromURL = HttpUtility.UrlDecode(FromURL);
            }

            FromURL = "http://" + Request.Url.Authority + VirtualDirectory + FromURL;
        }

        #endregion
    }
}