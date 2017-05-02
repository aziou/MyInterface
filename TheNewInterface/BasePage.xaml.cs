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
using System.Windows.Shapes;

namespace TheNewInterface
{
    /// <summary>
    /// BasePage.xaml 的交互逻辑
    /// </summary>
    public partial class BasePage : Window
    {
        public BasePage()
        {
            InitializeComponent();
            LoadBaseConfig();
        }
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";


        #region 数据加载初始化
        private void LoadBaseConfig()
        {

            #region Combox数据加载
            //软件类型
            cmb_SoftType.Items.Add("CL3000G");
            cmb_SoftType.Items.Add("CL3000S");
            cmb_SoftType.Items.Add("CL3000F");
            cmb_SoftType.Items.Add("CL3000DV80");
            //地区编号
            //铅封加载
            cmb_Seal01.Items.Add("左耳封");
            cmb_Seal01.Items.Add("右耳封");
            cmb_Seal01.Items.Add("编程小门"); 
            cmb_Seal01.Items.Add("表盖封印");

            cmb_Seal02.Items.Add("左耳封");
            cmb_Seal02.Items.Add("右耳封");
            cmb_Seal02.Items.Add("编程小门");
            cmb_Seal02.Items.Add("表盖封印");

            cmb_Seal03.Items.Add("左耳封");
            cmb_Seal03.Items.Add("右耳封");
            cmb_Seal03.Items.Add("编程小门");
            cmb_Seal03.Items.Add("表盖封印");
            //人员加载
            LoadUser();
            //地区编号
            LoadCompany();
            #endregion
            string strSection = "NewUser/CloumMIS/Item";
            txt_DataPath.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            txt_equipment.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_equipment", "Value", "", BaseConfigPath);
            txt_Jyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_Jyy", "Value", "", BaseConfigPath);
            txt_Hyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_Hyy", "Value", "", BaseConfigPath);
            cmb_SoftType.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            cmb_Company.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Company", "Value", "", BaseConfigPath);
            cmb_Hyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Hyy", "Value", "", BaseConfigPath);
            cmb_Jyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Jyy", "Value", "", BaseConfigPath);
            cmb_Seal03.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Seal03", "Value", "", BaseConfigPath);
            cmb_Seal02.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Seal02", "Value", "", BaseConfigPath);
            cmb_Seal01.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Seal01", "Value", "", BaseConfigPath);
            txt_Report.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_Report", "Value", "", BaseConfigPath);
          
        }

        #endregion
        /// <summary>
        /// 利用visualtreehelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }
        private void btn_SetPath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog =

                  new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "数据库文件|*.mdb;*.accdb";

            if (dialog.ShowDialog() == true)
            {
                txt_DataPath .Text= dialog.FileName;
     

            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string control_name;
            string control_content;
            List<TextBox> txtList = FindVisualChild<TextBox>(DataConfig);
            List<ComboBox> ComboxList = FindVisualChild<ComboBox>(DataConfig);
            string strSection = "NewUser/CloumMIS/Item";
            foreach (TextBox v in txtList)
            {

                control_content = ((TextBox)v).Text;
                control_name = ((TextBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection,"Name", control_name,"Value", control_content, BaseConfigPath);


            }
            foreach (ComboBox v in ComboxList)
            {

                control_content = ((ComboBox)v).Text;
                control_name = ((ComboBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection, "Name", control_name, "Value", control_content, BaseConfigPath);


            }
            #region 数据库链接字符串
            string LinkAccessWord = "";
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            LinkAccessWord = Sql_word_1 + txt_DataPath.Text.ToString().Trim() + Sql_word_2;
            OperateData.FunctionXml.UpdateElement(strSection, "Name", "AccessLink", "Value", Sql_word_1+txt_DataPath.Text.ToString().Trim()+Sql_word_2, BaseConfigPath);

            #endregion 
            #region 获取工单字符串
            string WorkNumLoad = "";
            WorkNumLoad = "select GZDBH from {0} where wcbz='0' and rymc ='" + cmb_Jyy.Text + "' and DQBM='" + txt_CompanyNum.Text + "'order by GZDBH desc ";
            OperateData.FunctionXml.UpdateElement(strSection, "Name", "WorkNumLoad", "Value", WorkNumLoad, BaseConfigPath);

            #endregion
            this.Close();
        }

        private void btn_AddMember_Click(object sender, RoutedEventArgs e)
        {
            AddUser adduser = new AddUser("AddMember");
            adduser.ShowDialog();
        }
        private void LoadUser()
        {
            List<string> UserList = new List<string>();
            UserList = OperateData.FunctionXml.GetAllNodeData("NewUser/User", "Item", "UserName", BaseConfigPath);

            AddListToCombox(cmb_Hyy, UserList);
            AddListToCombox(cmb_Jyy, UserList);
        }
        private void LoadCompany()
        {
            List<string> UserList = new List<string>();
            UserList = OperateData.FunctionXml.GetAllNodeData("NewUser/DQBM", "Item", "Company", BaseConfigPath);

            AddListToCombox(cmb_Company, UserList);
           
        }
        private void AddListToCombox(ComboBox cmb, List<string> addList)
        {
            cmb.Items.Clear();
            foreach (string temp in addList)
            {
                cmb.Items.Add(temp);
            }
        }
        private void CmbChangeValue(ComboBox cmb,TextBox Value_Txt)
        {
           
            Value_Txt.Text = OperateData.FunctionXml.ReadElement("NewUser/User/Item", "UserName", cmb.SelectedValue.ToString(), "UserNumber", "", BaseConfigPath);
        }
        private void CmbChangeValue(ComboBox cmb, TextBox Value_Txt,bool isCompany)
        {

            Value_Txt.Text = OperateData.FunctionXml.ReadElement("NewUser/DQBM/Item", "Company", cmb.SelectedValue.ToString(), "CompanyNum", "", BaseConfigPath);
        }
        private void cmb_Jyy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbChangeValue(cmb_Jyy, txt_Jyy);
        }

       

        private void cmb_Hyy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbChangeValue(cmb_Hyy, txt_Hyy);
        }

        private void btn_AddDQBM_Click(object sender, RoutedEventArgs e)
        {
            AddUser adduser = new AddUser("AddArea");
            adduser.ShowDialog();
        }

        private void cmb_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbChangeValue(cmb_Company, txt_CompanyNum,true);
        }

        private void btn_SetReportPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;

                MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            //Microsoft.Win32.OpenFileDialog dialog =

            //      new Microsoft.Win32.OpenFileDialog();

            //dialog.Filter = "数据库文件|*.mdb;*.accdb";

            //if (dialog.ShowDialog() == true)
            //{

            txt_Report.Text = dialog.SelectedPath;  //   

        }
  
    }
}
