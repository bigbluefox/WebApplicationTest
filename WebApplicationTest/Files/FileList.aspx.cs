using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Model.Media;

namespace WebApplicationTest.Files
{
    public partial class FileList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 文件列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnList_Click(object sender, EventArgs e)
        {
            var rootPath = Server.MapPath("/");
            rootPath = @"D:\MSO\Publish\Processist.MSO-v3.0";

            try
            {
                var list = new List<ImageAttribute>();
                var di = new DirectoryInfo(rootPath);

                //遍历根目录文件
                foreach (var file in di.GetFiles())
                {
                    lblList.Text += "/bzfw" + file.FullName.Replace(rootPath, "").Replace(@"\", "/") +"<br/>";
                }

                //遍历目录
                foreach (var file in di.GetDirectories())
                {
                    //if (!IsImage(file.Extension)) continue;

                    //var img = Image.FromFile(file.FullName);
                    //var width = img.Width;
                    //var height = img.Height;
                    //if (width < 12 || height < 12 || height > 256 || width > 256 || (height * 1.0 / width * 1.0 > 1.6) ||
                    //    (width * 1.0 / height * 1.0 > 1.6)) continue;
                    //var model = new ImageAttribute();
                    //model.FullName = "\\" + file.FullName.Replace(rootPath, "");
                    //model.Name = file.Name;
                    //model.Size = file.Length;
                    //model.Extension = file.Extension;
                    //model.DirectoryName = file.DirectoryName;
                    //model.CreationTime = file.CreationTime;

                    //model.Width = img.Width;
                    //model.Height = img.Height;

                    //list.Add(model);

                    lblList.Text += "/bzfw" + file.FullName.Replace(rootPath, "").Replace(@"\", "/") + "/*" + "<br/>";
                    SubDirectory(file.FullName, rootPath);
                }

                //return Json(new { IsSuccess = true, Data = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Json(new { IsSuccess = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        protected void SubDirectory(string directory, string rootPath)
        {
            var di = new DirectoryInfo(directory);
            foreach (var file in di.GetDirectories())
            {
                lblList.Text += "/bzfw" + file.FullName.Replace(rootPath, "").Replace(@"\", "/") + "/*" + "<br/>";
                SubDirectory(file.FullName, rootPath);
            }
        }
    }
}