<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CombGrid.aspx.cs" Inherits="WebApplicationTest.CombGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Scripts/themes/default/easyui.css" rel="stylesheet"/>
    <link href="Scripts/themes/icon.css" rel="stylesheet"/>
    <link href="Scripts/themes/mobile.css" rel="stylesheet"/>
    <link href="Styles/main.css" rel="stylesheet"/>

    <%--<script src="Scripts/jquery-2.2.4.min.js"></script>--%>
    <script src="Scripts/jquery-1.12.4.min.js"></script>
    <script src="Scripts/jquery-migrate-1.4.1.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery.easyui.mobile.js"></script>
    <script src="Scripts/locale/easyui-lang-zh_CN.js"></script>
    
    <style type="text/css">
        body{ padding: 5px;margin: 5px;}
    </style>
    
    <script type="text/javascript">
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="margin:0 0 5px 0">
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="getValue()">GetValue</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="setValue1()">SetValue1</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="setValue2()">SetValue2</a>
    </div>

    <div class="easyui-panel" style="width:100%;max-width:400px;padding:10px 20px;">
        <div style="margin-bottom:20px">
            <select id="cc" class="easyui-combogrid" style="width:100%" data-options="
                    panelWidth: 450,
                    idField: 'itemid',
                    textField: 'productname',
                    url: 'datagrid_data1.json',
                    method: 'get',
                    columns: [[
                        {field:'itemid',title:'Item ID',width:80},
                        {field:'productname',title:'Product',width:120},
                        {field:'listprice',title:'List Price',width:80,align:'right'},
                        {field:'unitcost',title:'Unit Cost',width:80,align:'right'},
                        {field:'attr1',title:'Attribute',width:200},
                        {field:'status',title:'Status',width:60,align:'center'}
                    ]],
                    fitColumns: true,
                    label: 'Select Item:',
                    labelPosition: 'top'
                ">
            </select>
        </div>
    </div>

    <script type="text/javascript">
        function getValue() {
            var val = $('#cc').combogrid('getValue');
            alert(val);
        }
        function setValue1() {
            $('#cc').combogrid('setValue', 'EST-13');
        }
        function setValue2() {
            $('#cc').combogrid('setValue', {
                itemid: 'customid',
                productname: 'CustomName'
            });
        }
    </script>
        
 <div class="easyui-panel" style="width:100%;max-width:400px;padding:10px 20px; margin-top: 5px;">
		<div style="margin-bottom:20px">
		    <input id="Text1" style="width: 100%;"/>
		</div>
	</div>
	<div id="sp">
		<div style="line-height:22px;background:#fafafa;padding:5px;">Select a language</div>
		<div style="padding:10px">
		    <input type="checkbox" name="lang" value="01"/><span>Java</span><br/>
		    <input type="checkbox" name="lang" value="02"/><span>C#</span><br/>
		    <input type="checkbox" name="lang" value="03"/><span>Ruby</span><br/>
		    <input type="checkbox" name="lang" value="04"/><span>Basic</span><br/>
		    <input type="checkbox" name="lang" value="05"/><span>Fortran</span>
		</div>
	</div>
	<script type="text/javascript">
	    $(function () {
	        $('#Text1').combo({
	            required: true,
	            editable: false,
	            label: 'Language:',
	            labelPosition: 'top'
	        });
	        $('#sp').appendTo($('#Text1').combo('panel'));
	        $('#sp input').click(function () {
	            var v = "";
	            var s = "";
	            $.each($('#sp :checkbox:checked'), function () {
	                v += $(this).val() + ",";
	                s += $(this).next('span').text() + ",";
	            });
	            //var v = $(this).val();
	            //var s = $(this).next('span').text();

	            $('#Text1').combo('setValue', v).combo('setText', s).combo('hidePanel');
	        });
	    });
	</script>

    </div>
    </form>
</body>
</html>
