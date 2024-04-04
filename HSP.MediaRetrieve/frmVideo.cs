using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoNET;
using WMPLib;

namespace HSP.MediaRetrieve
{
    public partial class frmVideo : Form
    {
        /// <summary>
        /// 视频目录
        /// </summary>
        public  string VideoPath { get; set; }

        private readonly OpenFileDialog _opendialog;

        /// <summary>
        /// 构造函数
        /// </summary>
        
        public frmVideo()
        {
            InitializeComponent();

            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Start();

            //VideoPath = @"E:\Videos\阿拉巴马电厂展示.mp4";
            //axWindowsMediaPlayer1.URL = VideoPath;

            btnCheckVideo.Hide();
            btnAddVideoFile.Hide();
            btnFullScreen1.Hide();

            lblVideoTitle.Text = "";
            lblVideoProgess.Text = "";

            axWindowsMediaPlayer1.windowlessVideo = false; //设为false后双击屏幕可以全屏

            btnAddVideo.Click += new EventHandler(btnAddVideoFile_Click);
            btnFullScreen.Click += new EventHandler(btnFullScreen_Click);

            oldVolume = axWindowsMediaPlayer1.settings.volume;
            this.tbVolume.Value = oldVolume;
            this.tbProgress.Value = 0;

            //txtVideoInfo.Padding = new Padding(10);
            //txtVideoInfo.Margin = new Padding(10);
        }

        /// <summary>
        /// 检查视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckVideo_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择视频",
                Filter =
                    @"MP4文件(*.mp4;)|*.mp4;|AVI文件(*.avi;)|*.avi;|WMV文件(*.wmv;)|*.wmv;|所有文件(*.*;)|*.*;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            //axWindowsMediaPlayer1.URL = strVideoPath;
            //axWindowsMediaPlayer1.Ctlcontrols.play();

            //var s = axWindowsMediaPlayer1.currentMedia.duration + " * " +
            //        axWindowsMediaPlayer1.currentMedia.imageSourceWidth
            //        + " * " + axWindowsMediaPlayer1.currentMedia.imageSourceHeight;

            //this.lblVideoMessage.Text = s;

            //axWindowsMediaPlayer1.URL = fileDialog.FileName;
            //axWindowsMediaPlayer1.Ctlcontrols.pause();

            VideoPath = fileDialog.FileName;

            MediaFile mediaFile = new MediaFile(fileDialog.FileName);
            //ShowBriefInfo(mediaFile);
            ShowVideoInfo(mediaFile);

            IWMPMedia media = axWindowsMediaPlayer1.newMedia(VideoPath); //创建播放实例
            axWindowsMediaPlayer1.currentPlaylist.appendItem(media); //将播放实例加入播放列表
            //axWindowsMediaPlayer1.Ctlcontrols.playItem(media); //播放

            int intWidth = axWindowsMediaPlayer1.currentMedia.imageSourceWidth;
            int intHeight = axWindowsMediaPlayer1.currentMedia.imageSourceHeight;

            this.lblVideoTitle.Text = intWidth + " * " + intHeight + " * " + media.durationString;

            //this.lblVideoMessage.Text += media.getItemInfo("width");

        }

