<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rename.aspx.cs" Inherits="WebApplicationTest.Standard.Rename" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>本地文件重新命名</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
         <h1>本地文件重新命名</h1>
        <asp:TextBox ID="txtSource" runat="server" Width="240px"></asp:TextBox>
        &nbsp;替换为&nbsp;
        <asp:TextBox ID="txtTarget" runat="server" Width="240px"></asp:TextBox>
        <br/><br/>

        <asp:Button ID="btnProcess" runat="server" Text="文件名中部分字符替换" OnClick="btnProcess_Click" Width="240px"/>
        <br/><br/>

        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </div>
</form>
</body>
</html>