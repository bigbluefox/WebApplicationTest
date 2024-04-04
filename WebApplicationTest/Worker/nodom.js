
importScripts("js/jquery.nodom.js");


onmessage = function(event) {

    $.ajax({
        url: "/Handler/NodomHandler.ashx",
        dataType: "text", // xml,html,script,json,jsonp,text
        //type: "get", // get/post
        //async:"true/false",
        data: { "MSG": event.data },
        success: function(data) {
            postMessage("Hi, " + data);
        },
        error: function() {
            console.log("ajax失败");
        }
    });


};

//dataType 可用类型：
//（如果不指定，JQuery将自动根据http包mime信息返回responseXML或responseText，并作为回调函数参数传递）

//xml：返回XML文档，可用JQuery处理。
//html：返回纯文本HTML信息。
//script：返回纯文本JavaScript代码。
//json：返回json数据。
//jsonp：(JSON with Padding) 是 json 的一种"使用模式"，可以让网页从别的域名（网站）那获取资料，即跨域读取数据。
//text：返回纯文本字符串。
//说明：对于json和jsonp的区别，本小白暂时没有深入了解，目前只知道jsonp可以跨域读取数据，有待进一步学习~