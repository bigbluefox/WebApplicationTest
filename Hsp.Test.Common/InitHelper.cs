using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    /// Ini 配置文件操作类
    /// </summary>
    public class InitHelper
    {
        /// <summary>
        /// INI文件名
        /// </summary>
        internal readonly string Path;

        /// <summary>
        /// 声明INI文件的写操作函数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        /// <summary>
        /// 声明INI文件的读操作函数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
            StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 类的构造函数，传递INI文件名 
        /// </summary>
        /// <param name="iniPath"></param>
        public InitHelper(string iniPath)
        {
            this.Path = iniPath;
        }

        /// <summary>
        /// 写INI文件 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void IniWriteValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, this.Path);
        }

        /// <summary>
        /// 读取INI文件指定 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string IniReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            var sb = new StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径
            int i = GetPrivateProfileString(section, key, "", sb, 255, this.Path);
            return sb.ToString();
        }
    }
}