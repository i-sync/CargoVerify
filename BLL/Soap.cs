using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;
using BLL.Service;

namespace BLL
{
    public class Soap
    {
        //获取webService地址及sessionId
        private string serviceURL = Common.Instance.Service.Url;
        private static Soap instance = new Soap();
        private Soap() { }
        public static Soap Instance
        {
            get { return instance; }
        }

        #region 手写SOAP文件去请求WebService

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public loginResult login(string userId, string password, string scheme, out string errMsg)
        {
            errMsg = string.Empty;
            loginResult result = null;
            //根据URL创建请求对象
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serviceURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            StringBuilder soap = new StringBuilder();

            ////登录报文格式
            ///*
            // * <q0:login>
            // *  <userid>$userid</userid>
            // *  <password>$password</password>
            // *  <scheme>$scheme</scheme>
            // *  <algorithm>$algorithm</algorithm>
            // *  <securityCode></securityCode>
            // * </q0:login>
            // * *
            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<soapenv:Envelope xmlns:q0=\"http://ws.livebos.apex.com/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            soap.Append("<soapenv:Header>");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<q0:login>");
            soap.Append("<userid>{0}</userid>");
            soap.Append("<password>{1}</password>");
            soap.Append("<scheme>{2}</scheme>");
            soap.Append("<algorithm></algorithm>");
            soap.Append("<securityCode></securityCode>");
            soap.Append("</q0:login>");
            soap.Append("</soapenv:Body>  ");
            soap.Append("</soapenv:Envelope>");

            //转换成字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format(soap.ToString(),userId,password,scheme));
            webRequest.ContentLength = bytes.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();

            try
            {
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    //获取返回结果流
                    Stream tempStraem = webResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(tempStraem, Encoding.UTF8);
                    //获取结果
                    string res = sr.ReadToEnd();
                    sr.Close();//关闭读取器
                    tempStraem.Close();//关闭流

                    //把结果先转换为xml文件 查询<LoginResult>节点，然后把它该节点再转换为类对象
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(res);
                    XmlNode node = xmlDoc.SelectSingleNode("//LoginResult");
                    res = node.OuterXml.Replace("LoginResult", "loginResult");

                    //把xml转换成lbeResult对象
                    result = Deserialize(res,typeof(loginResult)) as loginResult;

                }
            }
            catch (WebException ex)
            {
                errMsg = ex.Message;
            }

            return result;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="objectName">查询的对象名</param>
        /// <param name="parms"> 传入的参数集合(当要查询的对象为 查询对象,带参数的视图 等要传入参数, 实体对象则传入空值) </param>
        /// <param name="condition">查询的附加条件(查询对象无效),如：“ID=1000” 等 </param>
        /// <param name="queryOption">查询选项</param>
        /// <returns></returns>
        public queryResult query(string objectName, lbParameter[] parms, string condition, queryOption queryOption ,out string errMsg)
        {
            errMsg = string.Empty;
            queryResult result = null;

            //根据URL创建请求对象
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serviceURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";

            //查询报文格式
            ///*
            //<q0:query>
            //    <sessionId>$sessionId</sessionId>
            //    <objectName>$objectName</objectName>
            //    <params>$params</params>
            //    <condition>$condition</condition>
            //    <queryOption>
            //        <batchNo>$batchNo</batchNo>
            //        <batchSize>$batchSize</batchSize>
            //        <queryCount>$queryCount</queryCount>
            //        <valueOption>$valueOption</valueOption>
            //        <orderBy>$orderBy</orderBy>
            //        <queryId>$queryId</queryId>>
            //    </queryOption>
            //</q0:query>
            // * 
            // * *

            StringBuilder soap = new StringBuilder();
            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<soapenv:Envelope xmlns:q0=\"http://ws.livebos.apex.com/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            soap.Append("<soapenv:Header>");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<q0:query>");
            soap.Append("<sessionId>{0}</sessionId>");
            soap.Append("<objectName>{1}</objectName>");
            soap.Append("{2}");
            soap.Append("<condition>{3}</condition>");
            soap.Append("<queryOption>");
            soap.Append("<batchNo>{4}</batchNo>");
            soap.Append("<batchSize>{5}</batchSize>");
            soap.Append("<queryCount>{6}</queryCount>");
            soap.Append("<valueOption>{7}</valueOption>");
            soap.Append("<orderBy></orderBy>");
            soap.Append("<queryId></queryId>>");
            soap.Append("</queryOption>");
            soap.Append("</q0:query>");
            soap.Append("</soapenv:Body>  ");
            soap.Append("</soapenv:Envelope>");

