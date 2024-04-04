<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IconDiv.aspx.cs" Inherits="WebApplicationTest.CssSprites.IconDiv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>图标测试</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>
    <link href="../Styles/icon.css" rel="stylesheet" />

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <!--[if lt IE 9]>
        <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Scripts/zTree/jquery.ztree.core.js"></script>
    <script src="/Scripts/uploadifive/jquery.uploadifive.min.js"></script>
    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>
    
    <style type="text/css">
        div a, div span, div>div{ display: inline-block;float: left;margin: 0 5px 0 0;}
    </style>
    
    <script type="text/javascript">
        

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; border: 0px solid #a0522d;">
        <a href="#" class="icon icon-delete ">&nbsp;</a>
        <span class="icon icon-view">&nbsp;</span>
        <div class="icon icon-file ">&nbsp;</div>
        <div class="icon icon-edit">&nbsp;</div>
        <div class="icon icon-info">&nbsp;</div>
    </div>
    </form>
</body>
</html>
