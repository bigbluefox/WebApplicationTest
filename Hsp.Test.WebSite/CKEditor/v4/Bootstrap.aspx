<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Bootstrap.aspx.cs" Inherits="CKEditor_v4_Bootstrap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    CKEditor 4 文章编辑 Bootstrap 测试
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
   
    <link href="/Bootstrap/v3/css/sticky-footer-navbar.css" rel="stylesheet">
    <link href="/Bootstrap/v3/css/bootstrap-datetimepicker.min.css" rel="stylesheet">   

    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">    
    
    <script src="CKEditor/ckeditor.js"></script>
    <script src="CKEditor/config.js"></script>

    <script src="/Bootstrap/v3/js/bootstrap-datetimepicker.min.js"></script>
    <script src="/Bootstrap/v3/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>
    
    <style type="text/css">
        body { background: #F5F6F8; }
        body > .container{padding: 15px 15px 0}
        .panel-default > .panel-heading { background-color: #68778e; }

        .table {
            table-layout: fixed;
        }

        tr td {
            -moz-text-overflow: ellipsis; /* for Firefox, mozilla */
            overflow: hidden;
            text-align: left;
            text-overflow: ellipsis; /* for IE */
            white-space: nowrap;
        }

        tr th{text-align: center;}

        .heading-title {
            color: #fff;
            line-height: 20px;
            font-weight: bold;
        }

        thead {
            font-size: 15px;
            color: #26477c;
            font-weight: 200;
        }

        tbody {
            font-size: 14px;
        }
    </style>

    <script type="text/javascript">

        $(function () {

            $("#btnSearch").unbind("click").bind("click", function () { // 文章查询点击事件响应

                //var name = $("#txtName").val();
                //if (name.length == 0) {
                //    alert("请填写查询条件!");
                //    $("#txtName").focus();
                //    return false;
                //}

                QueryArticle();

                return false;
            });
            $('#txtName').on('keypress', function (event) { // 模糊查询回车事件响应
                if (event.keyCode == 13) {
                    //var name = $("#txtName").val();
                    //if (name.length == 0) {
                    //    alert("请填写查询条件!");
                    //    $("#txtName").focus();
                    //} else {
                    //    CustomerStandardList(cid);
                    //}
                    QueryArticle();
                }
            });

            //var test = window.location.search;
            //alert(test);
            // &page=

            $('.form_date').datetimepicker({
                language: 'zh-CN',
                format: 'yyyy-mm-dd',
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });

            // 日期更新
            $("#startDate").prev().find("input").val("<% = StartDate %>"); // 开始日期
            $("#endDate").prev().find("input").val("<% = EndDate %>"); // 结束日期
            $("#startDate").val("<% = StartDate %>");
            $("#endDate").val("<% = EndDate %>");
            $('.form_date').datetimepicker('update');
        });

        // 查询文章信息
        function QueryArticle() {

            var url = window.location.href;
            url = url.split('?')[0];
            //url += "?mapName=" + HSP.Request.QueryString("mapName");

            var sdate = $("#startDate").val();
            var edate = $("#endDate").val();
            var name = $("#txtName").val();

            url += "&queryname=" + encodeURIComponent(name);
            url += "&sdate=" + sdate;
            url += "&edate=" + edate;

            window.location.href = url;
        }

    </script>   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
    
    <div class="table-responsive panel panel-default">

        <!-- Default panel contents -->
        <div class="panel-heading" style="height: 42px;">
            <span class="heading-title">CKEditor 4 文章编辑 Bootstrap 测试『弹出模态窗体』</span>
            <span style="float: right;">
                <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-id="" data-target="#importModal" style="display: none;">
                    <span class="glyphicon glyphicon-import" aria-hidden="true"></span>标准文件导入
                </button>
                <button type="button" class="btn btn-default btn-sm" onclick="Redirect('ArticleEdit.aspx');">
                    <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>&nbsp;跳转添加文章
                </button>
                <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-id="" data-target="#editModal">
                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>添加文章
                </button>
            </span>
        </div>

         <div class="panel-body">
            <form class="form-inline">
                <div class="form-group">
                    <label class="sr-only" for="startDate">开始时间</label>
                    <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd" data-link-field="startDate" data-link-format="yyyy-mm-dd">
                        <input class="form-control" size="16" type="text" value="" placeholder="选择开始时间..." readonly>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                    </div>
                    <input type="hidden" id="startDate" value="">
                </div>
                <div class="form-group">
                    <label class="sr-only" for="endDate">结束时间</label>
                    <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd" data-link-field="endDate" data-link-format="yyyy-mm-dd">
                        <input class="form-control" size="16" type="text" value="" placeholder="选择结束时间..." readonly>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                    </div>
                    <input type="hidden" id="endDate" value="">
                </div>
                <div class="input-group" style="width: 600px;" title="文章标题或者内容">
                    <input type="text" id="txtName" class="form-control" value="" placeholder="请输入要模糊查询的文章标题或者内容...">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" id="btnSearch">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>&nbsp;查询
                        </button>
                    </span>
                </div><!-- /input-group -->

            </form>            
        </div>   
        
        <!-- Table -->
        <table id="tb" class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th width="60">序号</th>
                    <th>标题</th>
                    <th width="150">作者</th>
                    <th width="150">来源</th>
                    <th width="150">日期</th>
                    <th width="90">操作</th>
                </tr>
            </thead>
            <tbody>
                
                <%--//Id, Title, Author, Source, Abstract, Content, CreateTime previewModal--%>

                <% if (List != null && List.Count > 0)
                   {
                       foreach (var m in List)
                       { %>
                <tr>
                    <th><% = m.Id %></th>
                    <td title="<% = m.Title %>"><% = m.Title %></td>
                    <td title="<% = m.Author %>"><% = m.Author %></td>
                    <td title="<% = m.Source %>"><% = m.Source %></td>
                    <td title="<% = m.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") %>"><% = m.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                    <td style="text-align: center;">
                        <span class="glyphicon glyphicon-modal-window" aria-hidden="true" title="预览" data-toggle="modal" data-id="<% = m.Id %>" data-target="#previewModal"></span>
                        <span class="glyphicon glyphicon-edit" aria-hidden="true" title="编辑" data-toggle="modal" data-id="<% = m.Id %>" data-target="#editModal"></span>
                        <span class="glyphicon glyphicon-remove" aria-hidden="true" title="删除" onclick="javascript:void(0);"></span>
                    </td>
                </tr>
                <% }
                   } %>                

            </tbody>

        </table>

    </div>    
    
    <% = PagerString %>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">

    <!--
    模态框区域 Modal
    模态框经过了优化，更加灵活，以弹出对话框的形式出现，具有最小和最实用的功能集。
    1)不支持同时打开多个模态框：
    千万不要在一个模态框上重叠另一个模态框。要想同时支持多个模态框，需要自己写额外的代码来实现。
    2)模态框的 HTML 代码放置的位置：
    务必将模态框的 HTML 代码放在文档的最高层级内（也就是说，尽量作为 body 标签的直接子元素），以避免其他组件影响模态框的展现和/或功能。
    -->

    <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="editModalLabel">文章编辑</h4>
                </div>
                <div class="modal-body">

                    <%--RowNumber, Id, Title, Author, Source, Abstract, Content, CreateTime --%>

                    <form>
                        <div class="form-group">
                            <label for="txtTitle">文章标题：</label>
                            <input type="text" class="form-control" id="txtTitle" placeholder="文章标题...">
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtAuthor">文章作者：</label>
                                    <input type="text" class="form-control" id="txtAuthor" placeholder="文章作者...">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtSource">文章来源：</label>
                                    <input type="text" class="form-control" id="txtSource" placeholder="文章来源...">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtContent">文章内容：</label>
                            <textarea class="form-control ckeditor" rows="3" id="txtContent" placeholder="文章内容..."></textarea>
                        </div>
                        <div class="form-group">
                            <label for="txtAbstract">文章摘要：</label>
                            <textarea class="form-control" rows="3" id="txtAbstract" placeholder="文章摘要..."></textarea>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <input type="hidden" id="txtId">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span>关闭
                    </button>
                    <button type="button" class="btn btn-primary" id="btnSave">
                        <span class="glyphicon glyphicon-floppy-saved" aria-hidden="true"></span>保存
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        // 从表格中获取数据试了几种方法只有通过document.getElementById("table").rows[id].cells[0].innerText; 方式可以获取。

        $(function () {

            // 模态窗体弹出事件 
            $('#editModal').on('shown.bs.modal', function (e) { // 弹出模态窗体事件

                // 这里的btn就是触发元素，即你点击的删除按钮
                var btn = $(e.relatedTarget),
                    id = btn.data("id");

                if (id > 0) {

                    $("#txtId").val(id);

                    $.get('/Handler/ArticleHandler.ashx?OPERATION=GETARTICLEBYID&ID=' + id + '&rnd=' + (Math.random() * 10), function (rst) {
                        if (rst) {

                            $("#txtTitle").val(rst.Data.Title);
                            $("#txtAuthor").val(rst.Data.Author);
                            $("#txtSource").val(rst.Data.Source);
                            $("#txtAbstract").val(rst.Data.Abstract);
                            $("#txtContent").val(rst.Data.Content);

                        } else {
                            $.messager.alert('提示', '标准数据获取异常，请重试！', 'error');
                        }
                    });
                    
                }
            });

            // 模态窗体关闭事件 
            $('#editModal').on('hidden.bs.modal', function () { // 关闭模态窗体事件
                $("#txtTitle").val("");
                $("#txtAuthor").val("");
                $("#txtSource").val("");
                $("#txtAbstract").val("");
                $("#txtContent").val("");
            });

            // 保存按钮点击事件 
            $("#btnSave").unbind("click").bind("click", function () { // 标准数据保存

                var id = $("#txtId").val();

            <%--
                $("#txtTitle").val(rst.Data.Title);
                $("#txtAuthor").val(rst.Data.Author);
                $("#txtSource").val(rst.Data.Source);
                $("#txtAbstract").val(rst.Data.Abstract);
                $("#txtContent").val(rst.Data.Content);
        
                $("#article-title").html(rst.Data.Title);
                $("#article-author").html(rst.Data.Author);
                $("#article-source").html(rst.Data.Source);
                $("#article-abstract").html(rst.Data.Abstract);
                $("#article-content").html(rst.Data.Content);
                $("#article-createtime").html(rst.Data.CreateTime.toDateTimeString("yyyy-MM-dd HH:mm:SS"));        

            --%>

                var params = {
                    title: $("#txtTitle").val(),
                    author: $("#txtAuthor").val(),
                    source: $("#txtSource").val(),
                    abstract: $("#txtAbstract").val(),
                    content: $("#txtContent").val()
                };

                $.ajax({
                    type: "POST",
                    url: "/Handler/ArticleHandler.ashx?OPERATION=" + ((id && id.length > 0) ? "ARTICLEEDIT" : "ARTICLESAVE") + "&rnd=" + (Math.random() * 10),
                    data: params,
                    success: function (rst) {
                        if (rst) {
                            if (rst.IsSuccess) {
                                //var table = document.getElementById("standardTable");
                                $.messager.show({ title: "操作提示", msg: rst.Message, timeout: 3000, showType: "fade" });
                                $('#editModal').modal('hide');
                            } else {
                                $.messager.alert("提示", rst.Message, "error");
                            }
                        } else {
                            $.messager.alert("提示", "编辑标准属性信息异常，请重试！", "info");
                        }
                    }
                });
            });
        });

    </script>
    
    <style type="text/css">
        
        .blog-post{ font-size: 15px;}

        .blog-post-meta{ margin: 10px 0;}
    </style>

    <!-- 文章预览模态窗体 //-->
    <div class="modal fade" id="previewModal" tabindex="-1" role="dialog" aria-labelledby="previewModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="previewModalLabel">文章预览</h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="padding: 0 20px 10px;">
                        <div class="blog-header" style="padding-top: 0px; padding-bottom: 5px;">
                            <h2 id="article-title"></h2>
                            <p class="lead blog-description" id="article-abstract" style="display: none;"></p>
                        </div>
                        <div class="blog-post">
                            <%--<h2 class="blog-post-title">Another blog post</h2>--%>
                            <p class="blog-post-meta">
                                <span id="article-source"></span>
                                <span id="article-createtime"></span> 
                                <a href="#" id="article-author"></a>
                            </p>
                            <div id="article-content"></div>
                        </div><!-- /.blog-post -->
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span>关闭
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {

            // 文章预览模态窗体弹出事件 
            $('#previewModal').on('shown.bs.modal', function (e) { // 弹出模态窗体事件

                // 这里的btn就是触发元素，即你点击的删除按钮
                var btn = $(e.relatedTarget),
                    id = btn.data("id");

                var params = {
                    OPERATION: "GETARTICLEBYID",
                    id: id,
                    rnd: (Math.random() * 10)
                };

                $.ajax({
                    type: "GET",
                    url: "/Handler/ArticleHandler.ashx",
                    data: params,
                    success: function (rst) {
                        if (rst) {
                            if (rst.success && rst.Data) {
                                $("#article-title").html(rst.Data.Title);
                                $("#article-author").html(rst.Data.Author);
                                $("#article-source").html(rst.Data.Source);
                                $("#article-abstract").html(rst.Data.Abstract);
                                $("#article-content").html(rst.Data.Content);
                                $("#article-createtime").html(rst.Data.CreateTime.toDateTimeString("yyyy-MM-dd HH:mm:SS"));
                                //Id, Title, Author, Source, Abstract, Content, CreateTime
                            } else {
                                $.messager.alert("提示", rst.Message, "error");
                            }
                        }
                    }
                });

            });

            // 文章预览模态窗体关闭事件 
            $('#previewModal').on('hidden.bs.modal', function () { // 关闭模态窗体事件
                $("#article-title").html("");
                $("#article-author").html("");
                $("#article-source").html("");
                $("#article-abstract").html("");
                $("#article-content").html("");
                $("#article-createtime").html("");
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
    
    <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>    

</asp:Content>

