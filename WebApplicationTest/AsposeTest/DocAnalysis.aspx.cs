using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;

namespace WebApplicationTest.AsposeTest
{
    public partial class DocAnalysis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Word 文档分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnalysis_Click(object sender, EventArgs e)
        {
            #region 检查文档发布根目录

            // 文档发布根目录
            string docFileRootPath = ConfigurationManager.AppSettings["DocFileRootPath"];

            // 检查文档发布根目录  
            if (string.IsNullOrEmpty(docFileRootPath))
            {
                docFileRootPath = "/Files/DocFile/";
            }

            if (!(docFileRootPath.EndsWith("/"))) docFileRootPath += "/";

            string docFileRootDirectory = HttpContext.Current.Server.MapPath(docFileRootPath);
            if (!Directory.Exists(docFileRootDirectory))
            {
                Directory.CreateDirectory(docFileRootDirectory);
            }

            #endregion

            const string strFileExt = ".docx";

            var docFile = @"D:\MSO\Doc\培训签到单.doc";
            docFile = @"D:\QCJDC 202.001-2016-上海上电漕泾发电有限公司物资需求计划管理标准.doc";
            Document doc = new Document(docFile);
            //DocumentBuilder builder = new DocumentBuilder(doc);


            // Loop through all sections in the source document.
            foreach (Section section in doc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node node in section.Body)
                {

                    //if (node.NodeType != NodeType.Table) continue;

                    var type = node.NodeType;

                    var t = node.ParentNode;



                }
            }


            Document desDoc = new Document();
            DocumentBuilder builder = new DocumentBuilder(desDoc);


            Paragraph curParagraph = builder.CurrentParagraph;


            Node insertAfterNode = curParagraph;

            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = curParagraph.ParentNode;

            // This object will be translating styles and lists during the import.
            var importer = new NodeImporter(doc, curParagraph.Document, ImportFormatMode.KeepSourceFormatting);


            // Loop through all sections in the source document.
            foreach (Section srcSection in doc.Sections)
            {

                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node srcNode in srcSection.Body)
                {
                    var node = srcNode;
                    // Let's skip the node if it is a last empty paragarph in a section.
                    if (node.NodeType.Equals(NodeType.Paragraph))
                    {
                        var para = (Paragraph)node;
                        var identifier = para.ParagraphFormat.StyleIdentifier;

                        if(identifier == StyleIdentifier.User) continue;

                        var fontSize = para.ParagraphFormat.Style.Font.Size;
                        para.ParagraphFormat.StyleIdentifier = para.ParagraphFormat.StyleIdentifier;



                        //var d = StyleIdentifier.User.ToString(); 


                        var isHeading = identifier.ToString().ToLower().IndexOf("heading", StringComparison.Ordinal) > -1;
                        if (para.IsEndOfSection && !para.HasChildNodes && isHeading) continue;



                        // Aspose.Words.StyleIdentifier.User
                        //var txt = para.GetText();
                        //var runsCount = para.Runs.Count;
                        //var isHeading = para.ParagraphFormat.IsHeading;

                        para.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;

                        //switch (identifier)
                        //{
                        //    case StyleIdentifier.Heading1:
                        //        fontSize = 22f;
                        //        break;

                        //    case StyleIdentifier.Heading2:
                        //        fontSize = 16f;
                        //        break;

                        //    case StyleIdentifier.Heading3:
                        //        fontSize = 16f;
                        //        break;

                        //    case StyleIdentifier.Heading4:
                        //        fontSize = 14f;
                        //        break;

                        //    case StyleIdentifier.Heading5:
                        //        fontSize = 14f;
                        //        break;

                        //    default:
                        //        fontSize = 14f;
                        //        break;
                        //}

                        para.ParagraphFormat.SpaceBefore = fontSize;
                        para.ParagraphFormat.SpaceAfter = fontSize;
                        para.ParagraphFormat.Style.Font.Size = fontSize;

                        //para.ParagraphFormat.Style.Font.

                        node = (Node)para;
                    }

                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Node newNode = importer.ImportNode(node, true);

                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }


            docFile = @"D:\QCJDC 202.001-2016-上海上电漕泾发电有限公司物资需求计划管理标准.doc";




            desDoc.Save(docFileRootDirectory + "Out" + DateTime.Now.ToString("yyyyMMdd") + strFileExt, SaveFormat.Docx);




        }
    }
}