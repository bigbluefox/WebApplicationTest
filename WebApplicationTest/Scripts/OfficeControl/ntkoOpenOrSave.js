//读取odbc源文档
function ntkoOpenFromODBCds(documentID) {
    try {
        TANGER_OCX_OBJ = document.all.item("TANGER_OCX"); //;初始化控件对象
        TANGER_OCX_OBJ.OpenFromODBCds(
        "myodbcsql",
        "select FileStream From dbo.Document where documentID = " + documentID,
        "sa",
        "sql2008"
        );
    } catch (err) {
        alert(alert("错误：" + err.number + ":" + err.description));
    }
}

//保存odbc源文档
function ntkoSaveFromODBCds(documentID) {
    try {
        TANGER_OCX_OBJ.SaveToODBCds(
            "myodbcsql",
            "update Document set FileStream = ? where DocumentID = " + documentID,
            "sa",
            "sql2008"
        );
        alert("保存成功！");
    } catch (err) {
        alert(alert("错误：" + err.number + ":" + err.description));
    }
}

//读取odbc源文档
function ntkoOpenActivityMemoFromODBCds(actitityID) {
    try {
        TANGER_OCX_OBJ = document.all.item("TANGER_OCX"); //;初始化控件对象
        TANGER_OCX_OBJ.OpenFromODBCds(
        "MSD-TJDC",
        "select Description From Activity where ID = " + actitityID,
        "sa",
        "sa@123"
        );
    } catch (err) {
        alert(alert("错误：" + err.number + ":" + err.description));
    }
}

//保存odbc源文档
function ntkoSaveActivityMemoFromODBCds(actitityID) {
    try {
        TANGER_OCX_OBJ.SaveToODBCds(
            "MSD-TJDC",
            "update Activity set Description = ? where ID = " + actitityID,
            "sa",
            "sa@123"
        );
        alert("保存成功！");
    } catch (err) {
        alert(alert("错误：" + err.number + ":" + err.description));
    }
}

//页面关闭触发事件
function onPageClosetodbc(IsSave) {
    if (IsFileOpened) {
        $.ajax({
            type: "GET",
            url: "DeleToFile.aspx"
        });
        if (!TANGER_OCX_OBJ.ActiveDocument.Saved) {
            if (IsSave) {
                if (confirm("文档修改过,还没有保存,是否需要保存?")) {
                    // ntkoSaveFromODBCds(documentID);
                    MysaveFileToUrl();
                }
            }
        }
    }
}

//打开网络地址文件
function ntkoOpenFromURL(Url, IsReadOnly) {
    try {
        TANGER_OCX_OBJ = document.getElementById("TANGER_OCX"); //;初始化控件对象
        //TANGER_OCX_OBJ = document.all.item("TANGER_OCX"); //;初始化控件对象
        TANGER_OCX_OBJ.OpenFromURL(
            Url,
            IsReadOnly
            );

    } catch (err) {
        if (err.number == -2146823281) {
            alert("错误:未安装WebOffice在线编辑插件(详见《Processist产品用户使用手册》客户端安装章)");
        } else {
            alert("错误：" + err.number + ":" + err.description);
        }
    }
}

//保存文件到网络
function MysaveFileToUrl(url) {
    var savecode = "DocToSave.aspx";
    if (url) {
        savecode = url;
    }
    var result, filedot;
    if (IsFileOpened) {

        switch (TANGER_OCX_OBJ.doctype) {
            case 1:
                fileType = "Word.Document";
                filedot = ".doc";
                break;
            case 2:
                fileType = "Excel.Sheet";
                filedot = ".xls";
                break;
            case 3:
                fileType = "PowerPoint.Show";
                filedot = ".ppt";
                break;
            case 4:
                fileType = "Visio.Drawing";
                filedot = ".vso";
                break;
            case 5:
                fileType = "MSProject.Project";
                filedot = ".pro";
                break;
            case 6:
                fileType = "WPS Doc";
                filedot = ".wps";
                break;
            case 7:
                fileType = "Kingsoft Sheet";
                filedot = ".et";
                break;
            default:
                fileType = "unkownfiletype";
                filedot = ".doc";
        }
        retHTML = TANGER_OCX_OBJ.saveToURL(savecode, //提交到的url地址
        "EDITFILE", //文件域的id，类似<input type=file id=upLoadFile 中的id
		"savetype=1&fileType=" + fileType,          //与控件一起提交的参数,savetype参数为要保存的文件格式office,html,pdf。filetype参数保存文件类型
		"",    //上传文件的名称，类似<input type=file 的value
		0  //与控件一起提交的表单id，也可以是form的序列号，这里应该是0.
        );

        if (TANGER_OCX_OBJ.StatusCode == 0) {
            SaveModify();
            alert("文件保存成功");
            TANGER_OCX_OBJ.ActiveDocument.Saved = true;
        }
        else {
            alert("文件保存失败");
        }
    }
}

//设置当前office用户名
function TANGER_OCX_SetDocUser(cuser) {
    with (TANGER_OCX_OBJ.ActiveDocument.Application) {
        UserName = cuser;
    }
}

//保存文件修改
function SaveModify() {
    var str_Content = escape(New_Document());
    var str_Params = { DocumentID: $("#hidDocumentID").val(), Document: str_Content };
    $.ajax({
        type: "POST",
        url: "/DocumentMgmt/TermDetail/SaveDocument/",
        data: $.param(str_Params)
    });
}

//拼装文件字符串
function New_Document() {
    var count = 0;
    var ConstantGroup = "12E0C46F-D93F-4C13-A811-E70D0907C98E";      //组分割符
    var ConstantFestival = "65F636FB-F772-42D6-B968-F2929117C975";   //节分割符
    var ContentString = "";
    if (TANGER_OCX_OBJ.ActiveDocument.Revisions != undefined) {
        count = TANGER_OCX_OBJ.ActiveDocument.Revisions.Count; //获取修订的数目
    }
    for (var i = 1; i <= count; i++) {
        var type = '';
        if (1 == TANGER_OCX_OBJ.ActiveDocument.Revisions(i).TYPE)     //修改类型
            type = "插入修订";
        else
            type = "删除修订";
        var strUSAdate = TANGER_OCX_OBJ.ActiveDocument.Revisions(i).Date;
        var datetemp = new Date(strUSAdate);
        var date = datetemp.getFullYear() + "-" + (datetemp.getMonth() + 1) + "-" + datetemp.getDate() + " " + datetemp.getHours() + ":" + datetemp.getMinutes() + ":" + datetemp.getSeconds();

        ContentString += TANGER_OCX_OBJ.ActiveDocument.Revisions(i).Author + ConstantFestival +    //修改人
                                 type + ConstantFestival + //修改类型
                                 date + ConstantFestival + //修改时间
                                 TANGER_OCX_OBJ.ActiveDocument.Revisions(i).Range + ConstantGroup;  //修改内容
    }
    return ContentString;
}