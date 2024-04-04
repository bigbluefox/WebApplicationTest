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
using Hsp.Test.Model.Media;

namespace Hsp.Test.Service
{
    /// <summary>
    /// 图书逻辑服务
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// 图书服务
        /// </summary>
        internal readonly IBookRepository BookRepository = new BookRepository();

        #region 图书归档

        /// <summary>
        /// 图书归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        public void BookArchiving(Dictionary<string, string> paramList)
        {
            DataSet ds = BookRepository.BookArchiving(paramList);
            if (ds == null || ds.Tables[0].Rows.Count <= 0) return;

            // 操作删除重复图书
            var defaultPath = ConfigurationManager.AppSettings["DefalutBook"] ?? "";
            if (string.IsNullOrEmpty(defaultPath)) return;
            if (!defaultPath.EndsWith("\\")) defaultPath += "\\";
            var list = new DataTableToList<Book>(ds.Tables[0]).ToList();

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
