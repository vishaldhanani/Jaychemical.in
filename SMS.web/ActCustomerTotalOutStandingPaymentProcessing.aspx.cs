#region "Library"

using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class ActCustomerTotalOutStandingPaymentProcessing : System.Web.UI.Page
{
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
                BindCustomerWiseBalance();
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
    private void BindCustomerWiseBalance()
    {
        try
        {
            List<CustomerwiseBalance> list = CustomerwiseBalance.ListTotOutstanding(SessionManager.GetAgentCode(HttpContext.Current));
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
                    rpt_Customer.Visible = false;
                    rpt_Customer.DataBind();
                   // l_Error.Text = "Customers are not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Customer.DataSource = null;
                rpt_Customer.Visible = false;
                rpt_Customer.DataBind();
               // l_Error.Text = "Customers are not found.";
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
        result = null;
        return result.ToArray();
    }
    #endregion

    #region Events
    protected void rpt_Customer_TotalOutStanding_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = "BankingInfoAccountwise.aspx?Customer=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&Name=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Name")) + "";
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
}