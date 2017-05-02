using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DataCore;
namespace TheNewInterface.ViewModel
{
    public  class AllMeterInfo:INotifyPropertyChanged
    {

        private volatile static AllMeterInfo _instance = null;
        private static readonly object lockHelper = new object();
        public static AllMeterInfo CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new AllMeterInfo();
                }
            }
            return _instance;
        }
        private ObservableCollection<MeterBaseInfoFactor> meterBaseInfo;
        public ObservableCollection<MeterBaseInfoFactor> MeterBaseInfo
        {
            get
            {
                return meterBaseInfo;
            }
            set
            {
                meterBaseInfo = value;
                OnPropertyChanged("MeterBaseInfo");
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
