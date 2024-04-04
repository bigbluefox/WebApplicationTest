<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrudDataGrid.aspx.cs" Inherits="WebApplicationTest.Crud.CrudDataGrid" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>CRUD DataGrid</title>

    <%--    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>    
    <script src="/Scripts/plugins/jquery.datagrid.js"></script>--%>

    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/icon.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/demo/demo.css">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="http://www.jeasyui.com/easyui/jquery.easyui.min.js"></script>
    <script src="../Scripts/locale/easyui-lang-zh_CN.js"></script>
    <%--<script type="text/javascript" src="http://www.jeasyui.com/easyui/jquery.edatagrid.js"></script>--%>
    <script src="../Scripts/plugins/jquery.edatagrid.js"></script>
</head>
<body>

<h2>CRUD DataGrid</h2>
<p>Double click the row to begin editing.</p>

<table id="dg" title="我的用户信息" style="width: 700px; height: 360px"
       toolbar="#toolbar" pagination="true" idField="id"
       rownumbers="true" fitColumns="true" singleSelect="true">
    <thead>
    <tr>
        <th field="firstname" width="50" editor="{type:'validatebox',options:{required:true}}">First Name</th>
        <th field="lastname" width="50" editor="{type:'validatebox',options:{required:true}}">Last Name</th>
        <th field="phone" width="50" editor="{type:'validatebox',options:{required:true}}">Phone</th>
        <th field="email" width="50" editor="{type:'validatebox',options:{required:true,validType:'email'}}">Email</th>
    </tr>
    </thead>
</table>
<div id="toolbar">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:$('#dg').edatagrid('addRow')">新建</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="javascript:$('#dg').edatagrid('destroyRow')">删除</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-save" plain="true" onclick="javascript:$('#dg').edatagrid('saveRow')">保存</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-undo" plain="true" onclick="javascript:$('#dg').edatagrid('cancelRow')">取消</a>
</div>

<script type="text/javascript">
    $(function() {
        $('#dg').edatagrid({
            url: '/Handler/CrudHandler.ashx?OPERATION=GETUSER',
            saveUrl: '/Handler/CrudHandler.ashx?OPERATION=SAVEUSER',
            updateUrl: '/Handler/CrudHandler.ashx?OPERATION=UPDATEUSER',
            destroyUrl: '/Handler/CrudHandler.ashx?OPERATION=DESTROYUSER'
        });
    });
</script>
</body>
</html>