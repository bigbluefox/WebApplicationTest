using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model;

namespace Hsp.Test.IDataAccess
{
    /// <summary>
    /// 附件数据服务接口
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// 添加附件信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        int AddFile(FileModel model);

        /// <summary>
        /// 编辑附件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int EditFile(FileModel model);

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        int DeleteFile(Dictionary<string, string> paramList);

        /// <summary>
        /// 根据附件组ID获取附件列表
        /// </summary>
        /// <returns></returns>
        DataSet GetFileList(Dictionary<string, string> paramList);

        /// <summary>
        /// 根据附件参数获取附件分页列表
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        DataSet GetAttachmentsByPage(Dictionary<string, string> paramList, int pageSize, int pageIndex);
    }
}
