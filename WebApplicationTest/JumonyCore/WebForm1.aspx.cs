using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ivony.Html;
using Ivony.Html.Parser;

namespace WebApplicationTest.JumonyCore
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //https://github.com/Ivony/Jumony
        //https://github.com/BabyAlice/JumonyCoreDemo
        //http://www.cnblogs.com/Ivony/p/3447536.html

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
                        //从指定的地址加载html文档
            IHtmlDocument source = new JumonyParser().LoadDocument("http://www.cnblogs.com/cate/csharp");
            var aLinks = source.Find(".titlelnk");//按照css选择器搜索符合要求的元素
            var lbl = "";
            foreach (var aLink in aLinks)
            {
                //<a>Hello</a> 获取hello 
                //Console.WriteLine(aLink.InnerText());
                lbl += aLink.InnerText() + "<br/>";
 
                //获取 a标签和它的父节点 <h3><a>Hello</a></h3>
                //Console.WriteLine(aLink.Parent());
                lbl += aLink.Parent() + "<br/>";
 
                //<a>Hello</a> 获取hello 
                //Console.WriteLine(aLink.InnerHtml());
                lbl += aLink.InnerHtml() + "<br/>";
 
                //获取指定属性名的值  value和AttributeValue都可以获取,但区别是value当 当前属性对象为null时不会抛出异常
                //Console.WriteLine(aLink.Attribute("href").Value());
                //Console.WriteLine(aLink.Attribute("href").AttributeValue);
 
                lbl += aLink.Attribute("href").Value() + "<br/>";
                lbl += aLink.Attribute("href").AttributeValue + "<br/>";
            }

            Label1.Text = lbl;
        

        }
    }
}