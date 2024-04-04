<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrectedName.aspx.cs" Inherits="WebApplicationTest.Standard.CorrectedName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修正本地标准文件名称</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>修正本地标准文件名称</h1>
        <asp:Button ID="btnRename" runat="server" Text="华润标准文件修正" OnClick="btnRename_Click" Width="300px" />
        <br/><br/>
        <asp:Button ID="btnCorrectedName" runat="server" Text="修正文件名缺乏扩展名" OnClick="btnCorrectedName_Click" Enabled="False" Width="300px"  />
        <br/><br/>
        
         <asp:Button ID="btnAddCodeToFileName" runat="server" Text="修正文件名缺乏编码" Width="300px" OnClick="btnAddCodeToFileName_Click" />
        <br/><br/>       

        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
