using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.Util;
using System.Xml;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Json;
using Hsp.Test.Model.Media;
using Hsp.Test.Service;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// IconsHandler 的摘要说明
    /// </summary>
    public class IconsHandler : IHttpHandler, IRequiresSessionState
    {

        /// <summary>
        ///     图像服务
        /// </summary>
        internal readonly IImageService ImageService = new ImageService();

        const string ImagesPath = "Images\\";

        #region ProcessRequest

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetNoStore();

            var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];
            if (string.IsNullOrEmpty(strOperation)) strOperation = context.Request.Form["OPERATION"];

            switch (strOperation.ToUpper())
            {
                // 获取图标信息
                case "ICONDATA":
                    IconZTreeData(context);
                    break;

                // 获取路径图标
                case "PATHICONS":
                    GetIconsByPath(context);
                    break;

                // 样式配置文件XML数据获取
                case "GETXMLFILE":
                    GetXmlFile(context);
                    break;

                // 样式配置文件XML数据保存
                case "SAVEXMLFILE":
                    SaveXmlFile(context);
                    break;

                // 获取样式配置数据
                case "GETCSSSETTINGS":
                    GetCssSettings(context);
                    break;

                // 保存样式配置数据
                case "SAVECSSSETTINGS":
                    SaveCssSettings(context);
                    break;

                // 样式处理
                case "CSSPROCESS":
                    CssProcess(context);
                    break;

                // BootStrap 图标处理
                case "GLYPHICONS":
                    GlyphiconsProcess(context);
                    break;


                default:

                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 图标目录ZTree数据

        /// <summary>
        ///     图标目录ZTree数据(分层异步获取)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void IconZTreeData(HttpContext context)
        {
            var rst = "";
            var js = "[]";

            #region 图标目录检查

            var defaultIconPath = HttpContext.Current.Server.MapPath("/") + ImagesPath;

            if (!Directory.Exists(defaultIconPath))
            {
                //return Json(new { IsSuccess = false, Message = "图标目录不存在！" }, JsonRequestBehavior.AllowGet);
            }

            #endregion

            if (!string.IsNullOrEmpty(context.Request.Params["id"]))
            {
                defaultIconPath = context.Request.Params["id"];
            }

            //id = Request.Form["id"];
            //var name = Request.Form["name"];
            //var level = Request.Form["level"];
            //if (string.IsNullOrEmpty(id)) id = "0";

            var list = new List<zTreeModel>();
            if (defaultIconPath == null)
            {
                //return Json(new { IsSuccess = true, list }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var di = new DirectoryInfo(defaultIconPath);

                //遍历文件夹
                foreach (var folder in di.GetDirectories())
                {
                    var model = new zTreeModel();
                    model.id = folder.FullName;
                    model.name = folder.Name;
                    model.pId = defaultIconPath;
                    model.isParent = true;
                    list.Add(model);
                }
                if (list.Count > 0)
                {
                    js = new JavaScriptSerializer().Serialize(list);
                }

                //return Json(list, JsonRequestBehavior.AllowGet);
                //rst = "{\"IsSuccess\":true,\"Message\": \"附件批量下载成功！\", \"Url\":\"" + "" + "\"}";
            }
            catch (Exception ex)
            {
                //return Json(new { IsSuccess = false, ex.Message }, JsonRequestBehavior.AllowGet);
                //rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
            }

            //context.Response.Write(rst);
            //var list = null; //   AttachmentService.GetFileList(paramList);

            if (js.Length > 0) rst = js;
            context.Response.Write(rst);
        }

        /// <summary>
        ///     根据路径获取图标
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void GetIconsByPath(HttpContext context)
        {
            var rst = "";
            var path = context.Request.Params["path"];

            #region 图标目录检查

            var rootPath = HttpContext.Current.Server.MapPath("/");

            var defaultIconPath = HttpContext.Current.Server.MapPath("/") + ImagesPath;

            if (!string.IsNullOrEmpty(path))
            {
                defaultIconPath = path;
            }

            #endregion

            var list = new List<Hsp.Test.Model.Media.ImageAttribute>();
            try
            {
                var di = new DirectoryInfo(defaultIconPath);

                //遍历文件
                foreach (var file in di.GetFiles())
                {
                    if (!IsImage(file.Extension)) continue;

                    var img = Image.FromFile(file.FullName);
                    var width = img.Width;
                    var height = img.Height;
                    if (width < 12 || height < 12 || height > 256 || width > 256 || (height*1.0/width*1.0 > 1.6) ||
                        (width*1.0/height*1.0 > 1.6)) continue;
                    var model = new ImageAttribute();
                    model.FullName = "\\" + file.FullName.Replace(rootPath, "");
                    model.Name = file.Name;
                    model.Size = file.Length;
                    model.Extension = file.Extension;
                    model.DirectoryName = file.DirectoryName;
                    model.CreationTime = file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");

                    model.Width = img.Width;
                    model.Height = img.Height;

                    list.Add(model);
                }

                if (list.Count > 0)
                {
                    rst = new JavaScriptSerializer().Serialize(list);
                }

                //return Json(new { IsSuccess = true, Data = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //return Json(new { IsSuccess = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }

            //if (js.Length > 0) rst = js;
            context.Response.Write(rst);
        }

        /// <summary>
        ///     是否图标文件
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public bool IsImage(string ext)
        {
            ext = ext.Trim('.').ToLower();
            var strExt = "jpg,png,gif,ico";
            return strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
        }

        #endregion

        #region XML数据处理

        /// <summary>
        /// 样式配置文件XML数据获取
        /// </summary>
        /// <param name="context"></param>
        private static void GetXmlFile(HttpContext context)
        {
            var rst = "";
            try
            {
                var xmlPath = System.Web.HttpContext.Current.Server.MapPath("~/Styles/Xml/CssFiles.xml");


                rst = "{\"IsSuccess\":true,\"Message\": \"样式配置文件XML数据获取成功！\", \"Url\":\"" + "" + "\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);
        }

        /// <summary>
        /// 样式配置文件XML数据保存
        /// </summary>
        /// <param name="context"></param>
        private static void SaveXmlFile(HttpContext context)
        {
            var rst = "";
            try
            {
                rst = "{\"IsSuccess\":true,\"Message\": \"样式配置文件XML数据保存成功！\", \"Url\":\"" + "" + "\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);
        }

        /// <summary>
        /// 获取样式配置数据
        /// </summary>
        /// <param name="context"></param>
        private static void GetCssSettings(HttpContext context)
        {
            var rst = "";
            try
            {
                var name = context.Request.Params["name"];
                var type = context.Request.Params["type"]; // 0:json/1:xml/2:str/
                var xmlPath = System.Web.HttpContext.Current.Server.MapPath("~/Styles/Xml/" + name + ".xml");
                if (File.Exists(xmlPath))
                {
                    //直接读取出字符串
                    var xml = System.IO.File.ReadAllText(xmlPath);
                    if (type == "0")
                    {
                        var cssSettings = XmlUtil.Deserialize(typeof (CssSettings), xml) as CssSettings;
                        rst = new JavaScriptSerializer().Serialize(cssSettings);
                    }
                    else
                    {
                        //rst = xml;

                        rst = "{\"IsSuccess\":true,\"Xml\": \"" + HttpContext.Current.Server.UrlEncode(xml) + "\"}";
                    }
                }

                //rst = "{\"IsSuccess\":true,\"Message\": \"获取样式配置数据成功！\", \"Url\":\"" + "" + "\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);
        }

        /// <summary>
        /// 保存样式配置数据
        /// </summary>
        /// <param name="context"></param>
        private static void SaveCssSettings(HttpContext context)
        {
            var rst = "";
            try
            {
                //{ "Direction": imgDirection, 
                //"Left": txtLeft, 
                //"Top": txtTop, 
                //"ImgType": selImgType, 
                //"FileName": txtFileName,
                //"Width": txtImgWidth, 
                //"Height": txtImgHeight, "Other":""};

                var data = context.Request.Params["Data"];
                var cssSettings = JsonConvert.DeserializeObject<CssSettings>(data);

                //{"direction":"0","left":"23","top":"44","imgtype":"jpg","filename":"filetype-icons","filedesc":"文件类型图标","width":"16","height":"16","files":[{"name":"AddFile_16x16.png","alias":"addfile_16x16","path":"http://localhost:8926/Images/AddFile_16x16.png","width":"16","height":"16"},{"name":"download.png","alias":"download","path":"http://localhost:8926/Images/download.png","width":"16","height":"16"}]}

                string xml = XmlUtil.Serializer(typeof(CssSettings), cssSettings);
                var xmlPath = System.Web.HttpContext.Current.Server.MapPath("~/Styles/Xml/filetype-icons.xml");
                File.WriteAllText(xmlPath, xml);

                //var nodePath = string.Format("/root");

                //XmlDocument xmlDoc = new XmlDocument();
                //XmlReaderSettings settings = new XmlReaderSettings();
                //settings.IgnoreComments = true; //忽略文档里面的注释

                //xmlDoc.Load(xmlPath);
                ////XmlNode xn = doc.SelectSingleNode(node);
                ////if (xn != null) if (xn.Attributes != null) value = xn.Attributes["value"].Value;
                //XmlElement xe = xmlDoc.DocumentElement; // DocumentElement 获取xml文档对象的根XmlElement
                ////string strPath = string.Format("/bookstore/book[@ISBN=\"{0}\"]", dgvBookInfo.CurrentRow.Cells[1].Value.ToString());
                //if (xe != null)
                //{
                //    XmlElement selectXe = (XmlElement)xe.SelectSingleNode(nodePath);
                //    //selectSingleNode 根据XPath表达式,获得符合条件的第一个节点.
                //    //if (selectXe != null) value = selectXe.GetAttribute("value").ToString();
                //}

                //, \"Direction\":\"" + strDirection + "\", \"Left\":\"" + strLeft + "\", \"Top\":\"" + strTop + "\"

                //3. DataTable转换到Xml
                //// 生成DataTable对象用于测试
                //DataTable dt1 = new DataTable("mytable");   // 必须指明DataTable名称

                //dt1.Columns.Add("Dosage", typeof(int));
                //dt1.Columns.Add("Drug", typeof(string));
                //dt1.Columns.Add("Patient", typeof(string));
                //dt1.Columns.Add("Date", typeof(DateTime));

                //// 添加行
                //dt1.Rows.Add(25, "Indocin", "David", DateTime.Now);
                //dt1.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
                //dt1.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
                //dt1.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
                //dt1.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

                //// 序列化
                //xml = XmlUtil.Serializer(typeof(DataTable), dt1);
                //Console.Write(xml);

                ////4. Xml转换到DataTable
                //// 反序列化
                //DataTable dt2 = XmlUtil.Deserialize(typeof(DataTable), xml) as DataTable;

                //// 输出测试结果
                //foreach (DataRow dr in dt2.Rows)
                //{
                //    foreach (DataColumn col in dt2.Columns)
                //    {
                //        Console.Write(dr[col].ToString() + " ");
                //    }
                //    Console.Write("\r\n");
                //}

                ////5. List转换到Xml
                //// 生成List对象用于测试
                //List<Student> list1 = new List<Student>(3);

                //list1.Add(new Student() { Name = "okbase", Age = 10 });
                //list1.Add(new Student() { Name = "csdn", Age = 15 });
                //// 序列化
                //xml = XmlUtil.Serializer(typeof(List<Student>), list1);
                //Console.Write(xml);

                //6. Xml转换到List
                //List<Student> list2 = XmlUtil.Deserialize(typeof(List<Student>), xml) as List<Student>;
                // foreach (Student stu in list2)
                // {
                //     Console.WriteLine(stu.Name + "," + stu.Age.ToString());
                // }


                rst = "{\"IsSuccess\":true,\"Message\": \"保存样式配置数据成功！\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);
        }

        #endregion

        #region 样式处理

        /// <summary>
        /// 样式处理
        /// </summary>
        /// <param name="context"></param>
        private static void CssProcess(HttpContext context)
        {
            var rst = "";
            try
            {
                var name = context.Request.Params["name"];
                var xmlPath = HttpContext.Current.Server.MapPath("~/Styles/Xml/" + name + ".xml");
                if (File.Exists(xmlPath))
                {
                    //直接读取出XML字符串
                    var xml = System.IO.File.ReadAllText(xmlPath);
                    var cssSettings = XmlUtil.Deserialize(typeof(CssSettings), xml) as CssSettings;
                    if (cssSettings != null)
                    {
                        #region 写入样式合成图片文件

                        var type = cssSettings.imgtype;
                        ImageFormat imgType;

                        switch (type)
                        {
                            case "jpg":
                                imgType = ImageFormat.Jpeg;
                                break;
                            case "png":
                                imgType = ImageFormat.Png;
                                break;
                            case "gif":
                                imgType = ImageFormat.Gif;
                                break;
                            default:
                                imgType = ImageFormat.Jpeg;
                                break;
                        }

                        var combImgPath = "/Styles/Xml/" + name + "." + type;
                        var cssImgPath = HttpContext.Current.Server.MapPath(combImgPath);

                        if (File.Exists(cssImgPath))
                        {
                            // 如果合成图片存在则需要先删除
                            File.Delete(cssImgPath);
                        }


                        var totalWeight = 0;
                        var totalHeight = 0;
                        foreach (var file in cssSettings.files)
                        {
                            totalWeight += file.width;
                            totalHeight += file.height;
                        }

                        Bitmap bmp = cssSettings.direction == 0
                            ? new Bitmap(cssSettings.width, totalHeight)
                            : new Bitmap(totalWeight, cssSettings.height);

                        var g = Graphics.FromImage(bmp);
                        g.SmoothingMode = SmoothingMode.AntiAlias; //呈现质量
                        g.Clear(Color.FromArgb(255, 255, 255)); //背景色
                        //g.DrawRectangle(new Pen(Color.FromArgb(227, 227, 227), 1), 1, 1, cssSettings.width - 2, totalHeight - 2);//灰色边框

                        var width = 0;
                        var height = 0;
                        foreach (var file in cssSettings.files)
                        {
                            var imgPath = HttpContext.Current.Server.MapPath(file.path);
                            if (File.Exists(imgPath))
                            {
                                Image img = null;
                                if (cssSettings.direction == 0)
                                {
                                    img = Image.FromFile(imgPath);
                                    //var img = new Bitmap(file.width, file.height);
                                    g.DrawImage(img, new Point(0, height));
                                    img.Dispose();
                                    height += file.height;
                                }
                                else
                                {
                                    img = Image.FromFile(imgPath);
                                    g.DrawImage(img, new Point(width, 0));
                                    img.Dispose();
                                    width += file.width;
                                }
                            }
                            else
                            {

                            }
                        }

                        g.Dispose();

                        //g.DrawImage(bmp1, 0, 0);
                        //g.DrawImage(bmp2, 0, bmp1.Height);
                        //g.Save(bmp);

                        bmp.Save(cssImgPath, imgType);
                        bmp.Dispose();

                        #endregion

                        // 写CSS

                        #region 写入CSS文件

                        var cssFilePath = HttpContext.Current.Server.MapPath("~/Styles/Xml/" + name + ".css");

                        if (File.Exists(cssFilePath))
                        {
                            // 如果样式文件存在则需要先删除
                            File.Delete(cssFilePath);
                        }

                        using (StreamWriter file = new StreamWriter(cssFilePath, true))
                        {
                            var cssLine = "." + name + "{ background: url('" + combImgPath + "'); }";
                            file.WriteLine(cssLine); // 直接追加文件末尾，换行 

                            width = 0;
                            height = 0;
                            foreach (var img in cssSettings.files)
                            {
                                if (cssSettings.direction == 0)
                                {
                                    cssLine = "." + img.alias + " { background-position:  -0px -" + height + "px; }";
                                    height += img.height;
                                }
                                else
                                {
                                    cssLine = "." + img.alias + " { background-position:  -" + width + "px -0px; }";
                                    width += img.width;
                                }
                                file.WriteLine(cssLine);
                            }
                        }

                        #endregion

                    }
                }


                rst = "{\"IsSuccess\":true,\"Message\": \"图片保存成功！\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);

            //代码说明：
            //根据参数进行横向或纵向合并图片
            //如果为横向，图片高度为最高的那张；如果纵向则宽度为最宽的那张
            //const string folderPath = "C:\\Users\\Public\\Pictures\\Sample Pictures";
            //var images = new DirectoryInfo(folderPath).GetFiles("*.jpg", SearchOption.TopDirectoryOnly);

            //CombineImages(images, "C:/FinalImage_H.tiff");
            //CombineImages(images, "C:/FinalImage_V.tiff", ImageMergeOrientation.Vertical); 

        }

        private enum ImageMergeOrientation
        {
            Horizontal,
            Vertical
        }

        private static void CombineImages(FileInfo[] files, string toPath, ImageMergeOrientation mergeType = ImageMergeOrientation.Vertical)
        {
            //change the location to store the final image.
            var finalImage = toPath;
            var imgs = files.Select(f => Image.FromFile(f.FullName));

            var finalWidth = mergeType == ImageMergeOrientation.Horizontal ?
                imgs.Sum(img => img.Width) :
                imgs.Max(img => img.Width);

            var finalHeight = mergeType == ImageMergeOrientation.Vertical ?
                imgs.Sum(img => img.Height) :
                imgs.Max(img => img.Height);

            var finalImg = new Bitmap(finalWidth, finalHeight);
            Graphics g = Graphics.FromImage(finalImg);
            g.Clear(SystemColors.AppWorkspace);

            var width = finalWidth;
            var height = finalHeight;
            var nIndex = 0;
            foreach (FileInfo file in files)
            {
                Image img = Image.FromFile(file.FullName);
                if (nIndex == 0)
                {
                    g.DrawImage(img, new Point(0, 0));
                    nIndex++;
                    width = img.Width;
                    height = img.Height;
                }
                else
                {
                    switch (mergeType)
                    {
                        case ImageMergeOrientation.Horizontal:
                            g.DrawImage(img, new Point(width, 0));
                            width += img.Width;
                            break;
                        case ImageMergeOrientation.Vertical:
                            g.DrawImage(img, new Point(0, height));
                            height += img.Height;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("mergeType");
                    }
                }
                img.Dispose();
            }
            g.Dispose();
            finalImg.Save(finalImage, System.Drawing.Imaging.ImageFormat.Tiff);
            finalImg.Dispose();
        }

        #endregion


                //// BootStrap 图标处理
                //case "GLYPHICONS":
        //    GlyphiconsProcess(context);


        #region BootStrap 图标处理

        /// <summary>
        /// BootStrap 图标处理
        /// </summary>
        /// <param name="context"></param>
        private void GlyphiconsProcess(HttpContext context)
        {
            var rst = "";
            try
            {
                var type = context.Request.Params["type"] ?? "0";
                var icons = context.Request.Params["icons"] ?? "";
                if (type.Length == 0) type = "0";
                if (icons.Length > 0) icons = icons.Trim().TrimEnd(',');

                var i = ImageService.AddGlyphicons(int.Parse(type), icons);

                rst = "{\"success\":true,\"Message\": \"图标处理成功！\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"success\":false,\"Message\": \"" + ex.Message.Replace('"', '\"') + "\"}";
            }

            context.Response.Write(rst);
        }

    #endregion








    }
}