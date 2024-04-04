using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

namespace WebApplicationTest
{
    public partial class md5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var filePath = @"e:\职业健康安全管理体系要求.pdf";

            //byte[] data = new byte[0]; ;
            //if (File.Exists(filePath))
            //{
            //    FileStream fs = new FileStream(filePath, FileMode.Open);

            //    //获取文件大小
            //    long size = fs.Length;

            //    data = new byte[size];

            //    //将文件读到byte数组中
            //    fs.Read(data, 0, data.Length);

            //    fs.Close();
            //}

            //var ms = new MemoryStream(data);
            //var hashMd5 = HashHelper.ComputeMD5(ms);

            var hashMd5 = HashHelper.ComputeMD5(filePath);

            this.TextBox1.Text = hashMd5;
        }
    }
}