            string strparams = GetSoapArrayString(Serialize(parms), "lbParameter", "params");
            //转换成字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format(soap.ToString(), Common.Instance.SessionId,objectName,strparams,condition, queryOption.batchNo,queryOption.batchSize,queryOption.queryCount.ToString().ToLower(),queryOption.valueOption));
            webRequest.ContentLength = bytes.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();

            try
            {
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    //获取返回结果流
                    Stream tempStraem = webResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(tempStraem, Encoding.UTF8);
                    //获取结果
                    string res = sr.ReadToEnd();                    
                    sr.Close();//关闭读取器
                    tempStraem.Close();//关闭流

                    //把结果先转换为xml文件 查询<LoginResult>节点，然后把它该节点再转换为类对象
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(res);
                    XmlNode node = xmlDoc.SelectSingleNode("//QueryResult");
                    res = node.OuterXml.Replace("QueryResult", "queryResult");


                    //把xml转换成lbeResult对象
                    result = Deserialize(res, typeof(queryResult)) as queryResult;

                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ex.Response as HttpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //获取结果
                errMsg = sr.ReadToEnd();   
            }

            return result;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="objectName">查询的对象名</param>
        /// <param name="id">要修改的记录ID</param>
        /// <param name="parms"> 传入的数据集合 </param>
        /// <returns></returns>
        public lbeResult update(string serviceURL,string sessionId, string objectName, string id, lbParameter[] parms,out string errMsg)
        {
            errMsg = string.Empty;
            lbeResult result = null;

            //根据URL创建请求对象
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serviceURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";

            ////查询报文格式
            ///*
            //<q0:update>
            //    <sessionId>$sessionId</sessionId>
            //    <objectName>$objectName</objectName>
            //    <id>$id</id>
            //    <params>$params</params>
            //</q0:update>
            // * 
            // * *

            StringBuilder soap = new StringBuilder();
            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<soapenv:Envelope xmlns:q0=\"http://ws.livebos.apex.com/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            soap.Append("<soapenv:Header>");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<q0:update>");
            soap.Append("<sessionId>{0}</sessionId>");
            soap.Append("<objectName>{1}</objectName>");
            soap.Append("<id>{2}</id>");
            soap.Append("{3}");
            soap.Append("</q0:update>");
            soap.Append("</soapenv:Body>  ");
            soap.Append("</soapenv:Envelope>");

            string strparams = GetSoapArrayString(Serialize(parms), "lbParameter", "params");
            //转换成字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format(soap.ToString(), Common.Instance.SessionId, objectName, id, strparams));
            webRequest.ContentLength = bytes.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();

