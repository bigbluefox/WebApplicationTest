using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Baidu.Aip.Nlp;
using Baidu.Aip.Ocr;
using Baidu.Aip.Speech;
using Newtonsoft.Json;

namespace Hsp.Test.AI
{
    /// <summary>
    /// 百度AI
    /// http://ai.baidu.com
    /// https://ai.baidu.com/ai-doc/OCR/Hkibizy6z
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly Asr _asrClient;
        private readonly Tts _ttsClient;
        internal string API_KEY = "y2ZIbUNqLKOLRaXBzOQrchdC";

        // 设置APPID/AK/SK
        internal string APP_ID = "10275427";

        private byte[] byData = new byte[100];
        private char[] charData = new char[65536];
        internal string SECRET_KEY = "2MBnjf5hLmA3ntuGk86KE0TjwPWhRMYj";

        public Form1()
        {
            InitializeComponent();

            //_asrClient = new Asr(APP_ID, API_KEY, SECRET_KEY);
            _ttsClient = new Tts(API_KEY, SECRET_KEY);

            lblFilePath.Text = "";
            lblResult.Text = "";
        }

        //AppID / 10275427
        //API Key / y2ZIbUNqLKOLRaXBzOQrchdC
        //Secret Key / 2MBnjf5hLmA3ntuGk86KE0TjwPWhRMYj

        #region 语音识别(×)

        /// <summary>
        ///     语音识别
        ///     支持音频格式：pcm、wav、amr、m4a
        ///     content len too long.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudioTest_Click(object sender, EventArgs e)
        {
            // {
            //  "err_msg": "speech data error.",
            //  "err_no": 3309,
            //  "sn": "488575288821508813825"
            //}

            //             "err_msg": "param rate invalid.",
            //  "err_no": 3311,
            //  "sn": "326682828901508814351"
            //}

            var data = File.ReadAllBytes(@"E:\Software\Media\pcmwav\pcm.pcm");
            // 可选参数
            var options = new Dictionary<string, object>
             {
                {"dev_pid", 1537}
             };
            //_asrClient.Timeout = 120000; // 若语音较长，建议设置更大的超时时间. ms
            var result = _asrClient.Recognize(data, "pcm", 128000, options);
            // content len too long.
            Console.Write(result);

            var sb = new StringBuilder();
            for (var i = 0; i < result.Count; i++)
            {
                sb.Append(i.ToString());
            }

            lblResult.Text = sb.ToString();
        }

        #endregion

        #region 色情识别

        /// <summary>
        ///     色情识别(√)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImageTest_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|BMP文件(*.bmp;)|*.bmp;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            //var client = new Baidu.Aip.ContentCensor.AntiPorn(API_KEY, SECRET_KEY);
            //var imgPath = @"E:\新建文件夹\IMAGES\131510724898429643.jpg";
            //imgPath = @"D:\新建文件夹\14ce36d3d539b600422be1fce150352ac75cb790.jpg";
            //var image = File.ReadAllBytes(fileDialog.FileName);
            //var result = client.Detect(image);

            //var text = "";
            //text = result.ToString();
            ////foreach (var rst in result["result"])
            ////{
            ////    text += rst["class_name"] + "：" + rst["probability"] + " * ";
            ////}

            ////text += result["conclusion"];

