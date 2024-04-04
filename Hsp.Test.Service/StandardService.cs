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
    /// 本地标准服务
    /// </summary>
    public class StandardService : IStandardService
    {
        /// <summary>
        /// 本地标准服务
        /// </summary>
        internal readonly IStandardRepository StandardRepository = new StandardRepository();

        #region 本地标准添加

        /// <summary>
        /// 本地标准添加
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        public int AddStandard(StandardLocal model)
        {
            return StandardRepository.AddStandard(model);
        }

        /// <summary>
        /// 本地标准更新
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        public int UpdateStandard(StandardLocal model)
        {
            return StandardRepository.UpdateStandard(model);
        }

        #endregion

        #region 本地标准批量添加

        /// <summary>
        /// 本地标准批量添加
        /// </summary>
        /// <param name="list">本地标准列表</param>
        /// <returns></returns>
        public int AddStandards(List<StandardLocal> list)
        {
            return StandardRepository.AddStandards(list);
        }

        #endregion

        #region 获取标准数据列表

        /// <summary>
        /// 获取标准数据列表
        /// </summary>
        /// <returns></returns>
        public List<StandardLocal> GetStandardList()
        {
            var list = new List<StandardLocal>();
            DataSet ds = StandardRepository.GetStandardData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<StandardLocal>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion



    }
}
