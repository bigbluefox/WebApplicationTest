using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Medias_Videos : System.Web.UI.Page
{
    //默认视频媒体目录
    internal string DefalutPath = ConfigurationManager.AppSettings["DefalutVideo"] ?? "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

//BootstrapTable 常用方法：
//刷新表格：$table.bootstrapTable('refresh');
//获取选择的行：$table.bootstrapTable('getSelections');

//bootstrap-table相关参数的解释

//参数按照通俗的说法翻译出来，方便以后查找！
//url:表格数据源地址
//method：请求数据的方式，常见的get和post，这里要注意，之前我用get不顶用，改成post可以了，还有如果自己使用http-header解析发现是option方式（好像是跨域了0.0），添加一个dataType参数就可以了，get方式添加（dataType:‘jsonp’）,post方式就用（dataType:‘json’）。
//queryParams：传过去的额外参数。
//pagination：底部是否采用分页方式。
//sortOrder：排序方式，只能是asc或者desc
//striped：是否隔行变色。
//pageList：每页几行记录。
//showToggle：是否显示详细视图和列表视图的切换按钮
//clickToSelect：是否启用点击选中行
//showRefresh：是否显示刷新按
//sortable：是否采用排序
//cache：设置为true禁止AJAX缓存。
//showColumns：是否显示所有的列
//cardView：是否显示详细的视图
//detailView：是否显示父子表
//sidePagination：客户端或者服务器分页。
//pageNumber：首页设置为哪一页。
//pageSize：每一页的记录数
//toolbar：自定义工具栏，一般是.toolbar(class),#toolbar(id)
//search:是否显示搜索框
//searchOnEnterKey：回车键会触发搜索方法。
//contentType：发送到服务器的数据编码类型。
//height：表格的高度。
//列参数
//checkbox：复选框。
//field：每列的字段名称，要和数据库字段名称对应。
//title：定义列的名称，显示在表格上面的。
