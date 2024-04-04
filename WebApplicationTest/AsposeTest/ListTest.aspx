<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListTest.aspx.cs" Inherits="WebApplicationTest.AsposeTest.ListTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>ASPOSE.WORD 列表测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnTest" runat="server" Text="Word 文档列表测试" OnClick="btnTest_Click" />
        &nbsp;<asp:Button ID="btnListLabel" runat="server" Text="List Label" OnClick="btnListLabel_Click" />
        &nbsp;<asp:Button ID="btnStringLength" runat="server" Text="String Length" OnClick="btnStringLength_Click" />
        
         <br />
         <br/>
        
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
