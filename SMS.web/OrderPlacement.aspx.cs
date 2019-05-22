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

public partial class OrderPlacement : System.Web.UI.Page
{
    #region Variable
    BlanketOrders_Service objbo = new BlanketOrders_Service();
    string inoviceno;
    decimal TotalQty = 0, TotalPrice = 0;
    DataTable dtCart;
    DataTable dtCart1;

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
                BindCustomerInfo();
                txtCustomerReferenceNo.Focus();
                string CartTableName = "Cart";
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                if (dsCart.Tables[CartTableName] != null && dsCart.Tables[CartTableName].Rows.Count == Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]))
                {
                    btn_back.Visible = false;                   
                }
                else
                {                 
                    btn_back.Visible = true;
                }
                // added new code for Disabling button
                btn_Submit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_Submit, null) + ";");
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

    private void BindCustomerInfo()
    {
        try
        {
            Qtm.Lib.CustomerInfo obj = Qtm.Lib.CustomerInfo.Find(SessionManager.GetCustomerId(HttpContext.Current));
            if (obj != null)
            {
                l_CustName.Text = obj.Name;
                l_CustAdd.Text = obj.Address1 + " " + obj.Address2 + " " + obj.City + " " + obj.PostCode;
                //l_CustConNo.Text = obj.PhoneNo;
            }

            Qtm.Lib.ConsigneeInfo objc = Qtm.Lib.ConsigneeInfo.Find(SessionManager.GetConsigneeId(HttpContext.Current), SessionManager.GetCustomerId(HttpContext.Current));
            if (objc != null)
            {
                l_ConName.Text = objc.Name;
                l_ConAdd.Text = objc.Address1 + " " + objc.Address2 + " " + objc.City + " " + objc.PostCode;
                //l_ConNo.Text = objc.PhoneNo;
            }
            else
            {
                l_ConName.Text = obj.Name;
                l_ConAdd.Text = obj.Address1 + " " + obj.Address2 + " " + obj.City + " " + obj.PostCode;
                //l_ConNo.Text = obj.PhoneNo;
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

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            BlanketOrders bo = new BlanketOrders();
            // Network Credentials for Accessing web Service. 
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"]; // "JCIL\\ADMINISTRATOR"; //"MSPL\\NAVTRN1"; //
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"]; //"JCILADMIN@123"; //"admin@12"; //

            // create New Blanket Order  - it will return key of the record.
            objbo = new BlanketOrders_Service();
            objbo.UseDefaultCredentials = true;
            objbo.Credentials = NetCredentials;
            objbo.Create(ref bo);            
            string KEY = bo.Key;
            objbo = null;
            bo = null;

            Qtm.Lib.CustomerInfo obj = Qtm.Lib.CustomerInfo.Find(SessionManager.GetCustomerId(HttpContext.Current));
            if (obj != null)
            {
                BlanketOrders bo1 = new BlanketOrders();
                BlanketOrders_Service objbo1 = new BlanketOrders_Service();
                objbo1.UseDefaultCredentials = true;
                objbo1.Credentials = NetCredentials;
                string strSalesInvoiceRec = objbo1.GetRecIdFromKey(KEY);
                bo1 = objbo1.ReadByRecId(strSalesInvoiceRec);

                bo1.Bill_to_Customer_No = obj.Code;
                bo1.Sell_to_Customer_No = obj.Code;
                bo1.Location_Code = ConfigurationManager.AppSettings["Location"];
                
                objbo1 = new BlanketOrders_Service();
                objbo1.UseDefaultCredentials = true;
                objbo1.Credentials = NetCredentials;
                objbo1.Update(ref bo1);
                bo = bo1;
                objbo = objbo1;
                bo1 = null;
                objbo1 = null;
            }

            BlanketOrders blanketorders = new BlanketOrders();
            BlanketOrders_Service objblanketservies = new BlanketOrders_Service();
            objblanketservies.UseDefaultCredentials = true;
            objblanketservies.Credentials = NetCredentials;
            string recid = objblanketservies.GetRecIdFromKey(KEY);
            blanketorders = objblanketservies.ReadByRecId(recid);

            // once finalization of consignee is done then we will udpate it.
            ConsigneeInfo objC = Qtm.Lib.ConsigneeInfo.Find(SessionManager.GetConsigneeId(HttpContext.Current), SessionManager.GetCustomerId(HttpContext.Current));
            if (objC != null)
            {
                bo.Ship_to_Code = objC.Code;

            }
            if (txtCustomerReferenceNo.Text != "" && txtCustomerReferenceNo.Text != null)
            {
                bo.Customer_Order_No = txtCustomerReferenceNo.Text;
            }
            if (!string.IsNullOrEmpty(SessionManager.GetAgentCode(HttpContext.Current)))
            {
                bo.Agent_Group_Code = Convert.ToString(SessionManager.GetAgentCode(HttpContext.Current));
            }
          
            bo.Order_Date = System.DateTime.Now;
            bo.Document_Date = System.DateTime.Now;
            bo.Posting_Date = System.DateTime.Now;
            
            objbo = new BlanketOrders_Service();
            objbo.UseDefaultCredentials = true;
            objbo.Credentials = NetCredentials;
            objbo.Update(ref bo);

            DataSet ds = (DataSet)HttpContext.Current.Session[SessionManager.CartTableName];
            if (ds != null && ds.Tables[SessionManager.CartTableName].Rows.Count > 0)
            {
                dtCart = ds.Tables[SessionManager.CartTableName];                
                inoviceno = blanketorders.No.ToString();
                blanketorders = null;
                objblanketservies = null;

                if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                {
                    for (int i = 0; i <= dtCart.Rows.Count - 1; i++)
                    {
                        Web_Order_Mail objser1 = new Web_Order_Mail();
                        objser1.UseDefaultCredentials = true;
                        objser1.Credentials = NetCredentials;
                        objser1.CreateBlanketOrderLines(Convert.ToString(inoviceno), Convert.ToString(dtCart.Rows[i]["ProductCode"]), Convert.ToString(dtCart.Rows[i]["VariantCode"]), Convert.ToDecimal(dtCart.Rows[i]["Quantity"]), Convert.ToString(dtCart.Rows[i]["UOM"]), Convert.ToDecimal(dtCart.Rows[i]["BillPrice"]), Convert.ToString(dtCart.Rows[i]["Remark"]), Convert.ToDecimal(dtCart.Rows[i]["DiscPercentage"]));
                        objser1 = null;
                        
                    }   
                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                {
                    for (int i = 0; i <= dtCart.Rows.Count - 1; i++)
                    {
                        Web_Order_Mail objser1 = new Web_Order_Mail();
                        objser1.UseDefaultCredentials = true;
                        objser1.Credentials = NetCredentials;
                        objser1.CreateBlanketOrderLines(Convert.ToString(inoviceno), Convert.ToString(dtCart.Rows[i]["ProductCode"]), Convert.ToString(dtCart.Rows[i]["VariantCode"]), Convert.ToDecimal(dtCart.Rows[i]["Quantity"]), Convert.ToString(dtCart.Rows[i]["UOM"]), Convert.ToDecimal(dtCart.Rows[i]["BillPrice"]), Convert.ToString(dtCart.Rows[i]["Remark"]), Convert.ToDecimal(dtCart.Rows[i]["DiscPercentage"]));
                        objser1 = null;
                    }
                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                {
                    for (int i = 0; i <= dtCart.Rows.Count - 1; i++)
                    {
                        Web_Order_Mail objser1 = new Web_Order_Mail();
                        objser1.UseDefaultCredentials = true;
                        objser1.Credentials = NetCredentials;
                        objser1.CreateBlanketOrderLines(Convert.ToString(inoviceno), Convert.ToString(dtCart.Rows[i]["ProductCode"]), Convert.ToString(dtCart.Rows[i]["VariantCode"]), Convert.ToDecimal(dtCart.Rows[i]["Quantity"]), Convert.ToString(dtCart.Rows[i]["UOM"]), Convert.ToDecimal(dtCart.Rows[i]["BillPrice"]), Convert.ToString(dtCart.Rows[i]["Remark"]), Convert.ToDecimal(dtCart.Rows[i]["DiscPercentage"]));
                        objser1 = null;
                    }
                }

                else
                {
                    for (int i = 0; i <= dtCart.Rows.Count - 1; i++)
                    {
                        Web_Order_Mail objser1 = new Web_Order_Mail();
                        objser1.UseDefaultCredentials = true;
                        objser1.Credentials = NetCredentials;
                        objser1.CreateBlanketOrderLines(Convert.ToString(inoviceno), Convert.ToString(dtCart.Rows[i]["ProductCode"]), Convert.ToString(dtCart.Rows[i]["VariantCode"]), Convert.ToDecimal(dtCart.Rows[i]["Quantity"]), Convert.ToString(dtCart.Rows[i]["UOM"]), Convert.ToDecimal(dtCart.Rows[i]["CSellingPrice"]), Convert.ToString(dtCart.Rows[i]["Remark"]), Convert.ToDecimal(0)); 
                        objser1 = null;
                    }   
                }
                       
                Web_Order_Mail objser = new Web_Order_Mail();
                objser.UseDefaultCredentials = true;
                objser.Credentials = NetCredentials;
                objser.InsertComment(Convert.ToString(inoviceno), Convert.ToInt32(ConfigurationManager.AppSettings["DocumentType"]), txtDelivery_Comment.Text);
                objser.SendNotificationMail(Convert.ToString(inoviceno), Convert.ToInt32(ConfigurationManager.AppSettings["DocumentType"]));
                objser = null;
              
                string usertype = string.Empty;
                usertype = (SessionManager.GetUserType(HttpContext.Current));

                if (usertype == "Internal")
                {
                    var message1 = new JavaScriptSerializer().Serialize("Thank you for placing Order.Your Order Placement No. " + inoviceno.ToString() + "");
                    var script1 = string.Format("alert({0});window.location='InternalMakeOrderView_OrderPlacement.aspx?OrderNo=" + inoviceno.ToString() + "';", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                }
                else if (usertype == "External")
                {
                    var message = new JavaScriptSerializer().Serialize("Thank you for placing Order.Your Order Placement No. " + inoviceno.ToString() + " :- We will revert back shortly with Order Confirmation");
                    var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);                   
                }
                else
                {
                    var message2 = new JavaScriptSerializer().Serialize("User Access Level must be either Internal or External.");
                    var script2 = string.Format("alert({0});window.location='DashBoard.aspx';", message2);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script2, true);
                }
                //Response.Write("<script language='javascript'>window.alert('Thank you for placing Order.Your Order Placement No. " + inoviceno.ToString() + " :- We will revert back shortly with Order Confirmation');window.location='DashBoard.aspx';</script>");
                //var message = new JavaScriptSerializer().Serialize("Thank you for placing Order.Your Order Placement No. " + inoviceno.ToString() + " :- We will revert back shortly with Order Confirmation'");
                //var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);   
            }
        }
        catch (Exception ex)
        {                        
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
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
                SessionManager.DeleteProductFromCart(hf_ProductCode.Value,Convert.ToInt32 (hdn_Nofield.Value));
            }
            BindCartData();

            string CartTableName = "Cart";
            DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
            if (dsCart.Tables[CartTableName] != null && dsCart.Tables[CartTableName].Rows.Count == Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]))
            {
                btn_back.Visible = false;                
            }
            else
            {               
                btn_back.Visible = true;
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