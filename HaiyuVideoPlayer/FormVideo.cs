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
using Hsp.Player.Common;
using Un4seen.Bass;

namespace HaiyuVideoPlayer
{
    /// <summary>
    /// https://www.videolan.org/
    /// https://github.com/ZeBobo5/Vlc.DotNet
    /// </summary>
    public partial class FormVideo : Form
    {
        internal MusicKernel Music;
        private int _tickCounter = 0;
        private int _updateInterval = 50; // 50ms
        private Un4seen.Bass.BASSTimer _updateTimer = null;
        private string _fileName = String.Empty;
        private int _apePlugIn = 0;

        public FormVideo()
        {
            InitializeComponent();

            lblVideoTitle.Text = ""; // 音乐标题
            lblPlayProgress.Text = ""; // 播放进度
            
            //Music.FileName = @"E:\Music\moonlight.mp3";
            //Music.Play(true);






        }

        private void FormVideo_Load(object sender, EventArgs e)
        {
            Music = new MusicKernel(-1, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            #region 插件加载

            List<BassPlugin> pluginList = new List<BassPlugin>();

            BassPlugin plugin = new BassPlugin("tags.dll");
            plugin.Load();
            pluginList.Add(plugin);

            plugin = new BassPlugin("bass_ape.dll");
            plugin.Load();
            pluginList.Add(plugin);

            plugin = new BassPlugin("basswma.dll");
            plugin.Load();
            pluginList.Add(plugin);

            plugin = new BassPlugin("bassmidi.dll");
            plugin.Load();
            pluginList.Add(plugin);


            Music.BassPlugins = pluginList;

            #endregion



            //var targetPath = Application.StartupPath;

            // 加载插件
            //var loadedPlugIns = Bass.BASS_PluginLoadDirectory(Application.StartupPath);


            //foreach (var plugIn in loadedPlugIns)
            //{
            //    var ii = plugIn.Key;
            //    var v = plugIn.Value;
            //    var p = ii + v;
            //}

            // create a secure timer
            _updateTimer = new Un4seen.Bass.BASSTimer(_updateInterval);
            _updateTimer.Tick += new EventHandler(timerUpdate_Tick);

        }

        private void btnAddList_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择视频",
                InitialDirectory = @"E:\Videos", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;|AVI文件(*.avi;)|*.avi;|MP4文件(*.mp4;)|*.mp4;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            _fileName = fileDialog.FileName;

            var fi = new FileInfo(_fileName);

            _fileName = fi.FullName;
            //lblMusicTitle.Text = fi.Name;

            MusicTags tags = new MusicTags(_fileName, false);
            lblVideoTitle.Text = tags.Title;

            Music.TagsInfo = tags;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _updateTimer.Start();
            Music.FileName = _fileName;
            Music.Play(true);
        }


        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            _tickCounter++;
            if (_tickCounter % 5 == 0)
            {
                //var prog = Music.LoadProgress;
                //lblPlayProgress.Text = prog.ToString("N0");
                lblPlayProgress.Text = GetFormatTimeString(Music.MusicTime) + @" / " + GetFormatTimeString(Music.Length);
                    
                //Music.GetFormatTime();
                //GetFormatTimeString(Music.MusicTime) + " / "  +  GetFormatTimeString(Music.Length);
            }

            //lblPlayProgress.Text = _tickCounter + " * " + Music.MusicTime.ToString();
        }

        private string GetFormatTimeString(double second)
        {
            return new DateTime(1970, 1, 1).AddSeconds(second).ToString("mm:ss:fff");
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Music.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Music.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
