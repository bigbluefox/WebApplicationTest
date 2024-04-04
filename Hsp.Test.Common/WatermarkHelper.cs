using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = Aspose.Pdf.Document;
using SaveFormat = Aspose.Words.SaveFormat;

namespace Hsp.Test.Common
{
    /// <summary>
    /// 水印帮助类
    /// </summary>
   public class WatermarkHelper
    {
        #region word，excel，wps，et转换pdf

        /// <summary>
        /// excel导出pdf
        /// created by yuejj 20171114
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
       public static void ExportExcelToPdf(string fromPath, string toPath)
        {
            Workbook wb = new Workbook(fromPath);
            wb.Save(toPath, Aspose.Cells.SaveFormat.Pdf);
        }

        /// <summary>
        /// word导出pdf
        /// created by yuejj 20171114
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static void ExportWordToPdf(string fromPath, string toPath)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document(fromPath);
            doc.Save(toPath, SaveFormat.Pdf);
        }

        /// <summary>
        /// wps导出pdf
        /// created by yuejj 20171114
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static void ExportWpsToPdf(string fromPath, string toPath)
        {
            //WPS.ApplicationClass application = new WPS.ApplicationClass();
            //WPS.Document doc = application.Documents.Open(fromPath);
            //doc.ExportPdf(toPath);
        }

        /// <summary>
        /// et导出pdf
        /// created by yuejj 20171114
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static void ExportEtToPdf(string fromPath, string toPath)
        {
            //object type = System.Reflection.Missing.Value;
            //ET.ApplicationClass application = new ET.ApplicationClass();
            //ET.workbook book =
            //    (ET.workbook)
            //        application.Workbooks.Open(fromPath, type, type, type, type, type, type, type, type, type, type,
            //            type, type);
            //book.ExportPdf(toPath, "", ""); //this.GetFilePath(path)是获取文件路径+文件名（不含后缀）
        }

        #endregion

        #region 导出pdf并增加水印

        /// <summary>
        /// 导出pdf并增加水印 
        /// created by yuejj 20171114
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileExt">扩展名称</param>
        /// <param name="logoPath">图片路径</param>
        /// <param name="no">编号</param>
        public static void ExportPdfAndAddWaterMark(string filePath, string fileName, string fileExt, string logoPath, string no)
        {
            var canTrans = true;
            var replaceUrl = filePath.Replace(fileName + fileExt, "") + fileName;
            var outPdfUrl = replaceUrl + ".pdf";
            var outPdfWaterMarkUrl = replaceUrl + "_WaterMark.pdf";

            #region 生成PDF文件

            if (fileExt == ".doc" || fileExt == ".docx")
            {
                ExportWordToPdf(filePath, outPdfUrl);
            }
            else if (fileExt == ".wps")
            {
                ExportWpsToPdf(filePath, outPdfUrl);
            }
            else if (fileExt == ".et")
            {
                ExportEtToPdf(filePath, outPdfUrl);
            }
            else if (fileExt == ".xls" || fileExt == ".xlsx")
            {
                ExportExcelToPdf(filePath, outPdfUrl);
            }
            else
            {
                canTrans = false;
            }

            #endregion

            if (!canTrans && fileExt != ".pdf") return;

            var watermarkName = System.Configuration.ConfigurationManager.AppSettings["WatermarkName"] ?? "";
            //if (System.Configuration.ConfigurationManager.AppSettings["WatermarkName"] != null)
            //{
            //    watermarkName = System.Configuration.ConfigurationManager.AppSettings["WatermarkName"].Trim();
            //}

            if (logoPath.IsNullOrEmpty())
            {
                SetWatermark(outPdfUrl, outPdfWaterMarkUrl, watermarkName, "", "", 1);
            }
            else if (watermarkName.IsNullOrEmpty())
            {
                SetWatermark(outPdfUrl, outPdfWaterMarkUrl, logoPath, no, 0, 0);
            }
            else
            {
                SetWatermark(outPdfUrl, outPdfWaterMarkUrl, watermarkName, logoPath, no, 0, 0);
            }
            File.Delete(outPdfUrl);
        }

        #endregion

        #region iTextSharp增加水印

