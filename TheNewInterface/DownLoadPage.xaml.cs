using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
namespace TheNewInterface
{
    /// <summary>
    /// DownLoadPage.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoadPage : Window
    {
        public DownLoadPage()
        {
            InitializeComponent();
        }

        private void btn_click_Click(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            //btn.Name = "btn_click2";
            string Url = @"http://10.150.23.35:7010/PMS_WS/services/I_DNJLSBSNJD_CXDNBXX?wsdl";
            string method= GetNamespace(Url);
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            System.Net.HttpWebResponse respond = (System.Net.HttpWebResponse)request.GetResponse();
            string Xml = new StreamReader(respond.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            XmlDocument docxml = new XmlDocument();
            
            docxml.LoadXml(Xml);
            
            //string str = docxml.ChildNodes[1].InnerText;

            GetWebService();
        }

        private void GetWebService()
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://10.150.23.35:7010/PMS_WS/services/I_DNJLSBSNJD_CXDNBXX?wsdl");

            //要发送soap请求的内容，必须使用post方法传送数据
            myHttpWebRequest.Method = "POST";
           // System.Net.HttpWebResponse myWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            myHttpWebRequest.ContentType = @"text/xml";

            //缺省当前登录用户的身份凭据
            myHttpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myHttpWebRequest.Timeout = 10000;
            //soap请求的内容
  

            StringBuilder soap = new StringBuilder();
            soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soap.Append("<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            soap.Append("<SOAP-ENV:Header/>");
            soap.Append("<SOAP-ENV:Body>");
            soap.Append("<lee:I_DNJLSBSNJD_CXDNBXXRequest xmlns:lee=\"http://gd.soa.csg.cn\">");
            soap.Append("<lee:SB_JLZC_IN>");
            soap.Append("<lee:ZCBH>JBD6515041089</lee:ZCBH>");
            soap.Append("<lee:DQBM>031900</lee:DQBM>");
            soap.Append("</lee:I_DNJLSBSNJD_CXDNBXXRequest>");
            soap.Append("</SOAP-ENV:Body>");
            soap.Append("</SOAP-ENV:Envelope>");
            byte[] byteRequest = Encoding.UTF8.GetBytes(soap.ToString());


            // myHttpWebRequest.ContentLength = byteRequest.Length;

      

            //将soap请求的内容放入HttpWebRequest对象post方法的请求数据部分

            Stream newStream = myHttpWebRequest.GetRequestStream();
          //  var ms = StreamToMemoryStream(newStream);

            newStream.Write(byteRequest, 0, byteRequest.Length);
            newStream.Close();
            //发送请求
            System.Net.HttpWebResponse myWebResponse = null;
            try
            {
                myWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();

            }
            catch (WebException Exml)
            {
               myWebResponse = (System.Net.HttpWebResponse)Exml.Response;
            }
           
            //将收到的回应从Stream转换成string
            XmlDocument exDoc = new XmlDocument();
            exDoc = ReadXmlResponse(myWebResponse);
            string temp = ConverXmlToString(exDoc);
            newStream = myWebResponse.GetResponseStream();

            byte[] byteResponse = new byte[myWebResponse.ContentLength];

            newStream.Read(byteResponse, 0, (int)myWebResponse.ContentLength);

            //str里面就是返回soap回应的字符串了

            string str = Encoding.UTF8.GetString(byteResponse);
            myWebResponse.Close();
        }
        private string ConverXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string Xmlstring = sr.ReadLine();
            sr.Close();
            stream.Close();
            return Xmlstring;
        }
        private MemoryStream StreamToMemoryStream(Stream InsetStream)
        {
            MemoryStream outStream = new MemoryStream();
            const int BuffLen = 4096;
            byte[] buffer = new byte[BuffLen];
            int count = 0;
            while((count=InsetStream.Read(buffer,0,BuffLen))>0)
            {
                outStream.Write(buffer, 0, count);
            }
            return outStream;
        }


        private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace

        /**/
        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public static XmlDocument QueryPostWebService(String URL, String MethodName)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            string cont = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope\" xmlns:mk=\"http://mk.gd.soa.csg.cn\">" +
                      "<SOAP-ENV:Body>" +
"<mk:I_DNJLSBSNJD_CXDNBXX>" +
  "<mk:SB_JLZC_IN>" +
      "<mk:ZCBH>JBD6515041089</mk:ZCBH>" +
      "<mk:DQBM>031900</mk:DQBM>" +
  "</mk:SB_JLZC_IN>" +
"</mk:I_DNJLSBSNJD_CXDNBXX>" +
"</SOAP-ENV:Body>" +
"</SOAP-ENV:Envelope>";
            byte[] byteRequest = Encoding.UTF8.GetBytes(cont);
           // byte[] data = EncodePars(Pars);
            WriteRequestData(request, byteRequest);

            return ReadXmlResponse(request.GetResponse());
        }
        /**/
        /// <summary>
        /// 需要WebService支持Get调用
        /// </summary>
        public static XmlDocument QueryGetWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName + "?" + ParsToString(Pars));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadXmlResponse(request.GetResponse());
        }


        /**/
        /// <summary>
        /// 通用WebService调用(Soap),参数Pars为String类型的参数名、参数值
        /// </summary>
        public static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars)
        {
            if (_xmlNamespaces.ContainsKey(URL))
            {
                return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString());
            }
            else
            {
                return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL));
            }
        }

        private static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs)
        { //By 同济黄正 http://hz932.ys168.com 2008-3-19
            _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
            doc = ReadXmlResponse(request.GetResponse());

            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            doc2.LoadXml("<root>" + RetXml + "</root>");
            AddDelaration(doc2);
            return doc2;
        }
        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
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
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
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
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }

        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }

        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }

        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }

        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }
    }

}
