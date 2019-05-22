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
using BO_LINES;
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class OrderPlacementCartListDyes : System.Web.UI.Page
{

    #region Variable
    BlanketOrders_Service objbo = new BlanketOrders_Service();
   
    decimal TotalQty = 0, TotalPrice = 0;
   
    #endregion

    #region Page Events
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
            if (!Page.IsPostBack)
            {
                BindCartData();               
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
    private void BindCartData()
    {
        try
        {
            DataSet ds = (DataSet)HttpContext.Current.Session[SessionManager.CartTableName];
            if (ds != null && ds.Tables[SessionManager.CartTableName].Rows.Count > 0)
            {

                rpt_FinalCart.DataSource = ds;
                rpt_FinalCart.DataBind();
            }

            if (ds.Tables[SessionManager.CartTableName].Rows.Count == 0)
            {
                Response.Redirect("ProductItemNew.aspx", false);
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

    #region Events
    protected void rpt_FinalCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Quantity"));
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SellingPrice"));              
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label l_TotalQty = e.Item.FindControl("l_TotalQty") as Label;
                Label l_TotalPrice = e.Item.FindControl("l_TotalPrice") as Label;               
                if (l_TotalQty != null && l_TotalPrice != null)
                {
                    l_TotalQty.Text = TotalQty.ToString("0");
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

    protected void lb_Finish_Click(object sender, EventArgs e)
    {
         Response.Redirect("OrderPlacement.aspx", false);
              
    }
    #endregion

    #region "Edit and Delete Command event"

    protected void btn_edit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            LinkButton btn_edit = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn_edit.NamingContainer;
            HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
            HiddenField hf_VariantCode = (HiddenField)item.FindControl("hf_VariantCode");
            HiddenField hdn_Nofield = (HiddenField)item.FindControl("hdn_Nofield");

            if (hf_ProductCode != null && hdn_Nofield != null)
            {
                if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                {
                    Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + hf_ProductCode.Value + "&Idnumber=" + hdn_Nofield.Value, false);
                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                {
                    Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + hf_ProductCode.Value + "&Idnumber=" + hdn_Nofield.Value, false);
                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                {
                    Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + hf_ProductCode.Value + "&Idnumber=" + hdn_Nofield.Value, false);
                }

                else
                {
                    Response.Redirect("OrderReplacementCartNew.aspx?ProductCode=" + hf_ProductCode.Value + "&Idnumber=" + hdn_Nofield.Value, false);
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

    protected void btn_delete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            LinkButton btn_Delete = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn_Delete.NamingContainer;

            HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
            HiddenField hdn_Nofield = (HiddenField)item.FindControl("hdn_Nofield");
            if (hf_ProductCode != null)
            {
                SessionManager.DeleteProductFromCart(hf_ProductCode.Value, Convert.ToInt32(hdn_Nofield.Value));
            }
            BindCartData();

            //string CartTableName = "Cart";
            //DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
            //if (dsCart.Tables[CartTableName] != null && dsCart.Tables[CartTableName].Rows.Count == Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]))
            //{
            //    btn_back.Visible = false;
            //}
            //else
            //{
            //    btn_back.Visible = true;
            //}

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