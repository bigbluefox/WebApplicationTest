using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model
{
    /// <summary>
    /// 附件文件实体
    /// </summary>
    [Serializable]
    //[ActiveRecord("Attachments")]
    public class FileModel
    {
        /// <summary>
        /// RowNumber
        /// </summary>
        [DataMember]
        public int RowNumber { get; set; }

        /// <summary>
        /// 记录数
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        /// 附件ID
        /// </summary>
        [DataMember]
        public string FileId { get; set; }

        /// <summary>
        /// 附件分类ID
        /// </summary>
        [DataMember]
        public string TypeId { get; set; }

        /// <summary>
        /// 附件分类名称
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// 附件分组ID
        /// </summary>
        [DataMember]
        public string GroupId { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 附件描述
        /// </summary>
        [DataMember]
        public string FileDesc { get; set; }

        /// <summary>
        /// 附件扩展名
        /// </summary>
        [DataMember]
        public string FileExt { get; set; }

        /// <summary>
        /// 附件大小
        /// </summary>
        [DataMember]
        public int FileSize { get; set; }

        /// <summary>
        /// 互联网媒体类型，Internet Media Type，MIME类型，内容类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        [DataMember]
        public string FileUrl { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// 附件参数
        /// </summary>
        [DataMember]
        public string Params { get; set; }

        /// <summary>
        /// 上传人ID
        /// </summary>
        [DataMember]
        public string Creator { get; set; }    
    
        /// <summary>
        /// 上传人姓名
        /// </summary>
        [DataMember]
        public string CreatorName { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [DataMember]
        public string CreateTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DataMember]
        public string Modifier { get; set; }    

        /// <summary>
        /// 修改人姓名
        /// </summary>
        [DataMember]
        public string ModifierName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public string ModifyTime { get; set; }

    }
}
