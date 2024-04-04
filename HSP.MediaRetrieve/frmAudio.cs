using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Hsp.Test.Common;
using Shell32;

namespace HSP.MediaRetrieve
{
    public partial class frmAudio : Form
    {
        internal OpenFileDialog _opendialog;

        public frmAudio()
        {
            InitializeComponent();

            lblAudioInfo.Text = "";
            lblAudioMessage.Text = "";

            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Start();
        }

        //4.使用axWindowsMediaPlayer的COM组件来播放
        //a.加载COM组件:ToolBox->Choose Items->COM Components->Windows Media Player如下图：
        //b.把Windows Media Player控件拖放到Winform窗体中，把axWindowsMediaPlayer1中URL属性设置为MP3或是AVI的文件路径，F5运行。

        //鼠标按下时停止Timer，或者添加一个字段鼠标按下时为true通过在Timer里判断鼠标是否在进度条上按下，
        //没按下的情况下才把歌曲进度赋给进度条。鼠标松开后设置歌曲进度再开启Timer或者把鼠标按下的字段改为false


        #region 检查音频

        /// <summary>
        /// 检查音频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckAudio_Click(object sender, EventArgs e)
        {
            var strAudioPath = @"E:\Music\moonlight.mp3";

            Hsp.Test.Common.Mp3FileInfo mp3 = new Mp3FileInfo(strAudioPath);
            var title = mp3.Info.Title;

            GetMusicInfo(strAudioPath);

            //AxWindowsMediaPlayer mediaPlayer = new AxWindowsMediaPlayer();
            //mediaPlayer.URL = strAudioPath;
            //mediaPlayer.Ctlcontrols.play();

            this.axWindowsMediaPlayer1.URL = strAudioPath;
            this.axWindowsMediaPlayer1.Ctlcontrols.play();

            // iCol 对应文件详细属性汇总
            //ID  => DETAIL-NAME
            //0   => Name
            //1   => Size     // MP3 文件大小
            //2   => Type
            //3   => Date modified
            //4   => Date created
            //5   => Date accessed
            //6   => Attributes
            //7   => Offline status
            //8   => Offline availability
            //9   => Perceived type
            //10  => Owner
            //11  => Kinds
            //12  => Date taken
            //13  => Artists   // MP3 歌手
            //14  => Album     // MP3 专辑
            //15  => Year
            //16  => Genre
            //17  => Conductors
            //18  => Tags
            //19  => Rating
            //20  => Authors
            //21  => Title     // MP3 歌曲名
            //22  => Subject
            //23  => Categories
            //24  => Comments
            //25  => Copyright
            //26  => #
            //27  => Length    // MP3 时长
            //28  => Bit rate
            //29  => Protected
            //30  => Camera model
            //31  => Dimensions
            //32  => Camera maker
            //33  => Company
            //34  => File description
            //35  => Program name
            //36  => Duration
            //37  => Is online
            //38  => Is recurring
            //39  => Location
            //40  => Optional attendee addresses
            //41  => Optional attendees
            //42  => Organizer address
            //43  => Organizer name
            //44  => Reminder time
            //45  => Required attendee addresses
            //46  => Required attendees
            //47  => Resources
            //48  => Free/busy status
            //49  => Total size
            //50  => Account name
            //51  => Computer
            //52  => Anniversary
            //53  => Assistant's name
            //54  => Assistant's phone
            //55  => Birthday
            //56  => Business address
            //57  => Business city
            //58  => Business country/region
            //59  => Business P.O. box
            //60  => Business postal code
            //61  => Business state or province
            //62  => Business street
            //63  => Business fax
            //64  => Business home page
            //65  => Business phone
            //66  => Callback number
            //67  => Car phone
            //68  => Children
            //69  => Company main phone
            //70  => Department
            //71  => E-mail Address
            //72  => E-mail2
            //73  => E-mail3
            //74  => E-mail list
            //75  => E-mail display name
            //76  => File as
            //77  => First name
            //78  => Full name
            //79  => Gender
            //80  => Given name
            //81  => Hobbies
            //82  => Home address
            //83  => Home city
            //84  => Home country/region
            //85  => Home P.O. box
            //86  => Home postal code

        }

        private void GetMusicInfo(string strAudioPath)
        {
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(strAudioPath));
            FolderItem item = dir.ParseName(Path.GetFileName(strAudioPath));
            StringBuilder sb = new StringBuilder();
            for (int i = -1; i < 86; i++)
            {
                // 0 Retrieves the name of the item.  
                // 1 Retrieves the size of the item.  
                // 2 Retrieves the type of the item.  
                // 3 Retrieves the date and time that the item was last modified.  
                // 4 Retrieves the attributes of the item.  
                // -1 Retrieves the Info tip information for the item.  

                var itemString = dir.GetDetailsOf(item, i);
                if (string.IsNullOrEmpty(itemString)) continue;

                sb.Append(i.ToString());
                sb.Append(":");
                sb.Append(itemString);
                sb.Append(Environment.NewLine);
                // 如果执行过程中，出现是错误提示，通过Shell32 的 Embed Interop Types 属性设置为 False 即可；
            }
            //string c = sb.ToString();

            this.lblAudioInfo.Text = sb.ToString();
        }

        //private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        //{
        //    if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
        //    {
        //        Thread thread = new Thread(new ThreadStart(PlayThread));
        //        thread.Start();
        //    }
        //}
        //private void PlayThread()
        //{
        //    axWindowsMediaPlayer1.URL = @"E:\Music\moonlight.mp3";
        //    axWindowsMediaPlayer1.Ctlcontrols.play();
        //}

        public string[] getSongInfoFromWma(string FileName)
        {
            //string[] wmaFileStruct = new string[3];
            ////MP3File mp3=new MP3File(); 
            ////create shell instance
            //Shell32.Shell shell  = new Shell32.ShellClass();
            ////set the namespace to file path
            //Shell32.Folder folder = shell.NameSpace(FileName.Substring(0,FileName.LastIndexOf("\\")));
            ////get ahandle to the file
            //Shell32.FolderItem folderItem = folder.ParseName(FileName.Substring(FileName.LastIndexOf("\\")+1));
            ////did we get a handle ?


            //wmaFileStruct[0] = folder.GetDetailsOf(folderItem,10);  //歌曲名称
            //wmaFileStruct[1] = folder.GetDetailsOf(folderItem,9);   //歌手名称
            //wmaFileStruct[2] = folder.GetDetailsOf(folderItem,21);  //播放时间

            //return wmaFileStruct;

            return null;
        }

        #endregion

        #region 添加音频文件

        /// <summary>
        /// 添加音频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMusicFile_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲",
                InitialDirectory = @"E:\Music", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            string path = fileDialog.FileName;
            var info = new FileInfo(path);

            axWindowsMediaPlayer1.URL = info.FullName;
            axWindowsMediaPlayer1.Ctlcontrols.play();

            GetMusicInfo(info.FullName);
        }

        #endregion

        #region 计时器显示歌曲进度及长度

        /// <summary>
        /// 计时器显示歌曲进度及长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var endPoint = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            if (endPoint > 0)
            {
                lblAudioMessage.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString + @" / " +
                                      axWindowsMediaPlayer1.currentMedia.durationString;
            }
            else
            {
                lblAudioMessage.Text = DateTime.Now.ToLongTimeString();
            }
        }

        #endregion

        #region 窗体正在关闭事件

        /// <summary>
        /// 窗体正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.axWindowsMediaPlayer1.close();
            this.axWindowsMediaPlayer1.Dispose();
        }

        #endregion


    }



}
