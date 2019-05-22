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
using System.Net;
using WS_C_FUN;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

public partial class BlanketOrderModificationItems : System.Web.UI.Page
{
    #region Page Events
    List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();

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
                string str = string.Empty;
                if (Request.QueryString["ProductCode"] != "" && Request.QueryString["OrderNo"] != "" && Request.QueryString["LineNo"] != "")
                {
                    str = Request.QueryString["ProductCode"].ToString();
                    BindCartData(Convert.ToString(str));
                    foreach (RepeaterItem item in rpt_Cart.Items)
                    {
                        DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                        TextBox lblMultiply_Quantity = (TextBox)item.FindControl("tb_Qty");
                        TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");
                        TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                        HiddenField hf_CustomerPriceGroup = (HiddenField)item.FindControl("hf_CustomerPriceGroup");

                        if (dd_Variant.SelectedIndex != 0)
                        {
                            string Qty = Convert.ToString(VarainyQtyforProduct(Convert.ToString(Request.QueryString["ProductCode"]), dd_Variant.SelectedItem.Text, hf_CustomerPriceGroup.Value));
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
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
    #endregion

    #region Events
   
    public int VarainyQtyforProduct(string productcode, string variantcode, string CustomerPriceGroup)
    {
        int d = 0;
        try
        {
            //List<AgentCompanies> listcmp = AgentCompanies.List(SessionManager.GetAgentCode(HttpContext.Current));
            //string CompanySubType = listcmp[0].AgentSubType;

            //List<AgentCustomer> list1 = AgentCustomer.List(SessionManager.GetAgentCode(HttpContext.Current), CompanySubType);

            //string CustPricegrp = list1[0].CustomerPriceGrp;
            //string SplCustPricegrp = list1[0].SplCustPriceGrp;

            List<Variant> list = Variant.List(productcode, CustomerPriceGroup, string.Empty, variantcode);
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

    protected void rpt_Cart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList dd_Variant = e.Item.FindControl("dd_Variant") as DropDownList;
                TextBox tb_Discount = e.Item.FindControl("tb_Discount") as TextBox;
                // Label lbl_Discount = e.Item.FindControl("lbl_discount") as Label;
                HiddenField ProductCode = e.Item.FindControl("hf_ProductCode") as HiddenField;
                HiddenField hf_CustomerPriceGroup = e.Item.FindControl("hf_CustomerPriceGroup") as HiddenField;

                var tr_condition = e.Item.FindControl("tr_condition") as HtmlTableRow;

                if (tb_Discount.Text == "0")
                {
                    tb_Discount.Visible = false;
                    // lbl_Discount.Visible = false;
                    tr_condition.Visible = false;
                }
                else
                {
                    tb_Discount.Visible = true;
                    // lbl_Discount.Visible = true;
                    tr_condition.Visible = true;
                }

                if (dd_Variant != null)
                {
                    BindVariant(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ItemNo")), dd_Variant, hf_CustomerPriceGroup.Value);
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
                TextBox tb_Price = (TextBox)item.FindControl("tb_Price");
                TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");
                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                TextBox tb_Discount = (TextBox)item.FindControl("tb_Discount");
                TextBox tb_Total = (TextBox)item.FindControl("tb_Total");
                HiddenField hf_CustomerPriceGroup = (HiddenField)item.FindControl("hf_CustomerPriceGroup");

                decimal amt = Convert.ToDecimal(Convert.ToDecimal(tb_Qty.Text) * Convert.ToDecimal(tb_CPrice.Text));
                decimal totalamount = Convert.ToDecimal(amt);

                if (tb_Qty.Text != null && tb_CPrice.Text != null && tb_Qty.Text != string.Empty)
                {
                    if (Convert.ToDecimal(tb_CPrice.Text) < Convert.ToDecimal(tb_Price.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter price above base price.');", true);
                        flag = false;
                        tb_CPrice.Focus();
                        tb_CPrice.Text = string.Empty;
                        break;
                    }
                    else
                    {
                        List<Variant> list = Variant.List(hf_ProductCode.Value, hf_CustomerPriceGroup.Value, string.Empty, dd_Variant.SelectedItem.Text);
                       // List<Variant> list1 = Variant.ListItemModification(hf_ProductCode.Value);
                        int quantity = list[0].Quantity;

                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
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
                            decimal d = Convert.ToDecimal(tb_Price.Text) * 15 / 100;
                            d = Convert.ToDecimal(tb_Price.Text) + d;
                            if (Convert.ToDecimal(tb_CPrice.Text) >= d)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: fnShowMessage(); ", true);
                                flag = false;
                                tb_CPrice.Focus();
                                tb_CPrice.Text = string.Empty;
                                break;
                            }
                            else
                            {
                                flag = true;
                                Web_Order_Mail objser1 = new Web_Order_Mail();
                                objser1.UseDefaultCredentials = true;
                                objser1.Credentials = NetCredentials;
                                objser1.UpdateSalesLine(Convert.ToString(Request["OrderNo"]), Convert.ToInt32(Request["LineNo"]), hf_ProductCode.Value, dd_Variant.SelectedItem.Text, dd_UOM.Value, Convert.ToDecimal(tb_Qty.Text), Convert.ToDecimal(tb_CPrice.Text), tb_Remark.Text);
                                objser1 = null;
                            }
                        }
                    }
                }
                else
                {
                    var message = new JavaScriptSerializer().Serialize("No. of Packs and Bill Price should not be allowed blank or zero.");
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                }

                if (flag == false)
                {

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record updated successfully.');window.location ='OrderModificationView.aspx';", true);
                    Response.Redirect("BlanketOrderModificationView.aspx?OrderNo=" + Request["OrderNo"].ToString() + "&PostingDate=" + Convert.ToString(Request.QueryString["PostingDate"]), false);
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

    private void BindCartData(string str)
    {
        try
        {
            list = Qtm.Lib.OrderModifiCustomerInfo.BlanketListFor_EditItem(Request["OrderNo"].ToString());
            if (list != null && list.Count > 0)
            {
                var listdata = list.Where(I => I.ItemNo == Convert.ToString(Request["ProductCode"]) && I.LineNo == Convert.ToString(Request["LineNo"]));
                rpt_Cart.DataSource = listdata;
                rpt_Cart.DataBind();
            }
            else
            {
                rpt_Cart.DataSource = null;
                rpt_Cart.DataBind();
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

    private void BindVariant(string ItemNo, DropDownList dd_Variant, string CustomerPriceGroup)
    {
        try
        {
            //List<AgentCompanies> listcmp = AgentCompanies.List(SessionManager.GetAgentCode(HttpContext.Current));
            //string CompanySubType = listcmp[0].AgentSubType;

            //List<AgentCustomer> list1 = AgentCustomer.List(SessionManager.GetAgentCode(HttpContext.Current), CompanySubType);

            //string CustPricegrp = list1[0].CustomerPriceGrp;
            //string SplCustPricegrp = list1[0].SplCustPriceGrp;

            List<Variant> list = Variant.List(ItemNo, CustomerPriceGroup, string.Empty, string.Empty);            
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

    private void BindUOM(string ProductCode, DropDownList dd_UOM)
    {
        try
        {
            List<UOM> list = UOM.List(ProductCode);
            if (list != null && list.Count > 0)
            {
                dd_UOM.DataSource = list;
                dd_UOM.DataTextField = "Code";
                dd_UOM.DataValueField = "Code";
                dd_UOM.DataBind();
            }
            dd_UOM.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    public decimal PriceforProduct(string productcode, string variantcode, string CustomerPriceGroup)
    {
        decimal d = 0;
        try
        {
            //List<AgentCompanies> listcmp = AgentCompanies.List(SessionManager.GetAgentCode(HttpContext.Current));
            //string CompanySubType = listcmp[0].AgentSubType;

            //List<AgentCustomer> list1 = AgentCustomer.List(SessionManager.GetAgentCode(HttpContext.Current), CompanySubType);

            //string CustPricegrp = list1[0].CustomerPriceGrp;
            //string SplCustPricegrp = list1[0].SplCustPriceGrp;
            List<Variant> list = Variant.GetPrice(productcode, variantcode, CustomerPriceGroup, string.Empty);
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

    public string UMOforProduct(string productcode, string variantcode, string CustomerPriceGroup)
    {
        string d = string.Empty;
        try
        {
            //List<AgentCompanies> listcmp = AgentCompanies.List(SessionManager.GetAgentCode(HttpContext.Current));
            //string CompanySubType = listcmp[0].AgentSubType;

            //List<AgentCustomer> list1 = AgentCustomer.List(SessionManager.GetAgentCode(HttpContext.Current), CompanySubType);
            //string CustPricegrp = list1[0].CustomerPriceGrp;
            //string SplCustPricegrp = list1[0].SplCustPriceGrp;

            List<Variant> list = Variant.GetUMO(productcode, variantcode, CustomerPriceGroup, string.Empty);
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            RepeaterItem ri = ddl.NamingContainer as RepeaterItem;

            if (ri != null)
            {
                TextBox tb_Price = ri.FindControl("tb_Price") as TextBox;
                HiddenField hdproductcode = ri.FindControl("hf_ProductCode") as HiddenField;
                HiddenField hdUOM = ri.FindControl("hf_UOM") as HiddenField;
                HiddenField hf_Price = ri.FindControl("hf_Price") as HiddenField;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                TextBox tb_Cust_Price = ri.FindControl("txt_Customerprice") as TextBox;
                TextBox tb_Qty1 = ri.FindControl("tb_Qty") as TextBox;
                HiddenField hf_CustomerPriceGroup = ri.FindControl("hf_CustomerPriceGroup") as HiddenField;

                string s = Convert.ToString(PriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value));
                string UMO = Convert.ToString(UMOforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value));
                string Qty = Convert.ToString(VarainyQtyforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value));
                tb_Qty1.Text = string.Empty;
                if (tb_Price != null)
                {
                    tb_Price.Text = Convert.ToString(s);
                    Decimal price = Convert.ToDecimal(tb_Price.Text);
                    tb_Cust_Price.Text = Convert.ToString(price);
                    hdUOM.Value = Convert.ToString(UMO);
                    hf_Price.Value = Convert.ToString(s);
                    tb_Qty1.Focus();
                    tb_Qty1.Attributes.Add("placeholder", "Multiple of :" + Qty);
                }
                // added by Raj Shah due to problem of Price was not persisting while postback occurs
                foreach (RepeaterItem item in rpt_Cart.Items)
                {
                    HiddenField dd_UOM = (HiddenField)item.FindControl("hf_UOM");
                    TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                    TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");
                    DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                    TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                    TextBox tb_Discount = (TextBox)item.FindControl("tb_Discount");
                    TextBox tb_Total = (TextBox)item.FindControl("tb_SellPrice");
                    tb_Remark.Text = "";
                    hdUOM.Value = Convert.ToString(UMO);

                    if (tb_Qty.Text != "" && tb_CPrice.Text != "" && tb_CPrice.Text != String.Empty && tb_Qty.Text != String.Empty)
                    {
                        decimal d = Convert.ToDecimal(Convert.ToDecimal(tb_Qty.Text) * Convert.ToDecimal(tb_CPrice.Text));
                        tb_Total.Text = Convert.ToString(d);
                        hdUOM.Value = Convert.ToString(UMO);
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
    
    protected void tb_Qty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox tb_Qty = sender as TextBox;
            RepeaterItem ri = tb_Qty.NamingContainer as RepeaterItem;
            if (ri != null)
            {
                TextBox txtPrice = ri.FindControl("tb_Price") as TextBox;
                HiddenField hf_ProductCode = ri.FindControl("hf_ProductCode") as HiddenField;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                TextBox txt_BillPrice = ri.FindControl("txt_Customerprice") as TextBox;
                TextBox tb_Remark = ri.FindControl("tb_Remark") as TextBox;
                HiddenField hf_CustomerPriceGroup = ri.FindControl("hf_CustomerPriceGroup") as HiddenField;
                TextBox tb_SellPrice = ri.FindControl("tb_SellPrice") as TextBox;

                if (DDLValue.SelectedIndex != 0)
                {
                    if (tb_Qty.Text != "")
                    {                       
                        string Qty = Convert.ToString(VarainyQtyforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text, hf_CustomerPriceGroup.Value));
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(Qty);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(Qty) + "');", true);
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            tb_SellPrice.Text = Convert.ToString(0);
                        }

                        if (txt_BillPrice.Text != null)
                        {
                            decimal d = Convert.ToDecimal(txt_BillPrice.Text) * Convert.ToDecimal(tb_Qty.Text);
                            tb_SellPrice.Text = d.ToString("0.00");
                        }
                        txt_BillPrice.Focus();
                        var script = String.Format("document.getElementById('{0}').select();", txt_BillPrice.ClientID);
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
    #endregion
}