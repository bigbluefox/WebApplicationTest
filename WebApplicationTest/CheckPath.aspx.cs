using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class CheckPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChkPath_Click(object sender, EventArgs e)
        {
            var strPath = txtPath.Text;
            if (string.IsNullOrEmpty(strPath))
            {
                this.lblMsg.Text = "请输入目录名称";
                return;
            }
        }

        /// <summary>
        /// 检查年代号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPromulgatedReign_Click(object sender, EventArgs e)
        {
            var strFileName = txtPath.Text;
            if (string.IsNullOrEmpty(strFileName))
            {
                this.lblMsg.Text = "请输入目录名称";
                return;
            }
            var txtPromulgatedReig = "";

            //提取日期的正则表达式
            Regex reg = new Regex(@"((?<!\d)((\d{2,4}(\.|年|\/|\-))((((0?[13578]|1[02])(\.|月|\/|\-))((3[01])|([12][0-9])|(0?[1-9])))|(0?2(\.|月|\/|\-)((2[0-8])|(1[0-9])|(0?[1-9])))|(((0?[469]|11)(\.|月|\/|\-))((30)|([12][0-9])|(0?[1-9]))))|((([0-9]{2})((0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))(\.|年|\/|\-))0?2(\.|月|\/|\-)29))日?(?!\d))");

            reg = new Regex(@"(-[1-2][0-9][0-9][0-9])\s");

            var strRegex = @"([-][1-2][0-9][0-9][0-9])\s"; //@"([-][1-2][0-9][0-9][0-9])\s"

            foreach (Match m in Regex.Matches(strFileName, strRegex))
            {
                string temp = m.Groups[1].Value;//滤除全为0的
                if (temp != "")
                   txtPromulgatedReig += " * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length + " => " + temp;
            }

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

            lblPromulgatedReign.Text = strFileName + txtPromulgatedReig;

            lblStandType.Text = StandType(strStandardNumber.Trim());
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
            foreach (Match m in Regex.Matches(name, @"([-][1-2][0-9][0-9][0-9])\s"))
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
            var type = ""; // @"([a-zA-Z]+)\s"))
            foreach (Match m in Regex.Matches(name, @"([a-zA-Z/]+)\s"))
            {
                if (m.Groups[1].Value == "") continue;
                type = m.Groups[1].Value;
            }
            return type;
        }
    }
}