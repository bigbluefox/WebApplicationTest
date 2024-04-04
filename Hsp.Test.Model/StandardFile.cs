using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{   
    /// <summary>
    /// 标准文件
    /// </summary>
    public class StandardFile
    {

        /// <summary>
        /// 文件编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 标准代号
        /// </summary>
        [DataMember]
        public string PreCode { get; set; }

        /// <summary>
        /// 标准颁布年代
        /// </summary>
        [DataMember]
        public string Promulgation { get; set; }

        /// <summary>
        /// 标准基础编号
        /// </summary>
        [DataMember]
        public string BaseCode { get; set; }

        /// <summary>
        /// 标准号
        /// </summary>
        [DataMember]
        public string StandCode { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [DataMember]
        public string FileExt { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public int FileSize { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// 文件目录
        /// </summary>
        [DataMember]
        public string Directory { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

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
 



    }

    /// <summary>
    /// 标准代号对应表
    /// </summary>
    public class PreCodeCorresponding
    {
        /// <summary>
        /// 标准代号
        /// </summary>
        [DataMember]
        public string PreCode { get; set; }

        /// <summary>
        /// 对应字符
        /// </summary>
        [DataMember]
        public string Corresponding { get; set; }
 

    }
}
