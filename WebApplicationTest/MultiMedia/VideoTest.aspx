<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoTest.aspx.cs" Inherits="WebApplicationTest.MultiMedia.VideoTest" %>

<!DOCTYPE html>

<html lang="zh-ch">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title>视频测试</title>

<link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
<link href="/Scripts/themes/color.css" rel="stylesheet"/>
<link href="/Scripts/themes/icon.css" rel="stylesheet"/>
<link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
<link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>

<script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>
<script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>

<!--[if lt IE 9]>
    <script src="/Scripts/html5shiv.min.js"></script>
    <script src="/Scripts/respond.min.js"></script>
    <script src="/Scripts/html5media.min.js"></script>
<![endif]-->

<script src="/Scripts/jquery.easyui.min.js"></script>
<script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
<script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
<script src="/Scripts/Hsp.js"></script>

<%--<script src="/Scripts/Hsp.Common.js"></script>--%>

<%--当前，video 元素支持三种视频格式：    
Ogg = 带有 Theora 视频编码和 Vorbis 音频编码的 Ogg 文件
MPEG4 = 带有 H.264 视频编码和 AAC 音频编码的 MPEG 4 文件
WebM = 带有 VP8 视频编码和 Vorbis 音频编码的 WebM 文件  --%>  

<style type="text/css">
    .post-category:before {
      content: "";
      position: absolute;
      top: -10px;
      left: 0px;
      border-color: transparent #6f0d0d #6f0d0d transparent;
      border-style: solid;
      border-width: 5px;
      width: 0;
      height: 0;
    }

     .post-category {
         display: inline-block;
         margin-left: -30px;
         padding: 6px 10px 8px;
         padding-left: 50px;
         border-radius: 0 5px 5px 0;
         position: relative;
         box-shadow: 0 1px 5px rgba(0, 0, 0, .3), inset 0 1px 0 rgba(255,255,255,.2), inset 0 -1px 0 rgba(0,0,0,.3);
         background: #9e2812;
         background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzllMjgxMiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2ZjBkMGQiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
         background: -moz-linear-gradient(top,  #9e2812 0%, #6f0d0d 100%);
         background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#9e2812), color-stop(100%,#6f0d0d));
         background: -webkit-linear-gradient(top,  #9e2812 0%,#6f0d0d 100%);
         background: -o-linear-gradient(top,  #9e2812 0%,#6f0d0d 100%);
         background: -ms-linear-gradient(top,  #9e2812 0%,#6f0d0d 100%);
         background: linear-gradient(to bottom,  #9e2812 0%,#6f0d0d 100%);
         filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e2812', endColorstr='#6f0d0d',GradientType=0 );
     }
</style>

<script type="text/javascript">
    var $media = null, $track = null, controlbar = null;

    $(function() {

        $media = $('#media')[0], $track = $("track")[0];

        $('#mp4').unbind('click').bind("click", function() {
            
            $media.src = "火电站发电过程.mp4";
            $media.width = 672;
            $media.height = 378;

            //if (media.paused) {
            //    media.play();
            //} else {
            //    media.pause();
            //}

            var h264Test = $media.canPlayType('video/mp4; codecs="avc1.42E01E, mp4a.40.2"');

            if (h264Test) {
                play("MP4");

                //alert(media.width);
            }
        });

        $('#ogv').unbind('click').bind("click", function() {

            $media.src = "OgvTest.ogv";
            $media.width = 672;
            $media.height = 378;

            //检测是否可以播放ogg格式的视频 
            var oggTest = $media.canPlayType('video/ogg; codecs="theora, vorbis"');

            if (oggTest) {
                play("OGV");
            }
        });

        $('#webm').unbind('click').bind("click", function () {
            $media.pause();
            $media.src = "phfx_4KHD_VP9TestFootage.webm";
            $media.width = 672;
            $media.height = 378;

            //alert(!!media.canPlayType);
            //play("WebM");

            //检测是否可以播放WebM格式的视频 
            var webmTest = $media.canPlayType('video/webm; codecs="vp8.0, vorbis"');

            if (webmTest) {
                play("WebM");
            }
        });

        if (HSP.Browser.IS_IE) {
            //使用事件监听方式捕捉事件
            //media.attachEvent("ontimeupdate", function () {
            //    var timeDisplay = document.getElementById("time");
            //    //用秒数来显示当前播放进度
            //    timeDisplay.innerHTML = Math.floor(media.currentTime) + "／" + Math.floor(media.duration) + "（秒）";
            //}, false);

            $media.attachEvent("onerror", function () {
                var error = $media.error;
                switch (error.code) {
                case 1:
                    alert('取回过程被用户中止。');
                    break;
                case 2:
                    alert('当下载时发生错误。');
                    break;
                case 3:
                    alert('当解码时发生错误。');
                    break;
                case 4:
                    alert('媒体不可用或者不支持音频/视频。');
                    break;
                }
            }, false);

            //media.attachEvent('ended', function () {
            //    //  第一个视频播放结束
            //    //  如果要继续播放第二个视频，改变 <video> 标签的 (src="test2.mp4") 属性或者新建一个 video 标签播放视频都可以
            //}, false);

            //$track.attachEvent('onload', function () {
            //    var c = $media.textTracks[0].cues;
            //    for (var i = 0; i < c.length; i++) {
            //        var s = document.createElement("span");
            //        s.innerHTML = c[i].text;
            //        s.setAttribute('data-start', c[i].startTime);
            //        s.addEventListener("click", seek);
            //        controlbar.appendChild(s);
            //    }
            //});


            //$track.onload = function (e) {
            //    var c = $media.textTracks[0].cues;
            //    for (var i = 0; i < c.length; i++) {
            //        var s = document.createElement("span");
            //        s.innerHTML = c[i].text;
            //        s.setAttribute('data-start', c[i].startTime);
            //        s.addEventListener("click", seek);
            //        controlbar.appendChild(s);
            //    }
            //};

            //controlbar.attachEvent('onmousemove', function(e) {
            //    // first we convert from mouse to time position ..
            //    var p = (e.pageX - controlbar.offsetLeft) * video.duration / 480;

            //    // ..then we find the matching cue..
            //    var c = media.textTracks[0].cues;
            //    for (var i = 0; i < c.length; i++) {
            //        if (c[i].startTime <= p && c[i].endTime > p) {
            //            break;
            //        };
            //    }

            //    // ..next we unravel the JPG url and fragment query..
            //    var url = c[i].text.split('#')[0];
            //    var xywh = c[i].text.substr(c[i].text.indexOf("=") + 1).split(',');

            //    // ..and last we style the thumbnail overlay
            //    thumbnail.style.backgroundImage = 'url(' + c[i].text.split('#')[0] + ')';
            //    thumbnail.style.backgroundPosition = '-' + xywh[0] + 'px -' + xywh[1] + 'px';
            //    thumbnail.style.left = e.pageX - xywh[2] / 2 + 'px';
            //    thumbnail.style.top = controlbar.offsetTop - xywh[3] + 8 + 'px';
            //    thumbnail.style.width = xywh[2] + 'px';
            //    thumbnail.style.height = xywh[3] + 'px';
            //});
        } else {

            //使用事件监听方式捕捉事件
            $media.addEventListener("timeupdate", function () {
                var timeDisplay = document.getElementById("time");
                //用秒数来显示当前播放进度
                timeDisplay.innerHTML = Math.floor($media.currentTime) + "／" + Math.floor($media.duration) + "（秒）";
            }, false);

            $media.addEventListener("error", function () {
                var error = $media.error;
                switch (error.code) {
                case 1:
                    alert('取回过程被用户中止。');
                    break;
                case 2:
                    alert('当下载时发生错误。');
                    break;
                case 3:
                    alert('当解码时发生错误。');
                    break;
                case 4:
                    alert('媒体不可用或者不支持音频/视频。');
                    break;
                }
            }, false);

            $media.addEventListener('ended', function () {

                $("#txtEnded").val("已经结束");

                //  第一个视频播放结束
                //  如果要继续播放第二个视频，改变 <video> 标签的 (src="test2.mp4") 属性或者新建一个 video 标签播放视频都可以
            }, false);

            $track.addEventListener('load', function() {
                var c = $media.textTracks[0].cues;
                for (var i = 0; i < c.length; i++) {
                    var s = document.createElement("span");
                    s.innerHTML = c[i].text;
                    s.setAttribute('data-start', c[i].startTime);
                    s.addEventListener("click", seek);
                    controlbar.appendChild(s);
                }
            });

            //controlbar.addEventListener('mousemove', function(e) {
            //    // first we convert from mouse to time position ..
            //    var p = (e.pageX - controlbar.offsetLeft) * video.duration / 480;

            //    // ..then we find the matching cue..
            //    var c = media.textTracks[0].cues;
            //    for (var i = 0; i < c.length; i++) {
            //        if (c[i].startTime <= p && c[i].endTime > p) {
            //            break;
            //        };
            //    }

            //    // ..next we unravel the JPG url and fragment query..
            //    var url = c[i].text.split('#')[0];
            //    var xywh = c[i].text.substr(c[i].text.indexOf("=") + 1).split(',');

            //    // ..and last we style the thumbnail overlay
            //    thumbnail.style.backgroundImage = 'url(' + c[i].text.split('#')[0] + ')';
            //    thumbnail.style.backgroundPosition = '-' + xywh[0] + 'px -' + xywh[1] + 'px';
            //    thumbnail.style.left = e.pageX - xywh[2] / 2 + 'px';
            //    thumbnail.style.top = controlbar.offsetTop - xywh[3] + 8 + 'px';
            //    thumbnail.style.width = xywh[2] + 'px';
            //    thumbnail.style.height = xywh[3] + 'px';
            //});
        }

    });

    function play(type) {
        if ($media.paused) {
            //if(videoUrl != video.src) 
            //{  
            //    video.src = videoUrl;  
            //    video.load();  
            //} 
            //else 
            //{  
            //    video.play();  
            //}
            $media.play();
            //document.getElementById(type.toLowerCase()).innerHTML = type + "暂停";
            $('#' + type.toLowerCase()).text(type + "暂停");
            $.parser.parse('#' + type.toLowerCase());
            $('#' + type.toLowerCase()).linkbutton({ height: '26px' });
        } else {
            $media.pause();
            //document.getElementById(type.toLowerCase()).innerHTML = type + "播放";
            $('#' + type.toLowerCase()).text(type + "播放");
            $.parser.parse('#' + type.toLowerCase());
            $('#' + type.toLowerCase()).linkbutton({ height: '26px' });
        }
    }

    function seek() {
        $media.currentTime = this.getAttribute('data-start');
        if (media.paused) {
            media.play();
        }
    };


    function checkVideo() {
        if (!!document.createElement('video').canPlayType) {
            var vidTest = document.createElement("video");
            var oggTest = vidTest.canPlayType('video/ogg; codecs="theora, vorbis"');
            if (!oggTest) {
                var h264Test = vidTest.canPlayType('video/mp4; codecs="avc1.42E01E, mp4a.40.2"');
                if (!h264Test) {
                    document.getElementById("checkVideoResult").innerHTML = "Sorry. No video support.";
                } else {
                    if (h264Test == "probably") {
                        document.getElementById("checkVideoResult").innerHTML = "Yes! Full support!";
                    } else {
                        document.getElementById("checkVideoResult").innerHTML = "Well. Some support.";
                    }
                }
            } else {
                if (oggTest == "probably") {
                    document.getElementById("checkVideoResult").innerHTML = "Yes! Full support!";
                } else {
                    document.getElementById("checkVideoResult").innerHTML = "Well. Some support.";
                }
            }
        } else {
            document.getElementById("checkVideoResult").innerHTML = "Sorry. No video support.";
        }
    }
</script>

</head>
<body>
<form id="form1" runat="server">
    <div style="margin: 5px;">

        <h1>HTML 5 video示例</h1>
        <video id="media" controls="controls" preload="auto" width="672" height="378" poster="http://www.w3school.com.cn/i/w3school_logo_black.gif">
            <source src="火电站发电过程.mp4"/>
            <%--<source src="OgvTest.ogv"/>--%>
            <%--<source src="phfx_4KHD_VP9TestFootage.webm"/>--%>
            <track kind="subtitles" label="简体中文" src="subschi.srt" srclang="zh" default>
            <track src="cn_track.vtt" srclang="zh-cn" label="简体中文" kind="caption" default>

            <p>你的浏览器不支持html5的video标签 / This browser does not support HTML5 video</p>
        </video>

        <div style="margin: 20px 5px">
            <a id="mp4" href="#" class="easyui-linkbutton c5" style="width: 150px">MP4视频播放</a>
            <a id="ogv" href="#" class="easyui-linkbutton c6" style="width: 150px">OGV视频播放</a>
            <a id="webm" href="#" class="easyui-linkbutton c7" style="width: 150px">WebM视频播放</a>
            <span id="time"></span> 
        </div>

        <%--<embed src="http://player.youku.com/player.php/sid/XNjgwMDU5MDU2/v.swf" allowFullScreen="true" quality="high" width="480" height="400" align="middle" allowScriptAccess="always" type="application/x-shockwave-flash"></embed>--%>

        <div id="checkVideoResult" style="border: 0; margin: 10px 0 0 0; padding: 0;">
            <button style="font-family: Arial, Helvetica, sans-serif;" onclick="checkVideo()">Check</button>
        </div>

    </div>
    
    <div style="margin: 100px; padding: 0;">
        <div class="post-category" style="height: 36px; width: 300px;">&nbsp;</div>
        <asp:Label ID="VideoInfo" runat="server" Text=""></asp:Label>
    </div>
</form>
</body>
</html>