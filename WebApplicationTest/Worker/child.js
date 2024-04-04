
// Worker 内部如果要加载其他脚本，有一个专门的方法importScripts()。
importScripts('importScripts.js');

var childlength = 12;
if (iMax) childlength = iMax;

for (var i = 0; i < childlength; i++) {
    console.log("子线程计数：" + i);
}


postMessage("I\'m working before postMessage(\'hello my worker\').");

onmessage = function (event) {

    if (isNumber(event.data)) {
        console.log("前台传递循环数：" + event.data);

        for (var i = 0; i < event.data; i++) {
            console.log("子线程计数：" + i);
        }
    } else {
        postMessage("Hi " + event.data);
    }
    
};

/**
 * 判断是否是数字
 */
function isNumber(value) {
    if (isNaN(value)) {
        return false;
    }
    else {
        return true;
    }
}