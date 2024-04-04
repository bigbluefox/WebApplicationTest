namespace HSP.MediaRetrieve
{
    partial class frmAudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAudio));
            this.btnCheckAudio = new System.Windows.Forms.Button();
            this.lblAudioMessage = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnAddMusicFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblAudioInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckAudio
            // 
            this.btnCheckAudio.Location = new System.Drawing.Point(830, 12);
            this.btnCheckAudio.Name = "btnCheckAudio";
            this.btnCheckAudio.Size = new System.Drawing.Size(106, 36);
            this.btnCheckAudio.TabIndex = 0;
            this.btnCheckAudio.Text = "检查音频";
            this.btnCheckAudio.UseVisualStyleBackColor = true;
            this.btnCheckAudio.Click += new System.EventHandler(this.btnCheckAudio_Click);
            // 
            // lblAudioMessage
            // 
            this.lblAudioMessage.AutoSize = true;
            this.lblAudioMessage.Location = new System.Drawing.Point(12, 191);
            this.lblAudioMessage.Name = "lblAudioMessage";
            this.lblAudioMessage.Size = new System.Drawing.Size(62, 18);
            this.lblAudioMessage.TabIndex = 3;
            this.lblAudioMessage.Text = "label1";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(11, 11);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(457, 103);
            this.axWindowsMediaPlayer1.TabIndex = 4;
            // 
            // btnAddMusicFile
            // 
            this.btnAddMusicFile.Location = new System.Drawing.Point(718, 12);
            this.btnAddMusicFile.Name = "btnAddMusicFile";
            this.btnAddMusicFile.Size = new System.Drawing.Size(106, 36);
            this.btnAddMusicFile.TabIndex = 0;
            this.btnAddMusicFile.Text = "添加音频";
            this.btnAddMusicFile.UseVisualStyleBackColor = true;
            this.btnAddMusicFile.Click += new System.EventHandler(this.btnAddMusicFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblAudioInfo
            // 
            this.lblAudioInfo.AutoSize = true;
            this.lblAudioInfo.Location = new System.Drawing.Point(718, 55);
            this.lblAudioInfo.Name = "lblAudioInfo";
            this.lblAudioInfo.Size = new System.Drawing.Size(62, 18);
            this.lblAudioInfo.TabIndex = 5;
            this.lblAudioInfo.Text = "label1";
            // 
            // frmAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 615);
            this.Controls.Add(this.lblAudioInfo);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.lblAudioMessage);
            this.Controls.Add(this.btnAddMusicFile);
            this.Controls.Add(this.btnCheckAudio);
            this.Name = "frmAudio";
            this.Text = "音频处理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAudio_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckAudio;
        private System.Windows.Forms.Label lblAudioMessage;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button btnAddMusicFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblAudioInfo;
    }
}