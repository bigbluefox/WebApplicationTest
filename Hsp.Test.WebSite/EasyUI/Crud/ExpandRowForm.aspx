<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpandRowForm.aspx.cs" Inherits="Crud_ExpandRowForm" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="UTF-8">
    <title>Build CRUD Application with edit form in expanded row details - jQuery EasyUI Demo</title>
<%--    <link rel="stylesheet" type="text/css" href="https://www.jeasyui.com/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="https://www.jeasyui.com/easyui/themes/icon.css">
    <link rel="stylesheet" type="text/css" href="https://www.jeasyui.com/easyui/demo/demo.css">--%>
    
    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://www.jeasyui.com/easyui/jquery.easyui.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://www.jeasyui.com/easyui/datagrid-detailview.js"></script>--%>
    
    <link href="/Scripts/V1.5.3/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/V1.5.3/themes/icon.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="https://www.jeasyui.com/easyui/demo/demo.css">

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/V1.5.3/jquery.easyui.min.js"></script>
    <script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>

    <script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/V1.5.3/plugins/datagrid-detailview.js"></script>

</head>
<body>
    <h2>Edit form in expanded row details『SqlLite Db』</h2>
    <p>Click the expand button to expand a detail form.</p>
    
    <table id="dg" title="我的用户信息" style="width:700px;height:365px"
            url="/Handler/CrudHandler.ashx?OPERATION=GETUSER"
            toolbar="#toolbar" pagination="true"
            fitColumns="true" singleSelect="true">
        <thead>
            <tr>
                <th field="firstname" width="50">First Name</th>
                <th field="lastname" width="50">Last Name</th>
                <th field="phone" width="50">Phone</th>
                <th field="email" width="50">Email</th>
            </tr>
        </thead>
    </table>

    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="newItem()">新建</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="destroyItem()">删除</a>
    </div>

<script type="text/javascript">
    function save1(target) {
        var tr = $(target).closest('.datagrid-row-detail').closest('tr').prev();
        var index = parseInt(tr.attr('datagrid-row-index'));
        saveItem(index);
    }
    function cancel1(target) {
        var tr = $(target).closest('.datagrid-row-detail').closest('tr').prev();
        var index = parseInt(tr.attr('datagrid-row-index'));
        console.log(index)
        cancelItem(index);
    }
</script>	    

    <script type="text/javascript">
        $(function () {
            $('#dg').datagrid({
                view: detailview,
                detailFormatter: function (index, row) {
                    return '<div class="ddv"></div>';
                },
                onExpandRow: function (index, row) {
                    var ddv = $(this).datagrid('getRowDetail', index).find('div.ddv');
                    //alert(row.id);
                    ddv.panel({
                        border: false,
                        cache: true,
                        href: '/Handler/CrudHandler.ashx?OPERATION=SHOWFORM&index=' + index,
                        onLoad: function () {
                            $('#dg').datagrid('fixDetailRowHeight', index);
                            $('#dg').datagrid('selectRow', index);
                            $('#dg').datagrid('getRowDetail', index).find('form').form('load', row);
                        }
                    });
                    $('#dg').datagrid('fixDetailRowHeight', index);
                }
            });
        });

        function saveItem(index) {
            var row = $('#dg').datagrid('getRows')[index];
            var url = '/Handler/CrudHandler.ashx?OPERATION=';
            url += row.isNewRecord ? 'SAVEUSER' : 'UPDATEUSER&id=' + row.id;
            $('#dg').datagrid('getRowDetail', index).find('form').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (data) {
                    data = eval('(' + data + ')');
                    data.isNewRecord = false;
                    $('#dg').datagrid('collapseRow', index);
                    $('#dg').datagrid('updateRow', {
                        index: index,
                        row: data
                    });
                }
            });
        }
        function cancelItem(index) {
            var row = $('#dg').datagrid('getRows')[index];
            if (row.isNewRecord) {
                $('#dg').datagrid('deleteRow', index);
            } else {
                $('#dg').datagrid('collapseRow', index);
            }
        }
        function destroyItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', 'Are you sure you want to remove this user?', function (r) {
                    if (r) {
                        var index = $('#dg').datagrid('getRowIndex', row);
                        $.post('/Handler/CrudHandler.ashx', { id: row.id, OPERATION: "DESTROYUSER" }, function () {
                            $('#dg').datagrid('deleteRow', index);
                        });
                    }
                });
            }
        }
        function newItem() {
            $('#dg').datagrid('appendRow', { isNewRecord: true });
            var index = $('#dg').datagrid('getRows').length - 1;
            $('#dg').datagrid('expandRow', index);
            $('#dg').datagrid('selectRow', index);
        }
    </script>
    <style type="text/css">
        form{
            margin:0;
            padding:0;
        }
        .dv-table td{
            border:0;
        }
        .dv-table input{
            border:1px solid #ccc;
        }
    </style>
    
</body>
</html>
