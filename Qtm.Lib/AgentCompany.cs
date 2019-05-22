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
    public class AgentCompanies
    {
        private String m_AgentSubType;
        public String AgentSubType
        {
            get { return m_AgentSubType; }
            set { m_AgentSubType = value; }
        }
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private int m_ConsigneeCounter;
        public int ConsigneeCounter
        {
            get { return m_ConsigneeCounter; }
            set { m_ConsigneeCounter = value; }
        }
        private int m_AgentCompaniesCounter;
        public int AgentCompaniesCounter
        {
            get { return m_AgentCompaniesCounter; }
            set { m_AgentCompaniesCounter = value; }
        }
        private String m_CustomerPriceGrp;
        public String CustomerPriceGrp
        {
            get { return m_CustomerPriceGrp; }
            set { m_CustomerPriceGrp = value; }
        }

        private String m_SplCustPriceGrp;
        public String SplCustPriceGrp
        {
            get { return m_SplCustPriceGrp; }
            set { m_SplCustPriceGrp = value; }
        }
        private String m_DiscPriceGrp;
        public String DiscPriceGrp
        {
            get { return m_DiscPriceGrp; }
            set { m_DiscPriceGrp = value; }
        }
        public static List<AgentCompanies> List(string Code)
        {
            string strSQL = string.Empty;
            List<AgentCompanies> list = new List<AgentCompanies>();
            SqlDataReader reader;
            strSQL = "SP_WA_AgentCompanies";
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
                        AgentCompanies obj = new AgentCompanies();
                        obj.AgentSubType = Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent SubType")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.ConsigneeCounter = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ConsigneeCounter")));
                        obj.AgentCompaniesCounter = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("AgentCompaniesCounter")));
                        obj.CustomerPriceGrp = Convert.ToString(reader.GetValue(reader.GetOrdinal("Customer Price Group")));
                        obj.SplCustPriceGrp = Convert.ToString(reader.GetValue(reader.GetOrdinal("DefaultCustomerPriceGroup")));
                        obj.DiscPriceGrp = Convert.ToString(reader.GetValue(reader.GetOrdinal("CustomerDiscountGroup")));       
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
