<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Retrieve.aspx.cs" Inherits="WebApplicationTest.Media.Retrieve" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title>媒体检索</title>
<link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
<link href="/Scripts/themes/icon.css" rel="stylesheet"/>
<script src="/Scripts/jquery-1.12.4.min.js"></script>
<script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>
<script src="/Scripts/jquery.easyui.min.js"></script>
<script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
<script src="/Scripts/Hsp.js"></script>
<script src="/Scripts/Hsp.Common.js"></script>

<style type="text/css">

    
</style>

<script type="text/javascript">

    $(function() {
        InitMediaTb();
    });

    /// <summary>
    /// 媒体数据加载
    /// </summary>
    function DuplicateLoad() {
        GetMediaList();
    }

    /// <summary>
    /// 媒体批量删除
    /// </summary>
    function BatchDelete() {
        DelMediaByIds();
    }

    /// <summary>
    /// 媒体表初始化
    /// </summary>

    function InitMediaTb() {
        $('#MediaList').datagrid({
            width: 'auto',
            height: 600,
            fit: true,
            fitColumns: true,
            nowrap: true,
            striped: true,
            pagination: false,
            rownumbers: true,
            singleselect: true,
            columns: [
                [
                    //{ field: 'Id', title: '编号', width: 30, halign: 'center', align: 'center', height: 26, hidden: true },
                    { field: 'ck', checkbox: true },
                    {
                        field: 'Name',
                        title: '媒体名称',
                        halign: 'center',
                        align: 'left',
                        width: 75,
                        formatter: function(value, row, index) {
                            value = value.replace(" ", "&nbsp;");
                            var s = '<a title=' + value + ' href="javascript:void(0);" onclick="ViewMediaOnline(\'' + row.MediaId + '\',\'' + row.MediaExt + '\',\'' + row.MediaUrl + '\');">' + value + '</a>';
                            return row.MediaSize > 0.0 ? s : value;
                        }
                    },
                    { field: 'Title', title: '媒体标题', halign: 'center', align: 'left', width: 150 },
                    { field: 'DirectoryName', title: '所属目录', halign: 'center', align: 'left', width: 150 },
                    // SELECT Id, Name, Title, Width, Height, Size, Extension
                    // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
                    // FROM MediaAttribute
                    { field: 'Extension', title: '扩展名', halign: 'center', align: 'center', width: 45 },
                    { field: 'Size', title: '大小(B)', halign: 'center', align: 'right', width: 45 },
                    { field: 'Width', title: '媒体宽', halign: 'center', align: 'center', width: 45 },
                    { field: 'Height', title: '媒体高', halign: 'center', align: 'center', width: 45 },
                    { field: 'MD5', title: 'MD5', halign: 'center', align: 'center', width: 90 },
                    {
                        field: 'Id',
                        title: '操作',
                        halign: 'center',
                        align: 'center',
                        width: 45,
                        hidden: true,
                        formatter:
                            function(value, row, index) {
                                //var downloadMedia = ' <Media alt="媒体下载" title="媒体下载" src="/Medias/Next-16x16.png" onclick="DownloadMedia(\'' + row.MediaId + '\', \'' + row.MediaUrl + '\');" />';
                                //var viewDocMedia = ' <Media alt="媒体编辑" title="媒体编辑" src="/Medias/edit.jpg" onclick="EditMediaOnline(\'' + row.MediaId + '\',\'' + row.MediaExt + '\');" />';
                                var del = ' <Media alt="媒体删除" title="媒体删除" src="/Medias/Remove-16x16.png" onclick="DelMediaById(\'' + row.Id + '\');" />';

                                var s = del;
                                //s += viewDocMedia;
                                //if (row.Creator == userId) s += delMediaMedia;
                                return s;
                            }
                    }
                ]
            ],
            rowStyler: function(index, row, css) {
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
            onLoadSuccess: function(data) {
                //console.info(data);
                //var panel = $(this).datagrid('getPanel');
                //var tr = panel.find('div.datagrid-view .datagrid-view2 .datagrid-body table tbody tr');
                //tr.each(function() {
                //    var td = $(this).children('td[field="MediaId"]');
                //    var cell = td.children('div.datagrid-cell');
                //    var Media = cell.children('Media');
                //    Media.each(function() {
                //        $(this).hover(function() { $(this).addClass("hand"); }, function() { $(this).removeClass("hand"); });
                //    });
                //});
            }
        });

        GetMediaList();

    }


    /// <summary>
    /// 删除媒体
    /// </summary>

    function DelMediaById(id, path) {
        //var r = confirm("您确定要删除该媒体吗？");
        if (confirm("您确定要删除该媒体吗？")) {
            var url = "/Handler/MediaHandler.ashx?OPERATION=DELETE&ID=" + id;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data) {
                    if (data.IsSuccess) {
                        $.messager.alert("操作提示", data.Message, "info");
                        GetMediaList();
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
    /// 批量删除媒体
    /// </summary>

    function DelMediaByIds() {
        var ids = "";
        var selecteds = $('#MediaList').datagrid('getSelections');
        if (selecteds.length == 0) {
            alert('请选择要删除的媒体！');
            return false;
        }

        for (var i = 0; i < selecteds.length; i++) {
            ids += selecteds[i].Id + ",";
        }

        if (confirm("您确定要批量删除这些媒体吗？")) {
            var url = "/Handler/MediaHandler.ashx?OPERATION=BATCHDELETE&IDs=" + ids;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data) {
                    if (data.IsSuccess) {
                        GetMediaList();
                        $.messager.alert("操作提示", data.Message, "info");

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
    /// 获取媒体信息
    /// </summary>
    /// <history> Created At 2014.06.07 By Tli </history> 

    function GetMediaList() {

        var title = $("#txtMediaInfo").val();
        var chkValue = [];
        $('input[name="cbxType"]:checked').each(function() {
            chkValue.push($(this).val());
        });

        // 参数
        var arr = { OPERATION: "LIST", Title: title, Type: chkValue, RID: Math.round(Math.random() * 10) };

        // 发送ajax请求
        $.getJSON("/Handler/MediaHandler.ashx", arr)
            //向服务器发出的查询字符串 
            // 对返回的JSON数据进行处理 
            .done(function(data) {
                //console.info(data);
                if (data != null && data.length > 0) {
                    $("#MediaList").datagrid("loading");
                    $("#MediaList").datagrid("loadData", { "total": data.length, "rows": data });
                    $("#MediaList").datagrid("loaded");
                } else {
                    //$.messager.alert('温馨提示', '查询失败！', 'error');
                    $("#MediaList").datagrid("loadData", { "total": 0, "rows": [] });
                    $("#MediaList").datagrid("loaded");
                }
            });
    }

</script>
</head>
<body>
<form id="form1" runat="server">

    <div style="margin: 0 auto;">

        来源：<asp:TextBox ID="txtSourcePath" runat="server" Width="230px"></asp:TextBox>&nbsp;
        目的：<asp:TextBox ID="txtTargetPath" runat="server" Width="230px"></asp:TextBox>
        <br/><br/>
        类型：<asp:DropDownList ID="ddlMediaType" runat="server">
            <asp:ListItem Selected="True" Value="0">默认媒体</asp:ListItem>
            <asp:ListItem Value="1">图片</asp:ListItem>
            <asp:ListItem Value="2">音频</asp:ListItem>
            <asp:ListItem Value="3">视频</asp:ListItem>
            <asp:ListItem Value="4">阅读</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        哈希：<asp:CheckBox ID="cbxMd5" runat="server" Text="MD5"/>&nbsp;
        <asp:CheckBox ID="cbxSha1" runat="server" Text="SHA1"/>
        <br/><br/>

        <asp:Button ID="btnMediaProcess" runat="server" Text="媒体检索" OnClick="btnMediaProcess_Click"/>
        <asp:Button ID="btnDirectoryProcess" runat="server" Text="目录检索" OnClick="btnDirectoryProcess_Click" ToolTip="检索媒体文件目录"/>
        <asp:Button ID="btnMediaTogether" runat="server" Text="媒体归拢" OnClick="btnMediaTogether_Click"/>

        <asp:Button ID="btnCreateDirectory" runat="server" Text="批量创建目录"/>
        <br/><br/><hr/><br/>

        <asp:TextBox ID="txtSource" runat="server" Width="230px"></asp:TextBox>
        &nbsp;替换为&nbsp;
        <asp:TextBox ID="txtTarget" runat="server" Width="230px"></asp:TextBox>
        <asp:CheckBox ID="cbxRegular" runat="server" Text="正则替换"/>&nbsp;，如【*】，使用*隔开前后字符
        <br/><br/>

        <asp:Button ID="btnFileRename" runat="server" Text="文件名中部分字符替换" OnClick="btnFileRename_Click"/>
        <asp:Button ID="btnDirectoryRename" runat="server" Text="目录名中部分字符替换" OnClick="btnDirectoryRename_Click"/>
        <asp:Button ID="btnRenameCheck" runat="server" Text="正则替换检测" OnClick="btnRenameCheck_Click"/>
        <br/><br/><hr/><br/>

        <input id="txtMediaInfo" type="text" placeholder="媒体信息"/>&nbsp;
        <input id="cbxType1" name="cbxType" type="checkbox" value="1"/><label for="cbxType1">重复</label>&nbsp;
        <input id="cbxType2" name="cbxType" type="checkbox" value="2"/><label for="cbxType2">名称</label>&nbsp;
        <input id="cbxType3" name="cbxType" type="checkbox" value="3"/><label for="cbxType3">目录</label>
        <br/><br/>

        <input id="btnDuplicateLoad" type="button" value="媒体加载" onclick="DuplicateLoad();"/>
        <input id="btnBatchDelete" type="button" value="批量删除" onclick="BatchDelete();"/>
        <%--<asp:Button ID="btnDuplicateLoad" runat="server" Text="媒体加载" OnClientClick="DuplicateLoad();" />--%>
        <%--<asp:Button ID="btnBatchDelete" runat="server" Text="批量删除" />--%>
        <br/><br/>

        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        <br/><br/>

        <div style="padding: 10px 0; width: 100%;">
            <table id="MediaList" class="easyui-datagrid" title="媒体列表" loadmsg="加载中..."></table>
        </div>

    </div>

</form>

</body>
</html>