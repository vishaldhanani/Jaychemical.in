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
    public class ConsigneeInfo
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

        public static ConsigneeInfo Find(string id,string customercode)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_FindConsigneeByCode_SP";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            ConsigneeInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@ConsigneeCode", DbType.String, id);
                db.AddInParameter(dbCommand, "@CustCode", DbType.String, customercode);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ConsigneeInfo();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("City")));
                        obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("Phone No_")));
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
