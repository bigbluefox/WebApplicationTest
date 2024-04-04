using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{
    /// <summary>
    /// 图片实体
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 图片标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// 图片大小
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
        /// 图片全称
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// 图片目录名
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
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 数据数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
    }
}
