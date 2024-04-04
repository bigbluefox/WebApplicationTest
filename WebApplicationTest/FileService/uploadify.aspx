<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadify.aspx.cs" Inherits="WebApplicationTest.uploadify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title>uploadify 文件上传测试()</title>
<link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
<link href="/Scripts/themes/icon.css" rel="stylesheet"/>
<link href="/Scripts/uploadify/uploadify.css" rel="stylesheet"/>

<%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
<script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>
<script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>

<script src="/Scripts/jquery.easyui.min.js"></script>
<script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
<script src="/Scripts/uploadify/jquery.uploadify-3.1.min.js"></script>
<script src="/Scripts/Hsp.js"></script>
<script src="Attachment.js?v=0.0003"></script>

<style type="text/css">
    body { font: 13px Arial, Helvetica, Sans-serif; }

    img { cursor: pointer; }

    a { text-decoration: none; }

    #queue {
        border: 1px solid #E5E5E5;
        height: 177px;
        margin-bottom: 10px;
        overflow: auto;
        padding: 0 3px 3px;
        width: 600px;
    }

    input { height: 30px; }

    /*.hand { cursor: pointer; }*/

    .tip {
        border-bottom: 1px solid #CCC;
        height: 20px;
        margin-bottom: 10px;
    }

    .uploadify-button {
        background: url(/Images/uploadBtnBg.png) 0 0 no-repeat;
        background-color: transparent;
        border: none;
        padding: 0;
    }

    td img {
        border: 0;
        padding-left: 5px;
        padding-top: 5px;
    }

    .hovering {
        border: 2px dashed #00bfff;
        -webkit-box-shadow: inset 0px 0px 50px #BBB;
        -moz-box-shadow: inset 0px 0px 50px #BBB;
        -o-box-shadow: inset 0px 0px 50px #BBB;
        box-shadow: inset 0px 0px 50px #BBB;
    }

    input[type=file], input + div, table { float: left; }

    html, body {
        overflow-x: hidden;
        overflow-y: auto;
    }
</style>

