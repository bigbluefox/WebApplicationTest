<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecodeMoneyCn.aspx.cs" Inherits="WebApplicationTest.Regular.DecodeMoneyCn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>中文数字转阿拉伯数字</title>
    <style type="text/css">
        input[type='text']{ height: 20px;}
        #<%= txtChinese.ClientID%>{ width: 360px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtChinese" runat="server"></asp:TextBox> - 
        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        <br/><br/>
        <asp:Button ID="Button1" runat="server" Text="中文数字转阿拉伯数字" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
