using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

using Oracle.ManagedDataAccess.Client;
namespace OperateOracle
{
    public class operateData
    {
        //public const string fullPath = @"D:\clou_lee\showAccessData\showAccessData\DATA\ClouMeterData.mdb";
        //public const string ClouConfig = @"D:\clou_lee\showAccessData\showAccessData\DATA\ClouConfig.mdb";
        //public const string strCoon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPath + ";Persist Security Info=False";
        //public const string strCoon_config = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ClouConfig + ";Persist Security Info=False";
        //public const string StrOracleCoon = "User ID=PMS_JL;Password=1*oracle;Data Source=" +
        //                                   "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.150.41.101)(PORT=1522)))(CONNECT_DATA=(SERVISE_NAME=pmssc)))";
        public string StrOracleCoon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.150.41.101)(PORT=1522)))(CONNECT_DATA=(SERVICE_NAME= pmssc)));User Id=PMS_JL; Password=1*oracle";

        public operateData()
        {




            StrOracleCoon = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "OracleLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
 
        }
        public int GetOracleData(out List<string> OraTableList, out string stateOracle)
        {
            int restult = 0;
            try
            {
                List<string> listTableName = new List<string>();

                OracleConnection conn = new OracleConnection(StrOracleCoon);
                //if (conn.State == ConnectionState.Closed)
                conn.Open();
                stateOracle = conn.State.ToString();
                string sql = "select * from VT_SB_JKDNBJDJL";
                OracleCommand adp = new OracleCommand(sql, conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();

                while (myReader.Read())
                {

                    listTableName.Add(myReader["ZCBH"].ToString());
                }

                myReader.Dispose();
                conn.Close();
                OraTableList = listTableName;

            }
            catch (Exception e)
            {
                restult = -1;
                OraTableList = null;
                stateOracle = e.ToString();
            }
            return restult;
        }
        public int GetTableNameList(out List<string> TableList)
        {
            int result = 0;
            try
            {
                List<string> listTableName = new List<string>();
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "select * from dba_tables";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                while (myReader.Read())
                {
                    listTableName.Add(myReader["TABLE_NAME"].ToString());
                }
                TableList = listTableName;
                return result;
            }
            catch
            {
                TableList = null;
                return result;
            }
        }
        public int ShowTable(string TableName, out DataTable Dt_temp)
        {
            int result = 0;
            try
            {
                DataTable Dr = new DataTable();
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                string sql = "select * from " + TableName;
                DataTable dtFrom = new DataTable();
                OracleDataAdapter adp = new OracleDataAdapter(sql, Conn);

                adp.Fill(dtFrom);
                Conn.Close();
                Dr.Load(new DataTableReader(dtFrom));
                Dt_temp = Dr;

                DataTableMember.CreateInstance().Dt_temp = Dr;
                return result;
            }
            catch
            {
                Dt_temp = new DataTable();
                return result;
            }
        }
        public int GetTheTime(string FromName, string KeywordTpye, string Keyword, string Relation1, out List<DataTableMember> TempTableInfoList)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                #region
                string sql = "";
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                if (Keyword.Substring(0, 1) == "E" || Keyword.Substring(0, 1) == "F" || Keyword.Contains("ZP") || Keyword.Contains("ZF"))
                {

                    sql = "SELECT * FROM  Vt_Sb_Jkzdjcjl  WHERE " + KeywordTpye + " " + Relation1 + " '" + Keyword + "'";
                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    List<string> tempTimeList = new List<string>();

                    int count = 0;
                    while (myReader.Read())
                    {
                        count++;

                        tempInfoMeter.Add(new DataTableMember()
                        {
                            ID = count,
                            StrJlbh = myReader["ZCBH"].ToString(),
                            StrJdjl = myReader["JCJLDM"].ToString(),
                            StrGZDBH = myReader["GZDBH"].ToString(),
                            StrWD = myReader["WD"].ToString(),
                            StrSD = myReader["SD"].ToString(),
                            IntMeterNum = Convert.ToInt32(myReader["BW"]),
                            StrJdrq = myReader["HYRQ"].ToString(),
                            StrJyy = myReader["JDRYBH"].ToString(),
                            StrJddw = myReader["BZ"].ToString(),  //检定单位
                            StrHyy = myReader["HYRYBH"].ToString(),

                            //StrTestType = myReader["chrTestType"].ToString(),
                            //StrBlx = myReader["chrblx"].ToString(),
                            //StrBmc = myReader["chrBmc"].ToString(),
                            //StrUb = myReader["chrUb"].ToString(),
                            //StrIb = myReader["chrIb"].ToString(),
                            //StrMeterLevel = myReader["chrBdj"].ToString(),
                            //StrManufacture = myReader["chrZzcj"].ToString(),
                            //StrWcResult = myReader["chrJbwc"].ToString(),
                            //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                            //StrCodeSeal = myReader["chrQianFeng2"].ToString(),

                        });

                    }
                    myReader.Dispose();
                }
                else
                {
                    sql = "SELECT * FROM " + FromName + " WHERE " + KeywordTpye + " " + Relation1 + " '" + Keyword + "' ORDER BY HYRQ DESC";

                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    List<string> tempTimeList = new List<string>();

                    int count = 0;
                    while (myReader.Read())
                    {
                        count++;

                        tempInfoMeter.Add(new DataTableMember()
                        {
                            ID = count,
                            StrJlbh = myReader["ZCBH"].ToString(),
                            StrJdjl = myReader["JDJLDM"].ToString(),

                            StrWD = myReader["WD"].ToString(),
                            StrSD = myReader["SD"].ToString(),
                            IntMeterNum = Convert.ToInt32(myReader["BW"]),
                            StrJdrq = myReader["HYRQ"].ToString(),
                            StrJyy = myReader["JDRYBH"].ToString(),
                            StrJddw = myReader["BZ"].ToString(),  //检定单位
                            StrHyy = myReader["HYRYBH"].ToString(),
                            StrGZDBH = myReader["GZDBH"].ToString(),
                            //StrTestType = myReader["chrTestType"].ToString(),
                            //StrBlx = myReader["chrblx"].ToString(),
                            //StrBmc = myReader["chrBmc"].ToString(),
                            //StrUb = myReader["chrUb"].ToString(),
                            //StrIb = myReader["chrIb"].ToString(),
                            //StrMeterLevel = myReader["chrBdj"].ToString(),
                            //StrManufacture = myReader["chrZzcj"].ToString(),
                            //StrWcResult = myReader["chrJbwc"].ToString(),
                            //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                            //StrCodeSeal = myReader["chrQianFeng2"].ToString(),

                        });

                    }
                    myReader.Dispose();
                }

                #endregion


               
                Conn.Close();
                TempTableInfoList = tempInfoMeter;
                return result;

            }
            catch (Exception e)
            {

                TempTableInfoList = null;
                return -1;
            }

        }
       
        public int ShowTheConditionTable(string FromName, string ConditionShow, string endTime, string man, string Keyword, out List<DataTableMember> TempTableInfoList)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                if (Keyword.Substring(0, 1) == "E" || Keyword.Substring(0, 1) == "F" || Keyword.Contains("ZP") || Keyword.Contains("ZF"))
                {
                    //string sql = "SELECT * FROM Vt_Sb_Jkzdjcjl WHERE to_char(JDRQ,'yyyy/MM/dd HH24:MI:SS') BETWEEN '" + ConditionShow + "' AND '" + endTime + " ' AND JDRYBH = '" + man + "' ORDER BY JDRQ,to_number(BW)";
                    string sql = "SELECT * FROM  Vt_Sb_Jkzdjcjl WHERE HYRQ > to_date('" + ConditionShow + "','yyyy-mm-dd,hh24:mi:ss') AND HYRQ <to_date('" + endTime + "','yyyy-mm-dd,hh24:mi:ss') AND JDRYBH = '" + man + "'  ORDER BY HYRQ DESC,to_number(BW)";

                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;

                        tempInfoMeter.Add(new DataTableMember()
                        {
                            ID = count,

                            StrJlbh = myReader["ZCBH"].ToString(),
                            StrJdjl = myReader["JCJLDM"].ToString(),
                            StrGZDBH = myReader["GZDBH"].ToString(),
                            StrWD = myReader["WD"].ToString(),
                            StrSD = myReader["SD"].ToString(),
                            IntMeterNum = Convert.ToInt32(myReader["BW"]),
                            StrJdrq = myReader["JDRQ"].ToString(),
                            StrJyy = myReader["JDRYBH"].ToString(),
                            StrJddw = myReader["BZ"].ToString(),
                            StrHyy = myReader["HYRYBH"].ToString(),
                            StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                            // StrTestType=myReader["chrTestType"].ToString(),
                            // StrBlx=myReader["chrblx"].ToString(),
                            // StrBmc=myReader["chrBmc"].ToString(),
                            // StrUb=myReader["chrUb"].ToString(),
                            // StrIb=myReader["chrIb"].ToString(),
                            // StrMeterLevel=myReader["chrBdj"].ToString(),
                            // StrManufacture=myReader["chrZzcj"].ToString(),
                            //StrWcResult=myReader["chrJbwc"].ToString(),
                            //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                            //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                        });



                    }
                    myReader.Dispose();
                }
                else
                {
                    string sql = "SELECT * FROM " + FromName + " WHERE HYRQ > to_date('" + ConditionShow + "','yyyy-mm-dd,hh24:mi:ss') AND HYRQ <to_date('" + endTime + "','yyyy-mm-dd,hh24:mi:ss') AND JDRYBH = '" + man + "' ORDER BY to_number(BW) asc";
                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;

                        tempInfoMeter.Add(new DataTableMember()
                        {
                            ID = count,

                            StrJlbh = myReader["ZCBH"].ToString(),
                            StrJdjl = myReader["JDJLDM"].ToString(),
                            StrGZDBH = myReader["GZDBH"].ToString(),
                            StrWD = myReader["WD"].ToString(),
                            StrSD = myReader["SD"].ToString(),
                            IntMeterNum = Convert.ToInt32(myReader["BW"]),
                            StrJdrq = myReader["JDRQ"].ToString(),
                            StrJyy = myReader["JDRYBH"].ToString(),
                            StrJddw = myReader["BZ"].ToString(),
                            StrHyy = myReader["HYRYBH"].ToString(),
                            StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                            // StrTestType=myReader["chrTestType"].ToString(),
                            // StrBlx=myReader["chrblx"].ToString(),
                            // StrBmc=myReader["chrBmc"].ToString(),
                            // StrUb=myReader["chrUb"].ToString(),
                            // StrIb=myReader["chrIb"].ToString(),
                            // StrMeterLevel=myReader["chrBdj"].ToString(),
                            // StrManufacture=myReader["chrZzcj"].ToString(),
                            //StrWcResult=myReader["chrJbwc"].ToString(),
                            //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                            //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                        });

                    }
                    myReader.Dispose();
                }


               
                Conn.Close();
                TempTableInfoList = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                TempTableInfoList = null;
                return -1;
            }
        }

        public int ShowTheConditionTable(string FromName, string ConditionShow, string man, string Keyword, out List<DataTableMember> TempTableInfoList)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                if (Keyword.Substring(0, 1) == "E" || Keyword.Substring(0, 1) == "F" || Keyword.Contains("ZP") || Keyword.Contains("ZF"))
                {
                    //string sql = "SELECT * FROM Vt_Sb_Jkzdjcjl WHERE to_char(JDRQ,'yyyy/MM/dd HH24:MI:SS') BETWEEN '" + ConditionShow + "' AND '" + endTime + " ' AND JDRYBH = '" + man + "' ORDER BY JDRQ,to_number(BW)";
                    string sql = "SELECT * FROM  Vt_Sb_Jkzdjcjl WHERE ZCBH ='" + ConditionShow + "' ORDER BY JDRQ DESC";

                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;
                        if (count != 1)
                        {
                            tempInfoMeter.Add(new DataTableMember()
                            {
                                ID = count,

                                StrJlbh = myReader["ZCBH"].ToString(),
                                StrJdjl = myReader["JCJLDM"].ToString(),
                                StrGZDBH = myReader["GZDBH"].ToString(),
                                StrWD = myReader["WD"].ToString(),
                                StrSD = myReader["SD"].ToString(),
                                IntMeterNum = Convert.ToInt32(myReader["BW"]),
                                StrJdrq = myReader["JDRQ"].ToString(),
                                StrJyy = myReader["JDRYBH"].ToString(),
                                StrJddw = myReader["BZ"].ToString(),
                                StrHyy = myReader["HYRYBH"].ToString(),
                                StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                                // StrTestType=myReader["chrTestType"].ToString(),
                                // StrBlx=myReader["chrblx"].ToString(),
                                // StrBmc=myReader["chrBmc"].ToString(),
                                // StrUb=myReader["chrUb"].ToString(),
                                // StrIb=myReader["chrIb"].ToString(),
                                // StrMeterLevel=myReader["chrBdj"].ToString(),
                                // StrManufacture=myReader["chrZzcj"].ToString(),
                                //StrWcResult=myReader["chrJbwc"].ToString(),
                                //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                                //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                            });
                        }

                    }
                    myReader.Dispose();
                }
                else
                {
                    string sql = "SELECT * FROM " + FromName + "  WHERE ZCBH ='" + ConditionShow + "' ORDER BY JDRQ DESC ";
                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;
                        if (count != 1)
                        {
                            tempInfoMeter.Add(new DataTableMember()
                            {
                                ID = count,

                                StrJlbh = myReader["ZCBH"].ToString(),
                                StrJdjl = myReader["JDJLDM"].ToString(),
                                StrGZDBH = myReader["GZDBH"].ToString(),
                                StrWD = myReader["WD"].ToString(),
                                StrSD = myReader["SD"].ToString(),
                                IntMeterNum = Convert.ToInt32(myReader["BW"]),
                                StrJdrq = myReader["JDRQ"].ToString(),
                                StrJyy = myReader["JDRYBH"].ToString(),
                                StrJddw = myReader["BZ"].ToString(),
                                StrHyy = myReader["HYRYBH"].ToString(),
                                StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                                // StrTestType=myReader["chrTestType"].ToString(),
                                // StrBlx=myReader["chrblx"].ToString(),
                                // StrBmc=myReader["chrBmc"].ToString(),
                                // StrUb=myReader["chrUb"].ToString(),
                                // StrIb=myReader["chrIb"].ToString(),
                                // StrMeterLevel=myReader["chrBdj"].ToString(),
                                // StrManufacture=myReader["chrZzcj"].ToString(),
                                //StrWcResult=myReader["chrJbwc"].ToString(),
                                //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                                //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                            });
                        }


                    }
                    myReader.Dispose();
                }


               
                Conn.Close();
                TempTableInfoList = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                TempTableInfoList = null;
                return -1;
            }
        }

        public int ShowTheBasicErrorTable(string soleId, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {

                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKDNBJDWC WHERE ZCBH = '" + soleId + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrGLFXDM = myReader["GLFXDM"].ToString(),
                        StrGLYSDM = myReader["GLYSDM"].ToString(),
                        StrFZDLDM = myReader["FZDLDM"].ToString(),
                        StrXBDM = myReader["XBDM"].ToString(),
                        StrFZLXDM = myReader["FZLXDM"].ToString(),
                        StrFYDM = myReader["FYDM"].ToString(),
                        StrWC1 = myReader["WCZ1"].ToString(),
                        StrWC2 = myReader["WCZ2"].ToString(),
                        StrWC3 = myReader["WCZ3"].ToString(),
                        StrWC4 = myReader["WCZ4"].ToString(),
                        StrWC5 = myReader["WCZ5"].ToString(),
                        StrWCPJZ = myReader["WCPJZ"].ToString(),
                        StrXYZ = myReader["WCXYZ"].ToString(),
                        StrJLDM = myReader["JLDM"].ToString(),
                        StrWCCZ = myReader["WCCZ"].ToString(),
                        StrWCCZXYZ = myReader["WCCZXYZ"].ToString(),
                        StrDQMB = myReader["DQBM"].ToString(),

                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;

            }
            catch
            {
                Table_temp = null;
            }
            return result;
        }
        public int ShowTheBasicDayErrorTable(string soleID, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKDNBJDWC WHERE ZCBH = '" + soleID + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrCSZ1 = myReader["CSZ1"].ToString(),


                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheDayCalcTable(string soleID, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKRJSWC WHERE ZCBH = '" + soleID + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrCSZ1 = myReader["CSZ1"].ToString(),
                        StrCSZ2 = myReader["CSZ2"].ToString(),
                        StrCSZ3 = myReader["CSZ3"].ToString(),
                        StrCSZ4 = myReader["CSZ4"].ToString(),
                        StrCSZ5 = myReader["CSZ5"].ToString(),
                        StrPJZ = myReader["PJZ"].ToString(),
                        StrRJSDQBM = myReader["DQBM"].ToString(),

                    });

                }
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheNeedRecordTable(string soleID, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM vt_sb_jkxlwcjl WHERE ZCBH = '" + soleID + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrFZDLDM = myReader["FZDLDM"].ToString(),
                        StrBZZDXL = myReader["BZZDXL"].ToString(),
                        StrSJXL = myReader["SJXL"].ToString(),
                        StrWCZ = myReader["WCZ"].ToString(),
                        StrXLJLDM = myReader["JLDM"].ToString(),
                        StrXLDQBM = myReader["DQBM"].ToString(),



                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheTimeTQTable(string soleID, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {

                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKSDTQWCJL WHERE ZCBH = '" + soleID + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrTime = myReader["SD"].ToString(),
                        StrBZTQSJ = myReader["BZTQSJ"].ToString(),
                        StrSJTQSJ = myReader["SJTQSJ"].ToString(),
                        StrTQWC = myReader["TQWC"].ToString(),
                        StrTQDQBM = myReader["DQBM"].ToString(),

                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheDiaplayTable(string soleID, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKDNBSSJL WHERE ZCBH = '" + soleID + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrSSLXDM = myReader["SSLXDM"].ToString(),
                        StrBSS = myReader["BSS"].ToString(),
                        StrCBSJ = myReader["CBSJ"].ToString(),
                        StrSSDQBM = myReader["DQBM"].ToString(),


                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheRunWordTable(string soleID, string soleWorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKDNBZZJL WHERE ZCBH = '" + soleID + "' AND GZDBH = '" + soleWorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrZZSSLXDM = myReader["SSLXDM"].ToString(),
                        StrBZQQSS = myReader["BZQQSS"].ToString(),
                        StrBZQZSS = myReader["BZQZSS"].ToString(),
                        StrQSS = myReader["QSS"].ToString(),
                        StrZSS = myReader["ZSS"].ToString(),
                        StrZZWC = myReader["ZZWC"].ToString(),
                        StrZZDQBM = myReader["DQBM"].ToString(),


                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheLockTable(string soleID, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKFYBGJL WHERE ZCBH = '" + soleID + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrBGBZ = myReader["BGBZ"].ToString(),
                        StrFYZCBH = myReader["FYZCBH"].ToString(),
                        StrJFWZDM = myReader["JFWZDM"].ToString(),
                        StrJFSJ = myReader["JFSJ"].ToString(),
                        StrFYDQBM = myReader["DQBM"].ToString(),


                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }
        public int ShowTheLockTable(string soleID, string WorkNum, out List<DataTableMember> Table_temp)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(StrOracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = "SELECT * FROM VT_SB_JKFYBGJL WHERE ZCBH = '" + soleID + "' AND GZDBH ='" + WorkNum + "'";
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                while (myReader.Read())
                {
                    count++;

                    tempInfoMeter.Add(new DataTableMember()
                    {
                        ID = count,
                        StrBGBZ = myReader["BGBZ"].ToString(),
                        StrFYZCBH = myReader["FYZCBH"].ToString(),
                        StrJFWZDM = myReader["JFWZDM"].ToString(),
                        StrJFSJ = myReader["JFSJ"].ToString(),
                        StrFYDQBM = myReader["DQBM"].ToString(),


                    });

                }
                myReader.Dispose();
                Conn.Close();
                Table_temp = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                Table_temp = null;
                return -1;
            }
        }

        public static string  ExcuteOneWord(string Sql_str)
        {
            int result = 0;
            string Name_cj = "";
            string OracleCoon = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "OracleLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

            try
            {
                OracleConnection Conn = new OracleConnection(OracleCoon);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string sql = Sql_str;
                OracleCommand adp = new OracleCommand(sql, Conn);
                OracleDataReader myReader = null;
                myReader = adp.ExecuteReader();
                int count = 0;
              
                while (myReader.Read())
                {
                    count++;

                   Name_cj=myReader["SCCJMC"].ToString().Trim();

                }
                myReader.Dispose();
                Conn.Close();
                return Name_cj;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public int GetAllMeter(int MeterNum)
        {
            int result = 0;
            try
            {

                return result;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
    }
}
