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
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Text;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION


public partial class PrintAccountStatementReceipt : System.Web.UI.Page
{     
    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    #endregion

    #region "Events"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (Convert.ToString(Request["CustomerNo"]) != "")
            {

                if (txtStartDate.Text != null && txtEndDate.Text != null && txtEndDate.Text != "" && txtEndDate.Text != "")
                {
                    DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
                    DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
                    DateTime todayDate = DateTime.Now;

                    if (enddate < startdate)
                    {
                        var message = new JavaScriptSerializer().Serialize("End Date should not be less than Start Date.");
                        var script = string.Format("alert({0});", message);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                    }
                    else
                    {
                        //Web_Order_Mail objser = new Web_Order_Mail();
                        //objser.UseDefaultCredentials = true;
                        //objser.Credentials = NetCredentials;

                        //string str = ConfigurationManager.AppSettings["FilePath"] + objser.CustomerLedgerPrint(Convert.ToString(Request["CustomerNo"]), Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), Convert.ToBoolean(0));
                        //Response.Clear();
                        //Response.ContentType = "application/pdf";                                                                                                                                                            
                        //Response.WriteFile(str);
                        //Response.Flush();
                        Response.Redirect("PrintActReceipt.aspx?CustomerNo=" + Request["CustomerNo"].ToString() + "&startdate=" + startdate + "&enddate=" + enddate);
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter Start date and End Date both.";
                    lblMessage.Visible = true;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        lblMessage.Visible = false;
    }
    
    protected void btnExcel_Download_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (txtStartDate.Text != null && txtEndDate.Text != null && txtEndDate.Text != "" && txtEndDate.Text != "")
            {
                DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
                DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
                DateTime todayDate = DateTime.Now;

                if (enddate < startdate)
                {
                    var message = new JavaScriptSerializer().Serialize("End Date should not be less than Start Date.");
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                }
                else
                {
                    Web_Order_Mail objser = new Web_Order_Mail();
                    objser.UseDefaultCredentials = true;
                    objser.Credentials = NetCredentials;
                    string str = ConfigurationManager.AppSettings["FilePath"] + objser.CustomerLedgerPrint(Convert.ToString(Request["CustomerNo"]), Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), Convert.ToBoolean(1));

                    hf_Excel.Value = str;
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: AddPathExcel(); ", true);

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("content-disposition", "attachment; filename=\"" + str + "\"");
                    Response.WriteFile(str);
                    Response.Flush();
                    Response.End();
                    Response.Close();                 
                }
            }
            else
            {
                lblMessage.Text = "Please enter Start date and End Date both.";
                lblMessage.Visible = true;
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