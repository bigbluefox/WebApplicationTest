<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HashMd5.aspx.cs" Inherits="WebApplicationTest.Files.HashMd5" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge"/>
    <title>计算大文件的MD5值测试</title>
    <script src="../Scripts/jquery/jquery-1.12.4.js"></script>
    <script src="../Scripts/spark-md5.js"></script>

    <style type="text/css">
        input[type='file'] {
            height: 26px;
            width: 283px;
        }

        input[type='text'] {
            border: 1px solid #ccc !important;
            height: 22px;
            width: 280px;
        }

        /*input{border: 1px solid #a4bed4 !important;}*/

        input[type='button'], input[type='submit'] {
            border: 1px solid #ccc !important;
            height: 26px;
        }
    </style>
</head>
<body>

<h1>计算本地文件MD5</h1>

<form id="form1" runat="server">
    <div>

        <asp:FileUpload ID="FileUpload1" runat="server"/>
        <br/><br/>
        <asp:TextBox ID="txtMd5Value" runat="server"></asp:TextBox>
        <br/><br/>
        <asp:Button ID="btnMd5Value" runat="server" Text="计算文件前端MD5值1" OnClientClick="return getMd5_1();" UseSubmitBehavior="False"/>
        <asp:Button ID="Button1" runat="server" Text="计算文件前端MD5值2" OnClientClick="return getMd5();" UseSubmitBehavior="False"/>
        <asp:Button ID="btnMd5Test" runat="server" Text="计算文件后台MD5值3" OnClick="btnMd5Test_Click"/>
        <br/><br/>
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>

    </div>
</form>
</body>
</html>

<script type="text/javascript">

    var rst = $("#<% = lblResult.ClientID %>");


    function getFileMd5() {
        // 获取文件
        //var file = document.getElementById("fileInput").files[0];
        var file = $("#<% = FileUpload1.ClientID %>")[0].files[0];

        if (file == null) {
            rst.html("请添加要计算的文件");
            return;
        }

        // 创建文件读取对象，此对象允许Web应用程序异步读取存储在用户计算机上的文件内容
        var fileReader = new FileReader();

        // 根据浏览器获取文件分割方法
        //var blobSlice = File.prototype.mozSlice || File.prototype.webkitSlice || File.prototype.slice;

        // 指定文件分块大小(2M)
        var chunkSize = 5 * 1024 * 1024;

        // 计算文件分块总数
        var chunks = Math.ceil(file.size / chunkSize); //反回大于参数x的最小整数 8=》8  8.4=》9  8.5=》9 -8.5=》-8

        // 指定当前块指针
        var currentChunk = 0;

        // 创建MD5计算对象
        var spark = new SparkMD5.ArrayBuffer();

        // 记录开始时间
        var startTime = new Date().getTime();

        // FileReader分片式读取文件
        loadNext();

        // 获取输出信息区域
        //var showInfo = $(".showInfo");
        //showInfo.html('');

        // 当读取操作成功完成时调用
        fileReader.onload = function() {

            // 将文件内容追加至spark中
            spark.append(this.result);
            currentChunk += 1;

            rst.html("正在检查文件: " + (currentChunk + 1) + "/" + chunks);

            // 判断文件是否都已经读取完
            if (currentChunk < chunks) {
                loadNext();
            } else {
                var md5Value = spark.end();
                var msg = "文件加密结束，密钥为：" + md5Value;
                rst.html(msg);
                console.log(msg);

                $("#<% = txtMd5Value.ClientID %>").val(md5Value);

                // 计算spack中内容的MD5值,并返回
                rst.append('MD5值为： <strong><font color="green">' + md5 + '</font></strong><br/>');
                rst.append('计算时长 ： <strong><font color="green">' + (new Date().getTime() - startTime) + '</font></strong> 毫秒！<br/>');
                //return spark.end();
            }
        };

        // FileReader分片式读取文件
        function loadNext() {
            // 计算开始读取的位置
            var start = currentChunk * chunkSize;
            // 计算结束读取的位置
            var end = start + chunkSize >= file.size ? file.size : start + chunkSize;
            //fileReader.readAsArrayBuffer(blobSlice.call(file, start, end));

            fileReader.readAsArrayBuffer(file.slice(start, end)); //读取为二进制字符串
        }
    }

    function getMd5() {

        //debugger;

        var file = $("#<% = FileUpload1.ClientID %>")[0].files[0];

        if (file == null) {
            rst.html("请添加要计算的文件");
            return;
        }

        //获取文件的md5字符串，用于标识文件的唯一性。
        calculate(file);

        //获取文件的加密字符串
        function calculate(file) {
            var fileReader = new FileReader();
            var chunkSize = 1024 * 1024 * 5; //每次读取5MB
            var chunksCount = Math.ceil(file.size / chunkSize); //反回大于参数x的最小整数 8=》8  8.4=》9  8.5=》9 -8.5=》-8
            var currentChunk = 0; //当前块的索引
            var spark = new SparkMD5();
            fileReader.onload = function(e) {
                console.log((currentChunk + 1) + "/" + chunksCount);
                rst.html("正在检查文件: " + (currentChunk + 1) + "/" + chunksCount);
                spark.appendBinary(e.target.result); // 添加二进制字符串
                currentChunk++;
                if (currentChunk < chunksCount) {
                    loadNext();
                } else {
                    var md5value = spark.end();
                    var msg = "文件加密结束，密钥为：" + md5value;
                    rst.html(msg);
                    console.log(msg);

                    $("#<% = txtMd5Value.ClientID %>").val(md5value);

                    //checkfile(md5value, file); //检查服务器是否存在该文件，存在就从断点继续上传
                }
            };

            function loadNext() {
                var start = currentChunk * chunkSize; //计算读取开始位置
                var end = start + chunkSize >= file.size ? file.size : start + chunkSize; //计算读取结束位置
                fileReader.readAsArrayBuffer(file.slice(start, end)); //读取为二进制字符串
            };

            loadNext();
        }


        //var fd = new FormData();
        //fd.append('fileData', file);
        //fd.append('filename', file.name);
        //fd.append('filepath', "");
        //console.log(fd);

        return false;

        //fd = { id: "jdskfjldskjflkds j", row: Math.random() }

        // https://blog.csdn.net/qq_41802303/article/details/80066160
        // 如今主流浏览器都开始支持一个叫做FormData的对象，有了这个FormData，我们就可以轻松地使用Ajax方式进行文件上传了

        $.ajax({
            url: '/Handler/FileMd5Handler.ashx?rnd=' + Math.random(),
            type: 'POST',
            data: fd,
            // 告诉jQuery不要去处理发送的数据，用于对data参数进行序列化处理 这里必须false
            processData: false, //重要
            // 告诉jQuery不要去设置Content-Type请求头
            contentType: false, //重要，必须
            success: function(rst) {

                if (rst) $("#<% = lblResult.ClientID %>").html(rst);

            },
            complete: function(xhr, errorText, errorType) {

                //debugger;

                var p = "";
                if (window.console) console.log(xhr);
                alert("请求完成后");
            },
            error: function(xhr, errorText, errorType) {
                alert("请求错误后");
                if (window.console) console.log(xhr);
            },
            beforSend: function() {
                alert("请求之前");
            }
        });

    };

    function getMd5_1() {

        //debugger;

        var file = $("#<% = FileUpload1.ClientID %>")[0].files[0];

        if (file == null) {
            rst.html("请添加要计算的文件");
            return;
        }

        //获取文件的md5字符串，用于标识文件的唯一性。
        calculate(file);

        //获取文件的加密字符串
        function calculate(file) {
            var fileReader = new FileReader();
            var chunkSize = 1024 * 200 * 1; //每次读取200KMB
            var chunksCount = Math.ceil(file.size / chunkSize); //反回大于参数x的最小整数 8=》8  8.4=》9  8.5=》9 -8.5=》-8
            var currentChunk = 0; //当前块的索引
            var spark = new SparkMD5.ArrayBuffer();

            fileReader.onload = function(e) {
                console.log((currentChunk + 1) + "/" + chunksCount);
                rst.html("正在检查文件: " + (currentChunk + 1) + "/" + chunksCount);
                //spark.appendBinary(e.target.result); // 添加二进制字符串
                spark.append(this.result);
                //currentChunk++;

                //if (currentChunk < chunksCount) {
                //    loadNext();
                //} else {}

                    var md5Value = spark.end();
                    var msg = "文件加密结束，密钥为：" + md5Value;
                    rst.html(msg);
                    console.log(msg);

                    $("#<% = txtMd5Value.ClientID %>").val(md5Value);

                    rst.append('MD5值为：<strong><font color="green">' + md5 + '</font></strong><br/>');
                    rst.append('计算时长：<strong><font color="green">' + (new Date().getTime() - startTime) + '</font></strong> 毫秒！<br/>');
                
            };

            function loadNext() {
                var start = currentChunk * chunkSize; //计算读取开始位置
                var end = start + chunkSize >= file.size ? file.size : start + chunkSize; //计算读取结束位置
                fileReader.readAsArrayBuffer(file.slice(start, end)); //读取为二进制字符串
            };

            loadNext();
        }
    };