            try
            {
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    //获取返回结果流
                    Stream tempStraem = webResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(tempStraem, Encoding.UTF8);
                    //获取结果
                    string res = sr.ReadToEnd();
                    sr.Close();//关闭读取器
                    tempStraem.Close();//关闭流

                    //把结果先转换为xml文件 查询<LbeResult>节点，然后把它该节点再转换为类对象
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(res);
                    XmlNode node = xmlDoc.SelectSingleNode("//LbeResult");
                    res = node.OuterXml.Replace("LbeResult", "lbeResult");

                    //把xml转换成lbeResult对象
                    result = Deserialize(res, typeof(lbeResult)) as lbeResult;

                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ex.Response as HttpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //获取结果
                errMsg = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// 执行对象流程
        /// </summary>
        /// <param name="serviceURL"></param>
        /// <param name="sessionId"></param>
        /// <param name="bizProcessName"></param>
        /// <param name="id"></param>
        /// <param name="parms"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public bizProcessResult execBizProcess(string bizProcessName, string id, lbParameter[] parms, lbParameter[] variables, out string errMsg)
        {
            errMsg = string.Empty;
            bizProcessResult result = null;

            //根据URL创建请求对象
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serviceURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";

            ////报文格式
            ///*
            //<q0:execBizProcess>
            //    <sessionId>$sessionId</sessionId>
            //    <bizProcessName>$bizProcessName</bizProcessName>
            //    <id>$id</id>
            //    <params>$params</params>
            //    <variables>$variables</variables>
            //</q0:execBizProcess>
            // * 
            // * *

            StringBuilder soap = new StringBuilder();
            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<soapenv:Envelope xmlns:q0=\"http://ws.livebos.apex.com/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            soap.Append("<soapenv:Header>");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<q0:execBizProcess>");
            soap.Append("<sessionId>{0}</sessionId>");
            soap.Append("<bizProcessName>{1}</bizProcessName>");
            soap.Append("<id>{2}</id>");
            soap.Append("{3}");
            soap.Append("{4}");
            soap.Append("</q0:execBizProcess>");
            soap.Append("</soapenv:Body>  ");
            soap.Append("</soapenv:Envelope>");

            string strparms = GetSoapArrayString(Serialize(parms), "lbParameter", "params");
            string strvariables = GetSoapArrayString(Serialize(variables), "lbParameter", "variables");
            //转换成字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format(soap.ToString(), Common.Instance.SessionId, bizProcessName, id, strparms, strvariables));
            webRequest.ContentLength = bytes.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();

            try
            {
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    //获取返回结果流
                    Stream tempStraem = webResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(tempStraem, Encoding.UTF8);
                    //获取结果
                    string res = sr.ReadToEnd();
                    sr.Close();//关闭读取器
                    tempStraem.Close();//关闭流

                    //把结果先转换为xml文件 查询<LbeResult>节点，然后把它该节点再转换为类对象
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(res);
                    XmlNode node = xmlDoc.SelectSingleNode("//BizProcessResult");
                    res = node.OuterXml.Replace("BizProcessResult", "bizProcessResult");

                    //把xml转换成lbeResult对象
                    result = Deserialize(res, typeof(bizProcessResult)) as bizProcessResult;

                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ex.Response as HttpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //获取结果
                errMsg = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// 获取指定格式的xml
        /// </summary>
        /// <param name="content">xml内容</param>
        /// <param name="type">类名称</param>
        /// <param name="node">节点名称</param>
        /// <returns></returns>
        private string GetSoapArrayString(string content, string type, string node)
        {
            if (string.IsNullOrEmpty(content))
                return string.Format("<{0}></{0}>",node);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            XmlNodeList nodeList = doc.SelectNodes(string.Format("//{0}",type));

            string result = string.Empty;
            string format = "<" + node + ">{0}</" + node + ">";
            foreach (XmlNode n in nodeList)
            {
                result += string.Format(format, n.InnerXml);
            }
            return result;
        }

        #endregion

        #region 提供xml文档序列化 反序列化

        /// <summary>
        /// 反序列化XML字符串为指定类型
        /// </summary>
        public object Deserialize(string Xml, Type ThisType)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ThisType);
            object result;
            try
            {
                using (StringReader stringReader = new StringReader(Xml))
                {
                    //MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(Xml));
                    result = xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception innerException)
            {
                bool flag = false;
                if (Xml != null)
                {
                    if (Xml.StartsWith(Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble(),0,Encoding.UTF8.GetPreamble().Length)))
                    {
                        flag = true;
                    }
                }
                throw new ApplicationException(string.Format("Couldn't parse XML: '{0}'; Contains BOM: {1}; Type: {2}.",
                Xml, flag, ThisType.FullName), innerException);
            }
            return result;
        }

        /// <summary>
        /// 序列化object对象为XML字符串
        /// </summary>
        public string Serialize(object ObjectToSerialize)
        {
            if (ObjectToSerialize == null)
                return null;
            string result = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding(false));
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(xmlTextWriter, ObjectToSerialize);
                    xmlTextWriter.Flush();
                    xmlTextWriter.Close();
                    UTF8Encoding uTF8Encoding = new UTF8Encoding(false, true);
                    byte[] bytes = memoryStream.ToArray();
                    result = uTF8Encoding.GetString(bytes, 0, bytes.Length);
                }
            }
            catch (Exception innerException)
            {
                throw new ApplicationException("Couldn't Serialize Object:" + ObjectToSerialize.GetType().Name, innerException);
            }
            return result;
        }

        #endregion
        
    }
}
