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

public partial class InternalSalesOrder_Preview : System.Web.UI.Page
{
    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (Request["SalesOrder"].ToString() != "")
            {
                Web_Order_Mail objser = new Web_Order_Mail();
                objser.UseDefaultCredentials = true;
                objser.Credentials = NetCredentials;

                //string st = objser.SalesOrderPreview(Convert.ToString(Request["SalesOrder"]));
                string str = ConfigurationManager.AppSettings["FilePath"] + objser.SalesOrderPreview(Convert.ToString(Request["SalesOrder"]));
                objser.ShipmentScheduleViewed(Convert.ToString(Request["SalesOrder"]));

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.WriteFile(str);
                Response.Flush();
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