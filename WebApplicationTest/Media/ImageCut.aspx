<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/Page.Master" AutoEventWireup="true" CodeBehind="ImageCut.aspx.cs" Inherits="WebApplicationTest.Media.ImageCut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图片切片调整测试
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <%--<link href="../Scripts/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    <link href="/Scripts/Jcrop/css/main.css" rel="stylesheet"/>
    <link href="/Scripts/Jcrop/css/demos.css" rel="stylesheet"/>
    <link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet"/>
    <link href="/Scripts/Jcrop/css/jquery.Jcrop.css" rel="stylesheet"/>

    <script src="/Scripts/uploadifive/jquery.uploadifive.min.js"></script>
    <script src="/Scripts/Jcrop/js/jquery.Jcrop.js"></script>
    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        /*body {
            font: 13px Arial, Helvetica, Sans-serif;
            overflow: hidden;
        }*/

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-size: 100%;
        }

        body {
            background-color: #ffffff;
            color: #333333;
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            /*line-height: 20px;*/
            margin: 0;
        }

        img { cursor: pointer; }

        input[type='button'], input[type='file'] { cursor: pointer; }

        .uploadifive-button {
            float: left;
            margin-right: 10px;
        }

        #queue {
            border: 1px solid #E5E5E5;
            height: 177px;
            margin-bottom: 10px;
            overflow: auto;
            padding: 0 3px 3px;
            width: 600px;
        }

        input[type='button'] { height: 30px; }

        #imgdiv {
            display: none;
            margin: -10px;
        }

        #preview {
            /*max-width: 886px;*/
            display: block;
            float: left;
        }

        /*#preview,*/

        /* Apply these styles only when #result has
           been placed within the Jcrop widget */

        .jcrop-holder #result-pane {
            -moz-border-radius: 6px;
            -moz-box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);
            -webkit-border-radius: 6px;
            -webkit-box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);
            background-color: white;

            border: 1px rgba(0, 0, 0, .4) solid;
            border-radius: 6px;
            box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);

            display: block;
            float: right;
            /*right: -280px;*/
            padding: 6px;
            position: absolute;
            right: 10px;
            /*z-index: 2000;*/
            top: 10px;
        }

        /* The Javascript code will set the aspect ratio of the crop
           area based on the size of the thumbnail preview,
           specified here */

        /*#result{
            overflow: hidden;
            position: absolute;
            top: 0px;}
        */
        #result .result-container {
            overflow: hidden;
            position: absolute;
            top: 0px;
        }

        #btnOkCut {
            border: 1px solid #a4bed4 !important;
            overflow: hidden;
            position: absolute;
        }

        .inline-labels, .msg-container {
            /*display: none;*/
            overflow: hidden;
            position: absolute;
        }

        .inline-labels input {
            height: 16px;
          width: 42px;
        }

        input[type='radio'], input[type='checkbox']{ margin: 0;height: 21px;}
        .radio, .checkbox{ padding-left: 0;}

    </style>

    <script type="text/javascript">
        var ServiceUrl = "<%= FileUrl %>"; //根目录
        var UpFiletype = "<% = UpFileTypes %>"; //默认从配置文件获取文件类型，实际是获取别人传入的值
        var VD = "<% = VirtualDirectory %>"; // 虚拟目录

        var TypeID = "<% = TypeId %>"; //分类ID
        var GroupID = "<% = GroupId %>"; //分组ID

        //jQuery(function ($) {
        //    // How easy is this??
        //    $('#target').Jcrop();
        //});

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <div class="row">

        <div class="page-header">
            <ul class="breadcrumb first">
                <li>
                    <a href="../index.html">Jcrop</a> <span class="divider">/</span></li>
                <li>
                    <a href="../index.html">Demos</a> <span class="divider">/</span></li>
                <li class="active">Aspect Ratio with Preview Pane 图片尺寸：<span id="cutWidth"></span>*<span id="cutHeight"></span></li>
            </ul>
            <h1>jQuery Jcrop 图像裁剪 按高宽比例预览裁剪效果</h1>
        </div>

        <form id="form1" runat="server">
            <div id="queue"></div>
            <input id="file_upload" name="file_upload" type="file" multiple="multiple"/>
            <input id="btnUpload" type="button" value="上传" onclick="javascript: upload();"/>
        </form>

        <div id="imgdiv">
            <img src="..." id="preview" class="rounded" alt="Preview">
            <div id="result">
                <div class="result-container">
                    <img src="..." class="jcrop-preview" alt="Result"/>
                </div>
                <input type="button" value="剪切并保存" id="btnOkCut"/>

                <div class="inline-labels">
                    <label>X1 <input type="text" size="4" id="x1" name="x1"/></label>
                    <label>Y1 <input type="text" size="4" id="y1" name="y1"/></label><br/>
                    <label>X2 <input type="text" size="4" id="x2" name="x2"/></label>
                    <label>Y2 <input type="text" size="4" id="y2" name="y2"/></label><br/>
                    <label>W&nbsp; <input type="text" size="4" id="w" name="w"/></label>
                    <label>H&nbsp; <input type="text" size="4" id="h" name="h"/></label>
                </div>
                <div class="msg-container">
                    <div class="span3" id="interface">
                      <label class="checkbox">
                        <input type="checkbox" id="can_size" /> 允许调整裁剪框尺寸
                      </label>
                      <label class="checkbox">
                        <input type="checkbox" id="size_lock" /> 设置最大尺寸和最小尺寸 
                      </label>
                    </div>
                </div>

            </div>

        </div>
    </div>
