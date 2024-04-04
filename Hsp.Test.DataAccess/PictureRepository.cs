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
    /// 图片数据服务
    /// </summary>
    public class PictureRepository : IPictureRepository
    {
        #region 图片归档

        /// <summary>
        /// 图片归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public DataSet ImageArchiving(Dictionary<string, string> paramList)
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

            //SELECT Id, Name, Title, Width, Height, Size, Extension, ContentType
            //    , FullName, DirectoryName, MD5, SHA1, CreationTime FROM Image;

            var strSql = string.Format(@"
            INSERT INTO Image 
            (Name, Title, Width, Height, Size, Extension, ContentType
            , FullName, DirectoryName, MD5, SHA1, CreationTime)
            SELECT Name, Title, Width, Height, Size, Extension, ContentType
            , FullName, DirectoryName, MD5, Id, CreationTime
            FROM Medias WHERE (MD5 IS NOT NULL) AND (LENGTH(MD5) > 0)
            AND (Id IN (SELECT MIN(Id) AS Id FROM Medias GROUP BY MD5))
            AND (MD5 NOT IN (SELECT MD5 FROM Image)) {0};
            DELETE FROM Medias WHERE (ID IN (SELECT SHA1 FROM Image));
            SELECT * FROM Medias WHERE (ID NOT IN (SELECT SHA1 FROM Image));
            ", strQry);
            return SQLiteHelper.ExecuteDataSet(strSql);
        }

        #endregion
    }
}
