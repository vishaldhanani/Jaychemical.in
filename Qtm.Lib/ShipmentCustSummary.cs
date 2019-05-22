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
    public class ShipmentCustSummary
    {
        private String m_Code;

        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        private String m_Name;

        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_Name1;

        public string Name1
        {
            get { return m_Name1; }
            set { m_Name1 = value; }
        }

        private string m_Consigneename;

        public string Consigneename
        {
            get { return m_Consigneename; }
            set { m_Consigneename = value; }
        }

        private DateTime m_PlannedDate;

        public DateTime PlannedDate
        {
            get { return m_PlannedDate; }
            set { m_PlannedDate = value; }
        }
        private DateTime m_DueDate;

        public DateTime DueDate
        {
            get { return m_DueDate; }
            set { m_DueDate = value; }
        }
        private Decimal m_qty;

        public Decimal qty
        {
            get { return m_qty; }
            set { m_qty = value; }
        }
        private Decimal m_count;

        public Decimal count
        {
            get { return m_count; }
            set { m_count = value; }
        }

        private Decimal m_CustomerNo;

        public Decimal CustomerNo
        {
            get { return m_CustomerNo; }
            set { m_CustomerNo = value; }
        }

        private DateTime m_OrderDate;

        public DateTime OrderDate
        {
            get { return m_OrderDate; }
            set { m_OrderDate = value; }
        }

        private string m_UOM;

        public string UOM
        {
            get { return m_UOM; }
            set { m_UOM = value; }
        }
        

        public static List<ShipmentCustSummary> ListShipmentCust(String Code, String Customer)
        {
            string strSQL = string.Empty;
            List<ShipmentCustSummary> list = new List<ShipmentCustSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Customerwise_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            ShipmentCustSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);
             

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ShipmentCustSummary();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Blanket Order No.")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Name")));
                        obj.PlannedDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Planned Date")));
                        obj.count = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Counter")));
                        obj.qty = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Qty")));
                        obj.Consigneename = Convert.ToString(reader.GetValue(reader.GetOrdinal("Consignee Name")));
                        obj.OrderDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("OrderDate")));
                        obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM")));
                    
                        
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

        public static List<ShipmentCustSummary> ListShipmentConsignee(String Code, String Customer)
        {
            string strSQL = string.Empty;
            List<ShipmentCustSummary> list = new List<ShipmentCustSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Consigneewise_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            ShipmentCustSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ShipmentCustSummary();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Blanket Order No.")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Name")));
                        obj.PlannedDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Planned Date")));
                        obj.count = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Counter")));
                        obj.qty = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Qty")));
                        obj.Consigneename = Convert.ToString(reader.GetValue(reader.GetOrdinal("Consignee Name")));
                        obj.OrderDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("OrderDate")));    //Added By Vishal                  
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

        public static List<ShipmentCustSummary> ListShipmentItem(String Code, String ItemNo)
        {
            string strSQL = string.Empty;
            List<ShipmentCustSummary> list = new List<ShipmentCustSummary>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Itemwise_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            ShipmentCustSummary obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ShipmentCustSummary();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Blanket Order No.")));
                        obj.Name1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Name")));
                        obj.PlannedDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Planned Date")));                        
                        obj.qty = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Qty")));
                        obj.OrderDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("OrderDate")));
                        obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM")));
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