        /// <summary>
        /// 时钟滴答
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var endPoint = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            if (endPoint > 0)
            {
                //int intWidth = axWindowsMediaPlayer1.currentMedia.imageSourceWidth;
                //int intHeight = axWindowsMediaPlayer1.currentMedia.imageSourceHeight;

                lblVideoProgess.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString + @" / " +
                                       axWindowsMediaPlayer1.currentMedia.durationString; // + @" " + intWidth + @"*" + intHeight; 

                #region 进度

                tbProgress.Value = (int)((axWindowsMediaPlayer1.Ctlcontrols.currentPosition) / (axWindowsMediaPlayer1.currentMedia.duration) * 100); // 进度

                #endregion
            }
            else
            {
                lblVideoProgess.Text = DateTime.Now.ToLongTimeString();
            }
        }

        #region 添加视频文件

        /// <summary>
        /// 添加视频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVideoFile_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择视频",
                InitialDirectory = @"E:\Videos", // 设置默认打开路径(绝对路径)
                Filter =
                    @"MP4文件(*.mp4;)|*.mp4;|AVI文件(*.avi;)|*.avi;|WMV文件(*.wmv;)|*.wmv;|MKV文件(*.mkv;)|*.mkv;|所有文件(*.*;)|*.*;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            //string path = fileDialog.FileName;
            //var info = new FileInfo(path);

            timer1.Start();

            #region 视频播放

            this.tbProgress.Value = 0;
            axWindowsMediaPlayer1.URL = fileDialog.FileName;
            axWindowsMediaPlayer1.Ctlcontrols.play();

            #endregion

            #region 显示视频信息

            MediaFile mediaFile = new MediaFile(fileDialog.FileName);
            ShowVideoInfo(mediaFile);
            lblVideoTitle.Text = mediaFile.Name;

            #endregion
        }

        #endregion

        #region 视频信息

        /// <summary>
        /// 显示视频信息
        /// </summary>
        /// <param name="mediaFile"></param>
        private void ShowVideoInfo(MediaFile mediaFile)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("");
            sb.AppendFormat("General ---------------------------------" + Environment.NewLine);
            sb.AppendFormat("");
            sb.AppendFormat("File Name : {0}" + Environment.NewLine, mediaFile.Name);
            sb.AppendFormat("Format : {0}" + Environment.NewLine, mediaFile.General.Format);
            sb.AppendFormat("Duration : {0}" + Environment.NewLine, mediaFile.General.DurationString);
            sb.AppendFormat("Bitrate : {0}" + Environment.NewLine, mediaFile.General.Bitrate);

            if (mediaFile.Audio.Count > 0)
            {
                sb.AppendFormat("");
                sb.AppendFormat("Audio ---------------------------------" + Environment.NewLine);
                sb.AppendFormat("");
                sb.AppendFormat("Format : {0}" + Environment.NewLine, mediaFile.Audio[0].Format);
                sb.AppendFormat("Bitrate : {0}" + Environment.NewLine, mediaFile.Audio[0].Bitrate);
                sb.AppendFormat("Channels : {0}" + Environment.NewLine, mediaFile.Audio[0].Channels);
                sb.AppendFormat("Sampling : {0}" + Environment.NewLine, mediaFile.Audio[0].SamplingRate);
            }

            if (mediaFile.Video.Count > 0)
            {
                sb.AppendFormat("");
                sb.AppendFormat("Video ---------------------------------" + Environment.NewLine);
                sb.AppendFormat("");
                sb.AppendFormat("Format : {0}" + Environment.NewLine, mediaFile.Video[0].Format);
                sb.AppendFormat("Bit rate : {0}" + Environment.NewLine, mediaFile.Video[0].Bitrate);
                sb.AppendFormat("Frame rate : {0}" + Environment.NewLine, mediaFile.Video[0].FrameRate);
                sb.AppendFormat("Frame size : {0}" + Environment.NewLine, mediaFile.Video[0].FrameSize);
            }

            txtVideoInfo.Text = sb.ToString();
        }

        /// <summary>
        /// 视频简要信息
        /// </summary>
        /// <param name="mediaFile"></param>
        private void ShowBriefInfo(MediaFile mediaFile)
        {
            this.lblVideoProgess.Text = mediaFile.Video[0].FrameSize.Replace("x", " * ") + @" X " +
                                        mediaFile.General.DurationString;
        }

        #endregion

        #region 窗体正在关闭事件

        /// <summary>
        /// 窗体正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.axWindowsMediaPlayer1.close();
            this.axWindowsMediaPlayer1.Dispose();
        }

        #endregion

        #region 全屏播放事件

        /// <summary>
        /// 全屏播放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (this.axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
            {
                if (!axWindowsMediaPlayer1.fullScreen)
                {
                    axWindowsMediaPlayer1.fullScreen = true;
                }
            }
            //必须处于播放状态，才可以设置全屏
        }

        #endregion

        #region 音量调节

        private int oldVolume = 100; //原来的音量
        private int volume = 100; //音量
        private bool silence = true;

        /// <summary>
        /// 静音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (silence)
            {
                volume = 0;
                silence = false;
                axWindowsMediaPlayer1.settings.mute = true;
            }
            else
            {
                volume = oldVolume;
                silence = true;
                axWindowsMediaPlayer1.settings.mute = false;
            }
        }

        /// <summary>
        /// 音量变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbVolume_ValueChanged(object sender, EventArgs e)
        {
            oldVolume = this.tbVolume.Value;
            axWindowsMediaPlayer1.settings.volume = oldVolume;
            lblVideoProgess.Text = oldVolume.ToString(); // 音量
        }

        #endregion

    }
}


//AxWindowsMediaPlayer的详细用法收藏 
//找我把,剛做過,相當熟悉


