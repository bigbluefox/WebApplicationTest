﻿using System;
using System.Collections.Generic;
using System.Data;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;

namespace Hsp.Test.DataAccess
{
    /// <summary>
    ///     视频数据服务
    /// </summary>
    public class VideoRepository : IVideoRepository
    {
        #region 根据编号获取视频信息

        /// <summary>
        ///     根据编号获取视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        /// <returns></returns>
        public DataSet GetVideoById(int id)
        {
            var strSql = string.Format(@"SELECT * FROM Video WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号集合获取视频信息

        /// <summary>
        ///     根据编号集合获取视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        /// <returns></returns>
        public DataSet GetVideoByIds(string ids)
        {
            var strSql = string.Format(@"SELECT * FROM Video WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 根据编号删除视频信息

        /// <summary>
        ///     根据编号删除视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        public int DelVideoById(int id)
        {
            var strSql = string.Format(@"DELETE FROM Video WHERE (Id = {0});", id);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 根据编号集合删除视频信息

        /// <summary>
        ///     根据编号集合删除视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        public int DelVideoByIds(string ids)
        {
            var strSql = string.Format(@"DELETE FROM Video WHERE (Id IN ({0}));", ids);
            return SQLiteHelper.ExecuteNonQuery(strSql);
        }

        #endregion

        #region 获取视频数据

        /// <summary>
        ///     获取视频数据
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public DataSet GetVideoData(Dictionary<string, string> paramList)
        {
            var strTitle = paramList["Title"] ?? "";
            var strType = paramList["Type"] ?? "";
            string strQry = "", strOrderBy = " ORDER BY DirectoryName";

            if (strType.IndexOf("1", StringComparison.Ordinal) > -1) // 重复
            {
                strQry += @" AND (MD5 IN (SELECT MD5 FROM Video GROUP BY MD5 HAVING COUNT(0) > 1))";
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

            var pageIndex = paramList.ContainsKey("PageIndex") ? int.Parse(paramList["PageIndex"] ?? "1") : 1;
            var pageSize = paramList.ContainsKey("PageSize") ? int.Parse(paramList["PageSize"] ?? "10") : 10;

            // offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果

            var iOffset = (pageIndex - 1)*pageSize;
            var iLimit = pageSize;

            #endregion

            var strSql =
                string.Format(
                    @"SELECT *, c.Count FROM Video v, (SELECT count(Id) as Count FROM Video WHERE (1 = 1){0}) c WHERE (1 = 1){0}{1} limit {2} offset {3};",
                    strQry, strOrderBy, iLimit, iOffset);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion

        #region 视频归档

        /// <summary>
        ///     视频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public DataSet VideoArchiving(Dictionary<string, string> paramList)
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

            var strSql = string.Format(@"
            INSERT INTO Video 
            (Name, Title, Duration, Width, Height, Size, Extension
            , ContentType, FullName, DirectoryName, CreationTime, MD5, SHA1)
            SELECT Name, Title, Duration, Width, Height, Size, Extension
            , ContentType, FullName, DirectoryName, CreationTime, MD5, Id 
            FROM Video WHERE (MD5 IS NOT NULL) AND (LENGTH(MD5) > 0)
            AND (Id IN (SELECT MIN(Id) AS Id FROM Video GROUP BY MD5))
            AND (MD5 NOT IN (SELECT MD5 FROM Video)) {0};
            DELETE FROM Video WHERE (ID IN (SELECT SHA1 FROM Video));
            SELECT * FROM Video WHERE (ID NOT IN (SELECT SHA1 FROM Video));
            ", strQry);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion
    }
}