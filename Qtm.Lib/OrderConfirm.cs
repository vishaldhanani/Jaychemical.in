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
    public class OrderConfirm
    {
        private String m_OrderNo;
        public String OrderNo
        {
            get { return m_OrderNo; }
            set { m_OrderNo = value; }
        }
        private DateTime m_PostingDate;
        public DateTime PostingDate
        {
            get { return m_PostingDate; }
            set { m_PostingDate = value; }
        }

        private String m_CustomerPriceGrp;
        public String CustomerPriceGrp
        {
            get { return m_CustomerPriceGrp; }
            set { m_CustomerPriceGrp = value; }
        }

        private String m_SalesOrderNo;
        public String SalesOrderNo
        {
            get { return m_SalesOrderNo; }
            set { m_SalesOrderNo = value; }
        }


        public static List<OrderConfirm> List(string Code)
        {
            string strSQL = string.Empty;
            List<OrderConfirm> list = new List<OrderConfirm>();
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderHistory";
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
                        OrderConfirm obj = new OrderConfirm();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No_")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.SalesOrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("SalesOrderNo")));
                        
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

        public static List<OrderConfirm> ListSearch(string Code, string AgentCode)
        {
            string strSQL = string.Empty;
            List<OrderConfirm> listsearch = new List<OrderConfirm>();
            SqlDataReader reader;
            strSQL = "SP_WA_SearchOrder_No";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, Code);
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderConfirm obj = new OrderConfirm();
                        obj.OrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No_")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.SalesOrderNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("SalesOrderNo")));

                        listsearch.Add(obj);
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
            return listsearch;
        }
    }
}
