using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

public partial class Watermark_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 生成水印文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        var fileName = @"QHHCP 201.001-2015-淮沪煤电有限公司田集发电厂规划管理标准.doc";
        //fileName = @"nubia UI3.9.6 SD卡升级指导.pdf";
        fileName = @"上海上电漕泾发电有限公司管理术语标准.doc";

        var source = @"Files\" + fileName;
        var sourcePath = Server.MapPath(source);
        var targetName = @"QHHCP 201.001-2015-淮沪煤电有限公司田集发电厂规划管理标准_WaterMark.pdf";
        var target = @"Files\" + targetName;
        var targerPath = Server.MapPath(target);

        var arr = fileName.Split('.');
        var lastIndex = fileName.LastIndexOf(".", StringComparison.Ordinal);
        var fileTitle = fileName.Substring(0, lastIndex);
        var fileExt = fileName.Substring(lastIndex, fileName.Length - lastIndex);

        txtSource.Text = sourcePath;
        txtTarget.Text = targerPath;

        if (!File.Exists(sourcePath))
        {
            lblMessage.Text = "来源文件不存在！";
        }

        var logoImg = ConfigurationManager.AppSettings["WatermarkFilePath"] ?? "";
        if (!logoImg.IsNullOrEmpty()) logoImg = HttpContext.Current.Server.MapPath(@"..\\" + logoImg);
        if (!File.Exists(logoImg))
        {
            lblMessage.Text += "水印图片文件不存在！";
        }

        var fonts = ConfigurationManager.AppSettings["WatermarkFontPath"] ?? "";
        if (!File.Exists(fonts))
        {
            lblMessage.Text += "水印字体文件不存在！";
        }

        var strContractNumber = "PW1101AG101"; // 合同编号

        WatermarkHelper.ExportPdfAndAddWaterMark(sourcePath, fileTitle, fileExt, logoImg, strContractNumber);

    }
}