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
using WS_C_FUN;
using System.Net;
using System.Configuration;
using System.Web.Script.Serialization;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION
public partial class BankingInfoAccountwise : System.Web.UI.Page
{
    Random rnd = new Random();
    #region "Global Variables"
    List<AccountStmtSummary> list = new List<AccountStmtSummary>();
    #endregion
   
    #region Variable

    decimal TotalPrice = 0;
    
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
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
                

      
                CalendarExtender2.SelectedDate = DateTime.Today;
                BindAcc_SummaryData();
                if (Request["Name"].ToString() != "")
                {
                    lblName.Text = Request["Name"].ToString();
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
    private void BindAcc_SummaryData()
    {
        try
        {
            if (Request["Customer"].ToString() != "" && Request["Customer"].ToString() != null)
            {
                list = Qtm.Lib.AccountStmtSummary.ListTotalOutStanding(SessionManager.GetAgentCode(HttpContext.Current), Request["Customer"]);
            }
            else
            {                
                list = null; 
            }          
            if (list != null && list.Count > 0)
            {
                if (list.Count > 0)
                {
                    rpt_Account_Stmt.DataSource = list;
                    rpt_Account_Stmt.DataBind();
                }
                else
                {
                    rpt_Account_Stmt.DataSource = null;
                    rpt_Account_Stmt.Visible = false;
                    rpt_Account_Stmt.DataBind();
                   // l_Error.Text = "Total OutStanding Detial is not found.";
                    l_Error.Visible = true;
                }
            }
            else
            {
                rpt_Account_Stmt.DataSource = null;
                rpt_Account_Stmt.Visible = false;
                rpt_Account_Stmt.DataBind();
               // l_Error.Text = "Total OutStanding Detial is not found.";
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

    protected void rpt_TotalOutStandingSumm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {                
                TotalPrice += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string dt = System.DateTime.Now.ToString("dd/MM/yy");
                Label td = (Label)e.Item.FindControl("tdAmt"); //Where TD1 is the ID of the Table Cell
                Label td1 = (Label)e.Item.FindControl("tdduedate"); //Where TD1 is the ID of the Table Cell
                if (Convert.ToDateTime(td1.Text) < Convert.ToDateTime(dt))
                {
                    
                    td.Attributes.Add("style", "color: red;");
                    
                }
                else
                {
                   
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {                
                Label l_TotalPrice = e.Item.FindControl("l_TotalPrice") as Label;
                if (l_TotalPrice != null)
                {                    
                    l_TotalPrice.Text = TotalPrice.ToString("#,##0"); 
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
    protected void chk_Select_CheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox chk = sender as CheckBox;
        decimal amount = 0;
       RepeaterItem ri = chk.NamingContainer as RepeaterItem;
       if (ri != null)
       {
           
           Label tb_Qty1 = ri.FindControl("tdAmt") as Label;
           TextBox txtPrice = ri.FindControl("txtAmounttoVerify") as TextBox;
           if (chk.Checked == true)
           {
               if (tb_Qty1.Text != "")
               {
                   txtPrice.Text = tb_Qty1.Text;
                   
                   if (txt_PaymentAmount.Text != "")
                   {
                       
                       amount = Convert.ToDecimal(txtPrice.Text) + Convert.ToDecimal(txt_PaymentAmount.Text);
                   }
                   else
                   {
                       amount = Convert.ToDecimal(txtPrice.Text) + 0;
                   }
                   txt_PaymentAmount.Text = Convert.ToString(amount);
                    
               }
           }
           else
           {
               if (txt_PaymentAmount.Text != "")
               {

                   txt_PaymentAmount.Text = Convert.ToString(Convert.ToDecimal(txt_PaymentAmount.Text) - Convert.ToDecimal(txtPrice.Text));
                   txtPrice.Text = "";
               }
               else
               {
                   txtPrice.Text = "";
               }
           }
       }
    }
    protected void txtAmounttoVerify_TextChanged(object sender, EventArgs e)
        {
        TextBox chk = sender as TextBox;
       RepeaterItem ri = chk.NamingContainer as RepeaterItem;
       if (ri != null)
       {
           Label tb_Qty1 = ri.FindControl("tdAmt") as Label;
           TextBox txtPrice = ri.FindControl("txtAmounttoVerify") as TextBox;
           if (txtPrice.Text != "")
           {
               if (Convert.ToDecimal(txtPrice.Text) > Convert.ToDecimal(tb_Qty1.Text))
               {
                   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter less Amount or equal amount of invoice.');", true);
                   txtPrice.Focus();
               }
               else
               {
                   

                   if (txt_PaymentAmount.Text != "")
                   {

                       txt_PaymentAmount.Text = Convert.ToString(Convert.ToDecimal(txtPrice.Text) + Convert.ToDecimal(txt_PaymentAmount.Text) - Convert.ToDecimal(tb_Qty1.Text));
                   }
                   else
                   {
                       txt_PaymentAmount.Text = Convert.ToString((Convert.ToDecimal(txtPrice.Text) + 0));
                   }
               }
           }
           
       }

    }
    protected void lb_Finish_Click(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        
         RepeaterItem ri = chk.NamingContainer as RepeaterItem;
         if (ri != null)
         {
             Label tb_Qty1 = ri.FindControl("tdAmt") as Label;
             TextBox txtPrice = ri.FindControl("txtAmounttoVerify") as TextBox;
             CheckBox chk1 = ri.FindControl("chk_Select") as CheckBox;
             if (chk1.Checked == true)
             {
 
             }
         }
        try
        {
            NetworkCredential NetCredentials = new NetworkCredential();
            NetCredentials.UserName = ConfigurationManager.AppSettings["UserName"]; // "JCIL\\ADMINISTRATOR"; //"MSPL\\NAVTRN1"; //
            NetCredentials.Password = ConfigurationManager.AppSettings["Password"]; //"JCILADMIN@123"; //"admin@12"; //

            Web_Order_Mail objser1 = new Web_Order_Mail();
            objser1.UseDefaultCredentials = true;
            objser1.Credentials = NetCredentials;
            string bankname = string.Empty;
            string bankbranch = string.Empty;
            string modeofpayment = string.Empty;
            decimal amount = 0;
            string accountno = string.Empty;
            string customerno = Convert.ToString(Request["Customer"]);
            DateTime d = Convert.ToDateTime(txtDate.Text);
            if (tb_BankName.Text != "" && tb_BankName.Text != string.Empty)
            {
                bankname = tb_BankName.Text;
            }
            if (txt_BankBranch.Text != "" && txt_BankBranch.Text != string.Empty)
            {
                bankbranch = txt_BankBranch.Text;
            }
            if (drpModeofPayment.SelectedItem.Text != "")
            {
                modeofpayment = drpModeofPayment.SelectedItem.Text;
            }
            if (txt_PaymentAmount.Text != "" && txt_PaymentAmount.Text != string.Empty)
            {
                amount = Convert.ToDecimal(txt_PaymentAmount.Text);
            }
            if (txtAccountNo.Text != "" && txtAccountNo.Text != string.Empty)
            {
                accountno = txtAccountNo.Text;
            }
            if (txtDate.Text != "" && txtDate.Text != string.Empty)
            {
                d = Convert.ToDateTime(txtDate.Text);
            }
            //string strdata = objser1.SendRTGSForm(bankname, bankbranch, modeofpayment, amount, accountno, customerno, d, Convert.ToString(rnd.Next(1, 10000)));
            objser1 = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

    