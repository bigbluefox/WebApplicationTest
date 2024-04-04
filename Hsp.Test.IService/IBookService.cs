using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 图书逻辑服务接口
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// 图书归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        void BookArchiving(Dictionary<string, string> paramList);
    }
}
