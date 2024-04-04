<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Audios.aspx.cs" Inherits="Medias_Audios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    音频文件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

<link href="../Bootstrap/v3/css/bootstrap-table.css" rel="stylesheet"/>
<link href="../Bootstrap/v3/css/bootstrap-editable.css" rel="stylesheet"/>
<link href="../Styles/BootStrapBase.css" rel="stylesheet" />

<script src="../Bootstrap/v3/js/bootstrap-table.js"></script>
<script src="../Bootstrap/v3/js/locales/bootstrap-table-zh-CN.js"></script>
<script src="../Bootstrap/v3/js/extensions/export/bootstrap-table-export.js"></script>
<script src="../Bootstrap/v3/js/extensions/editable/bootstrap-table-editable.js"></script>

<%--<script src="../Bootstrap/js/bootstrap-editable.js"></script>--%>

<style type="text/css">
    
    .breadcrumb { margin-bottom: 0; }

</style>
<script type="text/javascript">

    var $table = $('#audio-table'),
        $remove = $('#remove'),
        selections = [];

    var pageNumber = 1;
    var pageListUrl = "/Handler/AudioHandler.ashx?OPERATION=PAGELIST";

    $(function() {

        $table = $('#audio-table'),
            $remove = $('#remove'),
            selections = [];

        //$("#btnRetrieve").click(function () {
        //    Page("/Medias/Retrieve.aspx");
        //}); // 音频检索

        //$("#btnArchiving").click(function () {
        //    Page("/Medias/Archiving.aspx");
        //}); // 音频数据归档

        //$("#btnImages").click(function () {
        //    Page("/Medias/Images.aspx");
        //}); // 图像文件

        //$("#btnVideos").click(function () {
        //    Page("/Medias/Videos.aspx");
        //}); // 视频文件

        //$("#btnAudios").click(function () {
        //    Page("/Medias/Audios.aspx");
        //}); // 音频文件

        //$("#btnBooks").click(function () {
        //    Page("/Medias/Books.aspx");
        //}); // 图书文件

        //initTable();

        $("#btnClear").click(function() {
            var url = "/Handler/MediaHandler.ashx?OPERATION=EMPTYING&name=Audio";
            $.get(url + "&rnd=" + (Math.random() * 10), function(data) {
                if (data && data.success) {
                    $table.bootstrapTable('removeAll');
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }); // 清空音频库

        initTable();
    });

    function initTable1() {
        //先销毁表格  
        $('#audio-table').bootstrapTable('destroy');

        //初始化表格,动态从服务器加载数据  
        $("#audio-table").bootstrapTable({
            method: "get", //使用get请求到服务器获取数据  
            url: pageListUrl, //获取数据的Servlet地址  
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
                pageNumber = params.pageNumber;
                return param;
            },
            onLoadSuccess: function() { //加载成功时执行  
                alert("加载成功");
            },
            onLoadError: function() { //加载失败时执行  
                alert("加载数据失败");
            }
        });
    }
    
    //select Type, Name, Title, Album, Artist, Duration, Width, Height, Size, Extension
    //, ContentType, FullName, DirectoryName, CreationTime, MD5, SHA1 from Medias; 
    //音频名称 音频标题 所属目录  扩展名 大小(B) 音频宽 音频高 MD5

    function initTable() {

        //先销毁表格  
        $table.bootstrapTable('destroy');

        var queryUrl = pageListUrl + '&rnd=' + Math.random();

        //alert(queryUrl);

        $table.bootstrapTable({
            height: getHeight() - 35,
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


            //method: 'get',
            //url: queryUrl, //'/qStock/AjaxPage',  
            ////dataType: "json",  
            //striped: true, //使表格带有条纹  
            //pagination: true, //在表格底部显示分页工具栏  
            //pageSize: 10,
            //pageNumber: 1,
            //pageList: [10, 20, 50, 100, 200, 500],
            ////idField: "Id",  //标识哪个字段为id主键  
            ////showToggle: false,   //名片格式  
            ////cardView: false,//设置为True时显示名片（card）布局  
            //showColumns: true, //显示隐藏列    
            //showRefresh: true, //显示刷新按钮  
            //singleSelect: true, //复选框只能选择一条记录  
            //search: false, //是否显示右上角的搜索框  
            ////clickToSelect: true,//点击行即可选中单选/复选框  
            //sidePagination: "server", //表格分页的位置  
            //queryParams: function queryParams(params) { //设置查询参数  
            //    var param = {
            //        pageNumber: params.pageNumber,
            //        pageSize: params.pageSize
            //        //,orderNum: $("#TypeId").val()
            //    };
            //    return param;
            //}, //参数  
            //queryParamsType: "undefined", //参数格式,发送标准的RESTFul类型的参数请求  

            columns: [
                [
                    {
                        field : 'checked',
                        checkbox : true
                        //,formatter : stateFormatter

                    },
                    {
                        title: '编号',
                        field: 'Id',
                        halign: 'center',
                        align: 'center',
                        width: 75,
                        sortable: true
                        //,footerFormatter: totalTextFormatter

                    }, {
                        //    //field: '',
                        //    title: '阔行',
                        //    colspan: 8,
                        //    align: 'center'
                        //    //,events: operateEvents,
                        //    //formatter: operateFormatter
                        //}
                        //],[
                        //{
                        field: 'Name',
                        title: '名称',
                        sortable: true,
                        halign: 'center',
                        align: 'left',
                        formatter: titleFormatter

                        //}, {
                        //    field: 'Size',
                        //    title: '文件大小',
                        //    sortable: true,
                        //    align: 'center',
                        //    editable: {
                        //        type: 'text',
                        //        title: '文件大小',
                        //        validate: function(value) {
                        //            value = $.trim(value);
                        //            if (!value) {
                        //                return 'This field is required';
                        //            }

                        //            if (!/^\$/.test(value)) {
                        //                return 'This field needs to start width $.';
                        //            }

                        //            var data = $table.bootstrapTable('getData'),
                        //                index = $(this).parents('tr').data('index');
                        //            console.log(data[index]);
                        //            return '';
                        //        }
                        //    },
                        //    footerFormatter: totalPriceFormatter

                    }, {
                        field: 'Title',
                        title: '标题',
                        halign: 'center',
                        align: 'left',
                        formatter: titleFormatter
                    }, {
                        field: 'DirectoryName',
                        title: '所属目录',
                        halign: 'center',
                        align: 'left',
                        formatter: titleFormatter
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
                        formatter: titleFormatter
                    }, {
                        field: 'MD5',
                        title: 'MD5',
                        align: 'center',
                        formatter: titleFormatter
                    }, {
                        title: '操作',
                        width: 75,
                        align: 'center',
                        events: operateEvents,
                        formatter: operateFormatter
                    }
                ]
            ]

            //<th data-field="Title" >标题</th>
            //<th data-field="DirectoryName" >所属目录</th>
            //<th data-field="Extension" data-sortable="true">扩展名</th>
            //<th data-field="Size" data-sortable="true">大小</th>
            //<th data-field="ContentType" data-sortable="true">内容类型</th>
            //<th data-field="CreationTime" data-sortable="true">创建时间</th>
            //<th data-field="MD5" data-sortable="true">MD5</th>
            ,
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

        $table.on('expand-row.bs.table', function(e, index, row, $detail) {
            if (index % 2 == 1) {
                $detail.html('Loading from ajax request...');
                $.get('LICENSE', function(res) {
                    $detail.html(res.replace(/\n/g, '<br>'));
                });
            }
        });

        $table.on('all.bs.table', function(e, name, args) {
            console.log(name, args);
        });

        $remove.click(function() { // 批量删除
            var ids = getIdSelections();
            $table.bootstrapTable('remove', {
                field: 'Id',
                values: ids
            });
            $remove.prop('disabled', true);

            DelAudioByIds(ids); // 批量删除
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


    function responseHandler(res) {
        $.each(res.rows, function(i, row) {
            row.state = $.inArray(row.id, selections) !== -1;
        });
        return res;
    }


    function detailFormatter(index, row) {
        var html = [];
        $.each(row, function(key, value) {
            html.push('<p><b>' + key + ':</b> ' + value + '</p>');
        });
        return html.join('');
    }


    function operateFormatter(value, row, index) {
        return [
            //'<a class="like" href="javascript:void(0)" title="播放">',
            //'<i class="glyphicon glyphicon-play"></i>',
            //'</a>  ',
            '<a class="edit" href="javascript:void(0)" title="修改文件名称">',
            '<i class="glyphicon glyphicon-pencil"></i>',
            '</a>  ',
            '<a class="remove" href="javascript:void(0)" title="删除">',
            '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }

    // 文件大小格式化
    function sizeFormatter(value, row, index) {

        var s = "";
        if (value < 102.4) {
            s = "0.10K";
        } else if (value < 1024 * 1024) {
            s = (value / 1024.0).toFixed(2) + "K";
        } else if (value < 1024 * 1024 * 1024) {
            s = (value / 1024.0 / 1024.0).toFixed(2) + "M";
        } else {
            s = (value / 1024.0 / 1024.0 / 1024.0).toFixed(2) + "G";
        }
        return "<span title='" + value + "'>" + s + "</span>";
    }

    // 文件类型格式化
    function typeFormatter(value, row, index) {
        //<img src="/images/filetype/16/doc.gif" alt="" />
        var ext = value.replace(".", "");
        return '<img src="/images/filetype/16/' + ext + '.gif" alt="" title="' + ext + '" />';
    }

    // 提示内容格式化
    function titleFormatter(value, row, index) {
        return "<span title='" + value + "'>" + value + "</span>";
    }

    function tipsFormatter(value, row, index) {
        return "<span title='" + value + "'>" + value + "</span>";
    }

    window.operateEvents = {
        //'click .like': function(e, value, row, index) {
        //    alert('You click like action, row: ' + JSON.stringify(row));
        //},
        'click .edit': function (e, value, row, index) {
            //alert('You click edit action, row: ' + JSON.stringify(row));

            $("#txtId").val(row.Id);
            $("#txtTitle").val(row.Title);
            $("#txtOldName").val(row.Title);
            $("#txtExtension").val(row.Extension);
            $("#txtFullName").val(row.FullName);
            $("#txtDirectoryName").val(row.DirectoryName);

            $('#myModal').modal('toggle'); // 弹出名称修改

        },
        'click .remove': function(e, value, row, index) {
            $table.bootstrapTable('remove', {
                field: 'Id',
                values: [row.Id]
            });

            DelAudioById(row.Id); // 删除行数据，考虑要将上述表格响应纳入到删除操作中
        }
    };


    function totalTextFormatter(data) {
        return 'Total';
    }


    function totalNameFormatter(data) {
        return data.length;
    }


    function totalPriceFormatter(data) {
        var total = 0;
        $.each(data, function(i, row) {
            total += +(row.price.substring(1));
        });

        return '$' + total;
    }


    function getHeight() {
        return $(window).height() - $('h1').outerHeight(true);
    }


    $(function() {

        //var scripts = [
        //        location.search.substring(1) || '../Bootstrap/v3/js/bootstrap-table.js',
        //        '../Bootstrap/v3/js/extensions/export/bootstrap-table-export.js',
        //        'http://rawgit.com/hhurz/tableExport.jquery.plugin/master/tableExport.js',
        //        '../Bootstrap/v3/js/extensions/editable/bootstrap-table-editable.js',
        //        'http://rawgit.com/vitalets/x-editable/master/dist/bootstrap3-editable/js/bootstrap-editable.js'
        //    ],

        //    eachSeries = function(arr, iterator, callback) {
        //        callback = callback || function() {};
        //        if (!arr.length) {
        //            return callback();
        //        }

        //        var completed = 0;
        //        var iterate = function() {
        //            iterator(arr[completed], function(err) {
        //                if (err) {
        //                    callback(err);
        //                    callback = function() {};
        //                } else {
        //                    completed += 1;
        //                    if (completed >= arr.length) {
        //                        callback(null);
        //                    } else {
        //                        iterate();
        //                    }
        //                }
        //            });
        //        };
        //        iterate();
        //    };

        //eachSeries(scripts, getScript, initTable);

    });


    function getScript(url, callback) {
        var head = document.getElementsByTagName('head')[0];
        var script = document.createElement('script');
        script.src = url;
        var done = false;

        // Attach handlers for all browsers

        script.onload = script.onreadystatechange = function() {
            if (!done && (!this.readyState ||
                this.readyState == 'loaded' || this.readyState == 'complete')) {
                done = true;
                if (callback)
                    callback();

                // Handle memory leak in IE

                script.onload = script.onreadystatechange = null;
            }
        };

        head.appendChild(script);

        // We handle everything using the script element injection

        return undefined;

    }

    /// <summary>
    /// 删除音频
    /// </summary>

    function DelAudioById(id) {
        if (confirm("您确定要删除该音频吗？")) {
            var url = "/Handler/AudioHandler.ashx?OPERATION=DELETE&ID=" + id;
            $.get(url + "&rnd=" + (Math.random() * 10), function (data) {
                if (data && data.success) {
                    $.messager.alert("操作提示", data.Message, "info");
                } else {
                    $.messager.alert("操作提示", data.Message, "error");
                }
            });
        }
    }

    /// <summary>
    /// 批量删除音频
    /// </summary>

    function DelAudioByIds(ids) {
        if (confirm("您确定要批量删除这些音频吗？")) {
            var url = "/Handler/AudioHandler.ashx?OPERATION=BATCHDELETE&IDs=" + ids;
            $.get(url + "&rnd=" + (Math.random() * 10), function (data) {
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

    <h1 class="page-header">音频文件</h1>
    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">音频文件</li>
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
                </div>--%>

            <div class="form-group">
                <input name="search" class="form-control" type="text" placeholder="Search">
            </div>

            <button id="remove" class="btn btn-danger" disabled>
                <i class="glyphicon glyphicon-remove"></i> 批量删除
            </button>

            <button type="button" id="btnClear" class="btn btn-danger">
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空音频库
            </button>

        </div>
    </div>

    <table id="audio-table"
           data-toolbar="#toolbar"
           data-search="true"
           data-show-refresh="true"
           data-show-toggle="true"
           data-show-columns="true"
           data-show-export="true"
           data-detail-view="true"
           data-detail-formatter="detailFormatter"
           data-minimum-count-columns="2"
           data-show-pagination-switch="true"
           data-pagination="true"
           data-id-field="Id"
           data-page-list="[10, 25, 50, 100, ALL]"
           data-show-footer="false"
           data-side-pagination="server"
           data-url="/Handler/AudioHandler.ashx?OPERATION=PAGELIST"
           data-response-handler="responseHandler">

        <%--        <thead>  
          <tr> 
              <th data-field="Id" data-sortable="true">编号</th>                                                           
              <th data-field="Type">分类</th>  
              <th data-field="Name" >名称</th>
              
              <th data-field="Title" >标题</th>
              <th data-field="DirectoryName" >所属目录</th>
              <th data-field="Extension" data-sortable="true">扩展名</th>
              <th data-field="Size" data-sortable="true">大小</th>
              <th data-field="ContentType" data-sortable="true">内容类型</th>
              <th data-field="CreationTime" data-sortable="true">创建时间</th>
              <th data-field="MD5" data-sortable="true">MD5</th>

              <!-- 在此省略表格列的代码，代码和上面差不多 -->  
              <th class="col-xs-2" data-field="action" data-formatter="actionFormatter" data-events="actionEvents">操作</th>     
          </tr>  
       </thead>  
       <tbody>  
       </tbody>  --%>

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
                    <h4 class="modal-title" id="progressModalLabel">操作正在进行中，请稍候...</h4>
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
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span>关闭
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
                    <h4 class="modal-title" id="myModalLabel">音频文件名称修改</h4>
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
    
    $(function () {
        var index = "";
        $('#myModal').on('shown.bs.modal', function (e) { // 弹出模态窗体事件
            //debugger;

            // 这里的btn就是触发元素，即你点击的删除按钮 $("#txtFullName").val(row.FullName);
            //var btn = $(e.relatedTarget),
            //    idx = btn.data("id");

        });

        $('#myModal').on('hidden.bs.modal', function () { // 关闭模态窗体事件
            $("#txtId").val("");
            $("#txtTitle").val("");
            $("#txtOldName").val("");
            $("#txtExtension").val("");
            $("#txtFullName").val("");
            $("#txtDirectoryName").val("");
        });

        $("#btnSave").unbind("click").bind("click", function () { // 名称数据保存

            var params = {
                id: $("#txtId").val(),
                title: escape($("#txtTitle").val()) ,
                ext: escape($("#txtExtension").val()) ,
                dir: escape($('#txtDirectoryName').val()),
                fullname: escape($('#txtFullName').val())
            };

            $.ajax({
                type: "POST",
                url: "/Handler/AudioHandler.ashx?OPERATION=SAVE&rnd=" + (Math.random() * 10),
                data: params,
                success: function (rst) {
                    if (rst && rst.success) {
                        $table.bootstrapTable('updateByUniqueId', {
                            id: $("#txtId").val(),
                            row: {
                                Title: $("#txtTitle").val(),
                                FullName: $('#txtDirectoryName').val() + "\\" + $("#txtTitle").val() + $("#txtExtension").val()
                            }
                        });

                        //$table.bootstrapTable('refresh', { url: pageListUrl });
                        //$table.bootstrapTable('refreshOptions', { pageNumber: pageNumber });

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

    <div class="container" style="text-align: center;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>