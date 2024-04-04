using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
    /// 视频逻辑服务
    /// </summary>
    public class VideoService : IVideoService
    {
        /// <summary>
        /// 视频服务
        /// </summary>
        internal readonly IVideoRepository VideoRepository = new VideoRepository();

        #region 根据编号获取视频信息

        /// <summary>
        /// 根据编号获取视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        /// <returns></returns>
        public Video GetVideoById(int id)
        {
            var list = new List<Video>();
            DataSet ds = VideoRepository.GetVideoById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Video>(ds.Tables[0]).ToList();
            }

            return list.FirstOrDefault();
        }

        #endregion

        #region 根据编号集合获取视频信息

        /// <summary>
        /// 根据编号集合获取视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        /// <returns></returns>
        public List<Video> GetVideoByIds(string ids)
        {
            var list = new List<Video>();
            DataSet ds = VideoRepository.GetVideoByIds(ids);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Video>(ds.Tables[0]).ToList();
            }

            return list;
        }

        #endregion

        #region 根据编号删除视频信息

        /// <summary>
        /// 根据编号删除视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        public int DelVideoById(int id)
        {
            return VideoRepository.DelVideoById(id);
        }

        #endregion

        #region 根据编号集合删除视频信息

        /// <summary>
        /// 根据编号集合删除视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        public int DelVideoByIds(string ids)
        {
            return VideoRepository.DelVideoByIds(ids);
        }

        #endregion

        #region 获取视频数据列表

        /// <summary>
        /// 获取视频数据列表
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public List<Video> GetVideoList(Dictionary<string, string> paramList)
        {
            var list = new List<Video>();
            DataSet ds = VideoRepository.GetVideoData(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Video>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion

        #region 视频归档

        /// <summary>
        /// 视频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public void VideoArchiving(Dictionary<string, string> paramList)
        {
            DataSet ds = VideoRepository.VideoArchiving(paramList);
            if (ds == null || ds.Tables[0].Rows.Count <= 0) return;
            
            // 操作删除重复视频
            var defaultPath = ConfigurationManager.AppSettings["DefalutVideo"] ?? "";
            if (string.IsNullOrEmpty(defaultPath)) return;
            if (!defaultPath.EndsWith("\\")) defaultPath += "\\";
            var list = new DataTableToList<Medias>(ds.Tables[0]).ToList();

            foreach (var media in list)
            {
                //if (media.FullName.IndexOf(defaultPath, StringComparison.Ordinal) == -1){}

                if (File.Exists(media.FullName))
                {
                    File.Delete(media.FullName);
                }
            }
        }

        #endregion
    }
}
