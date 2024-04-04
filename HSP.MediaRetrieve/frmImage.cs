using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSP.MediaRetrieve
{
    public partial class frmImage : Form
    {
        public frmImage()
        {
            InitializeComponent();

            var strSqlCeConnection = ConfigurationManager.AppSettings["SqlCeConnectionString"] ?? "";
            //System.Data.SqlServerCe.SqlCeConnection conn = new System.Data.SqlServerCe.SqlCeConnection(strSqlCeConnection);
            //conn.Open();

        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cbxMd5 = new System.Windows.Forms.CheckBox();
            this.cbxSha1 = new System.Windows.Forms.CheckBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.lblHash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cbxMd5
            // 
            this.cbxMd5.AutoSize = true;
            this.cbxMd5.Location = new System.Drawing.Point(162, 133);
            this.cbxMd5.Name = "cbxMd5";
            this.cbxMd5.Size = new System.Drawing.Size(61, 22);
            this.cbxMd5.TabIndex = 1;
            this.cbxMd5.Text = "MD5";
            this.cbxMd5.UseVisualStyleBackColor = true;
            // 
            // cbxSha1
            // 
            this.cbxSha1.AutoSize = true;
            this.cbxSha1.Location = new System.Drawing.Point(229, 132);
            this.cbxSha1.Name = "cbxSha1";
            this.cbxSha1.Size = new System.Drawing.Size(70, 22);
            this.cbxSha1.TabIndex = 2;
            this.cbxSha1.Text = "SHA1";
            this.cbxSha1.UseVisualStyleBackColor = true;
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(47, 50);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(71, 18);
            this.lblFilePath.TabIndex = 3;
            this.lblFilePath.Text = " 目录：";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(162, 39);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(226, 28);
            this.txtDirectory.TabIndex = 5;
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Location = new System.Drawing.Point(394, 39);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(96, 28);
            this.btnSelectDir.TabIndex = 0;
            this.btnSelectDir.Text = "目录选择";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // lblHash
            // 
            this.lblHash.AutoSize = true;
            this.lblHash.Location = new System.Drawing.Point(47, 136);
            this.lblHash.Name = "lblHash";
            this.lblHash.Size = new System.Drawing.Size(62, 18);
            this.lblHash.TabIndex = 3;
            this.lblHash.Text = "哈希：";
            // 
            // frmImage
            // 
            this.ClientSize = new System.Drawing.Size(1050, 592);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblHash);
            this.Controls.Add(this.cbxSha1);
            this.Controls.Add(this.cbxMd5);
            this.Controls.Add(this.btnSelectDir);
            this.Controls.Add(this.button1);
            this.Name = "frmImage";
            this.Text = "图片处理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {

        }
    }
}