        /// <summary>
        /// 在pdf中增加图片和文字水印
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="watermarkName">公司简称</param>
        /// <param name="modelPicName">图片路径</param>
        /// <param name="no">编号</param>
        /// <param name="top">顶部位置</param>
        /// <param name="left">左边位置</param>
        public static void SetWatermark(string inputFilePath, string outputFilePath, string watermarkName, string modelPicName, string no,
            float top, float left)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(inputFilePath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(outputFilePath, FileMode.Create));
                int total = pdfReader.NumberOfPages + 1;

                #region 图片水印处理，右上角

                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(modelPicName);
                //image.GrayFill = 20; //透明度，灰色填充
                //image.Rotation//旋转
                //image.RotationDegrees//旋转角度

                //image.Width = image.Width / 2;
                //image.Height = image.Height/2;

                //水印的位置 
                left = width - image.Width + left - 5; // 横向
                top = height - image.Height + top - 5;

                image.SetAbsolutePosition(left, top);

                #endregion

                #region 文字水印处理，居中

                var watermarkFontPath = System.Configuration.ConfigurationManager.AppSettings["WatermarkFontPath"] ?? @"C:\WINDOWS\Fonts\STCAIYUN.TTF";
                BaseFont font = BaseFont.CreateFont(watermarkFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();
                for (int i = 1; i < total; i++)
                {
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(i);//在内容下方加水印
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.LIGHT_GRAY);
                    content.SetFontAndSize(font, 100);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_CENTER, watermarkName, width / 2 + 50, height / 2 + 0, 55);
                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();

