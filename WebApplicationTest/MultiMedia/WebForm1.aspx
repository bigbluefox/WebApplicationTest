<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplicationTest.MultiMedia.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script src="/Scripts/jquery-1.12.4.min.js"></script>
    <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>

    <script type="text/javascript">

        $(function() {

            $("#btnCheck").unbind("click").bind("click", function() {
                FileValue(document.getElementById("File1"));
            });

            $("#btnCheck").attr("disabled", "disabled");
        });

        function FileValue(val) {

            //debugger;

            var file = val.files[0];
            var url = URL.createObjectURL(file);
            $("#myvideo").prop("src", url);
            $("#myvideo")[0].addEventListener("loadedmetadata", function() {
                //var tol = this.duration; //获取总时长
                //alert("时长：" + tol);
                //alert("宽度：" + this.videoWidth);
                //alert("高度：" + this.videoHeight);

                var s = "宽度：" + this.videoWidth + "，";
                s += "高度：" + this.videoHeight + "，";
                s += "时长：" + this.duration + "";

                $("#lblMsg").html("视频规格：" +s);
            });
        }

        function CheckFile(obj) {

            //debugger;

            if (obj.files.length > 0) {
                $("#btnCheck").removeAttr("disabled");
            }
            //"video/mpeg"

            alert(obj.files.length);
        }


    </script>

</head>
<body>
<form id="form1" runat="server">
    <div>
        <input id="File1" type="file" onchange="javaScript:CheckFile(this);"/><br /><br />

        <input id="btnCheck" type="button" value="检查视频信息"/><br /><br />

        <video id="myvideo" controls="controls" preload="auto">
            <p>This browser does not support HTML5 video</p>
        </video>
        <br /><br />
        <label id="lblMsg"></label>
        <br /><br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

    </div>
</form>
</body>

</html>