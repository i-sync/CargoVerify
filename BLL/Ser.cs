using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net;
using System.IO;

namespace BLL
{
    public class Ser
    {
        public string Test()
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create("http://222.82.39.129:8099/service/LBEBusiness");
            webRequest.Method = "POST";
            //webRequest.Proxy=
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";

            StringBuilder soap = new StringBuilder();
            //soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //soap.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            ////soap.Append("<soap:Header>");
            ////soap.Append("<SoapHeader xmlns=\"http://ws.livebos.apex.com/\">");

            ////soap.Append("</SoapHeader>");
            ////soap.Append("</soap:Header>");

            //soap.Append("<soap:Body>");
            //soap.Append("<login xmlns=\"http://ws.livebos.apex.com/\">");
            //soap.Append("<userid>wsuer</userid>");
            //soap.Append("<password>123456</password>");
            //soap.Append("<scheme>\"\"</scheme>");
            //soap.Append("<algorithm>\"\"</algorithm>");
            //soap.Append("<securityCode>\"\"</securityCode>");
            //soap.Append("</login>");
            //soap.Append("</soap:Body>");
            //soap.Append("</soap:Envelope>");

            ////webRequest.Headers["SoapAction"] = "http://ws.livebos.apex.com/login";

            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<soapenv:Envelope xmlns:q0=\"http://ws.livebos.apex.com/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            soap.Append("<soapenv:Header>");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<q0:login>");
            soap.Append("<userid>wsuser</userid>");
            soap.Append("<password>123456</password>");
            soap.Append("<scheme></scheme>");
            soap.Append("<algorithm></algorithm>");
            soap.Append("<securityCode></securityCode>");
            soap.Append("</q0:login>");
            soap.Append("</soapenv:Body>  ");
            soap.Append("</soapenv:Envelope>");

            byte[] bytes = Encoding.UTF8.GetBytes(soap.ToString());
            webRequest.ContentLength = bytes.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();

            string result = "";
            try
            {
                HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream tStream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(tStream, Encoding.UTF8);
                    result = sr.ReadToEnd();
                    sr.Close();
                    tStream.Close();

                }
                else
                {
                    result = "连接错误";
                }
                response.Close();
            }
            catch (WebException ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
