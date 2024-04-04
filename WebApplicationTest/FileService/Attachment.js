/**
* @author Tli
* @description 附件文件相关操作方法
* @date 2017-3-22
*/

// 可上传文档类型
var fileTypeImg = "doc|docx|ppt|pptx|xls|xlsx|xlt|dot|dps|dpt|et|ett|gif|htm|html|bmp|jpeg|jpg|mdb|pdf|png|pot|psd|pub|rar|rtf|txt|tif|tiff|txt|vsd|vss|vst|wps|wpt|xml|zip";
var viewFileType = "jpg|png|pdf"; // 可查看文档类型
var editFileType = "doc|docx|ppt|pptx|xls|xlsx|vso|pro|wps|et"; // 可编辑文档类型

var editFileType = "doc|docx|ppt|pptx|xls|xlsx|vso|pro|wps|et";

String.prototype.getBytes = function() {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
};

//验证字符数
function validFileLenth(file) {
    //alert("唯一标识:" + queueId + "\r\n" + "文件名：" + fileObj.name + "\r\n" + "文件大小：" + fileObj.size + "\r\n" + "创建时间：" + fileObj.creationDate + "\r\n" + "最后修改时间：" + fileObj.modificationDate + "\r\n" + "文件类型：" + fileObj.type ); } 
    var fileName = file.name;
    var len = fileName.getBytes() - 4;
    if (len > 390) {
        alert("文件名称汉字限制在200字以内,英文限制在400字符以内");
        return false;
    }
    return true;
}

//--------------------------------------------------------------------------
// 生成随机数

function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

/// <summary>
/// 获取上传文件信息
/// </summary>
/// <history> Created At 2014.06.07 By Tli </history> 

function GetFileList(typeId, groupId) {
    typeId = typeId || TypeID, groupId = groupId || GroupID;
    // 参数
    var arr = { OPERATION: "ATTACHMENTLIST", FID: "", TID: typeId, GID: groupId, UID: UserID, RID: Math.round(Math.random() * 10) };

    // 发送ajax请求
    $.getJSON("/Handler/AttachmentHandler.ashx", arr)
        //向服务器发出的查询字符串 
        // 对返回的JSON数据进行处理 
        .done(function(data) {
            //console.info(data);
            if (data != null && data.length > 0) {
                $("#tbFileList").datagrid("loading");
                $("#tbFileList").datagrid("loadData", { "total": data[0].RecordCount, "rows": data });
                $("#tbFileList").datagrid("loaded");
            } else {
                //$.messager.alert('温馨提示', '查询失败！', 'error');
                $("#tbFileList").datagrid("loadData", { "total": 0, "rows": [] });
                $("#tbFileList").datagrid("loaded");
            }
        });
}

/// <summary>
/// 下载文档
/// </summary>

function DownloadFile(id, url) {
    var href = "/Handler/AttachmentHandler.ashx?OPERATION=DOWNLOAD&FID=" + id + "&file=" + escape(url) + "&name=" + escape(name);
    window.location.href = href;
    //window.open(ServiceUrl + VD + url, "_blank");
}

/// <summary>
/// 在线查看文档
/// </summary>

function ViewFileOnline(id, extName, url) {

    //var url = "";
    extName = extName.replace(".", "");

    //debugger;
    //var na = navigator.userAgent;
    //var n = navigator;

    //alert(navigator.userAgent);

    if (editFileType.indexOf(extName.toLocaleLowerCase()) > -1) {
        var isIe = (0 <= navigator.userAgent.indexOf("MSIE"));
        if (isIe) { // IE浏览器 
            url = "/FileService/DocWatch.aspx?FID=" + id;
            if (extName) {
                url += "&extName=" + extName;
            }

            window.open(url, "标准体系文档");
        } else {
            //url = "/WebOffice/DocShow.aspx?DocumentID=" + id;

            //url = "/DocWatch.aspx?FID=" + id;
            //if (extName) {
            //    url += "&extName=" + extName;
            //}

            //url = ServiceUrl + VD + url;

            DownloadFile(id, url);
        }

        //window.open(url, "标准体系文档");
    } else {
        //url = "/WebOffice/DocShow.aspx?DocumentID=" + id;
        //window.open(url, "标准体系文档");
        // 可查看文档
        if (viewFileType.indexOf(extName.toLocaleLowerCase()) > -1) {
            url = ServiceUrl + VD + url;
            window.open(url, "标准体系文档");
        } else {
            DownloadFile(id, url);
        }
    }
}

