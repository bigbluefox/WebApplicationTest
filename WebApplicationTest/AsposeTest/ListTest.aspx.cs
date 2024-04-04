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
using Hsp.Test.Common;

namespace WebApplicationTest.AsposeTest
{
    public partial class ListTest : System.Web.UI.Page
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

                //doc.FirstSection.Body.PrependChild(new Paragraph(doc));
                //// Move DocumentBuilder cursor to the beginning.
                //builder.MoveToDocumentStart();
                //// Insert a table of contents at the beginning of the document.
                //builder.InsertTableOfContents("\\o \"1-3\" \\h \\z \\u");
                //// Start the actual document content on the second page.

                // 将焦点移动到文档末尾
                builder.MoveToDocumentEnd();

                //builder.InsertBreak(BreakType.SectionBreakNewPage);

                // Build a document with complex structure by applying different heading styles thus creating TOC entries.
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
                builder.Writeln("Heading 1");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
                builder.Writeln("Heading 1.1");
                builder.Writeln("Heading 1.2");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
                builder.Writeln("Heading 2");
                builder.Writeln("Heading 3");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
                builder.Writeln("Heading 3.1");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading3;
                builder.Writeln("Heading 3.1.1");
                builder.Writeln("Heading 3.1.2");
                builder.Writeln("Heading 3.1.3");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
                builder.Writeln("Heading 3.2");
                builder.Writeln("Heading 3.3");
                builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
                // Call the method below to update the TOC.

                //builder.Document.UpdateFields();

