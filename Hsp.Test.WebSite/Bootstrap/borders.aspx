<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap4.master" AutoEventWireup="true" CodeFile="borders.aspx.cs" Inherits="Bootstrap_borders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    borders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <link href="https://cdn.bootcss.com/docsearch.js/2.4.1/docsearch.min.css" rel="stylesheet">
    <link href="v4/css/docs.min.css" rel="stylesheet"/>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">

    <!-- Begin page content -->
    <div class="container">

        <h1>container</h1>
        <div class="bd-example-border-utils">
            <div class="bd-example">
                <span class="border"></span>
                <span class="border-top"></span>
                <span class="border-right"></span>
                <span class="border-bottom"></span>
                <span class="border-left"></span>
            </div>
            <br/>
            <div class="bd-example">
                <span class="border"></span>
                <span class="border-top"></span>
                <span class="border-right"></span>
                <span class="border-bottom"></span>
                <span class="border-left"></span>
            </div>
            <br/>
            <div class="bd-example">
                <span class="border-0"></span>
                <span class="border-top-0"></span>
                <span class="border-right-0"></span>
                <span class="border-bottom-0"></span>
                <span class="border-left-0"></span>    
            </div>
            <br/>
            <div class="bd-example">
                <span class="border border-primary"></span>
                <span class="border border-secondary"></span>
                <span class="border border-success"></span>
                <span class="border border-danger"></span>
                <span class="border border-warning"></span>
                <span class="border border-info"></span>
                <span class="border border-light"></span>
                <span class="border border-dark"></span>
                <span class="border border-white"></span> 
            </div>
        </div>

    </div>

    <div class="container-fluid">
        <h1>container-fluid</h1>
        <div class="bd-example">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-top">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-right">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-bottom">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-left">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-circle">
            <img src="/images/user-signaturephoto.jpg" alt="..." class="rounded-0">
        </div>

        <%--
        <div class="bd-example">
            <img src="/images/5g.png" alt="..." class="rounded-circle">
            <img src="/images/5g.png" alt="..." class="rounded-circle">
            <img src="/images/5g.png" alt="..." class="rounded-circle">
            <img src="/images/5g.png" alt="..." class="rounded-circle">
        </div>
        <img src="/images/5g.png" alt="" />--%>

    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>