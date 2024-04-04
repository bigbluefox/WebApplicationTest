using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    /// Mp3信息结构 
    /// </summary>
    public struct Mp3Info
    {
        public string identify;     //TAG，三个字节 
        public string Title;        //歌曲名,30个字节 
        public string Artist;       //歌手名,30个字节 
        public string Album;        //所属唱片,30个字节 
        public string Year;         //年,4个字符 
        public string Comment;      //注释,28个字节 
        public char reserved1;      //保留位，一个字节 
        public char reserved2;      //保留位，一个字节 
        public char reserved3;      //保留位，一个字节 
    }

    /// <summary>
    /// Mp3文件信息类
    /// </summary>
    public class Mp3FileInfo
    {
        public Mp3Info Info;

        /// <summary>
        /// 构造函数,输入文件名即得到信息
        /// </summary>
        /// <param name="mp3FilePos"></param>
        public Mp3FileInfo(string mp3FilePos)
        {
            Info = GetMp3Info(GetLast128(mp3FilePos));
        }

        /// <summary>
        /// 获取整理后的Mp3文件名,这里以标题和艺术家名定文件名
        /// </summary>
        /// <returns></returns>
        public string GetOriginalName()
        {
            return FormatString(Info.Title.Trim()) + "-" + FormatString(Info.Artist.Trim());
        }

        /// <summary>
        /// 去除\0字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string FormatString(string str)
        {
            return str.Replace("\0", "");
        }

        /// <summary> 
        /// 获取MP3文件最后128个字节 
        /// </summary> 
        /// <param name="fileName">文件名</param> 
        /// <returns>返回字节数组</returns> 
        private static byte[] GetLast128(string fileName)
        {

            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Stream stream = fs;
            stream.Seek(-128, SeekOrigin.End);

            const int seekPos = 128;
            var info = new byte[seekPos];
            stream.Read(info, 0, seekPos);

            stream.Close();
            fs.Close();

            return info;
        }

        /// <summary> 
        /// 获取MP3歌曲的相关信息 
        /// </summary> 
        /// <param name = "info">从MP3文件中截取的二进制信息</param> 
        /// <returns>返回一个Mp3Info结构</returns> 
        private static Mp3Info GetMp3Info(byte[] info)
        {
            var mp3Info = new Mp3Info();

            string str = null;
            int i;
            var position = 0;//循环的起始值 
            var currentIndex = 0;//Info的当前索引值 

            //获取TAG标识 (数组前3个)

            for (i = currentIndex; i < currentIndex + 3; i++)
            {
                str = str + (char)info[i];
                position++;
            }

            currentIndex = position;
            mp3Info.identify = str;

            //获取歌名 （数组3-32）
            str = null;
            var bytTitle = new byte[30];//将歌名部分读到一个单独的数组中 
            var j = 0;
            for (i = currentIndex; i < currentIndex + 30; i++)
            {
                bytTitle[j] = info[i];
                position++;
                j++;
            }

            currentIndex = position;

            mp3Info.Title = ByteToString(bytTitle);

            //获取歌手名 （数组33-62） 

            str = null;
            j = 0;
            var bytArtist = new byte[30];//将歌手名部分读到一个单独的数组中 

            for (i = currentIndex; i < currentIndex + 30; i++)
            {

                bytArtist[j] = info[i];
                position++;
                j++;
            }

            currentIndex = position;
            mp3Info.Artist = ByteToString(bytArtist);

            //获取唱片名 （数组63-92）
            str = null;
            j = 0;
            var bytAlbum = new byte[30];//将唱片名部分读到一个单独的数组中 

            for (i = currentIndex; i < currentIndex + 30; i++)
            {

                bytAlbum[j] = info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Album = ByteToString(bytAlbum);

            //获取年 （数组93-96）
            str = null;
            j = 0;
            var bytYear = new byte[4];//将年部分读到一个单独的数组中 

            for (i = currentIndex; i < currentIndex + 4; i++)
            {
                bytYear[j] = info[i];
                position++;
                j++;
            }

            currentIndex = position;
            mp3Info.Year = ByteToString(bytYear);

            //获取注释 （数组97-124）  
            str = null;
            j = 0;
            var bytComment = new byte[28];//将注释部分读到一个单独的数组中 

            for (i = currentIndex; i < currentIndex + 25; i++)
            {
                bytComment[j] = info[i];
                position++;
                j++;
            }

            currentIndex = position;
            mp3Info.Comment = ByteToString(bytComment);

            //以下获取保留位 （数组125-127）   
            mp3Info.reserved1 = (char)info[++position];
            mp3Info.reserved2 = (char)info[++position];
            mp3Info.reserved3 = (char)info[++position];

            return mp3Info;
        }

        /// <summary>
        /// 将字节数组转换成字符串 
        /// </summary> 
        /// <param name = "b">字节数组</param> 
        /// <returns>返回转换后的字符串</returns>
        private static string ByteToString(byte[] b)
        {
            var enc = Encoding.GetEncoding("GB2312");
            var str = enc.GetString(b);
            str = str.Substring(0, str.IndexOf("#CONTENT#", StringComparison.Ordinal) >= 0 ? str.IndexOf("#CONTENT#", StringComparison.Ordinal) : str.Length);//去掉无用字符             
            return str;
        }
    }
}
