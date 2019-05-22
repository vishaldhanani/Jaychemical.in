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
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION


public partial class OrderConfirmationView : System.Web.UI.Page
{
    #region "Global Variables"
    List<OrderView> list1 = new List<OrderView>();
    List<OrderInfo> listinfo = new List<OrderInfo>();
    #endregion

    #region Variable
    decimal TotalQty = 0, TotalPrice = 0;
    #endregion

    #region Page Events
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
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["OrderNo"]) != "" && Convert.ToString(Request.QueryString["OrderNo"]) != String.Empty)
                {
                    lblorderno.Text = Convert.ToString(Request.QueryString["OrderNo"]);

                }
                if (Convert.ToString(Request.QueryString["PostingDate"]) != string.Empty && Convert.ToString(Request.QueryString["PostingDate"]) != null)
                {
                    lblpostingdate.Text = Convert.ToString((Convert.ToDateTime(Request.QueryString["PostingDate"]).ToString("dd/MM/yyyy")));
                }
                if (Convert.ToString(Request.QueryString["SalesOrderNo"]) != "" && Convert.ToString(Request.QueryString["SalesOrderNo"]) != String.Empty)
                {
                    lblSalesOrderNo.Text = Convert.ToString(Request.QueryString["SalesOrderNo"]);

                }

                BindOrderData();
                BindOrderInfo();
                ViewedOrder();
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
    private void BindOrderData()
    {
        try
        {
            if (Request["OrderNo"] != "" && Request["OrderNo"] != null)
            {
                list1 = Qtm.Lib.OrderView.List(Request["OrderNo"]);
            }
            else
            {
                //never happen 
                list1 = null;
            }


            if (list1 != null && list1.Count > 0)
            {
                if (list1.Count > 0)
                {
                    rpt_FinalCart.DataSource = list1;
                    rpt_FinalCart.DataBind();
                }
                else
                {
                    rpt_FinalCart.DataSource = null;
                    rpt_FinalCart.DataBind();
                    //l_Error.Text = "Order Confirmation Detail not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_FinalCart.DataSource = null;
                rpt_FinalCart.DataBind();
                // l_Error.Text = "Order Confirmation Detail not found.";
                l_Error.Visible = true;
            }
        }
        catch (Exception ex)
        {

            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindOrderInfo()
    {
        try
        {
            if (Request["OrderNo"].ToString() != "" && Request["OrderNo"].ToString() != null)
            {
                listinfo = Qtm.Lib.OrderInfo.List(Request["OrderNo"].ToString());
            }
            else
            {
                //never happen 
                listinfo = null;
            }
            var list = listinfo;

            if (listinfo != null)
            {
                l_CustName.Text = list[0].Name;
                l_CustAdd.Text = list[0].Address1 + " " + list[0].Address2 + " " + list[0].City;
                l_CustConNo.Text = list[0].PhoneNo;
                l_ConName.Text = list[0].ConName;
                l_ConAdd.Text = list[0].ConAddress1 + " " + list[0].ConAddress2 + " " + list[0].ConCity;
                l_ConNo.Text = list[0].ConPhoneNo;
            }

        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    public void ViewedOrder()
    {
        try
        {
            if (Request["OrderNo"].ToString() != "" && Request["OrderNo"].ToString() != null)
            {
                NetworkCredential NetCredentials = new NetworkCredential();
                NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"]; // "JCIL\\ADMINISTRATOR"; //"MSPL\\NAVTRN1"; //
                NetCredentials.Password = ConfigurationManager.AppSettings["Password"]; //"JCILADMIN@123"; //"admin@12"; //

                Web_Order_Mail objser = new Web_Order_Mail();
                objser.UseDefaultCredentials = true;
                objser.Credentials = NetCredentials;

                objser.OrderViewed(Request["OrderNo"]);
            }
            else
            {

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
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Unit_Price"));
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

    #endregion


}