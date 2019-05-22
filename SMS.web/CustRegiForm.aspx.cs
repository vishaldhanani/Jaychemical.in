#region "Library"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qtm.Lib;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
#endregion "Library"

public partial class CustRegiForm : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SessionManager.CheckUserSession(HttpContext.Current, "Login.aspx");
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

    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            for (int i = 0; i < CheckInterestedJCILProduct.Items.Count; i++)
            {
                if (CheckInterestedJCILProduct.Items[i].Selected)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                String strConnString = ConfigurationManager.ConnectionStrings["WA_JAYCHEM_INTDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strConnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_CutomerRegistration";
                    cmd.Parameters.Add("@DistributorName", SqlDbType.VarChar).Value = txtDistributorName.Text.ToUpper();
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = (txtDate.Text);
                    cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = txtCustomerName.Text.ToUpper();
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text.ToUpper();
                    cmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = txtAddress2.Text.ToUpper();
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = txtCity.Text.ToUpper();
                    cmd.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = txtPinCode.Text;
                    cmd.Parameters.Add("@Telephone", SqlDbType.VarChar).Value = txtTelephoneLandline.Text;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = txtMobile.Text;
                    cmd.Parameters.Add("@Fax", SqlDbType.VarChar).Value = txtFax.Text;
                    cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = txtEMail.Text;
                    cmd.Parameters.Add("@EstablishedIn", SqlDbType.VarChar).Value = txtEstablishedIn.Text.ToUpper();
                    cmd.Parameters.Add("@MainContactPerson", SqlDbType.VarChar).Value = txtMainContactPerson.Text.ToUpper();
                    cmd.Parameters.Add("@SisterConsernName", SqlDbType.VarChar).Value = txtSisterConcern.Text.ToUpper();

                    string Type = string.Empty;
                    for (int i = 0; i < CheckType.Items.Count; i++)
                    {
                        if (CheckType.Items[i].Selected)
                        {
                            Type += CheckType.Items[i].Text.Trim().ToUpper() + ",";
                        }
                    }

                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Convert.ToString(Type).ToUpper();
                    cmd.Parameters.Add("@ProductionCapacity", SqlDbType.VarChar).Value = txtProdCapacity.Text.ToUpper();
                    cmd.Parameters.Add("@BusinessVolumePM", SqlDbType.VarChar).Value = txtbusinessVolume.Text.ToUpper();
                    cmd.Parameters.Add("@CompetitionNameAndProductRange", SqlDbType.VarChar).Value = txtCompetitionNameAndProductRange.Text.ToUpper();
                    cmd.Parameters.Add("@ExpectedBusinessVolumePM", SqlDbType.VarChar).Value = txtExpectedBusinessVolume.Text.ToUpper();
                    cmd.Parameters.Add("@Reference", SqlDbType.VarChar).Value = txtReference.Text.ToUpper();

                    string CheckInstProduct = string.Empty;
                    for (int i = 0; i < CheckInterestedJCILProduct.Items.Count; i++)
                    {
                        if (CheckInterestedJCILProduct.Items[i].Selected)
                        {
                            CheckInstProduct += CheckInterestedJCILProduct.Items[i].Text.Trim().ToUpper() + ",";
                        }
                    }
                    cmd.Parameters.Add("@InterestedinJCILProduct", SqlDbType.VarChar).Value = Convert.ToString(CheckInstProduct).ToUpper();
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                var message1 = new JavaScriptSerializer().Serialize("Record has been Saved Successfully.");
                var script1 = string.Format("alert({0});window.location='DashBoard.aspx';", message1);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script1, true);
            }
            else
            {
                var message = new JavaScriptSerializer().Serialize("Please select atleast one Interested JCIL Product.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                CheckInterestedJCILProduct.Focus();
            }

        }

        catch (Exception ex)
        {
            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            var script = string.Format("alert({0});window.location='DashBoard.aspx';", message);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        }
    }
}