using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplicationTest.Crud
{
    public partial class AutoLogin : System.Web.UI.Page
    {
        //readonly WebBrowser webBrowser1 = new WebBrowser();
        static System.Windows.Forms.WebBrowser wb;


        protected void Page_Load(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("172.22.48.8/Account/LogOn");

            //wb = new System.Windows.Forms.WebBrowser();
            //wb.DocumentCompleted += wb_DocumentCompleted;
            //wb.Navigate("http://172.22.48.8/Account/LogOn");

            //Console.Out.Write(wb.DocumentText);
            //while (wb.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
            //{
            //    System.Windows.Forms.Application.DoEvents(); //避免假死，若去掉则可能无法触发 DocumentCompleted 事件。
            //}

            var thread = new Thread(FatchDataToResult);
            //Apartment 是处理序当中让物件共享相同执行绪存取需求的逻辑容器。 同一 Apartment 内的所有物件都能收到 Apartment 内任何执行绪所发出的呼叫。 
            //.NET Framework 并不使用 Apartment；Managed 物件必须自行以安全执行绪 (Thread-Safe) 的方式运用一切共享资源。
            //因为 COM 类别使用 Apartment，所以 Common Language Runtime 在 COM Interop 的状况下呼叫出 COM 物件时必须建立 Apartment 并且加以初始化。 
            //Managed 执行绪可以建立并且输入只容许一个执行绪的单一执行绪 Apartment (STA)，或者含有一个以上执行绪的多执行绪 Apartment (MTA)。 
            //只要把执行绪的 ApartmentState 属性设定为其中一个 ApartmentState 列举型别 (Enumeration)，即可控制所建立的 Apartment 属于哪种型别。 
            //因为特定执行绪一次只能初始化一个 COM Apartment，所以第一次呼叫 Unmanaged 程式码之后就无法再变更 Apartment 型别。
            //From : http://msdn.microsoft.com/zh-tw/library/system.threading.apartmentstate.aspx
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();


        }

        /// <summary>
        /// Call _WebBrowder 抓取资料
        /// For thread Call
        /// </summary>
        private void FatchDataToResult()
        {
            wb = new WebBrowser();

            wb.DocumentCompleted += _WebBrowder_DocumentCompleted;
            wb.Navigate("http://172.22.48.8/Account/LogOn");

            //处理目前在讯息伫列中的所有 Windows 讯息。
            //如果在程式码中呼叫 DoEvents，您的应用程式就可以处理其他事件。例如，如果您的表单将资料加入 
            //ListBox 并将 DoEvents 加入程式码中，则当另一个视窗拖到您的表单上时，该表单将重新绘製。
            //如果您从程式码移除 DoEvents，您的表单将不会重新绘製，直到按钮按一下的事件处理常式执行完毕。
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                //Application.DoEvents();
            }

            wb.Dispose();

        }

        //结束后回填
        public static void _WebBrowder_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //_Result = (sender as WebBrowser).Document.Body.InnerHtml;

        }


        protected void wb_DocumentCompleted(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 设置登陆的用户名和密码
        /// </summary>
        internal bool SetLoginField(WebBrowser wbBrowser)
        {
            bool success = false;
            if (null != wbBrowser.Document)
            {
                // 获取Document
                HtmlDocument document = wbBrowser.Document.Window.Document;
                // 获取元素集合
                HtmlElementCollection all = document.All;
                //// 根据名称获得“登录”文本框
                //HtmlElementCollection loginName = all.GetElementsByName(GM.LoginOn.LoginNameText);
                //// 根据名称获得 “密码”文本框
                //HtmlElementCollection loginPwd = all.GetElementsByName(GM.LoginOn.LoginPwdText);
                //#region 设置具体值
                //if (loginName.Count > 0 && loginPwd.Count > 0)
                //{
                //    loginName[0].InnerText = GM.LoginOn.LoginNameValue;
                //    loginPwd[0].InnerText = GM.LoginOn.LoginPwdValue;
                //    success = true;
                //}
                //#endregion
            }
            return success;

        }

        /// <summary>
        /// 自动登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAutoLogin_Click(object sender, EventArgs e)
        {

            var userId = this.UserID.Text;
            var pwd = this.Password.Text;

            //webBrowser1.Document.All["userName"].InnerText = userId;
            //webBrowser1.Document.All["passWord"].InnerText = pwd;

            //HtmlElement tem = null;
            //var tem111 = webBrowser1.Document.GetElementsByTagName("input");
            //for (int i = 0; i < tem111.Count; i++)
            //{
            //    HtmlElement aaaa = tem111[i];
            //    if (aaaa.GetAttribute("type") == "submit")
            //    {
            //        tem = aaaa;
            //    }
            //}

            //if (tem != null) tem.InvokeMember("click");
            //Thread.Sleep(1000);
            //webBrowser1.Navigate("www.tiantianit.com");//登陆成功后转到写blog

            System.Threading.Thread t = new System.Threading.Thread(new ThreadStart(() =>
            {
                wb = new System.Windows.Forms.WebBrowser();
                wb.DocumentCompleted += wb_DocumentCompleted;
                wb.Navigate("http://10.5.10.143:9091/");
                while (wb.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                {
                    System.Windows.Forms.Application.DoEvents(); //避免假死，若去掉则可能无法触发 DocumentCompleted 事件。
                }
            })
            );
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            //if (webBrowser1.Url.ToString() == "www.tiantianit.com")
            //{
            //    webBrowser1.Document.All["username"].InnerText = "tiantianit.com";
            //    webBrowser1.Document.All["password"].InnerText = "tiantianit.com";
            //    //webBrowser1.
            //    HtmlElement tem = null;
            //    var tem111 = webBrowser1.Document.GetElementsByTagName("input");
            //    for (int i = 0; i < tem111.Count; i++)
            //    {
            //        HtmlElement aaaa = tem111[i];
            //        if (aaaa.GetAttribute("type") == "button")
            //        {
            //            tem = aaaa;
            //        }
            //    }

            //    tem.InvokeMember("click");
            //    Thread.Sleep(1000);
            //    webBrowser1.Navigate("www.tiantianit.com");//登陆成功后转到写blog
            //    //this.Show();
            //    //bool isLogIn = false;
            //    //if (webBrowser1.Url.ToString() == "www.tiantianit.com")
            //    //{
            //    //    System.Windows.Forms.MessageBox.Show("用户名或密码错误!");
            //    //    return;
            //    //}
            //    //else
            //    //{
            //    //    //isSetForm = false;
            //    //    //mylogin.Close();
            //    //    webBrowser1.Navigate("www.tiantianit.com");
            //    //    this.Show();
            //    //}
            //}
            //if (webBrowser1.Url.ToString() == "www.tiantianit.com")
            //{
            //    webBrowser1.Document.All["txtTitle"].InnerText = "hello 我的第一篇博客";
            //    webBrowser1.Document.All["editor"].InnerText = "hello 我的第一篇博客";
            //    //var tem111 = webBrowser1.Document.GetElementsByTagName("body");
            //    //HtmlElement bbbb = null;
            //    //for (int i = 0; i < tem111.Count; i++)
            //    //{
            //    //    HtmlElement aaaa = tem111[i];
            //    //    if (aaaa.GetAttribute("class") == "editMode")
            //    //    {
            //    //        bbbb = aaaa;
            //    //    }
            //    //}
            //    //bbbb.InnerHtml = "hello 我的第一篇博客";
            //    webBrowser1.Document.GetElementById("selType").SetAttribute("innerHTML", "转载");
            //    webBrowser1.Document.GetElementById("txtTag2").InnerText = "asp.net";//标题
            //    webBrowser1.Document.GetElementById("txtTag").InnerText = "asp.net";//内容
            //    HtmlElement authortb = webBrowser1.Document.All["radChl2"];
            //    authortb.SetAttribute("checked", "checked");

            //}
        }
    }
}