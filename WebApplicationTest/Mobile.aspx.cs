using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class Mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var strUserCode = Request.Headers["X-Test"]; // one, two

            if (!string.IsNullOrEmpty(strUserCode))
            {
                var filePath = GetFilePath("Request_Headers_");
                File.WriteAllText(filePath, strUserCode);
            }

            //Response.Headers

        }

        /// <summary>
        ///     文件路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal string GetFilePath(string str)
        {
            var strSavePath = Server.MapPath(@".");
            if (!strSavePath.EndsWith("/")) strSavePath += "/";
            return strSavePath + str + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
        }
    }
}