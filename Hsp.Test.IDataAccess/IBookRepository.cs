using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IDataAccess
{
    /// <summary>
    /// 图书数据服务接口
    /// </summary>
    public interface IBookRepository
    {

        /// <summary>
        /// 图书归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        DataSet BookArchiving(Dictionary<string, string> paramList);
    }
}
