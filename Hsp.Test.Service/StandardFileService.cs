using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DataAccess;
using Hsp.Test.IDataAccess;
using Hsp.Test.IService;

namespace Hsp.Test.Service
{
    public class StandardFileService : IStandardFileService
    {
        /// <summary>
        /// 标准文件服务
        /// </summary>
        internal readonly IStandardFileRepository StandardFileRepository = new StandardFileRepository();

        /// <summary>
        /// 根据标准编号前缀获取对应修正代码
        /// </summary>
        /// <param name="code">标准编号前缀</param>
        /// <returns></returns>
        public string PreCodeCorresponding(string code)
        {
            return StandardFileRepository.PreCodeCorresponding(code);
        }
    }
}
