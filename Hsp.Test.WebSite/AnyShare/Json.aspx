<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Json.aspx.cs" Inherits="AnyShare_Json" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script type="text/javascript">
        
        $(function () {
            $("#btnJsonTest").click(function () {
                JsonTest();
            });

        });

        function JsonTest() {
            var url = "/Handler/AnyShareHandler.ashx?OP=TEST";
            $.get(url + "&rnd=" + (Math.random() * 10), function (data) {
                if (data == null) {
                    $(".alert-link").html("服务器在线");
                } else {
                    $(".alert-link").html(data.Message);
                }
            });
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input id="btnJsonTest" type="button" value="button" />
        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
        <a href="#" target="_blank"></a>
    </div>
        
    </form>
</body>
</html>