</script>

<script>
    var log = document.getElementById("<% = lblResult.ClientID %>");
    document.getElementById("<% = FileUpload1.ClientID %>").addEventListener("change", function() {

        // 记录开始时间
        //var startTime = new Date().getTime();

        //var blobSlice = File.prototype.slice || File.prototype.mozSlice || File.prototype.webkitSlice,
        //        file = this.files[0],
        //        chunkSize = 2097152, // read in chunks of 2MB
        //        chunks = Math.ceil(file.size / chunkSize),
        //        currentChunk = 0,
        //        spark = new SparkMD5.ArrayBuffer(),
        //        frOnload = function (e) {
        //            log.innerHTML += "\nread chunk number " + parseInt(currentChunk + 1) + " of " + chunks;

        //            spark.append(e.target.result); // append array buffer
        //            currentChunk++;
        //            if (currentChunk < chunks)
        //                loadNext();
        //            else
        //                log.innerHTML += "\n加载结束 :\n\n计算后的文件md5:\n" + spark.end() + "\n\n现在你可以选择另外一个文件!\n";
        //            log.innerHTML += '计算时长 ： <strong><font color="green">' + (new Date().getTime() - startTime) + '</font></strong> 毫秒！<br/>';
        //        },
        //        frOnerror = function () {
        //            log.innerHTML += "\糟糕，好像哪里错了.";
        //        };

        //function loadNext() {
        //    var fileReader = new FileReader();
        //    fileReader.onload = frOnload;
        //    fileReader.onerror = frOnerror;
        //    var start = currentChunk * chunkSize,
        //            end = ((start + chunkSize) >= file.size) ? file.size : start + chunkSize;
        //    fileReader.readAsArrayBuffer(blobSlice.call(file, start, end));
        //};

        //loadNext();
    });


</script>