using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplicationTest.FFmpeg
{
    public class PublicMethod : System.Web.UI.Page
    {
        public PublicMethod()
        {
        }

        //文件路径
        public static string ffmpegtool = ConfigurationManager.AppSettings["ffmpeg"];
        public static string mencodertool = ConfigurationManager.AppSettings["mencoder"];
        public static string mplayertool = ConfigurationManager.AppSettings["mplayer"];

        public static string upFile = ConfigurationManager.AppSettings["upfile"] + "/";
        public static string imgFile = ConfigurationManager.AppSettings["imgfile"] + "/";
        public static string playFile = ConfigurationManager.AppSettings["playfile"] + "/";

        //文件图片大小
        public static string sizeOfImg = ConfigurationManager.AppSettings["CatchFlvImgSize"];

        //文件大小
        public static string widthOfFile = ConfigurationManager.AppSettings["widthSize"];
        public static string heightOfFile = ConfigurationManager.AppSettings["heightSize"];

        /// <summary>
        /// 获取文件的名字
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileName(string fileName)
        {
            int i = fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1;
            return fileName.Substring(i);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return fileName.Substring(i);
        }

        #region 运行FFMpeg的视频解码，(这里是绝对路径)

        /// <summary>
        /// 转换文件并保存在指定文件夹下面(这里是绝对路径)
        /// </summary>
        /// <param name="fileName">上传视频文件的路径（原文件）</param>
        /// <param name="target">转换后的文件的路径（网络播放文件）</param>
        /// <param name="img">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟地址; 失败:返回空字符串</returns>
        public string ChangeFilePhy(string fileName, string target, string img)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:<add key="ffmpeg" value="E:\51aspx\ffmpeg.exe" /> 

            string ffmpeg = Server.MapPath(PublicMethod.ffmpegtool);

            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(fileName)))
            {
                return "";
            }

            //获得图片和(.flv)文件相对路径/最后存储到数据库的路径,如:/Web/User1/00001.jpg 

            string flvFile = System.IO.Path.ChangeExtension(target, ".flv");

            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" /> 

            string flvImgSize = PublicMethod.sizeOfImg;

            System.Diagnostics.ProcessStartInfo filestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);

            filestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //FilestartInfo.Arguments = " -i " + fileName + " -ab 56 -ar 22050 -b 500 -r 15 -s " + widthOfFile + "x" +
            //                          heightOfFile + " " + flv_file;


            filestartInfo.Arguments = " -i " + fileName + " -y -b 1024k -acodec copy -f mp4 " + flvFile;

            //ImgstartInfo.Arguments = " -i " + fileName + " -y -f image2 -t 0.05 -s " + FlvImgSize + " " + flv_img;

            try
            {
                //转换
                System.Diagnostics.Process.Start(filestartInfo);

                //截图
                CatchImg(fileName, img);

                //System.Diagnostics.Process.Start(ImgstartInfo);
            }

            catch
            {
                return "";
            }

            return "";
        }

        /// <summary>
        /// 截取视频图片文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public string CatchImg(string fileName, string img)
        {
            //
            string ffmpeg = Server.MapPath(PublicMethod.ffmpegtool);

            //
            string flvImg = img + ".jpg";

            //
            string flvImgSize = PublicMethod.sizeOfImg;

            //
            System.Diagnostics.ProcessStartInfo imgstartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);

            imgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //
            imgstartInfo.Arguments = " -i " + fileName + " -y -f image2 -ss 2 -vframes 1 -s " + flvImgSize + " " +
                                     flvImg;

            try
            {
                System.Diagnostics.Process.Start(imgstartInfo);
            }
            catch
            {
                return "";
            }

            //
            if (System.IO.File.Exists(flvImg))
            {
                return flvImg;
            }

            return "";
        }

        #endregion

        #region 运行FFMpeg的视频解码，(这里是(虚拟)相对路径)

        /// <summary>
        /// 转换文件并保存在指定文件夹下面(这里是相对路径)
        /// </summary>
        /// <param name="fileName">上传视频文件的路径（原文件）</param>
        /// <param name="target">转换后的文件的路径（网络播放文件）</param>
        /// <param name="img">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟地址; 失败:返回空字符串</returns>
        public string ChangeFileVir(string fileName, string target, string img)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:<add key="ffmpeg" value="E:\51aspx\ffmpeg.exe" /> 

            string ffmpeg = Server.MapPath(PublicMethod.ffmpegtool);

            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(fileName)))
            {
                return "";
            }

            //获得图片和(.flv)文件相对路径/最后存储到数据库的路径,如:/Web/User1/00001.jpg 

            string flvImg = System.IO.Path.ChangeExtension(Server.MapPath(img), ".jpg");
            string flvFile = System.IO.Path.ChangeExtension(Server.MapPath(target), ".flv");

            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" /> 

            string flvImgSize = PublicMethod.sizeOfImg;

            System.Diagnostics.ProcessStartInfo filestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            System.Diagnostics.ProcessStartInfo imgstartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);

            filestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            imgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过 

            //ffmpeg -i F:\01.wmv -ab 56 -ar 22050 -b 500 -r 15 -s 320x240 f:\test.flv

            filestartInfo.Arguments = " -i " + fileName + " -ab 56 -ar 22050 -b 500 -r 15 -s " + widthOfFile + "x" +
                                      heightOfFile + " " + flvFile;

            imgstartInfo.Arguments = " -i " + fileName + " -y -f image2 -t 0.001 -s " + flvImgSize + " " + flvImg;

            try
            {
                System.Diagnostics.Process.Start(filestartInfo);
                System.Diagnostics.Process.Start(imgstartInfo);
            }

            catch
            {
                return "";
            }

            /**/

            //注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长; 
            //这儿需要延时后再检测,我服务器延时8秒,即如果超过8秒图片仍不存在,认为截图失败; 
            //此处略去延时代码.如有那位知道如何捕捉ffmpeg.exe截图失败消息,请告知,先谢过! 

            if (System.IO.File.Exists(flvImg))
            {
                return flvImg;
            }

            return "";
        }

        #endregion

        #region 运行mencoder的视频解码器转换(这里是(绝对路径))

        public string MChangeFilePhy(string vFileName, string target, string img)
        {
            string tool = Server.MapPath(PublicMethod.mencodertool);

            //string mplaytool = Server.MapPath(PublicMethod.ffmpegtool);

            if ((!System.IO.File.Exists(tool)) || (!System.IO.File.Exists(vFileName)))
            {
                return "";
            }

            string flvFile = System.IO.Path.ChangeExtension(target, ".flv");


            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" /> 

            string flvImgSize = PublicMethod.sizeOfImg;

            System.Diagnostics.ProcessStartInfo filestartInfo = new System.Diagnostics.ProcessStartInfo(tool);

            filestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            filestartInfo.Arguments = " " + vFileName + " -o " + flvFile +
                                      " -of lavf -lavfopts i_certify_that_my_video_stream_does_not_use_b_frames -oac mp3lame -lameopts abr:br=56 -ovc lavc  -lavcopts vcodec=flv:vbitrate=200:mbd=2:mv0:trell:v4mv:cbp:last_pred=1:dia=-1:cmp=0:vb_strategy=1  -vf scale=" +
                                      widthOfFile + ":" + heightOfFile + " -ofps 12 -srate 22050";

            try
            {
                System.Diagnostics.Process.Start(filestartInfo);
                CatchImg(flvFile, img);
            }

            catch
            {
                return "";
            }

            return "";
        }

        #endregion
    }
}