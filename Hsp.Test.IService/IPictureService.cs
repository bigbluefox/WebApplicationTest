using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 图片逻辑服务接口
    /// </summary>
    public interface IPictureService
    {

        /// <summary>
        /// 图片归档
        /// </summary>
        /// <param name="paramList">参数列表</param>
        void ImageArchiving(Dictionary<string, string> paramList);
    }
}
