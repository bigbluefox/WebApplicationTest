using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.PDF
{
    public partial class viewer : System.Web.UI.Page
    {
        /// <summary>
        /// PDF文件地址
        /// </summary>
        public string PdfUrl { get; set; }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PdfUrl = @"/Files/《兵器知识》2011年9A期.pdf";
            PdfUrl = @"20kv并联电抗器技术规范.pdf";
            PdfUrl = @"/Files/G395办安环〔2014〕104号 转发国家能源局关于印发《防止电力生产事故的二十五项重点要求》的通知.pdf";
            PdfUrl = Server.UrlEncode(PdfUrl);
            PdfUrl = PdfUrl.Replace("+", "%20");
            PdfUrl = PdfUrl.Replace("%2f", "/"); 

            //E:\My Documents\Visual Studio 2012\Projects\WebApplicationTest\WebApplicationTest\Files\《兵器知识》2011年9A期.pdf

            Page.ClientScript.RegisterStartupScript(GetType(), "MsgBox",
                                        "<script type=\"text/javascript\">DEFAULT_URL=\"/Files/《兵器知识》2011年9A期.pdf\";</script>");
        }
    }
}