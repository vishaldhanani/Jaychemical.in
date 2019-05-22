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

public partial class InternalOrderModification : System.Web.UI.Page
{
    #region "Global Variables"
    List<OrderModifi> list = new List<OrderModifi>();
    #endregion

    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.GetItemCategoryCode(HttpContext.Current);
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
                BindOrderNo();
                //BindBlockOrderNo();
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
            list = Qtm.Lib.OrderModifi.InternalList(Convert.ToString(SessionManager.GetItemCategoryCode(HttpContext.Current)));

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

    //private void BindBlockOrderNo()
    //{
    //    try
    //    {
    //        list = Qtm.Lib.OrderModifi.InternalListForBlockOrders(Convert.ToString(SessionManager.GetItemCategoryCode(HttpContext.Current)));

    //        if (list != null && list.Count > 0)
    //        {
    //            if (list.Count > 0)
    //            {
    //                rpt_OrderBlock.DataSource = list;
    //                rpt_OrderBlock.DataBind();
    //                lblBlockOrder.Visible = true;
    //            }
    //            else
    //            {
    //                rpt_OrderBlock.DataSource = null;
    //                rpt_OrderBlock.DataBind();
    //                lblBlockOrder.Visible = false;
                    
    //            }
    //        }
    //        else
    //        {
    //            rpt_OrderBlock.DataSource = null;
    //            rpt_OrderBlock.DataBind();
    //            lblBlockOrder.Visible = false;
               
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
    //        var script = string.Format("alert({0});", message);
    //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
    //    }
    //}
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
                    //  a_link.HRef = "InternalOrderModificationView.aspx?OrderNo=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderNo"));
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
                String[] OrderNo = tb_Search.Text.Split('-');
                if (OrderNo.Length >= 1)
                {
                    String Code = OrderNo[0].Trim();
                    list = Qtm.Lib.OrderModifi.SearchOrder(Convert.ToString(SessionManager.GetItemCategoryCode(HttpContext.Current)), Code);
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
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void rpt_OrderBlock_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    //  a_link.HRef = "InternalOrderModificationView.aspx?OrderNo=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderNo"));
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