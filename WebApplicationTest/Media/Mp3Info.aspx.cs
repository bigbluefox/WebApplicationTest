using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ID3;
using Shell32;

namespace WebApplicationTest.Media
{
    public partial class Mp3Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            #region 保存音乐文件到本地

            string strMp3 = @"~/upload/musics/";
            if (!Directory.Exists(Server.MapPath(strMp3)))
            {
                Directory.CreateDirectory(Server.MapPath(strMp3));
            }
            //strMp3 += fileMp3.FileName;
            if (File.Exists(Server.MapPath(strMp3)))
            {
                File.Delete(Server.MapPath(strMp3));
            }
            //fileMp3.SaveAs(Server.MapPath(strMp3));

            #endregion  

            #region 获取音乐文件信息
            string mp3InfoInterHtml = "";
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(Server.MapPath(strMp3)));
            FolderItem item = dir.ParseName(Path.GetFileName(Server.MapPath(strMp3)));
            mp3InfoInterHtml += "文件名：" + dir.GetDetailsOf(item, 0) + "<br>";
            mp3InfoInterHtml += "文件大小：" + dir.GetDetailsOf(item, 1) + "<br>";
            mp3InfoInterHtml += "歌曲名：" + dir.GetDetailsOf(item, 21) + "<br>";
            mp3InfoInterHtml += "歌手：" + dir.GetDetailsOf(item, 13) + "<br>";
            mp3InfoInterHtml += "专辑：" + dir.GetDetailsOf(item, 14) + "<br>";
            mp3InfoInterHtml += "时长：" + dir.GetDetailsOf(item, 27) + "<br>";
            #endregion


            #region 显示专辑图片

            string picturePath = @"~/image/play_null_img.png";
            if (!Directory.Exists(Server.MapPath(@"~/upload/images/")))
            {
                Directory.CreateDirectory(Server.MapPath(@"~/upload/images/"));
            }
            // 加载MP3
            ID3Info info = new ID3Info(Server.MapPath(strMp3), true);
            System.Drawing.Image image = null;
            if (info.ID3v2Info.AttachedPictureFrames.Count > 0)
            {
                image = System.Drawing.Image.FromStream(info.ID3v2Info.AttachedPictureFrames.Items[0].Data);
                picturePath = @"~/upload/images/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                if (File.Exists(Server.MapPath(picturePath)))
                {
                    File.Delete(Server.MapPath(picturePath));
                }
                image.Save(Server.MapPath(picturePath));
            }
            //imgMP3.ImageUrl = picturePath;
            //dMp3.InnerHtml = mp3InfoInterHtml;

            #endregion

        }
    }
}