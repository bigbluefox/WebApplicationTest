<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioInfo.aspx.cs" Inherits="WebApplicationTest.Media.AudioInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>音频信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Button ID="btnAudioInfo" runat="server" Text="音频信息" OnClick="btnAudioInfo_Click" />
        <br />
        <br />
        <asp:Label ID="lblAudioInfo" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
