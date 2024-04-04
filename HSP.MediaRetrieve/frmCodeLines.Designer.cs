namespace HSP.MediaRetrieve
{
    partial class frmCodeLines
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
            this.btnProjectDir = new System.Windows.Forms.Button();
            this.lblProjectDir = new System.Windows.Forms.Label();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblCodeLineCount = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lvwCodesCount = new System.Windows.Forms.ListView();
            this.lvw_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Idx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnReList = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.lvw_ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProjectDir
            // 
            this.btnProjectDir.Location = new System.Drawing.Point(396, 21);
            this.btnProjectDir.Name = "btnProjectDir";
            this.btnProjectDir.Size = new System.Drawing.Size(145, 36);
            this.btnProjectDir.TabIndex = 0;
            this.btnProjectDir.Text = "选择项目目录";
            this.btnProjectDir.UseVisualStyleBackColor = true;
            this.btnProjectDir.Click += new System.EventHandler(this.btnProjectDir_Click);
            // 
            // lblProjectDir
            // 
            this.lblProjectDir.AutoSize = true;
            this.lblProjectDir.Location = new System.Drawing.Point(22, 30);
            this.lblProjectDir.Name = "lblProjectDir";
            this.lblProjectDir.Size = new System.Drawing.Size(98, 18);
            this.lblProjectDir.TabIndex = 1;
            this.lblProjectDir.Text = "项目目录：";
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Location = new System.Drawing.Point(22, 68);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(98, 18);
            this.lblFileType.TabIndex = 2;
            this.lblFileType.Text = "文件类型：";
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(22, 106);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(98, 18);
            this.lblFileCount.TabIndex = 3;
            this.lblFileCount.Text = "文件数量：";
            // 
            // lblCodeLineCount
            // 
            this.lblCodeLineCount.AutoSize = true;
            this.lblCodeLineCount.Location = new System.Drawing.Point(22, 144);
            this.lblCodeLineCount.Name = "lblCodeLineCount";
            this.lblCodeLineCount.Size = new System.Drawing.Size(98, 18);
            this.lblCodeLineCount.TabIndex = 4;
            this.lblCodeLineCount.Text = "代码行数：";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(126, 25);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(250, 28);
            this.txtDirectory.TabIndex = 6;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(553, 21);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 36);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始统计";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lvwCodesCount
            // 
            this.lvwCodesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvw_Id,
            this.lvw_Idx,
            this.lvw_Name,
            this.lvw_Path,
            this.lvw_Count});
            this.lvwCodesCount.ContextMenuStrip = this.lvw_ContextMenuStrip;
            this.lvwCodesCount.FullRowSelect = true;
            this.lvwCodesCount.Location = new System.Drawing.Point(27, 240);
            this.lvwCodesCount.Name = "lvwCodesCount";
            this.lvwCodesCount.Size = new System.Drawing.Size(996, 330);
            this.lvwCodesCount.TabIndex = 8;
            this.lvwCodesCount.UseCompatibleStateImageBehavior = false;
            this.lvwCodesCount.View = System.Windows.Forms.View.Details;
            this.lvwCodesCount.SelectedIndexChanged += new System.EventHandler(this.lvwCodesCount_SelectedIndexChanged);
            // 
            // lvw_Id
            // 
            this.lvw_Id.Text = "";
            this.lvw_Id.Width = 0;
            // 
            // lvw_Idx
            // 
            this.lvw_Idx.Text = "序号";
            this.lvw_Idx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvw_Idx.Width = 45;
            // 
            // lvw_Name
            // 
            this.lvw_Name.Text = "名称";
            this.lvw_Name.Width = 210;
            // 
            // lvw_Path
            // 
            this.lvw_Path.Text = "目录";
            this.lvw_Path.Width = 335;
            // 
            // lvw_Count
            // 
            this.lvw_Count.Text = "代码行数";
            this.lvw_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lvw_ContextMenuStrip
            // 
            this.lvw_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delToolStripMenuItem});
            this.lvw_ContextMenuStrip.Name = "lvw_ContextMenuStrip";
            this.lvw_ContextMenuStrip.Size = new System.Drawing.Size(135, 32);
            this.lvw_ContextMenuStrip.Text = "列项右键菜单";
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.delToolStripMenuItem.Text = "删除行";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.lvwCodesCount_RowDelete);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(668, 21);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(100, 36);
            this.btnClearList.TabIndex = 9;
            this.btnClearList.Text = "清空列表";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnReList
            // 
            this.btnReList.Location = new System.Drawing.Point(780, 21);
            this.btnReList.Name = "btnReList";
            this.btnReList.Size = new System.Drawing.Size(100, 36);
            this.btnReList.TabIndex = 10;
            this.btnReList.Text = "列表刷新";
            this.btnReList.UseVisualStyleBackColor = true;
            this.btnReList.Click += new System.EventHandler(this.btnReList_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(27, 201);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(349, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(396, 68);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(627, 156);
            this.txtResult.TabIndex = 13;
            this.txtResult.Text = "";
            // 
            // frmCodeLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 599);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnReList);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.lvwCodesCount);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.lblCodeLineCount);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.lblProjectDir);
            this.Controls.Add(this.btnProjectDir);
            this.Name = "frmCodeLines";
            this.Text = "项目代码行数统计";
            this.lvw_ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProjectDir;
        private System.Windows.Forms.Label lblProjectDir;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Label lblCodeLineCount;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView lvwCodesCount;
        private System.Windows.Forms.ColumnHeader lvw_Id;
        private System.Windows.Forms.ColumnHeader lvw_Idx;
        private System.Windows.Forms.ColumnHeader lvw_Name;
        private System.Windows.Forms.ColumnHeader lvw_Path;
        private System.Windows.Forms.ColumnHeader lvw_Count;
        private System.Windows.Forms.ContextMenuStrip lvw_ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnReList;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox txtResult;
    }
}