using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model.Media;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 音频逻辑服务接口
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// 根据编号获取音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        /// <returns></returns>
        Audio GetAudioById(int id);

        /// <summary>
        /// 根据编号集合获取音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        /// <returns></returns>
        List<Audio> GetAudioByIds(string ids);

        /// <summary>
        /// 根据编号删除音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        int DelAudioById(int id);

        /// <summary>
        /// 根据编号集合删除音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        int DelAudioByIds(string ids);

        /// <summary>
        /// 获取音频数据列表
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        List<Audio> GetAudioList(Dictionary<string, string> paramList);

        /// <summary>
        /// 音频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        void AudioArchiving(Dictionary<string, string> paramList);

        /// <summary>
        /// 音频文件名称修改
        /// </summary>
        /// <param name="id">文件编号</param>
        /// <param name="title">文件标题</param>
        /// <param name="name">文件全名</param>
        int AudioRename(int id, string title, string name);
    }
}
