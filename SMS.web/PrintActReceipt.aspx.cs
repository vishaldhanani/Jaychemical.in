﻿#region "Library"
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
public partial class PrintActReceipt : System.Web.UI.Page
{
    #region "Page events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            Web_Order_Mail objser = new Web_Order_Mail();
            objser.UseDefaultCredentials = true;
            objser.Credentials = NetCredentials;

            string str = ConfigurationManager.AppSettings["FilePath"] + objser.CustomerLedgerPrint(Convert.ToString(Request["CustomerNo"]), Convert.ToDateTime(Request["startdate"]), Convert.ToDateTime(Request["enddate"]), Convert.ToBoolean(0));
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.WriteFile(str);
            Response.Flush();
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