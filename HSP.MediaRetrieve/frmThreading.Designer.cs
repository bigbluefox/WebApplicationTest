namespace HSP.MediaRetrieve
{
    partial class frmThreading
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
            this.lblResult = new System.Windows.Forms.Label();
            this.rtbThreadingMsg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(13, 13);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(62, 18);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "label1";
            // 
            // rtbThreadingMsg
            // 
            this.rtbThreadingMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbThreadingMsg.Location = new System.Drawing.Point(0, 0);
            this.rtbThreadingMsg.Name = "rtbThreadingMsg";
            this.rtbThreadingMsg.Size = new System.Drawing.Size(850, 565);
            this.rtbThreadingMsg.TabIndex = 1;
            this.rtbThreadingMsg.Text = "";
            // 
            // frmThreading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 565);
            this.Controls.Add(this.rtbThreadingMsg);
            this.Controls.Add(this.lblResult);
            this.Name = "frmThreading";
            this.Text = "frmThreading";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.RichTextBox rtbThreadingMsg;
    }
}