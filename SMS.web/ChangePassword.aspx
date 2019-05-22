<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

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
      <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
<link href="css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script src="js/AutoComplete.js" type="text/javascript"></script>
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

    <script type="text/javascript">
        $(document).ready(function () {
            $("#tb_Password").keyup(function (event) {

                if (event.keyCode == 13) {
                    $("#btn_Login").click();
                }
            });
        });
    </script>
    <script type="text/javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

    </script>
</head>
<body>
    <form runat="server" id="frm_Customer">
        <main>            
        <header>
            <div class="wrap">                
                <div class="inner_logo"><a href="#"><img src="images/logo.png" /></a>                   
                </div>
            </div>
        </header>
       <div id="middle">
        
        <div class="wrap">
                 <div style="font-size:40px; text-align:center;  font-weight:200">Change Password</div><br />
           
            <table border="0" width="100%" style="flex-align:initial">
            
            <tr runat="server" id="UserId" visible="true">
            <td align="right"  style="flex-align:start">
                <asp:Label  runat="server" ID="lblUser_Id" align="center"  Font-Size="20px" Text="Old Password :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_UserId" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:end">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11" TextMode="Password" onkeydown = "return (event.keyCode!=13);" onkeyup="checkDec(this);" Height="25px" Placeholder="******" ID="txt_UserId" Width="200px"></asp:TextBox>
             </td>
            </tr>

            <tr runat="server" id="Tr1" visible="true">
            <td align="right"  style="flex-align:start">
                <asp:Label  runat="server" ID="Label3" align="center"  Font-Size="20px" Text="New Password :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPassword" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:end">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11" TextMode="Password" onkeydown = "return (event.keyCode!=13);" onkeyup="checkDec(this);"  Height="25px" Placeholder="******" ID="txtNewPassword" Width="200px"></asp:TextBox>
             </td>
            </tr>            
                <tr>
                    <td></td>

                   <td align="left" style="flex-align:initial;">
                       <br />
                        <asp:LinkButton ID="Bnt_UpdatePassword" runat="server"  CausesValidation="true" OnClick="Bnt_SendMail_Click" Text="Update Password" class="u_f_btn12 links" />
                    </td>
                </tr>
                <tr></tr>
                <tr></tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblMessage" Visible="false" ></asp:Label>
                    </td>
                </tr>
 
                 <tr runat="server" id="UserId1" visible="false">
            <td align="right" style="flex-align:start">
                <asp:Label  runat="server" ID="Label1" align="center"  Font-Size="14px" Text="User ID :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_UserId1" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:end">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11"  Height="25px" Enabled="false" Placeholder="User ID" ID="txt_UserId1" Width="200px"></asp:TextBox>
             </td>
            </tr>

                 <tr runat="server" id="MailId1" visible="false">
            <td align="right" style="flex-align:start">
                <asp:Label runat="server" ID="Label2" align="right" Font-Size="14px" Text="Email ID :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_EmailId1" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:initial; width:55%">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11" Enabled="false" Height="25px" Placeholder="Email ID" ID="txt_EmailId1" Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="txt_EmailId1" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

             </td>
            </tr>


                  <tr runat="server" id="Vcode" visible="false">
            <td align="right" style="flex-align:start">
                <asp:Label runat="server" ID="lblKey" align="right" Font-Size="14px" Text="Verify Code :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_VerifyCode" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:initial">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11"  Height="25px" Placeholder="Verify Code" ID="txt_VerifyCode" Width="200px"></asp:TextBox>
             </td>
            </tr>

                 <tr runat="server" id="Conf_PWD" visible="false">
            <td align="right" style="flex-align:start">
                <asp:Label runat="server" ID="lblCon_Password" align="right" Font-Size="14px" Text="Confirm Password :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txt_ConfirmPwd" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:initial">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11"  Height="25px" TextMode="Password" Placeholder="Confirm Password" ID="txt_ConfirmPwd" Width="200px"></asp:TextBox>
             </td>
            </tr>

                <tr runat="server" id="ReConf_PWD" visible="false">
            <td align="right" style="flex-align:start">
                <asp:Label runat="server" ID="lbl_ReConPwd" align="right" Font-Size="14px" Text="Re-Confirm Password :"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtRe_ConfirmPwd"  ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </td>
            <td align="left" style="flex-align:initial">
                <asp:TextBox runat="server" CssClass="u_oder_fill_white_label11" TextMode="Password"  Height="25px" Placeholder="Re-Confirm Password" ID="txtRe_ConfirmPwd" Width="200px"></asp:TextBox>
             </td>
            </tr>

                <tr>
                    <td></td>
                    <td align="left" style="flex-align:initial;">
                        <asp:LinkButton ID="Btn_Submit" runat="server" CausesValidation="true"  Visible="false"  Text="Submit" class="u_f_btn12 links" />

                    </td>

                </tr>

            </table>
        	</div>
           </div>                        
       <footer>            
    	    <div class="wrap">
               </div>                            
        </footer>
             
     </main>
    </form>
</body>
</html>
