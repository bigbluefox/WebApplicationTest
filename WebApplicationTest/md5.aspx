<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="md5.aspx.cs" Inherits="WebApplicationTest.md5" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>js使用FileReader和Google的md5.js计算文件的MD5值 </title>

    <link href="/Scripts/uploadify/uploadify.css" rel="stylesheet"/>
    <%--<link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet" />--%>

    <script src="Scripts/jquery/jquery-1.12.4.min.js"></script>
    <%--<script src="/Scripts/uploadifive/jquery.uploadifive.min.js"></script>--%>
    <script src="/Scripts/uploadify/jquery.uploadify-3.1.min.js"></script>
    <script src="Scripts/md5.js"></script>

    <script src="Scripts/browser-md5-file.js"></script>

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="/Scripts/bootstrap/CSS/ie10-viewport-bug-workaround.css" rel="stylesheet"/>

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="/Scripts/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="/Scripts/ie-emulation-modes-warning.js"></script>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Scripts/ie10-viewport-bug-workaround.js"></script>

    <style type="text/css">
        #queue {
            border: 1px solid #E5E5E5;
            height: 177px;
            margin-bottom: 10px;
            overflow: auto;
            padding: 0 3px 3px;
            width: 600px;
        }

        .uploadify-button {
            background: url(/Images/uploadBtnBg.png) 0 0 no-repeat;
            background-color: transparent;
            border: none;
            padding: 0;
        }       
        
    </style>

    <script type="text/javascript">
        ;
        (function($, window, document, undefined) {
            window.method = null;

            $(document).ready(function() {
                var input = $('#input');
                var output = $('#output');
                var checkbox = $('#auto-update');
                var dropzone = $('#droppable-zone');
                var option = $('[data-option]');

                var execute = function() {
                    try {
                        debugger;

                        var file = document.getElementById("input").files[0];

                        alert(file.size);


                        output.val(method(input.val(), option.val()));
                    } catch (e) {
                        output.val(e);
                    }
                };

                function autoUpdate() {
                    if (!checkbox[0].checked) {
                        return;
                    }
                    execute();
                }

                if (checkbox.length > 0) {
                    input.bind('input propertychange', autoUpdate);
                    option.bind('input propertychange', autoUpdate);
                    checkbox.click(autoUpdate);
                }

                if (dropzone.length > 0) {
                    var dropzonetext = $('#droppable-zone-text');

                    $(document.body).bind('dragover drop', function(e) {
                        e.preventDefault();
                        return false;
                    });

                    if (!window.FileReader) {
                        dropzonetext.text('Your browser dose not support.');
                        $('input').attr('disabled', true);
                        return;
                    }

                    dropzone.bind('dragover', function() {
                        dropzone.addClass('hover');
                    });

                    dropzone.bind('dragleave', function() {
                        dropzone.removeClass('hover');
                    });

                    dropzone.bind('drop', function(e) {
                        dropzone.removeClass('hover');
                        file = e.originalEvent.dataTransfer.files[0];
                        dropzonetext.text(file.name);
                        autoUpdate();
                    });

                    input.bind('change', function() {
                        file = input[0].files[0];
                        dropzonetext.text(file.name);
                        autoUpdate();
                    });

                    var file;
                    execute = function() {
                        reader = new FileReader();
                        reader.onload = function(event) {
                            try {
                                if (method.update) {
                                    var batch = 1024 * 1024;
                                    var start = 0;
                                    var current = method;
                                    var asyncUpdate = function() {
                                        if (start < event.total) {
                                            output.val('hashing...' + (start / event.total * 100).toFixed(2) + '%');
                                            var end = Math.min(start + batch, event.total);
                                            current = current.update(event.target.result.slice(start, end));
                                            start = end;
                                            setTimeout(asyncUpdate);
                                        } else {
                                            output.val(current.hex());
                                        }
                                    };
                                    asyncUpdate();
                                } else {
                                    output.val(method(event.target.result));
                                }
                            } catch (e) {
                                output.val(e);
                            }
                        };
                        output.val('loading...');
                        reader.readAsArrayBuffer(file);
                    };
                }

                $('#execute').click(execute);
            });
        })(jQuery, window, document);

    </script>
</head>
<body>
<form id="form1" runat="server">
<%--   <input id="input_md5" type="file" multiple="multiple"></input>  
      
    <div class="progress progress-striped active progress-success">  
          <div id="div_load" style="width:40%" class="bar"></div>  
    </div>  
      
    <div id="md5_show"></div> 
        --%>

