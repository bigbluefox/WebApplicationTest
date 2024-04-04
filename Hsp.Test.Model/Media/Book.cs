using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model.Media
{
    /// <summary>
    /// 图书文件实体
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 图书编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 图书名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 图书标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

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
        /// 图书全称
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// 图书目录名
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
