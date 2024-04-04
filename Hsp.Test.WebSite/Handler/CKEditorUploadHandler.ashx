<%@ WebHandler Language="C#" Class="CKEditorUploadHandler" %>

using System;
using System.Web;

public class CKEditorUploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        //context.Response.Write("Hello World");

        //context.Response.ContentType = "application/json";
        //context.Response.Cache.SetNoStore();

        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        if (uploads != null)
        {
            string file = System.IO.Path.GetFileName(uploads.FileName);
            uploads.SaveAs(context.Server.MapPath("\\CKEditor\\Images\\" + file));
            string url = "/CKEditor/Images/" + file;
            //context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
            
            //{
            //    "uploaded": 1,
            //    "fileName": "foo.jpg",
            //    "url": "/files/foo.jpg"
            //}            

            //context.Response.Write("{\"uploaded\": " + CKEditorFuncNum + ",\"fileName\": \"" + uploads.FileName + "\",\"url\": \"" + url + "\"}");

            context.Response.Write(url);
        }
        context.Response.End();   

        //var fileName = System.IO.Path.GetFileName(upload.FileName);
        //var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹

        //upload.SaveAs(filePhysicalPath);

        //var url = "/upload/" + fileName;
        
        //var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

        ////上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
        //return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}