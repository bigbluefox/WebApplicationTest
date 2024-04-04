using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Hsp.Test.Common;
using Hsp.Test.Model;

namespace WebApplicationTest.Handler
{
    /// <summary>
    /// TestHandler 的摘要说明
    /// </summary>
    public class TestHandler : IHttpHandler
    {
        #region ProcessRequest

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //var para = "{ 'Date': '2016-5-6', 'Name': 'Tom', 'Age': 20, 'Company': 'IBM' }";
            //context.Response.Write(para);



            var strOperation = context.Request.Params["OPERATION"] ?? "";
            if (string.IsNullOrEmpty(strOperation)) strOperation = context.Request.Form["OPERATION"];

            switch (strOperation.ToUpper())
            {
                //// 获取图标信息
                //case "ICONDATA":
                //    IconZTreeData(context);
                //    break;

                //// 获取路径图标
                //case "PATHICONS":
                //    GetIconsByPath(context);
                //    break;

                //// 样式配置文件XML数据获取
                //case "GETXMLFILE":
                //    GetXmlFile(context);
                //    break;

                //// 样式配置文件XML数据保存
                //case "SAVEXMLFILE":
                //    SaveXmlFile(context);
                //    break;

                // 获取样式配置数据
                case "GETCSSSETTINGS":
                    GetCssSettings(context);
                    break;

                //// 保存样式配置数据
                //case "SAVECSSSETTINGS":
                //    SaveCssSettings(context);
                //    break;

                //// 附件批量下载
                //case "BATCHDOWNLOAD":
                //    BatchDownload(context);
                //    break;

                default:
                    // 在线附件保存
                    //AttachmentEdit(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region 获取样式配置数据

        /// <summary>
        /// 获取样式配置数据
        /// </summary>
        /// <param name="context"></param>
        private static void GetCssSettings(HttpContext context)
        {
            var rst = "";
            try
            {
                var name = context.Request.Params["name"];
                var type = context.Request.Params["type"]; // 0:json/1:xml/2:str/
                var xmlPath = HttpContext.Current.Server.MapPath("~/Styles/Xml/" + name + ".xml");
                if (File.Exists(xmlPath))
                {
                    //直接读取出字符串
                    var xml = File.ReadAllText(xmlPath);
                    if (type == "0")
                    {
                        var cssSettings = XmlUtil.Deserialize(typeof (CssSettings), xml) as CssSettings;
                        rst = new JavaScriptSerializer().Serialize(cssSettings);
                    }
                    else
                    {
                        rst = xml;
                    }
                }

                //rst = "{\"IsSuccess\":true,\"Message\": \"获取样式配置数据成功！\", \"Url\":\"" + "" + "\"}";
            }
            catch (Exception ex)
            {
                rst = "{\"IsSuccess\":false,\"Message\": \"" + ex.Message + "\"}";
                //throw;
            }

            context.Response.Write(rst);
        }

        #endregion

    }
}