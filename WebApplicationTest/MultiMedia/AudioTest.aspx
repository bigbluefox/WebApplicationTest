<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioTest.aspx.cs" Inherits="WebApplicationTest.MultiMedia.AudioTest" %>

<!DOCTYPE html>

<html lang="zh-ch">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>音频测试</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/color.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/font-awesome.css" rel="stylesheet"/>
    <link href="css/audio.css" rel="stylesheet" />

    <%--	<link rel="stylesheet" href="css/reset.css" />
	<link rel="stylesheet" type="text/css" href="css/default.css">
	<link rel="stylesheet" href="css/style.css" />
	<link rel="stylesheet" href="css/audioplayer.css" /> --%>

    <script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>

    <!--[if lt IE 9]>
        <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
        <script src="http://api.html5media.info/1.2.2/html5media.min.js"></script>
    <![endif]-->

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <%--<script src="/Scripts/audioplayer.js"></script>--%>

    <script src="/Scripts/Hsp.js"></script>
    <%--<script src="js/audio.js"></script>--%>

    <style type="text/css">
        body { margin: 5px; }

        audio { width: 100%; }

        .play {
            border: 1px solid #00bfff;
            cursor: pointer;
            display: inline-block;
            text-align: center;
            width: 120px;
        }

    </style>

    <script type="text/javascript">
        
        //http://www.w3school.com.cn/tags/html_ref_audio_video_dom.asp

        var $media = null, audioTimer = null, audioUrl = "";

        $(function() {
            $media = $('#media')[0], audioUrl = "moonlight.mp3";

            // 快退，-30S
            $('#btnFastBack').bind('click', function() {
                $media.currentTime = $media.currentTime - 30.0;
                $media.play();
            });

            // 播放 / 暂停
            $('#btnPlay').bind('click', function () {
                if (!$media.currentSrc) $media.src = audioUrl;
                $media.play();

                if ($(this).attr("title") == "暂停") {
                    $(this).html('<i class="glyphicon glyphicon-play"></i>');
                    $(this).attr("title", "播放");
                    $media.pause();
                } else {
                    $(this).html('<i class="glyphicon glyphicon-pause"></i>');
                    $(this).attr("title", "暂停");
                    $media.play();
                }
            });

            // 暂停 / 播放
            $('#btnPause').bind('click', function () {

                //debugger;

                var a = $(this).html();
                var b = this.innerHTML;

                //alert($(this).html());
                if ($(this).attr("title") == "暂停") {
                    $(this).html('<i class="glyphicon glyphicon-play"></i>');
                    $(this).attr("title", "播放");
                    $media.pause();
                } else {
                    $(this).html('<i class="glyphicon glyphicon-pause"></i>');
                    $(this).attr("title", "暂停");
                    $media.play();
                }
                //debugger;

                //var playing = $media.paused();


                //if ($media.paused()) {
                //    $media.play();
                //} else {
                //    $media.pause();
                //}
            });

            // 停止
            $('#btnStop').bind('click', function () {
                $media.pause();
                $media.currentTime = 0;

                var $play = $("#btnPlay");
                if ($play.attr("title") == "暂停") {
                    $play.html('<i class="glyphicon glyphicon-play"></i>');
                    $play.attr("title", "播放");
                }

            });

            // 快进，+30S
            $('#btnFastForward').bind('click', function() {
                $media.currentTime = $media.currentTime + 30.0;
                $media.play();
            });

            //绑定播放暂停控制  
            $('#mp3').bind('click', function() {
                $media.src = audioUrl;
                $media.preload = "metadata";
                playAudio();
                autioTimeSpan();
            });

            $('#ogg').bind('click', function() {
                $media.src = "music.ogg";
                playAudio();
            });

            $('#wav').bind('click', function() {
                $media.src = "music.wav";
                //$media.src = "月满西楼.flac";
                playAudio();
            });

            // 重新播放
            $('#btnReplay').unbind('click').bind("click", function() {
                $media.src = "audio/Scarborough%20Fair.mp3";
                $media.controls = true; // 是否有默认控制条 
                try {
                    $media.currentTime = 0.0;
                } catch (e) {

                }

                $media.play();
                //var h264Test = $media.canPlayType('video/mp4; codecs="avc1.42E01E, mp4a.40.2"');
                //play("MP4");
            });

            //if (HSP.Browser.IS_IE ) {//|| HSP.Browser.IS_IE11
            //    $('#btnResumePlay').attr("disabled", "disabled");
            //} else {}

                // 继续播放
                $('#btnResumePlay').unbind('click').bind("click", function() {

                    //debugger;

                    $media.src = "audio/Scarborough%20Fair.mp3";
                    $media.controls = true; // 是否有默认控制条 
                    //$media.load(); //重新加载src指定的资源 
                    $media.preload = "auto";

                    //$media.play();

                    //var status = $media.paused();


                    ////var readyState = $media.readyState();
                    ////$("#txtMediaMessage").val($("#txtMediaMessage").val() + "状态：" + readyState + "\n");

                    //$media.pause();

                    var value = $("#txtCurrentTime").val();
                    if (value.length == 0) value = "0";
                    value = parseFloat(value);
                    //$media.attr("data-start", value);seek();

                    //$("#txtMediaMessage").val("开始：" + value + "\n"); data-start
                    //try {
                    //    $media.currentTime = parseFloat(value); //当前播放的位置，赋值可改变位置，IE浏览器不支持 
                    //} catch (e) {

                    //}

                    //$media.currentTime = value;

                    //$media.play();

                    //setTimeout("PlayMusic()", 100); //90秒

                });
            
            
            if (HSP.Browser.IS_IE) {
            } else {

                // 当浏览器可在不因缓冲而停顿的情况下进行播放时
                audio.addEventListener("canplaythrough", function () {
                    alert('音频文件已经准备好，随时待命');
                }, false);

                audio.addEventListener("onloadeddata", function () {
                    //alert('音频文件已经准备好，随时待命');



                }, false);

                //当浏览器已加载音频 / 视频的元数据时
                $media.addEventListener("loadedmetadata", function() {
                    var duration = $media.duration; //获取总时长

                    //var duration = $("#txtDuration").val();
                    //if (duration.length == 0) {}
                    //$("#txtMediaMessage").val("时长：" + $media.duration + "\n");

                    $("#txtDuration").val($media.duration);


                    //function getFirstBuffRange() {}
                    var buff = $media.buffered.start(0) + " - " + $media.buffered.end(0);
                    

                    //// 获取已缓冲部分的 TimeRanges 对象
                    //var timeRanges = $media.buffered;
                    //// 获取以缓存的时间
                    //var timeBuffered = timeRages.end(timeRages.length - 1);
                    //// 获取缓存进度，值为0到1
                    //var bufferPercent = timeBuffered / $media.duration;
                    $("#txtTimeRange").val(buff);

                    var value = $("#txtCurrentTime").val();
                    if (value.length == 0) value = "0";
                    value = parseFloat(value);

                    $media.currentTime = value;
                    $media.play();

                });

                //使用事件监听方式捕捉事件
                $media.addEventListener("timeupdate", function(e) {
                    var timeDisplay = document.getElementById("time");
                    //用秒数来显示当前播放进度
                    timeDisplay.innerHTML = Math.floor($media.currentTime) + "／" + Math.floor($media.duration) + "（秒）";

                    $("#txtCurrentTime").val($media.currentTime);

                    console.log((new Date()).getTime(), e);

                }, false);

                $media.addEventListener('ended', function () {

                    $("#txtEnded").val("已经结束");

                    //  第一个视频播放结束
                    //  如果要继续播放第二个视频，改变 <video> 标签的 (src="test2.mp4") 属性或者新建一个 video 标签播放视频都可以
                }, false);
            }

        });

        function PlayMusic() {
            var value = $("#txtCurrentTime").val();
            if (value.length == 0) value = "0";
            value = parseFloat(value);

            $media.currentTime = $media.currentTime + value;

            $media.play();
        }


        function autioTimeSpan() {
            var ProcessNow = 0;
            setInterval(function () {
                var ProcessNow = ($media.currentTime / $media.duration) * 260;
                $(".ProcessNow").css("width", ProcessNow);
                var currentTime = autioTimeFormat($media.currentTime);
                var timeAll = autioTimeFormat($media.duration);
                $(".SongTime").html(currentTime + " | " + timeAll);
            }, 1000);
        }  //TimeSpan()

        function autioTimeFormat(number) {
            var minute = parseInt(number / 60);
            var second = parseInt(number % 60);
            minute = minute >= 10 ? minute : "0" + minute;
            second = second >= 10 ? second : "0" + second;
            return minute + ":" + second;
        } //timeFormat()

        //播放暂停切换  
        function playAudio() {
            if ($media.paused) {
                play();
            } else {
                pause();
            }
        }

        //播放  
        function play() {
            $media.play();
            //$('#play').html('Pause');
        }

        //暂停  
        function pause() {
            $media.pause();
            //$('#play').html('Play');
        }
    </script>
