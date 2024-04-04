<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IconsTest.aspx.cs" Inherits="WebApplicationTest.Bootstrap.IconsTest" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Icons</title>
    <link href="../Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>

    <link href="../Scripts/V1.5.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Scripts/V1.5.3/themes/icon.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="../Scripts/html5shiv.min.js"></script>
        <script src="../Scripts/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <!--<script src="https://cdn.bootcss.com/jquery/1.12.4/jquery.min.js"></script>-->
    <script src="../Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="../Scripts/V1.5.3/jquery.easyui.min.js"></script>
    <script src="../Scripts/V1.5.3/locale/easyui-lang-zh_CN.js"></script>

    <!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
    <!--<script src="https://cdn.bootcss.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>-->
    <script src="../Scripts/bootstrap/js/bootstrap.min.js"></script>
    
<style type="text/css">    
    body{ padding: 15px;}
    .icon-demo{ padding-left: 5px;}
    .icon-demo i{ font-size: 36px;}
</style>

</head>
<body>

<!-- 主体区域 -->
<div class="container-fluid">
    <div class="row">
        <button type="button" class="btn btn-primary" id="btnIcons">
            <span class="glyphicon glyphicon-picture" aria-hidden="true"></span> 图标选择
        </button>
        <blockquote style="margin-top: 10px;">
          <p>已选图标：</p>
          <footer></footer>
        </blockquote>
        <div class="icon-demo"><i class=""></i></div>
    </div>
</div>

<!-- #include file = "/Pages/Glyphicons.html" --> 
     
</body>
</html>
