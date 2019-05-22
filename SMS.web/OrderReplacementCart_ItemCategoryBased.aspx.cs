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

public partial class OrderReplacementCart_ItemCategoryBased : System.Web.UI.Page
{
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
                        dd_Variant.SelectedIndex = 0;
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
            l_Error.Text = ex.Message;
            l_Error.Visible = true;
        }
    }

    protected void rpt_Cart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DropDownList dd_Variant = e.Item.FindControl("dd_Variant") as DropDownList;
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
                TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                HiddenField tb_Price = (HiddenField)item.FindControl("tb_Price");//AgentPrice as Lower Limit                
                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");                
                TextBox txt_netPrice = (TextBox)item.FindControl("txt_netPrice");
                TextBox txtPrice = (TextBox)item.FindControl("txtPrice");
                TextBox txtSellingPrice = (TextBox)item.FindControl("txtSellingPrice"); //Consumer Price as Upper Limit with its 7%
                string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));
                string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));
                if (tb_Qty.Text != null && txtSellingPrice.Text != null)
                {
                    decimal ConsPriceIncPercentage = 0;
                    ConsPriceIncPercentage = Convert.ToDecimal(ConsumerPrice) + (Convert.ToDecimal(ConsumerPrice) * Convert.ToDecimal(ConfigurationManager.AppSettings["ConsumerPriceIncreasePercentage"]) / 100);
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
                        List<Variant> list = Variant.VariantList_Quantity_dyes(hf_ProductCode.Value, dd_Variant.SelectedValue);
                        //List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), dd_Variant.SelectedValue);
                        int quantity = list[0].Quantity;
                        if (quantity > 0)
                        {
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
                            else
                            {

                                flag = true;
                                if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                                {
                                    
                                    SessionManager.UpdateProductToCartByNoForDyes(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));

                                }
                                else
                                {
                                    SessionManager.UpdateProductToCartForDyes(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));

                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please contact to Admin Department for updating Item Variants.');", true);
                            flag = false;
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            break;
                        }
                    }

                    //if (tb_Qty.Text != "")
                    //{
                    //    List<Variant> list = Variant.VariantList_Quantity_dyes(hf_ProductCode.Value, dd_Variant.SelectedValue);
                    //    //List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), dd_Variant.SelectedValue);
                    //    int quantity = list[0].Quantity;
                    //    // added variant code - as filter due to qtyt multiplication issue. 
                    //    decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                    //    if (mod > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                    //        flag = false;
                    //        tb_Qty.Focus();
                    //        tb_Qty.Text = string.Empty;
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        flag = true;
                    //        if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                    //        {
                    //            SessionManager.UpdateProductToCartByNoForDyes(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));
                    //        }
                    //        else
                    //        {
                    //            SessionManager.UpdateProductToCartForDyes(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));
                    //        }
                    //    }
                    //}
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
                HiddenField hdUOM = (HiddenField)item.FindControl("hf_UOM");
                TextBox tb_ProductName = (TextBox)item.FindControl("tb_ProductName");
                TextBox tb_Qty = (TextBox)item.FindControl("tb_Qty");
                HiddenField tb_Price = (HiddenField)item.FindControl("tb_Price");  //AgentPrice as Lower Limit              
                DropDownList dd_Variant = (DropDownList)item.FindControl("dd_Variant");
                TextBox tb_Remark = (TextBox)item.FindControl("tb_Remark");                
                TextBox txt_netPrice = (TextBox)item.FindControl("txt_netPrice");
                TextBox txtPrice = (TextBox)item.FindControl("txtPrice");
                TextBox txtSellingPrice = (TextBox)item.FindControl("txtSellingPrice"); //Consumer Price as Upper Limit with its 7%
                string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));
                string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hf_ProductCode.Value, dd_Variant.SelectedItem.Text));

                if (tb_Qty.Text != null && txtSellingPrice.Text != null)
                {
                    decimal ConsPriceIncPercentage = 0;
                    ConsPriceIncPercentage = Convert.ToDecimal(ConsumerPrice) + (Convert.ToDecimal(ConsumerPrice) * Convert.ToDecimal(ConfigurationManager.AppSettings["ConsumerPriceIncreasePercentage"]) / 100);
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
                        List<Variant> list = Variant.VariantList_Quantity_dyes(hf_ProductCode.Value, dd_Variant.SelectedValue);
                        //List<Variant> list = Variant.List(hf_ProductCode.Value, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), dd_Variant.SelectedValue);
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
                        else
                        {

                            flag = true;
                            if (Request.QueryString["Idnumber"] != "" && Request.QueryString["Idnumber"] != string.Empty && Request.QueryString["Idnumber"] != null)
                            {
                                SessionManager.UpdateProductToCartByNoForDyes(Convert.ToInt32(Request.QueryString["Idnumber"]), hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));
                            }
                            else
                            {
                                SessionManager.UpdateProductToCartForDyes(hf_ProductCode.Value, tb_ProductName.Text.Trim(), dd_Variant.SelectedItem.Text, Convert.ToString(hdUOM.Value), Convert.ToDecimal(txtPrice.Text.Trim()), Convert.ToDecimal(tb_Qty.Text.Trim()), tb_Remark.Text.Trim(), Convert.ToDecimal(tb_Price.Value.Trim()), Convert.ToDecimal(0), Convert.ToDecimal(txtSellingPrice.Text));
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
            //l_Error.Text = ex.Message;
            //l_Error.Visible = true;
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
            List<Variant> list = Variant.VariantList_Dyes(ProductCode);
            //List<Variant> list = Variant.List(ProductCode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), string.Empty);
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
            List<Variant> list = Variant.GetPrice_Dyes(productcode, variantcode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), SessionManager.GetCustomerId(HttpContext.Current), SessionManager.GetConsigneeId(HttpContext.Current));
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
            List<Variant> list = Qtm.Lib.Variant.VariantList_Quantity_dyes(productcode, variantcode);
            //List<Variant> list = Variant.List(productcode, SessionManager.GetCustomerPriceGroup(HttpContext.Current), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), variantcode);
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
            TextBox txt_netPrice = ri.FindControl("txt_netPrice") as TextBox;
            TextBox txtSellingPrice = ri.FindControl("txtSellingPrice") as TextBox;           

            string CustomerId = Convert.ToString(SessionManager.GetCustomerId(HttpContext.Current));
            // same customer and same consignee. //08/03/2016 Raj Shah
            string ConsigneeId = Convert.ToString(SessionManager.GetConsigneeId(HttpContext.Current));

            string s = Convert.ToString(PriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string UMO = Convert.ToString(UMOforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string Qty = Convert.ToString(VarainyQtyforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hdproductcode.Value, DDLValue.SelectedItem.Text));
            tb_Qty1.Text = string.Empty;

            if (txtPrice != null)
            {               
                if (Convert.ToString(s) != "0" && Convert.ToString(s) != null)
                {
                    if (txtPrice.Text != "0" && txtPrice.Text != null)
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

                      

                        //if (AgentCode == CustomerId)
                        //{
                        //    txt_billPrice.Text = Convert.ToString(s);                            
                        //}
                        //else
                        //{
                        //    txt_billPrice.Text = Convert.ToString(price);                           
                        //}                      
                    }                    
                }
                else
                {
                    if (hdproductcode != null)
                    {
                        SessionManager.DeleteProductFromCartData(hdproductcode.Value, Convert.ToInt16(s));
                    }
                    string st = DDLValue.SelectedItem.Text;
                    Response.Redirect("ProductItemNew.aspx?MessageId=" + Convert.ToString(st), false);
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
                Response.Redirect("ProductItemNew.aspx?MessageId=" + Convert.ToString(st), false);
                DDLValue.SelectedIndex = 0;
            }
        }
    }

    #endregion
       
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
                TextBox txt_netPrice = ri.FindControl("txt_netPrice") as TextBox;
                TextBox tb_Remark = ri.FindControl("tb_Remark") as TextBox;
                TextBox txtSellingPrice = ri.FindControl("txtSellingPrice") as TextBox;
               
                if (DDLValue.SelectedIndex != 0)
                {
                    if (tb_Qty.Text != "")
                    {
                        List<Variant> list = Variant.VariantList_Quantity_dyes(hf_ProductCode.Value, DDLValue.SelectedValue);
                        int quantity = list[0].Quantity;
                        decimal mod = Convert.ToDecimal(tb_Qty.Text) % Convert.ToDecimal(quantity);
                        if (mod > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter quantity in multiple of packs:  " + Convert.ToInt32(quantity) + "');", true);
                            tb_Qty.Focus();
                            tb_Qty.Text = string.Empty;
                            txt_netPrice.Text = Convert.ToString(0);
                        }

                        if (txtSellingPrice.Text != null)
                        {
                            decimal d = Convert.ToDecimal(txtSellingPrice.Text) * Convert.ToDecimal(tb_Qty.Text);
                            txt_netPrice.Text = d.ToString("0.00");
                        }
                        txtSellingPrice.Focus();                        
                        var script = String.Format("document.getElementById('{0}').select();", txtSellingPrice.ClientID);
                        ClientScript.RegisterStartupScript(GetType(), "focus", script, true);
                    }
                    else
                    {
                        txt_netPrice.Text = Convert.ToString(0);
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
                TextBox txt_netPrice = ri.FindControl("txt_netPrice") as TextBox;
                TextBox txtPrice = ri.FindControl("txtPrice") as TextBox;
                DropDownList DDLValue = ri.FindControl("dd_Variant") as DropDownList;
                TextBox tb_Qty = ri.FindControl("tb_Qty") as TextBox;
                string ConsumerPrice = Convert.ToString(ConsumerPriceforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text));
                string s = Convert.ToString(PriceforProduct(hf_ProductCode.Value, DDLValue.SelectedItem.Text));
              
                if (!string.IsNullOrEmpty(tb_Qty.Text))
                {
                    if (txtSellingPrice.Text != null && txtSellingPrice.Text != "")
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
                                txt_netPrice.Text = d.ToString("0.00");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please update Sales Price.');", true);
                            txtSellingPrice.Focus();
                            txt_netPrice.Text = "0.00";                            
                        }
                    }
                    else
                    {
                        var message1 = new JavaScriptSerializer().Serialize("Please enter Bill Price.");
                        var script1 = string.Format("alert({0});", message1);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                        txtSellingPrice.Focus();
                        txt_netPrice.Text = "0.00";
                    }
                }
                else
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please enter Quantity.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    tb_Qty.Focus();
                    txt_netPrice.Text = "0.00";

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
}