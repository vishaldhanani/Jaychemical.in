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

public partial class ShipmentSchDashboard : System.Web.UI.Page
{
    #region "Global Variables"
    List<ShipmentSche> list = new List<ShipmentSche>();
    #endregion

    #region PageEvents
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

                }
               
                if (string.IsNullOrEmpty(Request["Index"]))
                {
                    BindCustomer();
                    BindConsignee();
                    BindItem();
                }
                else if ((Request["Index"]).Equals("1"))
                {
                    BindCustomer();
                    div_Customer.Visible = true;
                    div_consignee.Visible = false;
                    div_Item.Visible = false;
                    drpSelection.SelectedValue = Convert.ToString(Request["Index"]);
                }
                else if ((Request["Index"]).Equals("2"))
                {
                    BindItem();
                    div_Item.Visible = true;
                    div_Customer.Visible = false;
                    div_consignee.Visible = false;

                    drpSelection.SelectedValue = Convert.ToString(Request["Index"]);
                }
                else if ((Request["Index"]).Equals("3"))
                {
                    BindConsignee();
                    div_consignee.Visible = true;
                    div_Item.Visible = false;
                    div_Customer.Visible = false;
                    drpSelection.SelectedValue = Convert.ToString(Request["Index"]);
                }               
                else
                {
                    BindCustomer();
                    BindConsignee();
                    BindItem();
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
 

    #endregion

    #region Methods
    protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpSelection.SelectedValue == "2")
        {
            div_consignee.Visible = false;
            div_Customer.Visible = false;
            div_Item.Visible = true;
            BindItem();
        }
        else if (drpSelection.SelectedValue == "3")
        {
            div_consignee.Visible = true;
            div_Customer.Visible = false;
            div_Item.Visible = false;
            BindConsignee();
        }
        else
        {
            div_Item.Visible = false;
            div_Customer.Visible = true;
            div_consignee.Visible = false;
            BindCustomer();

        }
    }

    protected void rpt_Customer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = "ShipmentScheduleCustSummaryNew.aspx?Code=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Code")) + "&Name= " + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Name") + "&Index=" + 1 + "");
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

    protected void rpt_Item_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = "ShipmentScheduleItemSummaryNew.aspx?ItemNo=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "itemNo")) + "&Name=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Description") + "&Index=" + 2 + "");
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

    protected void rpt_rpt_Consignee_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = "ShipmentScheduleConsigneeSummaryNew.aspx?Code=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Code")) + "&Name=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Name") + "&Index=" + 3 + "");
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

    private void BindItem()
    {
        try
        {
            List<ShipmentSche> list = ShipmentSche.Itemwise(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {

                if (list.Count > 0)
                {
                    Rpt_Item.DataSource = list;
                    Rpt_Item.DataBind();
                }
                else
                {
                    Rpt_Item.DataSource = null;
                    Rpt_Item.DataBind();
                    // l_Error.Text = "Shipment schedule records not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                Rpt_Item.DataSource = null;
                Rpt_Item.DataBind();
                // l_Error.Text = "Shipment schedule records not found.";
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

    private void BindCustomer()
    {
        try
        {
            List<ShipmentSche> list = ShipmentSche.Customerwise(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Customer.DataSource = list;
                    rpt_Customer.DataBind();
                }
                else
                {
                    rpt_Customer.DataSource = null;
                    rpt_Customer.DataBind();
                    // l_Error.Text = "Data not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Customer.DataSource = null;
                rpt_Customer.DataBind();
                // l_Error.Text = "Data not found.";
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

    private void BindConsignee()
    {
        try
        {
            List<ShipmentSche> list = ShipmentSche.Consigneewise(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Consignee.DataSource = list;
                    rpt_Consignee.DataBind();
                }
                else
                {
                    rpt_Consignee.DataSource = null;
                    rpt_Consignee.DataBind();
                    // l_Error.Text = "Data not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Consignee.DataSource = null;
                rpt_Consignee.DataBind();
                // l_Error.Text = "Data not found.";
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
    #endregion

}