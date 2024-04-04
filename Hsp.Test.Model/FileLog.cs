using System;
using System.Runtime.Serialization;

namespace Hsp.Test.Model
{
    /// <summary>
    ///     附件操作日志实体
    /// </summary>
    [Serializable]
    public class FileLog
    {
        /// <summary>
        ///     记录数
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        ///     数据排序号
        /// </summary>
        [DataMember]
        public int RowNumber { get; set; }

        /// <summary>
        ///     附件日志表ID
        /// </summary>
        [DataMember]
        public string LogId { get; set; }

        /// <summary>
        ///     附件分组ID
        /// </summary>
        [DataMember]
        public string GroupId { get; set; }

        /// <summary>
        ///     附件分类ID
        /// </summary>
        [DataMember]
        public string TypeId { get; set; }

        /// <summary>
        ///     操作(添加，修改，删除)
        /// </summary>
        [DataMember]
        public string Operation { get; set; }

        /// <summary>
        ///     客户端信息(IP, 浏览器)
        /// </summary>
        [DataMember]
        public string ClientInfo { get; set; }

        /// <summary>
        ///     添加人ID
        /// </summary>
        [DataMember]
        public string Creator { get; set; }

        /// <summary>
        ///     添加人姓名
        /// </summary>
        [DataMember]
        public string CreatorName { get; set; }

        /// <summary>
        ///     添加日期
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}