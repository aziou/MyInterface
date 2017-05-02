using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
using System.IO;
using System.Collections;
namespace OperateOracle
{
    public class csFunctionOracle
    {
        public readonly string OracleLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "OracleLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public static readonly string AccessLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "AccessLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public Dictionary<string, string> BaseInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> ErrorInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> RjsInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> JKXKInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> JKSDTQInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> JKSSInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> JKZZInfoColumnName = new Dictionary<string, string>();
        public Dictionary<string, string> JKFYInfoColumnName = new Dictionary<string, string>();
        public int ShowTheConditionTable(string FromName, string ConditionShow, string man, string Keyword, out List<DataTableMember> TempTableInfoList)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(OracleLink);
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
                }



                TempTableInfoList = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                TempTableInfoList = null;
                return -1;
            }
        }
        public static string str_DQBM = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_CompanyNum", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public static string str_Path = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Report", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public DataTable GetZcbhTable(string Sql)
        {
            DataTable dt = new DataTable ();
            using (OracleConnection conn = new OracleConnection(OracleLink))
            {
                OracleCommand cmd = new OracleCommand(Sql, conn);
                OracleDataAdapter OracleDa = new OracleDataAdapter(Sql, conn);
                OracleDa.Fill(dt);
                dt.Dispose();
                return dt;
            }

        }

        public DataTable GetZcbhTableLocal(string MeterZcbh, string Sql)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(AccessLink))
            {
                OleDbCommand cmd = new OleDbCommand(Sql, conn);
                OleDbDataAdapter OracleDa = new OleDbDataAdapter(Sql, conn);
                OracleDa.Fill(dt);
                dt.Dispose();
                return dt;
            }

        }

        public  void ExportEasy(DataTable dtSource, string strFileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("基本信息");

            //填充表头   
            IRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dtSource.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }
             //填充内容   
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dtSource.Rows[i][j].ToString());
                }
            }
            for (int i = 0; i <= dtSource.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }


            //保存   
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
         
        }
         public static void ExportEasy(DataTable dtSource, string strFileName,string SheetName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            
            using (FileStream fs = File.Open(strFileName, FileMode.Open,
          FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了  
                workbook = new HSSFWorkbook(fs);
                fs.Close();
            }
            ISheet sheet = workbook.CreateSheet(SheetName);
            //填充表头   
            IRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dtSource.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }


            //填充内容   
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dtSource.Rows[i][j].ToString());
                }
            }
            for (int i = 0; i <= dtSource.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }


            //保存   
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
         
        }

        #region 导出OracleMis 中间库的信息到Excel
        public string OutPutAllInfoToExcel(string MeterZCBH)
         {

             try
             {

                 DataTable BaseInfo = new DataTable();
                 BaseInfo = MisBaseTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls");
                 BaseInfo = MisErrorTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "基本误差");
                 BaseInfo = MisRJSTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "日计时误差");
                 BaseInfo = MisXLTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "电能表需量信息");
                 BaseInfo = MisSDTQTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "时段投切数据");
                 BaseInfo = MisFYTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "封印信息");
                 BaseInfo = MisSSTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "电能表示数信息");
                 BaseInfo = MisZZTable(MeterZCBH);
                 ExportEasy(BaseInfo, str_Path + @"\" + MeterZCBH + @".xls", "电能表走字信息");
                 return "导出成功";

             }
             catch (Exception Excel_E)
             {
                 return "导出失败："+Excel_E.ToString();
             }
           
             return "";
        }
        private DataTable MisBaseTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql=string.Format("SELECT * FROM VT_SB_JKDNBJDJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (BaseInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = BaseInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }
                    
                }
            }
            catch (Exception BaseinfoE)
            { 
            
            }
            return dt;
        }
        private DataTable MisErrorTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKDNBJDWC WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (ErrorInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = ErrorInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = TransGLFX(dt.Rows[i][2].ToString().Trim());
                    dt.Rows[i][3] = Trans_GLYS(dt.Rows[i][3].ToString().Trim());
                    dt.Rows[i][4] = Trans_FZDL(dt.Rows[i][4].ToString().Trim());
                    dt.Rows[i][5] = Trans_XBDM(dt.Rows[i][5].ToString().Trim());
                    dt.Rows[i][6] = Trans_FZLX(dt.Rows[i][6].ToString().Trim());
                    dt.Rows[i][7] = Trans_FYDM(dt.Rows[i][7].ToString().Trim());

                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisRJSTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM vt_sb_jkrjswc WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (RjsInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = RjsInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisXLTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKXLWCJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (JKXKInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = JKXKInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = Trans_FZDL(dt.Rows[i][2].ToString().Trim());
                    

                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisSDTQTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKSDTQWCJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (JKSDTQInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = JKSDTQInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisFYTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKFYBGJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (JKFYInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = JKFYInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][4] = Trans_FYWZDM(dt.Rows[i][4].ToString().Trim());


                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisSSTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKDNBSSJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (JKSSInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = JKSSInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = Trans_SSLX(dt.Rows[i][2].ToString().Trim());


                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        private DataTable MisZZTable(string METERzcbh)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM VT_SB_JKDNBZZJL WHERE ZCBH='{0}' AND DQBM='{1}'", METERzcbh, str_DQBM);
            dt = GetZcbhTable(sql);
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (JKZZInfoColumnName.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns[i].ColumnName = JKZZInfoColumnName[dt.Columns[i].ColumnName.ToString().Trim()];
                    }

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = Trans_SSLX(dt.Rows[i][2].ToString().Trim());


                }
            }
            catch (Exception BaseinfoE)
            {

            }
            return dt;
        }
        #endregion 
        public csFunctionOracle()
        {
            #region 列头
            #region baseInfoCol
            BaseInfoColumnName.Add("GZDBH", "工作单编号");
            BaseInfoColumnName.Add("ZCBH", "资产编号");
            BaseInfoColumnName.Add("SJBZ", "首检标志");
            BaseInfoColumnName.Add("BW", "表位");
            BaseInfoColumnName.Add("WD", "温度");
            BaseInfoColumnName.Add("SD", "湿度");
            BaseInfoColumnName.Add("JDYJDM", "检定依据代码");
            BaseInfoColumnName.Add("JDJLDM", "检定结论代码");
            BaseInfoColumnName.Add("WGJCJLDM", "外观检查结论代码");
            BaseInfoColumnName.Add("WGBZJCJLDM", "外观标志检查结论代码");
            BaseInfoColumnName.Add("YQJJCJLDM", "元器件检查结论代码");
            BaseInfoColumnName.Add("JYXNSYJLDM", "绝缘性能试验结论代码");
            BaseInfoColumnName.Add("MCDYSYJLDM", "脉冲电压试验结论代码");
            BaseInfoColumnName.Add("JLDYSYJLDM", "交流电压试验结论代码");
            BaseInfoColumnName.Add("ZQDYQSYJLDM", "准确度要求试验结论代码");
            BaseInfoColumnName.Add("CSSYJLDM", "常数试验结论代码");
            BaseInfoColumnName.Add("QDDL", "起动电流");
            BaseInfoColumnName.Add("QDSYJLDM", "起动试验结论代码");
            BaseInfoColumnName.Add("QDSYDYZ", "潜动试验电压值");
            BaseInfoColumnName.Add("FQDLZ", "防潜电流值");
            BaseInfoColumnName.Add("QISYJLDM", "潜动试验结论代码");
            BaseInfoColumnName.Add("JBWCSYJLDM", "基本误差试验结论代码");
            BaseInfoColumnName.Add("RJSWCSYJLDM", "日计时误差试验结论代码");
            BaseInfoColumnName.Add("RJSWCZ", "日计时误差值");
            BaseInfoColumnName.Add("JDQZDNSZWCSYJLDM", "计度器总电能示值误差试验结论代码");
            BaseInfoColumnName.Add("FLSDDNSSWCSYJLDM", "费率时段电能示数误差试验结论代码");
            BaseInfoColumnName.Add("ZDXLWCSYJLDM", "最大需量误差试验结论代码");
            BaseInfoColumnName.Add("DQYQSYJLDM", "电气要求试验结论代码");
            BaseInfoColumnName.Add("GLXHSYJLDM", "功率消耗试验结论代码");
            BaseInfoColumnName.Add("DYDYYXSYJLDM", "电源电压影响试验结论代码");
            BaseInfoColumnName.Add("DYFWSYJLDM", "电压范围试验结论代码");
            BaseInfoColumnName.Add("DYZJHDSZDYXSYJLDM", "电压暂降或短时中断影响试验结论代码");
            BaseInfoColumnName.Add("DYDSZDDSZDYXSYJLDM", "电压短时中断对时钟的影响试验结论代码");
            BaseInfoColumnName.Add("DYCSJZDDSZYXSYJLDM", "电压长时间中断对时钟影响试验结论代码");
            BaseInfoColumnName.Add("DYCSJZDDDNBYXSYJLDM", "电压长时间中断对电能表影响试验结论代码");
            BaseInfoColumnName.Add("DYHZLDYTSZDYXSYJLDM", "电压和直流电源同时中断对电能表程序和存贮数据的影响试验结论代码");
            BaseInfoColumnName.Add("GNJCJLDM", "功能检查结论代码");
            BaseInfoColumnName.Add("DNJLGNJCJLDM", "电能计量功能检查结论代码");
            BaseInfoColumnName.Add("DLDJGNJCJLDM", "电量冻结功能检查结论代码");
            BaseInfoColumnName.Add("ZDXLGNJCJLDM", "最大需量功能检查结论代码");
            BaseInfoColumnName.Add("FLHSDGNJCJLDM", "费率和时段功能检查结论代码");
            BaseInfoColumnName.Add("SJJLGNJCJLDM", "事件记录功能检查结论代码");
            BaseInfoColumnName.Add("MCSCGNJCJLDM", "脉冲输出功能检查结论代码");
            BaseInfoColumnName.Add("XSGNJCJLDM", "显示功能检查结论代码");
            BaseInfoColumnName.Add("YZNRJCJLDM", "预置内容检查结论代码");
            BaseInfoColumnName.Add("AQFHGNJCJLDM", "安全防护功能检查结论代码");
            BaseInfoColumnName.Add("TDDGNJCJLDM", "通断电功能检查结论代码");
            BaseInfoColumnName.Add("TXGNJCJLDM", "通信功能检查结论代码");
            BaseInfoColumnName.Add("TXGYYZXJCJLDM", "通信规约一致性检查结论代码");
            BaseInfoColumnName.Add("SJCSXKGRSYJLDM", "数据传输线抗干扰试验结论代码");
            BaseInfoColumnName.Add("YZXSYJLDM", "一致性试验结论代码");
            BaseInfoColumnName.Add("WCBCSYJLDM", "误差变差试验结论代码");
            BaseInfoColumnName.Add("WCYZXSYJLDM", "误差一致性试验结论代码");
            BaseInfoColumnName.Add("FZDLSYBCSYJLDM", "负载电流升降变差试验结论代码");
            BaseInfoColumnName.Add("SDTQWCSYJLDM", "时段投切误差试验结论代码");
            BaseInfoColumnName.Add("XLZQWCSYJLDM", "需量周期误差试验结论代码");
            BaseInfoColumnName.Add("DNCLBZPCGJZ", "电能测量标准偏差估计值");
            BaseInfoColumnName.Add("GPSDSCZ", "GPS对时差值");
            BaseInfoColumnName.Add("GPSDSJLDM", "GPS对时结论代码");
            BaseInfoColumnName.Add("JDRYBH", "检定员编号");
            BaseInfoColumnName.Add("JDRQ", "检定日期");
            BaseInfoColumnName.Add("HYRYBH", "核验员编号");
            BaseInfoColumnName.Add("HYRQ", "核验日期");
            BaseInfoColumnName.Add("DQBM", "地区编码");
          
            BaseInfoColumnName.Add("BZ", "检定情况说明");
            BaseInfoColumnName.Add("BZZZZCBH", "电能计量标准设备资产编号");

            #endregion

            #region Error
            ErrorInfoColumnName.Add("GZDBH", "工作单编号");
            ErrorInfoColumnName.Add("ZCBH", "资产编号");
            ErrorInfoColumnName.Add("GLFXDM", "功率方向代码");
            ErrorInfoColumnName.Add("GLYSDM", "功率因数代码");
            ErrorInfoColumnName.Add("FZDLDM", "负载电流代码");
            ErrorInfoColumnName.Add("XBDM", "相别代码");
            ErrorInfoColumnName.Add("FZLXDM", "负载类型代码");
            ErrorInfoColumnName.Add("FYDM", "分元代码");
            ErrorInfoColumnName.Add("WCZ1", "误差1");
            ErrorInfoColumnName.Add("WCZ2", "误差2");
            ErrorInfoColumnName.Add("WCZ3", "误差3");
            ErrorInfoColumnName.Add("WCZ4", "误差4");
            ErrorInfoColumnName.Add("WCZ5", "误差5");
            ErrorInfoColumnName.Add("WCPJZ", "误差平均值");
            ErrorInfoColumnName.Add("WCXYZ", "误差修约值");
            ErrorInfoColumnName.Add("JLDM", "结论代码");
            ErrorInfoColumnName.Add("WCCZ", "不平衡负载与平衡负载的误差差值");
            ErrorInfoColumnName.Add("WCCZXYZ", "误差差值修约值");
            ErrorInfoColumnName.Add("DQBM", "地区编码");

            #endregion

            #region Rjs
            RjsInfoColumnName.Add("GZDBH", "工作单编号");
            RjsInfoColumnName.Add("ZCBH", "资产编号");
            RjsInfoColumnName.Add("CSZ1", "测试值1");
            RjsInfoColumnName.Add("CSZ2", "测试值2");
            RjsInfoColumnName.Add("CSZ3", "测试值3");
            RjsInfoColumnName.Add("CSZ4", "测试值4");
            RjsInfoColumnName.Add("CSZ5", "测试值5");
            RjsInfoColumnName.Add("PJZ", "平均值");
            RjsInfoColumnName.Add("DQBM", "地区编码");

            #endregion

            #region 需量
            JKXKInfoColumnName.Add("GZDBH", "工作单编号");
            JKXKInfoColumnName.Add("ZCBH", "资产编号");
            JKXKInfoColumnName.Add("FZDLDM", "负载电流代码");
            JKXKInfoColumnName.Add("BZZDXL", "标准最大需量");
            JKXKInfoColumnName.Add("SJXL", "实际需量");
            JKXKInfoColumnName.Add("WCZ", "误差值");
            JKXKInfoColumnName.Add("JLDM", "结论代码");
            JKXKInfoColumnName.Add("DQBM", "地区编码");
            #endregion

            #region 时段投切
            JKSDTQInfoColumnName.Add("GZDBH", "工作单编号");
            JKSDTQInfoColumnName.Add("ZCBH", "资产编号");
            JKSDTQInfoColumnName.Add("SD", "时段");
            JKSDTQInfoColumnName.Add("BZTQSJ", "标准投切时间");
            JKSDTQInfoColumnName.Add("SJTQSJ", "实际投切时间");
            JKSDTQInfoColumnName.Add("TQWC", "投切误差");
            JKSDTQInfoColumnName.Add("DQBM", "地区编码");
            #endregion

            #region 示数
            JKSSInfoColumnName.Add("GZDBH", "工作单编号");
            JKSSInfoColumnName.Add("ZCBH", "资产编号");
            JKSSInfoColumnName.Add("SSLXDM", "示数类型代码");
            JKSSInfoColumnName.Add("BSS", "表示数");
            JKSSInfoColumnName.Add("CBRQ", "抄表日期");
            JKSSInfoColumnName.Add("CBSJ", "抄表时间");
            JKSSInfoColumnName.Add("DQBM", "地区编码");
            #endregion

            #region 走字
            JKZZInfoColumnName.Add("GZDBH", "工作单编号");
            JKZZInfoColumnName.Add("ZCBH", "资产编号");
            JKZZInfoColumnName.Add("SSLXDM", "示数类型代码");
            JKZZInfoColumnName.Add("BZQQSS", "标准器起示数");
            JKZZInfoColumnName.Add("BZQZSS", "标准器止示数");
            JKZZInfoColumnName.Add("QSS", "起示数");
            JKZZInfoColumnName.Add("ZSS", "止示数");
            JKZZInfoColumnName.Add("ZZWC", "走字误差");
            JKZZInfoColumnName.Add("DQBM", "地区编码");
            #endregion

            #region 封印
            JKFYInfoColumnName.Add("GZDBH", "工作单编号");
            JKFYInfoColumnName.Add("ZCBH", "资产编号");
            JKFYInfoColumnName.Add("BGBZ", "变更标识");
            JKFYInfoColumnName.Add("FYZCBH", "封印资产编号");
            JKFYInfoColumnName.Add("JFWZDM", "加封位置代码");
            JKFYInfoColumnName.Add("JFSJ", "加封时间");
            JKFYInfoColumnName.Add("DQBM", "地区编码");
            #endregion
            #endregion
        }
        #region 转译代码
        private string TransGLFX(string origrial)
        {
            switch (origrial)
            {
                case "1":
                    origrial = "正向有功";
                    break;
                case "2":
                    origrial = "正向无功";
                    break;
                case "3":
                    origrial = "反向有功";
                    break;
                case "4":
                    origrial = "反向无功";
                    break;
                default:

                    break;
            }
            return origrial;
        }
        private string Trans_GLYS(string origrial)
        {
            switch (origrial)
            {
                case "1":
                    origrial = "0.5(L)";
                    break;
                case "2":
                    origrial = "1";
                    break;
                case "3":
                    origrial = "0.8(c)";
                    break;
                default:

                    break;
            }
            return origrial;
        }
        private string Trans_FYDM(string origrial)
        {
            switch (origrial)
            {
                case "01":
                    origrial = "合元";
                    break;
                case "02":
                    origrial = "A相";
                    break;
                case "03":
                    origrial = "B相";
                    break;
                case "04":
                    origrial = "C相";
                    break;
                default:

                    break;

            }
            return origrial;
        }
        private string Trans_XBDM(string origrial)
        {
            switch (origrial)
            {
                case "01":
                    origrial = "单相";
                    break;
                case "02":
                    origrial = "三相";
                    break;
                default:

                    break;

            }
            return origrial;
        }
        private string Trans_FYWZDM(string origrial)
        {
            switch (origrial)
            {
                case "01":
                    origrial = "左耳封";
                    break;
                case "02":
                    origrial = "右耳封";
                    break;
                case "05":
                    origrial = "表盖封印";
                    break;
                case "07":
                    origrial = "编程小门";
                    break;
                
                default:

                    break;

            }
            return origrial;
        }
        private string Trans_FZLX(string origrial)
        {
            switch (origrial)
            {
                case "1":
                    origrial = "平衡负载";
                    break;
                case "2":
                    origrial = "不平衡负载A相";
                    break;
                case "3":
                    origrial = "不平衡负载B相";
                    break;
                case "4":
                    origrial = "不平衡负载C相";
                    break;
                default:

                    break;

            }
            return origrial;
        }
        private string Trans_FZDL(string origrial)
        {
            switch (origrial)
            {
                case "01":
                    origrial = "0.05Ib";
                    break;

                case "02":
                    origrial = "0.1Ib";
                    break;

                case "05":
                    origrial = "Ib";
                    break;

                case "06":
                    origrial = "Imax";
                    break;

                case "07":
                    origrial = "0.5Imax";
                    break;

                case "11":
                    origrial = "0.02Ib";
                    break;

                case "12":
                    origrial = "0.01Ib";
                    break;
                case "15":
                    origrial = "0.03Ib";
                    break;
                default:
                    break;
            }
            return origrial;
        }
        private string Trans_SSLX(string origrial)
        {
            switch (origrial)
            {
                case "121":
                    origrial = "正有功总";
                    break;

                case "123":
                    origrial = "正有功峰";
                    break;

                case "124":
                    origrial = "正有功平";
                    break;

                case "125":
                    origrial = "正有功谷";
                    break;

                case "131":
                    origrial = "正无功总";
                    break;

                case "133":
                    origrial = "正无功峰";
                    break;

                case "134":
                    origrial = "正无功谷";
                    break;

                case "135":
                    origrial = "正无功平";
                    break;

                case "221":
                    origrial = "反有功总";
                    break;

                case "222":
                    origrial = "反有功峰";
                    break;

                case "223":
                    origrial = "反有功平";
                    break;

                case "224":
                    origrial = "反有功谷";
                    break;

                case "231":
                    origrial = "反无功总";
                    break;

                case "236":
                    origrial = "反无功峰";
                    break;

                case "237":
                    origrial = "反无功谷";
                    break;

                case "238":
                    origrial = "反无功平";
                    break;
                default:
                    break;

            }
            return origrial;
        }
        private string Trans_ZZFX(string origrial)
        {
            switch (origrial)
            {
                case "121":
                    origrial = "正有功总";
                    break;

                case "123":
                    origrial = "正有功峰";
                    break;

                case "124":
                    origrial = "正有功平";
                    break;

                case "125":
                    origrial = "正有功谷";
                    break;

                case "131":
                    origrial = "正无功总";
                    break;

                case "133":
                    origrial = "正无功峰";
                    break;

                case "134":
                    origrial = "正无功谷";
                    break;

                case "135":
                    origrial = "正无功平";
                    break;

                case "221":
                    origrial = "反有功总";
                    break;

                case "222":
                    origrial = "反有功峰";
                    break;

                case "223":
                    origrial = "反有功平";
                    break;

                case "224":
                    origrial = "反有功谷";
                    break;

                case "231":
                    origrial = "反无功总";
                    break;

                case "236":
                    origrial = "反无功峰";
                    break;

                case "237":
                    origrial = "反无功谷";
                    break;

                case "238":
                    origrial = "反无功平";
                    break;
                default:
                    break;

            }
            return origrial;
        }
        #endregion
       
    }
}
