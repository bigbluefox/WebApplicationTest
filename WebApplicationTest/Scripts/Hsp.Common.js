
/*
Author		: Tli
CreateTime	: 2016-09-20
Description	: HSP通用对象处理
*/
/// <reference path="~/Scripts/HSP.js" />

HSP.CONST.PAGE_INDEX = 0, HSP.CONST.PAGE_SIZE = 0, HSP.CONST.PAGE_LIST = [100, 50, 20]; // 常量定义
HSP.CONST.HEIGHT_OFFSET = 0; // 高度偏移量

/// <summary>
///     页面内容窗体有效宽度，Tli，2016-09-21
/// </summary>
HSP.Common.AvailWidth = function() {

    var width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;

    //if ($.browser.msie) {
    //} else if ($.browser.chrome) {
    //} else if ($.browser.safari || $.browser.mozilla) {
    //}

    return width;
};

/// <summary>
/// 页面内容窗体有效高度，Tli，2016-09-21
/// </summary>

HSP.Common.AvailHeight = function() {
    //兼容各浏览器 
    var availHeight = window.screen.availHeight - (window.screenTop ? window.screenTop : 0);
    var winheight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
    var height = Math.min(availHeight, winheight);

    var zoom = HSP.Common.BrowserScaling();

    if (HSP.Browser.IS_EDGE) {
        if (zoom == 100) {
            height -= -60; // 824/100
        }
        if (zoom == 125) {
            height -= -26; // 677/125 
        }
        if (zoom == 150) {
            height -= -8; // 575/150
        }
        if (zoom == 175) {
            height -= 2; // 499/175
        }
    }

    if (HSP.Browser.IS_GC && !HSP.Browser.IS_EDGE) {
        height -= 59;
        // 594/150-100
    }

    if (HSP.Browser.IS_FF) {
        height -= 16; // 525/150-100
    }

    if (HSP.Browser.IS_IE) {
        if (zoom == 100) {
            height -= 3; // 800/100
        }
        if (zoom == 125) {
            height -= 18; // 640/125 
        }
        if (zoom == 150) {
            height -= 19; // 524/150
        }
        if (zoom == 175) {
            height -= 19; // 440/175
        }
    }

    //if (HSP.Browser.IS_IE) {
    //}

    //if (HSP.Browser.IS_GC) {
    //    height -= 80;
    //}

    //if ($.browser.msie) {
    //} else if ($.browser.chrome) {
    //    height -= 80;
    //} else if ($.browser.safari || $.browser.mozilla) {
    //}

    return height;
};


HSP.Common.BrowserScaling = function () {
    var ratio = 0,
  screen = window.screen,
  ua = navigator.userAgent.toLowerCase();

    if (window.devicePixelRatio !== undefined) {
        ratio = window.devicePixelRatio;
    }
    else if (~ua.indexOf('msie')) {
        if (screen.deviceXDPI && screen.logicalXDPI) {
            ratio = screen.deviceXDPI / screen.logicalXDPI;
        }
    }
    else if (window.outerWidth !== undefined && window.innerWidth !== undefined) {
        ratio = window.outerWidth / window.innerWidth;
    }

    if (ratio) {
        ratio = Math.round(ratio * 100);
    }

    return ratio;
};


/// <summary>
/// 页面数据列表分页参数配置，Tli，2018-03-22
/// 基于屏幕分辨率 1920 * 1080 推算
/// </summary>

