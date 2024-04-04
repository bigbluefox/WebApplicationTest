using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.AI
{
    public class IdCard_Result
    {
        public IdCard_Info words_result { get; set; }



    }


    public class IdCard_Info
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public Ocr_Result 姓名 { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public Ocr_Result 民族 { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public Ocr_Result 住址 { get; set; }


        /// <summary>
        /// 公民身份号码
        /// </summary>
        public Ocr_Result 公民身份号码 { get; set; }

        /// <summary>
        /// 出生
        /// </summary>
        public Ocr_Result 出生 { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Ocr_Result 性别 { get; set; }
    }

    /// <summary>
    /// 识别结果
    /// </summary>
    public class Ocr_Result
    {
        public string words { get; set; }



    }


}
