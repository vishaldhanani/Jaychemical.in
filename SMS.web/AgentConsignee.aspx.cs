#region "Library"

using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.Script.Serialization;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class AgentConsignee : System.Web.UI.Page
{
    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
            SessionManager.GetItemCategoryCode(HttpContext.Current);
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
            l_Error.Visible = false;
            if (!Page.IsPostBack)
            
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {                    
                }

                if (Request["CompanyCode"] != null)
                {
                    SessionManager.AddCompanyToSession(HttpContext.Current, Convert.ToString(Request["CompanyCode"]));
                }
                if (Request["CustomerId"] != null)
                {
                    SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                }
                AssignToSession();
                BindAgentCustomer();
            }
        }
        catch (Exception ex)
        {           
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void AssignToSession()
    {
        try
        {
            if (Request["CustomerId"] != null && Request["CustPriGrp"] != null)
            {
                SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                SessionManager.AddConsigneeToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                SessionManager.AddCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["CustPriGrp"]));
                SessionManager.AddSplCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["SplPriGrp"]));
                SessionManager.AddProductGroupCodeDefault(HttpContext.Current, "DYES");
                SessionManager.AddCustDiscountPercentage(HttpContext.Current, Convert.ToString(Request["DiscGrp"]));
            }
            else if (Request["Customer"] != null && Request["CustPriGrp"] != null)
            {
                SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["Customer"]));
                SessionManager.AddCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["CustPriGrp"]));
                SessionManager.AddSplCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["SplPriGrp"]));
                SessionManager.AddCustDiscountPercentage(HttpContext.Current, Convert.ToString(Request["DiscGrp"]));
            }
            else if (Request["Consignee"] != null)
            {
                SessionManager.AddConsigneeToSession(HttpContext.Current, Convert.ToString(Request["Consignee"]));
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

    #region Methods
    private void BindAgentCustomer()
    {
        try
        {
            List<CustomerConsignee> list;
            if (Request["Customer"] != "" && Request["Customer"] != null)
            {
                list = CustomerConsignee.ConsigneeList(Request["Customer"]);
            }
            else
            {
                list = CustomerConsignee.ConsigneeList(Request["CustomerId"]);
            }

            if (Request["NoCustomerNoconsignee"] == "Yes")
            {
                Response.Redirect("CustomerInfo.aspx?ExistConsignee=" + "No" + "&NoCustomerNoconsignee=" + "Yes", false);
            }
            else
            {
                if (list.Count > 0)
                {
                    if (Request["ExistConsignee"] == "Yes")
                    {
                        rpt_Customer.DataSource = list;
                        rpt_Customer.DataBind();
                    }
                    else
                    {
                        Response.Redirect("CustomerInfo.aspx?ExistConsignee=" + "Yes", false);
                    }
                }
                else
                {
                    Response.Redirect("CustomerInfo.aspx?ExistConsignee=" + "No", false);
                }

            }
            //List<CustomerConsignee> list;
            //if (Request["Customer"] != "" && Request["Customer"] != null)
            //{
            //    list = CustomerConsignee.ConsigneeList(Request["Customer"]);
            //}
            //else
            //{
            //    list = CustomerConsignee.ConsigneeList(Request["CustomerId"]);
            //}

            //if (list != null && list.Count > 0)
            //{
            //    if (tb_Search.Text.Trim() != string.Empty)
            //    {
            //        int i = tb_Search.Text.Trim().IndexOf('-');
            //        if (i >= 0) tb_Search.Text = tb_Search.Text.Trim().Substring(i + 1);
            //        list = list.FindAll(x => x.Name.ToLower().Contains(tb_Search.Text.Trim().ToLower()) || x.CustomerNo.ToLower().Contains(tb_Search.Text.Trim().ToLower()));
            //    }
            //    if (list.Count > 0)
            //    {
            //        rpt_Customer.DataSource = list;
            //        rpt_Customer.DataBind();
            //    }
            //    else
            //    {
            //        rpt_Customer.DataSource = null;
            //        rpt_Customer.DataBind();
            //        l_Error.Text = "Consignee details not available for selected customer. You can use Add Customer as Consignee.";
            //        l_Error.Visible = true;
            //        if (Request.QueryString["NoCustomerNoconsignee"] == "Yes")
            //        {
            //            Response.Redirect("CustomerInfo.aspx?Value=" + "Yes", true);
            //        }
            //        else
            //        {
            //            Response.Redirect("CustomerInfo.aspx", true);
            //        }
            //    }
            //}
            //else
            //{
            //    rpt_Customer.DataSource = null;
            //    rpt_Customer.DataBind();
            //    l_Error.Text = "Consignee details not available for selected customer. You can use Add Customer as Consignee.";
            //    l_Error.Visible = true;
            //    if (Request["NoCustomerNoconsignee"] == "Yes")
            //    {
            //        Response.Redirect("CustomerInfo.aspx?Value=" + "Yes", false);
            //    }
            //    else
            //    {
            //        Response.Redirect("CustomerInfo.aspx", true);
            //    }
            //}
        }
        catch (ThreadAbortException ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    [WebMethod]
    public static string[] GetCustomers(string SearchedTxt)
    {
        List<string> result = new List<string>();
        DataTable dt = new DataTable();
        try
        {
            dt = Qtm.Lib.AgentCustomer.GetSuggestedCustomers(SearchedTxt, SessionManager.GetAgentCode(HttpContext.Current), SessionManager.GetCompanyCode(HttpContext.Current));
            DataView dv = new DataView(dt);

            int i = 0;
            while (i < dv.Table.Rows.Count)
            {
                result.Add(string.Format("{0}", dv.Table.Rows[i]["CustCodeWithName"].ToString()));
                i++;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result.ToArray();
    }
    #endregion

    #region Events
    protected void lb_Finish_Click(object sender, EventArgs e)
    {        
        if (Convert.ToString(Request["CustomerId"]) != "")
        {
            Response.Redirect("CustomerInfo.aspx?Consignee=" + Convert.ToString(Request["CustomerId"]) + "&ExistConsignee=" + "Yes", false);
        }
        else
        {

        }

    }
    protected void BtnAddCustConsignee_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerInfo.aspx", false);
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewConsignee.aspx", false);
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
                    if (Request["FromCust"] != null && Convert.ToInt32(Request["FromCust"]) == 1)
                    {
                        a_link.HRef = "CustomerInfo.aspx?Customer=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp") + "&ExistConsignee=" + "Yes");
                    }
                    else if (Request["FromCust"] != null && Convert.ToInt32(Request["FromCust"]) == 0)
                    {
                        a_link.HRef = "CustomerInfo.aspx?Consignee=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo") + "&ExistConsignee=" + "Yes");
                    }
                    else if (Request["CompanyCode"] != null)
                    {
                        a_link.HRef = "CustomerInfo.aspx?CustomerId=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp") + "&ExistConsignee=" + "Yes");
                    }
                    else
                    {
                        a_link.HRef = "CustomerInfo.aspx?Consignee=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo") + "&ExistConsignee=" + "Yes");
                    }
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            BindAgentCustomer();
            tb_Search.Text = "";
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void tb_Search_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BindAgentCustomer();
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