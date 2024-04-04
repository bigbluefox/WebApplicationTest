using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    ///     验证码生成方法
    /// </summary>
    public class CheckCodeMethod
    {
        /// <summary>
        ///     产生随机数
        /// </summary>
        /// <returns></returns>
        internal static string GenerateCheckCode()
        {
            var strCode = string.Empty;
            //随机种子 
            var random = new Random();
            for (var i = 0; i < 4; i++) //验证码长度为4 
            {
                //随机整数 
                var number = random.Next();
                //字符从0-9，A-Z中产生，对应的ASCII码为48-57，65-90 
                number = number % 36;
                if (number < 10)
                {
                    number += 48;
                }
                else
                {
                    number += 55;
                }
                strCode += ((char)number).ToString();
            }
            //将字符串保存在Cookies 
            //Response.Cookies.Add(new HttpCookie("CheckCode", strCode));
            return strCode;
        }

        /// <summary>
        ///     生成图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public static MemoryStream CreateCheckCodeImageMemoryStream(out string checkCode)
        {
            checkCode = GenerateCheckCode();
            return CreateCheckCodeImageMemoryStream(checkCode);
        }

        /// <summary>
        ///     生成图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        internal static MemoryStream CreateCheckCodeImageMemoryStream(string checkCode)
        {
            //若检验码为空，则直接返回 
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return null;
            //根据验证码长度确定输出图片的长度 
            double iLen = checkCode.Length * 15;
            var image = new Bitmap((int)Math.Ceiling(iLen), 20);
            //创建Graphics对象 
            var graph = Graphics.FromImage(image);

            //生成随机数种子 
            var random = new Random();
            //清除图片背景颜色 
            graph.Clear(Color.CornflowerBlue);
            //画背景图片的噪音线10条 
            for (var i = 0; i < 10; i++)
            {
                //噪音线起点坐标(x1,y1),终点坐标(x2,y2) 
                var x1 = random.Next(image.Width);
                var x2 = random.Next(image.Width);
                var y1 = random.Next(image.Height);
                var y2 = random.Next(image.Height);
                //用银色线画出噪音线 
                graph.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //输出图片中校验码的字体:12号Arial,粗斜体 
            var font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
            //线性渐变画刷 
            var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Width), Color.White, Color.Yellow,
                1.2f, true);
            graph.DrawString(checkCode, font, brush, 2, 2);
            //画图片的前景噪点50个 
            for (var i = 0; i < 50; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //画图片段边框线 
            graph.DrawRectangle(new Pen(Color.CornflowerBlue), 0, 0, image.Width - 1, image.Height - 1);
            //创建内存流用于输出图片 
            var ms = new MemoryStream();
            //图片格式指定为png 
            image.Save(ms, ImageFormat.Png);
            //释放Bitmap和Graphics对象 
            graph.Dispose();
            image.Dispose();
            return ms;

            //清除缓冲区流中的所有输出 
            //Response.ClearContent(); 
            //输出流的Http Mime类型设置为"image/Png" 
            //Response.ContentType="image/Png"; 
            //输出图片的二进制流 
            //Response.BinaryWrite(ms.ToArray()); 

            //释放Bitmap和Graphics对象 
            //g.Dispose();
            //image.Dispose();
        }
    }
}