<div id="main">
    <h1>
        MD5 File Checksum
    </h1>
    <div class="description">
        MD5 online hash file checksum function
    </div>
    <div class="input">
        <div id="droppable-zone">
            <div id="droppable-zone-wrapper">
                <div id="droppable-zone-text">Drop File Here</div>
            </div>
            <div id="fileQueue"></div>
            <input id="input" type="file" placeholder="Input2" class="droppable-file">
        </div>
    </div>
    <div class="submit">
        <input id="execute" type="button" value="Hash" class="btn btn-default">
        <%--<input id="btnUpload" type="button" value="上传" onclick="javascript: upload();"/>--%>
        <label>
            <input id="auto-update" type="checkbox" value="1" checked="checked">Auto Update
        </label>
    </div>
    <div class="output">
        <textarea id="output" placeholder="Output"></textarea>
    </div>
</div>

<div id="new" style="border: 1px solid #556b2f; display: block; margin-top: 5px;">
    <div>
        <input id="File1" type="file" placeholder="Input2">
    </div>
    <div class="submit">
        <input id="Button1" type="button" value="计算Hash值">
    </div>
    <div class="output">
        <textarea id="Textarea1" placeholder="Output"></textarea>
    </div>

</div>

<div id="Div1" style="border: 1px solid #008080; display: block; margin-top: 5px;">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click"/>
</div>

