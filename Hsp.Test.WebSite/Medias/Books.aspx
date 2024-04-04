<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Books.aspx.cs" Inherits="Medias_Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    图书文件
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">

    <h1 class="page-header">图书文件</h1>
    <ol class="breadcrumb">
        <li>
            <a href="/Default.aspx">首页</a>
        </li>
        <li class="active">图书文件</li>
    </ol>

    <button type="button" id="btnClear" class="btn btn-danger">
        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 清空媒体库
    </button>

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

    <div class="container" style="text-align: center;">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>