using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace TheNewInterface.ViewModel
{
    public class ViewMember : INotifyPropertyChanged
    {
        private volatile static ViewMember _instance = null;
        private static readonly object lockHelper = new object();
        public static ViewMember CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ViewMember();
                }
            }
            return _instance;
        }
        private string oracleUserName;
        public string OracleUserName
        {
            get;
            set;
        }
        private string oraclePassword;
        public string OraclePassword
        {
            get;
            set;
        }
        private ObservableCollection<MeterInfoItem> meterInfoList;
        public ObservableCollection<MeterInfoItem> MeterInfoList
        {
            get
            {
                return meterInfoList;
            }
            set
            {
                meterInfoList = value;
                OnPropertyChanged("MeterInfoList");
            }
        }
        private ObservableCollection<MeterInfoItem> basicmeterInfoList;
        public ObservableCollection<MeterInfoItem> BasicMeterInfoList
        {
            get
            {
                return basicmeterInfoList;
            }
            set
            {
                basicmeterInfoList = value;
                OnPropertyChanged("BasicMeterInfoList");
            }
        }
        private ObservableCollection<MeterInfoItem> wcItemList;
        public ObservableCollection<MeterInfoItem> WcItemList
        {
            get
            {
                return wcItemList;
            }
            set
            {
                wcItemList = value;
                OnPropertyChanged("WcItemList");
            }
        }
        private ObservableCollection<MeterInfoItem> dayJSWCList;
        public ObservableCollection<MeterInfoItem> DayJSWCList
        {
            get
            {
                return dayJSWCList;
            }
            set
            {
                dayJSWCList = value;
                OnPropertyChanged("DayJSWCList");
            }
        }
        private ObservableCollection<MeterInfoItem> needRecordList;
        public ObservableCollection<MeterInfoItem> NeedRecordList
        {
            get
            {
                return needRecordList;
            }
            set
            {
                needRecordList = value;
                OnPropertyChanged("NeedRecordList");
            }
        }
        private ObservableCollection<MeterInfoItem> timeTQList;
        public ObservableCollection<MeterInfoItem> TimeTQList
        {
            get
            {
                return timeTQList;
            }
            set
            {
                timeTQList = value;
                OnPropertyChanged("TimeTQList");
            }
        }
        private ObservableCollection<MeterInfoItem> disPlayList;
        public ObservableCollection<MeterInfoItem> DiaPlayList
        {
            get
            {
                return disPlayList;
            }
            set
            {
                disPlayList = value;
                OnPropertyChanged("DiaPlayList");
            }
        }
        private ObservableCollection<MeterInfoItem> runWordList;
        public ObservableCollection<MeterInfoItem> RunWordList
        {
            get
            {
                return runWordList;
            }
            set
            {
                runWordList = value;
                OnPropertyChanged("RunWordList");
            }
        }
        private ObservableCollection<MeterInfoItem> lockInList;
        public ObservableCollection<MeterInfoItem> LockInList
        {
            get
            {
                return lockInList;
            }
            set
            {
                lockInList = value;
                OnPropertyChanged("LockInList");
            }
        }
        private string checkTime;
        public string CheckTime
        {
            get
            {
                return checkTime;
            }
            set
            {
                checkTime = value;
                OnPropertyChanged("CheckTime");
            }
        }
        private string operater;
        public string Operater
        {
            get
            {
                return operater;
            }
            set
            {
                operater = value;
                OnPropertyChanged("Operater");
            }
        }
        private string isRightWord;
        public string IsRightWord
        {
            get
            {
                return isRightWord;
            }
            set
            {
                isRightWord = value;
                OnPropertyChanged("IsRightWord");
            }
        }
        private string txt_KeyWord;
        public string Txt_KeyWord
        {
            get
            {
                return txt_KeyWord;
            }
            set
            {
                txt_KeyWord = value;
                OnPropertyChanged("Txt_KeyWord");
            }
        }
        private int ifRunTheItemChanged;
        public int IfRunTheItemChanged
        {
            get
            {
                return ifRunTheItemChanged;
            }
            set
            {
                ifRunTheItemChanged = value;
                OnPropertyChanged("IfRunTheItemChanged");
            }
        }
        private string meterOnlyId;
        public string MeterOnlyId
        {
            get
            {
                return meterOnlyId;
            }
            set
            {
                meterOnlyId = value;
                OnPropertyChanged("MeterOnlyId");
            }
        }
        private string thisMeterWorkNum;
        public string ThisMeterWorkNum
        {
            get
            {
                return thisMeterWorkNum;
            }
            set
            {
                thisMeterWorkNum = value;
                OnPropertyChanged("ThisMeterWorkNum");
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
