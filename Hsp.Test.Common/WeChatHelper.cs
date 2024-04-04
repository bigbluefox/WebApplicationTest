using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hsp.Test.Common
{
    /// <summary>
    ///     微信公众平台操作类
    /// </summary>
    public class WeChatHelper
    {
        /*
        /// <summary>
        ///     微信访问凭证Cookie名称
        /// </summary>
        private const string CookieName = "Token";

        /// <summary>
        ///     账号信息：corpId
        /// </summary>
        private const string corpId = "wx02980f0c94e6ae7c";

        /// <summary>
        ///     账号信息：Secret
        /// </summary>
        private const string Secret = "d4624c36b6795d1d99dcf0547af5443d";

        /// <summary>
        ///     接口配置信息：URL
        /// </summary>
        private const string Url = "http://mobile.processist.cn/Handler/WeChatHandler.ashx";

        /// <summary>
        ///     接口配置信息：Token
        /// </summary>
        private const string Token = "c1160b1a1df24024b75802a6d2604298";

        /// <summary>
        /// EncodingAESKey
        /// </summary>
        private const string EncodingAESKey = "6oeHHGyiEybqtlViKjbxRTBqvhyPhLrKYZWCKEgJeJh";

         * 
         */

        #region 获取微信Token

        /// <summary>
        ///     获取微信Token
        /// </summary>
        /// <param name="corpId">账号信息：corpId </param>
        /// <param name="secret">账号信息：Secret </param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetToken(string corpId, string secret, string cookieName)
        {
            string token = string.Empty;
            var tokenExpiresTimeName = "TokenExpiresTime";
            if (TokenHashTable.Instance().Contains(cookieName))
            {
                token = TokenHashTable.Instance()[cookieName].ToString();
                var expires_in = (DateTime)(TokenHashTable.Instance()[tokenExpiresTimeName]);

                System.TimeSpan timeSpan = DateTime.Now - expires_in;
                double seconds = timeSpan.Seconds;

                if (seconds > 3600)
                {
                    // 设定过期时间为一个小时
                    //return TokenHashTable.Instance()[cookieName].ToString(); 
                    token = null;
                }
            }

            // 当服务程序调用时候，不能使用Cookie，因此要改写
            //string cookie = CookieHelper.GetCookieValue(cookieName);
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }
            else
            {
                string res = "";
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId, secret);
                var req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    var myResponse = (HttpWebResponse)req.GetResponse();
                    var reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                    string content = reader.ReadToEnd();

                    // {"access_token":"e04rSYzv4282rJ0odB3R7FAobiZJW0NpyoyJjppBHpkzEnSHcXZMWv2jGt4WglHitB8jf8Pa9KKXSenVEEZu4g_N7UeKX7SRKM1LQTj6Uw8EPReAFAMXZ","expires_in":7200}

                    var myAccesstoken = JsonHelper.JsonDeserialize<AccessToken>(content);
                    res = myAccesstoken.access_token;
                }

                //CookieHelper.SetWeChatTokenCookie(cookieName, res);

                TokenHashTable.Instance().Clear();
                TokenHashTable.Instance().Add(cookieName, res);
                TokenHashTable.Instance().Add(tokenExpiresTimeName, DateTime.Now);

                return res;
            }
        }

        #endregion

        #region 校验签名

        /// <summary>
        /// 获取解密组合字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetEncryptXmlString(string str)
        {
            return string.Format(@"<xml><Encrypt><![CDATA[{0}]]></Encrypt></xml>", str);
        }

        #endregion

        #region 发送信息

        /// <summary>
        ///     发送信息
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string SendMessage(string accessToken, string paramData, Encoding dataEncode)
        {
            //https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}
            //https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}

            string postUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}",
                accessToken);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region 群发信息

        /// <summary>
        ///     群发信息
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string SendMassMessage(string accessToken, string paramData, Encoding dataEncode)
        {
            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}",
                accessToken);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region 删除群发信息

        /// <summary>
        ///     删除群发信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static string DeleteMassMessage(string accessToken, string paramData, Encoding dataEncode)
        {
            return null;
        }

        #endregion

        #region 创建应用菜单

        /// <summary>
        ///     创建应用菜单
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看 </param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string CreateAppMenu(string accessToken, string agentId, string paramData, Encoding dataEncode)
        {
            //https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}
            //https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}

            string postUrl =
                string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token={0}&agentid={1}",
                    accessToken, agentId);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region 删除菜单

        /// <summary>
        ///     删除菜单 
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看 </param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string DeleteAppMenu(string accessToken, string agentId, string paramData, Encoding dataEncode)
        {
            string postUrl =
                string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/delete?access_token={0}&agentid={1}",
                    accessToken, agentId);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region ####

        //public void Handle(string postStr)
        //{
        //    //封装请求类  
        //    var doc = new XmlDocument();
        //    doc.LoadXml(postStr);
        //    XmlElement rootElement = doc.DocumentElement;
        //    //MsgType  
        //    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
        //    //接收的值--->接收消息类(也称为消息推送)  
        //    var requestXML = new RequestXML();
        //    requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
        //    requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
        //    requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
        //    requestXML.MsgType = MsgType.InnerText;

        //    //根据不同的类型进行不同的处理  
        //    switch (requestXML.MsgType)
        //    {
        //        case "text": //文本消息  
        //            requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
        //            break;
        //        case "image": //图片  
        //            requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
        //            break;
        //        case "location": //位置  
        //            requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
        //            requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
        //            requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
        //            requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
        //            break;
        //        case "link": //链接  
        //            break;
        //        case "event": //事件推送 支持V4.5+  
        //            break;
        //    }

        //    //消息回复  
        //    //ResponseMsg(requestXML);
        //}

        #endregion

        #region unix时间转换为datetime

        /// <summary>
        ///     unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        #endregion

        #region datetime转换为unixtime

        /// <summary>
        ///     datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        #endregion

        #region 调用百度地图，返回坐标信息

        /// <summary>
        ///     调用百度地图，返回坐标信息
        /// </summary>
        /// <param name="y">经度</param>
        /// <param name="x">纬度</param>
        /// <returns></returns>
        public string GetMapInfo(string x, string y)
        {
            try
            {
                string res = string.Empty;
                string parame = string.Empty;
                string url = "http://maps.googleapis.com/maps/api/geocode/xml";

                parame = "latlng=" + x + "," + y + "&language=zh-CN&sensor=false"; //此key为个人申请  
                res = HttpPost(url, parame);

                var doc = new XmlDocument();
                doc.LoadXml(res);

                XmlElement rootElement = doc.DocumentElement;
                string Status = rootElement.SelectSingleNode("status").InnerText;

                if (Status == "OK")
                {
                    //仅获取城市  
                    XmlNodeList xmlResults = rootElement.SelectSingleNode("/GeocodeResponse").ChildNodes;
                    for (int i = 0; i < xmlResults.Count; i++)
                    {
                        XmlNode childNode = xmlResults[i];
                        if (childNode.Name == "status")
                        {
                            continue;
                        }
                        string city = "0";
                        for (int w = 0; w < childNode.ChildNodes.Count; w++)
                        {
                            for (int q = 0; q < childNode.ChildNodes[w].ChildNodes.Count; q++)
                            {
                                XmlNode childeTwo = childNode.ChildNodes[w].ChildNodes[q];
                                if (childeTwo.Name == "long_name")
                                {
                                    city = childeTwo.InnerText;
                                }
                                else if (childeTwo.InnerText == "locality")
                                {
                                    return city;
                                }
                            }
                        }
                        return city;
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteTxt("map异常:" + ex.Message.ToString() + "Struck:" + ex.StackTrace.ToString());  
                return "0";
            }
            return "0";
        }

        #endregion

        #region Get请求

        /// <summary>    
        /// HTTP GET 方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "*/*";
            req.Timeout = 15000;
            req.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = req.GetResponse();
                {
                    var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                req = null;
                response = null;
            }

            return responseStr;
        }

        #endregion

        #region Get请求(有参数)

        /// <summary>    
        /// HTTP GET 方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding dataEncode)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "*/*";
            req.Timeout = 15000;
            req.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = req.GetResponse();
                {
                    var reader = new StreamReader(response.GetResponseStream(), dataEncode);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                req = null;
                response = null;
            }

            return responseStr;
        }

        #endregion

        #region Post发送(无参数)

        /// <summary>   
        /// HTTP POST方式请求数据         
        /// </summary> 
        /// <param name="url">URL.</param>          
        /// <returns></returns>
        public static string HttpPost(string url)
        {

            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "*/*";
            req.Timeout = 15000;
            req.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;
            try
            {
                response = req.GetResponse();
                {
                    var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    //File.WriteAllText(Server.MapPath("~/") + @"\test.txt", responseStr); 
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                req = null;
                response = null;
            }

            return responseStr;
        }

        #endregion

        #region Post发送(有参数)

        /// <summary>
        ///     Post 提交调用抓取
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="param">参数</param>
        /// <returns>string</returns>
        public string HttpPost(string url, string param)
        {
            byte[] bs = Encoding.UTF8.GetBytes(param);
            var req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
            req.Method = "POST";
            req.Accept = "*/*";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }

            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理  
                Stream strm = wr.GetResponseStream();
                if (strm != null)
                {
                    var sr = new StreamReader(strm, Encoding.UTF8);

                    string line;
                    var sb = new StringBuilder();
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line + Environment.NewLine);
                    }
                    sr.Close();
                    strm.Close();
                    return sb.ToString();
                }
                return null;
            }
        }

        #endregion

        #region Post数据接口(有参数)

        /// <summary>
        ///     Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string HttpPost(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                var webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length); //写入参数
                newStream.Close();
                var response = (HttpWebResponse)webReq.GetResponse();
                var sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        #endregion

        #region 获取微信服务器IP地址

        /// <summary>
        ///     获取微信服务器IP地址
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string GetWeChatServer(string accessToken, string paramData, Encoding dataEncode)
        {
            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}",
                accessToken);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region 获取微信用户列表

        /// <summary>
        ///     获取微信用户列表(GET)
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string GetWeChatUserList(string accessToken, string paramData, Encoding dataEncode)
        {
            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid=",
                accessToken);
            return HttpPost(postUrl, paramData, dataEncode);
        }

        #endregion

        #region 获取微信用户信息

        /// <summary>
        ///     获取微信用户信息(GET)
        /// </summary>
        /// <param name="accessToken">获取到的凭证</param>
        /// <param name="openid">普通用户的标识，对当前公众号唯一</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string GetWeChatUserInfo(string accessToken, string openid, Encoding dataEncode)
        {
            string postUrl =
                string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN",
                    accessToken, openid);
            return HttpPost(postUrl, "", dataEncode);
        }

        #endregion
    }


    /// <summary>
    ///     微信公众平台操作类
    /// </summary>
    public class weixin
    {
        private string Token = "my_weixin_token"; //换成自己的token  


        /// <summary>
        ///     消息回复(微信信息返回)
        /// </summary>
        /// <param name="requestXML">The request XML.</param>
        private void ResponseMsg(RequestXML requestXML)
        {
            try
            {
                //string resXml = "";
                ////主要是调用数据库进行关键词匹配自动回复内容，可以根据自己的业务情况编写。  
                ////1.通常有，没有匹配任何指令时，返回帮助信息  
                //AutoResponse mi = new AutoResponse(requestXML.Content, requestXML.FromUserName);

                //switch (requestXML.MsgType)
                //{
                //    case "text":
                //        //在这里执行一系列操作，从而实现自动回复内容.   
                //        string _reMsg = mi.GetReMsg();
                //        if (mi.msgType == 1)
                //        {
                //            resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName +
                //                     "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName +
                //                     "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) +
                //                     "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>2</ArticleCount><Articles>";
                //            resxml += mi.GetRePic(requestXML.FromUserName);
                //            resxml += "</Articles><FuncFlag>1</FuncFlag></xml>";
                //        }
                //        else
                //        {
                //            resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName +
                //                     "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName +
                //                     "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) +
                //                     "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + _reMsg +
                //                     "]]></Content><FuncFlag>1</FuncFlag></xml>";
                //        }
                //        break;
                //    case "location":
                //        string city = GetMapInfo(requestXML.Location_X, requestXML.Location_Y);
                //        if (city == "0")
                //        {
                //            resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName +
                //                     "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName +
                //                     "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) +
                //                     "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[好啦，我们知道您的位置啦。您可以:" +
                //                     mi.GetDefault() + "]]></Content><FuncFlag>1</FuncFlag></xml>";
                //        }
                //        else
                //        {
                //            resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName +
                //                     "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName +
                //                     "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) +
                //                     "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[好啦，我们知道您的位置啦。您可以:" +
                //                     mi.GetDefault() + "]]></Content><FuncFlag>1</FuncFlag></xml>";
                //        }
                //        break;
                //    case "image":
                //        //图文混合的消息 具体格式请见官方API“回复图文消息”   
                //        break;
                //}

                //System.Web.HttpContext.Current.Response.Write(resXml);
                //WriteToDB(requestXML, resxml, mi.pid);
            }
            catch (Exception ex)
            {
                //WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
                //wx_logs.MyInsert("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
            }
        }


        /// <summary>
        ///     将本次交互信息保存至数据库中
        /// </summary>
        /// <param name="requestXML"></param>
        /// <param name="_xml"></param>
        /// <param name="_pid"></param>
        private void WriteToDB(RequestXML requestXML, string _xml, int _pid)
        {
            //WeiXinMsg wx = new WeiXinMsg();
            //wx.FromUserName = requestXML.FromUserName;
            //wx.ToUserName = requestXML.ToUserName;
            //wx.MsgType = requestXML.MsgType;
            //wx.Msg = requestXML.Content;
            //wx.Creatime = requestXML.CreateTime;
            //wx.Location_X = requestXML.Location_X;
            //wx.Location_Y = requestXML.Location_Y;
            //wx.Label = requestXML.Label;
            //wx.Scale = requestXML.Scale;
            //wx.PicUrl = requestXML.PicUrl;
            //wx.reply = _xml;
            //wx.pid = _pid;

            try
            {
                //wx.Add();
            }
            catch (Exception ex)
            {
                //wx_logs.MyInsert(ex.Message);  
                //ex.message;  
            }
        }



        /// <summary>
        ///   普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                             <ToUserName><![CDATA[{0}]]></ToUserName>
                             <FromUserName><![CDATA[{1}]]></FromUserName>
                             <CreateTime>{2}</CreateTime>
                             <MsgType><![CDATA[text]]></MsgType>
                             <Content><![CDATA[{3}]]></Content>
                             </xml>";
            }
        }

        /// <summary>
        ///   图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                             <ToUserName><![CDATA[{0}]]></ToUserName>
                             <FromUserName><![CDATA[{1}]]></FromUserName>
                             <CreateTime>{2}</CreateTime>
                             <MsgType><![CDATA[news]]></MsgType>
                             <ArticleCount>{3}</ArticleCount>
                             <Articles>{4}</Articles>
                             </xml> ";
            }
        }

        /// <summary>
        ///   图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                             <Title><![CDATA[{0}]]></Title>
                             <Description><![CDATA[{1}]]></Description>
                             <PicUrl><![CDATA[{2}]]></PicUrl>
                             <Url><![CDATA[{3}]]></Url>
                             </item>";
            }
        }
    }

    #region 微信请求类 RequestXML

    /// <summary>
    ///     微信请求类
    /// </summary>
    public class RequestXML
    {
        private string content = "";
        private string createTime = "";
        private string fromUserName = "";
        private string label = "";
        private string location_X = "";
        private string location_Y = "";
        private string msgType = "";
        private string picUrl = "";
        private string scale = "";
        private string toUserName = "";

        /// <summary>
        ///     消息接收方微信号，一般为公众平台账号微信号
        /// </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }

        /// <summary>
        ///     消息发送方微信号
        /// </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }

        /// <summary>
        ///     创建时间
        /// </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        ///     信息类型 地理位置:location,文本消息:text,消息类型:image
        /// </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        /// <summary>
        ///     信息内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        ///     地理位置纬度
        /// </summary>
        public string Location_X
        {
            get { return location_X; }
            set { location_X = value; }
        }

        /// <summary>
        ///     地理位置经度
        /// </summary>
        public string Location_Y
        {
            get { return location_Y; }
            set { location_Y = value; }
        }

        /// <summary>
        ///     地图缩放大小
        /// </summary>
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        ///     地理位置信息
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        /// <summary>
        ///     图片链接，开发者可以用HTTP GET获取
        /// </summary>
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
    }

    #endregion

    #region 微信 AccessToken 凭证实体

    /// <summary>
    ///     微信 AccessToken 凭证实体
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        ///     获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        ///     凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }

    #endregion
}
