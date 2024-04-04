using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace Hsp.Test.DBUtility
{
    /// <summary>
    ///     数据访问抽象基础类
    /// </summary>
    public abstract class DbHelperSql
    {
        //数据库连接字符串(web.config来配置)
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"] ?? "";

        //定义日志
        public static ILog Logger;

        protected DbHelperSql()
        {
            //Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            Logger = LogManager.GetLogger("Hsp.Test");

            //private static readonly ILog logger = LogManager.GetLogger(typeof(DataHelper));
        }

        #region 公用方法

        public static int GetMaxId(string fieldName, string tableName)
        {
            var strsql = "select max(" + fieldName + ") + 1 from " + tableName;
            var obj = GetSingle(strsql);
            return obj == null ? 1 : int.Parse(obj.ToString());
        }

        public static bool Exists(string strSql)
        {
            var obj = GetSingle(strSql);
            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            return cmdresult != 0;
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            var obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            return cmdresult != 0;
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        ///     执行SQL语句，设置命令的执行等待时间
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="iTimes"></param>
        /// <returns></returns>
        public static int ExecuteSqlByTime(string sqlString, int iTimes)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = iTimes;
                        var rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        //Logger.Error(e.Message);
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(ArrayList sqlStringList)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                var tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (var n = 0; n < sqlStringList.Count; n++)
                    {
                        var strsql = sqlStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (SqlException e)
                {
                    tx.Rollback();
                    //Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
            }
        }

        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, string content)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(sqlString, connection);
                var myParameter = new SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    var rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string sqlString, string content)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(sqlString, connection);
                var myParameter = new SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    var obj = cmd.ExecuteScalar();
                    if (Equals(obj, null) || Equals(obj, DBNull.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException e)
                {
                    //Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        ///     向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSql, byte[] fs)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(strSql, connection);
                var myParameter = new SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    var rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    //Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sqlString)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var obj = cmd.ExecuteScalar();
                        if (Equals(obj, null) || Equals(obj, DBNull.Value))
                        {
                            return null;
                        }
                        return obj;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        //Logger.Error(e.Message);
                        throw new Exception(e.Message);
                    }
                }
            }
        }


        /// <summary>
        ///     执行查询语句，返回SqlDataReader(使用该方法切记要手工关闭SqlDataReader和连接)
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql)
        {
            var connection = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(strSql, connection);
            try
            {
                connection.Open();
                var myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            //finally //不能在此关闭，否则，返回的对象将无法使用
            //{
            //	cmd.Dispose();
            //	connection.Close();
            //}	
        }

        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var ds = new DataSet();
                try
                {
                    connection.Open();
                    var command = new SqlDataAdapter(sqlString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException e)
                {
                    //Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
                return ds;
            }
        }

        /// <summary>
        ///     执行查询语句，返回DataSet, 设置命令的执行等待时间
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="iTimes"></param>
        /// <returns></returns>
        public static DataSet Query(string sqlString, int iTimes)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var ds = new DataSet();
                try
                {
                    connection.Open();
                    var command = new SqlDataAdapter(sqlString, connection);
                    command.SelectCommand.CommandTimeout = iTimes;
                    command.Fill(ds, "ds");
                }
                catch (SqlException e)
                {
                    //Logger.Error(e.Message);
                    throw new Exception(e.Message);
                }
                return ds;
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                        var rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        //Logger.Error(E.Message);
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable sqlStringList)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    var cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDe in sqlStringList)
                        {
                            var cmdText = myDe.Key.ToString();
                            var cmdParms = (SqlParameter[]) myDe.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            var val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch (SqlException e)
                    {
                        trans.Rollback();
                        //Logger.Error(e.Message);
                        throw new Exception(e.Message);
                    }
                }
            }
        }


        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sqlString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                        var obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if (Equals(obj, null) || Equals(obj, DBNull.Value))
                        {
                            return null;
                        }
                        return obj;
                    }
                    catch (SqlException e)
                    {
                        //Logger.Error(e.Message);
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        ///     执行查询语句，返回SqlDataReader (使用该方法切记要手工关闭SqlDataReader和连接)
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sqlString, params SqlParameter[] cmdParms)
        {
            var connection = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                var myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SqlException e)
            {
                //Logger.Error(e.Message);
                throw new Exception(e.Message);
            }
            //finally //不能在此关闭，否则，返回的对象将无法使用
            //{
            //	cmd.Dispose();
            //	connection.Close();
            //}	
        }

        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException e)
                    {
                        //Logger.Error(e.Message);
                        throw new Exception(e.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText,
            SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text; //cmdType;
            if (cmdParms != null)
            {
                foreach (var parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        ///     执行存储过程  (使用该方法切记要手工关闭SqlDataReader和连接)
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            var returnReader = command.ExecuteReader();
            //Connection.Close(); 不能在此关闭，否则，返回的对象将无法使用            
            return returnReader;
        }


        /// <summary>
        ///     执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var dataSet = new DataSet();
                connection.Open();
                var sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName,
            int iTimes)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var dataSet = new DataSet();
                connection.Open();
                var sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = iTimes;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        ///     执行分页存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="iCurrentPage"></param>
        /// <param name="iPageSize"></param>
        /// <returns>DataSet</returns>
        public static DataSet GetPageRecordBySql(string strSql, int iCurrentPage, int iPageSize)
        {
            const string strStoreProcedure = "dbo.sp_GetPageData";
            var sqlParameter = new SqlParameter[3];
            sqlParameter[0] = new SqlParameter("@SQLSTR", strSql);
            sqlParameter[1] = new SqlParameter("@CURPAEG", iCurrentPage);
            sqlParameter[2] = new SqlParameter("@PAGESIZE", iPageSize);
            return RunProcedure(strStoreProcedure, sqlParameter, "PageRecords");
        }


        /// <summary>
        ///     构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName,
            IDataParameter[] parameters)
        {
            var command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        ///     执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                var result = (int) command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        ///     创建 SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName,
            IDataParameter[] parameters)
        {
            var command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #endregion
    }
}