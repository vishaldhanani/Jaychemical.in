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

public partial class BlanketOrderModificationView : System.Web.UI.Page
{
    #region Variable
    BlanketOrders_Service objbo = new BlanketOrders_Service();
    decimal TotalQty = 0, TotalPrice = 0;
    DataTable dtCart;
    List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
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
                    lbl_blanketOrderNo.Text = Convert.ToString(Request.QueryString["OrderNo"]);
                }

                lblpostingdate.Text = Convert.ToString(Request.QueryString["PostingDate"]);

                BindCartData();
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
    private void BindCartData()
    {
        try
        {
            list = Qtm.Lib.OrderModifiCustomerInfo.BlanketOrderList(Request["OrderNo"].ToString());

            if (list != null && list.Count > 0)
            {
                rpt_FinalCart.DataSource = list;
                rpt_FinalCart.DataBind();
            }
            else
            {
                rpt_FinalCart.DataSource = null;
                rpt_FinalCart.DataBind();
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

    private void BindCustomerInfo()
    {
        try
        {
            Qtm.Lib.OrderModifiCustomerInfo obj = Qtm.Lib.OrderModifiCustomerInfo.FindCustomerForBlanketOrder(Request["OrderNo"].ToString());
            if (obj != null)
            {
                l_CustName.Text = obj.Name;
                l_CustAdd.Text = obj.Address1 + " " + obj.Address2 + " " + obj.City + " " + obj.PostCode;
                l_CustConNo.Text = obj.PhoneNo;
                txtCustomerReferenceNo.Text = obj.Cust_Ref_No;
            }

            Qtm.Lib.OrderModifiCustomerInfo objc = Qtm.Lib.OrderModifiCustomerInfo.FindConsigneeForBlanketOrder(Request["OrderNo"].ToString());
            if (objc != null)
            {
                l_ConName.Text = objc.Name;
                l_ConAdd.Text = objc.Address1 + " " + objc.Address2 + " " + objc.City + " " + objc.PostCode;
                l_ConNo.Text = objc.PhoneNo;
            }
            else
            {
                l_ConName.Text = obj.Name;
                l_ConAdd.Text = obj.Address1 + " " + obj.Address2 + " " + obj.City + " " + obj.PostCode;
                l_ConNo.Text = obj.PhoneNo;
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
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Amount"));
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

    #region "Edit and Delete Command event"
    protected void btn_edit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            LinkButton btn_edit = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn_edit.NamingContainer;
            HiddenField ItemNo = (HiddenField)item.FindControl("hf_ItemNo");
            HiddenField LineNo = (HiddenField)item.FindControl("hf_LineNo");
            HiddenField hf_ItemCategoryCode = (HiddenField)item.FindControl("hf_ItemCategoryCode");

            //if (ItemNo != null)
            //{
            //    Response.Redirect("BlanketOrderModificationItems.aspx?ProductCode=" + ItemNo.Value + "&OrderNo=" + Convert.ToString(Request["OrderNo"]) + "&LineNo=" + LineNo.Value + "&PostingDate=" + Convert.ToString(Request["PostingDate"]), false);
            //}

            if (hf_ItemCategoryCode.Value != "CONCHEM")
            {
                Response.Redirect("BlanketOrderModificationItemsDyes.aspx?ProductCode=" + ItemNo.Value + "&OrderNo=" + Convert.ToString(Request["OrderNo"]) + "&LineNo=" + LineNo.Value + "&PostingDate=" + Convert.ToString(Request["PostingDate"]), false);
            }
            else
            {
                Response.Redirect("BlanketOrderModificationItems.aspx?ProductCode=" + ItemNo.Value + "&OrderNo=" + Convert.ToString(Request["OrderNo"]) + "&LineNo=" + LineNo.Value + "&PostingDate=" + Convert.ToString(Request["PostingDate"]), false);
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    //protected void btn_delete_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        LinkButton btn_Delete = (LinkButton)sender;
    //        RepeaterItem item = (RepeaterItem)btn_Delete.NamingContainer;
    //        HiddenField ItemNo = (HiddenField)item.FindControl("hf_ItemNo");
    //        HiddenField LineNo = (HiddenField)item.FindControl("hf_LineNo");

    //        list = Qtm.Lib.OrderModifiCustomerInfo.ListDeleteItemLine(Request["OrderNo"].ToString(), ItemNo.Value, LineNo.Value, string.Empty);
    //        //NetworkCredential NetCredentialsOrder = new NetworkCredential();
    //        //NetCredentialsOrder.UserName = ConfigurationManager.AppSettings["UserName"];
    //        //NetCredentialsOrder.Password = ConfigurationManager.AppSettings["Password"];

    //        //delete For Blanket Order Line
    //        //Web_Order_Mail objserBlkOrder = new Web_Order_Mail();
    //        //objserBlkOrder.UseDefaultCredentials = true;
    //        //objserBlkOrder.Credentials = NetCredentialsOrder;
    //        //objserBlkOrder.DeleteSalesLine(Convert.ToInt16(ConfigurationManager.AppSettings["DocumentType"]), Convert.ToString(Request["OrderNo"]), Convert.ToInt32(LineNo.Value), ItemNo.Value, false);
    //        //objserBlkOrder = null;

    //        if (list != null && list.Count > 0)
    //        {
    //            rpt_FinalCart.DataSource = list;
    //            rpt_FinalCart.DataBind();
    //        }
    //        else
    //        {
    //            rpt_FinalCart.DataSource = null;
    //            rpt_FinalCart.DataBind();
    //            l_Error.Visible = true;
    //        }
    //        BindCartData();
    //    }
    //    catch (Exception ex)
    //    {
    //        var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
    //        var script = string.Format("alert({0});", message);
    //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
    //    }
    //}
    #endregion
}