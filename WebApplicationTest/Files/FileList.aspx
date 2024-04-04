<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="WebApplicationTest.Files.FileList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>列出文件目录</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnList" runat="server" Text="列出文件目录" OnClick="btnList_Click" />
        <br/>
        <asp:Label ID="lblList" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
