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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class SendMail_Customers : System.Web.UI.Page
{
    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblDate.Visible = true;
            tblMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    NetworkCredential NetCredentials = new NetworkCredential();
                    NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

                    Web_Order_Mail objser = new Web_Order_Mail();
                    objser.UseDefaultCredentials = true;
                    objser.Credentials = NetCredentials;
                    txtMail.Text = objser.GetAgentEmailAddr(SessionManager.GetAgentCode(HttpContext.Current));
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

    #region SendMail
    //send for pdf
    protected void btnmail_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (Request["CustomerNo"].ToString() != "")
            {
                if (txtStartDate.Text != null && txtEndDate.Text != null && txtEndDate.Text != "" && txtEndDate.Text != "" && txtMail.Text != "")
                {
                    DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
                    DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
                    DateTime todayDate = DateTime.Now;

                    if (IsValidEmailId(txtMail.Text))
                    {
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
                            objser.SendMailforCustomerLedger(Convert.ToString(Request["CustomerNo"]), txtMail.Text, txtMail1.Text, startdate, enddate, Convert.ToBoolean(0));
                            txtEndDate.Text = "";
                            txtStartDate.Text = "";
                            txtMail.Text = "";
                            txtMail1.Text = "";
                            tblMessage.Visible = true;
                            tblDate.Visible = false;
                        }
                    }
                    else
                    {
                        Label1.Text = "Please enter Start date,End Date and Email.";
                        Label1.Visible = true;
                    }
                }
                else
                {
                    Label1.Text = "Please enter Start date,End Date and Email.";
                    Label1.Visible = true;
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
    private bool IsValidEmailId(string InputEmail)
    {
        //Regex To validate Email Address
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
    }

    //send for excel 
    protected void btnExcel_Mail_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (txtStartDate.Text != null && txtEndDate.Text != null && txtEndDate.Text != "" && txtEndDate.Text != "" && txtMail.Text != "")
            {
                if (IsValidEmailId(txtMail.Text))
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
                        objser.SendMailforCustomerLedger(Convert.ToString(Request["CustomerNo"]), txtMail.Text, txtMail1.Text, startdate, enddate, Convert.ToBoolean(1));

                        txtEndDate.Text = "";
                        txtStartDate.Text = "";
                        txtMail.Text = "";
                        txtMail1.Text = "";
                        tblMessage.Visible = true;
                        tblDate.Visible = false;
                    }
                }
                else
                {
                    Label1.Text = "Please enter Start date,End Date and Email.";
                    Label1.Visible = true;
                }
            }
            else
            {
                Label1.Text = "Please enter Start date,End Date and Email.";
                Label1.Visible = true;
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