<script type="text/javascript">

    //$(function() {
    //    RegUpload();
    //});

    var el = document.getElementById('File1');
    el.addEventListener('change', handle, false);

    function handle(e) {
        var file = e.target.files[0];
        var startTime = new Date();//获取开始时间
        browserMD5File(file, function(err, md5) {
            //console.log(md5); // 97027eb624f85892c69c4bcec8ab0f11
            var timespan = new Date().getTime() - startTime.getTime();
            var rst = "文件大小：" + file.size + "，耗时：" + timespan + "毫秒，MD5：" + md5;
            $("#Textarea1").val(rst);

        });
    }


    function RegUpload() { //在这里我列出了常用的参数和注释，更多的请直接看jquery.uploadify.js

        //var wfName = parent.Request.QueryString('wfName').toString();
        //var wfDesc = unescape(parent.Request.QueryString('wfDesc').toString());
        //var wfVersion = parent.Request.QueryString('wfVersion').toString();
        //var wfId = parent.Request.QueryString('wfId').toString();
        //var actionId = parent.Request.QueryString('actionId').toString();
        //var taskid = parent.Request.QueryString('task_id').toString();

        var upFiletype = "*.*;";
        //var upFiletype = UpFiletype;
        var params = escape("wfName=1234&wfId=1234&actionId=1234&taskid=1234");

        var TypeID = "64C64907-F110-4A41-9DAF-5532A5A135FB";
        var GroupID = "1236";
        var UserID = "0";
        var UserName = "MD5";

        $("#input").uploadify({
            swf: '/Scripts/uploadify/uploadify.swf?rnd=' + (Math.random() * 10), //上传的Flash，不用管，路径对就行
            uploader: "/Handler/AttachmentHandler.ashx?OPERATION=UPLOAD&TID=" + TypeID + "&GID=" + GroupID + "&UID=" + UserID + "&UName=" + UserName + "&Params=" + params + "&rnd=" + (Math.random() * 10), //Post文件到指定的处理文件
            //folder: '/Files',
            //scriptData: { 'folder': 'upload' },
            auto: true, //是否直接上传
            //buttonClass: 'easyui-linkbutton', //浏览按钮的class <a id="peoplebutton" class="easyui-linkbutton">选择人员</a>
            buttonText: '', //浏览按钮的Text
            //buttonImage: "/Images/select-file.gif",
            cancelImage: '/Scripts/uploadify/uploadify-cancel.png', //取消按钮的图片地址
            fileTypeDesc: upFiletype, //'*.doc;*.docx;*.xls;*.pdf;*.jpg;*.gif;*.txt;xlsx;*.zip', //需过滤文件类型   
            fileTypeExts: upFiletype, //'*.doc;*.docx;*.xls;*.pdf;*.jpg;*.gif;*.txt;xlsx;*.zip', //需过滤文件类型的提示
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
                //GetFileList();
            } //当单个文件上传成功后激发的事件
        });

        //InitAttachmentTb();

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

        //GetFileList();

    }


    //$(function() {


    //    $("#input").uploadifive({
    //        'auto': false,
    //        'fileSizeLimit': 0, // The maximum upload size allowed in KB.  This option also accepts a unit.  If using a unit, the value must begin with a number and end in either KB, MB, or GB.  Set this option to 0 for no limit.
    //        'fileObjName': 'fileData',
    //        'buttonText': "选择文件",
    //        'buttonClass': 'btn btn-primary',
    //        'removeCompleted': true,
    //        'checkScript': '/Handler/CheckExistsHandler.ashx',
    //        'uploadScript': '/Handler/UploadHandler.ashx', //这里是通过MVC里的FileUpload控制器里的Upload 方法  
    //        'onUploadComplete': function (file, data) { // 文件上传成功后执行 

    //            //debugger;
    //            //console.log(data);

    //            //var flag = "上传成功", msg = "";
    //            //var arr, ext = "";
    //            //if (data.length > 0) {
    //            //    arr = data.split('*');
    //            //    flag = arr[0] == "0" ? "<span style=\"color:red;\">上传失败</span>" : "上传成功";
    //            //    msg = arr.length > 1 ? (arr[0] == "0" ? arr[1] : "") : "";
    //            //}

    //            //if (file.name.length > 0) {
    //            //    arr = file.name.split('.');
    //            //    ext = arr.length > 1 ? arr[arr.length - 1] : "";
    //            //}

    //            ////SELECT     TOP (200) FileId, TypeId, GroupId, FileName, FileDesc, FileExt, FileSize, ContentType, FileUrl
    //            ////, FilePath, Params, Creator, CreatorName, CreateTime, Modifier, ModifierName, ModifyTime
    //            ////FROM         Attachments

    //            //var rows = $('#tbFileList').datagrid('getRows');
    //            //var row = { "RowNumber": (rows.length + 1), "TypeName": "工作流", "FileName": file.name, "FileSize": Math.round(file.size / 1024.0), "FileExt": "." + ext, "ContentType": file.type, "CreateTime": new Date(), "flag": flag, "message": msg };
    //            //rows.push(row);

    //            //$('#tbFileList').datagrid('loadData', { "total": rows.length, "rows": rows });
    //            //$('#tbFileList').datagrid('loaded');
    //        },
    //        'queueID': 'queue',
    //        'multi': true,
    //        onFallback: function () {
    //            // HTML5 API不支持的浏览器触发, 很重要
    //            alert("该浏览器无法使用上传功能!");
    //        }
    //        , 'onAddQueueItem': function (file) {
    //            //alert('The file ' + file.name + ' was added to the queue!'); // 有效
    //        }, 'onUploadFile': function (file) {
    //            //alert('The file ' + file.name + ' is being uploaded.'); // 无效
    //        }, 'onCheck': function (file, exists) {
    //            if (exists) {
    //                alert('文件 ' + file.name + ' 在服务器上已经存在！'); // 有效
    //            }
    //        }, 'onQueueComplete': function (uploads) {
    //            //alert(uploads.successful + ' files were uploaded successfully.'); // 有效
    //        }, 'onSelect': function (queue) {
    //            //alert(queue.queued + ' files were added to the queue.'); // 有效
    //        }, 'onUpload': function (filesToUpload) {
    //            //alert(filesToUpload + ' files will be uploaded.'); // 有效，插入前提示
    //        }

    //        , 'formData': { 'ID': "", 'GID': "", 'TID': "" } //由于这里初始化时传递的参数，当然就这里就只能传静态参数了  
    //    });

    //});

    //function upload() {
    //    var myid = "", gid = "1234", tid = "64C64907-F110-4A41-9DAF-5532A5A135FB";
    //    $('#input').data('uploadifive').settings.formData = { 'ID': myid, 'GID': gid, 'TID': tid }; //动态更改formData的值
    //    $('#input').uploadifive('upload'); //上传
    //}


    //$(function() {

    //    $("#Button1").click(function() {
    //        var file;
    //        var input = $('#File1');
    //        var output = $("#Textarea1");

    //        var execute = function() {
    //            try {
    //                output.val(method(input.val(), option.val()));
    //            } catch (e) {
    //                output.val(e);
    //            }
    //        }

    //        input.bind('change', function() {
    //            file = input[0].files[0];
    //            //dropzonetext.text(file.name);
    //            //autoUpdate();
    //        });

    //        execute = function() {
    //        var reader = new FileReader();
    //            reader.onload = function(event) {
    //                try {
    //                    if (method.update) {
    //                        var batch = 1024 * 1024;
    //                        var start = 0;
    //                        var current = method;
    //                        var asyncUpdate = function() {
    //                            if (start < event.total) {
    //                                output.val('hashing...' + (start / event.total * 100).toFixed(2) + '%');
    //                                var end = Math.min(start + batch, event.total);
    //                                current = current.update(event.target.result.slice(start, end));
    //                                start = end;
    //                                setTimeout(asyncUpdate);
    //                            } else {
    //                                output.val(current.hex());
    //                            }
    //                        }
    //                        asyncUpdate();
    //                    } else {
    //                        output.val(method(event.target.result));
    //                    }
    //                } catch (e) {
    //                    output.val(e);
    //                }
    //            };
    //            output.val('loading...');
    //            reader.readAsArrayBuffer(file);
    //        };
    //    execute();

    //        });


    //});

    //var execute = function () {
    //    try {
    //        output.val(method(input.val(), option.val()));
    //    } catch (e) {
    //        output.val(e);
    //    }
    //}

    //var file;
    //execute = function () {
    //   var reader = new FileReader();
    //    reader.onload = function (event) {
    //        try {
    //            if (method.update) {
    //                var batch = 1024 * 1024;
    //                var start = 0;
    //                var current = method;
    //                var asyncUpdate = function () {
    //                    if (start < event.total) {
    //                        output.val('hashing...' + (start / event.total * 100).toFixed(2) + '%');
    //                        var end = Math.min(start + batch, event.total);
    //                        current = current.update(event.target.result.slice(start, end));
    //                        start = end;
    //                        setTimeout(asyncUpdate);
    //                    } else {
    //                        output.val(current.hex());
    //                    }
    //                }
    //                asyncUpdate();
    //            } else {
    //                output.val(method(event.target.result));
    //            }
    //        } catch (e) {
    //            output.val(e);
    //        }
    //    };
    //    output.val('loading...');
    //    reader.readAsArrayBuffer(file);
    //};
