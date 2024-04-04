<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="CKEditor_v5_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>CKEditor 5 文章编辑测试</title>

    <script>
        (function insertCKE5Script() {
            if (/rv:11.0/i.test(navigator.userAgent)) {
                return;
            }

            //const versions = {
            //    classic: "1.0.0-alpha.2",
            //    balloon: "1.0.0-alpha.2",
            //    inline: "1.0.0-alpha.2",
            //};
            //const types = Object.keys( versions );
            //const hash = window.location.hash.substring( 1 );
            //const type = types.indexOf( hash ) > -1 ? hash : types[ 0 ];

            //<![CDATA[
            document.write(("<script src='https://ckeditor.com/assets/libs/ckeditor5/VERSION/editor-TYPE.js'><\/script>").replace('TYPE', type).replace('VERSION', versions[type]));
            //]]>
        })();
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div>

    </div>
</form>
</body>
</html>