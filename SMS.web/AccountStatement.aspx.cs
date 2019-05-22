#region "Library"

using Qtm.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class AccountStatement : System.Web.UI.Page
{

    #region "Global Variables"
    List<AccountStatementInfo> list = new List<AccountStatementInfo>();
    #endregion 

    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
            lblUserName.Text = SessionManager.GetAgentName(HttpContext.Current);

        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true); 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");                   
                }
                else
                {
                    DueNextAmountDataBind();
                    OverDueAmountDataBind();
                    TotalOutstandingAmountDataBind();                   
                }
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

    #region Methods
    private void DueNextAmountDataBind()
    {
        try
        {
            list = Qtm.Lib.AccountStatementInfo.DueNextAmount(SessionManager.GetAgentCode(HttpContext.Current));
            var listinfo = list;
            if (list != null)
            {
                lbl_Due_Next7Days.Text = (listinfo[0].Due_NextAmount).ToString("#,##0"); 
            }
        }
        catch (Exception ex)
        {           
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true); 
        }
    }
    
    private void OverDueAmountDataBind()
    {
        try
        {
            list = Qtm.Lib.AccountStatementInfo.OverDueAmount(SessionManager.GetAgentCode(HttpContext.Current));
            var listinfo = list;
            if (list != null)
            {              
              lbl_Over_Due.Text = (listinfo[0].OverDue_Amount).ToString("#,##0");
            }
        }
        catch (Exception ex)
        {            
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true); 
        }

    }

    private void TotalOutstandingAmountDataBind()
    {
        try
        {
            list = Qtm.Lib.AccountStatementInfo.TotalOutstandingAmount(SessionManager.GetAgentCode(HttpContext.Current));
            var listinfo = list;
            if (list != null)
            {
                lbl_Total_Outstanding.Text = (listinfo[0].Total_Outstanding_Amount).ToString("#,##0"); 
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