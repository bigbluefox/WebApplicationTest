<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegularTest.aspx.cs" Inherits="WebApplicationTest.Fakes.RegularTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:TextBox ID="txtContent" runat="server" Height="61px" Width="418px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnTest" runat="server" Text="Button" OnClick="btnTest_Click" />
    
        <br />
        <br />
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
