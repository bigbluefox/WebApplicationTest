using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Common;
using Hsp.Test.DataAccess;
using Hsp.Test.IDataAccess;
using Hsp.Test.IService;
using Hsp.Test.Model;

namespace Hsp.Test.Service
{
    /// <summary>
    /// 图片逻辑服务
    /// </summary>
    public class PictureService : IPictureService
    {
        /// <summary>
        /// 图片服务
        /// </summary>
        internal readonly IPictureRepository PictureRepository = new PictureRepository();

        #region 图片归档

        /// <summary>
        /// 图片归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public void ImageArchiving(Dictionary<string, string> paramList)
        {
            DataSet ds = PictureRepository.ImageArchiving(paramList);
            if (ds == null || ds.Tables[0].Rows.Count <= 0) return;

            // 操作删除重复图片
            var defaultPath = ConfigurationManager.AppSettings["DefalutImage"] ?? "";
            if (string.IsNullOrEmpty(defaultPath)) return;
            if (!defaultPath.EndsWith("\\")) defaultPath += "\\";
            var list = new DataTableToList<Picture>(ds.Tables[0]).ToList();

            foreach (var media in list)
            {
                //if (media.FullName.IndexOf(defaultPath, StringComparison.Ordinal) == -1){}

                if (File.Exists(media.FullName))
                {
                    File.Delete(media.FullName);
                }
            }
        }

        #endregion
    }
}
