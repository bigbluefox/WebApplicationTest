using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebApplicationTest.AppCodes
{
    public class FileAttribute
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        ///     文件类型(0:目录，1:文件)
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 文件目录名
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文件创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// zTree 实体
    /// </summary>
    [DataContract]
    public class zTreeModel
    {
        /// <summary>
        ///    编号
        /// </summary>
        [DataMember(Name = "id")]
        public string id { get; set; }

        /// <summary>
        ///     父节点编号
        /// </summary>
        [DataMember(Name = "pId")]
        public string pId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [DataMember(Name = "name")]
        public string name { get; set; }

        /// <summary>
        ///     是否是父级节点
        /// </summary>
        [DataMember(Name = "isParent")]
        public bool isParent { get; set; }

        /// <summary>
        ///     是否展开（序列化后要修改为open）
        /// </summary>
        [DataMember(Name = "open")]
        public bool IsOpen { get; set; }

        /// <summary>
        ///     是否选中（序列化后要修改为checked）
        /// </summary>
        [DataMember(Name = "checked")]
        public bool IsChecked { get; set; }

        public string icon { get; set; }

    }
}