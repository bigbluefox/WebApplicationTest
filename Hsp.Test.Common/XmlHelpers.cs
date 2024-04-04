using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hsp.Test.Common
{
    /// <summary>
    /// XML 帮助类
    /// </summary>
    public static class XmlHelpers
    {
        /// <summary>
        /// 读取xml文件中的指定节点内容 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="filePath">文件和路经</param>
        /// <returns></returns>
        public static string XmlTextReader(this string node, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            XmlTextReader tr = new XmlTextReader(filePath);
            while (tr.Read())
            {
                if (node.Equals(tr.LocalName))
                {
                    sb.Append(tr.ReadInnerXml());
                    break;
                }
            }
            tr.Close();
            return sb.ToString();
        }

        public static string TextReaderProcess(this string xmlData)
        {
            string tempStr = null;
            XmlTextReader reader = new XmlTextReader(new StringReader(xmlData));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            // Display each element node.
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        tempStr = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        tempStr = reader.Value;
                        break;
                    case XmlNodeType.EndElement:
                        tempStr = reader.Name;
                        break;
                }
            }

            // Close the reader.
            reader.Close();

            return tempStr;
        }

        public static void XmlWriterSP(string filePath, DataTable dt)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            //if (File.Exists(filePath)) File.Delete(filePath);

            XmlWriter xwt = XmlWriter.Create(filePath, settings);
            xwt.WriteStartDocument();
            xwt.WriteStartElement(dt.TableName);
            foreach (DataRow dr in dt.Rows)
            {
                xwt.WriteStartElement("Table");
                xwt.WriteAttributeString("ItemID", dr[0].ToString());
                xwt.WriteElementString("ItemCode", dr[1].ToString());
                xwt.WriteElementString("ItemDescription", dr[3].ToString());
                xwt.WriteElementString("Url", dr[4].ToString());
                xwt.WriteEndElement();
            }
            xwt.WriteEndDocument();
            xwt.Flush();
            xwt.Close();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (xn != null) value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 获取该节点下面的所有内容包括节点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Readxml(string path, string node)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = xn.InnerXml;
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 获取该节点下面的所有内容包括节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Readxml(XmlDocument doc, string node)
        {
            XmlNode xn = doc.SelectSingleNode(node);
            if (xn == null) return "";
            return xn.InnerXml;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch { }
        }

        public static void Insert(XmlDocument doc, string node, string element, string value)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode(node);
                if (xn != null)
                {
                    XmlElement xe = doc.CreateElement(element);
                    //xe.InnerText = value;
                    xe.InnerXml = value;
                    xn.AppendChild(xe);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 设置此节点的子级标记（填充所以内容包括子节点）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public static void UpdateXml(string path, string node, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                xe.InnerXml = value;
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }

        public static void Delete(XmlDocument doc, string node)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode(node);
                if (xn != null)
                {
                    xn.ParentNode.RemoveChild(xn);
                }
                //doc.Save(path);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
