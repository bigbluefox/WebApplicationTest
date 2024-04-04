<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="ArticlePreview.aspx.cs" Inherits="CKEditor_v4_ArticlePreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    CKEditor 4 文章预览
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">
    
    <link rel="stylesheet" href="/Bootstrap/v3/css/font-awesome-site.css">
    <link rel="stylesheet" href="/Bootstrap/v3/css/pygments.css">
    <link rel="stylesheet" href="/Bootstrap/v3/css/font-awesome.css">

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

    <div class="table-responsive panel panel-default">

        <!-- Default panel contents -->
        <div class="panel-heading" style="height: 42px;">
            <span class="heading-title">文章预览 </span>
            <span style="float: right;">
                <button type="button" class="btn btn-default btn-sm" onclick="Redirect('<% = ReturnUrl %>');">
                    <span class="fa fa-mail-reply" aria-hidden="true"></span>&nbsp; 返回
                </button>
            </span>
        </div>

        <div class="blog-header" style="margin: 0 15px; overflow: hidden;">
            <h1 class="blog-title"><% = Article.Title %></h1>
            <p class="lead blog-description"><% = Article.Abstract %></p>
        </div>

        <div class="row" style="margin: 0 5px;">

            <div class="col-sm-8 blog-main">

                <div class="blog-post">
                    <%--<h2 class="blog-post-title">Another blog post　　</h2>--%>
                    <p class="blog-post-meta">
                        <% = Article.CreateTime.ToString("yyyyy-MM-dd HH:mm:ss") %> <a href="#"><% = Article.Author %></a>
                    </p>

                    <% = Article.Content %>
                </div><!-- /.blog-post -->

                <nav>
                    <ul class="pager">
                        <li>
                            <a href="#">Previous</a>
                        </li>
                        <li>
                            <a href="#">Next</a>
                        </li>
                    </ul>
                </nav>

            </div><!-- /.blog-main -->

            <div class="col-sm-3 col-sm-offset-1 blog-sidebar">
                <div class="sidebar-module sidebar-module-inset">
                    <h4>About</h4>
                    <p>Etiam porta <em>sem malesuada magna</em> mollis euismod. Cras mattis consectetur purus sit amet fermentum. Aenean lacinia bibendum nulla sed consectetur.
                    </p>
                </div>
                <div class="sidebar-module">
                    <h4>Archives</h4>
                    <ol class="list-unstyled">
                        <li>
                            <a href="#">March 2014</a>
                        </li>
                        <li>
                            <a href="#">February 2014</a>
                        </li>
                        <li>
                            <a href="#">January 2014</a>
                        </li>
                        <li>
                            <a href="#">December 2013</a>
                        </li>
                        <li>
                            <a href="#">November 2013</a>
                        </li>
                        <li>
                            <a href="#">October 2013</a>
                        </li>
                        <li>
                            <a href="#">September 2013</a>
                        </li>
                        <li>
                            <a href="#">August 2013</a>
                        </li>
                        <li>
                            <a href="#">July 2013</a>
                        </li>
                        <li>
                            <a href="#">June 2013</a>
                        </li>
                        <li>
                            <a href="#">May 2013</a>
                        </li>
                        <li>
                            <a href="#">April 2013</a>
                        </li>
                    </ol>
                </div>
                <div class="sidebar-module">
                    <h4>Elsewhere</h4>
                    <ol class="list-unstyled">
                        <li>
                            <a href="#">GitHub</a>
                        </li>
                        <li>
                            <a href="#">Twitter</a>
                        </li>
                        <li>
                            <a href="#">Facebook</a>
                        </li>
                    </ol>
                </div>
            </div><!-- /.blog-sidebar -->

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
    <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>
</asp:Content>