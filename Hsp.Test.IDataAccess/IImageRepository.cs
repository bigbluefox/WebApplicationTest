using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model.Media;

namespace Hsp.Test.IDataAccess
{
    /// <summary>
    /// 图片数据服务接口
    /// </summary>
    public interface IImageRepository
    {
        /// <summary>
        /// 图片属性添加
        /// </summary>
        /// <param name="model">图片属性实体</param>
        /// <returns></returns>
        int AddImage(ImageAttribute model);

        /// <summary>
        /// 图片属性批量添加
        /// </summary>
        /// <param name="list">图片属性列表</param>
        /// <returns></returns>
        int AddImages(List<ImageAttribute> list);

        /// <summary>
        /// 根据编号删除图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        int DelImageById(int id);

        /// <summary>
        /// 根据编号获取图片信息
        /// </summary>
        /// <param name="id">图片编号</param>
        /// <returns></returns>
        DataSet GetImageById(int id);

        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <returns></returns>
        DataSet GetImageData();

        /// <summary>
        /// 获取重复图片数据
        /// </summary>
        /// <returns></returns>
        DataSet GetDuplicateImageData();

        /// <summary>
        /// 添加 BootStrap 图标
        /// </summary>
        /// <param name="type">类型：1-基础图标，2-专业图标，3-文件类型图标</param>
        /// <param name="icons"></param>
        /// <returns></returns>
        int AddGlyphicons(int type, string icons);


    }
}
