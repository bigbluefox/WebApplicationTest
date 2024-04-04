namespace HSP.MediaRetrieve
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnImage = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.新建搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.视图VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更多选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作向导ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于我们ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.項目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.統計代碼行數ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnThreadingTest = new System.Windows.Forms.Button();
            this.btnGetMediaInfo = new System.Windows.Forms.Button();
            this.btnShowMusic = new System.Windows.Forms.Button();
            this.btnMediaInfo = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.threadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadingTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginInvokeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbList = new System.Windows.Forms.ToolStripButton();
            this.tsbBars = new System.Windows.Forms.ToolStripButton();
            this.btnAttach = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImage
            // 
            this.btnImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImage.Location = new System.Drawing.Point(894, 24);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(136, 38);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "检索图片文件";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAudio.Location = new System.Drawing.Point(894, 76);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(136, 38);
            this.btnAudio.TabIndex = 0;
            this.btnAudio.Text = "检索音频文件";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // btnVideo
            // 
            this.btnVideo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnVideo.Location = new System.Drawing.Point(894, 128);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(136, 38);
            this.btnVideo.TabIndex = 0;
            this.btnVideo.Text = "检索视频文件";
            this.btnVideo.UseVisualStyleBackColor = true;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.视图VToolStripMenuItem,
            this.选项ToolStripMenuItem,
            this.項目ToolStripMenuItem,
            this.threadingToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建搜索ToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 28);
            this.toolStripMenuItem1.Text = "文件(&F)";
            // 
            // 新建搜索ToolStripMenuItem
            // 
            this.新建搜索ToolStripMenuItem.Name = "新建搜索ToolStripMenuItem";
            this.新建搜索ToolStripMenuItem.Size = new System.Drawing.Size(176, 28);
            this.新建搜索ToolStripMenuItem.Text = "新建搜索";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 28);
            this.toolStripMenuItem2.Text = "&退出程序(&X)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // 视图VToolStripMenuItem
            // 
            this.视图VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片预览ToolStripMenuItem,
            this.文件删除ToolStripMenuItem});
            this.视图VToolStripMenuItem.Name = "视图VToolStripMenuItem";
            this.视图VToolStripMenuItem.Size = new System.Drawing.Size(82, 28);
            this.视图VToolStripMenuItem.Text = "视图(&V)";
            // 
            // 图片预览ToolStripMenuItem
            // 
            this.图片预览ToolStripMenuItem.Name = "图片预览ToolStripMenuItem";
            this.图片预览ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.图片预览ToolStripMenuItem.Text = "图片预览";
            // 
            // 文件删除ToolStripMenuItem
            // 
            this.文件删除ToolStripMenuItem.Name = "文件删除ToolStripMenuItem";
            this.文件删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.文件删除ToolStripMenuItem.Text = "文件删除";
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更多选项ToolStripMenuItem});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(85, 28);
            this.选项ToolStripMenuItem.Text = "选项(&O)";
            // 
            // 更多选项ToolStripMenuItem
            // 
            this.更多选项ToolStripMenuItem.Name = "更多选项ToolStripMenuItem";
            this.更多选项ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.更多选项ToolStripMenuItem.Text = "更多选项";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作向导ToolStripMenuItem,
            this.检查更新ToolStripMenuItem,
            this.关于我们ToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(84, 28);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 操作向导ToolStripMenuItem
            // 
            this.操作向导ToolStripMenuItem.Name = "操作向导ToolStripMenuItem";
            this.操作向导ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.操作向导ToolStripMenuItem.Text = "操作向导";
            // 
            // 检查更新ToolStripMenuItem
            // 
            this.检查更新ToolStripMenuItem.Name = "检查更新ToolStripMenuItem";
            this.检查更新ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.检查更新ToolStripMenuItem.Text = "检查更新";
            // 
            // 关于我们ToolStripMenuItem
            // 
            this.关于我们ToolStripMenuItem.Name = "关于我们ToolStripMenuItem";
            this.关于我们ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.关于我们ToolStripMenuItem.Text = "关于我们";
            // 
            // 項目ToolStripMenuItem
            // 
            this.項目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.統計代碼行數ToolStripMenuItem});
            this.項目ToolStripMenuItem.Name = "項目ToolStripMenuItem";
            this.項目ToolStripMenuItem.Size = new System.Drawing.Size(81, 28);
            this.項目ToolStripMenuItem.Text = "項目(&P)";
            // 
            // 統計代碼行數ToolStripMenuItem
            // 
            this.統計代碼行數ToolStripMenuItem.Name = "統計代碼行數ToolStripMenuItem";
            this.統計代碼行數ToolStripMenuItem.Size = new System.Drawing.Size(209, 28);
            this.統計代碼行數ToolStripMenuItem.Text = "统计代码行数(&L)";
            this.統計代碼行數ToolStripMenuItem.Click += new System.EventHandler(this.統計代碼行數ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 36);
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1064, 583);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnThreadingTest);
            this.tabPage1.Controls.Add(this.btnGetMediaInfo);
            this.tabPage1.Controls.Add(this.btnShowMusic);
            this.tabPage1.Controls.Add(this.btnMediaInfo);
            this.tabPage1.Controls.Add(this.btnImage);
            this.tabPage1.Controls.Add(this.btnAudio);
            this.tabPage1.Controls.Add(this.btnVideo);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1056, 539);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "  首页";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(894, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "空白";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnThreadingTest
            // 
            this.btnThreadingTest.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnThreadingTest.Location = new System.Drawing.Point(894, 418);
            this.btnThreadingTest.Name = "btnThreadingTest";
            this.btnThreadingTest.Size = new System.Drawing.Size(136, 36);
            this.btnThreadingTest.TabIndex = 5;
            this.btnThreadingTest.Text = "线程测试";
            this.btnThreadingTest.UseVisualStyleBackColor = true;
            this.btnThreadingTest.Click += new System.EventHandler(this.btnThreadingTest_Click);
            // 
            // btnGetMediaInfo
            // 
            this.btnGetMediaInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetMediaInfo.Location = new System.Drawing.Point(894, 323);
            this.btnGetMediaInfo.Name = "btnGetMediaInfo";
            this.btnGetMediaInfo.Size = new System.Drawing.Size(136, 36);
            this.btnGetMediaInfo.TabIndex = 5;
            this.btnGetMediaInfo.Text = "媒体信息";
            this.btnGetMediaInfo.UseVisualStyleBackColor = true;
            this.btnGetMediaInfo.Click += new System.EventHandler(this.btnGetMediaInfo_Click);
            // 
            // btnShowMusic
            // 
            this.btnShowMusic.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShowMusic.Location = new System.Drawing.Point(894, 223);
            this.btnShowMusic.Name = "btnShowMusic";
            this.btnShowMusic.Size = new System.Drawing.Size(136, 36);
            this.btnShowMusic.TabIndex = 3;
            this.btnShowMusic.Text = "音乐播放";
            this.btnShowMusic.UseVisualStyleBackColor = true;
            this.btnShowMusic.Click += new System.EventHandler(this.btnShowMusic_Click);
            // 
            // btnMediaInfo
            // 
            this.btnMediaInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMediaInfo.Location = new System.Drawing.Point(894, 273);
            this.btnMediaInfo.Name = "btnMediaInfo";
            this.btnMediaInfo.Size = new System.Drawing.Size(136, 36);
            this.btnMediaInfo.TabIndex = 4;
            this.btnMediaInfo.Text = "媒体测试";
            this.btnMediaInfo.UseVisualStyleBackColor = true;
            this.btnMediaInfo.Click += new System.EventHandler(this.btnMediaInfo_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1056, 539);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = " 搜索规则 ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbList,
            this.tsbBars});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1050, 89);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "列表";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnAttach);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1056, 539);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = " 重复文件 ";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1056, 539);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = " 重复目录 ";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // threadingToolStripMenuItem
            // 
            this.threadingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadingTestToolStripMenuItem,
            this.threadToolStripMenuItem,
            this.beginInvokeToolStripMenuItem,
            this.threadPoolToolStripMenuItem});
            this.threadingToolStripMenuItem.Name = "threadingToolStripMenuItem";
            this.threadingToolStripMenuItem.Size = new System.Drawing.Size(80, 28);
            this.threadingToolStripMenuItem.Text = "线程(&T)";
            // 
            // threadingTestToolStripMenuItem
            // 
            this.threadingTestToolStripMenuItem.Name = "threadingTestToolStripMenuItem";
            this.threadingTestToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.threadingTestToolStripMenuItem.Text = "线程测试";
            this.threadingTestToolStripMenuItem.Click += new System.EventHandler(this.threadingTestToolStripMenuItem_Click);
            // 
            // threadToolStripMenuItem
            // 
            this.threadToolStripMenuItem.Name = "threadToolStripMenuItem";
            this.threadToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.threadToolStripMenuItem.Text = "使用Thread类";
            this.threadToolStripMenuItem.Click += new System.EventHandler(this.threadToolStripMenuItem_Click);
            // 
            // beginInvokeToolStripMenuItem
            // 
            this.beginInvokeToolStripMenuItem.Name = "beginInvokeToolStripMenuItem";
            this.beginInvokeToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.beginInvokeToolStripMenuItem.Text = "使用Delegate.BeginInvoke ";
            this.beginInvokeToolStripMenuItem.Click += new System.EventHandler(this.beginInvokeToolStripMenuItem_Click);
            // 
            // threadPoolToolStripMenuItem
            // 
            this.threadPoolToolStripMenuItem.Name = "threadPoolToolStripMenuItem";
            this.threadPoolToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.threadPoolToolStripMenuItem.Text = "使用线程池";
            this.threadPoolToolStripMenuItem.Click += new System.EventHandler(this.threadPoolToolStripMenuItem_Click);
            // 
            // tsbList
            // 
            this.tsbList.Image = ((System.Drawing.Image)(resources.GetObject("tsbList.Image")));
            this.tsbList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbList.Name = "tsbList";
            this.tsbList.Size = new System.Drawing.Size(52, 86);
            this.tsbList.Text = "列表";
            this.tsbList.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbBars
            // 
            this.tsbBars.Image = ((System.Drawing.Image)(resources.GetObject("tsbBars.Image")));
            this.tsbBars.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbBars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBars.Name = "tsbBars";
            this.tsbBars.Padding = new System.Windows.Forms.Padding(5);
            this.tsbBars.Size = new System.Drawing.Size(62, 86);
            this.tsbBars.Text = "条码";
            this.tsbBars.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnAttach
            // 
            this.btnAttach.BackgroundImage = global::HSP.MediaRetrieve.Properties.Resources.attach11;
            this.btnAttach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAttach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttach.FlatAppearance.BorderSize = 0;
            this.btnAttach.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAttach.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttach.Location = new System.Drawing.Point(405, 129);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(128, 128);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 615);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "文件检索";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Button btnVideo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 视图VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 图片预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更多选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作向导ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检查更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于我们ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnShowMusic;
        private System.Windows.Forms.Button btnMediaInfo;
        private System.Windows.Forms.Button btnGetMediaInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbList;
        private System.Windows.Forms.ToolStripButton tsbBars;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnThreadingTest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 項目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 統計代碼行數ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadingTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginInvokeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadPoolToolStripMenuItem;
    }
}

