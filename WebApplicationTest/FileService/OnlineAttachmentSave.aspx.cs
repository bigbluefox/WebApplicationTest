using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.IService;
using Hsp.Test.Service;

namespace WebApplicationTest
{
    public partial class OnlineAttachmentSave : System.Web.UI.Page
    {
        #region 参数

        /// <summary>
        ///     附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        /// <summary>
        /// 附件ID
        /// </summary>
        private string documentID = string.Empty;

        /// <summary>
        ///     附件虚拟目录
        /// </summary>
        public string VirtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"];

        #endregion

        #region 页面加载

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            documentID = Session["Current_SaveDocumentID_Save"].ToString();

            VirtualDirectory = VirtualDirectory.StartsWith("\\")
                ? VirtualDirectory
                : "\\" + VirtualDirectory;
            VirtualDirectory = VirtualDirectory.EndsWith("\\")
                ? VirtualDirectory
                : VirtualDirectory + "\\";

            SaveDocument();
        }

        #endregion

        #region 保存文档

        /// <summary>
        /// 保存文档
        /// </summary>
        public void SaveDocument()
        {
            var uploadFiles = Request.Files;
            var file = uploadFiles[0];
            try
            {
                var model = AttachmentService.GetFileModel(documentID);
                if (model == null) return;

                model.FileSize = file.ContentLength; // 文件长度
                model.Modifier = "0";
                model.ModifierName = "";
                model.ModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var filePath =
                    HttpContext.Current.Server.MapPath(VirtualDirectory + HttpUtility.UrlDecode(model.FilePath));
                file.SaveAs(filePath);

                AttachmentService.EditFile(model);
            }
            catch
            {
                Response.Write("失败");
            }
        }

        #endregion
    }
}