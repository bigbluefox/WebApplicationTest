<%@ Page Title="" validateRequest="false" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Crawl.aspx.cs" Inherits="SQLite_Crawl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    页面内容抓取
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>

    <style type="text/css">
        input[type='text'], textarea { width: 900px; }
    </style>

    <script type="text/javascript">

        $(function() {

        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">

    <form id="form1" runat="server" style="padding-top: 10px;">
        <asp:Button ID="btnSohuCrawl" runat="server" Text="&nbsp; 搜狐页面内容抓取 &nbsp;" OnClick="btnSohuCrawl_Click"/>
        <asp:Button ID="btnMydrivers" runat="server" Text="&nbsp; 驱动之家内容抓取 &nbsp;" OnClick="btnMydrivers_Click"/>
        <br/><br/>
        <asp:TextBox ID="txtContent" runat="server" Rows="18" TextMode="MultiLine"></asp:TextBox>
         <br/><br/>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    </form>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
    
    <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>

</asp:Content>