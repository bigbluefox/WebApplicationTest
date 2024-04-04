namespace HSP.MediaRetrieve
{
    partial class frmMediaInfo
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
            this.txtMediaInfo = new System.Windows.Forms.TextBox();
            this.txtMediaResult = new System.Windows.Forms.TextBox();
            this.btnCheckMedia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMediaInfo
            // 
            this.txtMediaInfo.Location = new System.Drawing.Point(13, 13);
            this.txtMediaInfo.Multiline = true;
            this.txtMediaInfo.Name = "txtMediaInfo";
            this.txtMediaInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaInfo.Size = new System.Drawing.Size(1039, 367);
            this.txtMediaInfo.TabIndex = 0;
            // 
            // txtMediaResult
            // 
            this.txtMediaResult.Location = new System.Drawing.Point(13, 386);
            this.txtMediaResult.Multiline = true;
            this.txtMediaResult.Name = "txtMediaResult";
            this.txtMediaResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaResult.Size = new System.Drawing.Size(1039, 170);
            this.txtMediaResult.TabIndex = 0;
            // 
            // btnCheckMedia
            // 
            this.btnCheckMedia.Location = new System.Drawing.Point(13, 567);
            this.btnCheckMedia.Name = "btnCheckMedia";
            this.btnCheckMedia.Size = new System.Drawing.Size(158, 36);
            this.btnCheckMedia.TabIndex = 1;
            this.btnCheckMedia.Text = "媒体文件检查";
            this.btnCheckMedia.UseVisualStyleBackColor = true;
            this.btnCheckMedia.Click += new System.EventHandler(this.btnCheckMedia_Click);
            // 
            // frmMediaInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 615);
            this.Controls.Add(this.btnCheckMedia);
            this.Controls.Add(this.txtMediaResult);
            this.Controls.Add(this.txtMediaInfo);
            this.Name = "frmMediaInfo";
            this.Text = "媒体信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMediaInfo;
        private System.Windows.Forms.TextBox txtMediaResult;
        private System.Windows.Forms.Button btnCheckMedia;
    }
}