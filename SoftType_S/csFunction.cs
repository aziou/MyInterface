using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using DataCore;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace SoftType_S
{
    public class csFunction : Mis_Interface_Driver.MisDriver
    {
        public string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
        public string Sql_word_2 = ";Persist Security Info=False";
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        public readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static readonly string AccessLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "AccessLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static string str_GZDBH = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml"), strXBDM = "", strJDTime = "";
        public static string str_pkId = "";
        public static string str_DQBM = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_CompanyNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public static string MeterZCBH = "";
        public ObservableCollection<MeterBaseInfoFactor> GetBaseInfo(string CheckTime, string SQL)
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
            str_pkId = PKid;
            string Runtime = "";
            List<string> SealList = new List<string>();
            List<string> mysql = new List<string>();
            try
            {

                Stopwatch watch = new Stopwatch();
                watch.Start();
                mysql = Get_VT_SB_JKDNBJDJL(PKid, out SealList);
                watch.Stop();
                Runtime = watch.ElapsedMilliseconds.ToString() + "ms";

                watch = new Stopwatch();
                watch.Start();
                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorResult);
                watch.Stop();
                Runtime =Runtime+"执行："+ watch.ElapsedMilliseconds.ToString() + "ms";

                if (excuteSuccess == 0)
                {
                    Col_For_Seal = SealList;
                    return MeterZCBH + "基本信息上传到中间库成功！" ;
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
            string Runtime = "";
            List<string> mysql = new List<string>();
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                mysql = Get_VT_SB_JKDNBJDWC(OnlyIdNum);
                watch.Stop();
                Runtime = watch.ElapsedMilliseconds.ToString() + "ms";

                watch = new Stopwatch();
                watch.Start();
                excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(mysql, out ErrorReason);
                watch.Stop();
                Runtime = Runtime + "执行：" + watch.ElapsedMilliseconds.ToString() + "ms";


              



               
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
        private static List<string> Get_VT_SB_JKDNBJDJL(string PK_ID,out List<string> Col_For_Seal)
        {
            List<string> lis_Sql = new List<string>();
            List<string> lis_seal = new List<string>();
            string strFZLXDM = "", Runtime="";
            string str_CheckTime = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "CheckTimeFlag", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            string str_CheckName = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "cmb_Jyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
            string strSQL = "SELECT * FROM METER_INFO where PK_LNG_METER_ID='" + PK_ID + "' and DTM_TEST_DATE=#" + str_CheckTime + "# and AVR_TEST_PERSON = '" + str_CheckName + "'";
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            AccessConntion.Open();
            OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);


            Stopwatch watch = new Stopwatch();
            watch.Start();
            OleDbDataReader OldRead = ccmd.ExecuteReader();
            watch.Stop();
            Runtime = watch.ElapsedMilliseconds.ToString() + "ms";

            watch = new Stopwatch();
            watch.Start();

            OldRead.Read();
            string strOracleSQL = "insert into VT_SB_JKDNBJDJL (";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string UpdateResult = "";
            try
            {
                MeterZCBH = OldRead["AVR_ASSET_NO"].ToString().Trim();
                UpdateResult = string.Format("电表：{0}", OldRead["AVR_ASSET_NO"].ToString().Trim());
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["AVR_MADE_NO"].ToString().Trim();
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["AVR_BAR_CODE"].ToString().Trim();

                strFZLXDM = OldRead["CHR_CT_CONNECTION_FLAG"].ToString().Trim();
                strJDTime = OldRead["DTM_TEST_DATE"].ToString().Trim();
                strXBDM = OldRead["AVR_WIRING_MODE"].ToString().Trim();

                string strValue = OldRead["AVR_IB"].ToString().Trim();
                int iIb = strValue.IndexOf("(");
                string strIb = strValue.Substring(0, iIb);

                #region ----------基本检定记录

                strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";
                strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                strOracleSQL_Name = strOracleSQL_Name + "SJBZ,";
                strOracleSQL_Value = strOracleSQL_Value + "','1";
                strOracleSQL_Name = strOracleSQL_Name + "BW,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["LNG_BENCH_POINT_NO"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "WD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["AVR_TEMPERATURE"].ToString().Trim();

                strOracleSQL_Name = strOracleSQL_Name + "SD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["AVR_HUMIDITY"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "JDYJDM,"; //检定依据代码
                strOracleSQL_Value = strOracleSQL_Value + "','01";

                strOracleSQL_Name = strOracleSQL_Name + "JDJLDM,";  //总结论代码
                strValue = OldRead["AVR_TOTAL_CONCLUSION"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "WGJCJLDM,";    //外观标志检查结论代码
                strValue = Get_METER_RESULTS("100");
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
                strValue = Get_METER_RESULTS("102");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "ZQDYQSYJLDM,";  //准确度要求试验结论代码---- 基本误差试验代替
                strValue = Get_METER_RESULTS("103");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "CSSYJLDM,";  //常数试验结论代码
                strValue = Get_METER_RESULTS("106");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDDL,";  //起动电流
                strValue = Get_METER_START_NO_LOAD("1091", "AVR_CURRENT") + "A";
                //if (strValue != "")
                //strValue = (double.Parse(strValue) * double.Parse(strIb)).ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "QDSYJLDM,";  //起动试验结论代码
                strValue = Get_METER_START_NO_LOAD("1091", "AVR_CONCLUSION");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDSYDYZ,";  //潜动试验电压值
                strValue = Get_METER_START_NO_LOAD("1101115", "AVR_VOLTAGE");
                if (strValue == "")
                    strValue = "1";
                // strValue = OldRead["AVR_UB"].ToString().Trim();
                strValue = (double.Parse(strValue) * double.Parse(OldRead["AVR_UB"].ToString().Trim())).ToString().Trim() + "V";
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "FQDLZ,";  //防潜电流值
                strOracleSQL_Value = strOracleSQL_Value + "','0";
                strOracleSQL_Name = strOracleSQL_Name + "QISYJLDM,";  //潜动试验结论代码
                strValue = Get_METER_START_NO_LOAD("1101115", "AVR_CONCLUSION");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JBWCSYJLDM,";  //基本误差试验结论代码
                strValue = Get_METER_RESULTS("103");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "RJSWCSYJLDM,";  //日计时误差试验结论代码
                strValue = Get_METER_COMMUNICATION("002");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "RJSWCZ,";  //日计时误差值
                strValue = Get_METER_COMMUNICATION("00201");
                int iTmpPoint = strValue.IndexOf("|");
                if (iTmpPoint > 0)
                    strValue = strValue.Substring(iTmpPoint + 1, strValue.Length - iTmpPoint - 1);

                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;



                strOracleSQL_Name = strOracleSQL_Name + "JDQZDNSZWCSYJLDM,";  //计度器总电能示值误差试验结论代码
                strValue = Get_METER_COMMUNICATION("005");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "FLSDDNSSWCSYJLDM,";  //费率时段电能示数误差试验结论代码
                strValue = Get_METER_COMMUNICATION("006");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;


                //strOracleSQL_Name = strOracleSQL_Name + "XLZQWCSYJLDM,";  //需量周期误差试验结论代码
                //strValue = Get_METER_COMMUNICATION("0151");
                //strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "ZDXLWCSYJLDM,";  //最大需量误差试验结论代码
                strValue = Get_METER_COMMUNICATION("0151");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);


                strOracleSQL_Name = strOracleSQL_Name + "YZXSYJLDM,";  //一致性试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("411020700");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCBCSYJLDM,";  //误差变差试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("511020700");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCYZXSYJLDM,";  //误差一致性试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("611010100");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                //strOracleSQL_Name = strOracleSQL_Name + "FZDLSYBCSYJLDM,";  //负载电流升降变差试验结论代码
                //strValue = Get_METER_CONSISTENCY_DATA("411010700");
                //strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "TXGNJCJLDM,";  //通信功能检查结论代码
                strValue = Get_METER_COMMUNICATION("001");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "SDTQWCSYJLDM,";  //时段投切误差试验结论代码
                strValue = Get_METER_COMMUNICATION("004");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "GPSDSJLDM,";  //GPS对时结论代码
                strValue = Get_METER_COMMUNICATION("007");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JDRYBH,";  //检定员编号
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Jyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Name = strOracleSQL_Name + "HYRYBH,";  //核验员编号
                strValue = OldRead["AVR_AUDIT_PERSON"].ToString().Trim();
                string strSection = "MIS_Info/UserName/Item";
                string strXmlValue = "";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Hyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                strOracleSQL_Name = strOracleSQL_Name + "BZZZZCBH,";  //电能计量标准设备资产编号
                strSection = "NewUser/CloumMIS/Item";
                strXmlValue = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_equipment", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Value = strOracleSQL_Value + "','" + strXmlValue;

                #region ----------日期型处理

                strOracleSQL_Name = strOracleSQL_Name + "JDRQ,";  //检定日期
                // strValue = OldRead["DTM_TEST_DATE"].ToString().Trim();
                strValue = Convert.ToDateTime(OldRead["DTM_TEST_DATE"]).ToShortDateString().Trim() + " " + DateTime.Now.ToLongTimeString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                strOracleSQL_Name = strOracleSQL_Name + "HYRQ";  //核验日期
                strValue = Convert.ToDateTime(OldRead["DTM_TEST_DATE"]).ToShortDateString().Trim() + " " + DateTime.Now.ToLongTimeString().Trim();
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
                for (int iCirc = 1; iCirc < 6; iCirc++)
                {
                    string strCode = "AVR_SEAL_" + iCirc.ToString().Trim();
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
                        lis_seal.Add(OldRead[strCode].ToString().Trim());
                        strOracleSQL_Name = strOracleSQL_Name + "JFWZDM,";//加封位置代码-------
                        strValue = ColSeal[iCirc - 1];
                        //strValue = Get_JFWZDM(iCirc.ToString().Trim());
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                        strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                        strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                        strOracleSQL_Name = strOracleSQL_Name + "JFSJ";//时间
                        //strValue = Convert.ToDateTime(OldRead["DTM_TEST_DATE"]).ToShortDateString().Trim() + " " + DateTime.Now.ToLongTimeString().Trim();
                        strValue = OldRead["DTM_TEST_DATE"].ToString().Trim();
               
                        strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                        strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                        lis_Sql.Add(strOracleSQL);
                    }
                }

                #endregion


                AccessConntion.Close();
                OldRead.Close();
                watch.Stop();
                Runtime = Runtime + "执行：" + watch.ElapsedMilliseconds.ToString() + "ms";
            }
            catch (Exception Error)
            {
                AccessConntion.Close();
                OldRead.Close();
            }
            Col_For_Seal = lis_seal;
            return lis_Sql;

        }
        /// <summary>
        /// 被检表多功能信息
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_COMMUNICATION(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_VALUE FROM METER_COMMUNICATION where AVR_PROJECT_NO='" + RESULT_ID + "' and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }
        private static string Get_METER_COMMUNICATION_HanPu(string RESULT_ID, string meterId)
        {
            string strResults = "";
            string strSQL = " SELECT DialEnd FROM [Meters].[dbo].[PM_Constant] where TestID='" + meterId + "'";
            string ServerName = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_SqlServerName", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
            string Con = string.Format("Server={0};Database=Meters;Trusted_Connection=SSPI", ServerName);
            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData_SqlServer(strSQL, Con);


            return strResults;
        }
        private static string Get_METER_COMMUNICATION(string RESULT_ID,string meterId)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_VALUE FROM METER_COMMUNICATION where AVR_PROJECT_NO='" + RESULT_ID + "' and FK_LNG_METER_ID='" + meterId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }
        /// <summary>
        /// 日计量误差记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKRJSWC(string PK_ID)
        {
            List<string> listSQL = new List<string>();
         
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into vt_sb_jkrjswc (";
            #region 规避重复数据
            List<string> ProjectCol = new List<string>();
            #endregion
            try
            {
                strValue = Get_METER_COMMUNICATION("00202");
                if (strValue.IndexOf("|") > 0)  //有误差值
                {
                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                    char[] csplit = { '|' };
                    string[] strParm = null;
                    strParm = strValue.Split(csplit);
                    strOracleSQL_Name = strOracleSQL_Name + "CSZ1,";   //误差1
                    if (strParm.Length > 0)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ2,";   //误差2
                    if (strParm.Length > 1)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ3,";   //误差3
                    if (strParm.Length > 2)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";
                    strOracleSQL_Name = strOracleSQL_Name + "CSZ4,";   //误差4
                    if (strParm.Length > 3)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[3];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "CSZ5,";   //误差5
                    if (strParm.Length > 4)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[4];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";


                    strValue = Get_METER_COMMUNICATION("00201");//平均值与 化整值
                    if (strValue.IndexOf("|") > 0)  //有误差值
                    {
                        strParm = strValue.Split(csplit);
                        strOracleSQL_Name = strOracleSQL_Name + "PJZ,";   //平均值
                        if (strParm.Length > 0)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";


                    }

                    strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";

                    listSQL.Add(strOracleSQL);
                }
               
            }
            catch { }

            return listSQL;
        }
        /// <summary>
        /// 电能表误差记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBJDWC(string PK_ID)
        {
            
            List<string> listSQL = new List<string>();
            string strSQL = "SELECT * FROM METER_ERROR WHERE  FK_LNG_METER_ID='" + PK_ID + "'and CHR_ERROR_TYPE = '0' order by CHR_POWER_TYPE,AVR_IB_MULTIPLE ";
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBJDWC (";

            string Error_avr = "", Error_limit = "", Runtime="";
            string ErrorResult = "Y";
            #region 去除重复信息
            List<string> proCol = new List<string>();


            #endregion
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            try
            {
                #region 获取误差表的信息
                AccessConntion.Open();
                OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);


              


                Stopwatch watch = new Stopwatch();
                watch.Start();
                OleDbDataReader red = ccmd.ExecuteReader();
                watch.Stop();
                Runtime = watch.ElapsedMilliseconds.ToString() + "ms";

                watch = new Stopwatch();
                watch.Start();



                while (red.Read() == true)
                {
                    strOracleSQL = "insert into VT_SB_JKDNBJDWC (";
                    strOracleSQL_Value = "";
                    strOracleSQL_Name = "";

                    if (!OutRunSampleInfo(ref proCol, red["AVR_PROJECT_NO"].ToString().Trim()))
                    {
                        continue;
                    }


                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                    strOracleSQL_Name = strOracleSQL_Name + "GLFXDM,";   //功率方向代码
                    strValue = Get_GLFXDM(red["CHR_POWER_TYPE"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    strOracleSQL_Name = strOracleSQL_Name + "GLYSDM,";   //功率因数代码
                    strValue = Get_GLYSDM(red["AVR_POWER_FACTOR"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                    strOracleSQL_Name = strOracleSQL_Name + "FZDLDM,";   //负载电流代码
                    strValue = Get_FZDLDM(red["AVR_IB_MULTIPLE"].ToString().Trim());
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "XBDM,";   //相别代码 三相、单相
                    strValue = Get_XBDM(strXBDM);
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "FZLXDM,";   //负载类型代码 平衡负载、不平衡负载-1、2、3、4
                    strValue = red["CHR_COMPONENT"].ToString().Trim();
                    //strValue = "01";
                    //if (csPublicMember.strFZLXDM == "01" || csPublicMember.strFZLXDM == "1") 
                    //    strValue = "02";
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "FYDM,";   //分元代码 01、02、03、04
                    strValue = red["CHR_COMPONENT"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue.PadLeft(2, '0');

                    strValue = red["AVR_ERROR_MORE"].ToString().Trim();
                    char[] csplit = { '|' };
                    string[] strParm = null;
                    string[] strLimite = null;
                    strParm = strValue.Split(csplit);

                    Error_limit = red["AVR_UPPER_LIMIT"].ToString().Trim();
                    strLimite = Error_limit.Split(csplit);

                    Error_limit = strLimite[0];
                    if (strParm.Length > 2 && !(red["AVR_ERROR_CONCLUSION"].ToString().Trim().IndexOf("格", 0) >= 0))
                    {
                        ErrorResult = Convert.ToDouble(Error_limit) - Math.Abs(Convert.ToDouble(strParm[strParm.Length - 2])) > 0 ? "Y" : "N";
                    }


                    strOracleSQL_Name = strOracleSQL_Name + "WCZ1,";   //误差1
                    if (strParm.Length > 1)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ2,";   //误差2
                    if (strParm.Length > 2)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ3,";   //误差3
                    if (strParm.Length > 4)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ4,";   //误差4
                    if (strParm.Length > 5)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[3];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCZ5,";   //误差5
                    if (strParm.Length > 7)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[4];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCPJZ,";   //误差平均值
                    if (strParm.Length > 2)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[strParm.Length - 2];


                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";

                    strOracleSQL_Name = strOracleSQL_Name + "WCXYZ,";   //误差修约值
                    if (strParm.Length > 2)
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[strParm.Length - 1];
                    else
                        strOracleSQL_Value = strOracleSQL_Value + "','";



                    strOracleSQL_Name = strOracleSQL_Name + "JLDM,";   //结论代码
                    strValue = red["AVR_ERROR_CONCLUSION"].ToString().Trim();
                    if (!(red["AVR_ERROR_CONCLUSION"].ToString().Trim().IndexOf("格", 0) >= 0))
                    {
                        strOracleSQL_Value = strOracleSQL_Value + "','" + ErrorResult;

                    }
                    else
                    {
                        strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);


                    }

                    strOracleSQL_Name = strOracleSQL_Name + "WCCZ,";   //不平衡负载与平衡负载的误差差值
                    strValue = red["AVR_DIF_ERR_AVG"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "WCCZXYZ,";   //误差差值修约值
                    strValue = red["AVR_DIF_ERR_ROUND"].ToString().Trim();
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "DQBM";  //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";
                  
                    listSQL.Add(strOracleSQL);
                }
                red.Close();
                AccessConntion.Close();
                AccessConntion.Dispose();
                #endregion
                watch.Stop();
                Runtime = Runtime + "执行：" + watch.ElapsedMilliseconds.ToString() + "ms";

                #region 获取特殊检定表的影响量数据
                //strSQL = "SELECT * FROM METER_SPECIAL_DATA WHERE  FK_LNG_METER_ID='" + PK_ID + "'";

                //if (AccessConntion.State == System.Data.ConnectionState.Closed)
                //    AccessConntion.Open();
                //ccmd = new OleDbCommand(strSQL, AccessConntion);
                //red = ccmd.ExecuteReader();
                //while (red.Read() == true)
                //{
                //    strOracleSQL = "insert into VT_SB_JKDNBJDWC (";
                //    strOracleSQL_Value = "";
                //    strOracleSQL_Name = "";



                //    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                //    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH ;
                //    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                //    strOracleSQL_Name = strOracleSQL_Name + "GLFXDM,";   //功率方向代码

                //    strValue = Get_GLFXDM(red["CHR_POWER_TYPE"].ToString().Trim());
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //    strOracleSQL_Name = strOracleSQL_Name + "GLYSDM,";   //功率因数代码
                //    if (red["AVR_PROJECT_NO"].ToString().Trim().Contains("0.5L"))
                //    {
                //        strValue = "1";
                //    }
                //    else
                //    {
                //        strValue = "2";
                //    }
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //    strOracleSQL_Name = strOracleSQL_Name + "FZDLDM,";   //负载电流代码
                //    if (red["AVR_PROJECT_NO"].ToString().Trim().Contains("1.2"))
                //    {
                //        strValue = "14";//1.2Un
                //    }
                //    else
                //    {
                //        strValue = "13";//0.8Un
                //    }

                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //    strOracleSQL_Name = strOracleSQL_Name + "XBDM,";   //相别代码 三相、单相
                //    strValue = Get_XBDM(strXBDM);
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //    strOracleSQL_Name = strOracleSQL_Name + "FZLXDM,";   //负载类型代码 平衡负载、不平衡负载-1、2、3、4
                //    strValue = red["CHR_COMPONENT"].ToString().Trim();
                //    //strValue = "01";
                //    //if (csPublicMember.strFZLXDM == "01" || csPublicMember.strFZLXDM == "1") 
                //    //    strValue = "02";
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //    strOracleSQL_Name = strOracleSQL_Name + "FYDM,";   //分元代码 01、02、03、04
                //    strValue = red["CHR_COMPONENT"].ToString().Trim();
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue.PadLeft(2, '0');

                //    strValue = red["AVR_ERROR_MORE"].ToString().Trim();
                //    char[] csplit = { '|' };
                //    string[] strParm = null;
                //    strParm = strValue.Split(csplit);


                //    strOracleSQL_Name = strOracleSQL_Name + "WCZ1,";   //误差1
                //    if (strParm.Length > 1)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCZ2,";   //误差2
                //    if (strParm.Length > 2)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCZ3,";   //误差3
                //    if (strParm.Length > 4)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCZ4,";   //误差4
                //    if (strParm.Length > 5)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[3];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCZ5,";   //误差5
                //    if (strParm.Length > 7)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[4];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCPJZ,";   //误差平均值
                //    if (strParm.Length > 2)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[strParm.Length - 2];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";

                //    strOracleSQL_Name = strOracleSQL_Name + "WCXYZ,";   //误差修约值
                //    if (strParm.Length > 2)
                //        strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[strParm.Length - 1];
                //    else
                //        strOracleSQL_Value = strOracleSQL_Value + "','";



                //    strOracleSQL_Name = strOracleSQL_Name + "JLDM,";   //结论代码
                //    strValue = red["AVR_ERROR_CONCLUSION"].ToString().Trim();
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);


                //    strOracleSQL_Name = strOracleSQL_Name + "WCCZ,";   //不平衡负载与平衡负载的误差差值
                //    strValue = "";
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //    strOracleSQL_Name = strOracleSQL_Name + "WCCZXYZ,";   //误差差值修约值
                //    strValue = "";
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //    strOracleSQL_Name = strOracleSQL_Name + "DQBM";  //地区编码
                //    strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                //    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";
                  
                //    listSQL.Add(strOracleSQL);
                //}
                //red.Close();
                //AccessConntion.Close();
                //AccessConntion.Dispose();
                #endregion

            }
            catch (Exception error) { }
            finally
            {
                AccessConntion.Close();
            }


            return listSQL;
        }
        /// <summary>
        /// 电能表示数记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBSSJL(string PK_ID)
        {
            
            List<string> listSQL = new List<string>();
            string strValue = "";
            string strTypeCode = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBSSJL (";
            #region 防止重复数据
            List<string> ProjectCol = new List<string>();
            #endregion
            try
            {
                for (int iCirc = 1; iCirc < 5; iCirc++) //P+,P-,Q+,Q-, Q1,Q2,Q3,Q4
                {

                    strTypeCode = "0170" + iCirc.ToString().Trim();    //01701正向有功 /01702反向有功 /01703正向无功/01704反向无功

                    strValue = Get_METER_COMMUNICATION(strTypeCode, PK_ID);
                    if (strValue.IndexOf("|") > 0)  //有误差值
                    {
                        char[] csplit = { '|' };//总|尖|峰|平|谷|
                        string[] strParm = null;
                        strParm = strValue.Split(csplit);
                        if (strParm.Length >= 4)
                        {
                            for (int iCircLx = 0; iCircLx < strParm.Length; iCircLx++) //总|尖|峰|平|谷|
                            {
                                if (iCircLx == 1) continue;
                                strOracleSQL_Name = "";
                                strOracleSQL_Value = "";
                                strOracleSQL = "insert into VT_SB_JKDNBSSJL (";

                                //if (!OutRunSampleInfo(ref ProjectCol, strTypeCode))
                                //{
                                //    continue;
                                //}

                                strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                                strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH ;
                                strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                                strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                                strOracleSQL_Name = strOracleSQL_Name + "SSLXDM,";   //示数类型代码
                                strValue = strTypeCode + iCircLx.ToString().Trim();
                                strValue = Get_SSLXDM(strValue);
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                                strOracleSQL_Name = strOracleSQL_Name + "BSS,";   //表示值
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[iCircLx];

                                strOracleSQL_Name = strOracleSQL_Name + "DQBM,";   //地区编码
                                strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                                strOracleSQL_Name = strOracleSQL_Name + "CBSJ";  //抄表时间----检定日期代替
                                strValue = strJDTime;
                                strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                                strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";


                                listSQL.Add(strOracleSQL);
                               



                            }
                        }
                    }
                }
            }
            catch { }
            return listSQL;
        }
        /// <summary>
        /// 电能表投切记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKSDTQWCJL(string PK_ID)
        {
            
            List<string> listSQL = new List<string>();
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKSDTQWCJL (";
            #region  规避重复数据
            List<string> ProjectCol = new List<string>();
            #endregion

            try
            {
                for (int iCirc = 1; iCirc < 6; iCirc++)
                {
                   
                    strValue = "0040" + iCirc.ToString();    //00401/00402/00403


                    if (!OutRunSampleInfo(ref ProjectCol, strValue))
                    {

                        continue;
                    }
                    strValue = Get_METER_COMMUNICATION(strValue);
                    if (strValue.IndexOf("|") > 0)  //有误差值
                    {

                        strOracleSQL_Name = "";
                        strOracleSQL_Value = "";
                        strOracleSQL = "insert into VT_SB_JKSDTQWCJL (";

                        char[] csplit = { '|' };
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
                            if (strParm.Length > 0)
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";

                            strOracleSQL_Name = strOracleSQL_Name + "SJTQSJ,";   //实际投切时间
                            if (strParm.Length > 1)
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";

                            strOracleSQL_Name = strOracleSQL_Name + "TQWC,";   //投切误差
                            if (strParm.Length > 2)
                            {
                                strValue = strParm[2].Replace(":", "");
                                strValue = int.Parse(strValue).ToString();
                                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                            }
                            else
                                strOracleSQL_Value = strOracleSQL_Value + "','";


                            strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                            strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;


                            strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";

                            listSQL.Add(strOracleSQL);
                        }
                    }
                }
            }
            catch { }




            return listSQL;
        }
        /// <summary>
        /// 电能表需量记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKXLWCJL(string PK_ID,out List<string> Col_For_demand)
        {

            List<string> listSQL = new List<string>();
            List<string> listDemand = new List<string>();
            
            string strValue = "";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKXLWCJL (";
            #region  规避重复数据
            List<string> ProjectCol = new List<string>();
            #endregion

            try
            {
                for (int iCirc = 14; iCirc < 17; iCirc++)
                {
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    strOracleSQL = "insert into VT_SB_JKXLWCJL (";

                    strValue = iCirc.ToString("000") + "11";    //01401/01501/01601
                    if (!OutRunSampleInfo(ref ProjectCol, strValue))
                    {

                        continue;
                    }

                    strValue = Get_METER_COMMUNICATION(strValue);
                    if (strValue.IndexOf("|") > 0)  //有误差值
                    {
                        char[] csplit = { '|' };
                        string[] strParm = null;
                        strParm = strValue.Split(csplit);

                        strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                        strOracleSQL_Value = strOracleSQL_Value + "'" +str_GZDBH ;
                        strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                        strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                
                        if (iCirc == 14)
                        {
                            strValue = "02";   //0.1IB
                            listDemand.Add("上传最大需量0.1IB");
                        }
                        if (iCirc == 15)
                        {
                            strValue = "05";   //IB
                            listDemand.Add("上传最大需量1IB");

                        }
                        if (iCirc == 16)
                        {
                            strValue = "06";   //Imax
                            listDemand.Add("上传最大需量Imax");

                        } 
                        strOracleSQL_Name = strOracleSQL_Name + "FZDLDM,";   //负载电流代码
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;


                        strOracleSQL_Name = strOracleSQL_Name + "BZZDXL,";   //标准最大需量
                        if (strParm.Length > 0)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[0];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "SJXL,";   //实际需量
                        if (strParm.Length > 1)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[1];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "WCZ,";   //误差值
                        if (strParm.Length > 2)
                            strOracleSQL_Value = strOracleSQL_Value + "','" + strParm[2];
                        else
                            strOracleSQL_Value = strOracleSQL_Value + "','";


                        strValue = Get_METER_COMMUNICATION(iCirc.ToString("000"));//结论代码
                        strOracleSQL_Name = strOracleSQL_Name + "JLDM,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue) + "','";

                        strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                        strOracleSQL_Value = strOracleSQL_Value + str_DQBM;


                        strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";

                        listSQL.Add(strOracleSQL);
                    }
                }
            }
            catch { }
            Col_For_demand = listDemand;
            return listSQL;
        }
        /// <summary>
        /// 电能表走字记录表
        /// </summary>
        /// <returns></returns>
        private static List<string> VT_SB_JKDNBZZJL(string PK_ID)
        {
            bool blnOK = true;
            List<string> listSQL = new List<string>();   
            string strValue = "";
            string strTypeCode = "";
            string strSQL = "select * from METER_ENERGY_TEST_DATA  where  FK_LNG_METER_ID='" + PK_ID + "'";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string strOracleSQL = "insert into VT_SB_JKDNBZZJL (";
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            try
            {
                AccessConntion.Open();
                OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);
                OleDbDataReader red = ccmd.ExecuteReader();
                #region 防止重复数据
                List<string> ProjectCol = new List<string>();
                #endregion
                while (red.Read() == true)
                {
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    strOracleSQL = "insert into VT_SB_JKDNBZZJL (";

                    if (!OutRunSampleInfo(ref ProjectCol, red["AVR_PROJECT_NO"].ToString().Trim()))
                    {
                        continue;
                    }


                    strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";   //工作单编号
                    strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                    strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";   //资产编号
                    strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;

                    strOracleSQL_Name = strOracleSQL_Name + "SSLXDM,";   //示数类型代码
                    strValue = red["CHR_POWER_TYPE"].ToString().Trim();
                    //strTypeCode = "0170" + strValue;    //01701正向有功 /01702反向有功 /01703正向无功/01704反向无功
                    strTypeCode = strValue + red["AVR_RATES"].ToString().Trim(); // 总、峰平谷尖
                    strValue = Get_SSLXDM(strTypeCode);
                    strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                    strOracleSQL_Name = strOracleSQL_Name + "bzqqss,";   //标准器起示数
                    strOracleSQL_Value = strOracleSQL_Value + "','0";
                    strOracleSQL_Name = strOracleSQL_Name + "bzqzss,";   //标准器止示数
                    strOracleSQL_Value = strOracleSQL_Value + "','" + red["AVR_DIF_ENERGY"].ToString().Trim();
                    strOracleSQL_Name = strOracleSQL_Name + "qss,";   //被检电能表起示数
                    strOracleSQL_Value = strOracleSQL_Value + "','" + red["AVR_START_ENERGY"].ToString().Trim();
                    strOracleSQL_Name = strOracleSQL_Name + "zss,";   //被检电能表止示数
                    strOracleSQL_Value = strOracleSQL_Value + "','" + red["AVR_END_ENERGY"].ToString().Trim();
                    strOracleSQL_Name = strOracleSQL_Name + "zzwc,";   //走字误差
                    strOracleSQL_Value = strOracleSQL_Value + "','" + (Convert.ToDouble(red["AVR_END_ENERGY"].ToString().Trim()) - Convert.ToDouble(red["AVR_START_ENERGY"].ToString().Trim())).ToString("#0.000");

                    strOracleSQL_Name = strOracleSQL_Name + "DQBM";   //地区编码
                    strOracleSQL_Value = strOracleSQL_Value + "','" +str_DQBM ;

                    strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + "')";

                    listSQL.Add(strOracleSQL);
                    
                }
            }
            catch { }

            return listSQL;
        }
        /// <summary>
        /// 转换铅封位置代码
        /// </summary>
        /// <param name="str_Seal"></param>
        /// <returns></returns>
        private static string SwitchSealNum(string str_Seal)
        {
            string str_SealNum = "";
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
        /// 多功能结论读取
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_RESULTS(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_RESULT_VALUE FROM METER_RESULTS where AVR_RESULT_ID='" + RESULT_ID + "' and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }
        /// <summary>
        /// 启动潜动数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_START_NO_LOAD(string RESULT_ID, string strName)
        {
            string strResults = "";
            string strSQL = " SELECT " + strName + " FROM METER_START_NO_LOAD where AVR_PROJECT_NO='" + RESULT_ID + "' and  FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }

        /// <summary>
        /// 一致性试验数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_CONSISTENCY_DATA(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_CONC FROM METER_CONSISTENCY_DATA where FK_LNG_SCHEME_ID=" + RESULT_ID + " and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);



            return strResults;
        }
        private static bool OutRunSampleInfo(ref List<string> projectCol, string NowProjectId)
        {
            if (projectCol.Count < 1)
            {
                projectCol.Add(NowProjectId);
                return true;
            }
            else
            {
                if (projectCol.Contains(NowProjectId))
                {
                    return false;
                }
                else
                {
                    projectCol.Add(NowProjectId);
                    return true;
                }
            }

        }
        /// <summary>
        /// 功率方向代码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private static string Get_GLFXDM(string strValue)
        {
            string strResults = "1";
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
                    strResults = "01";
                    break;
                case "0.1IB":
                case ".1IB":
                    strResults = "02";
                    break;
                case "0.2IB":
                case ".2IB":
                    strResults = "03";
                    break;
                case "0.5IB":
                case ".5IB":
                    strResults = "04";
                    break;
                case "1.0IB":
                case "1IB":
                    strResults = "05";
                    break;
                case "IMAX":
                    strResults = "06";
                    break;
                case "0.5IMAX":
                    strResults = "07";
                    break;
                case "4IB":
                case "4.0IB":
                    strResults = "08";
                    break;
                case "3IB":
                case "3.0IB":
                    strResults = "09";
                    break;
                case "2IB":
                case "2.0IB":
                    strResults = "10";
                    break;
                case "0.02IB":
                case ".02IB":
                    strResults = "11";
                    break;
                case "0.01IB":
                case ".01IB":
                    strResults = "12";
                    break;
                case "0.03IB":
                case ".03IB":
                    strResults = "15";
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
            string strResults = "05";
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
                    strResults = "121";
                    break;
                case "017011"://正向有功 峰
                case "1峰":
                    strResults = "122";
                    break;
                case "017012"://正向有功 平
                case "1平":
                    strResults = "123";
                    break;
                case "017013"://正向有功 谷
                case "1谷":
                    strResults = "124";
                    break;
                case "017014"://正向有功 尖
                case "1尖":
                    strResults = "125";
                    break;
                case "017020"://反向有功 总
                case "2总":
                    strResults = "221";
                    break;
                case "017021"://         峰
                case "2峰":
                    strResults = "225";
                    break;
                case "017022"://         平
                case "2平":
                    strResults = "222";
                    break;
                case "017023"://         谷
                case "2谷":
                    strResults = "223";
                    break;
                case "017024"://         尖
                case "2尖":
                    strResults = "224";
                    break;
                case "017030"://正向无功 总
                case "3总":
                    strResults = "131";
                    break;
                case "017031"://         峰
                case "3峰":
                    strResults = "132";
                    break;
                case "017032"://         平
                case "3平":
                    strResults = "133";
                    break;
                case "017033"://         谷
                case "3谷":
                    strResults = "135";
                    break;
                case "017034"://         尖
                case "3尖":
                    strResults = "134";
                    break;
                case "017040"://反向无功 总
                case "4总":
                    strResults = "231";
                    break;
                case "017041"://         峰
                case "4峰":
                    strResults = "232";
                    break;
                case "017042"://         平
                case "4平":
                    strResults = "236";
                    break;
                case "017043"://         谷
                case "4谷":
                    strResults = "238";
                    break;
                case "017044"://         尖
                case "4尖":
                    strResults = "237";
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
            string strResults = "05";
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
        #region 别的厂家
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj(string CheckT, bool Flag,List<string> outPutList)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            switch (OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "Cmb_Facory", "Value", "", BaseConfigPath))
            {
                case "科陆电子":
                    temp = SetMemberForZj(CheckT, outPutList);
                    break;
                case "格宁":
                    temp = SetMemberForZj_GeNing(CheckT, outPutList);
                    break;
                case "涵普":
                    temp = SetMemberForZj_HanPu(CheckT, outPutList);
                    break;
            }
            return temp;

        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj(string CheckT, bool Flag)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            switch (OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "Cmb_Facory", "Value", "", BaseConfigPath))
            {
                case "科陆电子":
                    temp = SetMemberForZj(CheckT);
                    break;
                case "格宁":
                    temp = SetMemberForZj_GeNing(CheckT);
                    break;
                case "涵普":
                    temp = SetMemberForZj_HanPu(CheckT);
                    break;
            }
            return temp;
        
        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj_HanPu(string CheckTime)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            try
            {
                string ServerName = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_SqlServerName", "Value", "", BaseConfigPath);
                string Con = string.Format("Server={0};Database=Meters;Trusted_Connection=SSPI", ServerName);
                using (SqlConnection conn = new SqlConnection(Con))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql = string.Format("SELECT *  FROM [Meters].[dbo].[PM_Meters] WHERE FinishTime='{0}'", CheckTime);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader Myreader = null;
                    Myreader = cmd.ExecuteReader();

                    while (Myreader.Read())
                    {
                        temp.Add(new Clou_Report.Model.MemberForZJ()
                        {
                            StrZCBH = Myreader["Barcode"].ToString().Trim(),
                            StrMeterID = Myreader["TestID"].ToString().Trim(),
                            StrEquipment = Myreader["ProductName"].ToString().Trim(),
                            StrEquipmentType = Myreader["METype"].ToString().Trim() == "0" ? "电子式" : "机械式",
                            StrEquipmentSize = Myreader["Model"].ToString().Trim(),
                            StrFactory = Myreader["Manufacturer"].ToString().Trim(),
                            StrCheckTime = CheckTime.ToString().Trim(),
                            StrResult = Myreader["Result"].ToString().Trim() == "1" ? "合格" : "不合格",



                        });
                    }
                    //char[] csplit = { '|' };//总|尖|峰|平|谷|
                    //string[] strParm = null;
                    foreach (Clou_Report.Model.MemberForZJ member in temp)
                    {
                        member.StrElectric = Get_METER_COMMUNICATION_HanPu("",member.StrMeterID);
                    }
                    //conn.Close();

                    return temp;
                }
            }
            catch
            {
                return temp;
            }
        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj_HanPu(string CheckTime,List<string> lis_Id)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            try
            {
                string ServerName = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_SqlServerName", "Value", "", BaseConfigPath);
                string Con = string.Format("Server={0};Database=Meters;Trusted_Connection=SSPI", ServerName);
                using (SqlConnection conn = new SqlConnection(Con))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    foreach (string temp_id in lis_Id)
                    {
                        string sql = string.Format("SELECT *  FROM [Meters].[dbo].[PM_Meters] WHERE FinishTime='{0}' and TestID='{1}'", CheckTime, temp_id);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader Myreader = null;
                        Myreader = cmd.ExecuteReader();

                        while (Myreader.Read())
                        {
                            temp.Add(new Clou_Report.Model.MemberForZJ()
                            {
                                StrZCBH = Myreader["Barcode"].ToString().Trim(),
                                StrMeterID = Myreader["TestID"].ToString().Trim(),
                                StrEquipment = Myreader["ProductName"].ToString().Trim(),
                                StrEquipmentType = Myreader["METype"].ToString().Trim() == "0" ? "电子式" : "机械式",
                                StrEquipmentSize = Myreader["Model"].ToString().Trim(),
                                StrFactory = Myreader["Manufacturer"].ToString().Trim(),
                                StrCheckTime = CheckTime.ToString().Trim(),
                                StrResult = Myreader["Result"].ToString().Trim() == "1" ? "合格" : "不合格",



                            });
                        }
                        //char[] csplit = { '|' };//总|尖|峰|平|谷|
                        //string[] strParm = null;
                        foreach (Clou_Report.Model.MemberForZJ member in temp)
                        {
                            member.StrElectric = Get_METER_COMMUNICATION_HanPu("", member.StrMeterID);
                        }
                        Myreader.Dispose();
                    }
                    
                    conn.Close();

                    return temp;
                }
            }
            catch
            {
                return temp;
            }
        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj_GeNing(string CheckTime)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            try
            {

                using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql = string.Format("Select * from ResultData WHERE TESTDATE='{0}'", CheckTime);
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    OleDbDataReader Myreader = null;
                    Myreader = cmd.ExecuteReader();

                    while (Myreader.Read())
                    {
                        temp.Add(new Clou_Report.Model.MemberForZJ()
                        {
                            StrZCBH = Myreader["SerialNo"].ToString().Trim(),
                            StrMeterID = Myreader["MeterID"].ToString().Trim(),
                            StrEquipment = Myreader["WireModel"].ToString().Trim() + Myreader["Type"].ToString().Trim() +"电能表",
                            StrEquipmentType = Myreader["Type"].ToString().Trim(),
                            StrEquipmentSize = Myreader["Model"].ToString().Trim(),
                            StrFactory = Myreader["MadePlace"].ToString().Trim(),
                            StrCheckTime = CheckTime.ToString().Trim(),
                            StrResult = Myreader["Conclusion"].ToString().Trim(),
                            StrElectric = Myreader["CUNDUTOTAL"].ToString().Trim(),
                            

                        });
                    }
                    //char[] csplit = { '|' };//总|尖|峰|平|谷|
                    //string[] strParm = null;
                    //foreach (Clou_Report.Model.MemberForZJ member in temp)
                    //{
                    //    member.StrElectric = (Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit).Length > 0) ? Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit)[0] : "";
                    //}
                    //conn.Close();

                    return temp;
                }
            }
            catch
            {
                return temp;
            }
        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj_GeNing(string CheckTime, List<string> lis_Id)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            try
            {

                using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    foreach (string temp_id in lis_Id)
                    {
                        string sql = string.Format("Select * from ResultData WHERE TESTDATE='{0}'and MeterID={1}", CheckTime,temp_id);
                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        OleDbDataReader Myreader = null;
                        Myreader = cmd.ExecuteReader();

                        while (Myreader.Read())
                        {
                            temp.Add(new Clou_Report.Model.MemberForZJ()
                            {
                                StrZCBH = Myreader["SerialNo"].ToString().Trim(),
                                StrMeterID = Myreader["MeterID"].ToString().Trim(),
                                StrEquipment = Myreader["WireModel"].ToString().Trim() + Myreader["Type"].ToString().Trim() + "电能表",
                                StrEquipmentType = Myreader["Type"].ToString().Trim(),
                                StrEquipmentSize = Myreader["Model"].ToString().Trim(),
                                StrFactory = Myreader["MadePlace"].ToString().Trim(),
                                StrCheckTime = CheckTime.ToString().Trim(),
                                StrResult = Myreader["Conclusion"].ToString().Trim(),
                                StrElectric = Myreader["CUNDUTOTAL"].ToString().Trim(),


                            });
                        }
                        Myreader.Dispose();
                    }
                    
                   
                    conn.Close();

                    return temp;
                }
            }
            catch
            {
                return temp;
            }
        }
        #endregion 
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj(string CheckTime)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp=new ObservableCollection<Clou_Report.Model.MemberForZJ> ();
            try
            {

               using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql=string.Format("Select * from METER_INFO WHERE DTM_TEST_DATE=#{0}#",CheckTime);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader Myreader = null;
                Myreader = cmd.ExecuteReader();

                while (Myreader.Read())
                {
                    temp.Add(new Clou_Report.Model.MemberForZJ()
                    {
                        StrZCBH=Myreader["AVR_ASSET_NO"].ToString().Trim(),
                        StrMeterID=Myreader["PK_LNG_METER_ID"].ToString().Trim(),
                        StrEquipment=Myreader["AVR_METER_NAME"].ToString().Trim(),
                        StrEquipmentType=Myreader["AVR_METER_TYPE"].ToString().Trim(),
                        StrEquipmentSize=Myreader["AVR_METER_MODEL"].ToString().Trim(),
                        StrFactory=Myreader["AVR_FACTORY"].ToString().Trim(),
                        StrCheckTime=CheckTime.ToString().Trim(),
                        StrResult=Myreader["AVR_TOTAL_CONCLUSION"].ToString().Trim(),
                       
                        
                    });
                }
                 char[] csplit = { '|' };//总|尖|峰|平|谷|
                 string[] strParm = null;
                 foreach(Clou_Report.Model.MemberForZJ member in temp)
                 {
                     member.StrElectric =(Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit).Length>0)?Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit)[0]:"";
                 }
                conn.Close();

                return temp;
            }
            }
            catch
            {
                return temp;
            }
        }
        public ObservableCollection<Clou_Report.Model.MemberForZJ> SetMemberForZj(string CheckTime, List<string> lis_Id)
        {
            ObservableCollection<Clou_Report.Model.MemberForZJ> temp = new ObservableCollection<Clou_Report.Model.MemberForZJ>();
            try
            {

                using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    foreach (string Temp_id in lis_Id)
                    {
                        string sql = string.Format("Select * from METER_INFO WHERE DTM_TEST_DATE=#{0}# and PK_LNG_METER_ID='{1}'", CheckTime, Temp_id);
                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        OleDbDataReader Myreader = null;
                        Myreader = cmd.ExecuteReader();

                        while (Myreader.Read())
                        {
                            temp.Add(new Clou_Report.Model.MemberForZJ()
                            {
                                StrZCBH = Myreader["AVR_ASSET_NO"].ToString().Trim(),
                                StrMeterID = Myreader["PK_LNG_METER_ID"].ToString().Trim(),
                                StrEquipment = Myreader["AVR_METER_NAME"].ToString().Trim(),
                                StrEquipmentType = Myreader["AVR_METER_TYPE"].ToString().Trim(),
                                StrEquipmentSize = Myreader["AVR_METER_MODEL"].ToString().Trim(),
                                StrFactory = Myreader["AVR_FACTORY"].ToString().Trim(),
                                StrCheckTime = CheckTime.ToString().Trim(),
                                StrResult = Myreader["AVR_TOTAL_CONCLUSION"].ToString().Trim(),


                            });
                        }
                        char[] csplit = { '|' };//总|尖|峰|平|谷|
                        string[] strParm = null;
                        foreach (Clou_Report.Model.MemberForZJ member in temp)
                        {
                            member.StrElectric = (Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit).Length > 0) ? Get_METER_COMMUNICATION("01701", member.StrMeterID).Split(csplit)[0] : "";
                        }
                        Myreader.Dispose();
                       
                    }
                    conn.Close();

                    return temp;
                }
            }
            catch
            {
                return temp;
            }
        }
       
         public  void UpdateToOracle(object o)
        {
            List<string > Lis_Id=(List<string>)o;
            UpdateToOracle(Lis_Id);
        }
    }
}
