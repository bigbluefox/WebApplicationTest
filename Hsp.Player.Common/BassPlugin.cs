using Un4seen.Bass;

namespace Hsp.Player.Common
{
    /// <summary>
    ///     Bass插件
    /// </summary>
    public class BassPlugin
    {
        private string fileName = string.Empty;

        /// <summary>
        ///     Bass插件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public BassPlugin(string fileName)
        {
            IsLoaded = false;
            Handle = 0;
            this.fileName = fileName;
        }

        /// <summary>
        ///     插件句柄
        /// </summary>
        public int Handle { get; set; }

        /// <summary>
        ///     文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        ///     插件信息
        /// </summary>
        public BASS_PLUGININFO PluginInfo
        {
            get { return Bass.BASS_PluginGetInfo(Handle); }
        }

        /// <summary>
        ///     是否已经成功加载
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        ///     加载插件
        /// </summary>
        public void Load()
        {
            Handle = Bass.BASS_PluginLoad(fileName);
            if (Handle != 0)
            {
                IsLoaded = true;
            }
        }

        /// <summary>
        ///     释放插件
        /// </summary>
        public void Free()
        {
            if (Bass.BASS_PluginFree(Handle))
            {
                IsLoaded = false;
            }
        }
    }
}