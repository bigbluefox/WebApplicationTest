<%@ WebHandler Language="C#" Class="MediaHandler" %>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Hsp.Test.Common;
using Hsp.Test.IService;
using Hsp.Test.Model.Media;
using Hsp.Test.Service;
//using Shell32;
using MimeMapping = Hsp.Test.Common.MimeMapping;

/// <summary>
///   媒体处理程序
/// </summary>
public class MediaHandler : IHttpHandler, IRequiresSessionState
{
    /// <summary>
    ///   媒体服务
    /// </summary>
    internal readonly IMediaService MediaService = new MediaService();

    /// <summary>
    ///   视频服务
    /// </summary>
    internal readonly IVideoService VideoService = new VideoService();

    /// <summary>
    ///   音频服务
    /// </summary>
    internal readonly IAudioService AudioService = new AudioService();

    /// <summary>
    ///   图像服务
    /// </summary>
    internal readonly IPictureService PictureService = new PictureService();

    /// <summary>
    ///   图书服务
    /// </summary>
    internal readonly IBookService BookService = new BookService();

    /// <summary>
    ///   媒体总数量
    /// </summary>
    internal int Total;

    /// <summary>
    ///   媒体输出数量
    /// </summary>
    internal int SubTotal;

    #region ProcessRequest

    /// <summary>
    ///   ProcessRequest
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        context.Response.ContentType = "application/json";
        context.Response.Cache.SetNoStore();
        var strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            // 媒体信息检索
            case "RETRIEVAL":
                MediaRetrieval(context);
                break;

            // 获取媒体信息检索结果数量
            case "COUNT":
                GetCount(context);
                break;

            // 清空数据表
            case "EMPTYING":
                EmptyingTable(context);
                break;

            // 媒体列表信息
            case "LIST":
                GetMediaList(context);
                break;

            // 单个删除媒体
            case "DELETE":
                DeleteMedia(context);
                break;

            // 批量删除媒体
            case "BATCHDELETE":
                BatchDelete(context);
                break;

            // 媒体列表信息
            case "PAGELIST":
                GetPageList(context);
                break;

            // 媒体归档操作
            case "ARCHIVING":
                Archiving(context);
                break;

            // 媒体默认目录
            case "DEFAULTPATH":
                DefaultPath(context);
                break;

            // 媒空目录清理
            case "EMPTYDIR":
                EmptyDir(context);
                break;

            // 目录检索
            case "DIRECTORY":
                DirectoryRetrieve(context);
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

    #region 媒体信息检索

