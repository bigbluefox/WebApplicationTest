using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

public partial class SQLite_ContentCrawl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 页面内容抓取
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCrawl_Click(object sender, EventArgs e)
    {
        string getUrl = "http://www.sohu.com/a/205380121_428290?_f=index_news_1";
        //string getUrl = "http://www.jueceji.com/login/AjaxRegValid?type=email&validValue=ansenwork@163.com";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(getUrl);
        req.Method = "GET";

        HttpWebResponse res = null;
        Stream st = null;
        StreamReader sr = null;
        string html = string.Empty;

        try
        {
            res = (HttpWebResponse)req.GetResponse();
            st = res.GetResponseStream();
            sr = new StreamReader(st, System.Text.Encoding.UTF8);
            //Console.WriteLine(sr.CurrentEncoding);
            html = sr.ReadToEnd();

            //MatchCollection TitleMatchs = Regex.Matches(html, @"发表评论</a></p></div><div class=""body"">([\s\S]*?)</div><div class=""share"">", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //foreach (Match NextMatch in TitleMatchs)
            //{
            //    s += "<br>" + NextMatch.Groups[1].Value;
            //    TextBox1.Text += "\n" + NextMatch.Groups[1].Value;
            //}

            //var startIndex = html.LastIndexOf("<div class=\"text\">", StringComparison.Ordinal);
            //var endInex = html.IndexOf("<div class=\"article-oper-bord article-oper-bord\">", StringComparison.Ordinal);
            //var contentCrawl = html.Substring(startIndex, endInex - startIndex);

            #region 获取文章内容主体

            var exp = @"<div class=""text"">([\s\S]*?)<div class=""article-oper-bord article-oper-bord"">";
            var contentCrawl = CrawlHelper.ContentFetch(html, exp);

            #endregion

            #region 获取标题

            exp = @"<div class=""text-title"">([\s\S]*?)<span class=""article-tag"">";
            var title = CrawlHelper.ContentFetch(contentCrawl, exp);
            if (title.Length > 0) title = CrawlHelper.StripHtml(title);

            txtTitle.Text = title;

            #endregion

            #region 获取来源

            exp = @"<span data-role=""original-link"">来源:([\s\S]*?)<span class=""tag"">";
            var source = CrawlHelper.ContentFetch(contentCrawl, exp);
            if (source.Length > 0) source = CrawlHelper.StripHtml(source);

            //MatchCollection sourceMatchs = Regex.Matches(contentCrawl,
            //    @"<span data-role=""original-link"">来源:([\s\S]*?)<span class=""tag"">",
            //    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //foreach (Match nextMatch in sourceMatchs)
            //{
            //    source += nextMatch.Groups[1].Value.Trim();
            //}

            //if (source.Length > 0)
            //{
            //    source = StripHtml(source);
            //}

            txtSource.Text = source;

            #endregion

            #region 获取内容

            exp = @"<article class=""article"">([\s\S]*?)</article>";
            var content = CrawlHelper.ContentFetch(contentCrawl, exp);

            //var content = "";
            //MatchCollection contentMatchs = Regex.Matches(contentCrawl,
            //    @"<article class=""article"">([\s\S]*?)</article>",
            //    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //foreach (Match nextMatch in contentMatchs)
            //{
            //    content += nextMatch.Groups[1].Value.Trim();
            //}

            if (content.Length > 0)
            {
                //content = StripHtml(content);

                #region 原标题内容清除

                exp = @"<p data-role=""original-title""(.)*</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                //Regex regex = new Regex(exp, RegexOptions.IgnoreCase);

                ////MatchCollection matches = regex.Matches(content);
                ////if (matches.Count > 0)
                ////{
                ////    foreach (Match item in matches)
                ////    {
                ////        result += item.Groups[0];
                ////    }
                ////    if (result.Trim().Length > 0)
                ////    {
                ////        content = content.Replace(result, "");
                ////    }
                ////}

                //MatchCollection matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region 视频内容清除

                exp = @"<div id=""sohuplayer"">([\s\S]*?)</div>";
                content = CrawlHelper.ContentCleanup(content, exp);

                //regex = new Regex(exp, RegexOptions.IgnoreCase);

                ////matches = regex.Matches(content);
                ////if (matches.Count > 0)
                ////{
                ////    foreach (Match item in matches)
                ////    {
                ////        result += item.Groups[0];
                ////    }

                ////    if (result.Trim().Length > 0)
                ////    {
                ////        content = content.Replace(result, "");
                ////    }
                ////}

                //matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region 视频标题清除

                exp = @"<p class=""video-title"">>([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                //regex = new Regex(exp, RegexOptions.IgnoreCase);

                //matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region 视频标题清除

                exp = @"<p class=""video-title"">([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                //regex = new Regex(exp, RegexOptions.IgnoreCase);

                //matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region A标签内容清除

                exp = @"<a href=""//www.sohu.com([\s\S]*?)</a>";
                content = CrawlHelper.ContentCleanup(content, exp);

                //regex = new Regex(exp, RegexOptions.IgnoreCase);

                //matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region 责任编辑内容清除

                exp = @"<p data-role=""editor-name"">([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);
                //regex = new Regex(exp, RegexOptions.IgnoreCase);

                //matches = regex.Matches(content);
                //if (matches.Count > 0)
                //{
                //    foreach (Match item in matches)
                //    {
                //        var strReplace = item.Groups[0].ToString();
                //        content = content.Replace(strReplace, "");
                //    }
                //}

                //if (content.Length > 0)
                //{
                //    content = content.Replace("\n\r\n\r", "\n\r");
                //}

                #endregion

                #region 声明内容清除

                exp = @"<div class=""statement"">([\s\S]*?)</div>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region 匹配P标签内容

                var result = "";
                exp = @"<p[^>]*>(?:(?!<\/p>)[\s\S])*<\/p>";
                var regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                MatchCollection matches = regex.Matches(content);
                if (matches.Count > 0)
                {
                    foreach (Match item in matches)
                    {
                        result += "<p>" + Hsp.Test.Common.CrawlHelper.StripHtml(item.Groups[0].ToString()).Trim() + "</p>" + Environment.NewLine;
                    }
                }

                #endregion

                this.txtContent.Text = result;
            }

            txtSource.Text = source;

            #endregion

            this.txtCrawl.Text = contentCrawl;
            //this.txtContent.Text = content;
        }
        catch (IOException ex)
        {
            html = ex.Message;
        }
        catch (Exception ex)
        {
            html = ex.Message;
        }
        finally
        {
            if (res != null)
            {
                res.Close();
            }
            if (st != null)
            {
                st.Close();
            }
            if (sr != null)
            {
                sr.Close();
            }
        }

        //this.TextBox2.Text = html;
    }

}