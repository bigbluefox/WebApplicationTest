<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Video.aspx.cs" Inherits="AnyShare_Video" %>

<!DOCTYPE html>

<html lang="zh-CN">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>5. 文件协议（操作相关）</title>
    
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

            <br/><br/>

            <asp:Label ID="Label1" runat="server" Text="当前登录账号："></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="当前Token："></asp:Label>
            <asp:TextBox ID="txtToken" runat="server" style="width: 300px;" Text=""></asp:TextBox>
            <br/><br/>

            <asp:Label ID="Label3" runat="server" Text="在线播放："></asp:Label>
            <asp:TextBox ID="txtPlayInfoDocId" runat="server" style="width: 600px;" Text=""></asp:TextBox>
            <asp:Button ID="btnPlayInfo" runat="server" Text="5.18. 在线播放请求协议" OnClick="btnPlayInfo_Click" />
            <asp:Button ID="btnThumbnail" runat="server" Text="5.20. 获取视频缩略图协议" OnClick="btnThumbnail_Click" /> 
            <br/><br/>
        
            <asp:Label ID="Label4" runat="server" Text="在线播放："></asp:Label>
            <asp:TextBox ID="txtPlayDocId" runat="server" style="width: 600px;" Text=""></asp:TextBox>
            <asp:Button ID="btnPlay" runat="server" Text="5.19. 在线播放协议" OnClick="btnPlay_Click" />        

            
            <br/><br/>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label> <br/>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>   
    </div>
    </form>
    </div>

<script type="text/javascript">

    $(function () {
        //alert("Hello World!");

    });

</script>    

</body>
</html>
