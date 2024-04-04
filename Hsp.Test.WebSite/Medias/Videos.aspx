<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Videos.aspx.cs" Inherits="Medias_Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    视频文件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

    <link href="../Bootstrap/v3/css/bootstrap-table.css" rel="stylesheet"/>
    <link href="../Styles/BootStrapBase.css" rel="stylesheet"/>

    <script src="../Bootstrap/v3/js/bootstrap-table.js"></script>
    <script src="../Bootstrap/v3/js/locales/bootstrap-table-zh-CN.js"></script>

    <style type="text/css">
        td span { cursor: pointer; }

        .panel-default > .panel-heading { background-color: #68778e; }

        .heading-title {
            color: #fff;
            font-size: 16px;
            font-weight: bold;
            line-height: 30px;
        }

        .table { table-layout: fixed; }

        tr td {
            -moz-text-overflow: ellipsis; /* for Firefox, mozilla */
            -ms-text-overflow: ellipsis;
            -o-text-overflow: ellipsis;
            overflow: hidden;
            text-align: left;
            text-overflow: ellipsis; /* for IE */
            white-space: nowrap;
        }
    </style>

    <script type="text/javascript">

        var $table = $('#audio-table'),
            $remove = $('#remove'),
            selections = [];

        var pageNumber = 1;
        var pageListUrl = "/Handler/VideoHandler.ashx?OPERATION=PAGELIST";

        $(function() {
            //$("#btnRetrieve").click(function () {
            //    Page("/Medias/Retrieve.aspx");
            //}); // 媒体检索

            //$("#btnArchiving").click(function () {
            //    Page("/Medias/Archiving.aspx");
            //}); // 媒体数据归档

            //$("#btnImages").click(function () {
            //    Page("/Medias/Images.aspx");
            //}); // 图像文件

            //$("#btnVideos").click(function () {
            //    Page("/Medias/Videos.aspx");
            //}); // 视频文件

            //$("#btnVideos").click(function () {
            //    Page("/Medias/Videos.aspx");
            //}); // 视频文件

            //$("#btnBooks").click(function () {
            //    Page("/Medias/Books.aspx");
            //}); // 图书文件

            $("#btnClear").click(function() {
                var url = "/Handler/MediaHandler.ashx?OPERATION=EMPTYING&name=Video";
                $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                    if (data && data.success) {
                        //GetMediaList();
                        $.messager.alert("操作提示", data.Message, "info");
                    } else {
                        $.messager.alert("操作提示", data.Message, "error");
                    }
                });
            }); // 清空媒体库

            initTable();

        });

        function initTable() {
            //先销毁表格  
            $('#mediaTable').bootstrapTable('destroy');

            //初始化表格,动态从服务器加载数据  
            $("#mediaTable").bootstrapTable({
                method: "get", //使用get请求到服务器获取数据  
                url: "/Handler/VideoHandler.ashx?OPERATION=PAGELIST", //获取数据的Servlet地址  
                striped: true, //表格显示条纹  
                pagination: true, //启动分页  
                pageSize: 10, //每页显示的记录数  
                pageNumber: 1, //当前第几页  
                pageList: [5, 10, 15, 20, 25], //记录数可选列表  
                search: false, //是否启用查询  
                showColumns: true, //显示下拉框勾选要显示的列  
                showRefresh: true, //显示刷新按钮  
                sidePagination: "server", //表示服务端请求  
                //设置为undefined可以获取pageNumber，pageSize，searchText，sortName，sortOrder  
                //设置为limit可以获取limit, offset, search, sort, order  
                queryParamsType: "undefined",
                queryParams: function queryParams(params) { //设置查询参数  
                    var param = {
                        pageNumber: params.pageNumber,
                        pageSize: params.pageSize
                        //,orderNum: $("#TypeId").val()
                    };
                    return param;
                },
                onLoadSuccess: function() { //加载成功时执行  
                    //alert("加载成功");
                },
                onLoadError: function(data) { //加载失败时执行  
                    alert("加载数据失败");
                }
            });
        }

        //$(document).ready(function () {
        //    //调用函数，初始化表格  
        //    initTable();

        //    //当点击查询按钮的时候执行  
        //    $("#search").bind("click", initTable);
        //});

        function MediaTable() {}


        function stateFormatter(value, row, index) {

            if (index === 2) {
                return {
                    disabled: true
                };
            }

            if (index === 5) {
                return {
                    disabled: true,
                    checked: true
                };
            }

            return value;
        }


        /// <summary>
        /// 删除视频
        /// </summary>

        function DelVideoById(id) {
            if (confirm("您确定要删除该视频吗？")) {
                var url = "/Handler/VideoHandler.ashx?OPERATION=DELETE&ID=" + id;
                $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                    if (data && data.success) {
                        $.messager.alert("操作提示", data.Message, "info");
                    } else {
                        $.messager.alert("操作提示", data.Message, "error");
                    }
                });
            }
        }

        /// <summary>
        /// 批量删除视频
        /// </summary>

        function DelVideoByIds(ids) {
            if (confirm("您确定要批量删除这些视频吗？")) {
                var url = "/Handler/VideoHandler.ashx?OPERATION=BATCHDELETE&IDs=" + ids;
                $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                    if (data && data.success) {
                        $.messager.alert("操作提示", data.Message, "info");
                    } else {
                        $.messager.alert("操作提示", data.Message, "error");
                    }
                });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">

    <h1 class="page-header">视频文件</h1>
    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">视频文件</li>
    </ol>

    <div id="toolbar">

        <div class="form-inline" role="form">

            <%--
            <div class="form-group">
                <span>Offset: </span>
                <input name="offset" class="form-control w70" type="number" value="0">
            </div>
            <div class="form-group">
                <span>Limit: </span>
                <input name="limit" class="form-control w70" type="number" value="5">
            </div>
            <div class="form-group">
                <input name="search" class="form-control" type="text" placeholder="Search">
            </div>
            <button id="ok" type="submit" class="btn btn-default">OK</button>--%>

            <div class="form-group">
                <input name="search" class="form-control" type="text" placeholder="Search">
            </div>

            <button id="remove" class="btn btn-danger" disabled>
                <i class="glyphicon glyphicon-remove"></i> 删除
            </button>

            <button type="button" id="btnClear" class="btn btn-danger">
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
            </button>

        </div>

    </div>

    <!--
    select Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension
    , ContentType, FullName, DirectoryName, CreationTime, MD5, SHA1 from Medias; -->

    <%--媒体名称 媒体标题 所属目录  扩展名 大小(B)  媒体宽  媒体高  MD5--%>

    <table class="table table-hover" id="mediaTable"
           data-toolbar="#toolbar"
           data-pagination="true"
           data-show-refresh="true"
           data-show-toggle="true"
           data-showColumns="true">

        <%--        <thead>
        <tr>
            <th data-field="Id" data-checkbox="true" data-formatter="stateFormatter"></th>
            <th data-field="Id" data-sortable="true">编号</th>
            <th data-field="Type">分类</th>
            <th data-field="Name">名称</th>
            <th data-field="Title">标题</th>
            <th data-field="DirectoryName">所属目录</th>
            <th data-field="Extension" data-sortable="true">扩展名</th>
            <th data-field="Size" data-sortable="true">大小</th>
            <th data-field="ContentType" data-sortable="true">内容类型</th>
            <th data-field="CreationTime" data-sortable="true">创建时间</th>
            <th data-field="MD5" data-sortable="true">MD5</th>
            <th class="col-xs-2" data-field="action" data-formatter="actionFormatter" data-events="actionEvents">操作</th>
        </tr>
        </thead>
        <tbody>
        </tbody>--%>
    </table>

    <input type="hidden" id="TypeId" value="99"/>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">

    <!-- 进度条模态窗体 //-->
    <div class="modal fade" id="progressModal" tabindex="-1" role="dialog" aria-labelledby="progressModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="progressModalLabel">文件正在检索...</h4>
                </div>
                <div class="modal-body">

                    <div class="progress" style="margin-top: 15px;">
                        <div class="progress-bar progress-bar-info progress-bar-striped  active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                            <span class="sr-only">100% Complete</span>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span> 关闭
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">视频文件名称修改</h4>
                </div>
                <div class="modal-body">

                    <form>
                        <div class="form-group">
                            <label for="txtOldName">原名称</label>
                            <input type="text" class="form-control" id="txtOldName" placeholder="原名称" readonly="readonly">
                        </div>
                        <div class="form-group">
                            <label for="txtTitle">修改为 <span class="required">*</span></label>
                            <input type="text" class="form-control" id="txtTitle" placeholder="修改为" required="required">
                        </div>
                    </form>

                </div>
                <div class="modal-footer">
                    <input type="hidden" id="txtId">
                    <input type="hidden" id="txtExtension">
                    <input type="hidden" id="txtFullName">
                    <input type="hidden" id="txtDirectoryName">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span> 关闭
                    </button>
                    <button type="button" class="btn btn-primary" id="btnSave">
                        <span class="glyphicon glyphicon-floppy-saved" aria-hidden="true"></span> 保存
                    </button>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(function() {
            var index = "";
            $('#myModal').on('shown.bs.modal', function(e) { // 弹出模态窗体事件
                //debugger;

                // 这里的btn就是触发元素，即你点击的删除按钮 $("#txtFullName").val(row.FullName);
                //var btn = $(e.relatedTarget),
                //    idx = btn.data("id");

            });

            $('#myModal').on('hidden.bs.modal', function() { // 关闭模态窗体事件
                $("#txtId").val("");
                $("#txtTitle").val("");
                $("#txtOldName").val("");
                $("#txtExtension").val("");
                $("#txtFullName").val("");
                $("#txtDirectoryName").val("");
            });

            $("#btnSave").unbind("click").bind("click", function() { // 名称数据保存

                var params = {
                    id: $("#txtId").val(),
                    title: escape($("#txtTitle").val()),
                    ext: escape($("#txtExtension").val()),
                    dir: escape($('#txtDirectoryName').val()),
                    fullname: escape($('#txtFullName').val())
                };

                $.ajax({
                    type: "POST",
                    url: "/Handler/VideoHandler.ashx?OPERATION=SAVE&rnd=" + (Math.random() * 10),
                    data: params,
                    success: function(rst) {
                        if (rst && rst.success) {

                            $table.bootstrapTable('refresh', { url: pageListUrl });
                            $table.bootstrapTable('refreshOptions', { pageNumber: pageNumber });

                            $.messager.show({ title: "操作提示", msg: rst.Message, timeout: 3000, showType: "fade" });

                            $('#myModal').modal('hide');
                        } else {
                            $.messager.alert("提示", rst.Message, "error");
                        }
                    }
                });
            });

        });

    </script>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">

    <div class="container" style="margin-top: 20px; text-align: center;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>