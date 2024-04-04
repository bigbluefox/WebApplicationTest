<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilesView.aspx.cs" Inherits="WebApplicationTest.FilesView" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>服务器制定目录文件查询</title>
    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Styles/main.css" rel="stylesheet"/>

    <script src="Scripts/jquery-2.2.4.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>

    <script src="Scripts/Hsp.js"></script>
    <script src="Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        * { font-family: Arial, "Microsoft YaHei", 微软雅黑, "MicrosoftJhengHei", 华文细黑, STHeiti, MingLiu, Tahoma; }

        #main {
            margin: 0 auto;
            width: 100%;
        }

    </style>

    <script type="text/javascript">

        var width = 0, height = 0;
        var rootPath = "<% = RootPath %>";

        $(function() {
            height = HSP.Common.AvailHeight();

            showFileList();
            showDocList();
        });



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
                columns: [[
                    {
                        field: 'FileName', title: '名称', width: 150, halign: 'center',
                        formatter: function (val, row, index) {
                            return row.FileType == 0 ? '<span style="color:red;">' + val + '</span>' : val;
                        }
                    },
                    {
                        field: 'FileType', title: '类型', width: 45, halign: 'center', align: 'center',
                        formatter: function (val, row, index) {
                            return val == 0 ? '文件夹':'文件';
                        }
                    },
                    {
                        field: 'Extension', title: '扩展名', width: 45, halign: 'center', align: 'center',
                        formatter: function (val, row, index) {
                            return val.replace('.', '');
                        }
                    },
                    {
                        field: 'FileSize', title: '大小', width: 60, halign: 'center', align: 'right',
                        formatter: function (val, row, index) {
                            return row.FileType == 0 ? '' : parseFloat(val).toLocaleString();
                        }
                    }
                ]]
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
                columns: [[
                    {
                        field: 'FileName', title: '名称', width: 150, halign: 'center',
                        formatter: function (val, row, index) {
                            return row.FileType == 0 ? '<span style="color:red;">' + val + '</span>' : val;
                        }
                    },
                    {
                        field: 'FileType', title: '类型', width: 45, halign: 'center', align: 'center',
                        formatter: function (val, row, index) {
                            return val == 0 ? '文件夹' : '文件';
                        }
                    },
                    {
                        field: 'Extension', title: '扩展名', width: 45, halign: 'center', align: 'center',
                        formatter: function (val, row, index) {
                            return val.replace('.', '');
                        }
                    },
                    {
                        field: 'FileSize', title: '大小', width: 60, halign: 'center', align: 'right',
                        formatter: function (val, row, index) {
                            return row.FileType == 0 ? '' : parseFloat(val).toLocaleString();
                        }
                    }
                ]]
            });
        }


    </script>

</head>
<body>
<form id="form1" runat="server">
    <div id="main">
        
<%--    <div class="easyui-tabs" id="myTaskTabs" fit="true" plain="true" style="height: 570px;">
        <div title="可以发起的任务列表" style="height: 570px; padding: 5px;">border="false" border="false"  border="false"
        </div>
    </div> --%>              

            <div id="layout" class="easyui-layout" fit="true" style="width: 100%; height: 600px;">
                <div region="north" style="height: 36px; overflow: hidden;">
                    <div style="height:36px; line-height: 36px;">
                        <label id="path" style="margin-left:5px;"><a href="javascript:void(0);" onclick="javascript:getGridList();">根路径</a></label>
                    </div>
                </div>
                <div region="west" split="true" border="false" collapsible="false" style="width: 50%;">
                    <div id="fileList"></div>
                </div>
                <div region="center" border="false">
                    <div id="docList"></div>
                </div>
            </div>
     


    </div>
</form>
</body>
</html>