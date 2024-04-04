using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hsp.Player.Common;
using Hsp.Test.Common;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace HaiyuMusicPlayer
{
    public partial class FormMusic : Form
    {
        internal string IniFile = string.Empty;
        internal InitHelper Init;
        internal string RootDir = string.Empty;

        /// <summary>
        /// 音频流句柄
        /// </summary>
        //private int _stream = 0;

        /// <summary>
        /// 音乐标题
        /// </summary>
        internal string MusicTitle { get; set; }

        /// <summary>
        /// 窗体标题
        /// </summary>
        internal string FormTitle { get; set; }

        //private TAG_INFO _tagInfo;

        //private int _tickCounter = 0;
        //private int _updateInterval = 50; // 50ms

        //private int _wmaPlugIn = 0;
        //private int _apePlugIn = 0;
        //private int _midPlugIn = 0;


        // LOCAL VARS
        private int _stream = 0;
        private string _fileName = String.Empty;
        private int _tickCounter = 0;
        private DSP_PeakLevelMeter _plm1;
        private DSP_PeakLevelMeter _plm2;
        private Visuals _visModified = new Visuals();
        private bool _fullSpectrum = true;
        private SYNCPROC _sync = null;
        private int _syncer = 0;
        private DSP_Mono _mono;
        private DSP_Gain _gain;
        private DSP_StereoEnhancer _stereoEnh;
        private DSP_IIRDelay _delay;
        private DSP_SoftSaturation _softSat;
        private DSP_StreamCopy _streamCopy;
        private BASS_BFX_DAMP _damp = new BASS_BFX_DAMP();
        private BASS_DX8_COMPRESSOR _comp = new BASS_DX8_COMPRESSOR();
        private int _dampPrio = 3;
        private int _compPrio = 2;
        private int _deviceLatencyMS = 0; // device latency in milliseconds
        private int _deviceLatencyBytes = 0; // device latency in bytes
        private int _updateInterval = 50; // 50ms
        private Un4seen.Bass.BASSTimer _updateTimer = null;
        
        //在播放器里面实现上一曲下一曲的时候用了一个自定义的类PL_Index，专门用来作为索引类的，
        //用这个类去实现上一曲下一曲很方便的，主要是通过获取或设置索引的前驱和后继来运作的

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormMusic()
        {
            InitializeComponent();

            lblMusicTitle.Text = ""; // 音乐标题
            lblPlayProgress.Text = ""; // 播放进度
            lblVolume.Text = ""; // 音量
            lblAudioPath.Text = "";
            lblRate.Text = ""; // 码率

            tsMusic.Hide();
            lblVolume.Hide();
            lblVolumeTitle.Hide();
            lblAudioPath.Hide();

            btnVolumeUp.Hide();
            btnVolumnDown.Hide();
            btnSound.Hide();

            btnPrevious.Enabled = false;

            this.btnVolumeUp.BackColor = System.Drawing.Color.Transparent;
            this.btnVolumnDown.BackColor = System.Drawing.Color.Transparent;

            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 100;
            this.timer1.Start();

            #region 使用原生态的ADO.NET访问SQLite

            //string sql = "SELECT * FROM Audio";
            ////string conStr = "D:/sqlliteDb/document.db";
            string connStr = @"Data Source=" + @"E:\Db\SQLite\MusicList.db;Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
            //string connStr = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString ?? "";

            ////var sqlite = ConfigurationManager.ConnectionStrings["sqlite"];
            ////var a = sqlite;
            ////var b = sqlite.ConnectionString;

            ////string config = System.Configuration.ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;


            //using (SQLiteConnection conn = new SQLiteConnection(connStr))
            //{
            //    ////conn.Open();
            //    using (SQLiteDataAdapter ap = new SQLiteDataAdapter(sql, conn))
            //    {
            //        DataSet ds = new DataSet();
            //        ap.Fill(ds);

            //        DataTable dt = ds.Tables[0];
            //    }
            //}

            #endregion

            #region 使用SQLite.NET访问SQLite

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SQLite.EF6");
            using (DbConnection conn = fact.CreateConnection())
            {
                if (conn == null) return;
                conn.ConnectionString = connStr;
                    //ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
                conn.Open();
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = "select * from Audio";
                comm.CommandType = CommandType.Text;
                using (IDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dd = reader["Id"].ToString();

                        lblAudioPath.Text = reader.Depth.ToString();
                    }

                    DataTable table = new DataTable();
                    table.Load(reader);
                }
            }

            #endregion

            //可以用SQLiteConnection.CreateFile("D:/d.db");直接创建一个数据库文件
            
            #region 按钮提示信息

            ToolTip tip = new ToolTip();
            tip.ShowAlways = true;
            tip.SetToolTip(this.btnAddMusic, "添加音乐");
            tip.SetToolTip(this.btnAddList, "添加音乐列表");

            tip.SetToolTip(this.btnPrevious, "上一曲");
            tip.SetToolTip(this.btnPlay, "播放音乐");
            tip.SetToolTip(this.btnPause, "暂停播放");
            tip.SetToolTip(this.btnStop, "停止播放");
            tip.SetToolTip(this.btnNext, "下一曲");

            tip.SetToolTip(this.btnMute, "静音");
            tip.SetToolTip(this.btnRomdom, "随机播放");
            tip.SetToolTip(this.btnRetweet, "循环播放");

            #endregion

            this.FormTitle = this.Text;

            lblRollingInfo.Left = 6;
            lblRollingInfo.Text = "";
            lblRollingInfo.Hide();

            lblRate.Left = 6;
            lblRate.Hide();

            #region 站点根目录处理

            string strStartupPath = Application.StartupPath; //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称

#if DEBUG
            RootDir = strStartupPath.Replace("\\bin\\Debug", "");
#else
            RootDir = strStartupPath.Replace("\\bin\\Release", "");
#endif

            //LogFile = RootDir + "\\Operation.log";
            //Log = new LogHelper(LogFile);
            IniFile = RootDir + "\\Operation.ini";
            Init = new InitHelper(IniFile);

            Init.IniWriteValue("DefaultValue", "Path", RootDir);
            //Init.IniWriteValue("DefaultValue", "IniFile", IniFile);
            //Init.IniWriteValue("DefaultValue", "Time", DateTime.Now.ToString());

            #endregion
        }

        #region 按钮操作

        /// <summary>
        /// 添加音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMusic_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲", // /设置打开标题
                InitialDirectory = @"E:\Music", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            _fileName = fileDialog.FileName;
            var fi = new FileInfo(_fileName);

            StopStream();

            #region 默认音频目录保存

            lblAudioPath.Text = fi.DirectoryName;
            Init.IniWriteValue("DefaultValue", "Path", fi.DirectoryName); // 默认路径

            #endregion

            //_fileName = fi.FullName;
            //lblMusicTitle.Text = fi.Name;

            //BASS_INFO info = new BASS_INFO();
            //Bass.BASS_GetInfo(info);

            //lblMusicTitle.Text = fi.Name;

            GetMusicTags(); // 音乐标签信息

            #region 根据媒体扩展名选择插件库

            //if (fi.Extension.ToLower() == ".wma")
            //{
            //    _wmaPlugIn = Bass.BASS_PluginLoad("basswma.dll");
            //}
            //if (fi.Extension.ToLower() == ".ape")
            //{
            //    _apePlugIn = Bass.BASS_PluginLoad("bass_ape.dll");
            //}
            //if (fi.Extension.ToLower() == ".mid")
            //{
            //    _midPlugIn = Bass.BASS_PluginLoad("bassmidi.dll");
            //}

            #endregion

            //_stream = Bass.BASS_StreamCreateFile(fi.FullName, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);

            tbProgress.Value = 0; // 进度

            if (_stream == 0) Play(); // 播放

        }

        /// <summary>
        /// 音乐标签信息
        /// </summary>
        private void GetMusicTags()
        {
            #region 标题信息

            MusicTags tags = new MusicTags(_fileName, false);
            var title = tags.Title;

            title += @"[" + tags.Duration + @"]";
            title += @" " + tags.BitRate + @" Kbps ";
            title += @" * " + tags.Digitalisation + @" Hz ";
            if (!string.IsNullOrEmpty(tags.Album)) title += @" * " + tags.Album;
            if (!string.IsNullOrEmpty(tags.Artist)) title += @" * " + tags.Artist;
            title += @" * " + tags.Extension.Replace(".", "");
            if (!string.IsNullOrEmpty(tags.Comment)) title += @" * " + tags.Comment;

            //if (!string.IsNullOrEmpty(tags.AlbumArt)) title += @" * " + tags.AlbumArt;
            if (!string.IsNullOrEmpty(tags.Genre)) title += @" * " + tags.Genre;
            if (!string.IsNullOrEmpty(tags.Year)) title += @" * " + tags.Year;

            lblMusicTitle.Text = title;
            MusicTitle = title;

            lblRate.Text = tags.BitRate + @" Kbps"; // 码率，比特率
            lblRate.Text += @" " + tags.Digitalisation + @" Hz"; // 采样率

            #endregion
        }

        #region 播放参数

        internal string[] playList;
        private int numOfMusic;
        private int currentPlay;
        private List<int> indexList;
        private int currentIndex;

        public int NumOfMusic
        {
            get { return numOfMusic; }
        }

        public void AddFile(string path)
        {
            if (numOfMusic < 1000)
            {
                numOfMusic++;
                playList[numOfMusic] = path;
            }
        }

        public void DelFile(int selectNum)
        {
            for (int i = selectNum; i <= numOfMusic - 1; i++)
            {
                playList[i] = playList[i + 1];
            }
            numOfMusic--;
        }

        public int NextPlay(int type)
        {
            /* type = 0 顺序

            type = 1 重复播放全部
            type = 2 重复播放一首
            type = 3 随机播放

            */

            switch (type)
            {
                case 0:
                    currentPlay++;
                    if (currentPlay > numOfMusic) return 0;
                    else return currentPlay;
                case 1:
                    currentPlay++;
                    if (currentPlay > numOfMusic) return 1;
                    else return currentPlay;
                case 2:
                    return currentPlay;
                case 3:
                    Random rdm = new Random(unchecked((int) DateTime.Now.Ticks));
                    currentPlay = rdm.Next()%numOfMusic;
                    if (currentPlay == 0) return numOfMusic;
                    else return currentPlay;
                default:
                    return 0;
            }
        }

        #endregion

        /// <summary>
        /// 添加音乐列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddList_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog()
            {
                Description = @"请选择歌曲目录"
                , SelectedPath = @"E:\Music"
            };

            if (folderDialog.ShowDialog() != DialogResult.OK) return;

            var folderPath = folderDialog.SelectedPath;
            MusicProcess(folderPath);
            numOfMusic = this.lvwMusic.Items.Count;
            if (numOfMusic <= 0) return;

            indexList = new List<int>();
            currentIndex = 0;

            //for (int i = 0; i < numOfMusic; i++)
            //{
            //    ListViewItem it = this.lvwMusic.Items[i];
            //    indexList.Add(it.Index);
            //}

            foreach (ListViewItem it in this.lvwMusic.Items)
            {
                indexList.Add(it.Index);
            }

            #region 播放列表首曲

            StopStream();

            currentPlay = 1;
            this.lvwMusic.Focus();
            //this.lvwMusic.Items[0].Selected = true;
            ListViewItem item = this.lvwMusic.Items[0];
            item.Selected = true;
            //currentPlay = int.Parse(item.SubItems[1].Text);
            //PlayMusic(new FileInfo(item.SubItems[3].Text));

            #endregion
        }

        #region 选择并播放歌曲

        /// <summary>
        /// 选择并播放歌曲事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMusic.SelectedItems.Count <= 0) return;

            StopStream();

            ListViewItem item = this.lvwMusic.SelectedItems[0];
            //var p = item.SubItems[1].Text + "\n\n" + item.SubItems[2].Text + "\n\n" + item.SubItems[3].Text + "\n";
            //var pp = item.SubItems["lvw_Name"].Text + " * " + item.SubItems["lvw_Path"].Text;

            numOfMusic = lvwMusic.Items.Count;
            currentPlay = int.Parse(item.SubItems[1].Text);

            PlayMusic(new FileInfo(item.SubItems[3].Text));
        }

        #endregion

        /// <summary>
        /// 播放歌曲
        /// </summary>
        /// <param name="fi"></param>
        private void PlayMusic(FileInfo fi)
        {
            _fileName = fi.FullName;

            //MusicTags tags = new MusicTags(fi.FullName, false);
            //lblMusicTitle.Text = tags.Title;
            lblMusicTitle.Left = 6;
            GetMusicTags(); // 音乐标签信息

            //_stream = Bass.BASS_StreamCreateFile(fi.FullName, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);

            tbProgress.Value = 0; // 进度

            if (_stream == 0)
            {
                Play();
            }
        }

        private delegate void AddMusicListView(MusicEntity entity);

        /// <summary>
        /// 歌曲列表
        /// </summary>
        /// <param name="entity"></param>
        private void ShowMusicView(MusicEntity entity)
        {
            if (base.InvokeRequired)
            {
                var method = new AddMusicListView(this.ShowMusicView);
                IAsyncResult result = base.BeginInvoke(method, new object[] { entity });
                try
                {
                    method.EndInvoke(result);
                }
                catch
                {
                }
            }
            else
            {
                this.lvwMusic.BeginUpdate();
                var item = new ListViewItem();

                //item.SubItems["lvw_Path"].Text = "";

                item.SubItems.Add(entity.Idx.ToString(CultureInfo.InvariantCulture));
                item.SubItems.Add(entity.Name);
                item.SubItems.Add(entity.Path);
                item.SubItems.Add(entity.Duration);
                item.SubItems.Add(entity.Ext);
                this.lvwMusic.Items.Add(item);
                this.lvwMusic.EndUpdate();
            }
        }

        /// <summary>
        /// 目录歌曲处理
        /// </summary>
        /// <param name="strPath"></param>
        private void MusicProcess(string strPath)
        {
            // @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)
            //|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"

            #region 检查文件

            foreach (FileInfo fi in new DirectoryInfo(strPath).GetFiles(@"*.mp3"))
            {
                AddMusicList(fi);
            }
            foreach (FileInfo fi in new DirectoryInfo(strPath).GetFiles(@"*.ape"))
            {
                AddMusicList(fi);
            }
            foreach (FileInfo fi in new DirectoryInfo(strPath).GetFiles(@"*.flac"))
            {
                AddMusicList(fi);
            }

            #endregion

            #region 递归检查

            DirectoryInfo[] directories = new DirectoryInfo(strPath).GetDirectories();
            if (directories.Length <= 0) return;
            foreach (DirectoryInfo di in directories)
            {
                this.MusicProcess(di.FullName);
            }

            #endregion
        }

        /// <summary>
        /// 添加歌曲列表
        /// </summary>
        /// <param name="info"></param>
        private void AddMusicList(FileInfo info)
        {
            MusicTags tags = new MusicTags(info.FullName, false);

            var count = lvwMusic.Items.Count;
            var entity = new MusicEntity
            {
                Idx = count + 1,
                Name = info.Name.Replace(info.Extension, ""),
                Title = tags.Title,
                Path = info.FullName,
                Duration = tags.Duration,
                Ext = info.Extension,
                Length = info.Length
            };

            this.ShowMusicView(entity);
        }

        /// <summary>
        /// 添加音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            StopStream();

            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲1",
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            _fileName = fileDialog.FileName;

            var fi = new FileInfo(_fileName);

            #region 默认音频目录保存

            lblAudioPath.Text = fi.DirectoryName;

            #endregion


            //_fileName = fi.FullName;
            //lblMusicTitle.Text = fi.Name;

            //BASS_INFO info = new BASS_INFO();
            //Bass.BASS_GetInfo(info);

            //lblMusicTitle.Text = fi.Name;

            MusicTags tags = new MusicTags(_fileName, false);
            lblMusicTitle.Text = tags.Title;

            #region 根据媒体扩展名选择插件库

            //if (fi.Extension.ToLower() == ".wma")
            //{
            //    _wmaPlugIn = Bass.BASS_PluginLoad("basswma.dll");
            //}
            //if (fi.Extension.ToLower() == ".ape")
            //{
            //    _apePlugIn = Bass.BASS_PluginLoad("bass_ape.dll");
            //}
            //if (fi.Extension.ToLower() == ".mid")
            //{
            //    _midPlugIn = Bass.BASS_PluginLoad("bassmidi.dll");
            //}

            #endregion

            _stream = Bass.BASS_StreamCreateFile(fi.FullName, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);

            tbProgress.Value = 0; // 进度
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        /// <summary>
        /// 歌曲播放
        /// </summary>
        private void Play()
        {
            _updateTimer.Stop();
            Bass.BASS_StreamFree(_stream);
            if (_fileName == String.Empty) return;

            // create the stream
            _stream = Bass.BASS_StreamCreateFile(_fileName, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_SPEAKER_FRONT);
            if (_stream != 0)
            {
                // latency from milliseconds to bytes
                _deviceLatencyBytes = (int) Bass.BASS_ChannelSeconds2Bytes(_stream, _deviceLatencyMS/1000f);

                if (_plm1 != null)
                    _plm1.Notification -= new EventHandler(_plm1_Notification);
                // set up a ready-made DSP (here the PeakLevelMeter)
                _plm1 = new DSP_PeakLevelMeter(_stream, 2000);
                _plm1.CalcRMS = true;
                _plm1.Notification += new EventHandler(_plm1_Notification);
                checkBoxLevel1Bypass_CheckedChanged(this, EventArgs.Empty);

                //checkBoxMono.Checked = false;
                _mono = new DSP_Mono();

                //comboBoxStreamCopy.SelectedIndex = -1;
                //checkBoxStreamCopy.Checked = true;

                _gain = new DSP_Gain(_stream, 0);
                buttonSetGain_Click(this, EventArgs.Empty);
                trackBarGain_ValueChanged(this, EventArgs.Empty);

                _stereoEnh = new DSP_StereoEnhancer(_stream, 0);
                checkBoxStereoEnhancer_CheckedChanged(this, EventArgs.Empty);
                trackBarStereoEnhancerWetDry_ValueChanged(this, EventArgs.Empty);
                trackBarStereoEnhancerWide_ValueChanged(this, EventArgs.Empty);

                _delay = new DSP_IIRDelay(_stream, 0, 2f);
                checkBoxIIRDelay_CheckedChanged(this, EventArgs.Empty);
                trackBarIIRDelay_ValueChanged(this, EventArgs.Empty);
                trackBarIIRDelayWetDry_ValueChanged(this, EventArgs.Empty);
                trackBarIIRDelayFeedback_ValueChanged(this, EventArgs.Empty);

                _softSat = new DSP_SoftSaturation(_stream, 0);
                checkBoxSoftSat_CheckedChanged(this, EventArgs.Empty);
                trackBarSoftSat_ValueChanged(this, EventArgs.Empty);
                trackBarSoftSatDepth_ValueChanged(this, EventArgs.Empty);

                checkBoxDAmp_CheckedChanged(this, EventArgs.Empty);

                checkBoxCompressor_CheckedChanged(this, EventArgs.Empty);
                trackBarCompressor_ValueChanged(this, EventArgs.Empty);

                checkBoxGainDither_CheckedChanged(this, EventArgs.Empty);

                if (_plm2 != null)
                    _plm2.Notification -= new EventHandler(_plm2_Notification);
                _plm2 = new DSP_PeakLevelMeter(_stream, -2000);
                _plm2.CalcRMS = true;
                _plm2.Notification += new EventHandler(_plm2_Notification);
                checkBoxLevel2Bypass_CheckedChanged(this, EventArgs.Empty);
            }

            if (_stream != 0 && Bass.BASS_ChannelPlay(_stream, false))
            {
                // render wave form (this is done in a background thread, so that we already play the channel in parallel)
                //if (this._zoomed)
                //    this.buttonZoom.PerformClick();
                //GetWaveForm();

                //Console.WriteLine("Playing");

                _updateTimer.Start();

                //this.buttonStop.Enabled = true;
                //this.buttonPlay.Enabled = false;
            }
            else
            {
                Console.WriteLine("Error = {0}", Bass.BASS_ErrorGetCode());
            }

            //// create the stream
            //_stream = Bass.BASS_StreamCreateFile(_fileName, 0, 0,
            //    BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
            //if (_stream == 0) return;

            //if (_stream != 0 && Bass.BASS_ChannelPlay(_stream, false))
            //{
            //    this.lblPlayProgress.Text = "";
            //    _updateTimer.Start();

            //    // get some channel info
            //    BASS_CHANNELINFO info = new BASS_CHANNELINFO();
            //    Bass.BASS_ChannelGetInfo(_stream, info);
            //    this.lblPlayProgress.Text += "Info: " + info.ToString() + Environment.NewLine;

            //    // display the channel info..
            //    this.lblPlayProgress.Text += String.Format("Type={0}, Channels={1}, OrigRes={2}", Utils.BASSChannelTypeToString(info.ctype), info.chans, info.origres);

            //    // display the tags...
            //    TAG_INFO tagInfo = new TAG_INFO(_fileName);
            //    var i = tagInfo.encodedby.Length;

            //    BassTags.EvalNativeTAGs = true;

            //    if (BassTags.BASS_TAG_GetFromFile(_stream, tagInfo))
            //    {
            //        // and display what we get
            //        //this.textBoxAlbum.Text = tagInfo.album;
            //        //this.textBoxArtist.Text = tagInfo.artist;
            //        //this.textBoxTitle.Text = tagInfo.title;
            //        //this.textBoxComment.Text = tagInfo.comment;
            //        //this.textBoxGenre.Text = tagInfo.genre;
            //        //this.textBoxYear.Text = tagInfo.year;
            //        //this.textBoxTrack.Text = tagInfo.track;
            //        //this.pictureBoxTagImage.Image = tagInfo.PictureGetImage(0);
            //        //this.textBoxPicDescr.Text = tagInfo.PictureGetDescription(0);
            //        //if (this.textBoxPicDescr.Text == String.Empty)
            //        //    this.textBoxPicDescr.Text = tagInfo.PictureGetType(0);

            //        var strMusicInfo = tagInfo.album + Environment.NewLine;
            //        strMusicInfo += tagInfo.artist + Environment.NewLine;
            //        strMusicInfo += tagInfo.title + Environment.NewLine;
            //        strMusicInfo += tagInfo.genre + Environment.NewLine;
            //        strMusicInfo += tagInfo.year + Environment.NewLine;
            //        strMusicInfo += tagInfo.track + Environment.NewLine;
            //        strMusicInfo += tagInfo.PictureGetImage(0) + Environment.NewLine;
            //        strMusicInfo += tagInfo.PictureGetDescription(0) + Environment.NewLine;

            //        strMusicInfo += tagInfo.PictureGetType(0) + Environment.NewLine;
            //        //strMusicInfo += tagInfo.album + Environment.NewLine;
            //        //strMusicInfo += tagInfo.album + Environment.NewLine;
            //        //strMusicInfo += tagInfo.album + Environment.NewLine;
            //        //strMusicInfo += tagInfo.album + Environment.NewLine;

            //        //this.txtMusicInfo.Text = strMusicInfo;
            //    }

            //    //this.btnStop.Enabled = true;
            //    //this.btnPlay.Enabled = false;
            //    //this.btnPause.Enabled = true;
            //}
            //else
            //{
            //    this.lblPlayProgress.Text = @"Error=" + Bass.BASS_ErrorGetCode();
            //}
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPause_Click(object sender, EventArgs e)
        {
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_ChannelPause(_stream);
            }
            else
            {
                Bass.BASS_ChannelPlay(_stream, false);
            }
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            StopStream();
        }

        /// <summary>
        /// 停止音乐
        /// </summary>
        private void StopStream()
        {
            _updateTimer.Stop();
            // kills rendering, if still in progress, e.g. if a large file was selected
            if (WF != null && WF.IsRenderingInProgress)
                WF.RenderStop();
            DrawWavePosition(-1, -1);

            Bass.BASS_StreamFree(_stream);
            _stream = 0;

            this.lblPlayProgress.Text = @"播放停止";

            //this.button1.Text = "Select a file to play (e.g. MP3, OGG or WAV)...";
            //this.buttonStop.Enabled = false;
            //this.buttonPlay.Enabled = true;
        }

        #endregion

        #region 窗体关闭事件

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMusic_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer.Tick -= new EventHandler(timerUpdate_Tick);
            //_updateTimer.Stop();

            Bass.BASS_ChannelStop(_stream); // 停止播放
            Bass.BASS_StreamFree(_stream); // 释放音频流
            Bass.BASS_PluginFree(0); // 释放所有插件

            //Bass.BASS_StreamFree(_mixer);

            Bass.BASS_Stop(); // 停止所有输出
            Bass.BASS_Free(); // 释放所有资源

            #region 默认音频目录保存

            App.Default.DefaultPath = lblAudioPath.Text.Length > 0 ? lblAudioPath.Text : App.Default.DefaultPath;
            if (App.Default.RunningRecord == null) App.Default.RunningRecord = new ArrayList();
            App.Default.RunningRecord.Add(DateTime.Now);
            App.Default.Save();

            #endregion
        }

        #endregion

        #region 窗体加载事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMusic_Load(object sender, EventArgs e)
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_CPSPEAKERS, this.Handle))
            {
                MessageBox.Show(@"Bass初始化出错！" + Bass.BASS_ErrorGetCode().ToString());
            }

            _info = Bass.BASS_GetInfo();
            _deviceLatencyMS = _info.latency;

            var targetPath = Application.StartupPath;

            var loadedPlugIns = Bass.BASS_PluginLoadDirectory(targetPath);

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, 200);
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 20);

            // init some FX
            _comp.Preset_Soft();
            _damp.Preset_Medium();

            // create a secure timer
            _updateTimer = new BASSTimer(_updateInterval);
            _updateTimer.Tick += new EventHandler(timerUpdate_Tick);

            btnPlay.Click += new EventHandler(tsbPlay_Click);
            btnPause.Click += new EventHandler(tsbPause_Click);
            btnStop.Click += new EventHandler(tsbStop_Click);
            //btnAddList.Click += new EventHandler(tsbAdd_Click);

            _sync = new SYNCPROC(SetPosition);

            _visModified.MaxFFT = BASSData.BASS_DATA_FFT1024;
            _visModified.MaxFrequencySpectrum = Utils.FFTFrequency2Index(16000, 1024, 44100);

            //comboBoxStreamCopy.Items.AddRange(Bass.BASS_GetDeviceInfos());
            //comboBoxStreamCopy.SelectedIndex = -1;


            //// already create a mixer
            //_mixer = BassMix.BASS_Mixer_StreamCreate(44100, 2, BASSFlag.BASS_SAMPLE_FLOAT);
            //if (_mixer == 0)
            //{
            //    MessageBox.Show(this, "Could not create mixer!");
            //    Bass.BASS_Free();
            //    this.Close();
            //    return;
            //}

            //_mixerStallSync = new SYNCPROC(OnMixerStall);
            //Bass.BASS_ChannelSetSync(_mixer, BASSSync.BASS_SYNC_STALL, 0L, _mixerStallSync, IntPtr.Zero);

            //timerUpdate.Start();
            //Bass.BASS_ChannelPlay(_mixer, false);


            oldVolume = Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM)/100; // 音量

            tbVolume.Value = oldVolume;
            lblVolume.Text = oldVolume.ToString(); // 音量

            Init.IniWriteValue("DefaultValue", "Volume", oldVolume.ToString()); // 音量
        }

        #endregion

        #region 计时器显示歌曲进度及长度

        //PointF p;
        //Font ft = new Font("宋体", 10);
        //string temp, text;

        //private int paintX;

        private bool toLeft = true;
        private int space = 2;

        /// <summary>
        /// 计时器显示歌曲进度及长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 标题滚动

            var iContentWidth = this.Width;
            if (iContentWidth >= lblMusicTitle.Width) return;

            if (toLeft)
            {
                // 从右向左滚动
                if (lblMusicTitle.Right > iContentWidth - 25)
                {
                    lblMusicTitle.Left -= space; //设置label左边缘与其容器的工作区左边缘之间的距离                
                }
                else
                {
                    toLeft = false;
                }
            }
            else
            {
                // 从左向右滚动
                if (lblMusicTitle.Left < 10)
                {
                    lblMusicTitle.Left += space;
                }
                else
                {
                    toLeft = true;
                }
            }

            #endregion

            lblRollingInfo.Text = @"窗体宽度：" + iContentWidth + @", Label宽度：" + lblMusicTitle.Width;
            lblRollingInfo.Text += @", 左边坐标：" + lblMusicTitle.Left;
            lblRollingInfo.Text += @", 右边坐标：" + lblMusicTitle.Right;
            lblRollingInfo.Text += @", 移动间距：" + (iContentWidth - lblMusicTitle.Right);

            //if (string.IsNullOrEmpty(MusicTitle)) return;
            //paintX = (++paintX) % lblMusicTitle.Width;
            //lblMusicTitle.Invalidate();

            //Brush brush = Brushes.Blue;
            //text = MusicTitle; //滚动字幕内容  
            //Graphics g = this.lblMusicTitle.CreateGraphics();
            //var s = g.MeasureString(text, ft);
            //g.Clear(BackColor);//清除背景     
            //if (temp != text)//文字改变时,重新显示     
            //{
            //    p = new PointF(this.lblMusicTitle.Size.Width, 0);
            //    temp = text;
            //}
            //else
            //    p = new PointF(p.X - 5, 0);//每次偏移10     
            //if (p.X <= -s.Width)
            //    p = new PointF(this.lblMusicTitle.Size.Width, 0);
            //g.DrawString(text, ft, brush, p);  


            //Graphics g = this.lblMusicTitle.CreateGraphics();  

            ////创建滚动内容
            //var a = this.Width - 10;
            //string strNull = "  "; //占位空格
            //string title = MusicTitle;  //读取
            //for (int i = 0; i < this.lblMusicTitle.Width / 10; i++)
            //{
            //    strNull += "  ";
            //}
            //title = strNull + title;
            ////实现滚动效果
            //if (title.Length >= a)
            //{
            //    this.lblMusicTitle.Text = title.Substring(a, title.Length - a);
            //    a++;
            //}
            //else
            //{
            //    a = 0;
            //}


            //long pos = BassMix.BASS_Mixer_ChannelGetPosition(_currentTrack.Channel);
            //long len = Bass.BASS_ChannelGetLength(_currentTrack.Channel); // length in bytes

            ////labelTime.Text = Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, pos), "HHMMSS");
            ////labelRemain.Text = Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, _currentTrack.TrackLength - pos), "HHMMSS");

            //double totaltime = Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, len); // the total time length
            //double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, pos); // the elapsed time length
            //double remainingtime = totaltime - elapsedtime;
            //this.lblMusicInfo.Text = String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));
            //this.Text = String.Format("Bass-CPU: {0:0.00}% (not including Waves & Spectrum!)", Bass.BASS_GetCPU());

            //this.lblMusicInfo.Text = String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));

        }

        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            // here we gather info about the stream, when it is playing...
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_STOPPED)
            {
                // the stream is NOT playing anymore...
                //StopStream();
                return;
            }

            // from here on, the stream is for sure playing...
            _tickCounter++;
            long pos = Bass.BASS_ChannelGetPosition(_stream); // position in bytes
            long len = Bass.BASS_ChannelGetLength(_stream); // length in bytes

            #region 进度

            var ll = (int) ((pos*1.0)/(len*1.0)*100);
            tbProgress.Value = (int) ((pos*1.0)/(len*1.0)*100); // 进度

            lblAudioPath.Text = pos.ToString() + " * " + len.ToString() + " * " + ll;

            #endregion

            if (_tickCounter == 20)
            {
                _tickCounter = 0;
                // reset the peak level every 1000ms (since timer is 50ms)
                if (_plm1 != null)
                    _plm1.ResetPeakHold();
                if (_plm2 != null)
                    _plm2.ResetPeakHold();
            }
            if (_tickCounter % 5 == 0)
            {
                // display the position every 250ms (since timer is 50ms)
                double totaltime = Bass.BASS_ChannelBytes2Seconds(_stream, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_stream, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
                this.lblPlayProgress.Text = String.Format("{0:#0.00} / {1:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"));
                    //String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));
                this.Text = this.FormTitle + " " + String.Format("Bass-CPU: {0:0.00}%", Bass.BASS_GetCPU()); //(not including Waves & Spectrum!)
            }

            // update the wave position
            DrawWavePosition(pos, len);
            if (_fullSpectrum)
                this.pictureBoxSpectrum.Image = _visModified.CreateSpectrumLinePeak(_stream, this.pictureBoxSpectrum.Width, this.pictureBoxSpectrum.Height, Color.Wheat, Color.Gold, Color.DarkOrange, Color.Black, 2, 1, 1, 13, false, true, false);
            else
                this.pictureBoxSpectrum.Image = _visModified.CreateWaveForm(_stream, this.pictureBoxSpectrum.Width, this.pictureBoxSpectrum.Height, Color.Green, Color.Red, Color.Gray, Color.Linen, 1, true, false, true);
        }
        #endregion

        //private void pictureBoxSpectrum_Click(object sender, System.EventArgs e)
        //{
        //    _fullSpectrum = !_fullSpectrum;
        //}

        #region Wave Form

        // zoom helper varibales
        private bool _zoomed = false;
        private int _zoomStart = -1;
        private long _zoomStartBytes = -1;
        private int _zoomEnd = -1;
        private float _zoomDistance = 5.0f; // zoom = 5sec.

        private Un4seen.Bass.Misc.WaveForm WF = null;
        private void GetWaveForm()
        {
            // render a wave form2
            WF = new WaveForm(this._fileName, new WAVEFORMPROC(MyWaveFormCallback), this);
            WF.FrameResolution = 0.005f; // 5ms are very nice
            WF.CallbackFrequency = 4000; // every 20 seconds rendered (4000*5ms=20sec)
            WF.ColorBackground = SystemColors.Control;
            WF.ColorLeft = Color.Gainsboro;
            WF.ColorLeftEnvelope = Color.Gray;
            WF.ColorRight = Color.LightGray;
            WF.ColorRightEnvelope = Color.DimGray;
            WF.ColorMarker = Color.DarkBlue;
            WF.DrawWaveForm = WaveForm.WAVEFORMDRAWTYPE.Stereo;
            WF.DrawMarker = WaveForm.MARKERDRAWTYPE.Line | WaveForm.MARKERDRAWTYPE.Name | WaveForm.MARKERDRAWTYPE.NamePositionAlternate;
            WF.RenderStart(true, BASSFlag.BASS_DEFAULT);
            WF.SyncPlayback(_stream);
        }

        private void MyWaveFormCallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            // will be called during rendering...
            DrawWave();
            if (finished)
            {
                Console.WriteLine("Finished rendering in {0}sec.", elapsedTime);
                Console.WriteLine("FramesRendered={0} of {1}", WF.FramesRendered, WF.FramesToRender);
                // eg.g use this to save the rendered wave form...
                //WF.WaveFormSaveToFile( Path.ChangeExtension(_fileName, ".wf") );
            }
        }

        private void pictureBox1_Resize(object sender, System.EventArgs e)
        {
            DrawWave();
        }

        private void DrawWave()
        {
            //if (WF != null)
            //    this.pictureBox1.BackgroundImage = WF.CreateBitmap(this.pictureBox1.Width, this.pictureBox1.Height, _zoomStart, _zoomEnd, true);
            //else
            //    this.pictureBox1.BackgroundImage = null;
        }

        private void DrawWavePosition(long pos, long len)
        {
            //// Note: we might take the latency of the device into account here!
            //// so we show the position as heard, not played.
            //// That's why we called Bass.Bass_Init with the BASS_DEVICE_LATENCY flag
            //// and then used the BASS_INFO structure to get the latency of the device

            //if (len == 0 || pos < 0)
            //{
            //    this.pictureBox1.Image = null;
            //    return;
            //}

            //Bitmap bitmap = null;
            //Graphics g = null;
            //Pen p = null;
            //double bpp = 0;

            //try
            //{
            //    if (_zoomed)
            //    {
            //        // total length doesn't have to be _zoomDistance sec. here
            //        len = WF.Frame2Bytes(_zoomEnd) - _zoomStartBytes;

            //        int scrollOffset = 1;
            //        // if we scroll out the window...(scrollOffset*20ms before the zoom window ends)
            //        if (pos > (_zoomStartBytes + len - scrollOffset * WF.Wave.bpf))
            //        {
            //            // we 'scroll' our zoom with a little offset
            //            _zoomStart = WF.Position2Frames(pos - scrollOffset * WF.Wave.bpf);
            //            _zoomStartBytes = WF.Frame2Bytes(_zoomStart);
            //            _zoomEnd = _zoomStart + WF.Position2Frames(_zoomDistance) - 1;
            //            if (_zoomEnd >= WF.Wave.data.Length)
            //            {
            //                // beyond the end, so we zoom from end - _zoomDistance.
            //                _zoomEnd = WF.Wave.data.Length - 1;
            //                _zoomStart = _zoomEnd - WF.Position2Frames(_zoomDistance) + 1;
            //                if (_zoomStart < 0)
            //                    _zoomStart = 0;
            //                _zoomStartBytes = WF.Frame2Bytes(_zoomStart);
            //                // total length doesn't have to be _zoomDistance sec. here
            //                len = WF.Frame2Bytes(_zoomEnd) - _zoomStartBytes;
            //            }
            //            // get the new wave image for the new zoom window
            //            DrawWave();
            //        }
            //        // zoomed: starts with _zoomStartBytes and is _zoomDistance long
            //        pos -= _zoomStartBytes; // offset of the zoomed window

            //        bpp = len / (double)this.pictureBox1.Width;  // bytes per pixel
            //    }
            //    else
            //    {
            //        // not zoomed: width = length of stream
            //        bpp = len / (double)this.pictureBox1.Width;  // bytes per pixel
            //    }

            //    // we take the device latency into account
            //    // Not really needed, but if you have a real slow device, you might need the next line
            //    // so the BASS_ChannelGetPosition might return a position ahead of what we hear
            //    pos -= _deviceLatencyBytes;

            //    p = new Pen(Color.Red);
            //    bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            //    g = Graphics.FromImage(bitmap);
            //    g.Clear(Color.White);
            //    int x = (int)Math.Round(pos / bpp);  // position (x) where to draw the line
            //    g.DrawLine(p, x, 0, x, this.pictureBox1.Height - 1);
            //    bitmap.MakeTransparent(Color.White);
            //}
            //catch
            //{
            //    bitmap = null;
            //}
            //finally
            //{
            //    // clean up graphics resources
            //    if (p != null)
            //        p.Dispose();
            //    if (g != null)
            //        g.Dispose();
            //}

            //this.pictureBox1.Image = bitmap;
        }

        private void buttonZoom_Click(object sender, System.EventArgs e)
        {
            //if (WF == null)
            //    return;

            //// WF is not null, so the stream must be playing...
            //if (_zoomed)
            //{
            //    // unzoom...(display the whole wave form)
            //    _zoomStart = -1;
            //    _zoomStartBytes = -1;
            //    _zoomEnd = -1;
            //    _zoomDistance = 5.0f; // zoom = 5sec.
            //}
            //else
            //{
            //    // zoom...(display only a partial wave form)
            //    long pos = Bass.BASS_ChannelGetPosition(this._stream);
            //    // calculate the window to display
            //    _zoomStart = WF.Position2Frames(pos);
            //    _zoomStartBytes = WF.Frame2Bytes(_zoomStart);
            //    _zoomEnd = _zoomStart + WF.Position2Frames(_zoomDistance) - 1;
            //    if (_zoomEnd >= WF.Wave.data.Length)
            //    {
            //        // beyond the end, so we zoom from end - _zoomDistance.
            //        _zoomEnd = WF.Wave.data.Length - 1;
            //        _zoomStart = _zoomEnd - WF.Position2Frames(_zoomDistance) + 1;
            //        _zoomStartBytes = WF.Frame2Bytes(_zoomStart);
            //    }
            //}
            //_zoomed = !_zoomed;
            //// and display this new wave form
            //DrawWave();
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (WF == null)
            //    return;

            //if (e.Button == MouseButtons.Left)
            //{
            //    long pos = WF.GetBytePositionFromX(e.X, this.pictureBox1.Width, _zoomStart, _zoomEnd);
            //    // set Start marker
            //    WF.AddMarker("START", pos);
            //    Bass.BASS_ChannelSetPosition(_stream, pos);
            //    if (WF.Wave.marker.ContainsKey("END"))
            //    {
            //        long endpos = WF.Wave.marker["END"];
            //        if (endpos < pos)
            //        {
            //            WF.RemoveMarker("END");
            //        }
            //    }
            //    DrawWave();
            //}
            //else if (e.Button == MouseButtons.Right)
            //{
            //    long pos = WF.GetBytePositionFromX(e.X, this.pictureBox1.Width, _zoomStart, _zoomEnd);
            //    // set End marker
            //    WF.AddMarker("END", pos);
            //    Bass.BASS_ChannelRemoveSync(_stream, _syncer);
            //    _syncer = Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_POS, pos, _sync, IntPtr.Zero);
            //    if (WF.Wave.marker.ContainsKey("START"))
            //    {
            //        long startpos = WF.Wave.marker["START"];
            //        if (startpos > pos)
            //        {
            //            WF.RemoveMarker("START");
            //        }
            //    }
            //    DrawWave();
            //}
        }

        private void SetPosition(int handle, int channel, int data, IntPtr user)
        {
            //if (WF.Wave.marker.ContainsKey("START"))
            //{
            //    long startpos = WF.Wave.marker["START"];
            //    Bass.BASS_ChannelSetPosition(_stream, (long)startpos);
            //    if (_zoomed)
            //    {
            //        _zoomStart = WF.Position2Frames((long)startpos) - 1;
            //        _zoomStartBytes = WF.Frame2Bytes(_zoomStart);
            //        if (WF.Wave.marker.ContainsKey("END"))
            //        {
            //            long endpos = WF.Wave.marker["END"];
            //            _zoomEnd = WF.Position2Frames((long)endpos) + 10;
            //            _zoomDistance = WF.Frame2Bytes(_zoomEnd) - WF.Frame2Bytes(_zoomStart);
            //        }
            //        DrawWave();
            //    }
            //}
            //else
            //    Bass.BASS_ChannelSetPosition(_stream, 0);

        }

        #endregion

        #region PeakLevelMeter

        private void _plm1_Notification(object sender, EventArgs e)
        {
            //try
            //{
            //    // sender will be the DSP_PeakLevelMeter instance
            //    // you could also access it via: DSP_PeakLevelMeter plm = (DSP_PeakLevelMeter)sender;
            //    this.progressBarPeak1Left.Value = _plm1.LevelL;
            //    this.progressBarPeak1Right.Value = _plm1.LevelR;
            //    this.labelLevel1.Text = String.Format("RMS: {0:#00.0} dB    AVG: {1:#00.0} dB    Peak: {2:#00.0} dB", _plm1.RMS_dBV, _plm1.AVG_dBV, Math.Max(_plm1.PeakHoldLevelL_dBV, _plm1.PeakHoldLevelR_dBV));
            //}
            //catch { }
        }

        private void _plm2_Notification(object sender, EventArgs e)
        {
            //try
            //{
            //    // sender will be the DSP_PeakLevelMeter instance
            //    // you could also access it via: DSP_PeakLevelMeter plm = (DSP_PeakLevelMeter)sender;
            //    this.progressBarPeak2Left.Value = _plm2.LevelL;
            //    this.progressBarPeak2Right.Value = _plm2.LevelR;
            //    this.labelLevel2.Text = String.Format("RMS: {0:#00.0} dB    AVG: {1:#00.0} dB    Peak: {2:#00.0} dB", _plm2.RMS_dBV, _plm2.AVG_dBV, Math.Max(_plm2.PeakHoldLevelL_dBV, _plm2.PeakHoldLevelR_dBV));
            //    // display the effect
            //    int effect = Math.Max(_plm2.LevelL, _plm2.LevelR) - Math.Max(_plm1.LevelL, _plm1.LevelR);
            //    this.trackBarEffect.Value = effect;
            //}
            //catch { }
        }

        private void checkBoxLevel1Bypass_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_plm1 != null)
            //    _plm1.SetBypass(!checkBoxLevel1Bypass.Checked);
        }

        private void checkBoxLevel2Bypass_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_plm2 != null)
            //    _plm2.SetBypass(!checkBoxLevel2Bypass.Checked);
        }

        #endregion PeakLevelMeter

        #region DSP_Gain

        private void buttonSetGain_Click(object sender, System.EventArgs e)
        {
            //if (_gain != null)
            //{
            //    try
            //    {
            //        double gainDB = double.Parse(this.textBoxGainDBValue.Text);
            //        if (gainDB == 0.0)
            //            _gain.SetBypass(true);
            //        else
            //        {
            //            _gain.SetBypass(false);
            //            _gain.Gain_dBV = gainDB;
            //        }
            //        trackBarGain.Value = (int)(gainDB * 1000d);
            //    }
            //    catch { }
            //}
        }

        private void trackBarGain_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_gain != null)
            //    this.textBoxGainDBValue.Text = Convert.ToString(trackBarGain.Value / 1000d);
            //buttonSetGain_Click(this, EventArgs.Empty);
        }

        private void checkBoxGainDither_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_gain != null)
            //{
            //    _gain.UseDithering = checkBoxGainDither.Checked;
            //}
            //if (_stereoEnh != null)
            //{
            //    _stereoEnh.UseDithering = checkBoxGainDither.Checked;
            //}
        }

        #endregion DSP_Gain

        #region DynAmp

        private int _dampFX = 0;
        private void checkBoxDAmp_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_stream == 0)
            //    return;

            //if (checkBoxDAmp.Checked)
            //{
            //    _dampFX = Bass.BASS_ChannelSetFX(_stream, BASSFXType.BASS_FX_BFX_DAMP, _dampPrio);
            //    Bass.BASS_FXSetParameters(_dampFX, _damp);

            //}
            //else
            //{
            //    Bass.BASS_ChannelRemoveFX(_stream, _dampFX);
            //    _dampFX = 0;
            //}
        }

        #endregion DynAmp

        #region Compressor

        private int _compFX = 0;
        private void checkBoxCompressor_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_stream == 0)
            //    return;

            //if (checkBoxCompressor.Checked)
            //{
            //    _compFX = Bass.BASS_ChannelSetFX(_stream, BASSFXType.BASS_FX_DX8_COMPRESSOR, _compPrio);
            //    Bass.BASS_FXSetParameters(_compFX, _comp);

            //}
            //else
            //{
            //    Bass.BASS_ChannelRemoveFX(_stream, _compFX);
            //    _compFX = 0;
            //}
        }

        private void trackBarCompressor_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_stream == 0)
            //    return;

            //_comp.fThreshold = (float)Utils.DBToLevel(trackBarCompressor.Value / 10d, 1.0);
            //Bass.BASS_FXSetParameters(_compFX, _comp);

            //labelCompThreshold.Text = String.Format("Threshold: {0:#0.0} dB", trackBarCompressor.Value / 10d);
        }

        #endregion Compressor

        #region StereoEnhancer

        private void checkBoxStereoEnhancer_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_stereoEnh != null)
            //    _stereoEnh.SetBypass(!checkBoxStereoEnhancer.Checked);
        }

        private void trackBarStereoEnhancerWide_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_stereoEnh != null)
            //    _stereoEnh.WideCoeff = trackBarStereoEnhancerWide.Value / 100d;
            //labelStereoEnhancer.Text = String.Format("Wide: {0:#0.00}, {1:#0.00}", trackBarStereoEnhancerWide.Value / 100d, trackBarStereoEnhancerWetDry.Value / 100d);
        }

        private void trackBarStereoEnhancerWetDry_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_stereoEnh != null)
            //    _stereoEnh.WetDry = trackBarStereoEnhancerWetDry.Value / 100d;
            //labelStereoEnhancer.Text = String.Format("Wide: {0:#0.00} / {1:#0.00}", trackBarStereoEnhancerWide.Value / 100d, trackBarStereoEnhancerWetDry.Value / 100d);
        }

        #endregion StereoEnhancer

        #region Mono

        private void checkBoxMono_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_stream == 0)
            //    return;

            //if (_mono.IsAssigned)
            //{
            //    _mono.Stop();
            //}
            //else
            //{
            //    _mono.ChannelHandle = _stream;
            //    _mono.DSPPriority = 0;
            //    _mono.UseDithering = true;
            //    _mono.Invert = checkBoxMonoInvert.Checked;
            //    _mono.Start();
            //}
        }

        private void checkBoxMonoInvert_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_mono != null)
            //    _mono.Invert = checkBoxMonoInvert.Checked;
        }

        #endregion Mono

        #region IIR Delay

        private void checkBoxIIRDelay_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_delay != null)
            //    _delay.SetBypass(!checkBoxIIRDelay.Checked);
        }

        private void trackBarIIRDelay_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_delay != null)
            //    _delay.Delay = trackBarIIRDelay.Value;
            //labelIIRDelay.Text = String.Format("Delay: {0} samples", trackBarIIRDelay.Value);
        }

        private void trackBarIIRDelayWetDry_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_delay != null)
            //    _delay.WetDry = trackBarIIRDelayWet.Value / 100d;
        }

        private void trackBarIIRDelayFeedback_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_delay != null)
            //    _delay.Feedback = trackBarIIRDelayFeedback.Value / 100d;
        }

        #endregion IIR Delay

        #region Soft Saturation

        private void checkBoxSoftSat_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_softSat != null)
            //    _softSat.SetBypass(!checkBoxSoftSat.Checked);
        }

        private void trackBarSoftSat_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_softSat != null)
            //    _softSat.Factor = trackBarSoftSat.Value / 100d;
        }

        private void trackBarSoftSatDepth_ValueChanged(object sender, System.EventArgs e)
        {
            //if (_softSat != null)
            //    _softSat.Depth = trackBarSoftSatDepth.Value / 100d;
        }

        #endregion Soft Saturation

        #region Stream Copy

        private BASS_INFO _info = Bass.BASS_GetInfo();
        private void comboBoxStreamCopy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //if (_streamCopy != null)
            //{
            //    _streamCopy.Stop();
            //    _streamCopy = null;
            //    return;
            //}

            //int dev = comboBoxStreamCopy.SelectedIndex;
            //if (_stream != 0)
            //{
            //    if (!Bass.BASS_Init(dev, 44100, BASSInit.BASS_DEVICE_LATENCY, this.Handle))
            //        Bass.BASS_SetDevice(dev); // already?!
            //    _info = Bass.BASS_GetInfo();

            //    // add the stream copy option
            //    _streamCopy = new DSP_StreamCopy();
            //    _streamCopy.OutputLatency = _info.latency;
            //    _streamCopy.ChannelHandle = _stream;
            //    _streamCopy.DSPPriority = checkBoxStreamCopy.Checked ? -4000 : 4000;
            //    _streamCopy.StreamCopyDevice = dev;
            //    //_streamCopy.StreamCopyFlags = BASSFlag.BASS_SPEAKER_REAR;
            //    _streamCopy.Start();
            //}
        }

        private void checkBoxStreamCopy_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (_streamCopy != null)
            //{
            //    _streamCopy.DSPPriority = checkBoxStreamCopy.Checked ? -4000 : 4000;
            //}
        }

        #endregion Stream Copy

        #region 音量处理

        private int oldVolume = 100; //原来的音量
        private int volume = 100; //音量
        private bool silence = true;

        /// <summary>
        /// 静音按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (silence)
            {
                volume = 0;
                silence = false;
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, volume*100);
            }
            else
            {
                volume = oldVolume;
                silence = true;
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, volume*100);
            }
            //OnVolumeChanged(new MusicEventArgs(PlayState, volume, Schedule, false));

            //this.tbVolume.Value = volume;
        }

        private void tbVolume_ValueChanged(object sender, EventArgs e)
        {
            oldVolume = this.tbVolume.Value;
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, oldVolume*100);
            //lblVolume.Text = oldVolume.ToString(); // 音量
            lblPlayProgress.Text = oldVolume.ToString(); // 音量
        }

        /// <summary>
        /// 降低音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVolumnDown_Click(object sender, EventArgs e)
        {
            oldVolume = this.tbVolume.Value;

            if (oldVolume == 0)
            {
                volume = oldVolume;
            }
            else if (oldVolume < 11)
            {
                volume = oldVolume - 1;
            }
            else
            {
                volume = oldVolume - 5;
            }

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, volume*100);
            this.tbVolume.Value = volume;
            //lblVolume.Text = volume.ToString(); // 音量
            lblPlayProgress.Text = oldVolume.ToString(); // 音量

            oldVolume = volume;
        }

        /// <summary>
        /// 增加音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVolumeUp_Click(object sender, EventArgs e)
        {
            oldVolume = this.tbVolume.Value;

            if (oldVolume == 100)
            {
                volume = oldVolume;
            }
            else if (oldVolume < 10 || oldVolume > 89)
            {
                volume = oldVolume + 1;
            }
            else
            {
                volume = oldVolume + 5;
            }

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, volume*100);
            this.tbVolume.Value = volume;
            //lblVolume.Text = volume.ToString(); // 音量
            lblPlayProgress.Text = oldVolume.ToString(); // 音量

            oldVolume = volume;
        }

        private void tbVolume_MouseHover(object sender, EventArgs e)
        {
            _updateTimer.Stop();
            lblPlayProgress.Text = tbVolume.Value.ToString();
        }

        private void tbVolume_MouseLeave(object sender, EventArgs e)
        {
            _updateTimer.Start();
        }

        #endregion

        ///// <summary>
        ///// 音量改变事件
        ///// </summary>
        //public event EventHandler<MusicEventArgs> VolumeChanged;
        ///// <summary>
        ///// 触发音量改变事件
        ///// </summary>
        ///// <param name="e"></param>
        //protected virtual void OnVolumeChanged(MusicEventArgs e)
        //{
        //    if (VolumeChanged != null)
        //    {
        //        VolumeChanged(this, e);
        //    }
        //}

        #region 歌曲实体

        /// <summary>
        /// 歌曲实体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MusicEntity
        {
            /// <summary>
            /// 序号
            /// </summary>
            public int Idx;

            /// <summary>
            /// 歌曲名称
            /// </summary>
            public string Name;

            /// <summary>
            /// 歌曲标题
            /// </summary>
            public string Title;

            /// <summary>
            /// 歌曲路径
            /// </summary>
            public string Path;

            /// <summary>
            /// 歌曲时长
            /// </summary>
            public string Duration;

            /// <summary>
            /// 扩展名称
            /// </summary>
            public string Ext;

            /// <summary>
            /// 文件长度
            /// </summary>
            public long Length;
        }

        #endregion

        #region 播放顺序

        /* type = 0 顺序

        type = 1 重复播放全部
        type = 2 重复播放一首
        type = 3 随机播放

        */

        /// <summary>
        /// 随机播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRomdom_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 重复顺序播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetweet_Click(object sender, EventArgs e)
        {

        }

        #endregion


        //private void lblMusicTitle_Paint(object sender, PaintEventArgs e)
        //{
        //    Label lb = sender as Label;
        //    e.Graphics.DrawString(MusicTitle, lb.Font, Brushes.Black, new PointF(paintX, 0));
        //}



    }
}

