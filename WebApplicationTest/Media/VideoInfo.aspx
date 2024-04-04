
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoInfo.aspx.cs" Inherits="WebApplicationTest.Media.VideoInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>视频信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnVideoInfo" runat="server" Text="视频信息" OnClick="btnVideoInfo_Click" />
        <br />
        <br />
        <asp:Label ID="lblVideoInfo" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
