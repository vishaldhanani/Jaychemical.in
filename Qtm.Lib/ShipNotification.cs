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
    public class ShipNotification
    {
        private String m_ShipmentNo;
        public String ShipmentNo
        {
            get { return m_ShipmentNo; }
            set { m_ShipmentNo = value; }
        }
        
        private String m_VehicleNo;
        public String VehicleNo
        {
            get { return m_VehicleNo; }
            set { m_VehicleNo = value; }
        }

        private DateTime m_PostingDate;
        public DateTime PostingDate
        {
            get { return m_PostingDate; }
            set { m_PostingDate = value; }
        }

        private String m_StationFromTo;
        public String StationFromTo
        {
            get { return m_StationFromTo; }
            set { m_StationFromTo = value; }
        }

        public static List<ShipNotification> List(string Code)
        {
            string strSQL = string.Empty;
            List<ShipNotification> list = new List<ShipNotification>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentNotifcation";
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
                        ShipNotification obj = new ShipNotification();
                        obj.ShipmentNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No_")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.VehicleNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Vehicle No_")));
                        obj.StationFromTo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Station From_To")));
                        
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

        public static List<ShipNotification> Find(string Code, string AgentCode)
        {
            string strSQL = string.Empty;
            List<ShipNotification> list = new List<ShipNotification>();
            SqlDataReader reader;
            strSQL = "SP_WA_ShipmentNotifcation_Find";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@SearchCode", DbType.String, Code);
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShipNotification obj = new ShipNotification();
                        obj.ShipmentNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("No_")));
                        obj.PostingDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Posting Date")));
                        obj.VehicleNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Vehicle No_")));
                        obj.StationFromTo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Station From_To")));

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
