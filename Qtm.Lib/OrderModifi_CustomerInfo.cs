using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections;

namespace Qtm.Lib
{
    public class OrderModifiCustomerInfo
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

        private String m_Address1;
        public String Address1
        {
            get { return m_Address1; }
            set { m_Address1 = value; }
        }

        private String m_Address2;
        public String Address2
        {
            get { return m_Address2; }
            set { m_Address2 = value; }
        }

        private String m_City;
        public String City
        {
            get { return m_City; }
            set { m_City = value; }
        }

        private String m_PostCode;
        public String PostCode
        {
            get { return m_PostCode; }
            set { m_PostCode = value; }
        }

        private String m_PhoneNo;
        public String PhoneNo
        {
            get { return m_PhoneNo; }
            set { m_PhoneNo = value; }
        }

        private Decimal m_Quantity;

        public Decimal Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }
        private Decimal m_UnitPrice;

        public Decimal UnitPrice
        {
            get { return m_UnitPrice; }
            set { m_UnitPrice = value; }
        }

        private Decimal m_Amount;

        public Decimal Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        private String m_Description;

        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private String m_LineNo;

        public String LineNo
        {
            get { return m_LineNo; }
            set { m_LineNo = value; }
        }

        private String m_ItemNo;

        public String ItemNo
        {
            get { return m_ItemNo; }
            set { m_ItemNo = value; }
        }

        private String m_VariantCode;

        public String VariantCode
        {
            get { return m_VariantCode; }
            set { m_VariantCode = value; }
        }

        private Decimal m_Discount;

        public Decimal Discount
        {
            get { return m_Discount; }
            set { m_Discount = value; }
        }


        private String m_Remark;

        public String Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }

        private String m_Cust_Ref_No;

        public String Cust_Ref_No
        {
            get { return m_Cust_Ref_No; }
            set { m_Cust_Ref_No = value; }
        }

        private String m_LocationCode;

        public String LocationCode
        {
            get { return m_LocationCode; }
            set { m_LocationCode = value; }
        }

        private String m_UOM;

        public String UOM
        {
            get { return m_UOM; }
            set { m_UOM = value; }
        }

        private Decimal m_OutStandingQty;

        public Decimal OutStandingQty
        {
            get { return m_OutStandingQty; }
            set { m_OutStandingQty = value; }
        }

        private String m_ExciseGroupCode;

        public String ExciseGroupCode
        {
            get { return m_ExciseGroupCode; }
            set { m_ExciseGroupCode = value; }
        }

        private Decimal m_QuantityShipped;

        public Decimal QuantityShipped
        {
            get { return m_QuantityShipped; }
            set { m_QuantityShipped = value; }
        }

        private String m_TaxGroupCode;

        public String TaxGroupCode
        {
            get { return m_TaxGroupCode; }
            set { m_TaxGroupCode = value; }
        }

        private String m_Consignee;

        public String Consignee
        {
            get { return m_Consignee; }
            set { m_Consignee = value; }
        }

        private String m_ShiptoCode;

        public String ShiptoCode
        {
            get { return m_ShiptoCode; }
            set { m_ShiptoCode = value; }
        }
        private String m_SelltoCustomerNo;

        public String SelltoCustomerNo
        {
            get { return m_SelltoCustomerNo; }
            set { m_SelltoCustomerNo = value; }
        }

        private String m_ItemCategoryCode;

        public String ItemCategoryCode
        {
            get { return m_ItemCategoryCode; }
            set { m_ItemCategoryCode = value; }
        }

        private String m_CustomerPriceGroup;

        public String CustomerPriceGroup
        {
            get { return m_CustomerPriceGroup; }
            set { m_CustomerPriceGroup = value; }
        }

        private String m_Destination;

        public String Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        public static OrderModifiCustomerInfo FindCustomerForBlanketOrder(string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderModification_HeaderInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                // db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Customer No_")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Contact")));
                        obj.Cust_Ref_No = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Order No")));
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
            return obj;
        }

        public static OrderModifiCustomerInfo FetchDestination(string ShipToCode, string CustomerCode, string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_Destination";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ShipToCode", DbType.String, ShipToCode);
                db.AddInParameter(dbCommand, "@CustomerCode", DbType.String, CustomerCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Destination = Convert.ToString(reader.GetValue(reader.GetOrdinal("Destination")));
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
            return obj;
        }

        public static OrderModifiCustomerInfo Find(string ItemCategoryCode, string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_HeaderInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Customer No_")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Contact")));
                        obj.Cust_Ref_No = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Order No")));
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
            return obj;
        }

        public static OrderModifiCustomerInfo FindConsigneeForBlanketOrder(string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderModification_HeaderInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                // db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Contact")));
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
            return obj;
        }

        public static OrderModifiCustomerInfo FindConsignee(string ItemCategoryCode, string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_HeaderInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Contact")));
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
            return obj;
        }

        public static List<OrderModifiCustomerInfo> BlanketOrderList(string OrderNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderModification_LineInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                //db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Quantity = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Quantity"))));
                        obj.UnitPrice = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Unit Price"))));
                        obj.Amount = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.LineNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("LineNo")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.Remark = Convert.ToString(reader.GetValue(reader.GetOrdinal("Remarks")));
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Discount")));
                        obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM")));
                        obj.OutStandingQty = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("OutStandingQty"))));
                        obj.ItemCategoryCode = (Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemCategoryCode"))));
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


        public static List<OrderModifiCustomerInfo> BlanketListFor_EditItem(string OrderNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_BlanketOrderModification_LineInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                //db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Quantity = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Quantity"))));
                        obj.UnitPrice = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Unit Price"))));
                        obj.Amount = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.LineNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("LineNo")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.Remark = Convert.ToString(reader.GetValue(reader.GetOrdinal("Remarks")));
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Discount")));
                        obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM")));
                        obj.OutStandingQty = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("OutStandingQty"))));                        
                        obj.ItemCategoryCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemCategoryCode"))); //added by vishal: 06-09-2016
                        obj.SelltoCustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("SelltoCustomerNo"))); //added by vishal: 06-09-2016
                        obj.ShiptoCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ShiptoCode"))); //added by vishal: 06-09-2016
                        obj.CustomerPriceGroup = Convert.ToString(reader.GetValue(reader.GetOrdinal("CustomerPriceGroup"))); //added by vishal: 06-09-2016
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


        public static List<OrderModifiCustomerInfo> List(string ItemCategoryCode, string OrderNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_LineInfomation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ItemCategoryCode);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Quantity = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Quantity"))));
                        obj.UnitPrice = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Unit Price"))));
                        obj.Amount = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.LineNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("LineNo")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.Remark = Convert.ToString(reader.GetValue(reader.GetOrdinal("Remarks")));
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Discount")));
                        obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM")));
                        obj.OutStandingQty = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("OutStandingQty"))));
                        obj.QuantityShipped = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("QuantityShipped"))));
                        obj.ItemCategoryCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemCategoryCode"))); //added by vishal: 06-09-2016
                        obj.SelltoCustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("SelltoCustomerNo"))); //added by vishal: 06-09-2016
                        obj.ShiptoCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ShiptoCode"))); //added by vishal: 06-09-2016
                        obj.CustomerPriceGroup = Convert.ToString(reader.GetValue(reader.GetOrdinal("CustomerPriceGroup"))); //added by vishal: 06-09-2016
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

        public static List<OrderModifiCustomerInfo> ListDeleteItemLine(string OrderNo, string ItemNo, string LineNo, string BlanketOrderNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            strSQL = "SP_WA_OrderModification_LineDelete";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                db.AddInParameter(dbCommand, "@LineNo", DbType.String, LineNo);
                db.AddInParameter(dbCommand, "@BlanketOrderNo", DbType.String, BlanketOrderNo);

                db.ExecuteNonQuery(dbCommand);
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

        public static List<OrderModifiCustomerInfo> List_LineUpdate(string ItemNo, string OrderNo, string LineNo, decimal Qty, decimal UnitPrice, decimal Amount, string VariantCode, string Remark)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();

            strSQL = "SP_WA_OrderModification_LineUpdate";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                db.AddInParameter(dbCommand, "@LineNo", DbType.String, LineNo);
                db.AddInParameter(dbCommand, "@Qty", DbType.String, Qty);
                db.AddInParameter(dbCommand, "@UnitPrice", DbType.String, UnitPrice);
                db.AddInParameter(dbCommand, "@Amount", DbType.String, Amount);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, VariantCode);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, Remark);
                db.ExecuteNonQuery(dbCommand);

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

        public static List<OrderModifiCustomerInfo> InternalList(string OrderNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_LineInfomation_Internal";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Quantity = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Quantity"))));
                        obj.UnitPrice = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Unit Price"))));
                        obj.Amount = Math.Round(Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount"))));
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.LineNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("LineNo")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.Remark = Convert.ToString(reader.GetValue(reader.GetOrdinal("Remarks")));
                        obj.LocationCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("LocationCode")));
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Discount")));
                        obj.ExciseGroupCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ExciseGroupCode")));
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

        public static List<OrderModifiCustomerInfo> ExciseGroupCodeList(string OrderNo, string ItemNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_ExciseGroupcode_Linewise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.ExciseGroupCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ExciseGroupCode")));
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


        public static List<OrderModifiCustomerInfo> TaxGroupCodeList(string OrderNo, string ItemNo)
        {
            string strSQL = string.Empty;
            List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_TaxGroupCode_Linewise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ItemNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.TaxGroupCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("TaxGroupCode")));
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


        public static OrderModifiCustomerInfo InternalFind(string OrderNo)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_OrderModification_HeaderInfomation_Internal";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderModifiCustomerInfo obj = null;
            try
            {

                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderModifiCustomerInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Customer No_")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Bill-to Contact")));
                        obj.Consignee = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Name")));
                        obj.ShiptoCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ship-to Code")));
                        obj.SelltoCustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Sell-to Customer No_")));
                       
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
            return obj;
        }

    }
}
