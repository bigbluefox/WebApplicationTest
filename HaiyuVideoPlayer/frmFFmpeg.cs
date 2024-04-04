using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaiyuVideoPlayer
{
    public partial class frmFFmpeg : Form
    {
        public static string ffmpegtool = @"D:\Codes\ffmpeg\bin\ffmpeg.exe";
        //public static string ffmpegtool = @"F:\ABCSolution\ffmpeg-20160808-ce2217b-win64-static\bin\ffplay.exe";
        public static string playFile = @"D:\Codes\Videos\oceans.mp4";
        public static string imgFile = @"D:\Codes\ffmpeg\bin\Test.jpg";
        public static string sourceFile = @"D:\Codes\ffmpeg\bin\Test.flv";
        //public static string sourceFile = @"F:\ABCSolution\VideoSolution\VideoSolution\Content\Video\theme.mp4";

        //public static string sourceFile = @"D:\Codes\ffmpeg\bin\Test2.mp4";

        //申明一个委托对象
        public delegate void Action2<in T>(T t);

        public frmFFmpeg()
        {
            InitializeComponent();

            //ConvertVideo();

            //new Thread(() =>
            //{
            //    Action2<int> a = new Action2<int>(ConvertVideo);
            //    Invoke(a, 0);
                
            //}).Start();
        }

        public void ConvertVideo(int t)
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = ffmpegtool;//要调用外部程序的绝对路径
            //参数(这里就是FFMPEG的参数了)
            //p.StartInfo.Arguments = @"-i "+sourceFile+ " -ab 56  -b a -ar 44100 -b 500 -r 29.97 -s 1280x720 -y " + playFile+"";

            // p.StartInfo.Arguments = "-y -i \""+sourceFile+"\" -b v   -s 800x600 -r 29.97 -b 1500 -acodec aac -ac 2 -ar 24000 -ab 128 -vol 200 -f psp  \""+playFile+"\" ";

            // 转换格式
            //string strArg = "-i " + sourceFile + " -y -s 640x480 " + playFile + " ";
            string strArg = "-i " + sourceFile + " -y -s 1280x720 " + playFile + " ";
            strArg = " -i " + sourceFile + " -y -b 1024k -acodec copy -f mp4 " + playFile;

            //获取图片
            //截取图片jpg
            //string strArg = "-i " + sourceFile + " -y -f image2 -t 1 " + imgFile;
            //string strArg = "-i " + sourceFile + " -y -s 1280x720 -f image2 -t 1 " + imgFile;

            //string strArg = " -i " + sourceFile + " -y -f image2 -ss 3 -t 0.001 -s 480*270 " + imgFile;//480*270是图片分辨率;

            //视频截取
            //string strArg = "  -i " + sourceFile + " -y   -ss 0:20  -frames 100  " + playFile;

            //转化gif动画
            //string strArg = "-i " + sourceFile + " -y -s 1280x720 -f gif -vframes 30 " + imgFile;
            //string strArg = "  -i " + sourceFile + " -y  -f gif -vframes 50 " + imgFile;
            // string strArg = "  -i " + sourceFile + " -y  -f gif -ss 0:20  -dframes 10 -frames 50 " + imgFile;

            //显示基本信息
            //string strArg = "-i " + sourceFile + " -n OUTPUT";

            //播放视频 
            //string strArg = "-stats -i " + sourceFile + " ";

            p.StartInfo.Arguments = strArg;

            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            //p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.StartInfo.UseShellExecute = false;
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            //p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                //处理方法...
                //Console.WriteLine(output.Data);

                //lblMsg.Text = output.Data;

                ////去获取时长
                //string partitio1 = @"Duration: \d{2}:\d{2}:\d{2}.\d{2}";
                //if (RegexHelper.IsMatch(partitio1, output.Data))
                //{
                //    string partition = @"(?<=Duration: )\d{2}:\d{2}:\d{2}.\d{2}";
                //    string timespan = RegexHelper.Matchs(output.Data, partition).FirstOrDefault();
                //    TimeSpan span;
                //    if (TimeSpan.TryParse(timespan, out span))
                //    {
                //        Console.WriteLine(span.TotalMilliseconds);
                //    }
                //}

                ////获取时刻
                //string partitio2 = @"time=\d{2}:\d{2}:\d{2}.\d{2}";
                //if (RegexHelper.IsMatch(partitio2, output.Data))
                //{
                //    string partition = @"(?<=time=)\d{2}:\d{2}:\d{2}.\d{2}";

                //    string timespan = RegexHelper.Matchs(output.Data, partition).FirstOrDefault();
                //    TimeSpan span;
                //    if (TimeSpan.TryParse(timespan, out span))
                //    {
                //        Console.WriteLine(span.TotalMilliseconds);
                //    }
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Action2<int> a = new Action2<int>(ConvertVideo);
                Invoke(a, 0);

            }).Start();
        }
    }
}

//参数说明
/*
    * -i filename(input) 源文件目录
    * -y 输出新文件，是否强制覆盖已有文件
    * -c 指定编码器 
    * -b 500（视频流量数据）
    * -fs limit_size(outinput) 设置文件大小的限制，以字节表示的。没有进一步的字节块被写入后，超过极限。输出文件的大小略大于所请求的文件大小。
    * -s 视频比例  4:3 320x240/640x480/800x600  16:9  1280x720 ，默认值 'wxh'，和原视频大小相同
    * -vframes number(output) 将视频帧的数量设置为输出。别名：-frames:v
    * -dframes number (output) 将数据帧的数量设置为输出.别名：-frames:d
    * -frames[:stream_specifier] framecount (output,per-stream) 停止写入流之后帧数帧。
    * -bsf[:stream_specifier] bitstream_filters (output,per-stream)  指定输出文件流格式，
例如输出h264编码的MP4文件：ffmpeg -i h264.mp4 -c:v copy -bsf:v h264_mp4toannexb -an out.h264
    * -r 29.97 桢速率（可以改，确认非标准桢率会导致音画不同步，所以只能设定为15或者29.97） 
    * 
    */