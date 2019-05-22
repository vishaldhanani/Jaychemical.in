#region "Library"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qtm.Lib;
using System.IO;

#endregion "Library"

// Coding By Raj Shah - JAY APPLICATION

public partial class LogIn : System.Web.UI.Page
{
    #region "Page events"   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!Page.IsPostBack)
            {
                //tb_UserId.Focus();
                SessionManager.ClearCart(HttpContext.Current);
                SessionManager.EndFrontUserSession(HttpContext.Current);
                tb_UserId.Attributes["placeholder"] = "User Id";
                tb_Password.Attributes["placeholder"] = "Enter PIN No.";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    protected void Link_UserManual_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=CLiCK_User_Manual.pdf"); 
            //Response.TransmitFile(Server.MapPath("~/PDF/CLiCK_User_Manual.pdf"));
            //Response.End();

            const int bufferLength = 100000;
            byte[] buffer = new Byte[bufferLength];
            int length = 0;
            Stream download = null;
            try
            {
                download = new FileStream(Server.MapPath("~/PDF/CLiCK_User_Manual.pdf"), FileMode.Open, FileAccess.Read);
                do
                {
                    if (Response.IsClientConnected)
                    {
                        length = download.Read(buffer, 0, bufferLength);
                        Response.OutputStream.Write(buffer, 0, length);
                        buffer = new Byte[bufferLength];
                    }
                    else
                    {
                        length = -1;
                    }
                }
                while (length > 0);
                Response.Flush();
                Response.End();
            }
            finally
            {
                if (download != null)
                    download.Close();
            }
        }                
        catch (Exception ex)
        {            
            throw ex;
        }
    }
    #endregion
}