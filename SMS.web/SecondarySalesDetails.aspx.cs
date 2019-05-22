#region "Library"
using Qtm.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

#endregion "Library"

public partial class SecondarySalesDetails : System.Web.UI.Page
{
    #region Variable
    List<Items> list = new List<Items>();
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
            SessionManager.GetItemCategoryCode(HttpContext.Current);
            if (!string.IsNullOrEmpty(Request.QueryString["CompName"]))
            {
                lb_CompanyName.Text = Convert.ToString(Request.QueryString["CompName"]);
            }
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
            if (!IsPostBack)
            {
                SetInitialRow();
                lblUserName.Text = SessionManager.GetAgentName(HttpContext.Current);
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

    #endregion

    #region "Edit and Delete Command event"
    #endregion

    //protected void CurrentYearMonthBound()
    //{
    //    try
    //    {
    //        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
    //        for (int i = 0; i < Gridview3.Rows.Count; i++)
    //        {
    //            DropDownList lb_Date = (DropDownList)Gridview3.Rows[i].Cells[7].FindControl("lb_Date");
    //            for (int j = 1; j < 13; j++)
    //            {
    //                lb_Date.Items.Add(new ListItem(info.GetMonthName(j) + " - " + DateTime.Now.Year.ToString(), j.ToString()));
    //            }
    //            lb_Date.Items.Insert(0, new ListItem("-Select-", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            for (int i = 0; i < Convert.ToInt16(ConfigurationManager.AppSettings["SecondarySalesLine"]); i++)
            {
                if (i == 0)
                {
                    dt.Columns.Add(new DataColumn("No", typeof(string)));
                    dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
                    dt.Columns.Add(new DataColumn("Location", typeof(string)));
                    dt.Columns.Add(new DataColumn("Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("ItemList", typeof(string)));
                    dt.Columns.Add(new DataColumn("ProductCategory", typeof(string)));
                    dt.Columns.Add(new DataColumn("Range", typeof(string)));
                    dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                    // dt.Columns.Add(new DataColumn("Date", typeof(string)));
                    dt.Columns.Add(new DataColumn("Month", typeof(string)));
                    

                }
                dr = dt.NewRow();
                dr["No"] = i + 1;
                dr["CustomerName"] = string.Empty;
                dr["Location"] = string.Empty;
                dr["Code"] = string.Empty;
                dr["ItemList"] = string.Empty;
                dr["ProductCategory"] = string.Empty;
                dr["Range"] = string.Empty;
                dr["Quantity"] = string.Empty;
                // dr["Date"] = string.Empty;
                dr["Month"] = string.Empty;
                
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;

            //Store the DataTable in ViewState
            Gridview3.DataSource = dt;
            Gridview3.DataBind();
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void SaveGridLineData()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview3.Rows[rowIndex].Cells[1].FindControl("lb_CustomerName");
                        TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("lb_Location");
                        DropDownList box3 = (DropDownList)Gridview3.Rows[rowIndex].Cells[4].FindControl("ddl_ItemList");
                        TextBox box4 = (TextBox)Gridview3.Rows[rowIndex].Cells[5].FindControl("lb_ProductCategory");
                        TextBox box5 = (TextBox)Gridview3.Rows[rowIndex].Cells[6].FindControl("lb_Range");
                        TextBox box6 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Quantity");
                        //TextBox box7 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Date");
                        DropDownList box8 = (DropDownList)Gridview3.Rows[rowIndex].Cells[8].FindControl("ddlMonth");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["No"] = i;

                        dtCurrentTable.Rows[i - 1]["CustomerName"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Location"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["ItemList"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["ProductCategory"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Range"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = box6.Text;
                        // dtCurrentTable.Rows[i - 1]["Date"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["Month"] = box8.SelectedValue;

                        rowIndex++;
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;
                    Gridview3.DataSource = dtCurrentTable;
                    Gridview3.DataBind();


                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddl_ItemList1 = (DropDownList)Gridview3.Rows[i].Cells[0].FindControl("ddl_ItemList");
                            DropDownList ddlMonth = (DropDownList)Gridview3.Rows[i].Cells[0].FindControl("ddlMonth");
                            ddl_ItemList1.SelectedValue = dtCurrentTable.Rows[i]["ItemList"].ToString();
                            ddlMonth.SelectedValue = dtCurrentTable.Rows[i]["Month"].ToString();
                        }
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

    private void AddNewRowToGrid()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview3.Rows[rowIndex].Cells[1].FindControl("lb_CustomerName");
                        TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("lb_Location");
                        DropDownList box3 = (DropDownList)Gridview3.Rows[rowIndex].Cells[4].FindControl("ddl_ItemList");
                        TextBox box4 = (TextBox)Gridview3.Rows[rowIndex].Cells[5].FindControl("lb_ProductCategory");
                        TextBox box5 = (TextBox)Gridview3.Rows[rowIndex].Cells[6].FindControl("lb_Range");
                        TextBox box6 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Quantity");
                        // TextBox box7 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Date");
                        DropDownList box8 = (DropDownList)Gridview3.Rows[rowIndex].Cells[8].FindControl("ddlMonth");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["No"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["CustomerName"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Location"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["ItemList"] = box3.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ProductCategory"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Range"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = box6.Text;
                        // dtCurrentTable.Rows[i - 1]["Date"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["Month"] = box8.Text;


                        rowIndex++;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    Gridview3.DataSource = dtCurrentTable;
                    Gridview3.DataBind();
                }
                else
                {
                    SetInitialRow();
                }
            }
            else
            {
                Response.Write("Records are not available.");
            }
            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        catch (Exception ex)
        {

            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    private void SetPreviousData()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)Gridview3.Rows[rowIndex].Cells[1].FindControl("lb_CustomerName");
                        TextBox box2 = (TextBox)Gridview3.Rows[rowIndex].Cells[2].FindControl("lb_Location");
                        DropDownList box3 = (DropDownList)Gridview3.Rows[rowIndex].Cells[4].FindControl("ddl_ItemList");
                        TextBox box4 = (TextBox)Gridview3.Rows[rowIndex].Cells[5].FindControl("lb_ProductCategory");
                        TextBox box5 = (TextBox)Gridview3.Rows[rowIndex].Cells[6].FindControl("lb_Range");
                        TextBox box6 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Quantity");
                        //  TextBox box7 = (TextBox)Gridview3.Rows[rowIndex].Cells[7].FindControl("lb_Date");
                        DropDownList box8 = (DropDownList)Gridview3.Rows[rowIndex].Cells[8].FindControl("ddlMonth");

                        box1.Text = dt.Rows[i]["CustomerName"].ToString();
                        box2.Text = dt.Rows[i]["Location"].ToString();
                        box3.SelectedValue = dt.Rows[i]["ItemList"].ToString();
                        box4.Text = dt.Rows[i]["ProductCategory"].ToString();
                        box5.Text = dt.Rows[i]["Range"].ToString();
                        box6.Text = dt.Rows[i]["Quantity"].ToString();
                        //  box7.Text = dt.Rows[i]["Date"].ToString();
                        box8.SelectedValue = dt.Rows[i]["Month"].ToString();

                        rowIndex++;
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

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRowToGrid();
            EnableTextBoxGird3();
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void Gridview3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl_ItemList = (DropDownList)e.Row.FindControl("ddl_ItemList");
                list = Qtm.Lib.Items.SecondarySalesItemList(Convert.ToString(Request["SplPriGrp"])); //SplPriGrp=FARIDABAD                                
                if (list != null && list.Count > 0)
                {
                    ddl_ItemList.DataSource = list;
                    ddl_ItemList.DataTextField = "Description";
                    ddl_ItemList.DataValueField = "Description";
                    ddl_ItemList.DataBind();
                }
                ddl_ItemList.Items.Insert(0, new ListItem("-Select Product-", "0"));
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }

    protected void ddl_ItemList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            GridViewRow gvRow = (GridViewRow)ddl.NamingContainer;
            if (gvRow != null)
            {
                TextBox lb_ProductName = gvRow.FindControl("lb_ProductName") as TextBox;
                DropDownList ddl_ItemList = gvRow.FindControl("ddl_ItemList") as DropDownList;
                HiddenField hf_ItemNo = gvRow.FindControl("hf_ItemNo") as HiddenField;
                TextBox lb_ProductCategory = gvRow.FindControl("lb_ProductCategory") as TextBox;
                TextBox lb_Range = gvRow.FindControl("lb_Range") as TextBox;
                TextBox lb_Quantity = gvRow.FindControl("lb_Quantity") as TextBox;
                DropDownList ddlMonth = gvRow.FindControl("ddlMonth") as DropDownList;

                foreach (GridViewRow rows in Gridview3.Rows)
                {
                    String[] CodeAndDesc = ddl_ItemList.SelectedValue.Split('-');
                    if (CodeAndDesc.Length >= 1)
                    {
                        String Desc = CodeAndDesc[0].Trim();
                        list = Qtm.Lib.Items.GetProdCategoryAndRange(Convert.ToString(Desc));
                        if (list.Count > 0 && list != null)
                        {
                            lb_ProductCategory.Text = Convert.ToString(list[0].ProdCateGory);
                            lb_Range.Text = Convert.ToString(list[0].Range);
                            hf_ItemNo.Value = Convert.ToString(list[0].ItemNo);
                            ddlMonth.Focus();
                        }
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

    //protected void lb_Quantity_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox lb = (TextBox)sender;
    //        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;            
    //        String Desc = string.Empty;
    //        if (gvRow != null)
    //        {
    //            foreach (GridViewRow rows in Gridview3.Rows)
    //            {                    
    //                HiddenField hf_ItemNo = gvRow.FindControl("hf_ItemNo") as HiddenField;
    //                DropDownList ddl_ItemList = gvRow.FindControl("ddl_ItemList") as DropDownList;                                     
    //                TextBox txtQuantity = gvRow.FindControl("lb_Quantity") as TextBox;
    //                TextBox lb_Date = gvRow.FindControl("lb_Date") as TextBox;

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

    public void EnableTextBoxGird3()
    {
        try
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            int count = int.Parse(dtCurrentTable.Rows.Count.ToString());
            for (int i = 0; i < count; i++)
            {
                TextBox lb_Quantity = (TextBox)Gridview3.Rows[i].Cells[7].FindControl("lb_Quantity");
                TextBox lb_CustomerName = (TextBox)Gridview3.Rows[i].Cells[1].FindControl("lb_CustomerName");
                TextBox lb_Location = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("lb_Location");
                //TextBox lb_Date = (TextBox)Gridview3.Rows[i].Cells[7].FindControl("lb_Date");
                DropDownList ddl_ItemList = (DropDownList)Gridview3.Rows[i].Cells[4].FindControl("ddl_ItemList");
                DropDownList ddlMonth = (DropDownList)Gridview3.Rows[i].Cells[8].FindControl("ddlMonth");

                if (lb_Quantity.Text == string.Empty)
                {
                    lb_Quantity.Attributes.Remove("readonly");
                    lb_CustomerName.Attributes.Remove("readonly");
                    lb_Location.Attributes.Remove("readonly");
                    // lb_Date.Attributes.Remove("readonly");
                    ddl_ItemList.Attributes.Remove("readonly");
                }
                else
                {
                    lb_Quantity.Attributes.Add("readonly", "readonly");
                    lb_CustomerName.Attributes.Add("readonly", "readonly");
                    lb_Location.Attributes.Add("readonly", "readonly");
                    //lb_Date.Attributes.Add("readonly", "readonly");
                    ddl_ItemList.Attributes.Add("readonly", "readonly");
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

    protected void bt_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lb = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            if (gvRow != null)
            {
                int RowIndex = gvRow.RowIndex;
                foreach (GridViewRow rows in Gridview3.Rows)
                {
                    HiddenField hf_ItemNo = gvRow.FindControl("hf_ItemNo") as HiddenField;
                    DropDownList ddl_ItemList = gvRow.FindControl("ddl_ItemList") as DropDownList;
                    DropDownList ddlMonth = gvRow.FindControl("ddlMonth") as DropDownList;

                    TextBox txtQuantity = gvRow.FindControl("lb_Quantity") as TextBox;
                    TextBox lb_Date = gvRow.FindControl("lb_Date") as TextBox;
                    txtQuantity.Text = string.Empty;
                    //lb_Date.Text = string.Empty;
                    // ddlMonth.SelectedValue = "0";
                    EnableTextBoxGird3();
                    txtQuantity.Focus();
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

    protected void bt_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lb = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            if (gvRow != null)
            {
                int RowIndex = gvRow.RowIndex;
                DropDownList ddl_ItemList = gvRow.FindControl("ddl_ItemList") as DropDownList;

                if (ViewState["CurrentTable"] != null && ((DataTable)ViewState["CurrentTable"]).Rows.Count > 0)
                {
                    ((DataTable)ViewState["CurrentTable"]).Rows.RemoveAt(RowIndex);
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["CurrentTable"];
                    Gridview3.DataSource = dt;
                    Gridview3.DataBind();
                    EnableTextBoxGird3();
                    SetPreviousData();
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

    #region Button Click Events
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {   //Deleting Blank Lines of Gridview
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            Boolean flag = true;
            for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
            {
                if (dtCurrentTable.Rows[i]["CustomerName"].ToString() == string.Empty && dtCurrentTable.Rows[i]["Location"].ToString() == string.Empty && dtCurrentTable.Rows[i]["ItemList"].ToString() == Convert.ToString(0))
                {
                    dtCurrentTable.Rows[i].Delete();
                }
                else if (dtCurrentTable.Rows[i]["CustomerName"].ToString() == string.Empty && dtCurrentTable.Rows[i]["ItemList"].ToString() == Convert.ToString(0))
                {
                    dtCurrentTable.Rows[i].Delete();
                }

            }
            dtCurrentTable.AcceptChanges();
            DataTable dt = dtCurrentTable;
            //Deleting Blank Lines of Gridview

            //Checked validations of Gridview fields
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                TextBox lb_CustomerName = (TextBox)Gridview3.Rows[i].Cells[1].FindControl("lb_CustomerName");
                TextBox lb_Location = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("lb_Location");
                TextBox lb_Quantity = (TextBox)Gridview3.Rows[i].Cells[7].FindControl("lb_Quantity");
                DropDownList ddlMonth = (DropDownList)Gridview3.Rows[i].Cells[8].FindControl("ddlMonth");
                DropDownList ddl_ItemList = (DropDownList)Gridview3.Rows[i].Cells[4].FindControl("ddl_ItemList");

                if (lb_CustomerName.Text == string.Empty)
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please enter Customer Name.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                    //this.lblMessage.Text = "Please enter Customer Name."; 
                    flag = false;
                    lb_CustomerName.Focus();
                    break;
                }
                else if (lb_Location.Text == string.Empty)
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please enter Location.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    flag = false;
                    lb_Location.Focus();
                    break;
                }
                else if (ddl_ItemList.SelectedIndex == 0)
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please Select Product.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    ddl_ItemList.Focus();
                    flag = false;
                    break;
                }
                else if (lb_Quantity.Text == string.Empty && lb_Quantity.Text == "0")
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please enter Quantity.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    lb_Quantity.Focus();
                    flag = false;
                    break;
                }
                else if (ddlMonth.SelectedValue == "0")
                {
                    var message1 = new JavaScriptSerializer().Serialize("Please select Month.");
                    var script1 = string.Format("alert({0});", message1);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
                    ddlMonth.Focus();
                    flag = false;
                    break;
                }
            }
            //Checked validations of Gridview fields

            if (flag == true)
            {
                //Created Lines in SQL InternalDB Database's [Secondary Sales Details] Table
                string ItemNo = string.Empty;
                string Description = string.Empty;
                String strConnString = ConfigurationManager.ConnectionStrings["WA_JAYCHEM_INTDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strConnString))
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_SecondarySalesDetails";
                        String[] CodeAndDesc = dtCurrentTable.Rows[i]["ItemList"].ToString().Split('-');
                        if (CodeAndDesc.Length >= 1)
                        {
                            ItemNo = CodeAndDesc[0].Trim();
                            Description = CodeAndDesc[1].Trim();
                        }
                        //string[] sDate = Convert.ToString(dtCurrentTable.Rows[i]["Date"]).Split('/');
                        //string sDateTime = sDate[1] + '/' + sDate[2];

                        string st = "01/" + dtCurrentTable.Rows[i]["Month"] + "/" + DateTime.Now.Year.ToString();
                        DateTime dtt = Convert.ToDateTime(st);
                        DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                        string MonthYear = dinfo.GetMonthName(Convert.ToInt16(dtCurrentTable.Rows[i]["Month"])) + "-" + DateTime.Now.Year.ToString();

                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(dtt);
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = Convert.ToString(dtCurrentTable.Rows[i]["CustomerName"]);
                        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Convert.ToString(dtCurrentTable.Rows[i]["Location"]);
                        cmd.Parameters.Add("@ItemNo", SqlDbType.VarChar).Value = Convert.ToString(ItemNo);
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Convert.ToString(Description);
                        cmd.Parameters.Add("@ItemCategoryCode", SqlDbType.VarChar).Value = Convert.ToString(dtCurrentTable.Rows[i]["ProductCategory"]);
                        cmd.Parameters.Add("@Range", SqlDbType.VarChar).Value = Convert.ToString(dtCurrentTable.Rows[i]["Range"]);
                        cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = Convert.ToDecimal(dtCurrentTable.Rows[i]["Quantity"]);
                        cmd.Parameters.Add("@MonthYear", SqlDbType.VarChar).Value = Convert.ToString(MonthYear);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    con.Dispose();
                }
                //Created Lines in SQL InternalDB Database's [Secondary Sales Details] Table


                //Deleted SecondarySales.xls files if exist in Shared Folder 
                string path = ConfigurationManager.AppSettings["FilePath"] + "SecondarySales.xls";
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
                //Deleted SecondarySales.xls files if exist in Shared Folder 

                //Generated SecondarySales.xls file in Shared Folder with Design of excel
                string Desc = string.Empty;
                StreamWriter wr = new StreamWriter(ConfigurationManager.AppSettings["FilePath"] + "SecondarySales.xls");
                try
                {
                    wr.Write("<HTML><HEAD>");
                    wr.Write("<style> {font-family:Verdana; font-size: 11px; } </style>");
                    wr.Write("</HEAD><BODY>");
                    wr.Write("<TABLE border='1' width='100%' >");
                    wr.Write("<TR>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        wr.Write("<TD align='center' style='color: White; font-weight: bold;background-color:#8A0886;font-size: 12px'>");
                        wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                        wr.Write("</TD>");
                    }
                    
                    wr.Write("</TR>");
                    wr.WriteLine();

                    for (int i = 0; i < (dt.Rows.Count); i++)
                    {
                        wr.Write("<TR>");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            wr.Write("<style> {font-family:Verdana; font-size: 11px; } </style>");
                            wr.Write("<TD align='center'>");
                            if (dt.Rows[i][j] != null)
                            {
                                if (dt.Rows[i][j] == dt.Rows[i][4])
                                {
                                    String[] CodeAndDesc = dt.Rows[i][4].ToString().Split('-');
                                    if (CodeAndDesc.Length >= 1)
                                    {
                                        ItemNo = CodeAndDesc[0];
                                        Desc = CodeAndDesc[1].Trim();

                                    }
                                    wr.Write(Convert.ToString(Desc).ToUpper() + "\t");
                                }
                                else if (dt.Rows[i][j] == dt.Rows[i][8])
                                {
                                    DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                                    string MonthYear12 = Convert.ToString(dinfo.GetMonthName(Convert.ToInt16(dt.Rows[i]["Month"])));// + "-" + DateTime.Now.Year.ToString()                                  
                                    wr.Write((MonthYear12).ToUpper() + "-" + DateTime.Now.Year.ToString() + "\t");
                                }
                                else if (dt.Rows[i][j] == dt.Rows[i][3])
                                {
                                    
                                    wr.Write(ItemNo + "\t");
                                    
                                }
                                else
                                {
                                    wr.Write(Convert.ToString(dt.Rows[i][j]).ToUpper() + "\t");
                                }
                            }
                            else
                            {
                                wr.Write("\t");
                            }
                            wr.WriteLine();
                            wr.Write("</TD>");
                        }
                        wr.Write("</TR>");
                    }
                    wr.Write("</TABLE>");
                    wr.Write("</BODY>");
                    wr.Close();
                    //Generated SecondarySales.xls file in Shared Folder with Design of excel

                    //Mailing to Agents Email as well as General Ledger's Secondary Sales Internal Email 
                    NetworkCredential NetCredentials = new NetworkCredential();
                    NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

                    Web_Order_Mail objser1 = new Web_Order_Mail();
                    objser1.UseDefaultCredentials = true;
                    objser1.Credentials = NetCredentials;
                    objser1.SendMailForSecondarySalesExcel(ConfigurationManager.AppSettings["FilePath"] + "SecondarySales.xls", Convert.ToString(Request["CompanyCode"]));
                    objser1 = null;

                }
                catch (Exception ex)
                {
                    var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                }

                var message1 = new JavaScriptSerializer().Serialize("Mail Sent successfully.");
                var script1 = string.Format("alert({0});window.location='DashBoard.aspx';", message1);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
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

    //protected void lb_Date_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SaveGridLineData();
    //        EnableTextBoxGird3();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void lb_Quantity_TextChanged(object sender, EventArgs e)
    {
        SaveGridLineData();
        EnableTextBoxGird3();
    }

    protected void ddlMonth_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            GridViewRow gvRow = (GridViewRow)ddl.NamingContainer;
            if (gvRow != null)
            {
                DropDownList ddlMonth = gvRow.FindControl("ddlMonth") as DropDownList;
                TextBox txtQunatity = gvRow.FindControl("lb_Quantity") as TextBox;
                txtQunatity.Focus();
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
}