using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Lists;
using Aspose.Words.Tables;
using System.Drawing;

namespace Hsp.Test.Common
{
    public class WordHelper
    {

        #region 参数定义

        /// <summary>
        /// 黑体
        /// </summary>
        public static string FontHei = "黑体";

        /// <summary>
        /// 宋体
        /// </summary>
        public static string FontSong = "宋体";

        /// <summary>
        ///     表题前导空格字符串
        /// </summary>
        public static string HeadingLeadingCharacter = "　";

        /// <summary>
        /// 正文前导空格字符串
        /// </summary>
        public static string ParagraphLeadingCharacter = "    ";

        /// <summary>
        /// 三号字体
        /// 正文，标题
        /// </summary>
        public static float ThirdFontSize = 16f;

        /// <summary>
        /// 五号字体
        /// 正文，标题
        /// </summary>
        public static float FifthFontSize = 10.5f;

        /// <summary>
        /// 小五号字体
        /// 表中文字
        /// </summary>
        public static float SmallFifthFontSize = 9f;

        /*
        字号与磅值有对应关系
        字号‘八号’对应磅值5 
        字号‘七号’对应磅值5.5 
        字号‘小六’对应磅值6.5 
        字号‘六号’对应磅值7.5 
        字号‘小五’对应磅值9 
        字号‘五号’对应磅值10.5 
        字号‘小四’对应磅值12 
        字号‘四号’对应磅值14 
        字号‘小三’对应磅值15 
        字号‘三号’对应磅值16 
        字号‘小二’对应磅值18 
        字号‘二号’对应磅值22 
        字号‘小一’对应磅值24 
        字号‘一号’对应磅值26 
        字号‘小初’对应磅值36 
        字号‘初号’对应磅值42 
        */

        //builder.ParagraphFormat.FirstLineIndent = 2;// 首行缩进
        //builder.ParagraphFormat.LeftIndent = 2; // 左缩进
        //builder.ParagraphFormat.LineSpacing = 1;
        //builder.ParagraphFormat.SpaceAfter = 5.56;
        //builder.ParagraphFormat.SpaceBefore = 5.56;

        #endregion

        #region 输出word正文段落内容

        /// <summary>
        /// 输出word正文段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="wordsContent">文字内容</param>
        public static void ParagraphsContent(DocumentBuilder builder, string wordsContent)
        {
            var fontName = FontSong;
            ParagraphsContent(builder, fontName, wordsContent);
        }

        /// <summary>
        /// 输出word正文段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体</param>
        /// <param name="wordsContent">文字内容</param>
        public static void ParagraphsContent(DocumentBuilder builder, string fontName, string wordsContent)
        {
            const double spaceAfter = 0f;
            const double spaceBefore = 0f;
            ParagraphsContent(builder, fontName, wordsContent, spaceAfter, spaceBefore);
        }

