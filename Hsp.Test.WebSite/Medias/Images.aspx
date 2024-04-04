<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Images.aspx.cs" Inherits="Medias_Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    图片文件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

<link href="../Bootstrap/v3/css/bootstrap-table.css" rel="stylesheet"/>
<link href="../Bootstrap/v3/css/bootstrap-editable.css" rel="stylesheet"/>

<script src="../Bootstrap/v3/js/bootstrap-table.js"></script>
<script src="../Bootstrap/v3/js/locales/bootstrap-table-zh-CN.js"></script>
<script src="../Bootstrap/v3/js/extensions/export/bootstrap-table-export.js"></script>
<script src="../Bootstrap/v3/js/extensions/editable/bootstrap-table-editable.js"></script>

<style type="text/css"></style>
<script type="text/javascript">
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

        //$("#btnAudios").click(function () {
        //    Page("/Medias/Audios.aspx");
        //}); // 音频文件

        //$("#btnBooks").click(function () {
        //    Page("/Medias/Books.aspx");
        //}); // 图书文件

        initTable();
    });

    function queryParams(params) { //配置参数  
        var temp = {
            //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的  
            pageSize: params.limit, //页面大小  
            pageNumber: params.pageNumber, //页码  
            //minSize: $("#leftLabel").val(),  
            //maxSize: $("#rightLabel").val(),  
            //minPrice: $("#priceleftLabel").val(),  
            //maxPrice: $("#pricerightLabel").val(),  
            //Cut: Cut,  
            //Color: Color,  
            //Clarity: Clarity,  
            sort: params.sort, //排序列名  
            sortOrder: params.order //排位命令（desc，asc）  
        };
        return temp;
    }

    function initTable() {

        var queryUrl = '/Handler/MediaHandler.ashx?OPERATION=PAGELIST&rnd=' + Math.random();

        //先销毁表格  
        $('#image-table').bootstrapTable('destroy');

        ////初始化表格,动态从服务器加载数据  
        //$("#image-table").bootstrapTable({
        //    method: "get",  //使用get请求到服务器获取数据  
        //    url: "/Handler/MediaHandler.ashx?OPERATION=PAGELIST", //获取数据的Servlet地址  
        //    striped: true,  //表格显示条纹  
        //    pagination: true, //启动分页  
        //    pageSize: 1,  //每页显示的记录数  
        //    pageNumber: 1, //当前第几页  
        //    pageList: [5, 10, 15, 20, 25],  //记录数可选列表  
        //    search: false,  //是否启用查询  
        //    showColumns: true,  //显示下拉框勾选要显示的列  
        //    showRefresh: true,  //显示刷新按钮  
        //    sidePagination: "server", //表示服务端请求  
        //    //设置为undefined可以获取pageNumber，pageSize，searchText，sortName，sortOrder  
        //    //设置为limit可以获取limit, offset, search, sort, order  
        //    queryParamsType: "undefined",
        //    queryParams: function queryParams(params) {   //设置查询参数  
        //        var param = {
        //            pageNumber: params.pageNumber,
        //            pageSize: params.pageSize,
        //            orderNum: $("#TypeId").val()
        //        };
        //        return param;
        //    },
        //    onLoadSuccess: function () {  //加载成功时执行  
        //        //alert("加载成功");
        //    },
        //    onLoadError: function (data) {  //加载失败时执行  
        //        alert("加载数据失败");
        //    }
        //});

        $('#image-table').bootstrapTable({
            //height: 600,
            method: 'get',
            url: queryUrl, //'/qStock/AjaxPage',  
            //dataType: "json",  
            striped: true, //使表格带有条纹  
            pagination: true, //在表格底部显示分页工具栏  
            pageSize: 10,
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500],
            //idField: "Id",  //标识哪个字段为id主键  
            //showToggle: false,   //名片格式  
            //cardView: false,//设置为True时显示名片（card）布局  
            showColumns: true, //显示隐藏列    
            showRefresh: true, //显示刷新按钮  
            singleSelect: true, //复选框只能选择一条记录  
            search: false, //是否显示右上角的搜索框  
            //clickToSelect: true,//点击行即可选中单选/复选框  
            sidePagination: "server", //表格分页的位置  
            queryParams: function queryParams(params) { //设置查询参数  
                var param = {
                    pageNumber: params.pageNumber,
                    pageSize: params.pageSize
                    //,orderNum: $("#TypeId").val()
                };
                return param;
            }, //参数  
            queryParamsType: "undefined", //参数格式,发送标准的RESTFul类型的参数请求  
            //toolbar: "#toolbar", //设置工具栏的Id或者class  
            //columns: column, //列  
            columns: [
                {
                    //title: '活动名称',
                    //field: 'name',
                    //align: 'center',
                    //valign: 'middle'
                    title: '编号',
                    field: 'Id',
                    //rowspan: 2,
                    align: 'center',
                    valign: 'middle',
                    sortable: true
                },
                {
                    field: 'Name',
                    title: '名称',
                    sortable: true,
                    //editable: true,
                    //footerFormatter: totalNameFormatter,
                    align: 'center'
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
                }
                //,{
                //    title: '参与人数',
                //    field: 'participationCounts',
                //    align: 'center'
                //},
                //{
                //    title: '总人数',
                //    field: 'totalCounts',
                //    align: 'center'
                //},
                //{
                //    title: '开始时间',
                //    field: 'startTime',
                //    align: 'center',
                //},
                //{
                //    title: '操作',
                //    field: 'id',
                //    align: 'center',
                //    formatter: function (value, row, index) {
                //        var e = '<a href="#" mce_href="#" onclick="edit(\'' + row.id + '\')">编辑</a> ';
                //        var d = '<a href="#" mce_href="#" onclick="del(\'' + row.id + '\')">删除</a> ';
                //        return e + d;
                //    }
                //}
            ], //列  
            //silent: true,  //刷新事件必须设置  
            formatLoadingMessage: function() {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function() { //没有匹配的结果  
                return '无符合条件的记录';
            },
            onLoadError: function(data) {
                $('#reportTable').bootstrapTable('removeAll');
            },
            onClickRow: function (row) {
                alert(row.Id);
                //window.location.href = ""; // "/qStock/qProInfo/" + row.Id;  
            }
        });


        //$table = $('#image-table').bootstrapTable({

        //    //method: 'get',<br><br>
        //    method: 'post',
        //    dataType:'json',
        //    contentType: "application/x-www-form-urlencoded", //必须的
        //    url: queryUrl,
        //    height: $(window).height() - 200,
        //    striped: true,
        //    pagination: true,
        //    singleSelect: false,
        //    pageSize: 50,
        //    pageList: [10, 50, 100, 200, 500],
        //    search: false, //不显示 搜索框
        //    showColumns: false, //不显示下拉框（选择显示的列）
        //    sidePagination: "server", //服务端请求
        //    queryParams: queryParams,
        //    minimunCountColumns: 2,
        //    columns: [
        //        {
        //        //    field: 'state',
        //        //    checkbox: true
        //        //}, {
        //            field: 'Name',
        //            title: '名称',
        //            width: 100,
        //            align: 'center',
        //            valign: 'middle',
        //            sortable: true
        //            //,formatter: nameFormatter

        //        }, {
        //            field: 'Title',
        //            title: '标题',
        //            width: 40,
        //            align: 'left',
        //            valign: 'top',
        //            sortable: true
        //            //,formatter: sexFormatter,
        //        //    //sorter: priceSorter
        //        //}, {
        //        //    field: 'Birthday',
        //        //    title: '出生日期',
        //        //    width: 80,
        //        //    align: 'left',
        //        //    valign: 'top',
        //        //    sortable: true
        //        //}, {
        //        //    field: 'CtfId',
        //        //    title: '身份证',
        //        //    width: 80,
        //        //    align: 'middle',
        //        //    valign: 'top',
        //        //    sortable: true
        //        //}, {
        //        //    field: 'Address',
        //        //    title: '地址',
        //        //    width: 180,
        //        //    align: 'left',
        //        //    valign: 'top',
        //        //    sortable: true
        //        //}, {
        //        //    field: 'Tel',
        //        //    title: '固定电话',
        //        //    width: 100,
        //        //    align: 'left',
        //        //    valign: 'top',
        //        //    sortable: true
        //        //}, {
        //            //field: 'Mobile',
        //            //title: '手机号码',
        //            //width: 100,
        //            //align: 'left',
        //            //valign: 'top',
        //            //sortable: true
        //        //}, {
        //        //    field: 'operate',
        //        //    title: '操作',
        //        //    width: 100,
        //        //    align: 'center',
        //        //    valign: 'middle',
        //        //    formatter: operateFormatter,
        //        //    events: operateEvents
        //        }
        //    ],
        //    onLoadSuccess: function() {
        //    },

        //    onLoadError: function() {

        //        mif.showErrorMessageBox("数据加载失败！");

        //    }

        //});

    }

    //传递的参数

    function queryParams(params) {

        return {
            pageSize: params.pageSize,
            pageIndex: params.pageNumber
            //,UserName: $("#txtName").val(),
            //Birthday: $("#txtBirthday").val(),
            //Gender: $("#Gender").val(),
            //Address: $("#txtAddress").val(),
            //name: params.sortName,
            //order: params.sortOrder
        };
    }


