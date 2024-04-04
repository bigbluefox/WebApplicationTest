<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplicationTest.Fakes.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>框架页面</title>

    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="../Scripts/Hsp.js"></script>
    <script src="../Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        
    </style>

    <script type="text/javascript">

        // 通过多层DIV样式设置iframe中页面位置；

        $(function() {

            //alert(wfId);

            //if (wfId == undefined || wfId.length === 0) {
            //    wfId = "1235";
            //}

            var frameHeight = HSP.Common.AvailHeight() - 20;
            if (!HSP.Browser.IS_IE) {
                frameHeight -= 0;
            }

            var url = "HtmlPage1.html?TID=&GID=&UID=&UNAME=";
            var content = '<iframe id="iframes" src="' + url + '" width="100%" height="' + (frameHeight - 0)
                + '" frameborder="0" marginheight="0" marginwidth="0" border="0" scrolling="no" onload="this.height=this.contentWindow.document.documentElement.scrollHeight"></iframe>';
            $('#iframe').html(content);
            $.parser.parse('#iframe');

            setTimeout("SetIframeStyle()", 500);

        });


        function SetIframeStyle() {

            //debugger;

            var obj = document.getElementById("iframes").contentWindow;

            //document.getElementById("iframes").contentWindow.body.paddingLeft = -1000;
            //document.getElementById("iframes").contentWindow.body.marginLeft = -1000;

            //alert("?*?");

            changeStyle();
        }

        function changeStyle() {
            var x = document.getElementById("iframes");
            var y = (x.contentWindow || x.contentDocument);
            if (y.document) y = y.document;
            //y.body.style.backgroundColor = "#0000ff";

            //y.body.style.paddingLeft = -600;
            //y.body.style.marginLeft = -600;

            //y.body.style.left = -200;
            //y.body.style.top = -200;

            //y.body.style.vspace = -100;
            //y.body.style.hspace = -200;

            //y.html.style.paddingLeft = -600;
            //y.html.style.marginLeft = -600;

            //y.html.style.left = -200;
            //y.html.style.top = -200;

            //y.html.style.vspace = -100;
            //y.html.style.hspace = -200;

        }

    </script>

</head>
<body style="width: 100%">

<style type="text/css">
    #iframe { border: 1px solid #00ff00; }
</style>

<form id="form1" runat="server">
    <div style="margin: 0 auto; width: 800px; border: 1px solid red;">
        <div style="margin-left: -100px; margin-top: -5px; border: 1px solid green;">
            <div id="iframe" style="width: 100%;"></div>
        </div>
    </div>
</form>
</body>
</html>