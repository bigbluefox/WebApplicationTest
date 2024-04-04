using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using Aspose.Words.Lists;

namespace WebApplicationTest.AsposeTest
{
    public partial class ListTest1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 文档列表测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTest_Click(object sender, EventArgs e)
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

            try
            {
                var temp = @"\AsposeTest\Template\WorkStandardsTemplate.dot";
                var tempPath = HttpContext.Current.Server.MapPath(temp);

                const string strFileExt = ".docx";
                var strName = DateTime.Now.ToString("yyyyMMddhhmmss");
                var docFile = docFileRootDirectory + strName + strFileExt;

                Document doc = new Document(tempPath);
                DocumentBuilder builder = new DocumentBuilder(doc);

                // 将焦点移动到文档末尾
                builder.MoveToDocumentEnd();

                // ListLevel.NumberFormat Property

                //[C#]
                //public string NumberFormat {get; set;}

                //Remarks
                //Among normal text characters, the string can contain placeholder characters \x0000 to \x0008 
                //representing the numbers from the corresponding list levels.

                //For example, the string "\x0000.\x0001)" will generate a list label that looks something 
                //like "1.5)". The number "1" is the current number from the 1st list level, the number "5" 
                //is the current number from the 2nd list level.

                //Null is not allowed, but an empty string meaning no number is valid.
                
                // Create a list based on one of the Microsoft Word list templates.
                Aspose.Words.Lists.List list = doc.Lists.Add(ListTemplate.NumberDefault);

                // Completely customize one list level.
                ListLevel level1 = list.ListLevels[0];
                level1.Font.Color = Color.Red;
                level1.Font.Size = 24;
                level1.NumberStyle = NumberStyle.OrdinalText;
                level1.StartAt = 21;
                level1.NumberFormat = "\x0000";

                level1.NumberPosition = -36;
                level1.TextPosition = 36;
                level1.TabPosition = 36;

                // Completely customize yet another list level.
                ListLevel level2 = list.ListLevels[1];
                level2.Alignment = ListLevelAlignment.Right;
                level2.NumberStyle = NumberStyle.Bullet;
                level2.Font.Name = "Wingdings";
                level2.Font.Color = Color.Blue;
                level2.Font.Size = 24;
                level2.NumberFormat = "\xf0af";    // A bullet that looks like some sort of a star.
                level2.TrailingCharacter = ListTrailingCharacter.Space;
                level2.NumberPosition = 72;

                // Now add some text that uses the list that we created.            
                // It does not matter when to customize the list - before or after adding the paragraphs.
                //DocumentBuilder builder = new DocumentBuilder(doc);

                builder.ListFormat.List = list;
                builder.Writeln("The quick brown fox...");
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.ListIndent();
                builder.Writeln("jumped over the lazy dog.");
                builder.Writeln("jumped over the lazy dog.");

                builder.ListFormat.ListOutdent();
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.RemoveNumbers();

                builder.InsertBreak(BreakType.ParagraphBreak);

                //Aspose.Words ListLevel.NumberPosition Property
                //NumberPosition corresponds to LeftIndent plus FirstLineIndent of the paragraph.



                // Create a list based on one of the Microsoft Word list templates.
               list = doc.Lists.Add(ListTemplate.NumberDefault);

                // Completely customize one list level.
                level1 = list.ListLevels[0];
                level1.Font.Color = Color.Red;
                level1.Font.Size = 24;
                level1.NumberStyle = NumberStyle.OrdinalText;
                level1.StartAt = 21;
                level1.NumberFormat = "\x0000";

                level1.NumberPosition = -36;
                level1.TextPosition = 36;
                level1.TabPosition = 36;

                // Completely customize yet another list level.
                level2 = list.ListLevels[1];
                level2.Alignment = ListLevelAlignment.Right;
                level2.NumberStyle = NumberStyle.Bullet;
                level2.Font.Name = "Wingdings";
                level2.Font.Color = Color.Blue;
                level2.Font.Size = 24;
                level2.NumberFormat = "\xf0af";    // A bullet that looks like some sort of a star.
                level2.TrailingCharacter = ListTrailingCharacter.Space;
                level2.NumberPosition = 72;

                // Now add some text that uses the list that we created.            
                // It does not matter when to customize the list - before or after adding the paragraphs.
                //DocumentBuilder builder = new DocumentBuilder(doc);

                builder.ListFormat.List = list;
                builder.Writeln("The quick brown fox...");
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.ListIndent();
                builder.Writeln("jumped over the lazy dog.");
                builder.Writeln("jumped over the lazy dog.");

                builder.ListFormat.ListOutdent();
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.RemoveNumbers();

                builder.InsertBreak(BreakType.ParagraphBreak);



                //ListLevel.RestartAfterLevel Property

                //Sets or returns the list level that must appear before the specified list level restarts numbering.
                //The value of -1 means the numbering will continue.



                list = doc.Lists.Add(ListTemplate.NumberDefault);

                // Level 1 labels will be "Appendix A", continuous and linked to the Heading 1 paragraph style.
                list.ListLevels[0].NumberFormat = "Appendix \x0000";
                list.ListLevels[0].NumberStyle = NumberStyle.UppercaseLetter;
                list.ListLevels[0].LinkedStyle = doc.Styles["Heading 1"];

                // Level 2 labels will be "Section (1.01)" and restarting after Level 2 item appears.
                list.ListLevels[1].NumberFormat = "Section (\x0000.\x0001)";
                list.ListLevels[1].NumberStyle = NumberStyle.LeadingZero;
                // Notice the higher level uses UppercaseLetter numbering, but we want arabic number
                // of the higher levels to appear in this level, therefore set this property.
                list.ListLevels[1].IsLegal = true;
                list.ListLevels[1].RestartAfterLevel = 0;

                // Level 3 labels will be "-I-" and restarting after Level 2 item appears.
                list.ListLevels[2].NumberFormat = "-\x0002-";
                list.ListLevels[2].NumberStyle = NumberStyle.UppercaseRoman;
                list.ListLevels[2].RestartAfterLevel = 1;

                // Make labels of all list levels bold.
                foreach (ListLevel level in list.ListLevels)
                    level.Font.Bold = true;


                // Apply list formatting to the current paragraph.
                builder.ListFormat.List = list;

                // Exercise the 3 levels we created two times.
                for (int n = 0; n < 2; n++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        builder.ListFormat.ListLevelNumber = i;
                        builder.Writeln("Level " + i);
                    }
                }

