using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model.Media;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 媒体服务接口
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// 媒体文件批量添加
        /// </summary>
        /// <param name="list">媒体文件列表</param>
        /// <returns></returns>
        int AddMedias(List<Medias> list);

        /// <summary>
        /// 获取媒体数据列表
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        List<Medias> GetMediaList(Dictionary<string, string> paramList);

        /// <summary>
        /// 根据编号获取媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        /// <returns></returns>
        Medias GetMediaById(int id);

        /// <summary>
        /// 根据编号集合获取媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        /// <returns></returns>
        List<Medias> GetMediaByIds(string ids);

        /// <summary>
        /// 根据编号删除媒体信息
        /// </summary>
        /// <param name="id">媒体编号</param>
        int DelMediaById(int id);

        /// <summary>
        /// 根据编号集合删除媒体信息
        /// </summary>
        /// <param name="ids">媒体编号集合</param>
        int DelMediaByIds(string ids);

        /// <summary>
        /// 获取媒体数量
        /// </summary>
        /// <returns></returns>
        int MediaCount();

        /// <summary>
        /// 清空数据表
        /// </summary>
        /// <param name="name">表名</param>
        int EmptyingTable(string name);
    }
}
