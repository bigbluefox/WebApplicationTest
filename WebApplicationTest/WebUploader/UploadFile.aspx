<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="WebApplicationTest.WebUploader.UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>WebUploader测试</title>
    <link href="../Scripts/webuploader/webuploader.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    
    <script src="https://cdn.bootcss.com/jquery/1.12.4/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="/Scripts/jquery-1.12.4.min.js"><\/script>')</script>    
    <script src="../Scripts/webuploader/webuploader.js"></script>
    
    <style type="text/css"></style>
    
    <script type="text/javascript">

        var uploader = WebUploader.Uploader({
            swf: '/Scripts/webuploader/Uploader.swf',

            // 开起分片上传。
            chunked: true, // 是否要分片处理大文件上传
            chunkSize: 512 * 1024, // 分片大小
            server: "/Handler/WebUploaderHandler.ashx",
            fileNumLimit: 300, // 验证文件总数量, 超出则不允许加入队列
            fileSizeLimit: 200 * 1024 * 1024, // 200 M 验证文件总大小是否超出限制, 超出则不允许加入队列
            fileSingleSizeLimit: 50 * 1024 * 1024 // 50 M 验证单个文件大小是否超出限制, 超出则不允许加入队列
            , accept: { // 指定接受哪些类型的文件。
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/*'
            }, auto: false // 设置为 true 后，不需要手动调用上传，有文件选择即开始上传。
            , formData: { // 文件上传请求的参数表
                uid: 123, id:2
            }, fileVal: 'file' // 设置文件上传域的name
            , method: 'POST' // 文件上传方式，POST或者GET

            , beforeFileQueued: function(file) {
                // 当文件被加入队列之前触发，此事件的handler返回值为false，则此文件不会被添加进入队列。
            }

            , fileQueued: function(file) {
                // 当文件被加入队列以后触发。
            }

            , fileDequeued: function(file) {
                // 当文件被移除队列后触发。
            }

            , filesQueued: function (files) {
                // 当一批文件添加进队列以后触发。
            }

            , startUpload: function() {
                // 当开始上传流程时触发。
            }

            , stopUpload: function() {
                // 当开始上传流程暂停时触发。
            }

            , uploadFinished: function() {
                // 当所有文件上传结束时触发。
            }

            , uploadStart: function (file) {
                // 某个文件开始上传前触发，一个文件只会触发一次。
            }

            , uploadBeforeSend: function(object, data, headers) {
                // 当某个文件的分块在发送前触发，主要用来询问是否要添加附带参数，大文件在开起分片上传的前提下此事件可能会触发多次
            }

            //, uploadAccept: function (file) { }

            , uploadProgress: function(file, percentage) {
                // 上传过程中触发，携带上传进度。
            }

            , uploadError: function(file, reason) {
                // 当文件上传出错时触发
            }

            , uploadSuccess: function (file, response) {
                // 当文件上传成功时触发
            }

            , uploadComplete: function(file) {
                // 不管成功或者失败，文件上传完成时触发
            }

            , error: function(type) {
                // 
            }


            //, startUpload: function (file) { }

            //, startUpload: function (file) { }

            //, startUpload: function (file) { }
        });



    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
