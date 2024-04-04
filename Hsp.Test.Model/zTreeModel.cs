using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{
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

        ///// <summary>
        /////     是否展开（序列化后要修改为open）
        ///// </summary>
        // [DataMember(Name = "open")]
        // public bool open { get; set; }

        ///// <summary>
        /////     是否选中（序列化后要修改为checked）
        ///// </summary>
        // [DataMember(Name = "checked")]
        //public bool IsChecked { get; set; }
    }
}