/// <summary>
/// 在线编辑文档
/// </summary>

function EditFileOnline(id, extName) {

    extName = extName.replace(".", "");

    if (editFileType.indexOf(extName.toLocaleLowerCase()) > -1) {
        var url = "/FileService/DocEdit.aspx?FID=" + id;
        if (extName) {
            url += "&extName=" + extName;
        }
        window.open(url, "文档修改");
    } else {
        alert("该类型文件不可修改！");
    }
}

/// <summary>
/// 删除文档
/// </summary>

function DelFileById(id, path) {
    //var r = confirm("您确定要删除该文档吗？");
    if (confirm("您确定要删除该文档吗？")) {
        var url = "/Handler/AttachmentHandler.ashx?OPERATION=DELETEFILE&FID=" + id + "&GID=&TID=&PATH=" + path;
        $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
            if (data) {
                if (data.IsSuccess) {
                    $.messager.alert("操作提示", data.Message, "info");
                    GetFileList();
                } else {
                    alert(data.Message);
                }
            } else {
                alert("删除异常，请重试");
            }
        });
    }
}

/// <summary>
/// 附件批量下载
/// </summary>

function BatchDownload() {

    var url = "/Handler/AttachmentHandler.ashx?OPERATION=BATCHDOWNLOAD&FID=&TID=" + TypeID + "&GID=" + GroupID;
    $.get(url + "&rnd=" + (Math.random() * 10), null, function (data) {
        if (data) {
            if (data.IsSuccess) {
                window.open(data.Url, "附件批量下载");
            } else {
                $.messager.alert("操作提示", data.Message, "info");
            }
        } else {
            alert("附件批量下载异常，请重试");
        }
    });


    //$.ajax({
    //    url: url+ "&rnd=" + (Math.random() * 10),
    //    type: 'post',
    //    data: {},
    //    dataType: 'json',
    //    cache: false,

    //    success: function (data) {

    //        debugger;


    //        alert(data.IsSuccess);

    //        if (data) {
    //            if (data.IsSuccess) {
    //                //$.messager.alert("操作提示", data.Message + "\n 下载地址："+ data.Url, "info");
    //                //$.messager.show({ title: "操作提示", msg: data.Message + ", 下载地址："+ data.Url, timeout: 3000, showType: "fade" });
    //                window.open(data.Url, "附件批量下载");
    //            } else {
    //                //alert(data.Message);
    //                $.messager.alert("操作提示", data.Message, "info");
    //            }
    //        } else {
    //            alert("附件批量下载异常，请重试");
    //        }

    //        //if (data.status == 302) {
    //        //    location.href = data.location;
    //        //}
    //    }
    //}).fail(function (xhr, error, status) {

    //        debugger;

    //            alert("error");
    //        });
        


    ////var url = "/Handler/AttachmentHandler.ashx?OPERATION=DELETEFILE&FID=" + id + "&GID=&TID=&PATH=" + path;
    //$.get(url + "&rnd=" + (Math.random() * 10), function (data) {

    //    debugger;

    //    alert(data.IsSuccess);

    //    if (data) {
    //        if (data.IsSuccess) {
    //            //$.messager.alert("操作提示", data.Message + "\n 下载地址："+ data.Url, "info");
    //            //$.messager.show({ title: "操作提示", msg: data.Message + ", 下载地址："+ data.Url, timeout: 3000, showType: "fade" });
    //            window.open(data.Url, "附件批量下载");
    //        } else {
    //            //alert(data.Message);
    //            $.messager.alert("操作提示", data.Message, "info");
    //        }
    //    } else {
    //        alert("附件批量下载异常，请重试");
    //    }
    //}).fail(function (data) {

    //    debugger;

    //        alert("error");
    //    });



    //$.getJSON(url)
    //    .done(function (rst) {

    //debugger;

    //        if (rst) {
    //            var data = rst;
    //            //SetCookie("USERINFO", Hitech.Common.ObjToString(rst));

    //                if (data) {
    //                    if (data.IsSuccess) {
    //                        //$.messager.alert("操作提示", data.Message + "\n 下载地址："+ data.Url, "info");
    //                        //$.messager.show({ title: "操作提示", msg: data.Message + ", 下载地址："+ data.Url, timeout: 3000, showType: "fade" });
    //                        window.open(data.Url, "附件批量下载");
    //                    } else {
    //                        //alert(data.Message);
    //                        $.messager.alert("操作提示", data.Message, "info");
    //                    }
    //                } else {
    //                    alert("附件批量下载异常，请重试");
    //                }


    //        } else {
    //            $.messager.alert('错误提示', data.Message, 'error');
    //        }
    //    });

    //$.ajax({
    //    type: "POST",
    //    url: "/Handler/AttachmentHandler.ashx?OPERATION=BATCHDOWNLOAD&rnd=" + (Math.random() * 10),
    //    data: { FID: "", GID: GroupID, TID: TypeID },
    //    context: document.body,
    //    statusCode: {
    //        404: function () {
    //            alert("page not found");
    //        }
    //    }
    //}).done(function (data) {

    //        debugger;

    //    if (data) {
    //        if (data.IsSuccess) {
    //            //$.messager.alert("操作提示", data.Message + "\n 下载地址："+ data.Url, "info");
    //            //$.messager.show({ title: "操作提示", msg: data.Message + ", 下载地址："+ data.Url, timeout: 3000, showType: "fade" });
    //            window.open(data.Url, "附件批量下载");
    //        } else {
    //            //alert(data.Message);
    //            $.messager.alert("操作提示", data.Message, "info");
    //        }
    //    } else {
    //        alert("附件批量下载异常，请重试");
    //    }

    //    //debugger;
    //    //if (console && console.log) {
    //    //    console.log("Sample of data:", data.slice(0, 100));
    //    //}
    //    //$(this).addClass("done");

    //}).fail(function () {
    //    alert("error");
    //})
    //    .always(function () {
    //        //alert("complete");
    //    });
}

