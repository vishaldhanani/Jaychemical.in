#region "Library"
using Qtm.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using BO_SERV;
using BO_LINES;
using WS_C_FUN;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

public partial class ChangePassword : System.Web.UI.Page
{

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                //SessionManager.EndFrontUserSession(HttpContext.Current);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region "Button Click Events"
    protected void Bnt_SendMail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_UserId.Text != "" && txtNewPassword.Text != "")
            {             
                int a = Qtm.Lib.Agent.ChangePasswordCredentials(SessionManager.GetAgentCode(HttpContext.Current), txtNewPassword.Text); ;
                Response.Write("<script language='javascript'>window.alert('Your password changed successfully.Again login for placing orders.');window.location='LogIn.aspx';</script>");
            }
            
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true); 
           
        }
    }        
    #endregion   
}