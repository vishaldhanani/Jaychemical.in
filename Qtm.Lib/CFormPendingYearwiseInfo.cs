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
   public class CFormPendingYearwiseInfo
    {
        private String m_Yearwise;
        public String Yearwise
        {
            get { return m_Yearwise; }
            set { m_Yearwise = value; }
        }
       
        private Decimal m_Amount;
        public Decimal Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        private DateTime m_StartDate;
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
        private DateTime m_EndDate;
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        public static List<CFormPendingYearwiseInfo> CformYearwise(string Code)
        {
            string strSQL = string.Empty;
            List<CFormPendingYearwiseInfo> list = new List<CFormPendingYearwiseInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_CForm_Pending_Yearwise_Summary";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerNo", DbType.String, Code);

                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CFormPendingYearwiseInfo obj = new CFormPendingYearwiseInfo();
                        obj.Yearwise = Convert.ToString(reader.GetValue(reader.GetOrdinal("Yearwise")));
                        obj.Amount = System.Math.Round(Convert.ToDecimal((reader.GetValue(reader.GetOrdinal("Amt"))))); 
                        obj.StartDate = (Convert.ToDateTime((reader.GetValue(reader.GetOrdinal("Start Date"))))); 
                        obj.EndDate = (Convert.ToDateTime((reader.GetValue(reader.GetOrdinal("End Date"))))); 
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
