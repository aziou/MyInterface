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
    /// OracleConfig.xaml 的交互逻辑
    /// </summary>
    public partial class OracleConfig : Window
    {
        public OracleConfig()
        {
            InitializeComponent();
            LoadMessage();
        }
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
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";

        private void LoadMessage()
        {
            string strSection = "NewUser/CloumMIS/Item";

            txt_USERNAME.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_USERNAME", "Value", "", BaseConfigPath);
            txt_PASSWORD.Password = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_PASSWORD", "Value", "", BaseConfigPath);
            txt_IPADDRESS.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_IPADDRESS", "Value", "", BaseConfigPath);
            txt_PORT.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_PORT", "Value", "", BaseConfigPath);
            txt_ServerName.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_ServerName", "Value", "", BaseConfigPath);
            txt_SqlServerName.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_SqlServerName", "Value", "", BaseConfigPath);
            
            
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string control_name;
            string control_content;
            List<TextBox> txtList = FindVisualChild<TextBox>(DataConfig);
            List<PasswordBox> ComboxList = FindVisualChild<PasswordBox>(DataConfig);
            string strSection = "NewUser/CloumMIS/Item";
            foreach (TextBox v in txtList)
            {

                control_content = ((TextBox)v).Text;
                control_name = ((TextBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection, "Name", control_name, "Value", control_content, BaseConfigPath);


            }
            foreach (PasswordBox v in ComboxList)
            {

                control_content = ((PasswordBox)v).Password;
                control_name = ((PasswordBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection, "Name", control_name, "Value", control_content, BaseConfigPath);


            }
            #region Make Oracle Link
             string StrOracleCoon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVICE_NAME= {2})));User Id={3}; Password={4}";
             StrOracleCoon = string.Format(StrOracleCoon, txt_IPADDRESS.Text.ToString().Trim(), txt_PORT.Text.ToString().Trim(), txt_ServerName.Text.ToString().Trim(), txt_USERNAME.Text.ToString().Trim(), txt_PASSWORD.Password.ToString().Trim());
             OperateData.FunctionXml.UpdateElement(strSection, "Name", "OracleLink", "Value", StrOracleCoon, BaseConfigPath);

            #endregion
            this.Close();
        }
    }
}
