#region "Library"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qtm.Lib;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Configuration;
using WS_C_FUN;
using System.Web.Script.Serialization;
using System.Net;
#endregion "Library"

public partial class BlanketOrderModificationItemsDyes : System.Web.UI.Page
{
    #region Page Events
    List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();

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
                string str = string.Empty;
                string idnumber = string.Empty;

                if (Convert.ToString(Request.QueryString["ProductCode"]) != string.Empty && Convert.ToString(Request.QueryString["OrderNo"]) != string.Empty && Convert.ToString(Request.QueryString["LineNo"]) != string.Empty)
                {
                    str = Request.QueryString["ProductCode"].ToString();
                    BindCartData(Convert.ToString(str));

                    foreach (RepeaterItem item in rpt_Cart.Items)
                    {
                        DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                        TextBox lblMultiply_Quantity = (TextBox)item.FindControl("tb_Qty");
                        HiddenField hf_QuantityShipped = (HiddenField)item.FindControl("hf_QuantityShipped");
                        TextBox txtSellingPrice = (TextBox)item.FindControl("txtSellingPrice");
                        TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");

                        if (dd_Variant.SelectedIndex != 0)
                        {
                            string Qty = Convert.ToString(VaraintQtyforProduct(Request.QueryString["ProductCode"], dd_Variant.SelectedItem.Text));
                            lblMultiply_Quantity.Attributes.Add("placeholder", "Multiple of :" + Qty);
                        }
                        else
                        {
                            dd_Variant.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    var message = new JavaScriptSerializer().Serialize("Order No, Product Code, Line No should not be blank.");
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
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
    #endregion

    #region Events

    protected void rpt_Cart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList dd_Variant = e.Item.FindControl("dd_Variant") as DropDownList;
                if (dd_Variant != null)
                {
                    BindVariant(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ItemNo")), dd_Variant);
                    if (dd_Variant.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "VariantCode"))) != null)
                    {
                        dd_Variant.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "VariantCode"))).Selected = true;
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
    #endregion

