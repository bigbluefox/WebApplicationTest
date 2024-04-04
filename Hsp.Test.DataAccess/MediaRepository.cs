using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;
using Hsp.Test.Model.Media;

namespace Hsp.Test.DataAccess
{
    /// <summary>
    /// 媒体数据服务
    /// </summary>
    public class MediaRepository : IMediaRepository
    {
        #region 媒体文件批量添加

        /// <summary>
        ///     媒体文件批量添加
        /// </summary>
        /// <param name="list">媒体文件列表</param>
        /// <returns></returns>
        public int AddMedias(List<Medias> list)
        {
            var strSql = string.Empty;

            foreach (var model in list)
            {
                strSql += string.Format(@"INSERT INTO Medias
                    (Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension, ContentType, FullName, DirectoryName, CreationTime, MD5, SHA1) 
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}');"
                    , model.Type, model.Name, model.Title, model.Album, model.Artist, model.Duration, model.Width, model.Height
                    , model.Size, model.Extension, model.ContentType, model.FullName, model.DirectoryName
                    , model.CreationTime, model.MD5, model.SHA1);
                strSql += Environment.NewLine;
            }

            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        //SELECT Id, Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension, ContentType
        //    , FullName, DirectoryName, CreationTime, MD5, SHA1
        //FROM Medias

        #region 获取媒体数据

        /// <summary>
        /// 获取媒体数据
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public DataSet GetMediaData(Dictionary<string, string> paramList)
        {
            var strTitle = paramList.ContainsKey("Title") ? (paramList["Title"] ?? "") : "";
            var strType = paramList.ContainsKey("Type") ? (paramList["Type"] ?? "") : "";
            string strQry = "", strOrderBy = " ORDER BY DirectoryName";

            if (string.IsNullOrEmpty(strType))
            {
                strQry += string.Format(@" AND (Title LIKE '%{0}%')", strTitle);
            }
            else
            {
                if (strType.IndexOf("1", StringComparison.Ordinal) > -1) // 重复
                {
                    strQry += string.Format(@" AND (MD5 IN (SELECT MD5 FROM Medias GROUP BY MD5 HAVING COUNT(0) > 1))");
                    strOrderBy = " ORDER BY MD5";
                }

                if (strType.IndexOf("2", StringComparison.Ordinal) > -1) // 名称
                {
                    strQry += string.Format(@" AND (Title LIKE '%{0}%')", strTitle);
                }

                if (strType.IndexOf("3", StringComparison.Ordinal) > -1) // 目录
                {
                    strQry += string.Format(@" AND (DirectoryName LIKE '%{0}%')", strTitle);
                }
            }

            #region 页码处理

            var pageIndex = paramList.ContainsKey("PageIndex") ? int.Parse(paramList["PageIndex"] ?? "1"):1;
            var pageSize = paramList.ContainsKey("PageSize") ? int.Parse(paramList["PageSize"] ?? "10"):10;

            // offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果

            var iOffset = (pageIndex - 1) * pageSize;
            var iLimit = pageSize;

            #endregion

            var strSql = string.Format(@"SELECT *, c.Count FROM Medias m, (SELECT count(Id) as Count FROM Medias WHERE (1 = 1){0}) c WHERE (1 = 1){0}{1} limit {2} offset {3};", strQry, strOrderBy, iLimit, iOffset);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号获取媒体信息

        /// <summary>
        /// 根据编号获取媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        /// <returns></returns>
        public DataSet GetMediaById(int id)
        {
            var strSql = string.Format(@"SELECT * FROM Medias WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号集合获取媒体信息

        /// <summary>
        /// 根据编号集合获取媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        /// <returns></returns>
        public DataSet GetMediaByIds(string ids)
        {
            var strSql = string.Format(@"SELECT * FROM Medias WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号删除媒体信息

        /// <summary>
        /// 根据编号删除媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        public int DelMediaById(int id)
        {
            var strSql = string.Format(@"DELETE FROM Medias WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 根据编号集合删除媒体信息

        /// <summary>
        /// 根据编号集合删除媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        public int DelMediaByIds(string ids)
        {
            var strSql = string.Format(@"DELETE FROM Medias WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 获取媒体数量

        /// <summary>
        /// 获取媒体数量
        /// </summary>
        /// <returns></returns>
        public int MediaCount()
        {
            var strSql = string.Format(@"SELECT COUNT(*) FROM Medias;");
            DataSet ds = SQLiteHelper.ExecuteDataSet(strSql);
            var count = 0;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                count = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }

            return count;
        }

        #endregion

        #region 清空数据表

        /// <summary>
        /// 清空数据表
        /// </summary>
        /// <param name="name">表名</param>
        public int EmptyingTable(string name)
        {
            var strSql = string.Format(@"DELETE FROM {0}; --清空数据
                UPDATE sqlite_sequence SET seq = 0 WHERE name ='{0}'; --自增长ID为0", name);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion
    }
}