/*
一、窗口样式

WS_POPUP        弹出式窗口(不能与WS_CHILDWINDOW样式同时使用)
WS_CHILDWINDOW  子窗口(不能与WS_POPUP合用)
WS_MINIMIZE     创建窗口拥有最小化按钮
WS_MINIMIZEBOX  创建窗口拥有最小化按钮，须同时指定WS_SYSTEM样式
WS_VISIBLE      可见状态
WS_DISABLED     不可用状态
WS_CLIPSIBLINGS 使窗口排除子窗口之间的相对区域
WS_CLIPCHILDREN 当在父窗口内绘图时,排除子窗口区域
WS_MAXIMIZE     具有最大化按钮
WS_MAXIMIZEBOX  创建窗口拥有最大化按钮，须同时指定WS_SYSTEM样式
WS_CAPTION      有标题框和边框(和WS_TILED样式相同)
WS_BORDER       有单边框
WS_DLGFRAME     带对话框边框样式,不带标题框
WS_VSCROLL      有垂直滚动条
WS_HSCROLL      有水平滚动条
WS_SYSMENU      标题框上带有窗口菜单(须指定WS_CAPTION样式)
WS_THICKFRAME   有可调边框(与WS_SIZEBOX样式相同)
WS_TILED        与WS_OVERLAPPED风格相同
WS_TILEDWINDOW  与WWS_OVERLAPPEDWINDOW风格相同
WS_GROUP        组样式,每个组的第一个控件具有WS_TABSTOP样式
WS_TABSTOP      可接受TAB键
WS_OVERLAPPED   创建一个重叠式窗口,拥有标题栏和边框
WS_OVERLAPPEDWINDOW 即:WS_OVERLAPPED风格,WS_CAPTION风格,WS_SYSMENU风格
                  WS_THICKFRAME风格,WS_MINIMIZEBOX风格和WS_MAXIMIZEBOX
                    风格的组合
*/