HSP.Common.PagingParameters = function () {

    var zoom = HSP.Common.BrowserScaling();

    if (HSP.Browser.IS_EDGE) {
        //if (zoom > 125) {
        //    HSP.CONST.PAGE_SIZE = 15;
        //    HSP.CONST.PAGE_LIST = [15, 30, 50];
        //} else {
        //    HSP.CONST.PAGE_SIZE = 20;
        //    HSP.CONST.PAGE_LIST = [20, 30, 50];
        //}

        if (zoom < 125) {
            HSP.CONST.PAGE_SIZE = 25;
            HSP.CONST.PAGE_LIST = [25, 50, 100];
        } else if (zoom < 150) {
            HSP.CONST.PAGE_SIZE = 20;
            HSP.CONST.PAGE_LIST = [20, 30, 50];
        } else if (zoom < 175) {
            HSP.CONST.PAGE_SIZE = 15;
            HSP.CONST.PAGE_LIST = [15, 30, 50];
        } else {
            HSP.CONST.PAGE_SIZE = 10;
            HSP.CONST.PAGE_LIST = [10, 25, 50];
        }
    }

    if (HSP.Browser.IS_GC && !HSP.Browser.IS_EDGE) {
        if (zoom < 150) {
            HSP.CONST.PAGE_SIZE = 20;
            HSP.CONST.PAGE_LIST = [50, 30, 20];
        } else if (zoom < 175) {
            HSP.CONST.PAGE_SIZE = 15;
            HSP.CONST.PAGE_LIST = [50, 30, 15];
        } else {
            HSP.CONST.PAGE_SIZE = 15;
            HSP.CONST.PAGE_LIST = [50, 30, 15];
        }
    }

    if (HSP.Browser.IS_FF) {
        HSP.CONST.PAGE_SIZE = 15;
        HSP.CONST.PAGE_LIST = [50, 30, 15];
    }

    if (HSP.Browser.IS_IE || HSP.Browser.IS_IE11) {
        if (zoom < 125) {
            HSP.CONST.PAGE_SIZE = 25;
            HSP.CONST.PAGE_LIST = [100, 50, 25];
        } else if (zoom < 150) {
            HSP.CONST.PAGE_SIZE = 20;
            HSP.CONST.PAGE_LIST = [50, 30, 20];
        } else if (zoom < 175) {
            HSP.CONST.PAGE_SIZE = 15;
            HSP.CONST.PAGE_LIST = [50, 30, 15];
        } else {
            HSP.CONST.PAGE_SIZE = 10;
            HSP.CONST.PAGE_LIST = [50, 25, 10];
        }
    }
};

/// <summary>弹出对话框窗体方法</summary>
/// <param name="obj" type="function">对话框对象</param>
/// <param name="title" type="string">对话框标题</param>
/// <param name="params" type="[]">对话框宽高参数</param>
/// <param name="buttons" type="function">可选参数，按钮组，底部</param>
/// <param name="toolbar" type="function">可选参数，工具栏，顶部</param>
/// <param name="callbackFunc" type="function">可选参数，回调函数</param>
/// <returns type="void">弹出对话框，供用户从企业组织树中选择用户</returns>

//MSO.Common.Window = function (obj, title, params, buttons, toolbar, callbackFunc) {
//    MSO.Common.Dialog(obj, title, params, buttons, toolbar, callbackFunc);
//};

HSP.Common.Dialog = function (obj, title, params, buttons, toolbar, callbackFunc) {

    var width = 600, height = 300;

    if (params) {
        width = params.width;
        height = params.height;
    }

    obj.dialog({
        title: " " + title, // 标题
        collapsible: false,
        minimizable: false,
        maximizable: false,
        resizable: true,
        width: width, // 对话框宽度
        height: height, // 对话框高度
        modal: true, // 模态窗体
        onClose: function () {
            $(document).unbind("click");

            // 如果有子元素，先remove掉子元素再销毁窗口
            if ($(this).children()) {
                $(this).children().remove();
            }

            try {
                if (callbackFunc && typeof callbackFunc === "function") {
                    callbackFunc();
                }
            } catch (e) {
            }

            $(this).window("destroy");
        }
    });

    if (buttons) {
        obj.dialog({ buttons: buttons });
    }

    if (toolbar) {
        obj.dialog({ toolbar: toolbar });
    }
};

/// <summary>
/// 弹出窗体
/// </summary>

