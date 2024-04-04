<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ping.aspx.cs" Inherits="AnyShare_Ping" %>

<!DOCTYPE html>

<html lang="zh-CN">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>AnyShare Cloud 测试</title>

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

    <%--<script src="/Scripts/V1.5.3/jquery.easyui.min.js"></script>--%>
    <%--<script src="/Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>--%>

    <script src="/Bootstrap/v3/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Bootstrap/v3/js/ie10-viewport-bug-workaround.js"></script>

    <style type="text/css">
         li{ cursor: pointer;}
        input{ display: inline-block;}
        input[type='text'],input[type='file']{ height: 27px;}
        table td{ padding: 2px 2px;}
    </style>

</head>
<body>
<div class="container">
    <form id="form1" runat="server">
        <div class="row" style="padding-bottom: 15px;">
            <asp:Button ID="btnPing" runat="server" Text="4.1. 检查服务器是否在线" OnClick="btnPing_Click"/>
            <asp:Button ID="btnLoginTest" runat="server" Text="5.3.	登录（标准）测试" OnClick="btnLoginTest_Click"/>
            <asp:Button ID="btnRefreshToken" runat="server" Text="5.10.	刷新身份凭证有效期" OnClick="btnRefreshToken_Click" />
            <asp:Button ID="btnRevokeToken" runat="server" Text="5.11. 回收身份凭证" OnClick="btnRevokeToken_Click" Enabled="False" />
            <asp:Button ID="btnLogout" runat="server" Text="5.13. 登出" OnClick="btnLogout_Click" />

            <asp:Button ID="btnOAuth" runat="server" Text="5.2.	获取OAuth信息" OnClick="btnOAuth_Click" style="display: none;"/>
            <asp:Button ID="btnConfig" runat="server" Text="5.1. 获取服务器配置信息" OnClick="btnConfig_Click" style="display: none;"/>
            <asp:Button ID="btnCreateKey" runat="server" Text="RSA生成公私钥测试" OnClick="btnCreateKey_Click" style="display: none;"/>
            <br/><br/>

            <asp:Label ID="Label1" runat="server" Text="当前登录账号："></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="当前Token："></asp:Label>
            <asp:TextBox ID="txtToken" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <%--<br/><br/>--%>

            <asp:TextBox ID="txtDirList" runat="server" TextMode="MultiLine" style="display: none;width: 100%;"></asp:TextBox>
            <asp:TextBox ID="txtPrivateKey" runat="server" TextMode="MultiLine" style="display: none;"></asp:TextBox>
            <asp:TextBox ID="txtPublicKey" runat="server" TextMode="MultiLine" style="display: none;"></asp:TextBox>
            <br/><br/>

            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br/><br/>
            <%--<asp:Label ID="lblResult" runat="server" Text=""></asp:Label>--%>

        </div>
    </form>
</div>

<script type="text/javascript">

    $(function() {
        //alert("Hello World!");

        $("#<% = txtPrivateKey.ClientID %>").css("width", "100%");
        $("#<% = txtPublicKey.ClientID %>").css("width", "100%");

    });

</script>

<!-- Begin page content -->
<div class="container" style="display: none;">

    <div class="row">

        <div class="btn-toolbar" role="toolbar">
            <div class="btn-group">
                <button class="btn btn-default" aria-label="Left Align" type="button">
                    <span class="glyphicon glyphicon-align-left" aria-hidden="true"></span>
                </button>
                <button class="btn btn-default" aria-label="Center Align" type="button">
                    <span class="glyphicon glyphicon-align-center" aria-hidden="true"></span>
                </button>
                <button class="btn btn-default" aria-label="Right Align" type="button">
                    <span class="glyphicon glyphicon-align-right" aria-hidden="true"></span>
                </button>
                <button class="btn btn-default" aria-label="Justify" type="button">
                    <span class="glyphicon glyphicon-align-justify" aria-hidden="true"></span>
                </button>
            </div>
        </div>

    </div>

</div>

<footer class="footer"></footer>

</body>
</html>