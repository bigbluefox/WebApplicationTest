<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSprites.aspx.cs" Inherits="WebApplicationTest.CssSprites.CreateSprites" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title>图标精灵</title>
<link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
<link href="/Scripts/themes/icon.css" rel="stylesheet"/>
<link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet"/>
<link href="/Scripts/zTree/zTreeStyle.css" rel="stylesheet"/>
<link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
<link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>

<script src="/Scripts/jquery-1.12.4.min.js"></script>
<script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

<!--[if lt IE 9]>
    <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
<![endif]-->

<script src="/Scripts/jquery.easyui.min.js"></script>
<script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
<script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
<script src="/Scripts/zTree/jquery.ztree.core.js"></script>
<script src="/Scripts/uploadifive/jquery.uploadifive.min.js"></script>
<script src="/Scripts/Hsp.js"></script>
<script src="/Scripts/Hsp.Common.js"></script>

<style type="text/css">
    body { font: 13px Arial, Helvetica, Sans-serif; }

    img {
        border: 0;
        cursor: pointer;
        vertical-align: central;
    }

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

    input[type='button'], input[type='file'] { min-height: 30px; }

    .img16 { width: 16px; }

    .img32 { max-width: 32px; }

    .imgspan {
        height: 24px;
        line-height: 24px;
    }

    input[type='radio'], input[type='checkbox'] {
        height: 16px;
        margin-left: 2px;
        margin-top: 0px;
    }

    input[type='button'] { border: 1px solid #95b8e7 !important; }

    select, textarea, input[type='text'], input[type='button'], input[type='checkbox'], input[type='radio'] { vertical-align: middle; }

    </style>

<style type="text/css">
    .thumbnail { overflow: hidden; }

    .thumbnail div {
        display: block;
        float: left;
        width: 100%;
    }

    .thumbnail div:first-child { height: 48px; }

    .thumbnail div img,
    .thumbnail div span,
    .thumbnail div a { display: block; }

    .thumbnail div img,
    .thumbnail div span { float: left; }

    .thumbnail div a { float: right; }

    .thumbnail div img {
        max-height: 48px;
        max-width: 96px;
    }

    .thumbnail div img { vertical-align: central; }
</style>

<script type="text/javascript">

    var width = 0, height = 0;
    var imgWidth = 0, imgHeight = 0;
    var imgData;

    $(function() {

        width = HSP.Common.AvailWidth();
        height = HSP.Common.AvailHeight();

        // tabs的高度与宽度自适应处理
        $("#tabs").tabs({
            width: 'auto',
            height: (height - 66),
            onSelect: function(title, index) {
                TabSelect(title, index);
            }
        });

        $("#txtXml").css("height", (height - 118) + "px");
        $("#txtCss").css("height", (height - 118) + "px");

        //菜单图标选择
        $('#btnServerIcons').unbind('click').bind("click", function() {
            var onSelected = function(data) {
                if (data && data.length > 0) {

                    //alert(typeof data);
                    //alert(data.length + " * " + data[0].imgPath);
                    //$('#txtImg').val(data[0].imgPath);
                    //if (data.length == 1) {
                    //    $("#preview").attr("src", data[0].imgPath);
                    //} else {}

                    $(".preview").empty();
                    var imgs = '', imgData = data;
                    var tips = "图标别名，设为样式名称";
                    imgWidth = 0, imgHeight = 0;
                    $.each(data, function(i) {

                        if (Number(imgWidth) < Number(this.width)) imgWidth = this.width;
                        if (Number(imgHeight) < Number(this.height)) imgHeight = this.height;

                        //debugger;

                        var ext = this.imgName.replace(this.imgExt, "").replace("_", "-").toString();
                        if (ext.length > 0) ext = ext.toLowerCase();

                        imgs += '<div class="col-sm-9 col-md-6" id="div' + i + '">';
                        imgs += '<div class="thumbnail">';
                        imgs += '<div><img src="' + this.imgPath + '" alt="..."/><span>' + this.width + "X" + this.height + '</span><a href="#" onclick="ClearMe(' + i + ');">删除</a></div>';
                        imgs += '<div class="caption">' + this.imgName + '</div>';
                        imgs += '<div><input id="imgalias' + i + '" class="form-control" type="text" value="' + ext + '" title="' + tips + '" placeholder="' + tips + '"/></div>';

                        imgs += '<input type="hidden" id="imgname' + i + '" value="' + this.imgName + '"/>';
                        imgs += '<input type="hidden" id="imgpath' + i + '" value="' + this.imgPath + '"/>';
                        imgs += '<input type="hidden" id="imgwidth' + i + '" value="' + this.width + '"/>';
                        imgs += '<input type="hidden" id="imgheight' + i + '" value="' + this.height + '"/>';
                        imgs += '<input type="hidden" name="num" value="' + i + '"/>';

                        imgs += '</div>';
                        imgs += '</div>';

                    });

                    $(".preview").append(imgs);
                    $("#txtImgWidth").val(imgWidth);
                    $("#txtImgHeight").val(imgHeight);
                    $("body").data("imgData", imgData);
                }
            };
            var selectedData = [];
            //if ($('#txtImg').val() != null) {
            //    selectedData.push({ id: "", name: "" });
            //}
            HSP.Common.SelectIcon(selectedData, true, onSelected);

            $("body").data("imgData", []);
        });

        $("#Button1").unbind('click').bind("click", function() {

            //debugger;
            //alert(imgData.length);

            var data = $("body").data("imgData");
            if (data) {
                alert(data.length);
            }

        });

        // 保存设置
        $("#btnSettingsSave").unbind('click').bind("click", function() {
            SettingsSave();
        });

        // 处理样式
        $("#btnStyleProcessing").unbind('click').bind("click", function() {

            //debugger;
            //alert(imgData.length);

            //var data = $("body").data("imgData");
            //if (data) {
            //    alert(data.length);
            //}

            //SettingsSave();

            var val = $("#txtFileName").val();
            var url = "/Handler/IconsHandler.ashx?OPERATION=CSSPROCESS&name=" + val + "&rnd=" + (Math.random() * 10);
            $.get(url, function (rst, textStatus) {
                //alert(textStatus);
                //alert(rst.Xml);
                debugger;

                if (rst) {
                    //var xml = unescape(rst.Xml);
                    //var xml = decodeURIComponent(rst.Xml);
                    //xml = xml.replace(/\+/g, ' ');
                    //$("#txtXml").val(xml);
                }

            });


        });

        $.get('/Styles/Xml/CssFiles.xml', function(data) {

            //debugger;

            var sel = $("#selSettings");
            $(data).find("file").each(function() {
                var name = $(this).find('name').text();
                var desc = $(this).find('desc').text();
                sel.append($("<option value='" + name + "'>" + desc + "</option>"));
            });
        });

        // 配置文件选择变更事件
        $("#selSettings").bind("change", function() {
            var val = $(this).val();
            var txt = $('#selSettings option:selected').text();
            if (val && val.length > 0) {
                $("#txtFileName").val(val);
                $("#txtFileDesc").val(txt);

                $.get("/Handler/IconsHandler.ashx?OPERATION=GETCSSSETTINGS&name=" + val + "&type=0&rnd=" + (Math.random() * 10), function(rst) {
                    //debugger;
                    if (rst) {
                        $("input[name='imgDirection'][value=" + rst.direction + "]").attr("checked", true);
                        $("#txtLeft").val(rst.left);
                        $("#txtTop").val(rst.top);

                        $("#selImgType").val(rst.imgtype);
                        $("#txtFileName").val(rst.filename);
                        $("#txtFileDesc").val(rst.filedesc);
                        $("#txtImgWidth").val(rst.width);
                        $("#txtImgHeight").val(rst.height);

                        $(".preview").empty();
                        var imgs = '', tips = "图标别名，设为样式名称";
                        $.each(rst.files, function(i) {
                            //<name>AddFile_16x16.png</name>
                            //<alias>addfile_16x16</alias>
                            //<path>/Images/AddFile_16x16.png</path>
                            //<width>16</width>
                            //<height>16</height>

                            var alias = this.alias.replace("_", "-").toString();
                            if (alias.length > 0) alias = alias.toLowerCase();

                            imgs += '<div class="col-sm-9 col-md-6" id="div' + i + '">';
                            imgs += '<div class="thumbnail">';
                            imgs += '<div><img src="' + this.path + '" alt="..."/><span>' + this.width + "X" + this.height + '</span><a href="#" onclick="ClearMe(' + i + ');">删除</a></div>';
                            imgs += '<div class="caption">' + this.name + '</div>';
                            imgs += '<div><input id="imgalias' + i + '" class="form-control" type="text" value="' + alias + '" title="' + tips + '" placeholder="' + tips + '"/></div>';

                            imgs += '<input type="hidden" id="imgname' + i + '" value="' + this.name + '"/>';
                            imgs += '<input type="hidden" id="imgpath' + i + '" value="' + this.path + '"/>';
                            imgs += '<input type="hidden" id="imgwidth' + i + '" value="' + this.width + '"/>';
                            imgs += '<input type="hidden" id="imgheight' + i + '" value="' + this.height + '"/>';
                            imgs += '<input type="hidden" name="num" value="' + i + '"/>';

                            imgs += '</div>';
                            imgs += '</div>';

                        });

                        $(".preview").append(imgs);
                        $("body").data("imgData", rst);

                    } else {
                        //alert("查询图标数据异常，请检查并重试！");
                    }
                });

            } else {
                $("#txtFileName").val("");
                $("#txtFileDesc").val("");
            }
        });

        //var strHtml = ""; //初始化保存内容变量
        ////var url = '/Styles/icons.xml';
        //var url = '/Styles/Xml/CssFiles.xml';
        //$.ajax({
        //    url: url,
        //    dataType: 'xml',
        //    success: function (data) {

        //            debugger;

        //        //    var d = $(data)[0];

        //        //    var dd = d;
        //        //    var ddd = $(data)[0].childNodes[0].find("root");

        //        //var dddd = ddd;

        //        //var $strUser = $(data).find("User");
        //        //strHtml += "编号：" + $strUser.attr("id") + "<br>";
        //        //strHtml += "姓名：" + $strUser.children("name").text() + "<br>";
        //        //strHtml += "性别：" + $strUser.children("sex").text() + "<br>";
        //        //strHtml += "邮箱：" + $strUser.children("email").text() + "<hr>";

        //            var $strUser = $(data).find("file");
        //            strHtml += "名称：" + $strUser.children("name").text() + "<br>";
        //            strHtml += "描述：" + $strUser.children("desc").text() + "<br>";


        //        var s = strHtml;

        //        ////$("#Tip").html(strHTML); //显示处理后的数据
        //    }
        //});
    });

    function ClearMe(id) {
        var obj = $("#div" + id);
        obj.empty();
        obj.hide();
    }

    // 保存样式配置数据
    function SettingsSave(show) {
        //debugger;
        //alert(imgData.length);

        //var data = $("body").data("imgData");
        //if (data) {
        //    alert(data.length);
        //}
        if (show == undefined) {
            show = true;
        }

        var imgDirection = $("input:radio[name='imgDirection']:checked").val();
        var txtLeft = $("#txtLeft").val();
        var txtTop = $("#txtTop").val();

        var selImgType = $("#selImgType").val();
        var txtFileName = $("#txtFileName").val();
        var txtFileDesc = $("#txtFileDesc").val();
        var txtImgWidth = $("#txtImgWidth").val();
        var txtImgHeight = $("#txtImgHeight").val();

        var files = [];
        $.each($("input[name='num']"), function() {
            var num = this.value;
            var name = document.getElementById("imgname" + num).value;
            var alias = document.getElementById("imgalias" + num).value;
            var path = document.getElementById("imgpath" + num).value;
            var width = document.getElementById("imgwidth" + num).value;
            var height = document.getElementById("imgheight" + num).value;
            var file = { "name": name, "alias": alias, "path": path, "width": width, "height": height };
            files.push(file);
        });

        var data = { "direction": imgDirection, "left": txtLeft, "top": txtTop, "imgtype": selImgType, "filename": txtFileName, "filedesc": txtFileDesc, "width": txtImgWidth, "height": txtImgHeight, "files": files };
        var dataStr = HSP.Common.ObjToString(data);
        var params = { "Data": dataStr };
        var url = "/Handler/IconsHandler.ashx?OPERATION=SAVECSSSETTINGS&&rnd=" + (Math.random() * 10);

        $.post(url, params, function(data, status) {

            //debugger;
            //alert("Data: " + data + "\nStatus: " + status);

            if (data && data.IsSuccess) {
                if (show) {
                    $.messager.alert("操作提示", data.Message, "info");
                }
            } else {
                $.messager.alert("错误提示", data.Message, "error");
            }
        });
    }


    /// <summary>
    /// 选中Tab页
    /// </summary>
    /// <history> </history> 
    function TabSelect(title, index) {
        var tab = $('#tabs').tabs('getSelected');
        if (tab) {

            if (index === 0) {

            }
            if (index === 1) {
                ShowXml();
            }
            if (index === 2) {
                ShowCss();
            }
        }

        //alert(title + " * " + index);
    }

    function ShowXml() {

        var data = $("body").data("imgData");

        if (data) {
            //alert(data.length);

            SettingsSave(false);
            var val = $("#txtFileName").val();
            var url = "/Handler/IconsHandler.ashx?OPERATION=GETCSSSETTINGS&name=" + val + "&type=2&rnd=" + (Math.random() * 10);
            $.get(url, function(rst, textStatus) {
                //alert(textStatus);
                //alert(rst.Xml);
                //debugger;

                if (rst) {
                    //var xml = unescape(rst.Xml);
                    var xml = decodeURIComponent(rst.Xml);
                    xml = xml.replace(/\+/g, ' ');
                    $("#txtXml").val(xml);
                }

            });

            //debugger;
            // Assign handlers immediately after making the request,
            // and remember the jqxhr object for this request

            //var jqxhr = $.get(url, function (data) {


            //    alert("success");
            //})
            //  .done(function (data) {


            //      alert("second success");
            //  })
            //  .fail(function (msg) {
            //      alert("error");
            //  })
            //  .always(function (msg) {
            //      alert("finished");
            //  });

            //// Perform other work here ...

            //// Set another completion function for the request above
            //jqxhr.always(function (msg) {
            //    alert("second finished");
            //});


        }


    }

    function ShowCss() {

        var data = $("body").data("imgData");
        if (data) {
            alert(data.length);
        }
    }

</script>

</head>
<body>
<nav class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <button class="navbar-toggle collapsed" aria-expanded="false" aria-controls="navbar" type="button" data-toggle="collapse" data-target="#navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#">图标精灵</a>
        </div>
    </div>
</nav>

<div class="container" style="border: 0px solid #00bfff; margin-top: 60px; padding-top: 0px;">

    <div id="tabs" class="easyui-tabs" style="height: 550px">
        <div title="参数配置" style="padding: 10px">

            <div class="row" style="width: 98%;">
                <div class="col-sm-9 col-md-6">
                    <form id="form1" runat="server" class="form-horizontal">

                        <div class="form-group">
                            <label for="selImgType" class="col-sm-3 control-label">配置文件：</label>
                            <div class="col-sm-9">
                                <select class="form-control" id="selSettings">
                                    <option value="" selected="selected">新配置文件</option>
                                </select>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtFileName" class="col-sm-3 control-label">样式名称：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtFileName" placeholder="生成的样式文件名称">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtFileDesc" class="col-sm-3 control-label">样式描述：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtFileDesc" placeholder="生成的样式文件描述">
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">合成图片：</label>
                            <div class="col-sm-9">
                                <label class="radio-inline" style="height: 16px; line-height: 16px;">
                                    <input type="radio" name="imgDirection" id="imgDirection1" value="0" style="height: 16px; margin-top: 1px;" checked="checked"> 横向
                                </label>
                                <label class="radio-inline" style="height: 16px; line-height: 16px;">
                                    <input type="radio" name="imgDirection" id="imgDirection2" value="1" style="height: 16px; margin-top: 1px;"> 纵向
                                </label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLeft" class="col-sm-3 control-label">左边尺寸：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtLeft" value="0" placeholder="合成图片左边尺寸(px)">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtTop" class="col-sm-3 control-label">顶部尺寸：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtTop" value="0" placeholder="合成图片顶部尺寸(px)">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="selImgType" class="col-sm-3 control-label">图片类型：</label>
                            <div class="col-sm-9">
                                <select class="form-control" id="selImgType">
                                    <option value="jpg">JPG 图片</option>
                                    <option value="png">PNG 图片</option>
                                    <option value="gif">GIF 图片</option>
                                </select>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtImgWidth" class="col-sm-3 control-label">图标宽度：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtImgWidth" placeholder="图标宽度尺寸(px)">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtImgHeight" class="col-sm-3 control-label">图标高度：</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="txtImgHeight" placeholder="图标高度尺寸(px)">
                            </div>
                        </div>

                        <div class="form-group" style="display: none;">
                            <label for="exampleInputFile" class="col-sm-3 control-label">本地文件：</label>
                            <div class="col-sm-9">
                                <input type="file" id="exampleInputFile">
                                <p class="help-block" style="display: none;">Example block-level help text here.</p>
                            </div>
                        </div>

                        <%--<asp:FileUpload ID="FileUpload1" runat="server"/>--%>
                        <%--<br/>--%>
                        <%--<asp:Button ID="btnServerIcons" runat="server" Text="选择服务器图标文件" CssClass="btn btn-primary"/>--%>
                        <%--<input id="Button1" type="button" value="button" class="btn btn-primary"/>--%>

                        <div class="form-group">
                            <div class="col-sm-12" style="text-align: center;">
                                <input id="btnServerIcons" type="button" value="选择服务器图标文件" class="btn btn-primary"/>
                                <input id="Button1" type="button" value="检查数组" class="btn btn-primary"/>
                                <input id="btnSettingsSave" type="button" value="保存设置" class="btn btn-primary"/>
                                <input id="btnStyleProcessing" type="button" value="处理样式" class="btn btn-primary"/>
                            </div>
                        </div>

                        <%--<div class="preview" style="width: 100%;"></div>--%>

                        <!--
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon" id="sizing-addon1">图标高度：</span>
                            <input type="text" class="form-control" placeholder="Username" aria-describedby="sizing-addon1">
                        </div>

                        <br/>
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon" id="Span1">图标宽度：</span>
                            <input type="text" class="form-control" placeholder="Username" aria-describedby="sizing-addon1">
                        </div>
                        <br/>

                        <div class="form-group">
                            <label for="exampleInputEmail1">Email address</label>
                            <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <label for="exampleInputPassword1">password</label>
                            <input type="password" class="form-control" id="exampleInputPassword1" placeholder="password">
                        </div>
                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="text" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="password" placeholder="password">
                        </div>

                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="text" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="password" placeholder="password">
                        </div>
                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="text" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <label>dddddddddd</label>
                            <input class="form-control" type="password" placeholder="password">
                        </div>
                        //-->

                    </form>

                </div>

                <div class="col-sm-9 col-md-6" style="border: 0px solid #00bfff;">
                    <div class="preview" style="width: 100%;"></div>
                </div>

            </div>

        </div>
        <div title="XML" style="padding: 10px">
            <textarea id="txtXml" class="form-control" cols="20" rows="2" style="width: 100%;"></textarea>
        </div>
        <div title="CSS" style="padding: 10px">
            <textarea id="txtCss" class="form-control" cols="20" rows="2" style="width: 100%;"></textarea>
        </div>
    </div>

</div>

</body>
</html>