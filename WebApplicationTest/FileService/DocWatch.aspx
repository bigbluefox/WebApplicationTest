<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocWatch.aspx.cs" Inherits="WebApplicationTest.DocWatch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="IE=7" http-equiv="X-UA-Compatible"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>在线文件查看</title>
    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script src="/Scripts/OfficeControl/hitechwo.js"></script>
    <script src="/Scripts/OfficeControl/ntkoOpenOrSave.js"></script>

    <style type="text/css">
        body, html { height: 100% }
    </style>

    <script type="text/javascript">

        function ntkoOpenFromDocumentURL() {

            try {
                ntkoOpenFromURL("<%= FromURL %>", "false");
                if ('<%= key %>' != "") {
                    TANGER_OCX_OBJ.ActiveDocument.Application.Selection.Find.Execute('<%= key %>');
                }
            } catch (e) {
            }
        }

        $(function() {
            // 设置WebOffice 的高度  zq
            var show = document.getElementById("officecontrol");
            show.style.height = document.body.clientHeight - 20 + "px";
        });

    </script>
</head>
<body onload="ntkoOpenFromDocumentURL();" onunload="onPageClosetodbc(false);">
<form id="form1" runat="server" action="DocToSave.aspx" enctype="multipart/form-data">
    <div style="height: 100%">
        <div id="officecontrol" style="text-align: left;">
            <!--引用NTKO OFFICE文档控件-->
            <script src="/Scripts/officecontrol/ntkoofficecontrol.js" type="text/javascript"></script>
            <!--控件事件代码开始-->
            <script type="text/javascript" for="TANGER_OCX" event="OnFileCommand(cmd, canceled);">
                alert(cmd);
                CancelLastCommand = true;
            </script>
            <script type="text/javascript" for="TANGER_OCX" event="OnDocumentClosed();">
                setFileOpenedOrClosed(false);
            </script>
            <script type="text/javascript" for="TANGER_OCX" event="OnDocumentOpened(TANGER_OCX_str, TANGER_OCX_obj);">
                //saved属性用来判断文档是否被修改过,文档打开的时候设置成ture,当文档被修改,自动被设置为false,该属性由office提供.	                
                TANGER_OCX_OBJ.ActiveDocument.Saved = true;
                if (2 == TANGER_OCX_OBJ.DocType) {
                    try {
                        TANGER_OCX_OBJ.ActiveDocument.Application.ActiveWorkbook.Saved = true;
                    } catch (e) {
                        alert("错误：" + err.number + ":" + err.description);
                    }
                }
                setFileOpenedOrClosed(true); //设置文档状态值
                controlStyle(); //插入控件样式自定义菜单
                //SetReviewMode(false);//设置文档在痕迹模式下修改
                //setShowRevisions(true); //设置是否显示痕迹
                //setFileNew(false);//设置是否允许新建
                //setFileOpen(false);//设置是否允许打开

                // 设置文档是否允许复制
                setFileSaveAs(true);

                //IsSetReadOnly(true,"");
            </script>
            <script type="text/javascript" for="TANGER_OCX" event="BeforeOriginalMenuCommand(TANGER_OCX_str, TANGER_OCX_obj);">
                alert("BeforeOriginalMenuCommand事件被触发");
            </script>
            <script type="text/javascript" for="TANGER_OCX" event="OnFileCommand(TANGER_OCX_str, TANGER_OCX_obj);">
                if (TANGER_OCX_str == 3) {
                    alert("当前是浏览状态，您不能执行保存操作！");
                    CancelLastCommand = true;
                }
            </script>
            <script for="TANGER_OCX" event="OnCustomMenuCmd2(menuPos,submenuPos, subsubmenuPos, menuCaption, menuID)">
                alert("第" + menuPos + "," + submenuPos + "," + subsubmenuPos + "个菜单项,menuID=" + menuID + ",菜单标题为\"" + menuCaption + "\"的命令被执行.");
                if ("全网页查看" == menuCaption) objside();
                if ("恢复原大小" == menuCaption) objside2();
            </script>
            <script type="text/javascript" for="TANGER_OCX" event="AfterPublishAsPDFToURL(result, code);">
                result = trim(result);
                alert(result);
                if (result == "succeed") {
                    window.close();
                }
            </script>
            <!--控件事件代码结束-->
        </div>
    </div>
</form>
</body>
</html>