    #region Methods
    private void BindCartData(string str)
    {
        try
        {
            list = Qtm.Lib.OrderModifiCustomerInfo.BlanketListFor_EditItem(Convert.ToString(Request["OrderNo"]));
            if (list != null && list.Count > 0)
            {
                var listdata = list.Where(I => I.ItemNo == Convert.ToString(str) && I.LineNo == Convert.ToString(Request["LineNo"]));
                rpt_Cart.DataSource = listdata;
                rpt_Cart.DataBind();
            }
            else
            {
                rpt_Cart.DataSource = null;
                rpt_Cart.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindVariant(string ProductCode, DropDownList dd_Variant)
    {
        try
        {
            List<Variant> list = Variant.VariantList_Dyes(ProductCode);
            if (list != null && list.Count > 0)
            {
                dd_Variant.DataSource = list;
                dd_Variant.DataTextField = "Code";
                dd_Variant.DataValueField = "Code";
                dd_Variant.DataBind();
            }
            dd_Variant.Items.Insert(0, new ListItem("-Select Product Variant-", "0"));
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    public decimal PriceforProduct(string productcode, string variantcode, string CustomerPriceGroup, string CustomerID, string ConsigneeId)
    {
        decimal d = 0;
        try
        {
            string SpecialPrice = string.Empty;
            List<Variant> listSpcialPriceGrp = Variant.GetSpecialPriceGrp(CustomerID);
            if (listSpcialPriceGrp != null && listSpcialPriceGrp[0].SpecialPriceGrp != null && listSpcialPriceGrp[0].SpecialPriceGrp != string.Empty)
            {
                SpecialPrice = listSpcialPriceGrp[0].SpecialPriceGrp;
                List<Variant> list = Variant.GetPrice_Dyes(productcode, variantcode, SpecialPrice, CustomerPriceGroup, CustomerID, ConsigneeId);
                if (list != null && list.Count > 0)
                {
                    d = (list[0].Price);
                }
            }
            else
            {
                List<Variant> list = Variant.GetPrice_Dyes(productcode, variantcode, string.Empty, CustomerPriceGroup, CustomerID, ConsigneeId);
                if (list != null && list.Count > 0)
                {
                    d = (list[0].Price);
                }
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
        return d;
    }

    public string UMOforProduct(string productcode, string variantcode, string CustPricegrp, string SpecialPriceGrp)
    {
        string d = string.Empty;
        try
        {
            List<Variant> list = Variant.GetUMOforDyes(productcode, variantcode, CustPricegrp, SpecialPriceGrp);
            if (list != null && list.Count > 0)
            {
                d = (list[0].UMO);
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
        return d;
    }

    public int VaraintQtyforProduct(string productcode, string variantcode)
    {
        int d = 0;
        try
        {
            List<Variant> list = Qtm.Lib.Variant.VariantList_Quantity_dyes(productcode, variantcode);
            if (list != null && list.Count > 0)
            {
                d = (list[0].Quantity);
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
        return d;
    }

    public decimal ConsumerPriceforProduct(string productcode, string variantcode)
    {
        decimal d = 0;
        try
        {
            List<Variant> list = Variant.GetConsumerPrice_Dyes(productcode, variantcode);
            if (list != null && list.Count > 0)
            {
                d = (list[0].Price);
            }

        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
        return d;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Variant> list = new List<Variant>();
        DropDownList ddl = sender as DropDownList;
        RepeaterItem ri = ddl.NamingContainer as RepeaterItem;
        if (ri != null)
        {
            HiddenField hdproductcode = ri.FindControl("hf_ProductCode") as HiddenField;
            HiddenField hdUOM = ri.FindControl("hf_UOM") as HiddenField;
            HiddenField tb_Price = ri.FindControl("tb_Price") as HiddenField;
            DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
            TextBox tb_Qty1 = ri.FindControl("tb_Qty") as TextBox;
            TextBox txtPrice = ri.FindControl("txtPrice") as TextBox;
            TextBox tb_SellPrice = ri.FindControl("tb_SellPrice") as TextBox;
            TextBox txtSellingPrice = ri.FindControl("txtSellingPrice") as TextBox;

            HiddenField CustomerId = ri.FindControl("hf_SelltoCustomerNo") as HiddenField;
            HiddenField ConsigneeId = ri.FindControl("hf_ShiptoCode") as HiddenField;
            HiddenField hf_CustomerPriceGroup = ri.FindControl("hf_CustomerPriceGroup") as HiddenField;
            if (string.IsNullOrEmpty(ConsigneeId.Value))
            {
                ConsigneeId.Value = CustomerId.Value;
            }

            string SpecialPrice = string.Empty;
            List<Variant> listSpcialPriceGrp = Variant.GetSpecialPriceGrp(CustomerId.Value);
            if (listSpcialPriceGrp != null && listSpcialPriceGrp[0].SpecialPriceGrp != null && listSpcialPriceGrp[0].SpecialPriceGrp != string.Empty)
            {
                SpecialPrice = listSpcialPriceGrp[0].SpecialPriceGrp;
            }

            //string CustomerId = Convert.ToString(SessionManager.GetCustomerId(HttpContext.Current));
            // same customer and same consignee. //08/03/2016 Raj Shah
            //string ConsigneeId = Convert.ToString(SessionManager.GetConsigneeId(HttpContext.Current));

            string s = Convert.ToString(PriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value, CustomerId.Value, ConsigneeId.Value));
            string UMO = Convert.ToString(UMOforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value, Convert.ToString(SpecialPrice)));
            string Qty = Convert.ToString(VaraintQtyforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            tb_Qty1.Text = string.Empty;
            tb_SellPrice.Text = "0.00";

            if (txtPrice.Text != null && txtPrice.Text != "0")
            {
                if (Convert.ToString(s) != "0" && Convert.ToString(s) != null)
                {
                    tb_Price.Value = Convert.ToString(s);
                    hdUOM.Value = Convert.ToString(UMO);
                    tb_Qty1.Focus();
                    tb_Qty1.Attributes.Add("placeholder", "Multiple of :" + Qty);
                    decimal ConsPriceIncPercentage = 0;
                    ConsPriceIncPercentage = Convert.ToDecimal(ConsumerPrice) + (Convert.ToDecimal(ConsumerPrice) * Convert.ToDecimal(ConfigurationManager.AppSettings["ConsumerPriceIncreasePercentage"]) / 100);
                    list = Qtm.Lib.Variant.GetConsumerBool(Convert.ToString(ConsigneeId), Convert.ToString(CustomerId));
                    // changed the code - Error occured due to problem of same customer and same consignee //08/03/2016  Raj Shah
                    if (list.Count > 0)
                    {
                        if (ConsigneeId == CustomerId)
                        {
                            if (Convert.ToBoolean(list[0].BoolConsumerPrice) == true)
                            {
                                txtPrice.Text = Convert.ToString(ConsumerPrice);
                                txtSellingPrice.Text = Convert.ToString(ConsumerPrice);
                            }
                            else
                            {
                                txtPrice.Text = Convert.ToString(s);
                                txtSellingPrice.Text = Convert.ToString(s);
                            }
                        }
                        else
                        {
                            txtPrice.Text = Convert.ToString(s);
                            txtSellingPrice.Text = Convert.ToString(s);
                        }
                    }
                    else
                    {
                        txtPrice.Text = Convert.ToString(s);
                        txtSellingPrice.Text = Convert.ToString(s);
                    }
                }
                else
                {
                    //Message Popup due to Price may be zero or null  and redirect to Dashboard                
                    Response.Redirect("DashBoard.aspx?Message=" + "Yes", false);
                }
            }
            else
            {
                //Message Popup due to Price may be zero or null  and redirect to Dashboard
                Response.Redirect("DashBoard.aspx?Message=" + "Yes", false);
            }
        }
    }

    protected void tb_Qty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox tb_Qty = sender as TextBox;
            RepeaterItem ri = tb_Qty.NamingContainer as RepeaterItem;
            TextBox txtPrice = ri.FindControl("txtPrice") as TextBox;
            if (ri != null)
            {
                HiddenField hf_ProductCode = ri.FindControl("hf_ProductCode") as HiddenField;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                TextBox tb_SellPrice = ri.FindControl("tb_SellPrice") as TextBox;
                TextBox tb_Remark = ri.FindControl("tb_Remark") as TextBox;
                TextBox txtSellingPrice = ri.FindControl("txtSellingPrice") as TextBox;

                if (DDLValue.SelectedIndex != 0)
                {
                    if (tb_Qty.Text != string.Empty)
                    {
                        List<Variant> list = Variant.VariantList_Quantity_dyes(hf_ProductCode.Value, DDLValue.SelectedValue);
                        int quantity = list[0].Quantity;
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            tb_SellPrice.Text = Convert.ToString(0);
                        }

                        if (txtSellingPrice.Text != null)
                        {
                            decimal d = Convert.ToDecimal(txtSellingPrice.Text) * Convert.ToDecimal(tb_Qty.Text);
                            tb_SellPrice.Text = d.ToString("0.00");
                        }
                        txtSellingPrice.Focus();
                        var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                        ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                    }
                    else
                    {
                        tb_SellPrice.Text = Convert.ToString(0);
                        tb_Qty.Focus();
                    }
                }
                else
                {
                    var message = new JavaScriptSerializer().Serialize("Please select product variant code.");
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                }
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0}):", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void txtSellingPrice_TextChanged(object sender, EventArgs e)
    {
        try
        {
            List<Variant> list = new List<Variant>();
            TextBox txtSellingPrice = sender as TextBox;
            RepeaterItem ri = txtSellingPrice.NamingContainer as RepeaterItem;
            if (ri != null)
            {
                HiddenField hf_ProductCode = ri.FindControl("hf_ProductCode") as HiddenField;
                HiddenField tb_Price = ri.FindControl("tb_Price") as HiddenField;
                TextBox tb_SellPrice = ri.FindControl("tb_SellPrice") as TextBox;
                TextBox txtPrice = ri.FindControl("txtPrice") as TextBox;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                TextBox tb_Qty = ri.FindControl("tb_Qty") as TextBox;

                HiddenField CustomerId = ri.FindControl("hf_SelltoCustomerNo") as HiddenField;
                HiddenField ConsigneeId = ri.FindControl("hf_ShiptoCode") as HiddenField;
                HiddenField hf_CustomerPriceGroup = ri.FindControl("hf_CustomerPriceGroup") as HiddenField;
                if (string.IsNullOrEmpty(ConsigneeId.Value))
                {
                    ConsigneeId.Value = CustomerId.Value;
                }

                string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text));
                // string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text));
                string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value, CustomerId.Value, ConsigneeId.Value));

                if (!string.IsNullOrEmpty(tb_Qty.Text))
                {
                    if (txtSellingPrice.Text != null && txtSellingPrice.Text != string.Empty)
                    {
                        decimal ConsPriceIncPercentage = 0;
                        ConsPriceIncPercentage = Convert.ToDecimal(ConsumerPrice) + (Convert.ToDecimal(ConsumerPrice) * Convert.ToDecimal(ConfigurationManager.AppSettings["ConsumerPriceIncreasePercentage"]) / 100);
                        if (ConsPriceIncPercentage > 0)
                        {
                            if (Convert.ToDecimal(txtSellingPrice.Text) > Convert.ToDecimal(ConsPriceIncPercentage))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Price exceeds permissible limit.');", true);
                                txtSellingPrice.Focus();
                                var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                                ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                            }
                            else if (Convert.ToDecimal(txtSellingPrice.Text) < Convert.ToDecimal(s))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Price below permissible limit.');", true);
                                txtSellingPrice.Focus();
                                var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                                ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                            }
                            else
                            {
                                decimal d = Convert.ToDecimal(txtSellingPrice.Text) * Convert.ToDecimal(tb_Qty.Text);
                                tb_SellPrice.Text = d.ToString("0.00");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please update Sales Price.');", true);
                            txtSellingPrice.Focus();
                            tb_SellPrice.Text = "0.00";
                        }
                    }
                    else
                    {
                        var message1 = new JavaScriptSerializer().Serialize("Please enter Bill Price.");
                        var script1 = string.Format("alert({0});", message1);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                        txtSellingPrice.Focus();
                        tb_SellPrice.Text = "0.00";
                    }
                }
                else
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please enter Quantity.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    tb_Qty.Focus();
                    tb_SellPrice.Text = "0.00";
                }
            }
        }
        catch (Exception ex)
        {
            var message1 = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script1 = string.Format("alert({0});", message1);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
        }
    }

    protected void lb_Modify_Click(object sender, EventArgs e)
    {
        Boolean flag = false;
        NetworkCredential NetCredentials = new NetworkCredential();
        NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
        NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

        try
        {
            foreach (RepeaterItem item in rpt_Cart.Items)
            {
                HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
                TextBox tb_ProductName = (TextBox)item.FindControl("tb_ProductName");
                HiddenField dd_UOM = (HiddenField)item.FindControl("hf_UOM");
                TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                TextBox txtSellingPrice = (TextBox)item.FindControl("txtSellingPrice");
                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                HiddenField hf_QuantityShipped = (HiddenField)item.FindControl("hf_QuantityShipped");
                HiddenField hf_OutStandingQty = (HiddenField)item.FindControl("hf_OutStandingQty");

                HiddenField CustomerId = (HiddenField)item.FindControl("hf_SelltoCustomerNo") as HiddenField;
                HiddenField ConsigneeId = (HiddenField)item.FindControl("hf_ShiptoCode") as HiddenField;
                HiddenField hf_CustomerPriceGroup = (HiddenField)item.FindControl("hf_CustomerPriceGroup") as HiddenField;
                if (string.IsNullOrEmpty(ConsigneeId.Value))
                {
                    ConsigneeId.Value = CustomerId.Value;
                }
                string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text, hf_CustomerPriceGroup.Value, CustomerId.Value, ConsigneeId.Value));


                decimal amt = Convert.ToDecimal(Convert.ToDecimal(tb_Qty.Text) * Convert.ToDecimal(txtSellingPrice.Text));
                decimal totalamount = Convert.ToDecimal(amt);
                string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));
                decimal ConsPriceIncPercentage = 0;
                ConsPriceIncPercentage = Convert.ToDecimal(ConsumerPrice) + (Convert.ToDecimal(ConsumerPrice) * Convert.ToDecimal(ConfigurationManager.AppSettings["ConsumerPriceIncreasePercentage"]) / 100);

                if (tb_Qty.Text != null && txtSellingPrice.Text != null)
                {
                    if (Convert.ToDecimal(ConsPriceIncPercentage) > 0)
                    {
                        if (Convert.ToDecimal(txtSellingPrice.Text) > Convert.ToDecimal(ConsPriceIncPercentage))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Price exceeds permissible limit.');", true);
                            txtSellingPrice.Focus();
                            var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                            ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                        }
                        else if (Convert.ToDecimal(txtSellingPrice.Text) < Convert.ToDecimal(s))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Price below permissible limit.');", true);
                            txtSellingPrice.Focus();
                            var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                            ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                        }
                        else
                        {
                            string Qty = Convert.ToString(VaraintQtyforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));
                            decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(Qty);
                            if (mod > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of variant code.');", true);
                                flag = false;
                                tb_Qty.Focus();
                                tb_Qty.Text = string.Empty;
                                break;
                            }
                            else
                            {
                                flag = true;
                                //Update Blanket Order due to updating Qty in blanket Order as well as Sales Order
                                Web_Order_Mail objser2 = new Web_Order_Mail();
                                objser2.UseDefaultCredentials = true;
                                objser2.Credentials = NetCredentials;
                                objser2.UpdateSalesLine(Convert.ToString(Request["OrderNo"]), Convert.ToInt32(Request["LineNo"]), hf_ProductCode.Value, dd_Variant.SelectedItem.Text, dd_UOM.Value, Convert.ToDecimal(tb_Qty.Text), Convert.ToDecimal(txtSellingPrice.Text), tb_Remark.Text);
                                objser2 = null;
                            }
                        }
                    }
                }

                if (flag == false)
                {

                }
                else
                {
                    Response.Redirect("BlanketOrderModificationView.aspx?OrderNo=" + Request["OrderNo"].ToString() + "&BlanketOrderNo=" + Convert.ToString(Request["BlanketOrderNo"]) + "&PostingDate=" + Convert.ToString(Request.QueryString["PostingDate"]), false);
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