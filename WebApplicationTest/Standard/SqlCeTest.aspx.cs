using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Standard
{
    public partial class SqlCeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SQL Server Compact 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSqlCeTest_Click(object sender, EventArgs e)
        {
            try
            {
                //System.Data.SqlServerCe.SqlCeConnection conn =
                //    new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=|DataDirectory|\DataSetDb.sdf;Persist Security Info=False;Password=;");
                //conn.Open();

                var CONN_STRING = ConfigurationManager.AppSettings["SqlCe"] ?? "";

                using (SqlCeConnection conn = new SqlCeConnection(CONN_STRING))
                {
                    conn.Open();
                    //using (SqlCeCommand comm = new SqlCeCommand("create table test(col1 INT,col2 NVARCHAR(100))", conn))
                    //{
                    //    comm.ExecuteNonQuery();
                    //}

                    using (SqlCeCommand comm = new SqlCeCommand("insert into PreCode_Corresponding values('GBT','GB/T')", conn))
                    {
                        comm.ExecuteNonQuery();
                    }
                }

                using (SqlCeConnection conn = new SqlCeConnection(CONN_STRING))
                {
                    using (SqlCeCommand comm = new SqlCeCommand("select * from test", conn))
                    {
                        conn.Open();
                        using (IDataReader rdr = comm.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Console.WriteLine("{0}\t{1}", rdr["col1"], rdr["col2"]);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }

            //([a-zA-Z/]+[ ]?[0-9]+[.]?[0-9]*[-－—. ][1][0-9][0-9][0-9][ ]?)|([a-zA-Z/]+[ ]?[0-9]+[.]?[0-9]*[-－—. ][2][0][0-1][0-9][ ]?)


        }
    }
}