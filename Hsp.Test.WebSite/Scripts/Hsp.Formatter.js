/*
 * Bootstrap Table 格式化
 */

if (!HSP.Formatter) HSP.Formatter = {};

function detailFormatter(index, row) {
    var html = [];
    $.each(row, function(key, value) {
        html.push("<p><b>" + key + ":</b> " + value + "</p>");
    });
    return html.join("");
}

// 文件类型格式化
function typeFormatter(value, row, index) {
    var ext = value.replace(".", "");
    return '<img src="/images/filetype/16/' + ext + '.gif" alt="" title="' + ext + '" />';
}

// 提示内容格式化
function titleFormatter(value, row, index) {
    return "<span title='" + value + "'>" + value + "</span>";
}

// 提示内容格式化
function tipsFormatter(value, row, index) {
    return "<span title='" + value + "'>" + value + "</span>";
}

// 日期格式化
function dateFormatter(value, row, index) {
    return value.toDateTimeString("yyyy-MM-dd");
}

// 日期时间格式化
function dateTimeFormatter(value, row, index) {
    return value.toDateTimeString("yyyy-MM-dd HH:mm:SS");
}

// 数字千分位格式化  
function toThousands(num) {
    num = (num || 0).toString();
    var result = "";
    while (num.length > 3) {
        result = "," + num.slice(-3) + result;
        num = num.slice(0, num.length - 3);
    }
    if (num) {
        result = num + result;
    }
    return result;
}

// 文件大小格式化
function sizeFormatter(value, row, index) {
    var s = "";
    if (value < 102.4) {
        s = "0.10K";
    } else if (value < 1024 * 1024) {
        s = (value / 1024.0).toFixed(2) + "K";
    } else if (value < 1024 * 1024 * 1024) {
        s = (value / 1024.0 / 1024.0).toFixed(2) + "M";
    } else {
        s = (value / 1024.0 / 1024.0 / 1024.0).toFixed(2) + "G";
    }
    return "<span title='" + value + "'>" + s + "</span>";
}

// 文件大小数字格式化
function fileSizeFormat(num) {
    var s = "";
    if (num < 102.4) {
        s = "0.10K";
    } else if (num < 1024 * 1024) {
        s = (num / 1024.0).toFixed(2) + "K";
    } else if (num < 1024 * 1024 * 1024) {
        s = (num / 1024.0 / 1024.0).toFixed(2) + "M";
    } else {
        s = (num / 1024.0 / 1024.0 / 1024.0).toFixed(2) + "G";
    }
    return s;
}