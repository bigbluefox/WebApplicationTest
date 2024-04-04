<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadifive.aspx.cs" Inherits="WebApplicationTest.uploadifive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>上传测试（不能在兼容模式下启用）</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet" />
    <link href="/Scripts/themes/icon.css" rel="stylesheet" />
    <link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet" />
   
    <%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
    <%--<script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>--%>
    <%--<script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>--%>
    <script src="../Scripts/jquery/jquery-1.7.1.js"></script>

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/uploadifive/jquery.uploadifive.min.js"></script>   
    <script src="/Scripts/Hsp.js"></script>
    <script src="Attachment.js?v=0.0003"></script>

    <style type="text/css">
        body
        {
            font: 13px Arial, Helvetica, Sans-serif;
        }

        img { cursor: pointer; }

        input[type='button'],input[type='file']{cursor: pointer;}

        .uploadifive-button
        {
            float: left;
            margin-right: 10px;
        }

        #queue
        {
            border: 1px solid #E5E5E5;
            height: 177px;
            overflow: auto;
            margin-bottom: 10px;
            padding: 0 3px 3px;
            width: 600px;
        }

        input{ height: 30px;}        
    </style>
    
    <script type="text/javascript">
        
        var ServiceUrl = "<%= FileUrl %>"; //根目录
        var UpFiletype = "<% = UpFileTypes %>"; //默认从配置文件获取文件类型，实际是获取别人传入的值
        var VD = "<% = VirtualDirectory %>"; // 虚拟目录

        var TypeID = "<% = TypeId %>"; //分类ID
        var GroupID = "<% = GroupId %>"; //分组ID

        var UserID = "<% = UserId %>"; //用户ID
        var UserName = "<% = UserName %>"; //用户姓名

        $(function() {

            //$('#tbFileList').datagrid({
            //    title: '上传附件列表',
            //    iconCls: 'icon-terms',
            //    width: 'auto',
            //    fit: true,
            //    fitColumns: true,
            //    border: false,
            //    nowrap: true,
            //    striped: true,
            //    collapsible: false,
            //    pagination: true,
            //    pageSize: 10,
            //    pageList: [100, 50, 30, 20],
            //    columns: [[
            //        { field: 'rowNumber', title: '序号', width: 45, align: 'center' },
            //        { field: 'name', title: '文件名称', width: 260, align: 'left' },
            //        { field: 'size', title: '文件大小', width: 60, align: 'right' },
            //        { field: 'extension', title: '文件扩展名', width: 60, align: 'center' },
            //        { field: 'type', title: '文件类型', width: 260, align: 'left' },
            //        { field: 'flag', title: '上传状态', width: 60, align: 'center' },
            //        { field: 'message', title: '上传消息', width: 120, align: 'left' }
            //    ]]
            //});

            InitAttachmentTb();

        });

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
                        { field: 'TypeName', title: '附件类型', halign: 'center', align: 'center', width: 60 },
                        {
                            field: 'fileName',
                            title: '附件名称',
                            halign: 'center',
                            align: 'left',
                            width: 120,
                            formatter: function (value, row, index) {
                                value = value && value.length > 0 ? value.replace(" ", "&nbsp;") : "";
                                var s = '<a title=' + value + ' href="javascript:void(0);" onclick="ViewFileOnline(\'' + row.FileId + '\',\'' + row.FileExt + '\',\'' + row.FileUrl + '\');">' + value + '</a>';
                                return row.FileSize > 0.0 ? s : value;
                            }
                        },
                        { field: 'FileExt', title: '附件扩展名', halign: 'center', align: 'center', width: 45 },
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
                                function (value, row, index) {

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
                        },
                        { field: 'flag', title: '上传状态', width: 60, align: 'center' },
                        { field: 'message', title: '上传消息', width: 120, align: 'left' }
                    ]
                ],
                rowStyler: function (index, row, css) {
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
                onLoadSuccess: function (data) {
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

    </script>

</head>
<body>
    <h1>UploadiFive 文件上传演示</h1>
    <form id="form1" runat="server">
        <div id="queue"></div>
        <%--<input name="pic" type="file" id="pic" style="width: 300px" multiple="multiple"/>--%>
        <input id="file_upload" name="file_upload" type="file" multiple="multiple" />
        <%--<a style="position: relative; top: 8px;" href="#">上传</a>--%>
        <input id="btnUpload" type="button" value="上传" onclick="javascript: upload();" /></form>
        <div style="width: 100%; height: 10px;"></div>
        <div id="tbFileList" style="height: 600px; width: 100%;"></div>

    <script type="text/javascript">

        $(function () {

            //auto 是否自动上传，默认true 
            //uploadScript 上传路径 
            //fileObjName file文件对象名称 
            //buttonText 上传按钮显示文本 
            //queueID 进度条的显示位置 
            //fileType 上传文件类型 
            //multi 是否允许多个文件上传，默认为true 
            //fileSizeLimit 允许文件上传的最大尺寸  The maximum upload size allowed in KB.  This option also accepts a unit.  If using a unit, the value must begin with a number and end in either KB, MB, or GB.  Set this option to 0 for no limit.
            //uploadLimit 一次可以上传的最大文件数 
            //queueSizeLimit 允许队列中存在的最大文件数 
            //removeCompleted 隐藏完成上传的文件，默认为false 
            //方法 作用 
            //onUploadComplete 文件上传成功后执行 
            //onCancel 文件被删除时触发 
            //onUpload 开始上传队列时触发 
            //onFallback HTML5 API不支持的浏览器触发

            $("#file_upload").uploadifive({
                'auto': false, // 是否自动上传，默认true 
                'fileSizeLimit': 10000, // 允许文件上传的最大尺寸，0为无限制
                'fileObjName': 'fileData', // file文件对象名称 
                'buttonText': "选择文件", // 上传按钮显示文本
                'buttonClass': 'btn btn-primary',
                'dnd': true, // 如果设置为false，则无法启用拖放功能。
                'removeCompleted': true, // 隐藏完成上传的文件，默认为false 
                'queueID': 'queue', // 进度条的显示位置
                'multi': true, // 是否允许多个文件上传，默认为true
                'fileType': false, // Type of files allowed (image, etc), separate with a pipe character |
                'checkScript': '/Handler/CheckExistsHandler.ashx?rnd=' + (Math.random() * 10),
                'uploadScript': '/Handler/UploadHandler.ashx?rnd=' + (Math.random() * 10), // 这里是通过MVC里的FileUpload控制器里的Upload 方法  
                'onUploadComplete': function (file, data) { // 文件上传成功后执行 
                    
                    //debugger;
                    //console.log(data);

                    var flag = "上传成功", msg = "";
                    var arr, ext = "";
                    if (data.length > 0) {
                        arr = data.split('*');
                        flag = arr[0] == "0" ? "<span style=\"color:red;\">上传失败</span>" : "上传成功";
                        msg = arr.length > 1 ? (arr[0] == "0" ? arr[1] : "") : "";
                    }

                    if (file.name.length > 0) {
                        arr = file.name.split('.');
                        ext = arr.length > 1 ? arr[arr.length - 1] : "";
                    }

                    //SELECT     TOP (200) FileId, TypeId, GroupId, fileName, FileDesc, FileExt, FileSize, ContentType, FileUrl
                    //, FilePath, Params, Creator, CreatorName, CreateTime, Modifier, ModifierName, ModifyTime
                    //FROM         Attachments

                    var rows = $('#tbFileList').datagrid('getRows');
                    var row = { "RowNumber": (rows.length + 1), "TypeName": "工作流", "FileName": file.name, "FileSize": Math.round(file.size / 1024.0), "FileExt": "." + ext, "ContentType": file.type, "CreateTime": new Date(), "flag": flag, "message": msg };
                    rows.push(row);

                    $('#tbFileList').datagrid('loadData', { "total": rows.length, "rows": rows });
                    $('#tbFileList').datagrid('loaded');

                }, 'onFallback': function () {
                    // HTML5 API不支持的浏览器触发, 很重要
                    alert("该浏览器无法使用上传功能!");
                }
                , 'onAddQueueItem' : function(file) {
                    //alert('The file ' + file.name + ' was added to the queue!'); // 有效
                }, 'onUploadFile': function (file) {
                    //alert('The file ' + file.name + ' is being uploaded.'); // 无效
                }, 'onCheck': function (file, exists) {
                    if (exists) {
                        alert('文件 ' + file.name + ' 在服务器上已经存在！'); // 有效
                    }
                }, 'onQueueComplete': function (uploads) {
                    //alert(uploads.successful + ' files were uploaded successfully.'); // 有效
                }, 'onSelect': function (queue) {
                    //alert(queue.queued + ' files were added to the queue.'); // 有效
                }, 'onUpload': function (filesToUpload) { // 开始上传队列时触发
                    //alert(filesToUpload + ' files will be uploaded.'); // 有效，插入前提示
                }
                , 'formData': { 'ID': "", 'GID': "", 'TID': "" } //由于这里初始化时传递的参数，当然就这里就只能传静态参数了  
            });

        });

        function upload() {
            var myid = "", gid = "1234", tid = "64C64907-F110-4A41-9DAF-5532A5A135FB";
            $('#file_upload').data('uploadifive').settings.formData = { 'ID': myid, 'GID': gid, 'TID': tid }; //动态更改formData的值
            $('#file_upload').uploadifive('upload'); //上传
        }







        $('#file_upload').uploadifive({

            'auto': false,  //是否自动上传，默认true

            'multi': false,

            'dnd': true, //拖放

            'uploadScript': '../Pub/RambleHandler.ashx',            // 服务器处理地址

            'cancelImg': '../JS/uploadifive/uploadifive-cancel.png',

            //'itemTemplate': '<div class="uploadifive-queue-item">\

            //        <a class="close" href="#">X</a>\

            //        <div><span class="filename"></span><span class="fileinfo"></span></div>\

            //        <div class="progress">\

            //            <div class="progress-bar"></div>\

            //        </div>\

            //    </div>',

            //'checkExisting': 'url',

            'buttonText': "请选择文件",                  //按钮文字

            'height': 30,                             //按钮高度

            'width': 120,                              //按钮宽度

            'fileSizeLimit': "2GB",          //上传文件的大小限制 0则表示无限制

            'uploadLimit': 1,    //上传文件数

            'queueSizeLimit': 1, //一次可以在队列中拥有的最大文件数

            'fileType': 'image/jpeg',    //文件类型(mime类型)，要允许所有文件，请将此值设置为false，多个类型使用数组形式['image/jpeg','video/mp4'] 

            //'formData': { "imgType": "normal" }, //提交给服务器端的参数

            'overrideEvents': ['onDialogClose', 'onError'],    //onDialogClose 取消自带的错误提示

            'onFallback': function () { //如果浏览器没有兼容的HTML5文件API功能，则在初始化期间触发

                alert('浏览器不支持HTML5,无法上传！');

            },

            'onSelect': function (queue) {  //选择的每个文件触发一次，无论它是否返回和错误。cancelled:文件数量取消（未替换） count:队列中的文件总数   errors:返回错误的文件数  queued:添加到队列的文件数  replaced:文件替换的数量 selected:所选文件的数量

                //return false;

            },

            'onDrop': function (file, fileDropCount) {



            },

            'onAddQueueItem': function (file) {



            },

            'onClearQueue': function (queue) {



            },

            'onCancel': function (file) {



            },

            'onError': function (errorType, file) {

                if (file != 0) {

                    var settings = $('#file_upload').data('uploadifive').settings;

                    switch (errorType) {

                        case 'UPLOAD_LIMIT_EXCEEDED':

                            layer_Msg("上传的文件数量已经超出系统限制的" + settings.queueSizeLimit + "个文件！");

                            break;

                        case 'FILE_SIZE_LIMIT_EXCEEDED':

                            layer_Msg("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadifive('settings', 'fileSizeLimit') + "大小！");

                            break;

                        case 'QUEUE_LIMIT_EXCEEDED':

                            layer_Msg("任务数量超出队列限制");

                            break;

                        case 'FORBIDDEN_FILE_TYPE':

                            layer_Msg("文件 [" + file.name + "] 类型不正确！");

                            break;

                        case '404_FILE_NOT_FOUND':

                            layer_Msg("文件未上传成功或服务器存放文件的文件夹不存在");

                            break;

                    }

                }

            },

            //'onProgress': function (file, e) {  //每次文件上传都有进度更新时触发 e:事件(lengthComputable:布尔值，指示文件的长度是否可计算    loaded:加载的btyes数  total:要加载的总字节数  )

            //},

            'onUpload': function (filesToUpload) {  //在使用upload方法调用的上载操作期间触发一次  filesToUpload:需要上传的文件数

                if (filesToUpload < 1) {

                    layer_Msg("未选择任何文件");

                    return false;

                }

            },

            'onUploadFile': function (file) {   //每次启动的文件上载都会触发一次。file:正在上载的文件对象

                $('#file_upload').data('uploadifive').settings.formData = { id: id, point: point, type: type, oldFile: name };   //动态更改formData的值

            },

            'onUploadComplete': function (file, data) {   //对于完成的每个文件上载，触发一次。

                if (data) {

                    try {

                        data = JSON.parse(data);

                        if (data.result === true) {

                            name = data.msg;

                            layer_Msg("上传成功");

                        } else {

                            layer_Msg(data.msg);

                        }

                    } catch (e) {

                        alert(data);

                    }

                }

            },

            'onQueueComplete': function (uploads) {     //队列中的所有文件完成上传后触发。attempts:在上次上载操作中尝试的文件上载次数    successful:上次上载操作中成功上载文件的数量  errors:在上次上载操作中返回错误的文件上载数  count:从此UploadiFive实例上载的文件总数



            }

        });








    </script>

    <!--
        $('#file_upload').uploadifive({
        'auto': false,
        'checkScript': 'CheckExistsHandler.ashx',
        'formData': {
            'timestamp': '<% = DateTime.Now.ToLongTimeString()%>', 
            'token': '' 
        },
        'queueID': 'queue',
        'uploadScript': 'UploadHandler.ashx',
        'onUploadComplete': function (file, data) { console.log(data); }
    });   
    -->

</body>
</html>
