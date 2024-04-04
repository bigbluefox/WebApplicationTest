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
    public partial class Rename : System.Web.UI.Page
    {
        public int Total = 0;

        /// <summary>
        ///    本地标准服务
        /// </summary>
        internal readonly IStandardService StandardService = new StandardService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 标准名称字符替换

        /// <summary>
        /// 标准名称字符替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            Total = 0;
            var source = this.txtSource.Text;
            var target = this.txtTarget.Text;

            if (source.Length == 0)
            {
                this.lblResult.Text = "请输入被替换的字符！";
                return;
            }

            List<StandardLocal> list = StandardService.GetStandardList();

            foreach (var standard in list)
            {
                if (standard.FileName.IndexOf(source, StringComparison.Ordinal) <= -1 ||
                    !File.Exists(standard.FullName)) continue;
                try
                {
                    var strNewName =
                        Path.Combine(standard.DirectoryName + "\\" + standard.FileName.Replace(source, target));
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

            this.lblResult.Text = "共替换" + Total + "个文件！";
        }

        #endregion






    }
}