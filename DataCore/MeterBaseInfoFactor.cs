using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace DataCore
{
    public class MeterBaseInfoFactor : INotifyPropertyChanged
    {

        private bool bolTerminalWorkNum=false;
        public bool BolTerminalWorkNum
        {
            get
            {
                return bolTerminalWorkNum;
            }
            set
            {
                bolTerminalWorkNum = value;
                OnPropertyChanged("BolTerminalWorkNum");
            }
        }

        private bool bolIfup;
        public bool BolIfup
        {
            get
            {
                return bolIfup;
            }
            set
            {
                bolIfup = value;
                OnPropertyChanged("BolIfup");
            }
        }
        //唯一编号 
        private string pk_LNG_METER_ID;
        public string PK_LNG_METER_ID
        {
            get
            {
                return pk_LNG_METER_ID;
            }
            set
            {
                pk_LNG_METER_ID = value;
                OnPropertyChanged("PK_LNG_METER_ID");
            }
        }

        //台体编号 
        private string avr_DEVICE_ID;
        public string AVR_DEVICE_ID
        {
            get
            {
                return avr_DEVICE_ID;
            }
            set
            {
                avr_DEVICE_ID = value;
                OnPropertyChanged("AVR_DEVICE_ID");
            }
        }
        //表位号 
        private string lng_BENCH_POINT_NO;
        public string LNG_BENCH_POINT_NO
        {
            get
            {
                return lng_BENCH_POINT_NO;
            }
            set
            {
                lng_BENCH_POINT_NO = value;
                OnPropertyChanged("LNG_BENCH_POINT_NO");
            }
        }
        //计量编号、资产编号 
        private string Avr_ASSET_NO;
        public string AVR_ASSET_NO
        {
            get
            {
                return Avr_ASSET_NO;
            }
            set
            {
                Avr_ASSET_NO = value;
                OnPropertyChanged("AVR_ASSET_NO");
            }
        }
        //出厂编号 
        private string avr_MADE_NO;
        public string AVR_MADE_NO
        {
            get
            {
                return avr_MADE_NO;
            }
            set
            {
                avr_MADE_NO = value;
                OnPropertyChanged("AVR_MADE_NO");
            }
        }
        //条形码 
        private string avr_BAR_CODE;
        public string AVR_BAR_CODE
        {
            get
            {
                return avr_BAR_CODE;
            }
            set
            {
                avr_BAR_CODE = value;
                OnPropertyChanged("AVR_BAR_CODE");
            }
        }
        //表通信地址 
        private string avr_ADDRESS;
        public string AVR_ADDRESS
        {
            get
            {
                return avr_ADDRESS;
            }
            set
            {
                avr_ADDRESS = value;
                OnPropertyChanged("AVR_ADDRESS");
            }
        }
        //制造厂家 
        private string avr_FACTORY;
        public string AVR_FACTORY
        {
            get
            {
                return avr_FACTORY;
            }
            set
            {
                avr_FACTORY = value;
                OnPropertyChanged("AVR_FACTORY");
            }
        }
        //表型号 
        private string avr_METER_MODEL;
        public string AVR_METER_MODEL
        {
            get
            {
                return avr_METER_MODEL;
            }
            set
            {
                avr_METER_MODEL = value;
                OnPropertyChanged("AVR_METER_MODEL");
            }
        }
        //表常数 
        private string avr_AR_CONSTANT;
        public string AVR_AR_CONSTANT
        {
            get
            {
                return avr_AR_CONSTANT;
            }
            set
            {
                avr_AR_CONSTANT = value;
                OnPropertyChanged("AVR_AR_CONSTANT");
            }
        }
        //表类型 
        private string avr_METER_TYPE;
        public string AVR_METER_TYPE
        {
            get
            {
                return avr_METER_TYPE;
            }
            set
            {
                avr_METER_TYPE = value;
                OnPropertyChanged("AVR_METER_TYPE");
            }
        }

        //等级 
        private string avr_AR_CLASS;
        public string AVR_AR_CLASS
        {
            get
            {
                return avr_AR_CLASS;
            }
            set
            {
                avr_AR_CLASS = value;
                OnPropertyChanged("AVR_AR_CLASS");
            }
        }
        //出厂日期 
        private string avr_MADE_DATE;
        public string AVR_MADE_DATE
        {
            get
            {
                return avr_MADE_DATE;
            }
            set
            {
                avr_MADE_DATE = value;
                OnPropertyChanged("AVR_MADE_DATE");
            }
        }
        //送检单位 
        private string avr_CUSTOMER;
        public string AVR_CUSTOMER
        {
            get
            {
                return avr_CUSTOMER;
            }
            set
            {
                avr_CUSTOMER = value;
                OnPropertyChanged("AVR_CUSTOMER");
            }
        }
        //证书编号 
        private string avr_CERTIFICATE_NO;
        public string AVR_CERTIFICATE_NO
        {
            get
            {
                return avr_CERTIFICATE_NO;
            }
            set
            {
                avr_CERTIFICATE_NO = value;
                OnPropertyChanged("AVR_CERTIFICATE_NO");
            }
        }
        //表名称 
        private string avr_METER_NAME;
        public string AVR_METER_NAME
        {
            get
            {
                return avr_METER_NAME;
            }
            set
            {
                avr_METER_NAME = value;
                OnPropertyChanged("AVR_METER_NAME");
            }
        }
        //测量方式 0=三相三线；1=三相四线
        private string avr_WIRING_MODE;
        public string AVR_WIRING_MODE
        {
            get
            {
                return avr_WIRING_MODE;
            }
            set
            {
                avr_WIRING_MODE = value;
                OnPropertyChanged("AVR_WIRING_MODE");
            }
        }
        //额定电压（V） 
        private string avr_UB;
        public string AVR_UB
        {
            get
            {
                return avr_UB;
            }
            set
            {
                avr_UB = value;
                OnPropertyChanged("AVR_UB");
            }
        }
        //额定电流 
        private string avr_IB;
        public string AVR_IB
        {
            get
            {
                return avr_IB;
            }
            set
            {
                avr_IB = value;
                OnPropertyChanged("AVR_IB");
            }
        }
        //频率。单位HZ 
        private string avr_FREQUENCY;
        public string AVR_FREQUENCY
        {
            get
            {
                return avr_FREQUENCY;
            }
            set
            {
                avr_FREQUENCY = value;
                OnPropertyChanged("AVR_FREQUENCY");
            }
        }
        //止逆器  0=不经止逆器；1=经止逆器
        private string chr_CC_PREVENT_FLAG;
        public string CHR_CC_PREVENT_FLAG
        {
            get
            {
                return chr_CC_PREVENT_FLAG;
            }
            set
            {
                chr_CC_PREVENT_FLAG = value;
                OnPropertyChanged("CHR_CC_PREVENT_FLAG");
            }
        }

        //互感器 0=不经互感器；1=经互感器
        private string chr_CT_CONNECTION_FLAG;
        public string CHR_CT_CONNECTION_FLAG
        {
            get
            {
                return chr_CT_CONNECTION_FLAG;
            }
            set
            {
                chr_CT_CONNECTION_FLAG = value;
                OnPropertyChanged("CHR_CT_CONNECTION_FLAG");
            }
        }
        //检定类型
        private string avr_TEST_TYPE;
        public string AVR_TEST_TYPE
        {
            get
            {
                return avr_TEST_TYPE;
            }
            set
            {
                avr_TEST_TYPE = value;
                OnPropertyChanged("AVR_TEST_TYPE");
            }
        }
        //检定日期
        private string dtm_TEST_DATE;
        public string DTM_TEST_DATE
        {
            get
            {
                return dtm_TEST_DATE;
            }
            set
            {
                dtm_TEST_DATE = value;
                OnPropertyChanged("DTM_TEST_DATE");
            }
        }
        //计检日期
        private string dtm_VALID_DATE;
        public string DTM_VALID_DATE
        {
            get
            {
                return dtm_VALID_DATE;
            }
            set
            {
                dtm_VALID_DATE = value;
                OnPropertyChanged("DTM_VALID_DATE");
            }
        }
        //温度
        private string avr_TEMPERATURE;
        public string AVR_TEMPERATURE
        {
            get
            {
                return avr_TEMPERATURE;
            }
            set
            {
                avr_TEMPERATURE = value;
                OnPropertyChanged("AVR_TEMPERATURE");
            }
        }
        //湿度
        private string avr_HUMIDITY;
        public string AVR_HUMIDITY
        {
            get
            {
                return avr_HUMIDITY;
            }
            set
            {
                avr_HUMIDITY = value;
                OnPropertyChanged("AVR_HUMIDITY");
            }
        }
        //检定结论。Y/N
        //合格Y、不合格N

        private string avr_TOTAL_CONCLUSION;
        public string AVR_TOTAL_CONCLUSION
        {
            get
            {
                return avr_TOTAL_CONCLUSION;
            }
            set
            {
                avr_TOTAL_CONCLUSION = value;
                OnPropertyChanged("AVR_TOTAL_CONCLUSION");
            }
        }
        //检验员
        private string avr_TEST_PERSON;
        public string AVR_TEST_PERSON
        {
            get
            {
                return avr_TEST_PERSON;
            }
            set
            {
                avr_TEST_PERSON = value;
                OnPropertyChanged("AVR_TEST_PERSON");
            }
        }
        //核验员
        private string avr_AUDIT_PERSON;
        public string AVR_AUDIT_PERSON
        {
            get
            {
                return avr_AUDIT_PERSON;
            }
            set
            {
                avr_AUDIT_PERSON = value;
                OnPropertyChanged("AVR_AUDIT_PERSON");
            }
        }
        //主管
        private string avr_SUPERVISOR;
        public string AVR_SUPERVISOR
        {
            get
            {
                return avr_SUPERVISOR;
            }
            set
            {
                avr_SUPERVISOR = value;
                OnPropertyChanged("AVR_SUPERVISOR");
            }
        }
        //要检此表。1要检，0不检
        private string chr_CHECKED;
        public string CHR_CHECKED
        {
            get
            {
                return chr_CHECKED;
            }
            set
            {
                chr_CHECKED = value;
                OnPropertyChanged("CHR_CHECKED");
            }
        }
        //数据是否已上网标志。0：未上传,1：已上传

        private string chr_UPLOAD_FLAG;
        public string CHR_UPLOAD_FLAG
        {
            get
            {
                return chr_UPLOAD_FLAG;
            }
            set
            {
                chr_UPLOAD_FLAG = value;
                OnPropertyChanged("CHR_UPLOAD_FLAG");
            }
        }
        //铅封号1
        private string avr_SEAL_1;
        public string AVR_SEAL_1
        {
            get
            {
                return avr_SEAL_1;
            }
            set
            {
                avr_SEAL_1 = value;
                OnPropertyChanged("AVR_SEAL_1");
            }
        }
        //铅封号2
        private string avr_SEAL_2;
        public string AVR_SEAL_2
        {
            get
            {
                return avr_SEAL_2;
            }
            set
            {
                avr_SEAL_2 = value;
                OnPropertyChanged("AVR_SEAL_2");
            }
        }
        //铅封号3
        private string avr_SEAL_3;
        public string AVR_SEAL_3
        {
            get
            {
                return avr_SEAL_3;
            }
            set
            {
                avr_SEAL_3 = value;
                OnPropertyChanged("AVR_SEAL_3");
            }
        }
        //铅封号4
        private string avr_SEAL_4;
        public string AVR_SEAL_4
        {
            get
            {
                return avr_SEAL_4;
            }
            set
            {
                avr_SEAL_4 = value;
                OnPropertyChanged("AVR_SEAL_4");
            }
        }
        //铅封号5
        private string avr_SEAL_5;
        public string AVR_SEAL_5
        {
            get
            {
                return avr_SEAL_5;
            }
            set
            {
                avr_SEAL_5 = value;
                OnPropertyChanged("AVR_SEAL_5");
            }
        }
        //软件版本号
        private string avr_SOFT_VER;
        public string AVR_SOFT_VER
        {
            get
            {
                return avr_SOFT_VER;
            }
            set
            {
                avr_SOFT_VER = value;
                OnPropertyChanged("AVR_SOFT_VER");
            }
        }
        //硬件版本号
        private string avr_HARD_VER;
        public string AVR_HARD_VER
        {
            get
            {
                return avr_HARD_VER;
            }
            set
            {
                avr_HARD_VER = value;
                OnPropertyChanged("AVR_HARD_VER");
            }
        }
        //到货批次号
        private string avr_ARRIVE_BATCH_NO;
        public string AVR_ARRIVE_BATCH_NO
        {
            get
            {
                return avr_ARRIVE_BATCH_NO;
            }
            set
            {
                avr_ARRIVE_BATCH_NO = value;
                OnPropertyChanged("AVR_ARRIVE_BATCH_NO");
            }
        }

        //方案唯一编号
        private string fk_LNG_SCHEME_ID;
        public string FK_LNG_SCHEME_ID
        {
            get
            {
                return fk_LNG_SCHEME_ID;
            }
            set
            {
                fk_LNG_SCHEME_ID = value;
                OnPropertyChanged("FK_LNG_SCHEME_ID");
            }
        }
        //协议唯一编号
        private string fk_PROTOCOL_ID;
        public string FK_PROTOCOL_ID
        {
            get
            {
                return fk_PROTOCOL_ID;
            }
            set
            {
                fk_PROTOCOL_ID = value;
                OnPropertyChanged("FK_PROTOCOL_ID");
            }
        }
        //通讯协议名称
        private string avr_PROTOCOL_NAME;
        public string AVR_PROTOCOL_NAME
        {
            get
            {
                return avr_PROTOCOL_NAME;
            }
            set
            {
                avr_PROTOCOL_NAME = value;
                OnPropertyChanged("AVR_PROTOCOL_NAME");
            }
        }
        //脉冲类型（共阴共阳
        private string avr_PULSE_TYPE;
        public string AVR_PULSE_TYPE
        {
            get
            {
                return avr_PULSE_TYPE;
            }
            set
            {
                avr_PULSE_TYPE = value;
                OnPropertyChanged("AVR_PULSE_TYPE");
            }
        }
        //费控类型,0:远程表，1：本地表
        private string chr_RATES_TYPE;
        public string CHR_RATES_TYPE
        {
            get
            {
                return chr_RATES_TYPE;
            }
            set
            {
                chr_RATES_TYPE = value;
                OnPropertyChanged("CHR_RATES_TYPE");
            }
        }
        //任务编号
        private string avr_TASK_NO;
        public string AVR_TASK_NO
        {
            get
            {
                return avr_TASK_NO;
            }
            set
            {
                avr_TASK_NO = value;
                OnPropertyChanged("AVR_TASK_NO");
            }
        }
        //工单号
        private string avr_WORK_NO;
        public string AVR_WORK_NO
        {
            get
            {
                return avr_WORK_NO;
            }
            set
            {
                avr_WORK_NO = value;
                OnPropertyChanged("AVR_WORK_NO");
            }
        }
        //载波协议名称
        private string avr_CARR_PROTC_NAME;
        public string AVR_CARR_PROTC_NAME
        {
            get
            {
                return avr_CARR_PROTC_NAME;
            }
            set
            {
                avr_CARR_PROTC_NAME = value;
                OnPropertyChanged("AVR_CARR_PROTC_NAME");
            }
        }
        //备用1
        private string avr_OTHER_1;
        public string AVR_OTHER_1
        {
            get
            {
                return avr_OTHER_1;
            }
            set
            {
                avr_OTHER_1 = value;
                OnPropertyChanged("AVR_OTHER_1");
            }
        }

        //备用2
        private string avr_OTHER_2;
        public string AVR_OTHER_2
        {
            get
            {
                return avr_OTHER_2;
            }
            set
            {
                avr_OTHER_2 = value;
                OnPropertyChanged("AVR_OTHER_2");
            }
        }
        //备用3
        private string avr_OTHER_3;
        public string AVR_OTHER_3
        {
            get
            {
                return avr_OTHER_3;
            }
            set
            {
                avr_OTHER_3 = value;
                OnPropertyChanged("AVR_OTHER_3");
            }
        }
        //备用4
        private string avr_OTHER_4;
        public string AVR_OTHER_4
        {
            get
            {
                return avr_OTHER_4;
            }
            set
            {
                avr_OTHER_4 = value;
                OnPropertyChanged("AVR_OTHER_4");
            }
        }
        //备用5
        private string avr_OTHER_5;
        public string AVR_OTHER_5
        {
            get
            {
                return avr_OTHER_5;
            }
            set
            {
                avr_OTHER_5 = value;
                OnPropertyChanged("AVR_OTHER_5");
            }
        }




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