</div>

<div class="clearfix"></div>

<style type="text/css">
    /*.jcrop-preview{ float: right;}*/
    /*.container{ border: 1px solid #00bfff;}*/
    /*.row{ border: 1px solid #;}*/
    /*#imgdiv { margin: -10px; }*/
    /*#result label {
             position: absolute;
             top: 200px;
            left: 1200px;
        }*/
    
</style>

<script type="text/javascript">

    var imgWidth = 1600, imgHeight = 400;
    var preWidth = 160, preHeight = 40;

    $("#result .result-container").css("width", preWidth + "px");
    $("#result .result-container").css("height", preHeight + "px");

    var top = null, left = null, cutLength = null;

    // Create variables (in this scope) to hold the API and image size
    var jcrop_api,
        boundx,
        boundy,

        // Grab some information about the preview pane
        $preview = $('#preview'),
        $result = $('#result'),
        $pcnt = $('#result .result-container'),
        $pimg = $('#result .result-container img'),
        xsize = preWidth, //$pcnt.width(),
        ysize = preHeight; //$pcnt.height();

    $(function() {

        var width = HSP.Common.AvailWidth();
        var height = HSP.Common.AvailHeight();
        //$("#preview label").html(navigator.userAgent);

        //$("#result label").html(navigator.userAgent);
        //alert($(".container").width());

        $("#cutWidth").html(preWidth);
        $("#cutHeight").html(preHeight);

        width = $(".container").width() + 50;
        $("#btnOkCut").css({ "top": (preHeight + 5) + "px", "left": (width - preWidth - 10) + "px", "width": preWidth + "px" });

        $(".inline-labels").css({ "top": (preHeight + 35 + 5) + "px", "left": (width - preWidth - 10) + "px", "width": preWidth + "px" });
        $(".msg-container").css({ "top": (preHeight + 150 + 5) + "px", "left": (width - preWidth - 10) + "px", "width": preWidth + "px" });

        if (HSP.Browser.IS_IE11 || HSP.Browser.IS_IE) {
            height -= 16;
            //alert(height);
            // Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; rv:11.0) like Gecko 
        }

        if (HSP.Browser.IS_FF) {
            height -= 1;
        }

        if (HSP.Browser.IS_GC) {
            height += 41;
        }

        //auto 是否自动上传，默认true 
        //uploadScript 上传路径 
        //fileObjName file文件对象名称 
        //buttonText 上传按钮显示文本 
        //queueID 进度条的显示位置 
        //fileType 上传文件类型 
        //multi 是否允许多个文件上传，默认为true 
        //fileSizeLimit 允许文件上传的最大尺寸 
        //uploadLimit 一次可以上传的最大文件数 
        //queueSizeLimit 允许队列中存在的最大文件数 
        //removeCompleted 隐藏完成上传的文件，默认为false 
        //方法 作用 
        //onUploadComplete 文件上传成功后执行 
        //onCancel 文件被删除时触发 
        //onUpload 开始上传队列时触发 
        //onFallback HTML5 API不支持的浏览器触发

        $("#file_upload").uploadifive({
            'auto': false,
            'fileSizeLimit': 0, // The maximum upload size allowed in KB.  This option also accepts a unit.  If using a unit, the value must begin with a number and end in either KB, MB, or GB.  Set this option to 0 for no limit.
            'fileObjName': 'fileData',
            'buttonText': "选择文件",
            'buttonClass': 'btn btn-primary',
            'removeCompleted': true,
            //'checkScript': '/Handler/CheckExistsHandler.ashx?rnd=' + (Math.random() * 10),
            'uploadScript': '/Handler/UploadHandler.ashx?rnd=' + (Math.random() * 10), //这里是通过MVC里的FileUpload控制器里的Upload 方法  
            'onUploadComplete': function(file, data) { // 文件上传成功后执行 

                //debugger;
                //console.log(data);

                var flag = "上传成功", msg = "";
                var arr, ext = "", url = "";
                if (data.length > 0) {
                    arr = data.split('*');
                    url = decodeURIComponent(arr[1]);
                    flag = arr[0] == "0" ? "<span style=\"color:red;\">上传失败</span>" : "上传成功";
                    msg = arr.length > 1 ? (arr[0] == "0" ? arr[1] : "") : "";
                }

                if (file.name.length > 0) {
                    arr = file.name.split('.');
                    ext = arr.length > 1 ? arr[arr.length - 1] : "";
                }

                if (url.length > 0) {
                    url = VD + url;
                    $("#form1").hide();
                    $("#imgdiv").show();
                    $preview.attr("src", url);
                    $pimg.attr("src", url);

                    $preview.css("max-width", (width - preWidth - 15) + "px");
                    //$preview.css("max-height", height);

                    //$("#result .result-container").css("width", preWidth + "px");
                    //$("#result .result-container").css("height", preHeight + "px");
                    $("#result .result-container").css("left", (width - preWidth - 10) + "px");

                    xsize = $pcnt.width();
                    ysize = $pcnt.height();

                    //console.log('init', [xsize, ysize]);
                    $preview.Jcrop({
                        onChange: updatePreview,
                        onSelect: updatePreview,
                        aspectRatio: xsize / ysize
                    }, function() {
                        // Use the API to get the real image size
                        var bounds = this.getBounds();
                        boundx = bounds[0];
                        boundy = bounds[1];
                        // Store the API in the jcrop_api variable
                        jcrop_api = this;

                        // Move the preview into the jcrop container for css positioning
                        $result.appendTo(jcrop_api.ui.holder);
                    });

                }

                //SELECT     TOP (200) FileId, TypeId, GroupId, fileName, FileDesc, FileExt, FileSize, ContentType, FileUrl
                //, FilePath, Params, Creator, CreatorName, CreateTime, Modifier, ModifierName, ModifyTime
                //FROM         Attachments

                //var rows = $('#tbFileList').datagrid('getRows');
                //var row = { "RowNumber": (rows.length + 1), "TypeName": "工作流", "FileName": file.name, "FileSize": Math.round(file.size / 1024.0), "FileExt": "." + ext, "ContentType": file.type, "CreateTime": new Date(), "flag": flag, "message": msg };
                //rows.push(row);

                //$('#tbFileList').datagrid('loadData', { "total": rows.length, "rows": rows });
                //$('#tbFileList').datagrid('loaded');
            },
            'queueID': 'queue',
            'multi': true,
            onFallback: function() {
                // HTML5 API不支持的浏览器触发, 很重要
                alert("该浏览器无法使用上传功能!");
            },
            'onAddQueueItem': function(file) {
                //alert('The file ' + file.name + ' was added to the queue!'); // 有效
            },
            'onUploadFile': function(file) {
                //alert('The file ' + file.name + ' is being uploaded.'); // 无效
            },
            'onCheck': function(file, exists) {
                if (exists) {
                    alert('文件 ' + file.name + ' 在服务器上已经存在！'); // 有效
                }
            },
            'onQueueComplete': function(uploads) {
                //alert(uploads.successful + ' files were uploaded successfully.'); // 有效
            },
            'onSelect': function(queue) {
                //alert(queue.queued + ' files were added to the queue.'); // 有效
            },
            'onUpload': function(filesToUpload) {
                //alert(filesToUpload + ' files will be uploaded.'); // 有效，插入前提示
            },
            'formData': { 'ID': "", 'GID': "", 'TID': "" } //由于这里初始化时传递的参数，当然就这里就只能传静态参数了  
        });


        var url = "/FileService/images/8.jpg";

        $("#form1").hide();
        $("#imgdiv").show();
        $preview.attr("src", url);
        $pimg.attr("src", url);

        $preview.css("max-width", (width - preWidth - 15) + "px");
        //$preview.css("max-height", height);

        xsize = $pcnt.width();
        ysize = $pcnt.height();

        //$("#result .result-container").css("width", preWidth + "px");
        //$("#result .result-container").css("height", preHeight + "px");
        $("#result .result-container").css("left", (width - preWidth - 10) + "px");
        //$("#result label").css("left", (width - preWidth - 10) + "px");

        //$("#result label").html(xsize + " * " + ysize);

        //console.log('init', [xsize, ysize]);
        $preview.Jcrop({
            onChange: updatePreview,
            onSelect: updatePreview,
            aspectRatio: xsize / ysize,
            setSelect: [0, 0, preWidth, preHeight]
        }, function() {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;

            jcrop_api.animateTo([100, 100, 400, 300]);

            // Setup and dipslay the interface for "enabled"
            $('#can_size').attr('checked', 'checked');
            $('#size_lock').attr('checked', false);


            // Move the preview into the jcrop container for css positioning
            $result.appendTo(jcrop_api.ui.holder);
        });

        $("#btnOkCut").click(function() {
            if (top == null || top == undefined) {
                alert("未剪切图片无法保存：请将鼠标移至图片，左键单击并拖拽！");
                return;
            }
            var params = { top: top, left: left, cutLength: cutLength, imgPath: '' };
            $.ajax({
                url: '/SystemMgmt/UserMgmt/CutPhotoAndSave?rnd=' + (Math.random() * 16),
                type: 'Post',
                data: params,
                success: function(data) {
                    if (data) {
                        if (data.IsSuccess == true) {
                            //window.parent.closeImageFileWin();
                        } else {
                            alert(data.Message);
                        }
                    }
                }
            });
        });


        $('#can_size').change(function (e) {
            jcrop_api.setOptions({ allowResize: !!this.checked });
            jcrop_api.focus();
        });

        $('#size_lock').change(function (e) {
            jcrop_api.setOptions(this.checked ? {
                minSize: [350, 350],
                maxSize: [350, 350]
            } : {
                minSize: [0, 0],
                maxSize: [0, 0]
            });
            jcrop_api.focus();
        });


    });

    function upload() {
        var myid = "", gid = "1234", tid = "64C64907-F110-4A41-9DAF-5532A5A135FB";
        $('#file_upload').data('uploadifive').settings.formData = { 'ID': myid, 'GID': gid, 'TID': tid }; //动态更改formData的值
        $('#file_upload').uploadifive('upload'); //上传
    }

    // Simple event handler, called from onChange and onSelect
    // event handlers, as per the Jcrop invocation above
    function showCoords(c) {
        //$('#x1').val(c.x);
        //$('#y1').val(c.y);
        //$('#x2').val(c.x2);
        //$('#y2').val(c.y2);
        //$('#w').val(c.w);
        //$('#h').val(c.h);

        //$("#result label").html(c.w + " * " + c.h);
    };

    function clearCoords() {
        //$('#coords input').val('');
        $("#result label").html("");
    };

    function updatePreview(c) {
        if (parseInt(c.w) > 0) {
            var rx = xsize / c.w;
            var ry = ysize / c.h;

            $pimg.css({
                width: Math.round(rx * boundx) + 'px',
                height: Math.round(ry * boundy) + 'px',
                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                marginTop: '-' + Math.round(ry * c.y) + 'px'
            });

            $('#x1').val(c.x);
            $('#y1').val(c.y);
            $('#x2').val(c.x2);
            $('#y2').val(c.y2);
            $('#w').val(c.w);
            $('#h').val(c.h);
        }

        //$("#result label").html(boundx + " * " + boundy);
    };

    //简单的事件处理程序，响应自onChange,onSelect事件，按照上面的Jcrop调用
    //function updatePreview(coords) {
    //    if (parseInt(coords.w) > 0) {
    //        //计算预览区域图片缩放的比例，通过计算显示区域的宽度(与高度)与剪裁的宽度(与高度)之比得到
    //        var rx = $pimg.width() / coords.w;
    //        var ry = $pimg.height() / coords.h;
    //        //通过比例值控制图片的样式与显示
    //        $pimg.css({
    //            width: Math.round(rx * $preview.width()) + "px", //预览图片宽度为计算比例值与原图片宽度的乘积
    //            height: Math.round(rx * $preview.height()) + "px", //预览图片高度为计算比例值与原图片高度的乘积
    //            marginLeft: "-" + Math.round(rx * coords.x) + "px",
    //            marginTop: "-" + Math.round(ry * coords.y) + "px"
    //        });
    //        top = coords.y;
    //        left = coords.x;
    //        cutLength = coords.w;
    //    }
    //}

</script>

</asp:Content>