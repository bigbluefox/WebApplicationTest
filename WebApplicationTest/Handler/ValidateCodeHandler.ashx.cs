using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Hsp.Test.Common;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// 生成数字图片验证码处理
    /// </summary>
    public class ValidateCodeHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //ValidateCode validateCode = new ValidateCode();

            //string checkCode = validateCode.CreateValidateCode(6);
            //byte[] ms = validateCode.CreateValidateGraphic(checkCode);

            ////清除缓冲区流中的所有输出 
            //context.Response.ClearContent();
            ////输出流的Http Mime类型设置为"image/Png" 
            //context.Response.ContentType = "image/Png";
            ////输出图片的二进制流 
            //context.Response.BinaryWrite(ms);


            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点  
            Color[] color =
            {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown,
                Color.DarkBlue
            };
            //字体列表，用于验证码  
            string[] font = {"Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact"};
            //验证码的字符集，去掉了一些容易混淆的字符  
            char[] character =
            {
                '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L',
                'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y'
            };
            Random rnd = new Random();
            //生成验证码字符串  
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            Bitmap bmp = new Bitmap(100, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线  
            for (int i = 0; i < 10; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串  
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float) i*20 + 8, (float) 8);
            }
            //画噪点  
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            //清除该页输出缓存，设置该页无缓存  
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AppendHeader("Pragma", "No-Cache");

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出  
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Png";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源  
                bmp.Dispose();
                g.Dispose();
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}