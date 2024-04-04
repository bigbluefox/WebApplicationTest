<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WinLayout.aspx.cs" Inherits="WebApplicationTest.WinLayout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style type="text/css">
        /*
         * 公共对话框样式定义    
        */

        .window-div { background-color: #f1f1f1; }

        .content-div {
            background-color: #fbfbfb;
            margin: 2px;
            overflow: hidden;
            border: 1px solid silver;
            
        }

        a:last-child{}

        .btn-div {
            margin-top: 0px;
            height: 24px;
            line-height: 24px;
            overflow: hidden;
            text-align: right;
        }

        .row-div {
            margin-left: 8px;
            font-family: 微软雅黑;
            font-size: 12px;
            margin-top: 3px;
            display: block;
            float: left;
        }

        /*height:22px;*/

        .two-columns-input-item { width: 180px; }

        .two-columns-left-span {
            display: table;
            width: 300px;
            vertical-align: middle;
            line-height: 22px;
            float: left;
        }

        .two-columns-left-label {
            float: left;
            font-weight: bold;
            width: 80px;
            text-align: right;
        }

        .two-columns-left-item {
            float: left;
            display: block;
            width: 220px;
            height: 22px;
            line-height: 22px;
            vertical-align: middle;
            text-align: left;
        }

        .two-columns-right-span {
            display: table;
            width: 300px;
            vertical-align: middle;
            line-height: 22px;
            float: right;
        }

        .two-columns-right-label {
            float: left;
            font-weight: bold;
            width: 80px;
            text-align: right;
        }

        .two-columns-right-item {
            float: left;
            width: 220px;
            vertical-align: middle;
            text-align: left;
        }

        .two-columns-whole-item {
            float: left;
            width: 520px;
            vertical-align: middle;
            line-height: 22px;
            text-align: left;
        }

        .two-columns-whole-item img, .two-columns-left-item img, .two-columns-right-item img {
            margin-top: 3px;
            margin-left: 2px;
            cursor: pointer;
        }

        .selectcommon-div {
            height: 25px;
            margin: -10px 2px 5px 2px;
        }

        .selectcommomtree-div {
            width: 381px;
            height: 271px;
            overflow: auto;
        }

        .selectcommon-input {
            width: 120px;
            height: 24px;
            float: left;
        }

        .selectcommomtree-div-width {
            width: 421px;
            height: 271px;
            overflow: auto;
        }

        .properties-row-div {
            width: 610px;
            height: 22px;
            margin-left: 8px;
            font-family: 微软雅黑;
            font-size: 12px;
            margin-top: 3px;
            display: block;
        }

    </style>

</head>
<body>
<form id="form1" runat="server">
    <div style="width: 625px; border: 1px solid red;">

        <div id="editor" class="window-div" >

            <div class="content-div">
                <div class="row-div">
                    <span class="two-columns-left-label"><span style="color: red;">* </span>风险名称：</span>
                    <span class="two-columns-left-item"><input id="txtRiskName" /></span>
                    <span class="two-columns-right-label">风险编号：</span>
                    <span class="two-columns-right-item"><input id="txtRiskCode" /></span>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">风险类别：</span>
                    <div class="two-columns-whole-item">
                        <input type="text" id="PName" readonly="readonly" style="width: 490px;"/>
                        <img id="btnRiskCategorySelect" title="选择风险类别" src="/Content/images/properties.gif"/>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">风险描述：</span>
                    <div class="two-columns-whole-item">
                        <textarea name="txtRiskDesc" maxlength="999" id="txtRiskDesc" style="height: 75px;"></textarea>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">风险影响力：</span>
                    <div class="two-columns-left-item">
                        <select id="selRiskProperty" onchange="GetRiskValue();"></select>
                    </div>
                    <span class="two-columns-right-label">发生可能性：</span>
                    <div class="two-columns-right-item">
                        <select id="selPossibility" onchange="GetRiskValue();"></select>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">风险值：</span>
                    <div class="two-columns-left-item">
                        <input id="txtRiskValue" onkeypress=" return (/[\d.]/.test(String.fromCharCode(event.keyCode))) " style="ime-mode: Disabled" readonly="readonly"/>
                    </div>
                    <span class="two-columns-right-label">排序号：</span>
                    <div class="two-columns-right-item">
                        <input id="txtSequenceNumber" onkeypress=" return (/[\d.]/.test(String.fromCharCode(event.keyCode))) " style="ime-mode: Disabled"/>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">风险等级：</span>
                    <div class="two-columns-left-item">
                        <select id="selRiskRating" disabled="disabled"></select>
                    </div>
                    <span class="two-columns-right-label"><span style="display: none; width: 80px;">应对策略：</span></span>
                    <div class="two-columns-right-item">
                        <select id="selStrategy" style="display: none"></select>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">控制目标：</span>
                    <div class="two-columns-whole-item">
                        <textarea name="txtControlTarget" maxlength="999" style="height: 90px;" id="txtControlTarget"></textarea>
                    </div>
                </div>

                <div class="row-div">
                    <span class="two-columns-left-label">控制频率：</span>
                    <div class="two-columns-left-item">
                        <select id="selControlFrequency"></select>
                    </div>
                    <span class="two-columns-right-label">控制类型：</span>
                    <div class="two-columns-right-item">
                        <select id="selControlType"></select>
                    </div>
                </div>
                <div class="row-div">
                    <span class="two-columns-left-label">控制措施：</span>
                    <div class="two-columns-whole-item">
                        <textarea name="txtControlMeasures" maxlength="999" style="height: 90px;" id="txtControlMeasures"></textarea>
                    </div>
                </div>
                <div class="row-div" style="display: none;">
                    <span class="two-columns-left-label">控制点要求：</span>
                    <div class="two-columns-whole-item">
                        <textarea name="txtControlPointRequirements" maxlength="999" style="height: 90px;" id="txtControlPointRequirements"></textarea>
                    </div>
                </div>
            </div>
            <div class="btn-div" style="padding-top: 10px; text-align: right;">
                <input type="button" id="btnSave" style="width: 74px;" value="确定"/>
                <input type="button" id="btnExit" style="width: 74px;" value="关闭"/> 
                            <input type="hidden" id="txtID"/>
                <input id="txtControlPointCode" style="display: none;"/>
                <select id="selIsKeyPoint" style="display: none;">
                    <option value="0" selected="selected">否</option>
                </select>
            </div>
        </div>

    </div>
</form>
</body>
</html>