<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title><%= Constant.Title %></title>
<link href="css/fonts.css" rel="stylesheet" type="text/css" />
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="js/html5.js"></script>
<link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
<link href="css/media.css" rel="stylesheet" type="text/css" />
<link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>
<script src="js/AutoComplete.js" type="text/javascript"></script>
<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
<script type="text/javascript">
    function ComparisonPassword() {
        var Password = document.getElementById("txt_ConfirmPwd").value;
        var Confirm_password = document.getElementById("txtRe_ConfirmPwd").value;

        if (Password != Confirm_password) {
            alert("Password does not match the confirm password.");
            txt_ConfirmPwd.focus();
            return false;
        }
        else {
            return true;
        }
    }

</script>
</head>
<body id="forgot-pass">
<form runat="server" id="frm_Customer">
  <main >
	<div class="f-password">
    <div class="l_part_top">
        <div class="l_part_wrap">
          <div class="logo">
            <div class="text_middle"> <img src="images/jay-logo.png" alt="Jay Chemicals"> </div>
          </div>
        </div>
      </div>
<div class="l_part_wrap">
	    <h1>Forgot Password</h1>
	    <br />
	    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		  <tr runat="server" id="UserId" visible="true">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field" Placeholder="User ID" ID="txt_UserId"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_UserId" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
		  </tr>
		  <tr runat="server" id="MailId" visible="true">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field" Placeholder="Email ID" ID="txt_EmailId"></asp:TextBox>
			  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ControlToValidate="txt_EmailId" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
		  </tr>
		  <tr>
		    <td align="center"><asp:LinkButton ID="Bnt_SendMail" runat="server"  CausesValidation="true" OnClick="Bnt_SendMail_Click" Text="Send Mail" class="u_button" /></td>
	      </tr>
		  <tr>
		    <td align="center"><asp:Label runat="server" ID="lblMessage" visible="false" ></asp:Label></td>
	      </tr>
		  <tr runat="server" id="UserId1" visible="false">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field f-field"  Enabled="false" Placeholder="User ID" ID="txt_UserId1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_UserId1" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		  </tr>
		  <tr runat="server" id="MailId1" visible="false">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field f-field" Enabled="false" Placeholder="Email ID" ID="txt_EmailId1"></asp:TextBox>
			  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="txt_EmailId1" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
		  </tr>
		  <tr runat="server" id="Vcode" visible="false">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field f-field"  Placeholder="Verify Code" ID="txt_VerifyCode"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_VerifyCode" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		  </tr>
		  <tr runat="server" id="Conf_PWD" visible="false">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field f-field"  TextMode="Password" Placeholder="Confirm Password" ID="txt_ConfirmPwd"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txt_ConfirmPwd" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		  </tr>
		  <tr runat="server" id="ReConf_PWD" visible="false">
		    <td align="center"><asp:TextBox runat="server" CssClass="u_field f-field" TextMode="Password"  Placeholder="Re-Confirm Password" ID="txtRe_ConfirmPwd"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtRe_ConfirmPwd"  ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		  </tr>
		  <tr>
		    <td align="center"><asp:LinkButton ID="Btn_Submit" runat="server" CausesValidation="true" OnClick="Btn_Submit_Click"  OnClientClick="return ComparisonPassword();" visible="false"  Text="Submit" class="u_button" /></td>
		  </tr>
          <tr visible="false"><td height="80">&nbsp;</td></tr>
        </table>
	  </div>
	</div>
	<footer>
	  <div class="fl_icon"><a href="DashBoard.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
	  <div class="wrap"> </div>
	</footer>
  </main>
</form>
</body>
</html>