    /// <summary>
    ///   媒体信息检索
    /// </summary>
    /// <param name="context"></param>
    private void MediaRetrieval(HttpContext context)
    {
        var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s; // 1235

        var rst = "";

        try
        {

            Total = 0; SubTotal = 0;
            var type = context.Request.Params["type"] ?? "0";
            var strSource = context.Request.Params["dir"];
            var value = int.Parse(context.Request.Params["value"] ?? "0");

            var list = new List<Medias>();
            var di = new DirectoryInfo(strSource);

            FindMediaFile(di, ref list, type, value); // 媒体文件查找
            if (list.Count > 0)
            {
                var i = MediaService.AddMedias(list);
            }

            rst = "{\"success\":true,\"Message\":\"" + "共检索到：" + Total + "个媒体文件。" + "\"}";

        }
        catch (Exception ex)
        {
            rst = "{\"success\":false, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }


    /// <summary>
    ///   递归媒体文件查找
    /// </summary>
    /// <param name="di">目录</param>
    /// <param name="list">媒体列表</param>
    /// <param name="type">媒体类型</param>
    /// <param name="value">哈希选择结果值</param>
    public void FindMediaFile(DirectoryInfo di, ref List<Medias> list, string type, int value)
    {
        try
        {
            foreach (var file in di.GetFiles())
            {
                if (file.Extension.Length < 2 || file.Extension.Length > 8) continue;
                if (!IsMediaFile(type, file.Extension)) continue;

                var model = new Medias();

                #region 检查文件大小

                int width = 0, height = 0;

                //类型：0-其他；1-图片；2-音频；3-视频；4-图书；5-大文件

                #region 图片处理

                if (type == "1")
                {
                    try
                    {
                        //var image = Image.FromFile(file.FullName);
                        //width = image.Width;
                        //height = image.Height;
                        if (height < 128 || width < 128) continue;

                        //bytes = ImageToBytes(image);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        //Dispose(); // 防止内存溢出
                    }
                }

                #endregion

                #region 音频处理

                if (type == "2")
                {
                    try
                    {

                        #region 获取音乐文件信息
                        string mp3Info = "";

                        if (file.Extension.ToLower() == ".mp3")
                        {
                            //ShellClass sh = new ShellClass();
                            //Folder dir = sh.NameSpace(Path.GetDirectoryName(file.FullName));
                            //FolderItem item = dir.ParseName(Path.GetFileName(file.FullName));

                            //var artist = dir.GetDetailsOf(item, 13);
                            //var album = dir.GetDetailsOf(item, 14);
                            //var duration = dir.GetDetailsOf(item, 27);

                            //mp3Info += "文件名：" + dir.GetDetailsOf(item, 0) + " * ";
                            //mp3Info += "文件大小：" + dir.GetDetailsOf(item, 1) + " * ";
                            //mp3Info += "歌曲名：" + dir.GetDetailsOf(item, 21) + " * ";
                            //mp3Info += "歌手：" + dir.GetDetailsOf(item, 13) + " * ";
                            //mp3Info += "专辑：" + dir.GetDetailsOf(item, 14) + " * ";
                            //mp3Info += "时长：" + dir.GetDetailsOf(item, 27);

                            //model.Album = artist;
                            //model.Artist = album;
                            //model.SHA1 = duration;
                            //model.Artist = mp3Info.Length > 255 ? mp3Info.Substring(0, 254) : mp3Info;
                        }

                        #endregion

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        //Dispose(); // 防止内存溢出
                    }
                }

                #endregion

                #region 视频处理

                if (type == "3")
                {
                    try
                    {
                        if (file.Length < 1048576*2) continue; // 2M以下小视频文件不予统计
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        //Dispose(); // 防止内存溢出
                    }
                }

                #endregion

                #endregion

                model.Type = int.Parse(type);
                model.Name = DateTime.Now.ToFileTime().ToString();
                model.Title = file.Name.Replace(file.Extension, "").Replace("'", "''");
                model.Size = file.Length;
                model.Extension = file.Extension.ToLower();
                model.ContentType = MimeMapping.GetMimeMapping(file.FullName);
                model.FullName = file.FullName.Replace("'", "''");
                if (file.DirectoryName != null) model.DirectoryName = file.DirectoryName.Replace("'", "''");
                model.CreationTime = file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");

                //SELECT Id, Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension
                //    , ContentType, FullName, DirectoryName, CreationTime, MD5, SHA1
                //FROM Medias

                model.Width = width;
                model.Height = height;

                model.MD5 = value == 1 || value == 3 ? HashHelper.ComputeMD5(file.FullName) : "";
                //model.SHA1 = value == 2 || value == 3 ? HashHelper.ComputeSHA1(file.FullName) : "";

                list.Add(model);
                Total++;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            //Dispose();
        }

        if (list.Count > 99)
        {
            var rst = MediaService.AddMedias(list);
            list = new List<Medias>();
        }

        try
        {
            var dir = di.GetDirectories();
            foreach (var d in dir)
            {
                FindMediaFile(d, ref list, type, value);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            //Dispose();
        }
    }

    #region Convert Image to Byte

    /// <summary>
    ///   Convert Image to Byte[]
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static byte[] ImageToBytes(Image image)
    {
        var format = image.RawFormat;
        using (var ms = new MemoryStream())
        {
            if (format.Equals(ImageFormat.Jpeg))
            {
                image.Save(ms, ImageFormat.Jpeg);
            }
            else if (format.Equals(ImageFormat.Png))
            {
                image.Save(ms, ImageFormat.Png);
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                image.Save(ms, ImageFormat.Bmp);
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                image.Save(ms, ImageFormat.Gif);
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                image.Save(ms, ImageFormat.Icon);
            }
            var buffer = new byte[ms.Length];
            //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }

    #endregion

    #region 是否媒体文件

    /// <summary>
    ///   是否媒体文件
    /// </summary>
    /// <param name="type">媒体类型</param>
    /// <param name="ext">扩展名</param>
    /// <returns></returns>
    public bool IsMediaFile(string type, string ext)
    {
        ext = ext.Trim('.').ToLower();
        var strExt = "*";
        var isMediaFile = false;
        // 类型：0-其他；1-图片；2-音频；3-视频;4-阅读；5-大文件
        switch (type)
        {
            // 图片
            case "1":
                strExt = "jpg,png,jpeg,gif,icon,bmp,tiff,tga,exif,svg,psd,eps,ai.raw,wmf";
                isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                break;


            // 音频
            case "2":
                strExt = "mp3,flac,ape,mid,wav,aac,wma,ogg,cda,asf,rm,vof,m4a";
                isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                break;

            // 视频
            case "3":
                strExt = "mp4,mkv,avi,wmv,mpg,mpeg,mov,rm,rmvb,flv,f4v,m4v,3gp,dat,ts,mts,vob";
                isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                break;

            // 阅读
            case "4":
                strExt = "pdf,doc,docx,ppt,pptx,epub,chm,txt";
                isMediaFile = strExt.IndexOf(ext, StringComparison.Ordinal) > -1;
                break;

            default:
                // 其他
                isMediaFile = true;
                break;
        }

        return isMediaFile;
    }

    #endregion

    #region 获取文件名称

    /// <summary>
    ///   获取文件名称
    /// </summary>
    /// <returns></returns>
    public string GetFileName()
    {
        return DateTime.Now.ToFileTime().ToString();
    }

    #endregion

    #endregion

    #region 获取媒体数量

    /// <summary>
    /// 获取媒体数量
    /// </summary>
    /// <param name="context"></param>
    private void GetCount(HttpContext context)
    {
        var rst = "";

        try
        {
            var count = MediaService.MediaCount();
            rst = "{\"success\":true,\"Count\":" + count + "}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 清空媒体数据

    /// <summary>
    /// 清空媒体数据
    /// </summary>
    /// <param name="context"></param>
    private void EmptyingTable(HttpContext context)
    {
        var rst = "";

        try
        {
            var name = context.Request.Params["name"];
            var count = MediaService.EmptyingTable(name);
            rst = "{\"success\":true,\"Message\":\"表" + name + "数据已经清空！\"}";
        }
        catch (Exception ex)
        {
            rst = "{\"success\":true, \"Message\":\"" + ex.Message.Replace('"', '\"') + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 获取媒体列表信息

    /// <summary>
    ///     获取媒体列表信息
    /// </summary>
    /// <param name="context"></param>
    private void GetMediaList(HttpContext context)
    {
        var strTitle = context.Request.Params["Title"] ?? "";
        var strType = context.Request.Params["Type[]"] ?? "";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim();

        //if (string.IsNullOrWhiteSpace(strTitle) && string.IsNullOrWhiteSpace(strType))
        //{
        //    return;
        //}

        var pageSize = context.Request.Params["pageSize"] ?? "10";
        var pageNumber = context.Request.Params["pageNumber"] ?? "1";

        if (string.IsNullOrEmpty(pageSize)) pageSize = "10";
        if (string.IsNullOrEmpty(pageNumber)) pageNumber = "1";

        //得到客户端传递的页码和每页记录数，并转换成int类型  
        //int pageSize = Integer.parseInt(request.getParameter("pageSize"));
        //int pageNumber = Integer.parseInt(request.getParameter("pageNumber"));
        //String orderNum = request.getParameter("orderNum");          

        var paramList = new Dictionary<string, string>
            {
                {"Title", strTitle},
                {"Type", strType},
                {"PageSize", pageSize},
                {"PageIndex", pageNumber}
            };

        var list = MediaService.GetMediaList(paramList);
        var js = new JavaScriptSerializer().Serialize(list);
        context.Response.Write(js);
    }

    #endregion

    #region 获取媒体分页列表信息

    /// <summary>
    ///     获取媒体分页列表信息
    /// </summary>
    /// <param name="context"></param>
    private void GetPageList(HttpContext context)
    {
        var strTitle = context.Request.Params["Title"] ?? "";
        var strType = context.Request.Params["Type"] ?? "";
        if (strTitle.Length > 0) strTitle = strTitle.Trim();
        if (strType.Length > 0) strType = strType.Trim().TrimEnd(',');

        var pageSize = context.Request.Params["pageSize"] ?? "10";
        var pageNumber = context.Request.Params["pageNumber"] ?? "1";

        if (string.IsNullOrEmpty(pageSize)) pageSize = "10";
        if (string.IsNullOrEmpty(pageNumber)) pageNumber = "1";

        //得到客户端传递的页码和每页记录数，并转换成int类型  
        //int pageSize = Integer.parseInt(request.getParameter("pageSize"));
        //int pageNumber = Integer.parseInt(request.getParameter("pageNumber"));
        //String orderNum = request.getParameter("orderNum");  

        ////分页查找商品销售记录，需判断是否有带查询条件  
        //List<SimsSellRecord> sellRecordList = null;
        //sellRecordList = sellRecordService.querySellRecordByPage(pageNumber, pageSize, orderNum);

        ////将商品销售记录转换成json字符串  
        //String sellRecordJson = sellRecordService.getSellRecordJson(sellRecordList);
        ////得到总记录数  
        //int total = sellRecordService.countSellRecord(orderNum);

        var paramList = new Dictionary<string, string>
            {
                {"Title", strTitle},
                {"Type", strType},
                {"PageSize", pageSize},
                {"PageIndex", pageNumber}
            };

        var list = MediaService.GetMediaList(paramList);
        var js = new JavaScriptSerializer().Serialize(list);
        var count = 0;
        if (list.Count > 0)
        {
            count = list[0].Count;
        }

        //需要返回的数据有总记录数和行数据  
        var json = "{\"total\":" + count + ",\"rows\":" + js + "}";

        context.Response.Write(json);
    }

    #endregion

    #region 删除媒体

    /// <summary>
    ///     删除媒体
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void DeleteMedia(HttpContext context)
    {
        // 媒体文件ID
        var strMediaId = context.Request.Params["ID"] ?? "0";
        if (strMediaId.Length > 0) strMediaId = strMediaId.Trim();
        var rst = "";
        var media = MediaService.GetMediaById(int.Parse(strMediaId));
        var i = MediaService.DelMediaById(int.Parse(strMediaId)); // 从数据库中删除媒体
        if (i > 0)
        {
            rst = "{\"IsSuccess\":true,\"Message\": \"媒体删除成功！\"}";

            if (string.IsNullOrEmpty(media.FullName)) return;

            try
            {
                if (File.Exists(media.FullName))
                {
                    File.Delete(media.FullName);
                }
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message.Replace('"', '\"') + "\"}";
            }
        }
        else
        {
            rst = "{\"IsSuccess\":false,\"Message\": \"媒体数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 批量删除媒体

    /// <summary>
    ///     批量删除媒体
    /// </summary>
    /// PATH
    /// <param name="context"></param>
    private void BatchDelete(HttpContext context)
    {
        // 媒体文件ID
        var strMediaIds = context.Request.Params["IDs"] ?? "";
        if (strMediaIds.Length > 0) strMediaIds = strMediaIds.Trim().TrimEnd(',');
        var rst = "";
        var mediaList = MediaService.GetMediaByIds(strMediaIds);
        var i = MediaService.DelMediaByIds(strMediaIds); // 从数据库中批量删除媒体
        if (i > 0)
        {
            var strMessage = "媒体数据批量删除成功！";

            foreach (var media in mediaList)
            {
                if (string.IsNullOrEmpty(media.FullName)) return;

                try
                {
                    if (File.Exists(media.FullName))
                    {
                        File.Delete(media.FullName);
                    }
                }
                catch (Exception ex)
                {
                    strMessage += "媒体文件" + media.Title + "批量删除失败，原因：" + ex.Message.Replace('"', '\"');
                }
            }

            rst = "{\"IsSuccess\":true,\"Message\": \"" + strMessage + "\"}";
        }
        else
        {
            rst = "{\"IsSuccess\":false,\"Message\": \"媒体数据删除失败！\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 媒体归档操作

    /// <summary>
    ///   媒体归档操作
    /// </summary>
    /// <param name="context"></param>
    private void Archiving(HttpContext context)
    {
        var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s; // 1235

        var rst = "";

        try
        {

            var type = context.Request.Params["type"] ?? "0";
            var strDir = context.Request.Params["dir"] ?? "";

            if (string.IsNullOrEmpty(type) || type == "0")
            {
                rst = "{\"success\":false,\"Message\":\"归档分类不能为空！\"}";
            }
            else
            {
                var mediaType = "";
                var paramList = new Dictionary<string, string>
                {
                    {"Dir", strDir},
                    {"Type", type}
                };

                if (type == "1")
                {
                    mediaType = "图片";
                    PictureService.ImageArchiving(paramList);
                }
                if (type == "2")
                {
                    mediaType = "音频";
                    AudioService.AudioArchiving(paramList);
                }
                if (type == "3")
                {
                    mediaType = "视频";
                    VideoService.VideoArchiving(paramList);
                }
                if (type == "4")
                {
                    mediaType = "图书";
                    BookService.BookArchiving(paramList);
                }

                //<option value="1">图片</option>
                //<option value="2">音频</option>
                //<option value="3">视频</option>
                //<option value="4">图书</option>

                rst = "{\"success\":true,\"Message\":\"" + mediaType + "文件归档成功！\"}";
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false,\"Message\":\"" + ex.Message + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

    #region 媒体默认目录

    /// <summary>
    ///   媒体默认目录
    /// </summary>
    /// <param name="context"></param>
    private void DefaultPath(HttpContext context)
    {
        var rst = "";

        try
        {
            var type = context.Request.Params["type"] ?? "0";

            if (string.IsNullOrEmpty(type) || type == "0")
            {
                rst = "{\"success\":false,\"Message\":\"媒体分类不能为空！\"}";
            }
            else
            {
                var defaultPath = "";

                switch (type)
                {
                    case "1":
                        defaultPath = ConfigurationManager.AppSettings["DefalutImage"] ?? "";
                        break;
                    case "2":
                        defaultPath = ConfigurationManager.AppSettings["DefalutAudio"] ?? "";
                        break;
                    case "3":
                        defaultPath = ConfigurationManager.AppSettings["DefalutVideo"] ?? "";
                        break;
                    case "4":
                        defaultPath = ConfigurationManager.AppSettings["DefalutBook"] ?? "";
                        break;

                    default:
                        break;
                }

                rst = "{\"success\":true,\"Message\":\"" + HttpUtility.UrlEncode(defaultPath) + "\"}";
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false,\"Message\":\"" + ex.Message + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion


    #region 空目录清理

    /// <summary>
    ///   空目录清理
    /// </summary>
    /// <param name="context"></param>
    private void EmptyDir(HttpContext context)
    {
        var rst = "";

        try
        {
            var dir = context.Request.Params["dir"] ?? "";

            if (string.IsNullOrEmpty(dir))
            {
                rst = "{\"success\":false,\"Message\":\"请填写要清理的目录名！\"}";
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var list = new List<Medias>();
                    DirectoryInfo di = new DirectoryInfo(dir);

                    FindDirectory(di, ref list); // 检索目录
                    if (list.Count > 0)
                    {
                        foreach (var model in list)
                        {
                            if (model.Width == 0 && model.Height == 0)
                            {
                                var d = new DirectoryInfo(model.FullName);
                                d.Delete();
                            }
                        }
                    }
                }

                rst = "{\"success\":true,\"Message\":\"" + HttpUtility.UrlEncode(dir) + "下空目录清理完成！\"}";
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false,\"Message\":\"" + ex.Message + "\"}";
        }

        context.Response.Write(rst);
    }

    /// <summary>
    /// 检索目录
    /// </summary>
    /// <param name="di"></param>
    /// <param name="list"></param>
    public void FindDirectory(DirectoryInfo di, ref List<Medias> list)
    {
        try
        {
            var dir = di.GetDirectories();
            foreach (var d in dir)
            {
                var model = new Medias();
                model.Name = d.Name;
                model.Title = d.Name;
                model.FullName = d.FullName;
                if (d.Parent != null) model.DirectoryName = d.Parent.FullName;
                model.CreationTime = d.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                model.Width = d.GetFiles().Length;
                model.Height = d.GetDirectories().Length;
                list.Add(model);

                FindDirectory(d, ref list);
            }
        }
        catch (Exception ex)
        {
            //throw;
        }
        finally
        {
            //Dispose();
        }
    }

    #endregion

    #region 目录检索

    /// <summary>
    ///   目录检索
    /// </summary>
    /// <param name="context"></param>
    private void DirectoryRetrieve(HttpContext context)
    {
        var rst = "";

        try
        {
            var dir = context.Request.Params["dir"] ?? "";

            if (string.IsNullOrEmpty(dir))
            {
                rst = "{\"success\":false,\"Message\":\"请填写要检索的目录名！\"}";
            }
            else
            {
                var list = new List<Medias>();
                DirectoryInfo di = new DirectoryInfo(dir);

                FindDirectory(di, ref list); // 检索目录
                if (list.Count > 0)
                {
                    MediaService.AddMedias(list);
                }

                rst = "{\"success\":true,\"Message\":\"" + HttpUtility.UrlEncode(dir) + "目录检索完成！\"}";
            }
        }
        catch (Exception ex)
        {
            rst = "{\"success\":false,\"Message\":\"" + ex.Message + "\"}";
        }

        context.Response.Write(rst);
    }

    #endregion

}