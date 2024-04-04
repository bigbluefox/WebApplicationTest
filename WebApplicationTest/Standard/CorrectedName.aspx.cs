using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

namespace WebApplicationTest.Standard
{
    public partial class CorrectedName : System.Web.UI.Page
    {
        public int Total = 0;

        /// <summary>
        ///    本地标准服务
        /// </summary>
        internal readonly IStandardService StandardService = new StandardService();

        protected void Page_Load(object sender, EventArgs e)
        {
            //E:\Htty\标准库1\Renamed
        }

        /// <summary>
        /// 修正华润标准名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRename_Click(object sender, EventArgs e)
        {
            Total = 0;
            var txtTargetPath = @"E:\Htty\标准库1\Renamed\";

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                if (!File.Exists(standard.FullName)) continue;
                try
                {
                    var strNewName = Path.Combine(standard.DirectoryName + "\\Renamed\\" + standard.FileName + standard.FileExt);
                    FileInfo fileInfo = new FileInfo(standard.FullName);
                    fileInfo.MoveTo(strNewName);

                    Total++;
                }
                catch (Exception ex)
                {
                    //throw;
                }
                finally
                {
                    Dispose();
                }

            }

            this.lblResult.Text = "共修正了" + Total + "个文件！";
        }

        protected void btnCorrectedName_Click(object sender, EventArgs e)
        {
            return;

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                var fileName = standard.DirectoryName + "\\Renamed\\" + standard.FileName;

                if (!File.Exists(fileName)) continue;
                try
                {
                    var strNewName = Path.Combine(fileName + standard.FileExt);
                    FileInfo fileInfo = new FileInfo(fileName);
                    fileInfo.MoveTo(strNewName);

                    Total++;
                }
                catch (Exception ex)
                {
                    //throw;
                }
                finally
                {
                    Dispose();
                }

            }

            this.lblResult.Text = "共修正了" + Total + "个文件！";
        }

        /// <summary>
        /// 缺乏编码文件名称修复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCodeToFileName_Click(object sender, EventArgs e)
        {

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                if(standard.A107.Length > 0) continue;

                var fileName = standard.DirectoryName + "\\" + standard.A100 + " " + standard.FileName;

                if (!File.Exists(standard.FullName)) continue;
                try
                {
                    //var strNewName = Path.Combine(fileName + standard.FileExt);
                    FileInfo fileInfo = new FileInfo(standard.FullName);
                    fileInfo.MoveTo(fileName);

                    Total++;
                }
                catch (Exception ex)
                {
                    //throw;
                }
                finally
                {
                    Dispose();
                }

            }

            this.lblResult.Text = "共修正了" + Total + "个文件！";
        }
    }
}