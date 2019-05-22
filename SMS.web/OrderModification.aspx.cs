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

public partial class OrderModification : System.Web.UI.Page
{
    #region "Global Variables"
    List<OrderModifi> list = new List<OrderModifi>();    
    #endregion

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
                BindOrderNo();
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
    private void BindOrderNo()
    {
        try
        {
            list = Qtm.Lib.OrderModifi.List(SessionManager.GetItemCategoryCode(HttpContext.Current));

            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Order.DataSource = list;
                    rpt_Order.DataBind();
                }
                else
                {
                    rpt_Order.DataSource = null;
                    rpt_Order.DataBind();
                   // l_Error.Text = "Orders not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Order.DataSource = null;
                rpt_Order.DataBind();
               // l_Error.Text = "Orders not found.";
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

    #region Events
    protected void rpt_Item_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    //a_link.HRef = "OrderModificationView.aspx?OrderNo=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderNo")) +"";
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

    protected void tb_Search_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
    protected void btn_OrderSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (tb_Search.Text.Trim() != null && tb_Search.Text.Trim() != "")
            {

                list = Qtm.Lib.OrderModifi.SearchSalesOrder(Convert.ToString(SessionManager.GetItemCategoryCode(HttpContext.Current)), tb_Search.Text.Trim());
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count > 0)
                        {
                            rpt_Order.DataSource = list;
                            rpt_Order.DataBind();
                        }
                        else
                        {
                            rpt_Order.DataSource = null;
                            rpt_Order.DataBind();
                        }
                    }
                    else
                    {
                        rpt_Order.DataSource = null;
                        rpt_Order.DataBind();
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
}