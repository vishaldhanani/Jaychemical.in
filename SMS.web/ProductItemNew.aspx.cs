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
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text;
#endregion "Library"


// Coding By Raj Shah - JAY APPLICATION

public partial class ProductItemNew : System.Web.UI.Page
{
    #region "Global Variables"
    List<Items> list = new List<Items>();
    #endregion

    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
            SessionManager.GetItemCategoryCode(HttpContext.Current);
            SessionManager.GetSplCustomerPriceGroup(HttpContext.Current);
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
                l_CartCount.Text = SessionManager.GetCartCount();
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {

                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["MessageId"])))
                {
                    var message2 = new JavaScriptSerializer().Serialize("'" + Convert.ToString(Request["MessageId"]) + "' Packing is not available for this Product.");
                    var script2 = string.Format("alert({0});", message2);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", script2, true);
                }
                else
                {

                }
                BindProductCategory();
                BindProductItem(1);
            }
            l_CartCount.Text = SessionManager.GetCartCount();
            BindProductCategory();
            //DataSet dsCart = (DataSet)HttpContext.Current.Session[SessionManager.CartTableName];
            //for (int i = 0; i < dsCart.Tables[SessionManager.CartTableName].Rows.Count; i++)
            //{
            //    if (dsCart.Tables[SessionManager.CartTableName].Rows[i]["VariantCode"].ToString() == "")
            //    {
            //        dsCart.Tables[SessionManager.CartTableName].Rows.RemoveAt(i);
            //    }
            //}
            //dsCart.Tables[SessionManager.CartTableName].AcceptChanges();

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
    private void BindProductItem(Int32 PageIndex)
    {
        try
        {
            string ItemCategoryCodeDefault;
            if (SessionManager.GetItemCategoryCode(HttpContext.Current) == "")
            {
                ItemCategoryCodeDefault = ConfigurationManager.AppSettings["DefaultItemCategoryCode"].ToString();
            }
            else
            {
                ItemCategoryCodeDefault = Convert.ToString(Session["ItemCategoryCode"]);
            }

            list = Qtm.Lib.Items.LoadScrollWiseItemProducts(SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(ItemCategoryCodeDefault), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), Constant.ItemColor, PageIndex, SessionManager.GetCustDisPercentage(HttpContext.Current));

            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    if (Convert.ToString(Session["ItemCategoryCode"]) != "")
                    {
                        if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            rpt_Item.Visible = false;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                        }
                        else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            rpt_Item.Visible = false;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                        }
                        else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            rpt_Item.Visible = false;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                        }
                        else
                        {
                            rpt_Item.DataSource = list;
                            rpt_Item.DataBind();
                            rpt_Item.Visible = true;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = false;

                        }
                    }
                    else
                    {
                        rpt_Item.DataSource = null;
                        rpt_Item.DataBind();

                    }
                }
                else
                {
                    if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                    {

                        rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                    {
                        rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                    {
                        rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                    }
                    else
                    {
                        rpt_Item.DataSource = null;
                        rpt_Item.DataBind();
                        rpt_Item.Visible = true;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = false;
                    }
                    // l_Error.Text = "Product not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                {

                    rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
                    rpt_Item.Visible = false;
                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                {
                    rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
                    rpt_Item.Visible = false;
                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                }
                else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                {
                    rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
                    rpt_Item.Visible = false;
                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                }


                else
                {
                    rpt_Item.DataSource = null;
                    rpt_Item.DataBind();
                    rpt_Item.Visible = true;
                    rpt_Item_BasedOnDefaultItemCategory.Visible = false;
                }
                //l_Error.Text = "Product not found.";
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

    private void BindProductCategory()
    {
        try
        {
            List<ProductCat> list = ProductCat.List(SessionManager.GetAgentCode(HttpContext.Current));
            if (list != null && list.Count > 0)
            {
                rpt_ProductCat.DataSource = list;
                rpt_ProductCat.DataBind();
            }
            else
            {
                rpt_ProductCat.DataSource = null;
                rpt_ProductCat.DataBind();
                //  l_Error.Text = "Data not found.";
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

    [WebMethod]
    public static List<Items> LoadProducts(int pageIndex)
    {
        try
        {
            List<Items> list;

            list = null;
            return list;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public static string[] GetProducts(string SearchedTxt)
    {
        string ItemCategoryCodeDefault;
        if (SessionManager.GetItemCategoryCode(HttpContext.Current) == "")
        {
            ItemCategoryCodeDefault = ConfigurationManager.AppSettings["DefaultItemCategoryCode"].ToString();
        }
        else
        {
            ItemCategoryCodeDefault = SessionManager.GetItemCategoryCode(HttpContext.Current);
        }
        List<string> result = new List<string>();
        DataTable dt = new DataTable();
        try
        {
            dt = Qtm.Lib.Items.GetSuggestedProducts(Convert.ToString(SearchedTxt).ToUpper(), SessionManager.GetCustomerPriceGroup(HttpContext.Current), ItemCategoryCodeDefault, Constant.ItemColor, SessionManager.GetSplCustomerPriceGroup(HttpContext.Current));
            //SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(ItemCategoryCodeDefault), Convert.ToString(Desc), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), SessionManager.GetCustDisPercentage(HttpContext.Current));
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                int i = 0;
                while (i < dv.Table.Rows.Count)
                {
                    result.Add(string.Format("{0}", dv.Table.Rows[i]["Description"].ToString()));
                    i++;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result.ToArray();
    }

    [WebMethod]
    public static string AddToCart(String ItemNo)
    {
        string msg = string.Empty;
        try
        {
            string SplCustPriceGrp = SessionManager.GetSplCustomerPriceGroup(HttpContext.Current);
            Items itemObj = Qtm.Lib.Items.Find(Convert.ToString(ItemNo), SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(Constant.ItemColor), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), SessionManager.GetCustDisPercentage(HttpContext.Current));
            if (itemObj != null)
            {
                if (itemObj.Description != null && itemObj.VariantCode != null && itemObj.UOMCode != null && itemObj.UnitPrice != null) //&& itemObj.UnitPrice != null
                {
                    string strMsg = SessionManager.AddProductToCart(Convert.ToString(ItemNo), Convert.ToString(itemObj.Description), Convert.ToString(itemObj.VariantCode), Convert.ToString(itemObj.UOMCode), Convert.ToDecimal(itemObj.UnitPrice), Convert.ToDecimal(1), Convert.ToDecimal(itemObj.DicPercentage)); //Convert.ToDecimal(itemObj.UnitPrice)
                    if (strMsg != string.Empty)
                    {
                        msg = strMsg;
                        //msg = SessionManager.GetCartCount();                        
                    }
                    else
                    {
                        msg = SessionManager.GetCartCount();
                    }
                }
            }
            else
            {
                //msg = "There is no price available for the product.";
            }
            return msg;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public static string AddToCart_ByItemCategory(String ItemNo)
    {
        string msg = string.Empty;
        try
        {
            string SplCustPriceGrp = SessionManager.GetSplCustomerPriceGroup(HttpContext.Current);

            Items itemObj = Qtm.Lib.Items.Find(Convert.ToString(ItemNo), SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(Constant.ItemColor), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), SessionManager.GetCustDisPercentage(HttpContext.Current));
            if (itemObj != null)
            {
                if (itemObj.Description != null && itemObj.VariantCode != null && itemObj.UOMCode != null && itemObj.UnitPrice != null) //&& itemObj.UnitPrice != null
                {
                    string strMsg = SessionManager.AddProductToCart(Convert.ToString(ItemNo), Convert.ToString(itemObj.Description), Convert.ToString(itemObj.VariantCode), Convert.ToString(itemObj.UOMCode), Convert.ToDecimal(itemObj.UnitPrice), Convert.ToDecimal(1), Convert.ToDecimal(itemObj.DicPercentage)); //Convert.ToDecimal(itemObj.UnitPrice)
                    if (strMsg != string.Empty)
                    {
                        msg = strMsg;
                        //msg = SessionManager.GetCartCount();                        
                    }
                    else
                    {
                        msg = SessionManager.GetCartCount();
                    }
                }
            }
            else
            {
                //msg = "There is no price available for the product.";
            }
            return msg;
        }
        catch (Exception ex)
        {
            throw ex;
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
                    a_link.HRef = "ProductCategory.aspx?CustomerId=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp"));
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

    protected void rpt_Item_BasedOnDefaultItemCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a_link = e.Item.FindControl("a_link") as HtmlAnchor;
                if (a_link != null)
                {
                    a_link.HRef = "ProductCategory.aspx?CustomerId=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerNo")) + "&CustPriGrp=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CustomerPriceGrp"));
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

    protected void btn_SearchProduct_Click(object sender, EventArgs e)
    {
        try
        {
            string ItemCategoryCodeDefault;
            if (SessionManager.GetItemCategoryCode(HttpContext.Current) == "")
            {
                ItemCategoryCodeDefault = ConfigurationManager.AppSettings["DefaultItemCategoryCode"].ToString();
            }
            else
            {
                ItemCategoryCodeDefault = Convert.ToString(Session["ItemCategoryCode"]);
            }

            if (tb_Search.Text.Trim() != null && tb_Search.Text.Trim() != "")
            {
                String[] CodeAndDesc = tb_Search.Text.Split('-');
                if (CodeAndDesc.Length >= 1)
                {
                    String Desc = CodeAndDesc[0].Trim();

                    int i = tb_Search.Text.Trim().IndexOf('-');

                    list = Qtm.Lib.Items.ItemsFindByDescriptionAndProductCode(SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(ItemCategoryCodeDefault), Convert.ToString(Desc), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), SessionManager.GetCustDisPercentage(HttpContext.Current));
                    if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                    {
                        if (list.Count > 0)
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        }
                        else
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            l_Error.Visible = true;
                        }
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                    {
                        if (list.Count > 0)
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        }
                        else
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            l_Error.Visible = true;
                        }
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                    {
                        if (list.Count > 0)
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                            rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        }
                        else
                        {
                            rpt_Item_BasedOnDefaultItemCategory.DataSource = null;
                            rpt_Item_BasedOnDefaultItemCategory.DataBind();
                            l_Error.Visible = true;
                        }
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            rpt_Item.DataSource = list;
                            rpt_Item.Visible = true;
                            rpt_Item.DataBind();
                        }
                        else
                        {
                            rpt_Item.DataSource = null;
                            //rpt_Item.Visible = false;
                            rpt_Item.DataBind();
                            // l_Error.Text = "Product not found.";
                            l_Error.Visible = true;
                        }
                    }
                }
                else
                {
                    rpt_Item.DataSource = null;
                    rpt_Item.Visible = false;
                    rpt_Item.DataBind();
                    // l_Error.Text = "Product not found.";
                    l_Error.Visible = true;
                    tb_Search.Text = "";
                }
            }
            else
            {
                BindProductItem(1);
            }
            tb_Search.Text = "";
            l_CartCount.Text = SessionManager.GetCartCount();
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void cb_Color_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string ItemColor = string.Empty;

            if (cb_Black.Checked == true)
            {
                ItemColor = ",1";
            }
            if (cb_Blue.Checked == true)
            {
                ItemColor += ",2";
            }
            if (cb_RedYellow.Checked == true)
            {
                ItemColor += ",3";
            }
            if (ItemColor == "")
            {
                ItemColor = "0";
            }

            ItemColor = ItemColor.Trim(',');
            if (Request["Code"] != null)
                list = Qtm.Lib.Items.List(SessionManager.GetCustomerPriceGroup(HttpContext.Current), Convert.ToString(Request["Code"]), ItemColor);
            else
                list = Qtm.Lib.Items.List(SessionManager.GetCustomerPriceGroup(HttpContext.Current), string.Empty, ItemColor);

            if (list.Count > 0)
            {
                rpt_Item.DataSource = list;
                rpt_Item.DataBind();
            }
            else
            {
                rpt_Item.DataSource = null;
                rpt_Item.DataBind();
                // l_Error.Text = "Data not found.";
                l_Error.Visible = true;
            }

        }
        catch (Exception ex)
        {
            l_Error.Text = ex.Message;
            l_Error.Visible = true;
        }
    }

    protected void lb_Categories_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            LinkButton btn_Categories = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn_Categories.NamingContainer;           
            SessionManager.AddItemCategoryToSession(HttpContext.Current, e.CommandArgument.ToString());
            list = Qtm.Lib.Items.LoadScrollWiseItemProducts(SessionManager.GetCustomerPriceGroup(HttpContext.Current), e.CommandArgument.ToString(), SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), Constant.ItemColor, 1, SessionManager.GetCustDisPercentage(HttpContext.Current));
            // changed error due to issue of assignment   // RAJ SHAH 18/07/2016--
            SessionManager.AddItemCategoryToSession(HttpContext.Current, e.CommandArgument.ToString());           

            if (list.Count > 0)
            {
                if (Convert.ToString(Session["ItemCategoryCode"]) != "")
                {
                    if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                    {
                        rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                    {
                        rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;

                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                    {
                        rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
                        rpt_Item_BasedOnDefaultItemCategory.DataBind();
                        rpt_Item.Visible = false;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = true;
                    }
                    else
                    {
                        rpt_Item.DataSource = list;
                        rpt_Item.DataBind();
                        rpt_Item.Visible = true;
                        rpt_Item_BasedOnDefaultItemCategory.Visible = false;
                    }
                }
                else
                {
                    rpt_Item.DataSource = null;
                    rpt_Item.DataBind();
                }
            }

          
        }
    }

    protected void CartCount_Click(object sender, EventArgs e)
    {
        try
        {
            string CartTableName = "Cart";
            Boolean BlankEntryExists = false;
            DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
            if (dsCart != null && dsCart.Tables[CartTableName].Rows.Count > 0)
            {
                for (int i = 0; i <= dsCart.Tables[CartTableName].Rows.Count - 1; i++)
                {
                    if (dsCart.Tables[CartTableName].Rows[i]["Rate"].ToString() == "0")
                    {
                        if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                        {
                            Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString(), false);
                            BlankEntryExists = true;
                        }
                        else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                        {
                            Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString(), false);
                            BlankEntryExists = true;
                        }
                        else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                        {
                            Response.Redirect("OrderReplacementCart_ItemCategoryBased.aspx?ProductCode=" + dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString(), false);
                            BlankEntryExists = true;
                        }
                        else
                        {
                            Response.Redirect("OrderReplacementCartNew.aspx?ProductCode=" + dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString(), false);
                            BlankEntryExists = true;
                        }
                    }
                    else
                    {

                    }
                }
                if (BlankEntryExists == true)
                {

                }
                else
                {
                    if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
                    {
                        Response.Redirect("OrderPlacementCartListDyes.aspx", false);
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
                    {
                        Response.Redirect("OrderPlacementCartListDyes.aspx", false);
                    }
                    else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
                    {
                        Response.Redirect("OrderPlacementCartListDyes.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("OrderPlacementCartList.aspx", false);
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

    protected void tb_Search_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            BindProductItem(1);
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
    #endregion

    //protected void btnSubCategory_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        LinkButton btnSubCategory = (LinkButton)sender;
    //        RepeaterItem item = (RepeaterItem)btnSubCategory.NamingContainer;
    //        HiddenField hf_SubCategory = (HiddenField)item.FindControl("hf_SubCategory");

    //        list = Qtm.Lib.Items.LoadScrollWiseItemProducts_CategoryWise(SessionManager.GetCustomerPriceGroup(HttpContext.Current), hf_SubCategory.Value, SessionManager.GetSplCustomerPriceGroup(HttpContext.Current), Constant.ItemColor, 1, SessionManager.GetCustDisPercentage(HttpContext.Current),e.CommandArgument.ToString());
    //        SessionManager.AddItemCategoryToSession(HttpContext.Current, hf_SubCategory.Value);
    //        //SessionManager.AddProductGroupCodeDefault(HttpContext.Current, e.CommandArgument.ToString());

    //        if (list.Count > 0)
    //        {
    //            if (Convert.ToString(Session["ItemCategoryCode"]) != "")
    //            {
    //                if (Convert.ToString(Session["ItemCategoryCode"]) == "DYES")
    //                {
    //                    rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
    //                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
    //                    rpt_Item.Visible = false;
    //                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;

    //                }
    //                else if (Convert.ToString(Session["ItemCategoryCode"]) == "AUX")
    //                {
    //                    rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
    //                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
    //                    rpt_Item.Visible = false;
    //                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;

    //                }
    //                else if (Convert.ToString(Session["ItemCategoryCode"]) == "DIS. DYES")
    //                {
    //                    rpt_Item_BasedOnDefaultItemCategory.DataSource = list;
    //                    rpt_Item_BasedOnDefaultItemCategory.DataBind();
    //                    rpt_Item.Visible = false;
    //                    rpt_Item_BasedOnDefaultItemCategory.Visible = true;

    //                }
    //                else
    //                {
    //                    rpt_Item.DataSource = list;
    //                    rpt_Item.DataBind();
    //                    rpt_Item.Visible = true;
    //                    rpt_Item_BasedOnDefaultItemCategory.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                rpt_Item.DataSource = null;
    //                rpt_Item.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
    //        var script = string.Format("alert({0});", message);
    //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
    //    }
    //}
}