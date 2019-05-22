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
    public class Agent
    {
        
        private String m_Code;
        public String Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        private String m_AgentName;
        public String AgentName
        {
            get { return m_AgentName; }
            set { m_AgentName = value; }
        }
        private String m_AgentAddress;
        public String AgentAddress
        {
            get { return m_AgentAddress; }
            set { m_AgentAddress = value; }
        }
       
        private Int32 m_Status;
        public Int32 Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        private String m_UserName;
        public String UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        private String m_Password;
        public String Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        private String m_ItemCategoryCode;

        public String ItemCategoryCode
        {
            get { return m_ItemCategoryCode; }
            set { m_ItemCategoryCode = value; }
        }
        private DateTime m_LastLoginTime;

        public DateTime LastLoginTime
        {
            get { return m_LastLoginTime; }
            set { m_LastLoginTime = value; }
        }
        private String m_UserType;

        public String UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }

        private Boolean m_AccountActivated;

        public Boolean AccountActivated
        {
            get { return m_AccountActivated; }
            set { m_AccountActivated = value; }
        }

        public static int Add(Agent obj)
        {
            int result = 0;
            const string strSql = "AddAgent_sp";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSql);
            try
            {
                //db.AddInParameter(dbCommand,"timestamp",DbType.Time,obj.timestamp);
                //db.AddInParameter(dbCommand,"Code",DbType.String,obj.Code);
                //db.AddInParameter(dbCommand,"Agent Name",DbType.String,obj.Agent Name);
                //db.AddInParameter(dbCommand,"Agent Address",DbType.String,obj.Agent Address);
                //db.AddInParameter(dbCommand,"Agent Calc_ Base",DbType.Int32,obj.Agent Calc_ Base);
                //db.AddInParameter(dbCommand,"Commision Type",DbType.Int32,obj.Commision Type);
                //db.AddInParameter(dbCommand,"Agent Calc_ Rate",DbType.Decimal,obj.Agent Calc_ Rate);
                //db.AddInParameter(dbCommand,"Agent Currency",DbType.String,obj.Agent Currency);
                //db.AddInParameter(dbCommand,"Agent Price List",DbType.String,obj.Agent Price List);
                //db.AddInParameter(dbCommand,"Agent Special Price List",DbType.String,obj.Agent Special Price List);
                //db.AddInParameter(dbCommand,"Agent Type",DbType.Int32,obj.Agent Type);
                //db.AddInParameter(dbCommand,"Agent Code",DbType.String,obj.Agent Code);
                db.AddInParameter(dbCommand, "Status", DbType.Int32, obj.Status);
                db.AddInParameter(dbCommand, "UserName", DbType.String, obj.UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, obj.Password);

                db.ExecuteNonQuery(dbCommand);
                result = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Id"));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return result;
        }

        public static int Delete(string idString)
        {
            int result = 0;
            const string strSql = "DeleteAgent_sp";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSql);
            try
            {
                db.AddInParameter(dbCommand, "Ids", DbType.String, idString);
                result = db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return result;
        }

        public static Agent Find(int id)
        {
            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "FindAgent_sp";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            Agent obj = null;
            try
            {
                db.AddInParameter(dbCommand, "Id", DbType.Int32, id);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new Agent();
                        //obj.timestamp=Convert.ToTime(reader.GetValue(reader.GetOrdinal("timestamp")));
                        //obj.Code=Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        //obj.Agent Name=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Name")));
                        //obj.Agent Address=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Address")));
                        //obj.Agent Calc_ Base=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Agent Calc_ Base")));
                        //obj.Commision Type=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Commision Type")));
                        //obj.Agent Calc_ Rate=Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Agent Calc_ Rate")));
                        //obj.Agent Currency=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Currency")));
                        //obj.Agent Price List=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Price List")));
                        //obj.Agent Special Price List=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Special Price List")));
                        //obj.Agent Type=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Agent Type")));
                        //obj.Agent Code=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Code")));
                        obj.Status = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Status")));
                        obj.UserName = Convert.ToString(reader.GetValue(reader.GetOrdinal("UserName")));
                        obj.Password = Convert.ToString(reader.GetValue(reader.GetOrdinal("Password")));

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

        public int Update()
        {
            int result = 0;
            const string strSql = "UpdateAgent_sp";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSql);
            try
            {
                //db.AddInParameter(dbCommand,"timestamp",DbType.Time,this.timestamp);
                //db.AddInParameter(dbCommand,"Code",DbType.String,this.Code);
                //db.AddInParameter(dbCommand,"Agent Name",DbType.String,this.Agent Name);
                //db.AddInParameter(dbCommand,"Agent Address",DbType.String,this.Agent Address);
                //db.AddInParameter(dbCommand,"Agent Calc_ Base",DbType.Int32,this.Agent Calc_ Base);
                //db.AddInParameter(dbCommand,"Commision Type",DbType.Int32,this.Commision Type);
                //db.AddInParameter(dbCommand,"Agent Calc_ Rate",DbType.Decimal,this.Agent Calc_ Rate);
                //db.AddInParameter(dbCommand,"Agent Currency",DbType.String,this.Agent Currency);
                //db.AddInParameter(dbCommand,"Agent Price List",DbType.String,this.Agent Price List);
                //db.AddInParameter(dbCommand,"Agent Special Price List",DbType.String,this.Agent Special Price List);
                //db.AddInParameter(dbCommand,"Agent Type",DbType.Int32,this.Agent Type);
                //db.AddInParameter(dbCommand,"Agent Code",DbType.String,this.Agent Code);
                db.AddInParameter(dbCommand, "Status", DbType.Int32, this.Status);
                db.AddInParameter(dbCommand, "UserName", DbType.String, this.UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, this.Password);

                db.ExecuteNonQuery(dbCommand);
                result = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return result;
        }

        public static ArrayList List()
        {
            string strSQL = string.Empty;
            ArrayList list = new ArrayList();
            SqlDataReader reader;
            strSQL = "ListAgent_sp";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            try
            {
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Agent obj = new Agent();

                        //obj.timestamp=Convert.ToTime(reader.GetValue(reader.GetOrdinal("timestamp")));
                        //obj.Code=Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        //obj.Agent Name=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Name")));
                        //obj.Agent Address=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Address")));
                        //obj.Agent Calc_ Base=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Agent Calc_ Base")));
                        //obj.Commision Type=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Commision Type")));
                        //obj.Agent Calc_ Rate=Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Agent Calc_ Rate")));
                        //obj.Agent Currency=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Currency")));
                        //obj.Agent Price List=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Price List")));
                        //obj.Agent Special Price List=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Special Price List")));
                        //obj.Agent Type=Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Agent Type")));
                        //obj.Agent Code=Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Code")));
                        obj.Status = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Status")));
                        obj.UserName = Convert.ToString(reader.GetValue(reader.GetOrdinal("UserName")));
                        obj.Password = Convert.ToString(reader.GetValue(reader.GetOrdinal("Password")));

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

        public static Agent LoginCredentials(string UserName, string Password)
        {

            string strSQL = string.Empty;
            SqlDataReader reader;
            strSQL = "SP_WA_LoginCredentials";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSQL);
            Agent obj = null;
            try
            {
                db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, Password);
                reader = (SqlDataReader)db.ExecuteReader(dbCommand);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new Agent();
                        obj.Code = Convert.ToString(reader.GetValue(reader.GetOrdinal("Code")));
                        obj.AgentName = Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Name")));
                        obj.AgentAddress = Convert.ToString(reader.GetValue(reader.GetOrdinal("Agent Address")));
                        //obj.Status = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Status")));
                        obj.UserName = Convert.ToString(reader.GetValue(reader.GetOrdinal("UserName")));
                        obj.Password = Convert.ToString(reader.GetValue(reader.GetOrdinal("Password")));
                        obj.ItemCategoryCode = Convert.ToString(reader.GetValue(reader.GetOrdinal("ItemCategoryCode")));
                        obj.AccountActivated = Convert.ToBoolean(reader.GetValue(reader.GetOrdinal("AccountActivated")));
                        obj.UserType = Convert.ToString(reader.GetValue(reader.GetOrdinal("UserType")));
                        obj.LastLoginTime = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("LastLoginTime")));
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

        public static int ChangePasswordCredentials(string UserName, string Password)
        {

            int result = 0;
            string strSql = string.Empty;
            strSql = "SP_WA_ChangeLoginCredentials";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(strSql);
            try
            {
                db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, Password);
                db.ExecuteNonQuery(dbCommand);
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Dispose();
                dbCommand = null;
                db = null;
            }
            return result;
            
        }
    }
}