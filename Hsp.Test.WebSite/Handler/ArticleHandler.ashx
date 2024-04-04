<%@ WebHandler Language="C#" Class="ArticleHandler" %>

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

/// <summary>
/// 文章处理程序
/// </summary>
public class ArticleHandler : IHttpHandler
{
    /// <summary>
    /// 文章服务
    /// </summary>
    internal readonly IArticleService ArticleService = new ArticleService();
    
    #region ProcessRequest

    /// <summary>
    /// ProcessRequest
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();
        string strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            // 获取文章列表信息
            case "ARTICLELIST":
                GetArticleList(context);
                break;

            // 根据编号获取文章信息
            case "GETARTICLEBYID":
                GetArticleById(context);
                break;

            // 保存文章信息
            case "ARTICLESAVE":
                ArticleSave(context);
                break;

            // 更新文章信息
            case "ARTICLEEDIT":
                ArticleEdit(context);
                break;

            // 删除文章信息
            case "ARTICLEDEL":
                ArticleDelete(context);
                break;

            default:
                break;
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion

    #region 获取文章信息

    /// <summary>
    /// 获取文章信息
    /// </summary>
    /// <param name="context"></param>
    private void GetArticleList(HttpContext context)
    {
        //var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s; // 1235

        //var strFileId = context.Request.Params["FID"];
        //var strGroupId = context.Request.Params["GID"];
        //var strTypeId = context.Request.Params["TID"];

        //if (string.IsNullOrEmpty(strGroupId)) strGroupId = "1235";
        //if (string.IsNullOrEmpty(strTypeId)) strTypeId = AttachmentType.Workflow;

        //if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) && string.IsNullOrWhiteSpace(strTypeId))
        //{
        //    return;
        //}

        //page = 1
        //rows = 10
        
        var rst = "";
        
        try
        {
            var pageIndex = context.Request.Form["page"] ?? "1";
            var pageSize = context.Request.Form["rows"] ?? "10";

            var paramList = new Dictionary<string, string> 
            {
                {"pageSize", pageSize},
                {"pageIndex", pageIndex}
            };

            List<Article> list = ArticleService.GetArticleList(paramList);
            var rows = new JavaScriptSerializer().Serialize(list);
            rst = "{\"success\":true,\"total\":" + list[0].Count + ",\"rows\":" + rows + "}";

        }
        catch (Exception ex)
        {
            rst = "{\"success\":true,\"Message\":" + ex.Message.Replace('"', '\"') + "}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 根据编号获取文章信息

    /// <summary>
    /// 根据编号获取文章信息
    /// </summary>
    /// <param name="context"></param>
    private void GetArticleById(HttpContext context)
    {
        var rst = "";
        
        try
        {
            string strId = context.Request.Params["id"].Trim() ?? "0";
            Article article = ArticleService.GetArticleById(Int32.Parse(strId));
            var row = new JavaScriptSerializer().Serialize(article);
            rst = "{\"success\":true,\"Data\":" + row + "}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true,\"Message\":" + ex.Message.Replace('"', '\"') + "}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 保存文章信息

    /// <summary>
    /// 保存文章信息
    /// </summary>
    /// <param name="context"></param>
    private void ArticleSave(HttpContext context)
    {
        var rst = "";

        try
        {
            var strTitle = context.Request.Params["title"] ?? "";
            var strAuthor = context.Request.Params["author"] ?? "";
            var strSource = context.Request.Params["source"] ?? "";
            var strAbstract = context.Request.Params["abstract"] ?? "";
            var strContent = context.Request.Params["content"] ?? "";

            strTitle = HttpUtility.UrlDecode(strTitle);
            strAuthor = HttpUtility.UrlDecode(strAuthor);
            strSource = HttpUtility.UrlDecode(strSource);
            strAbstract = HttpUtility.UrlDecode(strAbstract);
            strContent = HttpUtility.UrlDecode(strContent);

            strAbstract = Utility.ClearHTML(strContent);
            strAbstract = strAbstract.Length > 255 ? strAbstract.Substring(0, 255) : strAbstract;
        
            Article article = new Article();
            article.Title = strTitle;
            article.Author = strAuthor;
            article.Source = strSource;
            article.Abstract = strAbstract;
            article.Content = strContent;

            ArticleService.ArticleAdd(article);
            rst = "{\"success\":true,\"message\":\"文章添加成功\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true,\"Message\":" + ex.Message.Replace('"', '\"') + "}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 更新文章信息

    /// <summary>
    /// 更新文章信息
    /// </summary>
    /// <param name="context"></param>
    private void ArticleEdit(HttpContext context)
    {
        var rst = "";

        try
        {
            var strId = context.Request.Params["id"] ?? "0";
            var strTitle = context.Request.Params["title"] ?? "";
            var strAuthor = context.Request.Params["author"] ?? "";
            var strSource = context.Request.Params["source"] ?? "";
            var strAbstract = context.Request.Params["abstract"] ?? "";
            var strContent = context.Request.Params["content"] ?? "";

            strTitle = HttpUtility.UrlDecode(strTitle);
            strAuthor = HttpUtility.UrlDecode(strAuthor);
            strSource = HttpUtility.UrlDecode(strSource);
            strAbstract = HttpUtility.UrlDecode(strAbstract);
            strContent = HttpUtility.UrlDecode(strContent);

            strAbstract = Utility.ClearHTML(strContent);
            strAbstract = strAbstract.Length > 255 ? strAbstract.Substring(0, 255) : strAbstract;

            Article article = ArticleService.GetArticleById(Int32.Parse(strId));
            article.Title = strTitle;
            article.Author = strAuthor;
            article.Source = strSource;
            article.Abstract = strAbstract;
            article.Content = strContent;

            ArticleService.ArticleEdit(article);
            rst = "{\"success\":true,\"message\":\"文章修改成功\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true,\"Message\":" + ex.Message.Replace('"', '\"') + "}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 删除文章信息

    /// <summary>
    /// 删除文章信息
    /// </summary>
    /// <param name="context"></param>
    private void ArticleDelete(HttpContext context)
    {
        var rst = "";

        try
        {
            var strArticleId = context.Request.Params["id"] ?? "0";
            ArticleService.ArticleDel(int.Parse(strArticleId));
            rst = "{\"success\":true,\"message\":\"文章删除成功\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true,\"Message\":" + ex.Message.Replace('"', '\"') + "}";
        }

        context.Response.Write(rst);
    }

    #endregion
}