using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Hsp.Test.Common
{
    /// <summary>
    /// 爱数帮助类
    /// </summary>
   public class AnyShareHelper
    {
       public string HttpPost(string url, string postDataStr)
        {
            HttpWebResponse response = GetHttpPostResponse(url, postDataStr);
            return GetResponseBody(response);
        }

        public string GetResponseBody(HttpWebResponse response)
        {
            string encoding = response.ContentEncoding;
            if (string.IsNullOrEmpty(encoding))
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return retString;
        }

        public string ProcessWebException(WebException ex)
        {
            string errStr;

            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var rsp = ex.Response as HttpWebResponse;
                StreamReader reader = new StreamReader(rsp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                errStr = reader.ReadToEnd();
            }
            else
            {
                errStr = ex.Message;
            }

            return errStr;
        }

        public HttpWebResponse GetHttpPostResponse(string url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";

                Stream requestStream = request.GetRequestStream();
                var buffer = Encoding.UTF8.GetBytes(postDataStr);

                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response;
            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        public HttpWebResponse HttpEossReq(string[] authHeaders, byte[] buffer, int bufferLength)
        {
            try
            {
                var method = authHeaders[0];
                var reqUrl = authHeaders[1];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);
                request.Method = method;
                for (var i = 2; i < authHeaders.Length; i++)
                {
                    var authHeaderArray = authHeaders[i].Split(":".ToCharArray(), 2);
                    // 标准头使用属性修改
                    if (string.Equals(authHeaderArray[0], "Content-Length", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.ContentLength = Convert.ToInt64(authHeaderArray[1]);
                    }
                    else if (string.Equals(authHeaderArray[0], "Content-Type", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.ContentType = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Expect", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Expect = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Accept", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Accept = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Referer", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Referer = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "User-Agent", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.UserAgent = authHeaderArray[1];
                    }
                    else
                    {
                        request.Headers.Add(authHeaderArray[0], authHeaderArray[1]);
                    }
                }

                Stream requestStream = request.GetRequestStream();

                requestStream.Write(buffer, 0, bufferLength);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response;

            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        public void HttpEossDownloadReq(string[] authHeaders, FileStream fs)
        {
            try
            {
                var method = authHeaders[0];
                var reqUrl = authHeaders[1];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);
                request.Method = method;
                for (var i = 2; i < authHeaders.Length; i++)
                {
                    var authHeaderArray = authHeaders[i].Split(":".ToCharArray(), 2);
                    // 标准头使用属性修改
                    if (string.Equals(authHeaderArray[0], "Content-Length", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.ContentLength = Convert.ToInt64(authHeaderArray[1]);
                    }
                    else if (string.Equals(authHeaderArray[0], "Content-Type", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.ContentType = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Expect", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Expect = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Accept", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Accept = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "Referer", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.Referer = authHeaderArray[1];
                    }
                    else if (string.Equals(authHeaderArray[0], "User-Agent", StringComparison.CurrentCultureIgnoreCase))
                    {
                        request.UserAgent = authHeaderArray[1];
                    }
                    else
                    {
                        request.Headers.Add(authHeaderArray[0], authHeaderArray[1]);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // 获取返回的数据
                Stream res = response.GetResponseStream();
                long length = response.ContentLength;

                long readCnt = 0;
                long readLength = 0;
                var trunksize = 1024 * 1024 * 1024;
                while (readCnt != length)
                {
                    var bytes = new byte[trunksize];
                    readLength = res.Read(bytes, 0, trunksize);
                    readCnt = readCnt + readLength;
                    fs.Write(bytes, 0, (int)readLength);
                }
            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        public byte[] DownloadFileBlock(string url, string docId, int sn, ref bool hasMore, ref long allSize, ref long downSize)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";

                // 构造请求
                Stream requestStream = request.GetRequestStream();
                JObject jo = new JObject();
                jo.Add(new JProperty("docid", docId));
                jo.Add(new JProperty("sn", sn));

                string jsonStr = jo.ToString();
                var buffer = Encoding.UTF8.GetBytes(jsonStr);

                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                // 发送请求
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // 获取返回的数据
                Stream res = response.GetResponseStream();
                var length = (int)response.ContentLength;
                var bytes = new byte[length];
                var readCnt = 0;
                var readLength = 0;
                while (readCnt != bytes.Length)
                {
                    readLength = res.Read(bytes, readCnt, bytes.Length - readCnt);
                    readCnt = readCnt + readLength;
                }

                // 解析二进制数据

                // index为boundry加上\r\n的长度
                var split = Encoding.ASCII.GetBytes("\r\n\r\n");

                // firstBoundaryEnd为不包含\r\n的第一个boundary的结束位置
                var firstBoundaryEnd = Locate(bytes, 0, split);

                var boundary = new byte[firstBoundaryEnd];
                Stream stream = new MemoryStream(bytes);
                stream.Read(boundary, 0, firstBoundaryEnd);

                var blockDatBegin = firstBoundaryEnd + 4;
                var blockDataEnd = Locate(bytes, firstBoundaryEnd, boundary) - 2;

                var blockData = new byte[blockDataEnd - blockDatBegin];
                stream.Seek(blockDatBegin, SeekOrigin.Begin);
                stream.Read(blockData, 0, blockDataEnd - blockDatBegin);

                var jsonDataBegin = blockDataEnd + 2 + boundary.Length + 4;
                var jsonDataEnd = Locate(bytes, jsonDataBegin, boundary) - 2;

                var jsonData = new byte[jsonDataEnd - jsonDataBegin];
                stream.Seek(jsonDataBegin, SeekOrigin.Begin);
                stream.Read(jsonData, 0, jsonDataEnd - jsonDataBegin);

                JObject resJo = JObject.Parse(Encoding.UTF8.GetString(jsonData));
                if (sn == 0)
                {
                    allSize = (long)resJo["size"];
                }

                hasMore = (downSize += blockData.Length) < allSize;

                var tmp = string.Format("DownloadFileBlock({0}-{1}, {2}/{3})", docId, sn, downSize, allSize);
                Console.WriteLine(tmp);

                return blockData;
            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        public string PostFileBlock(string url, string docId, bool hasMore, int sn, string name, byte[] blockData, int length, ref string rev)
        {
            try
            {
                var tmp = string.Format("PostFileBlock({0}-{1}, {2}, {3}, {4})", docId, sn, name, length, rev);
                Console.WriteLine(tmp);

                byte[] buffer = null;

                var tmpStr = string.Format("{0:N}", Guid.NewGuid());
                var boundary = "--" + tmpStr + "\r\n" + "\r\n";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "multipart/form-data; boundary=" + tmpStr;

                Stream requestStream = webRequest.GetRequestStream();

                buffer = Encoding.Default.GetBytes(boundary);
                requestStream.Write(buffer, 0, buffer.Length);

                requestStream.Write(blockData, 0, length);

                buffer = Encoding.Default.GetBytes("\r\n");
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.Default.GetBytes(boundary);
                requestStream.Write(buffer, 0, buffer.Length);

                JObject jo = new JObject();
                jo.Add(new JProperty("docid", docId));
                jo.Add(new JProperty("more", hasMore));
                jo.Add(new JProperty("sn", sn));
                jo.Add(new JProperty("rev", rev));
                jo.Add(new JProperty("name", name));
                jo.Add(new JProperty("ondup", 1));
                jo.Add(new JProperty("length", length));

                string jsonStr = jo.ToString();
                buffer = Encoding.UTF8.GetBytes(jsonStr);

                requestStream.Write(buffer, 0, buffer.Length);

                var endBoundary = "\r\n" + "--" + tmpStr + "--";
                buffer = Encoding.Default.GetBytes(endBoundary);
                requestStream.Write(buffer, 0, buffer.Length);

                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string retString = reader.ReadToEnd();

                JObject retjo = JObject.Parse(retString);

                rev = retjo["rev"].ToString();
                string retDocId = retjo["docid"].ToString();
                return retDocId;
            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        public void PostLargeFileBlock(string urlbase, string userid, string tokenid, string docId, string name, FileStream fileStream, out string retId)
        {
            try
            {
                // osinitmultiupload
                long length = fileStream.Length;
                var initResult = OsInitMultiUpload(urlbase, userid, tokenid, docId, name, length);

                JObject retjson = JObject.Parse(initResult);

                string retDocId = retjson["docid"].ToString();
                string retName = retjson["name"].ToString();
                string retRev = retjson["rev"].ToString();
                string retUploadId = retjson["uploadid"].ToString();

                retId = retDocId;

                //Console.WriteLine(initResult);

                // osuploadpart
                // 以4m为基数，算出分块总数
                var chunkSize = 4 * 1024 * 1024;
                var count = (int)(length / chunkSize) + 1;
                var part = "1-" + count;
                var uploadPartResult = OsUploadPart(urlbase, userid, tokenid, retDocId, retRev, retUploadId, part);
                
                //Console.WriteLine(uploadPartResult);

                JObject authjson = JObject.Parse(uploadPartResult);
                Dictionary<string, ArrayList> partInfoDict = new Dictionary<string, ArrayList>();
                for (var i = 1; i <= count; i++)
                {
                    JArray authRequests = (JArray)authjson["authrequests"][i.ToString()];
                    string[] authRequestArray = authRequests.ToObject<string[]>();

                    var buffer = new byte[chunkSize];
                    int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    HttpWebResponse response = HttpEossReq(authRequestArray, buffer, bytesRead);
                    string etag = null;
                    etag = response.Headers.Get("ETag");
                    response.Close();

                    ArrayList etagList = new ArrayList();
                    etagList.Add(etag);
                    etagList.Add(bytesRead);
                    partInfoDict.Add(i.ToString(), etagList);
                }

                // oscompleteupload
                ArrayList completeAuthData = OsCompleteUpload(urlbase, userid, tokenid, retDocId, retRev, retUploadId, partInfoDict);
                JArray completeAuthRequests = (JArray)completeAuthData[0];
                string[] cauthRequestArray = completeAuthRequests.ToObject<string[]>();
                var completeBuffer = (byte[])completeAuthData[1];
                HttpEossReq(cauthRequestArray, completeBuffer, completeBuffer.Length);

                // osendupload
                OsEndUpload(urlbase, userid, tokenid, retDocId, retRev);

            }
            catch (WebException ex)
            {
                var errStr = ProcessWebException(ex);

                var e = new Exception(errStr);
                throw e;
            }
        }

        private string OsInitMultiUpload(string urlbase, string userid, string tokenid, string docId, string name, long length)
        {
            // 初始化上传大文件协议
            var url = urlbase + "/v1/file?method=osinitmultiupload&userid=" + userid + "&tokenid=" + tokenid;
            JObject jo = new JObject();
            jo.Add(new JProperty("docid", docId));
            jo.Add(new JProperty("name", name));
            jo.Add(new JProperty("length", length));
            jo.Add(new JProperty("ondup", 1));

            string jsonStr = jo.ToString();
            var res = HttpPost(url, jsonStr);
            return res;
        }

        private string OsUploadPart(string urlbase, string userid, string tokenid, string docId, string rev, string uploadid, string parts)
        {
            // 开始上传大文件协议
            var url = urlbase + "/v1/file?method=osuploadpart&userid=" + userid + "&tokenid=" + tokenid;
            JObject jo = new JObject();
            jo.Add(new JProperty("docid", docId));
            jo.Add(new JProperty("rev", rev));
            jo.Add(new JProperty("uploadid", uploadid));
            jo.Add(new JProperty("parts", parts));
            jo.Add(new JProperty("ondup", 1));
            jo.Add(new JProperty("usehttps", false));

            string jsonStr = jo.ToString();
            var res = HttpPost(url, jsonStr);
            return res;
        }

        private ArrayList OsCompleteUpload(string urlbase, string userid, string tokenid, string docId, string rev, string uploadid, Dictionary<string, ArrayList> partsInfo)
        {
            // 开始上传大文件协议
            var url = urlbase + "/v1/file?method=oscompleteupload&userid=" + userid + "&tokenid=" + tokenid;
            JObject jo = new JObject();
            jo.Add(new JProperty("docid", docId));
            jo.Add(new JProperty("rev", rev));
            jo.Add(new JProperty("uploadid", uploadid));
            jo.Add(new JProperty("usehttps", false));

            JObject partInfoJo = new JObject();
            foreach (var key in partsInfo.Keys)
            {
                JArray arr = new JArray();
                arr.Add(partsInfo[key][0]);
                arr.Add(partsInfo[key][1]);
                partInfoJo.Add(new JProperty(key, arr));
            }
            jo.Add(new JProperty("partinfo", partInfoJo));

            string jsonStr = jo.ToString();
            HttpWebResponse response = GetHttpPostResponse(url, jsonStr);
            var length = (int)response.ContentLength;
            Stream res = response.GetResponseStream();
            var bytes = new byte[length];
            var readCnt = 0;
            var readLength = 0;
            while (readCnt != bytes.Length)
            {
                readLength = res.Read(bytes, readCnt, bytes.Length - readCnt);
                readCnt = readCnt + readLength;
            }
            // index为boundry加上\r\n的长度
            var split = Encoding.ASCII.GetBytes("\r\n\r\n");

            // firstBoundaryEnd为不包含\r\n的第一个boundary的结束位置
            var firstBoundaryEnd = Locate(bytes, 0, split);

            var boundary = new byte[firstBoundaryEnd];
            Stream stream = new MemoryStream(bytes);
            stream.Read(boundary, 0, firstBoundaryEnd);

            var blockDatBegin = firstBoundaryEnd + 4;
            var blockDataEnd = Locate(bytes, firstBoundaryEnd, boundary) - 2;

            var blockData = new byte[blockDataEnd - blockDatBegin];
            stream.Seek(blockDatBegin, SeekOrigin.Begin);
            stream.Read(blockData, 0, blockDataEnd - blockDatBegin);

            var jsonDataBegin = blockDataEnd + 2 + boundary.Length + 4;
            var jsonDataEnd = Locate(bytes, jsonDataBegin, boundary) - 2;

            var jsonData = new byte[jsonDataEnd - jsonDataBegin];
            stream.Seek(jsonDataBegin, SeekOrigin.Begin);
            stream.Read(jsonData, 0, jsonDataEnd - jsonDataBegin);

            JObject resJo = JObject.Parse(Encoding.UTF8.GetString(jsonData));
            ArrayList dataList = new ArrayList();
            dataList.Add((JArray)resJo["authrequest"]);
            dataList.Add(blockData);

            return dataList;
        }

        private string OsEndUpload(string urlbase, string userid, string tokenid, string docId, string rev)
        {
            // 完成大文件上传协议
            var url = urlbase + "/v1/file?method=osendupload&userid=" + userid + "&tokenid=" + tokenid;
            JObject jo = new JObject();
            jo.Add(new JProperty("docid", docId));
            jo.Add(new JProperty("rev", rev));

            string jsonStr = jo.ToString();
            var res = HttpPost(url, jsonStr);
            return res;
        }

        private string OsDownload(string urlbase, string userid, string tokenid, string docId)
        {
            // 下载大文件
            var url = urlbase + "/v1/file?method=osdownload&userid=" + userid + "&tokenid=" + tokenid;
            JObject jo = new JObject();
            jo.Add(new JProperty("docid", docId));
            jo.Add(new JProperty("usehttps", false));

            string jsonStr = jo.ToString();
            var res = HttpPost(url, jsonStr);
            return res;
        }

        public void DownloadLargeFile(string urlbase, string userid, string tokenid, string docId, FileStream fs)
        {
            // 下载大文件
            var res = OsDownload(urlbase, userid, tokenid, docId);
            JObject authjson = JObject.Parse(res);
            JArray authRequests = (JArray)authjson["authrequest"];
            string[] authRequestArray = authRequests.ToObject<string[]>();
            HttpEossDownloadReq(authRequestArray, fs);
        }

        private int Locate(byte[] src, int start, byte[] candidate)
        {
            if (IsEmptyLocate(src, candidate))
                return -1;

            for (var i = start; i < src.Length; i++)
            {
                if (!IsMatch(src, i, candidate))
                    continue;

                return i;
            }

            return -1;
        }

        private bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > array.Length - position)
                return false;

            for (var i = 0; i < candidate.Length; i++)
                if (array[position + i] != candidate[i])
                    return false;

            return true;
        }

        private bool IsEmptyLocate(byte[] array, byte[] candidate)
        {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }




        #region 0. RSA加密

        /// <summary>
        ///     0. RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string content)
        {
            var modulukey = (ConfigurationManager.AppSettings["AnySharePublicKey"] ?? "").Trim();
            var publickey = string.Format(
                @"<RSAKeyValue><Modulus>{0}</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", modulukey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);
            var cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            // 由于windows 使用的\r\n对 base64进行换行，而服务器rsa只支持 \n换行的base64 串，故将\r\n替换成 \n
            return Convert.ToBase64String(cipherbytes, Base64FormattingOptions.InsertLineBreaks).Replace("\r\n", "\n");
        }

        #endregion

        #region 1. 检查服务器是否在线

        /// <summary>
        /// 1. 检查服务器是否在线
        /// </summary>
        /// <param name="urlBase"></param>
        /// <returns></returns>
        public string Ping(string urlBase)
        {
            try
            {
                var url = urlBase + ":9998/v1/ping";
                var res = this.HttpPost(url, "");
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 2.1 登录处理

        /// <summary>
        /// 2.1 登录处理
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="account">用户登录账号</param>
        /// <param name="password">加密后的密文</param>
        /// <returns></returns>
        public string Login(string urlBase, string account, string password)
        {
            try
            {
                var url = urlBase + ":9998/v1/auth1?method=getnew";
                var json = "{ \"account\": \"" + account + "\",\"password\":\"" + RSAEncrypt(password) + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                //{"causemsg":"存在同类型的同名文件(gns:\/\/7F45B54D47A242FC837EB31BAD9940D1,生产安全概论)（错误提供者：EVFS，错误值：16777229，错误位置：\/var\/JFR\/workspace\/C_EVFS\/MY_OS_FULL\/CentOS_All_x64\/svnrepo\/DataEngine\/EFAST\/EApp\/EVFS\/src\/evfs\/util\/ncEVFSSameNameUtil.cpp:117）","errcode":403039,"errmsg":"存在同类型的同名文件名。"} 

                //var jobj = JObject.Parse(ex.Message);
                //var causemsg = jobj.GetValue("causemsg").ToString(); // causemsg
                //var errcode = jobj.GetValue("errcode").ToString(); // errcode
                //var errmsg = jobj.GetValue("errmsg").ToString(); // errmsg

                //lblMsg.Text = "错误代码：" + errcode + "，错误信息：" + errmsg + " * causemsg:" + causemsg;
                return ex.Message; // 无效的 URI: 未能确定 URI 的格式。
            }
        }

        #endregion

        #region 2.2 刷新身份凭证有效期

        /// <summary>
        /// 2.2 刷新身份凭证有效期
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="expirestype">
        /// 刷新有效期类型
        /// expirestype等于1时，刷新后token有效期为3天；
        /// expirestype等于2时，刷新后token有效期为1年；
        /// expirestype为其他值时，抛错参数值非法。
        /// </param>
        /// <returns></returns>
        public string RefreshToken(string urlBase, string userId, string tokenId, int expirestype)
        {
            try
            {
                var url = urlBase + ":9998/v1/auth1?method=refreshtoken";
                var json = "{ \"userid\": " + "\"" + userId + "\"," + "\"tokenid" + "\":\"" +
                           tokenId + "\"," + "\"expirestype" + "\":" + expirestype + "}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 2.3. 回收身份凭证

        /// <summary>
        /// 2.3. 回收身份凭证
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="tokenId">验证令牌</param>
        /// <returns></returns>
        public string RevokeToken(string urlBase, string tokenId)
        {
            try
            {
                var url = urlBase + ":9998/v1/auth1?method=revoketoken";
                var json = "{ \"tokenid" + "\":\"" + tokenId + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 2.4 登出处理

        /// <summary>
        /// 2.4 登出处理
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <returns></returns>
        public string Logout(string urlBase, string userId, string tokenId)
        {
            try
            {
                var url = urlBase + ":9998/v1/auth1?method=logout&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{ \"ostype\": 0 }";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion


        #region 3.1. 创建目录协议

        /// <summary>
        /// 3.1. 创建目录协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">父级gns路径</param>
        /// <param name="name">文件夹名称</param>
        /// <returns></returns>
        public string CreateDir(string urlBase, string userId, string tokenId, string docid, string name)
        {
            //UserId = !string.IsNullOrEmpty(UserId) ? UserId : (Session["UserId"] ?? "").ToString();
            //TokenId = !string.IsNullOrEmpty(TokenId) ? UserId : Session["TokenId"].ToString();
            //UrlPre = string.IsNullOrEmpty(UrlPre) ? (Session["UrlPre"] ?? "").ToString() : UrlPre;
            //var httpUtility = new AnyShareHelper();

            try
            {
                var url = urlBase + ":9123/v1/dir?method=create&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\", \"name\":\"" + name + "\", \"ondup\": \"1\"" + "}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 3.2. 删除目录协议

        /// <summary>
        ///    3.2.	删除目录协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">目录ns路径</param>
        /// <returns></returns>
        public string DeleteDir(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/dir?method=delete&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 3.3. 重命名目录协议

        /// <summary>
        /// 3.3. 重命名目录协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">父级gns路径</param>
        /// <param name="name">文件夹名称</param>
        /// <returns></returns>
        public string RenameDir(string urlBase, string userId, string tokenId, string docid, string name)
        {
            try
            {
                var url = urlBase + ":9123/v1/dir?method=rename&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\", \"name\":\"" + name + "\", \"ondup\": \"1\"" + "}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 3.4. 根目录查询

        /// <summary>
        /// 3.4. 根目录查询
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param>
        /// <returns></returns>
        public string RootDir(string urlBase, string userId, string tokenId)
        {
            try
            {
                var url = urlBase + ":9998/v1/entrydoc?method=get&userid=" + userId + "&tokenid=" + tokenId;
                var res = this.HttpPost(url, "");
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 3.5. 浏览目录协议

        /// <summary>
        ///     3.5. 浏览目录协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">gns路径</param>
        /// <returns></returns>
        public string ListDir(string urlBase, string userId, string tokenId, string docid)
        {
            //var httpUtility = new AnyShareHelper();
            try
            {
                var url = urlBase + ":9123/v1/dir?method=list&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 3.6. 获取目录属性协议

        /// <summary>
        ///     3.6. 获取目录属性协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">gns路径</param>
        /// <returns></returns>
        public string DirAttribute(string urlBase, string userId, string tokenId, string docid)
        {
            //var httpUtility = new AnyShareHelper();
            try
            {
                var url = urlBase + ":9123/v1/dir?method=attribute&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 4.1. 获取视频缩略图协议

        /// <summary>
        /// 4.1. 获取视频缩略图协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">文件gns路径</param>
        /// <returns></returns>
        public HttpWebResponse Thumbnail(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/file?method=playthumbnail&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                HttpWebResponse response = this.GetHttpPostResponse(url, json);
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region 5.1. 删除文件协议

        /// <summary>
        ///    5.1.	删除文件协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">要删除文件的gns路径</param>
        /// <returns></returns>
        public string DeleteFile(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/file?method=delete&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 回收站文件删除
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">已删除文件的gns路径</param>
        /// <returns></returns>
        public string RecycleDelete(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/recycle?method=delete&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 5.2. 重命名文件协议

        /// <summary>
        /// 5.2. 重命名文件协议
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">要重命名的文件gns路径</param>
        /// <param name="name">重命名成功后的新文件名，UTF8编码</param>
        /// <returns></returns>
        public string RenameFile(string urlBase, string userId, string tokenId, string docid, string name)
        {
            try
            {
                var url = urlBase + ":9123/v1/file?method=rename&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\", \"name\":\"" + name + "\", \"ondup\": \"1\"" + "}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion


        #region 7.1. 获取外链开启信息

        /// <summary>
        /// 7.1. 获取外链开启信息
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">文件gns路径</param>
        /// <returns></returns>
        public string LinkInfo(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/link?method=getdetail&userid=" + userId + "&tokenid=" + tokenId;
                var json = "{\"docid\":\"" + docid + "\"}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 7.2. 开启外链

        /// <summary>
        /// 7.2. 开启外链
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <param name="docid">文件gns路径</param>
        /// <returns></returns>
        public string SetLink(string urlBase, string userId, string tokenId, string docid)
        {
            try
            {
                var url = urlBase + ":9123/v1/link?method=open&userid=" + userId + "&tokenid=" + tokenId;
                var endTime = (DateTime.Now.AddYears(5).ToUniversalTime().Ticks - 621355968000000000) / 10; // 最多只能增加5年
                var accessRights = 3; // 爱数访问权限：3-预览+下载；1-仅预览
                var json = "{\"docid\":\"" + docid + "\",\"perm\":" + accessRights + ",\"endtime\":" + endTime + "}";
                var res = this.HttpPost(url, json);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 7.3. 我的外链

        /// <summary>
        /// 7.3. 我的外链
        /// </summary>
        /// <param name="urlBase">基础地址</param>
        /// <param name="userId">用户标识</param>
        /// <param name="tokenId">验证令牌</param> 
        /// <returns></returns>
        public string Linked(string urlBase, string userId, string tokenId)
        {
            try
            {
                var url = urlBase + ":9123/v1/link?method=getlinked&userid=" + userId + "&tokenid=" + tokenId;
                var res = this.HttpPost(url, "");
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion



    }
}
