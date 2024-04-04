using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SQLite_DbTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 使用原生态的ADO.NET访问SQLite

        string sql = "SELECT * FROM Audio";
        //string conStr = "D:/sqlliteDb/document.db";
        //string connStr = @"Data Source=" + @"E:\Db\SQLite\northwindEF.db;Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
        //string connStr = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString ?? "";
        string connStr = ConfigurationManager.AppSettings["sqlite"] ?? "";

        //var sqlite = ConfigurationManager.ConnectionStrings["sqlite"];
        //var a = sqlite;
        //var b = sqlite.ConnectionString;

        //string config = System.Configuration.ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
        
        DataSet ds = new DataSet();
        using (SQLiteConnection conn = new SQLiteConnection(connStr))
        {
            conn.Open();
            using (SQLiteDataAdapter ap = new SQLiteDataAdapter(sql, conn))
            {

                ap.Fill(ds);

                DataTable dt = ds.Tables[0];

                Label1.Text = dt.Rows.Count.ToString();
            }
        }

        #endregion

        #region aa

        ////这个文件是预先生成的数据库文件
        //string sqliteFilePath = Server.MapPath("~/App_Data/MusicList.db"); // "Data Source=" + sqliteFilePath
        ds = new DataSet();
        //声明一个Sqlite数据库的链接
        using (SQLiteConnection conn = new SQLiteConnection(connStr))
        {
            //创建sqlite命令
            using (SQLiteCommand comm = conn.CreateCommand())
            {
                //打开数据库链接
                conn.Open();

                ////插入数据
                //comm.CommandText = "INSERT INTO [t] VALUES(10,'Hello 9')";
                //comm.ExecuteNonQuery();

                ////更新数据
                //comm.CommandText = "UPDATE [t] SET name = 'Hello 10' WHERE id = 10";
                //comm.ExecuteNonQuery();

                ////使用参数插入数据
                //comm.CommandText = "INSERT INTO [t] VALUES(@id,@name)";
                //comm.Parameters.AddRange(
                //    new SQLiteParameter[]{
                //    CreateSqliteParameter("@id",DbType.Int32,4,11),
                //    CreateSqliteParameter("@name",DbType.String,10,"Hello 11")
                //    });
                //comm.ExecuteNonQuery();

                //comm.Parameters.Clear();


                //select数据分页用limit就行，很方便
                comm.CommandText = "Select * From Audio";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(comm))
                {
                    adapter.Fill(ds);

                    Label1.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
        }

        //gv1.DataSource = ds;
        //gv1.DataBind();

        //var d = ds;

        #endregion
    }

    /// <summary>
    /// 放回一个SQLiteParameter
    /// </summary>
    /// <param name="name">参数名字</param>
    /// <param name="type">参数类型</param>
    /// <param name="size">参数大小</param>
    /// <param name="value">参数值</param>
    /// <returns>SQLiteParameter的值</returns>
    private static SQLiteParameter CreateSqliteParameter(string name, DbType type, int size, object value)
    {
        SQLiteParameter parm = new SQLiteParameter(name, type, size);
        parm.Value = value;
        return parm;
    }
}