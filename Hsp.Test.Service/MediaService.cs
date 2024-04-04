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
    /// 媒体服务
    /// </summary>
    public class MediaService : IMediaService
    {
        /// <summary>
        /// 媒体服务
        /// </summary>
        internal readonly IMediaRepository MediaRepository = new MediaRepository();


        #region 媒体文件批量添加

        /// <summary>
        /// 媒体文件批量添加
        /// </summary>
        /// <param name="list">媒体文件列表</param>
        /// <returns></returns>
        public int AddMedias(List<Medias> list)
        {
            return MediaRepository.AddMedias(list);
        }

        #endregion

        #region 获取媒体数据列表

        /// <summary>
        /// 获取媒体数据列表
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public List<Medias> GetMediaList(Dictionary<string, string> paramList)
        {
            var list = new List<Medias>();
            DataSet ds = MediaRepository.GetMediaData(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Medias>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion

        #region 根据编号获取媒体信息

        /// <summary>
        /// 根据编号获取媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        /// <returns></returns>
        public Medias GetMediaById(int id)
        {
            var list = new List<Medias>();
            DataSet ds = MediaRepository.GetMediaById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Medias>(ds.Tables[0]).ToList();
            }

            return list.FirstOrDefault();
        }

        #endregion

        #region 根据编号集合获取媒体信息

        /// <summary>
        /// 根据编号集合获取媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        /// <returns></returns>
        public List<Medias> GetMediaByIds(string ids)
        {
            var list = new List<Medias>();
            DataSet ds = MediaRepository.GetMediaByIds(ids);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Medias>(ds.Tables[0]).ToList();
            }

            return list;
        }

        #endregion

        #region 根据编号删除媒体信息

        /// <summary>
        /// 根据编号删除媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        public int DelMediaById(int id)
        {
            return MediaRepository.DelMediaById(id);
        }

        #endregion

        #region 根据编号集合删除媒体信息

        /// <summary>
        /// 根据编号集合删除媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        public int DelMediaByIds(string ids)
        {
            return MediaRepository.DelMediaByIds(ids);
        }

        #endregion

        #region 获取媒体数量

        /// <summary>
        /// 获取媒体数量
        /// </summary>
        /// <returns></returns>
        public int MediaCount()
        {
            return MediaRepository.MediaCount();
        }

        #endregion


        #region 清空数据表

        /// <summary>
        /// 清空数据表
        /// </summary>
        /// <param name="name">表名</param>
        public int EmptyingTable(string name)
        {
            return MediaRepository.EmptyingTable(name);
        }

        #endregion
    }
}
