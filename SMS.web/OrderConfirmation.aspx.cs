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

public partial class OrderConfirmation : System.Web.UI.Page
{
    #region "Global Variables"

    List<OrderConfirm> list = new List<OrderConfirm>();
    List<OrderConfirm> listsearch=new List<OrderConfirm>();

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
                BindOrderItem();
 
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
    private void BindOrderItem()
    {
        try
        {
            list = Qtm.Lib.OrderConfirm.List(SessionManager.GetAgentCode(HttpContext.Current));

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
                    a_link.HRef = "OrderConfirmationView.aspx?No=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderNo"));
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

    

    protected void btn_SearchOrder_Click(object sender, EventArgs e)
    {
        try
        {
            if (tb_Search.Text.Trim() != null && tb_Search.Text.Trim() != "")
            {
             String[] OrderNo = tb_Search.Text.Split('-');
                if (OrderNo.Length >= 1)
                {
                    String Code = OrderNo[0].Trim();

                    listsearch = Qtm.Lib.OrderConfirm.ListSearch(Code, SessionManager.GetAgentCode(HttpContext.Current));
                    if (listsearch.Count > 0)
                    {
                        rpt_Order.DataSource = listsearch;
                        rpt_Order.DataBind();
                        
                    }
                    else
                    {
                        rpt_Order.DataSource = null;
                        rpt_Order.DataBind();
                        //l_Error.Text = "Orders not found.";
                        l_Error.Visible = true;
                    } 
                }
                else
                {
                    rpt_Order.DataSource = null;
                    rpt_Order.DataBind();
                   // l_Error.Text = "Orders not found.";
                    l_Error.Visible = true;
                    tb_Search.Text = "";
                }
                tb_Search.Text = string.Empty;

            }
            else
            {
                BindOrderItem();
            }
            //tb_Search.Text = "";
            //l_CartCount.Text = SessionManager.GetCartCount();
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
            BindOrderItem();
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