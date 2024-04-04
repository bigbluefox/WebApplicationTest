<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplicationTest.Regular.Test" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>正则测试</title>
    
    <link href="../Styles/base.css" rel="stylesheet" />    

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
        <label>待处理字符串</label>
        <asp:TextBox ID="txtSource" runat="server" Height="180px" Rows="6" TextMode="MultiLine" Width="100%" ToolTip="待处理字符串 "></asp:TextBox><br/>
        <label>匹配规则</label>
        <asp:TextBox ID="txtExpression" runat="server" Width="100%" ToolTip="匹配规则"></asp:TextBox><br/>
        <label>匹配结果</label>
        <asp:TextBox ID="txtMatchResult" runat="server" Height="90px" TextMode="MultiLine" Width="100%" ToolTip="匹配结果"></asp:TextBox><br/>
        <label>匹配目的</label>
        <asp:TextBox ID="txtTarget" runat="server" Width="100%" ToolTip="匹配目的"></asp:TextBox><br/>
        <label>匹配结果</label>
        <asp:TextBox ID="txtReplaceResult" runat="server" Height="90px" TextMode="MultiLine" Width="100%" ToolTip="匹配结果"></asp:TextBox><br/>
        <asp:Button ID="btnProcess" runat="server" Text="字符匹配" OnClick="btnProcess_Click" />
        <asp:Button ID="btnReplace" runat="server" OnClick="btnReplace_Click" Text="字符替换" />
    </div>
        
    </form>
</body>
</html>
