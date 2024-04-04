<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoHeight.aspx.cs" Inherits="WebApplicationTest.AutoHeight" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Autosize</title>
    <%--<script src="Scripts/MicrosoftAjax.js"></script>--%>
    <%--<script src="Scripts/autosize.js"></script>--%>

    <script src="Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="Scripts/Hsp.js"></script>

    <style>
        textarea {
            padding: 10px;
            vertical-align: top;
            width: 600px;
        }
        /*
        textarea:focus {
            outline-style: solid;
            outline-width: 2px;
        }*/
    </style>

    <script type="text/javascript">

        var Reserved_keyword = "[ADD],[EXCEPT],[PERCENTALL],[EXEC],[PLANALTER],[EXECUTE],[PRECISIONAND],[EXISTS],[PRIMARYANY],[EXIT],[PRINTAS],[FETCH],[PROCASC],[FILE],[PROCEDUREAUTHORIZATION],[FILLFACTOR],[PUBLICBACKUP],[FOR],[RAISERRORBEGIN],[FOREIGN],[READBETWEEN],[FREETEXT],[READTEXTBREAK],[FREETEXTTABLE],[RECONFIGUREBROWSE],[FROM],[REFERENCESBULK],[FULL],[REPLICATIONBY],[FUNCTION],[RESTORECASCADE],[GOTO],[RESTRICTCASE],[GRANT],[RETURNCHECK],[GROUP],[REVOKECHECKPOINT],[HAVING],[RIGHTCLOSE],[HOLDLOCK],[ROLLBACKCLUSTERED],[IDENTITY],[ROWCOUNTCOALESCE],[IDENTITY_INSERT],[ROWGUIDCOLCOLLATE],[IDENTITYCOL],[RULECOLUMN],[IF],[SAVECOMMIT],[IN],[SCHEMACOMPUTE],[INDEX],[SELECTCONSTRAINT],[INNER],[SESSION_USERCONTAINS],[INSERT],[SETCONTAINSTABLE],[INTERSECT],[SETUSERCONTINUE],[INTO],[SHUTDOWNCONVERT],[IS],[SOMECREATE],[JOIN],[STATISTICSCROSS],[KEY],[SYSTEM_USERCURRENT],[KILL],[TABLECURRENT_DATE],[LEFT],[TEXTSIZECURRENT_TIME],[LIKE],[THENCURRENT_TIMESTAMP],[LINENO],[TOCURRENT_USER],[LOAD],[TOPCURSOR],[NATIONAL],[TRANDATABASE],[NOCHECK],[TRANSACTIONDBCC],[NONCLUSTERED],[TRIGGERDEALLOCATE],[NOT],[TRUNCATEDECLARE],[NULL],[TSEQUALDEFAULT],[NULLIF],[UNIONDELETE],[OF],[UNIQUEDENY],[OFF],[UPDATEDESC],[OFFSETS],[UPDATETEXTDISK],[ON],[USEDISTINCT],[OPEN],[USERDISTRIBUTED],[OPENDATASOURCE],[VALUESDOUBLE],[OPENQUERY],[VARYINGDROP],[OPENROWSET],[VIEWDUMMY],[OPENXML],[WAITFORDUMP],[OPTION],[WHENELSE],[OR],[WHEREEND],[ORDER],[WHILEERRLVL],[OUTER],[WITHESCAPE],[OVER],[WRITETEXT]";

        var GetLocalTime = function (s) {
            //s = s.replace("/Date(", "").replace(")/", "");

            s = s.substr(6, 13);
            return new Date(parseInt(s) * 1000).toLocaleString().replace(/年|月/g, "-").replace(/日/g, "");
        };

        $(function () {
            var n = .3 - .2;
            //alert(n);

            //alert(window.location.pathname);

            //alert(window.location.href + " * " + 
            //    window.location.host + " * " + 
            //    window.location.hostname + " * " + 
            //    window.location.pathname + " * " + 
            //    window.location.search + " * " + 
            //    window.location.href + " * " + 
            //    window.location.href + " * " + 
            //    window.location.href + " * " + "");

            //var add_time = "/Date(1446704778000)/";

            //var dateStr = eval(add_time.replace(/\/Date\((\d+)\)\//gi, "new Date($1)")).format('yyyy-M-d h:m');

            //alert(dateStr);


            //var sql = "UserId IS NOT NULL";
            //var idx = sql.toLocaleLowerCase().indexOf("is");
            //var field = sql.substring(0, idx);
            //alert(field);

            //alert(CheckFloat("432"));

            var value = "over".toUpperCase();
            value = "[" + value + "]";

            //alert((new RegExp("\\.(" + Reserved_keyword + ")$", "i")).test(value));
            var last = new Date("2018-9-10".replace(/-/g, "/"));
            var now = new Date();

            var w = now.getDay();


            //alert("A=" + w + ",B=" +getWeekDate());
            //alert( Math.floor(YearDiff(last, now) * 5) );






        });

        /**
         *获取当前星期几
         *
        */
        function getWeekDate() {
            var now = new Date();
            var day = now.getDay();
            var weeks = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
            var week = weeks[day];
            return week;
        }
        

        function timeFn(d1) {//di作为一个变量传进来
            //如果时间格式是正确的，那下面这一步转化时间格式就可以不用了
            var dateBegin = new Date(d1.replace(/-/g, "/"));//将-转化为/，使用new Date
            var dateEnd = new Date();//获取当前时间
            var dateDiff = dateEnd.getTime() - dateBegin.getTime();//时间差的毫秒数
            var dayDiff = Math.floor(dateDiff / (24 * 3600 * 1000));//计算出相差天数
            var leave1 = dateDiff % (24 * 3600 * 1000)    //计算天数后剩余的毫秒数
            var hours = Math.floor(leave1 / (3600 * 1000))//计算出小时数
            //计算相差分钟数
            var leave2 = leave1 % (3600 * 1000)    //计算小时数后剩余的毫秒数
            var minutes = Math.floor(leave2 / (60 * 1000))//计算相差分钟数
            //计算相差秒数
            var leave3 = leave2 % (60 * 1000)      //计算分钟数后剩余的毫秒数
            var seconds = Math.round(leave3 / 1000)
            console.log(" 相差 " + dayDiff + "天 " + hours + "小时 " + minutes + " 分钟" + seconds + " 秒")
            console.log(dateDiff + "时间差的毫秒数", dayDiff + "计算出相差天数", leave1 + "计算天数后剩余的毫秒数"
                , hours + "计算出小时数", minutes + "计算相差分钟数", seconds + "计算相差秒数");
        }

        function YearDiff(d1, d2) {
            var dateDiff = d2.getTime() - d1.getTime();//时间差的毫秒数
            var diff = dateDiff / (24 * 3600 * 1000 * 365);//计算出相差天数

            return diff;
        }


        var CheckNumber = function (obj) {
            return /^\[-.0-9]+$/.test(obj);
        };

        var CheckFloat = function (v) {
            if (v == "") return true;
            var re = /^[\-\+]?([0-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)(\.\d+)?$/;
            return re.test(v);
        };

        //$.ajax({
        //    url:'www.seogjgsdggd.com/test.php',
        //    type:'POST',
        //    data:'age=20',
        //    error:function(xhr,status,statusText) {
        //        alert(xhr.status);
        //    }
        //});



        // 对Date的扩展，将 Date 转化为指定格式的String 
        // 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
        // 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
        // 例子： 
        // (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
        // (new Date()).Format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18 
        Date.prototype.format = function (fmt) {
            var o = {
                "M+": this.getMonth() + 1, //月份 
                "d+": this.getDate(), //日 
                "h+": this.getHours(), //小时 
                "m+": this.getMinutes(), //分 
                "s+": this.getSeconds(), //秒 
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
                "S": this.getMilliseconds() //毫秒 
            };
            fmt = fmt || "yyyy-MM-dd";
            if (/(y+)/.test(fmt))
                fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }

        function TextAutoSize(id) {
            var obj = document.getElementById(id);
            obj.style.position = "relative";
            obj.style.height = obj.scrollHeight + 0 + 'px';
        }


        (function () {
            //autosize(document.querySelectorAll('textarea'));

            //debugger;

            //var d = /Date(1354116249000)/;

            //var dd = new Date(d).toLocaleString();

            //var ddd = dd;

            //alert(GetLocalTime(1354116249000));

            TestRequest();

        })();


        function TestRequest() {

            var fd = new FormData();
            fd.append('rquesttype', "chekcfile");
            fd.append('filename', "NAMEFFFF");
            fd.append('md5value', "DJFKJDSKFJDSKJF");
            console.log(fd);

            //fd = { id: "jdskfjldskjflkds j", row: Math.random() }

            // https://blog.csdn.net/qq_41802303/article/details/80066160
            // 如今主流浏览器都开始支持一个叫做FormData的对象，有了这个FormData，我们就可以轻松地使用Ajax方式进行文件上传了

            $.ajax({
                url: '/Handler/RequestHandler.ashx?rnd=' + Math.random(),
                type: 'POST',
                data: fd,
                // 告诉jQuery不要去处理发送的数据，用于对data参数进行序列化处理 这里必须false
                processData: false,//重要
                // 告诉jQuery不要去设置Content-Type请求头
                contentType: false,//重要，必须
                success: function (rst) {

                    if (rst) $("body span:last-child").html(rst);

                    //if (rst.IsSuccess) {
                    //$('.Detail-heading').html(rst.Data.ArticleDesc);
                    //$('.detail-article').html(rst.Data.Contents);

                    //$('#CreateUserName').html(rst.Data.CreateUserName);
                    //$('#CreateDate').html(rst.Data.PublishDate);

                    //// 图片
                    //imageList(rst.Data.TypeId, rst.Data.ID, rst.Data.VirtualDirectory);

                    //// 附件
                    //attachmentList(rst.Data.TypeId, rst.Data.ID, rst.Data.VirtualDirectory);

                    //// 文章阅读记录
                    //articleReading(rst.Data.BizType, rst.Data.ID);
                    //} else {
                    //    $.messager.alert({ title: "操作提示", msg: rst.Message, showType: "error" });
                    //}
                }
                , complete: function (xhr, errorText, errorType) {

                    //debugger;

                    var p = "";
                    if (window.console) console.log(xhr);
                    alert("请求完成后");
                }
                , error: function (xhr, errorText, errorType) {
                    alert("请求错误后");
                    if (window.console) console.log(xhr);
                }
                , beforSend: function () {
                    alert("请求之前");
                }
            });

        };

    </script>

    <script type="text/javascript">
        //function addTen(num) {
        //    num += 10;
        //    return num;
        //}

        //var count = 20;
        //var result = addTen(count);
        ////alert(count);    //20
        ////alert(result);   //10

        //function buildUrl() {
        //    var qs = "?debug=true";
        //    var url;
        //    with (location) {
        //        url = href + qs;
        //    }

        //    return url;
        //}

        //var result = buildUrl();
        ////alert(result);


        //var numbers = [1, 2, 3, 4, 5, 4, 3, 2, 1];

        //var everyResult = numbers.every(function (item, index, array) {
        //    return (item > 2);
        //});

        ////alert(everyResult);       //false

        //var someResult = numbers.some(function (item, index, array) {
        //    return (item > 2);
        //});

        //alert(someResult);       //true

        //var text = "cat, bat, sat, fat";
        //var pattern = /.at/;

        //var matches = text.match(pattern);
        //alert(matches.index);        //0
        //alert(matches[0]);           //"cat"
        //alert(pattern.lastIndex);    //0

        //var pos = text.search(/at/);
        //alert(pos);   //1

        //var result = text.replace("at", "ond");
        //alert(result);    //"cond, bat, sat, fat"

        //result = text.replace(/at/g, "ond");
        //alert(result);    //"cond, bond, sond, fond"

        //result = text.replace(/(.at)/g, "word ($1)");
        //alert(result);    //word (cat), word (bat), word (sat), word (fat)

        //function htmlEscape(text) {
        //    return text.replace(/[<>"&]/g, function (match, pos, originalText) {
        //        switch (match) {
        //            case "<":
        //                return "&lt;";
        //            case ">":
        //                return "&gt;";
        //            case "&":
        //                return "&amp;";
        //            case "\"":
        //                return '&quot;';
        //        }
        //        return "&lt;";
        //    });
        //}

        //alert(htmlEscape("<p class=\"greeting\">Hello world!</p>")); //&lt;p class=&quot;greeting&quot;&gt;Hello world!&lt;/p&gt;

        //var colorText = "red,blue,green,yellow";
        //var colors1 = colorText.split(",");      //["red", "blue", "green", "yellow"]
        //var colors2 = colorText.split(",", 2);   //["red", "blue"]
        //var colors3 = colorText.split(/[^\,]+/); //["", ",", ",", ",", ""]

    </script>
</head>
<body onload="TextAutoSize('TextArea1');">
    <form id="form1" runat="server">
        <div>
            <textarea id="TextArea1" cols="60" rows="12" onkeypress="TextAutoSize('TextArea1');">            
            
  长期以来国际空间站不断尝试在低重力环境下培育地球植物，为今后的长途星际旅行做准备。
近日，宇航员Peggy Whitson首获了自己的新成果——中国小白菜。
据介绍，这是国际空间站第五批收割的太空蔬菜，也是第一次成功种植中国小白菜。研究人员经过一系列严格标准评估多种多叶蔬菜，最终选择中国小白菜，评估标准包括：蔬菜生长状况，以及营养价值等。
对于此次首获的小白菜，其中一部分将作为食物被宇航员们享用，剩下的则会被送往肯尼迪太空中心进行研究。          
  长期以来国际空间站不断尝试在低重力环境下培育地球植物，为今后的长途星际旅行做准备。
近日，宇航员Peggy Whitson首获了自己的新成果——中国小白菜。
据介绍，这是国际空间站第五批收割的太空蔬菜，也是第一次成功种植中国小白菜。研究人员经过一系列严格标准评估多种多叶蔬菜，最终选择中国小白菜，评估标准包括：蔬菜生长状况，以及营养价值等。
对于此次首获的小白菜，其中一部分将作为食物被宇航员们享用，剩下的则会被送往肯尼迪太空中心进行研究。        
        
        </textarea>

            <h3>max-height 300px</h3>
            <textarea id="TextArea2" style="max-height: 300px; position: absolute;">The coconut palm (also, cocoanut), Cocos nucifera, is a member of the family Arecaceae (palm family). It is the only accepted species in the genus Cocos.[2] The term coconut can refer to the entire coconut palm, the seed, or the fruit, which, botanically, is a drupe, not a nut. The spelling cocoanut is an archaic form of the word.[3] The term is derived from 16th-century Portuguese and Spanish coco, meaning "head" or "skull",[4] from the three small holes on the coconut shell that resemble human facial features.</textarea>

            <script type="text/javascript">
                TextAutoSize('TextArea2');
            </script>

            <h3>no max-height</h3>
            <textarea id="TextArea3" style="position: relative;">The coconut palm (also, cocoanut), Cocos nucifera, is a member of the family Arecaceae (palm family). It is the only accepted species in the genus Cocos.[2] The term coconut can refer to the entire coconut palm, the seed, or the fruit, which, botanically, is a drupe, not a nut. The spelling cocoanut is an archaic form of the word.[3] The term is derived from 16th-century Portuguese and Spanish coco, meaning "head" or "skull",[4] from the three small holes on the coconut shell that resemble human facial features.</textarea>

            <script type="text/javascript">
                TextAutoSize('TextArea3');
            </script>
        </div>

        <div>/</div>
        <label id="lblRst"></label>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>

    <script type="text/javascript">

        //autosize(document.querySelectorAll('textarea'));
        //debugger;

        //var ta = document.getElementsByTagName("textarea");
        //var res = '';

        ////onpropertychange = "this.style.height=this.scrollHeight + 'px'"
        ////oninput = "this.style.height=this.scrollHeight + 'px'"

        //for (var i = 0; i < ta.length; i++) {
        //    if (ta[i].type == 'textarea') {

        //        //debugger;

        //        var obj = ta[i];
        //        var h = ta[i].scrollHeight;

        //        var str = ta[i].value;
        //        var htm = ta[i].innerHTML;

        //        var v = obj.childNodes[0].data;
        //        //alert(h + " * " + str + " * " + htm + " * " + v);

        //        if (str.length > 0) {

        //            obj.style.position = "relative";
        //            ta[i].style.height = ta[i].scrollHeight + 5 + 'px';
        //        }

        //        //if(str != '') 
        //        //{ res +=str+'/'; 
        //        //} 
        //    }
        //}

    </script>

    <style type="text/css">
        .login {
            float: left;
            width: 32px;
            height: 32px;
            /*margin-top: 22px;*/
            border: 0px solid red;
            /*background: url(/Images/login.png) no-repeat;*/
            background-image: url(/Images/login.png);
            background-repeat: no-repeat;
            -ms-background-size: 100% 100%;
            -moz-background-size: 100% 100%;
            background-size: 100% 100%;
        }
    </style>
    <img src="Images/login.png" />
    <div class="login"></div>
    <input type="checkbox" value="1" id="Forced-Login" />
    <label for="Forced-Login">强行登录</label>
    <span style="color: red; font-size: 24px; font-weight: bold;">强行登录</span>

</body>

</html>
