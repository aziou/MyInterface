using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace TheNewInterface.ViewModel
{
    public class MeterInfoItem : INotifyPropertyChanged
    {
        private int id;
        public int ID           //行号
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        private string strJlbh; //资产编号
        public string StrJlbh
        {
            get
            {
                return strJlbh;
            }
            set
            {
                strJlbh = value;
                OnPropertyChanged("StrJlbh");
            }
        }
        private string strJdjl; //检定结论
        public string StrJdjl
        {
            get
            {
                return strJdjl;
            }
            set
            {
                strJdjl = value;
                OnPropertyChanged("StrJdjl");
            }
        }
        private string strGZDBH; // 出厂编号
        public string StrGZDBH
        {
            get
            {
                return strGZDBH;
            }
            set
            {
                strGZDBH = value;
                OnPropertyChanged("StrGZDBH");
            }
        }
        private string strWD; //温度
        public string StrWD
        {
            get
            {
                return strWD;
            }
            set
            {
                strWD = value;
                OnPropertyChanged("StrWD");
            }
        }
        private string strSD; // 湿度
        public string StrSD
        {
            get
            {
                return strSD;
            }
            set
            {
                strSD = value;
                OnPropertyChanged("StrSD");
            }
        }
        private int intMeterNum; //表位号
        public int IntMeterNum
        {
            get
            {
                return intMeterNum;
            }
            set
            {
                intMeterNum = value;
                OnPropertyChanged("IntMeterNum");
            }
        }
        private string strJdrq; // 检定日期
        public string StrJdrq
        {
            get
            {
                return strJdrq;
            }
            set
            {
                strJdrq = value;
                OnPropertyChanged("StrJdrq");
            }
        }
        private string strJyy; // 检验员
        public string StrJyy
        {
            get
            {
                return strJyy;
            }
            set
            {
                strJyy = value;
                OnPropertyChanged("StrJyy");
            }
        }
        private string strJddw; // 检定单位
        public string StrJddw
        {
            get
            {
                return strJddw;
            }
            set
            {
                strJddw = value;
                OnPropertyChanged("StrJddw");
            }
        }
        private string strHyy; // 核验员
        public string StrHyy
        {

            get
            {
                return strHyy;
            }
            set
            {
                strHyy = value;
                OnPropertyChanged("StrHyy");
            }
        }
        private string strYXRQ; // 有效日期
        public string StrYXRQ
        {
            get
            {
                return strYXRQ;
            }
            set
            {
                strYXRQ = value;
                OnPropertyChanged("StrYXRQ");
            }
        }
        private string strBZZZZCBH; // 台体编号
        public string StrBZZZZCBH
        {
            get
            {
                return strBZZZZCBH;
            }
            set
            {
                strBZZZZCBH = value;
                OnPropertyChanged("StrBZZZZCBH");
            }
        }
        //=================================================================================================================
        //=================================================================================================================
        //=================================================================================================================
        private string strTestType; // 检定类型
        public string StrTestType
        {
            get
            {
                return strTestType;
            }
            set
            {
                strTestType = value;
                OnPropertyChanged("StrTestType");
            }
        }
        private string strBlx; // 表类型
        public string StrBlx
        {
            get
            {
                return strBlx;
            }
            set
            {
                strBlx = value;
                OnPropertyChanged("StrBlx");
            }
        }
        private string strBmc; // 表名称
        public string StrBmc
        {
            get
            {
                return strBmc;
            }
            set
            {
                strBmc = value;
                OnPropertyChanged("StrBmc");
            }
        }
        private string strUb; // 额定电压
        public string StrUb
        {
            get
            {
                return strUb;
            }
            set
            {
                strUb = value;
                OnPropertyChanged("StrUb");
            }
        }
        private string strIb; // 额定电流
        public string StrIb
        {
            get
            {
                return strIb;
            }
            set
            {
                strIb = value;
                OnPropertyChanged("StrIb");
            }
        }
        private string strMeterLevel; // 表等级
        public string StrMeterLevel
        {
            get
            {
                return strMeterLevel;
            }
            set
            {
                strMeterLevel = value;
                OnPropertyChanged("StrMeterLevel");
            }
        }
        private string strManufacturer; // 生产厂家
        public string StrManufacture
        {
            get
            {
                return strManufacturer;
            }
            set
            {
                strManufacturer = value;
                OnPropertyChanged("StrManufacture");
            }
        }
        private string strVerification; // other
        public string StrVerification
        {
            get
            {
                return strVerification;
            }
            set
            {
                strVerification = value;
                OnPropertyChanged("StrVerification");
            }
        }
        private string strWcResult; // other
        public string StrWcResult
        {
            get
            {
                return strWcResult;
            }
            set
            {
                strWcResult = value;
                OnPropertyChanged("StrWcResult");
            }
        }
        private string strShellSeal; // 外壳封
        public string StrShellSeal
        {
            get
            {
                return strShellSeal;
            }
            set
            {
                strShellSeal = value;
                OnPropertyChanged("StrShellSeal");
            }
        }
        private string strCodeSeal; // 编程封
        public string StrCodeSeal
        {
            get
            {
                return strCodeSeal;
            }
            set
            {
                strCodeSeal = value;
                OnPropertyChanged("StrCodeSeals");
            }
        }
        //===========================================电能表检定误差数据=====================
        #region 电能表检定
        private string strGLFXDM; // 功率方向代码
        public string StrGLFXDM
        {
            get
            {
                return strGLFXDM;
            }
            set
            {
                strGLFXDM = value;
                OnPropertyChanged("StrGLFXDM");
            }
        }
        private string strGLYSDM; // 功率因素代码
        public string StrGLYSDM
        {
            get
            {
                return strGLYSDM;
            }
            set
            {
                strGLYSDM = value;
                OnPropertyChanged("StrGLYSDM");
            }
        }
        private string strFZDLDM; // 负载电流代码
        public string StrFZDLDM
        {
            get
            {
                return strFZDLDM;
            }
            set
            {
                strFZDLDM = value;
                OnPropertyChanged("StrFZDLDM");
            }
        }
        private string strXBDM; // 相别代码
        public string StrXBDM
        {
            get
            {
                return strXBDM;
            }
            set
            {
                strXBDM = value;
                OnPropertyChanged("StrXBDM");
            }
        }
        private string strFZLXDM; // 负载类型代码
        public string StrFZLXDM
        {
            get
            {
                return strFZLXDM;
            }
            set
            {
                strFZLXDM = value;
                OnPropertyChanged("StrFZLXDM");
            }
        }
        private string strFYDM; // 分元代码
        public string StrFYDM
        {
            get
            {
                return strFYDM;
            }
            set
            {
                strFYDM = value;
                OnPropertyChanged("StrFYDM");
            }
        }
        private string strWC1; // 误差1
        public string StrWC1
        {
            get
            {
                return strWC1;
            }
            set
            {
                strWC1 = value;
                OnPropertyChanged("StrWC1");
            }
        }
        private string strWC2; // 误差2
        public string StrWC2
        {
            get
            {
                return strWC2;
            }
            set
            {
                strWC2 = value;
                OnPropertyChanged("StrWC2");
            }
        }
        private string strWC3; // 误差3
        public string StrWC3
        {
            get
            {
                return strWC3;
            }
            set
            {
                strWC3 = value;
                OnPropertyChanged("StrWC3");
            }
        }
        private string strWC4; // 误差4
        public string StrWC4
        {
            get
            {
                return strWC4;
            }
            set
            {
                strWC4 = value;
                OnPropertyChanged("StrWC4");
            }
        }
        private string strWC5; // 误差5
        public string StrWC5
        {
            get
            {
                return strWC5;
            }
            set
            {
                strWC5 = value;
                OnPropertyChanged("StrWC5");
            }
        }
        private string strWCPJZ; // 误差平均值
        public string StrWCPJZ
        {
            get
            {
                return strWCPJZ;
            }
            set
            {
                strWCPJZ = value;
                OnPropertyChanged("StrWCPJZ");
            }
        }
        private string strXYZ; // 误差修约值
        public string StrXYZ
        {
            get
            {
                return strXYZ;
            }
            set
            {
                strXYZ = value;
                OnPropertyChanged("StrXYZ");
            }
        }
        private string strJLDM; // 结论代码
        public string StrJLDM
        {
            get
            {
                return strJLDM;
            }
            set
            {
                strJLDM = value;
                OnPropertyChanged("StrJLDM");
            }
        }
        private string strWCCZXYZ; // 误差差值修约值
        public string StrWCCZXYZ
        {
            get
            {
                return strWCCZXYZ;
            }
            set
            {
                strWCCZXYZ = value;
                OnPropertyChanged("StrWCCZXYZ");
            }
        }
        private string strWCCZ; //不平平衡负载与平衡负载的误差差值
        public string StrWCCZ
        {
            get
            {
                return strWCCZ;
            }
            set
            {
                strWCCZ = value;
                OnPropertyChanged("StrWCCZ");
            }
        }
        private string strDQBM; //地区编码
        public string StrDQMB
        {
            get
            {
                return strDQBM;
            }
            set
            {
                strDQBM = value;
                OnPropertyChanged("StrDQMB");
            }
        }
        #endregion
        //====================================日计量误差记录================================
        #region 日计量误差记录
        private string strCSZ1; // 测试值1
        public string StrCSZ1
        {
            get
            {
                return strCSZ1;
            }
            set
            {
                strCSZ1 = value;
                OnPropertyChanged("StrCSZ1");
            }
        }
        private string strCSZ2; // 测试值2
        public string StrCSZ2
        {
            get
            {
                return strCSZ2;
            }
            set
            {
                strCSZ2 = value;
                OnPropertyChanged("StrCSZ2");
            }
        }
        private string strCSZ3; // 测试值3
        public string StrCSZ3
        {
            get
            {
                return strCSZ3;
            }
            set
            {
                strCSZ3 = value;
                OnPropertyChanged("StrCSZ3");
            }
        }
        private string strCSZ4; // 测试值4
        public string StrCSZ4
        {
            get
            {
                return strCSZ4;
            }
            set
            {
                strCSZ4 = value;
                OnPropertyChanged("StrCSZ4");
            }
        }
        private string strCSZ5; // 测试值5
        public string StrCSZ5
        {
            get
            {
                return strCSZ5;
            }
            set
            {
                strCSZ5 = value;
                OnPropertyChanged("StrCSZ5");
            }
        }
        private string strPJZ; // 平均值
        public string StrPJZ
        {
            get
            {
                return strPJZ;
            }
            set
            {
                strPJZ = value;
                OnPropertyChanged("StrPJZ");
            }
        }
        private string strRJSDQBM; //  日计时地区编码
        public string StrRJSDQBM
        {
            get
            {
                return strRJSDQBM;
            }
            set
            {
                strRJSDQBM = value;
                OnPropertyChanged("StrRJSDQBM");
            }
        }
        #endregion
        //================================需量记录表=======================================
        #region 需量记录表
        private string strXLFZDLDM; // 负载电流代码
        public string StrXLFZDLDM
        {
            get
            {
                return strXLFZDLDM;
            }
            set
            {
                strXLFZDLDM = value;
                OnPropertyChanged("StrXLFZDLDM");
            }
        }
        private string strBZZDXL; // 标准最大需量
        public string StrBZZDXL
        {
            get
            {
                return strBZZDXL;
            }
            set
            {
                strBZZDXL = value;
                OnPropertyChanged("StrBZZDXL");
            }
        }
        private string strSJXL; // 实际需量
        public string StrSJXL
        {
            get
            {
                return strSJXL;
            }
            set
            {
                strSJXL = value;
                OnPropertyChanged("StrSJXL");
            }
        }
        private string strWCZ; //  误差值
        public string StrWCZ
        {
            get
            {
                return strWCZ;
            }
            set
            {
                strWCZ = value;
                OnPropertyChanged("StrWCZ");
            }
        }
        private string strXLJLDM; // 需量结论代码
        public string StrXLJLDM
        {
            get
            {
                return strXLJLDM;
            }
            set
            {
                strXLJLDM = value;
                OnPropertyChanged("StrXLJLDM");
            }
        }
        private string strXLDQBM; // 需量地区编码
        public string StrXLDQBM
        {
            get
            {
                return strXLDQBM;
            }
            set
            {
                strXLDQBM = value;
                OnPropertyChanged("StrXLDQBM");
            }
        }
        #endregion
        //=====================================投切时段======================================
        #region 投切时段
        private string strTime; // 时段
        public string StrTime
        {
            get
            {
                return strTime;
            }
            set
            {
                strTime = value;
                OnPropertyChanged("StrTime");
            }
        }
        private string strBZTQSJ; // 标准投切时间
        public string StrBZTQSJ
        {
            get
            {
                return strBZTQSJ;
            }
            set
            {
                strBZTQSJ = value;
                OnPropertyChanged("StrBZTQSJ");
            }
        }
        private string strSJTQSJ; // 实际投切时间
        public string StrSJTQSJ
        {
            get
            {
                return strSJTQSJ;
            }
            set
            {
                strSJTQSJ = value;
                OnPropertyChanged("StrSJTQSJ");
            }
        }
        private string strTQWC; // 投切误差
        public string StrTQWC
        {
            get
            {
                return strTQWC;
            }
            set
            {
                strTQWC = value;
                OnPropertyChanged("StrTQWC");
            }
        }
        private string strTQDQBM; // 投切地区编码
        public string StrTQDQBM
        {
            get
            {
                return strTQDQBM;
            }
            set
            {
                strTQDQBM = value;
                OnPropertyChanged("StrTQDQBM");
            }
        }
        #endregion
        //====================================电能表示数=================================
        #region 电能表示数
        private string strSSLXDM; // 示数类型代码
        public string StrSSLXDM
        {
            get
            {
                return strSSLXDM;
            }
            set
            {
                strSSLXDM = value;
                OnPropertyChanged("StrSSLXDM");
            }
        }
        private string strBSS; // 表示数
        public string StrBSS
        {
            get
            {
                return strBSS;
            }
            set
            {
                strBSS = value;
                OnPropertyChanged("StrBSS");
            }
        }
        private string strCBSJ; // 抄表时间
        public string StrCBSJ
        {
            get
            {
                return strCBSJ;
            }
            set
            {
                strCBSJ = value;
                OnPropertyChanged("StrCBSJ");
            }
        }
        private string strSSDQBM; // 示数地区编码
        public string StrSSDQBM
        {
            get
            {
                return strSSDQBM;
            }
            set
            {
                strSSDQBM = value;
                OnPropertyChanged("StrSSDQBM");
            }
        }
        #endregion
        //================================电能表走字结论====================================
        #region 电能表走字结论
        private string strZZSSLXDM; //示数类型代码
        public string StrZZSSLXDM
        {
            get
            {
                return strZZSSLXDM;
            }
            set
            {
                strZZSSLXDM = value;
                OnPropertyChanged("StrZZSSLXDM");
            }
        }
        private string strBZQQSS; //标准器起示数
        public string StrBZQQSS
        {
            get
            {
                return strBZQQSS;
            }
            set
            {
                strBZQQSS = value;
                OnPropertyChanged("StrBZQQSS");
            }
        }
        private string strBZQZSS; //标准器止示数
        public string StrBZQZSS
        {
            get
            {
                return strBZQZSS;
            }
            set
            {
                strBZQZSS = value;
                OnPropertyChanged("StrBZQZSS");
            }
        }
        private string strQSS; // 起示数
        public string StrQSS
        {
            get
            {
                return strQSS;
            }
            set
            {
                strQSS = value;
                OnPropertyChanged("StrQSS");
            }
        }
        private string strZSS; //止示数
        public string StrZSS
        {
            get
            {
                return strZSS;
            }
            set
            {
                strZSS = value;
                OnPropertyChanged("StrZSS");
            }
        }
        private string strZZWC; //走字误差
        public string StrZZWC
        {
            get
            {
                return strZZWC;
            }
            set
            {
                strZZWC = value;
                OnPropertyChanged("StrZZWC");
            }
        }
        private string strZZDQBM; //走字地区编码
        public string StrZZDQBM
        {
            get
            {
                return strZZDQBM;
            }
            set
            {
                strZZDQBM = value;
                OnPropertyChanged("StrZZDQBM");
            }
        }

        #endregion
        //=================================封印====================
        #region 封印
        private string strBGBZ; //变更标志
        public string StrBGBZ
        {
            get
            {
                return strBGBZ;
            }
            set
            {
                strBGBZ = value;
                OnPropertyChanged("StrBGBZ");
            }
        }
        private string strFYZCBH; //封印资产编号
        public string StrFYZCBH
        {
            get
            {
                return strFYZCBH;
            }
            set
            {
                strFYZCBH = value;
                OnPropertyChanged("StrFYZCBH");
            }
        }
        private string strJFWZDM; //加封位置代码
        public string StrJFWZDM
        {
            get
            {
                return strJFWZDM;
            }
            set
            {
                strJFWZDM = value;
                OnPropertyChanged("StrJFWZDM");
            }
        }
        private string strCodeFY; // 编程封印
        public string StrCodeFY
        {
            get
            {
                return strCodeFY;
            }
            set
            {
                strCodeFY = value;
                OnPropertyChanged("StrCodeFY");
            }
        }
        private string strJFSJ; //加封时间
        public string StrJFSJ
        {
            get
            {
                return strJFSJ;
            }
            set
            {
                strJFSJ = value;
                OnPropertyChanged("StrJFSJ");
            }
        }
        private string strFYDQBM; //封印地区编码
        public string StrFYDQBM
        {
            get
            {
                return strFYDQBM;
            }
            set
            {
                strFYDQBM = value;
                OnPropertyChanged("StrFYDQBM");
            }
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