            //txtResult.Text = text;
        }

        #endregion

        /// <summary>
        ///     带生僻字版文字识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGeneralEnhanced_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            // 带生僻字版
            var result = client.GeneralEnhanced(image);

            var l = result;
        }

        /// <summary>
        ///     网图识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWebImage_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            // 网图识别
            var result = client.WebImage(image, null);

            if (result != null)
                txtResult.Text = result.ToString();
        }

        /// <summary>
        ///     高精度识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccurate_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            // 高精度识别
            var result = client.Accurate(image);

            if (result != null)
                txtResult.Text = result.ToString();
        }

        /// <summary>
        ///     银行卡识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBankCard_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            // 银行卡识别
            //var result = client.BankCard(image);
        }

        /// <summary>
        ///     身份证识别(√,20220502)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdcard_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            var options = new Dictionary<string, object>
            {
                {"detect_direction", "true"} // 检测方向
            };
            // 身份证正面识别 idCardSide:front/back
            var result = client.Idcard(image, "front", options);
            // 身份证背面识别
            //result = client.IdCardBack(image);

            //if (result["error_code"].HasValues)
            //{
            //    txtResult.Text = result["error_msg"].ToString();
            //}

            var l = result;

            var retstring = result.ToString();
            //var words_result = result.First.words_result.住址.words;

            IdCard_Result idresult = JsonConvert.DeserializeObject<IdCard_Result>(retstring);
            var idinfo = idresult.words_result.姓名.words;
            idinfo += Environment.NewLine + idresult.words_result.民族.words;
            idinfo += Environment.NewLine + idresult.words_result.住址.words;
            idinfo += Environment.NewLine + idresult.words_result.公民身份号码.words;
            idinfo += Environment.NewLine + idresult.words_result.出生.words;
            idinfo += Environment.NewLine + idresult.words_result.性别.words;

            txtResult.Text = idinfo;


            //          {
            //              "direction": 0,
            //"words_result": {
            //                  "姓名": {
            //                      "location": {
            //                          "top": 24,
            //      "left": 63,
            //      "width": 41,
            //      "height": 17
            //                      },
            //    "words": "李海玉"
            //                  },
            //  "民族": {
            //                      "location": {
            //                          "top": 51,
            //      "left": 124,
            //      "width": 12,
            //      "height": 14
            //                      },
            //    "words": "汉"
            //  },
            //  "住址": {
            //                      "location": {
            //                          "top": 102,
            //      "left": 59,
            //      "width": 129,
            //      "height": 31
            //                      },
            //    "words": "北京市西城区月坛西街西里甲29号楼3门1号"
            //  },
            //  "公民身份号码": {
            //                      "location": {
            //                          "top": 165,
            //      "left": 111,
            //      "width": 169,
            //      "height": 16
            //                      },
            //    "words": "370902196910221236"
            //  },
            //  "出生": {
            //                      "location": {
            //                          "top": 75,
            //      "left": 60,
            //      "width": 103,
            //      "height": 14
            //                      },
            //    "words": "19691022"
            //  },
            //  "性别": {
            //                      "location": {
            //                          "top": 51,
            //      "left": 60,
            //      "width": 12,
            //      "height": 14
            //                      },
            //    "words": "男"
            //  }
            //              },
            //"words_result_num": 6,
            //"idcard_number_type": 1,
            //"image_status": "normal",
            //"log_id": 1520970424829568750
            //          }



            //{
            //    "errno": 0,
            //    "msg": "success",
            //    "data": {
            //        "log_id": "7686291699921345817",
            //        "words_result_num": 6,
            //        "direction": 0,
            //        "image_status": "normal",
            //        "words_result": {
            //            "住址": {
            //                "location": {
            //                    "width": 197,
            //                    "top": 150,
            //                    "height": 37,
            //                    "left": 78
            //                },
            //                "words": "北京市海淀区上地十号七栋2单元110室"
            //            },
            //            "出生": {
            //                "location": {
            //                    "width": 148,
            //                    "top": 111,
            //                    "height": 15,
            //                    "left": 79
            //                },
            //                "words": "19890601"
            //            },
            //            "姓名": {
            //                "location": {
            //                    "width": 63,
            //                    "top": 32,
            //                    "height": 25,
            //                    "left": 77
            //                },
            //                "words": "百度熊"
            //            },
            //            "公民身份号码": {
            //                "location": {
            //                    "width": 252,
            //                    "top": 243,
            //                    "height": 15,
            //                    "left": 139
            //                },
            //                "words": "532101198906010015"
            //            },
            //            "性别": {
            //                "location": {
            //                    "width": 20,
            //                    "top": 76,
            //                    "height": 15,
            //                    "left": 71
            //                },
            //                "words": "男"
            //            },
            //            "民族": {
            //                "location": {
            //                    "width": 12,
            //                    "top": 76,
            //                    "height": 15,
            //                    "left": 172
            //                },
            //                "words": "汉"
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        ///     驾驶证识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrivingLicense_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);
            var result = client.DrivingLicense(image);
        }

        /// <summary>
        ///     行驶证识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVehicleLicense_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);
            var result = client.VehicleLicense(image);

            if (result != null)
                txtResult.Text = result.ToString();
        }

        /// <summary>
        ///     车牌识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlateLicense_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);
            var result = client.LicensePlate(image);

            if (result != null)
                txtResult.Text = result.ToString();
        }

        /// <summary>
        ///     营业执照识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBusinessLicense_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);
            var result = client.BusinessLicense(image);
        }

        /// <summary>
        ///     自然语言处理(√)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNlp_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择文本",
                InitialDirectory = @"D:\Text", // 设置默认打开路径(绝对路径)
                Filter =
                    @"TEXT文件(*.txt;)|*.txt;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            //lblFilePath.Text = fileDialog.FileName;
            var client = new Nlp(API_KEY, SECRET_KEY);
            try
            {
                var reader = new StreamReader(fileDialog.FileName, Encoding.Default);
                var text = reader.ReadToEnd();
                text = text.Length > 10000 ? text.Substring(0, 10000) : text;
                var result = client.Lexer(text);

                txtResult.Text = result.ToString();
            }
            catch (IOException ex)
            {
                //Console.WriteLine(e.ToString());
            }
        }

        #region 文字识别

        /// <summary>
        ///     通用文字识别(√)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWordsTest_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择图片",
                InitialDirectory = @"D:\新建文件夹", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|JPG文件(*.jpg;)|*.jpg;|JPEG文件(*.jpeg;)|*.jpeg;|PNG文件(*.png;)|*.png;|GIF文件(*.gif;)|*.gif;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            lblFilePath.Text = fileDialog.FileName;

            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(fileDialog.FileName);

            // 通用文字识别
            var result = client.GeneralBasic(image);

            // 图片url
            //result = client.GeneralBasic("https://www.baidu.com/img/bd_logo1.png");

            var rst = "";
            foreach (var line in result["words_result"])
            {
                rst += line["words"] + Environment.NewLine;
            }

            txtResult.Text = rst;

            //{
            //  "log_id": 537147680,
            //  "words_result_num": 1,
            //  "words_result": [
            //    {
            //      "words": "Badu百度"
            //    }
            //  ]
            //}
        }


        public void GeneralBasicDemo()
        {
            var client = new Ocr(API_KEY, SECRET_KEY);
            //client.Timeout = 60000; // 修改超时时间

            var image = File.ReadAllBytes("图片文件路径");
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"language_type", "CHN_ENG"},
                {"detect_direction", "true"},
                {"detect_language", "true"},
                {"probability", "true"}
            };
            // 带参数调用通用文字识别, 图片参数为本地图片
            result = client.GeneralBasic(image, options);
            Console.WriteLine(result);
        }

        public void GeneralBasicUrlDemo()
        {
            var client = new Ocr(API_KEY, SECRET_KEY);
            //client.Timeout = 60000; // 修改超时时间

            var url = "http//www.x.com/sample.jpg";

            // 调用通用文字识别, 图片参数为远程url图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasicUrl(url);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"language_type", "CHN_ENG"},
                {"detect_direction", "true"},
                {"detect_language", "true"},
                {"probability", "true"}
            };
            // 带参数调用通用文字识别, 图片参数为远程url图片
            result = client.GeneralBasicUrl(url, options);
            Console.WriteLine(result);
        }

        #endregion
    }
}