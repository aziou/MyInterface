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
using System.Web.Services;
using OperateData;
using System.Collections.ObjectModel;
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
            LoadMessage();
            LoadDictory();
        }
        public Dictionary<string, string> CodeDL = new Dictionary<string, string>();
        public Dictionary<string, string> CodeDy = new Dictionary<string, string>();
        public Dictionary<string, string> CodeXB = new Dictionary<string, string>();
        public Dictionary<string, string> CodeDJ = new Dictionary<string, string>();
        public Dictionary<string, string> CodeConstant = new Dictionary<string, string>();
        public Dictionary<string, string> CodeClfs = new Dictionary<string, string>();
        public Dictionary<string, string> CodePrevent = new Dictionary<string, string>();
        protected override void  OnKeyDown(KeyEventArgs e)
        {
          
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus.GetType().ToString() == "System.Windows.Controls.TextBox")
                {
                   
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        private void LoadMessage()
        {
            cmb_MeterType.Items.Add("电能表");
            cmb_MeterType.SelectedIndex = 0;
            string strSection = "NewUser/CloumMIS/Item";
            string MeterNumber = "";
            try
            {

                 MeterNumber = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_MeterNum", "Value", "", BaseConfigPath);
                 txt_MeterNum.Text = MeterNumber;
                 if (MeterNumber != "")
                 {
                     RefreashGrid(Convert.ToInt16(MeterNumber));
                 }
            }
            catch(Exception CanNotFindItem)
            {
            
            }
           
        }
        private void RefreashGrid(int M_number)
        {
            this.GenMeter.Children.Clear();
            int Num_B = 1;
            StackPanel panel = new StackPanel();
            WrapPanel wrapPanel = new WrapPanel();
            for (int i = 1; i <=M_number; i++)
            {
                if (i % 2 != 0)
                {
                     wrapPanel = new WrapPanel();
                }
               
               
                    
                    Label lab_MeterNum = new Label();
                    lab_MeterNum.Width = 80;
                    lab_MeterNum.Height = 25;
                    lab_MeterNum.Content = "表" + Num_B.ToString() + ":";
                    lab_MeterNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    TextBox txt_MeterZcbh = new TextBox();
                    txt_MeterZcbh.Name = "MeterZcbh_" + Num_B.ToString().PadLeft(2,'0');
                    txt_MeterZcbh.Width = 200;
                    txt_MeterZcbh.Height = 25;
                    txt_MeterZcbh.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    System.Windows.Thickness Mar = new Thickness(0, 0, 0, 5);
                    wrapPanel.Margin = Mar;
                    wrapPanel.Children.Add(lab_MeterNum);
                    wrapPanel.Children.Add(txt_MeterZcbh);
                    Num_B++;

                    if ((i % 2 == 0)||(i==M_number))
                    {
                        panel.Children.Add(wrapPanel);
                    }
           
               
            }

            this.GenMeter.Children.Add(panel);
        }
        private void btn_click_Click(object sender, RoutedEventArgs e)
        {
            if (txt_MeterNum.Text == "")
            {
                MessageBox.Show("请输入表个数！", "提示", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            int Int_MeterNum = Convert.ToInt16(txt_MeterNum.Text);
            string strSection = "NewUser/CloumMIS/Item";
            OperateData.FunctionXml.UpdateElement(strSection, "Name", "txt_MeterNum", "Value", txt_MeterNum.Text.ToString().Trim(), BaseConfigPath);
            this.GenMeter.Children.Clear();

            RefreashGrid(Int_MeterNum);

            
        }
        #region Useless
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
        #endregion
        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }
        private void btn_downLoad_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> txtList = FindVisualChild<TextBox>(GenMeter);
            List<string> MeterZcbh_List = new List<string>();
            string temp="";
            foreach (TextBox v in txtList)
            {
                if (((TextBox)v).Text == "") continue;
                MeterZcbh_List.Add(((TextBox)v).Text);
            }
            GetMisInfo(MeterZcbh_List);
        }
        private void GetMisInfo(List<string> zcbh_list)
        {
            ObservableCollection<TheNewInterface.Model.DownLoadcs> DownInfo = new ObservableCollection<Model.DownLoadcs>();
            WebReference.CXDNBXXServerService downloadServer = new WebReference.CXDNBXXServerService();
             WebReference.SBJLZCInType InTypeMis=new WebReference.SBJLZCInType ();
            WebReference.SBJLZCOutType OutTypeMis=new WebReference.SBJLZCOutType ();
            int Count=1;
            foreach(string temp in zcbh_list)
            {
                InTypeMis.ZCBH=temp.Trim();
                InTypeMis.DQBM = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_CompanyNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                downloadServer.I_DNJLSBSNJD_CXDNBXX(InTypeMis,out OutTypeMis);
                if (OutTypeMis == null) { MessageBox.Show("无法查到该表的数据，请确认资产编号以及地区编号！"); return; }
                DownInfo.Add(new TheNewInterface.Model.DownLoadcs()
                {
                    Str_Zcbh=OutTypeMis.ZCBH,
                    Str_Ccbh=OutTypeMis.CCBH,
                    Str_Txm=OutTypeMis.SBTMH,
                    Str_Dy=OutTypeMis.EDDYDM,
                    Str_Dl=OutTypeMis.BDDLDM,
                    Str_Rank=OutTypeMis.ZQDDJDM,
                    Str_Constant=OutTypeMis.YGCSDM,
                    Str_Clfs=OutTypeMis.XXDM,
                    Str_Prevent=OutTypeMis.ZNBZ,
                    Str_Connection=OutTypeMis.JRFSDM,
                   Str_WGConstant=OutTypeMis.WGCSDM,
                    Str_Bnum = Count.ToString(),
                    Str_IsCheck="1",
                });
                Count++;
                
            }
            ChangeCode(ref DownInfo);
            if (MakeUpdateTemp(DownInfo))
            {
                MessageBox.Show("下载成功！", "提示");
            }
            else
            {
                MessageBox.Show("下载失败！", "提示");
            }
        }
        private void ChangeCode(ref ObservableCollection<TheNewInterface.Model.DownLoadcs> OutTypeInfo)
        {
            
            foreach (TheNewInterface.Model.DownLoadcs temp in OutTypeInfo)
            {
                temp.Str_Dy = CodeTransMean(CodeDy, temp.Str_Dy);
                temp.Str_Dl = CodeTransMean(CodeDL, temp.Str_Dl);
                temp.Str_Clfs = CodeTransMean(CodeXB, temp.Str_Clfs);
                temp.Str_Rank = CodeTransMean(CodeDJ, temp.Str_Rank);
                temp.Str_Constant = CodeTransMean(CodeConstant, temp.Str_Constant);
                temp.Str_WGConstant = CodeTransMean(CodeConstant, temp.Str_WGConstant);
                temp.Str_Connection = CodeTransMean(CodeClfs, temp.Str_Connection);
                temp.Str_Prevent = CodeTransMean(CodePrevent, temp.Str_Prevent);
            }
        }
        private bool MakeUpdateTemp(ObservableCollection<TheNewInterface.Model.DownLoadcs> OutTypeInfo)
        {
            List<string> Sql_list = new List<string>();
            string SQL = "";
            string AllValue="",Value="";
            foreach (TheNewInterface.Model.DownLoadcs temp in OutTypeInfo)
            { 
                Value=string.Format("AVR_UB='{0}'",temp.Str_Dy);
                AllValue=Value+",";
                Value = string.Format("AVR_ASSET_NO='{0}'", temp.Str_Zcbh);
                AllValue =AllValue+ Value + ",";
                Value = string.Format("AVR_MADE_NO='{0}'", temp.Str_Ccbh);
                AllValue = AllValue + Value + ",";
                Value = string.Format("AVR_BAR_CODE='{0}'", temp.Str_Txm);
                AllValue = AllValue + Value + ",";
                Value = string.Format("AVR_IB='{0}'", temp.Str_Dl);
                AllValue = AllValue + Value + ",";
                Value = string.Format("AVR_WIRING_MODE='{0}'", temp.Str_Clfs);
                AllValue = AllValue + Value + ",";
                Value = string.Format("AVR_AR_CLASS='{0}'", temp.Str_Rank);
                AllValue = AllValue + Value + ",";
                Value = string.Format("CHR_CHECKED='{0}'", temp.Str_IsCheck);
                AllValue = AllValue + Value + ",";
                Value = string.Format("CHR_CT_CONNECTION_FLAG='{0}'", temp.Str_Connection);
                AllValue = AllValue + Value + ",";
                Value = string.Format("AVR_AR_CONSTANT='{0}'", temp.Str_WGConstant.Trim() == "" ? temp.Str_Constant : temp.Str_Constant + "(" + temp.Str_WGConstant + ")");
                AllValue = AllValue + Value + ",";
                Value = string.Format("CHR_CC_PREVENT_FLAG='{0}'", temp.Str_Prevent);
                AllValue = AllValue + Value;
                SQL = string.Format("update TMP_METER_INFO SET {1} WHERE {2} = {0}", temp.Str_Bnum, AllValue, "LNG_BENCH_POINT_NO");
                Sql_list.Add(SQL);
            }
            OperateData.PublicFunction csFunction = new PublicFunction();
            return csFunction.ExcuteAccess(Sql_list);
        
        }
        #region Code Changes
        private void LoadDictory()
        {
            //电压
            #region 电压
            CodeDy.Add("1", "220");
            CodeDy.Add("2", "220");
            CodeDy.Add("3", "57.7");
            CodeDy.Add("4", "380V");
            CodeDy.Add("5", "100V");
            #endregion
            //电流
            #region 电流
            CodeDL.Add("66", "1(4)");
            CodeDL.Add("67", "3(5)");
            CodeDL.Add("9", "3");
            CodeDL.Add("10", "3(6)");
            CodeDL.Add("11", "3(10)");
            CodeDL.Add("12", "5");
            CodeDL.Add("13", "5(6)");
            CodeDL.Add("14", "5(10)");
            CodeDL.Add("15", "5(15)");
            CodeDL.Add("16", "5(20)");
            CodeDL.Add("17", "5(30)");
            CodeDL.Add("18", "5(40)");
            CodeDL.Add("19", "10");
            CodeDL.Add("40", "1(2)");
            CodeDL.Add("41", "1(5)");
            CodeDL.Add("42", "1.5(5)");
            CodeDL.Add("43", "1.5(10)");
            CodeDL.Add("44", "1.5(30)");
            CodeDL.Add("45", "2(3)");
            CodeDL.Add("46", "2(4)");
            CodeDL.Add("47", "2(6)");
            CodeDL.Add("48", "2.5(6)");
            CodeDL.Add("49", "2.5(15)");
            CodeDL.Add("50", "2.5(60)");
            CodeDL.Add("51", "3(20)");
            CodeDL.Add("52", "3(30)");
            CodeDL.Add("53", "5(60)");
            CodeDL.Add("54", "5(100)");
            CodeDL.Add("55", "10(30)");
            CodeDL.Add("56", "10(80)");
            CodeDL.Add("57", "10(100)");
            CodeDL.Add("58", "15(20)");
            CodeDL.Add("59", "15(60)");
            CodeDL.Add("60", "15(90)");
            CodeDL.Add("62", "40(60)");
            CodeDL.Add("63", "40(80)");
            CodeDL.Add("65", "5(150)");
            CodeDL.Add("24", "15(30)");
            CodeDL.Add("4", "1.5(6)");
            CodeDL.Add("8", "2.5(10)");
            CodeDL.Add("39", "2(10)");
            CodeDL.Add("20", "3000");
            CodeDL.Add("21", "10(40)");
            CodeDL.Add("22", "10(60)");
            CodeDL.Add("23", "15");
            CodeDL.Add("25", "20");
            CodeDL.Add("26", "20(40)");
            CodeDL.Add("27", "20(80)");
            CodeDL.Add("28", "25");
            CodeDL.Add("29", "25(50)");
            CodeDL.Add("30", "30");
            CodeDL.Add("31", "30(60)");
            CodeDL.Add("32", "30(100)");
            CodeDL.Add("34", "50");
            CodeDL.Add("35", "80");
            CodeDL.Add("36", "90");
            CodeDL.Add("1", "1(6)");
            CodeDL.Add("2", "1(10)");
            CodeDL.Add("3", "1.5");
            CodeDL.Add("5", "2");
            CodeDL.Add("6", "2.5");
            CodeDL.Add("7", "2.5(5)");
            CodeDL.Add("37", "0.3(1.2)");
            CodeDL.Add("73", "5(80)");
            #endregion

            #region 相别
            CodeXB.Add("01", "5");
            CodeXB.Add("02", "1");
            CodeXB.Add("03", "0");
            #endregion 

            #region 等级
            CodeDJ.Add("01", "0.001");
            CodeDJ.Add("02", "0.002");
            CodeDJ.Add("03", "0.005");
            CodeDJ.Add("04", "0.01");
            CodeDJ.Add("05", "0.02");
            CodeDJ.Add("06", "0.03");
            CodeDJ.Add("07", "0.05");
            CodeDJ.Add("08", "0.1");
            CodeDJ.Add("09", "0.2");
            CodeDJ.Add("12", "0.3");
            CodeDJ.Add("13", "0.5");
            CodeDJ.Add("14", "1.0");
            CodeDJ.Add("15", "2.0");
            CodeDJ.Add("16", "3.0");
            CodeDJ.Add("17", "0.2S");
            CodeDJ.Add("18", "(0.2S)0.5S");
            CodeDJ.Add("19", "(0.2S)(0.5S)5P");
            CodeDJ.Add("20", "(0.5)2.0");
            CodeDJ.Add("21", "0.5S");
            CodeDJ.Add("22", "(0.5S)1.0");
            CodeDJ.Add("23", "(0.5S)2.0");
            CodeDJ.Add("24", "(1.0S)1.0");
            CodeDJ.Add("25", "(1.0S)2.0");
            CodeDJ.Add("26", "(0.2)10p");
            CodeDJ.Add("27", "(0.5)10p");
            CodeDJ.Add("28", "0.2S/0.5/10P/10P");
            CodeDJ.Add("29", "0.001S");
            CodeDJ.Add("30", "0.002S");
            CodeDJ.Add("31", "0.005S");
            CodeDJ.Add("32", "0.01S");
            CodeDJ.Add("33", "0.02S");
            CodeDJ.Add("34", "0.05S");
            CodeDJ.Add("35", "0.1S");
            CodeDJ.Add("36", "1.0(2.0)");
            CodeDJ.Add("37", "1.0S");
            CodeDJ.Add("38", "0.2S(1.0)");
            CodeDJ.Add("39", "0.2S(2.0)");
            CodeDJ.Add("40", "0.2(0.5)3P");
            #endregion

            #region 常数
            CodeConstant.Add("01", "300");
            CodeConstant.Add("02", "600");
            CodeConstant.Add("03", "1200");
            CodeConstant.Add("04", "1600");
            CodeConstant.Add("05", "3200");
            CodeConstant.Add("06", "6400");
            CodeConstant.Add("07", "20000");
            CodeConstant.Add("08", "100000");
            CodeConstant.Add("09", "800");
            CodeConstant.Add("10", "10000");
            CodeConstant.Add("11", "180");
            CodeConstant.Add("12", "1800");
            CodeConstant.Add("13", "2000");
            CodeConstant.Add("14", "2200");
            CodeConstant.Add("15", "2400");
            CodeConstant.Add("16", "2500");
            CodeConstant.Add("17", "3000");
            CodeConstant.Add("18", "3150");
            CodeConstant.Add("19", "3600");
            CodeConstant.Add("20", "4000");
            CodeConstant.Add("21", "4500");
            CodeConstant.Add("22", "5000");
            CodeConstant.Add("23", "6100");
            CodeConstant.Add("24", "7200");
            CodeConstant.Add("25", "8000");
            CodeConstant.Add("26", "9600");
            CodeConstant.Add("27", "12000");
            CodeConstant.Add("28", "12800");
            CodeConstant.Add("29", "15000");
            CodeConstant.Add("30", "16000");
            CodeConstant.Add("31", "18200");
            CodeConstant.Add("32", "24000");
            CodeConstant.Add("33", "25000");
            CodeConstant.Add("34", "25600");
            CodeConstant.Add("35", "28000");
            CodeConstant.Add("36", "40000");
            CodeConstant.Add("37", "50000");
            CodeConstant.Add("38", "120000");
            CodeConstant.Add("39", "125000");
            CodeConstant.Add("40", "200000");
            CodeConstant.Add("41", "1100000");
            CodeConstant.Add("42", "400");
            #endregion

            #region 互感器
            CodeClfs.Add("1", "0");
            CodeClfs.Add("2", "1");
            #endregion

            #region 止逆器
            CodePrevent.Add("0", "0");
            CodePrevent.Add("1", "1");
            #endregion
        }

        private string CodeTransMean(Dictionary<string, string> DicCol, string TransCode)
        {
            string Result = "";
            try
            {
                Result = DicCol[TransCode];
                return Result;
            }
            catch(Exception TransCodeEX)
            {
                return "";
            }
            
        }
        #endregion 
    }

}
