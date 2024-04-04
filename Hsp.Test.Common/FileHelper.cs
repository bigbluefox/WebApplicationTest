using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace Hsp.Test.Common
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {

        #region Stream 和 byte[] 之间的转换


        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary> 

        /// 将 byte[] 转成 Stream 

        /// </summary> 

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        } 

        #endregion

        #region 如果目录不存在，建立

        /// <summary>
        /// 如果目录不存在，建立
        /// </summary>
        /// <param name="dirName">目录名称</param>
        public static void FilePathCheck(string dirName)
        {
            var directoryName = Path.GetDirectoryName(dirName);
            if (directoryName == null) return;
            String path = directoryName.TrimEnd('\\');
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

        #region 为文件名补充程序集所在路径

        /// <summary>
        ///     为文件名补充程序集所在路径。
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>带路径名的文件名</returns>
        public static string GetFilePath(string fileName)
        {
            if (!Path.IsPathRooted(fileName))
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }
            fileName = Path.GetFullPath(fileName);
            return fileName;
        }

        #endregion

        #region 用时间和随机数产生文件名

        /// <summary>
        ///     用时间和随机数产生文件名
        /// </summary>
        /// <returns></returns>
        public static string GenerateFileName()
        {
            DateTime dtTemp = DateTime.Now;
            string sTemp = FormatDateTimeToURLString(dtTemp);
            var rand = new Random(unchecked((int)DateTime.Now.Ticks));
            string sRand = rand.Next(0, 99999).ToString("0####");
            return sTemp + sRand;
        }

        public static string GenerateFileName(string userId)
        {
            DateTime dtTemp = DateTime.Now;
            string sTemp = FormatDateTimeToURLString(dtTemp);
            var rand = new Random(unchecked((int)DateTime.Now.Ticks));
            string sRand = rand.Next(0, 99999).ToString("0####");
            return sTemp + "-" + userId + "-" + sRand;
        }

        public static string GenerateFileName(Random pRand)
        {
            DateTime dtTemp = DateTime.Now;
            string sTemp = FormatDateTimeToURLString(dtTemp);
            string sRand = pRand.Next(0, 99999).ToString("0####");
            return sTemp + sRand;
        }

        public static string GenerateFileName(Guid id)
        {
            DateTime dtTemp = DateTime.Now;
            string sTemp = FormatDateToURLString(dtTemp);
            var rand = new Random(unchecked((int)DateTime.Now.Ticks));
            string sRand = rand.Next(0, 99999).ToString("0###");
            return String.Format("{0}{1}{2}", sTemp, id.ToString().Replace("-", "").ToUpper(), sRand);
        }

        /// <summary>
        ///     将日期类型转成yyyyMMdd
        /// </summary>
        /// <param name="argDt"></param>
        /// <returns></returns>
        public static string FormatDateToURLString(DateTime argDt)
        {
            string strDate = FormatDateToString(argDt).Replace("-", "");
            return strDate;
        }

        /// <summary>
        ///     将日期类型转成yyyy-MM-dd
        /// </summary>
        /// <param name="argDt"></param>
        /// <returns></returns>
        public static string FormatDateToString(DateTime argDt)
        {
            string strYear = argDt.Year.ToString();
            string strMonth = argDt.Month.ToString();
            if (strMonth.Length == 1)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = argDt.Day.ToString();
            if (strDay.Length == 1)
            {
                strDay = "0" + strDay;
            }
            return strYear + "-" + strMonth + "-" + strDay;
        }

        /// <summary>
        ///     将日期类型转成yyyyMMddhhmmss
        /// </summary>
        /// <param name="argDt"></param>
        /// <returns></returns>
        public static string FormatDateTimeToURLString(DateTime argDt)
        {
            string strDate = FormatDateToString(argDt).Replace("-", "");
            string strTime = FormatTimeToString(argDt).Replace(":", "");
            return strDate + strTime;
        }

        /// <summary>
        ///     将日期类型转成hh:mm:ss
        /// </summary>
        /// <param name="argDt"></param>
        /// <returns></returns>
        public static string FormatTimeToString(DateTime argDt)
        {
            string strHour = argDt.Hour.ToString();
            if (strHour.Length == 1)
            {
                strHour = "0" + strHour;
            }
            string strMinute = argDt.Minute.ToString();
            if (strMinute.Length == 1)
            {
                strMinute = "0" + strMinute;
            }
            string strSecond = argDt.Second.ToString();
            if (strSecond.Length == 1)
            {
                strSecond = "0" + strSecond;
            }
            return strHour + ":" + strMinute + ":" + strSecond;
        }

        #endregion

        #region Files the content.

        /// <summary>
        ///     Files the content.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static Byte[] FileContent(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                var buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (Exception ex)
            {
                //MessageBoxHelper.ShowPrompt(ex.Message);
                return null;
            }
            finally
            {
                //关闭资源
                fs.Close();
            }
        }

        #endregion

        #region 删除目录

        /// <summary>
        ///     删除目录
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public string DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 
                return dir + " 文件夹删除成功";
            }
            return dir + " 该文件夹不存在"; //如果文件夹不存在则提示 
        }

        #endregion

        #region 文件名非法字符过滤方法

        /// <summary>
        /// 非法字符列表
        /// </summary>
        private static readonly char[] InvalidFileNameChars =
        {
            '"',
            '<',
            '>',
            '|',
            '\0',
            '\u0001',
            '\u0002',
            '\u0003',
            '\u0004',
            '\u0005',
            '\u0006',
            '\a',
            '\b',
            '\t',
            '\n',
            '\v',
            '\f',
            '\r',
            '\u000e',
            '\u000f',
            '\u0010',
            '\u0011',
            '\u0012',
            '\u0013',
            '\u0014',
            '\u0015',
            '\u0016',
            '\u0017',
            '\u0018',
            '\u0019',
            '\u001a',
            '\u001b',
            '\u001c',
            '\u001d',
            '\u001e',
            '\u001f',
            ':',
            '*',
            '?',
            '\\',
            '/'
        };

        /// <summary>
        /// 文件名非法字符过滤方法
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CleanInvalidFileName(string fileName)
        {
            fileName = fileName + "";
            fileName = InvalidFileNameChars.Aggregate(fileName, (current, c) => current.Replace(c + "", ""));
            if (fileName.Length <= 1) return fileName;
            if (fileName[0] != '.') return fileName;
            fileName = "dot" + fileName.TrimStart('.');
            return fileName;
        }

        /// <summary>
        /// 替换文档编码非法字符
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static string CleanInvalidDocumentCode(string strCode)
        {
            if (String.IsNullOrEmpty(strCode)) return "";
            strCode = strCode.Replace('【', '（').Replace('】', '）');
            strCode = strCode.Replace('(', '（').Replace(')', '）');
            return strCode;
        }

        #endregion

        #region 流文件下载

        public static void DownLoadold(string file, string name, string ext)
        {
            DownLoadold(file, name, ext, "IE");
        }

        /// <summary>
        ///     流文件下载
        /// </summary>
        /// <param name="file">文件物理路径</param>
        /// <param name="name">文件名称</param>
        /// <param name="ext">文件扩展名</param>
        /// <param name="browser">浏览器</param>
        public static void DownLoadold(string file, string name, string ext, string browser)
        {
            if (!File.Exists(file)) return;
            var strContentType = "";
            ext = ext.Trim('.'); // 清除扩展名的点号
            name = name.Replace(" ", "　");　// 替换文件名半角空格为全角空格
            switch (ext.ToUpper())
            {
                case "PDF":
                    strContentType = ContentType.PDF;
                    break;
                case "DOC":
                    strContentType = ContentType.DOC;
                    break;
                case "DOCX":
                    strContentType = ContentType.DOCX;
                    break;
                case "XLS":
                    strContentType = ContentType.XLS;
                    break;
                case "XLSX":
                    strContentType = ContentType.XLSX;
                    break;
                case "PPT":
                    strContentType = ContentType.PPT;
                    break;
                case "PPTX":
                    strContentType = ContentType.PPTX;
                    break;
                case "ET":
                    strContentType = ContentType.ET;
                    break;
                case "DPS":
                    strContentType = ContentType.DPS;
                    break;
                case "WPS":
                    strContentType = ContentType.WPS;
                    break;
                case "ZIP":
                    strContentType = ContentType.ZIP;
                    break;
                case "RAR":
                    strContentType = ContentType.RAR;
                    break;
                case "PNG":
                    strContentType = ContentType.PNG;
                    break;
                case "JPG":
                    strContentType = ContentType.JPG;
                    break;
                case "TXT":
                    strContentType = ContentType.TXT;
                    break;
                default:
                    strContentType = ContentType.DEFAULT;
                    break;
            }

            //InternetExplorer
            //Firefox
            //Chrome
            //IE
            //Safari

            var filename = HttpUtility.UrlEncode(name, Encoding.UTF8);
            filename = filename.Replace("+", "%20"); 

            //if (browser == "InternetExplorer" || browser == "IE")
            //{
            //}

            // 火狐、Safari浏览器不需将中文文件名进行编码格式转换
            if (browser == "Firefox" || browser == "Safari")
            {
                filename = name;
            }

            var fi = new FileInfo(file);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
            HttpContext.Current.Response.ContentType = strContentType;
            HttpContext.Current.Response.WriteFile(file);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        #endregion

        #region 压缩单个文件

        /// <summary>
        /// ZIP:压缩单个文件
        /// add Tli by 2016-06-13
        /// </summary>
        /// <param name="fileToZip">需要压缩的文件（绝对路径）</param>
        /// <param name="zipedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="zipedFileName">压缩后的文件名称（文件名，默认 同源文件同名）</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <param name="isEncrypt">是否加密（默认 加密）</param>
        public static void ZipFile(string fileToZip, string zipedPath, string zipedFileName = "",
            int compressionLevel = 5, int blockSize = 2048, bool isEncrypt = false)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(fileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string zipFileName = string.IsNullOrEmpty(zipedFileName)
                ? zipedPath + "\\" +
                  new FileInfo(fileToZip).Name.Substring(0, new FileInfo(fileToZip).Name.LastIndexOf('.')) + ".zip"
                : zipedPath + "\\" + zipedFileName + ".zip";

            using (FileStream zipFile = File.Create(zipFileName))
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(zipFile))
                {
                    using (FileStream streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read))
                    {
                        string fileName = fileToZip.Substring(fileToZip.LastIndexOf("\\", StringComparison.Ordinal) + 1);

                        ZipEntry zipEntry = new ZipEntry(fileName);

                        if (isEncrypt)
                        {
                            //压缩文件加密
                            zipStream.Password = "123";
                        }

                        zipStream.PutNextEntry(zipEntry);

                        //设置压缩级别
                        zipStream.SetLevel(compressionLevel);

                        //缓存大小
                        byte[] buffer = new byte[blockSize];

                        int sizeRead = 0;

                        try
                        {
                            do
                            {
                                sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                                zipStream.Write(buffer, 0, sizeRead);
                            } while (sizeRead > 0);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        streamToZip.Close();
                    }

                    zipStream.Finish();
                    zipStream.Close();
                }

                zipFile.Close();
            }
        }

        #endregion

        #region 压缩文件夹

        /// <summary>
        /// ZIP：压缩文件夹
        /// add Tli by 2016-06-13
        /// </summary>
        /// <param name="directoryToZip">需要压缩的文件夹（绝对路径）</param>
        /// <param name="zipedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="zipedFileName">压缩后的文件名称（文件名，默认 同源文件夹同名）</param>
        /// <param name="isEncrypt">是否加密（默认 加密）</param>
        public static void ZipDirectory(string directoryToZip, string zipedPath, string zipedFileName = "",
            bool isEncrypt = false)
        {
            //如果目录不存在，则报错
            if (!Directory.Exists(directoryToZip))
            {
                throw new FileNotFoundException("指定的目录: " + directoryToZip + " 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string zipFileName = string.IsNullOrEmpty(zipedFileName)
                ? zipedPath + "\\" + new DirectoryInfo(directoryToZip).Name + ".zip"
                : zipedPath + "\\" + zipedFileName + ".zip";

            //using (FileStream zipFile = File.Create(zipFileName)){}

            FileStream zipFile = null;

            try
            {
                zipFile = File.Create(zipFileName);

                using (ZipOutputStream s = new ZipOutputStream(zipFile))
                {
                    if (isEncrypt)
                    {
                        //压缩文件加密
                        s.Password = "123";
                    }
                    ZipSetp(directoryToZip, s, "");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (zipFile != null)
                    zipFile.Dispose();  
            }

        }

        /// <summary>
        /// 递归遍历目录
        /// add Tli by 2016-06-13
        /// </summary>
        private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory += Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();

            string[] filenames = Directory.GetFileSystemEntries(strDirectory);

            foreach (string file in filenames) // 遍历所有的文件和目录
            {
                if (Directory.Exists(file)) // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    pPath += "\\";
                    ZipSetp(file, s, pPath);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        string fileName = parentPath + file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                        ZipEntry entry = new ZipEntry(fileName);

                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;

                        fs.Close();

                        crc.Reset();
                        crc.Update(buffer);

                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);

                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        #endregion

        #region 解压一个zip文件

        /// <summary>
        /// ZIP:解压一个zip文件
        /// add Tli by 2016-06-13
        /// </summary>
        /// <param name="zipFile">需要解压的Zip文件（绝对路径）</param>
        /// <param name="targetDirectory">解压到的目录</param>
        /// <param name="password">解压密码</param>
        /// <param name="overWrite">是否覆盖已存在的文件</param>
        public static void UnZip(string zipFile, string targetDirectory, string password, bool overWrite = true)
        {
            //如果解压到的目录不存在，则报错
            if (!Directory.Exists(targetDirectory))
            {
                throw new FileNotFoundException("指定的目录: " + targetDirectory + " 不存在!");
            }
            //目录结尾
            if (!targetDirectory.EndsWith("\\"))
            {
                targetDirectory = targetDirectory + "\\";
            }

            //using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(zipFile))){}

            ZipInputStream zipfiles = null;

            try
            {
                zipfiles = new ZipInputStream(File.OpenRead(zipFile));
                zipfiles.Password = password;
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    var directoryName = "";
                    var pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    var fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(targetDirectory + directoryName);

                    if (fileName == "") continue;

                    if ((File.Exists(targetDirectory + directoryName + fileName) && overWrite) ||
                        !File.Exists(targetDirectory + directoryName + fileName))
                    {
                        using (var streamWriter = File.Create(targetDirectory + directoryName + fileName))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                var size = zipfiles.Read(data, 0, data.Length);

                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        }
                    }
                }

                zipfiles.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (zipfiles != null)
                    zipfiles.Dispose();
            }
        }

        #endregion
    }
}