HSP.Common.Window = function(obj, title, params, isDestroy) {
    var winWidth = document.body.clientWidth, bodyWidth = window.parent.document.body.clientWidth;

    var width = 700, height = 300;
    //var top = 0, left = 0;

    if (params) {
        width = params.width;
        height = params.height;
    }

    //if (params && params.top) {
    //    top = params.top;
    //} else {

    //    //alert(winWidth + " * " + winHeight + " * " + bodyWidth + " * " + bodyHeight);

    //    //top = (Hitech.Common.getClientHeight() + 118 - height) / 2;
    //    //top = (winHeight - height + 118) / 2;
    //    //top = top < 118 ? 5 : top - 118;

    //    var scrollTop = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
    //    top = (HSP.Common.AvailHeight() - height) / 2;
    //    top += scrollTop; // 滚动条位置修正
    //    top = top < 0 ? 5 : top;

    //    // iframe内容高度大于iframe的高度时
    //    //if (winHeight > bodyHeight - 118) {
    //    //    // 滚轮滑动的上部分的高度不为0
    //    //    var scrollTopHeight = 0;
    //    //    if (document.documentElement.scrollTop != 0) {
    //    //        top = (bodyHeight - 118 - height) / 2 + document.documentElement.scrollTop;
    //    //        scrollTopHeight = document.documentElement.scrollTop;
    //    //    } else if (document.body.scrollTop != 0) {
    //    //        // chrome中document.documentElement.scrollTop为0，但是支持document.body.scrollTop
    //    //        top = (bodyHeight - 118 - height) / 2 + document.body.scrollTop;
    //    //        scrollTopHeight = document.body.scrollTop;
    //    //    } else {
    //    //        //滚轮没滑动
    //    //        top = (bodyHeight - 118 - height) / 2;
    //    //    }
    //    //    top = top < scrollTopHeight ? 5 + scrollTopHeight : top;
    //    //}
    //}

    //if (params && params.left) {
    //    left = params.left;
    //} else {
    //    // 重新计算对话框左边距
    //    var availWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
    //    left = (winWidth == bodyWidth) ? ((availWidth - width) / 2) : (Math.abs(availWidth - width)) / 2;
    //    left = left < 0 ? 5 : left;
    //}

    obj.window({
        title: title,
        width: width,
        height: height,
        //top: top,
        //left: left,
        modal: true,
        iconCls: "icon-win",
        shadow: false,
        minimizable: false,
        maximizable: false,
        resizable: false,
        collapsible: false,
        onClose: function(event) {
            //alert($(this).html());
            // 取消document的click事件绑定， Tli, 20150630
            $(document).unbind("click");
            if (isDestroy) {
                // 如果有子元素，先remove掉子元素再销毁窗口
                if ($(this).children()) {
                    $(this).children().remove();
                }
                $(this).window("destroy");
            }
        }
    }).fadeIn(500);

    //HSP.Common.InitDialogDiv(obj);
};

/// <summary>
/// 初始化对话框
/// </summary>

