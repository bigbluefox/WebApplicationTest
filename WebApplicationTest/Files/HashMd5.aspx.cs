using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Hsp.Test.Common;

namespace WebApplicationTest.Files
{
    public partial class HashMd5 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var rst = "";

            //Stopwatch sw = new Stopwatch();
            //Random rand = new Random();  // seed a random number generator
            //int numberOfBytes = 2 << 22; //8,192KB File
            //byte nextByte;
            //for (int i = 1; i <= 28; i++) //Limited loop to 28 to prevent out of memory
            //{
            //    sw.Start();
            //    using (FileStream fs = new FileStream(
            //        String.Format(@"D:\Text\TEST{0}.DAT", i),  // name of file
            //        FileMode.Create,    // create or overwrite existing file
            //        FileAccess.Write,   // write-only access
            //        FileShare.None,     // no sharing
            //        2 << i,             // block transfer of i=18 -> size = 256 KB
            //        FileOptions.None))
            //    {
            //        for (int j = 0; j < numberOfBytes; j++)
            //        {
            //            nextByte = (byte)(rand.Next() % 256); // generate a random byte
            //            fs.WriteByte(nextByte);               // write it
            //        }
            //    }
            //    sw.Stop();
            //    //Console.WriteLine(String.Format("Buffer is 2 << {0} Elapsed: {1}", i, sw.Elapsed));

            //    rst += String.Format("Buffer is 2 << {0} Elapsed: {1}", i, sw.Elapsed) + "<br/>";

            //    sw.Reset();
            //}

            //lblResult.Text = rst;
        }

        /// <summary>
        ///     计算大文件的MD5值测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMd5Test_Click(object sender, EventArgs e)
        {

            var fileValue = FileUpload1.FileName;

            var postFileName = FileUpload1.PostedFile.FileName;







            var rst = "";

            var fileUrl = "/Files/1901_Feature_Overview_Brief_FINAL.pdf";
            var filePath = @"E:\Software\Develop\SQL2008R2\cn_sql_server_2008_r2_enterprise_x86_x64_ia64_dvd_522233.ISO";
            filePath = @"d:\1901_Feature_Overview_Brief_FINAL.pdf";
            filePath = postFileName;


            FileInfo fi = new FileInfo(filePath);
            rst += "名称：" + fi.Name + "<br/>";
            rst += "大小：" + fi.Length + "<br/>";
            rst += "<br/>";

            //filePath = Server.MapPath(fileUrl);

            fi = new FileInfo(filePath);

            rst += "名称：" + fi.Name + "<br/>";
            rst += "大小：" + fi.Length + "<br/>";
            rst += "<br/>";

            rst += "方法1【GetMD5ByMD5CryptoService】<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";
            rst += MD5Checker.GetMD5ByMD5CryptoService(filePath) + "<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";

            rst += "<br/>";

            rst += "方法2【GetMD5ByHashAlgorithm】<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";
            rst += MD5Checker.GetMD5ByHashAlgorithm(filePath) + "<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";

            rst += "<br/>";

            rst += "方法3【ComputeMD5】<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";
            rst += HashHelper.ComputeMD5(filePath) + "<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";

            rst += "<br/>";

            rst += "方法4【MD5】<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";
            rst += MD5(filePath) + "<br/>";
            rst += DateTime.Now.Ticks + "<br/>";
            rst += DateTime.Now + "<br/>";


            #region 读取前200K数据计算MD5值

            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (File.Exists(filePath))
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var readLength = 200*1024; // 读取前200K数据计算MD5值

                    //尚未读取的文件内容长度
                    var left = fs.Length;

                    if (left < readLength)
                    {
                        readLength = Convert.ToInt32(left);
                    }

                    var fileByte = new byte[readLength];
                    fs.Seek(readLength, SeekOrigin.Begin);
                    fs.Read(fileByte, 0, readLength);

                    var ms = new MemoryStream(fileByte);
                    var hashMd5 = HashHelper.ComputeMD5(ms);

                    rst += "<br/>";

                    rst += "方法5【ComputeMD5，计算流文件的MD5值，前200K】<br/>";
                    rst += DateTime.Now.Ticks + "<br/>";
                    rst += DateTime.Now + "<br/>";
                    rst += hashMd5 + "<br/>";
                    rst += DateTime.Now.Ticks + "<br/>";
                    rst += DateTime.Now + "<br/>";

                    ////存储读取结果
                    //byte[] bytes = new byte[1024];
                    ////每次读取长度
                    //int maxLength = bytes.Length;
                    ////读取位置
                    //int start = 0;
                    ////实际返回结果长度
                    //int num = 0;
                    ////当文件未读取长度大于0时，不断进行读取
                    //while (start < readLength)
                    //{
                    //    fs.Position = start;
                    //    num = 0;
                    //    if (left < maxLength)
                    //        num = fs.Read(bytes, 0, Convert.ToInt32(left));
                    //    else
                    //        num = fs.Read(bytes, 0, maxLength);
                    //    if (num == 0)
                    //        break;
                    //    start += num;
                    //    left -= num;
                    //    //Console.WriteLine(Encoding.UTF8.GetString(bytes));
                    //}

                    //计算文件的MD5值
                    //var calculator = MD5.Create();
                    //var buffer = calculator.ComputeHash(fs);
                    //calculator.Clear();
                    ////将字节数组转换成十六进制的字符串形式
                    //var stringBuilder = new StringBuilder();
                    //for (var i = 0; i < buffer.Length; i++)
                    //{
                    //    stringBuilder.Append(buffer[i].ToString("x2"));
                    //}
                    //hashMD5 = stringBuilder.ToString();
                } //关闭文件流
            } //结束计算

            #endregion



            lblResult.Text = rst;
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="fileName">字符串</param>
        /// <returns></returns>
        public static string MD5(string fileName)
        {
            var hashMD5 = string.Empty;
            if (File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var md5 = new MD5CryptoServiceProvider();
                    //var bytValue = encoding.GetBytes(s);
                    var bytHash = md5.ComputeHash(fs);
                    md5.Clear();

                    var result = new StringBuilder();
                    for (var i = 0; i < bytHash.Length; i++)
                    {
                        result.Append(bytHash[i].ToString("x").PadLeft(2, '0'));
                    }
                    hashMD5 = result.ToString();
                }
            }

            return hashMD5;
        }
    }
}