/* 窗口扩展样式参考列表：
                                     
WS_EX_APPWINDOW - 当窗口可见时将一个顶层窗口放置在任务栏上                
WS_EX_NOINHERITLAYOUT - 子控件不继承窗体或控件的布局
WS_EX_LAYOUTRTL - 窗体或控件将具有从右向左的布局(因而会被镜像)
WS_EX_COMPOSITED - 用双缓冲从下到上绘制窗口的所有子孙(WinXP以上)
WS_EX_NOACTIVATE - 处于顶层但不激活

二、按钮风格 
    
BS_AUTOCHECKBOX   同复选按钮类似，点击一下选中，再次点击取消。
BS_AUTORADIOBUTTON同单选按钮类似，点击后选中标志将从同组的其他单选按钮处移到当前选项。
BS_CHECKBOX       复选按钮
BS_DEFPUSHBUTTON   默认普通按钮，具有较黑的边框。
BS_GROUPBOX       分组框
BS_LEFTTEXT       同单选按钮或复选按钮配合使用，标题将显示在左侧。
BS_OWNERDRAW       可创建一个拥有者自绘按钮。
BS_PUSHBUTTON       普通下压按钮
BS_RADIOBUTTON       圆形单选按钮
BS_3STATE       三态复选按钮,三种状态即：选中，未选中，未定
BS_AUTOCHECKBOX   检查框，按钮的状态会自动改变
BS_AUTORADIOBUTTON圆形选择按钮，按钮的状态会自动改变
BS_AUTO3STATE     允许按钮有三种状态即：选中，未选中，未定
BS_CHECKBOX       检查框
BS_LEFTTEXT       左对齐文字

三、旋转按钮控件

UDS_HORZ 指定一个水平旋转按钮．若不指定该风格则创建一个垂直的旋转按钮．
UDS_WRAP 当旋转按钮增大到超过最大值时，自动重置为最小值，当减小至低于最小值时，自动重置为最大值．
UDS_ARROWKEYS 当用户按下向下或向上箭头键时，旋转按钮值递增或递减．
UDS_SETBUDDYINT 旋转按钮将自动更新伙伴控件中显示的数值，如果伙伴控件能接受输入，则可在伙伴控件中输入新的旋转按钮值．
UDS_NOTHOUSANDS 伙伴控件中显示的数值每隔三位没有千位分隔符．
UDS_AUTOBUDDY  自动使旋转按钮拥有一个伙伴控件．
UDS_ALIGNRIGHT 旋转按钮在伙伴控件的右侧．
UDS_ALIGNLEFT  旋转按钮在伙伴控件的左侧．

四、轨道条控件

TBS_HORZ 指定一个水平轨道条．该风格是默认的．
TBS_VERT 指定一个垂直轨道条．
TBS_AUTOTICKS 在范围设定后，自动为轨道条加上刻度．
TBS_NOTICKS 轨道条无刻度．
TBS_BOTTOM  在水平轨道条的底部显示刻度，可与TBS_TOP一起使用．
TBS_TOP 在水平轨道条的顶部显示刻度，可与TBS_BOTTOM一起使用．
TBS_RIGHT 在垂直轨道条的右侧显示刻度，可与TBS_LEFT一起使用．
TBS_LEFT 在垂直轨道条的左侧显示刻度，可与TBS_RIGHT一起使用．
TBS_BOTH 在轨道条的上下部或左右两侧都显示刻度．
TBS_ENABLESELRANGE 在轨道条中显示一个选择范围．

五、文本编辑框风格

ES_AUTOHSCROLL     当在行尾添加一个字符后自动向右滚动10个字符。
ES_AUTOVSCROLL     当输入回车后自动上滚一行。
ES_CENTER     字符居中显示。
ES_LEFT     字符左对齐。
ES_LOWERCASE     统一转化为小写字母。
ES_MULTILINE     允许多行显示。
ES_NOHIDESEL     当编辑失去焦点时隐藏对字符的选定，重新获得焦点后以反色显示选中内容。
ES_OEMCONVERT     将ANSI字符转化为OEM字符。
ES_PASSWORD     以星号显示字符，多用于回显密码。
ES_RIGHT     字符右对齐
ES_UPPERCASE     统一转化为大写字母。
ES_READONLY     设置字符为只读。
ES_WANTRETURN     接受回车键输入。

六、列表框风格 
    
LBS_STANDARD     创建一个具有边界和垂直滚动条、当选择发生变化或条目被双击时能够通知父窗口的标准列表框。所有条目按字母排序。
LBS_SORT     按字母排序。
LBS_NOSEL     条目可视但不可选。
LBS_NOTIFY     当用户选择或双击一个串时，发出消息通知父窗口。
LBS_DISABLENOSCROLL     在条目不多时依然显示并不起作用的滚动条。
LBS_MULTIPLESEL     允许条目多选。
LBS_EXTENDEDSEL     可用SHIFT和鼠标或指定键组合来选择多个条目。
LBS_MULTICOLUMN     允许多列显示。
LBS_OWNERDRAWVARIABLE     创建一个拥有者画列表框，条目高度可以不同。
LBS_OWNERDRAWFIXED     创建一个具有相同条目高度的拥有者画列表框。
LBS_USETABSTOPS     允许使用TAB制表符。
LBS_NOREDRAW     当条目被增删后不自动更新列表显示。
LBS_HASSTRINGS     记忆了添加到列表中的字串。
LBS_WANTKEYBOARDINPUT     当有键按下时向父窗口发送WM_VKEYTOITEM或WM_CHARTOITEM消息。
LBS_NOINTEGRALHEIGHT     按程序设定尺寸创建列表框。

七、组合框风格

CBS_AUTOHSCROLL 当在行尾输入字符时自动将编辑框中的文字向右滚动。
CBS_DROPDOWN     同CBS_SIMPLE风格类似，只是只有在用户点击下拉图标时才会显示出下拉列表。
CBS_DROPDOWNLIST 同CBS_DROPDOWN类似，只是显示当前选项的编辑框为一静态框所代替。
CBS_HASSTRINGS     创建一个包含了由字串组成的项目的拥有者画组合框。
CBS_OEMCONVERT     将组合框中的ANSI字串转化为OEM字符。
CBS_OWNERDRAWFIXED 由下拉列表框的拥有者负责对内容的绘制；列表框中各项目高度相同。
CBS_OWNERDRAWVARIABLE 由下拉列表框的拥有者负责对内容的绘制；列表框中各项目高度可以不同。
CBS_SIMPLE     下拉列表始终显示。
CBS_SORT     自动对下拉列表中的项目进行排序。
CBS_DISABLENOSCROLL 当下拉列表显示内容过少时显示垂直滚动条。
CBS_NOINTEGRALHEIGHT在创建控件时以指定的大小来精确设定组合框尺寸。

八、树形视图控件

TVS_HASLINES 在父项与子项间连线以清楚地显示结构．
TVS_LINESATROOT 只在根部画线．
TVS_HASBUTTONS 显示带有＂+＂或＂-＂的小方框来表示某项能否被展开或已展开．
TVS_EDITLABELS 用户可以编辑表项的标题．
TVS_SHOWSELALWAYS 即使控件失去输入焦点，仍显示出项的选择状态．
TVS_DISABLEDRAGDROP 不支持拖动操作．

九、列表视图控件

LVS_ALIGNLEFT 当显示格式是大图标或小图标时，标题放在图标的左边．缺省情况下标题放在图标的下面．
LVS_ALIGNTOP 当显示格式是大图标或小图标时，标题放在图标的上边．
LVS_AUTOARRANGE 当显示格式是大图标或小图标时，自动排列控件中的表项．
LVS_EDITLABELS 用户可以修改标题．
LVS_ICON 指定大图标显示格式．
LVS_LIST 指定列表显示格式．
LVS_NOCOLUMNHEADER 在报告格式中不显示列的表头．
LVS_NOLABELWRAP 当显示格式是大图标时，使标题单行显示．缺省时是多行显示．
LVS_NOSCROLL 列表视图无滚动条．
LVS_NOSORTHEADER 报告列表视图的表头不能作为排序按钮使用．
LVS_OWNERDRAWFIXED 由控件的拥有者负责绘制表项．
LVS_REPORT 指定报告 显示格式．
LVS_SHAREIMAGELISTS 使列表视图共享图像序列．
LVS_SHOWSELALWAYS 即使控件失去输入焦点，仍显示出项的选择状态．
LVS_SINGLESEL 指定一个单选择列表视图．缺省时可以多项选择．
LVS_SMALLICON 指定小图标显示格式．
LVS_SORTASCENDING 按升序排列表项．
LVS_SORTDESCENDING 按降序排列表项．

十、静态文本框风格

SS_CENTER        字符居中显示。
SS_LEFT          字符左对齐。
SS_LEFTNOWORADWRAP 字符左对齐,可处理TAB制表符，不支持自动换行，超过末尾字符被裁剪。
SS_BLACKRECT    用窗口边框色填充的矩形。
SS_BLSCKFRAME   矩形边框，与窗口边框同色。
SS_GRAYRECT     用屏幕背景色填充的矩行。
SS_GRAYFRAME    矩形边框，使用屏幕背景色。
SS_WHITERECT    用窗口背景色填充的矩行。
SS_RIGHT        字符右对齐
SS_WHITEFRAME   矩形边框，使用窗口背景色。
 

控件样式参考列表：
DS_ABSALIGN - 对话框的坐标为屏幕坐标(缺省为客户区坐标)
DS_SYSMODAL - 系统模式(仅支持16位程序),不能与DS_CONTROL同用
DS_LOCALEDIT - 在对话框内部为编辑框分配内存(仅支持16位程序)
DS_SETFONT - 可定制对话框字体
DS_MODALFRAME - 框架样式(不能与WS_CAPTION同用)
DS_NOIDLEMSG - 无空闲消息
DS_SETFOREGROUND - 使对话框在最前面显示
DS_3DLOOK - 四周有3维边框
DS_FIXEDSYS - 使用系统固定字体
DS_NOFAILCREATE - 忽略创建过程中的错误
DS_CONTROL - 控件模式,可作为其他对话框的子窗口
DS_CENTER - 在屏幕居中
DS_CENTERMOUSE - 在鼠标位置居中
DS_CONTEXTHELP - 有上下文帮助按钮


已知代号的样式列表
GWL_EXSTYLE, "-20"
GWL_STYLE, "-16"
GWL_WNDPROC, "-4"
WS_EX_ACCEPTFILES, "16" 可接受文件拖放
WS_EX_APPWINDOW, "262144" 当窗口可见时将一个顶层窗口放置在任务栏上
WS_EX_CLIENTEDGE, "512" 带阴影的边缘
WS_EX_CONTEXTHELP, "1024"  有上下文帮助样式,标题栏包含一个问号标志
WS_EX_CONTROLPARENT, "65536" 允许用户使用TAB键在窗口的子窗口间搜索
WS_EX_DLGMODALFRAME, "1"  带双层边框
WS_EX_LEFT, "0" 左对齐
WS_EX_LEFTSCROLLBAR, "16384" 垂直滚动条在窗口左边界
WS_EX_LTRREADING, "0"
WS_EX_MDICHILD, "64" MDI子窗口样式
WS_EX_NOPARENTNOTIFY, "4" 创建/销毁时不通知父窗口
WS_EX_OVERLAPPEDWINDOW, "768" 带凸起边缘的边框,边缘有阴影
WS_EX_PALETTEWINDOW, "392" 带立体边框,有工具条窗口样式,窗口在顶层
WS_EX_RIGHT, "4096" 右对齐
WS_EX_RIGHTSCROLLBAR, "0" 垂直滚动条在窗口右边界
WS_EX_RTLREADING, "8192" 窗口文本从右到左显示
WS_EX_STATICEDGE, "131072" 当窗口为不可用状态时创建一个三维边缘
WS_EX_TOOLWINDOW, "128" 工具条窗口样式
WS_EX_TOPMOST, "8" 窗口置顶(停留在所有非最高层窗口的上面)
WS_EX_TRANSPARENT, "32" 透明样式,在同属窗口已重画时该窗口才可重画
WS_EX_WINDOWEDGE, "256" 带凸起边缘的边框
ICON_BIG, "1"
ICON_SMALL, "0"
WM_SETICON, "128"
WS_EX_LAYERED "524288"  分层或透明窗口,该样式可使用混合特效

Windows常见窗口样式和控件风格
*/

