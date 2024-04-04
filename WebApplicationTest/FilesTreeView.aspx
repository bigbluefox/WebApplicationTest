<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilesTreeView.aspx.cs" Inherits="WebApplicationTest.FilesTreeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>服务器制定目录文件查询</title>
<link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
<link href="Scripts/themes/icon.css" rel="stylesheet"/>
<link href="Scripts/themes/mobile.css" rel="stylesheet"/>
<link href="Styles/zTreeStyle/zTreeStyle.css" rel="stylesheet"/>
<link href="Styles/main.css" rel="stylesheet"/>

<%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
<script src="Scripts/jquery-1.12.4.min.js"></script>
<script src="Scripts/jquery-migrate-1.4.1.min.js"></script>
<script src="Scripts/jquery.easyui.min.js"></script>
<script src="Scripts/jquery.easyui.mobile.js"></script>
<script src="Scripts/locale/easyui-lang-zh_CN.js"></script>
<script src="Scripts/jquery.ztree.core.min.js"></script>
<script src="Scripts/jquery.jqprint-0.3.js"></script>
<script src="Scripts/jQuery.print.js"></script>
<script src="Scripts/Hsp.js"></script>
<script src="Scripts/Hsp.Common.js"></script>
    
    <script src="Scripts/jquery.printFinal.js"></script>
    <script src="Scripts/LodopFuncs.js"></script>

<style type="text/css">
    * { font-family: Arial, "Microsoft YaHei", 微软雅黑, "MicrosoftJhengHei", 华文细黑, STHeiti, MingLiu, Tahoma; }

    #main {
        margin: 0 auto;
        width: 100%;
    }

        @media print {
        .noprint { display: none }
    }
</style>

