<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplicationTest.ES.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script type="text/javascript">
        
        function arrTest() {

            debugger;

            var operate = [], opinion = [];

            if (true) {
                operate = [
                    '<a title="已收藏标准">',
                    '<i class="glyphicon glyphicon-star"></i>',
                    '</a>  '
                ];
            } else {
                operate = [
                    '<a class="collect" href="javascript:void(0)" title="收藏标准">',
                    '<i class="glyphicon glyphicon-star-empty"></i>',
                    '</a>  '
                ];
            }

            if (true) {
                opinion = [
                    '<a title="已提出建议">',
                    '<i class="fa fa-sticky-note"></i>',
                    '</a>'
                ];
            } else {
                opinion = [
                    '<a class="collect" href="javascript:void(0)" title="提出建议">',
                    '<i class="fa fa-sticky-note-o"></i>',
                    '</a>'
                ];
            }

            var d = operate.concat(opinion);

            console.log(d);


    //        var arr = [
    //'<a class="edit" href="javascript:void(0)" title="修改课程">',
    //'<i class="glyphicon glyphicon-edit"></i>',
    //'</a>  ',
    //'<a class="remove" href="javascript:void(0)" title="删除课程">',
    //'<i class="glyphicon glyphicon-remove"></i>',
    //'</a>  ',
    //'<a class="add-courseware" href="javascript:void(0)" title="添加课件">',
    //'<i class="glyphicon glyphicon-plus"></i>',
    //'</a>  '
    //, '<a class="add-exam" href="javascript:void(0)" title="课程考试">',
    //'<i class="glyphicon icon-notes"></i>',
    //'</a>  '
    //        ];
    //        var conArr = [
    //            '<a class="publish-course" href="javascript:void(0)" title="发布课程">',
    //            '<i class="glyphicon glyphicon-tasks"></i>',
    //            '</a>'
    //        ];

    //        console.log(arr.concat(conArr));


            return false;
        }


    </script>
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" OnClientClick="arrTest();" />
    </div>
    </form>
</body>
</html>
