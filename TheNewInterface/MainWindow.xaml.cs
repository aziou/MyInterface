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
using TheNewInterface.ViewModel;
using OperateOracle;
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
        public string TableName;
        Thread UpdateThread;
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        #region Tab 001
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


            //OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "07522300987", BaseConfigPath);

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
            if (cmb_Condition.SelectedIndex == 0)
            {
                csPublicMember.showInfo_less = (bool)chk_ShowLess.IsChecked;

                LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
   
            }

         }

        private void cmb_CheckTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Condition.SelectedIndex == 0)
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
        private void cmb_Condition_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_Condition.Items.Add("检定时间:");
            cmb_Condition.Items.Add("资产编号:");

            cmb_Condition.SelectedIndex = 0;


        }

        private void cmb_Condition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            switch (cmb_Condition.SelectedIndex)
            {
                case 0:
                    ReLoadCheckTime();
                    break;
                case 1:
                    cmb_CheckTime.Items.Clear();
                    break;
                default:
                    break;
            }
        }

        private void btn_FindMeter_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Condition.SelectedIndex == 1)
            {
                if (cmb_CheckTime.Text == "")
                {
                    MessageBox.Show("请输入你要查询的资产编号！");
                    return;
                }
                #region 软件类型判断
                switch (csPublicMember.strSoftType)
                {
                    case "CL3000G":
                    case "CL3000F":
                    case "CL3000DV80":
                        csPublicMember.strCondition = "chrjlbh";
                        csPublicMember.strTableName = "meterinfo";
                        break;
                    case "CL3000S":
                        csPublicMember.strCondition = "AVR_ASSET_NO";
                        csPublicMember.strTableName = "METER_INFO";
                        break;

                }

                #endregion
                try
                {

                    string Sql = string.Format("Select  * from {0} where {1} ='{2}'", csPublicMember.strTableName, csPublicMember.strCondition, cmb_CheckTime.Text);
                    List<MeterBaseInfoFactor> tempBaseInfo = new List<MeterBaseInfoFactor>();
                    ObservableCollection<MeterBaseInfoFactor> baseInfo = new ObservableCollection<MeterBaseInfoFactor>();
                    OperateData.PublicFunction csFunction = new OperateData.PublicFunction();
                    baseInfo = csFunction.GetBaseInfo("", Sql, csPublicMember.strSoftType);

                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo = baseInfo;

                }
                catch (Exception Ex)
                {
                }
            }
        }
        #endregion

        #region 删除中间库
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
        #endregion

        #region Tab Excel
        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            #region 查询中间库的数据

            if (txt_MisZcbh.Text.Trim() == "")
            {
                MessageBox.Show("请输入资产编号！", "提示", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            #endregion
        }

        public void showTheTable(string keyWord)
        {
            try
            {
                ViewMember.CreateInstance().IfRunTheItemChanged = 1;
                operateData operate = new operateData();
                ObservableCollection<MeterInfoItem> MeterList = new ObservableCollection<MeterInfoItem>();
                string ZcbhWord = keyWord;
                if (keyWord.Contains("ZP") || keyWord.Contains("ZF"))
                {
                    TableName = "VT_SB_JKZDJCJL";
                }
                #region

                #endregion


                List<DataTableMember> tempTableMember = new List<DataTableMember>();
                string KeyWordType = "ZCBH";
                List<string> lis_TimeHead = new List<string>();
                List<string> lis_TimeEnd = new List<string>();
                List<string> lis_ReZcbh = new List<string>();
                int LineNum = 0;
                operate.GetTheTime(TableName, KeyWordType, ZcbhWord, "=", out tempTableMember);

                for (int meterNum = 0; meterNum < 1; meterNum++)
                {
                    string TimeHead = (Convert.ToDateTime(tempTableMember[meterNum].StrJdrq).AddMinutes(-1d)).ToString("yyyy/MM/dd HH:mm:ss");
                    lis_TimeHead.Add(TimeHead);
                    ViewMember.CreateInstance().ThisMeterWorkNum = tempTableMember[meterNum].StrGZDBH.ToString();
                    string TimeEnd = (Convert.ToDateTime(tempTableMember[meterNum].StrJdrq).AddMinutes(1d)).ToString("yyyy/MM/dd HH:mm:ss");
                    lis_TimeEnd.Add(TimeEnd);
                    ViewMember.CreateInstance().CheckTime = tempTableMember[meterNum].StrJdrq;
                    ViewMember.CreateInstance().Operater = tempTableMember[meterNum].StrJyy;

                }
                # region 获取基本信息
                for (int TimeListNum = 0; TimeListNum < lis_TimeHead.Count; TimeListNum++)
                {
                    if (operate.ShowTheConditionTable(TableName, lis_TimeHead[TimeListNum], lis_TimeEnd[TimeListNum], ViewMember.CreateInstance().Operater, keyWord, out tempTableMember) == 0)
                    {
                        operate.ShowTheConditionTable(TableName, lis_TimeHead[TimeListNum], lis_TimeEnd[TimeListNum], ViewMember.CreateInstance().Operater, keyWord, out tempTableMember);

                        int currentMeterNum;
                        int currentIndex = 0;

                        for (int i = 0; i < tempTableMember.Count; i++)
                        {

                            if (tempTableMember[i].StrJlbh == ZcbhWord)
                            {
                                currentMeterNum = Convert.ToInt16(tempTableMember[i].IntMeterNum);
                                currentIndex = i;
                                break;
                            }
                        }
                        int FirstIndex = 0;
                        int LastIndex = currentIndex;
                        for (int i = currentIndex; i < tempTableMember.Count - 1; i++)
                        {
                            if (tempTableMember[i + 1].IntMeterNum < tempTableMember[i].IntMeterNum)
                            {
                                LastIndex = i;
                                break;
                            }
                            else
                            {
                                LastIndex = tempTableMember.Count - 1;
                            }
                        }
                        for (int i = currentIndex; i > 0; i--)
                        {
                            if (tempTableMember[i].IntMeterNum < tempTableMember[i - 1].IntMeterNum)
                            {
                                FirstIndex = i;
                                break;
                            }
                        }


                        #region 代码转中文
                        for (int j = FirstIndex; j <= LastIndex; j++)
                        {
                            if (tempTableMember[j].StrJddw == "")
                            {
                                tempTableMember[j].StrJddw = "深圳供电局有限公司";
                            }
                            switch (tempTableMember[j].StrJyy)
                            {
                                case "8E0257BE35ED1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrJyy = "陈佩茹";
                                    break;
                                case "acb7d25b68a24e8fbee725b0149e8a96":
                                    tempTableMember[j].StrJyy = "安琪儿";
                                    break;
                                case "7FD9789A0A2649D195CA49A441DE28EE":
                                    tempTableMember[j].StrJyy = "陈曦彤";
                                    break;
                                case "8E0257BE31911489E0440018FE2DCF1D":
                                    tempTableMember[j].StrJyy = "陈雯丽";
                                    break;
                                case "8E0257BE278F1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrJyy = "郭兴林";
                                    break;
                                case "yx_hexiaojun":
                                    tempTableMember[j].StrJyy = "何晓军";
                                    break;
                                case "yx_jiangqinghong":
                                    tempTableMember[j].StrJyy = "江庆洪";
                                    break;
                                case "yx_laishengsi":
                                    tempTableMember[j].StrJyy = "赖盛斯";
                                    break;
                                case "yx_lijinxia":
                                    tempTableMember[j].StrJyy = "李金霞";
                                    break;
                                case "yx_limi":
                                    tempTableMember[j].StrJyy = "李密";
                                    break;
                                case "yx_lina":
                                    tempTableMember[j].StrJyy = "李娜";
                                    break;
                                case "8E0257BE2F6B1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrJyy = "刘岚";
                                    break;
                                case "yx_wangshaosi":
                                    tempTableMember[j].StrJyy = "王少思";
                                    break;
                                case "yx_wangzhenglin":
                                    tempTableMember[j].StrJyy = "王正林";
                                    break;
                                case "9E79AE1983D84C0E83D3E0DC49977F38":
                                    tempTableMember[j].StrJyy = "王子慧";
                                    break;
                                case "yx_weichunyan":
                                    tempTableMember[j].StrJyy = "韦春艳";
                                    break;
                                case "09C368A980B041AEA98999BF9C6B80E0":
                                    tempTableMember[j].StrJyy = "杨立瑾";
                                    break;
                                case "yx_yangxuechong":
                                    tempTableMember[j].StrJyy = "杨薛冲";
                                    break;
                                case "yx_zhangjun":
                                    tempTableMember[j].StrJyy = "张军";
                                    break;
                                case "227EA885C6BF4F46918308254012ED1E":
                                    tempTableMember[j].StrJyy = "张小燕";
                                    break;
                                case "yx_zhangzhen":
                                    tempTableMember[j].StrJyy = "张珍";
                                    break;
                                case "BC0D3C2FC3F94746A730FEC04A71B898":
                                    tempTableMember[j].StrJyy = "朱玲";
                                    break;
                                case "8E0257BE2F091489E0440018FE2DCF1D":
                                    tempTableMember[j].StrJyy = "朱新涛";
                                    break;
                                case "yx_qinhaishan":
                                    tempTableMember[j].StrJyy = "覃海山";
                                    break;
                                default:
                                    break;

                            }
                            switch (tempTableMember[j].StrHyy)
                            {
                                case "8E0257BE35ED1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrHyy = "陈佩茹";
                                    break;
                                case "acb7d25b68a24e8fbee725b0149e8a96":
                                    tempTableMember[j].StrHyy = "安琪儿";
                                    break;
                                case "7FD9789A0A2649D195CA49A441DE28EE":
                                    tempTableMember[j].StrHyy = "陈曦彤";
                                    break;
                                case "8E0257BE31911489E0440018FE2DCF1D":
                                    tempTableMember[j].StrHyy = "陈雯丽";
                                    break;
                                case "8E0257BE278F1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrHyy = "郭兴林";
                                    break;
                                case "yx_hexiaojun":
                                    tempTableMember[j].StrHyy = "何晓军";
                                    break;
                                case "yx_jiangqinghong":
                                    tempTableMember[j].StrHyy = "江庆洪";
                                    break;
                                case "yx_laishengsi":
                                    tempTableMember[j].StrHyy = "赖盛斯";
                                    break;
                                case "yx_lijinxia":
                                    tempTableMember[j].StrHyy = "李金霞";
                                    break;
                                case "yx_limi":
                                    tempTableMember[j].StrHyy = "李密";
                                    break;
                                case "yx_lina":
                                    tempTableMember[j].StrHyy = "李娜";
                                    break;
                                case "8E0257BE2F6B1489E0440018FE2DCF1D":
                                    tempTableMember[j].StrHyy = "刘岚";
                                    break;
                                case "yx_wangshaosi":
                                    tempTableMember[j].StrHyy = "王少思";
                                    break;
                                case "yx_wangzhenglin":
                                    tempTableMember[j].StrHyy = "王正林";
                                    break;
                                case "9E79AE1983D84C0E83D3E0DC49977F38":
                                    tempTableMember[j].StrHyy = "王子慧";
                                    break;
                                case "yx_weichunyan":
                                    tempTableMember[j].StrHyy = "韦春艳";
                                    break;
                                case "09C368A980B041AEA98999BF9C6B80E0":
                                    tempTableMember[j].StrHyy = "杨立瑾";
                                    break;
                                case "yx_yangxuechong":
                                    tempTableMember[j].StrHyy = "杨薛冲";
                                    break;
                                case "yx_zhangjun":
                                    tempTableMember[j].StrHyy = "张军";
                                    break;
                                case "227EA885C6BF4F46918308254012ED1E":
                                    tempTableMember[j].StrHyy = "张小燕";
                                    break;
                                case "yx_zhangzhen":
                                    tempTableMember[j].StrHyy = "张珍";
                                    break;
                                case "BC0D3C2FC3F94746A730FEC04A71B898":
                                    tempTableMember[j].StrHyy = "朱玲";
                                    break;
                                case "8E0257BE2F091489E0440018FE2DCF1D":
                                    tempTableMember[j].StrHyy = "朱新涛";
                                    break;
                                case "yx_qinhaishan":
                                    tempTableMember[j].StrHyy = "覃海山";
                                    break;
                                default:
                                    break;

                            }

                        }
                        #endregion

                        for (int i = FirstIndex; i <= LastIndex; i++)
                        {
                            LineNum++;
                            lis_ReZcbh.Add(tempTableMember[i].StrJlbh.ToString());
                            MeterList.Add(new MeterInfoItem()
                            {
                                ID = LineNum,
                                StrBZZZZCBH = tempTableMember[i].StrBZZZZCBH,
                                StrJlbh = tempTableMember[i].StrJlbh,
                                StrJdjl = tempTableMember[i].StrJdjl,
                                StrGZDBH = tempTableMember[i].StrGZDBH,
                                StrWD = tempTableMember[i].StrWD,
                                StrSD = tempTableMember[i].StrSD,
                                IntMeterNum = tempTableMember[i].IntMeterNum,
                                StrJdrq = tempTableMember[i].StrJdrq,
                                StrJyy = tempTableMember[i].StrJyy,
                                StrJddw = tempTableMember[i].StrJddw,
                                StrHyy = tempTableMember[i].StrHyy,
                                StrTestType = tempTableMember[i].StrTestType,
                                StrBlx = tempTableMember[i].StrBlx,
                                StrBmc = tempTableMember[i].StrBmc,
                                StrUb = tempTableMember[i].StrUb,
                                StrIb = tempTableMember[i].StrIb,
                                StrMeterLevel = tempTableMember[i].StrMeterLevel,
                                StrManufacture = tempTableMember[i].StrManufacture,
                                StrWcResult = tempTableMember[i].StrWcResult,
                                StrShellSeal = tempTableMember[i].StrShellSeal,
                                StrCodeSeal = tempTableMember[i].StrCodeSeal,
                                StrYXRQ = (Convert.ToDateTime(tempTableMember[i].StrJdrq).AddYears(5)).ToString(),

                            });
                        }

                    }
                }
                #endregion

                #region Get One MetetInfo
                for (int ReZcbh = 0; ReZcbh < lis_ReZcbh.Count; ReZcbh++)
                {
                    operate.ShowTheConditionTable(TableName, lis_ReZcbh[ReZcbh], ViewMember.CreateInstance().Operater, keyWord, out tempTableMember);
                    for (int j = 0; j < tempTableMember.Count; j++)
                    {
                        LineNum++;
                        MeterList.Add(new MeterInfoItem()
                        {
                            ID = LineNum,
                            StrBZZZZCBH = tempTableMember[j].StrBZZZZCBH,
                            StrJlbh = tempTableMember[j].StrJlbh,
                            StrJdjl = tempTableMember[j].StrJdjl,
                            StrGZDBH = tempTableMember[j].StrGZDBH,
                            StrWD = tempTableMember[j].StrWD,
                            StrSD = tempTableMember[j].StrSD,
                            IntMeterNum = tempTableMember[j].IntMeterNum,
                            StrJdrq = tempTableMember[j].StrJdrq,
                            StrJyy = tempTableMember[j].StrJyy,
                            StrJddw = tempTableMember[j].StrJddw,
                            StrHyy = tempTableMember[j].StrHyy,
                            StrTestType = tempTableMember[j].StrTestType,
                            StrBlx = tempTableMember[j].StrBlx,
                            StrBmc = tempTableMember[j].StrBmc,
                            StrUb = tempTableMember[j].StrUb,
                            StrIb = tempTableMember[j].StrIb,
                            StrMeterLevel = tempTableMember[j].StrMeterLevel,
                            StrManufacture = tempTableMember[j].StrManufacture,
                            StrWcResult = tempTableMember[j].StrWcResult,
                            StrShellSeal = tempTableMember[j].StrShellSeal,
                            StrCodeSeal = tempTableMember[j].StrCodeSeal,
                            StrYXRQ = (Convert.ToDateTime(tempTableMember[j].StrJdrq).AddYears(5)).ToString(),

                        });

                    }

                }


                #endregion


                #region
                for (int i = 0; i < MeterList.Count; i++)
                {
                    List<DataTableMember> temp = new List<DataTableMember>();
                    operate.ShowTheLockTable(MeterList[i].StrJlbh, MeterList[i].StrGZDBH, out temp);

                    if ((keyWord.Substring(0, 1) == "E" || keyWord.Substring(0, 1) == "F" || keyWord.Contains("ZP") || keyWord.Contains("ZF")))
                    {
                        if (temp.Count != 0)
                        {
                            MeterList[i].StrShellSeal = temp[0].StrFYZCBH;
                            MeterList[i].StrCodeSeal = " ";

                        }
                        else
                        {
                            MeterList[i].StrShellSeal = " ";
                            MeterList[i].StrCodeSeal = " ";
                        }
                    }
                    else
                    {
                        if (temp.Count > 0)
                        {
                            if (temp[0].StrJFWZDM == "07")
                            {
                                MeterList[i].StrShellSeal = temp[1].StrFYZCBH;
                                MeterList[i].StrCodeSeal = temp[0].StrFYZCBH;
                            }
                            else
                            {
                                MeterList[i].StrShellSeal = temp[0].StrFYZCBH;
                                MeterList[i].StrCodeSeal = temp[1].StrFYZCBH;

                            }
                        }

                    }

                }
                string blank = " ";
                for (int j = 0; j < MeterList.Count; j++)
                {
                    if (MeterList[j].StrJlbh.Length == 24)
                    {
                        string A = MeterList[j].StrJlbh.Substring(0, 4);
                        string B = MeterList[j].StrJlbh.Substring(4, 4);
                        string C = MeterList[j].StrJlbh.Substring(8, 4);
                        string D = MeterList[j].StrJlbh.Substring(12, 4);
                        string E = MeterList[j].StrJlbh.Substring(16, 4);
                        string F = MeterList[j].StrJlbh.Substring(20, 4);
                        MeterList[j].StrJlbh = A + blank + B + blank + C + blank + D + blank + E + blank + F;
                    }
                }
                for (int j = 0; j < MeterList.Count; j++)
                {
                    if (MeterList[j].StrShellSeal != null)
                    {
                        if (MeterList[j].StrShellSeal.Length == 24)
                        {
                            string A = MeterList[j].StrShellSeal.Substring(0, 4);
                            string B = MeterList[j].StrShellSeal.Substring(4, 4);
                            string C = MeterList[j].StrShellSeal.Substring(8, 4);
                            string D = MeterList[j].StrShellSeal.Substring(12, 4);
                            string E = MeterList[j].StrShellSeal.Substring(16, 4);
                            string F = MeterList[j].StrShellSeal.Substring(20, 4);
                            MeterList[j].StrShellSeal = A + blank + B + blank + C + blank + D + blank + E + blank + F;
                        }
                    }

                }
                for (int j = 0; j < MeterList.Count; j++)
                {
                    if (MeterList[j].StrCodeSeal != null)
                    {
                        if (MeterList[j].StrCodeSeal.Length == 24)
                        {
                            string A = MeterList[j].StrCodeSeal.Substring(0, 4);
                            string B = MeterList[j].StrCodeSeal.Substring(4, 4);
                            string C = MeterList[j].StrCodeSeal.Substring(8, 4);
                            string D = MeterList[j].StrCodeSeal.Substring(12, 4);
                            string E = MeterList[j].StrCodeSeal.Substring(16, 4);
                            string F = MeterList[j].StrCodeSeal.Substring(20, 4);
                            MeterList[j].StrCodeSeal = A + blank + B + blank + C + blank + D + blank + E + blank + F;
                        }
                    }

                }
                if (ViewMember.CreateInstance().MeterInfoList != null)
                    ViewMember.CreateInstance().MeterInfoList.Clear();
                ViewMember.CreateInstance().MeterInfoList = MeterList;

                #endregion




            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
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
