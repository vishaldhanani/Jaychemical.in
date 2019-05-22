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
    public class TaxationPendingInfo
    {
        private decimal m_TaxationAmount;

        public Decimal TaxationAmount
        {
            get { return m_TaxationAmount; }
            set { m_TaxationAmount = value; }
        }
        private decimal m_Quarter1_Amount;

        public decimal Quarter1_Amount
        {
            get { return m_Quarter1_Amount; }
            set { m_Quarter1_Amount = value; }
        }

        private decimal m_Quarter2_Amount;

        public decimal Quarter2_Amount
        {
            get { return m_Quarter2_Amount; }
            set { m_Quarter2_Amount = value; }
        }

        private decimal m_Quarter3_Amount;

        public decimal Quarter3_Amount
        {
            get { return m_Quarter3_Amount; }
            set { m_Quarter3_Amount = value; }
        }
        private decimal m_Quarter4_Amount;

        public decimal Quarter4_Amount
        {
            get { return m_Quarter4_Amount; }
            set { m_Quarter4_Amount = value; }
        }
       
        public static List<TaxationPendingInfo> TaxationPending(String Agentcode)
        {
            string strSQL = string.Empty;
            List<TaxationPendingInfo> list = new List<TaxationPendingInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_PendingSummary"; 
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
                        TaxationPendingInfo obj = new TaxationPendingInfo();
                        obj.TaxationAmount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));                        
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

        public static List<TaxationPendingInfo> Quarter1Amt(String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<TaxationPendingInfo> list = new List<TaxationPendingInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q1_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);
               // db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, EndDate);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TaxationPendingInfo obj = new TaxationPendingInfo();
                        obj.Quarter1_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));                        
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

        public static List<TaxationPendingInfo> Quarter2Amt(String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<TaxationPendingInfo> list = new List<TaxationPendingInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q2_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);                
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        TaxationPendingInfo obj = new TaxationPendingInfo();
                        obj.Quarter2_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));

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

        public static List<TaxationPendingInfo> Quarter3Amt(String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<TaxationPendingInfo> list = new List<TaxationPendingInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q3_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);               
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        TaxationPendingInfo obj = new TaxationPendingInfo();
                        obj.Quarter3_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));

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

        public static List<TaxationPendingInfo> Quarter4Amt(String CustomerNo, DateTime StartDate)
        {
            string strSQL = string.Empty;
            List<TaxationPendingInfo> list = new List<TaxationPendingInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Q4_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);               
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TaxationPendingInfo obj = new TaxationPendingInfo();
                        obj.Quarter4_Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt")))));
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
