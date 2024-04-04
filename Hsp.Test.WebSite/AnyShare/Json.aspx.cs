using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class AnyShare_Json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var ret =
            "{\"causemsg\":\"存在同类型的同名文件(gns:\\/\\/7F45B54D47A242FC837EB31BAD9940D1,宪法学)（错误提供者：EVFS，错误值：16777229，错误位置：\\/var\\/JFR\\/workspace\\/C_EVFS\\/MY_OS_FULL\\/CentOS_All_x64\\/svnrepo\\/DataEngine\\/EFAST\\/EApp\\/EVFS\\/src\\/evfs\\/util\\/ncEVFSSameNameUtil.cpp:117）\",\"errcode\":403039,\"errmsg\":\"存在同类型的同名文件名。\"}";

        var obj = JObject.Parse(ret);

        var b = obj;

        var causemsg = obj.GetValue("causemsg").ToString();

        var startIdx = causemsg.IndexOf("gns:", StringComparison.Ordinal);
        var lastIdx = causemsg.IndexOf(",", StringComparison.Ordinal);
        //var gns = causemsg.Substring(startIdx, 38);

        var gns = causemsg.Substring(startIdx, lastIdx - startIdx);


        var bb = gns;

        lblMsg.Text = gns;

        var serverFile = @"d:\JavaScript权威指南(第4版).pdf";

        FileInfo fi = new FileInfo(serverFile);
        string strFileId = Guid.NewGuid().ToString().ToUpper();
        string strFileName = fi.Name, strFileExt = fi.Extension;
        string strSaveName = strFileId + strFileExt;
    }
}