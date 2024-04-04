<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CharacterRecognition.aspx.cs" Inherits="Aip_CharacterRecognition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnCharacterRecognition" runat="server" Text="文字识别" OnClick="btnCharacterRecognition_Click" />
        <br/>
        <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
