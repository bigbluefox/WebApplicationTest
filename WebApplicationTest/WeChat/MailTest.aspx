<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTest.aspx.cs" Inherits="WebApplicationTest.WeChat.MailTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>

        <asp:Label ID="Label4" runat="server" Text="SMTP："></asp:Label>
        <asp:TextBox ID="txtSMTP" runat="server" Width="432px" Enabled="False"></asp:TextBox>
        <br/>
        <br/>
        <asp:Label ID="Label5" runat="server" Text="FROM："></asp:Label>
        <asp:TextBox ID="txtFrom" runat="server" Width="432px" Enabled="False"></asp:TextBox>
        <br/>
        <br/>
        <asp:Label ID="Label2" runat="server" Text="发送："></asp:Label>
        <asp:TextBox ID="txtSendAddr" runat="server" Width="432px"></asp:TextBox>
        <br/>
        <br/>
        <asp:Label ID="Label3" runat="server" Text="抄送："></asp:Label>
        <asp:TextBox ID="txtCCAddr" runat="server" Width="429px"></asp:TextBox>
        <br/>
        <br/>
        <asp:Button ID="btnMailSendTest" runat="server" OnClick="btnMailSendTest_Click" Text="邮件发送测试"/>
        <br/>
        <br/>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
</form>
</body>
</html>