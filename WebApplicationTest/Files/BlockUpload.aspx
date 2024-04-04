﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockUpload.aspx.cs" Inherits="WebApplicationTest.Files.BlockUpload" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge"/>
    <title>分块上传大文件</title>
    <style type="text/css">
        * {
            font-family: "微软雅黑";
            margin: 0;
            padding: 0;
        }

        .container {
            padding-left: 10px;
            padding-top: 10px;
        }

        .container input {
            background-color: blue;
            border: 0;
            border-radius: 5px;
            color: white;
            cursor: pointer;
            height: 30px;
            line-height: 30px;
            margin-right: 5px;
            outline: none;
            width: 120px;
        }

        #filelist {
            border: solid 1px #eee;
            border-collapse: collapse;
            margin: 10px;
            width: 800px;
        }

        #filelist td {
            border-bottom: solid 1px #eee;
            font-size: 12px;
            height: 30px;
            /*line-height:30px ;*/
            padding: 0 3px;
        }

        .filename {
            text-align: center;
            width: 200px;
        }

        .filestatus {
            /*text-align: center;*/
            width: 100px;
        }

        .fileprogress { text-align: center; }

        .domprogress { width: 320px; }

        .domsize { display: block; }

        #tdmsg { text-align: center; }

        #fileselect { display: none; }

        span.domtime { display: block; }

        h1 {
            height: 60px;
            line-height: 60px;
            padding-left: 10px;
        }

        .dompercent { padding-left: 3px; }

    /*table td { border: 1px solid #a52a2a;}*/

        /*.domtime{ padding-top: 2px;}*/

        body h1{ letter-spacing: 5px;}

    </style>
</head>
<body>

<h1>分块上传大文件，支持断点续传</h1>

<form id="form1" runat="server">
    <div class="container">
        <input type="file" name="fileselect" id="fileselect"/>
        <input type="button" id="btnselect" value="选择上传的文件"/>
        <input type="button" id="btnupload" value="开始上传"/>
    </div>
</form>

<table cellspacing="0" cellpadding="0" id="filelist">
    <%--    <tr>
        <td class="filename">文件名</td>
        <td class="fileprogress">进度</td>
        <td class="filestatus">状态</td>
    </tr>--%>
    <!--<tr>
        <td>人民的名义.avi </td><td>
            <progress value="10" max="100" class="domprogress"></progress><span class="dompercent">10%</span><span class="domsize">0/1.86GB</span></td><td class="filestatus">
            <span class="domstatus">排队中</span></td>
    </tr>-->
    <tr id="trmsg">
        <td colspan="3" id="tdmsg">请选择要上传的文件! </td>
    </tr>

</table>

<script src="../Scripts/jquery/jquery-1.12.4.js"></script>
<script src="../Scripts/spark-md5.js"></script>