</script>


</form>

<style type="text/css">
    #main { border: 1px solid #00bfff; }

    #new { border: 1px solid #006400; }

    #output, #Textarea1 {
        height: 60px;
        width: 624px;
    }

</style>

<script>
    method = md5;
</script>
</body>
</html>

<script type="text/javascript">

    //var message = [];

    //if (!document.getElementById("input_md5").files) {
    //    message = '<p>浏览器不支持FileReader API</p>';
    //    document.querySelector("body").innerHTML = message;
    //} else {
    //    document.getElementById('input_md5').addEventListener('change', handleFileSelection, false);
    //}

    //function handleFileSelection(event) {
    //    var files = event.target.files;
    //    if (!files) {
    //        msa.alert("<p>At least one selected file is invalid - do not select any folders.</p><p>Please reselect and try again.</p>");
    //        return;
    //    }

    //    var file = files[0];

    //    var chunkSize = 204800;
    //    var pos = 0;

    //    var md5Instance = CryptoJS.algo.MD5.create();

    //    var reader = new FileReader();
    //    function progressiveReadNext() {
    //        var end = Math.min(pos + chunkSize, file.size);

    //        reader.onload = function (e) {
    //            pos = end;
    //            md5Instance.update(CryptoJS.enc.Latin1.parse(e.target.result));
    //            var present = (parseFloat(pos) / parseFloat(file.size)) * 10000 / 100;
    //            $("#div_load").css("width", Math.round(present) + "%");
    //            if (pos < file.size) {
    //                progressiveReadNext();
    //            } else {
    //                var md5Value = md5Instance.finalize();
    //                console.log(md5Value.toString());
    //                $("#md5_show").html(md5Value.toString());

    //            }
    //        }

    //        if (file.slice) {
    //            var blob = file.slice(pos, end);
    //        } else if (file.webkitSlice) {
    //            var blob = file.webkitSlice(pos, end);
    //        } else if (File.prototype.mozSlice) {
    //            var blob = file.mozSlice(pos, end);
    //        }
    //        reader.readAsBinaryString(blob);
    //    }
    //    progressiveReadNext();
    //}

</script>