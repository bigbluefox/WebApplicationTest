<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplicationTest.Crud.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>

    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>

    <style type="text/css">
        
    </style>

    <script type="text/javascript">

        $(function() {

            //var arr = { OPERATION: "GETFILELIST", FID: "", TID: "64C64907-F110-4A41-9DAF-5532A5A135FB", GID: "1235", UID: "", RID: Math.round(Math.random() * 10) };

            //// 发送ajax请求
            //$.getJSON("/Handler/GetFileHandler.ashx", arr)
            //    //向服务器发出的查询字符串 
            //    // 对返回的JSON数据进行处理 
            //    .done(function(data) {

            //        //debugger;

            //        //console.info(data);
            //        if (data != null && data.length > 0) {
            //            $('#dg').datagrid('loading');
            //            $('#dg').datagrid('loadData', { "total": data[0].RecordCount, "rows": data });
            //            $('#dg').datagrid('loaded');
            //        } else {
            //            //$.messager.alert('温馨提示', '查询失败！', 'error');
            //            //$('#dg').datagrid('loadData', { "total": 0, "rows": [] });
            //            //$('#dg').datagrid('loaded');
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

            //var arr = { OPERATION: "GETUSER", RID: Math.round(Math.random() * 10) };
            //// 发送ajax请求
            //$.getJSON("/Handler/CrudHandler.ashx", arr)
            //    //向服务器发出的查询字符串 
            //    // 对返回的JSON数据进行处理 
            //    .done(function(data) {

            //        //debugger;

            //        //console.info(data);
            //        //if (data != null && data.length > 0) {
            //        //    $('#dg').datagrid('loading');
            //        //    $('#dg').datagrid('loadData', { "total": data.total, "rows": data.rows });
            //        //    $('#dg').datagrid('loaded');
            //        //} else {
            //        //    //$.messager.alert('温馨提示', '查询失败！', 'error');
            //        //    $('#dg').datagrid('loadData', { "total": 0, "rows": [] });
            //        //    $('#dg').datagrid('loaded');
            //        //}
            //    });

        });

    </script>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
        <br/><br/>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
</form>
</body>
</html>