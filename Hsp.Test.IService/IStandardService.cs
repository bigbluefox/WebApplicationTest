﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model;

namespace Hsp.Test.IService
{
    /// <summary>
    /// 本地标准服务接口
    /// </summary>
    public interface IStandardService
    {
        /// <summary>
        /// 本地标准添加
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        int AddStandard(StandardLocal model);

        /// <summary>
        /// 本地标准更新
        /// </summary>
        /// <param name="model">本地标准实体</param>
        /// <returns></returns>
        int UpdateStandard(StandardLocal model);

        /// <summary>
        /// 本地标准批量添加
        /// </summary>
        /// <param name="list">本地标准列表</param>
        /// <returns></returns>
        int AddStandards(List<StandardLocal> list);

        /// <summary>
        /// 获取标准数据列表
        /// </summary>
        /// <returns></returns>
        List<StandardLocal> GetStandardList();
    }
}
