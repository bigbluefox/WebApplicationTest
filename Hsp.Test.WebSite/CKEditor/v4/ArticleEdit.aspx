<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap.master" AutoEventWireup="true" CodeFile="ArticleEdit.aspx.cs" Inherits="CKEditor_v4_ArticleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
     CKEditor 4 文章编辑
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
    
    <link href="/Bootstrap/v3/css/sticky-footer-navbar.css" rel="stylesheet">
    <link href="/Bootstrap/v3/css/bootstrap-datetimepicker.min.css" rel="stylesheet">   

    <link href="/Styles/Base.css" rel="stylesheet">
    <link href="/Styles/main.css" rel="stylesheet">    
    
    <script src="CKEditor/ckeditor.js"></script>
    <script src="CKEditor/config.js"></script>

    <script src="/Scripts/Hsp.js"></script>
    <script src="/Scripts/Hsp.Common.js"></script>    

    <style type="text/css">
        body { background: #F5F6F8; }
        body > .container{padding: 15px 15px 0}
        .panel-default > .panel-heading { background-color: #68778e; }

        .heading-title {
            color: #fff;
            line-height: 20px;
            font-weight: bold;
        }

        form { padding: 10px 15px 0;}  
        
        .cke_dialog_ui_input_file{ height: 125px!important;}      
    </style>
    
    <script type="text/javascript">

        $(function () {

        });

    </script>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
 
    <div class="table-responsive panel panel-default">

        <!-- Default panel contents -->
        <div class="panel-heading" style="height: 42px;">
            <span class="heading-title">CKEditor 4 文章编辑 Bootstrap 测试『弹出模态窗体』</span>
            <span style="float: right;">
                <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-id="" data-target="#importModal" style="display: none;">
                    <span class="glyphicon glyphicon-import" aria-hidden="true"></span>标准文件导入
                </button>
                <button type="button" class="btn btn-default btn-sm" onclick="void(0);">
                    <span class="glyphicon glyphicon-floppy-disk" aria-hidden="true"></span>&nbsp;保存
                </button>
                <button type="button" class="btn btn-default btn-sm" onclick="Redirect('Bootstrap.aspx');">
                    <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"></span>&nbsp;返回
                </button>
            </span>
        </div>
    
        <form>
            <div class="form-group">
                <label for="txtTitle">文章标题：</label>
                <input type="text" class="form-control" id="txtTitle" value="<% = this.Article == null ? "" :this.Article.Title %>" placeholder="文章标题...">
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtAuthor">文章作者：</label>
                        <input type="text" class="form-control" id="txtAuthor" value="<% = this.Article == null ? "" :this.Article.Author %>" placeholder="文章作者...">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtSource">文章来源：</label>
                        <input type="text" class="form-control" id="txtSource" value="<% = this.Article == null ? "" :this.Article.Source %>" placeholder="文章来源...">
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label for="txtContent">文章内容：</label>
                <textarea class="form-control ckeditor" rows="3" id="txtContent" placeholder="文章内容..."><% = this.Article == null ? "" :this.Article.Content %></textarea>
            </div>
            <div class="form-group">
                <label for="txtAbstract">文章摘要：</label>
                <textarea class="form-control" rows="3" id="txtAbstract" placeholder="文章摘要..."><% = this.Article == null ? "" :this.Article.Abstract %></textarea>
            </div>
        </form>

</div> 
           
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">
        <div class="container">
        <p class="text-muted">&copy; 2000 ～ <% = DateTime.Now.ToString("yyyy") %> Haiyu Studio 保留所有权利 v1.0 </p>
    </div>    
</asp:Content>

