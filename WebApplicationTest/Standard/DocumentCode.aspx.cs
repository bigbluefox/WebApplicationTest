using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

namespace WebApplicationTest.Standard
{
    public partial class DocumentCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 标准体系代码解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDocumentCode_Click(object sender, EventArgs e)
        {
            var fileValue = FileUpload1.FileName;
            var postFileName = FileUpload1.PostedFile.FileName;

            var documentCode = this.txtDocumentCode.Text;

            if (string.IsNullOrEmpty(documentCode))
            {
                documentCode = fileValue;
                this.txtDocumentCode.Text = postFileName;
            }

            if (string.IsNullOrEmpty(documentCode))
            {
                this.lblResult.Text = "请输入要解析的标准名称";
                return;
            }

            Dictionary<string, string> keyValues = null;
            DocumentInfo info = StandardCodeProcess(documentCode, keyValues);

            this.lblResult.Text = info.DocumentCode;

        }


        #region 标准编号处理

        /// <summary>
        /// 替换文档编码非法字符
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static string CleanInvalidDocumentCode(string strCode)
        {
            if (String.IsNullOrEmpty(strCode)) return "";
            strCode = strCode.Replace('【', '（').Replace('】', '）'); // 方括号处理
            strCode = strCode.Replace('(', '（').Replace(')', '）'); // 半角括号处理，防止影响正则表达式
            strCode = strCode.Replace("　", " ").Replace("  ", " "); // 全角空格处理
            return strCode;
        }

        /// <summary>
        ///     标准编号处理
        /// </summary>
        /// <param name="name">标准文件名称</param>
        /// <param name="keyValues">标准编号修正键值对</param>
        /// <returns></returns>
        public static DocumentInfo StandardCodeProcess(string name, Dictionary<string, string> keyValues)
        {
            name = CleanInvalidDocumentCode(name);
            DocumentInfo documentInfo = new DocumentInfo();
            documentInfo.FileName = name; // 原始文件名称(经过修正)
            documentInfo.DocumentName = name; // 标准名称
            string strBaseCode = "", strStandardCode = ""; // 标准基础编号，标准编号

            #region 标准属性处理

            var strFileName = name.Replace("／", "/");
            strStandardCode = StandardHelper.StandardCode(strFileName); // 标准号处理
            var strStandardName = string.IsNullOrEmpty(strStandardCode)
                ? strFileName
                : strFileName.Replace(strStandardCode, ""); // 标准名称，不包括标准编号

            if (!string.IsNullOrEmpty(strStandardCode))
                strStandardCode = strStandardCode.Replace("　", " ").Replace("  ", " "); // 全角空格处理

            #region 标准年号处理

            var strPromulgatedReig = string.IsNullOrEmpty(strStandardCode)
                ? ""
                : StandardHelper.PromulgatedReign(strStandardCode); // 标准年号

            if (string.IsNullOrEmpty(strPromulgatedReig)) return documentInfo; // 如果标准年号为空，则返回

            strPromulgatedReig = strPromulgatedReig.Replace("—", "-").Replace("－", "-").Trim('-').Trim();

            #endregion

            #region 基础编号处理

            var strStandardBaseCode = string.IsNullOrEmpty(strStandardCode)
                ? ""
                : string.IsNullOrEmpty(strPromulgatedReig)
                    ? ""
                    : strStandardCode.Replace(strPromulgatedReig, ""); // 基础编号

            #endregion

            #region 标准属性处理

            var strPreCode = string.IsNullOrEmpty(strStandardCode)
                ? ""
                : StandardHelper.StandardPreCode(strStandardBaseCode); // 标准属性
            //var strStandClass = string.IsNullOrEmpty(strStandardCode) ? "" : this.StandClass(strPreCode); // 标准类型

            #endregion

            #region 补充标准编号处理

            if (string.IsNullOrEmpty(strStandardCode))
            {
                strStandardCode = StandardHelper.StandardCodeExt(strFileName); // 标准号 A100
                strStandardName = string.IsNullOrEmpty(strStandardCode)
                    ? strFileName
                    : strFileName.Replace(strStandardCode, ""); // 文件名称，不包括标准编号
                //strStandardBaseCode = strStandardCode;
                strPreCode = string.IsNullOrEmpty(strStandardCode)
                    ? ""
                    : StandardHelper.StandardPreCodeExt(strStandardCode); // 标准属性
            }

            #endregion

            strStandardCode = string.IsNullOrEmpty(strStandardCode)
                ? ""
                : strStandardCode.Replace("—", "-").Replace("－", "-");

            strStandardName = strStandardName
                .Trim()
                .Trim('.')
                .Trim()
                .Trim('-').Trim();
            strPreCode = strPreCode.Trim();

            if (!string.IsNullOrEmpty(strPreCode))
            {
                var strOldPreCode = strPreCode.Trim();
                //strPreCode = keyValues.ContainsKey(strOldPreCode) ? keyValues[strOldPreCode] : "";
                if (string.IsNullOrEmpty(strPreCode)) strPreCode = strOldPreCode;
                strPreCode = strPreCode.Trim();
                //if (strStandardBaseCode != null &&
                //    strStandardBaseCode.IndexOf(" ", StringComparison.Ordinal) == -1)
                //{
                //    strPreCode += " "; // 如果前缀后面没有空格，则补上之
                //}
                strStandardCode = strStandardCode.Replace(strOldPreCode, strPreCode).Trim();
                strStandardBaseCode = strStandardBaseCode.Replace(strOldPreCode, strPreCode);
            }

            #endregion

            strBaseCode = strStandardBaseCode.Trim()
                .Trim('-').Trim();

            strPromulgatedReig = strPromulgatedReig.Replace(".", "").Trim();

            documentInfo.DocumentName = strStandardName; // 标准名称
            documentInfo.DocumentCode = strStandardCode; // 标准号
            documentInfo.PreCode = strPreCode.Trim();
            documentInfo.BaseCode = strBaseCode;
            documentInfo.PromulgatedReign = strPromulgatedReig; // 标准年号

            return documentInfo;
        }

        #endregion

    }


    /// <summary>
    /// 文件实体
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        [DataMember]
        public string DocumentName { get; set; }

        /// <summary>
        /// 标准编号
        /// </summary>
        [DataMember]
        public string DocumentCode { get; set; }

        /// <summary>
        /// 文档编码前导部分，标准代号
        /// </summary>
        [DataMember]
        public string PreCode { get; set; }

        /// <summary>
        /// 标准基础编号(不含年代号)
        /// </summary>
        [DataMember]
        public string BaseCode { get; set; }

        /// <summary>
        /// 文档编码年号部分
        /// </summary>
        [DataMember]
        public string PromulgatedReign { get; set; }

        /// <summary>
        /// StandardType
        /// </summary>
        //[DataMember]
        //public int StandardType { get; set; }

        /// <summary>
        /// 文档编码格式化副本
        /// </summary>
        //[DataMember]
        //public string FormatCode { get; set; }


        /// <summary>
        /// 文档编码附属部分
        /// </summary>
        //[DataMember]
        //public string AddCode { get; set; }


        /// <summary>
        /// 文档编码排序码
        /// </summary>
        //[DataMember]
        //public string OrdCode { get; set; }

    }
}