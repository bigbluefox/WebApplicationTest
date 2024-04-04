using System.Runtime.Serialization;

namespace Hsp.Test.Model
{
    /// <summary>
    ///     本地标准属性
    /// </summary>
    public class StandardLocal
    {
        /// <summary>
        ///     文件编号
        /// </summary>
        [DataMember]
        public int FileId { get; set; }

        /// <summary>
        ///     文件名称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        ///     文件路径
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        ///     文件目录
        /// </summary>
        [DataMember]
        public string DirectoryName { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        [DataMember]
        public string FileExt { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        [DataMember]
        public int FileSize { get; set; }

        /// <summary>
        ///     内容类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        ///     标准类型：国家标准[CN]，国际标准[ISO]，国外标准[GW]，行业标准[QT]，计量规程规范[JJ]
        /// </summary>
        [DataMember]
        public string StandClass { get; set; }

        /// <summary>
        ///     标准属性：GB,GBE,ISO,IEC,CSA,AS,ANSI,BB,CB,FZ,CY,DA,GA...
        /// </summary>
        [DataMember]
        public string StandType { get; set; }

        /// <summary>
        ///     标准号
        /// </summary>
        [DataMember]
        public string A100 { get; set; }

        /// <summary>
        ///     标准代号
        /// </summary>
        [DataMember]
        public string StandPreNo { get; set; }

        /// <summary>
        ///     标准编号(基础)
        /// </summary>
        [DataMember]
        public string A107 { get; set; }

        /// <summary>
        ///     标准年代号
        /// </summary>
        [DataMember]
        public int A225 { get; set; }

        /// <summary>
        ///     中标分类号
        /// </summary>
        [DataMember]
        public string A825 { get; set; }

        /// <summary>
        ///     标准中文名称
        /// </summary>
        [DataMember]
        public string A301 { get; set; }

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
    }
}