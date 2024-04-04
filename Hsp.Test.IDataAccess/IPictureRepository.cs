using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IDataAccess
{
    /// <summary>
    /// 图片数据服务接口
    /// </summary>
    public interface IPictureRepository
    {


        /// <summary>
        /// 图片归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        DataSet ImageArchiving(Dictionary<string, string> paramList);
    }
}
