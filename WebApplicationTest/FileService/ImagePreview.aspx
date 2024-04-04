<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImagePreview.aspx.cs" Inherits="WebApplicationTest.FileService.ImagePreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>图片预览效果</title>

<style type="text/css">
    .photo-mask {
        -moz-opacity: 0.8;
        background: rgba(0, 0, 0, 0.8);
        bottom: 0;
        display: none;
        filter: alpha(opacity=20);
        left: 0;
        opacity: 0.8;
        position: fixed;
        right: 0;
        top: 0;
        z-index: 10;
    }

    .photo-panel {
        bottom: 0;
        clear: both;
        display: none;
        left: 0;
        position: absolute;
        right: 0;
        top: 0;
        z-index: 10;
    }

    .photo-panel .photo-div,
    .photo-panel .photo-bar { width: 100%; }

    .photo-panel .photo-div {
        height: 560px;
        margin: auto;
        position: relative;
        width: 960px;
        z-index: 11;
    }

    .photo-panel .photo-close {
        background: url(images/close.png);
        height: 56px;
        margin-left: 664px;
        position: absolute;
        width: 56px;
    }

    .photo-panel .photo-close:hover {
        background: url(images/close_ch.png);
        height: 56px;
        margin-left: 664px;
        position: absolute;
        width: 56px;
    }

    .photo-panel .photo-bar-tip {
        height: 44px;
        margin-top: -64px;
        padding: 10px;
        position: absolute;
        width: 700px;
    }

    .photo-panel .photo-bar-tip:hover {
        -moz-opacity: 0.8;
        background: #000;
        color: #fff;
        filter: alpha(opacity=20);
        height: 44px;
        margin-top: -64px;
        opacity: 0.8;
        padding: 10px;
        position: absolute;
        width: 700px;
    }

    .photo-panel .photo-img {
        background: #fff;
        float: left;
        height: 560px;
        width: 720px;
    }

    .photo-panel .photo-view-w {
        display: table-cell;
        height: 560px;
        text-align: center;
        vertical-align: middle;
        width: 720px;
    }

    .photo-panel .photo-view-h {
        height: 560px;
        text-align: center;
        vertical-align: middle;
        width: 720px;
    }

    .photo-panel .photo-view-w img {
        -moz-animation: swing 1s .2s ease both;
        -moz-box-shadow: 5px 5px 5px #a6a6a6;
        -webkit-animation: swing 1s .2s ease both;
        /* 老的 Firefox */
        box-shadow: 5px 5px 5px #a6a6a6;
        height: auto;
        margin: 10px;
        max-height: 540px;
        max-width: 700px;
        text-align: center;
        vertical-align: middle;
    }

    .photo-panel .photo-view-h img {
        -moz-animation: swing 1s .2s ease both;
        -moz-box-shadow: 5px 5px 5px #a6a6a6;
        -webkit-animation: swing 1s .2s ease both;
        /* 老的 Firefox */
        box-shadow: 5px 5px 5px #a6a6a6;
        height: 540px;
        margin: 10px;
        max-width: 700px;
    }

    @-webkit-keyframes swing {
        20%,
        40%,
        60%,
        80%,
        100% { -webkit-transform-origin: top center }

        20% { -webkit-transform: rotate(15deg) }

        40% { -webkit-transform: rotate(-10deg) }

        60% { -webkit-transform: rotate(5deg) }

        80% { -webkit-transform: rotate(-5deg) }

        100% { -webkit-transform: rotate(0deg) }
    }

    @-moz-keyframes swing {
        20%,
        40%,
        60%,
        80%,
        100% { -moz-transform-origin: top center }

        20% { -moz-transform: rotate(15deg) }

        40% { -moz-transform: rotate(-10deg) }

        60% { -moz-transform: rotate(5deg) }

        80% { -moz-transform: rotate(-5deg) }

        100% { -moz-transform: rotate(0deg) }
    }

    .photo-panel .photo-left,
    .photo-panel .photo-right {
        float: left;
        margin-top: 220px;
        width: 120px;
    }

    .photo-panel .arrow-prv {
        background: url(images/l.png);
        height: 120px;
        width: 120px;
    }

    .photo-panel .arrow-prv:hover {
        background: url(images/l_ch.png);
        cursor: pointer;
        height: 120px;
        width: 120px;
    }

    .photo-panel .arrow-next {
        background: url(images/r.png);
        height: 120px;
        width: 120px;
    }

    .photo-panel .arrow-next:hover {
        background: url(images/r_ch.png);
        cursor: pointer;
        height: 120px;
        width: 120px;
    }

    .demo {
        margin: 10px auto;
        width: 800px;
    }

    .demo li {
        float: left;
        height: 200px;
        overflow: hidden;
        width: 200px;
    }

    .demo li img {
        height: auto;
        width: auto;
    }
