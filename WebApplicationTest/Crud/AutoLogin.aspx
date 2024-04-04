<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoLogin.aspx.cs" Inherits="WebApplicationTest.Crud.AutoLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>自动登录测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="UserID" runat="server"></asp:TextBox>
        <br /><br />
        <asp:TextBox ID="Password" runat="server"></asp:TextBox>

        <br /><br />

        <asp:Button ID="btnAutoLogin" runat="server" Text="自动登录" OnClick="btnAutoLogin_Click" />
    </div>
    </form>
</body>
</html>
