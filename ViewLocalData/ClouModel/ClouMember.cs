using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
namespace ViewLocalData.ClouModel
{
    public  class ClouMember:INotifyPropertyChanged
    {
        private volatile static ClouMember _instance = null;
        private static readonly object lockHelper = new object();
        public static ClouMember CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ClouMember();
                }
            }
            return _instance;
        }
        private DataTable  dataBase;
        public DataTable DataBase
        {
            get
            {
                return dataBase;
            }
            set
            {
                dataBase = value;
                OnPropertyChanged("DataBase");
            }
        }
        private DataTable resultDataBase;
        public DataTable ResultDataBase
        {
            get
            {
                return resultDataBase;
            }
            set
            {
                resultDataBase = value;
                OnPropertyChanged("ResultDataBase");
            }
        }
        private DataTable multiDataBase;
        public DataTable MultiDataBase
        {
            get
            {
                return multiDataBase;
            }
            set
            {
                multiDataBase = value;
                OnPropertyChanged("MultiDataBase");
            }
        }
        private DataTable errorDataBase;
        public DataTable ErrorDataBase
        {
            get
            {
                return errorDataBase;
            }
            set
            {
                errorDataBase = value;
                OnPropertyChanged("ErrorDataBase");
            }
        }
        private DataTable qiDongDataBase;
        public DataTable QiDongDataBase
        {
            get
            {
                return qiDongDataBase;
            }
            set
            {
                qiDongDataBase = value;
                OnPropertyChanged("QiDongDataBase");
            }
        }
        private DataTable runDataBase;
        public DataTable RunDataBase
        {
            get
            {
                return runDataBase;
            }
            set
            {
                runDataBase = value;
                OnPropertyChanged("RunDataBase");
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
