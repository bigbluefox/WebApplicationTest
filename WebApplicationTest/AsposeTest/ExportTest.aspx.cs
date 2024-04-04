using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using Aspose.Cells;

namespace WebApplicationTest.AsposeTest
{
    public partial class ExportTest : Page
    {
        private Workbook book;
        private Worksheet sheet;

        private string fullFilename = "";
        private string outFileName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddmmHHss");
            outFileName = Server.MapPath(fileName + ".xlsx");
            book = new Workbook();
            // book.Open(tempfilename);这里我们暂时不用模板
            sheet = book.Worksheets[0];
        }

        /// <summary>
        ///     EXCEL 导出测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            var version = CellsHelper.GetVersion();

            //Workbook wb = new Workbook("文件路径");
            //WorkbookDesigner designer = new WorkbookDesigner(wb);

            //var workbook = new Workbook(); //工作簿 
            //var sheet = workbook.Worksheets[0]; //工作表 
            //var cells = sheet.Cells; //单元格 

            var strSql = "SELECT ROW_NUMBER() OVER(ORDER BY [大小] DESC) AS 序号, 文件名, 类型, 大小, 文件全名 FROM dbo.Media;";
            var strConnection = ConfigurationManager.AppSettings["ConnectionString"] ?? "";

            SqlParameter[] cmdParms = null;

            using (var connection = new SqlConnection(strConnection))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, strSql, cmdParms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();

                        var dt = ds.Tables[0];
                        var len = dt.Rows.Count;

                        var exp = DatatableToExcel("媒体数据表", dt);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    //return ds;
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


        //private string outFileName = "";
        //private string fullFilename = "";
        //private Workbook book = new Workbook();
        //private Worksheet sheet = book.Worksheets[0];


        //public AsposeExcel(string outfilename, string tempfilename)//导出构造数
        //{
        //    outFileName = outfilename;
        //    book = new Workbook();
        //    // book.Open(tempfilename);这里我们暂时不用模板
        //    sheet = book.Worksheets[0];
        //}

        //public AsposeExcel(string fullfilename)//导入构造数
        //{
        //    fullFilename = fullfilename;
        //    // book = new Workbook();
        //    //book.Open(tempfilename);
        //    //sheet = book.Worksheets[0];
        //}

        /// <summary>
        ///     添加标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="columnCount"></param>
        private void AddTitle(string title, int columnCount)
        {
            sheet.Cells.Merge(0, 0, 1, columnCount);
            sheet.Cells.Merge(1, 0, 1, columnCount);

            //为标题设置样式     
            var styleTitle = book.Styles[book.Styles.Add()]; //新增样式 
            styleTitle.Font.Color = Color.FromArgb(255, 99, 71); //字体颜色RBG颜色
            styleTitle.ForegroundColor = Color.FromArgb(250, 240, 230); //背景颜色RBG颜色
            styleTitle.HorizontalAlignment = TextAlignmentType.Center; //文字居中 
            styleTitle.Font.Name = "黑体"; //文字字体 
            styleTitle.Font.Size = 18; //文字大小 
            styleTitle.Font.IsBold = true; //粗体 

            //为标题设置样式     
            var styleSubTitle = book.Styles[book.Styles.Add()]; //新增样式 
            styleSubTitle.HorizontalAlignment = TextAlignmentType.Right; //文字居中右
            styleSubTitle.Font.Name = "宋体"; //文字字体 
            styleSubTitle.Font.Size = 14; //文字大小 

            //cell1.Style.HorizontalAlignment = TextAlignmentType.Center;
            //cell1.Style.Font.Name = "黑体";
            //cell1.Style.Font.Size = 14;
            //cell1.Style.Font.IsBold = true;

            var cell1 = sheet.Cells[0, 0];
            cell1.PutValue(title); //填写内容 
            cell1.SetStyle(styleTitle); //使用标题样式

            var cell2 = sheet.Cells[1, 0];
            cell2.PutValue("查询时间：" + DateTime.Now.ToLocalTime());
            cell2.SetStyle(styleSubTitle); //使用副标题样式
        }

        /// <summary>
        ///     添加列头
        /// </summary>
        /// <param name="dt"></param>
        private void AddHeader(DataTable dt)
        {
            var initRow = 2;

            //列头样式
            var styleHeader = book.Styles[book.Styles.Add()]; //新增样式 
            styleHeader.HorizontalAlignment = TextAlignmentType.Center; //文字居中 
            styleHeader.Font.Name = "宋体"; //文字字体 
            styleHeader.Font.Size = 14; //文字大小 
            styleHeader.Font.IsBold = true; //粗体 
            styleHeader.IsTextWrapped = true; //单元格内容自动换行 
            //上下左右增加细边框线
            styleHeader.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            for (var col = 0; col < dt.Columns.Count; col++)
            {
                var cell = sheet.Cells[initRow, col];
                cell.PutValue(dt.Columns[col].ColumnName);
                //cell.Style.Font.IsBold = true;
                cell.SetStyle(styleHeader); //使用列头样式
            }
        }

        /// <summary>
        ///     添加单元格
        /// </summary>
        /// <param name="dt"></param>
        private void AddBody(DataTable dt)
        {
            var initRow = 3;

            //普通单元格样式
            var styleContent = book.Styles[book.Styles.Add()]; //新增样式 
            styleContent.HorizontalAlignment = TextAlignmentType.Left; //文字靠左
            styleContent.Font.Name = "宋体"; //文字字体 
            styleContent.Font.Size = 12; //文字大小 
            //styleContent.IsTextWrapped = true;//单元格内容自动换行 
            styleContent.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            for (var r = 0; r < dt.Rows.Count; r++)
            {
                for (var c = 0; c < dt.Columns.Count; c++)
                {
                    sheet.Cells[r + initRow, c].PutValue(dt.Rows[r][c].ToString());
                    sheet.Cells[r + initRow, c].SetStyle(styleContent); //使用普通单元格样式
                }
            }
        }

        /// <summary>
        ///     导出数据到EXCEL文件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="dt">数据表</param>
        /// <returns></returns>
        public bool DatatableToExcel(string title, DataTable dt)
        {
            var yn = false;
            try
            {
                //sheet.Name = sheetName;

                AddTitle(title, dt.Columns.Count);
                AddHeader(dt);
                AddBody(dt);

                sheet.AutoFitColumns();
                sheet.AutoFitRows();

                book.Save(outFileName);
                yn = true;
                return yn;
            }
            catch (Exception e)
            {
                return yn;
                // throw e;
            }
        }

        public DataTable ExcelToDatatable() //导入
        {
            var book = new Workbook();
            //book.Open(fullFilename);
            var sheet = book.Worksheets[0];
            var cells = sheet.Cells;
            //获取excel中的数据保存到一个datatable中
            var dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
            // dt_Import.
            return dt_Import;
        }
    }
}