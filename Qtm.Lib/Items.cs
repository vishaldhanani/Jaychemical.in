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
    public class Items
    {
        private String m_ItemNo;
        public String ItemNo
        {
            get { return m_ItemNo; }
            set { m_ItemNo = value; }
        }
        private String m_MainItemNo;
        public String MainItemNo
        {
            get { return m_MainItemNo; }
            set { m_MainItemNo = value; }
        }
        private DateTime m_EndingDate;
        public DateTime EndingDate
        {
            get { return m_EndingDate; }
            set { m_EndingDate = value; }
        }

        private String m_Description;
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private String m_ItemColour;
        public String ItemColour
        {
            get { return m_ItemColour; }
            set { m_ItemColour = value; }
        }

        private String m_VariantCode;
        public String VariantCode
        {
            get { return m_VariantCode; }
            set { m_VariantCode = value; }
        }

        private String m_UnitPrice;
        public String UnitPrice
        {
            get { return m_UnitPrice; }
            set { m_UnitPrice = value; }
        }

        private String m_UOMCode;
        public String UOMCode
        {
            get { return m_UOMCode; }
            set { m_UOMCode = value; }
        }

        private decimal m_DicPercentage;
        public decimal DicPercentage
        {
            get { return m_DicPercentage; }
            set { m_DicPercentage = value; }
        }

        private Int32 m_PageCount;
        public Int32 PageCount
        {
            get { return m_PageCount; }
            set { m_PageCount = value; }
        }

        private String m_ProdCateGory;

        public String ProdCateGory
        {
            get { return m_ProdCateGory; }
            set { m_ProdCateGory = value; }
        }

        private String m_Range;

        public String Range
        {
            get { return m_Range; }
            set { m_Range = value; }
        }

        private String m_SubCateGory;

        public String SubCateGory
        {
            get { return m_SubCateGory; }
            set { m_SubCateGory = value; }
        }

        private String m_ProdGroupCode;

        public String ProdGroupCode
        {
            get { return m_ProdGroupCode; }
            set { m_ProdGroupCode = value; }
        }

        public static Items Find(string ItemNo, string CustPriGrp, String color, string SplCustPricGrp, string CustDisPercentage)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_FindItems";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            Items obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ItemNO", DbType.String, ItemNo);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, SplCustPricGrp);
                db.AddInParameter(dbCommand, "@DiscountGroup", DbType.String, CustDisPercentage);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new Items();
                        obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.UOMCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM_Code")));
                        obj.DicPercentage = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscPercentage")));

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

        public static List<Items> List(string CustPriGrp, string ProductCode, String color)
        {
            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemList";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item No_")));
                        obj.MainItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item")));
                        obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.UOMCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM_Code")));
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

        public static ArrayList ListArray(string CustPriGrp, string ProductCode, String color)
        {
            string strSQL = string.Empty;
            ArrayList list = new ArrayList();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemList";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item No_")));
                        obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.DicPercentage = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscPercentage")));
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

        public static DataTable GetSuggestedProducts(string SearchedTxt, string CustPriGrp, string ProductCode, String color, string SplCustPriceGrp)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_GetSuggestedProducts_SP";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                db.AddInParameter(dbCommand, "@Desc", DbType.String, SearchedTxt);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);
                db.AddInParameter(dbCommand, "@SplCustPriceGrp", DbType.String, SplCustPriceGrp);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                // found the problem of reader close.. - Raj Shah - 04/07/2016               
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {

                //    }
                //}

                DataTable dt = new DataTable();
                dt.Load(reader);

                if (!reader.IsClosed)
                    reader.Close();

                return dt;
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
        }

        public static List<Items> LoadScrollWiseItemProducts(string CustPriGrp, string ProductCode, string SplCustPricGrp, String color, Int32 PageIndex, string CustDiscPercentage)
        {
            Int32 pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            Int32 pageCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageCount"]);


            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_LoadScrollWiseItemProducts";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, SplCustPricGrp);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);
                db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                db.AddOutParameter(dbCommand, "@PageCount", DbType.Int32, pageCount);
                db.AddInParameter(dbCommand, "@DiscountGroup", DbType.String, CustDiscPercentage);


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item No_")));
                        obj.MainItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item")));
                        // Not required
                        //  obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.UOMCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM_Code")));
                        obj.DicPercentage = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscPercentage")));
                        obj.PageCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "@PageCount"));
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

        //SP_WA_LoadScrollWiseItemProducts_CategoryWise

        public static List<Items> LoadScrollWiseItemProducts_CategoryWise(string CustPriGrp, string ProductCode, string SplCustPricGrp, String color, Int32 PageIndex, string CustDiscPercentage, string ProdGroupCode)
        {
            Int32 pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            Int32 pageCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageCount"]);


            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_LoadScrollWiseItemProducts_CategoryWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, SplCustPricGrp);
                db.AddInParameter(dbCommand, "@ItemColour", DbType.String, color);
                db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                db.AddOutParameter(dbCommand, "@PageCount", DbType.Int32, pageCount);
                db.AddInParameter(dbCommand, "@DiscountGroup", DbType.String, CustDiscPercentage);
                db.AddInParameter(dbCommand, "@ProductGroupCode", DbType.String, ProdGroupCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item No_")));
                        obj.MainItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item")));
                        // Not required
                        //  obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.UOMCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM_Code")));
                        obj.DicPercentage = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscPercentage")));
                        obj.PageCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "@PageCount"));
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

        public static List<Items> ItemsFindByDescriptionAndProductCode(string CustPriGrp, string ProductCode, String Desc, string SplCustPricGrp, string CustDisPercentage)
        {
            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemsFindByDescriptionAndProductCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                //db.AddInParameter(dbCommand, "@Code", DbType.String, Code);
                db.AddInParameter(dbCommand, "@SalesCode", DbType.String, CustPriGrp);
                db.AddInParameter(dbCommand, "@SplCustPricGrp", DbType.String, SplCustPricGrp);
                db.AddInParameter(dbCommand, "@ProductGroup", DbType.String, ProductCode);
                db.AddInParameter(dbCommand, "@Desc", DbType.String, Desc);
                db.AddInParameter(dbCommand, "@DiscountGroup", DbType.String, CustDisPercentage);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item No_")));
                        obj.MainItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item")));
                        obj.EndingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Ending Date")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.ItemColour = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Colour")));
                        obj.VariantCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        obj.UnitPrice = Convert.ToString(reader.GetValue(reader.GetOrdinal("UnitPrice")));
                        obj.UOMCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("UOM_Code")));
                        obj.DicPercentage = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscPercentage")));
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

        public static List<Items> SecondarySalesItemList(string SplPriGrp)
        {
            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_SecondarySalesItemList";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SplPriGrp", DbType.String, SplPriGrp);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
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

        public static List<Items> GetProdCategoryAndRange(string ProductCode)
        {
            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_ItemBasedCategoryAndRange";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemNo", DbType.String, ProductCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.ItemNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemNo")));
                        obj.ProdCateGory = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item Category Code")));
                        obj.Range = Convert.ToString(reader.GetValue(reader.GetOrdinal("Product Sub Category")));
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

        public static List<Items> GetSubCateGory(string ProdCateGory)
        {
            string strSQL = string.Empty;
            List<Items> list = new List<Items>();
            SqlDataReader reader;
            strSQL = "SP_WA_ProductSubCategory";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ItemCategoryCode", DbType.String, ProdCateGory);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Items obj = new Items();
                        obj.SubCateGory = Convert.ToString(reader.GetValue(reader.GetOrdinal("ProductSubCategory")));
                        obj.ProdCateGory = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemCategoryCode")));
                        obj.ProdGroupCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
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
