<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetRemoteFile.aspx.cs" Inherits="WebApplicationTest.Files.GetRemoteFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>获取远程文件</title>
    
    <style type="text/css">
        
    </style>
    
    <script type="text/javascript">
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnGetRemoteFile" runat="server" Text="获取远程文件" OnClick="btnGetRemoteFile_Click" />
    </div>
    </form>
</body>
</html>
