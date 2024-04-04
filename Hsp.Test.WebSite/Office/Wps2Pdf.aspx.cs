using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WPS;

public partial class Office_Wps2Pdf : System.Web.UI.Page, IDisposable
{
    Application wps = new Application();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Type type;
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

        //type = Type.GetTypeFromProgID("WPS.Application");//V8版本类型
        //if (type == null)//没有安装V8版本
        //{
        //    type = Type.GetTypeFromProgID("KWps.Application");//V9版本类型
        //    if (type == null)//没有安装V9版本
        //    {
        //        type = Type.GetTypeFromProgID("WORD.Application");//MS EXCEL类型
        //        if (type == null)
        //        {
        //            return;//没有安装Office软件
        //        }
        //    }
        //}

        var wpsFilename = @"D:\（原合同）江西博微软件购销服务合同.wps";
        var pdfFilename = "";

        if (wpsFilename == null) { throw new ArgumentNullException("wpsFilename"); }

        if (string.IsNullOrEmpty(pdfFilename))
        {
            pdfFilename = Path.ChangeExtension(wpsFilename, "pdf");
        }

        //dynamic doc = wps.Documents.Open(wpsFilename, Visible: false); //这句大概是用wps 打开  word  不显示界面
        //doc.ExportAsFixedFormat(pdfFilename, WdExportFormat.wdExportFormatPDF); //doc  转pdf 
        //doc.Close();

        //Console.WriteLine(string.Format(@"正在转换 [{0}]
        //-> [{1}]", wpsFilename, pdfFilename));

        //WPS.PdfExportOptions pdfExportOptions = wps.PdfExportOptions;

        //pdfExportOptions.PrintRight = WPS.WpsPdfPrintRight.wpsPdfNotAllowPrint;

        //Document doc = wps.Documents.Open(wpsFilename, Visible: false);
        //doc.ExportPdf(pdfFilename);
        //doc.Close();

        //WPS.ApplicationClass oWordApp = new WPS.ApplicationClass();//建立Word   对象，启动word程序 

    }

    public override void Dispose()
    {
        if (wps != null)
        {
            wps.Terminate();
        }
    }
}



