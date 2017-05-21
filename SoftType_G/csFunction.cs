using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DataCore;
using System.Data;
using System.Data.OleDb;
using OperateData;
namespace SoftType_G
{
    public class csFunction:Mis_Interface_Driver.MisDriver
    {
        public string Sql_word_1 = "Provider=Microsoft.ACE.OleDb.12.0;Data Source=";
        public string Sql_word_2 = ";Persist Security Info=False";
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        public  readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static readonly string AccessLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "AccessLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static string str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static string str_pkId = "";
        public static string str_DQBM =  OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_CompanyNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml"), strMinIb = "", strMaxIb = "", strXBDM = "", strJDTime = "";
        public  ObservableCollection<MeterBaseInfoFactor> GetBaseInfo(string CheckTime, string SQL)
        {
            ObservableCollection<MeterBaseInfoFactor> Temp_Base = new ObservableCollection<MeterBaseInfoFactor>();

            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                OleDbDataReader Myreader = null;
                Myreader = cmd.ExecuteReader();

                while (Myreader.Read())
                {
                    Temp_Base.Add(new MeterBaseInfoFactor()
                    {
                        PK_LNG_METER_ID = Myreader["PK_LNG_METER_ID"].ToString(),
                        LNG_BENCH_POINT_NO = Myreader["LNG_BENCH_POINT_NO"].ToString(),
                        AVR_ASSET_NO = Myreader["AVR_ASSET_NO"].ToString(),
                        AVR_UB = Myreader["AVR_UB"].ToString(),
                        AVR_IB = Myreader["AVR_IB"].ToString(),
                        AVR_TEST_PERSON = Myreader["AVR_TEST_PERSON"].ToString(),
                        AVR_TOTAL_CONCLUSION = Myreader["AVR_TOTAL_CONCLUSION"].ToString(),
                        CHR_UPLOAD_FLAG = Myreader["CHR_UPLOAD_FLAG"].ToString(),
                        AVR_SEAL_1 = Myreader["AVR_SEAL_1"].ToString(),
                        AVR_SEAL_2 = Myreader["AVR_SEAL_2"].ToString(),
                        AVR_SEAL_3 = Myreader["AVR_SEAL_3"].ToString(),
                    });
                }
                conn.Close();
                return Temp_Base;
            }
        }
        public static string MeterZCBH="";
        #region delete
        public override string DeleteMis(string PKid)
        {
            int excuteSuccess = 0;
            string ErrorResult;
            List<string> SealList = new List<string>();
            List<string> mysql = new List<string>();
            try
            {


                DeletedMidDataBaeInfo(PKid, out SealList);


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(SealList, out ErrorResult);

                if (excuteSuccess == 0)
                {

                    return PKid + "删除中间库成功！";
                }
                else
                {

                    return PKid + "删除中间库失败！" + ErrorResult;
                }


            }
            catch (Exception e)
            {

                return PKid + "删除中间库失败！" + e.ToString();
            }


        }
        public override string DeleteAllMis()
        {
            int excuteSuccess = 0;
            string ErrorResult;
            List<string> SealList = new List<string>();
            List<string> mysql = new List<string>();
            try
            {


                DeletedAllMidDataBaeInfo(out SealList);


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(SealList, out ErrorResult);

                if (excuteSuccess == 0)
                {

                    return "删除中间库工作单成功！";
                }
                else
                {

                    return "删除中间库工作单失败！" + ErrorResult;
                }


            }
            catch (Exception e)
            {

                return "删除中间库工作单失败！" + e.ToString();
            }


        }
        private void DeletedMidDataBaeInfo(string DeleteMeterZCBH, out List<string> DeleteSQL)
        {
            bool blnOK = true;
            List<string> lisSQL = new List<string>();
            string strSQL = "";
            strSQL = "delete from VT_SB_JKDNBJDJL  where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from vt_sb_jkzdjcjl  where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKFYBGJL  where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBJDWC   where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKXLWCJL   where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKSDTQWCJL  where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBSSJL  where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from vt_sb_jkrjswc   where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBZZJL   where  gzdbh='" + str_GZDBH + "' and zcbh='" + DeleteMeterZCBH + "' ";
            lisSQL.Add(strSQL);
            DeleteSQL = lisSQL;
        }
        private void DeletedAllMidDataBaeInfo(out List<string> DeleteSQL)
        {
            bool blnOK = true;
            List<string> lisSQL = new List<string>();
            string strSQL = "";
            strSQL = "delete from VT_SB_JKDNBJDJL  where  gzdbh='" + str_GZDBH + "'";
            lisSQL.Add(strSQL);
            strSQL = "delete from vt_sb_jkzdjcjl  where  gzdbh='" + str_GZDBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKFYBGJL  where  gzdbh='" + str_GZDBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBJDWC   where  gzdbh='" + str_GZDBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKXLWCJL   where  gzdbh='" + str_GZDBH + "' ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKSDTQWCJL  where  gzdbh='" + str_GZDBH + "'  ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBSSJL  where  gzdbh='" + str_GZDBH + "'  ";
            lisSQL.Add(strSQL);
            strSQL = "delete from vt_sb_jkrjswc   where  gzdbh='" + str_GZDBH + "'  ";
            lisSQL.Add(strSQL);
            strSQL = "delete from VT_SB_JKDNBZZJL   where  gzdbh='" + str_GZDBH + "' ";
            lisSQL.Add(strSQL);
            DeleteSQL = lisSQL;
        }
        #endregion 
        #region 上传数据
        public override string UpadataBaseInfo(string PKid, out List<string> Col_For_Seal)
        {
            int excuteSuccess = 0;
            string ErrorResult;
            List<string> SealList = new List<string>();
            List<string> mysql = new List<string>();
            str_pkId = PKid;
            try
            {


                mysql = Get_VT_SB_JKDNBJDJL(PKid, out SealList);
                foreach(string temp in mysql)
                {
                    //PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath)+@"\Log.txt");
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt",true);
                
                }
              
                
                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorResult);

                if (excuteSuccess == 0)
                {
                    Col_For_Seal = SealList;
                    return MeterZCBH + "基本信息上传到中间库成功！";
                }
                else
                {
                    Col_For_Seal = SealList;
                    return MeterZCBH + "基本信息上传到中间库失败！" + ErrorResult;
                }


            }
            catch (Exception e)
            {
                Col_For_Seal = SealList;
                return MeterZCBH + "基本信息上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataErrorInfo(string OnlyIdNum)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            try
            {

                mysql = Get_VT_SB_JKDNBJDWC(OnlyIdNum);
                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    if (mysql.Count == 0)
                    {
                        return "没有基本误差数据！";
                    }
                    else
                    {
                        return "基本误差数据上传到中间库成功！";
                    }

                }
                else
                {
                    return "基本误差上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                return "基本误差上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataJKRJSWCInfo(string OnlyIdNum)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            try
            {

                mysql = Get_VT_SB_JKRJSWC(OnlyIdNum);
                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    if (mysql.Count == 0)
                    {
                        return "没有日计时数据！";
                    }
                    else
                    {
                        return "日计时数据上传到中间库成功！";
                    }

                }
                else
                {
                    return "日计时数据上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                return "日计时数据上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataJKXLWCJLInfo(string OnlyIdNum, out List<string> Col_For_Demand)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            List<string> list_Demand = new List<string>();
            try
            {

                mysql = Get_VT_SB_JKXLWCJL(OnlyIdNum, out list_Demand);
                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    MakeUp_Result(ref list_Demand, true);
                    Col_For_Demand = list_Demand;
                    if (mysql.Count == 0)
                    {
                        return "没有电表需量数据数据！";
                    }
                    else
                    {
                        return "电表需量数据上传到中间库成功！";
                    }

                }
                else
                {
                    MakeUp_Result(ref list_Demand, false);
                    Col_For_Demand = list_Demand;
                    return "电表需量数据上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                Col_For_Demand = null;
                return "电表需量数据上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataSDTQWCJLInfo(string OnlyIdNum)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            try
            {

                mysql = Get_VT_SB_JKSDTQWCJL(OnlyIdNum);
                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    if (mysql.Count == 0)
                    {
                        return "没有时段投切数据数据！";
                    }
                    else
                    {
                        return "时段投切数据上传到中间库成功！";
                    }

                }
                else
                {
                    return "时段投切数据上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                return "时段投切数据上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataDNBSSJLInfo(string OnlyIdNum)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            try
            {

                mysql = Get_VT_SB_JKDNBSSJL(OnlyIdNum);
                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }


                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    if (mysql.Count == 0)
                    {
                        return "没有电表底度数据！";
                    }
                    else
                    {
                        return "电表底度上传到中间库成功！";
                    }

                }
                else
                {
                    return "电表底度上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                return "电表底度上传到中间库失败！" + e.ToString();
            }


        }

        public override string UpdataDNBZZJLInfo(string OnlyIdNum)
        {

            int excuteSuccess;
            string ErrorReason;
            List<string> mysql = new List<string>();
            try
            {

                mysql = VT_SB_JKDNBZZJL(OnlyIdNum);

                foreach (string temp in mysql)
                {
                    PublicFunction.WriteLog(temp, OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", BaseConfigPath) + @"\Log.txt");
                }

                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                if (excuteSuccess == 0)
                {
                    if (mysql.Count == 0)
                    {
                        return "没有电表走字数据数据！";
                    }
                    else
                    {
                        return "电表走字数据上传到中间库成功！";
                    }


                }
                else
                {
                    return "电表走字数据上传到中间库失败！" + ErrorReason;
                }




            }
            catch (Exception e)
            {
                return "电表走字数据上传到中间库失败！" + e.ToString();
            }


        }

        #endregion
       
        /// <summary>
        /// 电能表检定记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBJDJL(string str_PkID,out List<string> Col_Seal)
        {
           
            List<string> lis_Sql = new List<string>();
            List<string> lis_Seal = new List<string>();
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            string strSQL = "SELECT * FROM meterinfo where intMyId=" + str_PkID + "";
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            AccessConntion.Open();
            OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);
            OleDbDataReader OldRead = ccmd.ExecuteReader();
            OldRead.Read();
            #region 判断当前要上传的表是否为终端表

            #endregion
            string strOracleSQL = "insert into VT_SB_JKDNBJDJL (";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            try
            {
                MeterZCBH = OldRead["chrJlbh"].ToString().Trim();
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["chrCcbh"].ToString().Trim();
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["chrTxm"].ToString().Trim();

                //csPublicMember.strFZLXDM = OldRead["CHR_CT_CONNECTION_FLAG"].ToString().Trim();
                strJDTime = OldRead["datJdrq"].ToString().Trim();
                strXBDM = OldRead["intClfs"].ToString().Trim();

                string strValue = OldRead["chrIb"].ToString().Trim();
                int iIb = strValue.IndexOf("(");
                int Jib = strValue.IndexOf(")");
                string strIb = strValue.Substring(0, iIb);
                strMinIb = strIb;
                strMaxIb = strValue.Substring(iIb + 1, Jib - iIb - 1);
                #region ----------基本检定记录

                strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";
                strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                strOracleSQL_Name = strOracleSQL_Name + "SJBZ,";
                strOracleSQL_Value = strOracleSQL_Value + "','1";
                strOracleSQL_Name = strOracleSQL_Name + "BW,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["intBno"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "WD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["chrWd"].ToString().Trim();


                strOracleSQL_Name = strOracleSQL_Name + "SD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["chrSd"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "JDYJDM,"; //检定依据代码
                strOracleSQL_Value = strOracleSQL_Value + "','01";

                strOracleSQL_Name = strOracleSQL_Name + "JDJLDM,";  //总结论代码
                strValue = OldRead["chrJdjl"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "WGJCJLDM,";    //外观标志检查结论代码
                strValue = OldRead["chrZgjc"].ToString().Trim();
                if (strValue == "")
                    strValue = "合格";
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WGBZJCJLDM,";    //外观标志检查结论代码
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                #region ----//不检定项目

                //strValue = "W";
                //strOracleSQL_Name = strOracleSQL_Name + "YQJJCJLDM,";    //元器件检查结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "JYXNSYJLDM,";    //绝缘性能试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "MCDYSYJLDM,";    //脉冲电压试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "MCDYSYJLDM,";    //脉冲电压试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "JDQZDNSZWCSYJLDM,";    //计度器总电能示值误差试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DQYQSYJLDM,";    //电气要求试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //strOracleSQL_Name = strOracleSQL_Name + "GLXHSYJLDM,";    //功率消耗试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYDYYXSYJLDM,";    //电源电压影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYFWSYJLDM,";    //电压范围试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYZJHDSZDYXSYJLDM,";    //电压暂降或短时中断影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYDSZDDSZDYXSYJLDM,";    //电压短时中断对时钟的影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "GNJCJLDM,";    //功能检查结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;



                #endregion

                strOracleSQL_Name = strOracleSQL_Name + "JLDYSYJLDM,";  //交流电压试验结论代码
                strValue = "合格";
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "ZQDYQSYJLDM,";  //准确度要求试验结论代码---- 基本误差试验代替
                strValue = OldRead["chrJbwc"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "CSSYJLDM,";  //常数试验结论代码
                strValue = OldRead["chrZzsy"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDDL,";  //起动电流
                strValue = Get_METER_START_NO_LOAD("0002", "chrDL");
                if (strValue != "")
                    strValue = (double.Parse(strValue) * double.Parse(strIb)).ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "QDSYJLDM,";  //起动试验结论代码
                strValue = Get_METER_START_NO_LOAD("0002", "chrJL");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDSYDYZ,";  //潜动试验电压值
                strValue = Get_METER_START_NO_LOAD("0006", "chrProjectName");
                if (strValue != "") {
                    strValue = strValue.Substring(strValue.LastIndexOf("动") + 1, strValue.Length - strValue.LastIndexOf("动") - 1);
                    strValue = (Convert.ToDouble(strValue.Trim('%')) / 100 * double.Parse(OldRead["chrUb"].ToString().Trim())).ToString().Trim();
               
                }
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "FQDLZ,";  //防潜电流值
                strOracleSQL_Value = strOracleSQL_Value + "','0";
                strOracleSQL_Name = strOracleSQL_Name + "QISYJLDM,";  //潜动试验结论代码
                strValue = Get_METER_START_NO_LOAD("0006", "chrJL");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JBWCSYJLDM,";  //基本误差试验结论代码
                strValue = Get_METERINFO_RESULTS("chrJbwc");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "RJSWCSYJLDM,";  //日计时误差试验结论代码
                strValue = Get_METER_COMMUNICATION("002");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "RJSWCZ,";  //日计时误差值
                strValue = Get_METER_COMMUNICATION("102");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "JDQZDNSZWCSYJLDM,";  //计度器总电能示值误差试验结论代码
                strValue = ResultsCode(Get_METER_COMMUNICATION("031"));
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "FLSDDNSSWCSYJLDM,";  //费率时段电能示数误差试验结论代码
                strValue = ResultsCode(Get_METER_COMMUNICATION("032"));
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "XLZQWCSYJLDM,";  //需量周期误差试验结论代码
                strValue = Get_METER_COMMUNICATION("016");
                strValue = strValue + Get_METER_COMMUNICATION("017");
                strValue = strValue + Get_METER_COMMUNICATION("018");
                if (strValue.IndexOf("不", 0) >= 0)
                {
                    strValue = "不合格";
                }
                else
                {
                    strValue = "合格";
                }
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "ZDXLWCSYJLDM,";  //最大需量误差试验结论代码
                strValue = Get_METER_COMMUNICATION("016");
                strValue = strValue + Get_METER_COMMUNICATION("017");
                strValue = strValue + Get_METER_COMMUNICATION("018");
                if (strValue.IndexOf("不", 0) >= 0)
                {
                    strValue = "不合格";
                }
                else
                {
                    strValue = "合格";
                }
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);


                strOracleSQL_Name = strOracleSQL_Name + "YZXSYJLDM,";  //一致性试验结论代码
                strValue = Get_METERFK_DATA("一致性试验");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCBCSYJLDM,";  //误差变差试验结论代码
                strValue = Get_METERFK_DATA("误差变差试验");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCYZXSYJLDM,";  //误差一致性试验结论代码
                strValue = Get_METERFK_DATA("误差一致性试验");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "FZDLSYBCSYJLDM,";  //负载电流升降变差试验结论代码
                strValue = Get_METERFK_DATA("负载电流升降变差试验");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "TXGNJCJLDM,";  //通信功能检查结论代码
                strValue = Get_METER_COMMUNICATION("001");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "SDTQWCSYJLDM,";  //时段投切误差试验结论代码
                strValue = Get_METER_COMMUNICATION("003");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "GPSDSJLDM,";  //GPS对时结论代码
                strValue = Get_METER_COMMUNICATION("004");
                strValue = strValue + Get_METER_COMMUNICATION("015");
                if (strValue.IndexOf("不", 0) >= 0)
                {
                    strValue = "不合格";
                }
                else
                {
                    strValue = "合格";
                }
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JDRYBH,";  //检定员编号
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Jyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                

                strOracleSQL_Name = strOracleSQL_Name + "HYRYBH,";  //核验员编号
                strValue = OldRead["chrHyy"].ToString().Trim();
                string strSection = "MIS_Info/UserName/Item";
                string strXmlValue ="";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Hyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                

                strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                strOracleSQL_Name = strOracleSQL_Name + "BZZZZCBH,";  //电能计量标准设备资产编号
                strSection = "NewUser/CloumMIS/Item";
                strXmlValue = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_equipment", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strXmlValue;

                #region ----------日期型处理

                strOracleSQL_Name = strOracleSQL_Name + "JDRQ,";  //检定日期
                //strValue  =OldRead["datJdrq"].ToString().Trim();
                strValue = Convert.ToDateTime(OldRead["datJdrq"]).ToShortDateString().Trim() + " " + DateTime.Now.ToLongTimeString().Trim();

                strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                strOracleSQL_Name = strOracleSQL_Name + "HYRQ";  //核验日期
                strValue = OldRead["datJdrq"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + ",to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                #endregion


                strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                #endregion

                lis_Sql.Add(strOracleSQL);

                #region ----------铅封上传

                string strSealValuePath = "NewUser/CloumMIS/Item";
                string str_Seal01 = "", str_Seal02 = "", str_Seal03 = "";
                List<string> ColSeal = new List<string>();
                str_Seal01 = OperateData.FunctionXml.ReadElement(strSealValuePath, "Name", "cmb_Seal01", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal02 = OperateData.FunctionXml.ReadElement(strSealValuePath, "Name", "cmb_Seal02", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal03 = OperateData.FunctionXml.ReadElement(strSealValuePath, "Name", "cmb_Seal03", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal01 = SwitchSealNum(str_Seal01);
                str_Seal02 = SwitchSealNum(str_Seal02);
                str_Seal03 = SwitchSealNum(str_Seal03);

                ColSeal.Add(str_Seal01); ColSeal.Add(str_Seal02); ColSeal.Add(str_Seal03);
                for (int iCirc = 1; iCirc < 4; iCirc++)
                {
                    string strCode = "chrQianFeng" + iCirc.ToString().Trim();
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    if (OldRead[strCode].ToString().Trim() != "")
                    {

                        strOracleSQL = "insert into VT_SB_JKFYBGJL (";

                        strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                        strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                        strOracleSQL_Name = strOracleSQL_Name + "BGBZ,";
                        strOracleSQL_Value = strOracleSQL_Value + "','10";

                        strOracleSQL_Name = strOracleSQL_Name + "FYZCBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead[strCode].ToString().Trim();
                        lis_Seal.Add(OldRead[strCode].ToString().Trim());
                        strOracleSQL_Name = strOracleSQL_Name + "JFWZDM,";//加封位置代码-------
                        strValue = ColSeal[iCirc - 1];//此处修改为读取配置信息里面的加封位置
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                        strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                        strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                        strOracleSQL_Name = strOracleSQL_Name + "JFSJ";//时间
                        strValue = OldRead["datJdrq"].ToString().Trim();
                        strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                        strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                        lis_Sql.Add(strOracleSQL);
                    }
                   
                }

                #endregion


                AccessConntion.Close();
                OldRead.Close();

            }
            catch (Exception Error)
            {
                AccessConntion.Close();
                OldRead.Close();
            }
            Col_Seal = lis_Seal;
            return lis_Sql;

        }
        /// <summary>
        /// 电能表误差记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBJDWC(string str_PkID)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_SQL = new List<string>();
            string strSQL = "SELECT * FROM METERERROR WHERE  intMyId=" + str_PkID + "";
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBJDWC (";

            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            try
            {
                AccessConntion.Open();
                OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);


                OleDbDataReader red = ccmd.ExecuteReader();
                while (red.Read() == true)
                {
                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                    strOracleSQL_Name = strOracleSQL_Name + "GLFXDM,";   //功率方向代码
                    strValue = Get_GLFXDM(red["intWcLb"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    strOracleSQL_Name = strOracleSQL_Name + "GLYSDM,";   //功率因数代码
                    strValue = Get_GLYSDM(red["chrglys"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    strOracleSQL_Name = strOracleSQL_Name + "FZDLDM,";   //负载电流代码
                    strValue = Get_FZDLDM(red["dblxIb"].ToString().Trim());
                    //double test = Convert.ToDouble(red["dblxIb"].ToString().Trim());
                    string DLBS = red["chrProjectNo"].ToString().Trim().Substring(1, 3);
                    int test = Convert.ToInt16(red["chrProjectNo"].ToString().Trim().Substring(1, 3)) / 5;
                    if ((Convert.ToDouble(strMinIb) * 2 == Convert.ToDouble(strMaxIb)) && (test == 21 || test == 34 || test == 47 || test == 60 || test == 81))
                    {
                        strOracleSQL_Name = "";
                        strOracleSQL_Value = "";
                        strOracleSQL = "insert into VT_SB_JKDNBJDWC (";
                        continue;
                    }

                    if (Convert.ToDouble(red["dblxIb"].ToString().Trim()) > 1)
                    {
                        if (((int)(Convert.ToDouble(strMinIb) * Convert.ToDouble(red["dblxIb"].ToString().Trim()) + 0.5)).ToString() == strMaxIb)
                        {
                            strValue = Get_FZDLDM("IMAX");
                        }
                        else
                        {
                            strValue = Get_FZDLDM("0.5IMAX");

                        }
                    }
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "XBDM,";   //相别代码 三相、单相
                    strValue = Get_XBDM(strXBDM);
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    strOracleSQL_Name = strOracleSQL_Name + "FZLXDM,";   //负载类型代码 平衡 与 不平衡
                    strValue = Get_FZLXDM(red["intYj"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "FYDM,";   //分元代码
                    strValue = Get_FZLXDM(red["intYj"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ1,";   //WC1
                    strValue = red["chrWc0"].ToString().Trim();
                    if (strValue != "")
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','" + "+0.0000";
                    strOracleSQL_Name = strOracleSQL_Name + "WCZ2,";   //WC2
                    strValue = red["chrWc1"].ToString().Trim();
                    if (strValue != "")
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','" + "+0.0000";

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ3,";   //WC3
                    strValue = red["chrWc2"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ4,";   //WC4
                    strValue = red["chrWc3"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ5,";   //WC5
                    strValue = red["chrWc4"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCPJZ,";  //误差平均值
                    strValue = red["chrWc"].ToString().Trim();
                    if (strValue != "")
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','" + "+0.0000";

                    strOracleSQL_Name = strOracleSQL_Name + "WCXYZ,";  //误差修约值
                    strValue = red["chrWcHz"].ToString().Trim();
                    if (strValue != "")
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','" + "+0.0";


                    strOracleSQL_Name = strOracleSQL_Name + "JLDM,";   //结论代码
                    strValue = ResultsCode(red["chrWcJl"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;


                    strOracleSQL_Name = strOracleSQL_Name + "WCCZ,";   //不平衡负载与平衡负载的误差差值
                    strValue = red["chrWc11"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCCZXYZ,";   //误差差值修约值
                    strValue = red["chrWc12"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "DQBM";  //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM+ "'";


                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";
                    list_SQL.Add(strOracleSQL);
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    strOracleSQL = "insert into VT_SB_JKDNBJDWC (";

                }
                red.Close();
                AccessConntion.Close();
                AccessConntion.Dispose();

                //
            }
            catch (Exception error) { }
            finally
            {
                AccessConntion.Close();
            }


            return list_SQL;
        }

        /// <summary>
        /// 日计量误差记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKRJSWC(string PKId)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_SQL = new List<string>();
            
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKRJSWC (";

            try
            {
                strValue = Get_METER_COMMUNICATION("202");
                if (strValue.IndexOf(",") > 0)  //有误差值
                {
                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" +str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;


                    char[] csplit = { ',' };
                    string[] strParm = null;
                    strParm = strValue.Split(csplit);
                    strOracleSQL_Name = strOracleSQL_Name + "CSZ1,";   //误差1
                    if (strParm.Length > 1)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ2,";   //误差2
                    if (strParm.Length > 2)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ3,";   //误差3
                    if (strParm.Length > 3)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";
                    strOracleSQL_Name = strOracleSQL_Name + "CSZ4,";   //误差4
                    if (strParm.Length > 4)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[3];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ5,";   //误差5
                    if (strParm.Length > 5)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[4];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";


                    //平均值与 化整值
                    if (Get_METER_COMMUNICATION("102") != "")  //有误差值
                    {

                        strOracleSQL_Name = strOracleSQL_Name + "PJZ,";   //平均值

                        strOracleSQL_Value = strOracleSQL_Value + "','" + Get_METER_COMMUNICATION("102").ToString().Trim();
                    }
                    else
                    {
                        strOracleSQL_Name = strOracleSQL_Name + "PJZ,";   //平均值

                        strOracleSQL_Value = strOracleSQL_Value + "','" + "+0.000";
                    }
                    strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM + "'";


                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";
                    list_SQL.Add(strOracleSQL);
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    strOracleSQL = "insert into VT_SB_JKRJSWC (";
                }

            }
            catch { }

            return list_SQL;
        }

        /// <summary>
        /// 电能表需量记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKXLWCJL(string pk_ID,out List<string> DemandList)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_sql = new List<string>();
            List<string> lis_Demand= new List<string>();
           
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKXLWCJL (";

            try
            {
                for (int iCirc = 16; iCirc < 19; iCirc++)
                {
                    strValue = "1" + iCirc.ToString();    //01701/01801/01901
                    strValue = Get_METER_COMMUNICATION(strValue);
                    if (strValue.IndexOf(",") > 0)  //有误差值
                    {
                        char[] csplit = { ',' };
                        string[] strParm = null;
                        strParm = strValue.Split(csplit);

                        strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                        strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                        strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                        strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                        if (iCirc == 16)
                        {
                            strValue = "02";   //0.1IB
                            lis_Demand.Add("上传最大需量0.1IB");
                        }
                        if (iCirc == 17)
                        {
                            strValue = "05";   //IB
                            lis_Demand.Add("上传最大需量1IB");

                        }
                        if (iCirc == 18)
                        {
                            strValue = "06";   //Imax
                            lis_Demand.Add("上传最大需量Imax");

                        } 
                        strOracleSQL_Name = strOracleSQL_Name + "FZDLDM,";   //负载电流代码
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;


                        strOracleSQL_Name = strOracleSQL_Name + "BZZDXL,";   //标准最大需量
                        if (strParm.Length > 1)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "SJXL,";   //实际需量
                        if (strParm.Length > 2)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "WCZ,";   //误差值
                        if (strParm.Length >= 3)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";


                        strValue = ResultsCode(Get_METER_COMMUNICATION("0" + iCirc.ToString().Trim()));//结论代码
                        strOracleSQL_Name = strOracleSQL_Name + "JLDM,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                        strOracleSQL_Value = strOracleSQL_Value + str_DQBM + "'";


                        strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";
                        list_sql.Add(strOracleSQL);
                        strOracleSQL_Name = "";
                        strOracleSQL_Value = "";
                        strOracleSQL = "insert into VT_SB_JKXLWCJL (";

                    }
                }
            }
            catch { }
            DemandList = lis_Demand;
            return list_sql;
        }

        /// <summary>
        /// 电能表投切记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKSDTQWCJL(string PK_ID)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_sql = new List<string>();
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKSDTQWCJL (";

            try
            {
                for (int iCirc = 1; iCirc < 9; iCirc++)
                {
                    strValue = iCirc.ToString() + "03";    //01701/01801/01901
                    strValue = Get_METER_COMMUNICATION(strValue);
                    if (strValue.IndexOf(",") > 0)  //有误差值
                    {
                        char[] csplit = { ',' };
                        string[] strParm = null;
                        strParm = strValue.Split(csplit);
                        if (strParm.Length >= 4)
                        {
                            strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                            strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                            strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                            strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                            strOracleSQL_Name = strOracleSQL_Name + "SD,";   //时段
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[3] + "时段";


                            strOracleSQL_Name = strOracleSQL_Name + "BZTQSJ,";   //标准投切时间
                            if (strParm.Length > 1)
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";

                            strOracleSQL_Name = strOracleSQL_Name + "SJTQSJ,";   //实际投切时间
                            if (strParm.Length > 2)
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";

                            strOracleSQL_Name = strOracleSQL_Name + "TQWC,";   //投切误差
                            if (strParm.Length >= 3)
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";


                            strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                            strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM+ "'";


                            strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                            list_sql.Add(strOracleSQL);
                            strOracleSQL_Name = "";
                            strOracleSQL_Value = "";
                            strOracleSQL = "insert into VT_SB_JKSDTQWCJL (";
                        }
                    }
                }
            }
            catch { }




            return list_sql;
        }

        /// <summary>
        /// 电能表示数记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBSSJL(string PK_IDL)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_sql = new List<string>();
            string strValue = "";
            string strTypeCode = "";
            string strSQL = "select * from meterdgn  where  	FK_LNG_METER_ID='" + PK_IDL + "'";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBSSJL (";

            try
            {
                for (int iCirc = 1; iCirc < 5; iCirc++)
                {

                    strTypeCode = iCirc.ToString().Trim() + "19";    //119正向有功 /219反向有功 /319正向无功/419反向无功

                    strValue = Get_METER_COMMUNICATION(strTypeCode);
                    if (strValue.IndexOf(",") > 0)  //有误差值
                    {
                        char[] csplit = { ',' };
                        string[] strParm = null;
                        strParm = strValue.Split(csplit);
                        if (strParm.Length >= 4)
                        {
                            for (int iCircLx = 0; iCircLx < 4; iCircLx++) //总，峰，平，谷，尖
                            {
                                strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                                strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                                strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                                strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                                strOracleSQL_Name = strOracleSQL_Name + "SSLXDM,";   //示数类型代码
                                strValue = iCirc.ToString().Trim() + iCircLx.ToString().Trim();
                                strValue = Get_SSLXDM(strValue);
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                                strOracleSQL_Name = strOracleSQL_Name + "DQBM,";   //地区编码
                                strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                                strOracleSQL_Name = strOracleSQL_Name + "BSS,";  //
                                strValue = strParm[iCircLx];
                                if (strValue == "")
                                {
                                    strOracleSQL_Name = "";
                                    strOracleSQL_Value = "";
                                    strOracleSQL = "insert into VT_SB_JKDNBSSJL (";
                                    continue;
                                }
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                                strOracleSQL_Name = strOracleSQL_Name + "CBSJ";  //
                                strValue = strJDTime;
                                strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                                strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                                list_sql.Add(strOracleSQL);
                                strOracleSQL_Name = "";
                                strOracleSQL_Value = "";
                                strOracleSQL = "insert into VT_SB_JKDNBSSJL (";


                            }
                        }
                    }
                }
            }
            catch { }
            return list_sql;
        }

        /// <summary>
        /// 电能表走字记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> VT_SB_JKDNBZZJL(string PK_IDL)
        {
            str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            List<string> list_sql = new List<string>();
            string strValue = "";
            string strTypeCode = "";
            string strSQL = "select * from MeterZzData  where  	intMyId=" + PK_IDL + "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBZZJL (";
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            try
            {
                AccessConntion.Open();
                OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);
                OleDbDataReader red = ccmd.ExecuteReader();
                while (red.Read() == true)
                {


                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                    strOracleSQL_Name = strOracleSQL_Name + "SSLXDM,";   //示数类型代码
                    strValue = red["chrJdfx"].ToString().Trim();
                    strTypeCode = strValue;    //01701正向有功 /01702反向有功 /01703正向无功/01704反向无功
                    strTypeCode = strTypeCode + red["chrFl"].ToString().Trim(); // 总、峰平谷尖
                    strValue = Get_SSLXDM(strTypeCode);
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "DQBM,";   //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                    strOracleSQL_Name = strOracleSQL_Name + "BZQQSS,";  // 
                    strValue = "0";
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "BZQZSS,";  //
                    strValue = red["chrZiMa"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "QSS,";  //
                    strValue = red["chrQiMa"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "ZSS,";  //
                    strValue = red["chrZiMa"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "ZZWC";  //
                    strValue = red["chrWc"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue + "'";


                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                    list_sql.Add(strOracleSQL);
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    strOracleSQL = "insert into VT_SB_JKDNBZZJL (";

                }
            }
            catch { }
            return list_sql;
        }


        /// <summary>
        /// 结论与代号转换
        /// </summary>
        /// <param name="strResult"></param>
        /// <returns></returns>
        private static string ResultsCode(string strResult)
        {
            string strResultCode = "W";
            if (strResult.IndexOf("合格", 0) >= 0)
            {
                strResultCode = "Y";

                if (strResult.IndexOf("不", 0) >= 0)
                {
                    strResultCode = "N";
                }
            }


            else
                strResultCode = "W";
            return strResultCode;
        }

        /// <summary>
        /// 转换铅封位置代码
        /// </summary>
        /// <param name="str_Seal"></param>
        /// <returns></returns>
        private static string SwitchSealNum(string str_Seal)
        {
            string str_SealNum="";
            switch (str_Seal.Trim())
            { 
                case "左耳封":
                    str_SealNum = "01";
                    break;
                case "右耳封":
                    str_SealNum = "02";
                    break;
                case "编程小门":
                    str_SealNum = "07";
                    break;
                case "表盖封印":
                    str_SealNum = "05";
                    break;
            }
            return str_SealNum;
        }
        /// <summary>
        /// 启动潜动数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID">起动0002 潜动0006</param>
        /// <returns></returns>
        private static string Get_METER_START_NO_LOAD(string RESULT_ID, string strName)
        {
            string strResults = "";
            string strSQL = " SELECT " + strName + " FROM meterqdqid where chrProjectNo='" + RESULT_ID + "' and  intMyId=" + str_pkId + "";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);

            return strResults;
        }

        /// <summary>
        /// 3000G基本信息表结论读取
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METERINFO_RESULTS(string RESULT_COL)
        {
            string strResults = "";
            string strSQL = " SELECT " + RESULT_COL + " FROM MeterInfo where intMyId=" + str_pkId + "";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);



            return strResults;
        }

        /// <summary>
        /// 3000G被检表多功能信息
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_COMMUNICATION(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT chrvalue FROM METERDGN where chrProjectNo='" + RESULT_ID + "' and intMyId=" + str_pkId + "";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);
            

            return strResults;
        }

        /// <summary>
        /// 获取3000G费控数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID">直接写项目名字</param>
        /// <returns></returns>
        private static string Get_METERFK_DATA(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT chrJL FROM MeterFK where chrProjectName='" + RESULT_ID + "' and intMyId=" + str_pkId + "";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }
        /// <summary>
        /// 功率方向代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_GLFXDM(string strValue)
        {
            string strResults = "0";
            switch (strValue)
            {
                case "0":
                    strResults = "1";
                    break;
                case "1":
                    strResults = "3";
                    break;
                case "2":
                    strResults = "2";
                    break;
                case "3":
                    strResults = "4";
                    break;
            }

            return strResults;
        }

        /// <summary>
        /// 功率因数代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_GLYSDM(string strValue)
        {
            string strResults = "1";
            switch (strValue)
            {
                case "0.5L":
                case ".5L":
                    strResults = "1";
                    break;
                case "1.0":
                case "1":
                    strResults = "2";
                    break;
                case "0.8C":
                case ".8C":
                    strResults = "3";
                    break;
                case "0.5C":
                case ".5C":
                    strResults = "4";
                    break;
                case "0.25L":
                case ".25L":
                    strResults = "5";
                    break;
                case "0.25C":
                case ".25C":
                    strResults = "6";
                    break;
            }

            return strResults;
        }
        /// <summary>
        /// 负载电流
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_FZDLDM(string strValue)
        {
            string strResults = "";
            switch (strValue.ToUpper())
            {
                case "0.05IB":
                case ".05IB":
                case ".05":
                case "0.05":
                    strResults = "01";
                    break;
                case "0.1IB":
                case ".1IB":
                case "0.1":
                case ".1":
                    strResults = "02";
                    break;
                case "0.2IB":
                case ".2IB":
                case ".2":
                case "0.2":
                    strResults = "03";
                    break;
                case "0.5IB":
                case ".5IB":
                case ".5":
                case "0.5":
                    strResults = "04";
                    break;
                case "1.0IB":
                case "1IB":
                case "1":
                    strResults = "05";
                    break;
                case "IMAX":
                case "10":
                    strResults = "06";
                    break;
                case "0.5IMAX":
                    strResults = "07";
                    break;
                case "4IB":
                case "4":
                    strResults = "08";
                    break;
                case "3IB":
                case "3":
                    strResults = "09";
                    break;
                case "2IB":
                case "2":
                    strResults = "10";
                    break;
                case "0.02IB":
                case ".02IB":
                case ".02":
                case "0.02":
                    strResults = "11";
                    break;
                case "0.01IB":
                case ".01IB":
                case "0.01":
                case ".01":
                    strResults = "12";
                    break;
                case "0.03IB":
                case ".03IB":
                case "0.03":
                case ".03":
                    strResults = "15";
                    break;
            }

            return strResults;
        }
        /// <summary>
        /// 相别代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_XBDM(string strValue)
        {
            string strResults = "01";
            switch (strValue)
            {
                case "5":
                    strResults = "01";
                    break;
                case "0":
                    strResults = "02";
                    break;
                case "1":
                    strResults = "02";
                    break;
            }

            return strResults;
        }

        /// <summary>
        /// 负载类型代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_FZLXDM(string strValue)
        {
            string strResults = "1";
            switch (strValue)
            {
                case "0":
                    strResults = "1";
                    break;
                case "1":
                    strResults = "2";
                    break;
                case "2":
                    strResults = "3";
                    break;
                case "3":
                    strResults = "4";
                    break;
            }

            return strResults;
        }

        /// <summary>
        /// 示数类型代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_SSLXDM(string strValue)
        {
            string strResults = "121";
            switch (strValue.Trim())
            {
                case "017010"://正向有功 总
                case "1总"://正向有功 总
                case "10"://正向有功 总
                case "正向有功总":
                    strResults = "121";
                    break;
                case "017011"://正向有功 峰
                case "1峰":
                case "11":
                case "正向有功峰":
                    strResults = "123";
                    break;
                case "017012"://正向有功 平
                case "1平":
                case "12":
                case "正向有功平":
                    strResults = "124";
                    break;
                case "017013"://正向有功 谷
                case "1谷":
                case "13":
                case "正向有功谷":
                    strResults = "125";
                    break;
                case "017014"://正向有功 尖
                case "1尖":
                case "14":
                case "正向有功尖":
                    strResults = "122";
                    break;
                case "017020"://反向有功 总
                case "2总":
                case "20":
                case "反向有功总":
                    strResults = "221";
                    break;
                case "017021"://         峰
                case "2峰":
                case "22":
                case "反向有功峰":
                    strResults = "223";
                    break;
                case "017022"://         平
                case "2平":
                case "23":
                case "反向有功平":
                    strResults = "224";
                    break;
                case "017023"://         谷
                case "2谷":
                case "反向有功谷":
                    strResults = "225";
                    break;
                case "017024"://         尖
                case "2尖":
                case "21":
                case "反向有功尖":
                    strResults = "222";
                    break;
                case "017030"://正向无功 总
                case "3总":
                case "30":
                case "正向无功总":
                    strResults = "131";
                    break;
                case "017031"://         峰
                case "3峰":
                case "31":
                case "正向无功峰":
                    strResults = "133";
                    break;
                case "017032"://         平
                case "3平":
                case "32":
                case "正向无功平":
                    strResults = "135";
                    break;
                case "017033"://         谷
                case "3谷":
                case "33":
                case "正向无功谷":
                    strResults = "134";
                    break;
                case "017034"://         尖
                case "3尖":
                case "34":
                case "正向无功尖":
                    strResults = "132";
                    break;
                case "017040"://反向无功 总
                case "4总":
                case "40":
                case "反向无功总":
                    strResults = "231";
                    break;
                case "017041"://         峰
                case "4峰":
                case "41":
                case "反向无功峰":
                    strResults = "236";
                    break;
                case "017042"://         平
                case "4平":
                case "42":
                case "反向无功平":
                    strResults = "238";
                    break;
                case "017043"://         谷
                case "4谷":
                case "43":
                case "反向无功谷":
                    strResults = "237";
                    break;
                case "017044"://         尖
                case "4尖":
                case "反向无功尖":
                    strResults = "232";
                    break;
            }

            return strResults;
        }

        private static void MakeUp_Result(ref List<string> Col_Result, bool IsSuccess)
        {
            if (IsSuccess)
            {
                for (int i = 0; i < Col_Result.Count; i++)
                {
                    Col_Result[i] = Col_Result[i] + "成功";
                }
            }
            else
            {
                for (int i = 0; i < Col_Result.Count; i++)
                {
                    Col_Result[i] = Col_Result[i] + "失败";
                }
            }
        }
    
    }   
}
