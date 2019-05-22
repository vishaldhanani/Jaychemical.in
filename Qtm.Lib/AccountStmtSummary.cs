using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace Qtm.Lib
{
  public class AccountStmtSummary
    {
        private String m_Code;

        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        private String m_InvoiceNo;

        public String InvoiceNo
        {
            get { return m_InvoiceNo; }
            set { m_InvoiceNo = value; }
        }
        private String m_PostingDate;

        public String PostingDate
        {
            get { return m_PostingDate; }
            set { m_PostingDate = value; }
        }
        private DateTime m_DueDate;

        public DateTime DueDate
        {
            get { return m_DueDate; }
            set { m_DueDate = value; }
        }
        private Decimal m_Amount;

        public Decimal Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }


        public static List<AccountStmtSummary> List(String Code, String Customer)
        {
            string strSQL = string.Empty;
            List<AccountStmtSummary> list = new List<AccountStmtSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_OutStandingAmt_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            AccountStmtSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, Customer);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new AccountStmtSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.DueDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Due Date")));
                        obj.Amount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Remaining Amt")));                       
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

        public static List<AccountStmtSummary> ListTotalOutStanding(String Code, String Customer)
        {
            string strSQL = string.Empty;
            List<AccountStmtSummary> list = new List<AccountStmtSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_TotalOutstandingAmt_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            AccountStmtSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, Customer);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new AccountStmtSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.DueDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Due Date")));
                        obj.Amount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Remaining Amt")));
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

        public static List<AccountStmtSummary> ListOverDue(String Code, String Customer)
        {
            string strSQL = string.Empty;
            List<AccountStmtSummary> listOverDue = new List<AccountStmtSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_OverDueOutStandingAmt_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            AccountStmtSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, Customer);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new AccountStmtSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.DueDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Due Date")));
                        obj.Amount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Remaining Amt")));
                        listOverDue.Add(obj);
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
            return listOverDue;
        }

    }
}