<script type="text/javascript">

    var width = 0, height = 0;
    var rootPath = "<% = RootPath %>";

    $(function() {
        width = HSP.Common.AvailWidth() - 16;
        height = HSP.Common.AvailHeight() - 16;

        $('#panel').panel({
            width: width,
            height: height,
            title: '标准体系文档导入',
            tools: [
                {
                    iconCls: 'icon-add',
                    handler: function() { alert('new') }
                }, {
                    iconCls: 'icon-print',
                    handler: function() {
                        //alert('print') 
                        ////$("body").jqprint();

                        //jQuery('body').print();

                        //$("body").printArea();

                        //doPrint("打印预览...");

                        printPreview();
                    }

                }, {
                    iconCls: 'icon-save',
                    handler: function() { alert('save') }
                }
            ]
        });

        //showFileList();
        //showDocList();

        loadFileTree();

        // 体系文件导入
        $('#btnImport').click(function() {
            var txtSourcePath = $('#txtSourcePath').val();
            var txtTargetPath = $('#txtTargetPath').val();

            if (txtSourcePath.length == 0) {
                alert("请选择要导入的体系文件所在目录！");
                return false;
            }
            if (txtTargetPath.length == 0) {
                alert("请选择导入的体系文件进入的目录！");
                return false;
            }
        });

        // 体系文件清空
        $('#btnEmpty').click(function() {
            //if (confirm("您确定要清空标准体系分类、目录及文件数据吗？")) {
            //    //return false;
            //}

            $.messager.confirm('操作确认', '您确定要清空标准体系分类、目录及文件数据吗？', function(r) {
                if (r) {
                    alert('confirmed: ' + r);
                }
            });

        });

        // <input id="txtSourcePath" type="hidden" /><input id="txtTargetPath" type="hidden" />
    });

    // 增加页面打印功能，Tli, 2017-01-13
    function printPreview() {

        var bodyHtml = window.document.body.innerHTML;
        var printhtml = bodyHtml;
            //topic.window.document.body.innerHTML;
        printhtml = '<OBJECT classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WindowPrint" name="WindowPrint" width="0"></OBJECT>' + printhtml;
        window.document.body.innerHTML = printhtml;
        WindowPrint.execwb(7, 1); // 发布以后不能使用



        //$("body").jqprint();
        //jQuery('body').print();

        window.document.body.innerHTML = bodyHtml;
    }

    function doPrint1() {
        var myDoc = {
            documents: document,
            /*
             要打印的div 对象在本文档中，控件将从本文档中的 id 为 'page1' 的div对象，
             作为首页打印id 为'page2'的作为第二页打印            */
            copyrights: '杰创软件拥有版权  www.jatools.com' // 版权声明,必须   
        };
        document.getElementById("jatoolsPrinter").print(myDoc, false); // 直接打印，不弹出打印机设置对话框 
    }

    function doPrint2(how) {
        var myDoc = {
            documents: document,
            copyrights: '杰创软件拥有版权  www.jatools.com'
        };
        var jatoolsPrinter = document.getElementById("jatoolsPrinter");
        if (how == '打印预览...')
            jatoolsPrinter.printPreview(myDoc); // 打印预览
        else if (how == '打印...')
            jatoolsPrinter.print(myDoc, true); // 打印前弹出打印设置对话框
        else
            jatoolsPrinter.print(myDoc, false); // 不弹出对话框打印
    }



    // 文件列表
    function showFileList() {
        $('#fileList').datagrid({
            fit: true,
            fitColumns: true,
            nowrap: false,
            striped: true,
            singleSelect: true,
            collapsible: true,
            pagination: false,
            rownumbers: true,
            url: '/Handler/FileHandler.ashx?OPERATION=FILEPROCESS&rnd=' + (Math.random() * 16),
            onClickRow: onClickRow,
            columns: [
                [
                    {
                        field: 'FileName',
                        title: '名称',
                        width: 150,
                        halign: 'center',
                        formatter: function(val, row, index) {
                            return row.FileType == 0 ? '<span style="color:red;">' + val + '</span>' : val;
                        }
                    },
                    {
                        field: 'FileType',
                        title: '类型',
                        width: 45,
                        halign: 'center',
                        align: 'center',
                        formatter: function(val, row, index) {
                            return val == 0 ? '文件夹' : '文件';
                        }
                    },
                    {
                        field: 'Extension',
                        title: '扩展名',
                        width: 45,
                        halign: 'center',
                        align: 'center',
                        formatter: function(val, row, index) {
                            return val.replace('.', '');
                        }
                    },
                    {
                        field: 'FileSize',
                        title: '大小',
                        width: 60,
                        halign: 'center',
                        align: 'right',
                        formatter: function(val, row, index) {
                            return row.FileType == 0 ? '' : parseFloat(val).toLocaleString();
                        }
                    }
                ]
            ]
        });
    }

    // 单击datagrid单元格事件
    function onClickRow(index, row) {
        //$('#typeId').val(row.id);
        //$('#taskName_txt').val("");＞

        //alert(row.FileName + " * " + row.FullName);href=\"javascript:void(0);\" 

        //debugger;

        if (row.FileType == 0) {

            var p = row.FullName;

            var path = $("#path").html();
            path += " ＞ " + "<a href=\"javascript:void(0);\" onclick=\"javascript:getGridList('" + escape(row.FullName.encodeQuotes()) + "');\">" + row.FileName + "</a>";
            $("#path").html(path);

            getGridList(row.FullName);

            //var url = '/Handler/FileHandler.ashx?OPERATION=FILEPROCESS&rootPath=' + escape(row.FullName.encodeQuotes());
            //getGridList($('#fileList'), url);
        }

        //var rows = $('#docList').datagrid("getRows");
        //rows.push(row);

        //$('#docList').datagrid('loading');
        //$('#docList').datagrid('loadData', { "total": rows.length, "rows": rows });
        //$('#docList').datagrid('loaded');
    }

    //function name(parameters) {
    //}

    // 体系文件目录列表查询
    function getGridList(path) {

        if (path == null || path == undefined || path == "") {
            path = rootPath;
        }

        //alert(path);

        var url = '/Handler/FileHandler.ashx?OPERATION=FILEPROCESS&rootPath=' + path;
        var obj = $('#fileList');

        obj.datagrid('options').url = url + '&rnd=' + (Math.random() * 16);
        obj.datagrid('getPager').pagination({ pageNumber: 1 });
        obj.datagrid('options').pageNumber = 1;
        obj.datagrid('reload');
    }

    // 文档列表
    function showDocList() {
        $('#docList').datagrid({
            fit: true,
            fitColumns: true,
            nowrap: false,
            striped: true,
            singleSelect: true,
            collapsible: true,
            pagination: false,
            rownumbers: true,
            //url: '/Handler/FileHandler.ashx?OPERATION=FILEPROCESS&rnd=' + (Math.random() * 16),
            //onClickRow: onClickRow,
            columns: [
                [
                    {
                        field: 'FileName',
                        title: '名称',
                        width: 150,
                        halign: 'center',
                        formatter: function(val, row, index) {
                            return row.FileType == 0 ? '<span style="color:red;">' + val + '</span>' : val;
                        }
                    },
                    {
                        field: 'FileType',
                        title: '类型',
                        width: 45,
                        halign: 'center',
                        align: 'center',
                        formatter: function(val, row, index) {
                            return val == 0 ? '文件夹' : '文件';
                        }
                    },
                    {
                        field: 'Extension',
                        title: '扩展名',
                        width: 45,
                        halign: 'center',
                        align: 'center',
                        formatter: function(val, row, index) {
                            return val.replace('.', '');
                        }
                    },
                    {
                        field: 'FileSize',
                        title: '大小',
                        width: 60,
                        halign: 'center',
                        align: 'right',
                        formatter: function(val, row, index) {
                            return row.FileType == 0 ? '' : parseFloat(val).toLocaleString();
                        }
                    }
                ]
            ]
        });
    }


    // 加载目录树
    function loadFileTree() {
        $.fn.zTree.init($("#fileTree"), {
            view: {
                selectedMulti: false,
                showLine: false
            },
            async: {
                enable: true,
                url: "/Handler/FileHandler.ashx?OPERATION=ZTREEDATA&rnd=" + (Math.random() * 10),
                autoParam: ["id", "name", "level", "pId"],
                otherParam: { "enterpriseId": "icon" },
                dataFilter: function(treeId, parentNode, childNodes) {
                    if (!childNodes) return null;
                    for (var i = 0, l = childNodes.length; i < l; i++) {
                        childNodes[i].name = childNodes[i].name == null ? "" : childNodes[i].name.replace(/\.n/g, '.');
                    }
                    return childNodes;
                }
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: function(event, treeId, node) {
                    if (node.isParent) {
                        $("#lblSource").html(node.name);
                        $("#txtSourcePath").val(node.id);
                    } else {
                        $("#lblSource").html("");
                        $("#txtSourcePath").val("");
                    }

                    //getDocumentList(node.id);txtSourcePath
                    //$("#path").html("根路径：" + "id=" + node.id + ", name=" + node.name + ", pId=" + node.pId + ", level=" + node.level + ", isParent=" + node.isParent);
                    //alert("id=" + node.id + ", name=" + node.name + ", pId=" + node.pId + ", level=" + node.level);
                },

                onExpand: function(event, treeId, node) {
                    //$("#path").html("根路径：" + "id=" + node.id + ", name=" + node.name + ", pId=" + node.pId + ", level=" + node.level + ", isParent=" + node.isParent);

                    //alert("id=" + node.id + ", name=" + node.name + ", pId=" + node.pId + ", level=" + node.level);
                    // 有新的节点展开时，为每个节点重新绑定文档拖拽进入的事件
                    //    BindDocumentCatalogDrop();
                },
                beforeAsync: function(treeId, node) {
                    //$("#fileTree").html("<img src='/Content/images/loading.gif' id='imgloading'/>");
                    return true;
                },
                onAsyncSuccess: function(event, treeId, node, msg) {
                    $("#fileTree").find('#imgloading').remove();
                    //var data = $.parseJSON(msg);
                    //var treeObj = $.fn.zTree.getZTreeObj("fileTree");
                    //var nodes = treeObj.getNodes();

                    //// 将第一个节点选中展开
                    //if (nodes && nodes.length > 0) {
                    //    treeObj.setting.callback.onClick(event, 'fileTree', nodes[0]);
                    //    treeObj.expandNode(nodes[0], true, false, true, true);
                    //}
                    ////   将第一个节点设置为选中状态
                    //var curMenu = treeObj.getNodes()[0];
                    //treeObj.selectNode(curMenu);
                }
            }
        });
    }


    (function ($) {
        var printAreaCount = 0;
        $.fn.printArea = function () {
            var ele = $(this);
            var idPrefix = "printArea_";
            removePrintArea(idPrefix + printAreaCount);
            printAreaCount++;
            var iframeId = idPrefix + printAreaCount;
            var iframeStyle = 'position:absolute;width:0px;height:0px;left:-500px;top:-500px;';
            iframe = document.createElement('IFRAME');
            $(iframe).attr({
                style: iframeStyle,
                id: iframeId
            });
            document.body.appendChild(iframe);
            var doc = iframe.contentWindow.document;
            $(document).find("link").filter(function () {
                return $(this).attr("rel").toLowerCase() == "stylesheet";
            }).each(
                    function () {
                        doc.write('<link type="text/css" rel="stylesheet" href="'
                                + $(this).attr("href") + '" >');
                    });
            doc.write('<div class="' + $(ele).attr("class") + '">' + $(ele).html()
                    + '</div>');
            doc.close();
            var frameWindow = iframe.contentWindow;
            frameWindow.close();
            frameWindow.focus();
            frameWindow.print();
        }
        var removePrintArea = function (id) {
            $("iframe#" + id).remove();
        };
    })(jQuery);

