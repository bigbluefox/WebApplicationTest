<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="CKEditor_v4_ArticleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    CKEditor 4 文章编辑 Bootstrap 测试
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
    
    <link href="/Bootstrap/v3/css/sticky-footer-navbar.css" rel="stylesheet" />
    <link href="/Bootstrap/v3/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />   

    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">    
    
    <script src="CKEditor/ckeditor.js"></script>
    <script src="CKEditor/config.js"></script>

    <script src="/Bootstrap/v3/js/bootstrap-datetimepicker.min.js"></script>
    <script src="/Bootstrap/v3/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>
    
    <style type="text/css">
        body {
            background: #F5F6F8;
        }

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

        .panel-default > .panel-heading {
            background-color: #68778e;
        }

        .heading-title {
            color: #fff;
            line-height: 20px;
            font-weight: bold;
        }

        #standardTable thead {
            font-size: 15px;
            color: #26477c;
            font-weight: 200;
        }

        #standardTable tbody {
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
            <span class="heading-title">CKEditor 4 文章编辑 Bootstrap 测试『跳转页面编辑』</span>
            <span style="float: right;">
                <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-id="" data-target="#importModal" style="display: none;">
                    <span class="glyphicon glyphicon-import" aria-hidden="true"></span>标准文件导入
                </button>
                <button type="button" class="btn btn-default btn-sm" onclick="StandardStats();" style="display: none;">
                    <span class="glyphicon glyphicon-stats" aria-hidden="true"></span>标准下载统计
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
                
                <%--//Id, Title, Author, Source, Abstract, Content, CreateTime--%>

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
                        <span class="glyphicon glyphicon-edit" aria-hidden="true" title="编辑" data-toggle="modal" data-id="<% = m.Id - (PageIndex - 1)*PageSize %>" data-target="#editModal"></span>
                        <span class="glyphicon glyphicon-remove" aria-hidden="true" title="删除" onclick="javascript:StandardDelete(this);"></span>
                        <span class="glyphicon glyphicon-download-alt" aria-hidden="true" title="下载" onclick="javascript:StandardDownload('<% = m.Id %>');"></span>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
    
    <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>       

</asp:Content>

