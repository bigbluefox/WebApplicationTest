<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicCrudApplication.aspx.cs" Inherits="Crud_BasicCrudApplication" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>Basic CRUD (增查改删) Application</title>

    <link href="/Scripts/V1.5.3/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/V1.5.3/themes/icon.css" rel="stylesheet"/>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/V1.5.3/jquery.easyui.min.js"></script>
    <script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>

    <style type="text/css">
        
    </style>

    <script type="text/javascript">

        $(function() {

            //var arr = { OPERATION: "GETFILELIST", FID: "", TID: "64C64907-F110-4A41-9DAF-5532A5A135FB", GID: "1235", UID: "", RID: Math.round(Math.random() * 10) };

            //// 发送ajax请求
            //$.getJSON("/Handler/GetFileHandler.ashx", arr)
            //    //向服务器发出的查询字符串 
            //    // 对返回的JSON数据进行处理 
            //    .done(function (data) {

            //        debugger;

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


            //$.ajax({
            //    type: "GET",
            //    url: "/Handler/CrudHandler.ashx?OPERATION=GETUSER" + "&rnd=" + (Math.random() * 10),
            //    success: function (result) {

            //        alert(result.success);

            //        debugger;

            //        var msg = eval("(" + result + ")");
            //        if (msg) {
            //            if (msg.IsSuccess == "true") {

            //            } else {
            //                $.messager.alert('提示', msg.Message, 'error');
            //            }
            //        } else {
            //            $.messager.alert('提示', '保存异常，请重试', 'error');
            //        }
            //    }
            //});

            //var arr = { OPERATION: "GETUSER", RID: Math.round(Math.random() * 10) };
            //// 发送ajax请求
            //$.getJSON("/Handler/CrudHandler.ashx", arr)
            //    //向服务器发出的查询字符串 
            //    // 对返回的JSON数据进行处理 
            //    .done(function (data) {

            //        //debugger;

            //        //console.info(data);
            //        if (data != null && data.success) {
            //            $('#dg').datagrid('loading');
            //            $('#dg').datagrid('loadData', { "total": data.total, "rows": data.rows });
            //            $('#dg').datagrid('loaded');
            //        } else {
            //            //$.messager.alert('温馨提示', '查询失败！', 'error');
            //            $('#dg').datagrid('loadData', { "total": 0, "rows": [] });
            //            $('#dg').datagrid('loaded');
            //        }
            //    });


            //var arr = { OPERATION: "GETFILELIST", FID: "", TID: "64C64907-F110-4A41-9DAF-5532A5A135FB", GID: "1235", UID: "", RID: Math.round(Math.random() * 10) };

            //// 发送ajax请求
            //$.getJSON("/Handler/GetFileHandler.ashx", arr)
            //    //向服务器发出的查询字符串 
            //    // 对返回的JSON数据进行处理 
            //    .done(function (data) {

            //        debugger;

            //        //console.info(data);
            //        if (data != null && data.length > 0) {
            //            $('#dg').datagrid('loading');
            //            $('#dg').datagrid('loadData', { "total": data[0].RecordCount, "rows": data });
            //            $('#dg').datagrid('loaded');
            //        } else {
            //            //$.messager.alert('温馨提示', '查询失败！', 'error');
            //            $('#dg').datagrid('loadData', { "total": 0, "rows": [] });
            //            $('#dg').datagrid('loaded');
            //        }
            //    });

        });

    </script>

</head>
<body>

<h2>Basic CRUD (增查改删) Application『SqlLite Db』</h2>
<p>Click the buttons on datagrid toolbar to do crud actions.</p>

<form id="form1" runat="server">
    <table id="dg" title="我的用户信息" class="easyui-datagrid" style="height: 365px; width: 700px;"
           url="/Handler/CrudHandler.ashx?OPERATION=GETUSER"
           toolbar="#toolbar" pagination="true" rownumbers="true" fitColumns="true" singleSelect="true">
        <thead>
        <tr>
            <th field="firstname" width="50">First Name</th>
            <th field="lastname" width="50">Last Name</th>
            <th field="phone" width="50">Phone</th>
            <th field="email" width="50">Email</th>
        </tr>
        </thead>
    </table>
</form>


<div id="toolbar">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="newUser()">添加用户</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="editUser()">修改用户</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="destroyUser()">删除</a>
</div>

<div id="dlg" class="easyui-dialog" style="width: 400px" closed="true" buttons="#dlg-buttons">
    <form id="fm" method="post" novalidate style="margin: 0; padding: 20px 50px">
        <div style="border-bottom: 1px solid #ccc; font-size: 14px; margin-bottom: 20px;">User Information</div>
        <div style="margin-bottom: 10px">
            <input name="firstname" class="easyui-textbox" required="true" label="First Name:" style="width: 100%">
        </div>
        <div style="margin-bottom: 10px">
            <input name="lastname" class="easyui-textbox" required="true" label="Last Name:" style="width: 100%">
        </div>
        <div style="margin-bottom: 10px">
            <input name="phone" class="easyui-textbox" required="true" label="Phone:" style="width: 100%">
        </div>
        <div style="margin-bottom: 10px">
            <input name="email" class="easyui-textbox" required="true" validType="email" label="Email:" style="width: 100%">
        </div>
    </form>
</div>
<div id="dlg-buttons">
    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconCls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');" style="width: 90px">取消</a>
</div>
<script type="text/javascript">
    var url;

    function newUser() {
        $('#dlg').dialog('open').dialog('center').dialog('setTitle', '新用户');
        $('#fm').form('clear');
        url = '/Handler/CrudHandler.ashx?OPERATION=SAVEUSER';
    }

    function editUser() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg').dialog('open').dialog('center').dialog('setTitle', '编辑用户');
            $('#fm').form('load', row);
            url = '/Handler/CrudHandler.ashx?OPERATION=UPDATEUSER&id=' + row.id;
        }
    }

    function saveUser() {
        $('#fm').form('submit', {
            url: url,
            onSubmit: function() {
                return $(this).form('validate');
            },
            success: function(result) {
                var result = eval('(' + result + ')');
                if (result.errorMsg) {
                    $.messager.show({
                        title: 'error',
                        msg: result.errorMsg
                    });
                } else {
                    $('#dlg').dialog('close'); // close the dialog
                    $('#dg').datagrid('reload'); // reload the user data
                }
            }
        });
    }

    function destroyUser() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.messager.confirm('Confirm', '您确定要删除该用户吗？', function(r) {
                if (r) {
                    $.post('/Handler/CrudHandler.ashx', { id: row.id, OPERATION: "DESTROYUSER" }, function(result) {
                        if (result.success) {
                            $('#dg').datagrid('reload'); // reload the user data
                        } else {
                            $.messager.show({
                                // show error message
                                title: 'error',
                                msg: result.errorMsg
                            });
                        }
                    }, 'json');
                }
            });
        }
    }


    //$.getJSON({
    //    url: "/Handler/CrudHandler.ashx?OPERATION=GETUSER&rnd=" + (Math.random() * 10),
    //    success: function (result) {

    //        alert(result.success);

    //        debugger;

    //        //var msg = eval("(" + result + ")");
    //        //if (msg) {
    //        //    if (msg.IsSuccess == "true") {

    //        //    } else {
    //        //        $.messager.alert('提示', msg.Message, 'error');
    //        //    }
    //        //} else {
    //        //    $.messager.alert('提示', '保存异常，请重试', 'error');
    //        //}
    //    }
    //});

</script>
</body>
</html>