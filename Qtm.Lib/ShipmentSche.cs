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
    public class ShipmentSche
    {
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        
        private decimal m_qty;
        public decimal qty
        {
            get { return m_qty; }
            set { m_qty = value; }
        }
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private string  m_itemno;
        public string itemno
        {
            get { return m_itemno; }
            set { m_itemno = value; }
        }

        private Decimal m_qty1;
        public Decimal qty1
        {
            get { return m_qty1; }
            set { m_qty1 = value; }
        }
        private String m_Description;
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        public static List<ShipmentSche> Customerwise(string Code)
        {
            string strSQL = string.Empty;
            List<ShipmentSche> list = new List<ShipmentSche>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Customerwise";
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
                        ShipmentSche obj = new ShipmentSche();
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Qty")))));
                        
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

        public static List<ShipmentSche> Consigneewise(string Code)
        {
            string strSQL = string.Empty;
            List<ShipmentSche> list = new List<ShipmentSche>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Consigneewise";
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
                        ShipmentSche obj = new ShipmentSche();
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.qty = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Qty")))));

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

        public static List<ShipmentSche> Itemwise(string Code)
        {
            string strSQL = string.Empty;
            List<ShipmentSche> list = new List<ShipmentSche>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentSchedule_Itemwise";
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
                        ShipmentSche obj = new ShipmentSche();
                        obj.Description = Convert.ToString(reader.GetValue(reader.GetOrdinal("Description")));

                        obj.itemno = Convert.ToString(reader.GetValue(reader.GetOrdinal("Item")));
                        obj.qty1 = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Qty")))));
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
