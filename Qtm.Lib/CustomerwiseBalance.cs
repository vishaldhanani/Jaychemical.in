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
    public class CustomerwiseBalance
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

        public static List<CustomerwiseBalance> ListDueInNext7Days(string Code)
        {
            string strSQL = string.Empty;
            List<CustomerwiseBalance> list = new List<CustomerwiseBalance>();
            SqlDataReader reader;
            strSQL = "SP_WA_OutStandingAmt_CustomerWise_Summary";
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
                        CustomerwiseBalance obj = new CustomerwiseBalance();
                        obj.CustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Custbalance = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Balance"))))); //Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Balance")));

                        if (obj.Custbalance != 0)
                        {
                            list.Add(obj);
                        }
                        else
                        {

                        }
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

        public static List<CustomerwiseBalance> ListTotOutstanding(string Code)
        {
            string strSQL = string.Empty;
            List<CustomerwiseBalance> list = new List<CustomerwiseBalance>();
            SqlDataReader reader;
            strSQL = "SP_WA_TotalOutstandingAmt_CSummary";
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
                        CustomerwiseBalance obj = new CustomerwiseBalance();
                        obj.CustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Custbalance = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Balance"))))); //Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Balance")));

                        if (obj.Custbalance != 0)
                        {
                            list.Add(obj);
                        }
                        else
                        {

                        }
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
        
        public static List<CustomerwiseBalance> ListOverDue(string Code)
        {
            string strSQL = string.Empty;
            List<CustomerwiseBalance> list = new List<CustomerwiseBalance>();
            SqlDataReader reader;
            strSQL = "SP_WA_OverDueOutStandingAmt_CSummary";   
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
                        CustomerwiseBalance obj = new CustomerwiseBalance();
                        obj.CustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Custbalance = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Balance"))))); //Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Balance")));
                        if (obj.Custbalance != 0)
                        {
                            list.Add(obj);
                        }
                        else
                        {
                            
                        }                       
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
                DataTable dt = new DataTable();
                dt.Load(reader);
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
