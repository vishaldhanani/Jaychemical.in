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
using BO_SERV;
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION
public partial class ShipmentScheduleConsigneeSummaryNew : System.Web.UI.Page
{
    #region "Global Variables"
    List<ShipmentCustSummary> list = new List<ShipmentCustSummary>();
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
                BindShipmentCust_Summary();
                if (Request["Name"].ToString() != null && Request["Name"] !=string.Empty)
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

    private void BindShipmentCust_Summary()
    {
        try
        {
            if (Request["Code"].ToString() != "" && Request["Code"].ToString() != null)
            {
                list = Qtm.Lib.ShipmentCustSummary.ListShipmentConsignee(SessionManager.GetAgentCode(HttpContext.Current), Request["Code"]);
            }
            else
            {                
                list = null; 
            }           
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Q1_Cust_Invoice.DataSource = list;
                    rpt_Q1_Cust_Invoice.DataBind();
                }
                else
                {
                    rpt_Q1_Cust_Invoice.DataSource = null;
                    rpt_Q1_Cust_Invoice.Visible = false;
                    rpt_Q1_Cust_Invoice.DataBind();
                   // l_Error.Text = "Customer wise Shipment Detail is not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Q1_Cust_Invoice.DataSource = null;
                rpt_Q1_Cust_Invoice.Visible = false;
                rpt_Q1_Cust_Invoice.DataBind();
              //  l_Error.Text = "Customer wise Shipment Detail is not found.";
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

    protected void rpt_ShipmentSch_Consignee_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {                
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "qty"));
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tddate"); //Where TD1 is the ID of the Table Cell
                HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdCounter"); //Where TD1 is the ID of the Table Cell
                if (Convert.ToInt32(td1.InnerText) > 1)
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
                    l_TotalPrice.Text = TotalPrice.ToString();
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

    

    protected void BackLink_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ShipmentSchDashboard.aspx?Index=" + Convert.ToString(Request["Index"]) + "", false);
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

    