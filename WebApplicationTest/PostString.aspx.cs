using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class PostString : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:1406/Handler/TestHandler.ashx?OPERATION=RECEIVEMESSAGE";
            var strMessage = txtString.Text ?? "家庭";
            string result = Post(url, strMessage, "application/x-www-form-urlencoded");

            lblResult.Text = result;
        }

        /// <summary>
        /// 发送Json串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendJson_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:1406/Handler/TestHandler.ashx?OPERATION=RECEIVEMESSAGE";
            var strMessage = txtString.Text ?? "家庭";

            //获取凭证(access_token)输入参数 ：
            var responeJsonStr = "{";
            responeJsonStr += "\"Message\": \"" + strMessage + "\",";
            responeJsonStr += "\"SendTime\": \"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\"";
            responeJsonStr += "}";

            string result = Post(url, responeJsonStr, "application/json;charset=UTF-8");

            lblResult.Text = result;
        }


        #region Post

        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>
        /// <param name="contentType">发送数据内容类型</param>
        /// <returns>结果</returns>  
        public static string Post(string url, string content, string contentType)
        {
            //申明一个容器result接收数据
            string result = "";
            //首先创建一个HttpWebRequest,申明传输方式POST
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = contentType;

            //添加POST参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            //申明一个容器resp接收返回数据
            HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion

        #region Get

        /// <summary>
        /// 请求http get 方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            //GET方法
            //首先创建一个httpRequest，用Webrequest.Create(url)的方法
            HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create(url);
            //初始化httpRequest，申明用的GET方法请求http
            httpRequest.Timeout = 2000;
            httpRequest.Method = "GET";
            //创建一个httpResponse，存放服务器返回信息
            HttpWebResponse httpResponse = (HttpWebResponse) httpRequest.GetResponse();
            //这个地方我也不晓得干了啥，反正都是抄写别人的
            //就理解为读取页面吧
            StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
            //页面读完了，result接收结果
            string result = sr.ReadToEnd();
            //一个新字符串，其中当前实例中出现的所有指定字符串都替换为另一个指定的字符串 Replace(string oldValue,string newValue)
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            //请求状态的反馈
            int status = (int) httpResponse.StatusCode;
            sr.Close();

            return result;

        }

        #endregion

        #region 获取远程数据

        /// <summary>
        /// 获取远程数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGet_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:1406/Handler/TestHandler.ashx?OPERATION=SENDMESSAGE&ID=" + Guid.NewGuid().ToString();
            string result = Get(url);
            lblResult.Text = result;
        }

        #endregion



    }
}