using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;
using Hsp.Test.Model;

namespace Hsp.Test.DataAccess
{
    /// <summary>
    /// 文章数据服务
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        public int ArticleAdd(Article model)
        {
            string strSql = string.Format
                (@"INSERT INTO Article (Title, Author, Source, Abstract, Content, CreateTime) 
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', (datetime('now')));"
                 , model.Title, model.Author, model.Source, model.Abstract, model.Content);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model">文章数据实体</param>
        /// <returns></returns>
        public int ArticleEdit(Article model)
        {
            string strSql = string.Format
                (@"UPDATE Article SET Title = '{0}', Author = '{1}', Source = '{2}', Abstract = '{3}'
                , Content = '{4}', CreateTime = (datetime('now')) WHERE (Id = {5});"
                 , model.Title, model.Author, model.Source, model.Abstract, model.Content, model.Id);

            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        //Id, Title, Author, Source, Abstract, Content, CreateTime

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public int ArticleDel(int id)
        {
            string strSql = string.Format(@"DELETE FROM Article WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }


        #region 获取文章列表

        /// <summary>
        /// 根据编号获取文章信息
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public DataSet GetArticleById(int id)
        {
            string strSql = string.Format
                (@" SELECT Id, Title, Author, Source, Abstract, Content, CreateTime FROM Article WHERE (1 = 1) AND (Id = {0});", id);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        /// <summary>
        /// 根据标题获取文章信息
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public DataSet GetArticleByTitle(string title)
        {
            string strSql = string.Format
                (@" SELECT Id, Title, Author, Source, Abstract, Content, CreateTime FROM Article WHERE (1 = 1) AND (Title = '{0}');", title);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        /// <summary>
        /// 根据地址获取文章信息
        /// </summary>
        /// <param name="url">文章地址</param>
        /// <returns></returns>
        public DataSet GetArticleByUrl(string url)
        {
            string strSql = string.Format
                (@" SELECT Id, Title, Author, Source, Abstract, Content, CreateTime FROM Article WHERE (1 = 1) AND (Abstract = '{0}');", url);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetArticleList(Dictionary<string, string> paramList)
        {
            #region 页码处理

            int pageSize = paramList.ContainsKey("pageSize") ? int.Parse(paramList["pageSize"] ?? "10") : 10;
            int pageIndex = paramList.ContainsKey("pageIndex") ? int.Parse(paramList["pageIndex"] ?? "1") : 1;

            // offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果

            var iOffset = (pageIndex - 1) * pageSize;
            var iLimit = pageSize;

            #endregion

            var strQry = "";
            var strTitle = paramList.ContainsKey("Title") ? (paramList["Title"] ?? "") : "";
            if (!string.IsNullOrEmpty(strTitle)) strQry = string.Format(@" AND (Title = '{0}')", strTitle);

            string strSql = string.Format(@" SELECT Id, Title, Author, Source, Abstract, Content, CreateTime, c.Count  
                FROM Article u, (SELECT count(Id) as Count FROM Article) c 
                WHERE (1 = 1) {0} order by Id limit {1} offset {2};"
                , strQry, iLimit, iOffset);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion
	
        //Id         INTEGER        PRIMARY KEY AUTOINCREMENT
        //                          UNIQUE
        //                          NOT NULL,
        //Title      NVARCHAR (64)  COLLATE Chinese_PRC_CI_AS,
        //Author     NVARCHAR (32)  COLLATE Chinese_PRC_CI_AS,
        //Source     NVARCHAR (32)  COLLATE Chinese_PRC_CI_AS,
        //Abstract   NVARCHAR (255) COLLATE Chinese_PRC_CI_AS,
        //Content    TEXT           COLLATE Chinese_PRC_CI_AS,
        //CreateTime DATETIME
	   
    }
}
