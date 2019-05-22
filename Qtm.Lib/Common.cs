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
    public class UOM
    {
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        public static List<UOM> List(string Code)
        {
            string strSQL = string.Empty;
            List<UOM> list = new List<UOM>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemUnitOfMeasure";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, Code);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UOM obj = new UOM();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
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

    public class Variant
    {
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        private int m_Quantity;
        public int Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }
        private decimal m_Price;
        public decimal Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }

        private string m_UMO;
        public string UMO
        {
            get { return m_UMO; }
            set { m_UMO = value; }
        }

        private decimal m_Discount;

        public decimal Discount
        {
            get { return m_Discount; }
            set { m_Discount = value; }
        }

        private Boolean m_BoolConsumerPrice;

        public Boolean BoolConsumerPrice
        {
            get { return m_BoolConsumerPrice; }
            set { m_BoolConsumerPrice = value; }
        }

        private String m_SpecialPriceGrp;

        public String SpecialPriceGrp
        {
            get { return m_SpecialPriceGrp; }
            set { m_SpecialPriceGrp = value; }
        }

        public static List<Variant> List(string Code, string custpricegroup, string splpricegroup, string vcode)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {

                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, Code);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, custpricegroup);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, splpricegroup);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, vcode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Quantity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Quantity")));
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

        public static List<Variant> GetUMOforDyes(string productCode, string variantcode, string custpricegroup, string splpricegroup)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Price_UOM_Dyes";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, productCode);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, variantcode);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, custpricegroup);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, splpricegroup);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.UMO = (Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM"))));
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

        public static List<Variant> GetConsumerBool(string AgentCode, string CustomerId)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_AgentCustomer_ConsumerPrice";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@CustomerId", DbType.String, CustomerId);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.BoolConsumerPrice = Convert.ToBoolean(reader.GetValue(reader.GetOrdinal("ShowConsumerPrice")));
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

        public static List<Variant> ListDiscount(string AgentCode, string AgentSubType, string LinkedCustomerNo, string CustPriceGroup)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_Agentwise_Discount_Dyes";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {

                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                db.AddInParameter(dbCommand, "@AgentSubType", DbType.String, AgentSubType);
                db.AddInParameter(dbCommand, "@LinkedCustomerNo", DbType.String, LinkedCustomerNo);
                db.AddInParameter(dbCommand, "@CustPriceGroup", DbType.String, CustPriceGroup);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Discount")));
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

        public static List<Variant> GetConsumerPrice_Dyes(string productCode, string variantcode)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_ConsumerPrice_Dye";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, productCode);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, variantcode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Price = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("UnitPrice"))));
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

        public static List<Variant> VariantList_Dyes(string Code)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Dyes";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, Code);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Quantity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Quantity")));
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

        public static List<Variant> VariantList_Quantity_dyes(string Code, string VariantCode)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Quantity_Dyes";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, Code);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, VariantCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Quantity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Quantity")));
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

        public static List<Variant> ListItemModification(string Code)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_ItemModification";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {

                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, Code);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Quantity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Quantity")));
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

        public static List<Variant> GetPrice(string productCode, string variantcode, string custpricegroup, string splpricegroup)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Price_";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, productCode);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, variantcode);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, custpricegroup);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, splpricegroup);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Price = (Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("UnitPrice"))));
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

        public static List<Variant> GetPrice_Dyes(string productCode, string variantcode, string custpricegroup, string splpricegroup, string Customer, string Consignee)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Price_Dye";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, productCode);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, variantcode);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, custpricegroup);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, splpricegroup);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, Customer);
                db.AddInParameter(dbCommand, "@Consignee", DbType.String, Consignee);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.Price = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("UnitPrice")));
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

        public static List<Variant> GetSpecialPriceGrp(string Customer)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_GetSpecialCustPriceGroupFromCustomerCard";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Customer);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.SpecialPriceGrp = Convert.ToString(reader.GetValue(reader.GetOrdinal("SpecialPriceGroup")));
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


        public static List<Variant> GetUMO(string productCode, string variantcode, string custpricegroup, string splpricegroup)
        {
            string strSQL = string.Empty;
            List<Variant> list = new List<Variant>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemVariant_Price_UOM_";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, productCode);
                db.AddInParameter(dbCommand, "@VariantCode", DbType.String, variantcode);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, custpricegroup);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, splpricegroup);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Variant obj = new Variant();
                        obj.UMO = (Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM"))));
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
