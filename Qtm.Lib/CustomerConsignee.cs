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
    public class CustomerConsignee
    {
        private String m_CustomerNo;
        public String CustomerNo
        {
            get { return m_CustomerNo; }
            set { m_CustomerNo = value; }
        }
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private String m_CustomerPriceGrp;
        public String CustomerPriceGrp
        {
            get { return m_CustomerPriceGrp; }
            set { m_CustomerPriceGrp = value; }
        }

        public static List<CustomerConsignee> ConsigneeList(string Code)
        {
            string strSQL = string.Empty;
            List<CustomerConsignee> list = new List<CustomerConsignee>();
            SqlDataReader reader;
            strSQL = "SP_WA_CustomerConsignee";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                db.AddInParameter(dbCommand, "@CustomerCode", DbType.String, Code);
                
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerConsignee obj = new CustomerConsignee();
                        obj.CustomerNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
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
