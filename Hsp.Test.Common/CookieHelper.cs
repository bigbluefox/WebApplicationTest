using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hsp.Test.Common
{
    /// <summary>
    /// Cookie 帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>  
        /// 清除指定Cookie  
        /// </summary>  
        /// <param name="cookiename">cookiename</param>  
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>  
        /// 获取指定Cookie值  
        /// </summary>  
        /// <param name="cookiename">cookiename</param>  
        /// <returns></returns>  
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }

        /// <summary>  
        /// 添加一个Cookie（24小时过期）  
        /// </summary>  
        /// <param name="cookiename"></param>  
        /// <param name="cookievalue"></param>  
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }

        /// <summary>  
        /// 添加一个Cookie  
        /// </summary>  
        /// <param name="cookiename">cookie名</param>  
        /// <param name="cookievalue">cookie值</param>  
        /// <param name="expires">过期时间 DateTime</param>  
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            var cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加微信凭证Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param> 
        /// <param name="cookievalue">cookie值</param>
        public static void SetWeChatTokenCookie(string cookiename, string cookievalue)
        {
            // access_token的有效期为7200秒，即2小时
            DateTime expires = DateTime.Now.Add(new TimeSpan(0, 2, 0, 0));

            var cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };

            //var cookie = new HttpCookie(cookiename);
            //DateTime dt = DateTime.Now;//定义时间对象 
            //var ts = new TimeSpan(0, 0, 0, 0, 7200);//cookie有效作用时间，具体查msdn
            //cookie.Expires = dt.Add(ts);//添加作用时间 
            //cookie.Value = cookievalue;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }





    }



    #region 单例模式

    /// <summary>
    /// 单例模式
    /// 单例模式就是保证在整个应用程序的生命周期中，在任何时刻，
    /// 被指定的类只有一个实例，并为客户程序提供一个获取该实例的全局访问点。
    /// </summary>
    public abstract class TokenHashTable //<-----------这个类名可以自定义
    {
        //private static readonly object lockObject = new object(); //<-----定义一个多线程对像
        private static Hashtable instance; //<-------首先，我们来定义一个静态的，本类的对象instancec

        //private WeatherHashTable()
        //{
        //} //<----这里很重要，我们要私有化构造函数，以阻止类的实例化。

        public static Hashtable Instance() //<----好啦，我们来定义一个方法返回的是本类对像的实例。
        {
            //lock (lockObject) //<----线程同步
            //{
            if (instance == null) //<-----判断实例在内存中是否存在
            {
                instance = new Hashtable(); //<-----如果不存在，我们构造一个新的实例。
            }
            //}
            return instance; //<------返回本类的实例。
        }

        //注：许多编程期间为了不实例化，很多时候我们会把方法写为静态的。在单键模式中，不能是静态的。
        //因为静态的我们便无法从唯一的一个实例中进行调用。如下Grade方法
        public string Grade() //<-----这个是类里面的一个方法返回字符串，只是用来做调用的。修饰符
        {
            Instance().Add("ndfd", "");
            const string strcn = "单键模式，保持本对像在运行中永远只有一个实例。";
            return strcn;
        }
    }

    #endregion


}
