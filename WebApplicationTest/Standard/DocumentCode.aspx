<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentCode.aspx.cs" Inherits="WebApplicationTest.Standard.DocumentCode" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>标准体系代码解析</title>
    <link href="../Styles/base.css" rel="stylesheet" />
    <style type="text/css">
        body{ padding: 5px;}
         h1{ height: 36px;line-height: 36px;}
        input[type='text']{ width: 99.35%;}

        input[type='file'] {
            height: 26px;
            width: 50%;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>标准体系代码解析</h1>
        <asp:TextBox ID="txtDocumentCode" runat="server"></asp:TextBox>
        <br/><br/>

        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br/><br/>

         <asp:Button ID="btnDocumentCode" runat="server" Text="标准体系代码解析" Width="300px" OnClick="btnDocumentCode_Click" />
        <br/><br/>       

        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>    
    </div>
    </form>
</body>
</html>
