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
    /// 附件数据服务
    /// </summary>
    public class AttachmentRepository : IAttachmentRepository
    {

        #region 添加附件信息

        /// <summary>
        /// 添加附件信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        public int AddFile(FileModel model)
        {
            string strSql = string.Format
                (@"INSERT INTO Attachments
                    (FileId, TypeId, GroupId, FileName, FileDesc, FileExt, FileSize, ContentType, FileUrl, FilePath, Creator, CreatorName, Params) 
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');"
                 , model.FileId, model.TypeId, model.GroupId, model.FileName, model.FileDesc, model.FileExt
                 , model.FileSize, model.ContentType, model.FileUrl, model.FilePath, model.Creator, model.CreatorName, model.Params);
            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 编辑附件信息

        /// <summary>
        /// 编辑附件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditFile(FileModel model)
        {
            string strSql = string.Format
                (@"UPDATE dbo.Attachments SET FileSize = {1}, Modifier = '{2}', ModifierName = '{3}', ModifyTime = '{4}' WHERE (FileId = '{0}');"
                    , model.FileId, model.FileSize, model.Modifier, model.ModifierName, model.ModifyTime);
            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 删除附件

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        public int DeleteFile(Dictionary<string, string> paramList)
        {
            string strQry = "";
            var strFileId = paramList.ContainsKey("FID") ? (paramList["FID"] ?? "") : "";
            var strGroupId = paramList.ContainsKey("GID") ? (paramList["GID"] ?? "") : "";
            var strTypeId = paramList.ContainsKey("TID") ? (paramList["TID"] ?? "") : "";
            if (!string.IsNullOrEmpty(strFileId)) strFileId = Hsp.Test.Common.Utility.MASK(strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strGroupId = Hsp.Test.Common.Utility.MASK(strGroupId);
            if (!string.IsNullOrEmpty(strFileId)) strQry += string.Format(@" AND (FileId = '{0}') ", strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strQry += string.Format(@" AND (GroupId = '{0}') ", strGroupId);
            if (!string.IsNullOrEmpty(strGroupId)) strQry += string.Format(@" AND (TypeId = '{0}') ", strTypeId);

            string strSql = string.Format(@"DELETE FROM Attachments WHERE (1 = 1) {0};", strQry);
            return string.IsNullOrEmpty(strQry) ? 0 : Hsp.Test.DBUtility.DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 根据附件组ID获取附件列表

        /// <summary>
        /// 根据附件组ID获取附件列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetFileList(Dictionary<string, string> paramList)
        {
            string strQry = "";
            var strFileId = paramList.ContainsKey("FID") ? (paramList["FID"] ?? "") : "";
            var strGroupId = paramList.ContainsKey("GID") ? (paramList["GID"] ?? "") : "";
            var strTypeId = paramList.ContainsKey("TID") ? (paramList["TID"] ?? "") : "";
            if (!string.IsNullOrEmpty(strFileId)) strFileId = Hsp.Test.Common.Utility.MASK(strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strGroupId = Hsp.Test.Common.Utility.MASK(strGroupId);
            if (!string.IsNullOrEmpty(strTypeId)) strTypeId = Hsp.Test.Common.Utility.MASK(strTypeId);

            if (!string.IsNullOrEmpty(strFileId)) strQry += string.Format(@" AND (FileId = '{0}') ", strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strQry += string.Format(@" AND (GroupId = '{0}') ", strGroupId);
            if (!string.IsNullOrEmpty(strTypeId)) strQry += string.Format(@" AND (TypeId = '{0}') ", strTypeId);

            string strSql = string.Format
                (@" SELECT ROW_NUMBER() OVER ( ORDER BY CreateTime ASC) AS RowNumber, FileId, TypeId, GroupId, [FileName], FileDesc, FileExt, Params, 
                    CASE WHEN FileSize < 1024 THEN 1 ELSE FileSize / 1024 END AS FileSize, ContentType, FileUrl, FilePath
                    , Creator, CreatorName, CONVERT(VARCHAR(16), CreateTime, 120) AS CreateTime
                    , Modifier, ModifierName, CONVERT(VARCHAR(16), ModifyTime, 120) AS ModifyTime
                    , (SELECT TypeName FROM dbo.AttachmentType WHERE (TypeId = a.TypeId)) AS TypeName
                                FROM Attachments AS a WHERE (1 = 1) {0};", strQry);

            return Hsp.Test.DBUtility.DbHelperSql.Query(strSql);
        }

        #endregion

        #region 根据附件参数获取附件分页列表

        /// <summary>
        /// 根据附件参数获取附件分页列表
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetAttachmentsByPage(Dictionary<string, string> paramList, int pageSize, int pageIndex)
        {
            var strQry = "";
            var strFileId = paramList.ContainsKey("FID") ? (paramList["FID"] ?? "") : "";
            var strGroupId = paramList.ContainsKey("GID") ? (paramList["GID"] ?? "") : "";
            var strTypeId = paramList.ContainsKey("TID") ? (paramList["TID"] ?? "") : "";
            if (!string.IsNullOrEmpty(strFileId)) strFileId = Common.Utility.MASK(strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strGroupId = Common.Utility.MASK(strGroupId);
            if (!string.IsNullOrEmpty(strTypeId)) strTypeId = Common.Utility.MASK(strTypeId);

            if (!string.IsNullOrEmpty(strFileId)) strQry += string.Format(@" and (FileId = '{0}') ", strFileId);
            if (!string.IsNullOrEmpty(strGroupId)) strQry += string.Format(@" and (GroupId = '{0}') ", strGroupId);
            if (!string.IsNullOrEmpty(strTypeId)) strQry += string.Format(@" and (TypeId = '{0}') ", strTypeId);

            //var sqlCount = string.Format(" select count(*) from FileModel where 1 = 1 {0}", strQry);
            //var countQuery = new ScalarQuery<long>(typeof(FileModel), sqlCount);
            //total = (int)countQuery.Execute();

            // 页码处理
            var iMinPage = (pageIndex - 1) * pageSize + 1;
            var iMaxPage = pageIndex * pageSize;

            string strSql = string.Format
                (@" ;WITH PageTb AS (
                        SELECT ROW_NUMBER() OVER ( ORDER BY TypeId ASC, CreateTime ASC) AS RowNumber, FileId, TypeId, GroupId, [FileName], FileDesc, FileExt, Params, 
                        CASE WHEN FileSize < 1024 THEN 1 ELSE FileSize / 1024 END AS FileSize, ContentType, FileUrl, FilePath
                        , Creator, CreatorName, CONVERT(VARCHAR(16), CreateTime, 120) AS CreateTime
                        , Modifier, ModifierName, CONVERT(VARCHAR(16), ModifyTime, 120) AS ModifyTime
                        , (SELECT TypeName FROM dbo.AttachmentType WHERE (TypeId = a.TypeId)) AS TypeName
                        FROM Attachments AS a WHERE (1 = 1) {0}
                    )
                    SELECT * 
                    FROM PageTb a
                    CROSS JOIN (SELECT MAX(RowNumber) AS RecordCount FROM PageTb) AS b 
                    WHERE (a.RowNumber BETWEEN {1} AND {2});
            ", strQry, iMinPage, iMaxPage);

            return DbHelperSql.Query(strSql);
        }

        #endregion
    }
}
