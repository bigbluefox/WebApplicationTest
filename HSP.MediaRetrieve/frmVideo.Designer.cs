namespace HSP.MediaRetrieve
{
    partial class frmVideo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVideo));
            this.btnCheckVideo = new System.Windows.Forms.Button();
            this.lblVideoTitle = new System.Windows.Forms.Label();

            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAddVideoFile = new System.Windows.Forms.Button();
            this.txtVideoInfo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddVideo = new System.Windows.Forms.Button();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.btnFullScreen1 = new System.Windows.Forms.Button();
            this.lblVideoProgess = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.tbProgress = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckVideo
            // 
            this.btnCheckVideo.Location = new System.Drawing.Point(12, 567);
            this.btnCheckVideo.Name = "btnCheckVideo";
            this.btnCheckVideo.Size = new System.Drawing.Size(106, 36);
            this.btnCheckVideo.TabIndex = 1;
            this.btnCheckVideo.Text = "检查视频";
            this.btnCheckVideo.UseVisualStyleBackColor = true;
            this.btnCheckVideo.Click += new System.EventHandler(this.btnCheckVideo_Click);
            // 
            // lblVideoTitle
            // 
            this.lblVideoTitle.AutoSize = true;
            this.lblVideoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoTitle.Location = new System.Drawing.Point(4, 6);
            this.lblVideoTitle.Name = "lblVideoTitle";
            this.lblVideoTitle.Size = new System.Drawing.Size(96, 25);
            this.lblVideoTitle.TabIndex = 2;
            this.lblVideoTitle.Text = "视频信息";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(1064, 615);
            this.axWindowsMediaPlayer1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnAddVideoFile
            // 
            this.btnAddVideoFile.Location = new System.Drawing.Point(267, 8);
            this.btnAddVideoFile.Name = "btnAddVideoFile";
            this.btnAddVideoFile.Size = new System.Drawing.Size(106, 36);
            this.btnAddVideoFile.TabIndex = 4;
            this.btnAddVideoFile.Text = "打开视频";
            this.btnAddVideoFile.UseVisualStyleBackColor = true;
            this.btnAddVideoFile.Click += new System.EventHandler(this.btnAddVideoFile_Click);
            // 
            // txtVideoInfo
            // 
            this.txtVideoInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVideoInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtVideoInfo.Location = new System.Drawing.Point(492, 5);
            this.txtVideoInfo.Multiline = true;
            this.txtVideoInfo.Name = "txtVideoInfo";
            this.txtVideoInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVideoInfo.Size = new System.Drawing.Size(567, 90);
            this.txtVideoInfo.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddVideo);
            this.panel1.Controls.Add(this.btnFullScreen);
            this.panel1.Controls.Add(this.btnMute);
            this.panel1.Controls.Add(this.btnFullScreen1);
            this.panel1.Controls.Add(this.txtVideoInfo);
            this.panel1.Controls.Add(this.btnAddVideoFile);
            this.panel1.Controls.Add(this.lblVideoProgess);
            this.panel1.Controls.Add(this.lblVideoTitle);
            this.panel1.Controls.Add(this.tbVolume);
            this.panel1.Controls.Add(this.tbProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 515);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1064, 100);
            this.panel1.TabIndex = 6;
            // 
            // btnAddVideo
            // 
            this.btnAddVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddVideo.BackgroundImage")));
            this.btnAddVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddVideo.FlatAppearance.BorderSize = 0;
            this.btnAddVideo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVideo.Location = new System.Drawing.Point(6, 60);
            this.btnAddVideo.Name = "btnAddVideo";
            this.btnAddVideo.Size = new System.Drawing.Size(32, 32);
            this.btnAddVideo.TabIndex = 8;
            this.btnAddVideo.UseVisualStyleBackColor = true;
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFullScreen.BackgroundImage")));
            this.btnFullScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFullScreen.FlatAppearance.BorderSize = 0;
            this.btnFullScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFullScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullScreen.Location = new System.Drawing.Point(44, 60);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(32, 32);
            this.btnFullScreen.TabIndex = 8;
            this.btnFullScreen.UseVisualStyleBackColor = true;
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
            this.btnMute.Location = new System.Drawing.Point(236, 58);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(32, 32);
            this.btnMute.TabIndex = 8;
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnFullScreen1
            // 
            this.btnFullScreen1.Location = new System.Drawing.Point(380, 8);
            this.btnFullScreen1.Name = "btnFullScreen1";
            this.btnFullScreen1.Size = new System.Drawing.Size(106, 36);
            this.btnFullScreen1.TabIndex = 6;
            this.btnFullScreen1.Text = "全屏播放";
            this.btnFullScreen1.UseVisualStyleBackColor = true;
            this.btnFullScreen1.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // lblVideoProgess
            // 
            this.lblVideoProgess.AutoSize = true;
            this.lblVideoProgess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoProgess.Location = new System.Drawing.Point(82, 64);
            this.lblVideoProgess.Name = "lblVideoProgess";
            this.lblVideoProgess.Size = new System.Drawing.Size(96, 25);
            this.lblVideoProgess.TabIndex = 2;
            this.lblVideoProgess.Text = "视频进度";
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(267, 59);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(222, 69);
            this.tbVolume.TabIndex = 7;
            this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
            // 
            // tbProgress
            // 
            this.tbProgress.Location = new System.Drawing.Point(0, 34);
            this.tbProgress.Maximum = 100;
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(489, 69);
            this.tbProgress.TabIndex = 9;
            // 
            // frmVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 615);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.btnCheckVideo);
            this.Name = "frmVideo";
            this.Text = "视频处理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVideo_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckVideo;
        private System.Windows.Forms.Label lblVideoTitle;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAddVideoFile;
        private System.Windows.Forms.TextBox txtVideoInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFullScreen1;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.Label lblVideoProgess;
        private System.Windows.Forms.Button btnAddVideo;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.TrackBar tbProgress;
    }
}