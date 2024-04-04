//1.通过postMessage( data ) 方法来向主线程发送数据。
//2.绑定onmessage方法来接收主线程发送过来的数据

var func = function() {};

onmessage = function(evt) {
    var d = evt.data; //通过evt.data获得发送来的数据
    postMessage(d); //将获取到的数据发送会主线程
};