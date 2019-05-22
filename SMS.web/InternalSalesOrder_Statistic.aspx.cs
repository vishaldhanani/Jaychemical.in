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
using SO_Statistics;
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"


public partial class InternalSalesOrder_Statistic : System.Web.UI.Page
{
    #region Variable
    SalesOrderStatistics objbo = new SalesOrderStatistics();
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
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
                if (Convert.ToString(Request.QueryString["SalesOrder"]) != "")
                {
                    lblOrderNo.Text = Convert.ToString(Request.QueryString["SalesOrder"]);
                }
                StatisticData();
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
    public void StatisticData()
    {
        try
        {
            // Called SalesOrderStatistic function Codeunit Function.
            SalesOrderStatistics so = new SalesOrderStatistics();
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objser1 = new Web_Order_Mail();
            objser1.UseDefaultCredentials = true;
            objser1.Credentials = NetCredentials;
            objser1.SalesOrderStatistics(Convert.ToString(Request.QueryString["SalesOrder"]));
            objser1 = null;

            // Filter with Sales Order No and Fetched Statistic Data
            SalesOrderStatistics_Service objso = new SalesOrderStatistics_Service();
            objso.UseDefaultCredentials = true;
            objso.Credentials = NetCredentials;
            List<SalesOrderStatistics_Filter> filterArray = new List<SalesOrderStatistics_Filter>();
            SalesOrderStatistics_Filter Filter = new SalesOrderStatistics_Filter();
            Filter.Field = SalesOrderStatistics_Fields.No;
            Filter.Criteria = Convert.ToString(Request.QueryString["SalesOrder"]);
            filterArray.Add(Filter);
            SalesOrderStatistics[] ListData = objso.ReadMultiple(filterArray.ToArray(), null, 1);

            if (ListData.Length > 0)
            {
                lbl_Amt_Excl_VAT.Text = (ListData[0].TotalSalesLine1_Line_Amount).ToString("0.00");
                lbl_Total_Excl_VAT.Text = (ListData[0].TotalAmount11).ToString("0.00");
                lbl_Charges_Amt.Text = (ListData[0].TotalSalesLine1_Charges_To_Customer).ToString("0.00");
                lbl_Tax_Amt.Text = (ListData[0].TotalSalesLine1_Tax_Amount).ToString("0.00");
                lbl_Net_total.Text = (ListData[0].TotalSalesLine1_Amount_To_Customer).ToString("0.00");
                lbl_Excise_Amt.Text = (ListData[0].TotalSalesLine1_Excise_Amount).ToString("0.00");
            }
            objso = null;
            so = null;
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