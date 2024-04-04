using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WPS;

public partial class Office_WPS : System.Web.UI.Page
{
    //Application wps = new Application();
    WPS.ApplicationClass application = new WPS.ApplicationClass();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void Dispose()
    {
        if (application != null)
        {
            application.Terminate();
        }
    }

    // WORD文档转WPS
    protected void Button1_Click(object sender, EventArgs e)
    {


        var url = "/Office/File/Word_Test.docx";
        var path = Server.MapPath(url);
        var wpsPath = path.Replace(".docx", ".wps");

        Type type;
        //type = Type.GetTypeFromProgID("ET.Application");//V8版本类型
        //if (type == null)//没有安装V8版本
        //{
        //    type = Type.GetTypeFromProgID("Ket.Application");//V9版本类型
        //    if (type == null)//没有安装V9版本
        //    {
        //        type = Type.GetTypeFromProgID("EXCEL.Application");//MS EXCEL类型
        //        if (type == null)
        //        {
        //            return;//没有安装Office软件
        //        }
        //    }
        //}
        //dynamic app = Activator.CreateInstance(type);//根据类型创建App实例
        //app.Visible = false;//后台打开，不显示Excel界面      
        //dynamic workbook = app.Workbooks.Open(@"D:\aaa.xls");//打开aaa.xls文件
        //dynamic worksheet = workbook.Worksheets["Sheet1"];//获取Sheet1工作薄
        //string A1 = worksheet.Range["A1"].Text.ToString();//读取Sheet1工作薄的A1单元格内容
        //worksheet.Range["A1"].Value = @"bbbb";//修改Sheet1工作薄A1单元格内容为bbbb
        //workbook.Save();//保存修改的内容
        //System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);//释放worksheet
        //workbook.Close();//关闭workbook      System.Runtime.InteropServices.Marshal.ReleaseComObject(etMainbook);//释放workbook 
        //app.Quit();//退出Excel实例
        //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);//释放app

        type = Type.GetTypeFromProgID("WPS.Application");//V8版本类型
        if (type == null)//没有安装V8版本
        {
            type = Type.GetTypeFromProgID("KWps.Application");//V9版本类型
            if (type == null)//没有安装V9版本
            {
                type = Type.GetTypeFromProgID("WORD.Application");//MS EXCEL类型
                if (type == null)
                {
                    return;//没有安装Office软件
                }
            }
        }

        dynamic wpsApp = Activator.CreateInstance(type);//根据类型创建App实例
        wpsApp.Visible = false;//后台打开，不显示界面  

        // 启动WPS

        //var wps = new WPS.ApplicationClass();


        //dynamic doc = wpsApp.Documents.Open(path, Visible: false); //这句大概是用wps 打开  word  不显示界面
        //doc.ExportAsFixedFormat(wpsPath, WdExportFormat.wdExportFormatPDF); //doc  转pdf 
        //doc.Close();


        WPS.ApplicationClass application = new WPS.ApplicationClass();
        //application.PdfExportOptions.CopyRight = WPS.WpsPdfCopyRight.wpsPdfFreeCopy;
        //WPS.PdfExportOptions pdfExportOptions = application.PdfExportOptions;
        //pdfExportOptions.PrintRight = WPS.WpsPdfPrintRight.wpsPdfNotAllowPrint;
        WPS.Document doc = application.Documents.Open(path);
        doc.PrintRevisions = true;
        doc.ExportPdf(wpsPath);

    }
}