HSP.Common.InitDialogDiv = function(win) {
    var isIe = (0 <= navigator.userAgent.indexOf("MSIE")); // ie浏览器
    var isGc = (0 <= navigator.userAgent.indexOf("Chrome/")); // 谷歌浏览器
    var windowDiv = $(".window-div");
    var contentDiv = $(".content-div", win)[0];
    var btnDiv = $(".btn-div", win)[0];

    // 设置窗口总体风格
    windowDiv.css("background-color", "#f1f1f1");

    // 设置主内容区式样
    $(contentDiv).attr("style", "background-color: #fbfbfb; margin:5px; overflow:hidden; border:1px solid silver;");

    if (isIe) {
        $(contentDiv).css("width", $(win).width() - 11).css("height", $(win).height() - 40);
    } else if (isGc) {
        $(contentDiv).css("width", $(win).width() - 25).css("height", $(win).height() - 53);
    } else {
        $(contentDiv).css("width", $(win).width() - 11).css("height", $(win).height() - 53);
    }

    //alert(($(win).width() - 11) + " * " + ($(win).height() - 40));
    //alert($(win).width() + " * " + $(win).height());

    // 设置内容区中的行区域
    //var rowDiv = $(".row-div", contentDiv);
    $.each($(".row-div", contentDiv), function(i, item) {
        $(item).attr("style", "height:24px; margin-left:8px; margin-top:8px; display:block;");
        if (isIe) {
            $(item).css("width", $(win).width() - 18);
        } else {
            $(item).css("width", $(win).width() - 32);
        }
    });

    // 设置按钮区式样
    $(btnDiv).attr("style", "margin-top:5px; margin-left:5px; overflow:hidden; text-align:right; ")
        .css("width", $(win).width() - 10);

    // 设置按钮高度，防止有些页面里按钮很矮    ---   yangjin  2013-12-26
    if (isIe) {
        $(":button", btnDiv).css("height", "24px");
    } else if (isGc) {
        $(":button", btnDiv).css("height", "28px").css("margin-right", "14px");
    }
};
HSP.Common.BeautifulWindow = function(obj, title, params, closeCallBack) {
    var winWidth = document.body.clientWidth, bodyWidth = window.parent.document.body.clientWidth;

    var width = 700, height = 300;
    var top = 0, left = 0;

    if (params) {
        width = params.width;
        height = params.height;
    }

    if (params && params.top) {
        top = params.top;
    } else {
        //var scrollTopHeight = 0; //滚轮移动的高度
        //var clientHeight = 0; //页面窗口的显示高度
        //if (document.documentElement.scrollTop) {
        //    scrollTopHeight = document.documentElement.scrollTop;
        //} else if (document.body.scrollTop) {
        //    scrollTopHeight = document.body.scrollTop;
        //}

        //if (document.documentElement.clientHeight) {
        //    clientHeight = document.documentElement.clientHeight;
        //} else if (document.documentElement.offsetHeight) {
        //    clientHeight = document.documentElement.offsetHeight;
        //}
        //top = (clientHeight - height) / 2 + scrollTopHeight;

        //top = top < scrollTopHeight ? 5 + scrollTopHeight : top;

        var scrollTop = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
        top = (HSP.Common.AvailHeight() - height) / 2;
        top += scrollTop; // 滚动条位置修正
        top = top < 0 ? 5 : top;
    }

    if (params && params.left) {
        left = params.left;
    } else {
        // 重新计算对话框左边距
        var availWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        left = (winWidth == bodyWidth) ? ((availWidth - width) / 2) : (Math.abs(availWidth - width)) / 2;
        left = left < 0 ? 5 : left;
    }

    obj.window({
        title: title,
        width: width,
        height: height,
        top: top,
        left: left,
        modal: true,
        iconCls: "icon-win",
        shadow: false,
        minimizable: false,
        maximizable: false,
        resizable: false,
        collapsible: false,
        onClose: function(event) {
            // 如果有子元素，先remove掉子元素再销毁窗口
            if ($(this).children()) {
                $(this).children().remove();
            };

            //$("html, body").css("overflow-y", "auto");

            if (closeCallBack) {
                closeCallBack();
            }
            $(this).window("destroy");
        }
    }).fadeIn(300);
};

