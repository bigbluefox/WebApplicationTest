<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSWork.aspx.cs" Inherits="WebApplicationTest.JSWork" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="author" content="Yeeku.H.Lee(CrazyIt.org)" />
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">    

    <title>HTML5 Web Worker的使用</title>

    <script src="Scripts/jquery-1.12.4.min.js"></script>

    <script type="text/javascript">
        //WEB页主线程
        //var worker = new Worker("Scripts/Work/worker.js"); //创建一个Worker对象并向它传递将在新线程中执行的脚本的URL
        //worker.postMessage("hello world");     //向worker发送数据
        //worker.onmessage = function (evt) {     //接收worker传过来的数据函数
        //    console.log(evt.data);              //输出worker发送来的数据
        //}
        var worker; // HTML5 Web Worker

        $(function () {
            
            //try {
            //    document.getElementById('msg').innerHTML = "支持 HTML5 javascript多线程解决方案";
            //    // 使用Worker启动多线程来计算、收集质数
            //    worker = new Worker('Scripts/Work/worker.js');
            //    worker.onmessage = function (event) {
            //        document.getElementById('result').innerHTML
            //            += event.data + ", ";
            //    };                
            //} catch (e) {
            //    document.getElementById('msg').innerHTML = "不支持 HTML5 javascript多线程解决方案";
            //    var n = 1;
            //    search:
            //        while (n < 99999) {
            //            // 开始搜寻下一个质数
            //            n += 1;
            //            for (var i = 2; i <= Math.sqrt(n) ; i++) {
            //                // 如果除以n的余数为0，开始判断下一个数字。
            //                if (n % i == 0) {
            //                    continue search;
            //                }
            //            }
            //            document.getElementById('result').innerHTML += (n + ", ");
            //        }
            //} 

            //try {
                
            //    //// 使用Worker启动多线程来计算、收集质数
            //    //worker = new Worker('Scripts/Work/worker.js');
            //    //worker.onmessage = function (event) {
            //    //    document.getElementById('Div1').innerHTML
            //    //        += event.data + ", ";
            //    //};

            //    var timer = (new Date()).valueOf();
            //    worker = new Worker('Scripts/Work/fibonacci.js');

            //    worker.addEventListener('message', function (event) {
            //        var timer2 = (new Date()).valueOf();
            //        //console.log('结果：' + event.data, '时间:' + timer2, '用时：' + (timer2 - timer));
            //        document.getElementById('Div1').innerHTML += '结果：' + event.data + '，时间:' + timer2 + '，用时：' + (timer2 - timer) + '<br/>';


            //    }, false);

            //    //console.log('开始计算：40', '时间:' + timer);
            //    document.getElementById('Div1').innerHTML += '开始计算：40，时间:' + timer + '<br/>';

            //    setTimeout(function () {
            //        //console.log('定时器函数在计算数列时执行了', '时间:' + (new Date()).valueOf());
            //        document.getElementById('Div1').innerHTML += '定时器函数在计算数列时执行了，时间:' + (new Date()).valueOf() + '<br/>';

            //    }, 1000);
            //    worker.postMessage(40);
            //    //console.log('我在计算数列的时候执行了', '时间:' + (new Date()).valueOf());
            //    document.getElementById('Div1').innerHTML += '我在计算数列的时候执行了，时间:' + (new Date()).valueOf() + '<br/>';

            //} catch (e) {
            //    document.getElementById('msg').innerHTML = "不支持 HTML5 javascript多线程解决方案";
            //    var n = 1;
            //    search:
            //        while (n < 99999) {
            //            // 开始搜寻下一个质数
            //            n += 1;
            //            for (var i = 2; i <= Math.sqrt(n) ; i++) {
            //                // 如果除以n的余数为0，开始判断下一个数字。
            //                if (n % i == 0) {
            //                    continue search;
            //                }
            //            }
            //            document.getElementById('Div1').innerHTML += (n + ", ");
            //        }
            //}

            //debugger;

            if (typeof (Worker) !== "undefined") {
                if (typeof (worker) == "undefined") {
                    worker = new Worker("Scripts/Work/Test.js");
                }
                worker.onmessage = function (event) {

                    //debugger;
                    var rst = event.data;

                    //document.getElementById("result").innerHTML = event.data;
                };

                document.getElementById('msg').innerHTML = "支持 HTML5 javascript多线程解决方案";
            }
            else {
                document.getElementById('msg').innerHTML = "不支持 HTML5 javascript多线程解决方案";
                //document.getElementById("result").innerHTML = "Sorry, your browser does not support Web Workers...";
            }

        });

    </script>

    <style type="text/css">
        p{ width: 100%;display: block;}
         div{ width: 49.5%;float: left;display: block;border: 1px solid #00bfff;height: auto;}
    </style>

</head>
<body>
	<p>已经发现的所有质数【<span id="msg"></span>】：</p>
    <div id="result"></div>
    <div id="Div1"></div>
	<script type="text/javascript">
	    //var n = 1;
	    //search:
	    //    while (n < 99999) {
	    //        // 开始搜寻下一个质数
	    //        n += 1;
	    //        for (var i = 2; i <= Math.sqrt(n) ; i++) {
	    //            // 如果除以n的余数为0，开始判断下一个数字。
	    //            if (n % i == 0) {
	    //                continue search;
	    //            }
	    //        }
	    //        document.getElementById('result').innerHTML += (n + ", ");
	    //    }

	    // 使用Worker启动多线程来计算、收集质数
	    //var worker = new Worker('Scripts/Work/worker.js');
	    //worker.onmessage = function (event) {
	    //    document.getElementById('result').innerHTML
        //        += event.data + ", ";
	    //};

	</script>
<br/>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</body>
</html>