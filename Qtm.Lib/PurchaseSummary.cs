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
    public class PurchaseSummary
    {
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        private decimal m_Qty;
        public decimal Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private String m_ItemNo;
        public String ItemNo
        {
            get { return m_ItemNo; }
            set { m_ItemNo = value; }
        }
        private String m_Description;
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        private String m_DocumentNo;
        public String DocumentNo
        {
            get { return m_DocumentNo; }
            set { m_DocumentNo = value; }
        }
        private DateTime m_PostingDate;
        public DateTime PostingDate
        {
            get { return m_PostingDate; }
            set { m_PostingDate = value; }
        }
        private String m_Year;
        public String Year
        {
            get { return m_Year; }
            set { m_Year = value; }
        }
        private String m_Month;
        public String Month
        {
            get { return m_Month; }
            set { m_Month = value; }
        }

        private String m_Month_Year;
        public String Month_Year
        {
            get { return m_Month_Year; }
            set { m_Month_Year = value; }
        }

        public static List<PurchaseSummary> List(string AgentCode)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_CustomerWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));

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

        public static List<PurchaseSummary> ItemWise(string AgentCode, string Customer)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_ItemWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));

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

        public static List<PurchaseSummary> PurchaseItemList(string AgentCode, string ItemNo)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_ItemWise_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.DocumentNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
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

        public static List<PurchaseSummary> YearWise(string AgentCode, string Customer, string ItemNo)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_YearWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.Year = Convert.ToString(reader.GetValue(reader.GetOrdinal("YEAR")));                     
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));

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

        public static List<PurchaseSummary> MonthWise(string AgentCode, string Customer, string ItemNo, string Year)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_MonthWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                db.AddInParameter(dbCommand, "@Year", DbType.String, Year);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.Month_Year = Convert.ToString(reader.GetValue(reader.GetOrdinal("MonthYear")));
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));
                        obj.Month = Convert.ToString(reader.GetValue(reader.GetOrdinal("Month")));

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

        public static List<PurchaseSummary> PurchaseDetails(string AgentCode, string CustomerNo, string ItemNo, string Year, string Month)
        {
            string strSQL = string.Empty;
            List<PurchaseSummary> list = new List<PurchaseSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_PurchaseSummary_Details";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            PurchaseSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, CustomerNo);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                db.AddInParameter(dbCommand, "@Year", DbType.String, Year);
                db.AddInParameter(dbCommand, "@Month", DbType.String, Month);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new PurchaseSummary();
                        obj.Month_Year = Convert.ToString(reader.GetValue(reader.GetOrdinal("MonthYear")));
                        obj.Qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Quantity")))));
                        obj.Month = Convert.ToString(reader.GetValue(reader.GetOrdinal("Month")));
                        obj.DocumentNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ShipmentNo")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("PostingDate")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Year = Convert.ToString(reader.GetValue(reader.GetOrdinal("Year")));
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
