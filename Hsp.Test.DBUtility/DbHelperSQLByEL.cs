using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Hsp.Test.DBUtility
{
    //Enterprise Library 2.0 数据访问进一步封装类
    public abstract class DbHelperSQLByEL
    {
        #region 公用方法

        /// <summary>
        ///     获取表某个字段的最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            var strSql = "select max(" + FieldName + ")+1 from " + TableName;
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            var obj = db.ExecuteScalar(dbCommand);
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                return 1;
            }
            return int.Parse(obj.ToString());
        }

        /// <summary>
        ///     检测一个记录是否存在(SQL语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            var obj = db.ExecuteScalar(dbCommand);
            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     检测一个记录是否存在(SqlParameter语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            var obj = db.ExecuteScalar(dbCommand);
            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            if (cmdresult == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        ///     加载参数
        /// </summary>
        public static void BuildDBParameter(Database db, DbCommand dbCommand, params SqlParameter[] cmdParms)
        {
            foreach (var sp in cmdParms)
            {
                db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
            }
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        ///     执行SQL语句，返回影响的记录数(对于长时间查询的语句，设置等待时间避免查询超时)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static int ExecuteSqlByTime(string strSql, int Times)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            dbCommand.CommandTimeout = Times;
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            var db = DatabaseFactory.CreateDatabase();
            using (var dbconn = db.CreateConnection())
            {
                dbconn.Open();
                var dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    for (var n = 0; n < SQLStringList.Count; n++)
                    {
                        var strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            var dbCommand = db.GetSqlStringCommand(strsql);
                            db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }

        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string strSql)
        {
            var db = DatabaseFactory.CreateDatabase();

            var dbCommand = db.GetSqlStringCommand(strSql);

            var obj = db.ExecuteScalar(dbCommand);

            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                return null;
            }
            return obj;
        }


        /// <summary>
        ///     执行查询语句，返回SqlDataReader ( 注意：使用后一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            var dr = (SqlDataReader) db.ExecuteReader(dbCommand);
            return dr;
        }


        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string strSql)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteDataSet(dbCommand);
        }

        //(对于长时间查询的语句，设置等待时间避免查询超时)

        public static DataSet Query(string strSql, int Times)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            dbCommand.CommandTimeout = Times;
            return db.ExecuteDataSet(dbCommand);
        }

        #region 执行一个特殊字段带参数的语句

        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql, string content)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@content", DbType.String, content);
            return db.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>返回语句里的查询结果</returns>
        public static object ExecuteSqlGet(string strSql, string content)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@content", DbType.String, content);
            object obj = db.ExecuteNonQuery(dbCommand);
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                return null;
            }
            return obj;
        }

        /// <summary>
        ///     向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSql, byte[] fs)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@fs", DbType.Byte, fs);
            return db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql, params SqlParameter[] cmdParms)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            var db = DatabaseFactory.CreateDatabase();
            using (var dbconn = db.CreateConnection())
            {
                dbconn.Open();
                var dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        var strsql = myDE.Key.ToString();
                        var cmdParms = (SqlParameter[]) myDE.Value;
                        if (strsql.Trim().Length > 1)
                        {
                            var dbCommand = db.GetSqlStringCommand(strsql);
                            BuildDBParameter(db, dbCommand, cmdParms);
                            db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }

        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string strSql, params SqlParameter[] cmdParms)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);

            var obj = db.ExecuteScalar(dbCommand);
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                return null;
            }
            return obj;
        }
        
        /// <summary>
        ///     执行查询语句，返回SqlDataReader ( 注意：使用后一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql, params SqlParameter[] cmdParms)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            var dr = (SqlDataReader) db.ExecuteReader(dbCommand);
            return dr;
        }

        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string strSql, params SqlParameter[] cmdParms)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteDataSet(dbCommand);
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
        ///     执行存储过程，返回影响的行数
        /// </summary>
        public static int RunProcedure(string storedProcName)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        ///     执行存储过程，返回输出参数的值和影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="inParameters">存储过程参数</param>
        /// <param name="outParameter">输出参数名称</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static object RunProcedure(string storedProcName, IDataParameter[] inParameters,
            SqlParameter outParameter, out int rowsAffected)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, (SqlParameter[]) inParameters);
            db.AddOutParameter(dbCommand, outParameter.ParameterName, outParameter.DbType, outParameter.Size);
            rowsAffected = db.ExecuteNonQuery(dbCommand);
            return db.GetParameterValue(dbCommand, "@" + outParameter.ParameterName); //得到输出参数的值
        }

        /// <summary>
        ///     执行存储过程，返回SqlDataReader ( 注意：使用后一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            //BuildDBParameter(db, dbCommand, parameters);
            return (SqlDataReader) db.ExecuteReader(dbCommand);
        }

        /// <summary>
        ///     执行存储过程，返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            //BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand);
        }

        /// <summary>
        ///     执行存储过程，返回DataSet(设定等待时间)
        /// </summary>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName,
            int Times)
        {
            var db = DatabaseFactory.CreateDatabase();
            var dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            dbCommand.CommandTimeout = Times;
            //BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand);
        }


        /// <summary>
        ///     构建SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
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
        ///     创建SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
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