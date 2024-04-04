<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostString.aspx.cs" Inherits="WebApplicationTest.PostString" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>http post/get</title>
    <style type="text/css">
        img{ cursor: pointer;}
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtString" runat="server" Height="92px" Rows="3" TextMode="MultiLine" Width="426px"></asp:TextBox>
        <br/><br/>
        <asp:Button ID="btnSend" runat="server" Text="发送字符串" OnClick="btnSend_Click"/>
        <asp:Button ID="btnSendJson" runat="server" Text="发送Json串" OnClick="btnSendJson_Click" />
        <asp:Button ID="btnGet" runat="server" Text="获取远程数据" OnClick="btnGet_Click"/>
        <br />
        <br />
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                <br />
        <br />
        
<img id="validcode" alt="验证码，看不清，换一张图片" title="验证码，看不清，换一张图片" src="Handler/ValidateCodeHandler.ashx" style="margin-top: -3px; vertical-align: middle;" onclick=" this.src = 'Handler/ValidateCodeHandler.ashx?' + (new Date().getTime()); "/>

    </div>
</form>

</body>
</html>