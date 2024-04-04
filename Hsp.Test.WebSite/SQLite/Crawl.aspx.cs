using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

public partial class SQLite_Crawl : System.Web.UI.Page
{
    /// <summary>
    /// 文章服务
    /// </summary>
    internal readonly IArticleService ArticleService = new ArticleService();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 获取搜狐内容

    /// <summary>
    /// 搜狐页面内容抓取
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSohuCrawl_Click(object sender, EventArgs e)
    {
        string html = "";

        try
        {
            string url = "http://www.sohu.com";
            html = CrawlHelper.GetHtml(url);

            var exp = @"<div class=""focus-news"">([\s\S]*?)<div class=""right sidebar"">";
            //var contentCrawl = CrawlHelper.ContentFetch(html, exp);
            var contentCrawl = html;

            #region 匹配a标签内容

            var result = "";
            var i = 0;
            //exp = @"<a(.)*>";
            exp = @"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>";
            var regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(contentCrawl);
            if (matches.Count > 0)
            {
                foreach (Match item in matches)
                {
                    var s = item.Groups[0].ToString();
                    exp = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>";
                    var match = Regex.Match(s, exp, RegexOptions.IgnoreCase);
                    var href = match.Groups["href"].Value;
                    if (href == "#") continue;

                    exp = @"<a[^>]*title=([""'])?(?<title>[^'""]+)\1[^>]*>";
                    match = Regex.Match(s, exp, RegexOptions.IgnoreCase);
                    var title = match.Groups["title"].Value;

                    i++;

                    href = href.Replace("http://", "");
                    href = href.Replace("//", "");
                    href = "http://" + href;
                    result += title + " * " + href + Environment.NewLine;

                    GetSohuContent(href);
                }
            }

            #endregion

            //exp = @"<div class=""main-box clearfix business-news"" data-role=""main-panel"">([\s\S]*?)<li><span data-god-id=";
            //contentCrawl = CrawlHelper.ContentFetch(html, exp);

            #region 匹配P标签内容

            //result = "";
            //i = 0;
            //exp = @"<a(.)*>";
            //regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //matches = regex.Matches(contentCrawl);
            //if (matches.Count > 0)
            //{
            //    foreach (Match item in matches)
            //    {
            //        var s = item.Groups[0].ToString();
            //        exp = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>";
            //        var match = Regex.Match(s, exp, RegexOptions.IgnoreCase);
            //        var href = match.Groups["href"].Value;
            //        if (href == "#") continue;

            //        exp = @"<a[^>]*title=([""'])?(?<title>[^'""]+)\1[^>]*>";
            //        match = Regex.Match(s, exp, RegexOptions.IgnoreCase);
            //        var title = match.Groups["title"].Value;

            //        i++;

            //        href = href.Replace("http://", "");
            //        href = href.Replace("//", "");
            //        href = "http://" + href;
            //        result += title + " * " + href + Environment.NewLine;

            //        GetNewsContent(href);
            //    }
            //}

            #endregion

            lblMessage.Text = i.ToString();
            txtContent.Text = result;

        }
        catch (IOException ex)
        {
            html = ex.Message;
            txtContent.Text = html;
        }
        catch (Exception ex)
        {
            html = ex.Message;
            txtContent.Text = html;
        }
        finally
        {
        }
    }