        /// <summary>
        /// 输出word正文段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体</param>
        /// <param name="wordsContent">文字内容</param>
        /// <param name="spaceAfter">段前空白</param>
        /// <param name="spaceBefore">段后空白</param>
        public static void ParagraphsContent(DocumentBuilder builder, string fontName, string wordsContent,
            double spaceAfter, double spaceBefore)
        {
            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = FifthFontSize;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左对齐
            builder.ParagraphFormat.SpaceAfter = spaceAfter;
            builder.ParagraphFormat.SpaceBefore = spaceBefore;
            builder.RowFormat.Height = FifthFontSize;
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #region 输出word正文标题内容

        /// <summary>
        /// 设置word标题字体样式（包含大纲级别的设置）
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="heading">标题级别</param>
        /// <param name="paragraphAlignment">对齐方式</param>
        /// <param name="format">表题列项格式</param>
        /// <param name="wordsContent">文字内容</param>
        public static void TitleContent(DocumentBuilder builder, StyleIdentifier heading, List format, string[] wordsContent)
        {
            TitleContent(builder, FontHei, FifthFontSize, heading, ParagraphAlignment.Left, format, wordsContent);
        }
        
        public static void TitleContent(DocumentBuilder builder, float fontSize, StyleIdentifier heading,
            ParagraphAlignment paragraphAlignment, List format, string[] wordsContent)
        {
            TitleContent(builder, FontHei, fontSize, heading, paragraphAlignment, format, wordsContent);
        }

        public static void TitleContent(DocumentBuilder builder, string fontName, float fontSize, StyleIdentifier heading,
            ParagraphAlignment paragraphAlignment, List format, string[] wordsContent)
        {
            #region 列表项定义

            var listLevelNumber = 0;

            switch (heading)
            {
                case StyleIdentifier.Heading1:
                    listLevelNumber = 0;
                    break;
                case StyleIdentifier.Heading2:
                    listLevelNumber = 1;
                    break;
                case StyleIdentifier.Heading3:
                    listLevelNumber = 2;
                    break;
                case StyleIdentifier.Heading4:
                    listLevelNumber = 3;
                    break;
                case StyleIdentifier.Heading5:
                    listLevelNumber = 4;
                    break;
                case StyleIdentifier.Heading6:
                    listLevelNumber = 5;
                    break;
                default:
                    listLevelNumber = 0;
                    break;
            }

            builder.ListFormat.List = format;
            builder.ListFormat.ListLevelNumber = listLevelNumber;

            #endregion

            builder.Bold = false;
            builder.Font.Italic = false;
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.ParagraphFormat.StyleIdentifier = heading;
            builder.ParagraphFormat.Alignment = paragraphAlignment; //水平居中对齐
            builder.ParagraphFormat.SpaceAfter = fontSize / 2;
            builder.ParagraphFormat.SpaceBefore = fontSize / 2;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = fontSize;
            //builder.Writeln(wordsContent);

            for (int i = 0; i < wordsContent.Length; i++)
            {
                if (i == wordsContent.Length - 1)
                {
                    builder.Writeln(wordsContent[i]);
                }
                else
                {

                    builder.Write(wordsContent[i]);
                    builder.InsertBreak(BreakType.LineBreak);
                }
            }

            builder.ListFormat.RemoveNumbers();

            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #region StyleIdentifier.Heading0 居中

        public static void Title0Content(DocumentBuilder builder, float fontSize, string wordsContent)
        {
            const StyleIdentifier heading = StyleIdentifier.Heading1;
            Title0Content(builder, fontSize, heading, wordsContent);
        }

        public static void Title0Content(DocumentBuilder builder, float fontSize, StyleIdentifier heading,
            string wordsContent)
        {
            var fontName = FontHei;
            const ParagraphAlignment paragraphAlignment = ParagraphAlignment.Center;
            Title0Content(builder, fontName, fontSize, heading, paragraphAlignment, wordsContent);
        }

        public static void Title0Content(DocumentBuilder builder, string fontName, float fontSize,
            StyleIdentifier heading, ParagraphAlignment paragraphAlignment, string wordsContent)
        {
            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.ParagraphFormat.StyleIdentifier = heading;
            builder.ParagraphFormat.Alignment = paragraphAlignment; //水平居中
            builder.ParagraphFormat.SpaceBefore = 32f;
            builder.ParagraphFormat.SpaceAfter = 28f;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = fontSize;
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #region StyleIdentifier.Heading1 居左/或自定义

        public static void Title1Content(DocumentBuilder builder, float fontSize, List format, string[] wordsContent)
        {
            var fontName = FontHei;
            Title1Content(builder, fontName, fontSize, format, wordsContent);
        }

        public static void Title1Content(DocumentBuilder builder, string fontName, float fontSize, List format, string[] wordsContent)
        {
            const ParagraphAlignment alignment = ParagraphAlignment.Left;
            Title1Content(builder, fontName, fontSize, alignment, format, wordsContent);
        }

        public static void Title1Content(DocumentBuilder builder, float fontSize, ParagraphAlignment alignment, List format, string[] wordsContent)
        {
            var fontName = FontHei;
            Title1Content(builder, fontName, fontSize, alignment, format, wordsContent);
        }

        public static void Title1Content(DocumentBuilder builder, string fontName, float fontSize, ParagraphAlignment alignment, List format, string[] wordsContent)
        {
            #region 列表项定义

            builder.ListFormat.List = format;
            builder.ListFormat.ListLevelNumber = 0;

            #endregion

            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            builder.ParagraphFormat.Alignment = alignment; //水平居左
            builder.ParagraphFormat.SpaceAfter = fontSize;
            builder.ParagraphFormat.SpaceBefore = fontSize;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = fontSize;
            //builder.Writeln(wordsContent);

            for (int i = 0; i < wordsContent.Length; i++)
            {
                if (i == wordsContent.Length - 1)
                {
                    builder.Writeln(wordsContent[i]);
                }
                else
                {

                    builder.Write(wordsContent[i]);
                    builder.InsertBreak(BreakType.LineBreak);
                }
            }

            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.SpaceAfter = 0f;

            builder.ListFormat.RemoveNumbers();

            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        /// <summary>
        /// 无题头输出
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="listContent"></param>
        /// <param name="wordsContent"></param>
        public static void ListContent(DocumentBuilder builder, string listContent, string wordsContent)
        {
            builder.Bold = false;
            builder.Font.Name = FontHei;
            builder.Font.Size = FifthFontSize;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居中对齐
            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = FifthFontSize;
            builder.Write(listContent);
            builder.Font.Name = FontSong;
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #region 获取段落分割数组结果

        /// <summary>
        /// 获取段落分割数组结果
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string[] GetStringArr(string s)
        {
            string[] arr = {};
            s = s.Trim();
            s = s.Replace((char) 13, (char) 0);
            s = s.Replace((char) 10, (char) 0);
            if (string.IsNullOrEmpty(s)) return arr;
            arr = s.Split('\0');
            //if (s.IndexOf("\0", StringComparison.Ordinal) > -1)
            //{
            //    arr = s.Split('\0');
            //}
            //else
            //{
            //    if (s.IndexOf("；", StringComparison.Ordinal) > -1)
            //    {
            //        arr = s.Split('；');
            //    }
            //    else
            //    {
            //        if (s.IndexOf("。", StringComparison.Ordinal) > -1)
            //        {
            //            arr = s.Split('。');
            //        }
            //    }
            //}
            return arr;
        }

        /// <summary>
        /// 获取段落分割数组结果
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">分割字符</param>
        /// <returns></returns>
        public static string[] GetStringArr(string s, char c)
        {
            string[] arr = { };
            s = s.Trim();
            return string.IsNullOrEmpty(s) ? arr : s.Split(c);
        }

        #endregion

        #region 添加表格单元格

        #region 添加表头单元格

        /// <summary>
        /// 添加表头单元格
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="wordsContent"></param>
        public static void InsertTableTitleCell(DocumentBuilder builder, int width, string wordsContent)
        {
            // 单元格合并
            const CellMerge cellMerge = CellMerge.None;
            var lineWidth = new[] { 0.5, 0.5, 0.5, 0.5 };
            InsertTableTitleCell(builder, width, lineWidth, cellMerge, wordsContent);
        }

        public static void InsertTableTitleCell(DocumentBuilder builder, int width, double[] lineWidth, string wordsContent)
        {
            // 单元格合并
            const CellMerge cellMerge = CellMerge.None;
            InsertTableTitleCell(builder, width, lineWidth, cellMerge, wordsContent);
        }

        public static void InsertTableTitleCell(DocumentBuilder builder, int width, CellMerge cellMerge, string wordsContent)
        {
            var lineWidth = new[] { 0.5, 0.5, 0.5, 0.5 };
            InsertTableTitleCell(builder, width, lineWidth, cellMerge, wordsContent);
        }

        /// <summary>
        /// 添加表头单元格
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="lineWidth">表格线宽度[上，右，下，左]</param>
        /// <param name="wordsContent"></param>
        public static void InsertTableTitleCell(DocumentBuilder builder, int width, double[] lineWidth, CellMerge cellMerge, string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.Font.Name = FontSong;
            builder.Font.Size = SmallFifthFontSize;

            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            builder.CellFormat.Width = width;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.CellFormat.Borders.Top.LineWidth = lineWidth[0];
            builder.CellFormat.Borders.Top.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Right.LineWidth = lineWidth[1];
            builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Bottom.LineWidth = lineWidth[2];
            builder.CellFormat.Borders.Bottom.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Left.LineWidth = lineWidth[3];
            builder.CellFormat.Borders.Left.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.VerticalMerge = cellMerge;
            builder.Write(wordsContent);

            //builder.CellFormat.ClearFormatting();
            //builder.ParagraphFormat.ClearFormatting();
        }
        
        #endregion

        #region 添加表体单元格

        /// <summary>
        /// 添加表体单元格（默认居中）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="wordsContent"></param>
        public static void InsertTableBodyCell(DocumentBuilder builder, int width, string wordsContent)
        {
            const ParagraphAlignment alignment = ParagraphAlignment.Center;
            InsertTableBodyCell(builder, width, alignment, wordsContent);
        }

        public static void InsertTableBodyCell(DocumentBuilder builder, int width, double[] lineWidth, string wordsContent)
        {
            const ParagraphAlignment alignment = ParagraphAlignment.Center;
            InsertTableBodyCell(builder, width, lineWidth, alignment, wordsContent);
        }

        /// <summary>
        /// 添加表体单元格（水平对齐）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="wordsContent"></param>
        public static void InsertTableBodyCell(DocumentBuilder builder, int width, ParagraphAlignment paragraphAlignment,
            string wordsContent)
        {
            const CellMerge cellMerge = CellMerge.None;
            var lineWidth = new[] { 0.5, 0.5, 0.5, 0.5 };
            InsertTableBodyCell(builder, width, lineWidth, paragraphAlignment, cellMerge, wordsContent);
        }

        public static void InsertTableBodyCell(DocumentBuilder builder, int width, double[] lineWidth, ParagraphAlignment paragraphAlignment,
            string wordsContent)
        {
            // 单元格合并
            const CellMerge cellMerge = CellMerge.None;
            InsertTableBodyCell(builder, width, lineWidth, paragraphAlignment, cellMerge, wordsContent);
        }

        /// <summary>
        /// 添加单元格合并
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="lineWidth"></param>
        /// <param name="cellMerge"></param>
        public static void InsertTableBodyCell(DocumentBuilder builder, int width, double[] lineWidth, CellMerge cellMerge)
        {
            const string wordsContent = "";
            const ParagraphAlignment paragraphAlignment = ParagraphAlignment.Left;
            InsertTableBodyCell(builder, width, lineWidth, paragraphAlignment, cellMerge, wordsContent);
        }

        public static void InsertTableBodyCell(DocumentBuilder builder, int width, CellMerge cellMerge)
        {
            const string wordsContent = "";
            var lineWidth = new[] { 0.5, 0.5, 0.5, 0.5 };
            const ParagraphAlignment paragraphAlignment = ParagraphAlignment.Left;
            InsertTableBodyCell(builder, width, lineWidth, paragraphAlignment, cellMerge, wordsContent);
        }

        public static void InsertTableBodyCell(DocumentBuilder builder, int width, ParagraphAlignment paragraphAlignment,
            CellMerge cellMerge, string wordsContent)
        {
            var lineWidth = new[] { 0.5, 0.5, 0.5, 0.5 };
            InsertTableBodyCell(builder, width, lineWidth, paragraphAlignment, cellMerge, wordsContent);
        }

        /// <summary>
        /// 添加表体单元格（水平对齐）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="paragraphAlignment"></param>
        /// <param name="cellMerge"></param>
        /// <param name="lineWidth">表格线宽度[上，右，下，左]</param>
        /// <param name="wordsContent"></param>
        public static void InsertTableBodyCell(DocumentBuilder builder, int width, double[] lineWidth, ParagraphAlignment paragraphAlignment,
            CellMerge cellMerge, string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.Font.Name = FontSong;
            builder.Font.Size = SmallFifthFontSize;

            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.Alignment = paragraphAlignment;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;

            builder.CellFormat.Width = width;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.CellFormat.Borders.Top.LineWidth = lineWidth[0];
            builder.CellFormat.Borders.Top.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Right.LineWidth = lineWidth[1];
            builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Bottom.LineWidth = lineWidth[2];
            builder.CellFormat.Borders.Bottom.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Left.LineWidth = lineWidth[3];
            builder.CellFormat.Borders.Left.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.VerticalMerge = cellMerge;
            builder.Write(wordsContent);
        }
        
        #endregion

        #region 添加表体单元格（内容换行）

        /// <summary>
        /// 添加表体单元格（内容换行）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="paragraphAlignment"></param>
        /// <param name="lineWidth">表格线宽度[上，右，下，左]</param>
        /// <param name="wordsContent"></param>
        public static void InsertTableContentlnCell(DocumentBuilder builder, int width, double[] lineWidth, ParagraphAlignment paragraphAlignment,
             string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.Font.Name = FontSong;
            builder.Font.Size = SmallFifthFontSize;

            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.Alignment = paragraphAlignment;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;

            builder.CellFormat.Width = width;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.CellFormat.Borders.Top.LineWidth = lineWidth[0];
            builder.CellFormat.Borders.Top.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Right.LineWidth = lineWidth[1];
            builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Bottom.LineWidth = lineWidth[2];
            builder.CellFormat.Borders.Bottom.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Left.LineWidth = lineWidth[3];
            builder.CellFormat.Borders.Left.LineStyle = Aspose.Words.LineStyle.Single;

            //builder.CellFormat.VerticalMerge = cellMerge;

            var wordsContentArr = wordsContent.Split(',');
            for (int i = 0; i < wordsContentArr.Length; i++)
            {
                if (wordsContentArr[i].IsNullOrEmpty()) continue;
                builder.Writeln(wordsContentArr[i]);
            }
        }

        #endregion

        #region 添加表体单元格（列项）

        /// <summary>
        /// 添加表体单元格（列项）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="width"></param>
        /// <param name="lineWidth"></param>
        /// <param name="paragraphAlignment"></param>
        /// <param name="wordsContent"></param>
        public static void InsertListTbCell(Document doc, DocumentBuilder builder, int width, double[] lineWidth,
            ParagraphAlignment paragraphAlignment, string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.Font.Name = FontSong;
            builder.Font.Size = SmallFifthFontSize;

            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.Alignment = paragraphAlignment;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;

            builder.CellFormat.Width = width;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.CellFormat.Borders.Top.LineWidth = lineWidth[0];
            builder.CellFormat.Borders.Top.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Right.LineWidth = lineWidth[1];
            builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Bottom.LineWidth = lineWidth[2];
            builder.CellFormat.Borders.Bottom.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.Borders.Left.LineWidth = lineWidth[3];
            builder.CellFormat.Borders.Left.LineStyle = Aspose.Words.LineStyle.Single;

            builder.CellFormat.VerticalMerge = CellMerge.None;

            #region 输出表内列项

            List<DocListNode> docList = DocListNodes(wordsContent);

            if (docList.Count > 1)
            {
                var idx = 0;
                var currentLevel = 0;

                foreach (var node in docList.Where(node => !string.IsNullOrEmpty(node.Text)))
                {
                    idx += 1;
                    if (!node.IsListItem)
                    {
                        // 非列项数据
                        builder.ListFormat.RemoveNumbers();

                        builder.Writeln(ParagraphLeadingCharacter + node.Text);

                        currentLevel = 0;
                    }
                    else
                    {
                        // 列项数据
                        if (currentLevel == 0)
                        {
                            builder.ParagraphFormat.ClearFormatting(); //使用列项前需要首先去掉段落格式！！！；
                            builder.ListFormat.List = GetDocListFormat(doc);
                        }

                        if (node.NumberStyle == ListNumberStyle.LowercaseLetter)
                        {
                            // 字母
                            for (int i = 1; i < currentLevel; i++)
                            {
                                builder.ListFormat.ListOutdent();
                            }

                            currentLevel = node.NodeLevel;
                        }

                        if (node.NumberStyle == ListNumberStyle.Arabic)
                        {
                            // 数字
                            if (currentLevel == 1)
                            {
                                builder.ListFormat.ListIndent();
                            }

                            for (int i = 2; i < currentLevel; i++)
                            {
                                builder.ListFormat.ListOutdent();
                            }

                            currentLevel = node.NodeLevel;
                        }

                        if (node.NumberStyle == ListNumberStyle.Bullet)
                        {
                            // 圆点
                            for (int i = currentLevel; i < 4; i++)
                            {
                                builder.ListFormat.ListIndent();
                            }

                            currentLevel = node.NodeLevel;
                        }

                        if (node.NumberStyle == ListNumberStyle.None)
                        {
                            // 破折号
                            if (currentLevel == 2)
                            {
                                builder.ListFormat.ListIndent();
                            }

                            for (int i = 3; i < currentLevel; i++)
                            {
                                builder.ListFormat.ListOutdent();
                            }

                            currentLevel = node.NodeLevel;
                        }

                        builder.Writeln(node.Text);
                    }
                }

                builder.ListFormat.RemoveNumbers();
            }
            else
            {
                // 应田集要求 统一输出格式 加上 ParagraphLeadingCharacter
                builder.Write(ParagraphLeadingCharacter + wordsContent); 
            }

            #endregion
        }

        #endregion

        #endregion

        #region 设置word表格样式

        /// <summary>
        /// 设置word表格样式
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="width">单元格宽度</param>
        /// <param name="cellMerge">合并单元格</param>
        /// <param name="wordsContent">文字内容</param>
        public static void FontStyle(DocumentBuilder builder, string fontName, float fontSize,
            int width, CellMerge cellMerge, string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.CellFormat.Width = width;
            builder.CellFormat.HorizontalMerge = cellMerge;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.Write(wordsContent);
        }

        #endregion

        #region 设置word表格样式

        /// <summary>
        /// 设置word表格样式
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="width">单元格宽度</param>
        /// <param name="cellMerge">合并单元格</param>
        /// <param name="wordsContent">文字内容</param>
        public static void FontStyle(DocumentBuilder builder, string fontName, float fontSize,
            int width, CellMerge cellMerge, ParagraphAlignment paragraphAlignment, string wordsContent)
        {
            builder.InsertCell();
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.Font.Bold = false;
            builder.Font.Color = Color.Black;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.Alignment = paragraphAlignment; //水平居中对齐
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.CellFormat.Width = width;
            builder.CellFormat.HorizontalMerge = cellMerge;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.Write(wordsContent);
        }

        #endregion

        #region 设置word字体样式（包含大纲级别的设置）

        /// <summary>
        /// 设置word字体样式（包含大纲级别的设置）
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="heading">标题级别</param>
        /// <param name="paragraphAlignment">对齐方式</param>
        /// <param name="wordsContent">文字内容</param>
        public static void FontStyle(DocumentBuilder builder, string fontName, float fontSize,
            StyleIdentifier heading, ParagraphAlignment paragraphAlignment, string wordsContent)
        {
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.Font.Bold = false;
            builder.Font.Italic = false;
            builder.ParagraphFormat.StyleIdentifier = heading;
            builder.ParagraphFormat.Alignment = paragraphAlignment; //水平对齐方式
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #region 设置书签内容

        /// <summary>
        /// 设置书签内容
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="bookmarkName">书签名</param>
        /// <param name="wordsContent">文字内容</param>
        public static void SetBookmarkContent(Document document, string bookmarkName, string wordsContent)
        {
            //if (string.IsNullOrEmpty(wordsContent)) return;

            var isExist = false;
            foreach (Bookmark bm in document.Range.Bookmarks.Cast<Bookmark>().Where(bm => bm.Name == bookmarkName))
            {
                isExist = true;
            }

            if (isExist)
            {
                document.Range.Bookmarks[bookmarkName].Text = wordsContent;
            }
            else
            {
                throw new Exception("书签(" + bookmarkName + ")不存在");
            }
        }

        #endregion

        #region 单元格合并

        /// <summary>
        /// 单元格合并
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="mergeCount"></param>
        public static void SetCellMerge(DocumentBuilder builder, int mergeCount)
        {
            for (int i = 0; i < mergeCount; i++)
            {
                builder.InsertCell();
                FontStyle(builder, "宋体", SmallFifthFontSize, 230, CellMerge.Previous, "");
            }
        }

        #endregion


        /// <summary>
        /// 属于标题输出
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="wordsContent"></param>
        public static void TermTitle(DocumentBuilder builder, string[] wordsContent)
        {
            builder.Bold = false;
            builder.Font.Name = FontHei;
            builder.Font.Size = FifthFontSize;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左
            builder.ParagraphFormat.SpaceAfter = FifthFontSize;
            builder.ParagraphFormat.SpaceBefore = FifthFontSize;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = FifthFontSize;

            for (int i = 0; i < wordsContent.Length; i++)
            {
                if (i == wordsContent.Length - 1)
                {
                    builder.Writeln(wordsContent[i]);
                }
                else
                {

                    builder.Write(wordsContent[i]);
                    builder.InsertBreak(BreakType.LineBreak);
                }
            }

            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        /// <summary>
        /// Word文档术语列项定义
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List TermListFormat(Aspose.Words.Document doc)
        {
            // Create a list based on one of the Microsoft Word list templates.
            Aspose.Words.Lists.List list = doc.Lists.Add(ListTemplate.NumberDefault);

            // Level 1 labels.
            list.ListLevels[0].NumberFormat = "\x00";
            list.ListLevels[0].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[0].LinkedStyle = doc.Styles["Heading 1"];
            list.ListLevels[0].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[0].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[0].TabPosition = WordHelper.FifthFontSize * 4;

            // Level 2 labels.
            list.ListLevels[1].NumberFormat = "\x00.\x01" + HeadingLeadingCharacter;
            list.ListLevels[1].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[1].LinkedStyle = doc.Styles["Heading 2"];
            list.ListLevels[1].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[1].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[1].TabPosition = WordHelper.FifthFontSize * 4;

            // Level 3 labels.
            list.ListLevels[2].NumberFormat = "\x00.\x01.\x02" + HeadingLeadingCharacter;
            list.ListLevels[2].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[2].LinkedStyle = doc.Styles["Heading 3"];
            list.ListLevels[2].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[2].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[2].TabPosition = WordHelper.FifthFontSize * 4;

            // Make labels of all list levels bold.
            foreach (ListLevel level in list.ListLevels)
            {
                level.Font.Bold = false;
                level.Font.Italic = false;
                level.Font.Name = WordHelper.FontHei;
                level.Font.Size = WordHelper.FifthFontSize;
                level.Alignment = ListLevelAlignment.Left;

                level.TrailingCharacter = ListTrailingCharacter.Space;
            }

            return list;
        }

        #region 附录内容输出

        #region 附录标题输出

        /// <summary>
        /// 附录标题输出
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="format">附录输出格式</param>
        /// <param name="wordsContent"></param>
        public static void AppendixTitle(DocumentBuilder builder, List format, string[] wordsContent)
        {
            AppendixTitle(builder, FontHei, FifthFontSize, ParagraphAlignment.Center, format, wordsContent);
        }

        /// <summary>
        /// 附录标题输出
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="alignment"></param>
        /// <param name="format"></param>
        /// <param name="wordsContent"></param>
        public static void AppendixTitle(DocumentBuilder builder, string fontName, float fontSize,
            ParagraphAlignment alignment, List format, string[] wordsContent)
        {
            #region 列表项定义

            builder.ListFormat.List = format;
            builder.ListFormat.ListLevelNumber = 0;

            #endregion

            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            builder.ParagraphFormat.Alignment = alignment; //水平居左
            builder.ParagraphFormat.SpaceAfter = fontSize;
            builder.ParagraphFormat.SpaceBefore = fontSize;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = fontSize;

            for (int i = 0; i < wordsContent.Length; i++)
            {
                if (i == wordsContent.Length - 1)
                {
                    builder.Writeln(wordsContent[i]);
                }
                else
                {
                    builder.Write(wordsContent[i]);
                    builder.InsertBreak(BreakType.LineBreak);
                }
            }

            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.SpaceAfter = 0f;

            builder.ListFormat.RemoveNumbers();

            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        /// <summary>
        /// Word文档附录列项定义
        /// </summary>
        /// <param name="doc">文档</param>
        /// <returns></returns>
        public static List AppendixListFormat(Document doc)
        {
            // Create a list based on one of the Microsoft Word list templates.
            Aspose.Words.Lists.List list = doc.Lists.Add(ListTemplate.NumberDefault);

            // Level 1 labels.
            list.ListLevels[0].NumberFormat = "附 录 \x00";
            list.ListLevels[0].NumberStyle = NumberStyle.UppercaseLetter;
            list.ListLevels[0].LinkedStyle = doc.Styles["Heading 1"];

            // Level 2 labels.
            list.ListLevels[1].NumberFormat = "\x00.\x01" + WordHelper.HeadingLeadingCharacter;
            list.ListLevels[1].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[1].LinkedStyle = doc.Styles["Heading 2"];

            // Level 3 labels.
            list.ListLevels[2].NumberFormat = "\x00.\x01.\x02" + WordHelper.HeadingLeadingCharacter;
            list.ListLevels[2].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[2].LinkedStyle = doc.Styles["Heading 3"];

            // Make labels of all list levels bold.
            foreach (ListLevel level in list.ListLevels)
            {
                level.Font.Bold = false;
                level.Font.Italic = false;
                level.Font.Name = WordHelper.FontHei;
                level.Font.Size = WordHelper.FifthFontSize;
                level.Alignment = ListLevelAlignment.Left;

                level.NumberPosition = WordHelper.FifthFontSize * 0;
                level.TextPosition = WordHelper.FifthFontSize * 0;
                level.TabPosition = WordHelper.FifthFontSize * 0;

                level.TrailingCharacter = ListTrailingCharacter.Space;
            }

            return list;
        }


        #endregion

        #region 附录次级标题输出

        /// <summary>
        /// 附录次级标题输出
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="format">附录列项格式</param>
        /// <param name="wordsContent">文字内容</param>
        public static void AppendixSubTitle(DocumentBuilder builder, List format, string wordsContent)
        {
            #region 列表项定义

            builder.ListFormat.List = format;
            builder.ListFormat.ListLevelNumber = 1;

            #endregion

            builder.Bold = false;
            builder.Font.Name = FontHei;
            builder.Font.Size = FifthFontSize;
            builder.RowFormat.Height = FifthFontSize;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左
            builder.ParagraphFormat.SpaceBefore = FifthFontSize;
            builder.ParagraphFormat.SpaceAfter = FifthFontSize;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.ParagraphFormat.SpaceAfter = 0f;

            builder.ListFormat.RemoveNumbers();
        }

        #endregion

        #region 输出word附录段落内容

        /// <summary>
        /// 输出word附录段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="wordsContent">文字内容</param>
        public static void AppendixParagraphs(DocumentBuilder builder, string wordsContent)
        {
            var fontName = FontSong;
            AppendixParagraphs(builder, fontName, wordsContent);
        }

        /// <summary>
        /// 输出word附录段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体</param>
        /// <param name="wordsContent">文字内容</param>
        public static void AppendixParagraphs(DocumentBuilder builder, string fontName, string wordsContent)
        {
            const double spaceAfter = 0f;
            const double spaceBefore = 0f;
            AppendixParagraphs(builder, fontName, wordsContent, spaceAfter, spaceBefore);
        }

        /// <summary>
        /// 输出word附录段落内容
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        /// <param name="fontName">字体</param>
        /// <param name="wordsContent">文字内容</param>
        /// <param name="spaceAfter">段前空白</param>
        /// <param name="spaceBefore">段后空白</param>
        public static void AppendixParagraphs(DocumentBuilder builder, string fontName, string wordsContent,
            double spaceAfter, double spaceBefore)
        {
            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = FifthFontSize;
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左对齐
            builder.ParagraphFormat.SpaceAfter = spaceAfter;
            builder.ParagraphFormat.SpaceBefore = spaceBefore;
            builder.RowFormat.Height = FifthFontSize;
            builder.Write(wordsContent);
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #endregion

        #region 文档截止线输出

        /// <summary>
        /// 文档截止线输出
        /// </summary>
        /// <param name="builder">文档生成对象</param>
        public static void WriteCutOffLine(DocumentBuilder builder)
        {
            builder.Writeln("");
            builder.Font.Name = "Times New Roman";
            builder.Font.Size = FifthFontSize;
            builder.Font.Bold = false;
            builder.Font.Italic = false;
            builder.RowFormat.Height = FifthFontSize;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.ParagraphFormat.LineSpacing = FifthFontSize;
            //builder.ParagraphFormat.SpaceAfter = FifthFontSize / 2.0;
            //builder.ParagraphFormat.SpaceBefore = FifthFontSize / 2.0; 
            //builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText; ;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center; //水平对齐方式
            builder.Writeln("_________________________");
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }

        #endregion

        #region Word文档列项定义

        /// <summary>
        /// Word文档列项定义，共分为四级：
        /// 一级用小写字母+半角右括号；
        /// 二级用数字+半角右括号；
        /// 三级用破折号，后面不跟空格；
        /// 圆点相当于是四级，不缩进；
        /// </summary>
        /// <param name="doc">文档</param>
        /// <returns></returns>
        public static List GetDocListFormat(Document doc)
        {
            string template = string.Empty;

            try
            {
                template = ConfigurationManager.AppSettings["StandardSystemTemplate"];
            }
            catch (Exception)
            {
                template = StandardSystemTemplate.Standard;
                //throw;
            }

            // 取消田集模板，Tli，20170309
            //if (template == StandardSystemTemplate.Tianji)
            //{
            //    return DocListFormatofTianji(doc);
            //}
            //else
            //{
            //    return DocListFormatByTemp(doc);
            //}

            return DocListFormatByTemp(doc);
        }

        private static List DocListFormatByTemp(Document doc)
        {
            // Create a list based on one of the Microsoft Word list templates.
            List list = doc.Lists.Add(ListTemplate.NumberDefault);

            // Completely customize one list level.
            ListLevel level1 = list.ListLevels[0];
            level1.Font.Size = 10.5;
            level1.Font.Color = Color.Black;
            level1.NumberStyle = NumberStyle.LowercaseLetter;
            level1.StartAt = 1;
            level1.NumberFormat = "\x0)";
            level1.NumberPosition = 21;
            level1.TextPosition = 42;
            level1.TabPosition = 42;
            //level1.RestartAfterLevel = 1;
            level1.TrailingCharacter = ListTrailingCharacter.Tab;

            // Completely customize yet another list level.
            ListLevel level2 = list.ListLevels[1];
            level2.Font.Size = 10.5;
            level2.Font.Color = Color.Black;
            level2.NumberStyle = NumberStyle.Arabic;
            level2.StartAt = 1;
            level2.NumberFormat = "\x1)";
            level2.NumberPosition = 42;
            level2.TextPosition = 63;
            level2.TabPosition = 63;
            level2.TrailingCharacter = ListTrailingCharacter.Tab;

            ListLevel level3 = list.ListLevels[2];
            level3.Font.Size = 10.5;
            level3.Font.Name = "宋体";
            level3.Font.Color = Color.Black;
            level3.NumberStyle = NumberStyle.None;
            level3.NumberFormat = "\x2——";
            level3.NumberPosition = 84; // 63
            level3.TextPosition = 84; // 63
            level3.TabPosition = 84; // 0.0
            level3.TrailingCharacter = ListTrailingCharacter.Nothing;

            ListLevel level4 = list.ListLevels[3];
            level4.Font.Size = 9;
            level4.Font.Name = "Times New Roman";
            level4.Font.Color = Color.Black;
            level4.NumberStyle = NumberStyle.Bullet;
            level4.NumberFormat = "\x3●";
            level4.NumberPosition = 84; // 42
            level4.TextPosition = 105; // 63
            level4.TabPosition = 105; // 0.0
            level4.TrailingCharacter = ListTrailingCharacter.Tab;

            return list;
        }

        private static List DocListFormatofTianji(Document doc)
        {
            // Create a list based on one of the Microsoft Word list templates.
            List list = doc.Lists.Add(ListTemplate.NumberDefault);

            // Completely customize one list level.
            ListLevel level1 = list.ListLevels[0];
            level1.Font.Size = 10.5;
            level1.Font.Color = Color.Black;
            level1.NumberStyle = NumberStyle.LowercaseLetter;
            level1.StartAt = 1;
            level1.NumberFormat = "\x0)";
            level1.NumberPosition = 21;
            level1.TextPosition = 42;
            level1.TabPosition = 42;
            level1.TrailingCharacter = ListTrailingCharacter.Tab;

            // Completely customize yet another list level.
            ListLevel level2 = list.ListLevels[1];
            level2.Font.Size = 10.5;
            level2.Font.Color = Color.Black;
            level2.NumberStyle = NumberStyle.Arabic;
            level2.StartAt = 1;
            level2.NumberFormat = "\x1)";
            level2.NumberPosition = 21;
            level2.TextPosition = 42;
            level2.TabPosition = 42;
            level2.TrailingCharacter = ListTrailingCharacter.Tab;

            ListLevel level3 = list.ListLevels[2];
            level3.Font.Size = 10.5;
            level3.Font.Name = "宋体";
            level3.Font.Color = Color.Black;
            level3.NumberStyle = NumberStyle.None;
            level3.NumberFormat = "\x2——";
            level3.NumberPosition = 42;
            level3.TextPosition = 42;
            level3.TabPosition = 0.0;
            level3.TrailingCharacter = ListTrailingCharacter.Nothing;

            ListLevel level4 = list.ListLevels[3];
            level4.Font.Size = 9;
            level4.Font.Name = "Times New Roman";
            level4.Font.Color = Color.Black;
            level4.NumberStyle = NumberStyle.Bullet;
            level4.NumberFormat = "\x3●";
            level4.NumberPosition = 21;
            level4.TextPosition = 42;
            level4.TabPosition = 0.0;
            level4.TrailingCharacter = ListTrailingCharacter.Tab;

            return list;
        }

        /// <summary>
        /// Word文档表题列项定义
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List HeaderListFormat(Aspose.Words.Document doc)
        {
            // Create a list based on one of the Microsoft Word list templates.
            Aspose.Words.Lists.List list = doc.Lists.Add(ListTemplate.NumberDefault);

            // Level 1 labels.
            list.ListLevels[0].NumberFormat = "\x00　";
            list.ListLevels[0].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[0].LinkedStyle = doc.Styles["Heading 1"];
            list.ListLevels[0].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[0].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[0].TabPosition = WordHelper.FifthFontSize * 0;

            // Level 2 labels.
            list.ListLevels[1].NumberFormat = "\x00.\x01　";
            list.ListLevels[1].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[1].LinkedStyle = doc.Styles["Heading 2"];
            list.ListLevels[1].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[1].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[1].TabPosition = WordHelper.FifthFontSize * 0;
            // Notice the higher level uses UppercaseLetter numbering, but we want arabic number
            // of the higher levels to appear in this level, therefore set this property.
            //list.ListLevels[1].IsLegal = true;
            //list.ListLevels[1].RestartAfterLevel = 0;

            // Level 3 labels.
            list.ListLevels[2].NumberFormat = "\x00.\x01.\x02　";
            list.ListLevels[2].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[2].LinkedStyle = doc.Styles["Heading 3"];
            list.ListLevels[2].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[2].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[2].TabPosition = WordHelper.FifthFontSize * 0;
            //list.ListLevels[2].RestartAfterLevel = 1;

            // Level 4 labels.
            list.ListLevels[3].NumberFormat = "\x00.\x01.\x02.\x03　";
            list.ListLevels[3].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[3].LinkedStyle = doc.Styles["Heading 4"];
            list.ListLevels[3].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[3].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[3].TabPosition = WordHelper.FifthFontSize * 0;

            // Level 5 labels.
            list.ListLevels[4].NumberFormat = "\x00.\x01.\x02.\x03.\x04　";
            list.ListLevels[4].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[4].LinkedStyle = doc.Styles["Heading 5"];
            list.ListLevels[4].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[4].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[4].TabPosition = WordHelper.FifthFontSize * 0;

            // Level 6 labels.
            list.ListLevels[5].NumberFormat = "\x00.\x01.\x02.\x03.\x04.\x05　";
            list.ListLevels[5].NumberStyle = NumberStyle.Arabic;
            list.ListLevels[5].LinkedStyle = doc.Styles["Heading 6"];
            list.ListLevels[5].NumberPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[5].TextPosition = WordHelper.FifthFontSize * 0;
            list.ListLevels[5].TabPosition = WordHelper.FifthFontSize * 0;

            // Make labels of all list levels bold.
            foreach (ListLevel level in list.ListLevels)
            {
                level.Font.Bold = false;
                level.Font.Italic = false;
                level.Font.Name = WordHelper.FontHei;
                level.Font.Size = WordHelper.FifthFontSize;
                level.Alignment = ListLevelAlignment.Left;

                level.TrailingCharacter = ListTrailingCharacter.Space;
            }

            return list;
        }

        public static void HeaderListContent(DocumentBuilder builder, string fontName, float fontSize, StyleIdentifier heading,
            ParagraphAlignment paragraphAlignment, string wordsContent)
        {
            builder.Bold = false;
            builder.Font.Name = fontName;
            builder.Font.Size = fontSize;
            builder.ParagraphFormat.StyleIdentifier = heading;
            builder.ParagraphFormat.Alignment = paragraphAlignment; //水平居中对齐
            builder.ParagraphFormat.SpaceAfter = fontSize / 2;
            builder.ParagraphFormat.SpaceBefore = fontSize / 2;
            builder.ParagraphFormat.FirstLineIndent = 0f;
            builder.RowFormat.Height = fontSize;
            builder.Writeln(wordsContent);
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
        }


        #endregion

        #region 获取文档列项数据列表

        /// <summary>
        /// 获取文档列项数据列表
        /// </summary>
        /// <param name="strDoc"></param>
        /// <returns></returns>
        public static List<DocListNode> DocListNodes(string strDoc)
        {
            var i = 0;
            string[] arr = GetStringArr(strDoc);
            var nodes = new List<DocListNode>();

            foreach (var s in arr.Where(s => !string.IsNullOrEmpty(s)))
            {
                var str = s.Trim();
                if(str.Length < 3) continue;
                i += 1;
                var node = new DocListNode {Id = i};

                /*
                 * 
                // ？•
                var pattern = @"\b[?•]\w*\b";
                if (Regex.IsMatch(str, pattern))
                {
                    node.IsListItem = true;
                    node.NodeLevel = 4;
                    node.NumberStyle = ListNumberStyle.Bullet;
                    node.NumberFormat = ListNumberFormat.Blank;
                    node.Text = str.Substring(1).Trim();
                }

                // ——
                pattern = @"\b[—]{2}\w*\b";
                if (Regex.IsMatch(str, pattern))
                {
                    node.IsListItem = true;
                    node.NodeLevel = 3;
                    node.NumberStyle = ListNumberStyle.None;
                    node.NumberFormat = ListNumberFormat.Dash;
                    node.Text = str.Substring(2).Trim();
                }

                // 小写字母
                pattern = @"\b[A-Za-z][\s]*[)）]\w*\b";
                if (Regex.IsMatch(str, pattern))
                {
                    // 小写字母
                    node.IsListItem = true;
                    node.NodeLevel = 1;
                    node.NumberStyle = ListNumberStyle.LowercaseLetter;
                    node.NumberFormat = ListNumberFormat.Bracket;
                    node.Text = str.Substring(headTwoLength + 1).Trim();
                }

                // 数字
                pattern = @"\b[A-Za-z][\s]*[)）]\w*\b";
                if (Regex.IsMatch(str, pattern))
                {
                    // 数字
                    node.IsListItem = true;
                    node.NodeLevel = 2;
                    node.NumberStyle = ListNumberStyle.Arabic;
                    node.NumberFormat = ListNumberFormat.Bracket;
                    node.Text = str.Substring(headTwoLength + 1).Trim();
                }

                // 非列项
                pattern = @"\b[A-Za-z][\s]*[)）]\w*\b";
                if (Regex.IsMatch(str, pattern))
                {
                    // 非列项
                    node.IsListItem = false;
                    node.NodeLevel = 0;
                    node.NumberStyle = ListNumberStyle.None;
                    node.NumberFormat = ListNumberFormat.Blank;
                    node.Text = str.Trim();
                }
                */

                var headOne = str.Substring(0, 1);
                var headTwo = str.Substring(0, 2);

                if (headOne == "?" || headOne == "●" || headOne == "•" || headOne == "．")
                {
                    node.IsListItem = true;
                    node.NodeLevel = 4;
                    node.NumberStyle = ListNumberStyle.Bullet;
                    node.NumberFormat = ListNumberFormat.Blank;
                    node.Text = str.Substring(1).Trim();
                }
                else if (headTwo == "——")
                {
                    node.IsListItem = true;
                    node.NodeLevel = 3;
                    node.NumberStyle = ListNumberStyle.None;
                    node.NumberFormat = ListNumberFormat.Dash;
                    node.Text = str.Substring(2).Trim();
                }
                else
                {
                    var headstr = str.Replace("）", ")");
                    if (headstr.IndexOf(")", StringComparison.Ordinal) > -1)
                    {
                        headTwo = headstr.Split(')')[0];
                        var headTwoLength = headTwo.Length;
                        headTwo = headTwo.Trim();
                        if (headTwo.Length < 3)
                        {
                            if (headTwo.Length < 2 && Regex.IsMatch(headTwo, "[a-zA-Z]"))
                            {
                                // 小写字母
                                node.IsListItem = true;
                                node.NodeLevel = 1;
                                node.NumberStyle = ListNumberStyle.LowercaseLetter;
                                node.NumberFormat = ListNumberFormat.Bracket;
                                node.Text = str.Substring(headTwoLength + 1).Trim();
                            }
                            else if (headTwo.Length < 3 && Regex.IsMatch(headTwo, "^[0-9]*$"))
                            {
                                // 数字
                                node.IsListItem = true;
                                node.NodeLevel = 2;
                                node.NumberStyle = ListNumberStyle.Arabic;
                                node.NumberFormat = ListNumberFormat.Bracket;
                                node.Text = str.Substring(headTwoLength + 1).Trim();
                            }
                            else
                            {
                                // 非列项
                                node.IsListItem = false;
                                node.NodeLevel = 0;
                                node.NumberStyle = ListNumberStyle.None;
                                node.NumberFormat = ListNumberFormat.Blank;
                                node.Text = str.Trim();
                            }
                        }
                        else
                        {
                            // 非列项
                            node.IsListItem = false;
                            node.NodeLevel = 0;
                            node.NumberStyle = ListNumberStyle.None;
                            node.NumberFormat = ListNumberFormat.Blank;
                            node.Text = str.Trim(); 
                        }
                    }
                    else
                    {
                        // 非列项
                        node.IsListItem = false;
                        node.NodeLevel = 0;
                        node.NumberStyle = ListNumberStyle.None;
                        node.NumberFormat = ListNumberFormat.Blank;
                        node.Text = str.Trim();
                    }
                }

                nodes.Add(node);
            }
            return nodes;
        }

        #endregion

        #region 文档列项数据输出

        /// <summary>
        /// 文档列项数据输出
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="builder"></param>
        /// <param name="nodes"></param>
        public static void WriteDocList(Document doc, DocumentBuilder builder, IEnumerable<DocListNode> nodes)
        {
            WriteDocList(doc, builder, nodes, "");
        }

        /// <summary>
        /// 文档列项数据输出
        /// </summary>
        /// <param name="doc">文档</param>
        /// <param name="builder"></param>
        /// <param name="nodes">列项数据列表</param>
        /// <param name="listIndex"></param>
        public static void WriteDocList(Document doc, DocumentBuilder builder, IEnumerable<DocListNode> nodes, string listIndex)
        {
            WriteDocList(doc, builder, nodes, listIndex, "");
        }

        /// <summary>
        /// 文档列项数据输出
        /// </summary>
        /// <param name="doc">文档</param>
        /// <param name="builder"></param>
        /// <param name="nodes">列项数据列表</param>
        /// <param name="listIndex"></param>
        /// <param name="title">列项标题内容</param>
        public static void WriteDocList(Document doc, DocumentBuilder builder, IEnumerable<DocListNode> nodes, string listIndex, string title)
        {
            var idx = 0;
            var currentLevel = 0;

            builder.Font.Name = "宋体";
            builder.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left; //水平居左对齐
            builder.ParagraphFormat.SpaceAfter = 0f;
            builder.ParagraphFormat.SpaceBefore = 0f;
            builder.Font.Size = 10.5f;
            builder.RowFormat.Height = 10.5f;

            foreach (var node in nodes)
            {
                if (string.IsNullOrEmpty(node.Text)) continue;

                idx += 1;
                if (!node.IsListItem)
                {
                    // 非列项数据
                    builder.ListFormat.RemoveNumbers();
                    if (idx == 1)
                    {
                        var tabTitle = title + node.Text + "。";
                        tabTitle = tabTitle.Replace("。。", "。").Replace("。，", "，").Replace("：。", "：").Replace("；。", "：");

                        if (string.IsNullOrEmpty(listIndex))
                        {
                            ParagraphsContent(builder, ParagraphLeadingCharacter + tabTitle);
                        }
                        else
                        {
                            ListContent(builder, listIndex, "　" + tabTitle);
                        }
                    }
                    else
                    {
                        builder.Writeln(ParagraphLeadingCharacter + node.Text);
                    }

                    currentLevel = 0;
                }
                else
                {
                    // 列项数据
                    if (currentLevel == 0)
                    {
                        builder.ParagraphFormat.ClearFormatting();//使用列项前需要首先去掉段落格式！！！；
                        builder.ListFormat.List = GetDocListFormat(doc);
                    }

                    if (node.NumberStyle == ListNumberStyle.LowercaseLetter)
                    {
                        // 字母
                        for (int i = 1; i < currentLevel; i++)
                        {
                            builder.ListFormat.ListOutdent();
                        }

                        currentLevel = node.NodeLevel;
                    }

                    if (node.NumberStyle == ListNumberStyle.Arabic)
                    {
                        // 数字
                        if (currentLevel == 1)
                        {
                            builder.ListFormat.ListIndent();
                        }

                        for (int i = 2; i < currentLevel; i++)
                        {
                            builder.ListFormat.ListOutdent();
                        }

                        currentLevel = node.NodeLevel;
                    }

                    if (node.NumberStyle == ListNumberStyle.Bullet)
                    {
                        // 圆点
                        //if (currentLevel < 4)
                        //{
                        //    builder.ListFormat.ListIndent();
                        //}

                        for (int i = currentLevel; i < 4; i++)
                        {
                            builder.ListFormat.ListIndent();
                        }
                        
                        currentLevel = node.NodeLevel;
                    }

                    if (node.NumberStyle == ListNumberStyle.None)
                    {
                        // 破折号
                        if (currentLevel == 2)
                        {
                            builder.ListFormat.ListIndent();
                        }

                        for (int i = 3; i < currentLevel; i++)
                        {
                            builder.ListFormat.ListOutdent();
                        }

                        currentLevel = node.NodeLevel;
                    }

                    builder.Writeln(node.Text);
                }
            }

            builder.ListFormat.RemoveNumbers();
        }

        #endregion

        #region 插入一个文档到另外一个文档

        /// <summary>
        ///     插入一个文档到另外一个文档
        ///     Inserts content of the external document after the specified node.
        ///     Section breaks and section formatting of the inserted document are ignored.
        /// </summary>
        /// <param name="insertAfterNode">
        ///     Node in the destination document after which the content
        ///     should be inserted. This node should be a block level node (paragraph or table).
        /// </param>
        /// <param name="srcDoc">The document to insert.</param>
        public static void InsertDocument(Node insertAfterNode, Document srcDoc)
        {
            // Make sure that the node is either a pargraph or table.
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
                (!insertAfterNode.NodeType.Equals(NodeType.Table)))

                throw new ArgumentException("The destination node should be either a paragraph or table.");

            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = insertAfterNode.ParentNode;

            // This object will be translating styles and lists during the import.
            var importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

            // Loop through all sections in the source document.
            foreach (Section srcSection in srcDoc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node srcNode in srcSection.Body)
                {
                    // Let's skip the node if it is a last empty paragarph in a section.
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        var para = (Paragraph)srcNode;
                        if (para.IsEndOfSection && !para.HasChildNodes) continue;
                    }

                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Node newNode = importer.ImportNode(srcNode, true);

                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }
        }

        /// <summary>
        ///     插入一个文档到另外一个文档
        ///     Inserts content of the external document after the specified node.
        ///     Section breaks and section formatting of the inserted document are ignored.
        /// </summary>
        /// <param name="insertAfterNode">
        ///     Node in the destination document after which the content
        ///     should be inserted. This node should be a block level node (paragraph or table).
        /// </param>
        /// <param name="srcDoc">The document to insert.</param>
        /// <param name="builder"></param>
        /// <param name="firstSectionOrientation">第一节页面方向</param>
        /// <param name="document"></param>
        public static Orientation InsertDocument(Node insertAfterNode, Document srcDoc, Document document, DocumentBuilder builder, Orientation firstSectionOrientation)
        {
            // Make sure that the node is either a pargraph or table.
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
                (!insertAfterNode.NodeType.Equals(NodeType.Table)))

                throw new ArgumentException("The destination node should be either a paragraph or table.");

            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = insertAfterNode.ParentNode;

            // This object will be translating styles and lists during the import.
            //var importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.UseDestinationStyles);
            var importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

            var addBreak = false; // 添加换页符号标识
            var sectionPageOrientation = Orientation.Portrait;

            // Loop through all sections in the source document.
            for (int i = 0; i < srcDoc.Sections.Count; i++)
            {
                var srcSection = srcDoc.Sections[i];

                #region 节纸张属性设置

                // 节纸张方向
                sectionPageOrientation = srcSection.PageSetup.Orientation;

                if (srcSection.PageSetup.PaperSize != PaperSize.A4)
                {
                    srcSection.PageSetup.PaperSize = PaperSize.A4;
                }

                srcSection.PageSetup.RestartPageNumbering = false;

                srcSection.PageSetup.HeaderDistance = 70.85;
                srcSection.PageSetup.FooterDistance = 56.7;

                #endregion

                if (sectionPageOrientation == firstSectionOrientation)
                {
                    #region 添加换页符号

                    if (i > 0 && addBreak)
                    {
                        builder.MoveToDocumentEnd();
                        builder.InsertBreak(BreakType.PageBreak);
                    }

                    #endregion
                }
                else
                {
                    #region 插入新节，并根据插入内容纸张情况变更纸张方向

                    var newSection = new Section(document);
                    SetSectionPage(newSection, sectionPageOrientation);
                    document.Sections.Add(newSection);

                    // 光标移动到新添加的节中
                    builder.MoveToSection(document.Sections.Count - 1);

                    insertAfterNode = builder.CurrentParagraph;
                    dstStory = insertAfterNode.ParentNode;
                    importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

                    #endregion
                }

                #region 插入段落内容

                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node srcNode in srcSection.Body)
                {
                    addBreak = false;

                    #region 换页处理 (仅当节中最后一个段落中包含换页符才有效)

                    // Let's skip the node if it is a last empty paragarph in a section.
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        var para = (Paragraph)srcNode;
                        //if (para.IsEndOfSection && !para.HasChildNodes) continue;

                        if (para.IsEndOfSection)
                        {
                            if (srcNode.Range.Text.Contains(ControlChar.PageBreak) ||
                                srcNode.Range.Text.Contains(ControlChar.SectionBreak))
                            {
                                if (sectionPageOrientation == firstSectionOrientation && i < srcDoc.Sections.Count - 1)
                                {
                                    addBreak = true;
                                }
                            }
                        }
                    }

                    #endregion

                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Node newNode = importer.ImportNode(srcNode, true);

                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }

                #endregion

                firstSectionOrientation = sectionPageOrientation;
            }

            return sectionPageOrientation;
        }

        #endregion

        #region 设置节页面属性

        /// <summary>
        /// 设置节页面属性
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="pageOrientation">页面方向</param>
        public static void SetSectionPage(Section section, Orientation pageOrientation)
        {
            //var pageOrientation = isLandscape ? Orientation.Landscape : Orientation.Portrait;

            section.PageSetup.PaperSize = PaperSize.A4;
            section.PageSetup.Orientation = pageOrientation;
            section.PageSetup.SectionStart = SectionStart.Continuous;
            section.PageSetup.RestartPageNumbering = false;

            if (pageOrientation == Orientation.Landscape)
            {
                // 横向(Orientation.Landscape)
                section.PageSetup.TopMargin = 70.85;
                section.PageSetup.BottomMargin = 56.7;
                section.PageSetup.LeftMargin = 56.7;
                section.PageSetup.RightMargin = 56.7;
                section.PageSetup.HeaderDistance = 70.85;
                section.PageSetup.FooterDistance = 56.7;
            }
            else
            {
                // 纵向(Orientation.Portrait)
                section.PageSetup.TopMargin = 28.35;
                section.PageSetup.BottomMargin = 56.7;
                section.PageSetup.LeftMargin = 70.85;
                section.PageSetup.RightMargin = 56.7;
                section.PageSetup.HeaderDistance = 70.85;
                section.PageSetup.FooterDistance = 56.7;
            }
        }

        #endregion
    }


    #region 文档列项数据实体

    /// <summary>
    ///     文档列项数据实体
    /// </summary>
    public class DocListNode
    {
        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     是否列表项(true/false)
        /// </summary>
        public bool IsListItem { get; set; }

        /// <summary>
        ///     节点级别(如果IsListItem=false，则NodeLevel=0)
        /// </summary>
        public int NodeLevel { get; set; }

        /// <summary>
        ///     NumberStyle
        /// </summary>
        public string NumberStyle { get; set; }

        /// <summary>
        ///     NumberFormat
        /// </summary>
        public string NumberFormat { get; set; }

        /// <summary>
        ///     文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     子节点数
        /// </summary>
        //public int ChildNodeCount { get; set; }

        /// <summary>
        ///     子节点
        /// </summary>
        //public List<DocListNode> Children { get; set; }
    }

    #endregion

    #region 文档编号样式

    /// <summary>
    ///     文档编号样式
    /// </summary>
    public sealed class ListNumberStyle
    {
        /// <summary>
        ///     无
        /// </summary>
        public const string None = "None";

        /// <summary>
        ///     小写字母
        /// </summary>
        public const string LowercaseLetter = "LowercaseLetter";

        /// <summary>
        ///     数字
        /// </summary>
        public const string Arabic = "Arabic";

        /// <summary>
        ///     圆点
        /// </summary>
        public const string Bullet = "Bullet";
    }

    #endregion

    #region 文档编号格式

    /// <summary>
    ///     文档编号格式
    /// </summary>
    public sealed class ListNumberFormat
    {
        /// <summary>
        ///     空
        /// </summary>
        public const string Blank = "  ";

        /// <summary>
        ///     右括号
        /// </summary>
        public const string Bracket = " )";

        /// <summary>
        ///     破折号
        /// </summary>
        public const string Dash = "——";
    }

    #endregion
}