</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
    <h1 class="page-header">图片文件</h1>
    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">图片文件</li>
    </ol>

                <button type="button" id="btnClear" class="btn btn-danger">
                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
                </button>     

    <table id="image-table"

           data-pagination="true"
           data-show-refresh="true"
           data-show-toggle="true"
           data-showColumns="true"

           <%-- data-toolbar="#toolbar"
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
           data-url="/Handler/MediaHandler.ashx?OPERATION=PAGELIST"
           data-response-handler="responseHandler"--%>
        >

        <%-- <thead> 
          <tr> 
              <th data-field="Id" data-sortable="true">编号</th>                                                           
              <th data-field="Type">分类</th>  
              <th data-field="Name" >名称</th>              
              
         </tr>      
       </thead>  
       <tbody>  
       </tbody> --%>
    </table>

    

<%--            <thead>
            <tr>
                <th colspan="2">Item Detail</th>
                <th data-field="id" rowspan="2" data-valign="middle">Item ID</th>
            </tr>
            <tr>
                <th data-field="name">Item Name</th>
                <th data-field="price">Item Price</th>
            </tr>
            </thead> --%>   
    

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
                    <h4 class="modal-title" id="progressModalLabel">文件正在检索中，请稍候...</h4>
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

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">

    <div class="container" style="text-align: center; margin-top: 20px;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>