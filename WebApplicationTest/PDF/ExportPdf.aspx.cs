using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Saving;

namespace WebApplicationTest.PDF
{
    public partial class ExportPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 生成PDF文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            var doc = @"D:\QSDYY 201.001-2018 上海上电电力运营有限公司规划管理标准.doc";
            var pdf = @"D:\QSDYY 201.001-2018 上海上电电力运营有限公司规划管理标准.pdf";

            ConvertDocToPDF(doc, pdf);

        }


        public void ConvertDocToPDF(string docPath, string savePdfPath)
        {
            Aspose.Words.Document wordDocument = new Aspose.Words.Document(docPath);
            InsertWatermarkText(wordDocument, "内部资料 注意保密\r\n XX公司  ");
            wordDocument.Save("D:\\abc.doc");
            Aspose.Words.Saving.PdfSaveOptions saveOption = new Aspose.Words.Saving.PdfSaveOptions();
            saveOption.SaveFormat = Aspose.Words.SaveFormat.Pdf;
            //user pass 设置了打开时，需要密码   
            //owner pass 控件编辑等权限  
            PdfEncryptionDetails encryptionDetails = new PdfEncryptionDetails(string.Empty, "PasswordHere", PdfEncryptionAlgorithm.RC4_128);
            encryptionDetails.Permissions = PdfPermissions.DisallowAll;


            saveOption.EncryptionDetails = encryptionDetails;
            wordDocument.Save(savePdfPath, saveOption);
        }

        private static void InsertWatermarkText(Aspose.Words.Document doc, string watermarkText)
        {
            // Create a watermark shape. This will be a WordArt shape.   
            // You are free to try other shape types as watermarks.  
            Aspose.Words.Drawing.Shape watermark = new Aspose.Words.Drawing.Shape(doc, Aspose.Words.Drawing.ShapeType.TextPlainText);

            // Set up the text of the watermark.  
            watermark.TextPath.Text = watermarkText;
            watermark.TextPath.FontFamily = "宋体";
            watermark.Width = 500;
            watermark.Height = 100;
            // Text will be directed from the bottom-left to the top-right corner.  
            watermark.Rotation = -40;
            // Remove the following two lines if you need a solid black text.  
            watermark.Fill.Color = System.Drawing.Color.Gray; // Try LightGray to get more Word-style watermark  
            watermark.StrokeColor = System.Drawing.Color.Gray; // Try LightGray to get more Word-style watermark  

            // Place the watermark in the page center.  
            watermark.RelativeHorizontalPosition = RelativeHorizontalPosition.Page;
            watermark.RelativeVerticalPosition = RelativeVerticalPosition.Page;
            watermark.WrapType = WrapType.None;
            watermark.VerticalAlignment = Aspose.Words.Drawing.VerticalAlignment.Center;
            watermark.HorizontalAlignment = Aspose.Words.Drawing.HorizontalAlignment.Center;

            // Create a new paragraph and append the watermark to this paragraph.  
            Aspose.Words.Paragraph watermarkPara = new Aspose.Words.Paragraph(doc);
            watermarkPara.AppendChild(watermark);
            foreach (Section sect in doc.Sections)
            {
                // There could be up to three different headers in each section, since we want  
                // the watermark to appear on all pages, insert into all headers.  
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderPrimary);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderFirst);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderEven);
            }


        }

        private static void InsertWatermarkIntoHeader(Aspose.Words.Paragraph watermarkPara, Section sect, HeaderFooterType headerType)
        {
            Aspose.Words.HeaderFooter header = sect.HeadersFooters[headerType];

            if (header == null)
            {
                // There is no header of the specified type in the current section, create it.  
                header = new Aspose.Words.HeaderFooter(sect.Document, headerType);
                sect.HeadersFooters.Add(header);
            }

            // Insert a clone of the watermark into the header.  
            header.AppendChild(watermarkPara.Clone(true));
        }  



    }
}