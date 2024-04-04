using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Regular
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string str = "window NT, window CE";
            //var exp = "^(.*?)(NT)(.*?)(CE)$";
            //var target = "$1$4$3$2";
            //string s = Regex.Replace(str, exp, target);
            ////Console.WriteLine(s);   //window CE, window NT 

            //txtReplaceResult.Text = s;
        }

        #region 字符匹配

        /// <summary>
        /// 字符匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            var source = txtSource.Text;
            var exp = txtExpression.Text;
            var rst = Contentextract(source, exp);
            txtMatchResult.Text = rst;

        }

        /// <summary>
        /// 正则表达式提取和替换内容
        /// </summary>
        public static string Contentextract(string sour, string exp)
        {
            string result = "";
            //string str = "大家好! <User EntryTime='2010-10-7' Email='zhangsan@163.com'>张三</User> 自我介绍。";
            //Regex regex = new Regex(@"<User\s*EntryTime='(?<time>[\s\S]*?)'\s+Email='(?<email>[\s\S]*?)'>(?<userName>[\s\S]*?)</User>", RegexOptions.IgnoreCase);

            Regex regex = new Regex(exp, RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(sour);
            if (matches.Count > 0)
            {
                //string userName = match.Groups["userName"].Value; //获取用户名
                //string time = match.Groups["time"].Value; //获取入职时间
                //string email = match.Groups["email"].Value; //获取邮箱地址
                //string strFormat = String.Format("我是：{0}，入职时间：{1}，邮箱：{2}", userName, time, email);
                //result = regex.Replace(sour, strFormat); //替换内容
                //Console.WriteLine(result);

                foreach (Match item in matches)
                {
                    if (item.Groups[0].Value == "") continue;
                    for (int i = 0; i < item.Groups.Count; i++)
                    {
                        result += item.Groups[i].ToString() + Environment.NewLine;
                    }
                    //result += item.Groups[0].ToString() + Environment.NewLine;
                }


            }
            return result; //结果：大家好！我是张三，入职时间：2010-10-7，邮箱：zhangsan@163.com 自我介绍。
        }

        #endregion


        /// <summary>
        /// 字符替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReplace_Click(object sender, EventArgs e)
        {
            var source = txtSource.Text;
            var exp = txtExpression.Text;
            var target = txtTarget.Text;

            //string result = "";

            source = RegexReplace(exp, source, target);

            txtReplaceResult.Text = source;
        }

        private static string RegexReplace(string exp, string source, string target)
        {
            Regex regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            MatchCollection matches = regex.Matches(source);
            if (matches.Count > 0)
            {
                foreach (Match item in matches)
                {
                    var strReplace = item.Groups[0].ToString();
                    if (!string.IsNullOrEmpty(strReplace))
                    {
                        source = Regex.Replace(source, exp, target);
                        //source = source.Replace(strReplace, target);
                    }
                }
            }

            source = source.Replace("\n\r\n\r", "\n\r");
            return source;
        }
    }
}

/*
 1、标准代码提取表达式：([a-zA-Z/]+[ ]?[0-9]+[.]?[0-9]*[-－—. ][1][0-9][0-9][0-9][ ]?)|([a-zA-Z/]+[ ]?[0-9]+[.]?[0-9]*[-－—. ][2][0][0-1][0-9][ ]?)
 
 */