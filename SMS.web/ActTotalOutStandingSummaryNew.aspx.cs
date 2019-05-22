﻿#region "Library"

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
using BO_SERV;
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION
public partial class ActTotalOutStandingSummaryNew : System.Web.UI.Page
{
    #region "Global Variables"
    List<AccountStmtSummary> list = new List<AccountStmtSummary>();
    #endregion
   
    #region Variable

    decimal TotalPrice = 0;
    
    #endregion

    #region Page Events
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
                BindAcc_SummaryData();
                if (Request["Name"].ToString() != "")
                {
                    lblName.Text = Request["Name"].ToString();
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
    private void BindAcc_SummaryData()
    {
        try
        {
            if (Request["Customer"].ToString() != "" && Request["Customer"].ToString() != null)
            {
                list = Qtm.Lib.AccountStmtSummary.ListTotalOutStanding(SessionManager.GetAgentCode(HttpContext.Current), Request["Customer"]);
            }
            else
            {                
                list = null; 
            }          
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Account_Stmt.DataSource = list;
                    rpt_Account_Stmt.DataBind();
                }
                else
                {
                    rpt_Account_Stmt.DataSource = null;
                    rpt_Account_Stmt.Visible = false;
                    rpt_Account_Stmt.DataBind();
                   // l_Error.Text = "Total OutStanding Detial is not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Account_Stmt.DataSource = null;
                rpt_Account_Stmt.Visible = false;
                rpt_Account_Stmt.DataBind();
               // l_Error.Text = "Total OutStanding Detial is not found.";
                l_Error.Visible = true;
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);           
        }
    }

    protected void rpt_TotalOutStandingSumm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {                
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string dt = System.DateTime.Now.ToString("dd/MM/yy");
                Label td = (Label)e.Item.FindControl("tdAmt"); //Where TD1 is the ID of the Table Cell
                Label td1 = (Label)e.Item.FindControl("tdduedate"); //Where TD1 is the ID of the Table Cell
                if (Convert.ToDateTime(td1.Text) < Convert.ToDateTime(dt))
                {
                    
                    td.Attributes.Add("style", "color: red;");
                    
                }
                else
                {
                   
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {                
                Label l_TotalPrice = e.Item.FindControl("l_TotalPrice") as Label;
                if (l_TotalPrice != null)
                {                    
                    l_TotalPrice.Text = TotalPrice.ToString("#,##0"); 
                }
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);           
        }
    }
    #endregion
}

    