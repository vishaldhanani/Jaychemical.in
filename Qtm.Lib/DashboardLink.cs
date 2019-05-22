using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Qtm.Lib
{
    public class DashboardLink
    {
        private String m_URL;
        public String URL
        {
            get { return m_URL; }
            set { m_URL = value; }
        }


        private String m_DynamicMenu;
        public String DynamicMenu
        {
            get { return m_DynamicMenu; }
            set { m_DynamicMenu = value; }
        }

        private String m_LastLoginTimeAgent;

        public String  LastLoginTimeAgent
        {
            get { return m_LastLoginTimeAgent; }
            set { m_LastLoginTimeAgent = value; }
        }

        public static List<DashboardLink> DashboardFill(string Code, int UserType, string ItemCategoryCode)
        {
            string strSQL = string.Empty;
            List<DashboardLink> list = new List<DashboardLink>();
            SqlDataReader reader;
            strSQL = "SP_WA_DynamicMenu";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@UserType", DbType.Int32, UserType);
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);

                
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DashboardLink obj = new DashboardLink();
                        obj.DynamicMenu = Convert.ToString(reader.GetValue(reader.GetOrdinal("DynamicMenu")));
                        obj.URL = Convert.ToString(reader.GetValue(reader.GetOrdinal("URL")));                      
                        
                        list.Add(obj);
                    }
                }
                if (!reader.IsClosed)
                    reader.Close();
            }
            catch (SqlException e)
            { throw e; }
            catch (Exception e)
            { throw e; }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return list;
        }
        
        public static List<DashboardLink> RequisitionFormsFill(string Code, int UserType, string ItemCategoryCode)
        {
            string strSQL = string.Empty;
            List<DashboardLink> list = new List<DashboardLink>();
            SqlDataReader reader;
            strSQL = "SP_WA_RequisitionForms";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@UserType", DbType.Int32, UserType);
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DashboardLink obj = new DashboardLink();
                        obj.DynamicMenu = Convert.ToString(reader.GetValue(reader.GetOrdinal("DynamicMenu")));
                        obj.URL = Convert.ToString(reader.GetValue(reader.GetOrdinal("URL")));

                        list.Add(obj);
                    }
                }
                if (!reader.IsClosed)
                    reader.Close();
            }
            catch (SqlException e)
            { throw e; }
            catch (Exception e)
            { throw e; }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return list;
        }               
    }
}
