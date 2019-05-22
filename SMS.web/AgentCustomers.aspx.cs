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

public partial class AgentCustomers : System.Web.UI.Page
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
    #endregion

    #region Methods
    private void BindAgentCustomer()
    {
        try
        {
            List<AgentCustomer> list = AgentCustomer.List(SessionManager.GetAgentCode(HttpContext.Current), SessionManager.GetCompanyCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                if (tb_Search.Text.Trim() != string.Empty)
                {
                    int i = tb_Search.Text.Trim().IndexOf('-');
                    if (i >= 0) tb_Search.Text = tb_Search.Text.Trim().Substring(i + 1);
                    list = list.FindAll(x => x.Name.ToLower().Contains(tb_Search.Text.Trim().ToLower()) || x.CustomerNo.ToLower().Contains(tb_Search.Text.Trim().ToLower()));
                }
                if (list.Count > 0)
                {
                    rpt_Customer.DataSource = list;
                    rpt_Customer.DataBind();
                }
                else
                {
                    rpt_Customer.DataSource = null;
                    rpt_Customer.DataBind();                   
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Customer.DataSource = null;
                rpt_Customer.DataBind();               
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
            dt = Qtm.Lib.AgentCustomer.GetSuggestedCustomers(SearchedTxt.ToUpper(), SessionManager.GetAgentCode(HttpContext.Current), SessionManager.GetCompanyCode(HttpContext.Current));
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
                        a_link.HRef = "AgentConsignee.aspx?Customer=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&SplPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "SplCustPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp"));
                    }
                    else if (Request["FromCust"] != null && Convert.ToInt32(Request["FromCust"]) == 0)
                    {
                        a_link.HRef = "CustomerInfo.aspx?Consignee=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo"));
                    }
                    else if (Request["CompanyCode"] != null)
                    {
                        a_link.HRef = "AgentConsignee.aspx?CustomerId=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&SplPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "SplCustPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp"));
                    }
                    else if (SessionManager.GetConsigneeId(HttpContext.Current) != "")
                    {
                        a_link.HRef = "AgentConsignee.aspx?Customer=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp")) + "&SplPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "SplCustPriceGrp")) + "&DiscGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DiscPriceGrp"));
                    }
                    else
                    {
                        a_link.HRef = "CustomerInfo.aspx";
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