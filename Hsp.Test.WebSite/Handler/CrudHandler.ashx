<%@ WebHandler Language="C#" Class="CrudHandler" %>

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Hsp.Test.IService;
using Hsp.Test.Model;
using Hsp.Test.Service;

public class CrudHandler : IHttpHandler {

    /// <summary>
    /// 用户服务
    /// </summary>
    internal readonly IUserService UserService = new UserService();

    #region ProcessRequest

    /// <summary>
    /// ProcessRequest
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        //context.Response.ContentType = "application/json";
        //context.Response.ContentType = "text/javascript";
        context.Response.ContentType = "text/plain";
        //context.Response.Cache.SetNoStore();

        string strOperation = context.Request.Params["OPERATION"] ?? context.Request.Params["OP"];

        switch (strOperation.ToUpper())
        {
            // 获取用户信息
            case "GETUSER":
                GetUser(context);
                break;

            // 根据编号获取用户信息
            case "GETUSERBYID":
                GetUserById(context);
                break;

            // 获取用户表单信息
            case "SHOWFORM":
                ShowUser(context);
                break;

            // 保存用户信息
            case "SAVEUSER":
                SaveUser(context);
                break;

            // 更新用户信息
            case "UPDATEUSER":
                UpdateUser(context);
                break;

            // 删除用户信息
            case "DESTROYUSER":
                DeleteUser(context);
                break;

            default:
                break;
        }

    }

    #endregion

    #region 获取用户信息

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="context"></param>
    private void GetUser(HttpContext context)
    {
        var s = "";
        foreach (var name in context.Request.Form)
        {
            s += name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
        }

        var a = s;
        s = "";

        foreach (var name in context.Request.Params)
        {
            s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        }

        var b = s; // 1235

        //var strFileId = context.Request.Params["FID"];
        //var strGroupId = context.Request.Params["GID"];
        //var strTypeId = context.Request.Params["TID"];

        //if (string.IsNullOrEmpty(strGroupId)) strGroupId = "1235";
        //if (string.IsNullOrEmpty(strTypeId)) strTypeId = AttachmentType.Workflow;

        //if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) && string.IsNullOrWhiteSpace(strTypeId))
        //{
        //    return;
        //}
        
        //page = 1
        //rows = 10

        var pageIndex = context.Request.Form["page"] ?? "1";
        var pageSize = context.Request.Form["rows"] ?? "10";
        
        var paramList = new Dictionary<string, string> 
        {
            {"pageSize", pageSize},
            {"pageIndex", pageIndex}
        };

        List<User> list = UserService.GetUserList(paramList);
        var js = new JavaScriptSerializer().Serialize(list);
        var rst = "{\"success\":true,\"total\":" + list[0].count + ",\"rows\":" + js + "}";

        context.Response.Write(rst);
    }

    #endregion

    #region 根据编号获取用户信息

    /// <summary>
    /// 根据编号获取用户信息
    /// </summary>
    /// <param name="context"></param>
    private void GetUserById(HttpContext context)
    {
        //var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + Environment.NewLine;
        //}

        //var b = s; // 1235

        string strId = context.Request.Params["id"].Trim() ?? "0";

        User user = UserService.GetUserById(Int32.Parse(strId));
        var row = new JavaScriptSerializer().Serialize(user);
        var rst = "{\"success\":true,\"total\":" + "" + ",\"row\":" + row + "}";

        context.Response.Write(rst);
    }

    #endregion


    #region 显示用户信息

    /// <summary>
    /// 显示用户信息
    /// </summary>
    /// <param name="context"></param>
    private void ShowUser(HttpContext context)
    {
        //var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s; // 1235

        //var strFileId = context.Request.Params["FID"];
        //var strGroupId = context.Request.Params["GID"];
        //var strTypeId = context.Request.Params["TID"];

        //if (string.IsNullOrEmpty(strGroupId)) strGroupId = "1235";
        //if (string.IsNullOrEmpty(strTypeId)) strTypeId = AttachmentType.Workflow;

        //if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId) && string.IsNullOrWhiteSpace(strTypeId))
        //{
        //    return;
        //}
        //var paramList = new Dictionary<string, string> { };
        //{
        //    {"FID", strFileId},
        //    {"GID", strGroupId},
        //    {"TID", strTypeId}
        //};

        //List<User> list = UserService.GetUserList(paramList);
        //var js = new JavaScriptSerializer().Serialize(list);
        //var rst = "{\"success\":true,\"total\":" + list.Count + ",\"rows\":" + js + "}";

        var index = context.Request.Params["index"];

        var sb = new StringBuilder();

        sb.Append("<form method=\"post\">");
        sb.Append("	<table class=\"dv-table\" style=\"width:100%;border:1px solid #ccc;padding:5px;margin-top:5px;\">");
        sb.Append("		<tbody><tr>");
        sb.Append("			<td>First Name</td>");
        sb.Append("			<td><input name=\"firstname\" class=\"easyui-validatebox validatebox-text\" required=\"true\"></td>");
        sb.Append("			<td>Last Name</td>");
        sb.Append("			<td><input name=\"lastname\" class=\"easyui-validatebox validatebox-text\" required=\"true\"></td>");
        sb.Append("		</tr>");
        sb.Append("		<tr>");
        sb.Append("			<td>Phone</td>");
        sb.Append("			<td><input name=\"phone\" class=\"easyui-validatebox validatebox-text\" required=\"true\"></td>");
        sb.Append("			<td>Email</td>");
        sb.Append("			<td><input name=\"email\" class=\"easyui-validatebox validatebox-text\" required=\"true\" validtype=\"email\"></td>");
        sb.Append("		</tr>");
        sb.Append("	</tbody></table>");
        sb.Append("	<div style=\"padding:5px 0;text-align:right;padding-right:30px\">");
        sb.Append("		<a onclick=\"save1(this)\" href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-save'\">保存</a>");
        sb.Append("		<a onclick=\"cancel1(this)\" href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-cancel'\">取消</a>");
        sb.Append("	</div>");
        sb.Append("</form>");
        
        //sb.Append("<script type=\"text/javascript\">");
        //sb.Append("	function save1(target){");
        //sb.Append("		var tr = $(target).closest('.datagrid-row-detail').closest('tr').prev();");
        //sb.Append("		var index = parseInt(tr.attr('datagrid-row-index'));");
        //sb.Append("		saveItem(index);");
        //sb.Append("	}");
        //sb.Append("	function cancel1(target){");
        //sb.Append("		var tr = $(target).closest('.datagrid-row-detail').closest('tr').prev();");
        //sb.Append("		var index = parseInt(tr.attr('datagrid-row-index'));");
        //sb.Append("		console.log(index)");
        //sb.Append("		cancelItem(index);");
        //sb.Append("	}");
        //sb.Append("</script>");

        context.Response.Write(sb.ToString());
    }

    #endregion

    #region 保存用户信息

    /// <summary>
    /// 保存用户信息
    /// </summary>
    /// <param name="context"></param>
    private void SaveUser(HttpContext context)
    {
        //var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s +=  name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s;

        //var strFileId = context.Request.Params["FID"];
        //var strGroupId = context.Request.Params["GID"];
        //var strTypeId = context.Request.Params["TID"];

        //if (string.IsNullOrWhiteSpace(strFileId) && string.IsNullOrWhiteSpace(strGroupId))
        //{
        //    return;
        //}
        //var paramList = new Dictionary<string, string>
        //{
        //    {"FID", strFileId},
        //    {"GID", strGroupId},
        //    {"TID", strTypeId}
        //};

        var firstname = context.Request.Form["firstname"] ?? "";
        var lastname = context.Request.Form["lastname"] ?? "";
        var phone = context.Request.Form["phone"] ?? "";
        var email = context.Request.Form["email"] ?? "";

        User user = new User();
        user.firstname = firstname;
        user.lastname = lastname;
        user.phone = phone;
        user.email = email;

        var rst = UserService.UserAdd(user);
        var js = "{\"success\":true,\"message\":\"用户添加成功\"}";
        context.Response.Write(js);
    }

    #endregion

    #region 更新用户信息

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="context"></param>
    private void UpdateUser(HttpContext context)
    {

        //var s = "";
        //foreach (var name in context.Request.Form)
        //{
        //    s += name + " = " + context.Request.Form[name.ToString()] + System.Environment.NewLine;
        //}

        //var a = s;
        //s = "";

        //foreach (var name in context.Request.Params)
        //{
        //    s += name + " = " + context.Request.Params[name.ToString()] + System.Environment.NewLine;
        //}

        //var b = s;


        var id = context.Request.Params["id"] ?? "0";
        var firstname = context.Request.Form["firstname"] ?? "";
        var lastname = context.Request.Form["lastname"] ?? "";
        var phone = context.Request.Form["phone"] ?? "";
        var email = context.Request.Form["email"] ?? "";

        User user = new User();
        user.id = int.Parse(id);
        user.firstname = firstname;
        user.lastname = lastname;
        user.phone = phone;
        user.email = email;

        var rst = UserService.UserEdit(user);
        var js = "{\"success\":true,\"message\":\"用户修改成功\"}";
        context.Response.Write(js);
    }

    #endregion

    #region 删除用户信息

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="context"></param>
    private void DeleteUser(HttpContext context)
    {
        var strUserId = context.Request.Params["id"];

        if (string.IsNullOrWhiteSpace(strUserId))
        {
            return;
        }

        var rst = UserService.UserDel(int.Parse(strUserId));
        var js = "{\"success\":true,\"message\":\"用户删除成功\"}";
        context.Response.Write(js);
    }

    #endregion

    #region IsReusable

    /// <summary>
    /// IsReusable
    /// </summary>
    public bool IsReusable
    {
        get { return false; }
    }

    #endregion

}