<script type="text/javascript">
    $("#btnselect").click(function() {
        $("#fileselect").click();
    });

    $("#fileselect").change(function() {

        $("#filelist").empty();

        //var $title = $('    <tr>\
        //        <td class="filename">文件名</td>\
        //        <td class="fileprogress">进度</td>\
        //        <td class="filestatus">状态</td>\
        //    </tr>\
        //');
        //$("#filelist").append($title);

        var files = this.files;
        if (files.length > 0) {
            $("#trmsg").remove();
            $(files).each(function(index, item) {
                console.log(index, item);
                var filesize = 0;
                if ((item.size / 1024 / 1024 / 1024) >= 1) {
                    filesize = (item.size / 1024 / 1024 / 1024).toFixed(2) + "GB"; // b=>kb=>mb=>gb
                } else if ((item.size / 1024 / 1024 / 1024) < 1 && (item.size / 1024 / 1024) >= 1) {
                    filesize = (item.size / 1024 / 1024).toFixed(2) + "MB";
                } else if ((item.size / 1024 / 1024) < 1 && (item.size / 1024) >= 1) {
                    filesize = (item.size / 1024).toFixed(2) + "KB";
                } else {
                    filesize = item.size + "B";
                }

                var htmlstr = '<tr><td>文件名：' + item.name + '</td></tr>';
                htmlstr += '<tr><td><progress value="0" max="100" class="domprogress"></progress><span class="dompercent"> 0/' + filesize + '</span></td></tr>';
                htmlstr += '<tr><td><span class="domtime">总共耗时：0.000 秒。</span></td></tr>';
                htmlstr += '<tr><td>md5：<span class="md5"></span></td></tr>';
                htmlstr += '<tr><td class="filestatus"><span class="domstatus">排队中...</span></td></tr>';
                htmlstr += '';

                $("#filelist").append(htmlstr);

            });
        }

    });

    $("#btnupload").click(function() {
        var files = $("#fileselect")[0].files;
        $(files).each(function(index, item) {
            yyupload(files[index], $("span.domstatus").eq(index), $("span.dompercent").eq(index), $(".domprogress").eq(index), $("span.domtime").eq(index));
        });
    });

    //文件上传
    function yyupload(file, dommsg, dompercentmb, domprogress, domtime, fn) {

        var startTime = new Date();
        //获取文件的md5字符串，用于标识文件的唯一性。
        calculate(file);

        //获取文件的加密字符串
        function calculate(file) {
            var fileReader = new FileReader();
            var chunkSize = 1024 * 1024 * 2; //每次读取2MB

            var chunksCount = Math.ceil(file.size / chunkSize); //反回大于参数x的最小整数 8=》8  8.4=》9  8.5=》9 -8.5=》-8
            var currentChunk = 0; //当前块的索引
            var spark = new SparkMD5();
            fileReader.onload = function(e) {
                console.log((currentChunk + 1) + "/" + chunksCount);
                dommsg.text("正在检查文件: " + (currentChunk + 1) + "/" + chunksCount);
                spark.appendBinary(e.target.result); // 添加二进制字符串
                currentChunk++;
                if (currentChunk < chunksCount) {
                    loadNext();
                } else {
                    var md5value = spark.end();
                    $("span.md5").text(md5value);
                    console.log("文件加密结束，密钥为：" + md5value);
                    checkfile(md5value, file); //检查服务器是否存在该文件，存在就从断点继续上传
                }
            };

            function loadNext() {
                var start = currentChunk * chunkSize; //计算读取开始位置
                var end = start + chunkSize >= file.size ? file.size : start + chunkSize; //计算读取结束位置
                fileReader.readAsArrayBuffer(file.slice(start, end)); //读取为二进制字符串
            };

            loadNext();
        }

        //var temp = function () { };

        //if (!FileReader.prototype.readAsBinaryString) {

        //    FileReader.prototype.readAsBinaryString = function(fileData) {

        //        var binary = "";

        //        var pt = this;

        //        var reader = new FileReader();

        //        reader.onload = function(e) {

        //            var bytes = new Uint8Array(reader.result);

        //            var length = bytes.byteLength;

        //            for (var i = 0; i < length; i++) {

        //                binary += String.fromCharCode(bytes[i]);

        //            }

        //            //pt.result  - readonly so assign binary

        //            pt.content = binary;

        //            $(pt).trigger('onload');

        //        };
        //        reader.readAsArrayBuffer(fileData);

        //    };
        //}

        var repeatcount = 0;

        //检查文件是否已经存在
        function checkfile(md5value, file) {

            //console.log(md5value);

            var fd = new FormData();
            fd.append('rquesttype', "chekcfile");
            fd.append('filename', file.name);
            fd.append('md5value', md5value);
            var xhr = new XMLHttpRequest();
            xhr.open('post', 'FileUpoload.ashx', true);
            xhr.onreadystatechange = function(res) {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var jsonobj = JSON.parse(xhr.responseText); //可以将json字符串转换成json对象  //JSON.stringify(jsonobj); //可以将json对象转换成json对符串
                    console.log("继续上传的位置:" + jsonobj.startindex);
                    switch (jsonobj.flag) {
                    case "0":
                        doUpload(md5value, file, 0);
                        break;
                    case "1":
                        doUpload(md5value, file, parseInt(jsonobj.startindex));
                        break;
                    case "2":
                        secondUpload(file);
                        break;
                    }
                    repeatcount = 0;
                } else if (xhr.status == 500) {
                    setTimeout(function() {
                        if (repeatcount < 3) {
                            checkfile(md5value, file);
                        }
                        repeatcount++;
                    }, 3000);
                }
            };
            //开始发送
            xhr.send(fd);
        }

        //实现秒传功能
        function secondUpload(file) {
            var timerange = (new Date().getTime() - startTime.getTime()) / 1000;
            domtime.text("耗时" + timerange + "秒");
            //显示结果进度
            var percent = 100;
            dommsg.text(percent.toFixed(2) + "%");
            domprogress.val(percent);
            var total = file.size;
            if (total > 1024 * 1024 * 1024) {
                dompercentmb.text((total / 1024 / 1024 / 1024).toFixed(2) + "GB/" + (total / 1024 / 1024 / 1024).toFixed(2) + "GB");
            } else if (total > 1024 * 1024) {
                dompercentmb.text((total / 1024 / 1024).toFixed(2) + "MB/" + (total / 1024 / 1024).toFixed(2) + "MB");
            } else if (total > 1024 && total < 1024 * 1024) {
                dompercentmb.text((total / 1024).toFixed(2) + "KB/" + (total / 1024).toFixed(2) + "KB");
            } else {
                dompercentmb.text((total).toFixed(2) + "B/" + (total).toFixed(2) + "B");
            }
        }


        //上传文件
        function doUpload(md5value, file, startindex) {
            var reader = new FileReader(); //新建一个读文件的对象
            var step = 1024 * 200; //每次读取文件大小  200KB
            var cuLoaded = startindex; //当前已经读取总数
            var total = file.size; //文件的总大小
            //读取一段成功
            reader.onload = function(e) {
                //处理读取的结果
                var result = reader.result; //本次读取的数据
                var loaded = e.loaded; //本次读取的数据长度
                uploadFile(result, cuLoaded, function() { //将分段数据上传到服务器
                    cuLoaded += loaded; //如果没有读完，继续
                    var timerange = (new Date().getTime() - startTime.getTime()) / 1000;
                    if (total > 1024 * 1024 * 1024) {
                        dompercentmb.text((cuLoaded / 1024 / 1024 / 1024).toFixed(2) + "GB/" + (total / 1024 / 1024 / 1024).toFixed(2) + "GB");
                    } else if (total > 1024 * 1024) {
                        dompercentmb.text((cuLoaded / 1024 / 1024).toFixed(2) + "MB/" + (total / 1024 / 1024).toFixed(2) + "MB");
                    } else if (total > 1024 && total < 1024 * 1024) {
                        dompercentmb.text((cuLoaded / 1024).toFixed(2) + "KB/" + (total / 1024).toFixed(2) + "KB");
                    } else {
                        dompercentmb.text((cuLoaded).toFixed(2) + "B/" + (total).toFixed(2) + "B");
                    }

                    domtime.text("耗时" + timerange.toFixed(2) + "秒");
                    if (cuLoaded < total) {
                        readBlob(cuLoaded);
                    } else {
                        console.log('总共用时：' + timerange.toFixed(2));
                        cuLoaded = total;
                        sendfinish(); //告知服务器上传完毕  
                        domtime.text("上传完成，总共耗时" + timerange.toFixed(2) + "秒。");
                    }
                    //显示结果进度
                    var percent = (cuLoaded / total) * 100;
                    dommsg.text(percent.toFixed(2) + "%");
                    domprogress.val(percent);
                });
            };

            var k = 0;

            function sendfinish() {
                var fd = new FormData();
                fd.append('rquesttype', "finishupload");
                fd.append('filename', file.name);
                fd.append('md5value', md5value);
                fd.append('totalsize', file.size);
                var xhr = new XMLHttpRequest();
                xhr.open('post', 'FileUpoload.ashx', true);
                xhr.onreadystatechange = function() {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        if (fn) {
                            fn(); //如果上传成功,继续上传下一个文件
                        }
                        k = 0;
                    } else if (xhr.status == 500) {
                        setTimeout(function() {
                            if (k < 3) {
                                sendfinish();
                                //上传完毕的前端处理
                            }
                            k++;
                        }, 3000);
                    }
                };
                //开始发送
                xhr.send(fd);
            }

            var m = 0;

            //关键代码上传到服务器
            function uploadFile(result, startIndex, onSuccess) {
                var blob = new Blob([result]);
                //提交到服务器
                var fd = new FormData();
                fd.append('file', blob);
                fd.append('rquesttype', "uploadblob");
                fd.append('filename', file.name);
                fd.append('md5value', md5value);
                fd.append('loaded', startIndex);
                var xhr = new XMLHttpRequest();
                xhr.open('post', 'FileUpoload.ashx', true);
                xhr.onreadystatechange = function() {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        m = 0;
                        if (onSuccess)
                            onSuccess();
                    } else if (xhr.status == 500) {
                        setTimeout(function() {
                            if (m < 3) {
                                containue();
                                m++;
                            }
                        }, 1000);
                    }
                };
                //开始发送
                xhr.send(fd);
            }

            //指定开始位置，分块读取文件
            function readBlob(start) {
                //指定开始位置和结束位置读取文件
                var blob = file.slice(start, start + step); //读取开始位置和结束位置的文件
                reader.readAsArrayBuffer(blob); //读取切割好的文件块
            }

            //继续
            function containue() {
                readBlob(cuLoaded);
            }

            readBlob(cuLoaded);
        }
    }

</script>
</body>
</html>