<script type="text/javascript">
    var ServiceUrl = "<%= FileUrl %>"; //根目录
    var UpFiletype = "<% = UpFileTypes %>"; //默认从配置文件获取文件类型，实际是获取别人传入的值
    var VD = "<% = VirtualDirectory %>"; // 虚拟目录

    /*
    var RootPathShineupon = ""; //物理路径的映射
    var RootPath = ""; //默认从配置文件获取物理路径，实际是通过url参数传入
    var Url = ""; //默认从配置文件获取网络路径，实际是通过url参数传入
    */

    var TypeID = "<% = TypeId %>"; //分类ID
    var GroupID = "<% = GroupId %>"; //分组ID

    var UserID = "<% = UserId %>"; //用户ID
    var UserName = "<% = UserName %>"; //用户姓名

    //上传控件-------------------------------------------------------

    $(document).ready(function() {
        RegUpload();

        var html = "";
        //html += "<img onclick=\"javascript:UploadAttachment();\" src='/Images/AddFile.png' width='12' title='添加附件' />";
        //html += "&nbsp;";
        html += "<img onclick=\"javascript:BatchDownload();\" src='/Images/Download.png' title='批量下载' />";
        html += "&nbsp;";
        html += "<img onclick=\"javascript:AttachmentLog();\" src='/Images/Info.png' title='操作日志' id=\"loginfo\" />";
        $('.panel-tool').html(html);

        //alert(HSP.Browser.IS_IE);

        //var nv = getBrowserInfo();

        //alert(navigator.userAgent);

    });

    function RegUpload() { //在这里我列出了常用的参数和注释，更多的请直接看jquery.uploadify.js

        //var wfName = parent.Request.QueryString('wfName').toString();
        //var wfDesc = unescape(parent.Request.QueryString('wfDesc').toString());
        //var wfVersion = parent.Request.QueryString('wfVersion').toString();
        //var wfId = parent.Request.QueryString('wfId').toString();
        //var actionId = parent.Request.QueryString('actionId').toString();
        //var taskid = parent.Request.QueryString('task_id').toString();

        //var upFiletype = "*.*;";
        var upFiletype = UpFiletype;
        var params = escape("wfName=1234&wfId=1234&actionId=1234&taskid=1234");

        $("#uploadify").uploadify({
            swf: '/Scripts/uploadify/uploadify.swf?rnd=' + (Math.random() * 10), //上传的Flash，不用管，路径对就行
            uploader: "/Handler/AttachmentHandler.ashx?OPERATION=UPLOAD&TID=" + TypeID + "&GID=" + GroupID + "&UID=" + UserID + "&UName=" + UserName + "&Params=" + params + "&rnd=" + (Math.random() * 10), //Post文件到指定的处理文件
            //folder: '/Files',
            //scriptData: { 'folder': 'upload' },
            auto: true, //是否直接上传
            //buttonClass: 'easyui-linkbutton', //浏览按钮的class <a id="peoplebutton" class="easyui-linkbutton">选择人员</a>
            buttonText: '', //浏览按钮的Text
            //buttonImage: "/Images/select-file.gif",
            cancelImage: '/Scripts/uploadify/uploadify-cancel.png', //取消按钮的图片地址
            fileTypeDesc: '*.*', //'*.doc;*.docx;*.xls;*.pdf;*.jpg;*.gif;*.txt;xlsx;*.zip', //需过滤文件类型   
            fileTypeExts: '*.*', //'*.doc;*.docx;*.xls;*.pdf;*.jpg;*.gif;*.txt;xlsx;*.zip', //需过滤文件类型的提示
            // height: -1,//浏览按钮高
            // width:-1,//浏览按钮宽
            sizeLimit: 102400000, //控制上传文件大小
            multi: true, //是否允许多文件上传
            uploadLimit: 99, //同时上传多小个文件
            queueSizeLimit: 99, //队列允许的文件总数
            removeCompleted: true, //当上传成功后是否将该Item删除
            onSelect: function(file) { validFileLenth(file); }, //选择文件时触发事件
            onSelectOnce: function(event, data) {
                var count = data.fileCount;
                //alert(count);
            },
            onSelectError: function(file, errorCode, errorMsg) {
            }, //选择文件有误触发事件
            onUploadComplete: function(file) {
            }, //上传成功触发事件
            onUploadError: function(file, errorCode, errorMsg) {
                alert(errorCode);
                return false;
            }, //上传失败触发事件
            onUploadProgress: function(file, fileBytesLoaded, fileTotalBytes) {
            }, //上传中触发事件
            onUploadStart: function(file) {
            }, //上传开始触发事件
            hideButton: true, //上传按钮权限控制
            onUploadSuccess: function(event, response, status) {
                /*$("#tbFileList").datagrid('reload');*/
                GetFileList();
            } //当单个文件上传成功后激发的事件
        });

        InitAttachmentTb();

        //$.post('/Handler/CrudHandler.ashx', { OPERATION: "GETUSER" }, function (result) {
        //    debugger;
        //    if (result.success) {

        //        //debugger;

        //        var len = result.total;
        //        var data = result.rows;


        //        //$('#dg').datagrid('reload');    // reload the user data
        //    } else {
        //        $.messager.show({    // show error message
        //            title: 'error',
        //            msg: result.errorMsg
        //        });
        //    }
        //}, 'json');

        //var arr = { OPERATION: "GETFILELIST", FID: "", TID: TypeID, GID: GroupID, UID: userId, RID: Math.round(Math.random() * 10) };

        //// 发送ajax请求
        //$.getJSON("/Handler/GetFileHandler.ashx", arr)
        //    //向服务器发出的查询字符串 
        //    // 对返回的JSON数据进行处理 
        //    .done(function (data) {

        //        //console.info(data);
        //        if (data != null && data.length > 0) {
        //            $('#tbFileList').datagrid('loading');
        //            $('#tbFileList').datagrid('loadData', { "total": data[0].RecordCount, "rows": data });
        //            $('#tbFileList').datagrid('loaded');
        //        } else {
        //            //$.messager.alert('温馨提示', '查询失败！', 'error');
        //            $('#tbFileList').datagrid('loadData', { "total": 0, "rows": [] });
        //            $('#tbFileList').datagrid('loaded');
        //        }
        //    });

        GetFileList();

    }


    /// <summary>
    /// 附件表初始化
    /// </summary>

    function InitAttachmentTb() {
        $('#tbFileList').datagrid({
            //url: requestUrl,
            width: 'auto',
            height: 'auto',
            idField: 'FileId',
            fitColumns: true,
            pagination: false,
            rownumbers: true,
            singleselect: false,
            striped: true,
            nowrap: false,
            columns: [
                [
                    { field: 'RowNumber', title: '序号', width: 30, halign: 'center', align: 'center', height: 26, hidden: true },
                    { field: 'TypeName', title: '附件类型', halign: 'center', align: 'left', width: 60 },
                    {
                        field: 'FileName',
                        title: '附件名称',
                        halign: 'center',
                        align: 'left',
                        width: 120,
                        formatter: function(value, row, index) {
                            value = value.replace(" ", "&nbsp;");
                            var s = '<a title=' + value + ' href="javascript:void(0);" onclick="ViewFileOnline(\'' + row.FileId + '\',\'' + row.FileExt + '\',\'' + row.FileUrl + '\');">' + value + '</a>';
                            return row.FileSize > 0.0 ? s : value;
                        }
                    },
                    { field: 'FileExt', title: '扩展名', halign: 'center', align: 'center', width: 45 },
                    { field: 'FileSize', title: '大小（KB）', halign: 'center', align: 'right', width: 45 },
                    { field: 'CreatorName', title: '上传人', halign: 'center', align: 'center', width: 40 },
                    { field: 'CreateTime', title: '上传时间', halign: 'center', align: 'center', width: 75 },
                    { field: 'FileUrl', title: '地址', halign: 'center', align: 'center', width: 60, hidden: true },
                    {
                        field: 'FileId',
                        title: '操作',
                        halign: 'center',
                        align: 'center',
                        width: 45,
                        formatter:
                            function(value, row, index) {

                                var downloadImg = ' <img alt="文件下载" title="文件下载" src="/Images/Next-16x16.png" onclick="DownloadFile(\'' + row.FileId + '\', \'' + row.FileUrl + '\');" />';
                                var viewDocImg = ' <img alt="文件编辑" title="文件编辑" src="/Images/edit.jpg" onclick="EditFileOnline(\'' + row.FileId + '\',\'' + row.FileExt + '\');" />';
                                var delFileImg = ' <img alt="文件删除" title="文件删除" src="/Images/Remove-16x16.png" onclick="DelFileById(\'' + row.FileId + '\', \'' + row.FilePath + '\');" />';

                                var s = downloadImg;
                                s += viewDocImg;
                                if (row.Creator == UserID) s += delFileImg;
                                return s;

                                /*
                                var str = "";
                                if (authority_type.indexOf("Download") > -1) {
                                str = str + '<a title="下载" style="cursor:pointer;" class="s_action" plain="true" href="../Handler/UpDownloadHandler.ashx?GetFunction=DownloadFile&filesID=' + row.Files_Id + '&FilesName=' + row.FilesName + '&IsCrossDomain=' + IsCrossDomain + '&RootPathShineupon=' + RootPathShineupon + '&CroseDomainUserName=' + CroseDomainUserName + '&CroseDomainUserPwd=' + CroseDomainUserPwd + '&IsDel=' + row.IsDel + '&CreateTime=' + row.CreateTime + '&netaddress=' + row.Url + '&DownState=' + row.DownState + '&downAddress=' + Url + '&physicaladdress=' + physicaladdress + '"><img style="border:none;" src="../themes/icons/document_small_download.png"></a>&nbsp;'
                                }
                                if (authority_type.indexOf("dell") > -1) {

                                if (row.User_Name == UserNameCode) {
                                str = str + '&nbsp;<a title="删除" style="cursor:pointer;" class="s_action " plain="true" onclick="show_confirm(' + index + ')"><img style="border:none;" src="../themes/icons/page_delete.png"></a>';
                                }
                                }
                                str = str + '</a>';
                                return str;
                                */

                            }
                    }
                ]
            ],
            rowStyler: function(index, row, css) {
                /*
                //红色已删除  紫色已下载   删除优先级高于下载
                if (row.IsDel.toString() == "1") {
                return 'color:#ff0000;'; //font-weight:bold;粗体
                } else if (row.DownState == "已下载") {
                return 'color:#cc0088;'; //background-color:#fff 
                } else {
                return 'color:#000000;';
                }*/
            },
            onLoadSuccess: function(data) {
                //console.info(data);
                //var panel = $(this).datagrid('getPanel');
                //var tr = panel.find('div.datagrid-view .datagrid-view2 .datagrid-body table tbody tr');
                //tr.each(function() {
                //    var td = $(this).children('td[field="FileId"]');
                //    var cell = td.children('div.datagrid-cell');
                //    var img = cell.children('img');
                //    img.each(function() {
                //        $(this).hover(function() { $(this).addClass("hand"); }, function() { $(this).removeClass("hand"); });
                //    });
                //});
            }
        });

        GetFileList();

    }


    function getBrowserInfo() {
        var agent = navigator.userAgent.toLowerCase();

        var regStr_ie = /msie [\d.]+;/gi;
        var regStr_ff = /firefox\/[\d.]+/gi;
        var regStr_chrome = /chrome\/[\d.]+/gi;
        var regStr_saf = /safari\/[\d.]+/gi;
        //IE
        if (agent.indexOf("msie") > 0) {
            return agent.match(regStr_ie);
        }

        //firefox
        if (agent.indexOf("firefox") > 0) {
            return agent.match(regStr_ff);
        }

        //Chrome
        if (agent.indexOf("chrome") > 0) {
            return agent.match(regStr_chrome);
        }

        //Safari
        if (agent.indexOf("safari") > 0 && agent.indexOf("chrome") < 0) {
            return agent.match(regStr_saf);
        }

    }

