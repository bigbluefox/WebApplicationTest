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
using Image = System.Drawing.Image;

namespace WebApplicationTest.Standard
{
    public partial class Local : System.Web.UI.Page
    {
        public int Total = 0;

        /// <summary>
        ///    本地标准服务
        /// </summary>
        internal readonly IStandardService StandardService = new StandardService();

        /// <summary>
        ///    标准文件服务
        /// </summary>
        internal readonly IStandardFileService StandardFileService = new StandardFileService();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.txtLocalPath.Text = @"E:\Htty\标准库";
        }

        /// <summary>
        /// 本地标准文件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStandardProcess_Click(object sender, EventArgs e)
        {
            Total = 0;
            var strLocalPath = txtLocalPath.Text;
            if(string.IsNullOrEmpty(strLocalPath)) return;

            var list = new List<StandardLocal>();
            var di = new DirectoryInfo(strLocalPath);

            FindImageFile(di, ref list); // 标准文件查找

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
        public void FindImageFile(DirectoryInfo di, ref List<StandardLocal> list)
        {
            foreach (var file in di.GetFiles())
            {
                //if (file.Extension.Length > 4) continue;
                if (file.Extension.Length == 0 || !IsValidFile(file.Extension)) continue;
                var strFileName = file.Name;

                #region 标准年号处理

                // 标准年号处理
                //var idx = 0; // 年号顺序索引序号
                //var strPromulgatedReig = PromulgatedReign(strFileName, out idx).Trim(); // 标准年号
                //var strStandardBaseCode = string.IsNullOrEmpty(strPromulgatedReig)
                //    ? ""
                //    : strFileName.Substring(0, idx); // 标准编号 (基础) A107
                //var strStandardCode = string.IsNullOrEmpty(strPromulgatedReig)
                //    ? ""
                //    : strStandardBaseCode + strPromulgatedReig; // 标准号 A100
                //var strStandardName = string.IsNullOrEmpty(strPromulgatedReig)
                //    ? ""
                //    : strFileName.Substring(idx + 5).Trim(); // 标准名称 A301
                //var strPreCode = string.IsNullOrEmpty(strPromulgatedReig)
                //    ? ""
                //    : StandType(strStandardBaseCode).Trim(); // 标准属性

                //var strStandClass = string.IsNullOrEmpty(strStandType)
                //    ? ""
                //    : StandClass(strStandType); // 标准类型

                #endregion

                #region 标准属性处理

                //var strStandardCode = StandardHelper.StandardCode(strFileName); // 标准号 A100
                //var strStandardName = string.IsNullOrEmpty(strStandardCode) ? strFileName : strFileName.Replace(strStandardCode, ""); // 文件名称，不包括标准编号
                //if (!string.IsNullOrEmpty(strStandardCode)) strStandardCode = strStandardCode.Replace("　", " ").Replace("  ", " "); // 全角空格处理
                //var strPromulgatedReig = string.IsNullOrEmpty(strStandardCode) ? "" : StandardHelper.PromulgatedReign(strStandardCode); // 标准年号
                //var strStandardBaseCode = string.IsNullOrEmpty(strStandardCode) ? "" : string.IsNullOrEmpty(strPromulgatedReig) ? "" : strStandardCode.Replace(strPromulgatedReig, ""); // 基础编号 A107
                //var strPreCode = string.IsNullOrEmpty(strStandardCode) ? "" : StandardHelper.StandardPreCode(strStandardBaseCode); // 标准属性
                //var strStandClass = string.IsNullOrEmpty(strStandardCode) ? "" : StandardHelper.StandClass(strPreCode); // 标准类型
                //strStandardCode = string.IsNullOrEmpty(strStandardCode) ? "" : strStandardCode.Replace("—", "-").Replace("－", "-");
                //strStandardName = strStandardName.Replace(file.Extension, "").Trim();

                //if (!string.IsNullOrEmpty(strPreCode))
                //{
                //    var strOldPreCode = strPreCode.Trim();
                //    strPreCode = StandardFileService.PreCodeCorresponding(strPreCode);
                //    if (string.IsNullOrEmpty(strPreCode)) strPreCode = strOldPreCode;
                //    strPreCode = strPreCode.Trim();
                //    if (strStandardBaseCode.IndexOf(" ", StringComparison.Ordinal) == -1)
                //    {
                //        strPreCode += " ";  // 如果前缀后面没有空格，则补上之
                //    }
                //    strStandardCode = strStandardCode.Replace(strOldPreCode, strPreCode).Trim();
                //    strStandardBaseCode = strStandardBaseCode.Replace(strOldPreCode, strPreCode);
                //}

                #endregion

                var model = new StandardLocal();
                //model.A301 = strStandardName.Replace(file.Extension, "").Trim(); // 字符串的长度不能为零。
                //model.A107 = strStandardBaseCode.Length < 32 ? strStandardBaseCode : strStandardBaseCode.Substring(0, 31); // 标准编号 (基础) A107
                //model.A100 = strStandardCode.Length < 32 ? strStandardCode : strStandardCode.Substring(0, 31); // 标准号 A100
                //model.StandClass = strStandClass;
                //model.StandType = strPreCode;
                //model.StandPreNo = strPreCode;

                model.FileSize = (int)file.Length;
                model.FileExt = file.Extension;
                model.ContentType = Hsp.Test.Common.MimeMapping.GetMimeMapping(file.FullName);

                model.FileName = file.Name;
                model.FullName = file.FullName;//.Length < 127 ? file.FullName : file.FullName.Substring(0, 127);
                model.DirectoryName = file.DirectoryName;

                //model.A825 = string.IsNullOrEmpty(strPromulgatedReig) ? "" : strPromulgatedReig.Trim().Replace(".", "").Replace("—", "").Replace("－", "").Replace("-", "");

                // SELECT FileId, FileName, FullName, DirectoryName, FileExt, FileSize, ContentType
                // , StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1    
                // FROM Standard_Local

                model.MD5 = (cbxMd5.Checked) ? HashHelper.ComputeMD5(file.FullName) : "";
                //model.SHA1 = (cbxSha1.Checked) ? HashHelper.ComputeSHA1(file.FullName) : "";

                list.Add(model);
                Total++;
            }

            if (list.Count > 999)
            {
                if (list.Count > 0)
                {
                    var rst = StandardService.AddStandards(list);
                }
                
                list = new List<StandardLocal>();
            }

            DirectoryInfo[] dis = di.GetDirectories();
            foreach (DirectoryInfo t in dis)
            {
                //Console.WriteLine("目录：" + t.FullName);
                FindImageFile(t, ref list);
            }
        }

        /// <summary>
        ///     标准文件名年号处理
        /// </summary>
        /// <param name="name">标准文件名</param>
        /// <param name="index">年号索引序号</param>
        /// <returns></returns>
        //internal string PromulgatedReign(string name, out int index)
        //{
        //    index = 0;
        //    var reign = "";
        //    foreach (Match m in Regex.Matches(name, @"([-][1-2][0-9][0-9][0-9])[\s]?"))
        //    {
        //        if (m.Groups[1].Value == "") continue;
        //        //" * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length
        //        reign = m.Groups[1].Value;
        //        index = m.Index;
        //    }
        //    return reign;
        //}

        /// <summary>
        ///     是否图片文件
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public bool IsValidFile(string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "doc,docx,xls,xlsx,ppt,pptx,pdf,wps,ceb,zip,rar";
            return strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
        }

    }
}