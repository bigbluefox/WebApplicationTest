using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;
using Hsp.Test.Model;

namespace Hsp.Test.DataAccess
{
    /// <summary>
    ///     本地标准数据服务
    /// </summary>
    public class StandardRepository : IStandardRepository
    {
        #region 本地标准添加

        /// <summary>
        ///     本地标准添加
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        public int AddStandard(StandardLocal model)
        {
            var strSql = string.Format(@"INSERT INTO Standard_Local
                (FileName, FullName, DirectoryName, FileExt, FileSize, ContentType, StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1) 
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}');"
                , model.FileName, model.FullName, model.DirectoryName, model.FileExt, model.FileSize, model.ContentType
                , model.StandClass, model.StandType, model.A100, model.StandPreNo, model.A107, model.A225, model.A825
                , model.A301, model.MD5, model.SHA1);
            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        /// <summary>
        /// 本地标准更新
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        public int UpdateStandard(StandardLocal model)
        {
            var strSql = string.Format(@"UPDATE dbo.Standard_Local 
                SET [FileName] = '{1}', FullName = '{2}'
                WHERE (FileId = {0});"
                , model.FileId, model.FileName, model.FullName, model.DirectoryName, model.FileExt, model.FileSize, model.ContentType
                , model.StandClass, model.StandType, model.A100, model.StandPreNo, model.A107, model.A225, model.A825
                , model.A301, model.MD5, model.SHA1);
            return DbHelperSql.ExecuteSql(strSql);
        }

        // SELECT FileId, FileName, FullName, DirectoryName, FileExt, FileSize, ContentType
        // , StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1    
        // FROM Standard_Local

        #region 本地标准批量添加

        /// <summary>
        ///     本地标准批量添加
        /// </summary>
        /// <param name="list">本地标准列表</param>
        /// <returns></returns>
        public int AddStandards(List<StandardLocal> list)
        {
            var strSql = string.Empty;

            foreach (var model in list)
            {
                strSql += string.Format(@"INSERT INTO Standard_Local
                    (FileName, FullName, DirectoryName, FileExt, FileSize, ContentType, StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1) 
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}');"
                    , model.FileName, model.FullName, model.DirectoryName, model.FileExt, model.FileSize, model.ContentType
                    , model.StandClass, model.StandType, model.A100, model.StandPreNo, model.A107, model.A225, model.A825
                    , model.A301, model.MD5, model.SHA1);
                strSql += Environment.NewLine;
            }

            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 获取标准数据

        /// <summary>
        /// 获取标准数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetStandardData()
        {
            var strSql = string.Format(@"SELECT * FROM dbo.Standard_Local --WHERE (LEN(A100) > 0) AND (LEN(A825) = 1);");
            return DbHelperSql.Query(strSql);
        }

        #endregion

    }
}