using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model.Media
{
    /// <summary>
    /// 标签表
    /// </summary>
   public class Tags
    {
        /// <summary>
        /// 标签编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 类型：0-其他；1-图片；2-音频；3-视频；4-图书
        /// </summary>
        [DataMember]
        public int Type { get; set; }

    }

    /// <summary>
   /// 媒体标签关联关系表
    /// </summary>
   public class MediaTag
    {
        /// <summary>
        /// 关系编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 媒体编号
        /// </summary>
        [DataMember]
        public int MediaId { get; set; }

        /// <summary>
        /// 标签编号
        /// </summary>
        [DataMember]
        public int TagId { get; set; }

        /// <summary>
        /// 类型：0-其他；1-图片；2-音频；3-视频；4-图书
        /// </summary>
        [DataMember]
        public int Type { get; set; }

    }
}
