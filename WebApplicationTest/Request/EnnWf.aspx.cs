using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Hsp.Test.Common;

namespace WebApplicationTest.Request
{
    public partial class EnnWf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 新奥工作流列表测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            // 标准模块服务地址
            var serviceUrl = "http://182.92.9.188:8048/";
            //serviceUrl = "http://localhost:1848/";

            if (!serviceUrl.EndsWith("/")) serviceUrl += "/";
            var logServiceUrl = serviceUrl + "WebService/WorkFlowService.asmx/UserTodoList";

            string rst = "", responeJsonStr = ""; // 返回结果，输入参数

            #region 输入参数

            //responeJsonStr = "{";
            //responeJsonStr += "\"userId\": \"19200162\"";
            //responeJsonStr += "}";

            responeJsonStr = "userId=" + 19200162;

            #endregion


            //// 获取凭证验证结果
            WebRequestHelper wrh = new WebRequestHelper();
            rst = wrh.HttpPost(logServiceUrl, responeJsonStr, Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var hh = rst;

            //var strEncode = Encoding.UTF8;
            //HttpWebResponse res;
            //try
            //{
            //    //logServiceUrl += "?userId=" + 19200162;
            //    //responeJsonStr = "userId=" + 19200162;

            //    byte[] byteArray = strEncode.GetBytes(responeJsonStr); //转化
            //    var webReq = (HttpWebRequest)WebRequest.Create(new Uri(logServiceUrl));
            //    webReq.Method = "POST";
            //    webReq.ContentType = "application/x-www-form-urlencoded";
            //    //webReq.ContentType = "text/xml; charset=utf-8";
            //    //webReq.ContentType = "text/xml";
            //    webReq.ContentLength = byteArray.Length;
            //    Stream dataStream = webReq.GetRequestStream();
            //    dataStream.Write(byteArray, 0, byteArray.Length); //写入参数
            //    dataStream.Close();
            //    res = (HttpWebResponse)webReq.GetResponse();
            //}
            //catch (WebException ex)
            //{
            //    res = (HttpWebResponse)ex.Response;
            //}
            //StreamReader sr = new StreamReader(res.GetResponseStream(), strEncode);
            //var strHtml = sr.ReadToEnd();

            //var aa = strHtml;










            //string str = GetWebContent(logServiceUrl + "&userId=19200162");
            //XmlDocument doc = new XmlDocument();

        }


        public static string GetWebContent(string strUrl)
        {
            WebRequest req = HttpWebRequest.Create(strUrl);
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (Stream read = response.GetResponseStream())
                    {
                        byte[] arr = new byte[1];
                        IList<byte> lstByte = new List<byte>();
                        while (true)
                        {
                            int count = read.Read(arr, 0, 1);
                            if (count > 0)
                            {
                                lstByte.Add((byte)arr[0]);
                            }
                            else
                                break;
                        }

                        byte[] buf = new byte[lstByte.Count];
                        for (int i = 0; i < lstByte.Count; i++)
                        {
                            buf[i] = lstByte[i];
                        }
                        System.Text.Encoding encod = Encoding.GetEncoding("utf-8");
                        string strXML = encod.GetString(buf);
                        return strXML;
                    }
                }
            }
        }
    }


}