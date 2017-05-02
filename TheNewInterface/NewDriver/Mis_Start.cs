using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TheNewInterface.NewDriver
{
    public class Mis_Start
    {
        //public static Mis_Interface_Driver.MisDriver misDriver = null;
        //public static void SetUseInterface()
        //{
        //    string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        //    string strSection = "NewUser/CloumMIS/Item";
        //    csPublicMember.strSoftType = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
        //    if (csPublicMember.strSoftType == "CL3000G")
        //    {
        //        misDriver = new SoftType_G.Interface();
        //    }

        //}

        public ObservableCollection<DataCore.MeterBaseInfoFactor> GetCheckTimeDate(string CheckTime)
        {

            return null;
        }
    }
}
