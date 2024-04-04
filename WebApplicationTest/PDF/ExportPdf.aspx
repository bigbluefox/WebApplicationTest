<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportPdf.aspx.cs" Inherits="WebApplicationTest.PDF.ExportPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>主要实现添加水印及生成PDF时加上安全设置</title>

    <script src="../Scripts/jquery/jquery-1.12.4.js"></script>

    <script type="text/javascript">

        var url = "PdfShow/QSDYY%20201.001-2018%20上海上电电力运营有限公司规划管理标准.pdf";
        //url = "E:/职业健康安全管理体系要求.pdf";

        function PrintPdf() {

            //printURL(url);
            //return;

            if (document.getElementById("iPrint").attachEvent) {
                document.getElementById("iPrint").attachEvent("onload", function() {
                    document.getElementById("iPrint").focus();
                    document.getElementById("iPrint").contentWindow.print();
                });

            } else {
                document.getElementById("iPrint").onload = function() {
                    document.getElementById("iPrint").focus();
                    document.getElementById("iPrint").contentWindow.print();
                };
            }
            //document.getElementById("iPrint").src = "./invSa.aspx?action=toPdf";
            document.getElementById("iPrint").src = url;
            //<a href="PdfShow/QSDYY%20201.001-2018%20上海上电电力运营有限公司规划管理标准.pdf">PdfShow/QSDYY 201.001-2018 上海上电电力运营有限公司规划管理标准.pdf</a>
        }

        function printURL(url) {
            if (window.print && window.frames && window.frames['printerIframe']) {
                var html = '';
                html = '<html><head>';
                html += '<object id=factory viewastext style="display:none" classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814" codebase="smsx.cab#Version=6,4,438,06"></object>';
                html += '<script language="javascript">';
                html += 'function set_print(){';
                html += 'factory.printing.header = "";';
                html += 'factory.printing.footer = "";';
                html += 'factory.printing.portrait = 1;';
                html += 'factory.printing.leftMargin = 13;';
                html += 'factory.printing.topMargin = 20;';
                html += 'factory.DoPrint(false);';
                html += '}<\/script>';
                html += '</head><body onload="parent.printFrame(window.frames[\'urlToPrint\']);">';
                html += '<iframe name="urlToPrint" width="100%" height="100%" src="' + url + '" frameborder="no" border="0" marginwidth="0″ marginheight="0" scrolling="no" allowtransparency="yes"><\/iframe>';
                html += '<\/body><\/html>';
                var ifd = window.frames['printerIframe'].document;
                ifd.open();
                ifd.write(html);
                ifd.close();
            }
        }

        function printFrame(frame) {
            if (frame.print) {
                frame.focus();
                //window.print();调用页面打印  
                window.frames['printerIframe'].set_print(); //使用页面的打印  
            }
        }


        $(document).ready(function() {

            $("#printIframe").load(function() { //等待iframe加载完成后再执行doPrint.每次iframe设置src之后都会重新执行这部分代码。
                doPrint();
            });

        });

        //点击打印按钮，触发事件】
        function printPDF() {
            var src = $("#printIframe").attr("src");
            if (!src) { //当src为空，即第一次加载时才赋值，如果是需要动态生成的话，那么条件要稍稍变化一下
                $("#printIframe").attr("src", url); //暂时静态PDF文件
            } else
                doPrint();
        }

        function doPrint() {
            $("#printIframe")[0].contentWindow.print();
        }

        /**
        * PDF文件直接打印 需安装adobe reader,并在浏览器加载项中启用Adobe加载项adobe PDF Reader;
        * 可以打印带有汉字名称的PDF文件
        */

        function directpdfprint(srcFile) {
            //debugger;
            if (srcFile.length == 0) srcFile = url;
            //debugger;
            var pdfprint = document.getElementById("createPDF");
            if (pdfprint != undefined) {
                var parentNode = pdfprint.parentNode;
                parentNode.removeChild(pdfprint);
            }
            var pdfprintdiv = document.getElementById("createPDFDIV");

            var p = document.createElement("object");
            try {
                p.id = "createPDF";
                p.classid = "CLSID:CA8A9780-280D-11CF-A24D-444553540000";
                p.width = 1;
                p.height = 1;
                p.src = srcFile; // encodeURI(encodeURI(srcFile)); // 处理中文名称
                pdfprintdiv.appendChild(p);
                p.printWithDialog(); // 带打印窗口的直接打印
                // p.printAll();//直接打印
            } catch (e) {
                // alert(e);
                $.messager.alert("提示", '请确保已安装Adobe，并开启Adobe加载项！' + e, "warning");
            }
        }

    </script>


    <script type="text/javascript">
        function printdiv() {
            var newstr = document.getElementById("PrintContentDiv").innerHTML; //获得需要打印的内容
            // alert(newstr);
            var oldstr = document.body.innerHTML; //保存原先网页的代码
            document.body.innerHTML = newstr; //将网页内容更改成需要打印
            window.print();
            document.body.innerHTML = oldstr; //将网页还原
            return false;
        }

        //打印页面预览
        function printpreview() {
            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.getElementById("divButton").style.display = "none"; //隐藏打印及其打印预览页面
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(7, 1); //打印预览
        }

    </script>
</head>
<body>
<form id="form1" runat="server" style="padding-bottom: 5px;">
    <div>
        <asp:Button ID="btnExportPDF" runat="server" Text="生成PDF文件" OnClick="btnExportPDF_Click"/>&nbsp;
        <input type="button" value="打印PDF文件" onclick="PrintPdf()"/>&nbsp;
        <input type="button" value="toPrinter" onclick="directpdfprint('')"/>
    </div>
</form>

<div id="createPDFDIV" style="height: 0px; margin: 0 auto; text-align: center; width: 100%"></div>

<%--//创建一个空的iframe，因为如果每次请求都生成PDF，那么是不必要的。--%>
<iframe style="display: none; height: 600px; width: 100%;" id="printIframe"></iframe>
<iframe id="iPrint" style="height: 553px; width: 100%;"></iframe>
    
    ABC_NoSeal.pdf
    ABC_Sealed.pdf
    ABC_Sealed_Noprint.pdf;

</body>
</html>