<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendTest.aspx.cs" Inherits="WebApplicationTest.WeChat.SendTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>微信发送测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnSend" runat="server" Text="微信发送" OnClick="btnSend_Click" />
        <br />
        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
