<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplicationTest.CssSprites.Test" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>图标测试</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="/Scripts/uploadifive/uploadifive.css" rel="stylesheet"/>
    <link href="/Scripts/zTree/zTreeStyle.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>

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
        .thumbnail { overflow: hidden; }

        .thumbnail div {
            width: 100%;
            display: block;
            float: left;
        }

        .thumbnail div:first-child { height: 48px; }

        .thumbnail div img,
        .thumbnail div span,
        .thumbnail div a { display: block; }

        .thumbnail div img,
        .thumbnail div span { float: left; }

        .thumbnail div a { float: right; }

        .thumbnail div img {
            max-height: 48px;
            max-width: 96px;
        }

        .thumbnail div img{ vertical-align: central;}
    </style>

    <script type="text/javascript">

        $(function() {
            
            $("#Button1").unbind('click').bind("click", function () {

                //debugger;

                //alert(imgData.length);

                //var data = $("body").data("imgData");
                //if (data) {
                //    alert(data.length);
                //}

                var objs = $("input[type='text']");

                //alert(objs.length);

                var s = "";
                $.each($("input[name='num']"), function () {
                    s += this.value + ",";
                });

                //alert(s);

                var img = document.getElementById("img1");
                    //$("#img1");

                alert(img.src);

            });

            $("#Button2").unbind('click').bind("click", function () {

                debugger;

                var data = { "Name": "ppp.jpg", "Width": 15, "Height": 10 };
                var str = HSP.Common.ObjToString(data);
                var s = str;

                // "{\"Name\":\"ppp.jpg\",\"Width\":15,\"Height\":10}"

                var arr = [{ "Name": "A.jpg", "Width": 12, "Height": 12 }, { "Name": "B.jpg", "Width": 13, "Height": 13 }];
                str = HSP.Common.ObjToString(arr);
                s = str;

                // "[{\"Name\":\"A.jpg\",\"Width\":12,\"Height\":12},{\"Name\":\"B.jpg\",\"Width\":13,\"Height\":13}]"

                data = { "Name": "ppp.jpg", "Width": 15, "Height": 10, "Data": arr };
                str = HSP.Common.ObjToString(data);
                s = str;

                // "{\"Name\":\"ppp.jpg\",\"Width\":15,\"Height\":10,\"Data\":[{\"Name\":\"A.jpg\",\"Width\":12,\"Height\":12},{\"Name\":\"B.jpg\",\"Width\":13,\"Height\":13}]}"

            });
        });


        function ClearMe(id) {
            var obj = $("#div" + id);
            obj.empty();
            obj.hide();
        }

    </script>

</head>
<body>
<%--<form id="form1" runat="server"></form>--%>

<div class="container">
    <div class="row" style="width: 600px;">

        <div class="col-sm-9 col-md-6" id="div1">
            <div class="thumbnail">
                <div><img id="img1" src="/Images/AddFile_16x16.png" alt="..."/><span>16X16</span><a href="#" onclick="ClearMe(1);">删除</a></div>
                <div class="caption">AddFile_16x16.png</div>
                <div><input id="imgalias1" class="form-control" type="text" value="AddFile_16x16.png" title="图标别名，设为样式名称" placeholder="图标别名，设为样式名称"/></div>
                <input type="hidden" id="imgname1" value="AddFile_16x16.png"/>
                <input type="hidden" id="imgwidth1" value="16"/>
                <input type="hidden" id="imgheight1" value="16"/>
                <input type="hidden" name="num" value="1"/>
            </div>
        </div>

        <div class="col-sm-9 col-md-6" id="div2">
            <div class="thumbnail">
                <div>
                    <img src="/Images/FileTpe/256/doc.png"/><span> 256X256</span><a href="#" onclick="ClearMe(2);">删除</a>
                </div>
                <div class="caption">AddFile_16x16.png</div>
                <div>
                    <input id="Text2" class="form-control" type="text" value="AddFile_16x16.png" title="图标别名" placeholder="图标别名"/>
                </div>

                <input type="hidden" name="num" value="2"/>
            </div>

        </div>
        
        <div class="col-sm-9 col-md-6" id="div3">
            <div class="thumbnail">
                <div>
                    <img src="../Images/AddFile_16x16.png"/><span> 16X16</span><a href="#" onclick="ClearMe(3);">删除</a>
                </div>
                <div class="caption">AddFile_16x16.png</div>
                <div>
                    <input id="Text3" class="form-control" type="text" value="AddFile_16x16.png" title="图标别名" placeholder="图标别名"/>
                </div>


            </div>
        </div>

        <div class="col-sm-9 col-md-6" id="div4">
            <div class="thumbnail">
                <div>
                    <img src="/Images/FileTpe/256/doc.png"/><span> 256X256</span><a href="#" onclick="ClearMe(4);">删除</a>
                </div>
                <div class="caption">AddFile_16x16.png</div>
                <div>
                    <input id="Text4" class="form-control" type="text" value="AddFile_16x16.png" title="图标别名" placeholder="图标别名"/>
                </div>

            </div>

        </div>       
        
        <input id="Button1" type="button" value="检查数组" class="btn btn-primary"/>
        <input id="Button2" type="button" value="查看数据" class="btn btn-primary"/>

    </div>
</div>

</body>
</html>