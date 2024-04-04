﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 文章逻辑服务接口
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        int ArticleAdd(Article model);

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        int ArticleEdit(Article model);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        int ArticleDel(int id);

        /// <summary>
        /// 根据编号获取文章信息
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        Article GetArticleById(int id);

        /// <summary>
        /// 根据标题获取文章信息
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        Article GetArticleByTitle(string title);

        /// <summary>
        /// 根据地址获取文章信息
        /// </summary>
        /// <param name="url">文章地址</param>
        /// <returns></returns>
        Article GetArticleByUrl(string url);

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        List<Article> GetArticleList(Dictionary<string, string> paramList);	   
	   
	   
    }
}
