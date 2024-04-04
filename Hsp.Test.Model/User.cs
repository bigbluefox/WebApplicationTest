using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{
    public class User
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// firstname
        /// </summary>
        [DataMember]
        public string firstname { get; set; }

        /// <summary>
        /// lastname
        /// </summary>
        [DataMember]
        public string lastname { get; set; }

        /// <summary>
        /// phone
        /// </summary>
        [DataMember]
        public string phone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [DataMember]
        public string email { get; set; }

        /// <summary>
        /// 数据数量
        /// </summary>
        [DataMember]
        public int count { get; set; }
    }
}