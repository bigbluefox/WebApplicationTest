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
    /// 音频逻辑服务
    /// </summary>
    public class AudioService : IAudioService
    {
        /// <summary>
        /// 音频服务
        /// </summary>
        internal readonly IAudioRepository AudioRepository = new AudioRepository();

        #region 根据编号获取音频信息

        /// <summary>
        /// 根据编号获取音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        /// <returns></returns>
        public Audio GetAudioById(int id)
        {
            var list = new List<Audio>();
            DataSet ds = AudioRepository.GetAudioById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Audio>(ds.Tables[0]).ToList();
            }

            return list.FirstOrDefault();
        }

        #endregion

        #region 根据编号集合获取音频信息

        /// <summary>
        /// 根据编号集合获取音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        /// <returns></returns>
        public List<Audio> GetAudioByIds(string ids)
        {
            var list = new List<Audio>();
            DataSet ds = AudioRepository.GetAudioByIds(ids);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<Audio>(ds.Tables[0]).ToList();
            }

            return list;
        }

        #endregion

        #region 根据编号删除音频信息

        /// <summary>
        /// 根据编号删除音频信息
        /// </summary>
        /// <param name="id">音频编号</param>
        public int DelAudioById(int id)
        {
            return AudioRepository.DelAudioById(id);
        }

        #endregion

        #region 根据编号集合删除音频信息

        /// <summary>
        /// 根据编号集合删除音频信息
        /// </summary>
        /// <param name="ids">音频编号集合</param>
        public int DelAudioByIds(string ids)
        {
            return AudioRepository.DelAudioByIds(ids);
        }

        #endregion


        #region 获取音频数据列表

        /// <summary>
        /// 获取音频数据列表
        /// </summary>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public List<Audio> GetAudioList(Dictionary<string, string> paramList)
        {
            var list = new List<Audio>();
            try
            {
                DataSet ds = AudioRepository.GetAudioData(paramList);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = new DataTableToList<Audio>(ds.Tables[0]).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }

        #endregion

        #region 音频归档

        /// <summary>
        /// 音频归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public void AudioArchiving(Dictionary<string, string> paramList)
        {
            DataSet ds = AudioRepository.AudioArchiving(paramList);
            if (ds == null || ds.Tables[0].Rows.Count <= 0) return;

            // 操作删除重复音频
            var defaultPath = ConfigurationManager.AppSettings["DefalutAudio"] ?? "";
            if (string.IsNullOrEmpty(defaultPath)) return;
            if (!defaultPath.EndsWith("\\")) defaultPath += "\\";
            var list = new DataTableToList<Audio>(ds.Tables[0]).ToList();

            foreach (var Audio in list)
            {
                //if (Audio.FullName.IndexOf(defaultPath, StringComparison.Ordinal) == -1){}

                if (File.Exists(Audio.FullName))
                {
                    File.Delete(Audio.FullName);
                }
            }
        }

        #endregion

        #region 音频文件名称修改

        /// <summary>
        /// 音频文件名称修改
        /// </summary>
        /// <param name="id">文件编号</param>
        /// <param name="title">文件标题</param>
        /// <param name="name">文件全名</param>
        public int AudioRename(int id, string title, string name)
        {
            return AudioRepository.AudioRename(id, title, name);
        }

        #endregion
    }
}
