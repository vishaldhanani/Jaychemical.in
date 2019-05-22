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
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class OrderReplacementCartNew : System.Web.UI.Page
{
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
                string str = string.Empty;
                string idnumber = string.Empty;
                if (Request.QueryString["ProductCode"] != "")
                {
                    str = Request.QueryString["ProductCode"].ToString();
                }
                if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                {
                    idnumber = Request.QueryString["Idnumber"];
                }
                BindCartData(Convert.ToString(str), Convert.ToString(idnumber));

                // 01/10/2015
                // Added by Vishal, added this code for edit product in Order Placement page to fill the Multiply lable value
                foreach (RepeaterItem item in rpt_Cart.Items)
                {
                    DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                    TextBox lblMultiply_Quantity = (TextBox)item.FindControl("tb_Qty");
                    if (dd_Variant.SelectedIndex != 0)
                    {
                        string Qty = Convert.ToString(VarainyQtyforProduct(Request.QueryString["ProductCode"], dd_Variant.SelectedItem.Text));
                        lblMultiply_Quantity.Attributes.Add("placeholder", "Multiple of :" + Qty);

                    }
                    else
                    {

                    }
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
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_Delete = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn_Delete.NamingContainer;

            HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
            if (hf_ProductCode != null)
            {
                SessionManager.DeleteProductFromCart(hf_ProductCode.Value, 0);
            }
            BindCartData(string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void rpt_Cart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DropDownList dd_Variant = e.Item.FindControl("dd_Variant") as DropDownList;
                TextBox tb_Discount = e.Item.FindControl("tb_Discount") as TextBox;
                Label lbl_Discount = e.Item.FindControl("lbl_discount") as Label;
                var tr_condition = e.Item.FindControl("tr_condition") as HtmlTableRow;

                if (tb_Discount.Text == "0")
                {
                    tb_Discount.Visible = false;
                    lbl_Discount.Visible = false;
                    tr_condition.Visible = false;
                }
                else
                {
                    tb_Discount.Visible = true;
                    lbl_Discount.Visible = true;
                    tr_condition.Visible = true;
                }

                if (dd_Variant != null)
                {
                    BindVariant(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ProductCode")), dd_Variant);
                    if (dd_Variant.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "VariantCode"))) != null)
                    {
                        dd_Variant.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "VariantCode"))).Selected = true;
                    }
                    string CartTableName = "Cart";
                    DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                    if (dsCart.Tables[CartTableName] != null && dsCart.Tables[CartTableName].Rows.Count == Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]))
                    {
                        LinkButton1.Visible = false;
                    }
                    else
                    {
                        LinkButton1.Visible = true;
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

    protected void lb_Finish_Click(object sender, EventArgs e)
    {
        Boolean flag = false;
        try
        {
            foreach (RepeaterItem item in rpt_Cart.Items)
            {
                HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
                HiddenField hdUOM = (HiddenField)item.FindControl("hf_UOM");
                TextBox tb_ProductName = (TextBox)item.FindControl("tb_ProductName");

                //DropDownList dd_UOM = (DropDownList)item.FindControl("dd_UOM");
                TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                TextBox tb_Price = (TextBox)item.FindControl("tb_Price");
                TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");

                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                TextBox tb_Discount = (TextBox)item.FindControl("tb_Discount");
                TextBox tb_Total = (TextBox)item.FindControl("tb_Total");

                if (tb_Qty.Text != null && tb_CPrice.Text != null)
                {
                    if (Convert.ToDecimal(tb_CPrice.Text) < Convert.ToDecimal(tb_Price.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter price above base price.');", true);
                        flag = false;
                        tb_CPrice.Focus();
                        tb_CPrice.Text = string.Empty;
                        break;
                    }
                    else if (tb_Qty.Text != "")
                    {
                        List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), dd_Variant.SelectedValue);
                        int quantity = list[0].Quantity;
                        // added variant code - as filter due to qtyt multiplication issue. 
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                            flag = false;
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            break;
                        }
                    }
                    else
                    {
                        flag = true;
                        if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                        {
                            SessionManager.UpdateProductToCartByNo(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                        }
                        else
                        {
                            SessionManager.UpdateProductToCart(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                        }
                    }

                    if (tb_CPrice.Text != "")
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
                            // Decimal DisPrice = (Convert.ToDecimal(tb_CPrice.Text) * Convert.ToDecimal(tb_Discount.Text)) / 100;
                            if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                            {
                                SessionManager.UpdateProductToCartByNo(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                            }
                            else
                            {
                                SessionManager.UpdateProductToCart(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                            }
                        }
                    }
                }
            }
            if (flag == false)
            {

            }
            else
            {
                Response.Redirect("OrderPlacement.aspx", false);
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);           
        }
    }

    protected void lb_AddProduct_Click(object sender, EventArgs e)
    {
        Boolean flag = false;
        try
        {
            foreach (RepeaterItem item in rpt_Cart.Items)
            {
                HiddenField hf_ProductCode = (HiddenField)item.FindControl("hf_ProductCode");
                TextBox tb_ProductName = (TextBox)item.FindControl("tb_ProductName");
                TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                TextBox tb_Price = (TextBox)item.FindControl("tb_Price");
                TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");
                HiddenField hdUOM = (HiddenField)item.FindControl("hf_UOM");
                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                TextBox tb_Discount = (TextBox)item.FindControl("tb_Discount");
                TextBox tb_Total = (TextBox)item.FindControl("tb_Total");

                if (tb_Qty.Text != null && tb_CPrice.Text != null)
                {
                    if (Convert.ToDecimal(tb_CPrice.Text) < Convert.ToDecimal(tb_Price.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter price above base price.');", true);
                        flag = false;
                        tb_CPrice.Focus();
                        tb_CPrice.Text = string.Empty;
                        break;
                    }
                    else if (tb_Qty.Text != "")
                    {
                        List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), dd_Variant.SelectedValue);
                        int quantity = list[0].Quantity;
                        // added variant code - as filter due to qtyt multiplication issue. 
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                            flag = false;
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            break;
                        }
                    }
                    else
                    {
                        flag = true;
                        if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                        {
                            SessionManager.UpdateProductToCartByNo(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                        }
                        else
                        {
                            SessionManager.UpdateProductToCart(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                        }
                    }                    

                    if (tb_CPrice.Text != "")
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
                            if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                            {
                                SessionManager.UpdateProductToCartByNo(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                            }
                            else
                            {
                                SessionManager.UpdateProductToCart(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(tb_Price.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_CPrice.Text.Trim()), Convert.ToDecimal(tb_Discount.Text.Trim()));
                            }

                        }
                    }
                }
            }

            if (flag == false)
            {

            }
            else
            {
                Response.Redirect("ProductItemNew.aspx", false);
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);            
        }
    }

    private void BindCartData(string str, string idnumber)
    {
        try
        {
            DataSet ds = (DataSet)HttpContext.Current.Session[SessionManager.CartTableName];
            DataTable table = ds.Tables[0];
            if (ds != null && ds.Tables[SessionManager.CartTableName].Rows.Count > 0)
            {
                if (table.Rows.Count > 0)
                {
                    // ADDED NEW FILTER IN THE PAGE AS PER NEW REQUIREMENT OF THE ITEMWISE - VARIANT MULTIPLE ADD.
                    if (str != "" && idnumber != "" && idnumber != null)
                    {
                        table.DefaultView.RowFilter = "ProductCode = " + str + "  AND noField = " + idnumber + "";
                        table = table.DefaultView.ToTable();
                    }
                    else
                    {
                        table.DefaultView.RowFilter = "ProductCode = " + str + "  AND VariantCode = ' '";
                        table = table.DefaultView.ToTable();
                    }
                }
                rpt_Cart.DataSource = table;
                rpt_Cart.DataBind();
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

    private void BindVariant(string ProductCode, DropDownList dd_Variant)
    {
        try
        {
            List<Variant> list = Variant.List(ProductCode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), string.Empty);
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

    public decimal PriceforProduct(string productcode, string variantcode)
    {
        decimal d = 0;
        try
        {
            List<Variant> list = Variant.GetPrice(productcode, variantcode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current));
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

    public string UMOforProduct(string productcode, string variantcode)
    {
        string d = string.Empty;
        try
        {
            List<Variant> list = Variant.GetUMO(productcode, variantcode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current));
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

    public int VarainyQtyforProduct(string productcode, string variantcode)
    {
        int d = 0;
        try
        {
            List<Variant> list = Variant.List(productcode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), variantcode);
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
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
            TextBox tb_Total1 = (TextBox)ri.FindControl("tb_SellPrice");

            string s = Convert.ToString(PriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string UMO = Convert.ToString(UMOforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string Qty = Convert.ToString(VarainyQtyforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            tb_Qty1.Text = string.Empty;

            if (tb_Price != null)
            {
                if (Convert.ToString(s) != "0" && Convert.ToString(s) != null)
                {
                    tb_Price.Text = Convert.ToString(s);
                    Decimal price = Convert.ToDecimal(tb_Price.Text);
                    tb_Cust_Price.Text = Convert.ToString(price);
                    hdUOM.Value = Convert.ToString(UMO);
                    hf_Price.Value = Convert.ToString(s);
                    tb_Qty1.Focus();
                    tb_Qty1.Attributes.Add("placeholder", "Multiple of :" + Qty);

                    if (tb_Qty1.Text == "")
                    {
                        tb_Total1.Text = Convert.ToString(0);
                    }


                    // added by Raj Shah due to problem of Price was not persisting while postback occurs
                    foreach (RepeaterItem item in rpt_Cart.Items)
                    {
                        DropDownList dd_UOM = (DropDownList)item.FindControl("dd_UOM");
                        TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                        TextBox tb_CPrice = (TextBox)item.FindControl("txt_Customerprice");
                        DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                        TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");
                        TextBox tb_Discount = (TextBox)item.FindControl("tb_Discount");
                        TextBox tb_Total = (TextBox)item.FindControl("tb_SellPrice");

                        if (tb_Qty.Text != "" && tb_CPrice.Text != "" && tb_CPrice.Text != String.Empty && tb_Qty.Text != String.Empty)
                        {
                            decimal d = Convert.ToDecimal(Convert.ToDecimal(tb_Qty.Text) * Convert.ToDecimal(tb_CPrice.Text));
                            tb_Total.Text = Convert.ToString(d);
                            hdUOM.Value = Convert.ToString(UMO);
                        }
                    }
                }
                else
                {
                    if (hdproductcode != null)
                    {
                        SessionManager.DeleteProductFromCartData(hdproductcode.Value, Convert.ToInt16(s));
                    }
                    string st = DDLValue.SelectedItem.Text;
                    Response.Redirect("ProductItemNew.aspx?MessageId=" + Convert.ToString(st));
                    DDLValue.SelectedIndex = 0; 
                }
            }
            else
            {
                if (hdproductcode != null)
                {
                    SessionManager.DeleteProductFromCartData(hdproductcode.Value, Convert.ToInt16(s));
                }
                string st = DDLValue.SelectedItem.Text;
                Response.Redirect("ProductItemNew.aspx?MessageId=" + Convert.ToString(st));
                DDLValue.SelectedIndex = 0; 
            }
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
                HiddenField hf_ProductCode = ri.FindControl("hf_ProductCode") as HiddenField;
                HiddenField hf_Price = ri.FindControl("hf_Price") as HiddenField;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                HiddenField hdproductcode = ri.FindControl("hf_ProductCode") as HiddenField;
                TextBox tb_Price = ri.FindControl("tb_Price") as TextBox;
                TextBox tb_CPrice = (TextBox)ri.FindControl("txt_Customerprice");
                TextBox tb_Total = (TextBox)ri.FindControl("tb_SellPrice");

                string s = Convert.ToString(PriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
                if (DDLValue.SelectedIndex != 0)
                {
                    if (tb_Qty.Text != "")
                    {
                        List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), DDLValue.SelectedValue);
                        int quantity = list[0].Quantity;
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            tb_Total.Text = Convert.ToString(0);
                        }
                        if (tb_Price.Text != null)
                        {
                            decimal d = Convert.ToDecimal(s) * Convert.ToDecimal(tb_Qty.Text);
                            tb_Total.Text = d.ToString("0.00");
                        }
                        var script = String.Format("document.getElementById('{0}').select();", tb_CPrice.ClientID);
                        ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                        tb_CPrice.Focus();
                    }
                    else
                    {
                        tb_Total.Text = Convert.ToString(0);
                        tb_Qty.Focus();
                    }
                }
                else
                {
                    var message = new JavaScriptSerializer().Serialize("Please select Packs.");
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