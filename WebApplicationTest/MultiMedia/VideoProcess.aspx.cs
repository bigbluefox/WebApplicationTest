using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

namespace WebApplicationTest.MultiMedia
{
    public partial class VideoProcess : System.Web.UI.Page
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total = 0;

        /// <summary>
        /// 本地标准数据
        /// </summary>
        public List<StandardLocal> List = new List<StandardLocal>();

        /// <summary>
        ///    本地标准服务
        /// </summary>
        internal readonly IStandardService StandardService = new StandardService();

        protected void Page_Load(object sender, EventArgs e)
        {
            //var s = "Xart - Victoria's Secret (P Boy) N Beach";
            //s = HttpUtility.UrlPathEncode(s);
            //lblResult.Text = s;
        }

        /// <summary>
        /// 视频检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVideoProcess_Click(object sender, EventArgs e)
        {
            var strLocalPath = txtSourcePath.Text;
            if (string.IsNullOrEmpty(strLocalPath)) return;

            List = new List<StandardLocal>();
            var list = new List<StandardLocal>();
            var di = new DirectoryInfo(strLocalPath);

            FindVideoFile(di, ref list); // 视频文件查找

            if (list.Count > 0)
            {
                var rst = StandardService.AddStandards(list);
            }

            lblResult.Text = Total.ToString();

        }

        /// <summary>
        /// 递归图片文件查找
        /// </summary>
        /// <param name="di"></param>
        /// <param name="list"></param>
        public void FindVideoFile(DirectoryInfo di, ref List<StandardLocal> list)
        {
            try
            {
                foreach (var file in di.GetFiles())
                {
                    if (file.Extension.Length < 2 || !IsVideo(file.Extension)) continue;
                    if (file.Length < 1048576 * 2) continue;
                    var strFileName = file.Name;

                    var model = new StandardLocal();
                    model.A301 = strFileName.Replace(file.Extension, "").Replace("'", "''");
                    model.A107 = "";
                    model.A100 = "";
                    model.StandClass = "";
                    model.StandType = "";
                    model.StandPreNo = "";

                    model.FileSize = (int) file.Length;
                    model.FileExt = file.Extension;
                    model.ContentType = Hsp.Test.Common.MimeMapping.GetMimeMapping(file.FullName);

                    model.FileName = file.Name.Replace("'", "''");
                    model.FullName = file.FullName.Replace("'", "''");
                    if (file.DirectoryName != null) model.DirectoryName = file.DirectoryName.Replace("'", "''");

                    // SELECT FileId, FileName, FullName, DirectoryName, FileExt, FileSize, ContentType
                    // , StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1    
                    // FROM Standard_Local

                    model.MD5 = (cbxMd5.Checked) ? HashHelper.ComputeMD5(file.FullName) : "";
                    model.SHA1 = (cbxSha1.Checked) ? HashHelper.ComputeSHA1(file.FullName) : "";

                    List.Add(model);
                    list.Add(model);
                    Total++;
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                //throw;
            }
            finally
            {
                Dispose();
            }

            if (list.Count > 999)
            {
                if (list.Count > 0)
                {
                    var rst = StandardService.AddStandards(list);
                }

                list = new List<StandardLocal>();
            }

            try
            {
                DirectoryInfo[] dis = di.GetDirectories();
                foreach (DirectoryInfo t in dis)
                {
                    FindVideoFile(t, ref list);
                }
            }
            catch (Exception ex)
            {
                var b = ex.Message;
                //throw;
            }
            finally
            {
                Dispose();
            }

        }


        /// <summary>
        /// 视频名称部分字符替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRename_Click(object sender, EventArgs e)
        {
            Total = 0;
            var txtSource = this.txtSourceName.Text;
            var txtTarget = this.txtTargetName.Text;

            if (txtSource.Length == 0)
            {
                this.lblResult.Text = "请输入被替换的字符！";
                return;
            }

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                if (standard.FileName.IndexOf(txtSource, StringComparison.Ordinal) <= -1 ||
                    !File.Exists(standard.FullName)) continue;
                try
                {
                    var strNewName =
                        Path.Combine(standard.DirectoryName + "\\" + standard.FileName.Replace(txtSource, txtTarget));
                    FileInfo fileInfo = new FileInfo(standard.FullName);
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

            this.lblResult.Text = "共替换" + Total + "个文件！";
        }

        /// <summary>
        ///     是否视频文件
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public bool IsVideo(string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "mp4,avi,wmv,mpg,mpeg,mov,rm,rmvb,flv,mkv";
            return strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
        }

        /// <summary>
        /// 名称替换检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            List = new List<StandardLocal>();

            string exp = txtExpression.Text, target = txtTarget.Text;

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                var source = standard.FileName;
                Regex regex = new Regex(exp, RegexOptions.IgnoreCase);

                MatchCollection matches = regex.Matches(source);
                if (matches.Count <= 0) continue;

                foreach (Match item in matches)
                {
                    var strReplace = item.Groups[0].ToString();
                    if (string.IsNullOrEmpty(strReplace)) continue;

                    source = Regex.Replace(source, exp, target);
                    standard.A301 = source;
                    List.Add(standard);
                }
            }

            this.lblResult.Text = "共检索到" + List.Count + "个文件！";
        }
    }
}