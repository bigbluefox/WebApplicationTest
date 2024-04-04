using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hsp.Test.Model
{
    [Serializable]
    public class CssModel
    {
    }

    /// <summary>
    ///     样式配置文件实体
    /// </summary>
    [Serializable]
    public class CssFile
    {
        /// <summary>
        ///     配置文件名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        ///     配置文件描述
        /// </summary>
        [DataMember]
        public string desc { get; set; }
    }

    /// <summary>
    ///     样式配置参数实体
    /// </summary>
    [Serializable]
    public class CssSettings
    {
        //<direction>1</direction>
        //<left>12</left>
        //<top>14</top>
        //<imgtype>png</imgtype>
        //<filename>filetype-icons</filename>
        //<width>16</width>
        //<height>16</height>

        /// <summary>
        ///     合成图片方向(0：横向，1：纵向)
        /// </summary>
        [DataMember]
        public int direction { get; set; }

        /// <summary>
        ///     合成图片左边位置(px)
        /// </summary>
        [DataMember]
        public int left { get; set; }

        /// <summary>
        ///     合成图片顶部位置(px)
        /// </summary>
        [DataMember]
        public int top { get; set; }

        /// <summary>
        ///     合成图片文件类型(jpg,png,gif)
        /// </summary>
        [DataMember]
        public string imgtype { get; set; }

        /// <summary>
        ///     合成图片文件名称
        /// </summary>
        [DataMember]
        public string filename { get; set; }

        /// <summary>
        ///     合成图片文件描述
        /// </summary>
        [DataMember]
        public string filedesc { get; set; }

        /// <summary>
        ///     合成图片文件宽度
        /// </summary>
        [DataMember]
        public int width { get; set; }

        /// <summary>
        ///     合成图片文件高度
        /// </summary>
        [DataMember]
        public int height { get; set; }

        /// <summary>
        ///     图标文件数据
        /// </summary>
        [DataMember(Name = "file")]
        public List<IconsFile> files { get; set; }
    }

    /// <summary>
    ///     图标文件实体
    /// </summary>
    [Serializable]
    public class IconsFile
    {
        /// <summary>
        ///     文件名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        ///     文件别名(生成样式名称，默认同name)
        /// </summary>
        [DataMember]
        public string alias { get; set; }

        /// <summary>
        ///     文件路径
        /// </summary>
        [DataMember]
        public string path { get; set; }

        /// <summary>
        ///     文件宽度
        /// </summary>
        [DataMember]
        public int width { get; set; }

        /// <summary>
        ///     文件高度
        /// </summary>
        [DataMember]
        public int height { get; set; }
    }
}