/*
* 方法名称：选择服务器图标公共方法
*/
HSP.Common.SelectIcon = function(selectedData, isAllowMultiSelect, onSelected) {
    /// <summary>选择图标公共方法</summary>
    /// <param name="selectedData" type="[]">数组，打开时已选的目录列表，例如“[id: 1, name: ‘软件工程师’]”</param>
    /// <param name="isAllowMultiSelect" type="bool">是否允许多选，true-多选，false-单选</param>
    /// <param name="onSelected" type="function">选择完成后的回调函数, 回调函数的参数与selectedData相同</param>
    /// <returns type="void">弹出对话框，供用户从企业组织树中选择用户</returns>
    //var obj = $('\
    //        <div id="IconSelectionDailog" class="window-div">\
    //            <div class="content-div">\
    //                <div style="margin-left:5px; margin-top:5px;">\
    //                    <div style="width:40%;float:left;height:250px;margin-right:2px;">\
    //                        <div id="titlePanel">\
    //                        <ul id="IconDirectoryTree" class="ztree" style=" background:none;margin-bottom:5px;border:0px solid #99bbe8;"></ul>\
    //                        </div>\
    //                    </div>\
    //                    <div style="width:58%;float:right;margin-right:5px;border:0px solid #99bbe8;">\
    //                        <div id="IconGrid" style="padding:2px;"></div>\
    //                    </div>\
    //                </div>\
    //            </div>\
    //            <div class="btn-div">\
    //                <input type="button" value="确定" id="selectIconBtnOk" style="width:74px; margin-right:5px;" />\
    //                <input type="button" value="取消" id="selectIconBtnCancel" style="width:74px;" />\
    //            </div>\
    //        </div>\
    //        ');

    var width = 642, height = 417;

    var obj = $('\
            <div id="IconSelectionDailog" style="padding:5px;">\
                <div id="layout" class="easyui-layout" style="height: ' + (height - 41) + 'px;">\
                    <div region="west" title="图标目录" split="true" style="width:240px; margin:0px; padding:5px;">\
                        <ul id="IconDirectoryTree" class="ztree" style=" background:none;margin-bottom:5px;"></ul>\
                    </div>\
                    <div region="center" title="图标选择">\
                        <div id="IconGrid" style="padding:2px;"></div>\
                    </div>\
                    <div region="south" border="false" style="height:40px; padding:0px; overflow:hidden;">\
                        <div style="padding:2px; width: 200px; display:block; float:left;">\
                            <div class="checkbox"><label>\
                            <input id="selectAll" type="checkbox"> 全选\
                            </label></div>\
                        </div>\
                        <div style="padding:3px 0px 0px 0px; display:block; float:right;">\
                            <input type="button" value="确定" id="selectIconBtnOk" style="width:74px;" />\
                            <input type="button" value="取消" id="selectIconBtnCancel" style="width:74px;" />\
                        </div>\
                    </div>\
                </div>\
            </div>\
            ');

    HSP.Common.Window(obj, "图标选择对话框 - " + (isAllowMultiSelect ? "多选" : "单选"), { width: width, height: height });

    $.parser.parse("#IconSelectionDailog");

    $("#selectIconBtnOk").click(function() {
        if (onSelected) {
            //var obj = isAllowMultiSelect ? "checkbox" : "radio";
            selectedData.clear();

            //var imgPath = $("#IconGrid input[name='radioIcon']:checked").val();
            //var chkValue = [];

            $('input[name="radioIcon"]:checked').each(function() {

                //debugger;
                //var w = $(this).attr("width");
                //var h = $(this).attr("height");
                //alert(w + " * " + h);

                var imgPath = $(this).val().replace(/\\/g, "/");
                var data = { imgName: $(this).attr("imgName"), imgPath: imgPath, imgExt: $(this).attr("imgExt"), width: $(this).attr("width"), height: $(this).attr("height") };
                selectedData.push(data);
            });

            //alert(chkValue.length);
            //imgPath = imgPath.replace(/\\/g, "/");
            //var data = { imgPath: imgPath, width: 0, height: 0 };
            //selectedData.push(data);

            onSelected(selectedData);

            // 关闭窗口
            $("#IconSelectionDailog").window("destroy");
        }
    });

    $("#selectIconBtnCancel").click(function() {
        $("#IconSelectionDailog").window("destroy");
    });

    $("#selectAll").change(function() {
        var checked = this.checked;
        $('input[name="radioIcon"]').each(function() {
            this.checked = checked;
        });
    });

    var getIcons = function(path) {
        $.get("/Handler/IconsHandler.ashx?OPERATION=PATHICONS&path=" + path + "&rnd=" + (Math.random() * 10), function(rst) {
            if (rst && rst.length > 0) {
                var obj = isAllowMultiSelect ? "checkbox" : "radio";
                var icons = rst; //rst.Data;
                var imgs = "";
                if (icons.length > 0) {
                    var css = "img32";
                    if (icons.length > 50) css = "img16";
                    $.each(icons, function(i) {
                        imgs += '<span title="' + this.Name + "(" + this.Width + "X" + this.Height + ')" class="imgspan"><img src="' + this.FullName + '" class="' + css + '" /><input name="radioIcon" imgExt="' + this.Extension + '" imgName="' + this.Name + '" width="' + this.Width + '" height="' + this.Height + '" type="' + obj + '" value="' + this.FullName + '" />&nbsp;&nbsp;</span>';
                    });
                }

                $("#IconGrid").html(imgs);

            } else {
                //alert("查询图标数据异常，请检查并重试！");
            }
        });
    };

    $.fn.zTree.init($("#IconDirectoryTree"), {
        view: {
            selectedMulti: false
        },
        async: {
            enable: true,
            url: "/Handler/IconsHandler.ashx?OPERATION=ICONDATA&rnd=" + (Math.random() * 10),
            autoParam: ["id", "name", "level", "pId"],
            otherParam: { "icon": "icon" },
            dataFilter: function(treeId, parentNode, childNodes) {
                if (!childNodes) return null;
                for (var i = 0, l = childNodes.length; i < l; i++) {
                    childNodes[i].name = childNodes[i].name.replace(/\.n/g, ".");
                }
                return childNodes;
            }
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            onClick: function(event, treeId, node) {
                getIcons(node.id);
            },
            beforeAsync: function(treeId, node) {
                $(this).html("<img src='/Images/loading.gif' id='imgloading'/>");
                return true;
            },
            onAsyncSuccess: function(event, treeId, node, msg) {
                $(this).find("#imgloading").remove();
                var data = $.parseJSON(msg);
                var treeObj = $.fn.zTree.getZTreeObj(this);
                if (treeObj == null) return;
                var nodes = treeObj.getNodes();

                // 将第一个节点选中展开
                if (nodes && nodes.length > 0) {
                    //treeObj.setting.callback.onClick(event, 'IconDirectoryTree', nodes[0]);
                    treeObj.expandNode(nodes[0], true, false, true, true);
                }
                // 将第一个节点设置为选中状态
                var curMenu = treeObj.getNodes()[0];
                treeObj.selectNode(curMenu);
            }
        }
    });

    getIcons("");

    // 给已选目录列表赋值
    if (selectedData && selectedData.length) {
    }
};

