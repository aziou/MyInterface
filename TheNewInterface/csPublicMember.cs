using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheNewInterface
{
    
    public class csPublicMember
    {
        private static object lockValue = new object();  // 让lock用，不用管是什么东西

        /// <summary>
        /// 父窗体
        /// </summary>
        

        //public  class csShowData
        //{
        //    /// <summary>
        //    /// 是否上传标记 
        //    /// </summary>            
        //    public  string strUpdata
        //    {
        //        get { return _strUpdata; }
        //        set { _strUpdata = value; }
        //    }
        //    private static string _strUpdata = "";

        //    /// <summary>
        //    /// 数据ID
        //    /// </summary>            
        //    public  string strDataID
        //    {
        //        get { return _strDataID; }
        //        set { _strDataID = value; }
        //    }
        //    private static string _strDataID = "";
        //    ///// <summary>
        //    ///// 表位号
        //    ///// </summary>            
        //    //public string strBwNo
        //    //{
        //    //    get { return _strBwNo; }
        //    //    set { _strBwNo = value; }
        //    //}
        //    //private string _strBwNo = "";

        //}

        //public static List<csShowData> lisDataID00 = new List<csShowData>();



        /// <summary>
        /// CLOU数据库的ID
        /// </summary>        
        public static List<string> lisDataID
        {
            get { return _lisDataID; }
            set { lock (lockValue) { _lisDataID = value; } }
        }
        private static List<string> _lisDataID = new List<string>();
        /// <summary>
        /// 铅封号1集合
        /// </summary>        
        public static List<string> lisSeal1
        {
            get { return _lisSeal1; }
            set { lock (lockValue) { _lisSeal1 = value; } }
        }
        private static List<string> _lisSeal1 = new List<string>();

        /// <summary>
        /// 铅封号2集合
        /// </summary>        
        public static List<string> lisSeal2
        {
            get { return _lisSeal2; }
            set { lock (lockValue) { _lisSeal2 = value; } }
        }
        private static List<string> _lisSeal2 = new List<string>();

        /// <summary>
        /// CLOU数据库的资产编号
        /// </summary>        
        public static List<string> lisZCBH
        {
            get { return _lisZCBH; }
            set { lock (lockValue) { _lisZCBH = value; } }
        }
        private static List<string> _lisZCBH = new List<string>();
        /// <summary>
        /// 是否上传标记
        /// </summary>        
        public static List<string> lisUpdata
        {
            get { return _lisUpdata; }
            set { lock (lockValue) { _lisUpdata = value; } }
        }
        private static List<string> _lisUpdata = new List<string>();

        /// <summary>
        /// 子窗体名称
        /// </summary>        
        public static string strFrmName
        {
            get { return _strFrmName; }
            set { lock (lockValue) { _strFrmName = value; } }
        }
        private static string _strFrmName = "";

        /// <summary>
        /// 用于判断是否为配变终端表
        /// </summary>

        public static bool strTerminalMeter
        {
            get { return _strTerminalMeter; }
            set { lock (lockValue) { _strTerminalMeter = value; } }
        }
        private static bool _strTerminalMeter = false;

        /// <summary>
        /// 中间库连接字符串 
        /// </summary>
        public static string strOrcaleConn
        {
            get { return _strOrcaleConn; }
            set { lock (lockValue) { _strOrcaleConn = value; } }
        }
        private static string _strOrcaleConn = "";
        /// <summary>
        /// ClouMeterData.mdb数据库连接字符串
        /// </summary>        
        public static string strOleDbConntion
        {
            get { return _strOleDbConntion; }
            set { lock (lockValue) { _strOleDbConntion = value; } }
        }
        private static string _strOleDbConntion = "";
        /// <summary>
        /// 数据库路径
        /// </summary>
        public static string str_DataPath
        {
            get { return _str_DataPath; }
            set { lock (lockValue) { _str_DataPath = value; } }
        }
        private static string _str_DataPath = "";
        /// <summary>
        /// ClouConfig.mdb数据库连接字符串
        /// </summary>        
        public static string strOleDbConfigConntion
        {
            get { return _strOleDbConfigConntion; }
            set { lock (lockValue) { _strOleDbConfigConntion = value; } }
        }
        private static string _strOleDbConfigConntion = "";
        /// <summary>
        /// SQL语句
        /// </summary>        
        public static string strSQLShowData
        {
            get { return _strSQLShowData; }
            set { lock (lockValue) { _strSQLShowData = value; } }
        }
        private static string _strSQLShowData = "";
        /// <summary>
        /// 工作单编号
        /// </summary>        
        public static string strGZDBH
        {
            get { return _strGZDBH; }
            set { lock (lockValue) { _strGZDBH = value; } }
        }
        private static string _strGZDBH = "";


        /// <summary>
        /// 登录用户名
        /// </summary>        
        public static string strUserName
        {
            get { return _strUserName; }
            set { lock (lockValue) { _strUserName = value; } }
        }
        private static string _strUserName = "";
        /// <summary>
        /// 登录用户名编号
        /// </summary>        
        public static string strUserNameNo
        {
            get { return _strUserNameNo; }
            set { lock (lockValue) { _strUserNameNo = value; } }
        }
        private static string _strUserNameNo = "";
        /// <summary>
        /// 核验员名字
        /// </summary>        
        public static string strCheckName
        {
            get { return _strCheckName; }
            set { lock (lockValue) { _strCheckName = value; } }
        }
        private static string _strCheckName = "";
        /// <summary>
        /// 核验员编号
        /// </summary>        
        public static string strCheckNameNo
        {
            get { return _strCheckNameNo; }
            set { lock (lockValue) { _strCheckNameNo = value; } }
        }
        private static string _strCheckNameNo = "";
        /// <summary>
        /// 用户使用软件类型 如3000s,3000G....
        /// </summary>
        public static string strSoftType
        {
            get { return _strSoftType; }
            set { lock (lockValue) { _strSoftType = value; } }
        }
        private static string _strSoftType = "";
        /// <summary>
        /// 地区编号
        /// </summary>
        public static string strDQBM
        {
            get { return _strDQBM; }
            set { lock (lockValue) { _strDQBM = value; } }
        }
        private static string _strDQBM = "";

        /// <summary>
        /// 铅封一位置
        /// </summary>
        public static string strSeal_1
        {
            get { return _strSeal_1; }
            set { lock (lockValue) { _strSeal_1 = value; } }
        }
        private static string _strSeal_1 = "";

        /// <summary>
        /// 铅封二位置
        /// </summary>
        public static string strSeal_2
        {
            get { return _strSeal_2; }
            set { lock (lockValue) { _strSeal_2 = value; } }
        }
        private static string _strSeal_2 = "";

        /// <summary>
        /// 铅封三位置
        /// </summary>
        public static string strSeal_3
        {
            get { return _strSeal_3; }
            set { lock (lockValue) { _strSeal_3 = value; } }
        }
        private static string _strSeal_3 = "";

        #region ------Ini和XML路径集合--字符型
        /// <summary>
        /// xml文件：SQL日志
        /// </summary>
        public static string XmlPath_SQL
        {
            get { return _XmlPath_SQL; }
            set { lock (lockValue) { _XmlPath_SQL = value; } }
        }
        private static string _XmlPath_SQL = "";

        /// <summary>
        /// Xml文件：NewUserInfo.xml
        /// </summary>
        public static string XmlPath_NewUserInfo
        {
            get { return _XmlPath_NewUserInfo; }
            set { lock (lockValue) { _XmlPath_NewUserInfo = value; } }
        }
        private static string _XmlPath_NewUserInfo = "";
        /// <summary>
        /// Xml文件：Clou_MIS_View.xml
        /// </summary>
        public static string XmlPath_Clou_MIS_View
        {
            get { return _XmlPath_Clou_MIS_View; }
            set { lock (lockValue) { _XmlPath_Clou_MIS_View = value; } }
        }
        private static string _XmlPath_Clou_MIS_View = "";
        /// <summary>
        /// Xml文件：Clou_DataBase.xml
        /// </summary>
        public static string XmlPath_Clou_DataBase
        {
            get { return _XmlPath_Clou_DataBase; }
            set { lock (lockValue) { _XmlPath_Clou_DataBase = value; } }
        }
        private static string _XmlPath_Clou_DataBase = "";


        #endregion
        /// <summary>
        ///是否显示前20条检定日期
        /// </summary>        
        public static bool showInfo_less
        {
            get { return _showInfo_less; }
            set { lock (lockValue) { _showInfo_less = value; } }
        }
        private static bool _showInfo_less = true;

        public static Dictionary<string, string> Dqbm_List
        {
            get { return _Dqbm_List; }
            set { lock (lockValue) { _Dqbm_List = value; } }
        }
        private static Dictionary<string, string> _Dqbm_List=new Dictionary<string,string> ();
        /// <summary>
        /// √ 符号
        /// </summary>        
        public static string strChekFh
        {
            get { return _strChekFh; }
            set { lock (lockValue) { _strChekFh = value; } }
        }
        private static string _strChekFh = "";


        public static string strCondition
        {
            get { return _strCondition; }
            set { lock (lockValue) { _strCondition = value; } }
        }
        private static string _strCondition = "";

        public static string strTableName
        {
            get { return _strTableName; }
            set { lock (lockValue) { _strTableName = value; } }
        }
        private static string _strTableName = "";

        public static string strBino
        {
            get { return _strBino; }
            set { lock (lockValue) { _strBino = value; } }
        }
        private static string _strBino = "";
    }
}
