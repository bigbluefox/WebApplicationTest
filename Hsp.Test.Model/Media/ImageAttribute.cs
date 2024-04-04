using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Model.Media
{
    /// <summary>
    /// 图片文件属性
    /// </summary>
    public class ImageAttribute
    {
        /// <summary>
        ///     图片编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     图片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图片标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     图片大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        ///     图片扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     图片全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     图片目录名
        /// </summary>
        public string DirectoryName { get; set; }

        // SELECT Id, Name, Title, Width, Height, Size, Extension
        // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
        // FROM ImageAttribute

        /// <summary>
        ///     MD5哈希值
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        ///     SHA1哈希值
        /// </summary>
        public string SHA1 { get; set; }

        /// <summary>
        ///     图片创建日期
        /// </summary>
        public string CreationTime { get; set; }
    }
}