using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Crud
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var strFileName = "GBT 2013-2012能源管理体系  要求";

            // 标准年号处理
            var idx = 0; // 年号顺序索引序号
            var strPromulgatedReig = PromulgatedReign(strFileName, out idx).Trim(); // 标准年号
            var strStandardNumber = string.IsNullOrEmpty(strPromulgatedReig)
                ? ""
                : strFileName.Substring(0, idx); // 标准编号 (基础) A107
            var strStandardCode = string.IsNullOrEmpty(strPromulgatedReig)
                ? ""
                : strStandardNumber + strPromulgatedReig; // 标准号 A100
            var strStandardName = string.IsNullOrEmpty(strPromulgatedReig)
                ? ""
                : strFileName.Substring(idx + 5).Trim(); // 标准名称 A301
            var strStandType = string.IsNullOrEmpty(strPromulgatedReig)
                ? ""
                : StandType(strStandardNumber).Trim(); // 标准属性

            Label1.Text = strPromulgatedReig + " * " + strStandardNumber + " * " + strStandardCode + " * " + strStandardName + " * " + strStandType;
        }

        /// <summary>
        ///     标准文件名年号处理
        /// </summary>
        /// <param name="name">标准文件名</param>
        /// <param name="index">年号索引序号</param>
        /// <returns></returns>
        internal string PromulgatedReign(string name, out int index)
        {
            index = 0;
            var reign = "";
            foreach (Match m in Regex.Matches(name, @"([-][1-2][0-9][0-9][0-9])[\s]?"))
            {
                if (m.Groups[1].Value == "") continue;
                //" * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length
                reign = m.Groups[1].Value;
                index = m.Index;
            }
            return reign;
        }

        /// <summary>
        ///     标准属性处理
        /// </summary>
        /// <param name="name">标准编号</param>
        /// <returns></returns>
        internal string StandType(string name)
        {
            var type = "";
            foreach (Match m in Regex.Matches(name, @"([a-zA-Z/]+)\s"))
            {
                if (m.Groups[1].Value == "") continue;
                type = m.Groups[1].Value;
            }
            return type;
        }

    }
}