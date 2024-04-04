namespace HSP.MediaRetrieve
{
    partial class frmMusic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMusic));
            this.btnAddMusic = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMusicInfo = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMusicInfo = new System.Windows.Forms.TextBox();
            this.lblMusicFileName = new System.Windows.Forms.Label();
            this.progressBarRight = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBarLeft = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.listBoxPlaylist = new System.Windows.Forms.ListBox();
            this.pictureBoxWaveForm = new System.Windows.Forms.PictureBox();
            this.btnAddPlayList = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelArtist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaveForm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddMusic
            // 
            resources.ApplyResources(this.btnAddMusic, "btnAddMusic");
            this.btnAddMusic.Name = "btnAddMusic";
            this.btnAddMusic.UseVisualStyleBackColor = true;
            this.btnAddMusic.Click += new System.EventHandler(this.btnAddMusic_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblMusicInfo
            // 
            resources.ApplyResources(this.lblMusicInfo, "lblMusicInfo");
            this.lblMusicInfo.Name = "lblMusicInfo";
            // 
            // btnPlay
            // 
            resources.ApplyResources(this.btnPlay, "btnPlay");
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            resources.ApplyResources(this.btnPause, "btnPause");
            this.btnPause.Name = "btnPause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtMusicInfo
            // 
            resources.ApplyResources(this.txtMusicInfo, "txtMusicInfo");
            this.txtMusicInfo.Name = "txtMusicInfo";
            // 
            // lblMusicFileName
            // 
            resources.ApplyResources(this.lblMusicFileName, "lblMusicFileName");
            this.lblMusicFileName.Name = "lblMusicFileName";
            // 
            // progressBarRight
            // 
            resources.ApplyResources(this.progressBarRight, "progressBarRight");
            this.progressBarRight.Maximum = 32768;
            this.progressBarRight.Name = "progressBarRight";
            this.progressBarRight.Step = 1;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // progressBarLeft
            // 
            resources.ApplyResources(this.progressBarLeft, "progressBarLeft");
            this.progressBarLeft.Maximum = 32768;
            this.progressBarLeft.Name = "progressBarLeft";
            this.progressBarLeft.Step = 1;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listBoxPlaylist
            // 
            this.listBoxPlaylist.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPlaylist, "listBoxPlaylist");
            this.listBoxPlaylist.Name = "listBoxPlaylist";
            // 
            // pictureBoxWaveForm
            // 
            this.pictureBoxWaveForm.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.pictureBoxWaveForm, "pictureBoxWaveForm");
            this.pictureBoxWaveForm.Name = "pictureBoxWaveForm";
            this.pictureBoxWaveForm.TabStop = false;
            // 
            // btnAddPlayList
            // 
            resources.ApplyResources(this.btnAddPlayList, "btnAddPlayList");
            this.btnAddPlayList.Name = "btnAddPlayList";
            this.btnAddPlayList.UseVisualStyleBackColor = true;
            this.btnAddPlayList.Click += new System.EventHandler(this.btnAddPlayList_Click);
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.Name = "labelTitle";
            // 
            // labelArtist
            // 
            resources.ApplyResources(this.labelArtist, "labelArtist");
            this.labelArtist.Name = "labelArtist";
            // 
            // frmMusic
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelArtist);
            this.Controls.Add(this.listBoxPlaylist);
            this.Controls.Add(this.pictureBoxWaveForm);
            this.Controls.Add(this.progressBarRight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMusicFileName);
            this.Controls.Add(this.txtMusicInfo);
            this.Controls.Add(this.btnAddPlayList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblMusicInfo);
            this.Controls.Add(this.btnAddMusic);
            this.Name = "frmMusic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMusic_FormClosing);
            this.Load += new System.EventHandler(this.frmMusic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaveForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddMusic;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblMusicInfo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMusicInfo;
        private System.Windows.Forms.Label lblMusicFileName;
        private System.Windows.Forms.ProgressBar progressBarRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ListBox listBoxPlaylist;
        private System.Windows.Forms.PictureBox pictureBoxWaveForm;
        private System.Windows.Forms.Button btnAddPlayList;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelArtist;
    }
}