using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model.Media;
using Hsp.Test.Service;

namespace WebApplicationTest.Media
{
    public partial class Retrieve : Page
    {
        /// <summary>
        ///     媒体服务
        /// </summary>
        internal readonly IMediaService MediaService = new MediaService();

        public int Total;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region 检索媒体

        /// <summary>
        ///     媒体检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMediaProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Total = 0;
                var type = ddlMediaType.SelectedValue;
                var strSource = txtSourcePath.Text;
                //var strTarget = txtTarget.Text;

                var list = new List<Medias>();
                var di = new DirectoryInfo(strSource);

                FindMediaFile(di, ref list, type); // 媒体文件查找
                if (list.Count > 0)
                {
                    var rst = MediaService.AddMedias(list);
                }
                lblResult.Text += "共检索到：" + Total + "个媒体文件。";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///     递归媒体文件查找
        /// </summary>
        /// <param name="di">目录</param>
        /// <param name="list">媒体列表</param>
        /// <param name="type">媒体类型</param>
        public void FindMediaFile(DirectoryInfo di, ref List<Medias> list, string type)
        {
            try
            {
                foreach (var file in di.GetFiles())
                {
                    if (file.Extension.Length < 2 || file.Extension.Length > 8) continue;
                    if (!IsMediaFile(type, file.Extension)) continue;

                    #region 检查文件大小

                    var width = 0;
                    var height = 0;

                    //类型：0-其他；1-图片；2-音频；3-视频

                    #region 图片处理

                    if (type == "1")
                    {
                        try
                        {
                            var image = Image.FromFile(file.FullName);
                            width = image.Width;
                            height = image.Height;
                            if (height < 128 || width < 128) continue;

                            //bytes = ImageToBytes(image);
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                        finally
                        {
                            Dispose(); // 防止内存溢出
                        }
                    }

                    #endregion

                    #region 音频处理

                    if (type == "2")
                    {
                        try
                        {
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                        finally
                        {
                            Dispose(); // 防止内存溢出
                        }
                    }

                    #endregion

                    #region 视频处理

                    if (type == "3")
                    {
                        try
                        {
                            if (file.Length < 1048576*2) continue;
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                        finally
                        {
                            Dispose(); // 防止内存溢出
                        }
                    }

                    #endregion

                    #endregion

                    var model = new Medias();
                    model.Name = DateTime.Now.ToFileTime().ToString();
                    model.Title = file.Name.Replace(file.Extension, "").Replace("'", "''");
                    model.Size = file.Length;
                    model.Extension = file.Extension;
                    model.ContentType = MimeMapping.GetMimeMapping(file.FullName);
                    model.FullName = file.FullName.Replace("'", "''");
                    model.DirectoryName = file.DirectoryName.Replace("'", "''");
                    model.CreationTime = file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");

                    //SELECT Id, Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension, ContentType
                    //    , FullName, DirectoryName, CreationTime, MD5, SHA1
                    //FROM Medias

                    model.Width = width;
                    model.Height = height;

                    model.MD5 = (cbxMd5.Checked) ? HashHelper.ComputeMD5(file.FullName) : "";
                    model.SHA1 = (cbxSha1.Checked) ? HashHelper.ComputeSHA1(file.FullName) : "";

                    list.Add(model);
                    Total++;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {
                Dispose();
            }

            if (list.Count > 99)
            {
                if (list.Count > 0)
                {
                    var rst = MediaService.AddMedias(list);
                }

                list = new List<Medias>();
            }

            try
            {
                var dir = di.GetDirectories();
                foreach (var d in dir)
                {
                    FindMediaFile(d, ref list, type);
                }
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

        #region Convert Image to Byte

        /// <summary>
        ///     Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image)
        {
            var format = image.RawFormat;
            using (var ms = new MemoryStream())
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
                var buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        #endregion

        #region 是否媒体文件

        /// <summary>
        ///     是否媒体文件
        /// </summary>
        /// <param name="type">媒体类型</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public bool IsMediaFile(string type, string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "*";
            var isMediaFile = false;
            // 类型：0-其他；1-图片；2-音频；3-视频;4-阅读
            switch (type)
            {
                // 图片
                case "1":
                    strExt = "jpg,png,jpeg,gif";
                    isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                    break;

                // 音频
                case "2":
                    strExt = "mp3,flac,ape,mid,wav,aac,wma,ogg";
                    isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                    break;

                // 视频
                case "3":
                    strExt = "mp4,avi,wmv,mpg,mpeg,mov,rm,rmvb";
                    isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                    break;

                // 阅读
                case "4":
                    strExt = "pdf,doc,docx,epub";
                    isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                    break;

                default:
                    // 其他
                    isMediaFile = true;
                    break;
            }

            return isMediaFile;
        }

        #endregion

        #region 获取文件名称

        /// <summary>
        ///     获取文件名称
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return DateTime.Now.ToFileTime().ToString();
        }

        #endregion

        #endregion

        #region 检索目录

        /// <summary>
        /// 目录检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDirectoryProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Total = 0;
                var strSource = txtSourcePath.Text;
                //var strTarget = txtTarget.Text;

                var list = new List<Medias>();
                var di = new DirectoryInfo(strSource);

                FindDirectory(di, ref list); // 检索目录
                if (list.Count > 0)
                {
                    var rst = MediaService.AddMedias(list);
                }
                lblResult.Text += "共检索到：" + Total + "个目录。";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 检索目录
        /// </summary>
        /// <param name="di"></param>
        /// <param name="list"></param>
        public void FindDirectory(DirectoryInfo di, ref List<Medias> list)
        {
            try
            {
                var dir = di.GetDirectories();
                foreach (var d in dir)
                {
                    var model = new Medias();
                    model.Name = d.Name;
                    model.Title = d.Name;
                    model.FullName = d.FullName;
                    if (d.Parent != null) model.DirectoryName = d.Parent.FullName;
                    model.CreationTime = d.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    list.Add(model);
                    Total++;

                    FindDirectory(d, ref list);
                }
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

        #endregion

        #region 目录名中部分字符替换

        /// <summary>
        /// 目录名中部分字符替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDirectoryRename_Click(object sender, EventArgs e)
        {
            Total = 0;
            var txtSource = this.txtSource.Text;
            var txtTarget = this.txtTarget.Text;
            var isRegular = this.cbxRegular.Checked; // 是否正则表达式替换

            if (txtSource.Length == 0)
            {
                this.lblResult.Text = "请输入被替换的字符！";
                return;
            }

            var paramList = new Dictionary<string, string>{};
            List<Medias> list = MediaService.GetMediaList(paramList);

            foreach (var dir in list)
            {
                #region 替换字符串处理

                string[] regStr = new[] { "[", "]" };
                var strTitle = "";
                var canReplace = false;
                if (isRegular && txtSource.IndexOf("*", StringComparison.Ordinal) > -1)
                {
                    // 正则处理
                    regStr = txtSource.Split('*');
                    strTitle = Regex.Match(dir.Title, @"([" + regStr[0] + "].*[" + regStr[1] + "])", RegexOptions.IgnoreCase).Value;
                    canReplace = strTitle.Trim().Length > 0;
                }
                else
                {
                    canReplace = dir.Title.IndexOf(txtSource, StringComparison.Ordinal) > -1;
                }

                #endregion

                if (!canReplace || !Directory.Exists(dir.FullName)) continue;

                try
                {
                    #region 替换字符串处理

                    if (isRegular && txtSource.IndexOf("*", StringComparison.Ordinal) > -1)
                    {
                        // 正则处理
                        strTitle = Regex.Replace(dir.Title, @"([" + regStr[0] + "].*[" + regStr[1] + "])", "",
                            RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        strTitle = dir.Title.Replace(txtSource, txtTarget);
                    }

                    #endregion

                    var strNewDirectory = Path.Combine(dir.DirectoryName + "\\" + strTitle);
                    DirectoryInfo dirInfo = new DirectoryInfo(dir.FullName);
                    dirInfo.MoveTo(strNewDirectory);

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

            this.lblResult.Text = "共替换" + Total + "个文件！";
        }

        #endregion

        #region 文件名中部分字符替换

        /// <summary>
        /// 文件名中部分字符替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFileRename_Click(object sender, EventArgs e)
        {
            Total = 0;
            var txtSource = this.txtSource.Text;
            var txtTarget = this.txtTarget.Text;

            var isRegular = this.cbxRegular.Checked; // 是否正则表达式替换

            if (txtSource.Length == 0)
            {
                this.lblResult.Text = "请输入被替换的字符！";
                return;
            }
            var paramList = new Dictionary<string, string>{};
            List<Medias> list = MediaService.GetMediaList(paramList);

            foreach (var media in list)
            {
                #region 替换字符串处理

                string[] regStr = new[] {"[", "]"};
                var strTitle = "";
                var canReplace = false;
                if (isRegular && txtSource.IndexOf("*", StringComparison.Ordinal) > -1)
                {
                    // 正则处理
                    regStr = txtSource.Split('*');
                    strTitle = Regex.Match(media.Title, @"([" + regStr[0] + "].*[" + regStr[1] + "])", RegexOptions.IgnoreCase).Value;
                    canReplace = strTitle.Trim().Length > 0;
                }
                else
                {
                    canReplace = media.Title.IndexOf(txtSource, StringComparison.Ordinal) > -1;
                }

                #endregion

                if (!canReplace || !File.Exists(media.FullName)) continue;

                try
                {
                     #region 替换字符串处理

                    if (isRegular && txtSource.IndexOf("*", StringComparison.Ordinal) > -1)
                    {
                        // 正则处理
                        strTitle = Regex.Replace(media.Title, @"([" + regStr[0] + "].*[" + regStr[1] + "])", "",
                            RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        strTitle = media.Title.Replace(txtSource, txtTarget);
                    }

                    #endregion

                    var strNewFileName = Path.Combine(media.DirectoryName + "\\" + strTitle + media.Extension);
                    FileInfo fileInfo = new FileInfo(media.FullName);
                    fileInfo.MoveTo(strNewFileName);

                    Total++;
                }
                catch (Exception ex)
                {
                    lblResult.Text += ex.Message;
                    //throw;
                }
                finally
                {
                    Dispose();
                }

            }

            this.lblResult.Text += "共替换" + Total + "个文件！";
        }

        #endregion


        /// <summary>
        /// 媒体归拢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMediaTogether_Click(object sender, EventArgs e)
        {

        }


        #region 正则替换检测

        /// <summary>
        /// 正则替换检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRenameCheck_Click(object sender, EventArgs e)
        {
            Total = 0;
            var txtSource = this.txtSource.Text;
            var txtTarget = this.txtTarget.Text;

            var isRegular = this.cbxRegular.Checked; // 是否正则表达式替换

            if (!isRegular)
            {
                this.lblResult.Text = "请选中正则匹配复选框！";
                return;
            }

            string[] regStr = new[] {"[", "]"};
            if (txtSource.IndexOf("*", StringComparison.Ordinal) > -1)
            {
                regStr = txtSource.Split('*');
            }

            var paramList = new Dictionary<string, string> { };
            List<Medias> list = MediaService.GetMediaList(paramList);

            foreach (var media in list)
            {
                var strMatch =
                    Regex.Match(media.Title, @"([" + regStr[0] + "].*[" + regStr[1] + "])", RegexOptions.IgnoreCase)
                        .Value;

                if (strMatch.Length > 0)
                {
                    Total++;
                }
            }

            this.lblResult.Text = "共匹配" + Total + "个文件！";
        }

        #endregion

    }
}