#region "Library"
using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class ProductCategory : System.Web.UI.Page
{
    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            l_Error.Visible = false;
            if (!Page.IsPostBack)
            {
                //if (Request["CustomerId"] != null && Request["CustPriGrp"] != null)
                //{
                //    SessionManager.AddCustomerToSession(HttpContext.Current, Convert.ToString(Request["CustomerId"]));
                //    SessionManager.AddCustomerPriceGroupToSession(HttpContext.Current, Convert.ToString(Request["CustPriGrp"]));
                //}
                BindProductCategory();
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
    private void BindProductCategory()
    {
        try
        {
            List<ProductCat> list = ProductCat.List(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                rpt_ProductCat.DataSource = list;
                rpt_ProductCat.DataBind();
            }
            else
            {
                rpt_ProductCat.DataSource = null;
                rpt_ProductCat.DataBind();
                //l_Error.Text = "Product Category not found.";
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