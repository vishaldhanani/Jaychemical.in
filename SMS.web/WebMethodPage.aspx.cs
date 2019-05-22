#region "Library"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qtm.Lib;
using System.Web.Services;
using System.Collections;
using System.Net;
using WS_C_FUN;
using System.Configuration;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION
public partial class WebMethodPage : System.Web.UI.Page
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string LoginCredential(string Username, string Password)
    {
        try
        {
            string Success = "Success";
            string SuccessApp = "SuccessApp";
          
            string Fail = "Fail";
            Agent obj = Agent.LoginCredentials(Username, Password);
            if (obj != null)
            {
                HttpContext.Current.Session["userName"] = Username;                
                SessionManager.AddToUserSession(HttpContext.Current, obj.Code, obj.AgentName);
                SessionManager.AddItemCategoryToSession(HttpContext.Current, obj.ItemCategoryCode);
                SessionManager.AddUserTypeSession(HttpContext.Current, obj.UserType);
                //SessionManager.AddLoginTimeSession(HttpContext.Current, Convert.ToDateTime(obj.LastLoginTime));

                HttpContext.Current.Session["ItemCategoryCode"] = obj.ItemCategoryCode;
               

                // for updating the date time of the user login. .// added by Raj Shah 09/11/2016
                try
                {
                    NetworkCredential NetCredentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                    Web_Order_Mail objser1 = new Web_Order_Mail();
                    objser1.UseDefaultCredentials = true;
                    objser1.Credentials = NetCredentials;

                    //DateTime LastLoginTime = objser1.GetLastLoginDateTime(obj.LastLoginTime);
                    SessionManager.AddLoginTimeSession(HttpContext.Current, Convert.ToDateTime(obj.LastLoginTime));
                    objser1.LastLoginLogoutDateTime(obj.Code, DateTime.Now, true);
                    objser1 = null;
                }
                catch (Exception ex)
                {
                    // not to throw error.
                }
                
                if (obj.AccountActivated == false)
                {
                    return SuccessApp;                    
                }
                else
                {
                    return Success;
                }
            }
            else
            {
                return Fail;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            return ex.Message;
        }
    }

    [WebMethod]
    public static List<AgentCompanies> GetAgentCompany()
    {
        try
        {
            //string Success = "Success";
            //string Fail = "Fail";
            List<AgentCompanies> list = AgentCompanies.List((SessionManager.GetAgentCode(HttpContext.Current)));
            if (list != null && list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

}