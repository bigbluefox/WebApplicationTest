<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Archiving.aspx.cs" Inherits="Medias_Archiving" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    媒体数据归档
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

<link href="/Bootstrap/v3/css/bootstrap-table.css" rel="stylesheet"/>
<link href="/Styles/BootStrapBase.css" rel="stylesheet"/>

<script src="/Bootstrap/v3/js/bootstrap-table.js"></script>
<script src="/Bootstrap/v3/js/locales/bootstrap-table-zh-CN.js"></script>

<script src="/Scripts/Hsp.js"></script>
<script src="/Scripts/Hsp.Common.js"></script>
<script src="/Scripts/Hsp.Formatter.js"></script>

<style type="text/css"></style>

<script type="text/javascript">

    var $table = $('#media-table'),
        $remove = $('#remove'),
        selections = [],
        key = 'Id';

    var pageNumber = 1;
    var pageListUrl = "/Handler/MediaHandler.ashx?OP=PAGELIST";

    $(function() {

        $table = $('#media-table'),
            $remove = $('#remove');

        $("#btnRetrieve").click(function() {
            Page("/Medias/Retrieve.aspx");
        });

        $("#btnArchiving").click(function() {
            var dir = $("#txtMediaDirectory").val();
            var type = $("#selMediaType").val();
            if (type == 0) {
                $.messager.alert("操作提示", "请选择要归档媒体类型!", "info");
                return false;
            }

            $("#progressModalLabel").html("文件正在归档中，请稍候...");
            $('#progressModal').modal('toggle'); // 弹出进度条

            var url = "/Handler/MediaHandler.ashx?OP=ARCHIVING";
            url += "&dir=" + escape(dir) + "&type=" + type;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                $('#progressModal').modal('hide'); // 关闭进度条
                if (data && data.success) {
                    refreshTable();
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });

            //$.ajax({
            //    url: url,
            //    type: 'GET',
            //    data: { rnd: Math.random() },
            //    success: function (rst) {
            //        if (rst.IsSuccess) {
            //            $.messager.alert({ title: "操作提示", msg: rst.Message, showType: "info" });

            //        } else {
            //            $.messager.alert({ title: "操作提示", msg: rst.Message, showType: "error" });
            //        }
            //    }
            //    , complete: function (xhr, errorText, errorType) {

            //        debugger;

            //        var p = "";

            //        alert("请求完成后");
            //    }
            //    , error: function(xhr, errorText, errorType) {
            //        alert("请求错误后");
            //    }
            //    , beforSend: function() {
            //        alert("请求之前");
            //    }
            //});

            //return false;
        }); // 归档

        $("#selMediaType").change(function() {
            var url = "/Handler/MediaHandler.ashx?OP=DEFAULTPATH&type=" + this.value;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data && data.success) {
                    $("#txtMediaDirectory").val(unescape(data.Message));
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        });

        $("#btnSearch").click(function () {
            refreshTable();
        }); // 媒体查询

        initTable();
    });

    // 刷新表格数据
    function refreshTable() {
        $table.bootstrapTable({ url: pageListUrl });
        $table.bootstrapTable('refresh');
    }

    function initTable() {

        //先销毁表格  
        $table.bootstrapTable('destroy');

        $table.bootstrapTable({
            height: getHeight() - 35,
            method: 'get', // 请求方式（*）
            dataType: "json", // 数据类型
            url: pageListUrl, // 请求后台地址,  
            toolbar: '#toolbar', //工具按钮用哪个容器
            striped: true, // 使表格带有条纹 
            uniqueId: key, // 唯一标识
            idField: key, // 标识哪个字段为id主键  
            pagination: true, // 在表格底部显示分页工具栏
            pageSize: 10, // 分页大小
            pageNumber: 1, // 当前页码
            pageList: [10, 20, 50, 100, 200, 500],
            sidePagination: "server", //表格分页的位置 
            //设置为undefined可以获取pageNumber，pageSize，searchText，sortName，sortOrder  
            //设置为limit可以获取limit, offset, search, sort, order  
            queryParamsType: "undefined",
            queryParams: function queryParams(params) { //设置查询参数  
                var param = {
                    pageNumber: params.pageNumber,
                    pageSize: params.pageSize,
                    title: $("input[name='search']").val()
                    //,type: getSearchTypes()
                };
                return param;
            },

            columns: [
                [
                    {
                        field: 'checked',
                        checkbox: true
                    }, {
                        title: '编号',
                        field: 'Id',
                        halign: 'center',
                        align: 'center',
                        width: 75
                    }, {
                        field: 'Name',
                        title: '名称',
                        halign: 'center',
                        align: 'left',
                        formatter: tipsFormatter
                    }, {
                        field: 'Title',
                        title: '标题',
                        halign: 'center',
                        align: 'left',
                        formatter: tipsFormatter
                    }, {
                        field: 'DirectoryName',
                        title: '所属目录',
                        halign: 'center',
                        align: 'left',
                        formatter: tipsFormatter
                    }, {
                        field: 'Size',
                        title: '大小',
                        width: 75,
                        halign: 'center',
                        align: 'right',
                        formatter: sizeFormatter
                    }, {
                        field: 'Extension',
                        title: '类型',
                        width: 75,
                        halign: 'center',
                        align: 'center',
                        formatter: typeFormatter
                    }, {
                        field: 'ContentType',
                        title: '内容类型',
                        halign: 'center',
                        align: 'left'
                    }, {
                        field: 'CreationTime',
                        title: '创建时间',
                        align: 'center',
                        formatter: tipsFormatter
                    }, {
                        field: 'MD5',
                        title: 'MD5',
                        align: 'center',
                        formatter: tipsFormatter
                    }, {
                        title: '操作',
                        width: 75,
                        align: 'center',
                        events: operateEvents,
                        formatter: operateFormatter
                    }
                ]
            ],

            formatLoadingMessage: function() {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function() { //没有匹配的结果  
                return '无符合条件的记录';
            },
            onLoadError: function(data) {
                $('#reportTable').bootstrapTable('removeAll');
            },
            onClickRow: function(row) {
                //window.location.href = ""; // "/qStock/qProInfo/" + row.Id;  
            }

        });

        // sometimes footer render error.

        setTimeout(function() {
            $table.bootstrapTable('resetView');
        }, 200);

        $table.on('check.bs.table uncheck.bs.table ' +
            'check-all.bs.table uncheck-all.bs.table', function() {
                $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);

                // save your data, here just save the current page

                selections = getIdSelections();

                // push or splice the selections if you want to save all data selections

            });

        //$table.on('expand-row.bs.table', function(e, index, row, $detail) {
        //    if (index % 2 == 1) {
        //        $detail.html('Loading from ajax request...');
        //        $.get('LICENSE', function(res) {
        //            $detail.html(res.replace(/\n/g, '<br>'));
        //        });
        //    }
        //});

        //$table.on('all.bs.table', function(e, name, args) {
        //    console.log(name, args);
        //});

        $remove.click(function() {
            var ids = getIdSelections();
            $table.bootstrapTable('remove', {
                field: 'Id',
                values: ids
            });
            $remove.prop('disabled', true);

            DelMediaByIds(ids); // 批量删除
        });

        $(window).resize(function() {
            $table.bootstrapTable('resetView', {
                height: getHeight()
            });
        });
    }

    function getIdSelections() {
        return $.map($table.bootstrapTable('getSelections'), function(row) {
            return row.Id;
        });
    }

    //function responseHandler(res) {
    //    $.each(res.rows, function(i, row) {
    //        row.state = $.inArray(row.id, selections) !== -1;
    //    });
    //    return res;
    //}


    function operateFormatter(value, row, index) {
        return [
            //'<a class="like" href="javascript:void(0)" title="播放">',
            //'<i class="glyphicon glyphicon-play"></i>',
            //'</a>  ',
            //'<a class="edit" href="javascript:void(0)" title="修改文件名称">',
            //'<i class="glyphicon glyphicon-pencil"></i>',
            //'</a>  ',
            '<a class="remove" href="javascript:void(0)" title="删除">',
            '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }

    window.operateEvents = {
        //'click .like': function(e, value, row, index) {
        //    alert('You click like action, row: ' + JSON.stringify(row));
        //},
        //'click .edit': function (e, value, row, index) {
        //    alert('You click edit action, row: ' + JSON.stringify(row));
        //},
        'click .remove': function(e, value, row, index) {
            $table.bootstrapTable('remove', {
                field: 'Id',
                values: [row.Id]
            });
            DelMediaById(row.Id); // 删除行数据
        }
    };

    function getHeight() {
        return $(window).height() - $('h1').outerHeight(true);
    }

    /// <summary>
    /// 删除媒体
    /// </summary>

    function DelMediaById(id) {

        if (confirm("您确定要删除该媒体吗？")) {
            var url = "/Handler/MediaHandler.ashx?OPERATION=DELETE&ID=" + id;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data && data.success) {
                        $.messager.alert("操作提示", data.Message, "info");
                        refreshTable();
                    } else {
                        alert(data.Message);
                    }
            });
        }
    }

    /// <summary>
    /// 批量删除媒体
    /// </summary>

    function DelMediaByIds(ids) {
        if (confirm("您确定要批量删除这些媒体吗？")) {
            $("#progressModalLabel").html("文件正在批量删除中，请稍候...");
            $('#progressModal').modal('toggle'); // 弹出进度条

            var url = "/Handler/MediaHandler.ashx?OPERATION=BATCHDELETE&IDs=" + ids;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                $('#progressModal').modal('hide'); // 关闭进度条
                if (data && data.success) {
                    refreshTable();
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    alert(data.Message);
                }
            });
        }
        return false;
    }

