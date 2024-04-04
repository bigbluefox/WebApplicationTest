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
using Hsp.Test.Common;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace HSP.MediaRetrieve
{
    public partial class frmMusic : Form
    {
        /// <summary>
        /// 文件打开对话框
        /// </summary>
        private OpenFileDialog _opendialog;

        /// <summary>
        /// 音频流句柄
        /// </summary>
        private int _stream = 0;

        private string _fileName { get; set; }

        private TAG_INFO _tagInfo;

        private int _tickCounter = 0;
        private int _updateInterval = 50; // 50ms
        private Un4seen.Bass.BASSTimer _updateTimer = null;

        private int _wmaPlugIn = 0;
        private int _apePlugIn = 0;
        private int _midPlugIn = 0;

        //private System.Windows.Forms.ProgressBar progressBarLeft;
        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ProgressBar progressBarRight;


        //最后还要把BASS音频库(bass.dll,第一个下载的)放到Debug目录里。 基本工作做好了。 现在开始编写代码！ 
        //然后 注册一个免费的Licence 使用起来会很方便
        //地址为http://www.bass.radio42.com/

        // private VARs
        private int _mixer = 0;
        private SYNCPROC _mixerStallSync;
        private Track _currentTrack = null;
        private Track _previousTrack = null;

        //private System.Windows.Forms.Timer timerUpdate;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMusic()
        {
            InitializeComponent();

            lblMusicInfo.Text = "";
            lblMusicFileName.Text = "";
            this.btnStop.Enabled = false;
            this.btnPause.Enabled = false;

            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Start();

            // create a secure timer
            _updateTimer = new Un4seen.Bass.BASSTimer(_updateInterval);
            _updateTimer.Tick += new EventHandler(timer1Update_Tick);

            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 50;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);

            //btnPause.Hide();
            button1.Hide();
        }

        #region 添加歌曲文件

        /// <summary>
        /// 添加歌曲文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMusic_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲",
                InitialDirectory = @"E:\Music", // 设置默认打开路径(绝对路径)
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            string path = fileDialog.FileName;
            var fi = new FileInfo(path);

            _fileName = fi.FullName;
            lblMusicFileName.Text = fi.Name;

            BASS_INFO info = new BASS_INFO();
            Bass.BASS_GetInfo(info);

            #region 根据媒体扩展名选择插件库

            if (fi.Extension.ToLower() == ".wma")
            {
                _wmaPlugIn = Bass.BASS_PluginLoad("basswma.dll");
            }
            if (fi.Extension.ToLower() == ".ape")
            {
                _apePlugIn = Bass.BASS_PluginLoad("bass_ape.dll");
            }
            if (fi.Extension.ToLower() == ".mid")
            {
                _midPlugIn = Bass.BASS_PluginLoad("bassmidi.dll");
            }

            #endregion

            _stream = Bass.BASS_StreamCreateFile(fi.FullName, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            //Bass.BASS_ChannelPlay(_stream, true);

            //Track track = new Track(path);

            //// add the new track to the mixer (in PAUSED mode!)
            //BassMix.BASS_Mixer_StreamAddChannel(_mixer, track.Channel, BASSFlag.BASS_MIXER_PAUSE | BASSFlag.BASS_MIXER_DOWNMIX | BASSFlag.BASS_STREAM_AUTOFREE);

            //// an BASS_SYNC_END is used to trigger the next track in the playlist (if no POS sync was set)
            //track.TrackSync = new SYNCPROC(OnTrackSync);
            //BassMix.BASS_Mixer_ChannelSetSync(track.Channel, BASSSync.BASS_SYNC_END, 0L, track.TrackSync, new IntPtr(0));

            ////if (_currentTrack == null)
            ////    PlayNextTrack();
        }

        private void OnTrackSync(int handle, int channel, int data, IntPtr user)
        {
            if (user.ToInt32() == 0)
            {
                // END SYNC
                BeginInvoke(new MethodInvoker(PlayNextTrack));
            }
            else
            {
                // POS SYNC
                BeginInvoke((MethodInvoker)delegate()
                {
                    // this code runs on the UI thread!
                    PlayNextTrack();
                    // and fade out and stop the 'previous' track (for 4 seconds)
                    if (_previousTrack != null)
                        Bass.BASS_ChannelSlideAttribute(_previousTrack.Channel, BASSAttribute.BASS_ATTRIB_VOL, -1f, 4000);
                });
            }
        }

        private void PlayNextTrack()
        {
            lock (listBoxPlaylist)
            {
                // get the next track to play
                if (listBoxPlaylist.Items.Count > 0)
                {
                    _previousTrack = _currentTrack;
                    _currentTrack = listBoxPlaylist.Items[0] as Track;

                    listBoxPlaylist.Items.RemoveAt(0);

                    // the channel was already added
                    // so for instant playback, we just unpause the channel
                    BassMix.BASS_Mixer_ChannelPlay(_currentTrack.Channel);

                    labelTitle.Text = _currentTrack.Tags.title;
                    labelArtist.Text = _currentTrack.Tags.artist;

                    // get the waveform for that track
                    GetWaveForm();
                }
            }
        }

        #endregion

        #region Wave Form

        // zoom helper varibales
        private bool _zoomed = false;
        private int _zoomStart = -1;
        private long _zoomStartBytes = -1;
        private int _zoomEnd = -1;
        private float _zoomDistance = 5.0f; // zoom = 5sec.

        private Un4seen.Bass.Misc.WaveForm _WF = null;
        private void GetWaveForm()
        {
            // unzoom...(display the whole wave form)
            _zoomStart = -1;
            _zoomStartBytes = -1;
            _zoomEnd = -1;
            _zoomed = false;
            // render a wave form
            _WF = new WaveForm(_currentTrack.Filename, new WAVEFORMPROC(MyWaveFormCallback), this);
            _WF.FrameResolution = 0.01f; // 10ms are nice
            _WF.CallbackFrequency = 30000; // every 5min.
            _WF.ColorBackground = Color.FromArgb(20, 20, 20);
            _WF.ColorLeft = Color.Gray;
            _WF.ColorLeftEnvelope = Color.LightGray;
            _WF.ColorRight = Color.Gray;
            _WF.ColorRightEnvelope = Color.LightGray;
            _WF.ColorMarker = Color.Gold;
            _WF.ColorBeat = Color.LightSkyBlue;
            _WF.ColorVolume = Color.White;
            _WF.DrawEnvelope = false;
            _WF.DrawWaveForm = WaveForm.WAVEFORMDRAWTYPE.HalfMono;
            _WF.DrawMarker = WaveForm.MARKERDRAWTYPE.Line | WaveForm.MARKERDRAWTYPE.Name | WaveForm.MARKERDRAWTYPE.NamePositionAlternate;
            _WF.MarkerLength = 0.75f;
            _WF.RenderStart(true, BASSFlag.BASS_DEFAULT);
        }

        private void MyWaveFormCallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            if (finished)
            {
                _WF.SyncPlayback(_currentTrack.Channel);

                // and do pre-calculate the next track position
                // in this example we will only use the end-position
                long startPos = 0L;
                long endPos = 0L;
                if (_WF.GetCuePoints(ref startPos, ref endPos, -24.0, -12.0, true))
                {
                    _currentTrack.NextTrackPos = endPos;
                    // if there is already a sync set, remove it first
                    if (_currentTrack.NextTrackSync != 0)
                        BassMix.BASS_Mixer_ChannelRemoveSync(_currentTrack.Channel, _currentTrack.NextTrackSync);

                    // set the next track sync automatically
                    _currentTrack.NextTrackSync = BassMix.BASS_Mixer_ChannelSetSync(_currentTrack.Channel, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, _currentTrack.NextTrackPos, _currentTrack.TrackSync, new IntPtr(1));

                    _WF.AddMarker("Next", _currentTrack.NextTrackPos);
                }
            }
            // will be called during rendering...
            DrawWave();
        }

        private void DrawWave()
        {
            if (_WF != null)
                this.pictureBoxWaveForm.BackgroundImage = _WF.CreateBitmap(this.pictureBoxWaveForm.Width, this.pictureBoxWaveForm.Height, _zoomStart, _zoomEnd, true);
            else
                this.pictureBoxWaveForm.BackgroundImage = null;
        }

        private void DrawWavePosition(long pos, long len)
        {
            if (_WF == null || len == 0 || pos < 0)
            {
                this.pictureBoxWaveForm.Image = null;
                return;
            }

            Bitmap bitmap = null;
            Graphics g = null;
            Pen p = null;
            double bpp = 0;

            try
            {
                if (_zoomed)
                {
                    // total length doesn't have to be _zoomDistance sec. here
                    len = _WF.Frame2Bytes(_zoomEnd) - _zoomStartBytes;

                    int scrollOffset = 10; // 10*20ms = 200ms.
                    // if we scroll out the window...(scrollOffset*20ms before the zoom window ends)
                    if (pos > (_zoomStartBytes + len - scrollOffset * _WF.Wave.bpf))
                    {
                        // we 'scroll' our zoom with a little offset
                        _zoomStart = _WF.Position2Frames(pos - scrollOffset * _WF.Wave.bpf);
                        _zoomStartBytes = _WF.Frame2Bytes(_zoomStart);
                        _zoomEnd = _zoomStart + _WF.Position2Frames(_zoomDistance) - 1;
                        if (_zoomEnd >= _WF.Wave.data.Length)
                        {
                            // beyond the end, so we zoom from end - _zoomDistance.
                            _zoomEnd = _WF.Wave.data.Length - 1;
                            _zoomStart = _zoomEnd - _WF.Position2Frames(_zoomDistance) + 1;
                            if (_zoomStart < 0)
                                _zoomStart = 0;
                            _zoomStartBytes = _WF.Frame2Bytes(_zoomStart);
                            // total length doesn't have to be _zoomDistance sec. here
                            len = _WF.Frame2Bytes(_zoomEnd) - _zoomStartBytes;
                        }
                        // get the new wave image for the new zoom window
                        DrawWave();
                    }
                    // zoomed: starts with _zoomStartBytes and is _zoomDistance long
                    pos -= _zoomStartBytes; // offset of the zoomed window

                    bpp = len / (double)this.pictureBoxWaveForm.Width;  // bytes per pixel
                }
                else
                {
                    // not zoomed: width = length of stream
                    bpp = len / (double)this.pictureBoxWaveForm.Width;  // bytes per pixel
                }

                p = new Pen(Color.Red);
                bitmap = new Bitmap(this.pictureBoxWaveForm.Width, this.pictureBoxWaveForm.Height);
                g = Graphics.FromImage(bitmap);
                g.Clear(Color.Black);
                int x = (int)Math.Round(pos / bpp);  // position (x) where to draw the line
                g.DrawLine(p, x, 0, x, this.pictureBoxWaveForm.Height - 1);
                bitmap.MakeTransparent(Color.Black);
            }
            catch
            {
                bitmap = null;
            }
            finally
            {
                // clean up graphics resources
                if (p != null)
                    p.Dispose();
                if (g != null)
                    g.Dispose();
            }

            this.pictureBoxWaveForm.Image = bitmap;
        }

        private void ToggleZoom()
        {
            if (_WF == null)
                return;

            // WF is not null, so the stream must be playing...
            if (_zoomed)
            {
                // unzoom...(display the whole wave form)
                _zoomStart = -1;
                _zoomStartBytes = -1;
                _zoomEnd = -1;
            }
            else
            {
                // zoom...(display only a partial wave form)
                long pos = BassMix.BASS_Mixer_ChannelGetPosition(_currentTrack.Channel);
                // calculate the window to display
                _zoomStart = _WF.Position2Frames(pos);
                _zoomStartBytes = _WF.Frame2Bytes(_zoomStart);
                _zoomEnd = _zoomStart + _WF.Position2Frames(_zoomDistance) - 1;
                if (_zoomEnd >= _WF.Wave.data.Length)
                {
                    // beyond the end, so we zoom from end - _zoomDistance.
                    _zoomEnd = _WF.Wave.data.Length - 1;
                    _zoomStart = _zoomEnd - _WF.Position2Frames(_zoomDistance) + 1;
                    _zoomStartBytes = _WF.Frame2Bytes(_zoomStart);
                }
            }
            _zoomed = !_zoomed;
            // and display this new wave form
            DrawWave();
        }

        private void pictureBoxWaveForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_WF == null)
                return;

            bool doubleClick = e.Clicks > 1;
            bool lowerHalf = (e.Y > pictureBoxWaveForm.Height / 2);

            if (lowerHalf && doubleClick)
            {
                ToggleZoom();
            }
            else if (!lowerHalf && e.Button == MouseButtons.Left)
            {
                // left button will set the position
                long pos = _WF.GetBytePositionFromX(e.X, pictureBoxWaveForm.Width, _zoomStart, _zoomEnd);
                //SetEnvelopePos(_currentTrack.Channel, pos);
            }
            else if (!lowerHalf)
            {
                _currentTrack.NextTrackPos = _WF.GetBytePositionFromX(e.X, pictureBoxWaveForm.Width, _zoomStart, _zoomEnd);
                // if there is already a sync set, remove it first
                if (_currentTrack.NextTrackSync != 0)
                    BassMix.BASS_Mixer_ChannelRemoveSync(_currentTrack.Channel, _currentTrack.NextTrackSync);

                // right button will set a next track position sync
                _currentTrack.NextTrackSync = BassMix.BASS_Mixer_ChannelSetSync(_currentTrack.Channel, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, _currentTrack.NextTrackPos, _currentTrack.TrackSync, new IntPtr(1));

                _WF.AddMarker("Next", _currentTrack.NextTrackPos);
                DrawWave();
            }
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
            //var endPoint = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            //if (endPoint > 0)
            //{
            //    lblMusicInfo.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString + @" / " +
            //                          axWindowsMediaPlayer1.currentMedia.durationString;
            //}
            //else
            //{
            //    lblMusicInfo.Text = DateTime.Now.ToLongTimeString();
            //}
        }

        #endregion

        #region 窗体正在关闭事件

        /// <summary>
        /// 窗体正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMusic_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer.Tick -= new EventHandler(timerUpdate_Tick);

            timerUpdate.Stop();

            Bass.BASS_ChannelStop(_stream); // 停止播放
            Bass.BASS_StreamFree(_stream); // 释放音频流
            Bass.BASS_PluginFree(0); // 释放所有插件

            Bass.BASS_StreamFree(_mixer);

            Bass.BASS_Stop(); // 停止所有输出
            Bass.BASS_Free(); // 释放所有资源
            
            //Bass.BASS_PluginFree(_wmaPlugIn);
            //Bass.BASS_PluginFree(_apePlugIn);
            //Bass.BASS_PluginFree(_midPlugIn);
        }

        #endregion

        private void OnMixerStall(int handle, int channel, int data, IntPtr user)
        {
            BeginInvoke((MethodInvoker)delegate()
            {
                // this code runs on the UI thread!
                if (data == 0)
                {
                    // mixer stalled
                    timerUpdate.Stop();
                    progressBarLeft.Value = 0;
                    progressBarRight.Value = 0;
                }
                else
                {
                    // mixer resumed
                    timerUpdate.Start();
                }
            });
        }

        private void timer1Update_Tick(object sender, System.EventArgs e)
        {
            // here we gather info about the stream, when it is playing...
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                // the stream is still playing...
            }
            else
            {
                // the stream is NOT playing anymore...
                _updateTimer.Stop();
                this.progressBarLeft.Value = 0;
                this.progressBarRight.Value = 0;
                //this.labelTime.Text = "Stopped";
                //DrawWavePosition(-1, -1);
                //this.pictureBoxSpectrum.Image = null;

                //this.btnStop.Enabled = false;
                //this.btnPlay.Enabled = true;
                return;
            }

            // from here on, the stream is for sure playing...
            _tickCounter++;
            long pos = Bass.BASS_ChannelGetPosition(_stream); // position in bytes
            long len = Bass.BASS_ChannelGetLength(_stream); // length in bytes

            if (_tickCounter == 5)
            {
                // display the position every 250ms (since timer is 50ms)
                _tickCounter = 0;
                double totaltime = Bass.BASS_ChannelBytes2Seconds(_stream, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_stream, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
                this.lblMusicInfo.Text = String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));
                this.Text = String.Format("Bass-CPU: {0:0.00}% (not including Waves & Spectrum!)", Bass.BASS_GetCPU());
            }

            // display the level bars
            int peakL = 0;
            int peakR = 0;
            // for testing you might also call RMS_2, RMS_3 or RMS_4
            //RMS(_stream, out peakL, out peakR);
            // level to dB
            double dBlevelL = Utils.LevelToDB(peakL, 65535);
            double dBlevelR = Utils.LevelToDB(peakR, 65535);
            //RMS_2(_stream, out peakL, out peakR);
            //RMS_3(_stream, out peakL, out peakR);
            //RMS_4(_stream, out peakL, out peakR);

            //this.progressBarLeft.Value = peakL;
            //this.progressBarRight.Value = peakR;

            // update the wave position
            //DrawWavePosition(pos, len);
            // update spectrum
            //DrawSpectrum();
        }


        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            int level = Bass.BASS_ChannelGetLevel(_mixer);
            progressBarLeft.Value = Utils.LowWord32(level);
            progressBarRight.Value = Utils.HighWord32(level);

            if (_currentTrack != null)
            {
                long pos = BassMix.BASS_Mixer_ChannelGetPosition(_currentTrack.Channel);
                long len = Bass.BASS_ChannelGetLength(_currentTrack.Channel); // length in bytes

                //labelTime.Text = Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, pos), "HHMMSS");
                //labelRemain.Text = Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, _currentTrack.TrackLength - pos), "HHMMSS");

                double totaltime = Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_currentTrack.Channel, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
                this.lblMusicInfo.Text = String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));
                this.Text = String.Format("Bass-CPU: {0:0.00}% (not including Waves & Spectrum!)", Bass.BASS_GetCPU());

                this.lblMusicInfo.Text = String.Format("Elapsed: {0:#0.00} - Total: {1:#0.00} - Remain: {2:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));

                DrawWavePosition(pos, _currentTrack.TrackLength);
            }

        }


        /// <summary>
        /// 视窗加载，初始化BASS音频库
        /// 第一个参数是输出设备的编号，-1是采用默认设备输出，第二个参数是输出采样率
        /// ，第三个参数是初始化设备的输出模式，最后一个是句柄，为本窗体的句柄就可以了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMusic_Load(object sender, EventArgs e)
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_CPSPEAKERS, this.Handle))
            {
                MessageBox.Show(@"Bass初始化出错！" + Bass.BASS_ErrorGetCode().ToString());
            }

            var targetPath = Application.StartupPath;

           var loadedPlugIns = Bass.BASS_PluginLoadDirectory(targetPath);  

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, 200);
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 20);

            // already create a mixer
            _mixer = BassMix.BASS_Mixer_StreamCreate(44100, 2, BASSFlag.BASS_SAMPLE_FLOAT);
            if (_mixer == 0)
            {
                MessageBox.Show(this, "Could not create mixer!");
                Bass.BASS_Free();
                this.Close();
                return;
            }

            _mixerStallSync = new SYNCPROC(OnMixerStall);
            Bass.BASS_ChannelSetSync(_mixer, BASSSync.BASS_SYNC_STALL, 0L, _mixerStallSync, IntPtr.Zero);

            timerUpdate.Start();
            Bass.BASS_ChannelPlay(_mixer, false);

        }

        /// <summary>
        /// 歌曲播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            _updateTimer.Stop();
            Bass.BASS_StreamFree(_stream);
            if (_fileName == String.Empty) return;

                // create the stream
                _stream = Bass.BASS_StreamCreateFile(_fileName, 0, 0,
                    BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
            if (_stream == 0) return;

            //// used in RMS
            //_30mslength = (int)Bass.BASS_ChannelSeconds2Bytes(_stream, 0.03); // 30ms window
            //// latency from milliseconds to bytes
            //_deviceLatencyBytes = (int)Bass.BASS_ChannelSeconds2Bytes(_stream, _deviceLatencyMS / 1000.0);

            //// set a DSP user callback method
            ////_myDSPAddr = new DSPPROC(MyDSPGain);
            ////Bass.BASS_ChannelSetDSP(_stream, _myDSPAddr, 0, 2);
            //// if you want to use the above two line instead (uncomment the above and comment below)
            //_myDSPAddr = new DSPPROC(MyDSPGainUnsafe);
            //Bass.BASS_ChannelSetDSP(_stream, _myDSPAddr, IntPtr.Zero, 2);

            //    if (WF2 != null && WF2.IsRendered)
            //    {
            //        // make sure playback and wave form are in sync, since
            //        // we rended with 16-bit but play here with 32-bit
            //        WF2.SyncPlayback(_stream);

            //        long cuein = WF2.GetMarker("CUE");
            //        long cueout = WF2.GetMarker("END");

            //        int cueinFrame = WF2.Position2Frames(cuein);
            //        int cueoutFrame = WF2.Position2Frames(cueout);
            //        Console.WriteLine("CueIn at {0}sec.; CueOut at {1}sec.", WF2.Frame2Seconds(cueinFrame), WF2.Frame2Seconds(cueoutFrame));

            //        if (cuein >= 0)
            //        {
            //            Bass.BASS_ChannelSetPosition(_stream, cuein);
            //        }
            //        if (cueout >= 0)
            //        {
            //            Bass.BASS_ChannelRemoveSync(_stream, _syncer);
            //            _syncer = Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_POS, cueout, _sync, IntPtr.Zero);
            //        }
            //    }
            //}

            if (_stream != 0 && Bass.BASS_ChannelPlay(_stream, false))
            {
                this.txtMusicInfo.Text = "";
                _updateTimer.Start();

                // get some channel info
                BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                Bass.BASS_ChannelGetInfo(_stream, info);
                this.txtMusicInfo.Text += "Info: " + info.ToString() + Environment.NewLine;

                // display the channel info..
                this.txtMusicInfo.Text += String.Format("Type={0}, Channels={1}, OrigRes={2}", Utils.BASSChannelTypeToString(info.ctype), info.chans, info.origres);

                // display the tags...
                TAG_INFO tagInfo = new TAG_INFO(_fileName);
                var i = tagInfo.encodedby.Length;

                BassTags.EvalNativeTAGs = true;
                
                if (BassTags.BASS_TAG_GetFromFile(_stream, tagInfo))
                {
                    // and display what we get
                    //this.textBoxAlbum.Text = tagInfo.album;
                    //this.textBoxArtist.Text = tagInfo.artist;
                    //this.textBoxTitle.Text = tagInfo.title;
                    //this.textBoxComment.Text = tagInfo.comment;
                    //this.textBoxGenre.Text = tagInfo.genre;
                    //this.textBoxYear.Text = tagInfo.year;
                    //this.textBoxTrack.Text = tagInfo.track;
                    //this.pictureBoxTagImage.Image = tagInfo.PictureGetImage(0);
                    //this.textBoxPicDescr.Text = tagInfo.PictureGetDescription(0);
                    //if (this.textBoxPicDescr.Text == String.Empty)
                    //    this.textBoxPicDescr.Text = tagInfo.PictureGetType(0);

                    var strMusicInfo = tagInfo.album + Environment.NewLine;
                    strMusicInfo += tagInfo.artist + Environment.NewLine;
                    strMusicInfo += tagInfo.title + Environment.NewLine;
                    strMusicInfo += tagInfo.genre + Environment.NewLine;
                    strMusicInfo += tagInfo.year + Environment.NewLine;
                    strMusicInfo += tagInfo.track + Environment.NewLine;
                    strMusicInfo += tagInfo.PictureGetImage(0) + Environment.NewLine;
                    strMusicInfo += tagInfo.PictureGetDescription(0) + Environment.NewLine;

                    strMusicInfo += tagInfo.PictureGetType(0) + Environment.NewLine;
                    //strMusicInfo += tagInfo.album + Environment.NewLine;
                    //strMusicInfo += tagInfo.album + Environment.NewLine;
                    //strMusicInfo += tagInfo.album + Environment.NewLine;
                    //strMusicInfo += tagInfo.album + Environment.NewLine;

                    this.txtMusicInfo.Text = strMusicInfo;
                }

                this.btnStop.Enabled = true;
                this.btnPlay.Enabled = false;
                this.btnPause.Enabled = true;
            }
            else
            {
                this.txtMusicInfo.Text = @"Error=" + Bass.BASS_ErrorGetCode();
            }
        }

        /// <summary>
        /// 歌曲停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            _updateTimer.Stop();

            //// kills rendering, if still in progress, e.g. if a large file was selected
            //if (WF2 != null && WF2.IsRenderingInProgress)
            //    WF2.RenderStop();

            Bass.BASS_StreamFree(_stream);
            _stream = 0;
            this.button1.Text = "Select a file to play (e.g. MP3, OGG or WAV)...";

            this.btnStop.Enabled = false;
            this.btnPlay.Enabled = true;
            this.btnPause.Enabled = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                //this.btnStop.Enabled = false;
                //this.btnPlay.Enabled = true;
                Bass.BASS_ChannelPause(_stream);
            }
            else
            {
                _updateTimer.Start();
                Bass.BASS_ChannelPlay(_stream, false);
            }
        }

        /// <summary>
        /// 添加播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPlayList_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = @"请选择歌曲",
                Filter =
                    @"所有文件(*.*;)|*.*;|MP3文件(*.mp3;)|*.mp3;|APE文件(*.ape;)|*.ape;|FLAC文件(*.flac;)|*.flac;|WAV文件(*.wav;)|*.wav;|WMA文件(*.wma;)|*.wma;|WMV文件(*.wmv;)|*.wmv;|MIDI文件(*.mid;)|*.mid;"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            //string path = fileDialog.FileName;
            //var fi = new FileInfo(path);
            //_fileName = fi.FullName;
            //lblMusicFileName.Text = fi.Name;

            // 检查是否重复添加音乐文件

            #region 检查是否重复添加音乐文件

            var list = listBoxPlaylist.Items.Cast<Track>().ToList();
            if (list.Count > 0)
            {
                foreach (var track in list)
                {
                    if (track.Filename == fileDialog.FileName)
                    {
                        return;
                    }
                }
            }

            #endregion
            
            lock (listBoxPlaylist)
            {
                Track track = new Track(fileDialog.FileName);
                listBoxPlaylist.Items.Add(track);

                // in the demo we already add each new track to the mixer
                // this is in real life not the best place to do so (especially with larger playlists)
                // but for the demo it is okay ;-)

                // add the new track to the mixer (in PAUSED mode!)
                BassMix.BASS_Mixer_StreamAddChannel(_mixer, track.Channel, BASSFlag.BASS_MIXER_PAUSE | BASSFlag.BASS_MIXER_DOWNMIX | BASSFlag.BASS_STREAM_AUTOFREE);

                // an BASS_SYNC_END is used to trigger the next track in the playlist (if no POS sync was set)
                track.TrackSync = new SYNCPROC(OnTrackSync);
                BassMix.BASS_Mixer_ChannelSetSync(track.Channel, BASSSync.BASS_SYNC_END, 0L, track.TrackSync, new IntPtr(0));
            }

            if (_currentTrack == null)
                PlayNextTrack();
        }

    }



    //string targetPath ="";  
    //if(Utils.Is64Bit)  
    //    targetPath = Path.Combine(Application.StartupPath,"x64");  
    //else  
    //    targetPath = Path.Combine(Application.StartupPath,"x86");
  
    //// now load all libs manually  
    //Bass.LoadMe(targetPath);  
    //BassMix.LoadMe(targetPath);  
    //...  
    //loadedPlugIns = Bass.BASS_PluginLoadDirectory(targetPath);  
    //...  
    //// at the end of your application call this!  
    //Bass.FreeMe(targetPath);  
    //BassMix.FreeMe(targetPath);  
    //...  
    //foreach(int plugin in LoadedBassPlugIns.Keys)  
    //    Bass.BASS_PluginFree(plugin); 

}
