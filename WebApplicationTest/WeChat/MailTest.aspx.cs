using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.WeChat
{
    public partial class MailTest : System.Web.UI.Page
    {
        /// <summary>
        /// 设置发送邮件服务器
        /// </summary>
        internal string SmtpHost = ConfigurationSettings.AppSettings["SmtpHost"]; 
        
        /// <summary>
        /// 设置发送者的邮件地址
        /// </summary>
        internal string From = ConfigurationSettings.AppSettings["From"]; 


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            this.txtSMTP.Text = SmtpHost;
            this.txtFrom.Text = From;
        }

        protected void btnMailSendTest_Click(object sender, EventArgs e)
        {
            //SendMailLocalhost();
            //return;

            //var SmtpHost = ConfigurationSettings.AppSettings["SmtpHost"]; // 设置发送邮件服务器
            ////SmtpHost = "smtp.xinaogroup.com";
            //var From = ConfigurationSettings.AppSettings["From"]; // 设置发送者的邮件地址
            //From = "info_tech@enn.cn"; // 设置发送者的邮件地址
            var to = "XIAOYOUFENG@ENN.CN"; // 设置收件人的邮件地址
            to = "lihaiyu@foxmail.com";
            to = "keyuliang@enn.cn";
            to = txtSendAddr.Text;
            if (string.IsNullOrEmpty(to))
            {
                this.Label1.Text = "请填写邮件发送地址！";
                return;
            }
            var cc = "XIAOYOUFENG@enn.cn";
            //cc = "tli@sinocal.com.cn";
            cc = txtCCAddr.Text;
            var subject = "工作流消息提醒"; // 设置邮件主题
            var password = ConfigurationSettings.AppSettings["Password"]; // 密码
            //password = "G-AGZALE";

            var msg = "(Tli)邮件发送测试！";
            msg += DateTime.Now;

            var message = new MailMessage();
            message.From = new MailAddress(From); // 必须是提供smtp服务的邮件服务器 
            message.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc)) message.CC.Add(new MailAddress(cc));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = msg;
            message.Priority = MailPriority.High;

            var client = new SmtpClient(SmtpHost);
            client.Port = 25;

            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.ServicePoint.MaxIdleTime = 0;
            client.ServicePoint.ConnectionLimit = 1;

            client.Credentials = new NetworkCredential(From, password); // 这里是申请的邮箱和密码 
            //client.EnableSsl = true; // 必须经过ssl加密

            ////创建SMTP1
            //SmtpClient smtpClient = new SmtpClient(Server.Address);
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.ServicePoint.MaxIdleTime = 0;
            //smtpClient.ServicePoint.ConnectionLimit = 1;

            ////创建SMTP 2是否启用SSL和指定网络凭据
            //smtpClient = new SmtpClient(smtpSetting.ServerName, smtpSetting.Port);
            //smtpClient.Credentials = new NetworkCredential(smtpSetting.UserName, smtpSetting.Password);
            //smtpClient.EnableSsl = true;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.ServicePoint.MaxIdleTime = 0;
            //smtpClient.ServicePoint.ConnectionLimit = 1;

            try
            {
                client.Send(message);
                this.Label1.Text = "邮件发送成功！";
            }
            catch (Exception ex)
            {
                this.Label1.Text = ex.Message;
                //throw;
            }
        }

        public void SendMailLocalhost()
        {
            var msg = new MailMessage();
            msg.To.Add("lihaiyu@sohu.com");
            //msg.To.Add("b@b.com");
            /* msg.To.Add("b@b.com");
            * msg.To.Add("b@b.com");
            * msg.To.Add("b@b.com");可以发送给多人
            */

            msg.CC.Add("tli@sinocal.com.cn");
            /*
            * msg.CC.Add("c@c.com");
            * msg.CC.Add("c@c.com");可以抄送给多人
            */

            msg.From = new MailAddress("info_tech@ENN.CN", "标准体系邮件测试", Encoding.UTF8);
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.Subject = "这是测试邮件"; //邮件标题
            msg.SubjectEncoding = Encoding.UTF8; //邮件标题编码
            msg.Body = "邮件内容"; //邮件内容
            msg.BodyEncoding = Encoding.UTF8; //邮件内容编码
            msg.IsBodyHtml = false; //是否是HTML邮件
            msg.Priority = MailPriority.High; //邮件优先级 <br />

            var client = new SmtpClient();
            client.Host = "localhost";

            object userState = msg;

            try
            {
                client.SendAsync(msg, userState);
                //简单一点儿可以client.Send(msg);
                //MessageBox.Show("发送成功");
                Label1.Text = "发送成功";
            }
            catch (SmtpException ex)
            {
                //MessageBox.Show(ex.Message, "发送邮件出错");
                Label1.Text = ex.Message;
            }
        }


        /**
         * 发送邮件
         * 
         * @param emailaddress 邮箱地址
         * @param msg 邮件正文
         * @param subject 邮件标题
         * @return null: 发送成功<br>
         *         not null: 发送失败
         */
        //public String sendMail(String emailaddress, String msg, String subject)
        //{
        //    String res = null;
        //    HtmlEmail email = new HtmlEmail();
        //    email.setHostName(mailSmtpServer);// stmp服务器
        //    try
        //    {
        //        email.setFrom(senderId + mailDomain, senderName);// 发件人信息
        //        email.setAuthentication(senderId, senderPassword);// 发件人账号密码
        //        email.setSubject(subject);// 设置邮件的主题
        //        email.setCharset("UTF-8");
        //        email.setHtmlMsg(msg);// 邮件正文消息
        //        if (emailaddress.indexOf("@") > 0)
        //        {
        //            String code = emailaddress.substring(0,
        //                    emailaddress.indexOf("@"));
        //            email.addTo(code + mailDomain, code);// 设置收件人邮箱
        //            email.send();
        //        }
        //        else
        //        {
        //            res = "邮件地址错误";
        //        }
        //        return res;
        //    }
        //    catch (EmailException e)
        //    {
        //        Object[] args = { emailaddress, msg, subject };
        //        LOG.error(ennMessages.getMessage("error.sendMail.1", args,
        //                Locale.CHINA), e);
        //        return "邮件发送失败:[" + emailaddress + "]";
        //    }
        //}
    }
}