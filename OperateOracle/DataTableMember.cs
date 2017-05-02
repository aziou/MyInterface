using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
namespace OperateOracle
{
    public class DataTableMember : INotifyPropertyChanged
    {
        private volatile static DataTableMember _instance = null;
        private static readonly object lockHelper = new object();
        public static DataTableMember CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new DataTableMember();
                }
            }
            return _instance;
        }
        private List<string> infoTime;
        public List<string> InfoTime
        {
            get
            {
                return infoTime;
            }
            set
            {
                infoTime = value;
                OnPropertyChanged("InfoTime");
            }
        }
        private DataTable dt_temp;
        public DataTable Dt_temp
        {
            get
            {
                return dt_temp;
            }
            set
            {
                dt_temp = value;
                OnPropertyChanged("Dt_temp");

            }

        }
        private int intSoleMyId;  //唯一识别各个表的Id
        public int IntSoleMyId
        {
            get;
            set;
        }
        #region 基本信息
        private int id; //行号
        public int ID
        {
            get;
            set;
        }
        private string strJlbh; //资产编号
        public string StrJlbh
        {
            get;
            set;
        }
        private string strJdjl; //检定结论
        public string StrJdjl
        {
            get;
            set;
        }
        private string strGZDBH; // 出厂编号
        public string StrGZDBH
        {
            get;
            set;
        }
        private string strWD; //温度
        public string StrWD
        {
            get;
            set;
        }
        private string strSD; // 湿度
        public string StrSD
        {
            get;
            set;
        }
        private int intMeterNum; //表位号
        public int IntMeterNum
        {
            get;
            set;
        }
        private string strJdrq; // 检定日期
        public string StrJdrq
        {
            get;
            set;
        }
        private string strYXRQ; // 有效日期
        public string StrYXRQ
        {
            get;
            set;
        }
        private string strJyy; // 检验员
        public string StrJyy
        {
            get;
            set;
        } private string strJddw; // 检定单位
        public string StrJddw
        {
            get;
            set;
        } private string strHyy; // 核验员
        public string StrHyy
        {
            get;
            set;
        }
        private string strBZZZZCBH; // 台体编号
        public string StrBZZZZCBH
        {
            get;
            set;
        }
        //=============================================================================================================================
        //=================================================TextBox Value============================================================
        //=============================================================================================================================
        private string strTestType; // 检定类型
        public string StrTestType
        {
            get;
            set;
        }
        private string strBlx; // 表类型
        public string StrBlx
        {
            get;
            set;
        }
        private string strBmc; // 表名称
        public string StrBmc
        {
            get;
            set;
        }
        private string strUb; // 额定电压
        public string StrUb
        {
            get;
            set;
        }
        private string strIb; // 额定电流
        public string StrIb
        {
            get;
            set;
        }
        private string strMeterLevel; // 表等级
        public string StrMeterLevel
        {
            get;
            set;
        }
        private string strManufacturer; // 生产厂家
        public string StrManufacture
        {
            get;
            set;
        }
        private string strVerification; // other
        public string StrVerification
        {
            get;
            set;
        }
        private string strWcResult; // 
        public string StrWcResult
        {
            get;
            set;
        }
        private string strShellSeal; // 外壳封
        public string StrShellSeal
        {
            get;
            set;
        }
        private string strCodeSeal; // 编程封
        public string StrCodeSeal
        {
            get;
            set;
        }
        #endregion
        //==============电能表误差检定============================
        #region 电能表误差检定
        private string strGLFXDM; // 功率方向代码
        public string StrGLFXDM
        {
            get;
            set;
        }
        private string strGLYSDM; // 功率因素代码
        public string StrGLYSDM
        {
            get;
            set;
        }
        private string strFZDLDM; // 负载电流代码
        public string StrFZDLDM
        {
            get;
            set;
        }
        private string strXBDM; // 相别代码
        public string StrXBDM
        {
            get;
            set;
        }
        private string strFZLXDM; // 负载类型代码
        public string StrFZLXDM
        {
            get;
            set;
        }
        private string strFYDM; // 分元代码
        public string StrFYDM
        {
            get;
            set;
        }
        private string strWC1; // 误差1
        public string StrWC1
        {
            get;
            set;
        }
        private string strWC2; // 误差2
        public string StrWC2
        {
            get;
            set;
        }
        private string strWC3; // 误差3
        public string StrWC3
        {
            get;
            set;
        }
        private string strWC4; // 误差4
        public string StrWC4
        {
            get;
            set;
        }
        private string strWC5; // 误差5
        public string StrWC5
        {
            get;
            set;
        }
        private string strWCPJZ; // 误差平均值
        public string StrWCPJZ
        {
            get;
            set;
        }
        private string strXYZ; // 误差修约值
        public string StrXYZ
        {
            get;
            set;
        }
        private string strJLDM; // 结论代码
        public string StrJLDM
        {
            get;
            set;
        }
        private string strWCCZXYZ; // 误差差值修约值
        public string StrWCCZXYZ
        {
            get;
            set;
        }
        private string strWCCZ; //不平平衡负载与平衡负载的误差差值
        public string StrWCCZ
        {
            get;
            set;
        }
        private string strDQBM; //地区编码
        public string StrDQMB
        {
            get;
            set;
        }
        #endregion
        //==============日计量误差记录============================
        #region 日计量误差记录
        private string strCSZ1; // 测试值1
        public string StrCSZ1
        {
            get;
            set;
        }
        private string strCSZ2; // 测试值2
        public string StrCSZ2
        {
            get;
            set;
        }
        private string strCSZ3; // 测试值3
        public string StrCSZ3
        {
            get;
            set;
        }
        private string strCSZ4; // 测试值4
        public string StrCSZ4
        {
            get;
            set;
        }
        private string strCSZ5; // 测试值5
        public string StrCSZ5
        {
            get;
            set;
        }
        private string strPJZ; // 平均值
        public string StrPJZ
        {
            get;
            set;
        }
        private string strRJSDQBM; //  日计时地区编码
        public string StrRJSDQBM
        {
            get;
            set;
        }

        #endregion
        //==============需量记录=================================
        #region 需量记录
        private string strXLFZDLDM; // 负载电流代码
        public string StrXLFZDLDM
        {
            get;
            set;
        }
        private string strBZZDXL; // 标准最大需量
        public string StrBZZDXL
        {
            get;
            set;
        }
        private string strSJXL; // 实际需量
        public string StrSJXL
        {
            get;
            set;
        }
        private string strWCZ; //  误差值
        public string StrWCZ
        {
            get;
            set;
        }
        private string strXLJLDM; // 需量结论代码
        public string StrXLJLDM
        {
            get;
            set;
        }
        private string strXLDQBM; // 需量地区编码
        public string StrXLDQBM
        {
            get;
            set;
        }

        #endregion
        //=============时段投切=================================
        #region 时段投切
        private string strTime; // 时段
        public string StrTime
        {
            get;
            set;
        }
        private string strBZTQSJ; // 标准投切时间
        public string StrBZTQSJ
        {
            get;
            set;
        }
        private string strSJTQSJ; // 实际投切时间
        public string StrSJTQSJ
        {
            get;
            set;
        }
        private string strTQWC; // 投切误差
        public string StrTQWC
        {
            get;
            set;
        }
        private string strTQDQBM; // 投切地区编码
        public string StrTQDQBM
        {
            get;
            set;
        }
        #endregion
        //========电能表示数===============================
        #region 电能表示数
        private string strSSLXDM; // 示数类型代码
        public string StrSSLXDM
        {
            get;
            set;
        }
        private string strBSS; // 表示数
        public string StrBSS
        {
            get;
            set;
        }
        private string strCBSJ; // 抄表时间
        public string StrCBSJ
        {
            get;
            set;
        }
        private string strSSDQBM; // 示数地区编码
        public string StrSSDQBM
        {
            get;
            set;
        }
        #endregion
        //==============电能表走字结论=======================
        #region 电能表走字结论
        private string strZZSSLXDM; //示数类型代码
        public string StrZZSSLXDM
        {
            get;
            set;
        }
        private string strBZQQSS; //标准器起示数
        public string StrBZQQSS
        {
            get;
            set;
        }
        private string strBZQZSS; //标准器止示数
        public string StrBZQZSS
        {
            get;
            set;
        }
        private string strQSS; // 起示数
        public string StrQSS
        {
            get;
            set;
        }
        private string strZSS; //止示数
        public string StrZSS
        {
            get;
            set;
        }
        private string strZZWC; //走字误差
        public string StrZZWC
        {
            get;
            set;
        }
        private string strZZDQBM; //走字地区编码
        public string StrZZDQBM
        {
            get;
            set;
        }

        #endregion
        //==================封印============================
        #region 封印
        private string strBGBZ; //变更标志
        public string StrBGBZ
        {
            get;
            set;
        }
        private string strFYZCBH; //封印资产编号
        public string StrFYZCBH
        {
            get;
            set;
        }
        private string strJFWZDM; //加封位置代码
        public string StrJFWZDM
        {
            get;
            set;
        }
        private string strCodeFY; // 编程封印
        public string StrCodeFY
        {
            get;
            set;
        }
        private string strJFSJ; //加封时间
        public string StrJFSJ
        {
            get;
            set;
        }
        private string strFYDQBM; //封印地区编码
        public string StrFYDQBM
        {
            get;
            set;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