/*
系统特殊路径一览 

要用到系统里面的特殊路径，显然直接写“C:\Document and Setting\”不现实，那还是用回.NET 类库里面提供的比较好。要获取到桌面文件夹的路径，上网找的有种办法是
Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
 
这个是感觉是最保险的，毕竟是.NET提供的。可是获取到的东西并不是我想要的，上面获取到的是当前用户的桌面路径，但是并非公共用户的桌面路径。
在网上看到一种用注册表获取的方式，没尝试过，但眼看也只是获取到当前用户而已，而且注册表有个不太好的地方就是在Win8下安全措施下，
单纯运行程序是不能访问注册表的，非得要“以管理员身份运行”才行。还有一种方式就是我现在使用的方式，就是使用系统的API，要声明的方法如下
 
[DllImport("shfolder.dll", CharSet = CharSet.Auto)]
private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);
 
其中第二个参数就是各种特殊文件夹的标识值：CSIDL，用这方法主要也是要找出文件夹的CSIDL。本文的主要目的只是罗列各种文件夹的CSIDL而已

CSIDL_Users_FAVORITES = 6 //当前用户\收藏夹
CSIDL_Users_DESKTOPDIRECTORY = 16 //当前用户\桌面
CSIDL_Users_STARTMENU = 11 //当前用户\开始菜单
CSIDL_Users_STARTMENU_cx = 2 //当前用户\开始-程序
CSIDL_Users_MyDocuments = 5 //当前用户\我的文档
CSIDL_Users_STARTMENU_a = 7 //当前用户\开始-程序-启动
CSIDL_Users_Recent = 8 //当前用户\//Recent
CSIDL_Users_SendTo = 9 //当前用户\SendTo
CSIDL_Users_MyMusic = 13 //当前用户\My Documents\My Music\
CSIDL_Users_NetHood = 19 //当前用户\NetHood
CSIDL_Users_Templates = 21 //当前用户\Templates
CSIDL_Users_AppData = 26 //当前用户\Application Data\
CSIDL_Users_PrintHood = 27 //当前用户\PrintHood\
CSIDL_Users_Local_AppData = 28 //当前用户\Local Settings\Application Data\
CSIDL_Users_Temp = 32 //当前用户\Local Settings\Temporary Internet Files\
CSIDL_Users_Cookies = 33 //当前用户\Cookies\
CSIDL_Users_History = 34 //当前用户\Local Settings\History\
CSIDL_Users_Pictures = 39 //当前用户\My Documents\My Pictures\
CSIDL_Users = 40 //当前用户\
CSIDL_Users_gl = 48 //当前用户\「开始」菜单\程序\管理工具\
CSIDL_Users_CDBurning = 59 //当前用户\Local Settings\Application Data\Microsoft\CD Burning\

CSIDL_AllUsers_STARTMENU = 22 //All Users\「开始」菜单\
CSIDL_AllUsers_STARTMENU_cx = 23 //All Users\「开始」菜单\程序\
CSIDL_AllUsers_STARTMENU_j = 24 //All Users\「开始」菜单\程序\启动\
CSIDL_AllUsers_DESKTOPDIRECTORY = 25 //All Users\桌面
CSIDL_AllUsers_FAVORITES = 31 //All Users\Favorites\(收藏夹)
CSIDL_AllUsers_Templates = 45 //All Users\Templates\
CSIDL_AllUsers_Documents = 46 //All Users\Documents\
CSIDL_AllUsers_gl = 47 //All Users\「开始」菜单\程序\管理工具\
CSIDL_AllUsers_Music = 53 //All Users\Documents\My Music\
CSIDL_AllUsers_Pictures = 54 //All Users\Documents\My Pictures\
CSIDL_AllUsers_Videos = 55 //All Users\Documents\My Videos\
CSIDL_AllUsers_AppData = 35 //All Users\Application Data\

CSIDL_WinDows = 36 //系统安装路径C:\WINDOWS\
CSIDL_WinSystem = 37 //系统文件夹C:\WINDOWS\system32\
CSIDL_ProgramFiles = 38 //应用程序安装文件夹C:\Program Files\
CSIDL__ProgramFiles_CommonFiles = 43 //C:\Program Files\Common Files\
CSIDL_WIN_Resources = 56 //C:\WINDOWS\Resources\
CSIDL_font = 20 //字体文件夹C:\WINDOWS\Fonts\

 * 
 * 音频 (MP3)
32 kbit/s — MW (AM) 质量
96 kbit/s — FM 质量
128 - 160 kbit/s –相当好的质量，有时有明显差别
192 kbit/s — 优良质量，偶尔有差别
224 - 320 kbit/s — 高质量

*/


