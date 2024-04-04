using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMPLib;
using AxWMPLib;

namespace WebApplicationTest.MultiMedia
{
    public partial class VideoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var videofile = "VideoTest.mp4";
            //var videoPath = Server.MapPath(videofile);
            //FileInfo file = new FileInfo(videoPath);

            //var fi = file.Exists;

            //WMPLib.WindowsMediaPlayer media = new WindowsMediaPlayer();
            //media.URL = Server.MapPath(videofile);
            //var w = media.currentMedia.imageSourceWidth;
            //var h = media.currentMedia.imageSourceHeight;
            //var l = media.currentMedia.duration;

            //AxWMPLib.AxWindowsMediaPlayer player = new AxWindowsMediaPlayer();
            //player.URL = Server.MapPath(videofile);

            //var w = player.currentMedia.imageSourceWidth;
            //var h = player.currentMedia.imageSourceHeight;
            //var l = player.currentMedia.duration;

            //this.VideoInfo.Text = w + " * " + h + " * " + l;

        }


        //private MediaPlayer getVideoMediaPlayer(File file)
        //{
        //    try
        //    {
        //        return MediaPlayer.create(getActivity(), Uri.fromFile(file));
        //    }
        //    catch (Exception e)
        //    {
        //        e.printStackTrace();
        //    }
        //    return null;
        //}






    }
}