/// <summary>
/// 生成Guid
/// </summary>

HSP.Common.Guid = function () {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid.toUpperCase();
};

/// <summary>
/// 对象转换为字符串方法
/// Hitech.Common.ObjToString(o)
/// </summary>
/// <history> Created At 2013.05.01 By Tli </history> 

HSP.Common.ObjToString = function (o) {
    var r = [];
    var otype = "undefined";
    if (typeof o == "string") {
        return "\"" + o.replace(/(['\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
    }
    if (typeof o == "undefined") {
        return otype;
    }
    if (typeof o == "object") {
        if (o === null) return "null";
        else if (!o.sort) {
            for (var i in o) {
                if (o.hasOwnProperty(i)) {
                    r.push('\"' + i + '\":' + HSP.Common.ObjToString(o[i]));
                }
            }
            r = "{" + r.join() + "}";
        } else {
            for (var j = 0; j < o.length; j++) {
                r.push(HSP.Common.ObjToString(o[j]));
            }
            r = "[" + r.join() + "]";
        }
        return r;
    }
    return o.toString();
};

/// <summary>
/// Bootstrap 消息处理
/// obj-消息对象主体
/// msg-消息内容
/// type-消息类型：success,warning,danger,info
/// fade-是否自动关闭
/// </summary>

HSP.Common.Message = function (obj, msg, type, fade) {

    var typeMessage, typeIcon;

    switch (type.toLowerCase()) {
        case "success":
            typeMessage = "成功！";
            typeIcon = "ok-sign";
            break;
        case "warning":
            typeMessage = "警告！";
            typeIcon = "warning-sign";
            break;
        case "danger":
            typeMessage = "错误！";
            typeIcon = "remove-sign";
            break;
        case "info":
            typeMessage = "信息：";
            typeIcon = "info-sign";
            break;
        default:
            typeMessage = "信息：";
            typeIcon = "info-sign";
    }

    $('<div class="alert alert-' + type + ' alert-dismissible" role="alert">\
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">\
                <span aria-hidden="true">&times;</span></button>\
            <span class="glyphicon glyphicon-' + typeIcon + '" aria-hidden="true"></span>\
            <span class="sr-only"></span>\
            <strong>' + typeMessage + '</strong> ' + msg + '\
        </div>').appendTo(obj);

    if (fade) {
        window.setTimeout(function () {
            $('[data-dismiss="alert"]').alert('close');
        }, 3000);
    }
};