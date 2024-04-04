using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Service;


namespace WebApplicationTest
{
    public partial class uploadify : System.Web.UI.Page
    {
        /// <summary>
        /// 附件服务
        /// </summary>
        internal readonly IAttachmentService AttachmentService = new AttachmentService();

        /// <summary>
        /// 附件访问网络地址
        /// </summary>
        public string FileUrl = System.Configuration.ConfigurationManager.AppSettings["Url"];

        /// <summary>
        /// 过滤上传文件类型
        /// </summary>
        public string UpFileTypes = System.Configuration.ConfigurationManager.AppSettings["UpFileType"];

        /// <summary>
        /// 附件虚拟目录
        /// </summary>
        public string VirtualDirectory = System.Configuration.ConfigurationManager.AppSettings["VirtualDirectory"];

        /// <summary>
        /// 附件分类ID
        /// </summary>
        public static string TypeId { get; set; }

        /// <summary>
        /// 附件分组ID
        /// </summary>
        public static string GroupId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            TypeId = Request.QueryString["TID"];
            GroupId = Request.QueryString["GID"];
            UserId = Request.QueryString["UID"];
            UserName = Request.QueryString["UNAME"];
            //Utility.WriteLog("UserName(Get)：", UserName);
            UserName = HttpUtility.UrlEncode(UserName);
            //Utility.WriteLog("UserName(UrlEncode)：", UserName);

            if (string.IsNullOrEmpty(TypeId)) TypeId = AttachmentType.Workflow;
            if (string.IsNullOrEmpty(GroupId)) GroupId = "1235";

            VirtualDirectory = VirtualDirectory.StartsWith("/")
                ? VirtualDirectory
                : "/" + VirtualDirectory;

            HttpBrowserCapabilities bc = Request.Browser;
            //this.Label1.Text = bc.Browser;
        }
    }
}