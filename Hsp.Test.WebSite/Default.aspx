<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    媒体检索
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">
    <style type="text/css">
        body {
            min-width: 300px !important;
        }

        table {
            table-layout: fixed !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnRetrieve").click(function () {
                Page("/Medias/Retrieve.aspx");
            }); // 媒体检索

            $("#btnArchiving").click(function () {
                Page("/Medias/Archiving.aspx");
            }); // 媒体数据归档

            $("#btnImages").click(function () {
                Page("/Medias/Images.aspx");
            }); // 图像文件

            $("#btnVideos").click(function () {
                Page("/Medias/Videos.aspx");
            }); // 视频文件

            $("#btnAudios").click(function () {
                Page("/Medias/Audios.aspx");
            }); // 音频文件

            $("#btnBooks").click(function () {
                Page("/Medias/Books.aspx");
            }); // 图书文件

            $("#btnClearDir").click(function () {
                var dir = $("#txtEmptyDirectory").val();
                var url = "/Handler/MediaHandler.ashx?OPERATION=EMPTYDIR";
                url += "&dir=" + escape(dir);
                $.get(url + "&rnd=" + (Math.random() * 10), function (data) {
                    if (data && data.success) {
                        $.messager.alert("操作提示", unescape(data.Message), "info");
                    } else {
                        $.messager.alert("操作提示", data.Message, "error");
                    }
                });
            }); // 清理空白目录




        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" runat="Server">

    <h1 class="page-header">媒体检索</h1>

    <button type="button" id="btnRetrieve" class="btn btn-primary">
        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>媒体检索
    </button>

    <button type="button" id="btnArchiving" class="btn btn-primary">
        <span class="glyphicon glyphicon-inbox" aria-hidden="true"></span>数据归档
    </button>

    <br />
    <br />

    <button type="button" id="btnImages" class="btn btn-success">
        <span class="glyphicon glyphicon-picture" aria-hidden="true"></span>图像文件
    </button>

    <button type="button" id="btnVideos" class="btn btn-info">
        <span class="glyphicon glyphicon-film" aria-hidden="true"></span>视频文件
    </button>

    <button type="button" id="btnAudios" class="btn btn-warning">
        <span class="glyphicon glyphicon-music" aria-hidden="true"></span>音频文件
    </button>

    <button type="button" id="btnBooks" class="btn btn-danger">
        <span class="glyphicon glyphicon-book" aria-hidden="true"></span>图书文件
    </button>

    <br />
    <br />



    <form class="form-horizontal">
        <div class="row">
            <div class="col-md-6">

                <div class="form-group">
                    <label for="txtEmptyDirectory" class="col-sm-2 control-label">目录名称</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="txtEmptyDirectory" placeholder="要清理的目录...">
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <button type="button" id="btnClearDir" class="btn btn-danger">
                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>空目录清理
                </button>
            </div>
        </div>




    </form>

    <table>
        <tr>
            <td style="width: 105px;"></td>
            <td></td>
        </tr>
    </table>

    <!-- Standard button -->
    <%--<button type="button" class="btn btn-default">（默认样式）Default</button>--%>

    <!-- Provides extra visual weight and identifies the primary action in a set of buttons -->
    <%--<button type="button" class="btn btn-primary" id="retrieve">媒体检索</button>--%>

    <%--    <!-- Indicates a successful or positive action -->
    <button type="button" class="btn btn-success">（成功）Success</button>

    <!-- Contextual button for informational alert messages -->
    <button type="button" class="btn btn-info">（一般信息）Info</button>

    <!-- Indicates caution should be taken with this action -->
    <button type="button" class="btn btn-warning">（警告）Warning</button>

    <!-- Indicates a dangerous or potentially negative action -->
    <button type="button" class="btn btn-danger">（危险）Danger</button>

    <!-- Deemphasize a button by making it look like a link while maintaining button behavior -->
    <button type="button" class="btn btn-link">（链接）Link</button>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="Server">
</asp:Content>
