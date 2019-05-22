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
using System.Configuration;


namespace Qtm.Lib
{
    public class AccountStatementInfo
    {
        private decimal m_Due_NextAmount;

        public Decimal Due_NextAmount
        {
            get { return m_Due_NextAmount; }
            set { m_Due_NextAmount = value; }
        }
        private decimal m_OverDue_Amount;

        public decimal OverDue_Amount
        {
            get { return m_OverDue_Amount; }
            set { m_OverDue_Amount = value; }
        }
        private decimal m_Total_Outstanding_Amount;

        public decimal Total_Outstanding_Amount
        {
            get { return m_Total_Outstanding_Amount; }
            set { m_Total_Outstanding_Amount = value; }
        }

        private decimal m_Account_StmtAmount;

        public decimal Account_StmtAmount
        {
            get { return m_Account_StmtAmount; }
            set { m_Account_StmtAmount = value; }
        }

        public static List<AccountStatementInfo> DueNextAmount(String Agentcode)
        {
            string strSQL = string.Empty;
            List<AccountStatementInfo> list = new List<AccountStatementInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OutStandingAmt"; 
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Agentcode);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                        
                        AccountStatementInfo obj = new AccountStatementInfo();
                        obj.Due_NextAmount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));                        
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

        public static List<AccountStatementInfo> OverDueAmount(String Agentcode)
        {
            string strSQL = string.Empty;
            List<AccountStatementInfo> list = new List<AccountStatementInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OverDueOutStandingAmt";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Agentcode);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AccountStatementInfo obj = new AccountStatementInfo();
                        obj.OverDue_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));                            
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

        public static List<AccountStatementInfo> TotalOutstandingAmount(String Agentcode)
        {
            string strSQL = string.Empty;
            List<AccountStatementInfo> list = new List<AccountStatementInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_TotalOutstandingAmt";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Agentcode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AccountStatementInfo obj = new AccountStatementInfo();
                        obj.Total_Outstanding_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));
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

        public static List<AccountStatementInfo> AccountStmtAmount()
        {
            string strSQL = string.Empty;
            List<AccountStatementInfo> list = new List<AccountStatementInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OutStandingAmt";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AccountStatementInfo obj = new AccountStatementInfo();
                        obj.Account_StmtAmount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));
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
