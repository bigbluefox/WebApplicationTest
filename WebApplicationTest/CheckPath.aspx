<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckPath.aspx.cs" Inherits="WebApplicationTest.CheckPath" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文件目录检查</title>

    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Styles/zTreeStyle/zTreeStyle.css" rel="stylesheet"/>
    <%--<link href="Styles/main.css" rel="stylesheet"/>--%>

    <script src="Scripts/jquery/jquery-2.2.4.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="Scripts/jquery.ztree.core.min.js"></script>

    <script src="Scripts/Hsp.js"></script>
    <script src="Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        * { font-family: Arial, "Microsoft YaHei", 微软雅黑, "MicrosoftJhengHei", 华文细黑, STHeiti, MingLiu, Tahoma; }

        #main {
            margin: 0 auto;
            width: 100%;
            cursor: pointer;
        }

    </style>

    <script type="text/javascript">


        function progress() {
            var win = $.messager.progress({
                title: 'Please waiting',
                msg: 'Loading data...'
            });
            setTimeout(function() {
                $.messager.progress('close');
            }, 5000);
        }

    </script>

</head>
<body>
<form id="form1" runat="server">
    <div id="main">
        <asp:TextBox ID="txtPath" runat="server" Width="765px"></asp:TextBox>
        <br/><br/>
        <asp:Button ID="btnChkPath" runat="server" Text="文件目录检查" OnClick="btnChkPath_Click"/>
        <br/><br/>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        
        <br/><br/>
        <input id="Button1" type="button" value="button" onclick="progress();" />
        <br />
        <br />
        <asp:Button ID="btnPromulgatedReign" runat="server" Text="检查年代号" OnClick="btnPromulgatedReign_Click" />

        <br />

        <br />
        <asp:Label ID="lblPromulgatedReign" runat="server" Text=""></asp:Label>

        <br />
        <br />
        <asp:Label ID="lblStandType" runat="server" Text=""></asp:Label>

    </div>
</form>
</body>
</html>