    /// <summary>
    /// 根据地址获取搜狐内容
    /// </summary>
    /// <param name="url"></param>
    internal void GetSohuContent(string url)
    {
        //string url = "http://www.sohu.com/a/205380121_428290?_f=index_news_1";
        //string url = "http://www.jueceji.com/login/AjaxRegValid?type=email&validValue=ansenwork@163.com";
        //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //req.Method = "GET";

        //HttpWebResponse res = null;
        //Stream st = null;
        //StreamReader sr = null;
        string html = "";

        try
        {
            //res = (HttpWebResponse)req.GetResponse();
            //st = res.GetResponseStream();
            //sr = new StreamReader(st, System.Text.Encoding.UTF8);
            ////Console.WriteLine(sr.CurrentEncoding);
            //html = sr.ReadToEnd();

            html = CrawlHelper.GetHtml(url);
            Article article = new Article();

            #region 获取文章内容主体

            var exp = @"<div class=""text"">([\s\S]*?)<div class=""article-oper-bord article-oper-bord"">";
            var contentCrawl = CrawlHelper.ContentFetch(html, exp);

            #endregion

            #region 获取标题

            exp = @"<div class=""text-title"">([\s\S]*?)<span class=""article-tag"">";
            var title = CrawlHelper.ContentFetch(contentCrawl, exp);
            if (title.Length > 0) title = CrawlHelper.StripHtml(title);

            if (title.Length == 0) return;

            article.Abstract = url;
            article.Title = title.Replace("'", "''");

            #endregion

            #region 获取来源

            exp = @"<span data-role=""original-link"">来源:([\s\S]*?)<span class=""tag"">";
            var source = CrawlHelper.ContentFetch(contentCrawl, exp);
            if (source.Length > 0) source = CrawlHelper.StripHtml(source);

            article.Source = source;

            #endregion

            #region 获取内容

            exp = @"<article class=""article"">([\s\S]*?)</article>";
            var content = CrawlHelper.ContentFetch(contentCrawl, exp);

            if (content.Length > 0)
            {
                #region 原标题内容清除

                exp = @"<p data-role=""original-title""(.)*</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region 视频内容清除

                exp = @"<div id=""sohuplayer"">([\s\S]*?)</div>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region 视频标题清除

                exp = @"<p class=""video-title"">>([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region 视频标题清除

                exp = @"<p class=""video-title"">([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region A标签内容清除

                exp = @"<a href=""//www.sohu.com([\s\S]*?)</a>";
                content = CrawlHelper.ContentCleanup(content, exp);

                #endregion

                #region 责任编辑内容清除

                exp = @"<p data-role=""editor-name"">([\s\S]*?)</p>";
                content = CrawlHelper.ContentCleanup(content, exp);

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
                        var s = item.Groups[0].ToString().Trim();
                        if (!string.IsNullOrEmpty(s))
                        {
                            result += "<p>" + s + "</p>" + Environment.NewLine;
                        }
                    }
                }

                #endregion

                article.Content = result.Replace("'", "''");

                ArticleService.ArticleAdd(article);

            }

