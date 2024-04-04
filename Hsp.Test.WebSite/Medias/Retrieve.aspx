<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Retrieve.aspx.cs" Inherits="Medias_Retrieve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    媒体检索
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

        // 检索媒体
        $("#btnRetrieve").click(function() {

            var dir = $("#txtMediaDirectory").val();
            var type = $("#selMediaType").val();
            var limit = $("#txtFileSizeLimit").val();
            var val = 0;
            if ($("#cbxMd5").attr("checked")) val += 1;
            if ($("#cbxSha1").attr("checked")) val += 2;

            if (dir.length == 0) {
                $.messager.alert("操作提示", "请输入要检索的媒体目录!", "info");
                return false;
            }

            $("#progressModalLabel").html("文件正在检索中，请稍候...");
            $('#progressModal').modal('toggle'); // 弹出进度条

            var url = "/Handler/MediaHandler.ashx?OPERATION=RETRIEVAL";
            url += "&dir=" + escape(dir) + "&type=" + type + "&value=" + val;
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                $('#progressModal').modal('hide'); // 关闭进度条
                if (data && data.success) {
                    refreshTable();
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }); // 文件检索

        $("#btnDirectory").click(function() {

            var dir = $("#txtMediaDirectory").val();
            if (dir.length == 0) {
                $.messager.alert("操作提示", "请输入要检索的媒体目录!", "info");
                return false;
            }

            $("#progressModalLabel").html("目录正在检索中，请稍候...");
            $('#progressModal').modal('toggle'); // 弹出进度条

            var url = "/Handler/MediaHandler.ashx?OPERATION=DIRECTORY";
            url += "&dir=" + escape(dir);
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                $('#progressModal').modal('hide'); // 关闭进度条
                if (data && data.success) {
                    refreshTable();
                    $.messager.alert("操作提示", unescape(data.Message), "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }); // 目录检索

        $("#btnClear").click(function() {
            var url = "/Handler/MediaHandler.ashx?OPERATION=EMPTYING&name=Medias";
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data && data.success) {
                    refreshTable();
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }); // 清空媒体库

        $("#btnCount").click(function() {
            var url = "/Handler/MediaHandler.ashx?OPERATION=COUNT";
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data && data.success) {
                    $.messager.alert("操作提示", data.Count, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }); // 检索媒体数量

        $("#btnArchiving").click(function() {
            Page("/Medias/Archiving.aspx");
        }); // 媒体数据归档

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
                    title: $("input[name='search']").val(),
                    type: getSearchTypes()
                };
                return param;
            },

            columns: [
                [
                    {
                        field: 'checked',
                        checkbox: true
                    }, {
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

                        //,events: operateEvents,
                        //formatter: operateFormatter
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
                //alert(row.Id);
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
        //    //if (index % 2 == 1) {
        //    //    $detail.html('Loading from ajax request...');
        //    //    $.get('LICENSE', function (res) {
        //    //        $detail.html(res.replace(/\n/g, '<br>'));
        //    //    });
        //    //}
        //});

        //$table.on('all.bs.table', function(e, name, args) {
        //    console.log(name, args);
        //});

        $remove.click(function() { // 批量删除
            var ids = getIdSelections();
            $table.bootstrapTable('remove', {
                field: key,
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

    // 检索类型值
    function getSearchTypes() {
        var val = "";
        $.each($("input[name='cbxType']"), function() {
            if (this.checked == true) {
                val += this.value + ",";
            }
        });
        return val;
    }

    //function responseHandler(res) {
    //    $.each(res.rows, function (i, row) {
    //        row.state = $.inArray(row.id, selections) !== -1;
    //    });
    //    return res;
    //}

    function getHeight() {
        return $(window).height() - $('h1').outerHeight(true);
    }

    function operateFormatter(value, row, index) {
        return [
            //'<a class="like" href="javascript:void(0)" title="播放">',
            //'<i class="glyphicon glyphicon-play"></i>',
            //'</a>  ',
            '<a class="edit" href="javascript:void(0)" title="修改文件名称">',
            '<i class="glyphicon glyphicon-edit"></i>',
            '</a>  ',
            '<a class="remove" href="javascript:void(0)" title="删除">',
            '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }

    window.operateEvents = {
        //'click .like': function(e, value, row, index) {
        //    alert('You click like action, row: ' + JSON.stringify(row));
        //},
        'click .edit': function(e, value, row, index) {
            alert('You click edit action, row: ' + JSON.stringify(row));

            //$("#txtId").val(row.Id);
            //$("#txtTitle").val(row.Title);
            //$("#txtOldName").val(row.Title);
            //$("#txtExtension").val(row.Extension);
            //$("#txtFullName").val(row.FullName);
            //$("#txtDirectoryName").val(row.DirectoryName);

            //$('#myModal').modal('toggle'); // 弹出名称修改

        },
        'click .remove': function(e, value, row, index) {
            $table.bootstrapTable('remove', {
                field: key,
                values: [row.Id]
            });

            DelMediaById(row.Id); // 删除行数据，考虑要将上述表格响应纳入到删除操作中
        }
    };

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
                    { field: 'ck', checkbox: true },
                    { field: 'Type', title: '类型', halign: 'center', align: 'center', width: 30 },
                    {
                        field: 'Name',
                        title: '媒体名称',
                        halign: 'center',
                        align: 'left',
                        width: 90,
                        formatter: function(value, row, index) {
                            value = value.replace(" ", "&nbsp;");
                            return value;
                        }
                    },
                    { field: 'Title', title: '媒体标题', halign: 'center', align: 'left', width: 150 },
                    { field: 'DirectoryName', title: '所属目录', halign: 'center', align: 'left', width: 150 },
                    // SELECT Id, Name, Title, Width, Height, Size, Extension
                    // , ContentType, FullName, DirectoryName, MD5, SHA1, CreationTime
                    // FROM MediaAttribute
                    { field: 'Extension', title: '扩展名', halign: 'center', align: 'center', width: 45 },
                    {
                        field: 'Size',
                        title: '文件大小',
                        halign: 'center',
                        align: 'right',
                        width: 45,
                        formatter: function(value, row, index) {
                            value = fileSizeFormat(value);
                            return value;
                        }
                    },
                    { field: 'Width', title: '媒体宽', halign: 'center', align: 'center', width: 45, hidden: true },
                    { field: 'Height', title: '媒体高', halign: 'center', align: 'center', width: 45, hidden: true },
                    { field: 'MD5', title: 'MD5', halign: 'center', align: 'center', width: 150 },
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
    <h1 class="page-header">媒体检索</h1>

    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">媒体检索</li>
    </ol>

    <form class="form-horizontal">

        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtMediaDirectory" class="col-sm-3 control-label">媒体目录</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="txtMediaDirectory" placeholder="本地媒体目录...">
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
                            <option value="5">大文件</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtFileSizeLimit" class="col-sm-3 control-label">大小限制</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="txtFileSizeLimit" placeholder="大文件最小限制为(M)...">
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtMediaDirectory" class="col-sm-3 control-label">哈希类型</label>
                    <div class="col-sm-9">
                        <div class="checkbox">
                            <label for="cbxMd5">
                                <input type="checkbox" id="cbxMd5" value="1" checked="checked"> MD5
                            </label>
                            <label for="cbxSha1" style="margin-left: 10px;">
                                <input type="checkbox" id="cbxSha1" value="2"> SHA1
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtMediaDirectory" class="col-sm-3 control-label">检索类型</label>
                    <div class="col-sm-9">
                        <div class="checkbox">
                            <label for="cbxType1"><input id="cbxType1" name="cbxType" type="checkbox" value="1"/> 重复</label>&nbsp;
                            <label for="cbxType2" style="margin-left: 10px;"><input id="cbxType2" name="cbxType" type="checkbox" value="2"/> 名称</label>&nbsp;
                            <label for="cbxType3" style="margin-left: 10px;"><input id="cbxType3" name="cbxType" type="checkbox" value="3"/> 目录</label>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-md-4">
                &nbsp;
            </div>            

        </div>

        <div class="form-group" style="display: none;">
            <div class="col-sm-offset-0 col-sm-12">
                <%--                <button type="button" id="btnBatchDelete" class="btn btn-danger" onclick="BatchDelete();">
                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 批量删除
                </button>--%>

            </div>
        </div>
    </form>

    <div id="toolbar">
         
        <div class="form-inline" role="form">

            <div class="form-group">
                <input name="search" class="form-control" type="text" placeholder="文件名称">
            </div>
            <button type="button" id="btnSearch" class="btn btn-primary">
                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 媒体查询
            </button>
            <button id="remove" class="btn btn-danger" disabled>
                <i class="glyphicon glyphicon-remove"></i> 批量删除
            </button>
            
            <button type="button" id="btnRetrieve" class="btn btn-primary">
                <span class="glyphicon glyphicon-file" aria-hidden="true"></span> 文件检索
            </button>

            <button type="button" id="btnDirectory" class="btn btn-success">
                <span class="glyphicon glyphicon-folder-open" aria-hidden="true"></span> 目录检索
            </button>

            <button type="button" id="btnArchiving" class="btn btn-success">
                <span class="glyphicon glyphicon-paperclip" aria-hidden="true"></span> 媒体数据归档
            </button>

            <button type="button" id="btnClear" class="btn btn-danger">
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
            </button>

        </div>
    </div>

    <table id="media-table"
           data-detail-view="true"
           data-detail-formatter="detailFormatter"
           data-minimum-count-columns="2">

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

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">

    <div class="container" style="text-align: center;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>