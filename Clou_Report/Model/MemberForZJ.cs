using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Clou_Report.Model
{
    public class MemberForZJ:INotifyPropertyChanged
    {
        private string strZCBH; 
        /// <summary>
        /// 资产编号
        /// </summary>
        public string StrZCBH
        {
            get
            {
                return strZCBH;
            }
            set
            {
                strZCBH = value;
                OnPropertyChanged("strZCBH");
            }
        }

        private string strEquipment;
        /// <summary>
        /// 设备类别
        /// </summary>
        public string StrEquipment
        {
            get
            {
                return strEquipment;
            }
            set
            {
                strEquipment = value;
                OnPropertyChanged("StrEquipment");
            }
        }

        private string strEquipmentType;
        /// <summary>
        /// 设备类型
        /// </summary>
        public string StrEquipmentType
        {
            get
            {
                return strEquipmentType;
            }
            set
            {
                strEquipmentType = value;
                OnPropertyChanged("StrEquipmentType");
            }
        }

        private string strEquipmentSize;
        /// <summary>
        /// 设备型号
        /// </summary>
        public string StrEquipmentSize
        {
            get
            {
                return strEquipmentSize;
            }
            set
            {
                strEquipmentSize = value;
                OnPropertyChanged("StrEquipmentSize");
            }
        }

        private string strFactory;
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string StrFactory
        {
            get
            {
                return strFactory;
            }
            set
            {
                strFactory = value;
                OnPropertyChanged("StrFactory");
            }
        }

        private string strElectric;
        /// <summary>
        /// 起码
        /// </summary>
        public string StrElectric
        {
            get
            {
                return strElectric;
            }
            set
            {
                strElectric = value;
                OnPropertyChanged("StrElectric");
            }
        }

        private string strCheckTime;
        /// <summary>
        /// 检定时间
        /// </summary>
        public string StrCheckTime
        {
            get
            {
                return strCheckTime;
            }
            set
            {
                strCheckTime = value;
                OnPropertyChanged("StrCheckTime");
            }
        }

        private string strResult;
        /// <summary>
        /// 检定结论
        /// </summary>
        public string StrResult
        {
            get
            {
                return strResult;
            }
            set
            {
                strResult = value;
                OnPropertyChanged("StrResult");
            }
        }
        private string strMeterID;
        /// <summary>
        /// 表唯一Id
        /// </summary>
        public string StrMeterID
        {
            get
            {
                return strMeterID;
            }
            set
            {
                strMeterID = value;
                OnPropertyChanged("StrMeterID");
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
