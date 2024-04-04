using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IDataAccess
{
    /// <summary>
    /// 视频数据服务接口
    /// </summary>
    public interface IVideoRepository
    {
        /// <summary>
        /// 根据编号获取视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        /// <returns></returns>
        DataSet GetVideoById(int id);

        /// <summary>
        /// 根据编号集合获取视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        /// <returns></returns>
        DataSet GetVideoByIds(string ids);

        /// <summary>
        /// 根据编号删除视频信息
        /// </summary>
        /// <param name="id">视频编号</param>
        int DelVideoById(int id);

        /// <summary>
        /// 根据编号集合删除视频信息
        /// </summary>
        /// <param name="ids">视频编号集合</param>
        int DelVideoByIds(string ids);

        /// <summary>
        /// 获取视频数据
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        DataSet GetVideoData(Dictionary<string, string> paramList);

        /// <summary>
        /// 视频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        DataSet VideoArchiving(Dictionary<string, string> paramList);

    }
}
