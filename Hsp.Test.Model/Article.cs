using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{
   /// <summary>
   /// 文章实体
   /// </summary>
    public class Article
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 文章作者
        /// </summary>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        /// 文章来源
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        [DataMember]
        public string Abstract { get; set; }

        /// <summary>
        /// 文章名称
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 数据数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
    }
}
