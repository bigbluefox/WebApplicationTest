<%@ Page Title="" validateRequest="false" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="ContentCrawl.aspx.cs" Inherits="SQLite_ContentCrawl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    页面内容抓取
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        
    </style>

    <script type="text/javascript">

        $(function() {

        });

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">

    <form id="form1" runat="server">

        <div class="row">

            <button type="button" class="btn btn-default btn-sm" onclick="void(0);" style="display: none;">
                <span class="glyphicon glyphicon-floppy-disk" aria-hidden="true"></span>&nbsp;保存
            </button>

            <style type="text/css">
                input[type='text'], textarea { width: 600px; }
            </style>

            抓取：<asp:TextBox ID="txtCrawl" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
            <br/><br/>
            标题：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <br/><br/>
            来源：<asp:TextBox ID="txtSource" runat="server"></asp:TextBox>
            <br/><br/>
            内容：<asp:TextBox ID="txtContent" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
            <br/><br/>
            <asp:Button ID="btnCrawl" runat="server" Text="&nbsp; 页面内容抓取 &nbsp;" OnClick="btnCrawl_Click"/>

            <br/><br/>
        </div>

    </form>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">

    <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>