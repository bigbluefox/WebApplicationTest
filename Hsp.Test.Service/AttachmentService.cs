using System;
using System.Collections.Generic;
using System.Data;
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
    /// 附件服务
    /// </summary>
    public class AttachmentService : IAttachmentService
    {
        /// <summary>
        /// 附件服务
        /// </summary>
        internal readonly IAttachmentRepository AttachmentRepository = new AttachmentRepository();
        
        #region 添加附件信息

        /// <summary>
        /// 添加附件信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        public int AddFile(FileModel model)
        {
            return AttachmentRepository.AddFile(model);
        }

        #endregion

        #region 编辑附件信息

        /// <summary>
        /// 编辑附件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditFile(FileModel model)
        {
           return AttachmentRepository.EditFile(model);
        }

        #endregion

        #region 删除附件信息

        /// <summary>
        /// 根据附件ID删除附件信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public int DeleteFile(Dictionary<string, string> paramList)
        {
            return AttachmentRepository.DeleteFile(paramList);
        }

        #endregion

        #region 根据附件组ID获取附件列表

        /// <summary>
        /// 根据附件组ID获取附件列表
        /// </summary>
        /// <returns></returns>
        public List<FileModel> GetFileList(Dictionary<string, string> paramList)
        {
            var list = new List<FileModel>();
            DataSet ds = AttachmentRepository.GetFileList(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<FileModel>(ds.Tables[0]).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据附件ID获取附件实体数据
        /// </summary>
        /// <param name="strFileId">附件ID</param>
        /// <returns></returns>
        public FileModel GetFileModel(string strFileId)
        {
            var paramList = new Dictionary<string, string>
            {
                {"FID", strFileId},
                {"GID", ""},
                {"TID", ""}
            };

            DataSet ds = AttachmentRepository.GetFileList(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                var list = new DataTableToList<FileModel>(ds.Tables[0]).ToList();
                return list.FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 根据附件组ID获取附件列表信息
        /// </summary>
        /// <remarks>创建人：李海玉   创建时间：2014-06-06</remarks>
        /// <returns></returns>
        //public JsonGridData<FileModel> GetFileJson(Dictionary<string, string> paramList)
        //{
        //    var jd = new JsonGridData<FileModel>();
        //    DataSet ds = Dal.FileDal.GetFileList(paramList);
        //    if (ds == null || ds.Tables[0].Rows.Count <= 0) return jd;
        //    var list = new DataTableToList<FileModel>(ds.Tables[0]).ToList();
        //    jd.rows = list;
        //    jd.total = list.Count;
        //    jd.flag = true;
        //    return jd;
        //}

        #endregion

        #region 根据附件参数获取附件分页列表

        /// <summary>
        /// 根据附件参数获取附件分页列表
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<FileModel> GetAttachmentsByPage(Dictionary<string, string> paramList, int pageSize, int pageIndex,
            out long total)
        {
            total = 0;
            var list = new List<FileModel>();
            DataSet ds = AttachmentRepository.GetAttachmentsByPage(paramList, pageSize, pageIndex);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<FileModel>(ds.Tables[0]).ToList();
                var model = list.FirstOrDefault();
                if (model != null) total = model.RecordCount;
            }

            return list;

            //return AttachmentRepository.GetAttachmentsByPage(paramList, pageSize, pageIndex, out total);
        }

        #endregion

    }
}
