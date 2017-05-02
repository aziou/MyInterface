using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Driver_Interface;
namespace Mis_Interface_Driver
{
    public abstract class MisDriver
    {
        public enum SoftType_Code : uint
        {
            CL3000S = 1,
            CL3000G = 2,
            CL3000F = 3,
            CL3000DV80 = 4,

        }

        public enum Function_Code : uint
        {
            GetSelectTimeData = 1,
            UpDataToOracle = 2,
            DeleteDataFromOracle = 3,
            OutPutMisInfoToReport = 4,

        }

        public abstract string UpadataBaseInfo(string PK_Id, out List<string> SealCol);
        public abstract string UpdataErrorInfo(string PK_Id);
        public abstract string UpdataJKRJSWCInfo(string PK_Id);
        public abstract string UpdataJKXLWCJLInfo(string PK_Id, out List<string> SealCol);
        public abstract string UpdataDNBSSJLInfo(string PK_Id);
        public abstract string UpdataDNBZZJLInfo(string PK_Id);
        public abstract string UpdataSDTQWCJLInfo(string PK_Id);
        public abstract string DeleteMis(string PK_Id);
        public abstract string DeleteAllMis();


       

    }
}
