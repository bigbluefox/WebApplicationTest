<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UtcTimeTest.aspx.cs" Inherits="WebApplicationTest.UtcTimeTest" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>UTC 时间测试</title>
    
    <link href="/Styles/base.css" rel="stylesheet" />    

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="/Scripts/html5shiv.min.js"></script>
        <script src="/Scripts/respond.min.js"></script>
    <![endif]-->
    
    <style type="text/css">
        body{ padding: 5px;}
        label{ height: 24px;line-height: 24px; }
        /*input[type='text'], textarea{margin-top: 5px;}*/
        input[type='submit']{ margin-top: 5px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 600px;">
        <label>UTC时间</label>
        <asp:TextBox ID="txtUTCTime" runat="server" Width="100%" ToolTip="UTC时间"></asp:TextBox><br/>
        <label>日期结果</label>
        <asp:TextBox ID="txtDate" runat="server" Width="100%" ToolTip="日期结果"></asp:TextBox><br/>
        <label>日期时间</label>
        <asp:TextBox ID="txtDateTime" runat="server" Width="100%" ToolTip="日期时间"></asp:TextBox><br/>
        <asp:Button ID="btnResult" runat="server" Text="查看结果" OnClick="btnResult_Click" />
    </div>
    </form>
</body>
</html>
