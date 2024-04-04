using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Regular
{
    public partial class DecodeMoneyCn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 中文数字转阿拉伯数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            var txtChineseNo = this.txtChinese.Text;
            if (!string.IsNullOrEmpty(txtChineseNo))
            {
                var rst = ProcessDecodeMoneyCn(txtChineseNo);
                txtNumber.Text = rst.ToString();
            }

        }


        /// <summary>
        /// 中文数字转阿拉伯数字
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        public double ProcessDecodeMoneyCn(string aText)
        {
            aText = aText.Replace("亿亿", "兆");
            aText = aText.Replace("万万", "亿");
            aText = aText.Replace("点", "元");
            aText = aText.Replace("块", "元");
            aText = aText.Replace("毛", "角");
            double vResult = 0;
            double vNumber = 0; // 当前数字
            double vTemp = 0;
            int vDecimal = 0; // 是否出现小数点
            foreach (char vChar in aText)
            {
                int i = "零一二三四五六七八九".IndexOf(vChar);
                if (i < 0) i = "洞幺两三四五六拐八勾".IndexOf(vChar);
                if (i < 0) i = "零壹贰叁肆伍陆柒捌玖".IndexOf(vChar);
                if (i > 0)
                {
                    vNumber = i;
                    if (vDecimal > 0)
                    {
                        vResult += vNumber * Math.Pow(10, -vDecimal);
                        vDecimal++;
                        vNumber = 0;
                    }
                }
                else
                {
                    i = "元十百千万亿".IndexOf(vChar);
                    if (i < 0) i = "整拾佰仟万亿兆".IndexOf(vChar);
                    if (i == 5) i = 8;
                    if (i == 6) i = 12;
                    if (i > 0)
                    {
                        if (vNumber == 0.0) vNumber = 1.0;

                        if (i >= 4)
                        {
                            vTemp += vNumber;
                            if (vTemp == 0) vTemp = 1;
                            vResult += vTemp * Math.Pow(10, i);
                            vTemp = 0;
                        }
                        else vTemp += vNumber * Math.Pow(10, i);
                    }
                    else
                    {
                        i = "元角分".IndexOf(vChar);
                        if (i > 0)
                        {
                            vTemp += vNumber;
                            vResult += vTemp * Math.Pow(10, -i);
                            vTemp = 0;
                        }
                        else if (i == 0)
                        {
                            vTemp += vNumber;
                            vResult += vTemp;
                            vDecimal = 1;
                            vTemp = 0;
                        }
                    }
                    vNumber = 0;
                }
            }
            return vResult + vTemp + vNumber;
        }
    }
}