</head>
<body>

<div id="main" class="container-fluid">

    <div class="row">
        <div id="audioControl" style="display: none; margin: 100px auto; width: 300px;">
            <div class="play">
                <span id="play">Play</span>  
            </div>
        </div>

        <%--<div style="height: 100px; margin: 20px 5px; width: 300px;"></div>--%>
        <audio controls="controls" preload="auto" id="media">
            <%-- <source src="music.mp3" type="audio/mp3"/>
                <source src="music.ogg" type="audio/ogg"/>
                <source src="music.wav" type="audio/wav"/>--%>
            你的浏览器不支持audio标签。
        </audio>

    </div>
    <div class="row">

        <div style="margin: 20px 5px;">
            <a id="mp3" href="#" class="easyui-linkbutton c5" style="width: 150px">MP3音频播放</a>
            <a id="ogg" href="#" class="easyui-linkbutton c6" style="width: 150px">OGG音频播放</a>
            <a id="wav" href="#" class="easyui-linkbutton c7" style="width: 150px">WAV音频播放</a>
            <span id="time"></span> 
        </div>
    </div>
    <div class="row">
        <div style="margin: 20px 5px;">
            <%--<textarea id="txtMediaMessage" cols="20" rows="5" style="width: 100%;"></textarea>--%>
            当前时间：<input id="txtCurrentTime" type="text"/>
            时长：<input id="txtDuration" type="text"/>
            是否结束：<input id="txtEnded" type="text"/>
            缓存范围：<input id="txtTimeRange" type="text"/>
            继续播放，重新播放；是否结束

        </div>
    </div>

