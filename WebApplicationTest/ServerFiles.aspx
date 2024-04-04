<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerFiles.aspx.cs" Inherits="WebApplicationTest.ServerFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>批量导入文件测试</title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Width="541px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="目录回退" />
            <asp:Button ID="Button2" runat="server" Text="详细" />
            <br />
            <asp:Label ID="lblFilePath" runat="server"></asp:Label>
            <br />
        </p>
        <p>
            <asp:DropDownList ID="ddlFilePath" runat="server" Height="210px" Width="659px">
                <asp:ListItem>A</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="目录/文件名称" />
                    <asp:BoundField DataField="FileType" HeaderText="类型" />
                    <asp:BoundField DataField="FileSize" HeaderText="文件大小" />
 
                    <asp:BoundField DataField="DirectoryName" HeaderText="所在目录" />
                    <asp:BoundField DataField="FullName" HeaderText="文件全称" />
                    <asp:BoundField DataField="Extension" HeaderText="文件扩展名" />
                    <asp:BoundField DataField="CreationTime" HeaderText="文件创建时间" />
                    
  
                                   </Columns>
            </asp:GridView>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    <div>
    
    </div>
    </form>
</body>
</html>
