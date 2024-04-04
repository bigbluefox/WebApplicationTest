<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dirs.aspx.cs" Inherits="AnyShare_Dirs" %>

<!DOCTYPE html>

<html lang="zh-CN">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>AnyShare Cloud 目录 测试</title>

    <!-- Bootstrap core CSS -->
    <link href="/Bootstrap/v3/css/bootstrap.css" rel="stylesheet">
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="/Bootstrap/v3/css/ie10-viewport-bug-workaround.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="/Scripts/respond.min.js"></script>
        <script src="/Scripts/html5shiv.min.js"></script>
    <![endif]-->

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <%--<script src="/Scripts/V1.5.3/jquery.easyui.min.js"></script>--%>    <%--<script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>--%>

    <script src="/Bootstrap/v3/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Bootstrap/v3/js/ie10-viewport-bug-workaround.js"></script>

    <style type="text/css">
        li{ cursor: pointer;}
        input{ display: inline-block;}
        input[type='text'],input[type='file']{ height: 27px;}
        table td{ padding: 2px 2px;}
    </style>
    
    <script type="text/javascript">

        $(function () {

        });

        function CheckThis(obj) {
            var html = $(obj).html();

            //alert(html);
            //var arr = html.split(","); //字符分割 
            var arr = html.split("*");
            var gns = arr[0].trim();

            $("#txtDocId").val(gns);
            $("#txtDirId").val(gns);
            $("#txtDirName").val(arr[1].trim());

            var type = arr[2].trim() == "-1" ? 1 : 0;

            $("input[name=<% = rblType.ClientID %>][value=" + type + "]").attr("checked", true);

            if (type == 1) {
                $("#btnDeleteDir").val("删除目录");
                $("#txtUploadDir").val(gns);
            } else {
                $("#btnDeleteDir").val("删除文件");
                $("#txtUploadDir").val("");
            }
        }

    </script>
</head>
<body>
<div class="container">
    <form id="form1" runat="server">
        <div class="row" style="padding-bottom: 15px;">
            
            <h1>文件目录协议</h1>

            <asp:Button ID="btnPing" runat="server" Text="4.1. 检查服务器是否在线" OnClick="btnPing_Click"/>
            <asp:Button ID="btnLoginTest" runat="server" Text="5.3.	登录（标准）测试" OnClick="btnLoginTest_Click"/>
            <asp:Button ID="btnGetDirList" runat="server" Text="3.5. 浏览目录协议" OnClick="btnGetDirList_Click"/>
            <asp:Button ID="btnGetEntryDoc" runat="server" Text="根目录浏览" OnClick="btnGetEntryDoc_Click"/>
            <br/><br/>

            <asp:Label ID="Label1" runat="server" Text="当前登录账号："></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="当前Token："></asp:Label>
            <asp:TextBox ID="txtToken" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <br/><br/>

            <asp:Label ID="Label3" runat="server" Text="目录名称："></asp:Label>
            <asp:TextBox ID="txtDirName" runat="server" style="width: 300px;" Text="安全生产概论"></asp:TextBox>
            <asp:Button ID="btnCreateDir" runat="server" Text="添加目录" OnClick="btnCreateDir_Click"/>
            <table><tr>
                <td style="width: 71px;">&nbsp;</td>
                <td>
            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="0">文件</asp:ListItem>
                <asp:ListItem Value="1">目录</asp:ListItem>
            </asp:RadioButtonList></td>
            </tr></table>
            <br/><br/>

            <asp:Label ID="Label4" runat="server" Text="文件删除："></asp:Label>
            <asp:TextBox ID="txtDocId" runat="server" style="width: 600px;" Text=""></asp:TextBox>
            <asp:Button ID="btnDeleteDir" runat="server" Text="删除文件/目录" OnClick="btnDeleteDir_Click"/>
            <br/> <br/>

            <asp:Label ID="Label5" runat="server" Text="文件目录："></asp:Label>
            <asp:TextBox ID="txtUploadDir" runat="server" style="width: 600px;" Text=""></asp:TextBox>

            <table><tr>
                <td style="width: 71px;">&nbsp;</td>
                <td><asp:FileUpload ID="FileUpload1" runat="server"/></td>
                <td><asp:Button ID="btnUpload" runat="server" Text="上传文件" OnClick="btnUpload_Click"/></td>
            </tr></table> <br/>

            <asp:Label ID="Label6" runat="server" Text="文件列表："></asp:Label>
            <asp:TextBox ID="txtDirId" runat="server" style="width: 600px;" Text=""></asp:TextBox>
            <asp:Button ID="btnFileList" runat="server" Text="文件列表" OnClick="btnFileList_Click"/>
            <br/> <br/>

            <asp:Label ID="Label7" runat="server" Text="文件预览：" style="display: none;"></asp:Label>
            <asp:TextBox ID="txtPreviewId" runat="server" Text="" style="width: 300px;display: none;"></asp:TextBox>
            <asp:Button ID="btnPreview" runat="server" Text="文件预览" Height="26px" OnClick="btnPreview_Click" style="display: none;"/>
            
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br/><br/>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>

        </div>
    </form>
</div>
</body>
</html>