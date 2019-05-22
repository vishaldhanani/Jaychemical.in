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
   public class QuarterCustomerSummary
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
        private DateTime m_PostingDate;

        public DateTime PostingDate
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
        private Decimal m_Amt;

        public Decimal Amt
        {
            get { return m_Amt; }
            set { m_Amt = value; }
        }


        public static List<QuarterCustomerSummary> ListQ1(String Code, String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<QuarterCustomerSummary> list = new List<QuarterCustomerSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q1_Cust_Inv_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            QuarterCustomerSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new QuarterCustomerSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.Amt = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount")));
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

        public static List<QuarterCustomerSummary> ListQ2(String Code, String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<QuarterCustomerSummary> list = new List<QuarterCustomerSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q2_Cust_Inv_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            QuarterCustomerSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new QuarterCustomerSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.Amt = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount")));
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

        public static List<QuarterCustomerSummary> ListQ3(String Code, String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<QuarterCustomerSummary> list = new List<QuarterCustomerSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q3_Cust_Inv_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            QuarterCustomerSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new QuarterCustomerSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.Amt = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount")));
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

        public static List<QuarterCustomerSummary> ListQ4(String Code, String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<QuarterCustomerSummary> list = new List<QuarterCustomerSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q4_Cust_Inv_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            QuarterCustomerSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new QuarterCustomerSummary();
                        obj.InvoiceNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Document No.")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.Amt = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount")));
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
   