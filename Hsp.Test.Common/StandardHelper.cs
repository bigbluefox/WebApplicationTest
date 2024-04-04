using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    /// 标准体系帮助类
    /// </summary>
    public class StandardHelper
    {
        #region 标准文件名年号处理

        /// <summary>
        ///     标准文件名年号处理
        /// </summary>
        /// <param name="name">标准文件名</param>
        /// <param name="index">年号索引序号</param>
        /// <returns></returns>
        public static string PromulgatedReign(string name, out int index)
        {
            index = 0;
            var reign = "";
            var exp = @"(([-－—. ][1][0-9][0-9][0-9])[\s]?)|(([-－—. ][2][0][0-1][0-9])[\s]?)";
            foreach (Match m in Regex.Matches(name, exp))
            {
                if (m.Groups[1].Value == "") continue;
                //" * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length
                reign = m.Groups[1].Value;
                index = m.Index;
            }
            return reign;
        }

        /// <summary>
        /// 标准文件名年号处理
        /// </summary>
        /// <param name="code">标准编号</param>
        /// <returns></returns>
        public static string PromulgatedReign(string code)
        {
            var reign = "";
            var exp = @"(([-－—. ][1][0-9][0-9][0-9])[ ]?)|(([-－—. ][2][0][0-1][0-9])[ ]?)";
            foreach (Match m in Regex.Matches(code, exp))
            {
                if (m.Groups[0].Value == "") continue;
                reign = m.Groups[0].Value;
            }
            return reign;
        }

        #endregion

        #region 去掉文档编码中的年号

        /// <summary>
        /// 去掉文档编码中的年号
        /// </summary>
        /// <param name="documentCode">文档编码</param>
        /// <returns></returns>
        public static string GetDocumentCodeWithoutReign(string documentCode)
        {
            if (string.IsNullOrEmpty(documentCode))
            {
                documentCode = "";
            }
            else
            {
                documentCode = documentCode.Trim().Trim('　').Replace("—", "-");
                if (documentCode.LastIndexOf("-", StringComparison.Ordinal) > 0)
                {
                    var codeArr = documentCode.Split('-');
                    if (codeArr.Length > 1)
                    {
                        var year = 0;
                        int.TryParse(codeArr[codeArr.Length - 1], out year);
                        if (year > 1970 && year < 9999)
                        {
                            documentCode = documentCode.Substring(0,
                                documentCode.LastIndexOf("-", StringComparison.Ordinal));
                        }
                    }
                }
                documentCode += "　";
            }
            return documentCode;
        }

        /// <summary>
        /// 去掉字符串中的年号
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string GetStringWithoutReign(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.Trim().Trim('　').Replace("—", "-");
            Regex regex = new Regex(@"[-－][ ]?[1-2][0-9]{3}");
            string returnStr = regex.Match(s).Value;
            return string.IsNullOrEmpty(returnStr) ? s : s.Replace(returnStr, "");
        }

        #endregion

        #region 标准类型

        /// <summary>
        /// 标准类型
        /// </summary>
        /// <param name="type">标准属性</param>
        /// <returns></returns>
        public static string StandClass(string type)
        {
            var className = "";
            //国家标准[CN]，国际标准[ISO]，国外标准[GW]，行业标准[QT]，计量规程规范[JJ]

            // 国家标准[CN]
            if (type.ToUpper().IndexOf("GB", StringComparison.Ordinal) > -1)
            {
                className = "CN";
            }

            // 国际标准[ISO]
            if (type.ToUpper().IndexOf("ISO", StringComparison.Ordinal) > -1 ||
                type.ToUpper().IndexOf("ITC", StringComparison.Ordinal) > -1)
            {
                className = "ISO";
            }

            return className;
        }

        #endregion

        #region 提取标准属性

        /// <summary>
        ///     提取标准属性(标准代码前缀)
        /// </summary>
        /// <param name="name">标准编号</param>
        /// <returns></returns>
        public static string StandardPreCode(string name)
        {
            var type = "";
            foreach (Match m in Regex.Matches(name, @"([a-zA-Z/]+[ ]?)"))
            {
                if (m.Groups[1].Value == "") continue;
                type = m.Groups[1].Value;
            }
            return type;
        }

        /// <summary>
        ///     提取标准属性(标准代码前缀)
        /// </summary>
        /// <param name="name">标准编号</param>
        /// <returns></returns>
        public static string StandardPreCodeExt(string name)
        {
            var type = "";
            foreach (Match m in Regex.Matches(name, @"([a-zA-Z]+[ 　]{1})"))
            {
                if (m.Groups[1].Value.Trim() == "") continue;
                type = m.Groups[1].Value;
            }
            return type;
        }

        #endregion

        #region 提取标准代码

        /// <summary>
        /// 提取标准代码
        /// </summary>
        /// <param name="name">标准文件名称</param>
        /// <returns></returns>
        public static string StandardCode(string name)
        {
            string exp =
                @"([a-zA-Z/]+[ ]?[-0-9. ~～、—]+[-－—. ][1][9][0-9][0-9][ ]?)|([a-zA-Z/]+[ ]?[-0-9. ~～、—]+[-－—. ][2][0][0-3][0-9][ ]?)";
            return ContentExtract(name, exp);
        }

        /// <summary>
        /// 提取标准代码扩展方法
        /// </summary>
        /// <param name="name">标准文件名称</param>
        /// <returns></returns>
        public static string StandardCodeExt(string name)
        {
            string exp = @"([a-zA-Z0-9 　.~～、—─－-]+)([ 　]{1})";
            return ContentExtract(name, exp);
        }

        #endregion

        #region 正则表达式提取

        /// <summary>
        /// 正则表达式提取
        /// </summary>
        /// <param name="sour">源字符串</param>
        /// <param name="exp">正则表达式</param>
        /// <returns>处理结果</returns>
        public static string ContentExtract(string sour, string exp)
        {
            string result = "";
            Regex regex = new Regex(exp, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(sour);
            return matches.Count <= 0
                ? result
                : matches.Cast<Match>().Aggregate(result, (current, item) => current + item.Groups[0].ToString());
            //foreach (Match item in matches)
            //{
            //    result += item.Groups[0].ToString();
            //}
            //return result;
        }

        #endregion

        #region 正则表达式替换内容

        /// <summary>
        /// 正则表达式替换内容
        /// </summary>
        /// <param name="sour">源字符串</param>
        /// <param name="exp">正则表达式</param>
        /// <returns>处理结果</returns>
        public static string ContentReplace(string sour, string exp)
        {
            string result = "";
            Regex regex = new Regex(exp, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(sour);
            if (matches.Count > 0)
            {
                foreach (Match item in matches)
                {
                    var strReplace = item.Groups[0].ToString();
                    sour = sour.Replace(strReplace, "");
                }
            }

            sour = sour.Replace("\n\r\n\r", "\n\r");
            return sour;
        }

        #endregion

    }
}
