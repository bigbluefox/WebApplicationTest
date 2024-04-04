<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTest.aspx.cs" Inherits="WebApplicationTest.PrintTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打印测试</title>

    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Styles/zTreeStyle/zTreeStyle.css" rel="stylesheet"/>
    <link href="Styles/main.css" rel="stylesheet"/>

    <%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
    <script src="Scripts/jquery-1.12.4.min.js"></script>
    <script src="Scripts/jquery-migrate-1.4.1.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="Scripts/jquery.ztree.core.min.js"></script>
    <script src="Scripts/jquery.jqprint-0.3.js"></script>
    <script src="Scripts/jQuery.print.js"></script>
    <script src="Scripts/Hsp.js"></script>
    <script src="Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        * { font-family: Arial, "Microsoft YaHei", 微软雅黑, "MicrosoftJhengHei", 华文细黑, STHeiti, MingLiu, Tahoma; }

        body {
            margin: 0 auto;
            width: 100%;
        }

        @media print {
            .noprint { display: none }
        }

        .news_info p { padding: 10px 0px; }

        .auto-style1 {
            margin: 0;
            padding: 0;
        }

        .news_info a, .pc_info a, .news_left a { color: #06C; }

        a {
            COLOR: #4c4c4c;
            TEXT-DECORATION: none;
        }

    </style>

    <script type="text/javascript">
        var width = 0, height = 0;

        $(function() {

            width = HSP.Common.AvailWidth() - 16;
            height = HSP.Common.AvailHeight() - 16;

            $('#panel').panel({
                width: 'auto', //width
                //height: height,
                title: '标准体系文档导入',
                tools: [
                    {
                        iconCls: 'icon-add',
                        handler: function() { alert('new') }
                    }, {
                        iconCls: 'icon-print',
                        handler: function() {
                            //alert('print') 
                            //$("body").jqprint();

                            //jQuery('body').print();

                            //$("body").printArea();

                            doPrint("打印预览...");

                            //printPreview();


                            //jQuery('.auto-style1').print();
                        }

                    }, {
                        iconCls: 'icon-save',
                        handler: function() { alert('save') }
                    }
                ]
            });
        });

        function doPrint(how) {

            //debugger;

            //var bodyHtml = window.document.body.innerHTML;

            ////if ($('#jatoolsPrinter').length == 0) {
            ////    $('<OBJECT ID="jatoolsPrinter" style="display:none" CLASSID="CLSID:B43D3361-D075-4BE2-87FE-057188254255" codebase="jatoolsPrinter.cab#version=5,0,0,0"></OBJECT>').appendTo($('body'));
            ////    bodyHtml = window.document.body.innerHTML;
            ////}

            //if ($('#page1').length == 0) {
            //    $('body').html("");
            //    $('<OBJECT ID="jatoolsPrinter" style="display:none" CLASSID="CLSID:B43D3361-D075-4BE2-87FE-057188254255" codebase="jatoolsPrinter.cab#version=5,0,0,0"></OBJECT>').appendTo($('body'));
            //    var obj = $('<div id="page1"></div>');
            //    obj.appendTo($('body'));
            //    $(bodyHtml).appendTo(obj);
            //}

            //var myDoc = {
            //    documents: document,
            //    copyrights: '杰创软件拥有版权  www.jatools.com'
            //};
            //var jatoolsPrinter = document.getElementById("jatoolsPrinter");
            //if (how == '打印预览...')
            //    jatoolsPrinter.printPreview(myDoc); // 打印预览
            //else if (how == '打印...')
            //    jatoolsPrinter.print(myDoc, true); // 打印前弹出打印设置对话框
            //else
            //    jatoolsPrinter.print(myDoc, false); // 不弹出对话框打印

            try {
                document.all.WebBrowser.ExecWB(7, 1);
            } catch (e) {
                //alert(e);
                //var p = e;
                $("body").jqprint();

                //jQuery('body').print();
            }
        }

    </script>

</head>
<body>

<form id="form1" runat="server">
    <div id="panel" style="padding: 2px;">

        <object id="WebBrowser" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>
        <div class="noprint">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(1,1)" type="button" value="打开">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(2,1)" type="button" value="关闭所有">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(4,1)" type="button" value="另存为">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(6,1)" type="button" value="打印">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(6,6)" type="button" value="直接打印">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(7,1)" type="button" value="打印预览">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(8,1)" type="button" value="页面设置">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(10,1)" type="button" value="属性">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(17,1)" type="button" value="全选">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(22,1)" type="button" value="刷新">
        <input name="Button" onclick="document.all.WebBrowser.ExecWB(45,1)" type="button" value="关闭">
        </div>

        <%--<OBJECT classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" id="WindowPrint" name="WindowPrint" width="0" height="0"></OBJECT>--%>
        <p class="auto-style1">
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p class="auto-style1">
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p class="auto-style1">
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p class="auto-style1">
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p class="auto-style1">
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p class="auto-style1">
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>


        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <p>
            在去年的广州车展上，上汽集团联合阿里巴巴正式首发了全球首款互联网家轿荣威i6。目前最新的消息显示，新车有望在2月17日正式上市。
        </p>
        <p>
            荣威i6定位于紧凑型轿车，官方号称为A+级，主要对手为思域、朗动等合资品牌紧凑车型。
        </p>
        <p>
            尺寸方面，<strong>荣威i6的三围是567191835*1464mm，轴距2715mm</strong>，相比思域和朗逸来说都有一定优势。
        </p>
        <p>
            外形方面，荣威i6采用了荣威最新的家族化设计，<strong>配备有全LED前大灯组，尾灯组同样采用了LED光源，</strong>顶部还配备有鲨鱼鳍。
        </p>
        <p>
            内饰方面，荣威i6和此前上市的RX5风格一致，但细节部分进行了微调，相比荣威RX5来说少了一些粗犷。<strong>其配备了10.4英寸中控显示屏，搭载阿里巴巴YunOS系统</strong>，差不多是目前最好用的车机，没有之一。
        </p>
        <p>
            动力方面，新车将提供三缸1.0T和四缸1.5T两种动力系统供消费者选择，匹配手动和双离合变速箱。
        </p>
        <br/> <br/>
        <p style="text-align: center;">
            <a href="http://img1.mydrivers.com/img/20170120/bc65b337cb304753a06c3a97fe3cf250.jpg" target="_blank">
                <img alt="全球首款互联网家轿荣威i6来了：硬罡思域朗动" src="http://img1.mydrivers.com/img/20170120/s_bc65b337cb304753a06c3a97fe3cf250.jpg"/>
            </a>
        </p>

    </div>
</form>
</body>
</html>