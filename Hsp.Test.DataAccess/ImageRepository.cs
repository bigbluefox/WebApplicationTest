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
    /// 图片数据服务
    /// </summary>
    public class ImageRepository : IImageRepository
    {
        #region 图片属性添加

        /// <summary>
        /// 图片属性添加
        /// </summary>
        /// <param name="model">图片属性实体</param>
        /// <returns></returns>
        public int AddImage(ImageAttribute model)
        {
            string strSql = string.Format(@"INSERT INTO ImageAttribute
                (Name, Title, Width, Height, Size, Extension, ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime) 
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}');"
                , model.Name, model.Title, model.Width, model.Height, model.Size, model.Extension
                , model.ContentType, model.FullName, model.DirectoryName, model.MD5, model.SHA1, model.CreationTime);
            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        // SELECT Id, Name, Title, Width, Height, Size, Extension
        // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
        // FROM ImageAttribute

        #region 图片属性批量添加

        /// <summary>
        /// 图片属性批量添加
        /// </summary>
        /// <param name="list">图片属性列表</param>
        /// <returns></returns>
        public int AddImages(List<ImageAttribute> list)
        {
            string strSql = string.Empty;

            foreach (var model in list)
            {
                strSql += string.Format(@"INSERT INTO ImageAttribute
                    (Name, Title, Width, Height, Size, Extension, ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime) 
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}');"
                    , model.Name, model.Title, model.Width, model.Height, model.Size, model.Extension
                    , model.ContentType, model.FullName, model.DirectoryName, model.MD5, model.SHA1, model.CreationTime);
                strSql += Environment.NewLine;
            }

            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 根据编号删除图片信息

        /// <summary>
        /// 根据编号删除图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        public int DelImageById(int id)
        {
            var strSql = string.Format(@"DELETE FROM dbo.ImageAttribute WHERE (Id = {0});", id);
            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

        #region 根据编号获取图片信息

        /// <summary>
        /// 根据编号获取图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        /// <returns></returns>
        public DataSet GetImageById(int id)
        {
            var strSql = string.Format(@"SELECT * FROM dbo.ImageAttribute WHERE (Id = {0});", id);
            return DbHelperSql.Query(strSql);
        }

        #endregion

        #region 获取图片数据

        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetImageData()
        {
            var strSql = string.Format(@";WITH Tb AS (
	                SELECT MIN(Id) AS Id FROM dbo.ImageAttribute GROUP BY MD5
                )
                SELECT * FROM dbo.ImageAttribute 
                WHERE Id IN (SELECT Id FROM Tb);");
            return DbHelperSql.Query(strSql);
        }

        #endregion

        #region 获取重复图片数据

        /// <summary>
        /// 获取重复图片数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDuplicateImageData()
        {
            var strSql = string.Format(@"SELECT * FROM dbo.ImageAttribute WHERE MD5 IN (
	            SELECT MD5 FROM dbo.ImageAttribute GROUP BY MD5 HAVING COUNT(0) > 1);");
            return DbHelperSql.Query(strSql);
        }

        #endregion

        #region 添加 BootStrap 图标

        /// <summary>
        /// 添加 BootStrap 图标
        /// </summary>
        /// <param name="type">类型：1-基础图标，2-专业图标，3-文件类型图标</param>
        /// <param name="icons"></param>
        /// <returns></returns>
        public int AddGlyphicons(int type, string icons)
        {
            string strSql = "";
            var iconArr = icons.Split(',');
            foreach (var s in iconArr)
            {
                strSql += string.Format(@"INSERT INTO dbo.Icons (Type, Icons) VALUES ({0}, '{1}');", type, s);
            }

            return DbHelperSql.ExecuteSql(strSql);
        }

        #endregion

    }
}
