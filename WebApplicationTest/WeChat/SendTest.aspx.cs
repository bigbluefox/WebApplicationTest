using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Hsp.Test.Model;

namespace WebApplicationTest.WeChat
{
    public partial class SendTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 微信发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            // 在此发送微信信息
            var rst = "";
            var responeJsonStr = "";

            // 获取微信Token
            WeChatTokenPara tokenPara = new WeChatTokenPara();
            string accessToken = WeChatHelper.GetToken(tokenPara.CorpId, tokenPara.Secret, tokenPara.CookieName);

            if (accessToken == null) return;

            #region 发送消息

            //{
            //   "touser": "UserID1|UserID2|UserID3",
            //   "toparty": " PartyID1 | PartyID2 ",
            //   "totag": " TagID1 | TagID2 ",
            //   "msgtype": "text",
            //   "agentid": 1,
            //   "text": {
            //       "content": "Holiday Request For Pony(http://xxxxx)"
            //   },
            //   "safe":"0"
            //}

            var sUserId = "13911517746";
            sUserId = "lihaiyu";
            var sMessage = "微信发送测试";

            //调用：
            responeJsonStr = "{";
            responeJsonStr += "\"touser\": \"" + sUserId + "\",";
            responeJsonStr += "\"toparty\": \"@all\",";
            responeJsonStr += "\"totag\": \"@all\",";
            responeJsonStr += "\"msgtype\": \"text\",";
            responeJsonStr += "\"agentid\": \"0\",";
            responeJsonStr += "\"text\": {";
            responeJsonStr += "  \"content\": \"" + sMessage + "\"";
            responeJsonStr += "},";
            responeJsonStr += "\"safe\":\"0\"";
            responeJsonStr += "}";

            // 发送消息
            rst = WeChatHelper.SendMessage(accessToken, responeJsonStr, Encoding.UTF8);
            var errMessage = JsonHelper.JsonDeserialize<ResultMessage>(rst);

            //{"errcode":41001,"errmsg":"access_token missing"}
            //{"errcode":0,"errmsg":"ok"}

            //if (errMessage.errcode != 0)
            //{
            //    Logger.error("发送短信/微信job发生异常:", new Exception(errMessage.errmsg));
            //}

            // 82001All touser & toparty & totag invalid 

            lblMsg.Text = errMessage.errcode + errMessage.errmsg;

            #endregion
        }
    }
}