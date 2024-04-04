$(document).ready(function () {

    var audio = document.getElementById("audio");
    audio.src = "../audio/Scarborough%20Fair.mp3";

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