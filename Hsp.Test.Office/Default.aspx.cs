using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using WPS;
namespace Hsp.Test.Office
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var name = "QHHCP 213.002-2019 节能管理标准";
            var docUrl = "/" + name + ".doc";
            var docPath = Server.MapPath(docUrl);
            var wpsUrl = "/" + name + DateTime.Now.ToString("yyyyMMddHHmmss")+ ".wps";
            var wpsPath = Server.MapPath(wpsUrl);
            //wpsPath = path.Replace(".doc", ".pdf");

            //WPS.ApplicationClass application = new WPS.ApplicationClass();
            ////application.PdfExportOptions.CopyRight = WPS.WpsPdfCopyRight.wpsPdfFreeCopy;
            ////WPS.PdfExportOptions pdfExportOptions = application.PdfExportOptions;
            ////pdfExportOptions.PrintRight = WPS.WpsPdfPrintRight.wpsPdfNotAllowPrint;
            //WPS.Document doc = application.Documents.Open(docPath);
            //doc.PrintRevisions = true;
            ////doc.SaveFormat(wpsPath, WdSaveFormat.wdFormatDocument);
            //doc.SaveAs(wpsPath);
            //doc.ExportPdf(wpsPath);
            //doc.Path(wpsPath);
            //doc.Save();

            //dynamic doc = application.Documents.Open(docPath, Visible: false); //这句大概是用wps 打开  word  不显示界面

            //doc.
            //doc.ExportAsFixedFormat(wpsPath, WdSaveFormat.wdFormatDocument); //doc  转pdf 
            //doc.Close();


            //path = "d:/2蔚县用户执行环节时间差.xlsx";
            //wpsPath = Server.MapPath("1.et");
            //object type = System.Reflection.Missing.Value;
            //ET.ApplicationClass application = new ET.ApplicationClass();
            //ET.workbook book =
            //    (ET.workbook)
            //        application.Workbooks.Open(path, type, type, type, type, type, type, type, type, type, type,
            //            type, type);
            //book.ExportPdf(wpsPath, "", ""); //this.GetFilePath(path)是获取文件路径+文件名（不含后缀）
        }
    }
}