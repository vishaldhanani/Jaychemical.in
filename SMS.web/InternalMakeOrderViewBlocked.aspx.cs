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
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

public partial class InternalMakeOrderViewBlocked : System.Web.UI.Page
{
    #region Variable
    List<OrderModifiCustomerInfo> list = new List<OrderModifiCustomerInfo>();
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["userName"] != null)
            {
                string UserName =  Convert.ToString(Session["userName"]);
            }
            //SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
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
                BindProductCode();
                BindNoSeriesCode();
                BindStructureCode();
                BindLocationCode();
                BindFormCode();
                tr_CForm.Visible = false;
                foreach (RepeaterItem item in rpt_FinalCart.Items)
                {
                    DropDownList dd_Excise_PostingGrpCode = (DropDownList)item.FindControl("dd_Excise_PostingGrpCode");
                    TextBox dd_TaxGroupCode = (TextBox)item.FindControl("dd_TaxGroupCode");

                    HiddenField hf_ItemNo = (HiddenField)item.FindControl("hf_ItemNo");
                    TextBox Qty_to_Ship = (TextBox)item.FindControl("Qty_to_Ship");
                    Label lbl_QTY = (Label)item.FindControl("lbl_QTY");
                    Qty_to_Ship.Text = lbl_QTY.Text;

                    
                    List<OrderModifiCustomerInfo> list1 = new List<OrderModifiCustomerInfo>();
                    list1 = Qtm.Lib.OrderModifiCustomerInfo.ExciseGroupCodeList(Convert.ToString(Request["OrderNo"]), hf_ItemNo.Value);
                    string ExciseGroupCode = Convert.ToString(list1[0].ExciseGroupCode);
                    dd_Excise_PostingGrpCode.SelectedValue = ExciseGroupCode.ToString();

                    List<OrderModifiCustomerInfo> list2 = new List<OrderModifiCustomerInfo>();
                    list2 = Qtm.Lib.OrderModifiCustomerInfo.TaxGroupCodeList(Convert.ToString(Request["OrderNo"]), hf_ItemNo.Value);
                    string TaxGroupCode = Convert.ToString(list2[0].TaxGroupCode);
                    dd_TaxGroupCode.Text = TaxGroupCode.ToString();                    
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
    private void BindCartData()
    {
        try
        {
            list = Qtm.Lib.OrderModifiCustomerInfo.InternalList(Request["OrderNo"].ToString());
            lblOrderNo.Text = Convert.ToString(Request["OrderNo"]);

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
            Qtm.Lib.OrderModifiCustomerInfo obj = Qtm.Lib.OrderModifiCustomerInfo.InternalFind(Request["OrderNo"].ToString());
            if (obj != null)
            {
                l_CustName.Text = obj.Name;
                l_CustAdd.Text = obj.Address1 + " " + obj.Address2 + " " + obj.City + " " + obj.PostCode;
                l_CustConNo.Text = obj.PhoneNo;
                l_Consignee.Text = obj.Consignee;
                hf_ShiptoCode.Value = obj.ShiptoCode;
                hf_SelltoCustomerNo.Value = obj.SelltoCustomerNo;
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindProductCode()
    {
        try
        {
            List<ProductCat> list = ProductCat.ListProductCode();
            if (list != null && list.Count > 0)
            {
                drpProductCode.DataSource = list;
                drpProductCode.DataTextField = "Code";
                drpProductCode.DataValueField = "Code";
                drpProductCode.DataBind();
                drpProductCode.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                drpProductCode.DataSource = null;
                drpProductCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindNoSeriesCode()
    {
        try
        {
            List<ProductCat> list = ProductCat.ListNoseriesCode();
            if (list != null && list.Count > 0)
            {
                drpNoseries.DataSource = list;
                drpNoseries.DataTextField = "Name";
                drpNoseries.DataValueField = "Name";
                drpNoseries.DataBind();
                drpNoseries.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                drpNoseries.DataSource = null;
                drpNoseries.DataBind();


            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }


    private void BindNoSeriesCodeLocationWise(string Code)
    {
        try
        {
            List<ProductCat> list = ProductCat.ListNoseriesCodeLocationWiseData(Code);
            if (list != null && list.Count > 0)
            {
                drpNoseries.SelectedValue =  list[0].NoSeries;
                //drpNoseries.DataSource = list;
                //drpNoseries.DataTextField = "Name";
                //drpNoseries.DataValueField = "Name";
                //drpNoseries.DataBind();
                //drpNoseries.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                drpNoseries.DataSource = null;
                drpNoseries.DataBind();


            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindStructureCode()
    {
        try
        {
            List<ProductCat> list = ProductCat.ListStructureCode();
            if (list != null && list.Count > 0)
            {
                drpStructure.DataSource = list;
                drpStructure.DataTextField = "Code";
                drpStructure.DataValueField = "Code";
                drpStructure.DataBind();
                drpStructure.Items.Insert(0, new ListItem("-Select-", "0"));
                //drpStructure.SelectedIndex = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultStructur"]);
            }
            else
            {
                drpStructure.DataSource = null;
                drpStructure.DataBind();


            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void SelectDefaultStructure(string LocationCode)
    {
        try
        {
            List<ProductCat> list = ProductCat.SelectDefaultStructure(LocationCode);
            if (list != null && list.Count > 0)
            {
                drpStructure.SelectedValue = list[0].Structure;
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindLocationCode()
    {
        try
        {
            List<ProductCat> list = ProductCat.ListLocationCode(hf_ShiptoCode.Value, Convert.ToString(Session["userName"]), hf_SelltoCustomerNo.Value);
            if (list != null && list.Count > 0)
            {
                drplocationcode.DataSource = list;  
                drplocationcode.DataTextField = "Name";
                drplocationcode.DataValueField = "Code";
                drplocationcode.DataBind();
                drplocationcode.Items.Insert(0, new ListItem("-Select-", "0"));

                List<ProductCat> list1 = ProductCat.DefaultLocationCode(Convert.ToString(Request["OrderNo"].ToString()));

                if (list1.Count > 0)
                {

                    drplocationcode.SelectedValue = (list1[0].Code);                                                                   
                    BindNoSeriesCodeLocationWise(Convert.ToString(list1[0].Code));
                    SelectDefaultStructure(drplocationcode.SelectedValue);
                }                              
            }
            else
            {
                drplocationcode.DataSource = null;
                drplocationcode.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindFormCode()
    {
        try
        {
            List<ProductCat> list = ProductCat.ListFormCode();
            if (list != null && list.Count > 0)
            {
                drpFormCode.DataSource = list;
                drpFormCode.DataTextField = "Code";
                drpFormCode.DataValueField = "Code";
                drpFormCode.DataBind();
                drpFormCode.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                drpFormCode.DataSource = null;
                drpFormCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindTaxAreaCode(string TaxAreaCode, DropDownList dd_TaxAreaCode)
    {
        try
        {
            List<ProductCat> list = ProductCat.ListTaxAreaCode();
            if (list != null && list.Count > 0)
            {
                dd_TaxAreaCode.DataSource = list;
                dd_TaxAreaCode.DataTextField = "Code";
                dd_TaxAreaCode.DataValueField = "Code";
                dd_TaxAreaCode.DataBind();
                dd_TaxAreaCode.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                dd_TaxAreaCode.DataSource = null;
                dd_TaxAreaCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void BindExcise_Prod_PostingGroupCode(string Excise_PostingGroupCode, DropDownList dd_Excise_PostingGrpCode)
    {
        try
        {
            List<ProductCat> list = ProductCat.ListExcisePostingGroupCode();
            if (list != null && list.Count > 0)
            {

                dd_Excise_PostingGrpCode.DataSource = list;
                dd_Excise_PostingGrpCode.DataTextField = "Code";
                dd_Excise_PostingGrpCode.DataValueField = "Code";
                dd_Excise_PostingGrpCode.DataBind();
                dd_Excise_PostingGrpCode.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                dd_Excise_PostingGrpCode.DataSource = null;
                dd_Excise_PostingGrpCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    public void btn_Update(object sender, EventArgs e)
    {
        try
        {
            Boolean flag = false;
            if (drpNoseries.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select sales order no series.');", true);
            }
            else if (drpStructure.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select structure.');", true);
            }
            else if (drpProductCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Product code.');", true);
            }
            else if (txtDestination.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Destination.');", true);
            }
            else if (drplocationcode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Location code.');", true);
            }
            else
            {
                NetworkCredential NetCredentials = new NetworkCredential();
                NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
                NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

                string str = string.Empty;
                if (drpFormCode.SelectedIndex == 0)
                {
                    str = "";
                }
                else
                {
                    str = Convert.ToString(drpFormCode.SelectedValue);
                }

                // Update Values of Blanket Sales  Order Header Details by Codeunit Function.
                Web_Order_Mail objser1 = new Web_Order_Mail();
                objser1.UseDefaultCredentials = true;
                objser1.Credentials = NetCredentials;
                objser1.UpdateBlanketOrder(Convert.ToString(Request.QueryString["OrderNo"]), string.Empty, drpProductCode.SelectedItem.Value, drpStructure.SelectedItem.Value, txtTransporterName.Text, drpNoseries.SelectedItem.Value, txtDestination.Text, Convert.ToString(str), drplocationcode.SelectedValue);
                objser1 = null;

                foreach (RepeaterItem item in rpt_FinalCart.Items)
                {
                    HiddenField ItemNo = (HiddenField)item.FindControl("hf_ItemNo");
                    HiddenField LineNo = (HiddenField)item.FindControl("hf_LineNo");
                    TextBox Qty_to_Ship = (TextBox)item.FindControl("Qty_to_Ship");
                    TextBox tb_Discount = (TextBox)item.FindControl("tb_discount");
                    Label lbl_QTY = (Label)item.FindControl("lbl_QTY");
                    DropDownList dd_TaxAreaCode = (DropDownList)item.FindControl("dd_TaxAreaCode");
                    TextBox dd_TaxGroupCode = (TextBox)item.FindControl("dd_TaxGroupCode");
                    DropDownList dd_Excise_PostingGrpCode = (DropDownList)item.FindControl("dd_Excise_PostingGrpCode");

                    Label lbl_SalesOrderNo = (Label)item.FindControl("lbl_SalesOrderNo");
                    Label lbl_home_logo = (Label)item.FindControl("lbl_home_logo");

                    if (Qty_to_Ship.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Quantity to Ship should not be allowed blank.');", true);
                        flag = true;
                    }
                    else if (Convert.ToDecimal(Qty_to_Ship.Text) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Quantity to Ship should not be allowed zero.');", true);
                        Qty_to_Ship.Focus();
                        flag = true;
                    }
                    else if (Convert.ToDecimal(Qty_to_Ship.Text) > Convert.ToDecimal(lbl_QTY.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Quantity to Ship should not be greater than Order Quantity.');", true);
                        Qty_to_Ship.Focus();
                        flag = true;
                    }
                    else if (Convert.ToString(tb_Discount.Text) == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Discount should not be allowed blank.');", true);
                        tb_Discount.Focus();
                        tb_Discount.Text = Convert.ToString(0);
                        flag = true;
                    }

                    //else if (dd_TaxGroupCode.SelectedIndex == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Tax Group Code.');", true);
                    //    flag = true;
                    //}

                    else if (dd_Excise_PostingGrpCode.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Excise Group Code.');", true);
                        flag = true;
                    }
                    else
                    {
                        //string str = string.Empty;
                        //if (drpFormCode.SelectedIndex == 0)
                        //{
                        //    str = "";
                        //}
                        //else
                        //{
                        //    str = Convert.ToString(drpFormCode.SelectedValue);
                        //}

                        //// Update Values of Blanket Sales  Order by Codeunit Function.
                        //Web_Order_Mail objser1 = new Web_Order_Mail();
                        //objser1.UseDefaultCredentials = true;
                        //objser1.Credentials = NetCredentials;
                        //objser1.UpdateBlanketOrder(Convert.ToString(Request.QueryString["OrderNo"]), string.Empty, drpProductCode.SelectedItem.Value, drpStructure.SelectedItem.Value, txtTransporterName.Text, drpNoseries.SelectedItem.Value, txtDestination.Text, Convert.ToString(str), drplocationcode.SelectedValue);
                        //objser1 = null;

                        // Update Quantity to ship in  Sales  Order by Codeunit Function.
                        Web_Order_Mail objser = new Web_Order_Mail();
                        objser.UseDefaultCredentials = true;
                        objser.Credentials = NetCredentials;
                        objser.UpdateQtyToShip(Convert.ToString(Request.QueryString["OrderNo"]), Convert.ToInt32(LineNo.Value), Convert.ToDecimal(Qty_to_Ship.Text), Convert.ToDecimal(tb_Discount.Text), string.Empty, Convert.ToString(dd_TaxGroupCode.Text), Convert.ToString(dd_Excise_PostingGrpCode.SelectedItem.Value));
                        objser = null;

                    }
                }
                if (flag == true)
                {
                }
                else
                {
                    // Release Blanket Sales  Order by Codeunit Function.
                    //Web_Order_Mail objservice = new Web_Order_Mail();
                    //objservice.UseDefaultCredentials = true;
                    //objservice.Credentials = NetCredentials;
                    //objservice.ReleaseBlanketOrder(Convert.ToString(Request.QueryString["OrderNo"]));
                    //objservice = null;


                    // Make Order By Codeunit Function Return with OrderNo.
                    Web_Order_Mail objser2 = new Web_Order_Mail();
                    objser2.UseDefaultCredentials = true;
                    objser2.Credentials = NetCredentials;

                    string MakeOrderNo = objser2.MakeOrder(4, Convert.ToString(Request.QueryString["OrderNo"]));
                    objser2 = null;

                    lb_Update.Visible = false;
                    lb_Satistic.Visible = true;

                    btn_BackLink.Visible = false;
                    lbl_home_logo.Visible = false;

                    lbl_SalesOrderNo.Visible = true;
                    lblOrderNo.Visible = false;
                    blanketOrder_tr.Visible = true;
                    tr_CForm.Visible = false;

                    lbl_blk_No.Text = Convert.ToString(Request.QueryString["OrderNo"]);
                    ViewState["SalesOrder"] = Convert.ToString(MakeOrderNo);
                    lbl_SalesOrderNo.Text = Convert.ToString(MakeOrderNo);
                    var message = new JavaScriptSerializer().Serialize("Order has been successfully updated and Sales Order generated - Sales Order No. " + MakeOrderNo.ToString() + "");
                    var script = string.Format("alert({0});", message); 
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                }
            }
        }
        catch (Exception ex)
        {
            string IsSalesOrderExist = string.Empty;
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objservice = new Web_Order_Mail();
            objservice.UseDefaultCredentials = true;
            objservice.Credentials = NetCredentials;

            IsSalesOrderExist = objservice.IsOrderExistfromBlanketOrder(Convert.ToString(Request.QueryString["OrderNo"]));
            if (IsSalesOrderExist != "")
            {
                lb_Update.Visible = false;
                lb_Satistic.Visible = true;
                ViewState["SalesOrder"] = Convert.ToString(IsSalesOrderExist);
                objservice.ReleaseBlanketOrder(Convert.ToString(Request.QueryString["OrderNo"]));
                objservice = null;

                var message = new JavaScriptSerializer().Serialize("Order has been successfully updated and Sales Order generated - Sales Order No. " + IsSalesOrderExist.ToString() + "");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
            }
            else
            {
                objservice.ReOpenOrder(Convert.ToString(Request.QueryString["OrderNo"]));
                objservice = null;
                lb_Update.Visible = true;
                lb_Satistic.Visible = false;

                var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
            }           
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
                DropDownList dd_Excise_PostingGrpCode = e.Item.FindControl("dd_Excise_PostingGrpCode") as DropDownList;
                DropDownList dd_TaxGroupCode = e.Item.FindControl("dd_TaxGroupCode") as DropDownList;
                //if (dd_TaxGroupCode != null)
                //{
                //    BindTextGroupCode(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ItemNo")), dd_TaxGroupCode);
                //}

                if (dd_Excise_PostingGrpCode != null)
                {
                    BindExcise_Prod_PostingGroupCode(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ItemNo")), dd_Excise_PostingGrpCode);
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

    #region Buttons Event
    protected void lb_Satistic_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_home_logo.Visible = false;
            lb_Preview.Visible = true;
            lb_SalesOrder_Released.Visible = false;
            lb_Satistic.Visible = false;
            string SalesOrder = Convert.ToString(ViewState["SalesOrder"]);
            ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "javascript: OpenFullScreenWindow('" + SalesOrder + "'); ", true);
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void lb_Preview_Click(object sender, EventArgs e)
    {
        try
        {
            lb_Preview.Visible = false;
            lbl_home_logo.Visible = false;
            lb_Satistic.Visible = false;
            lb_SalesOrder_Released.Visible = true;
            string SalesOrder = Convert.ToString(ViewState["SalesOrder"]);
            ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "javascript: OpenFullScreenWindowPreview('" + SalesOrder + "'); ", true);
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void lb_SalesOrder_Released_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objser1 = new Web_Order_Mail();
            objser1.UseDefaultCredentials = true;
            objser1.Credentials = NetCredentials;
            objser1.ReleaseSalesOrder(Convert.ToString(ViewState["SalesOrder"]));
            objser1 = null;

            var message = new JavaScriptSerializer().Serialize("Sales Order No: " + Convert.ToString(ViewState["SalesOrder"]) + " has been released successfully.");
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }   

    protected void drplocationcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindNoSeriesCodeLocationWise(drplocationcode.SelectedValue);
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