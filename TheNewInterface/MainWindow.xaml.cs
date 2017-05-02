using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataCore;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace TheNewInterface
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = TheNewInterface.ViewModel.AllMeterInfo.CreateInstance();
           
        }
        Thread UpdateThread;
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        private void Btn_Config_Click(object sender, RoutedEventArgs e)
        {
            BasePage basepage = new BasePage();
            basepage.ShowDialog();
            ReLoadCheckTime();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_WorkNumList.Text.Trim() == "")
            {
                MessageBox.Show("请选择工单号！");
                return;
            }
            //OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text.ToString(), BaseConfigPath);
            if (MessageBox.Show("请确定你要上传的工作单为：" + cmb_WorkNumList.Text, "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text, BaseConfigPath);

            }
            else
            {
                return;
            }


            OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "07522300987", BaseConfigPath);

            int MeterCount = ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo.Count;
            List<string> UpDateMeterId=new List<string> ();
            for (int i = 0; i < MeterCount; i++)
            {
                if (ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup == true)
                {
                    UpDateMeterId.Add(ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].PK_LNG_METER_ID);
                }
            }
            if (UpDateMeterId.Count == 0)
            {
                MessageBox.Show("你没有选择要上传的表", "提示", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }
            this.UpdateProgress.Maximum = UpDateMeterId.Count;
            listBox_UpInfo.Items.Clear();
            UpDateInfomation upinfo = new UpDateInfomation();
            upinfo.Lis_PkId = UpDateMeterId;
            SoftType_G.csFunction s_function = new SoftType_G.csFunction();
            UpdateThread = new Thread(new ParameterizedThreadStart(UpdateToOracle));
            UpdateThread.Start(upinfo);
        }
        #region update
        public void UpdateToOracle(object o)
        {
            UpDateInfomation Lis_Id = o as UpDateInfomation;
            UpdateInfoThread(Lis_Id.Lis_PkId.Count, Lis_Id.Lis_PkId);
        }
        public void UpdateInfoThread(double countItem,List<string> lis_UP_ID)
        {
           
            double i = 0;
            int sleepTime = 100; ;
            double t;

            List<string> MeterUp_info = new List<string>();
            List<string> Seal_info = new List<string>();
            List<string> Demand_info = new List<string>();
            Mis_Interface_Driver.MisDriver cs_Function = null;
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                     cs_Function = new SoftType_G.csFunction();
                    break;
                case "CL3000S":
                     cs_Function = new SoftType_S.csFunction();
                    break;

            }
           // SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();
    
            foreach (MeterBaseInfoFactor temp in ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo)
            {
                if (temp.BolIfup == true)
                {
                    t = i + 1;
                    i = t < countItem ? t : countItem;
                    MeterUp_info.Clear();

                    MeterUp_info.Add("第" + temp.LNG_BENCH_POINT_NO.ToString() + "表位" + cs_Function.UpadataBaseInfo(temp.PK_LNG_METER_ID, out Seal_info));

                    #region Add SEAL
                    foreach (string temp_id in Seal_info)
                    {
                        MeterUp_info.Add("添加铅封：" + temp_id + "成功");
                    }
                    #endregion

                    MeterUp_info.Add(cs_Function.UpdataErrorInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataJKRJSWCInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataJKXLWCJLInfo(temp.PK_LNG_METER_ID, out Demand_info));

                    #region Add demand
                    foreach (string temp_id in Demand_info)
                    {
                        MeterUp_info.Add(temp_id);
                    }
                    #endregion

                    MeterUp_info.Add(cs_Function.UpdataSDTQWCJLInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataDNBSSJLInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataDNBZZJLInfo(temp.PK_LNG_METER_ID));

                    foreach (string temp_id in MeterUp_info)
                    {
                        listBox_UpInfo.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, double>(UpDateMeter), temp_id, i);
                        Thread.Sleep(sleepTime);
                    }

                }
              
                
                
            }
          
            MessageBox.Show("成功上传 :" + countItem + "个表");
            try
            {
                UpdateThread.Abort();
            }
            catch (Exception e)
            { }
            finally
            {
                if (listBox_UpInfo.Items.Count != 0)
                {
                    this.listBox_UpInfo.Dispatcher.Invoke(new Action(() =>
                    {

                        this.listBox_UpInfo.UpdateLayout();

                        this.listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
                    }));
                }
              

            }
           

        }

        private void UpDateMeter(string Meter_update_info, double progressCount)
        {

           

           SoftType_G.csFunction cs_G_Function=new SoftType_G.csFunction();

            Stopwatch watch = new Stopwatch();
            watch.Start();
                listBox_UpInfo.Items.Add(Meter_update_info);
         

            listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
                  
            this.UpdateProgress.Value = progressCount;
           // listBox_UpInfo.UpdateLayout();
            watch.Stop();
           // listBox_UpInfo.Items.Add("使用时间为：" + watch.ElapsedMilliseconds.ToString() + "毫秒");



        } 
        #endregion
        
        private void btn_download_Click(object sender, RoutedEventArgs e)
        {
            DownLoadPage downloadpage = new DownLoadPage();
            downloadpage.ShowDialog();
        }

        private void btn_deldteMis_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_WorkNumList.Text.Trim() == "")
            {
                MessageBox.Show("请选择工单号！");
                return;
            }
            //OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text.ToString(), BaseConfigPath);
            if (MessageBox.Show("请确定你要删除的工作单为：" + cmb_WorkNumList.Text, "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text, BaseConfigPath);

            }
            else
            {
                return;
            }


          //  OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "07522300987", BaseConfigPath);

            int MeterCount = ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo.Count;
            List<string> UpDateMeterId = new List<string>();
            for (int i = 0; i < MeterCount; i++)
            {
                if (ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup == true)
                {
                    UpDateMeterId.Add(ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].AVR_ASSET_NO);
                }
            }
            if (UpDateMeterId.Count == 0)
            {
                MessageBox.Show("你没有选择要删除的表", "提示", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }
            this.UpdateProgress.Maximum = UpDateMeterId.Count;
            listBox_UpInfo.Items.Clear();
            UpDateInfomation upinfo = new UpDateInfomation();
            upinfo.Lis_PkId = UpDateMeterId;
           
            UpdateThread = new Thread(new ParameterizedThreadStart(DeleteToOracle));
            UpdateThread.Start(upinfo);
        }
        public void DeleteToOracle(object o)
        {
            UpDateInfomation Lis_Id = o as UpDateInfomation;
            DeleteInfoThread(Lis_Id.Lis_PkId.Count, Lis_Id.Lis_PkId);
        }
        public void DeleteInfoThread(double countItem, List<string> lis_UP_ID)
        {

            double i = 0;
            int sleepTime = 150; ;
            double t;

            List<string> MeterUp_info = new List<string>();
            List<string> Seal_info = new List<string>();
            List<string> Demand_info = new List<string>();
            Mis_Interface_Driver.MisDriver cs_Function = null;
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                    cs_Function = new SoftType_G.csFunction();
                    break;
                case "CL3000S":
                    cs_Function = new SoftType_S.csFunction();
                    break;

            }
            // SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();

            foreach (MeterBaseInfoFactor temp in ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo)
            {
                if (temp.BolIfup == true)
                {
                    t = i + 1;
                    i = t < countItem ? t : countItem;
                    MeterUp_info.Clear();

                    
                    MeterUp_info.Add(cs_Function.DeleteMis(temp.AVR_ASSET_NO.Trim()));

                    foreach (string temp_id in MeterUp_info)
                    {
                        listBox_UpInfo.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, double>(UpDateMeter), temp_id, i);
                        Thread.Sleep(sleepTime);
                    }

                }



            }

            MessageBox.Show("成功删除 :" + countItem + "个表");
            try
            {
                UpdateThread.Abort();
            }
            catch (Exception e)
            { }
            finally
            {
                if (listBox_UpInfo.Items.Count != 0)
                {
                    this.listBox_UpInfo.Dispatcher.Invoke(new Action(() =>
                    {

                        this.listBox_UpInfo.UpdateLayout();

                        this.listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
                    }));
                }


            }


        }
        private void DeleteMeter(string Meter_update_info, double progressCount)
        {



            SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            listBox_UpInfo.Items.Add(Meter_update_info);


            listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);

            this.UpdateProgress.Value = progressCount;
            // listBox_UpInfo.UpdateLayout();
            watch.Stop();
            // listBox_UpInfo.Items.Add("使用时间为：" + watch.ElapsedMilliseconds.ToString() + "毫秒");



        } 
        private void btn_MisConfig_Click(object sender, RoutedEventArgs e)
        {
            OracleConfig oracleConfig = new OracleConfig();
            oracleConfig.ShowDialog();
        }

        private void cmb_CheckTime_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_CheckTime.Items.Clear();
            string strSection = "NewUser/CloumMIS/Item";
            string datapath = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            csPublicMember.str_DataPath = datapath;
            csPublicMember.strSoftType = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            csPublicMember.showInfo_less =(bool)chk_ShowLess.IsChecked;
            #region 软件类型判断
            switch (csPublicMember.strSoftType)
            { 
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                   csPublicMember.strCondition  = "datJdrq";
                   csPublicMember.strTableName = "meterinfo";
                    break;
                case "CL3000S":
                    csPublicMember.strCondition = "DTM_TEST_DATE";
                    csPublicMember.strTableName = "METER_INFO";
                    break;

            }

            #endregion
            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
        }
        private void ReLoadCheckTime()
        {
            cmb_CheckTime.Items.Clear();
            string strSection = "NewUser/CloumMIS/Item";
            string datapath = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            csPublicMember.str_DataPath = datapath;
            csPublicMember.strSoftType = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            csPublicMember.showInfo_less = (bool)chk_ShowLess.IsChecked;
            #region 软件类型判断
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                    csPublicMember.strCondition = "datJdrq";
                    csPublicMember.strTableName = "meterinfo";
                    break;
                case "CL3000S":
                    csPublicMember.strCondition = "DTM_TEST_DATE";
                    csPublicMember.strTableName = "METER_INFO";
                    break;

            }

            #endregion
            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
   
        }
        /// <summary>
        /// 加载检定日期
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="Condition"></param>
        /// <param name="TableName"></param>
        private void LoadCheckTime(string dataPath, string Condition, string TableName,bool IsLess)
        {
            List<string> TimeList = new List<string>();
            OperateData.PublicFunction PbFunction = new OperateData.PublicFunction();
            string Less_SQL = IsLess == true ? " DISTINCT TOP 20" : " DISTINCT";
            if (dataPath.Trim() == "")
            {
                return;
            }
            else
            {
                string Sql = string.Format("select  {3} {0} from {1} order by {2} desc", Condition, TableName, Condition,Less_SQL);
                TimeList = PbFunction.ExcuteAccess(Sql, Condition);
            }
            try
            {
                cmb_CheckTime.Items.Clear();
                if(!(TimeList.Count>0))
                {
                    MessageBox.Show("请配置数据库的正确路径（或当前数据库没有数据）！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    BasePage basepage = new BasePage();
                    basepage.ShowDialog();
                    ReLoadCheckTime();
                }
                foreach (string temp in TimeList)
                {
                    cmb_CheckTime.Items.Add(temp);
                }
            }
            catch (Exception exAddCheckTime)
            {

            }
        }

        private void chk_ShowLess_Click(object sender, RoutedEventArgs e)
        {
            csPublicMember.showInfo_less = (bool)chk_ShowLess.IsChecked;

            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
        }

        private void cmb_CheckTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string CheckTime = cmb_CheckTime.SelectedValue.ToString();
                string Sql = string.Format("Select  * from {0} where {1} =#{2}#", csPublicMember.strTableName, csPublicMember.strCondition, CheckTime);
                List<MeterBaseInfoFactor> tempBaseInfo = new List<MeterBaseInfoFactor>();
                ObservableCollection<MeterBaseInfoFactor> baseInfo = new ObservableCollection<MeterBaseInfoFactor>();
                OperateData.PublicFunction csFunction = new OperateData.PublicFunction();
                baseInfo = csFunction.GetBaseInfo(CheckTime, Sql, csPublicMember.strSoftType);

                ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo = baseInfo;

            }
            catch (Exception Ex)
            {
            }
            
         
        }

        private void chk_SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (chk_SelectAll.IsChecked == true)
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup = true;
                }
            }
            else
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup = false;
                }
            }
        }

    

        private void chk_Terminal_Click(object sender, RoutedEventArgs e)
        {

         
            if (chk_SelectAll.IsChecked == true)
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolTerminalWorkNum = true;
                }
            }
            else
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolTerminalWorkNum = false;
                }
            }
            loadMisWorkNum();
        }

        private void btn_testFunction_Click(object sender, RoutedEventArgs e)
        {
            //listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
        }

        private void cmb_WorkNumList_Loaded(object sender, RoutedEventArgs e)
        {
            loadMisWorkNum();
        }
        private void loadMisWorkNum()
        {
            string GetWorkSQL = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "WorkNumLoad", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
            if (chk_Terminal.IsChecked == true)
            {
                GetWorkSQL = string.Format(GetWorkSQL, "VT_SB_JKZDJCGZD");
            }
            else
            {
                GetWorkSQL = string.Format(GetWorkSQL, "vt_sb_jkdnbjdgzd");
            }
            List<string> WorkNumList=new List<string> ();
            OperateData.PublicFunction csfunction = new OperateData.PublicFunction();
            csfunction.GetSingleOracleData(GetWorkSQL, "GZDBH", out WorkNumList);
            cmb_WorkNumList.Items.Clear();
            if(!(WorkNumList.Count>0))
            {
                return;
            }
            foreach (string temp in WorkNumList)
            {
                cmb_WorkNumList.Items.Add(temp);
            }
        }

        private void btn_deldteAllMis_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_WorkNumList.Text.Trim() == "")
            {
                MessageBox.Show("请选择工单号！");
                return;
            }
            //OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text.ToString(), BaseConfigPath);
            if (MessageBox.Show("请确定你要删除的工作单为：" + cmb_WorkNumList.Text, "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text, BaseConfigPath);

            }
            else
            {
                return;
            }


          //  OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "07522300987", BaseConfigPath);

        
            this.UpdateProgress.Maximum = 1;
            listBox_UpInfo.Items.Clear();
            UpDateInfomation upinfo = new UpDateInfomation();
            UpdateThread = new Thread(new ParameterizedThreadStart(DeleteAllToOracle));
            UpdateThread.Start(upinfo);
        }
        public void DeleteAllToOracle(object o)
        {
            UpDateInfomation Lis_Id = o as UpDateInfomation;
            DeleteAllInfoThread();
        }
        public void DeleteAllInfoThread()
        {

            double i = 0;
            int sleepTime = 150; ;
            double t;

            List<string> MeterUp_info = new List<string>();
            List<string> Seal_info = new List<string>();
            List<string> Demand_info = new List<string>();
            Mis_Interface_Driver.MisDriver cs_Function = null;
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                    cs_Function = new SoftType_G.csFunction();
                    break;
                case "CL3000S":
                    cs_Function = new SoftType_S.csFunction();
                    break;

            }
            // SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();

            foreach (MeterBaseInfoFactor temp in ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo)
            {
                if (temp.BolIfup == true)
                {
                    t = i + 1;
                   
                    MeterUp_info.Clear();


                    MeterUp_info.Add(cs_Function.DeleteAllMis());

                    foreach (string temp_id in MeterUp_info)
                    {
                        listBox_UpInfo.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, double>(UpDateMeter), temp_id, i);
                        Thread.Sleep(sleepTime);
                    }

                }



            }

            MessageBox.Show("成功删除整张工作单");
            try
            {
                UpdateThread.Abort();
            }
            catch (Exception e)
            { }
            finally
            {
                if (listBox_UpInfo.Items.Count != 0)
                {
                    this.listBox_UpInfo.Dispatcher.Invoke(new Action(() =>
                    {

                        this.listBox_UpInfo.UpdateLayout();

                        this.listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
                    }));
                }


            }


        }

        private void DeleteAllMeter(string Meter_update_info, double progressCount)
        {



            SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            listBox_UpInfo.Items.Add(Meter_update_info);


            listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);

            this.UpdateProgress.Value = progressCount;
            // listBox_UpInfo.UpdateLayout();
            watch.Stop();
            // listBox_UpInfo.Items.Add("使用时间为：" + watch.ElapsedMilliseconds.ToString() + "毫秒");



        }

    
        #region Tab Excel
        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_OutPutExcel_Click(object sender, RoutedEventArgs e)
        {
            OperateOracle.csFunctionOracle function = new OperateOracle.csFunctionOracle();

            if (txt_MisZcbh.Text == "" || txt_MisZcbh.Text == null)
            {
                MessageBox.Show("资产编号为空，请输入", "提示");
                return;
            }
            string Message = "";
            Message = function.OutPutAllInfoToExcel(txt_MisZcbh.Text.ToString().Trim());
            MessageBox.Show(Message);

            // DataTable dtable = new DataTable();
            //dtable= function.GetZcbhTableLocal("B16018871A", "select * from meter_info where avr_asset_no = 'B16018871A' ");
            //dtable.Rows[0][0] = "12345679";

            //for (int i = 0; i < dtable.Columns.Count; i++)
            //{
            //    dtable.Columns[i].ColumnName = "嫦娥"+i.ToString();
            //}
            //    //foreach (DataColumn c in dtable.Columns)
            //    //{
            //    //    // Console.WriteLine(c.ColumnName);
            //    //    lis_Col.Items.Add(c.ColumnName);
            //    //} 
            //function.ExportEasy(dtable, @"C:\Users\screw\Desktop\12345\demo.xls");
            //MessageBox.Show("ok");
        }
        #endregion

       
    }

    public class UpDateInfomation
    {
        private List<string> lis_PkId;
        public List<string> Lis_PkId
        {
            get;
            set;
        }

    }
}
