#region "Library"
using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
#endregion "Library"


// Coding By Raj Shah - JAY APPLICATION

public partial class AgentCompany : System.Web.UI.Page
{
    #region Variables
    public string AgentCode = string.Empty;
    #endregion

    #region "Page Events"
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
               
                if (Request["Next"] == null)
                {
                   
                }
                else
                {                   
                    Session["Next"] = Request["Next"].ToString();
                }

                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                   
                }                
                AgentCode = SessionManager.GetAgentCode(HttpContext.Current);
                BindAgentCompany();
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
    protected void rpt_Company_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {                   
                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ConsigneeCounter")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "AgentCompaniesCounter")) == 1)
                    {
                         a_link.HRef = "AgentConsignee.aspx?CustomerId=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "AgentSubType")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&SplPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "SplCustPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp")) +  "&NoCustomerNoconsignee=" + "Yes" ;
                    }
                    else
                    {
                        a_link.HRef = "AgentCustomers.aspx?CompanyCode=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "AgentSubType"));
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

    private void BindAgentCompany()
    {
        try
        {
            List<AgentCompanies> list = AgentCompanies.List(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                rpt_Company.DataSource = list;
                rpt_Company.DataBind();
            }
            else
            {
                rpt_Company.DataSource = null;
                rpt_Company.DataBind();
              //  l_Error.Text = "Agent Companies are not found.";
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