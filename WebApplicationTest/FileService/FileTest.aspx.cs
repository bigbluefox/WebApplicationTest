using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.FileService
{
    public partial class FileTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var filePath = @"d:\QHHCP 201.001—2015-淮沪煤电有限公司田集发电厂规划管理标准.doc";

            //var fi = new FileInfo(filePath);

            //var name = fi.Name;
            //var exist = fi.Exists;
            //var len = fi.Length;
            //var type = MimeMapping.GetMimeMapping(fi.Name); // 互联网媒体类型，Internet Media Type，MIME类型
            //var ext = fi.Extension.ToLower(); // 文件类型，扩展名

            ////var 
            //Label1.Text = name + " * " + exist + " * " + len + " * " + type + " * " + ext;

            //if (fi.Directory != null)
            //{
            //    //var path = fi.Directory.FullName;

            //    var rst = "Directory: " + fi.Directory.FullName + "<br />";
            //    rst += ", FilePath: " + fi.FullName + "<br />";
            //    rst += ", FileName: " + fi.Name + "<br />";

            //    Label1.Text = rst;
            //}

            //QHHCP 201.001—2015-淮沪煤电有限公司田集发电厂规划管理标准.doc * True * 92160 * application/msword * .doc 


            var name = "流程管理流程";

            var lidx = name.LastIndexOf("流程", StringComparison.Ordinal);


            var len = name.Length;

            if (lidx == len - 2)
            {
                name = name.Substring(0, lidx);
            }

            var p = len.ToString() + " * " + lidx.ToString() + " * " + name;

        }
    }
}