using System.Configuration;

namespace Hsp.Test.Model
{
    internal class WeChat
    {
    }

    /// <summary>
    ///     微信凭证获取参数实体
    /// </summary>
    public class WeChatTokenPara
    {
        /// <summary>
        ///     企业应用ID
        /// </summary>
        private string _agentID = ConfigurationManager.AppSettings["WeChat_AgentID"] ?? "";

        /// <summary>
        ///     微信访问凭证Cookie名称
        /// </summary>
        private string _cookieName = ConfigurationManager.AppSettings["WeChat_CookieName"] ?? "WeChatToken";

        /// <summary>
        ///     企业号的标识
        /// </summary>
        private string _corpId = ConfigurationManager.AppSettings["WeChat_CorpId"] ?? "";

        /// <summary>
        ///     EncodingAESKey
        /// </summary>
        private string _encodingAESKey = ConfigurationManager.AppSettings["WeChat_Secret"] ?? "";

        /// <summary>
        ///     管理组凭证密钥
        /// </summary>
        private string _secret = ConfigurationManager.AppSettings["WeChat_Secret"] ?? "";

        /// <summary>
        ///     Token
        /// </summary>
        private string _token = ConfigurationManager.AppSettings["WeChat_Token"] ?? "";

        /// <summary>
        ///     微信访问凭证Cookie名称
        /// </summary>
        public string CookieName
        {
            get { return _cookieName; }
            set { _cookieName = value; }
        }

        /// <summary>
        ///     企业号的标识
        /// </summary>
        public string CorpId
        {
            get { return _corpId; }
            set { _corpId = value; }
        }

        /// <summary>
        ///     管理组凭证密钥
        /// </summary>
        public string Secret
        {
            get { return _secret; }
            set { _secret = value; }
        }

        /// <summary>
        ///     接口配置信息：Token
        /// </summary>
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        /// <summary>
        ///     EncodingAESKey
        /// </summary>
        public string EncodingAESKey
        {
            get { return _encodingAESKey; }
            set { _encodingAESKey = value; }
        }

        /// <summary>
        ///     企业应用ID
        /// </summary>
        public string AgentID
        {
            get { return _agentID; }
            set { _agentID = value; }
        }
    }


    /// <summary>
    ///     回调参数实体
    /// </summary>
    public class CallbackUrlPara
    {
        //string sReqMsgSig = HttpContext.Current.Request.QueryString["msg_signature"];
        //string sReqTimeStamp = HttpContext.Current.Request.QueryString["timestamp"];
        //string sReqNonce = HttpContext.Current.Request.QueryString["nonce"];

        //sMsgSignature  是  从回调URL中获取的msg_signature参数  
        //sTimeStamp  是  从回调URL中获取的timestamp参数  
        //sNonce  是  从回调URL中获取的nonce参数  

        /// <summary>
        ///     加密签名：从回调URL中获取的msg_signature参数
        /// </summary>
        public string MsgSignature { get; set; }

        /// <summary>
        ///     时间戳：从回调URL中获取的timestamp参数
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        ///     随机数：从回调URL中获取的nonce参数
        /// </summary>
        public string Nonce { get; set; }
    }

    /// <summary>
    ///     消息类型
    /// </summary>
    public class MsgType
    {
        public const string text = "text";
        public const string image = "image";
        public const string voice = "voice";
        public const string video = "video";
        public const string location = "location";
        public const string link = "link";
        public const string events = "event";
    }

    #region 微信事件类型

    /// <summary>
    ///     微信事件类型
    /// </summary>
    public class EventType
    {
        //subscribe/unsubscribe/SCAN/LOCATION/CLICK/VIEW

        public const string subscribe = "subscribe";
        public const string unsubscribe = "unsubscribe";
        public const string scan = "SCAN";
        public const string location = "LOCATION";
        public const string click = "CLICK";
        public const string view = "VIEW";
    }

    #endregion

    /// <summary>
    ///     微信消息类型
    ///     图文消息为mpnews，文本消息为text，语音为voice，音乐为music，图片为image，视频为video
    /// </summary>
    public class MessageType
    {
        /// <summary>
        ///     文本消息
        /// </summary>
        public const string Text = "text";

        /// <summary>
        ///     语音
        /// </summary>
        public const string Voice = "voice";

        /// <summary>
        ///     音乐
        /// </summary>
        public const string Music = "music";

        /// <summary>
        ///     图片
        /// </summary>
        public const string Image = "image";

        /// <summary>
        ///     视频
        /// </summary>
        public const string Video = "video";

        /// <summary>
        ///     图文消息
        /// </summary>
        public const string MpNews = "mpnews";
    }


    public class MsgXml
    {
        /// <summary>
        ///     应用ID
        /// </summary>
        public string AgentID { get; set; }

        /// <summary>
        ///     本公众账号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        ///     用户账号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        ///     发送时间戳
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        ///     发送的文本内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     消息的类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        ///     消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        ///     图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        ///     图片、语音、视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        ///     语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///     视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string ThumbMediaId { get; set; }

        #region Event

        /// <summary>
        ///     事件类型 subscribe/unsubscribe/SCAN/LOCATION/CLICK/VIEW
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        ///     事件KEY值，qrscene_为前缀，后面为二维码的参数值
        ///     事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        ///     二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        #endregion

        #region Location

        /// <summary>
        ///     地理位置维度
        /// </summary>
        public string Location_X { get; set; }

        /// <summary>
        ///     地理位置经度
        /// </summary>
        public string Location_Y { get; set; }

        /// <summary>
        ///     地图缩放大小
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        ///     地理位置信息
        /// </summary>
        public string Label { get; set; }

        #endregion

        #region Link

        /// <summary>
        ///     消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     消息链接
        /// </summary>
        public string Url { get; set; }

        #endregion
    }


    public class WeChatData
    {
        public string[] touser { get; set; }

        public string msgtype { get; set; }

        /// <summary>
        ///     文本消息
        /// </summary>
        public TextContent text { get; set; }
    }


    public class TextContent
    {
        //public string media_id { get; set; }
        //public string title { get; set; }
        //public string discription { get; set; }
        public string content { get; set; }
    }

    /// <summary>
    ///     密文消息实体类
    /// </summary>
    public class EncryptMsgXml
    {
        /// <summary>
        ///     本企业账号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        ///     密文
        /// </summary>
        public string Encrypt { get; set; }

        /// <summary>
        ///     应用ID
        /// </summary>
        public string AgentID { get; set; }
    }

    /// <summary>
    ///     微信发送返回消息实体
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        ///     错误代码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        ///     错误消息
        /// </summary>
        public string errmsg { get; set; }
    }
}