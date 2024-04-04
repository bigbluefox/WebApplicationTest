using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Common;
using Hsp.Test.DataAccess;
using Hsp.Test.IDataAccess;
using Hsp.Test.IService;
using Hsp.Test.Model.Media;

namespace Hsp.Test.Service
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class ImageService : IImageService
    {
        /// <summary>
        /// 图片服务
        /// </summary>
        internal readonly IImageRepository ImageRepository = new ImageRepository();

        #region 图片属性添加

        /// <summary>
        /// 图片属性添加
        /// </summary>
        /// <param name="model">图片属性实体</param>
        /// <returns></returns>
        public int AddImage(ImageAttribute model)
        {
            return ImageRepository.AddImage(model);
        }

        #endregion

        #region 图片属性批量添加

        /// <summary>
        /// 图片属性批量添加
        /// </summary>
        /// <param name="list">图片属性列表</param>
        /// <returns></returns>
        public int AddImages(List<ImageAttribute> list)
        {
            return ImageRepository.AddImages(list);
        }

        #endregion

        #region 根据编号删除图片信息

        /// <summary>
        /// 根据编号删除图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        public int DelImageById(int id)
        {
            return ImageRepository.DelImageById(id);
        }

        #endregion

        #region 根据编号获取图片信息

        /// <summary>
        /// 根据编号获取图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        /// <returns></returns>
        public ImageAttribute GetImageById(int id)
        {
            var list = new List<ImageAttribute>();
            DataSet ds = ImageRepository.GetImageById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<ImageAttribute>(ds.Tables[0]).ToList();
            }

            return list.FirstOrDefault();
        }

        #endregion

        #region 获取图片数据

        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <returns></returns>
        public List<ImageAttribute> GetImageList()
        {
            var list = new List<ImageAttribute>();
            DataSet ds = ImageRepository.GetImageData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<ImageAttribute>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion

        #region 获取重复图片数据

        /// <summary>
        /// 获取重复图片数据
        /// </summary>
        /// <returns></returns>
        public List<ImageAttribute> GetDuplicateImageList()
        {
            var list = new List<ImageAttribute>();
            DataSet ds = ImageRepository.GetDuplicateImageData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<ImageAttribute>(ds.Tables[0]).ToList();
            }
            return list;
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
            return ImageRepository.AddGlyphicons(type, icons);
        }

        #endregion
    }
}
