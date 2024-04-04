using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

namespace WebApplicationTest.FFmpeg
{
    public partial class Test : System.Web.UI.Page
    {

        //UpFiles文件夹是要保存你上传的文件，PlayFiles文件夹是用于你转换后保存的文件（用于网上播放）
        //ImgFile文件夹是保存截取视频文件的图片，然后那两个mencoder和ffmpeg文件夹是视频转换工具.此视频转换也
        //可叫做mencoder+ffmpeg视频转换.
        //首先，在配置文件中给这些文件夹进行路径的配置.如下


        // 扩展名定义

        string[] strArrFfmpeg = new string[] { "asf", "avi", "mpg", "3gp", "mov" };

        string[] strArrMencoder = new string[] { "wmv", "rm", "rmvb" };

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            string upFileName = "";

            if (this.FileUpload1.HasFile)
            {

                string fileName = PublicMethod.GetFileName(this.FileUpload1.FileName);// GetFileName();

                if ((string)Session["file"] == fileName)
                {

                    return;

                }

                upFileName = Server.MapPath(PublicMethod.upFile + fileName);
                FileHelper.FilePathCheck(upFileName);

                this.FileUpload1.SaveAs(upFileName);

                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                string playFile = Server.MapPath(PublicMethod.playFile + saveName);
                FileHelper.FilePathCheck(playFile);

                string imgFile = Server.MapPath(PublicMethod.imgFile + saveName);
                FileHelper.FilePathCheck(imgFile);

                //System.IO.File.Copy(Server.MapPath(PublicMethod.playFile + "00000002.jpg"), Server.MapPath(PublicMethod.imgFile+"aa.jpg"));

                PublicMethod pm = new PublicMethod();

                string m_strExtension = PublicMethod.GetExtension(this.FileUpload1.PostedFile.FileName).ToLower();

                if (m_strExtension == "flv")
                {
                    //直接拷贝到播放文件夹下

                    System.IO.File.Copy(upFileName, playFile + ".flv");

                    pm.CatchImg(upFileName, imgFile);

                }

                string Extension = CheckExtension(m_strExtension);

                if (Extension == "ffmpeg")
                {

                    pm.ChangeFilePhy(upFileName, playFile, imgFile);

                }

                else if (Extension == "mencoder")
                {

                    pm.MChangeFilePhy(upFileName, playFile, imgFile);

                }

                InsertData(this.txtTitle.Text, fileName, saveName);

                Session["file"] = fileName;

            }
        }



        private string CheckExtension(string extension)
        {

            string m_strReturn = "";

            foreach (string var in this.strArrFfmpeg)
            {

                if (var == extension)
                {

                    m_strReturn = "ffmpeg"; break;

                }

            }

            if (m_strReturn == "")
            {

                foreach (string var in strArrMencoder)
                {

                    if (var == extension)
                    {

                        m_strReturn = "mencoder"; break;

                    }

                }

            }

            return m_strReturn;

        }



        #region 插入数据到数据库中

        private void InsertData(string MediaName, string fileName, string saveName)
        {

            //string name=fileName.Substring(0, fileName.LastIndexOf('.'));

            string imgName = saveName + ".jpg";//图片文件名;
            string playName = saveName + ".flv";

            string sqlstr = "insert into Media(FMediaName,FMediaUpPath,FMediaPlayPath,FMediaImgPath) values(@MName,@MUppath,@MPlaypath,@MImgpath)";

            //string constr = ConfigurationManager.ConnectionStrings["sqlcon"].ToString();
            //SqlDataSource1.InsertCommand = sqlstr;
            //SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.Text;// CommandType.Text;
            //SqlDataSource1.InsertParameters.Add("MName", MediaName);
            //SqlDataSource1.InsertParameters.Add("MUppath", PublicMethod.upFile + fileName);
            //SqlDataSource1.InsertParameters.Add("MPlaypath", PublicMethod.playFile + playName);
            //SqlDataSource1.InsertParameters.Add("MImgpath", PublicMethod.imgFile + imgName);
            //SqlDataSource1.Insert();

        }

        #endregion

        #region 视频转换测试

        /// <summary>
        /// 视频转换测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTest_Click(object sender, EventArgs e)
        {
            string tools = @"D:\MSO\ffmpeg\bin\ffmpeg.exe";
            string source = @"D:\MSO\ffmpeg\bin\Test.avi";
            string file = @"D:\MSO\ffmpeg\bin\Test.flv";
            string args = " -i " + source + " -ab 64 -ar 22050 -b 500 -r 15 -s 800x600 " + file;
            args = "ffmpeg.exe -i Test1.flv -y -b 1024k -acodec copy -f mp4 Test1.mp4";
            ProcessStartInfo startInfo = new ProcessStartInfo(tools);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = args;
            Process.Start(startInfo);
        }

        #endregion












    }
}