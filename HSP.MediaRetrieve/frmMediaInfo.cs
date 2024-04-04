using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoLib;

namespace HSP.MediaRetrieve
{
    public partial class frmMediaInfo : Form
    {
        public frmMediaInfo()
        {
            InitializeComponent();

            String toDisplay;
            MediaInfo mi = new MediaInfo();

            toDisplay = mi.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (toDisplay.Length == 0)
            {
                Console.Out.WriteLine("MediaInfo.Dll: this version of the DLL is not compatible");
                return;
            }

            //Information about MediaInfo
            toDisplay += "\r\n\r\nInfo_Parameters\r\n";
            toDisplay += mi.Option("Info_Parameters");

            toDisplay += "\r\n\r\nInfo_Capacities\r\n";
            toDisplay += mi.Option("Info_Capacities");

            toDisplay += "\r\n\r\nInfo_Codecs\r\n";
            toDisplay += mi.Option("Info_Codecs");

            //An example of how to use the library
            toDisplay += "\r\n\r\nOpen\r\n";

            //String File_Name = @"E:\Music\moonlight.mp3";
            ////if (Args.Length == 0)
            ////    File_Name = "Example.ogg";
            ////else
            ////    File_Name = Args[0];

            //mi.Open(File_Name);

            //ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
            //mi.Option("Complete");
            //ToDisplay += mi.Inform();

            //ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
            //mi.Option("Complete", "1");
            //ToDisplay += mi.Inform();

            //ToDisplay += "\r\n\r\nCustom Inform\r\n";
            //mi.Option("Inform", "General;File size is %FileSize% bytes");
            //ToDisplay += mi.Inform();

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
            //ToDisplay += mi.Get(0, 0, "FileSize");

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
            //ToDisplay += mi.Get(0, 0, 46);

            //ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
            //ToDisplay += mi.Count_Get(StreamKind.Audio);

            //ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
            //ToDisplay += mi.Get(StreamKind.General, 0, "AudioCount");

            //ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
            //ToDisplay += mi.Get(StreamKind.Audio, 0, "StreamCount");

            //ToDisplay += "\r\n\r\nClose\r\n";
            mi.Close();

            this.txtMediaInfo.Text = toDisplay;

        }

        /// <summary>
        /// 媒体文件检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckMedia_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲",
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            //0.视频StreamKind.Video,
            //音频参数StreamKind.Audio,
            //全局参数StreamKind.General Parameter

            MediaInfo mi = new MediaInfo();
            mi.Open(fileDialog.FileName);

            StreamKind mediaType = MediaType(fileDialog.DefaultExt);

            ////1.
            //string width = mi.Get(StreamKind.Video, 0, "Width");//视频width
            //string height = mi.Get(StreamKind.Video, 0, "Height");
            //string s1 = mi.Inform();
            ////2.
            //mi.Option("Inform", "General;%Duration%");
            //string durationL = mi.Inform();
            ////3.
            ////MI.Option("Info_Parameters");
            //string s = mi.Get(StreamKind.Audio, 0, "Duration");//音频时长
            //mi.Close();
            ////4.
            //StringBuilder sb = new StringBuilder();
            ////sb.AppendFormat("", mi.);
            //sb.Append("用mediainfo.dll计算时长：" + TimeSpan.FromMilliseconds(Convert.ToDouble(s)));

            //String File_Name = @"E:\Music\moonlight.mp3";
            ////if (Args.Length == 0)
            ////    File_Name = "Example.ogg";
            ////else
            ////    File_Name = Args[0];

            //mi.Open(File_Name);

            String toDisplay = "";

            toDisplay += "\r\n\r\nInform with Complete=false\r\n";
            mi.Option("Complete");
            toDisplay += mi.Inform();

            toDisplay += "\r\n\r\nInform with Complete=true\r\n";
            mi.Option("Complete", "1");
            toDisplay += mi.Inform();

            toDisplay += "\r\n\r\nCustom Inform\r\n";
            mi.Option("Inform", "General;File size is %FileSize% bytes");
            toDisplay += mi.Inform();

            toDisplay += "\r\n\r\n";
            mi.Option("Inform", "General;媒体时长：%Duration% (ms)");
            toDisplay += mi.Inform();

            toDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
            toDisplay += mi.Get(0, 0, "FileSize");

            toDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
            toDisplay += mi.Get(0, 0, 46);

            if (mediaType == StreamKind.Audio)
            {
                toDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
                toDisplay += mi.Count_Get(StreamKind.Audio);

                toDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
                toDisplay += mi.Get(StreamKind.General, 0, "AudioCount");

                toDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
                toDisplay += mi.Get(StreamKind.Audio, 0, "StreamCount");

                toDisplay += "\r\n\r\n音频时长 Parameter='Duration'\r\n";
                toDisplay += mi.Get(StreamKind.Audio, 0, "Duration");

                toDisplay += "\r\n\r\n媒体类型 Parameter='StreamKind'\r\n";
                toDisplay += mi.Get(0, 0, "StreamKindID");
            }

            if (mediaType == StreamKind.Video)
            {
                string width = mi.Get(StreamKind.Video, 0, "Width");//视频width
                string height = mi.Get(StreamKind.Video, 0, "Height");
                string s1 = mi.Inform();

                toDisplay += "\r\n\r\n视频尺寸：" + width + " * " + height;
                //toDisplay += width;

                toDisplay += "\r\n\r\n视频时长 Parameter='Duration'\r\n";
                toDisplay += mi.Get(StreamKind.Video, 0, "Duration");
            }
            //toDisplay += "\r\n\r\nClose\r\n";

            mi.Close();

            txtMediaResult.Text = toDisplay;
           
        }

        public StreamKind MediaType(string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "*";
            StreamKind mediaType = StreamKind.General;

            if ("jpg,png,jpeg,gif".IndexOf(ext, StringComparison.Ordinal) > -1)
            {
                mediaType = StreamKind.Image;
            }

            if ("mp3,flac,ape,mid,wav,aac,wma,ogg".IndexOf(ext, StringComparison.Ordinal) > -1)
            {
                mediaType = StreamKind.Audio;
            }

            if ("jpg,png,jpeg,gif".IndexOf(ext, StringComparison.Ordinal) > -1)
            {
                mediaType = StreamKind.Image;
            }

            if ("mp4,avi,wmv,mpg,mpeg,mov,rm,rmvb,flv".IndexOf(ext, StringComparison.Ordinal) > -1)
            {
                mediaType = StreamKind.Video;
            }

            // 类型：0-其他；1-图片；2-音频；3-视频;4-阅读
            //switch (ext)
            //{
            //    // 图片
            //    case "1":
            //        strExt = "jpg,png,jpeg,gif";
            //        isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
            //        break;

            //    // 音频
            //    case "2":
            //        strExt = "mp3,flac,ape,mid,wav,aac,wma,ogg";
            //        isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
            //        break;

            //    // 视频
            //    case "3":
            //        strExt = "mp4,avi,wmv,mpg,mpeg,mov,rm,rmvb";
            //        isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
            //        break;

            //    // 阅读
            //    case "4":
            //        strExt = "pdf,doc,docx,epub";
            //        isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
            //        break;

            //    default:
            //        // 其他
            //        isMediaFile = true;
            //        break;
            //}

            return mediaType;
        }
    }
}