</script>
</head>
<body>

     <!-- 插入打印控件 -->
      <%--<OBJECT  ID="jatoolsPrinter" CLASSID="CLSID:B43D3361-D075-4BE2-87FE-057188254255" codebase="jatoolsPrinter.cab#version=8,6,0,0"></OBJECT>--%>

<form id='page1' runat="server">
    <div id="main">
        <div id="panel" style="padding: 2px;">
            <div id="layout" class="easyui-layout" fit="true" style="width: 100%; height: 600px;">
                <div region="north" style="height: 36px; overflow: hidden;">
                    <div style="height: 36px; line-height: 36px; width: 600px; float: left; display: inline-block; overflow: hidden; border: 0px solid green; white-space: nowrap;">
                        <label style="margin-left: 5px;">从文件目录『</label>
                        <label id="lblSource" style="color: red; font-weight: bold;"></label>
                        <label>』导入到标准体系目录『</label>
                        <label id="lblTarget" style="color: red; font-weight: bold;"></label>
                        <label>』中</label>
                    </div>
                    <div style="height: 36px; line-height: 36px; width: 203px; float: right; display: inline-block;">
                        <input type="button" id="btnImport" style="width: 72px; height: 24px;" value="导入"/>
                        <input type="button" id="btnEmpty" style="width: 122px; height: 24px;" value="清空体系数据"/>
                    </div>
                </div>
                <div region="west" split="true" collapsible="false" style="width: 50%;">
                    <ul id="fileTree" class="ztree"></ul>
                    <%--<div id="fileList"></div>--%>
                </div>
                <div region="center">
                    <%--<div id="docList"></div>--%>
                    <ul id="docTree" class="ztree"></ul>
                </div>
            </div>
        </div>
        <input id="txtSourcePath" type="hidden"/><input id="txtTargetPath" type="hidden"/>

    </div>
</form>
</body>
</html>