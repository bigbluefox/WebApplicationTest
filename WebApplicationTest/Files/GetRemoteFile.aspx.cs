using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

namespace WebApplicationTest.Files
{
    public partial class GetRemoteFile : System.Web.UI.Page
    {
        public string RemoteFile = "http://112.124.17.9:8080/FilePath/Test/3.txt";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 获取远程文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetRemoteFile_Click(object sender, EventArgs e)
        {
            //string txtContent = Get(RemoteFile);
            //var t = txtContent;

//http://112.124.17.9:8080/FilePath/Test/1.bak
//        http://112.124.17.9:8080/FilePath/Test/2.rar
//        http://112.124.17.9:8080/FilePath/Test/3.txt
//        http://112.124.17.9:8080/FilePath/Test/4.sql
//        http://112.124.17.9:8080/FilePath/Test/5.log
//        http://112.124.17.9:8080/FilePath/Test/6.docx

//        http://112.124.17.9:8080/FilePath/Test/db.rar


            string RemoteFile = "http://112.124.17.9:8080/FilePath/Test/2.rar";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/4.sql";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/5.log";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/6.docx";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/db.rar";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/2.rar";
            RemoteFile = "http://112.124.17.9:8080/FilePath/Test/7.pdf";


            var url = "http://localhost:1845/Test/FileHandler.ashx";
            var name = "/FilePath/FileTest/PDF文件类型.pdf";
            name = "/FilePath/FileTest/PPT文件类型.ppt";
            name = "/FilePath/Accessory/GB 93-1987 标准型弹簧垫圈.pdf";
            url += "?file=" + HttpUtility.UrlEncode(name);
            RemoteFile = url;

            //WebRequest myrequest = WebRequest.Create(RemoteFile);
            //WebResponse myresponse = myrequest.GetResponse();
            //Stream imgstream = myresponse.GetResponseStream();
            //if (imgstream == null) return;
            //System.Drawing.Image img = System.Drawing.Image.FromStream(imgstream);
            ////img.Save(Server.MapPath("test.jpg"),System.Drawing.Imaging.ImageFormat.Jpeg);
            //MemoryStream ms = new MemoryStream();
            //img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.ClearContent(); //需要输出图象信息 要修改HTTP头 
            ////Response.ContentType = "image/gif";
            //Response.ContentType = Hsp.Test.Common.ContentType.RAR;
            //Response.BinaryWrite(ms.ToArray());

            //WebRequest request = WebRequest.Create(RemoteFile);
            //WebResponse response = request.GetResponse();
            //Stream responseStream = response.GetResponseStream();

            //Response.ContentType = Hsp.Test.Common.ContentType.RAR;
            //Response.BinaryWrite(responseStream.EndRead());

            string tempPath = HttpContext.Current.Server.MapPath(".");
            //Application.StartupPath + "\\";
            var arrFile = RemoteFile.Split('/');
            arrFile = name.Split('/');
            var fileName = arrFile[arrFile.Length - 1];

            //设定存放路径，被下载文件的url
            string savePath = tempPath + "\\" + "Mixed.rar", downFileUrl = "http://url:port" + "//" + "filename";
            savePath = tempPath + "\\" + fileName;
            downFileUrl = RemoteFile;

            WebClient wcClient = new WebClient();
            //对远程文件发送一个请求
            WebRequest webReq = WebRequest.Create(downFileUrl);
            //接收远程WEB服务器发回的响应
            WebResponse webRes = webReq.GetResponse();
            //获取文件长度
            long fileLength = webRes.ContentLength;
            //创建一个文件流，接收返回的流信息
            Stream srm = webRes.GetResponseStream();
            //使用特殊编码读取流信息
            if (srm == null) return;

            StreamReader srmReader = new StreamReader(srm);
            //定义缓冲区
            byte[] bufferbyte = new byte[fileLength];
            //缓冲区长度
            int allByte = (int) bufferbyte.Length;
            int startByte = 0;
            //读取缓冲区内容
            while (fileLength > 0)
            {
                int downByte = srm.Read(bufferbyte, startByte, allByte);
                if (downByte == 0)
                {
                    break;
                }
                
                startByte += downByte;
                allByte -= downByte;
            }
            //判断存储路径每一个节点是否存在
            if (!System.IO.File.Exists(savePath))
            {
                string[] dirArray = savePath.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
            //创建一个文件流，将处理的文件流写入磁盘
            FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.Write(bufferbyte, 0, bufferbyte.Length);
            srm.Close();
            srmReader.Close();
            fs.Close();

            //if (!System.IO.File.Exists(Application.StartupPath + "//Mixed.mdb"))
            //{
            //    labresult.Text = "下载失败！";
            //}
            //else
            //{
            //    labresult.Text = "下载成功";
            //}   
        }


        private string Get(string strURL)
        {
            HttpWebRequest request;
            // 创建一个HTTP请求
            request = (HttpWebRequest) WebRequest.Create(strURL);
            HttpWebResponse response;
            response = (HttpWebResponse) request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }

        #region 成员变量

        private string savePath;
        private bool newName;

        #endregion

        #region 属性

        public string SavePath
        {
            set
            {
                savePath = value.Replace("/", "\\");
                if (savePath.LastIndexOf("\\", StringComparison.Ordinal) != savePath.Length - 1)
                    savePath += "\\";
            }
            get { return savePath; }
        }

        public bool NewName
        {
            set { newName = value; }
            get { return newName; }
        }

        #endregion

        #region 构造函数

        //public DownFile() 
        //{
        //    newName = false;
        //    savePath = "c:\\";
        //}

        #endregion

        #region 私有方法

        /// <summary>
        /// 随机返回文件名
        /// </summary>
        /// <returns></returns>
        private string getFileName()
        {
            System.Random objDom = new Random();
            int intNum = objDom.Next(1000, 9999);
            objDom = null;
            string strNum = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            return strNum + intNum.ToString();
        }

        /// <summary>
        /// 根据文件得到文件扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private string getExtension(string fileName)
        {
            int start = fileName.IndexOf(".") + 1;
            string Ext = fileName.Substring(start, fileName.Length - start);
            return Ext;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 保存远程文件
        /// </summary>
        /// <param name="filePath">远程文件路径</param>
        /// <returns></returns>
        public string SaveFile(string filePath)
        {
            string fPath, fName, sPath;
            fPath = filePath.Replace("\\", "/");
            int start = fPath.LastIndexOf("/") + 1;
            fName = fPath.Substring(start, fPath.Length - start);
            if (newName)
            {
                sPath = savePath + getFileName() + "." + getExtension(fName);
            }
            else
            {
                sPath = savePath + fName;
            }

            System.Net.WebClient myWebClient = new System.Net.WebClient();
            try
            {
                myWebClient.DownloadFile(fPath, sPath);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                myWebClient.Dispose();
            }
            return fName;
        }

        #endregion
    }
}