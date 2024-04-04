using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.Model;

namespace Hsp.Test.IDataAccess
{
    public interface IUserRepository
    {

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model">用户数据实体</param>
        /// <returns></returns>
        int UserAdd(User model);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model">用户数据实体</param>
        /// <returns></returns>
        int UserEdit(User model);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        int UserDel(int id);

        /// <summary>
        /// 根据编号获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        DataSet GetUserById(int id);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        DataSet GetUserList(Dictionary<string, string> paramList);
    }
}
