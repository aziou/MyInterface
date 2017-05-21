using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace ViewLocalData
{
    public class ViewData
    {
        public readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static readonly string AccessLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "AccessLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static DataTable ViewDataBase(string SQL)
        {
            try
            {
                DataSet Ds = new DataSet();
                DataTable Dt = new DataTable();
                using (OleDbConnection conn = new OleDbConnection(AccessLink))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    OleDbDataAdapter Adapter = new OleDbDataAdapter(SQL, conn);

                    Adapter.Fill(Dt);
                    return Dt;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool ExceuteSql(string sqlword)
        {
            try
            {
                
                using (OleDbConnection conn = new OleDbConnection(AccessLink))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(sqlword,conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;

            }
            catch (Exception e)
            {
                return false; ;

            }
        }
    }
}
