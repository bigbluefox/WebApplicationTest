using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Aip_CharacterRecognition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = "";
    }

    #region 文字识别

    /// <summary>
    /// 文字识别
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCharacterRecognition_Click(object sender, EventArgs e)
    {
        var rst = general();
        this.lblResult.Text = rst;
    }

    // 通用文字识别
    public static string general()
    {
        var file = "/work/ai/images/ocr/general.jpeg";
        string token = "#####调用鉴权接口获取的token#####";
        string strbaser64 = Img2String(file); // //FileUtils.getFileBase64("/work/ai/images/ocr/general.jpeg"); // 图片的base64编码
        string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general?access_token=" + token;
        Encoding encoding = Encoding.Default; 
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "post";
        request.ContentType = "application/x-www-form-urlencoded";
        request.KeepAlive = true;
        String str = "image=" + HttpUtility.UrlEncode(strbaser64);
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Console.WriteLine("通用文字识别:");
        Console.WriteLine(result);
        return result;
    }

    #endregion

    #region 图片转字符

    /// <summary>
    /// 图片转字符
    /// </summary>
    /// <returns></returns>
    public static string Img2String(string file)
    {
        try
        {
            Stream s = File.Open(file, FileMode.Open, FileAccess.Read);
            int leng = 0;
            if (s.Length < Int32.MaxValue)
            {
                leng = (int) s.Length;
            }
            var by = new byte[leng];
            s.Read(by, 0, leng); //把图片读到字节数组中
            s.Close();

            string str = Convert.ToBase64String(by); //把字节数组转换成字符串
            //StreamWriter sw = File.CreateText("G:\\11.txt");//存入11.txt文件
            //sw.Write(str);
            //sw.Close();
            //sw.Dispose();

            return str;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    #endregion
}
    public static class AccessToken
    {
        // 设置APPID/AK/SK
        internal static string APP_ID = "10275427";
        internal static string API_KEY = "y2ZIbUNqLKOLRaXBzOQrchdC";
        internal static string SECRET_KEY = "2MBnjf5hLmA3ntuGk86KE0TjwPWhRMYj";

        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "百度云应用的AK";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "百度云应用的SK";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }