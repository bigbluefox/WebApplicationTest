<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mobile.aspx.cs" Inherits="WebApplicationTest.Mobile" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport"/>
    <title>Mobile Test</title>
    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>

    <script src="Scripts/jquery-2.2.4.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>

    <style type="text/css">
        * { font-family: "Microsoft YaHei", 微软雅黑, "MicrosoftJhengHei", 华文细黑, STHeiti, MingLiu, Tahoma, Arial, Roboto, "Droid Sans", "Helvetica Neue", "Droid Sans Fallback", "Heiti SC", sans-self; }
    </style>

    <script type="text/javascript">

        $(function() {
            
            debugger;
            var d = /Date(1354116249000)/;

            d = /Date(-62135596800000)/;

            var dd = new Date(d);

            var ddd = dd;

            alert(GetLocalTime(d.toLocaleString()));



        });

        var GetLocalTime = function (s) {
            s = s.replace("/Date(", "").replace(")/", "");
            //debugger;
            //s = s.substr(6, 13);
            return new Date(parseInt(s)).toLocaleString().replace(/年|月/g, "-").replace(/日/g, "");
        };



    </script>
</head>
<body>
<div class="easyui-navpanel">
    <header>
        <div class="m-toolbar">
            <span class="m-title">MSO标准服务系统</span>
        </div>
    </header>
    <div style="margin: 20px auto; width: 240px; height: 145px; overflow: hidden">
        <img src="/Images/Logo.png" style="margin: 0; width: 100%; height: 100%;" alt="MSO Logo"/>
    </div>
    <div style="padding: 0 20px">
        <div style="margin-bottom: 10px">
            <input class="easyui-textbox" data-options="prompt:'请输入账号...',iconCls:'icon-man'" style="width: 100%; height: 38px"/>
        </div>
        <div>
            <input class="easyui-passwordbox" data-options="prompt:'请输入密码...'" style="width: 100%; height: 38px"/>
            <input class="easyui-switchbutton">
        </div>
        <div style="text-align: center; margin-top: 30px">
            <a href="#" class="easyui-linkbutton" style="width: 100%; height: 40px">
                <span style="font-size: 16px">登录</span><span class="m-badge">2</span></a>
        </div>
        <div style="text-align: center; margin-top: 30px">
            <%--<a href="#" class="easyui-linkbutton" plain="true" outline="true" style="width:100px;height:35px"><span style="font-size:16px">Register</span></a>--%>
            
        </div>
    </div>

    <footer>
        <%--<div class="m-toolbar"> </div>--%>
            <div class="m-title" style="height: 36px; line-height: 36px; text-align: center;">
                <div class="gray">&copy; <% = DateTime.Now.Year %> 北京普思特信息技术有限公司 提供技术支持</div>
            </div>
       
    </footer>
</div>
</body>
</html> 