</div>

<div class="container-fluid">
    <div class="row">
        <button type="button" class="btn btn-primary" id="btnFastBack" title="快退，-30S">
            <i class="glyphicon glyphicon-step-backward"></i>
        </button>
        <button type="button" class="btn btn-primary" id="btnPlay" title="播放">
            <i class="glyphicon glyphicon-play"></i>
        </button>
        <button type="button" class="btn btn-primary" id="btnPause" title="暂停" style="display: none;">
            <i class="glyphicon glyphicon-pause"></i>
        </button>
        <button type="button" class="btn btn-primary" id="btnStop" title="停止">
            <i class="glyphicon glyphicon-stop"></i>
        </button>
        <button type="button" class="btn btn-primary" id="btnFastForward" title="快进，+30S">
            <i class="glyphicon glyphicon-step-forward"></i>
        </button>

        <button type="button" class="btn btn-primary" id="btnResumePlay">
            <i class="fa fa-play-circle"></i> 继续播放
        </button>

        <button type="button" class="btn btn-primary" id="btnReplay">
            <i class="fa fa-play-circle-o"></i> 重新播放
        </button>
    </div>
</div>

<div class="container-fluid">
    <div class="row">

        <audio id='audio'>你的浏览器不支持喔！</audio>

        <div class='MusicPanel'>
            <div class='PanelLeft'>
                <div class='circle'>
                    <span class='icon glyphicon-heart'></span></div>
            </div> <!-- Like Button -->

            <div class='PanelRight'>
                <div class='Prev'>
                    <span class='icon glyphicon-step-backward'></span></div> <!-- Prev Song Button -->
                <div id='Div1' class='Play'>
                    <span class='icon glyphicon-play'></span></div> <!-- Play & Pause Button -->
                <div class='Next'>
                    <span class='icon glyphicon-step-forward'></span></div> <!-- Next Song Button -->
                <div class="Song">
                    <span class='SongAuthor'>Greyson Chance</span><br/><span class='SongName'>Summertrain</span></div> <!-- Song Title -->

                <div class="Process">
                    <!-- Process -->
                    <div class="ProcessAll"></div> <!-- ProcessAll -->
                    <div class="ProcessNow"></div> <!-- ProcessNow -->
                    <div class="SongTime">00:00&nbsp;|&nbsp;00:00</div> <!-- Time -->
                </div> <!-- Process End -->
            </div> <!-- PanelRight End -->
        </div> <!-- MusicPanel End -->

    </div>
