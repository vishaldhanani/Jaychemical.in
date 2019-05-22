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
   public class OrderInfo
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
       
        private String m_PhoneNo;
        public String PhoneNo
        {
            get { return m_PhoneNo; }
            set { m_PhoneNo = value; }
        }

        private String m_ConName;
        public String ConName
        {
            get { return m_ConName; }
            set { m_ConName = value; }
        }

        private String m_ConAddress1;
        public String ConAddress1
        {
            get { return m_ConAddress1; }
            set { m_ConAddress1 = value; }
        }

        private String m_ConAddress2;
        public String ConAddress2
        {
            get { return m_ConAddress2; }
            set { m_ConAddress2 = value; }
        }

        private String m_ConCity;
        public String ConCity
        {
            get { return m_ConCity; }
            set { m_ConCity = value; }
        }
       

        private String m_ConPhoneNo;
        public String ConPhoneNo
        {
            get { return m_ConPhoneNo; }
            set { m_ConPhoneNo = value; }
        }

        public static List<OrderInfo> List(string id)
        {
            string strSQL = string.Empty;
            List<OrderInfo> list = new List<OrderInfo>();
            SqlDataReader reader;
            strSQL = "SP_WA_Customer_Details";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            OrderInfo obj = null;
            try
            {
                db.AddInParameter(dbCommand, "@Code", DbType.String, id);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Customer Details
                        obj = new OrderInfo();
                        obj.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        obj.Address1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Address")));
                        obj.Address2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("Address 2")));
                        obj.City = Convert.ToString(reader.GetValue(reader.GetOrdinal("City")));
                       // obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Post Code")));
                        obj.PhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("ContactNo")));

                        // Consignee Details
                        obj.ConName = Convert.ToString(reader.GetValue(reader.GetOrdinal("consiName")));
                        obj.ConAddress1 = Convert.ToString(reader.GetValue(reader.GetOrdinal("consiAddress")));
                        obj.ConAddress2 = Convert.ToString(reader.GetValue(reader.GetOrdinal("consiAddress2")));
                        obj.ConCity = Convert.ToString(reader.GetValue(reader.GetOrdinal("consiCity")));
                        // obj.PostCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("Post Code")));
                        obj.ConPhoneNo = Convert.ToString(reader.GetValue(reader.GetOrdinal("consiContactNo")));

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


