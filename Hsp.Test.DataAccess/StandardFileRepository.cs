using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Test.DBUtility;
using Hsp.Test.IDataAccess;

namespace Hsp.Test.DataAccess
{
    public class StandardFileRepository : IStandardFileRepository
    {

        /// <summary>
        /// 根据标准编号前缀获取对应修正代码
        /// </summary>
        /// <param name="code">标准编号前缀</param>
        /// <returns></returns>
        public string PreCodeCorresponding(string code)
        {
            string strSql = string.Format
                (@"SELECT [Corresponding] FROM [dbo].[Standard_PreCodeCorresponding] WHERE ([PreCode] = '{0}');", code);
            DataSet ds = DbHelperSql.Query(strSql);
            return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : "";
        }


    }
}