</div>

    <script type="text/javascript">
        
        $(document).ready(function () {

            var audio = document.getElementById("audio");
            audio.src = "audio/Scarborough%20Fair.mp3";

            $("#Play").on('click', function () {
                if (audio.paused) {
                    if ($(this).children().hasClass('glyphicon-play')) {
                        $("#Play").children("span").removeClass("glyphicon-play").addClass("glyphicon-pause");
                        Play();
                    }
                }
                else {
                    $("#Play").children("span").removeClass("glyphicon-pause").addClass("glyphicon-play");
                    Pause();
                }
            });// Button cilick

            function Play() {
                audio.play();
                TimeSpan();
            } //Play()

            function Pause() {
                audio.pause();
            } //Pause()

            function TimeSpan() {
                var ProcessNow = 0;
                setInterval(function () {
                    var ProcessNow = (audio.currentTime / audio.duration) * 260;
                    $(".ProcessNow").css("width", ProcessNow);
                    var currentTime = timeFormat(audio.currentTime);
                    var timeAll = timeFormat(TimeAll());
                    $(".SongTime").html(currentTime + " | " + timeAll);
                }, 1000);
            }  //TimeSpan()

            function timeFormat(number) {
                var minute = parseInt(number / 60);
                var second = parseInt(number % 60);
                minute = minute >= 10 ? minute : "0" + minute;
                second = second >= 10 ? second : "0" + second;
                return minute + ":" + second;
            } //timeFormat()

            function TimeAll() {
                return audio.duration;
            } //TimeAll()

        })


        //$(function () {
        //    var p = new Player();
        //    p.read("play");
        //    $("#stop").click(function () {
        //        p.pause();
        //    });
        //    $("#start").click(function () {
        //        p.play();
        //    });
        //    $("#show").click(function () {
        //        alert(p.duration());
        //    });
        //    setInterval(function () {
        //        $("#currentTime").text(p.currentTime());
        //    }, 1000);
        //});
        //function Player() { };
        //Player.prototype = {
        //    box: new Object(),
        //    read: function (id) {
        //        this.box = document.getElementById(id);
        //    },
        //    play: function () {
        //        this.box.play();
        //    },
        //    pause: function () {
        //        this.box.pause();
        //    },
        //    src: function (url) {
        //        this.box.src = url;
        //    },
        //    currentTime: function () {
        //        return (this.box.currentTime / 60).toFixed(2);
        //    }
        //};
        //Player.prototype.duration = function () {
        //    return (this.box.duration / 60).toFixed(2);
        //};





        //; (function () {

        //    var _init = function () {
        //        var audio = this.audio = document.createElement('audio');
        //        for (var prop in this.audioProp) {
        //            audio[prop] = this.audioProp[prop];
        //        }
        //        document.body.appendChild(audio);
        //    };

        //    var _bindEvt = function () {
        //        var audio = this.audio,
        //            audioEvt = this.audioEvt;
        //        for (var func in audioEvt) {
        //            audio.addEventListener(func, audioEvt[func].bind(this), false);
        //        }
        //    };

        //    var _extend = function (o1, o2) {
        //        for (var attr in o2) {
        //            o1[attr] = o2[attr];
        //        }
        //        return o1;
        //    };

        //    /*
        //     *   构造函数
        //     */
        //    function constructor(opton) {
        //        opton = _extend({
        //            audioProp: {

        //            },
        //            audioEvt: {

        //            },
        //            cssSelector: {

        //            }

        //        }, opton);

        //        _extend(this, opton);
        //        _init.call(this);
        //        _bindEvt.call(this);
        //    }

        //    _extend(constructor.prototype, {
        //        /*
        //         *   播放
        //         *     second 指定当前的播放时间 
        //         */
        //        play: function (second) {
        //            second && (this.audio.currentTime = second);
        //            this.audio.play();
        //        },

        //        /*
        //         *   暂停
        //         *   second 指定当前的播放时间
        //         */
        //        pause: function (second) {
        //            second && (this.audio.currentTime = second);
        //            this.audio.pause();
        //        },


        //        /*
        //         *   时间格式化
        //         *     00:00
        //         */
        //        timeFormat: function timeFormat(number) {
        //            var minute = parseInt(number / 60);
        //            var second = parseInt(number % 60);
        //            minute = minute >= 10 ? minute : "0" + minute;
        //            second = second >= 10 ? second : "0" + second;
        //            return minute + ":" + second;
        //        }

        //    });


        //    //兼容amd,cmd,原生js接口
        //    if (typeof module !== 'undefined' && typeof exports === 'object' && define.cmd) {
        //        module.exports = constructor;
        //    } else if (typeof define === 'function' && define.amd) {
        //        define(function () {
        //            return constructor;
        //        });
        //    } else {
        //        window.APlayer = constructor;
        //    }

        //})();


        //var music = new APlayer({
        //    audioProp: {
        //        title: '给我一个理由忘记',
        //        loop: true,
        //        src: "https://ss1.baidu.com/8aQDcnSm2Q5IlBGlnYG/stat/ogg/xinsui.mp3",
        //    },
        //    audioEvt: {
        //        canplay: function () {
        //        },
        //        timeupdate: function () {
        //            $('.currentTime').html(this.timeFormat(this.audio.currentTime));
        //        }
        //    }

        //});

        //$(".play").on("touchend", function () {
        //    music.play();
        //});
        //$(".pause").on("touchend", function () {
        //    music.pause(5);
        //});


        //编程总不会这么一帆风顺,莫名其妙的问题总是不会少的.下面就罗列一下我遇到的坑吧.

        //1.  安卓uc浏览器下,对timeupdate事件支持不好,只会很少的触发几次.这是什么概念,就是说我们基本上不能同步当前的播放时间和进度条了,还有歌词.

        //    解决方案:暂时没有

        //2.  在ios,Safari下,不能自动播放,autoplay,preload属性无效. 或者audio.src='xx' , 加载完后手动调用audio.play()也是不行的.

        //    原因: 在ios,Safari下要求与用户交互后才加载歌曲,播放歌曲,如touchstart , touchend , click等事件. 

        //    解决方案 : 在iphone4下touchstart无效, 目测是因为性能不够无法捕捉. 改用touchend就好了

        //3.  ios,Safari同一时间只能播放但音频/视频

        //解决方案 : 使用 audio sprite 是满足移动 Safari 中多音效需求的最佳解决方案。与 CSS image sprite 类似，audio sprite 可以将所有的音频综合到一个音频流.要注意的是更改 currentTime 并不是百分百正确的。

        //将 currentTime 设为 6.5，而实际得到的却是 6.7 或 6.2。每个 A sprite 之间需要少量的空间，以避免寻找到另一个 sprite 的尾部。


        //// audioSprite has already been loaded using a user touch event
        //var audioSprite = document.getElementById('audio');
        //var spriteData = {
        //    meow1: {
        //        start: 0,
        //        length: 1.1
        //    },
        //    meow2: {
        //        start: 1.3,
        //        length: 1.1
        //    },
        //    whine: {
        //        start: 2.7,
        //        length: 0.8
        //    },
        //    purr: {
        //        start: 5,
        //        length: 5
        //    }
        //};

        //// play meow2 sprite
        //audioSprite.currentTime = spriteData.meow2.start;
        //audioSprite.play();

        ////记得播放完毕手, 要手动停止

        //var handler = function () {
        //    if (this.currentTime >= spriteData.meow2.start + spriteData.meow2.length) {
        //        this.pause();
        //    }
        //};
        //audioSprite.addEventListener('timeupdate', handler, false);

        //if(navigator.userAgent.indexOf("Chrome") > -1){
        //    如果是Chrome：
        //    <audio src="" type="audio/mp3" autoplay=”autoplay” hidden="true"></audio>
        //}else if（navigator.userAgent.indexOf("Firefox")!=-1）{
        //    如果是Firefox：
        //    <embed src="" type="audio/mp3" hidden="true" loop="false" mastersound></embed>
        //}else if（navigator.appName.indexOf("Microsoft Internet Explorer")!=-1 && document.all）{
        //    如果是IE(6,7,8):
        //  <object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95"><param name="AutoStart" value="1" /><param name="Src" value="" /></object>
        //}else if（navigator.appName.indexOf("Opera")!=-1）{
        //    如果是Oprea：
        //    <embed src="" type="audio/mpeg"   loop="false"></embed>
        //}else{
        //<embed src="" type="audio/mp3" hidden="true" loop="false" mastersound></embed>
        //}

    </script>