</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
    <h1 class="page-header">媒体数据归档 <small>合并各种媒体入各自库</small></h1>
    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">媒体数据归档</li>
    </ol>

    <form class="form-horizontal">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtMediaDirectory" class="col-sm-3 control-label">默认目录</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="txtMediaDirectory" placeholder="默认媒体目录...">
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="selMediaType" class="col-sm-3 control-label">媒体类型</label>
                    <div class="col-sm-9">
                        <select class="form-control" id="selMediaType">
                            <option value="0">默认媒体</option>
                            <option value="1">图片</option>
                            <option value="2">音频</option>
                            <option value="3">视频</option>
                            <option value="4">图书</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <button type="button" id="btnArchiving" class="btn btn-primary">
                    <span class="glyphicon glyphicon-paperclip" aria-hidden="true"></span> 媒体归档
                </button>
            </div>
        </div>

        <%--        <div class="row">
            <div class="col-md-6">

                <div class="form-group">
                    <label for="txtMediaInfo" class="col-sm-2 control-label">媒体信息</label>
                    <div class="col-sm-10">
                        <input id="txtMediaInfo"class="form-control" type="text" placeholder="媒体信息"/>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtMediaDirectory" class="col-sm-2 control-label">检索类型</label>
                    <div class="col-sm-10">
                        <div class="checkbox">
                            <label for="cbxType1"><input id="cbxType1" name="cbxType" type="checkbox" value="1"/> 重复</label>&nbsp;
                            <label for="cbxType2" style="margin-left: 10px;"><input id="cbxType2" name="cbxType" type="checkbox" value="2"/> 名称</label>&nbsp;
                            <label for="cbxType3" style="margin-left: 10px;"><input id="cbxType3" name="cbxType" type="checkbox" value="3"/> 目录</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="form-group" style="display: none;">
            <div class="col-sm-offset-0 col-sm-12">

                <%--                <button type="button" id="btnClear" class="btn btn-primary">
                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
                </button>--%>
                <%--                <button type="button" id="btnBatchDelete" class="btn btn-danger" onclick="BatchDelete();">
                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 批量删除
                </button>--%>

            </div>
        </div>
    </form>

    <div id="toolbar">

        <div class="form-inline" role="form">

            <div class="form-group">
                <input name="search" class="form-control" type="text" placeholder="媒体标题">
            </div>

            <button type="button" id="btnSearch" class="btn btn-primary">
                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 媒体查询
            </button>

            <button id="remove" class="btn btn-danger" disabled>
                <i class="glyphicon glyphicon-remove"></i> 删除
            </button>

<%--            <button type="button" id="btnClear" class="btn btn-danger">
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
            </button>--%>

            <button type="button" id="btnRetrieve" class="btn btn-primary">
                <span class="glyphicon glyphicon-file" aria-hidden="true"></span> 媒体检索
            </button>

        </div>

    </div>

    <table class="table table-striped" id="media-table"
           data-detail-view="true"
           data-detail-formatter="detailFormatter">
    </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">

    <!-- 进度条模态窗体 //-->
    <div class="modal fade" id="progressModal" tabindex="-1" role="dialog" aria-labelledby="progressModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="progressModalLabel">文件正在操作中...</h4>
                </div>
                <div class="modal-body">

                    <div class="progress" style="margin-top: 15px;">
                        <div class="progress-bar progress-bar-info progress-bar-striped  active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                            <span class="sr-only">100% Complete</span>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <input type="hidden" id="txtRowId">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span>关闭
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
    <div class="container" style="text-align: center;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>
</asp:Content>