//属性/方法名： 说明： 
//[基本属性] 　 
//URL:String; 指定媒体位置，本机或网络地址 
//uiMode:String; 播放器界面模式，可为Full, Mini, None, Invisible 
//playState:integer; 播放状态，1=停止，2=暂停，3=播放，6=正在缓冲，9=正在连接，10=准备就绪 
//enableContextMenu:Boolean; 启用/禁用右键菜单 
//fullScreen:boolean; 是否全屏显示 
//[controls] wmp.controls //播放器基本控制 
//controls.play; 播放 
//controls.pause; 暂停 
//controls.stop; 停止 
//controls.currentPosition:double; 当前进度 
//controls.currentPositionString:string; 当前进度，字符串格式。如“00:23” 
//controls.fastForward; 快进 
//controls.fastReverse; 快退 
//controls.next; 下一曲 
//controls.previous; 上一曲 
//[settings] wmp.settings //播放器基本设置 
//settings.volume:integer; 音量，0-100 
//settings.autoStart:Boolean; 是否自动播放 
//settings.mute:Boolean; 是否静音 
//settings.playCount:integer; 播放次数 
//[currentMedia] wmp.currentMedia //当前媒体属性 
//currentMedia.duration:double; 媒体总长度 
//currentMedia.durationString:string; 媒体总长度，字符串格式。如“03:24” 
//currentMedia.getItemInfo(const string); 获取当前媒体信息"Title"=媒体标题，"Author"=艺术家，"Copyright"=版权信息，"Description"=媒体内容描述，"Duration"=持续时间（秒），"FileSize"=文件大小，"FileType"=文件类型，"sourceURL"=原始地址 
//currentMedia.setItemInfo(const string); 通过属性名设置媒体信息 
//currentMedia.name:string; 同 currentMedia.getItemInfo("Title") 
//[currentPlaylist] wmp.currentPlaylist //当前播放列表属性 
//currentPlaylist.count:integer; 当前播放列表所包含媒体数 
//currentPlaylist.Item[integer]; 获取或设置指定项目媒体信息，其子属性同wmp.currentMedia 
//AxWindowsMediaPlayer控件的属性收藏
//MediaPlayer1.Play　　　　　　　　　　播放  
//MediaPlayer1.Stop　　　　　　　　　　停止  
//MediaPlayer1.Pause　　　　　　　　　 暂停  
//MediaPlayer1.PlayCount　　　　　　　　文件播放次数  
//MediaPlayer1.AutoRewind　　　　　　　是否循环播放  
//MediaPlayer1.Balance　　　　　　　　　声道  
//MediaPlayer1.Volume　　　　　　　　　音量  
//MediaPlayer1.Mute　　　　　　　　　　静音  
//MediaPlayer1.EnableContextMenu　　　　是否允许在控件上点击鼠标右键时弹出快捷菜单  
//MediaPlayer1.AnimationAtStart　　　　是否在播放前先播放动画  
//MediaPlayer1.ShowControls　　　　　　是否显示控件工具栏  
//MediaPlayer1.ShowAudioControls　　　　是否显示声音控制按钮  
//MediaPlayer1.ShowDisplay　　　　　　　是否显示数据文件的相关信息  
//MediaPlayer1.ShowGotoBar　　　　　　　是否显示Goto栏  
//MediaPlayer1.ShowPositionControls　　是否显示位置调节按钮  
//MediaPlayer1.ShowStatusBar　　　　　　是否显示状态栏  
//MediaPlayer1.ShowTracker　　　　　　　是否显示进度条  
//MediaPlayer1.FastForward　　　　　　　快进  
//MediaPlayer1.FastReverse　　　　　　　快退  
//MediaPlayer1.Rate　　　　　　　　　　快进／快退速率  
//MediaPlayer1.AllowChangeDisplaySize　是否允许自由设置播放图象大小  
//MediaPlayer1.DisplaySize　　　　　　　设置播放图象大小  
//    1-MpDefaultSize　　　　　　　　　原始大小  
//    2-MpHalfSize　　　　　　　　　　 原始大小的一半  
//    3-MpDoubleSize　　　　　　　　　 原始大小的两倍  
//    4-MpFullScreen　　　　　　　　　 全屏  
//    5-MpOneSixteenthScreen　　　　　 屏幕大小的1/16  
//    6-MpOneFourthScreen　　　　　　　屏幕大小的1/4  
//    7-MpOneHalfScreen　　　　　　　　屏幕大小的1/2  
//MediaPlayer1.ClickToPlay　　　　　　　是否允许单击播放窗口启动Media Player  
 
//http://blog.csdn.net/brouse8079/archive/2007/10/17/1829885.aspx