                builder.InsertBreak(BreakType.ParagraphBreak);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + "无。");
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + "经济运行。");
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + "经济长期向好的基本面不断巩固和发展。");
                builder.InsertBreak(BreakType.ParagraphBreak);

                //1	经济运行 
                //1.1	经济长期向好的基本面不断巩固和发展 
                //2	经济结构优化 
                //2.1	紧紧依靠改革破解经济发展和结构失衡难题 

                //builder.ParagraphFormat.ClearFormatting(); //使用列项前需要首先去掉段落格式！！！；
                //builder.ListFormat.List = HeaderListFormat(doc);

                //WriteHeaderList(doc, builder, "1", "经济运行");
                //WriteHeaderList(doc, builder, "1.1", "经济长期向好的基本面不断巩固和发展");

                //WriteHeaderList(doc, builder, "2", "经济结构优化");
                //WriteHeaderList(doc, builder, "2.1", "紧紧依靠改革破解经济发展和结构失衡难题");

                //Document doc = new Document();
                //DocumentBuilder builder = new DocumentBuilder(doc);

                //Aspose.Words.Lists.List list = doc.Lists.Add(ListTemplate.NumberDefault);

                //// Level 1 labels will be "Appendix A", continuous and linked to the Heading 1 paragraph style.
                //list.ListLevels[0].NumberFormat = "\x00";
                //list.ListLevels[0].NumberStyle = NumberStyle.Arabic;
                ////list.ListLevels[0].LinkedStyle = doc.Styles["Heading 1"];
                //list.ListLevels[0].NumberPosition = WordHelper.FifthFontSize * 0;
                //list.ListLevels[0].TextPosition = WordHelper.FifthFontSize * 2;
                //list.ListLevels[0].TabPosition = WordHelper.FifthFontSize * 0;

                //// Level 2 labels will be "Section (1.01)" and restarting after Level 2 item appears.
                //list.ListLevels[1].NumberFormat = "\x00.\x01";
                //list.ListLevels[1].NumberStyle = NumberStyle.Arabic;
                ////list.ListLevels[1].LinkedStyle = doc.Styles["Heading 2"];
                //// Notice the higher level uses UppercaseLetter numbering, but we want arabic number
                //// of the higher levels to appear in this level, therefore set this property.
                ////list.ListLevels[1].IsLegal = true;
                ////list.ListLevels[1].RestartAfterLevel = 0;

                //list.ListLevels[1].NumberPosition = WordHelper.FifthFontSize * 0;
                //list.ListLevels[1].TextPosition = WordHelper.FifthFontSize * 2;
                //list.ListLevels[1].TabPosition = WordHelper.FifthFontSize * 0;


                //// Level 3 labels will be "-I-" and restarting after Level 2 item appears.
                //list.ListLevels[2].NumberFormat = "\x00.\x01.\x02";
                //list.ListLevels[2].NumberStyle = NumberStyle.Arabic;
                ////list.ListLevels[2].LinkedStyle = doc.Styles["Heading 3"];
                ////list.ListLevels[2].RestartAfterLevel = 1;

                //list.ListLevels[2].NumberPosition = WordHelper.FifthFontSize * 2;
                //list.ListLevels[2].TextPosition = WordHelper.FifthFontSize * 2;
                //list.ListLevels[2].TabPosition = WordHelper.FifthFontSize * 3;


                //// Level 4 labels will be "-I-" and restarting after Level 2 item appears.
                //list.ListLevels[3].NumberFormat = "\x00.\x01.\x02.\x03";
                //list.ListLevels[3].NumberStyle = NumberStyle.Arabic;
                ////list.ListLevels[3].LinkedStyle = doc.Styles["Heading 4"];

                //list.ListLevels[3].NumberPosition = WordHelper.FifthFontSize * 0;
                //list.ListLevels[3].TextPosition = WordHelper.FifthFontSize * 2;
                //list.ListLevels[3].TabPosition = WordHelper.FifthFontSize * 0;


                //// Level 5 labels will be "-I-" and restarting after Level 2 item appears.
                //list.ListLevels[4].NumberFormat = "\x00.\x01.\x02.\x03.\x04";
                //list.ListLevels[4].NumberStyle = NumberStyle.Arabic;
                ////list.ListLevels[4].LinkedStyle = doc.Styles["Heading 5"];

                //list.ListLevels[4].NumberPosition = WordHelper.FifthFontSize * 0;
                //list.ListLevels[4].TextPosition = WordHelper.FifthFontSize * 0;
                //list.ListLevels[4].TabPosition = WordHelper.FifthFontSize * 0;


                //// Make labels of all list levels bold.
                //foreach (ListLevel level in list.ListLevels)
                //{
                //    level.Font.Bold = true;
                //    level.Font.Size = WordHelper.FifthFontSize;
                //    level.TrailingCharacter = ListTrailingCharacter.Tab;
                //}

                // Apply list formatting to the current paragraph.
                Aspose.Words.Lists.List headerListFormat = WordHelper.HeaderListFormat(doc);
                builder.ListFormat.List = headerListFormat;

                // Exercise the 3 levels we created two times.
                string tabTitle;
                for (int n = 0; n < 6; n++)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        //builder.ListFormat.List = headerListFormat;

                        //builder.ListFormat.ListOutdent();
                        //builder.ListFormat.ListLevelNumber = i;
                        //builder.Writeln("Level " + i);

                        tabTitle = "Level " + i;
                        StyleIdentifier heading;
                        switch (i)
                        {
                            case 0:
                                heading = StyleIdentifier.Heading1;
                                break;
                            case 1:
                                heading = StyleIdentifier.Heading2;
                                break;
                            case 2:
                                heading = StyleIdentifier.Heading3;
                                break;
                            case 3:
                                heading = StyleIdentifier.Heading4;
                                break;
                            case 4:
                                heading = StyleIdentifier.Heading5;
                                break;
                            case 5:
                                heading = StyleIdentifier.Heading6;
                                break;
                            default:
                                heading = StyleIdentifier.BodyText;
                                break;
                        }

                        if (i == 0)
                        {
                            WordHelper.Title1Content(builder, WordHelper.FifthFontSize, headerListFormat, new[] { tabTitle });
                        }
                        else
                        {
                            WordHelper.TitleContent(builder, heading, headerListFormat, new[] { tabTitle });
                        }

                        //for (int j = 0; j < i + 1; j++)
                        //{
                        //    builder.ListFormat.ListLevelNumber = i;
                        //    builder.Writeln("Level " + i);                            
                        //}

                        //builder.ListFormat.RemoveNumbers();

                        tabTitle += " 正文。";
                        WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                        WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                        WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                        builder.InsertBreak(BreakType.ParagraphBreak);

                    }

                    //builder.InsertBreak(BreakType.ParagraphBreak);
                }

                builder.ListFormat.RemoveNumbers();

                //builder.Document.Save(MyDir + "Lists.CreateListRestartAfterHigher Out.doc");



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
                level1.TextPosition = 144;
                level1.TabPosition = 144;

                // Completely customize yet another list level.
                ListLevel level2 = list.ListLevels[1];
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
                builder.InsertBreak(BreakType.ParagraphBreak);




                //Shows how to create a list with some advanced formatting.
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
                builder.InsertBreak(BreakType.ParagraphBreak);




                // 术语表题处理
                //var termListFormat = WordHelper.TermListFormat(doc);
                builder.ListFormat.List = headerListFormat;
                builder.ListFormat.ListLevelNumber = 0;

                var termTitle = new[] { "术语和定义" };
                WordHelper.Title1Content(builder, WordHelper.FifthFontSize, headerListFormat, termTitle);

                builder.ListFormat.RemoveNumbers();

                tabTitle = "下列术语和定义适用于本标准。";
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                builder.ListFormat.List = headerListFormat;
                builder.ListFormat.ListLevelNumber = 1;

                termTitle = new[] { "", WordHelper.ParagraphLeadingCharacter + "风险" };
                WordHelper.TitleContent(builder, StyleIdentifier.Heading2, headerListFormat, termTitle);

                builder.ListFormat.RemoveNumbers();

                tabTitle = "不确定性对目标的影响。影响是指偏离预期，可以是正面的和/或负面的。目标可以是不同方面（如财务、健康与安全、环境等）和层面（如战略、组织、项目、产品和过程等）的目标。通常用潜在事件、后果或者两者的组合来区分风险。通常用事件后果（包括情形的变化）和事件发生可能性的组合来表示风险。不确定性是指对事件及其后果或可能性的信息缺失或了解片面的状态。";
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                builder.ListFormat.List = headerListFormat;
                builder.ListFormat.ListLevelNumber = 1;

                termTitle = new[] { "", WordHelper.ParagraphLeadingCharacter + "风险评估" };
                //WordHelper.TermTitle(builder, termTitle);
                WordHelper.TitleContent(builder, StyleIdentifier.Heading2, headerListFormat, termTitle);

                builder.ListFormat.RemoveNumbers();

                tabTitle = "包括风险识别、风险分析和风险评价的全过程。";
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                builder.ListFormat.List = headerListFormat;
                builder.ListFormat.ListLevelNumber = 1;

                termTitle = new[] { "", WordHelper.ParagraphLeadingCharacter + "风险应对" };
                //WordHelper.TermTitle(builder, termTitle);
                WordHelper.TitleContent(builder, StyleIdentifier.Heading2, headerListFormat, termTitle);

                builder.ListFormat.RemoveNumbers();

                tabTitle = "处理风险的过程。风险应对可以包括：不开始或不再继续导致风险的行为，以规避风险；为寻求机会而承担或增加风险；消除风险源；改变可能性；改变后果；与其他各方分担风险；慎重考虑后决定保留风险。针对负面后果的风险应对有时指“风险缓解”、“风险消除”、“风险降低”等。风险应对可能产生新的风险或改变现有风险。";
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);






                // 附录列表测试

                var appendixListFormat = WordHelper.AppendixListFormat(doc);
                //builder.ListFormat.List = appendixListFormat;

                //builder.ListFormat.ListLevelNumber = 0;
                //builder.Writeln("Level 1");

                var appendixSubTitle = "报告与记录";
                var appendixType = "规范性附录";
                var appendixTitle = new[] { "", "（" + appendixType + "）", appendixSubTitle };
                WordHelper.AppendixTitle(builder, appendixListFormat, appendixTitle);

                //builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                //builder.ListFormat.ListLevelNumber = 1;
                //builder.Writeln("Level 1.1");

                tabTitle = "Level 1.1";
                WordHelper.AppendixSubTitle(builder, appendixListFormat, tabTitle);

                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.1.1");
                //builder.ListFormat.RemoveNumbers();

                tabTitle = "内存狂魔芝奇又创造了新的奇迹，官方宣布在风冷条件下，将两条DDR4内存超到了5000MHz的恐怖频率，这在全球尚属首次";
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                //builder.ListFormat.List = appendixListFormat;
                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.1.2");
                //builder.ListFormat.RemoveNumbers();

                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //builder.InsertBreak(BreakType.ParagraphBreak);

                //builder.ListFormat.List = appendixListFormat;
                //builder.ListFormat.ListLevelNumber = 1;
                //builder.Writeln("Level 1.2");
                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.2.1");

                //builder.ListFormat.RemoveNumbers();

                tabTitle = "Level 1.2";
                WordHelper.AppendixSubTitle(builder, appendixListFormat, tabTitle);

                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                //builder.ListFormat.List = appendixListFormat;
                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.2.2");

                //builder.ListFormat.RemoveNumbers();

                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //builder.InsertBreak(BreakType.ParagraphBreak);

                //builder.ListFormat.List = appendixListFormat;
                //builder.ListFormat.ListLevelNumber = 1;
                //builder.Writeln("Level 1.3");
                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.3.1");

                //builder.ListFormat.RemoveNumbers();

                tabTitle = "Level 1.3";
                WordHelper.AppendixSubTitle(builder, appendixListFormat, tabTitle);

                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                builder.InsertBreak(BreakType.ParagraphBreak);

                //builder.ListFormat.List = appendixListFormat;
                //builder.ListFormat.ListLevelNumber = 2;
                //builder.Writeln("Level 1.3.2");

                //builder.ListFormat.RemoveNumbers();

                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //WordHelper.ParagraphsContent(builder, WordHelper.ParagraphLeadingCharacter + tabTitle);
                //builder.InsertBreak(BreakType.ParagraphBreak);






                builder.Document.UpdateFields(); // 更新目录


                doc.Save(docFile);
                doc.Save(docFile.Replace(".docx", ".pdf"), SaveFormat.Pdf);

                Label1.Text = "文档生成成功，时间：" + DateTime.Now.ToString("yyyy-MM-dd mm:HH:ss");
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }




        /// <summary>
        /// 文档表题列项数据输出
        /// </summary>
        /// <param name="doc">文档</param>
        /// <param name="builder"></param>
        /// <param name="listIndex"></param>
        /// <param name="title">列项标题内容</param>
        public static void WriteHeaderList(Document doc, DocumentBuilder builder, string listIndex, string title)
        {
            //var idx = 0;
            //var currentLevel = 0;

            builder.Font.Name = WordHelper.FontHei;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左对齐
            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.Font.Size = WordHelper.FifthFontSize;
            builder.RowFormat.Height = WordHelper.FifthFontSize;

            builder.ListFormat.ListIndent();
            builder.ListFormat.ListOutdent();
           

            WordHelper.ListContent(builder, listIndex, "　" + title);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnListLabel_Click(object sender, EventArgs e)
        {
            var file = @"D:\QCRPJSXZ 2208.08.01—2017《安全监督管理标准》.docx";
            Document doc = new Document(file);

            doc.UpdateListLabels();
            int listParaCount = 1;
            var txt = "";

            foreach (Paragraph paragraph in doc.GetChildNodes(NodeType.Paragraph, true))
            {
                // Find if we have the paragraph list. In our document our list uses plain arabic numbers,
                // which start at three and ends at six.
                if (paragraph.ListFormat.IsListItem)
                {
                    //Console.WriteLine("Paragraph #{0}", listParaCount);
                    txt += string.Format("Paragraph #{0}", listParaCount);
                    txt += "<br/ >";

                    // This is the text we get when actually getting when we output this node to text format. 
                    // The list labels are not included in this text output. Trim any paragraph formatting characters.
                    string paragraphText = paragraph.ToString(SaveFormat.Text).Trim();
                    //Console.WriteLine("Exported Text: " + paragraphText);
                    txt += string.Format("Exported Text: " + paragraphText);
                    txt += "<br/ >";

                    ListLabel label = paragraph.ListLabel;
                    // This gets the position of the paragraph in current level of the list. If we have a list with multiple level then this
                    // will tell us what position it is on that particular level.
                    //Console.WriteLine("Numerical Id: " + label.LabelValue);
                    txt += string.Format("Numerical Id: " + label.LabelValue);
                    txt += "<br/ >";

                    // Combine them together to include the list label with the text in the output.
                    //Console.WriteLine("List label combined with text: " + label.LabelString + " " + paragraphText);
                    txt += string.Format("List label combined with text: " + label.LabelString + " " + paragraphText);
                    txt += "<br/ ><br/ >";

                    listParaCount++;
                }

            }

            Label1.Text = txt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStringLength_Click(object sender, EventArgs e)
        {

            string[] wordsContent = {"aaa", "bbb"};
            var b = "sfsfs";

            wordsContent.Initialize();
            b = "";

            wordsContent = null;

            Label1.Text = (wordsContent == null? 0: wordsContent.Length) + " * " + b.Length + " | " + (wordsContent == null? 0: wordsContent.Count()) + " * " + b.Count();

        }


    }
}