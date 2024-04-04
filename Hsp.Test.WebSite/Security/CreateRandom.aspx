<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateRandom.aspx.cs" Inherits="Security_CreateRandom" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <p>所用字符：<asp:CheckBox ID="CheckBox1" runat="server" Text="A-Z" Checked="true"/>
            &nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox2" runat="server" Text="a-z" Checked="true"/>
            &nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox3" runat="server" Text="0-9" Checked="true"/>
            &nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox4" runat="server" Text="!@#$%^&*" Checked="false"/>
        </p>
        <p>密码长度：<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        </p>
        <p>密码标识：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>生成结果：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="生成密码" OnClick="Button1_Click"/>
        </p>

    </div>
</form>
</body>
</html>