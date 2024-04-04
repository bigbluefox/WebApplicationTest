<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hardware.aspx.cs" Inherits="WebApplicationTest.Fakes.Hardware" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnHardware" runat="server" Text="硬件信息" OnClick="btnHardware_Click" />
        <br/><br/>
        <asp:Label ID="lblHardware" runat="server" Text=""></asp:Label>

    </div>
    </form>
</body>
</html>
