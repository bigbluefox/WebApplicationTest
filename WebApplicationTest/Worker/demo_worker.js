addEventListener('message', function (event) {
    var count = event.data;
    var interval = setInterval(function () {
        postMessage(count--); !count && clearInterval(interval);
    }, 1000);

});