                    content.AddImage(image); // 增加图片
                }

                #endregion

                #region 编号水印处理，左上角，只在首页

                if (!string.IsNullOrEmpty(no))
                {
                    gs = new PdfGState();
                    //for (int i = 1; i < total; i++)//{}
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(1); //在内容下方加水印
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.LIGHT_GRAY);
                    content.SetFontAndSize(font, 16);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_LEFT, no, 5, height - 16, 0);
                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
        }

        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="modelPicName"></param>
        /// <param name="no">编号</param>
        /// <param name="top">顶部位置</param>
        /// <param name="left">左边位置</param>
        /// <returns></returns>
        public static void SetWatermark(string inputFilePath, string outputFilePath, string modelPicName, string no, float top,
            float left)
        {
            //throw new NotImplementedException();
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(inputFilePath);

                int numberOfPages = pdfReader.NumberOfPages;

                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);

                #region 图片水印处理，右上角

                float width = psize.Width;
                float height = psize.Height;

                pdfStamper = new PdfStamper(pdfReader, new FileStream(outputFilePath, FileMode.Create));

                PdfContentByte content;

                Image image = Image.GetInstance(modelPicName);

                image.GrayFill = 20; //透明度，灰色填充
                //image.Rotation//旋转
                //image.RotationDegrees//旋转角度

                //水印的位置 
                left = width - image.Width + left - 5; // 横向
                top = height - image.Height + top - 5;

                image.SetAbsolutePosition(left, top);

                //每一页加水印,也可以设置某一页加水印 
                for (int i = 1; i <= numberOfPages; i++)
                {
                    content = pdfStamper.GetUnderContent(i); //内容下层加水印
                    //waterMarkContent = pdfStamper.GetOverContent(i); //内容上层加水印

                    content.AddImage(image);
                }

                #endregion

                #region 编号水印处理，左上角，只在首页

                if (!string.IsNullOrEmpty(no))
                {
                    var watermarkFontPath = System.Configuration.ConfigurationManager.AppSettings["WatermarkFontPath"] ?? @"C:\WINDOWS\Fonts\STCAIYUN.TTF";
                    BaseFont font = BaseFont.CreateFont(watermarkFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    PdfGState gs = new PdfGState();
                    //for (int i = 1; i < total; i++)//{}
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(1); //在内容下方加水印
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.LIGHT_GRAY);
                    content.SetFontAndSize(font, 20);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_LEFT, no, 5, height - 20, 0);
                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();

                    content.AddImage(image); // 增加图片
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw;

            }
            finally
            {

                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
        }

        /// <summary>
        /// 添加普通偏转角度文字水印
        /// </summary>
        /// <param name="inputfilepath"></param>
        /// <param name="outputfilepath"></param>
        /// <param name="waterMarkName"></param>
        /// <param name="no">编号</param>
        public static void SetWatermark(string inputfilepath, string outputfilepath, string waterMarkName, string no)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(inputfilepath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(outputfilepath, FileMode.Create));
                int total = pdfReader.NumberOfPages + 1;
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;

                #region 文字水印处理，居中

                var watermarkFontPath = @"C:\WINDOWS\Fonts\STCAIYUN.TTF";

                if (!File.Exists(watermarkFontPath))
                {
                    throw new Exception("彩云字体不存在！");
                }

                if (ConfigurationManager.AppSettings["WatermarkFontPath"] != null)
                {
                    watermarkFontPath =
                        ConfigurationManager.AppSettings["WatermarkFontPath"].Trim();
                }

                BaseFont font = BaseFont.CreateFont(watermarkFontPath, BaseFont.IDENTITY_H,
                    BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();
                for (int i = 1; i < total; i++)
                {
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(i); //在内容下方加水印
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.LIGHT_GRAY);
                    content.SetFontAndSize(font, 100);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, width / 2 + 50, height / 2 + 0, 55);
                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();
                }

                #endregion

                #region 编号水印处理，左上角，只在首页

                if (!string.IsNullOrEmpty(no))
                {
                    gs = new PdfGState();
                    //for (int i = 1; i < total; i++)//{}
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(1); //在内容下方加水印
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.LIGHT_GRAY);
                    content.SetFontAndSize(font, 20);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_LEFT, no, 5, height - 20, 0);
                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();

                    //content.AddImage(image); // 增加图片
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
        }

        /// <summary>
        /// 添加倾斜水印
        /// </summary>
        /// <param name="inputfilepath"></param>
        /// <param name="outputfilepath"></param>
        /// <param name="waterMarkName"></param>
        /// <param name="userPassWord"></param>
        /// <param name="ownerPassWord"></param>
        /// <param name="permission"></param>
        public static void SetWatermark(string inputfilepath, string outputfilepath, string waterMarkName,
            string userPassWord, string ownerPassWord, int permission)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(inputfilepath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(outputfilepath, FileMode.Create));
                // 设置密码   
                //pdfStamper.SetEncryption(false,userPassWord, ownerPassWord, permission); 

                int total = pdfReader.NumberOfPages + 1;
                PdfContentByte content;

                var watermarkFontPath = @"C:\WINDOWS\Fonts\STCAIYUN.TTF";
                if (System.Configuration.ConfigurationManager.AppSettings["WatermarkFontPath"] != null)
                {
                    watermarkFontPath = System.Configuration.ConfigurationManager.AppSettings["WatermarkFontPath"].Trim();
                }

                BaseFont font = BaseFont.CreateFont(watermarkFontPath, BaseFont.IDENTITY_H,
                    BaseFont.EMBEDDED);

                PdfGState gs = new PdfGState();
                gs.FillOpacity = 0.2f; //透明度

                int j = waterMarkName.Length;
                char c;
                int rise = 0;
                for (int i = 1; i < total; i++)
                {
                    rise = 500;
                    //content = pdfStamper.GetOverContent(i); //在内容上方加水印
                    content = pdfStamper.GetUnderContent(i);//在内容下方加水印

                    content.BeginText();
                    content.SetColorFill(BaseColor.DARK_GRAY);
                    content.SetFontAndSize(font, 50);
                    // 设置水印文字字体倾斜 开始 
                    if (j >= 15)
                    {
                        content.SetTextMatrix(200, 120);
                        for (int k = 0; k < j; k++)
                        {
                            content.SetTextRise(rise);
                            c = waterMarkName[k];
                            content.ShowText(c + "");
                            rise -= 20;
                        }
                    }
                    else
                    {
                        content.SetTextMatrix(180, 100);
                        for (int k = 0; k < j; k++)
                        {
                            content.SetTextRise(rise);
                            c = waterMarkName[k];
                            content.ShowText(c + "");
                            rise -= 18;
                        }
                    }
                    // 字体设置结束 
                    content.EndText();
                    // 画一个圆 
                    //content.Ellipse(250, 450, 350, 550);
                    //content.SetLineWidth(1f);
                    //content.Stroke(); 
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
        }

        #endregion
    }
}
