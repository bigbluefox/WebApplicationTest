using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Diagram;
using Aspose.Slides;

public partial class Office_PreView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceFile = @"File/Standardized_Training_for_Party_Construction_Work.pptx";
        //sourceFile = @"File/Word_Test.docx";
        sourceFile = @"File/徐州华润标准体系结构图-12.1.2.vsd";
        var targetFile = @"File/Standardized_Training_for_Party_Construction_Work1.pdf";
        PreView(sourceFile, targetFile);
    }

    /// <summary>
    /// Aspose office （Excel,Word,PPT）,PDF 在线预览
    /// </summary>
    /// <param name="sourceDoc">需要预览的文件地址</param>
    /// <param name="saveDoc">展示的html文件地址</param>
    public void PreView(string sourceDoc, string saveDoc)
    {
        string s_sourceDoc = Server.MapPath(sourceDoc);
        string s_saveDoc = Server.MapPath(saveDoc);
        string docExtendName = Path.GetExtension(s_sourceDoc).ToLower();

        try
        {
            switch (docExtendName)
            {
                case ".doc":
                case ".docx":
                    Aspose.Words.Document doc = new Aspose.Words.Document(s_sourceDoc);
                    doc.Save(s_saveDoc, Aspose.Words.SaveFormat.Html);
                    Response.Redirect(saveDoc);
                    break;
                case ".xls":
                case ".xlsx":
                    Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(s_sourceDoc);
                    workbook.Save(s_saveDoc, Aspose.Cells.SaveFormat.Html);
                    Response.Redirect(saveDoc);
                    break;
                case ".ppt":
                case ".pptx":
                    Aspose.Slides.Presentation pptx = new Aspose.Slides.Presentation(s_sourceDoc);
                    pptx.Save(s_saveDoc, Aspose.Slides.Export.SaveFormat.Html);
                    Response.Redirect(saveDoc);

                    //using (Presentation pres = new Presentation(s_sourceDoc))
                    //{
                    //    pres.Save(s_saveDoc, Aspose.Slides.Export.SaveFormat.Html);
                    //    Response.Redirect(saveDoc);
                    //}

                    break;
                case ".vsd":
                case ".vsdx":
                    Aspose.Diagram.Diagram visio = new Diagram(s_sourceDoc);
                    visio.Save(s_saveDoc, Aspose.Diagram.SaveFileFormat.PDF);
                    Response.Redirect(saveDoc);
                    break;

                case ".pdf":
                    Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(s_sourceDoc);
                    pdf.Save(s_saveDoc, Aspose.Pdf.SaveFormat.Html);
                    Response.Redirect(saveDoc);
                    break;
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }


    }
}