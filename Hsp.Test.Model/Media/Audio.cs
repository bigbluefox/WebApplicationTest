using System.Runtime.Serialization;

namespace Hsp.Test.Model.Media
{
    /// <summary>
    /// 音频表
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// 音频编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 音频名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 音频标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 唱片集
        /// </summary>
        [DataMember]
        public string Album { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        [DataMember]
        public string Artist { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        [DataMember]
        public int Duration { get; set; }

        /// <summary>
        /// 音频大小
        /// </summary>
        [DataMember]
        public int Size { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// 音频全称
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// 音频目录名
        /// </summary>
        [DataMember]
        public string DirectoryName { get; set; }

        /// <summary>
        /// MD5哈希值
        /// </summary>
        [DataMember]
        public string MD5 { get; set; }

        /// <summary>
        /// SHA1哈希值
        /// </summary>
        [DataMember]
        public string SHA1 { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string CreationTime { get; set; }

        /// <summary>
        /// 数据数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
    }
}
