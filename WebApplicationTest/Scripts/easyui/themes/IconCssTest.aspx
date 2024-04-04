<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.Master" AutoEventWireup="true" CodeBehind="IconCssTest.aspx.cs" Inherits="Processist.MSO.Website.Content.EasyUI.themes.IconCssTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    站点图片数据初始化管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">

    <link href="/Scripts/easyui/themes/gray/easyui.min.css" rel="stylesheet">
    <link href="/Scripts/easyui/themes/icon.min.css" rel="stylesheet">

    <style type="text/css">
        .container { padding-top: 50px; }

        img {
            cursor: pointer;
            margin: 2px;
        }

        body { padding: 15px; }

        .icon {
            -ms-background-size: 16px 16px;
            background-size: 16px 16px;
            display: block;
            float: left;
            height: 16px;
            width: 16px;
        }

        .icons {
            display: block;
            float: left;
            overflow: hidden;
            width: 240px;
            cursor: pointer;
            border: 2px solid #fff;
        }

        .icons label {
            cursor: pointer;
        }

        #backtop {
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            background: #f9f9f9;
            border: 2px solid #ccc;
            border-radius: 3px;
            bottom: 15px;
            display: none;
            height: 208px;
            padding: 10px;
            position: fixed;
            right: 15px;
            width: 300px;
        }

        .checked { border: 2px solid #00bfff; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" runat="server">

    <h3> easyUI 1.8.8 图标样式测试 </h3>

    <%
        if (CssList == null) return;

        foreach (var css in CssList)
        {
            Response.Write("<div class='icons' onclick='CheckIcon(this);' title='" + css + "'><span class='icon " + css + "'></span><label>&nbsp; ." + css + "</label></div>");
        }
    %>

    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    <div id="backtop" style="display: block;">
        <form>
            <div class="form-group">
                <label for="txtIconId">图片编号：</label>
                <input type="text" class="form-control" id="txtIconId" placeholder="图片编号">
            </div>
            <div class="form-group">
                <label for="txtIconUrl">图片地址：</label>
                <input type="text" class="form-control" id="txtIconUrl" placeholder="图片地址">
            </div>
            <button type="button" id="btnCheckSelected" class="btn btn-primary">
                <span class="glyphicon glyphicon-th " aria-hidden="true"></span> &nbsp;选中图标
            </button>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SubScriptContent" runat="server">
    <script src="/Scripts/easyui/jquery.easyui.min.js"></script>
    <script src="/Scripts/easyui/locale/easyui-lang-zh_CN.min.js"></script>

    <script type="text/javascript">

        $(function() {
            $(".navbar-header a.navbar-brand:last-child").html("MSO 系统图标管理");

            var $nav = $("#navbar ul.navbar-nav");
            $('<li><a href="/Admin.aspx">工作台</a></li>').appendTo($nav);
            $('<li><a href="/Content/Images.aspx">站点图片数据初始化</a></li>').appendTo($nav);
            $('<li><a href="/Content/SpriteFewIcons.aspx">雪碧图少数图标</a></li>').appendTo($nav);
            $('<li><a href="/Content/SpriteMainIcons.aspx">雪碧图主要图标</a></li>').appendTo($nav);
            //$('<li><a href="/JQueryEasyUi/themes/IconCssTest.aspx">EasyUi图标 v1.2.3</a></li>').appendTo($nav);
            $('<li class="active"><a href="/Scripts/easyui/themes/IconCssTest.aspx">EasyUi图标 v1.8.8</a></li>').appendTo($nav);

        });

        function CheckIcon(img, id) {

            //alert($(img).attr("title") + " * " + id);
            if ($(img).hasClass("checked")) {
                $(img).removeClass("checked");
            } else {
                $(img).addClass("checked");
            }

            //$("#txtIconId").val($(img).attr("alt"));
            $("#txtIconUrl").val($(img).attr("title"));
        }

    </script>
</asp:Content>