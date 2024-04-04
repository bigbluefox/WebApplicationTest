<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationTest.FileService.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>附件框架</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>
    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>
    <style type="text/css">
        html, body {
            overflow-x: hidden;
            overflow-y: auto;
            padding: 0;
            margin: 0;
        }
        
        body { text-align: center; }

        #iframe {
            -webkit-overflow-scrolling: touch;
            overflow: scroll;
        }

        /*#iframe {min-width: 300px; min-height: 100px; border: 3px dashed silver;}*/

        label{display: inline-block; padding: 10px 0 5px 0;}
        progress
        {
            width: 100%;
        }
    </style>

    <script type="text/javascript">

        var height = 0, width = 0;

        var doc;


        $(function() {
            var frameHeight = HSP.Common.AvailHeight() - 20;
            if (!HSP.Browser.IS_IE) {
                frameHeight -= 0;
            }

            var url = "/FileService/uploadify.aspx?TID=64C64907-F110-4A41-9DAF-5532A5A135FB&GID=" + "&UID=" + "&UNAME=";
            var content = '<iframe id="iframes" src="' + url + '" width="100%" height="' + (frameHeight / 2 - 0)
                + '" frameborder="0" marginheight="0" marginwidth="0" border="0" scrolling="no" onload="this.height=this.contentWindow.document.documentElement.scrollHeight"></iframe>';
            $('#iframe').html(content);
            $.parser.parse('#iframe');

            if (document.all) { // IE 
                doc = document.frames["iframe"].document;
            } else { // 标准
                doc = document.getElementById("iframe").contentDocument;
            }
        });

    </script>
</head>
<body style="width: 100%;">
<div id="iframe" style="margin: 0 auto; width: 600px;" title="拖拽可直接上传文件"></div>
    <div id="uploadresult" style="margin: 0 auto; width: 600px; text-align: left;">
        <%--<label id="rstLabel"></label>--%>
        <%--<div id="progressbar" class="easyui-progressbar" style="width: 100%;"></div>--%>
    </div>
    
    <progress id="uploadprogress" min="0" max="100" value="0" style="margin: 0 auto; width: 600px; text-align: left; display: none;"></progress>

    <%--<div name="image" id="dropbox" style="min-width: 300px; min-height: 100px; border: 3px dashed silver;"></div>--%>
    <%--<div id="preview"></div>--%>

