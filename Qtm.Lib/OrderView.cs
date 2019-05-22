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
   public class OrderView
    {
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        private String m_Description;
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private Decimal m_Quantity;
        public Decimal Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        private String m_UOM;
        public String UOM
        {
            get { return m_UOM; }
            set { m_UOM = value; }
        }
        private String m_Variant_Code;
        public String Variant_Code
        {
            get { return m_Variant_Code; }
            set { m_Variant_Code = value; }
        }

        private Decimal m_Unit_Price;
        public Decimal Unit_Price
        {
            get { return m_Unit_Price; }
            set { m_Unit_Price = value; }
        }
        private Decimal m_Discount;
        public Decimal Discount
        {
            get { return m_Discount; }
            set { m_Discount = value; }
        }

        private Decimal m_Unit_Cost;
        public Decimal Unit_Cost
        {
            get { return m_Unit_Cost; }
            set { m_Unit_Cost = value; }
        }

        public static List<OrderView> List(string id)
        {
            string strSQL = string.Empty;
            List<OrderView> list1 = new List<OrderView>();
            SqlDataReader reader;
            strSQL = "SP_WA_GetSalesLine_OrderConfiView";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderView obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@Code", DbType.String, id);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new OrderView();
                        // obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("No_")));
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));
                        obj.Quantity = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Quantity")));
                       // obj.UOM = Convert.ToString(reader.GetValue(reader.GetOrdinal("Unit of Measure")));  //Changed By Vishal-16-09-2015
                        obj.Variant_Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Variant Code")));
                        // obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Post Code")));
                        obj.Unit_Price = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Line Amount")));
                        obj.Discount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DiscountPerc")));
                        obj.Unit_Cost = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Unit Cost (LCY)")));
                        list1.Add(obj);
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
            return list1;
        }
    }
}
