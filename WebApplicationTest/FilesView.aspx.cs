using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class FilesView : System.Web.UI.Page
    {
        /// <summary>
        /// 体系文件根目录
        /// </summary>
        public string RootPath { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            RootPath = ConfigurationManager.AppSettings["BatchSystemFilePath"];
        }
    }
}