<script type="text/javascript">
    
    alert(document.attachEvent + "*" + document.addEventListener);

    //var tests = {
    //         filereader: typeof FileReader != 'undefined',
    //         dnd: 'draggable' in document.createElement('span'),
    //         formdata: !!window.FormData,
    //         progress: "upload" in new XMLHttpRequest
    //     },
    //     //support = {
    //     //    filereader: document.getElementById('filereader'),
    //     //    formdata: document.getElementById('formdata'),
    //     //    progress: document.getElementById('progress')
    //     //},
    //     //acceptedTypes = {
    //     //    'image/png': true,
    //     //    'image/jpeg': true,
    //     //    'image/gif': true
    //     //},
    //     progress = document.getElementById('uploadprogress');


    //var dropbox = document.getElementById("iframe");
    var dropbox = document.getElementById("iframe");
    //var preview = document.getElementById("preview");
    //var dropbox = $("body");

    document.addEventListener("dragenter", function (e) {
        //dropbox.style.borderColor = 'gray';
        //this.classList.add('hovering');
    }, false);
    document.addEventListener("dragleave", function (e) {
        //dropbox.style.borderColor = 'silver';
        ////为当前元素移除CSS样式
        //this.classList.remove('hovering');
    }, false);

    //文件拖拽完成效果
    document.addEventListener("dragover", function (e) {
        e.stopPropagation();
        e.preventDefault();
        //把拖动的元素复制到放置目标（注1会给出dropEffect详细属性）。
        e.dataTransfer.dropEffect = 'copy';
    }, false);

    document.addEventListener("drop", function (e) {
        e.stopPropagation();
        e.preventDefault();

        //为当前元素移除CSS样式
        //this.classList.remove('hovering');

        var files = e.target.files || e.dataTransfer.files;
        handleFiles(files);

        //submit.disabled = false;
    }, false);

    //document.addEventListener("dragenter", function(e) {
    //    dropbox.style.borderColor = 'gray';
    //}, false);
    //document.addEventListener("dragleave", function(e) {
    //    dropbox.style.borderColor = 'silver';
    //}, false);
    //dropbox.addEventListener("dragenter", function(e) {
    //    dropbox.style.borderColor = 'gray';
    //    dropbox.style.backgroundColor = 'white';
    //}, false);
    //dropbox.addEventListener("dragleave", function(e) {
    //    dropbox.style.backgroundColor = 'transparent';
    //}, false);
    //dropbox.addEventListener("dragenter", function(e) {
    //    e.stopPropagation();
    //    e.preventDefault();
    //}, false);
    //dropbox.addEventListener("dragover", function(e) {
    //    e.stopPropagation();
    //    e.preventDefault();
    //}, false);
    //dropbox.addEventListener("drop", function(e) {
    //    e.stopPropagation();
    //    e.preventDefault();

    //    handleFiles(e.dataTransfer.files);

    //    //submit.disabled = false;
    //}, false);

    var handleFiles = function (files) {
        var rst = $("#uploadresult");
        for (var i = 0; i < files.length; i++) {

            //debugger;

            var file = files[i];
            var ext = file.name.substr(file.name.lastIndexOf('.'));
            //var msg = file.name + " * " + file.type + " * " + file.size;
            //alert(msg);

            var lbl = $('<label id="lbl' + i + '"></label>');
            var bar = $('<div id="bar' + i + '" class="easyui-progressbar" style="width: 100%;"></div>');
            lbl.appendTo(rst);
            bar.appendTo(rst);

            $.parser.parse(rst);

            lbl.html(file.name);

            //if (!file.type.match(/image*/)) {
            //    continue;
            //}

            //var img = document.createElement("img");
            //img.classList.add("obj");
            //img.file = file;
            //preview.appendChild(img);

            //var reader = new FileReader();
            //reader.onload = (function(aImg) { return function(e) { aImg.src = e.target.result; }; })(img);
            //reader.readAsDataURL(file);//表示将文件读取为一段以data:开头的字符串，就是Data URL,它是将小文件直接嵌入
            //文档的方案，如果你不写这句，那么就显示不出来因为文档中没有image.src的数据

            var fd = new FormData();
            fd.append("fileData", file);

            var xhr = new XMLHttpRequest();

            xhr.upload.addEventListener("progress", function (e, i) {
                //debugger;
                if (e.lengthComputable) {
                    var percentage = Math.round((e.loaded * 100) / e.total);
                    //img.style.opacity = 1 - percentage / 100.0;
                    //var msg1 = file.name + " * " + file.type + " * " + file.size;
                    bar.progressbar('setValue', percentage);

                    //progress.value = percentage;
                    //progress.innerHTML = percentage + "%";
                }
            }, false);

            //xhr.upload.onprogress = updateProgress;

            //xhr.upload.addEventListener("progress", uploadProgress, false);

            xhr.addEventListener("load", uploadComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);

            //var url = '/Handler/FileHandler.ashx';
            var url = "/Handler/AttachmentHandler.ashx?OPERATION=UPLOAD&TID=64C64907-F110-4A41-9DAF-5532A5A135FB&GID=1235&rnd=" + (Math.random() * 10);

            xhr.open('post', url, true); //提交服务器处理程序
            xhr.onload = function () { //加载完成之后设置进度条值为100
                //progress.value = 100;
                //progress.innerHTML = 100 + "%";
            };
            xhr.send(fd);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    // alert(xhr.responseText);
                    //debugger;
                    //var dg = $(window.frames["iframes"].document).find("#tbFileList")[0];

                    var dg = $("#iframes")[0].contentWindow.$('#tbFileList');//.datagrid('getColumnFields');

                    //var dg = doc.$('#tbFileList');
                    var rows = dg.datagrid('getRows');
                    //alert(rows.length);

                    var row = {
                        'RowNumber': rows.length + 1, 'TypeName': '工作流'
                        , 'FileName': file.name, 'FileExt': ext
                        , 'FileSize': (file.size / 1024).toFixed(0)
                        , 'CreatorName': "", 'CreateTime': getNowFormatDate()
                    };
                    rows.push(row);

                    dg.datagrid('loading');
                    dg.datagrid('loadData', { "total": row.length, "rows": rows });
                    dg.datagrid('loaded');

                    //$("#progressbar").hide();
                }
            };


            //xhr.upload.addEventListener("progress", function(e) {
            //    if (e.lengthComputable) {
            //        var percentage = Math.round((e.loaded * 100) / e.total);
            //        img.style.opacity = 1 - percentage / 100.0;
            //    }
            //}, false);

            //xhr.upload.addEventListener("load", function(e) {
            //}, false);

            //xhr.setRequestHeader("Content-Type", "multipart/form-data, boundary=" + boundary); // simulate a file MIME POST request.  
            //xhr.setRequestHeader("Content-Length", file.Size);

            //var body = '';
            //body += "--" + boundary + "\r\n";
            //body += "Content-Disposition: form-data; name=\"" + dropbox.getAttribute('name') + "\"; filename=\"" + fileName + "\"\r\n";
            //body += "Content-Type: " + fileType + "\r\n\r\n";
            //body += fileData + "\r\n";
            //body += "--" + boundary + "--\r\n";

            //xhr.sendAsBinary(body);


        }
    }

    /// <summary> 
    /// 获取当前日期时间“yyyy-MM-dd HH:MM:SS” 
    /// </summary> 

    function getNowFormatDate() {
        var date = new Date();
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
        return currentdate;
    }

    //抛出反馈成功结果
    function uploadComplete(evt) {
        //debugger;

        alert(evt.target.responseText ? evt.target.responseText : evt.target.statusText);
    }
    //抛出异常提示
    function uploadFailed(evt) {
        alert("上传文件异常.");
    }
    //抛出取消提示
    function uploadCanceled(evt) {
        alert("取消.");
    }

    function updateProgress(event) {
        //debugger;
        if (event.lengthComputable) {
            //event.loaded加载了多少字节  event.total总共多少字节
            var complete = (event.loaded / event.total * 100 | 0);
            console.log("complete->", complete);
            progress.value = complete;
            progress.innerHTML = complete + "%";
        }
    };

    ////进度把控
    //function uploadProgress(evt) {
    //    //debugger;
    //    if (evt.lengthComputable) {
    //        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
    //        //console.log( "已上传：" + percentComplete );
    //        var msg = $("#uploadresult").html();
    //        $("#uploadresult").html(msg + " * " + percentComplete);
    //        //$('#progressbar').progressbar('setValue', percentComplete);
    //    }
    //}

</script>

</body>
</html>