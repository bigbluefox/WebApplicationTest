using System;
using System.IO;
using System.Xml.Serialization;

namespace Hsp.Test.Common
{
    /// <summary>
    ///     Xml序列化与反序列化
    /// </summary>
    public class XmlUtil
    {
        #region 序列化

        /// <summary>
        ///     序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            var stream = new MemoryStream();
            var str = "";

            try
            {
                stream = new MemoryStream();
                var xml = new XmlSerializer(type);

                //序列化对象
                xml.Serialize(stream, obj);

                stream.Position = 0;
                var sr = new StreamReader(stream);
                str = sr.ReadToEnd();
                sr.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();  
            }

            return str;
        }

        #endregion

        #region 反序列化

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (var sr = new StringReader(xml))
                {
                    var xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            var xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }

        #endregion
    }
}