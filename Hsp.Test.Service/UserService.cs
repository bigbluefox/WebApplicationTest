using System.Collections.Generic;
using System.Linq;
using Hsp.Test.Common;
using Hsp.Test.DataAccess;
using Hsp.Test.IDataAccess;
using Hsp.Test.IService;
using Hsp.Test.Model;

namespace Hsp.Test.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        ///     附件服务
        /// </summary>
        internal readonly IUserRepository UserRepository = new UserRepository();

        /// <summary>
        ///     添加用户
        /// </summary>
        /// <param name="model">用户数据实体</param>
        /// <returns></returns>
        public int UserAdd(User model)
        {
            return UserRepository.UserAdd(model);
        }

        /// <summary>
        ///     修改用户
        /// </summary>
        /// <param name="model">用户数据实体</param>
        /// <returns></returns>
        public int UserEdit(User model)
        {
            return UserRepository.UserEdit(model);
        }

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public int UserDel(int id)
        {
            return UserRepository.UserDel(id);
        }

        #region 获取用户列表

        /// <summary>
        ///     根据编号获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            var user = new User();
            var ds = UserRepository.GetUserById(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                user = new DataTableToList<User>(ds.Tables[0]).ToList().FirstOrDefault();
            }
            return user;
        }

        /// <summary>
        ///     获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserList(Dictionary<string, string> paramList)
        {
            var list = new List<User>();
            var ds = UserRepository.GetUserList(paramList);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new DataTableToList<User>(ds.Tables[0]).ToList();
            }
            return list;
        }

        #endregion
    }
}