/// <summary>
/// 附件操作日志
/// </summary>

function AttachmentLog() {

    //parent.AttachmentLog();

    //$.ajax({
    //        type: "POST",
    //        url: "/SystemMgmt/Attachment/AttachmentLog/?rnd=" + (Math.random() * 10),
    //        data: { FID: "", GID: GroupID, TID: TypeID }
    //    })
    //    .done(function(rst) {
    //        if (rst) {
    //            if (rst.IsSuccess) {
    //                if (rst.ObjStr.length > 0) {
    //                    $("#toolbar").html(rst.ObjStr);
    //                }
    //            } else {
    //                $.messager.alert('提示', rst.Message, 'error');
    //            }
    //        }
    //    });
}



//var xmlHttp;

//function createRequest() {
//    if (window.XMLHttpRequest) {
//        xmlHttp = new XMLHttpRequest();
//    } else if (window.ActiveXObject) {
//        try {
//            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
//        } catch (e) {
//            try {
//                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
//            } catch (e) {
//                xmlHttp = false;
//            }
//        }
//    }
//}

//function checkuser() {
//    createRequest();
//    var url = "/Handler/AttachmentHandler.ashx?OPERATION=BATCHDOWNLOAD&FID=&TID=" + TypeID + "&GID=" + GroupID;

//    xmlHttp.open("GET", url, true);
//    xmlHttp.onreadystatechange = myfunc;
//    xmlHttp.send(null);
//}

//function myfunc() {

//    //debugger;

//    if (xmlHttp.readyState == 4) {
//        if (xmlHttp.status == 200) {
//            var mess = xmlHttp.responseText; // "{\"IsSuccess\":true,\"Message\": \"附件批量下载成功！\", Url = \"/Uploads/1235.zip?rnd=2041175501\"}"
//            var rst = eval("(" + mess + ")");
//            //if (mess == "success") {
//            //    alert("用户名正确.");
//            //} else if (mess == "fail") {
//            //    alert("该用户名不存在");
//            //}
//            //alert(mess);

//            var data = rst;

//                if (data) {
//                    if (data.IsSuccess) {
//                        //$.messager.alert("操作提示", data.Message + "\n 下载地址："+ data.Url, "info");
//                        //$.messager.show({ title: "操作提示", msg: data.Message + ", 下载地址："+ data.Url, timeout: 3000, showType: "fade" });
//                        window.open(data.Url, "附件批量下载");
//                    } else {
//                        //alert(data.Message);
//                        $.messager.alert("操作提示", data.Message, "info");
//                    }
//                } else {
//                    alert("附件批量下载异常，请重试");
//                }
//        }
//    }
//}