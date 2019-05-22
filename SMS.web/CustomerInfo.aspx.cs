#region "Library"
using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class CustomerInfo : System.Web.UI.Page
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
                if (Session["userName"] == null && Session["ItemCategoryCode"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (Convert.ToString(Request["ExistConsignee"]) == "No")
                    {
                        btn_AddConsignee.Visible = false;
                    }
                }                
                AssignToSession();
                BindCustomerInfo();
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
    private void AssignToSession()
    {
        try
        {
            if (Request["CustomerId"] != null && Request["CustPriGrp"] != null )
            {
                SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                SessionManager.AddConsigneeToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                SessionManager.AddProductGroupCodeDefault(HttpContext.Current, "DYES");                
            }
            else if (Request["Customer"] != null && Request["CustPriGrp"] != null )
            {                
                SessionManager.AddConsigneeToSession(HttpContext.Current, Convert.ToString(Request["Consignee"]));
                SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                SessionManager.AddProductGroupCodeDefault(HttpContext.Current, "DYES");                
            }
            
            else if (Request["Consignee"] != null)
            {
                SessionManager.AddConsigneeToSession(HttpContext.Current, Convert.ToString(Request["Consignee"]));
                SessionManager.AddProductGroupCodeDefault(HttpContext.Current, "DYES");
                //SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                //SessionManager.AddCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["CustPriGrp"]));
                //SessionManager.AddProductGroupCodeDefault(HttpContext.Current, "DYES");
                //SessionManager.AddCustDiscountPercentage(HttpContext.Current, Convert.ToString(Request["DiscGrp"]));
            }            
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindCustomerInfo()
    {
        try
        {
            Qtm.Lib.CustomerInfo obj = Qtm.Lib.CustomerInfo.Find(SessionManager.GetCustomerId(HttpContext.Current));
            if (obj != null)
            {
                l_CustCode.Text = obj.Code;
                l_Name.Text = obj.Name;
                l_Address.Text = obj.Address1;
                l_Address2.Text = obj.Address2;
                l_City.Text = obj.City + " " + obj.PostCode;
                Session["CustName"] = obj.Name;
                Session["CustAdd1"] = obj.Address1;
                Session["CustAdd2"] = obj.Address2;
                Session["CustCity"] = obj.City + " " + obj.PostCode;
            }

            Qtm.Lib.ConsigneeInfo objCons = Qtm.Lib.ConsigneeInfo.Find(SessionManager.GetConsigneeId(HttpContext.Current), SessionManager.GetCustomerId(HttpContext.Current));
            if (objCons != null)
            {
                l_CsCustCode.Text = objCons.Code;
                l_CsName.Text = objCons.Name;
                l_CsAddress.Text = objCons.Address1;
                l_CsAddress2.Text = objCons.Address2;
                l_CsCity.Text = objCons.City + " " + objCons.PostCode;
                Session["ConName"] = objCons.Name;
                Session["ConAdd1"] = objCons.Address1;
                Session["ConAdd2"] = objCons.Address2;
                Session["ConCity"] = objCons.City + " " + objCons.PostCode;
            }
            else
            {
                l_CsCustCode.Text = obj.Code;
                l_CsName.Text = obj.Name;
                l_CsAddress.Text = obj.Address1;
                l_CsAddress2.Text = obj.Address2;
                l_CsCity.Text = obj.City + " " + obj.PostCode;
                Session["ConName"] = obj.Name;
                Session["ConAdd1"] = obj.Address1;
                Session["ConAdd2"] = obj.Address2;
                Session["ConCity"] = obj.City + " " + obj.PostCode;            
            }

        }
        catch (Exception ex)
        {
            l_Error.Text = ex.Message;
            l_Error.Visible = true;
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
             
        if (Request.QueryString["NoCustomerNoconsignee"] == "Yes")
        {
            Response.Redirect("AgentCompany.aspx", false);
        }
        else
        {
            Response.Redirect("AgentCustomers.aspx?CustomerId=" + SessionManager.GetCustomerId(HttpContext.Current), false);
        }
    }
    
    protected void btn_AddConsignee_Click(object sender, EventArgs e)
    {
        if (Request["ExistConsignee"] == "Yes")
        {
            Response.Redirect("AgentConsignee.aspx?CustomerId=" + l_CustCode.Text + "&ExistConsignee=" + "Yes" + "&CustPriGrp=" + SessionManager.GetCustomerPriceGroup(HttpContext.Current) + "&SplPriGrp=" + SessionManager.GetSplCustomerPriceGroup(HttpContext.Current) + "&DiscGrp=" + SessionManager.GetCustDisPercentage(HttpContext.Current), false);

        }
        else if (Request["ExistConsignee"] == "No")
        {
            var message2 = new JavaScriptSerializer().Serialize("Consignees are not available.");
            var script2 = string.Format("alert({0});", message2);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script2, true);
        }
    }
    #endregion
}