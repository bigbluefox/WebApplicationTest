<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Watermark_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    水印测试
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

        $(function () {

        });

    </script>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
    
    <h2 class="page-header">水印测试</h2>

    <form id="form1" runat="server" style="padding-top: 10px;">
         来源文件：<asp:TextBox ID="txtSource" runat="server"></asp:TextBox>
         <br/><br/>
         目标文件：<asp:TextBox ID="txtTarget" runat="server"></asp:TextBox>
         <br/><br/>
        
        <asp:Button ID="btnGenerate" runat="server" Text="&nbsp; 生成水印文件 &nbsp;" OnClick="btnGenerate_Click"/>
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