</style>

</head>
<body>
<div class="demo">
    <ul>
        <li>
            <img src="images/69.jpg">
        </li>
        <li>
            <img src="images/74.jpg">
        </li>
        <li>
            <img src="images/14H58PICWJE.jpg">
        </li>
        <li>
            <img src="images/58.jpg">
        </li>
        <li>
            <img src="images/8.jpg">
        </li>
        <li>
            <img src="images/13611682080.jpg">
        </li>
    </ul>
</div>
<div class="photo-mask"></div>
<div class="photo-panel">
    <div class="photo-div">
        <div class="photo-left">
            <div class="arrow-prv"></div>
        </div>
        <div class="photo-img">
            <div class="photo-bar">
                <div class="photo-close"></div>
            </div>
            <!--div class="photo-view-w">
            <img src="http://image.tianjimedia.com/uploadImages/2013/127/5WW8V9Y75V00_1000x500.jpg"/>
            </div-->
            <div class="photo-view-h">
                <img src="http://b.zol-img.com.cn/sjbizhi/images/9/800x1280/1471524533521.jpg"/>
            </div>
            <!--div class="photo-bar">
            <div class="photo-bar-tip">
                待开发功能
            </div>
            </div-->
        </div>
        <div class="photo-right">
            <div class="arrow-next"></div>
        </div>
    </div>
</div>


<script src="../Scripts/jquery/jquery-1.12.4.min.js"></script>

<script type="text/javascript">
    var img_index = 0;
    var img_src = "";

    $(function() {
        //计算居中位置
        var mg_top = ((parseInt($(window).height()) - parseInt($(".photo-div").height())) / 2);

        $(".photo-div").css({
            "margin-top": "" + mg_top + "px"
        });
        //关闭
        $(".photo-close").click(function() {
            $(".photo-mask").hide();
            $(".photo-panel").hide();
        });
        //下一张
        $(".photo-panel .photo-div .arrow-next").click(function() {
            img_index++;
            if (img_index >= $(".demo li img").length) {
                img_index = 0;
            }
            img_src = $(".demo li img").eq(img_index).attr("src");
            photoView($(".demo li img"));
        });
        //上一张
        $(".photo-panel .photo-div .arrow-prv").click(function() {
            img_index--;
            if (img_index < 0) {
                img_index = $(".demo li img").length - 1;
            }
            img_src = $(".demo li img").eq(img_index).attr("src");
            photoView($(".demo li img"));
        });
        //如何调用？
        $(".demo li img").click(function() {
            $(".photo-mask").show();
            $(".photo-panel").show();
            img_src = $(this).attr("src");
            img_index = $(this).index();
            photoView($(this));
        });

    });

    //自适应预览
    function photoView(obj) {
        if ($(obj).width() >= $(obj).height()) {
            $(".photo-panel .photo-div .photo-img .photo-view-h").attr("class", "photo-view-w");
            $(".photo-panel .photo-div .photo-img .photo-view-w img").attr("src", img_src);
        } else {
            $(".photo-panel .photo-div .photo-img .photo-view-w").attr("class", "photo-view-h");
            $(".photo-panel .photo-div .photo-img .photo-view-h img").attr("src", img_src);
        }
        //此处写调试日志
        console.log(img_index);
    }
</script>


<div style="font: normal 14px/24px 'MicroSoft YaHei'; margin: 50px 0; text-align: center;">
    <br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>
    <p>适用浏览器：360、FireFox、Chrome、Safari、Opera、傲游、搜狗、世界之窗. 不支持IE8及以下浏览器。</p>
</div>


</body>
</html>