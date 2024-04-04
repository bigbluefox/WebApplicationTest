using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Hsp.Test.IService;
using Hsp.Test.Service;

namespace WebApplicationTest
{
    public partial class DocEdit : Page
    {
        #region 变量

        /// <summary>
        ///     附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        public string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"];

        public string DocumentID;
        public string FromURL;

        //public string FileName;
        //public string User;
        public string extName;

        public string title, attachpath, url, newofficetype;

        #endregion 变量

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

            DocumentID = Request.QueryString["FID"] ?? "";
            if (Request.QueryString["extName"] != null)
            {
                extName = Request.QueryString["extName"].ToLower();
            }

            //User = Request.QueryString["User"].ToString();
            //User = CurrentUser.UserCode;
            //GetFileData(Convert.ToInt32(DocumentID));

            if (string.IsNullOrEmpty(DocumentID)) return;

            Session["Current_SaveDocumentID_Save"] = DocumentID;
            var strFileId = DocumentID;

            //var paramList = new Dictionary<string, string>
            //{
            //    {"FID", strFileId},
            //    {"GID", ""},
            //    {"TID", ""}
            //};
            //List<FileModel> list = AttachmentService.GetFileList(paramList);
            //if (list.Count > 0){}

            var model = AttachmentService.GetFileModel(strFileId);
            if (model != null) FromURL = model.FileUrl;

            if (!string.IsNullOrEmpty(FromURL))
            {
                FromURL = HttpUtility.UrlDecode(FromURL);
            }

            //http://localhost:1845/WebOffice/Temp/170227173136.docx

            FromURL = "http://" + Request.Url.Authority + VirtualDirectory + FromURL;
        }

        #endregion

    }
}