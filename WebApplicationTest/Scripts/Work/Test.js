//onmessage = function (e) {
//    //console.log('Message received from main script');
//    //var workerResult = { "Date": "2016-5-6", "Name": "Tom", "Age": 20, "Company": "IBM" };
//    //console.log('Posting message back to main script');
//    var rst = (new Date()).valueOf();
//    postMessage(rst);
//}

var getResult = function (para) {

    //var p = para.Date;

    //return [{ "Date": "2016-5-6", "Name": "Tom", "Age": 20, "Company": "IBM" },
    //    { "Date": "2016-6-6", "Name": "Tony", "Age": 22, "Company": "MOTO" }];

    var url = "/Handler/TestHandler.ashx?rnd=" + Math.random() * 16;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url);
    xhr.onload = function () {
        postMessage(xhr.responseText);
    };
    xhr.send();
};

onmessage = function(evt) {
    //var d = evt.data;//通过evt.data获得发送来的数据
    //postMessage(d);//将获取到的数据发送会主线程
    //var rst = (new Date()).valueOf();
    //var rst = getResult();

    console.log(evt.data[0] + " * " + evt.data[1]);

    //var action = function (myaction, name) {
    //    eval(myaction + "('" + name + "')");
    //}

    //console.log(action(evt.data[0], evt.data[1]));
    //var rst = action(evt.data[0], evt.data[1]);

    //var func = evt.data[0];
    var action = eval(evt.data[0]);
    var rst = action(evt.data[1]);

    postMessage(rst);
}