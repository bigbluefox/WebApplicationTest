namespace HaiyuMusicPlayer
{
    partial class FormMusic
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMusic));
            this.tsMusic = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbPlay = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.lblMusicTitle = new System.Windows.Forms.Label();
            this.lblPlayProgress = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxSpectrum = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnAddList = new System.Windows.Forms.Button();
            this.btnRomdom = new System.Windows.Forms.Button();
            this.btnRetweet = new System.Windows.Forms.Button();
            this.btnSound = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblVolumeTitle = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnVolumnDown = new System.Windows.Forms.Button();
            this.btnVolumeUp = new System.Windows.Forms.Button();
            this.lblAudioPath = new System.Windows.Forms.Label();
            this.btnAddMusic = new System.Windows.Forms.Button();
            this.tbProgress = new System.Windows.Forms.TrackBar();
            this.lvwMusic = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Idx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Ext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRate = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblRollingInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tsMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMusic
            // 
            this.tsMusic.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMusic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbPlay,
            this.tsbPause,
            this.tsbStop});
            this.tsMusic.Location = new System.Drawing.Point(596, 79);
            this.tsMusic.Name = "tsMusic";
            this.tsMusic.ShowItemToolTips = false;
            this.tsMusic.Size = new System.Drawing.Size(104, 25);
            this.tsMusic.TabIndex = 0;
            this.tsMusic.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "添加";
            this.tsbAdd.ToolTipText = "添加";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbPlay
            // 
            this.tsbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlay.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlay.Image")));
            this.tsbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlay.Name = "tsbPlay";
            this.tsbPlay.Size = new System.Drawing.Size(23, 22);
            this.tsbPlay.Text = "播放";
            this.tsbPlay.ToolTipText = "播放";
            this.tsbPlay.Click += new System.EventHandler(this.tsbPlay_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Image = ((System.Drawing.Image)(resources.GetObject("tsbPause.Image")));
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(23, 22);
            this.tsbPause.Text = "暂停";
            this.tsbPause.ToolTipText = "暂停";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "停止";
            this.tsbStop.ToolTipText = "停止";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // lblMusicTitle
            // 
            this.lblMusicTitle.AutoSize = true;
            this.lblMusicTitle.Location = new System.Drawing.Point(12, 59);
            this.lblMusicTitle.Name = "lblMusicTitle";
            this.lblMusicTitle.Size = new System.Drawing.Size(80, 18);
            this.lblMusicTitle.TabIndex = 1;
            this.lblMusicTitle.Text = "音乐标题";
            // 
            // lblPlayProgress
            // 
            this.lblPlayProgress.AutoSize = true;
            this.lblPlayProgress.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPlayProgress.Location = new System.Drawing.Point(696, 20);
            this.lblPlayProgress.Name = "lblPlayProgress";
            this.lblPlayProgress.Size = new System.Drawing.Size(80, 18);
            this.lblPlayProgress.TabIndex = 1;
            this.lblPlayProgress.Text = "播放进度";
            // 
            // pictureBoxSpectrum
            // 
            this.pictureBoxSpectrum.BackColor = System.Drawing.Color.Black;
            this.pictureBoxSpectrum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSpectrum.Location = new System.Drawing.Point(12, 115);
            this.pictureBoxSpectrum.Name = "pictureBoxSpectrum";
            this.pictureBoxSpectrum.Size = new System.Drawing.Size(803, 142);
            this.pictureBoxSpectrum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSpectrum.TabIndex = 63;
            this.pictureBoxSpectrum.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(122, 12);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(32, 32);
            this.btnPlay.TabIndex = 64;
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(196, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 65;
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPause.BackgroundImage")));
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Location = new System.Drawing.Point(159, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(32, 32);
            this.btnPause.TabIndex = 66;
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnAddList
            // 
            this.btnAddList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddList.BackgroundImage")));
            this.btnAddList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddList.FlatAppearance.BorderSize = 0;
            this.btnAddList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddList.Location = new System.Drawing.Point(48, 12);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new System.Drawing.Size(32, 32);
            this.btnAddList.TabIndex = 67;
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // btnRomdom
            // 
            this.btnRomdom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRomdom.BackgroundImage")));
            this.btnRomdom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRomdom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRomdom.FlatAppearance.BorderSize = 0;
            this.btnRomdom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRomdom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRomdom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRomdom.Location = new System.Drawing.Point(270, 12);
            this.btnRomdom.Name = "btnRomdom";
            this.btnRomdom.Size = new System.Drawing.Size(32, 32);
            this.btnRomdom.TabIndex = 67;
            this.btnRomdom.UseVisualStyleBackColor = true;
            this.btnRomdom.Click += new System.EventHandler(this.btnRomdom_Click);
            // 
            // btnRetweet
            // 
            this.btnRetweet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRetweet.BackgroundImage")));
            this.btnRetweet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRetweet.FlatAppearance.BorderSize = 0;
            this.btnRetweet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRetweet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRetweet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetweet.Location = new System.Drawing.Point(307, 12);
            this.btnRetweet.Name = "btnRetweet";
            this.btnRetweet.Size = new System.Drawing.Size(32, 32);
            this.btnRetweet.TabIndex = 67;
            this.btnRetweet.UseVisualStyleBackColor = true;
            this.btnRetweet.Click += new System.EventHandler(this.btnRetweet_Click);
            // 
            // btnSound
            // 
            this.btnSound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSound.BackgroundImage")));
            this.btnSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSound.FlatAppearance.BorderSize = 0;
            this.btnSound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSound.Location = new System.Drawing.Point(781, 72);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(32, 32);
            this.btnSound.TabIndex = 67;
            this.btnSound.UseVisualStyleBackColor = true;
            // 
            // btnMute
            // 
            this.btnMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMute.BackgroundImage")));
            this.btnMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMute.FlatAppearance.BorderSize = 0;
            this.btnMute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMute.Location = new System.Drawing.Point(379, 13);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(32, 32);
            this.btnMute.TabIndex = 67;
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(233, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 32);
            this.btnNext.TabIndex = 67;
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevious.BackgroundImage")));
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Location = new System.Drawing.Point(85, 12);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 32);
            this.btnPrevious.TabIndex = 67;
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(405, 14);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(297, 69);
            this.tbVolume.TabIndex = 68;
            this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
            this.tbVolume.MouseLeave += new System.EventHandler(this.tbVolume_MouseLeave);
            this.tbVolume.MouseHover += new System.EventHandler(this.tbVolume_MouseHover);
            // 
            // lblVolumeTitle
            // 
            this.lblVolumeTitle.AutoSize = true;
            this.lblVolumeTitle.Location = new System.Drawing.Point(241, 84);
            this.lblVolumeTitle.Name = "lblVolumeTitle";
            this.lblVolumeTitle.Size = new System.Drawing.Size(62, 18);
            this.lblVolumeTitle.TabIndex = 1;
            this.lblVolumeTitle.Text = "音量：";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(304, 84);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(44, 18);
            this.lblVolume.TabIndex = 1;
            this.lblVolume.Text = "音量";
            // 
            // btnVolumnDown
            // 
            this.btnVolumnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVolumnDown.BackgroundImage")));
            this.btnVolumnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVolumnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolumnDown.FlatAppearance.BorderSize = 0;
            this.btnVolumnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnVolumnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVolumnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolumnDown.Location = new System.Drawing.Point(705, 73);
            this.btnVolumnDown.Name = "btnVolumnDown";
            this.btnVolumnDown.Size = new System.Drawing.Size(32, 32);
            this.btnVolumnDown.TabIndex = 69;
            this.btnVolumnDown.UseVisualStyleBackColor = true;
            this.btnVolumnDown.Click += new System.EventHandler(this.btnVolumnDown_Click);
            // 
            // btnVolumeUp
            // 
            this.btnVolumeUp.BackColor = System.Drawing.SystemColors.Control;
            this.btnVolumeUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVolumeUp.BackgroundImage")));
            this.btnVolumeUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVolumeUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolumeUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVolumeUp.FlatAppearance.BorderSize = 0;
            this.btnVolumeUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnVolumeUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVolumeUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolumeUp.Location = new System.Drawing.Point(743, 73);
            this.btnVolumeUp.Name = "btnVolumeUp";
            this.btnVolumeUp.Size = new System.Drawing.Size(32, 32);
            this.btnVolumeUp.TabIndex = 69;
            this.btnVolumeUp.UseVisualStyleBackColor = false;
            this.btnVolumeUp.Click += new System.EventHandler(this.btnVolumeUp_Click);
            // 
            // lblAudioPath
            // 
            this.lblAudioPath.AutoSize = true;
            this.lblAudioPath.Location = new System.Drawing.Point(12, 84);
            this.lblAudioPath.Name = "lblAudioPath";
            this.lblAudioPath.Size = new System.Drawing.Size(98, 18);
            this.lblAudioPath.TabIndex = 1;
            this.lblAudioPath.Text = "音频路径：";
            // 
            // btnAddMusic
            // 
            this.btnAddMusic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddMusic.BackgroundImage")));
            this.btnAddMusic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddMusic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddMusic.FlatAppearance.BorderSize = 0;
            this.btnAddMusic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddMusic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMusic.Location = new System.Drawing.Point(11, 12);
            this.btnAddMusic.Name = "btnAddMusic";
            this.btnAddMusic.Size = new System.Drawing.Size(32, 32);
            this.btnAddMusic.TabIndex = 67;
            this.btnAddMusic.UseVisualStyleBackColor = true;
            this.btnAddMusic.Click += new System.EventHandler(this.btnAddMusic_Click);
            // 
            // tbProgress
            // 
            this.tbProgress.Location = new System.Drawing.Point(0, 259);
            this.tbProgress.Maximum = 100;
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(826, 69);
            this.tbProgress.TabIndex = 70;
            // 
            // lvwMusic
            // 
            this.lvwMusic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.lvw_Idx,
            this.lvw_Name,
            this.lvw_Path,
            this.lvw_Duration,
            this.lvw_Ext});
            this.lvwMusic.FullRowSelect = true;
            this.lvwMusic.GridLines = true;
            this.lvwMusic.Location = new System.Drawing.Point(11, 313);
            this.lvwMusic.Margin = new System.Windows.Forms.Padding(4);
            this.lvwMusic.Name = "lvwMusic";
            this.lvwMusic.Size = new System.Drawing.Size(805, 269);
            this.lvwMusic.TabIndex = 71;
            this.lvwMusic.UseCompatibleStateImageBehavior = false;
            this.lvwMusic.View = System.Windows.Forms.View.Details;
            this.lvwMusic.SelectedIndexChanged += new System.EventHandler(this.lvwMusic_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "";
            this.ID.Width = 0;
            // 
            // lvw_Idx
            // 
            this.lvw_Idx.Text = "序号";
            this.lvw_Idx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvw_Idx.Width = 45;
            // 
            // lvw_Name
            // 
            this.lvw_Name.Text = "歌曲名称";
            this.lvw_Name.Width = 155;
            // 
            // lvw_Path
            // 
            this.lvw_Path.Text = "歌曲路径";
            this.lvw_Path.Width = 266;
            // 
            // lvw_Duration
            // 
            this.lvw_Duration.DisplayIndex = 5;
            this.lvw_Duration.Text = "时长";
            this.lvw_Duration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvw_Duration.Width = 50;
            // 
            // lvw_Ext
            // 
            this.lvw_Ext.DisplayIndex = 4;
            this.lvw_Ext.Text = "扩展名";
            this.lvw_Ext.Width = 0;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRate.Location = new System.Drawing.Point(421, 84);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(44, 18);
            this.lblRate.TabIndex = 72;
            this.lblRate.Text = "码率";
            // 
            // lblRollingInfo
            // 
            this.lblRollingInfo.AutoSize = true;
            this.lblRollingInfo.Location = new System.Drawing.Point(103, 85);
            this.lblRollingInfo.Name = "lblRollingInfo";
            this.lblRollingInfo.Size = new System.Drawing.Size(62, 18);
            this.lblRollingInfo.TabIndex = 73;
            this.lblRollingInfo.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 50);
            this.panel1.TabIndex = 74;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(816, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 50);
            this.panel2.TabIndex = 75;
            // 
            // FormMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 595);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRollingInfo);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.lvwMusic);
            this.Controls.Add(this.tbProgress);
            this.Controls.Add(this.pictureBoxSpectrum);
            this.Controls.Add(this.btnVolumeUp);
            this.Controls.Add(this.btnVolumnDown);
            this.Controls.Add(this.btnAddMusic);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRetweet);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.btnRomdom);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.lblAudioPath);
            this.Controls.Add(this.lblVolumeTitle);
            this.Controls.Add(this.lblPlayProgress);
            this.Controls.Add(this.lblMusicTitle);
            this.Controls.Add(this.tsMusic);
            this.Controls.Add(this.tbVolume);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMusic";
            this.Text = "海玉视频播放";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMusic_FormClosing);
            this.Load += new System.EventHandler(this.FormMusic_Load);
            this.tsMusic.ResumeLayout(false);
            this.tsMusic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMusic;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbPlay;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.Label lblMusicTitle;
        private System.Windows.Forms.Label lblPlayProgress;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxSpectrum;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnAddList;
        private System.Windows.Forms.Button btnRomdom;
        private System.Windows.Forms.Button btnRetweet;
        private System.Windows.Forms.Button btnSound;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lblVolumeTitle;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Button btnVolumnDown;
        private System.Windows.Forms.Button btnVolumeUp;
        private System.Windows.Forms.Label lblAudioPath;
        private System.Windows.Forms.Button btnAddMusic;
        private System.Windows.Forms.TrackBar tbProgress;
        private System.Windows.Forms.ListView lvwMusic;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader lvw_Idx;
        private System.Windows.Forms.ColumnHeader lvw_Name;
        private System.Windows.Forms.ColumnHeader lvw_Path;
        private System.Windows.Forms.ColumnHeader lvw_Ext;
        private System.Windows.Forms.ColumnHeader lvw_Duration;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblRollingInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

