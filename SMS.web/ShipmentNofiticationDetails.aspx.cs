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

public partial class ShipmentNofiticationDetails : System.Web.UI.Page
{
    #region "Global Variables"
    List<ShipNotification> list = new List<ShipNotification>();
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

                BindProductItem();                
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
    private void BindProductItem()
    {
        try
        {
            list = Qtm.Lib.ShipNotification.List(SessionManager.GetAgentCode(HttpContext.Current));

            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Item.DataSource = list;
                    rpt_Item.DataBind();
                }
                else
                {
                    rpt_Item.DataSource = null;
                    rpt_Item.DataBind();
                   // l_Error.Text = "Shipment Infromation not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Item.DataSource = null;
                rpt_Item.DataBind();
                //l_Error.Text = "Shipment Infromation not found.";
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
       
    }

    protected void btn_SearchProduct_Click(object sender, EventArgs e)
    {
        try
        {
            if (tb_Search.Text.Trim() != null && tb_Search.Text.Trim() != "")
            {
                list = Qtm.Lib.ShipNotification.Find(tb_Search.Text, SessionManager.GetAgentCode(HttpContext.Current));
                    if (list.Count > 0)
                    {
                        rpt_Item.DataSource = list;
                        rpt_Item.DataBind();
                    }
                    else
                    {
                        rpt_Item.DataSource = null;
                        rpt_Item.DataBind();
                      //  l_Error.Text = "Shipment Notification Information not found.";
                        l_Error.Visible = true;
                    }                
            }
            else
            {
                BindProductItem();
            }
            tb_Search.Text = string.Empty;
            
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
            BindProductItem();
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