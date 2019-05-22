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
    public class ProductCat
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

        private String m_NoSeries;

        public String NoSeries
        {
            get { return m_NoSeries; }
            set { m_NoSeries = value; }
        }

        private String m_Structure;

        public String Structure
        {
            get { return m_Structure; }
            set { m_Structure = value; }
        }

        private String m_ProductSubCategory;

        public String ProductSubCategory
        {
            get { return m_ProductSubCategory; }
            set { m_ProductSubCategory = value; }
        }

        public static List<ProductCat> List(String Code)
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_Product_Category";
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
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));                        
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

        public static List<ProductCat> ListProductCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_InternalProductCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListAcclocCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_InternalAcclocCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListNoseriesCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_InternalNoSeriesCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListNoseriesCodeLocationWiseData(string LocationCode)
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_InternalNoSeriesCode_LocationWise";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@LocationCode", DbType.String, LocationCode);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.NoSeries = Convert.ToString(reader.GetValue(reader.GetOrdinal("NoSeries")));                       
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

        public static List<ProductCat> SelectDefaultStructure(string LocationCode)
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_DefaultStructure";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@LocationCode", DbType.String, LocationCode);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Structure = Convert.ToString(reader.GetValue(reader.GetOrdinal("Structure")));
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


        public static List<ProductCat> ListStructureCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_InternalStructureCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListLocationCode(string ShiptoCode, string UserName, string SelltoCustomerNo)
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_LocationList";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@ShipToCode", DbType.String, ShiptoCode);
                db.AddInParameter(dbCommand, "@UserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, SelltoCustomerNo);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> DefaultLocationCode(string OrderNo)
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_DefalutLocation";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {

                db.AddInParameter(dbCommand, "@OrderNo", DbType.String, OrderNo);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListFormCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_FormCodeList";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListTaxAreaCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_TaxAreaCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {


                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListTextGroupCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_TaxGroupCode";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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

        public static List<ProductCat> ListExcisePostingGroupCode()
        {
            string strSQL = string.Empty;
            List<ProductCat> list = new List<ProductCat>();
            SqlDataReader reader;
            strSQL = "SP_WA_Excise_Prod_PostingGroup";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductCat obj = new ProductCat();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
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
