<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="CKEditor_v4_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>CKEditor 4 文章编辑测试</title>

    <link href="/Scripts/V1.5.3/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/V1.5.3/themes/icon.css" rel="stylesheet"/>
    <link href="/Styles/Base.css" rel="stylesheet"/>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/V1.5.3/jquery.easyui.min.js"></script>
    <script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>

    <script src="CKEditor/ckeditor.js"></script>
    <script src="CKEditor/config.js"></script>

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        
        #main { margin: 0 auto; width: 100%; }
        input[type="text"] { border: 1px solid #ccc; }

        .row-div { height: 23px;line-height: 23px;}
        .row-div, .row-float-div{ display: block; width: 100%;margin-bottom: 10px;}
        .row-div div, .row-float-div div { float: left; }
        .row-div span, .row-div input[type="text"], .row-float-div textarea { width: 100%; }

        .row-item{ width: 946px;}
        .row-label { width: 45px; text-align: right; vertical-align: middle; }
        .row-label span{font-weight: bold;}

        .datagrid-toolbar table { width: 100%; }
        .easyui-linkbutton{ width: 60px;}
        .cke_dialog_ui_input_file{ height: 125px!important;}  
    </style>

    <script type="text/javascript">

        var navigate = '<% = Request.QueryString["MapName"] %>';
        var width = 0, height = 0, winHeight = 0;
        var currentPage = 1, pageSize = 20;

        $(function() {
            width = HSP.Common.AvailWidth();
            height = HSP.Common.AvailHeight();

            winHeight = height > 850 ? 668 : 526;

            if (navigate.length == 0) navigate = "CKEditor 4 文章编辑测试";

            var obj = $('\
            <div id="layout" class="easyui-layout" style="height: ' + (height - 0) + 'px;width:' + width + 'px;">\
                <div region="center" border="false">\
                    <div id="articleGrid"></div>\
                </div>\
            </div>\
            ');

            obj.appendTo($('#main'));
            obj.layout();

            initGrid();
        });


        function initGrid() {

            $("#articleGrid").datagrid({
                title: navigate,
                height: (height - 0),
                fit: true,
                fitColumns: true,
                nowrap: false,
                striped: true,
                singleSelect: true,
                collapsible: false,
                pagination: true,
                rownumbers: true,
                pageSize: pageSize,
                pageList: [100, 50, 20],
                remoteSort: false,
                columns: [
                    [   //  Id, Title, Author, Source, Abstract, Content, CreateTime, c.Count
                        { field: 'Title', title: '标题', width: 360 },
                        { field: 'Author', title: '作者', width: 90 },
                        { field: 'Source', title: '来源', width: 90 },
                        {
                            field: 'CreateTime', title: '日期', width: 90, align: 'center',
                            formatter: function (value, rec, index) {
                                return value.toDateTimeString("yyyy-MM-dd HH:mm:SS");
                            }
                        },
                        {
                            field: 'Id',
                            title: '操作',
                            width: 75,
                            align: 'center',
                            formatter: function (value, rec, index) {
                                var html = '<a href="javascript:void(0);" onclick=ArticleView("' + value + '")><img src="/Images/view.png" alt="查看" title="查看"></a>&nbsp;&nbsp;';
                                html += '<a href="javascript:void(0);" onclick=ArticleEdit("' + value + '")><img src="/Images/edit.png" alt="修改" title="修改"/></a>&nbsp;&nbsp;';
                                html += '<a href="javascript:void(0);" onclick=ArticleDel("' + value + '")><img src="/Images/del.png" title="删除"></a>';
                                return html;
                            }
                        }
                    ]
                ]
                , toolbar: [
                    {
                        id: 'btnAdd',
                        text: '添加文章',
                        iconCls: 'icon-add',
                        handler: function () {
                            ArticleEdit();
                        }
                    }
                ]
            });

            // 追加搜索栏
            var searchDiv = $('\
                <td id="searchDiv" style="text-align: right;">\
                    <input id="articleTitle" type="text" placeholder="请输入标题..." title="请输入标题" style="width: 300px;" />\
                    <a href="javascript:void(0);" class="easyui-linkbutton" onclick="ArticleList();">查询</a>\
                </td>\
                ');

            $(".datagrid-toolbar table tr:first-child").append(searchDiv);
            $.parser.parse('.datagrid-toolbar');

            $('#btnAdd').attr('title', '添加内容详细');

            ArticleList();
        }

        //根据查询条件获得内容列表
        function ArticleList() {

            var title = $.trim($('#articleTitle').val());
            if (title == "请输入标题...") title = "";

            $('#articleGrid').datagrid('options').url = "/Handler/ArticleHandler.ashx?OPERATION=ARTICLELIST&title=" +
                escape(title) + "&rnd=" + (Math.random() * 10);

            $('#articleGrid').datagrid('getPager').pagination({ pageNumber: 1 });
            $('#articleGrid').datagrid('options').pageNumber = 1;
            $('#articleGrid').datagrid('reload');
            $('#articleGrid').datagrid('clearSelections');
        }


        // 添加/编辑文章
        function ArticleEdit(id) {

            var title = (!id ? '添加' : '编辑') + '文章内容';

            var obj = $('\
                <div style="overflow:hidden;">\
                    <div class="easyui-layout" style="height: ' + (winHeight - 37) + 'px;">\
                        <div region="center" border="false" style="padding:10px; overflow-x: hidden;">\
                            <div class="row-div">\
                                <div class="row-label"><span>标题：&nbsp;</span></div>\
                                <div class="row-item"><input id="txtTitle" type="text"></div>\
                            </div>\
                            <div class="row-div">\
                                <div class="row-label"><span>作者：&nbsp;</span></div>\
                                <div style="width:445px;"><input id="txtAuthor" type="text" placeholder="作者"></div>\
                                <div class="row-label" style="width:55px;"><span>&nbsp;来源：&nbsp;</span></div>\
                                <div style="width:445px;"><input id="txtSource" type="text" placeholder="来源"></div>\
                            </div>\
                            <div class="row-float-div">\
                                <div class="row-label"><span>内容：&nbsp;</span></div>\
                                <div style="width:950px;"><textarea id="txtContent" style="height:260px;" class="ckeditor"></textarea></div>\
                            </div>\
                            <div class="row-float-div" style="clear:both; padding-bottom:25px; padding-top:5px;">\
                                <div class="row-label"><span>摘要：&nbsp;</span></div>\
                                <div class="row-item"><textarea id="txtAbstract" style="height:45px;" placeholder="文章摘要"></textarea></div>\
                            </div>\
                        </div>\
                        <div region="south" split="true" border="false" style="height:40px; padding:0px; overflow: hidden;">\
                            <div id="label" style="margin:5px; float:left;"></div>\
                            <div class="button" style="margin:5px; float:right;">\
                                <a href="javascript:void(0);" id="btnPreview" class="easyui-linkbutton">预览</a>\
                                <a href="javascript:void(0);" id="btnSave" class="easyui-linkbutton">保存</a>\
                                <a href="javascript:void(0);" id="btnExit" class="easyui-linkbutton">取消</a>\
                            </div>\
                        </div>\
                    </div>\
                </div>\
                ');

            HSP.Common.Window(obj, title, { width: 1060, height: winHeight });
            $.parser.parse(obj);

            if (CKEDITOR.instances.myCKeditor) {
                //如果CKEDITOR已经创建存在则执行destroy                                
                CKEDITOR.instances.myCKeditor.destroy();
            }

            var editor = CKEDITOR.replace('txtContent');

            if (id) {
                $.getJSON('/Handler/ArticleHandler.ashx', { OPERATION: "GETARTICLEBYID", id: id, rnd: Math.random() }, function (rst) {
                    if (rst.success) {
                        $('#txtTitle').val(rst.Data.Title);
                        $('#txtAuthor').val(rst.Data.Author);
                        $('#txtSource').val(rst.Data.Source);
                        $('#txtAbstract').val(rst.Data.Abstract);
                        $('#txtContent').val(rst.Data.Content);

                        var waitCKEditorReady = function (data) {
                            if (editor.status == 'ready') {
                                editor.setData(data);
                            } else {
                                setTimeout(function () {
                                    waitCKEditorReady(data);
                                }, 20);
                            }
                        }
                        setTimeout(function () {
                            waitCKEditorReady(rst.Data.Content);
                        }, 50);

                        //editor.setData(rst.Data.Content);
                        //editor.updateElement(); //非常重要的一句代码
                    }
                });

                //$.ajax({
                //    url: '/Handler/ArticleHandler.ashx',
                //    type: 'GET',
                //    data: { OPERATION: "GETARTICLEBYID", id: id, rnd: Math.random() },
                //    success: function (rst) {
                //        //rst = eval(rst);
                //        if (rst.success) {
                //            $('#txtTitle').val(rst.Data.Title);
                //            $('#txtAuthor').val(rst.Data.Author);
                //            $('#txtSource').val(rst.Data.Source);
                //            $('#txtAbstract').val(rst.Data.Abstract);
                //            $('#txtContent').val(rst.Data.Contents);

                //            editor.setData(rst.Data.Notice);
                //            editor.updateElement(); //非常重要的一句代码
                //        } else {
                //            $.messager.alert({ title: "操作提示", msg: rst.Message, showType: "error" });
                //        }
                //    }
                //    , complete: function (xhr, errorText, errorType) {

                //        debugger;

                //        var p = "";

                //        alert("请求完成后");
                //    }
                //    , error: function(xhr, errorText, errorType) {
                //        alert("请求错误后");
                //    }
                //    , beforSend: function() {
                //        alert("请求之前");
                //    }
                //});
            }

            $('#btnSave').unbind('click').bind('click', function () {
                var txtTitle = $.trim($('#txtTitle').val());
                if (txtTitle.length > 32) {
                    $.messager.show({ title: "操作提示", msg: "标题长度不能超过32个字符！", timeout: 3000, showType: "fade" });
                } else {
                    var txtContent = editor.getData();
                    txtContent = txtContent.replace(/'/g, "''");

                    var txtAuthor = $.trim($('#txtAuthor').val());
                    var txtSource = $.trim($('#txtSource').val());
                    var txtAbstract = $.trim($('#txtAbstract').val());

                    var sendData = {
                        id: id,
                        OPERATION: (id) ? "ARTICLEEDIT" : "ARTICLESAVE",
                        title: encodeURI(txtTitle),
                        author: encodeURI(txtAuthor),
                        source: encodeURI(txtSource),
                        abstract: encodeURI(txtAbstract),
                        content: encodeURI(txtContent),
                        rnd: Math.random()
                    };
                    $.post('/Handler/ArticleHandler.ashx', sendData, function (data) {
                        if (data.success) {
                            $('#articleGrid').datagrid('reload');
                            obj.window('destroy');
                            $.messager.show({ title: "操作提示", msg: data.message, timeout: 3000, showType: "fade" });
                        } else {
                            $.messager.alert({ title: "操作提示", msg: data.message, showType: "error" });
                        }
                    });
                }
            });

            $('#btnExit').unbind('click').bind('click', function () {
                if (CKEDITOR.instances.myCKeditor) {
                    //如果CKEDITOR已经创建存在则执行destroy                                
                    CKEDITOR.instances.myCKeditor.destroy();
                }
                obj.window('destroy');
            });

            if (id) {
                $("#btnPreview").unbind('click').bind('click', function () { // 资讯预览
                    ArticleView(id);
                });
            } else {
                $("#btnPreview").css("display", "none");
            }
        }

        // 删除文章
        function ArticleDel(id) {
            //alert("删除文章");
            if (confirm("您确定要删除该内容信息吗？")) {
                $.getJSON('/Handler/ArticleHandler.ashx', { OPERATION: "ARTICLEDEL", id: id, rnd: Math.random() }, function (rst) {
                    if (rst.success) {
                        $.messager.show({ title: "操作提示", msg: rst.message, timeout: 3000, showType: "fade" });
                        $('#systemArticleGrid').datagrid('reload');
                    } else {
                        $.messager.alert({ title: "操作提示", msg: "删除内容信息失败！错误信息为：" + rst.message, showType: "error" });
                    }
                });
            }
        }

        // 预览文章
        function ArticleView(id) {
            Redirect("ArticlePreview.aspx?id=" + id);
        }

        /// <summary>
        ///     页面跳转
        /// </summary>
        function Redirect(url) {
            $.messager.progress({ title: "", msg: "正在加载页面...", text: "" });
            window.location.href = url;
        }

    </script>

</head>
<body>
<form id="form1" runat="server">
    <div id="main"></div>
</form>
</body>
</html>