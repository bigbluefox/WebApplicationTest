using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTest.AppCodes;

namespace WebApplicationTest
{
    public partial class ServerFiles : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var SmtpHost = ConfigurationSettings.AppSettings["BatchSystemFilePath"];

            var rootPath = ConfigurationManager.AppSettings["BatchSystemFilePath"];

            if (string.IsNullOrEmpty(rootPath)) return;

            if (!Directory.Exists(rootPath))
            {
                lblFilePath.Text = "设定目录不存在";
                return;
            }

            var strPath = "<a href=\"#\">目录A</a>&nbsp; > &nbsp;  <a href=\"#\">目录B</a>";
            lblFilePath.Text = strPath;

            // 限定文件类型 doc,docx,xls,xlsx,ppt,pptx,pdf,ceb,wps,dps,et,rtf,txt

            //if (filetype == "doc" || filetype == "docx" || filetype == "xls" || filetype == "xlsx" || filetype == "wps" ||
            //    filetype == "ppt" || filetype == "pptx" || filetype == "pdf" || filetype == "ceb")
            //{
            //}


            try
            {
                var di = new DirectoryInfo(rootPath);
                List<FileAttribute> list = new List<FileAttribute>();

                //遍历文件夹
                foreach (var folder in di.GetDirectories())
                {
                    var li = new ListItem();
                    li.Text = folder.Name;
                    li.Value = folder.Name;
                    ddlFilePath.Items.Add(li);

                    //var p = folder.Extension

                    FileAttribute fileAttribute = new FileAttribute();
                    fileAttribute.FileName = folder.Name;
                    fileAttribute.FileSize = 0;
                    fileAttribute.FileType = 0;// "文件夹";

                    fileAttribute.Extension = "";
                    fileAttribute.FullName = folder.FullName;
                    fileAttribute.DirectoryName = "";
                    fileAttribute.CreationTime = folder.CreationTime;

                    list.Add(fileAttribute);
                }

                //遍历文件
                foreach (var file in di.GetFiles())
                {
                    var li = new ListItem();
                    li.Text = file.Name;
                    li.Value = file.Name;
                    ddlFilePath.Items.Add(li);

                    FileAttribute fileAttribute = new FileAttribute();
                    fileAttribute.FileName = file.Name;
                    fileAttribute.FileSize = file.Length;
                    fileAttribute.FileType = 1;// "文件";

                    fileAttribute.Extension = file.Extension;
                    fileAttribute.FullName = file.FullName;
                    fileAttribute.DirectoryName = file.DirectoryName;
                    fileAttribute.CreationTime = file.CreationTime;

                    list.Add(fileAttribute);

                    var ext = file.Extension;
                    var fullName = file.FullName;
                    var creationTime = file.CreationTime;
                    //var attributes = file.Attributes.CompareTo();
                    var directoryName = file.DirectoryName;
                    //var p = file.

                }


                GridView1.DataSource = list;
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                lblFilePath.Text = ex.Message;
                throw;
            }


//C#遍历指定文件夹中的所有文件 
//DirectoryInfo TheFolder=new DirectoryInfo(folderFullName);
////遍历文件夹
//foreach(DirectoryInfo NextFolder in TheFolder.GetDirectories())
//   this.listBox1.Items.Add(NextFolder.Name);
////遍历文件
//foreach(FileInfo NextFile in TheFolder.GetFiles())
//   this.listBox2.Items.Add(NextFile.Name); 
//===================================================================
//如何获取指定目录包含的文件和子目录
//    1. DirectoryInfo.GetFiles()：获取目录中（不包含子目录）的文件，返回类型为FileInfo[]，支持通配符查找；
//    2. DirectoryInfo.GetDirectories()：获取目录（不包含子目录）的子目录，返回类型为DirectoryInfo[]，支持通配符查找；
//    3. DirectoryInfo. GetFileSystemInfos()：获取指定目录下（不包含子目录）的文件和子目录，返回类型为FileSystemInfo[]，支持通配符查找；
//如何获取指定文件的基本信息；
//    FileInfo.Exists：获取指定文件是否存在；
//    FileInfo.Name，FileInfo.Extensioin：获取文件的名称和扩展名；
//    FileInfo.FullName：获取文件的全限定名称（完整路径）；
//    FileInfo.Directory：获取文件所在目录，返回类型为DirectoryInfo；
//    FileInfo.DirectoryName：获取文件所在目录的路径（完整路径）；
//    FileInfo.Length：获取文件的大小（字节数）；
//    FileInfo.IsReadOnly：获取文件是否只读；
//    FileInfo.Attributes：获取或设置指定文件的属性，返回类型为FileAttributes枚举，可以是多个值的组合
//    FileInfo.CreationTime、FileInfo.LastAccessTime、FileInfo.LastWriteTime：分别用于获取文件的创建时间、访问时间、修改时间；
        }
    }


}