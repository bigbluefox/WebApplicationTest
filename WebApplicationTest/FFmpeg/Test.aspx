<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplicationTest.FFmpeg.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>上传视频并将各种视频文件转换成.flv格式</h1>
    标题：<asp:TextBox ID="txtTitle" runat="server" Width="358px"></asp:TextBox>

&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"

ErrorMessage="标题不为空"></asp:RequiredFieldValidator>

<br />

        视频：<asp:FileUpload ID="FileUpload1" runat="server" Width="339px" />

        <br />

<asp:Button ID="btnUpload" runat="server" Text="上传视频" OnClick="btnUpload_Click" />

        <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="视频转换测试" />
        <br />

        <br />

文件类型<span style="color:Red;">(.asf|.flv|.avi|.mpg|.3gp|.mov|.wmv|.rm|.rmvb)</span>

<asp:RegularExpressionValidator ID="imagePathValidator" runat="server" ErrorMessage="文件类型不正确"

ValidationGroup="vgValidation" Display="Dynamic" ValidationExpression="^[a-zA-Z]:(\\.+)(.asf|.flv|.avi|.mpg|.3gp|.mov|.wmv|.rm|.rmvb){1}" ControlToValidate="FileUpload1">

</asp:RegularExpressionValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" ErrorMessage="文件不为空"></asp:RequiredFieldValidator>
        <br />
    </div>

<div style=" height:0px; border-top:solid 1px red; font-size:0px;"></div>

<div>上传列表.</div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestConnectionString %>" SelectCommand="SELECT * FROM [ABC]"></asp:SqlDataSource>

    </form>
</body>
</html>
