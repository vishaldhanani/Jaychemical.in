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
using System.Net.Mail;
using System.Net;
using BO_SERV;
using BO_LINES;
using WS_C_FUN;
using System.Configuration;
using System.Web.Script.Serialization;
#endregion "Library"

public partial class ForgotPassword : System.Web.UI.Page
{

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.EndFrontUserSession(HttpContext.Current);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region "Button Click Events"

    protected void Bnt_SendMail_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (txt_UserId.Text != null && txt_EmailId.Text != null && txt_UserId.Text != "" && txt_EmailId.Text != "")
            {
                Web_Order_Mail objser = new Web_Order_Mail();
                objser.UseDefaultCredentials = true;
                objser.Credentials = NetCredentials;

                Boolean bol = objser.SendVerificationCode(txt_UserId.Text, txt_EmailId.Text);
                if (bol == true)
                {
                    Response.Write("<script language='javascript'>window.alert('Verification code has been sent successfully in your mail.');</script>");
                    ShowSection2();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Please enter valid User Id and E-mail Id.');</script>");
                    txt_UserId.Text = string.Empty;
                    txt_EmailId.Text = string.Empty;
                }

            }
            else
            {
                Response.Write("<script language='javascript'>window.alert('Please enter User Id and E-mail Id.');</script>");
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true); 
        }
    }

    public void ShowSection2()
    {
        try
        {
            UserId.Visible = false;
            MailId.Visible = false;
            Bnt_SendMail.Visible = false;
            UserId1.Visible = true;
            MailId1.Visible = true;
            Vcode.Visible = true;
            Conf_PWD.Visible = true;
            ReConf_PWD.Visible = true;
            Btn_Submit.Visible = true;
            txt_UserId1.Text = txt_UserId.Text;
            txt_EmailId1.Text = txt_EmailId.Text;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"];
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"];

            if (txt_VerifyCode.Text != null && txt_ConfirmPwd.Text != null && txtRe_ConfirmPwd.Text != null)
            {
                Web_Order_Mail objser = new Web_Order_Mail();
                objser.UseDefaultCredentials = true;
                objser.Credentials = NetCredentials;

                Boolean bol1 = objser.IsVarified(txt_UserId1.Text, txt_VerifyCode.Text, txt_ConfirmPwd.Text);
                if (bol1 == true)
                {
                    Response.Write("<script language='javascript'>window.alert('Password has been changed successfully.');window.location='LogIn.aspx';</script>");
                    ClearData();
                }
                else                
                {
                    Response.Write("<script language='javascript'>window.alert('Please check your verification code.');</script>");
                }
            }
            else
            {
                Response.Write("<script language='javascript'>window.alert('Please enter Verification Code, Confirm-Password and Reconfirm-Password.');</script>");
            }
        }
        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);            
        }
    }

    public void ClearData()
    {
        try
        {
            txt_UserId1.Text = string.Empty;
            txt_EmailId1.Text = string.Empty;
            txt_VerifyCode.Text = string.Empty;
            txt_ConfirmPwd.Text = string.Empty;
            txtRe_ConfirmPwd.Text = string.Empty;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    #endregion
  
}