<%--    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="1" height="1">

  <param name="movie" value="flash/music.swf" />

  <param name="quality" value="high" />

  <embed src="flash/music.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="1" height="1"></embed>

</object>--%>

</body>
</html>


<!--
著作权归作者所有。
商业转载请联系作者获得授权,非商业转载请注明出处。
链接:http://caibaojian.com/html5-audio.html
来源:http://caibaojian.com

附录：
Audio 对象属性
属性	描述
audioTracks	返回表示可用音频轨道的 AudioTrackList 对象。
autoplay	设置或返回是否在就绪（加载完成）后随即播放音频。
buffered	返回表示音频已缓冲部分的 TimeRanges 对象。
controller	返回表示音频当前媒体控制器的 MediaController 对象。
controls	设置或返回音频是否应该显示控件（比如播放/暂停等）。
crossOrigin	设置或返回音频的 CORS 设置。
currentSrc	返回当前音频的 URL。
currentTime	设置或返回音频中的当前播放位置（以秒计）。
defaultMuted	设置或返回音频默认是否静音。
defaultPlaybackRate	设置或返回音频的默认播放速度。
duration	返回音频的长度（以秒计）。
ended	返回音频的播放是否已结束。
error	返回表示音频错误状态的 MediaError 对象。
loop	设置或返回音频是否应在结束时再次播放。
mediaGroup	设置或返回音频所属媒介组合的名称。
muted	设置或返回是否关闭声音。
networkState	返回音频的当前网络状态。
paused	设置或返回音频是否暂停。
playbackRate	设置或返回音频播放的速度。
played	返回表示音频已播放部分的 TimeRanges 对象。
preload	设置或返回音频的 preload 属性的值。
readyState	返回音频当前的就绪状态。
seekable	返回表示音频可寻址部分的 TimeRanges 对象。
seeking	返回用户当前是否正在音频中进行查找。
src	设置或返回音频的 src 属性的值。
textTracks	返回表示可用文本轨道的 TextTrackList 对象。
volume	设置或返回音频的音量。
Audio 对象方法
方法	描述
addTextTrack()	向音频添加新的文本轨道。
canPlayType()	检查浏览器是否能够播放指定的音频类型。
fastSeek()	在音频播放器中指定播放时间。
getStartDate()	返回新的 Date 对象，表示当前时间线偏移量。
load()	重新加载音频元素。
play()	开始播放音频。
pause()	暂停当前播放的音频。    
    
    -->