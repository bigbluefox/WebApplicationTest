using System;
using System.Runtime.Serialization;

namespace Hsp.Test.Model.Media
{
    /// <summary>
    ///     媒体实体
    /// </summary>
    [Serializable]
    public class Medias
    {
        /// <summary>
        ///     编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        ///     类型：0-其他；1-图片；2-音频；3-视频
        /// </summary>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///     唱片集
        /// </summary>
        [DataMember]
        public string Album { get; set; }

        /// <summary>
        ///     艺术家
        /// </summary>
        [DataMember]
        public string Artist { get; set; }

        /// <summary>
        ///     时长
        /// </summary>
        [DataMember]
        public int Duration { get; set; }

        /// <summary>
        ///     （帧）宽度
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        ///     （帧）高度
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        ///     大小
        /// </summary>
        [DataMember]
        public long Size { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        ///     内容类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        ///     目录
        /// </summary>
        [DataMember]
        public string DirectoryName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [DataMember]
        public string CreationTime { get; set; }

        /// <summary>
        ///     MD5哈希值
        /// </summary>
        [DataMember]
        public string MD5 { get; set; }

        /// <summary>
        ///     SHA1哈希值
        /// </summary>
        [DataMember]
        public string SHA1 { get; set; }

        /// <summary>
        /// 数据数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
    }
}