//2. Ctlcontrols属性
//Ctlcontrols属性是AxWindowsMediaPlayer的一个重
//要属性， 此控件中有许多常用成员。
//(1) 方法play
//用于播放多媒体文件，其格式为：
//窗体名.控件名.Ctlcontrols.play()
//如： AxWindowsMediaPlayer1.Ctlcontrols.play()
//‘此处缺省窗体名是Me
//(2) 方法pause
//用于暂停正在播放的多媒体文件，其格式为：
//窗体名.控件名.Ctlcontrols.pause()
//如： AxWindowsMediaPlayer1.Ctlcontrols.pause()
//(3) 方法stop
//用于停止正在播放的多媒体文件，其格式为：
//窗体名.控件名.Ctlcontrols.stop()
//如： AxWindowsMediaPlayer1.Ctlcontrols.stop()
//(4) 方法fastforward
//用于将正在播放的多媒体文件快进，其格式为：
//窗体名.控件名.Ctlcontrols.fastforward()
//如：
//AxWindowsMediaPlayer1.Ctlcontrols.forward()
//(5) 方法fastreverse
//窗体名.控件名.Ctlcontrols.fastreverse()
//如：
//AxWindowsMediaPlayer1.Ctlcontrols.fastreverse
//()
//6. 属性CurrentPosition
//用于获取多媒体文件当前的播放进度，其值是数值类
//型，使用格式为：
//窗体名.控件名.Ctlcontrols.currentPosition
//d1=AxWindowsMediaPlayer1.Ctlcontrols.currentPosi
//tion
//其中d1 是一个整型变量。
//7. 属性Duration
//用于获取当前多媒体文件的播放的总时间，其值为数
//值类型，其使用格式为：
//窗体名.控件名.currentMedia.duration
//如：d2
//=AxWindowsMediaPlayer1.currentMedia.duration
//其中d2是一个整型变量。
//controls.currentPositionString:string; 当前进
//度，字符串格式。如“00:23”

//属性/方法名： 说明： 
//[基本属性] 　 
//URL:String; 指定媒体位置，本机或网络地址 
//uiMode:String; 播放器界面模式，可为Full, Mini,
//None, Invisible 
//playState:integer; 播放状态，1=停止，2=暂停，
//3=播放，6=正在缓冲，9=正在连接，10=准备就绪
//player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);

//private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
//{
//    // Test the current state of the player and display a message for each state.
//    switch (e.newState)
//    {
//        case 0:    // Undefined
//            currentStateLabel.Text = "Undefined";
//            break;

//        case 1:    // Stopped
//            currentStateLabel.Text = "Stopped";
//            break;

//        case 2:    // Paused
//            currentStateLabel.Text = "Paused";
//            break;

//        case 3:    // Playing
//            currentStateLabel.Text = "Playing";
//            break;

//        case 4:    // ScanForward
//            currentStateLabel.Text = "ScanForward";
//            break;

//        case 5:    // ScanReverse
//            currentStateLabel.Text = "ScanReverse";
//            break;

//        case 6:    // Buffering
//            currentStateLabel.Text = "Buffering";
//            break;

//        case 7:    // Waiting
//            currentStateLabel.Text = "Waiting";
//            break;

//        case 8:    // MediaEnded
//            currentStateLabel.Text = "MediaEnded";
//            break;

//        case 9:    // Transitioning
//            currentStateLabel.Text = "Transitioning";
//            break;

//        case 10:   // Ready
//            currentStateLabel.Text = "Ready";
//            break;

//        case 11:   // Reconnecting
//            currentStateLabel.Text = "Reconnecting";
//            break;

//        case 12:   // Last
//            currentStateLabel.Text = "Last";
//            break;

//        default:
//            currentStateLabel.Text = ("Unknown State: " + e.newState.ToString());
//            break;
//    }
//}
 
 

//enableContextMenu:Boolean; 启用/禁用右键菜单 
//fullScreen:boolean; 是否全屏显示 
//controls.currentPosition:double; 当前进度 
//controls.fastForward; 快进 
//controls.fastReverse; 快退 
//controls.next; 下一曲 
//controls.previous; 上一曲 
//[settings] wmp.settings //播放器基本设置 
//settings.volume:integer; 音量，0-100 
//settings.autoStart:Boolean; 是否自动播放 
//settings.mute:Boolean; 是否静音 
//settings.playCount:integer; 播放次数 
//[currentMedia] wmp.currentMedia //当前媒体属性 
//currentMedia.duration:double; 媒体总长度 
//currentMedia.durationString:string; 媒体总长度
//，字符串格式。如“03:24” 
//currentMedia.getItemInfo(const string); 获取当
//前媒体信息"Title"=媒体标题，"Author"=艺术
//家，"Copyright"=版权信息，"Description"=媒体内
//容描述，"Duration"=持续时间（秒），"FileSize"=
//文件大小，"FileType"=文件类型，"sourceURL"=原
//始地址 
//currentMedia.setItemInfo(const string); 通过属
//性名设置媒体信息 
//currentMedia.name:string; 同
//currentMedia.getItemInfo("Title") 
//[currentPlaylist] wmp.currentPlaylist //当前播
//放列表属性 
//currentPlaylist.count:integer; 当前播放列表所
//包含媒体数 
//currentPlaylist.Item[integer]; 获取或设置指定
//项目媒体信息，其子属性同wmp.currentMedia