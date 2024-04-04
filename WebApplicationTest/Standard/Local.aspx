<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Local.aspx.cs" Inherits="WebApplicationTest.Standard.Local" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>本地标准文件检索</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>本地标准文件检索</h1>
        目录：<asp:TextBox ID="txtLocalPath" runat="server" Width="600px"></asp:TextBox>
        <br/><br/>
        哈希：<asp:CheckBox ID="cbxMd5" runat="server" Text="MD5"/>&nbsp;<asp:CheckBox ID="cbxSha1" runat="server" Text="SHA1"/>
        <br/><br/>
        
        <asp:Button ID="btnStandardProcess" runat="server" Text="本地标准文件检索" OnClick="btnStandardProcess_Click" />
        <br/><br/>
        
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </div>
        
    </form>
</body>
</html>
