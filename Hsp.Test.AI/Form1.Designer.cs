namespace Hsp.Test.AI
{
    partial class Form1
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
            this.btnAudioTest = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnImageTest = new System.Windows.Forms.Button();
            this.btnWordsTest = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGeneralEnhanced = new System.Windows.Forms.Button();
            this.btnWebImage = new System.Windows.Forms.Button();
            this.btnAccurate = new System.Windows.Forms.Button();
            this.btnBankCard = new System.Windows.Forms.Button();
            this.btnIdcard = new System.Windows.Forms.Button();
            this.btnDrivingLicense = new System.Windows.Forms.Button();
            this.btnVehicleLicense = new System.Windows.Forms.Button();
            this.btnPlateLicense = new System.Windows.Forms.Button();
            this.btnBusinessLicense = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnNlp = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAudioTest
            // 
            this.btnAudioTest.Location = new System.Drawing.Point(15, 16);
            this.btnAudioTest.Name = "btnAudioTest";
            this.btnAudioTest.Size = new System.Drawing.Size(115, 36);
            this.btnAudioTest.TabIndex = 0;
            this.btnAudioTest.Text = "语音识别";
            this.btnAudioTest.UseVisualStyleBackColor = true;
            this.btnAudioTest.Click += new System.EventHandler(this.btnAudioTest_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(780, 66);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(80, 18);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "处理结果";
            // 
            // btnImageTest
            // 
            this.btnImageTest.Location = new System.Drawing.Point(15, 66);
            this.btnImageTest.Name = "btnImageTest";
            this.btnImageTest.Size = new System.Drawing.Size(115, 36);
            this.btnImageTest.TabIndex = 0;
            this.btnImageTest.Text = "色情识别";
            this.btnImageTest.UseVisualStyleBackColor = true;
            this.btnImageTest.Click += new System.EventHandler(this.btnImageTest_Click);
            // 
            // btnWordsTest
            // 
            this.btnWordsTest.Location = new System.Drawing.Point(15, 118);
            this.btnWordsTest.Name = "btnWordsTest";
            this.btnWordsTest.Size = new System.Drawing.Size(115, 36);
            this.btnWordsTest.TabIndex = 0;
            this.btnWordsTest.Text = "文字识别";
            this.btnWordsTest.UseVisualStyleBackColor = true;
            this.btnWordsTest.Click += new System.EventHandler(this.btnWordsTest_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(9, 226);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(80, 18);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "文件路径";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(12, 266);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(848, 289);
            this.txtResult.TabIndex = 2;
            // 
            // btnGeneralEnhanced
            // 
            this.btnGeneralEnhanced.Location = new System.Drawing.Point(136, 118);
            this.btnGeneralEnhanced.Name = "btnGeneralEnhanced";
            this.btnGeneralEnhanced.Size = new System.Drawing.Size(115, 36);
            this.btnGeneralEnhanced.TabIndex = 0;
            this.btnGeneralEnhanced.Text = "增强识别";
            this.btnGeneralEnhanced.UseVisualStyleBackColor = true;
            this.btnGeneralEnhanced.Click += new System.EventHandler(this.btnGeneralEnhanced_Click);
            // 
            // btnWebImage
            // 
            this.btnWebImage.Location = new System.Drawing.Point(257, 118);
            this.btnWebImage.Name = "btnWebImage";
            this.btnWebImage.Size = new System.Drawing.Size(115, 36);
            this.btnWebImage.TabIndex = 0;
            this.btnWebImage.Text = "网图识别";
            this.btnWebImage.UseVisualStyleBackColor = true;
            this.btnWebImage.Click += new System.EventHandler(this.btnWebImage_Click);
            // 
            // btnAccurate
            // 
            this.btnAccurate.Location = new System.Drawing.Point(379, 118);
            this.btnAccurate.Name = "btnAccurate";
            this.btnAccurate.Size = new System.Drawing.Size(115, 36);
            this.btnAccurate.TabIndex = 0;
            this.btnAccurate.Text = "高精度识别";
            this.btnAccurate.UseVisualStyleBackColor = true;
            this.btnAccurate.Click += new System.EventHandler(this.btnAccurate_Click);
            // 
            // btnBankCard
            // 
            this.btnBankCard.Location = new System.Drawing.Point(501, 118);
            this.btnBankCard.Name = "btnBankCard";
            this.btnBankCard.Size = new System.Drawing.Size(115, 36);
            this.btnBankCard.TabIndex = 0;
            this.btnBankCard.Text = "银行卡识别";
            this.btnBankCard.UseVisualStyleBackColor = true;
            this.btnBankCard.Click += new System.EventHandler(this.btnBankCard_Click);
            // 
            // btnIdcard
            // 
            this.btnIdcard.Location = new System.Drawing.Point(623, 118);
            this.btnIdcard.Name = "btnIdcard";
            this.btnIdcard.Size = new System.Drawing.Size(115, 36);
            this.btnIdcard.TabIndex = 0;
            this.btnIdcard.Text = "身份证识别";
            this.btnIdcard.UseVisualStyleBackColor = true;
            this.btnIdcard.Click += new System.EventHandler(this.btnIdcard_Click);
            // 
            // btnDrivingLicense
            // 
            this.btnDrivingLicense.Location = new System.Drawing.Point(745, 118);
            this.btnDrivingLicense.Name = "btnDrivingLicense";
            this.btnDrivingLicense.Size = new System.Drawing.Size(115, 36);
            this.btnDrivingLicense.TabIndex = 0;
            this.btnDrivingLicense.Text = "驾驶证识别";
            this.btnDrivingLicense.UseVisualStyleBackColor = true;
            this.btnDrivingLicense.Click += new System.EventHandler(this.btnDrivingLicense_Click);
            // 
            // btnVehicleLicense
            // 
            this.btnVehicleLicense.Location = new System.Drawing.Point(15, 171);
            this.btnVehicleLicense.Name = "btnVehicleLicense";
            this.btnVehicleLicense.Size = new System.Drawing.Size(115, 36);
            this.btnVehicleLicense.TabIndex = 0;
            this.btnVehicleLicense.Text = "行驶证识别";
            this.btnVehicleLicense.UseVisualStyleBackColor = true;
            this.btnVehicleLicense.Click += new System.EventHandler(this.btnVehicleLicense_Click);
            // 
            // btnPlateLicense
            // 
            this.btnPlateLicense.Location = new System.Drawing.Point(136, 171);
            this.btnPlateLicense.Name = "btnPlateLicense";
            this.btnPlateLicense.Size = new System.Drawing.Size(115, 36);
            this.btnPlateLicense.TabIndex = 0;
            this.btnPlateLicense.Text = "车牌识别";
            this.btnPlateLicense.UseVisualStyleBackColor = true;
            this.btnPlateLicense.Click += new System.EventHandler(this.btnPlateLicense_Click);
            // 
            // btnBusinessLicense
            // 
            this.btnBusinessLicense.Location = new System.Drawing.Point(257, 171);
            this.btnBusinessLicense.Name = "btnBusinessLicense";
            this.btnBusinessLicense.Size = new System.Drawing.Size(127, 36);
            this.btnBusinessLicense.TabIndex = 0;
            this.btnBusinessLicense.Text = "营业执照识别";
            this.btnBusinessLicense.UseVisualStyleBackColor = true;
            this.btnBusinessLicense.Click += new System.EventHandler(this.btnBusinessLicense_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(257, 66);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 36);
            this.button5.TabIndex = 0;
            this.button5.Text = "驾驶证识别";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(378, 66);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(115, 36);
            this.button6.TabIndex = 0;
            this.button6.Text = "驾驶证识别";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            // 
            // btnNlp
            // 
            this.btnNlp.Location = new System.Drawing.Point(136, 16);
            this.btnNlp.Name = "btnNlp";
            this.btnNlp.Size = new System.Drawing.Size(130, 36);
            this.btnNlp.TabIndex = 0;
            this.btnNlp.Text = "自然语言处理";
            this.btnNlp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNlp.UseVisualStyleBackColor = true;
            this.btnNlp.Click += new System.EventHandler(this.btnNlp_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(499, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 36);
            this.button2.TabIndex = 0;
            this.button2.Text = "驾驶证识别";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(136, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 36);
            this.button3.TabIndex = 0;
            this.button3.Text = "驾驶证识别";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 567);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnNlp);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnBusinessLicense);
            this.Controls.Add(this.btnPlateLicense);
            this.Controls.Add(this.btnVehicleLicense);
            this.Controls.Add(this.btnDrivingLicense);
            this.Controls.Add(this.btnIdcard);
            this.Controls.Add(this.btnBankCard);
            this.Controls.Add(this.btnAccurate);
            this.Controls.Add(this.btnWebImage);
            this.Controls.Add(this.btnGeneralEnhanced);
            this.Controls.Add(this.btnWordsTest);
            this.Controls.Add(this.btnImageTest);
            this.Controls.Add(this.btnAudioTest);
            this.Name = "Form1";
            this.Text = "百度智能测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAudioTest;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnImageTest;
        private System.Windows.Forms.Button btnWordsTest;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGeneralEnhanced;
        private System.Windows.Forms.Button btnWebImage;
        private System.Windows.Forms.Button btnAccurate;
        private System.Windows.Forms.Button btnBankCard;
        private System.Windows.Forms.Button btnIdcard;
        private System.Windows.Forms.Button btnDrivingLicense;
        private System.Windows.Forms.Button btnVehicleLicense;
        private System.Windows.Forms.Button btnPlateLicense;
        private System.Windows.Forms.Button btnBusinessLicense;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnNlp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

