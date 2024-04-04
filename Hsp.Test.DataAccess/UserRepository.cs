using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;
using Hsp.Test.Model;

namespace Hsp.Test.DataAccess
{
   public class UserRepository : IUserRepository
    {
       /// <summary>
       /// 添加用户
       /// </summary>
        /// <param name="model">用户数据实体</param>
       /// <returns></returns>
       public int UserAdd(User model)
       {
           string strSql = string.Format
               (@"INSERT INTO UserInfo (firstname, lastname, phone, email) VALUES ('{0}', '{1}', '{2}', '{3}');"
                , model.firstname, model.lastname, model.phone, model.email);
           return SQLiteHelper.ExecuteNonQuery(strSql);
       }

       /// <summary>
       /// 修改用户
       /// </summary>
       /// <param name="model">用户数据实体</param>
       /// <returns></returns>
       public int UserEdit(User model)
       {
           string strSql = string.Format
               (@"UPDATE UserInfo SET firstname = '{0}', lastname = '{1}', phone = '{2}', email = '{3}' WHERE (id = {4});"
                , model.firstname, model.lastname, model.phone, model.email, model.id);

           return SQLiteHelper.ExecuteNonQuery(strSql);
       }

       /// <summary>
       /// 删除用户
       /// </summary>
       /// <param name="id">用户编号</param>
       /// <returns></returns>
       public int UserDel(int id)
       {
           string strSql = string.Format(@"DELETE FROM UserInfo WHERE (id = {0});", id);
           return SQLiteHelper.ExecuteNonQuery(strSql);
       }


       #region 获取用户列表

       /// <summary>
       /// 根据编号获取用户信息
       /// </summary>
       /// <param name="id">用户编号</param>
       /// <returns></returns>
       public DataSet GetUserById(int id)
       {
           string strSql = string.Format
               (@" SELECT id, firstname, lastname, phone, email FROM UserInfo WHERE (1 = 1) AND (id = {0});", id);
           return SQLiteHelper.ExecuteDataSet(strSql);
       }

       /// <summary>
       /// 获取用户列表
       /// </summary>
       /// <returns></returns>
       public DataSet GetUserList(Dictionary<string, string> paramList)
       {
           #region 页码处理

           int pageSize = int.Parse(paramList["pageSize"] ?? "10");
           int pageIndex = int.Parse(paramList["pageIndex"] ?? "1");

           //var iMinPage = (pageIndex - 1)*pageSize + 1;
           //var iMaxPage = pageIndex*pageSize;

           // offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果

           var iOffset = (pageIndex - 1) * pageSize;
           var iLimit = pageSize;

           #endregion

           var strQry = "";
           string strSql = string.Format(@" SELECT id, firstname, lastname, phone, email, c.count  
                FROM UserInfo u, (SELECT count(id) as count FROM UserInfo) c 
                WHERE (1 = 1) {0} order by id limit {1} offset {2};"
               , strQry, iLimit, iOffset);
           return SQLiteHelper.ExecuteDataSet(strSql);
       }

       #endregion

    }
}
