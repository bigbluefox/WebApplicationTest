using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;

namespace Hsp.Test.DataAccess
{
    /// <summary>
    /// 音频数据服务
    /// </summary>
    public class AudioRepository : IAudioRepository
    {
        #region 根据编号获取音频信息

        /// <summary>
        /// 根据编号获取音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        /// <returns></returns>
        public DataSet GetAudioById(int id)
        {
            var strSql = string.Format(@"SELECT * FROM Audio WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号集合获取音频信息

        /// <summary>
        /// 根据编号集合获取音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        /// <returns></returns>
        public DataSet GetAudioByIds(string ids)
        {
            var strSql = string.Format(@"SELECT * FROM Audio WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion


        #region 根据编号删除音频信息

        /// <summary>
        /// 根据编号删除音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        public int DelAudioById(int id)
        {
            var strSql = string.Format(@"DELETE FROM Audio WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 根据编号集合删除音频信息

        /// <summary>
        /// 根据编号集合删除音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        public int DelAudioByIds(string ids)
        {
            var strSql = string.Format(@"DELETE FROM Audio WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 获取音频数据

        /// <summary>
        /// 获取音频数据
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public DataSet GetAudioData(Dictionary<string, string> paramList)
        {
            var strTitle = paramList["Title"] ?? "";
            var strType = paramList["Type"] ?? "";
            string strQry = "", strOrderBy = " ORDER BY DirectoryName";

            if (strType.IndexOf("1", StringComparison.Ordinal) > -1) // 重复
            {
                strQry += string.Format(@" AND (MD5 IN (SELECT MD5 FROM Audio GROUP BY MD5 HAVING COUNT(0) > 1))");
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

            #region 页码处理

            var pageIndex = paramList.ContainsKey("PageIndex") ? int.Parse(paramList["PageIndex"] ?? "1"):1;
            var pageSize = paramList.ContainsKey("PageSize") ? int.Parse(paramList["PageSize"] ?? "10"):10;

            // offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果

            var iOffset = (pageIndex - 1)*pageSize;
            var iLimit = pageSize;

            #endregion

            var strSql = string.Format(@"SELECT *, c.Count FROM Audio a, (SELECT count(Id) as Count FROM Audio WHERE (1 = 1){0}) c WHERE (1 = 1){0}{1} limit {2} offset {3};", strQry, strOrderBy, iLimit, iOffset);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 音频归档

        /// <summary>
        /// 音频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public DataSet AudioArchiving(Dictionary<string, string> paramList)
        {
            var strQry = string.Empty;
            var strType = paramList["Type"] ?? "0";
            var strDir = paramList["Dir"] ?? "";
            if (!string.IsNullOrEmpty(strType) && strType != "0")
            {
                strQry += string.Format(@" AND (Type = {0})", strType);
            }
            //if (!string.IsNullOrEmpty(strDir))
            //{
            //    if (!strDir.EndsWith("\\")) strDir += "\\";
            //    strQry += string.Format(@" AND (FullName LIKE '%{0}%')", strDir);
            //}

            //SELECT Id, Name, Title, Album, Artist, Duration, Size, Extension, ContentType
            //, FullName, DirectoryName, MD5, SHA1, CreationTime
            //  FROM Audio;

            var strSql = string.Format(@"
            INSERT INTO Audio 
            (Name, Title, Album, Artist, Duration, Size, Extension, ContentType
            , FullName, DirectoryName, MD5, SHA1, CreationTime)
            SELECT Name, Title, Album, Artist, Duration, Size, Extension, ContentType
            , FullName, DirectoryName, MD5, Id, CreationTime
            FROM Audio WHERE (MD5 IS NOT NULL) AND (LENGTH(MD5) > 0)
            AND (Id IN (SELECT MIN(Id) AS Id FROM Audio GROUP BY MD5))
            AND (MD5 NOT IN (SELECT MD5 FROM Audio)) {0};
            DELETE FROM Audio WHERE (ID IN (SELECT SHA1 FROM Audio));
            SELECT * FROM Audio WHERE (ID NOT IN (SELECT SHA1 FROM Audio));
            ", strQry);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 音频文件名称修改

        /// <summary>
        /// 音频文件名称修改
        /// </summary>
        /// <param name="id">文件编号</param>
        /// <param name="title">文件标题</param>
        /// <param name="name">文件全名</param>
        public int AudioRename(int id, string title, string name)
        {
            var strSql = string.Format(@"UPDATE Audio SET Title = '{1}' , FullName = '{2}' 
                WHERE (Id = {0});", id, title, name);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion


    }
}
