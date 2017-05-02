using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Collections;
using System.Xml;
namespace OperateData
{
    public class DownloadFunction
    {
        private static Hashtable _xmlNamespaces = new Hashtable();

        private static XmlDocument QuerySoapWebService(string URL, string MethodName, Hashtable Pars, string XmlNs)
        {
            _xmlNamespaces[URL] = XmlNs;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);

            request.Method = "POST";

            request.ContentType = "text/xml;charset=utf-8";

            request.Headers.Add("SOAPAction", @"/" + XmlNs + (XmlNs.EndsWith("/") ? "" : @"/") + MethodName + @"/");
            
            SetWebRequest(request);

            //byte[] data=EncodeParasToSoap(paras,XmlNs,MethodName);

            //WriteRequestData(request,data);

            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();

            //doc=ReadXmlResponse(request.GetResponse());

            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);

            mgr.AddNamespace("soap", "");

            string RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;

            doc2.LoadXml("<root>" + RetXml + "</root>");
            return doc2;
        }

        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            return doc.SelectSingleNode("//@targetNamespace").Value;
        }
        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(@"<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'></soap:Envelope>");
        AddDelaration(doc);
        XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
        XmlElement soapMethod = doc.CreateElement(MethodName);
        soapMethod.SetAttribute("xmlns", XmlNs);
        foreach (string k in Pars.Keys)
        {
            XmlElement soapPar = doc.CreateElement(k);
            soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
            soapMethod.AppendChild(soapPar);
        }
        soapBody.AppendChild(soapMethod);
        doc.DocumentElement.AppendChild(soapBody);
        return Encoding.UTF8.GetBytes(doc.OuterXml);
    }
        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }

    }
}
