using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Common;
using Hsp.Test.DataAccess;
using Hsp.Test.IDataAccess;
using Hsp.Test.IService;
using Hsp.Test.Model;

namespace Hsp.Test.Service
{
    /// <summary>
    /// 文章逻辑服务
    /// </summary>
    public class ArticleService : IArticleService
    {

        /// <summary>
        /// 文章服务
        /// </summary>
        internal readonly IArticleRepository ArticleRepository = new ArticleRepository();

        /// <summary>
        ///     添加文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        public int ArticleAdd(Article model)
        {
            return ArticleRepository.ArticleAdd(model);
        }

        /// <summary>
        ///     修改文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        public int ArticleEdit(Article model)
        {
            return ArticleRepository.ArticleEdit(model);
        }

        /// <summary>
        ///     删除文章
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public int ArticleDel(int id)
        {
            return ArticleRepository.ArticleDel(id);
        }

        #region 获取文章列表

        /// <summary>
        ///     根据编号获取文章信息
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public Article GetArticleById(int id)
        {
            var article = new Article();
            var ds = ArticleRepository.GetArticleById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                article = new DataTableToList<Article>(ds.Tables[0]).ToList().FirstOrDefault();
            }
            return article;
        }

        /// <summary>
        /// 根据标题获取文章信息
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public Article GetArticleByTitle(string title)
        {
            var article = new Article();
            var ds = ArticleRepository.GetArticleByTitle(title);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                article = new DataTableToList<Article>(ds.Tables[0]).ToList().FirstOrDefault();
            }
            return article;

            //Dictionary<string, string> paramList = new Dictionary<string, string> 
            //{
            //    {"pageSize", "999"},
            //    {"pageIndex", "1"},
            //    {"Title", title}
            //};
            //var article = new Article();
            //var ds = ArticleRepository.GetArticleList(paramList);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    article = new DataTableToList<Article>(ds.Tables[0]).ToList().FirstOrDefault();
            //}
            //return article;
        }

        /// <summary>
        /// 根据地址获取文章信息
        /// </summary>
        /// <param name="url">文章地址</param>
        /// <returns></returns>
        public Article GetArticleByUrl(string url)
        {
            var article = new Article();
            var ds = ArticleRepository.GetArticleByUrl(url);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                article = new DataTableToList<Article>(ds.Tables[0]).ToList().FirstOrDefault();
            }
            return article;
        }

        /// <summary>
        ///     获取文章列表
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticleList(Dictionary<string, string> paramList)
        {
            var list = new List<Article>();
            var ds = ArticleRepository.GetArticleList(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Article>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion	   
	   
	   
    }
}
