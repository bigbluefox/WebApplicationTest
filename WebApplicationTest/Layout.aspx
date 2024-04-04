<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Layout.aspx.cs" Inherits="WebApplicationTest.Layout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Styles/main.css" rel="stylesheet"/>

    <%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
    <script src="Scripts/jquery-1.12.4.min.js"></script>
    <script src="Scripts/jquery-migrate-1.4.1.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>

    <style type="text/css">
        h1 small, h2 small, h3 small, h4 small, h5 small, h6 small { color: #838383; }
    </style>
    <script type="text/javascript"></script>


</head>
<body>
<form id="form1" runat="server">
    <div>
        <h1>
            h1. Bootstrap heading <small>Secondary text</small>
        </h1>
        <h2>
            h2. Bootstrap heading <small>Secondary text</small>
        </h2>
        <h3>
            h3. Bootstrap heading <small>Secondary text</small>
        </h3>
        <h4>h4. Bootstrap heading <small>Secondary text</small></h4>
        <h5>h5. Bootstrap heading <small>Secondary text</small></h5>
        <h6>h6. Bootstrap heading <small>Secondary text</small></h6>
    </div>
</form>
</body>
</html>