<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Browser.aspx.cs" Inherits="WebApplicationTest.Browser" %>

<!DOCTYPE html>

<html lang="zh-ch">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>浏览器信息</title>
        <script src="Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="Scripts/Hsp.js"></script>

    <style type="text/css">
        img{ cursor: pointer;}
    </style>
    <script type="text/javascript">

        $(function() {
            //如何用js得到当前页面的url信息方法(JS获取当前网址信息) 

            //1，设置或获取对象指定的文件名或路径。
            //alert(window.location.pathname);
            //2，设置或获取整个 URL 为字符串。
            //alert(window.location.href);
            ////3，设置或获取与 URL 关联的端口号码。
            //alert(window.location.port)
            ////4，设置或获取 URL 的协议部分。
            //alert(window.location.protocol)
            ////5，设置或获取 href 属性中在井号“#”后面的分段。
            //alert(window.location.hash)
            ////6，设置或获取 location 或 URL 的 hostname 和 port 号码。
            //alert(window.location.host)
            ////7，设置或获取 href 属性中跟在问号后面的部分。
            //alert(window.location.search)
            ////8，获取变量的值(截取等号后面的部分)
            //var url = window.location.search;
            ////    alert(url.length);
            ////    alert(url.lastIndexOf('='));
            //var loc = url.substring(url.lastIndexOf('=')+1, url.length);
            ////9，用来得到当前网页的域名
            //var domain = document.domain;


            //alert(isNaN("2222DD"));

            //alert(parseFloat("0") + " * " + parseFloat("0.0"));



            $.ajax({
                type: 'HEAD', // 获取头信息，type=HEAD即可
                url: "/Handler/ReadPdfHandler.ashx",
                //url:"http://device.qq.com/cgi-bin/device_cgi/remote_bind_get_Verify",
                complete: function (xhr, data) {
                    // 获取相关Http Response header
                    var wpoInfo = {
                        // 服务器端时间
                        "date": xhr.getResponseHeader('Date'),
                        // 如果开启了gzip，会返回这个东西
                        "contentEncoding": xhr.getResponseHeader('Content-Encoding'),
                        // keep-alive ？ close？
                        "connection": xhr.getResponseHeader('Connection'),
                        // 响应长度
                        "contentLength": xhr.getResponseHeader('content-length'),
                        // 服务器类型，apache？lighttpd？
                        "server": xhr.getResponseHeader('Server'),
                        "vary": xhr.getResponseHeader('Vary'),
                        "transferEncoding": xhr.getResponseHeader('Transfer-Encoding'),
                        // text/html ? text/xml?
                        "contentType": xhr.getResponseHeader('Content-Type'),
                        "cacheControl": xhr.getResponseHeader('Cache-Control'),
                        // 生命周期？
                        "exprires": xhr.getResponseHeader('Exprires'),
                        "lastModified": xhr.getResponseHeader('Last-Modified')
                    };

                    //console.log(xhr.getAllResponseHeaders());

                    //Content-Disposition: attachment;filename=QSDYY%e3%80%80201.001-2018%e3%80%80%e4%b8%8a%e6%b5%b7%e4%b8%8a%e7%94%b5%e7%94%b5%e5%8a%9b%e8%bf%90%e8%90%a5%e6%9c%89%e9%99%90%e5%85%ac%e5%8f%b8%e8%a7%84%e5%88%92%e7%ae%a1%e7%90%86%e6%a0%87%e5%87%86


                    console.log(xhr.getResponseHeader('Content-Disposition'));

                    var contentDisposition = xhr.getResponseHeader('Content-Disposition');
                    var arr = contentDisposition.split(";filename=");
                    var filename = arr[1];
                    filename = decodeURI(filename); // 解析URL编码


                    console.log(filename);




                }
            });






        });


    </script>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br/>
        <label id="browserInfo"></label>
        <br/>
        <a href="#"></a>
        <input type="radio" checked="checked"/>
        <input type="radio" disabled="disabled"/>
        <br/>
        <img id="validcode" alt="验证码，看不清，换一张图片" src="Handler/CheckCodeHandler.ashx" style="margin-top: -3px; vertical-align: middle;" onclick=" this.src = 'Handler/CheckCodeHandler.ashx?' + (new Date().getTime()); "/>
    </div>
</form>
    <p>
        <input id="Text1" type="text" style="padding-left: 10px;" /></p>
</body>
</html>

<script type="text/javascript">
    var obj = document.getElementById("browserInfo");
    obj.innerHTML = navigator.userAgent;
</script>