using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// FileMd5Handler 的摘要说明
    /// </summary>
    public class FileMd5Handler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var rst = "";

            var filename = context.Request.Form["filename"];
            var filepath = context.Request.Form["filepath"];

            var file = context.Request.Files["fileData"];


            if (file != null)
            {
                var fileLength = file.ContentLength; // 附件长度
                var contentType = file.ContentType; // 互联网媒体类型，Internet Media Type，MIME类型

                var filename1 = file.FileName; // 附件名称 D:\新建文件夹 (4)\99A坦克.jpg








                var fileUrl = "/Files/1901_Feature_Overview_Brief_FINAL.pdf";
                var filePath =
                    @"E:\Software\Develop\SQL2008R2\cn_sql_server_2008_r2_enterprise_x86_x64_ia64_dvd_522233.ISO";
                filePath = @"d:\1901_Feature_Overview_Brief_FINAL.pdf";
                filePath = filename;


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

                    } //关闭文件流
                } //结束计算

                #endregion







            }
            else
            {
                rst = "获取文件为空！";
            }





            context.Response.Write(rst);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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