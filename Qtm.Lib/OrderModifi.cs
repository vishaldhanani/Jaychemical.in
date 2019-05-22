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
    public class OrderModifi
    {
        private String m_OrderNo;
        public String OrderNo
        {
            get { return m_OrderNo; }
            set { m_OrderNo = value; }
        }

        private Double m_Amount;

        public Double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        private String m_PostingDate;

        public String PostingDate
        {
            get { return m_PostingDate; }
            set { m_PostingDate = value; }
        }

        private String m_BlanketOrderNo;
        public String BlanketOrderNo
        {
            get { return m_BlanketOrderNo; }
            set { m_BlanketOrderNo = value; }
        }
        
        public static List<OrderModifi> List(string Code)
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModificationHistory";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, Code);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.Amount = Math.Round(Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("PostingDate")));
                        obj.BlanketOrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("BlanketOrderNo")));
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


        public static List<OrderModifi> InternalList(string ItemCategoryCode)
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModificationInternalHistory";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("PostingDate")));

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
        public static List<OrderModifi> InternalListForBlockOrders(string ItemCategoryCode)
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModificationInternal_Blocked";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("PostingDate")));
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
        
        public static List<OrderModifi> SearchOrder(string ItemCategoryCode,string Code)
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderSerachForInternal";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, Code);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("PostingDate")));
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
    
        public static List<OrderModifi> SearchSalesOrder(string ItemCategoryCode, string Code)
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_SalesOrderSearchForInternal";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, Code);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));
                        obj.Amount = Math.Round(Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.PostingDate = Convert.ToString(reader.GetValue(reader.GetOrdinal("PostingDate")));
                        obj.BlanketOrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("BlanketOrderNo")));
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

        public static List<OrderModifi> MakeOrder()
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_MakeOrderModificationInternalHistory";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));

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

        public static List<OrderModifi> InternalListApprover()
        {
            string strSQL = string.Empty;
            List<OrderModifi> list = new List<OrderModifi>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModificationInternalHistory_Approver";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModifi obj = new OrderModifi();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("OrderNo")));


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
