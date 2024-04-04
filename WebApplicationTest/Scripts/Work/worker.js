//worker.js
//onmessage = function (evt) {
//    var d = evt.data;//通过evt.data获得发送来的数据
//    postMessage(d);//将获取到的数据发送会主线程
//}

var n = 1;
search:
    while (n < 99999) {
        // 开始搜寻下一个质数
        n += 1;
        for (var i = 2; i <= Math.sqrt(n); i++) {
            // 如果除以n的余数为0，开始判断下一个数字。
            if (n % i == 0) {
                continue search;
            }
        }
        // 发现质数
        postMessage(n);
    }