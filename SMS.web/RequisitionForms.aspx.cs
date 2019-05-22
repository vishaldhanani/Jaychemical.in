#region "Library"
using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WS_C_FUN;
using System.Web.Script.Serialization;
using SMS;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class RequisitionForms : System.Web.UI.Page
{
    #region "Global Variables"
    List<DashboardLink> list = new List<DashboardLink>();    
    #endregion

    #region PageEvents

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
            SessionManager.CartTable(HttpContext.Current);
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
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Bind_DashboardGrid();
                    if (Convert.ToString(Request.QueryString["Message"]) =="Yes")
                    {
                        var message2 = new JavaScriptSerializer().Serialize("Price should not be blank or zero in Sales Price.");
                        var script2 = string.Format("alert({0});", message2);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", script2, true);
                    }
                    // Currently Not used - Commented by Raj Shah 04/07/2016
                    //ShowOrderCount();
                    //ShowShipmentCount();
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

    #region "Functions"
    public void ShowOrderCount()
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objser = new Web_Order_Mail();
            objser.UseDefaultCredentials = true;
            objser.Credentials = NetCredentials;

            //string s;
            //s = Convert.ToInt32(objser.CountAgentsOrder(SessionManager.GetAgentCode(HttpContext.Current), Convert.ToInt32(ConfigurationManager.AppSettings["Agenttype"]))).ToString();
            //if (s != null && s != "" && s != "0")
            //{
            //    lblCount.Text = "( " + Convert.ToInt32(objser.CountAgentsOrder(SessionManager.GetAgentCode(HttpContext.Current), Convert.ToInt32(ConfigurationManager.AppSettings["Agenttype"]))).ToString() + " )*";

            //}
            //else
            //{
            //    lblCount.Text = "";
            //}
            //objser = null;


        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
            // Response.Redirect("ThanksYouForSub.html");
        }
    }

    public void ShowShipmentCount()
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objser = new Web_Order_Mail();
            objser.UseDefaultCredentials = true;
            objser.Credentials = NetCredentials;

            //string str;
            //str = Convert.ToInt32(objser.CountAgentsOutward(SessionManager.GetAgentCode(HttpContext.Current), Convert.ToInt32(ConfigurationManager.AppSettings["Agenttype"]))).ToString();
            //if (str != null && str != "" && str != "0")
            //{
            //    lblShipmentNotification.Text = "( " + Convert.ToInt32(objser.CountAgentsOutward(SessionManager.GetAgentCode(HttpContext.Current), Convert.ToInt32(ConfigurationManager.AppSettings["Agenttype"]))).ToString() + " )*";
            //}
            //else
            //{
            //    lblShipmentNotification.Text = "";
            //}
            //objser = null;


        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
            //Response.Redirect("ThanksYouForSub.html");
        }
    }
    #endregion

    #region DashBoardBound
    protected void rpt_Dashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "URL"));
                }
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void Bind_DashboardGrid()
    {
        try
        {
            string usertype = string.Empty;
            usertype = (SessionManager.GetUserType(HttpContext.Current));
            int i = 0;
            if (usertype == "Internal")
            {
                i = 1;
            }
            else if (usertype == "External")
            {
                i = 2;
            }
            else
            {
                i = 0;
            }


            List<DashboardLink> list = DashboardLink.RequisitionFormsFill(SessionManager.GetAgentCode(HttpContext.Current), i, Convert.ToString(SessionManager.GetItemCategoryCode(HttpContext.Current)));
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Dashboard.DataSource = list;
                    rpt_Dashboard.DataBind();
                    
                }
                else
                {
                    rpt_Dashboard.DataSource = null;
                    rpt_Dashboard.DataBind();
                    // l_Error.Text = "Data not found.";
                    // l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Dashboard.DataSource = null;
                rpt_Dashboard.DataBind();
                // l_Error.Text = "Data not found.";
                //l_Error.Visible = true;
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
         
    protected void lnkd_Click(object sender, EventArgs e)
    {
        string jScript = "<script>window.close();</script>";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: close_window(); ", true);

    }
    #endregion

}