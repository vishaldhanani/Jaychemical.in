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
    public class QuarterwiseCustomerInfo
    {
        private String m_CustomerNo;
        public String CustomerNo
        {
            get { return m_CustomerNo; }
            set { m_CustomerNo = value; }
        }
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private Decimal m_Custbalance;
        public Decimal Custbalance
        {
            get { return m_Custbalance; }
            set { m_Custbalance = value; }
        }

        public static List<QuarterwiseCustomerInfo> Quarter1CustDetails(string Code)
        {
            string strSQL = string.Empty;
            List<QuarterwiseCustomerInfo> list = new List<QuarterwiseCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Agent_Cust_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        QuarterwiseCustomerInfo obj = new QuarterwiseCustomerInfo();
                        obj.CustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("CustomerNo")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Custbalance = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Balance"))))); //Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Balance")));

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

        public static DataTable GetSuggestedCustomers(string SearchedTxt, string Code, string Type)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_GetSuggestedCustomers";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                db.AddInParameter(dbCommand, "@Name", DbType.String, SearchedTxt);
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@AgentSubtype", DbType.String, Type);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                // added new code for closing the connection of reader  - error resolved - Raj Shah 04/07/2016
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //    }
                //}
               
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();

                return dt;
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
        }
    }
}