            #endregion

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
            //if (res != null)
            //{
            //    res.Close();
            //}
            //if (st != null)
            //{
            //    st.Close();
            //}
            //if (sr != null)
            //{
            //    sr.Close();
            //}
        }
    }

    #endregion

    #region 获取驱动之家内容

    /// <summary>
    /// 驱动之家内容抓取
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMydrivers_Click(object sender, EventArgs e)
    {
        string html = "";

        try
        {
            string url = "http://www.mydrivers.com/";
            html = CrawlHelper.GetHtml(url);

            var exp = @"<div class=""main_left"">([\s\S]*?)<div class=""module_page"" id=""news_content_page"">";
            //var contentCrawl = CrawlHelper.ContentFetch(html, exp);
            var contentCrawl = html;

            #region 匹配a标签内容

            var result = "";
            var i = 0;
            //exp = @"<a(.)*>";
            //exp = @"<a(.)*</a>"; // <a href="http://news.mydrivers.com/1/556/556706.htm"><font color="#ff0000">干掉安卓/Win10！谷歌全新自主OS：苹果惊</font></a>

            exp = @"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>";
            var regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(contentCrawl);
            if (matches.Count > 0)
            {
                foreach (Match item in matches)
                {
                    var s = item.Groups[0].ToString();
                    exp = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>";
                    var match = Regex.Match(s, exp, RegexOptions.IgnoreCase);
                    var href = match.Groups["href"].Value;
                    if (href == "#" || href == "javascript:;") continue;

                    if (href.IndexOf("mydrivers", StringComparison.Ordinal) == -1) continue;

                    var title = CrawlHelper.StripHtml(s); ;

                    i++;

                    result += title + " * " + href + Environment.NewLine;

                    Article article = ArticleService.GetArticleByUrl(href);
                    if (article != null && article.Id != 0) return;

                    GetMydriversContent(href);
                }
            }

            #endregion

            lblMessage.Text = i.ToString();
            txtContent.Text = result;

        }
        catch (IOException ex)
        {
            html = ex.Message;
            txtContent.Text = html;
        }
        catch (Exception ex)
        {
            html = ex.Message;
            txtContent.Text = html;
        }
        finally
        {
        }
    }

    /// <summary>
    /// 根据地址获取驱动之家内容
    /// </summary>
    /// <param name="url"></param>
    internal void GetMydriversContent(string url)
    {
        //string url = "http://www.sohu.com/a/205380121_428290?_f=index_news_1";
        //string url = "http://www.jueceji.com/login/AjaxRegValid?type=email&validValue=ansenwork@163.com";
        //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //req.Method = "GET";

        //HttpWebResponse res = null;
        //Stream st = null;
        //StreamReader sr = null;
        string html = "";

        try
        {
            //res = (HttpWebResponse)req.GetResponse();
            //st = res.GetResponseStream();
            //sr = new StreamReader(st, System.Text.Encoding.UTF8);
            ////Console.WriteLine(sr.CurrentEncoding);
            //html = sr.ReadToEnd();

            var exp = "";
            html = CrawlHelper.GetHtml(url);

            #region 获取文章内容主体

            //var exp = @"<div class=""text"">([\s\S]*?)<div class=""article-oper-bord article-oper-bord"">";
            //var contentCrawl = CrawlHelper.ContentFetch(html, exp);

            #endregion

            #region 获取标题

            exp = @"<div class=""news_bt"" id=""thread_subject"">([\s\S]*?)</div>";
            var title = CrawlHelper.ContentFetch(html, exp);
            if (title.Length > 0) title = CrawlHelper.StripHtml(title);
            if (title.Length == 0) return;
            Article article = new Article();

            article.Abstract = url;
            article.Title = title.Replace("'", "''");

            #endregion

            #region 获取来源

            exp = @"<div class=""news_bt1_left"" style=""width:570px;overflow:hidden;"">([\s\S]*?)<span id=""Hits"">";
            var source = CrawlHelper.ContentFetch(html, exp);
            if (source.Length > 0) source = CrawlHelper.StripHtml(source);
            source = source.Replace(" ", "");
            var author = source;

            // 2017-11-22 12:10:18  出处：爱活网  作者：Picca  编辑：万南
            if (source.IndexOf("出处：", StringComparison.Ordinal) > -1)
            {
                source = source.Replace("出处：", "*").Replace("作者：", "*").Replace("编辑：", "*");
                var arr = source.Split('*');
                source = arr[1];
            }
            else
            {
                source = "";
            }

            article.Source = source;

            #endregion

            #region 获取作者

            if (author.IndexOf("作者：", StringComparison.Ordinal) > -1)
            {
                author = author.Replace("作者：", "*").Replace("编辑：", "*");
                var arr = author.Split('*');
                author = arr[1];
            }
            else
            {
                author = "";
            }

            article.Author = author;

            #endregion

            #region 获取内容

            exp = @"<div class=""news_info"" style=""padding-top:0px; padding-bottom:0px;"">([\s\S]*?)<p class=""jcuo1"">";
            var content = CrawlHelper.ContentFetch(html, exp);
            if (content.Length <= 0) return;

            #region 匹配P标签内容

            var result = "";
            exp = @"<p[^>]*>(?:(?!<\/p>)[\s\S])*<\/p>";
            var regex = new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(content);
            if (matches.Count > 0)
            {
                foreach (Match item in matches)
                {
                    var s = item.Groups[0].ToString().Trim();
                    s = s.Trim('　');
                    //s = s.Replace("　　", "");
                    if (!string.IsNullOrEmpty(s))
                    {
                        result += "<p>" + s + "</p>" + Environment.NewLine;
                    }
                }
            }

            #endregion

            article.Content = result.Replace("'", "''");

            ArticleService.ArticleAdd(article);

            #endregion

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
            //if (res != null)
            //{
            //    res.Close();
            //}
            //if (st != null)
            //{
            //    st.Close();
            //}
            //if (sr != null)
            //{
            //    sr.Close();
            //}
        }
    }

    #endregion


}