</script>

</head>
<body title="拖拽到本区域可自动上传文件，每次一个文件">
<form id="form1" runat="server">
    <div id="dropbox" style="min-width: 300px; min-height: 100px;border: 2px dashed #BBB;">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <div style="height: auto; overflow: hidden; padding: 0 0 5px 0; width: 480px;">
            <input type="file" name="uploadify" id="uploadify"/>
            <div id="fileQueue" style="float: left;"></div>
        </div>
        <div id="progressbar" class="easyui-progressbar" style="width: 100%; display: none;"></div>
        <div style="width: 100%;">
            <table id="tbFileList" class="easyui-datagrid" title="附件列表" loadmsg="加载中..."></table>
        </div>
    </div>
</form>

<script type="text/javascript">

    var dropbox = document.getElementById("dropbox");

    ////文件进入事件
    //dropbox.addEventListener("dragenter", function(e) {

    //    //dropbox.style.borderColor = 'gray';
    //    //dropbox.style.backgroundColor = 'white';
    //    e.stopPropagation(); //不再派发事件
    //    e.preventDefault(); //取消事件的默认动作
    //    //为当前元素添加CSS样式（这里使用到的样式均会在下面展示出来）
    //    this.classList.add('hovering');
    //}, false);

    ////文件离开事件
    //dropbox.addEventListener("dragleave", function(e) {
    //    //dropbox.style.backgroundColor = 'transparent';

    //    e.stopPropagation();
    //    e.preventDefault();
    //    //为当前元素移除CSS样式
    //    this.classList.remove('hovering');
    //}, false);

    ////文件拖拽完成效果
    //dropbox.addEventListener("dragover", function(e) {
    //    e.stopPropagation();
    //    e.preventDefault();
    //    //把拖动的元素复制到放置目标（注1会给出dropEffect详细属性）。
    //    e.dataTransfer.dropEffect = 'copy';
    //}, false);

    ////文件拖拽到页面后处理方式
    //dropbox.addEventListener("drop", function(e) {
    //    e.stopPropagation();
    //    e.preventDefault();

    //    //为当前元素移除CSS样式
    //    //this.classList.remove('hovering');

    //    //target 事件属性可返回事件的目标节点（触发该事件的节点），如生成事件的元素、文档或窗口。
    //    var files = e.target.files || e.dataTransfer.files;
    //    handleFiles(files);

    //    //submit.disabled = false;
    //}, false);

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

    var handleFiles = function(files) {

        //var rst = $("#uploadresult");
        $("#progressbar").show();

        for (var i = 0; i < files.length; i++) {

            //debugger;

            var file = files[i];
            var ext = file.name.substr(file.name.lastIndexOf('.'));
            //var msg = file.name + " * " + file.type + " * " + file.size;
            //msg = file.name + "; <br/>";
            //alert(msg);

            //dropbox.innerHTML = dropbox.innerHTML + msg;

            //var lbl = $('<label id="lbl' + i + '"></label>');
            //var bar = $('<div id="bar' + i + '" class="easyui-progressbar" style="width: 100%;"></div>');
            //lbl.appendTo(rst);
            //bar.appendTo(rst);

            //$.parser.parse(rst);

            //lbl.html(file.name);
            //if (!file.type.match(/image*/)) {
            //    continue;
            //}

            //var img = document.createElement("img");
            //img.classList.add("obj");
            //img.file = file;
            //preview.appendChild(img);


            var fd = new FormData();
            fd.append("fileData", file);

            var xhr = new XMLHttpRequest();

            xhr.upload.addEventListener("progress", function(e) {
                if (e.lengthComputable) {//event.loaded加载了多少字节  event.total总共多少字节
                    var percentage = Math.round((e.loaded * 100) / e.total);
                    //img.style.opacity = 1 - percentage / 100.0;
                    $("#progressbar").progressbar('setValue', percentage);
                }
            }, false);

            //xhr.upload.addEventListener("progress", uploadProgress, false);

            xhr.addEventListener("load", function(e) {});

            //xhr.addEventListener("load", uploadComplete, false);
            //xhr.addEventListener("error", uploadFailed, false);
            //xhr.addEventListener("abort", uploadCanceled, false);

            var url = "/Handler/AttachmentHandler.ashx?OPERATION=UPLOAD&TID=64C64907-F110-4A41-9DAF-5532A5A135FB&GID=1235&rnd=" + (Math.random() * 10);
            xhr.open("POST", url, true); //提交服务器处理程序 "/Handler/FileHandler.ashx"
            xhr.onload = function() { //加载完成之后设置进度条值为100
                //progress.value = 100;
                //progress.innerHTML = 100 + "%";
            };
            xhr.send(fd);
            xhr.onreadystatechange = function() {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    // alert(xhr.responseText);

                    var rows = $('#tbFileList').datagrid('getRows');
                    //alert(rows.length);

                    var row = {
                        'RowNumber': rows.length + 1, 'TypeName': '工作流'
                        , 'FileName': file.name, 'FileExt': ext
                        , 'FileSize': (file.size / 1024).toFixed(0)
                        , 'CreatorName': UserName, 'CreateTime': getNowFormatDate()
                    };
                    rows.push(row);

                    $('#tbFileList').datagrid('loading');
                    $('#tbFileList').datagrid('loadData', { "total": row.length, "rows": rows });
                    $('#tbFileList').datagrid('loaded');

                    $("#progressbar").hide();
                }
            };

            //var reader = new FileReader();
            //reader.onload = (function(aImg) { return function(e) { aImg.src = e.target.result; }; })(img);
            //reader.readAsDataURL(file);//表示将文件读取为一段以data:开头的字符串，就是Data URL,它是将小文件直接嵌入
            //文档的方案，如果你不写这句，那么就显示不出来因为文档中没有image.src的数据

            //var xhr = new XMLHttpRequest();
            //xhr.open('post', '/Handler/FileHandler.ashx', true);

            //xhr.upload.addEventListener("progress", function(e) {
            //    if (e.lengthComputable) {
            //        var percentage = Math.round((e.loaded * 100) / e.total);
            //        img.style.opacity = 1 - percentage / 100.0;
            //    }
            //}, false);

            //xhr.upload.addEventListener("load", function(e) {}, false);

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

    //抛出反馈成功结果
    function uploadComplete(evt) {
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

</script>

</body>
</html>