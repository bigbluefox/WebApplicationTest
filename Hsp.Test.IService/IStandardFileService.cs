using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IService
{
    public interface IStandardFileService
    {
        /// <summary>
        /// 根据标准编号前缀获取对应修正代码
        /// </summary>
        /// <param name="code">标准编号前缀</param>
        /// <returns></returns>
        string PreCodeCorresponding(string code);
    }
}
