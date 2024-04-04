using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface IAttachmentService
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
        /// 根据附件ID删除附件信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        int DeleteFile(Dictionary<string, string> paramList);

        /// <summary>
        /// 根据附件组ID获取附件列表
        /// </summary>
        /// <returns></returns>
        List<FileModel> GetFileList(Dictionary<string, string> paramList);

        /// <summary>
        /// 根据附件ID获取附件实体数据
        /// </summary>
        /// <param name="strFileId">附件ID</param>
        /// <returns></returns>
        FileModel GetFileModel(string strFileId);

    }
}