                builder.ListFormat.RemoveNumbers();

                builder.InsertBreak(BreakType.ParagraphBreak);



                //ListLevel.StartAt Property
                //Returns or sets the starting number for this list level. 
                //Default value is 1.


                // Create a list based on a template.
                Aspose.Words.Lists.List list1 = doc.Lists.Add(ListTemplate.NumberArabicParenthesis);
                // Modify the formatting of the list.
                list1.ListLevels[0].Font.Color = Color.Red;
                list1.ListLevels[0].Alignment = ListLevelAlignment.Right;

                builder.Writeln("List 1 starts below:");
                // Use the first list in the document for a while.
                builder.ListFormat.List = list1;
                builder.Writeln("Item 1");
                builder.Writeln("Item 2");
                builder.ListFormat.RemoveNumbers();

                // Now I want to reuse the first list, but need to restart numbering.
                // This should be done by creating a copy of the original list formatting.
                Aspose.Words.Lists.List list2 = doc.Lists.AddCopy(list1);

                // We can modify the new list in any way. Including setting new start number.
                list2.ListLevels[0].StartAt = 10;

                // Use the second list in the document.
                builder.Writeln("List 2 starts below:");
                builder.ListFormat.List = list2;
                builder.Writeln("Item 1");
                builder.Writeln("Item 2");
                builder.ListFormat.RemoveNumbers();

                builder.InsertBreak(BreakType.ParagraphBreak);




                //ListLevel.TabPosition Property
                //Has effect only when TrailingCharacter is a tab.

                // Create a list based on one of the Microsoft Word list templates.
                 list = doc.Lists.Add(ListTemplate.NumberDefault);

                // Completely customize one list level.
                 level1 = list.ListLevels[0];
                level1.Font.Color = Color.Red;
                level1.Font.Size = 24;
                level1.NumberStyle = NumberStyle.OrdinalText;
                level1.StartAt = 21;
                level1.NumberFormat = "\x0000";

                level1.NumberPosition = -36;
                level1.TextPosition = 144;
                level1.TabPosition = 144;

                // Completely customize yet another list level.
                level2 = list.ListLevels[1];
                level2.Alignment = ListLevelAlignment.Right;
                level2.NumberStyle = NumberStyle.Bullet;
                level2.Font.Name = "Wingdings";
                level2.Font.Color = Color.Blue;
                level2.Font.Size = 24;
                level2.NumberFormat = "\xf0af";    // A bullet that looks like some sort of a star.
                level2.TrailingCharacter = ListTrailingCharacter.Space;
                level2.NumberPosition = 144;

                // Now add some text that uses the list that we created.            
                // It does not matter when to customize the list - before or after adding the paragraphs.
                //DocumentBuilder builder = new DocumentBuilder(doc);

                builder.ListFormat.List = list;
                builder.Writeln("The quick brown fox...");
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.ListIndent();
                builder.Writeln("jumped over the lazy dog.");
                builder.Writeln("jumped over the lazy dog.");

                builder.ListFormat.ListOutdent();
                builder.Writeln("The quick brown fox...");

                builder.ListFormat.RemoveNumbers();

                builder.InsertBreak(BreakType.ParagraphBreak);



                // Create a numbered list based on one of the Microsoft Word list templates and
                // apply it to the current paragraph in the document builder.
                builder.ListFormat.List = doc.Lists.Add(ListTemplate.NumberArabicDot);

                // There are 9 levels in this list, lets try them all.
                for (int i = 0; i < 9; i++)
                {
                    builder.ListFormat.ListLevelNumber = i;
                    builder.Writeln("Level " + i);
                }


                // Create a bulleted list based on one of the Microsoft Word list templates
                // and apply it to the current paragraph in the document builder.
                builder.ListFormat.List = doc.Lists.Add(ListTemplate.BulletDiamonds);

                // There are 9 levels in this list, lets try them all.
                for (int i = 0; i < 9; i++)
                {
                    builder.ListFormat.ListLevelNumber = i;
                    builder.Writeln("Level " + i);
                }

                // This is a way to stop list formatting. 
                builder.ListFormat.List = null;
















                builder.InsertBreak(BreakType.ParagraphBreak);


                builder.Document.UpdateFields(); // 更新目录

                doc.Save(docFile);

                Label1.Text = "文档生成成功，时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
                //throw;
            }

        }
    }
}