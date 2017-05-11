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
    /// AddUser.xaml 的交互逻辑
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            LoadUser();
        }
        public static string AddType = "";
        public static string strSection = "",key="",value="",loadSection="";
        public AddUser(string Type)
        {
            InitializeComponent();
            AddType = Type;
            switch (AddType)
            { 
                case "AddMember":
                    strSection = "NewUser/User/Item";
                    key = "UserName";
                    value = "UserNumber";
                    loadSection = "NewUser/User";
                    break;

                case "AddArea":
                    strSection = "NewUser/DQBM/Item";
                    key = "Company";
                    value = "CompanyNum";
                    lab_name.Content = "单位名称";
                    lab_Cname.Content = "单位名称";
                    lab_nameNum.Content = "地区编号";
                    loadSection = "NewUser/DQBM";
                    break;
            }
            LoadUser();
        }
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string UserName = "", UserNumber = "";
            UserName = txt_username.Text.ToString();
            UserNumber = txt_userNumber.Text.ToString();

            OperateData.FunctionXml.UpdateElement(strSection, key, UserName, value, UserNumber, BaseConfigPath);

            LoadUser();
            MessageBox.Show("添加" + UserName + "成功！");
        }

        

        private void AddListToCombox(ComboBox cmb, List<string> addList)
        {
            cmb.Items.Clear();
            foreach (string temp in addList)
            {
                cmb.Items.Add(temp);
            }
        }

        private void LoadUser()
        {
            List<string> UserList = new List<string>();
            UserList = OperateData.FunctionXml.GetAllNodeData(loadSection, "Item", key, BaseConfigPath);

            AddListToCombox(cmb_LoadUserName, UserList);
        }

        private void LoadDQBM()
        {
            List<string> UserList = new List<string>();
            UserList = OperateData.FunctionXml.GetAllNodeData(loadSection, "Item", key, BaseConfigPath);

            AddListToCombox(cmb_LoadUserName, UserList);
        }
        private void cmb_LoadUserName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txt_username.Text = cmb_LoadUserName.SelectedValue.ToString();
                txt_userNumber.Text = OperateData.FunctionXml.ReadElement(strSection, key, cmb_LoadUserName.SelectedValue.ToString(), value, "", BaseConfigPath);

            }
            catch (Exception ex_change)
            { 
            
            }
       }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            OperateData.FunctionXml.RemoveNode(strSection, key, cmb_LoadUserName.SelectedValue.ToString(), BaseConfigPath);
            LoadDQBM();
        }
    }
}
