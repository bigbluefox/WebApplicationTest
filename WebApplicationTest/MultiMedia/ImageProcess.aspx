<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageProcess.aspx.cs" Inherits="WebApplicationTest.MultiMedia.ImageProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>图片处理</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>
    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>    
    
    <style type="text/css">
                /*html, body {
            overflow-x: hidden;
            overflow-y: auto;
            padding: 0;
            margin: 0;
        }*/

    </style>
    <script type="text/javascript">
        
        $(function() {
            InitImageTb();
        });

        function DuplicateLoad(parameters) {
            alert("重复图片加载");
        }

        function DuplicateDel(parameters) {
            alert("重复图片删除");
        }

        /// <summary>
        /// 图片表初始化
        /// </summary>

        function InitImageTb() {
            $('#imageList').datagrid({
                //url: requestUrl,
                width: 'auto',
                height: 'auto',
                idField: 'ImageId',
                fitColumns: true,
                pagination: false,
                rownumbers: true,
                singleselect: true,
                striped: true,
                nowrap: false,
                columns: [
                    [
                        //{ field: 'Id', title: '编号', width: 30, halign: 'center', align: 'center', height: 26, hidden: true },
                        {
                            field: 'Name',
                            title: '图片名称',
                            halign: 'center',
                            align: 'left',
                            width: 75,
                            formatter: function (value, row, index) {
                                value = value.replace(" ", "&nbsp;");
                                var s = '<a title=' + value + ' href="javascript:void(0);" onclick="ViewImageOnline(\'' + row.ImageId + '\',\'' + row.ImageExt + '\',\'' + row.ImageUrl + '\');">' + value + '</a>';
                                return row.ImageSize > 0.0 ? s : value;
                            }
                        },
                        { field: 'Title', title: '图片标题', halign: 'center', align: 'left', width: 150 },
                        // SELECT Id, Name, Title, Width, Height, Size, Extension
                        // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
                        // FROM ImageAttribute
                        { field: 'Extension', title: '扩展名', halign: 'center', align: 'center', width: 45 },
                        { field: 'Size', title: '大小(B)', halign: 'center', align: 'right', width: 45 },
                        { field: 'Width', title: '图片宽', halign: 'center', align: 'center', width: 45 },
                        { field: 'Height', title: '图片高', halign: 'center', align: 'center', width: 45 },
                        { field: 'MD5', title: 'MD5', halign: 'center', align: 'center', width: 90 },
                        {
                            field: 'Id',
                            title: '操作',
                            halign: 'center',
                            align: 'center',
                            width: 45,
                            hidden: true, 
                            formatter:
                                function (value, row, index) {
                                    //var downloadImg = ' <img alt="图片下载" title="图片下载" src="/Images/Next-16x16.png" onclick="DownloadImage(\'' + row.ImageId + '\', \'' + row.ImageUrl + '\');" />';
                                    //var viewDocImg = ' <img alt="图片编辑" title="图片编辑" src="/Images/edit.jpg" onclick="EditImageOnline(\'' + row.ImageId + '\',\'' + row.ImageExt + '\');" />';
                                    var del = ' <img alt="图片删除" title="图片删除" src="/Images/Remove-16x16.png" onclick="DelImageById(\'' + row.Id + '\');" />';

                                    var s = del;
                                    //s += viewDocImg;
                                    //if (row.Creator == userId) s += delImageImg;
                                    return s;
                                }
                        }
                    ]
                ],
                rowStyler: function (index, row, css) {
                    /*
                    //红色已删除  紫色已下载   删除优先级高于下载
                    if (row.IsDel.toString() == "1") {
                    return 'color:#ff0000;'; //font-weight:bold;粗体
                    } else if (row.DownState == "已下载") {
                    return 'color:#cc0088;'; //background-color:#fff 
                    } else {
                    return 'color:#000000;';
                    }*/
                },
                onLoadSuccess: function (data) {
                    //console.info(data);
                    //var panel = $(this).datagrid('getPanel');
                    //var tr = panel.find('div.datagrid-view .datagrid-view2 .datagrid-body table tbody tr');
                    //tr.each(function() {
                    //    var td = $(this).children('td[field="ImageId"]');
                    //    var cell = td.children('div.datagrid-cell');
                    //    var img = cell.children('img');
                    //    img.each(function() {
                    //        $(this).hover(function() { $(this).addClass("hand"); }, function() { $(this).removeClass("hand"); });
                    //    });
                    //});
                }
            });

            GetImageList();

        }


        /// <summary>
        /// 删除图片
        /// </summary>

        function DelImageById(id, path) {
            //var r = confirm("您确定要删除该图片吗？");
            if (confirm("您确定要删除该图片吗？")) {
                var url = "/Handler/ImageHandler.ashx?OPERATION=DELETE&ID=" + id ;
                $.get(url + "&rnd=" + (Math.random() * 10), function (data) {
                    if (data) {
                        if (data.IsSuccess) {
                            $.messager.alert("操作提示", data.Message, "info");
                            GetImageList();
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
        /// 获取上传图片信息
        /// </summary>
        /// <history> Created At 2014.06.07 By Tli </history> 

        function GetImageList() {
            // 参数
            var arr = { OPERATION: "DUPLICATE", RID: Math.round(Math.random() * 10) };

            // 发送ajax请求
            $.getJSON("/Handler/ImageHandler.ashx", arr)
                //向服务器发出的查询字符串 
                // 对返回的JSON数据进行处理 
                .done(function (data) {
                    //console.info(data);
                    if (data != null && data.length > 0) {
                        $("#imageList").datagrid("loading");
                        $("#imageList").datagrid("loadData", { "total": data.length, "rows": data });
                        $("#imageList").datagrid("loaded");
                    } else {
                        //$.messager.alert('温馨提示', '查询失败！', 'error');
                        $("#imageList").datagrid("loadData", { "total": 0, "rows": [] });
                        $("#imageList").datagrid("loaded");
                    }
                });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto;">
        
        来源：<asp:TextBox ID="txtSourcePath" runat="server" Width="450px"></asp:TextBox>
        <br/><br/>
        目的：<asp:TextBox ID="txtTarget" runat="server" Width="450px"></asp:TextBox> * 要创建的目录
        <br/><br/>
        类型：<asp:RadioButton ID="rbImgType1" GroupName="rbImgType" runat="server" Checked="True" Text="大图" />&nbsp;<asp:RadioButton ID="rbImgType2" GroupName="rbImgType" runat="server" Text="小图" />
        <br/><br/>

        <asp:Button ID="btnImgProcess" runat="server" Text="图片检索" OnClick="btnImgProcess_Click" />
        <asp:Button ID="btnImgTogether" runat="server" Text="图片归拢" OnClick="btnImgTogether_Click" />
        <asp:Button ID="btnDuplicateLoad" runat="server" Text="重复图片加载" OnClientClick="DuplicateLoad();" />
        <asp:Button ID="btnDuplicateDel" runat="server" Text="重复图片删除" OnClientClick="DuplicateDel();" />
        <asp:Button ID="btnCreateDirectory" runat="server" Text="批量创建目录" OnClick="btnCreateDirectory_Click" />
        <br/><br/>
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>

        <div style="width: 100%; padding: 10px 0;">
            <table id="imageList" class="easyui-datagrid" title="图片列表" loadmsg="加载中..."></table>
        </div>        

    </div>
    </form>
</body>
</html>
