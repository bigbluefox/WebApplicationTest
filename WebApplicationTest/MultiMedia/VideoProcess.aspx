<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoProcess.aspx.cs" Inherits="WebApplicationTest.MultiMedia.VideoProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>视频整理</title>
    <link href="/Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="/Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet"/>
    <script src="/Scripts/jquery/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery/jquery-migrate-1.4.1.min.js"></script>

    <!--[if lt IE 9]>
        <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <script src="/Scripts/jquery.easyui.min.js"></script>
    <script src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <style type="text/css">
        body { padding: 5px; }

        div label {
            padding-left: 2px;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        $(function() {
            //InitListTb();
        });

        /// <summary>
        /// 新闻统计表初始化
        /// </summary>

        function InitListTb() {
            $('#videoList').datagrid({
                width: 'auto',
                height: 'auto',
                fit: true,
                fitColumns: true,
                pagination: true,
                pageSize: 15,
                pageList: [100, 50, 30, 15],
                rownumbers: true,
                singleselect: true,
                striped: true,
                nowrap: true,
                columns: [
                    [
                        { field: 'FileName', title: '名称', halign: 'center', align: 'left', width: 120 },
                        { field: 'Result', title: '结果', halign: 'center', align: 'left', width: 120 },
                        { field: 'FileExt', title: '扩展名', halign: 'center', align: 'center', width: 180 },
                        { field: 'FileSize', title: '大小', halign: 'center', align: 'right', width: 60 },
                        { field: 'ContentType', title: '内容类型', halign: 'center', align: 'left', width: 90 }
                    ]
                ]
            });
        }

    </script>
</head>
<body>
<div class="container">
    <form id="form1" runat="server">
        <div style="margin: 0 auto;">
            来源目录：<asp:TextBox ID="txtSourcePath" runat="server" Width="450px"></asp:TextBox>
            <br/><br/>
            哈希处理：<asp:CheckBox ID="cbxMd5" runat="server" Text=" MD5"/>&nbsp;&nbsp;<asp:CheckBox ID="cbxSha1" runat="server" Text=" SHA1"/>
            <br/><br/>
            名称替换：<asp:TextBox ID="txtSourceName" runat="server" Width="240px"></asp:TextBox>
            &nbsp;替换为&nbsp;
            <asp:TextBox ID="txtTargetName" runat="server" Width="240px"></asp:TextBox>
            <br/><br/>

            匹配规则：<asp:TextBox ID="txtExpression" runat="server" Width="240px" ToolTip="匹配规则"></asp:TextBox>
            &nbsp;目的：<asp:TextBox ID="txtTarget" runat="server" Width="240px" ToolTip="匹配目的"></asp:TextBox><br/><br/>

            <asp:Button ID="btnVideoProcess" runat="server" Text="视频检索" OnClick="btnVideoProcess_Click"/>
            <asp:Button ID="btnCheck" runat="server" Text="替换检查" OnClick="btnCheck_Click"/>
            <asp:Button ID="btnRename" runat="server" Text="视频名称部分字符替换" OnClick="btnRename_Click"/>
            <br/><br/>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            <br/>

            <%--<div id="videoList"></div>--%>

        </div>
    </form>

    <div class="row">

        <style type="text/css">
            th { text-align: center; }
        </style>

        <table class="table table-striped" id="customerTable">
            <thead>
            <tr>
                <th>名称</th>
                <th>结果</th>
                <th>扩展名</th>
                <th>大小</th>
                <th>内容类型</th>
            </tr>
            </thead>
            <tbody>

            <%--SELECT TOP (200) FileId, FileName, FullName, DirectoryName, FileExt, FileSize
            , ContentType, StandClass, StandType, A100, StandPreNo, A107, A225, A825, A301, MD5, SHA1
            FROM Standard_Local --%>

            <% if (List != null && List.Count > 0)
               {
                   foreach (var m in List)
                   { %>
                    <tr>
                        <td><% = m.FileName %></td>
                        <td><% = m.A301 %></td>
                        <td style="text-align: center;"><% = m.FileExt %></td>
                        <td style="text-align: right;"><% = (m.FileSize / 1024 /1024).ToString("N0") %></td>
                        <td><% = m.ContentType %></td>
                    </tr>
            <% }
               } %>

            </tbody>
        </table>

    </div>
</div>
</body>
</html>