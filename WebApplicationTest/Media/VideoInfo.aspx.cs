using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MediaInfoNET;

namespace WebApplicationTest.Media
{
    public partial class VideoInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVideoInfo_Click(object sender, EventArgs e)
        {
            var videoPath = @"E:\电影\a2ddbaa64d39a9208c70dfe0be18a703.mp4";
            MediaFile mediaFile = new MediaFile(videoPath);
            this.lblVideoInfo.Text = mediaFile.Video[0].FrameSize.Replace("x", " * ") + " * " +
                                        mediaFile.General.DurationString;
        }
    }
}