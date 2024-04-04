using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model.Media;
using Hsp.Test.Service;
using Image = System.Drawing.Image;

namespace WebApplicationTest.MultiMedia
{
    public partial class ImageProcess : System.Web.UI.Page
    {
        public int Total = 0;

        /// <summary>
        ///     图片服务
        /// </summary>
        internal readonly IImageService ImageService = new ImageService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 图片处理点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Total = 0;
                var type = rbImgType1.Checked;
                lblResult.Text += (type) ? " 当前选中大图" : " 当前选中小图";

                var strSource = txtSourcePath.Text; // @"D:\新建文件夹 (4)";
                var strTarget = txtTarget.Text; // @"E:\ProcessedImages";

                var list = new List<ImageAttribute>();
                var di = new DirectoryInfo(strSource);

                FindImageFile(di, list); // 图片文件查找
                if (list.Count > 0)
                {
                    var rst = ImageService.AddImages(list);
                }
                lblResult.Text += "，共检索到：" + Total + "个图片。";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 递归图片文件查找
        /// </summary>
        /// <param name="di"></param>
        /// <param name="list"></param>
        public void FindImageFile(DirectoryInfo di, List<ImageAttribute> list)
        {
            //FileInfo[] fis = di.GetFiles();
            foreach (var file in di.GetFiles())
            {
                //Console.WriteLine("文件：" + fis[i].FullName);
                if (file.Extension.Length == 0) continue;
                if (!IsImage(file.Extension)) continue;

                #region 检查文件大小

                Byte[] bytes;
                var width = 0;
                var height = 0;
                try
                {
                    var image = Image.FromFile(file.FullName);
                    width = image.Width;
                    height = image.Height;
                    if (height < 128 || width < 128) continue;

                    bytes = ImageToBytes(image);
                }
                catch (Exception)
                {
                    //throw;
                }
                finally
                {
                    Dispose(); // 防止内存溢出
                }

                #endregion

                var model = new ImageAttribute();
                model.Name = DateTime.Now.ToFileTime().ToString();
                model.Title = file.Name.Replace(file.Extension, "");
                model.Size = file.Length;
                model.Extension = file.Extension;
                model.ContentType = Hsp.Test.Common.MimeMapping.GetMimeMapping(file.FullName);
                model.FullName = file.FullName;
                model.DirectoryName = file.DirectoryName;
                model.CreationTime = file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");

                // SELECT Id, Name, Title, Width, Height, Size, Extension
                // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
                // FROM ImageAttribute

                model.Width = width;
                model.Height = height;

                model.MD5 = HashHelper.ComputeMD5(file.FullName);
                model.SHA1 = HashHelper.ComputeSHA1(file.FullName);

                list.Add(model);
                Total ++;
            }

            if (list.Count > 999)
            {
                if (list.Count > 0)
                {
                    var rst = ImageService.AddImages(list);
                }
                
                list = new List<ImageAttribute>();
            }

            DirectoryInfo[] dis = di.GetDirectories();
            foreach (DirectoryInfo t in dis)
            {
                //Console.WriteLine("目录：" + t.FullName);
                FindImageFile(t, list);
            }
        }

        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        ///     是否图片文件
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public bool IsImage(string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "jpg,png,jpeg,gif";
            return strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return DateTime.Now.ToFileTime().ToString();       
        }

        /// <summary>
        /// 图片归拢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgTogether_Click(object sender, EventArgs e)
        {
            Total = 0;
            var strSource = txtSourcePath.Text;
            var strTarget = txtTarget.Text; // 归拢目录
            if (string.IsNullOrEmpty(strTarget))
            {
                lblResult.Text = "请填写图片归拢目录！";
                return;
            }

            if (!Directory.Exists(strTarget))
            {
                lblResult.Text = "归拢目录不存在！";
                return;
            }

            if (!strTarget.EndsWith("\\")) strTarget += "\\";

            var list = ImageService.GetImageList();

            foreach (var image in list)
            {
                if(!File.Exists(image.FullName)) continue;

                try
                {
                    var strNewName = Path.Combine(strTarget + image.Name + image.Extension);
                    FileInfo fileInfo = new FileInfo(image.FullName);
                    fileInfo.MoveTo(strNewName);

                    Total++;
                }
                catch (Exception ex)
                {
                    //throw;
                }
                finally
                {
                    Dispose();
                }

            }

            lblResult.Text += "共迁移" + Total + "个图片。";

        }

        /// <summary>
        /// 批量创建目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateDirectory_Click(object sender, EventArgs e)
        {
            var strTarget = txtTarget.Text; // 归拢目录 E:\ProcessedImages
            if(string.IsNullOrEmpty(strTarget)) return;
            if (!strTarget.EndsWith("\\")) strTarget += "\\";

            for (int i = 100; i < 200; i++)
            {
                var strSubPath = "0" + i.ToString();
                var strPath = Path.Combine(strTarget + strSubPath.Substring(strSubPath.Length - 3, 3));
                Directory.CreateDirectory(strPath);
            }
        }

    }
}