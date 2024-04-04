<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/BootStrap4.master" AutoEventWireup="true" CodeFile="Starter.aspx.cs" Inherits="Bootstrap_Starter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    BootStrap4 测试
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">

    <style type="text/css">
        
        </style>

    <script type="text/javascript">
        $(function() {
            //alert("Hello World!");
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContainerContent" Runat="Server">
    
    <style type="text/css">
        .figure img{ width: 273px;}
    </style>

<!-- Begin page content -->
<div class="container">
    
    <h1>Example heading <span class="badge badge-secondary">New</span></h1>
    <h2>Example heading <span class="badge badge-secondary">New</span></h2>
    <h3>Example heading <span class="badge badge-secondary">New</span></h3>
    <h4>Example heading <span class="badge badge-secondary">New</span></h4>
    <h5>Example heading <span class="badge badge-secondary">New</span></h5>
    <h6>Example heading <span class="badge badge-secondary">New</span></h6>    

    <button type="button" class="btn btn-primary">
      Notifications <span class="badge badge-light">4</span>
    </button>    

    <button type="button" class="btn btn-primary">
      Profile <span class="badge badge-light">9</span>
      <span class="sr-only">unread messages</span>
    </button>    
    
    <br/>
    <span class="badge badge-primary">Primary</span>
    <span class="badge badge-secondary">Secondary</span>
    <span class="badge badge-success">Success</span>
    <span class="badge badge-danger">Danger</span>
    <span class="badge badge-warning">Warning</span>
    <span class="badge badge-info">Info</span>
    <span class="badge badge-light">Light</span>
    <span class="badge badge-dark">Dark</span>

    <br/>

    <span class="badge badge-pill badge-primary">Primary</span>
    <span class="badge badge-pill badge-secondary">Secondary</span>
    <span class="badge badge-pill badge-success">Success</span>
    <span class="badge badge-pill badge-danger">Danger</span>
    <span class="badge badge-pill badge-warning">Warning</span>
    <span class="badge badge-pill badge-info">Info</span>
    <span class="badge badge-pill badge-light">Light</span>
    <span class="badge badge-pill badge-dark">Dark</span>
    <br/>

    <a href="#" class="badge badge-primary">Primary</a>
    <a href="#" class="badge badge-secondary">Secondary</a>
    <a href="#" class="badge badge-success">Success</a>
    <a href="#" class="badge badge-danger">Danger</a>
    <a href="#" class="badge badge-warning">Warning</a>
    <a href="#" class="badge badge-info">Info</a>
    <a href="#" class="badge badge-light">Light</a>
    <a href="#" class="badge badge-dark">Dark</a>
    <br/><br/>

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item active" aria-current="page">Home</li>
        </ol>
    </nav>

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Home</a>
            </li>
            <li class="breadcrumb-item active" aria-current="page">Library</li>
        </ol>
    </nav>

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Library</a>
            </li>
            <li class="breadcrumb-item active" aria-current="page">Data</li>
        </ol>
    </nav>

    <br/>  
    
 <button type="button" class="btn btn-primary">Primary</button>
<button type="button" class="btn btn-secondary">Secondary</button>
<button type="button" class="btn btn-success">Success</button>
<button type="button" class="btn btn-danger">Danger</button>
<button type="button" class="btn btn-warning">Warning</button>
<button type="button" class="btn btn-info">Info</button>
<button type="button" class="btn btn-light">Light</button>
<button type="button" class="btn btn-dark">Dark</button>

<button type="button" class="btn btn-link">Link</button>   
   <br/><br/> 


<a class="btn btn-primary" href="#" role="button">Link</a>
<button class="btn btn-primary" type="submit">Button</button>
<input class="btn btn-primary" type="button" value="Input">
<input class="btn btn-primary" type="submit" value="Submit">
<input class="btn btn-primary" type="reset" value="Reset">
<br/><br/>  


<button type="button" class="btn btn-outline-primary">Primary</button>
<button type="button" class="btn btn-outline-secondary">Secondary</button>
<button type="button" class="btn btn-outline-success">Success</button>
<button type="button" class="btn btn-outline-danger">Danger</button>
<button type="button" class="btn btn-outline-warning">Warning</button>
<button type="button" class="btn btn-outline-info">Info</button>
<button type="button" class="btn btn-outline-light">Light</button>
<button type="button" class="btn btn-outline-dark">Dark</button>
<br/> <br/> 

<div class="btn-group-toggle" data-toggle="buttons">
  <label class="btn btn-secondary active">
    <input type="checkbox" checked autocomplete="off"> Checked
  </label>
      <label class="btn btn-secondary active">
    <input type="checkbox" checked autocomplete="off"> Checked
  </label>
      <label class="btn btn-secondary active">
    <input type="checkbox" checked autocomplete="off"> Checked
  </label>
      <label class="btn btn-secondary active">
    <input type="checkbox" checked autocomplete="off"> Checked
  </label>
</div>

<br/>



<div class="btn-group btn-group-toggle" data-toggle="buttons">
  <label class="btn btn-secondary active">
    <input type="radio" name="options" id="option1" autocomplete="off" checked> Active
  </label>
  <label class="btn btn-secondary">
    <input type="radio" name="options" id="option2" autocomplete="off"> Radio
  </label>
  <label class="btn btn-secondary">
    <input type="radio" name="options" id="option3" autocomplete="off"> Radio
  </label>
      <label class="btn btn-secondary">
    <input type="radio" name="options" id="Radio1" autocomplete="off"> Radio
  </label>
      <label class="btn btn-secondary">
    <input type="radio" name="options" id="Radio2" autocomplete="off"> Radio
  </label>
</div>
<br/> <br/> 

    
<div class="btn-group" role="group" aria-label="Basic example">
  <button type="button" class="btn btn-secondary">Left</button>
  <button type="button" class="btn btn-secondary">Middle</button>
  <button type="button" class="btn btn-secondary">Right</button>
</div>    
    
<br/> <br/>     
    
    
 <div class="btn-toolbar" role="toolbar" aria-label="Toolbar with button groups">
  <div class="btn-group mr-2" role="group" aria-label="First group">
    <button type="button" class="btn btn-secondary">1</button>
    <button type="button" class="btn btn-secondary">2</button>
    <button type="button" class="btn btn-secondary">3</button>
    <button type="button" class="btn btn-secondary">4</button>
  </div>
  <div class="btn-group mr-2" role="group" aria-label="Second group">
    <button type="button" class="btn btn-secondary">5</button>
    <button type="button" class="btn btn-secondary">6</button>
    <button type="button" class="btn btn-secondary">7</button>
  </div>
  <div class="btn-group" role="group" aria-label="Third group">
    <button type="button" class="btn btn-secondary">8</button>
  </div>
</div>   
    
<br/> <br/>     
    
<div class="btn-toolbar mb-3" role="toolbar" aria-label="Toolbar with button groups">
  <div class="btn-group mr-2" role="group" aria-label="First group">
    <button type="button" class="btn btn-secondary">1</button>
    <button type="button" class="btn btn-secondary">2</button>
    <button type="button" class="btn btn-secondary">3</button>
    <button type="button" class="btn btn-secondary">4</button>
  </div>
  <div class="input-group">
    <div class="input-group-prepend">
      <div class="input-group-text" id="btnGroupAddon">@</div>
    </div>
    <input type="text" class="form-control" placeholder="Input group example" aria-label="Input group example" aria-describedby="btnGroupAddon">
  </div>
</div>

<div class="btn-toolbar justify-content-between" role="toolbar" aria-label="Toolbar with button groups">
  <div class="btn-group" role="group" aria-label="First group">
    <button type="button" class="btn btn-secondary">1</button>
    <button type="button" class="btn btn-secondary">2</button>
    <button type="button" class="btn btn-secondary">3</button>
    <button type="button" class="btn btn-secondary">4</button>
  </div>
  <div class="input-group">
    <div class="input-group-prepend">
      <div class="input-group-text" id="btnGroupAddon2">@</div>
    </div>
    <input type="text" class="form-control" placeholder="Input group example" aria-label="Input group example" aria-describedby="btnGroupAddon2">
  </div>
</div>    
<br/> <br/>        

    <style type="text/css">
        /*.card img{width: 287px;}*/
        .card img{ height: auto;}
    </style>

<div class="row">
    <div class="col-sm-3">

        <div class="card" style="width: 18rem;">
            <img class="card-img-top" src="/Images/5G.png" alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">Card title</h5>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>

        </div>
    </div>
    <div class="col-sm-3">
        <div class="card" style="width: 18rem;">
            <img class="card-img-top" src="/Images/5G.png" alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">Card title</h5>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>

        </div>

    </div>
    
    
    <div class="col-sm-3">

        <div class="card" style="width: 18rem;">
            <img class="card-img-top" src="/Images/5G.png" alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">Card title</h5>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>

        </div>
    </div>
    <div class="col-sm-3">
        <div class="card" style="width: 18rem;">
            <img class="card-img-top" src="/Images/5G.png" alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">Card title</h5>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>

        </div>

    </div>
        
    
    
    


</div>    

<br/> <br/> 

<div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">Card title</h5>
    <h6 class="card-subtitle mb-2 text-muted">Card subtitle</h6>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="#" class="card-link">Card link</a>
    <a href="#" class="card-link">Another link</a>
  </div>
</div>    
    

<div class="card text-center">
  <div class="card-header">
    Featured
  </div>
  <div class="card-body">
    <h5 class="card-title">Special title treatment</h5>
    <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
  <div class="card-footer text-muted">
    2 days ago
  </div>
</div>
    
    <div></div>

<div class="row">
  <div class="col-sm-4">
    <div class="card">
      <div class="card-body">
        <h5 class="card-title">Special title treatment</h5>
        <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
      </div>
    </div>
  </div>
  <div class="col-sm-4">
    <div class="card">
      <div class="card-body">
        <h5 class="card-title">Special title treatment</h5>
        <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
      </div>
    </div>
  </div>
    
      <div class="col-sm-4">
    <div class="card">
      <div class="card-body">
        <h5 class="card-title">Special title treatment</h5>
        <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
      </div>
    </div>
  </div>
</div>

<br/> <br/>  
    
 <div class="card text-center">
  <div class="card-header">
    <ul class="nav nav-tabs card-header-tabs">
      <li class="nav-item">
        <a class="nav-link active" href="#">Active</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Link</a>
      </li>
      <li class="nav-item">
        <a class="nav-link disabled" href="#">Disabled</a>
      </li>
    </ul>
  </div>
  <div class="card-body">
    <h5 class="card-title">Special title treatment</h5>
    <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>   
    
<br/> <br/>        

<div class="card mb-3">
  <img class="card-img-top" src="/Images/5G.png" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">Card title</h5>
    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
  </div>
</div>
    <br/> <br/>  
<div class="card">
  <div class="card-body">
    <h5 class="card-title">Card title</h5>
    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
  </div>
  <img class="card-img-bottom" src="/Images/5G.png" alt="Card image cap">
</div>

<br/> <br/>  

 <div class="card bg-dark text-white">
  <img class="card-img" src="/Images/5G.png" alt="Card image">
  <div class="card-img-overlay">
    <h5 class="card-title">Card title</h5>
    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
    <p class="card-text">Last updated 3 mins ago</p>
  </div>
</div>   
    
<br/> <br/>      
    
 <div class="card text-white bg-primary mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Primary card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-secondary mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Secondary card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-success mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Success card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Danger card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-warning mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Warning card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-info mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Info card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card bg-light mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Light card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card text-white bg-dark mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Dark card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>   
    
<br/> <br/>      
    
<div class="card border-primary mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-primary">
    <h5 class="card-title">Primary card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-secondary mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-secondary">
    <h5 class="card-title">Secondary card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-success mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-success">
    <h5 class="card-title">Success card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-danger mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-danger">
    <h5 class="card-title">Danger card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-warning mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-warning">
    <h5 class="card-title">Warning card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-info mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-info">
    <h5 class="card-title">Info card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-light mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body">
    <h5 class="card-title">Light card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
<div class="card border-dark mb-3" style="max-width: 18rem;">
  <div class="card-header">Header</div>
  <div class="card-body text-dark">
    <h5 class="card-title">Dark card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>
    
<br/> <br/>      
    
 <div class="card border-success mb-3" style="max-width: 18rem;">
  <div class="card-header bg-transparent border-success">Header</div>
  <div class="card-body text-success">
    <h5 class="card-title">Success card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
  <div class="card-footer bg-transparent border-success">Footer</div>
</div>   
    
<br/> <br/>      
    
    <h1>Card groups</h1>    
    
  <div class="card-group">
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
</div>  
    
    
<h1>Card groups</h1>
    
    
<div class="card-group">
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
</div>    

    
 <div class="card-group">
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
</div>   
    
     
    
 <h1>Card decks</h1>   
       
 <div class="card-deck">
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
      <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
    </div>
  </div>
</div>   
    
    <h1>Slides only</h1>    
    
<div id="carouselExampleSlidesOnly" class="carousel slide" data-ride="carousel">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="d-block w-100" src="/Images/5G.png" alt="First slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/Palace.jpg" alt="Second slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/fly.jpg" alt="Third slide">
    </div>
  </div>
</div>    

    <h1>With controls</h1>
    
<div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="d-block w-100" src="/Images/5G.png" alt="First slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/Palace.jpg" alt="Second slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/fly.jpg" alt="Third slide">
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>    
   
    <h1>With indicators</h1>
     
<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
  </ol>
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="d-block w-100" src="/Images/5G.png" alt="First slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/Palace.jpg" alt="Second slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="/Images/fly.jpg" alt="Third slide">
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
    
    
    <h1>Form row</h1>
    
 <form>
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputEmail4">Email</label>
      <input type="email" class="form-control" id="inputEmail4" placeholder="Email">
    </div>
    <div class="form-group col-md-6">
      <label for="inputPassword4">Password</label>
      <input type="password" class="form-control" id="inputPassword4" placeholder="Password">
    </div>
  </div>
  <div class="form-group">
    <label for="inputAddress">Address</label>
    <input type="text" class="form-control" id="inputAddress" placeholder="1234 Main St">
  </div>
  <div class="form-group">
    <label for="inputAddress2">Address 2</label>
    <input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
  </div>
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputCity">City</label>
      <input type="text" class="form-control" id="inputCity">
    </div>
    <div class="form-group col-md-4">
      <label for="inputState">State</label>
      <select id="inputState" class="form-control">
        <option selected>Choose...</option>
        <option>...</option>
      </select>
    </div>
    <div class="form-group col-md-2">
      <label for="inputZip">Zip</label>
      <input type="text" class="form-control" id="inputZip">
    </div>
  </div>
  <div class="form-group">
    <div class="form-check">
      <input class="form-check-input" type="checkbox" id="gridCheck">
      <label class="form-check-label" for="gridCheck">
        Check me out
      </label>
    </div>
  </div>
  <button type="submit" class="btn btn-primary">Sign in</button>
</form>   
    
    
    <h1>Custom styles</h1>
    
 <form class="needs-validation" novalidate>
  <div class="form-row">
    <div class="col-md-4 mb-3">
      <label for="validationCustom01">First name</label>
      <input type="text" class="form-control" id="validationCustom01" placeholder="First name" value="Mark" required>
      <div class="valid-feedback">
        Looks good!
      </div>
    </div>
    <div class="col-md-4 mb-3">
      <label for="validationCustom02">Last name</label>
      <input type="text" class="form-control" id="validationCustom02" placeholder="Last name" value="Otto" required>
      <div class="valid-feedback">
        Looks good!
      </div>
    </div>
    <div class="col-md-4 mb-3">
      <label for="validationCustomUsername">Username</label>
      <div class="input-group">
        <div class="input-group-prepend">
          <span class="input-group-text" id="inputGroupPrepend">@</span>
        </div>
        <input type="text" class="form-control" id="validationCustomUsername" placeholder="Username" aria-describedby="inputGroupPrepend" required>
        <div class="invalid-feedback">
          Please choose a username.
        </div>
      </div>
    </div>
  </div>
  <div class="form-row">
    <div class="col-md-6 mb-3">
      <label for="validationCustom03">City</label>
      <input type="text" class="form-control" id="validationCustom03" placeholder="City" required>
      <div class="invalid-feedback">
        Please provide a valid city.
      </div>
    </div>
    <div class="col-md-3 mb-3">
      <label for="validationCustom04">State</label>
      <input type="text" class="form-control" id="validationCustom04" placeholder="State" required>
      <div class="invalid-feedback">
        Please provide a valid state.
      </div>
    </div>
    <div class="col-md-3 mb-3">
      <label for="validationCustom05">Zip</label>
      <input type="text" class="form-control" id="validationCustom05" placeholder="Zip" required>
      <div class="invalid-feedback">
        Please provide a valid zip.
      </div>
    </div>
  </div>
  <div class="form-group">
    <div class="form-check">
      <input class="form-check-input" type="checkbox" value="" id="invalidCheck" required>
      <label class="form-check-label" for="invalidCheck">
        Agree to terms and conditions
      </label>
      <div class="invalid-feedback">
        You must agree before submitting.
      </div>
    </div>
  </div>
  <button class="btn btn-primary" type="submit">Submit form</button>
</form>

<script>
    // Example starter JavaScript for disabling form submissions if there are invalid fields
    (function () {
        'use strict';
        window.addEventListener('load', function () {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        }, false);
    })();
</script>   
    
    
    

    

    <h1>Responsive images</h1>
    <img src="/Images/5G.png" class="img-fluid" alt="Responsive image">
    
    <div style="clear: both;">&nbsp;</div>
    
    <%--Documentation and examples for displaying related images and text with the figure component in Bootstrap.--%>

    <figure class="figure">
      <img src="/Images/5G.png" class="figure-img img-fluid rounded" alt="A generic square placeholder image with rounded corners in a figure.">
      <figcaption class="figure-caption">A caption for the above image.</figcaption>
    </figure>
    <figure class="figure">
      <img src="/Images/5G.png" class="figure-img img-fluid rounded" alt="A generic square placeholder image with rounded corners in a figure.">
      <figcaption class="figure-caption text-center">A caption for the above image.</figcaption>
    </figure>    
     <figure class="figure">
      <img src="/Images/5G.png" class="figure-img img-fluid rounded" alt="A generic square placeholder image with rounded corners in a figure.">
      <figcaption class="figure-caption text-center">A caption for the above image.</figcaption>
    </figure> 
     <figure class="figure">
      <img src="/Images/5G.png" class="figure-img img-fluid rounded" alt="A generic square placeholder image with rounded corners in a figure.">
      <figcaption class="figure-caption text-right">A caption for the above image.</figcaption>
    </figure>     

</div>
    
    
    
    
    
    
    
    
    
    
    
    


 <div class="container">   
    <div class="alert alert-success" role="alert">
      <h4 class="alert-heading">Well done!</h4>
      <p>Aww yeah, you successfully read this important alert message. This example text is going to run a bit longer so that you can see how spacing within an alert works with this kind of content.</p>
      <hr>
      <p class="mb-0">Whenever you need to, be sure to use margin utilities to keep things nice and tidy.</p>
    </div>   
     
     <div class="alert alert-warning alert-dismissible fade show" role="alert">
      <strong>Holy guacamole!</strong> You should check in on some of those fields below.
      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
      
</div>

<div class="container-fluid">
    <h1>Image thumbnails</h1>
    <img src="/Images/5G.png" class="img-fluid" alt="Responsive image">
    <img src="/Images/5G.png" class="img-thumbnail" alt="Responsive image">
    
    <div style="clear: both;">&nbsp;</div>

    <h1>Aligning images</h1>
    
    <div class="thumbnails">

        <img src="/Images/5G.png" class="rounded float-left" alt="..." style="">
        <img src="/Images/5G.png" class="rounded float-left" alt="..." style="padding-left: 5px;">
        <img src="/Images/5G.png" class="rounded float-left" alt="..." style="padding-left: 5px;">
        <img src="/Images/5G.png" class="rounded float-left" alt="..." style="padding-left: 5px;">
        <img src="/Images/5G.png" class="rounded float-right" alt="..." style="">
    
    </div>
    
    <style type="text/css">
        .thumbnails img{width: 273.5px;}
    </style>
  

</div>

<div style="clear: both;">&nbsp;</div>

<div class="container" style="padding-top: 15px; margin-top: 15px;">

<h1>Tables Examples</h1>

<table class="table">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1></h1>

<table class="table table-dark">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1>Table head options</h1>

<table class="table">
    <thead class="thead-dark">
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<table class="table">
    <thead class="thead-light">
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<table class="table">
    <thead class="thead-dark">
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<table class="table">
    <thead class="thead-light">
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<h1>Striped rows</h1>

<table class="table table-striped">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<table class="table table-striped table-dark">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1>Bordered table</h1>

<table class="table table-bordered">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<table class="table table-bordered table-dark">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1>Hoverable rows</h1>

<table class="table table-hover">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<table class="table table-hover table-dark">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1>Small table</h1>

<table class="table table-sm">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>

<table class="table table-sm table-dark">
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td colspan="2">Larry the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1>Contextual classes</h1>

<table class="table table-sm table-dark">

    <!-- On rows -->
    <%--<tr class="bg-primary">...</tr>
<tr class="bg-success">...</tr>
<tr class="bg-warning">...</tr>
<tr class="bg-danger">...</tr>
<tr class="bg-info">...</tr>--%>

    <!-- On cells (`td` or `th`) -->
    <tr>
        <td class="bg-primary">...</td>
        <td class="bg-success">...</td>
        <td class="bg-warning">...</td>
        <td class="bg-danger">...</td>
        <td class="bg-info">...</td>
    </tr>
</table>


<h1>Captions</h1>

<table class="table">
    <caption>List of users</caption>
    <thead>
    <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
    </tr>
    </thead>
    <tbody>
    <tr>
        <th scope="row">1</th>
        <td>Mark</td>
        <td>Otto</td>
        <td>@mdo</td>
    </tr>
    <tr>
        <th scope="row">2</th>
        <td>Jacob</td>
        <td>Thornton</td>
        <td>@fat</td>
    </tr>
    <tr>
        <th scope="row">3</th>
        <td>Larry</td>
        <td>the Bird</td>
        <td>@twitter</td>
    </tr>
    </tbody>
</table>


<h1></h1>


<h1></h1>


</div>


<div class="container">
    <div class="row">
        <div class="col-sm">
            One of three columns
        </div>
        <div class="col-sm">
            One of three columns
        </div>
        <div class="col-sm">
            One of three columns
        </div>
    </div>
</div>


<div class="bd-example-container">
    <div class="bd-example-container-header"></div>
    <div class="bd-example-container-sidebar"></div>
    <div class="bd-example-container-body"></div>
</div>

<div class="container-fluid">
<div class="row flex-xl-nowrap">
<div class="col-12 col-md-3 col-xl-2 bd-sidebar">


<nav class="collapse bd-links" id="bd-docs-nav">
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/getting-started/introduction/">
        Getting started
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/getting-started/introduction/">
                Introduction
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/download/">
                Download
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/contents/">
                Contents
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/browsers-devices/">
                Browsers &amp; devices
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/javascript/">
                JavaScript
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/theming/">
                Theming
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/build-tools/">
                Build tools
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/webpack/">
                Webpack
            </a>
        </li><li>
            <a href="/docs/4.0/getting-started/accessibility/">
                Accessibility
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item active">
    <a class="bd-toc-link" href="/docs/4.0/layout/overview/">
        Layout
    </a>

    <ul class="nav bd-sidenav">
        <li class="active bd-sidenav-active">
            <a href="/docs/4.0/layout/overview/">
                Overview
            </a>
        </li><li>
            <a href="/docs/4.0/layout/grid/">
                Grid
            </a>
        </li><li>
            <a href="/docs/4.0/layout/media-object/">
                Media object
            </a>
        </li><li>
            <a href="/docs/4.0/layout/utilities-for-layout/">
                Utilities for layout
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/content/reboot/">
        Content
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/content/reboot/">
                Reboot
            </a>
        </li><li>
            <a href="/docs/4.0/content/typography/">
                Typography
            </a>
        </li><li>
            <a href="/docs/4.0/content/code/">
                Code
            </a>
        </li><li>
            <a href="/docs/4.0/content/images/">
                Images
            </a>
        </li><li>
            <a href="/docs/4.0/content/tables/">
                Tables
            </a>
        </li><li>
            <a href="/docs/4.0/content/figures/">
                Figures
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/components/alerts/">
        Components
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/components/alerts/">
                Alerts
            </a>
        </li><li>
            <a href="/docs/4.0/components/badge/">
                Badge
            </a>
        </li><li>
            <a href="/docs/4.0/components/breadcrumb/">
                Breadcrumb
            </a>
        </li><li>
            <a href="/docs/4.0/components/buttons/">
                Buttons
            </a>
        </li><li>
            <a href="/docs/4.0/components/button-group/">
                Button group
            </a>
        </li><li>
            <a href="/docs/4.0/components/card/">
                Card
            </a>
        </li><li>
            <a href="/docs/4.0/components/carousel/">
                Carousel
            </a>
        </li><li>
            <a href="/docs/4.0/components/collapse/">
                Collapse
            </a>
        </li><li>
            <a href="/docs/4.0/components/dropdowns/">
                Dropdowns
            </a>
        </li><li>
            <a href="/docs/4.0/components/forms/">
                Forms
            </a>
        </li><li>
            <a href="/docs/4.0/components/input-group/">
                Input group
            </a>
        </li><li>
            <a href="/docs/4.0/components/jumbotron/">
                Jumbotron
            </a>
        </li><li>
            <a href="/docs/4.0/components/list-group/">
                List group
            </a>
        </li><li>
            <a href="/docs/4.0/components/modal/">
                Modal
            </a>
        </li><li>
            <a href="/docs/4.0/components/navs/">
                Navs
            </a>
        </li><li>
            <a href="/docs/4.0/components/navbar/">
                Navbar
            </a>
        </li><li>
            <a href="/docs/4.0/components/pagination/">
                Pagination
            </a>
        </li><li>
            <a href="/docs/4.0/components/popovers/">
                Popovers
            </a>
        </li><li>
            <a href="/docs/4.0/components/progress/">
                Progress
            </a>
        </li><li>
            <a href="/docs/4.0/components/scrollspy/">
                Scrollspy
            </a>
        </li><li>
            <a href="/docs/4.0/components/tooltips/">
                Tooltips
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/utilities/borders/">
        Utilities
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/utilities/borders/">
                Borders
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/clearfix/">
                Clearfix
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/close-icon/">
                Close icon
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/colors/">
                Colors
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/display/">
                Display
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/embed/">
                Embed
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/flex/">
                Flex
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/float/">
                Float
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/image-replacement/">
                Image replacement
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/position/">
                Position
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/screenreaders/">
                Screenreaders
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/sizing/">
                Sizing
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/spacing/">
                Spacing
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/text/">
                Text
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/vertical-align/">
                Vertical align
            </a>
        </li><li>
            <a href="/docs/4.0/utilities/visibility/">
                Visibility
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/extend/approach/">
        Extend
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/extend/approach/">
                Approach
            </a>
        </li><li>
            <a href="/docs/4.0/extend/icons/">
                Icons
            </a>
        </li>
    </ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/migration/">
        Migration
    </a>

    <ul class="nav bd-sidenav"></ul>
</div>
<div class="bd-toc-item">
    <a class="bd-toc-link" href="/docs/4.0/about/overview/">
        About
    </a>

    <ul class="nav bd-sidenav">
        <li>
            <a href="/docs/4.0/about/overview/">
                Overview
            </a>
        </li><li>
            <a href="/docs/4.0/about/brand/">
                Brand
            </a>
        </li><li>
            <a href="/docs/4.0/about/license/">
                License
            </a>
        </li>
    </ul>
</div>
</nav>

</div>


<div class="d-none d-xl-block col-xl-2 bd-toc">
    <ul class="section-nav">
        <li class="toc-entry toc-h2">
            <a href="#containers">Containers</a>
        </li>
        <li class="toc-entry toc-h2">
            <a href="#responsive-breakpoints">Responsive breakpoints</a>
        </li>
        <li class="toc-entry toc-h2">
            <a href="#z-index">Z-index</a>
        </li>
    </ul>
</div>


<main class="col-12 col-md-9 col-xl-8 py-md-3 pl-md-5 bd-content" role="main">
<h1 class="bd-title" id="content">Overview</h1>
<p class="bd-lead">Components and options for laying out your Bootstrap project, including wrapping containers, a powerful grid system, a flexible media object, and responsive utility classes.</p>

<h2 id="containers">
    <div>Containers<a class="anchorjs-link " style="padding-left: 0.37em;" aria-label="Anchor" href="#containers" data-anchorjs-icon="#"></a>
    </div>
</h2>

<p>Containers are the most basic layout element in Bootstrap and are <strong>required when using our default grid system</strong>. Choose from a responsive, fixed-width container (meaning its <code class="highlighter-rouge">max-width</code> changes at each breakpoint) or fluid-width (meaning it’s <code class="highlighter-rouge">100%</code> wide all the time).
</p>

<p>While containers <em>can</em> be nested, most layouts do not require a nested container.
</p>

<div class="bd-example">
    <div class="bd-example-container">
        <div class="bd-example-container-header"></div>
        <div class="bd-example-container-sidebar"></div>
        <div class="bd-example-container-body"></div>
    </div>
</div>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-html" data-lang="html"><span class="nt">&lt;div</span> <span class="na">class=</span><span class="s">"container"</span><span class="nt">&gt;</span>
  <span class="c">&lt;!-- Content here --&gt;</span>
<span class="nt">&lt;/div&gt;</span></code></pre></figure>

<p>Use <code class="highlighter-rouge">.container-fluid</code> for a full width container, spanning the entire width of the viewport.
</p>

<div class="bd-example">
    <div class="bd-example-container bd-example-container-fluid">
        <div class="bd-example-container-header"></div>
        <div class="bd-example-container-sidebar"></div>
        <div class="bd-example-container-body"></div>
    </div>
</div>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-html" data-lang="html"><span class="nt">&lt;div</span> <span class="na">class=</span><span class="s">"container-fluid"</span><span class="nt">&gt;</span>
  ...
<span class="nt">&lt;/div&gt;</span></code></pre></figure>

<h2 id="responsive-breakpoints">
    <div>Responsive breakpoints<a class="anchorjs-link " style="padding-left: 0.37em;" aria-label="Anchor" href="#responsive-breakpoints" data-anchorjs-icon="#"></a>
    </div>
</h2>

<p>Since Bootstrap is developed to be mobile first, we use a handful of <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/Media_Queries/Using_media_queries">media queries</a> to create sensible breakpoints for our layouts and interfaces. These breakpoints are mostly based on minimum viewport widths and allow us to scale up elements as the viewport changes.
</p>

<p>Bootstrap primarily uses the following media query ranges—or breakpoints—in our source Sass files for our layout, grid system, and components.</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="c1">// Extra small devices (portrait phones, less than 576px)</span>
<span class="c1">// No media query since this is the default in Bootstrap</span>

<span class="c1">// Small devices (landscape phones, 576px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">576px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Medium devices (tablets, 768px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">768px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Large devices (desktops, 992px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">992px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Extra large devices (large desktops, 1200px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">1200px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<p>Since we write our source CSS in Sass, all our media queries are available via Sass mixins:</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">xs</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">sm</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">md</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">lg</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">xl</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Example usage:</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-up</span><span class="p">(</span><span class="n">sm</span><span class="p">)</span> <span class="p">{</span>
  <span class="nc">.some-class</span> <span class="p">{</span>
    <span class="nl">display</span><span class="p">:</span> <span class="nb">block</span><span class="p">;</span>
  <span class="p">}</span>
<span class="p">}</span></code></pre></figure>

<p>We occasionally use media queries that go in the other direction (the given screen size <em>or smaller</em>):
</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="c1">// Extra small devices (portrait phones, less than 576px)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">575</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Small devices (landscape phones, less than 768px)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">767</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Medium devices (tablets, less than 992px)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">991</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Large devices (desktops, less than 1200px)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">1199</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Extra large devices (large desktops)</span>
<span class="c1">// No media query since the extra-large breakpoint has no upper bound on its width</span></code></pre></figure>

<div class="bd-callout bd-callout-info">
    <p>Note that since browsers do not currently support <a href="https://www.w3.org/TR/mediaqueries-4/#range-context">range context queries</a>, we work around the limitations of <a href="https://www.w3.org/TR/mediaqueries-4/#mq-min-max"><code class="highlighter-rouge">min-</code> and <code class="highlighter-rouge">max-</code> prefixes</a> and viewports with fractional widths (which can occur under certain conditions on high-dpi devices, for instance) by using values with higher precision for these comparisons.
    </p>
</div>

<p>Once again, these media queries are also available via Sass mixins:</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="k">@include</span> <span class="nd">media-breakpoint-down</span><span class="p">(</span><span class="n">xs</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-down</span><span class="p">(</span><span class="n">sm</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-down</span><span class="p">(</span><span class="n">md</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-down</span><span class="p">(</span><span class="n">lg</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<p>There are also media queries and mixins for targeting a single segment of screen sizes using the minimum and maximum breakpoint widths.</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="c1">// Extra small devices (portrait phones, less than 576px)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">575</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Small devices (landscape phones, 576px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">576px</span><span class="p">)</span> <span class="nf">and</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">767</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Medium devices (tablets, 768px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">768px</span><span class="p">)</span> <span class="nf">and</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">991</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Large devices (desktops, 992px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">992px</span><span class="p">)</span> <span class="nf">and</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">1199</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>

<span class="c1">// Extra large devices (large desktops, 1200px and up)</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">1200px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<p>These media queries are also available via Sass mixins:</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="k">@include</span> <span class="nd">media-breakpoint-only</span><span class="p">(</span><span class="n">xs</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-only</span><span class="p">(</span><span class="n">sm</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-only</span><span class="p">(</span><span class="n">md</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-only</span><span class="p">(</span><span class="n">lg</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span>
<span class="k">@include</span> <span class="nd">media-breakpoint-only</span><span class="p">(</span><span class="n">xl</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<p>Similarly, media queries may span multiple breakpoint widths:</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="c1">// Example</span>
<span class="c1">// Apply styles starting from medium devices and up to extra large devices</span>
<span class="k">@media</span> <span class="p">(</span><span class="n">min-width</span><span class="o">:</span> <span class="m">768px</span><span class="p">)</span> <span class="nf">and</span> <span class="p">(</span><span class="n">max-width</span><span class="o">:</span> <span class="m">1199</span><span class="mi">.98px</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<p>The Sass mixin for targeting the same screen size range would be:</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="k">@include</span> <span class="nd">media-breakpoint-between</span><span class="p">(</span><span class="n">md</span><span class="o">,</span> <span class="n">xl</span><span class="p">)</span> <span class="p">{</span> <span class="nc">...</span> <span class="p">}</span></code></pre></figure>

<h2 id="z-index">
    <div>Z-index<a class="anchorjs-link " style="padding-left: 0.37em;" aria-label="Anchor" href="#z-index" data-anchorjs-icon="#"></a>
    </div>
</h2>

<p>Several Bootstrap components utilize <code class="highlighter-rouge">z-index</code>, the CSS property that helps control layout by providing a third axis to arrange content. We utilize a default z-index scale in Bootstrap that’s been designed to properly layer navigation, tooltips and popovers, modals, and more.
</p>

<p>These higher values start at an arbitrary number, high and specific enough to ideally avoid conflicts. We need a standard set of these across our layered components—tooltips, popovers, navbars, dropdowns, modals—so we can be reasonably consistent in the behaviors. There’s no reason we couldn’t have used <code class="highlighter-rouge">100</code>+ or <code class="highlighter-rouge">500</code>+.
</p>

<p>We don’t encourage customization of these individual values; should you change one, you likely need to change them all.</p>

<div class="bd-clipboard">
    <button title="" class="btn-clipboard" data-original-title="复制到剪贴板">复制</button>
</div><figure class="highlight">
    <pre><code class="language-scss" data-lang="scss"><span class="nv">$zindex-dropdown</span><span class="p">:</span>          <span class="m">1000</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-sticky</span><span class="p">:</span>            <span class="m">1020</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-fixed</span><span class="p">:</span>             <span class="m">1030</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-modal-backdrop</span><span class="p">:</span>    <span class="m">1040</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-modal</span><span class="p">:</span>             <span class="m">1050</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-popover</span><span class="p">:</span>           <span class="m">1060</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span>
<span class="nv">$zindex-tooltip</span><span class="p">:</span>           <span class="m">1070</span> <span class="o">!</span><span class="nb">default</span><span class="p">;</span></code></pre></figure>

<p>To handle overlapping borders within components (e.g., buttons and inputs in input groups), we use low single digit <code class="highlighter-rouge">z-index</code> values of <code class="highlighter-rouge">1</code>, <code class="highlighter-rouge">2</code>, and <code class="highlighter-rouge">3</code> for default, hover, and active states. On hover/focus/active, we bring a particular element to the forefront with a higher <code class="highlighter-rouge">z-index</code> value to show their border over the sibling elements.
</p>

</main>
</div>
</div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" Runat="Server">

</asp:Content>