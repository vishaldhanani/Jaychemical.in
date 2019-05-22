<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustRegiForm.aspx.cs" Culture="en-GB" Inherits="CustRegiForm" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="True">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/Custom.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Common.js"></script>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />

    <style type="text/css">
        .auto-style3 {
            width: 20%;
            padding-bottom: 1%;
        }

        .auto-style1 {
            width: 36%;
            padding-bottom: 1%;
            color: #8A0886;
        }

        .bottomPadding {
            margin-bottom: 2%;
        }

        .Capital {
            text-transform: uppercase;
        }

        .u_oder_fill_white_label12:focus {
            border: 1px solid #8A0886;
        }
    </style>

    <script type="text/javascript">

        function validateCustRefNo() {
            var pin_code = document.getElementById("txtPinCode");
            var pat1 = /^\d{6}$/;
            var atLeast = 1;
            var checkBoxes = document.getElementById("CheckType").getElementsByTagName("input");            
            var checkBoxes1 = document.getElementById("CheckInterestedJCILProduct").getElementsByTagName("input");

            if (!form1.txtDistributorName.value) {
                alert("Please Enter Distributor Name.");
                var textbox = document.getElementById("txtDistributorName");
                textbox.focus();
                return (false);

            }
            else if (!form1.txtDate.value) {
                alert("Please Select Date.");
                var textbox = document.getElementById("txtDate");
                textbox.focus();
                return (false);
            }
            else if (!form1.txtCustomerName.value) {
                alert("Please Enter Customer Name.");
                var textbox = document.getElementById("txtCustomerName");
                textbox.focus();
                return (false);
            }
            else if (!form1.txtAddress.value) {
                alert("Please Enter Address.");
                var textbox = document.getElementById("txtAddress");
                textbox.focus();
                return (false);
            }
            else if (!form1.txtAddress2.value) {
                alert("Please Enter Address 2.");
                var textbox = document.getElementById("txtAddress2");
                textbox.focus();
                return (false);
            }
            else if (!form1.txtCity.value) {
                alert("Please Enter City.");
                var textbox = document.getElementById("txtCity");
                textbox.focus();
                return (false);
            }
            else if (!form1.txtPinCode.value) {
                alert("Please Enter Pin Code.");
                var textbox = document.getElementById("txtPinCode");
                textbox.focus();
                return (false);
            }
            else if (!pat1.test(pin_code.value)) {
                alert("Pin code should be 6 digits.");
                pin_code.focus();
                return false;
            }
            else if (!form1.txtMobile.value) {
                alert("Please Enter Mobile No.");
                var textbox = document.getElementById("txtMobile");
                textbox.focus();
                return (false);
            }
            else if (form1.txtMobile.value.length < 10 || form1.txtMobile.value.length > 10) {
                alert("Mobile No. is not valid, Please Enter 10 Digit Mobile No.");
                return false;
            }
            else if (!form1.txtEMail.value) {
                alert("Please Enter E-Mail ID.");
                var textbox = document.getElementById("txtEMail");
                textbox.focus();
                return (false);
            }
            else if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(form1.txtEMail.value)) {
                alert("You have entered an invalid email address.")
                return (false)
            }
            else if (!form1.txtMainContactPerson.value) {
                alert("Please Enter Main Contact Person Name.");
                var textbox = document.getElementById("txtMainContactPerson");
                textbox.focus();
                return (false);
            }
            else if (checkBoxes.length > 0) {
                var isAnyCheckBoxChecked = false;
                var checkBoxes = document.getElementById("CheckType").getElementsByTagName("input");
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox") {
                        if (checkBoxes[i].checked) {
                            isAnyCheckBoxChecked = true;
                            return true;
                        }
                    }
                }
                if (!isAnyCheckBoxChecked) {
                    alert("Please select atleast one Type either DYEING or PRINTING.");
                }
                return isAnyCheckBoxChecked;    
            }           
            else {
                return (true);
            }
        }
    </script>

    <script type="text/javascript">
        document.onkeypress = function noNumbers2(e) {
            e = e || window.event;
            var keynum = e.keyCode || e.which;
            if (keynum == 27) {
                AJS.AEV(document, "keypress", GB_hide);
            }
        }
    </script>

    <script type="text/javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <style type="text/css">
        .form {
            background: #f1f1f1;
            width: 600px;
            margin: 0 auto;
            padding-left: 120px;
            padding-top: 20px;
        }

            .form fieldset {
                border: 0px;
                padding: 0px;
                margin: 0px;
            }

            .form p.contact {
                font-size: 12px;
                margin: 0px 0px 10px 0;
                line-height: 14px;
                font-family: Arial, Helvetica;
            }

            .form input[type="text"] {
                width: 400px;
                text-transform: uppercase;
            }

            .form input[type="email"] {
                width: 400px;
            }

        .forminput[type="password"] {
            width: 400px;
        }

        .form input.birthday {
            width: 60px;
        }

        .form input.birthyear {
            width: 120px;
        }

        .form label {
            color: #000;
            font-weight: bold;
            font-size: 12px;
            font-family: Arial, Helvetica;
        }

            .form label.month {
                width: 135px;
            }

        .form input, textarea {
            background-color: rgba(255, 255, 255, 1);
            border: 1px solid #999;
            padding: 7px;
            font-family: Keffeesatz, Arial;
            color: #4b4b4b;
            font-size: 14px;
            -webkit-border-radius: 2px;
            margin-bottom: 15px;
            margin-top: -10px;
        }

            .form input:focus, textarea:focus {
                border: 1px solid green;
                background-color: rgba(255, 255, 255, 1);
            }

        .form .select-style {
            -webkit-appearance: button;
            -webkit-border-radius: 2px;
            -webkit-box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);
            -webkit-padding-end: 20px;
            -webkit-padding-start: 2px;
            -webkit-user-select: none;
            background-image: url(images/select-arrow.png), -webkit-linear-gradient(#FAFAFA, #F4F4F4 40%, #E5E5E5);
            background-position: center right;
            background-repeat: no-repeat;
            border: 0px solid #FFF;
            color: #555;
            font-size: inherit;
            margin: 0;
            overflow: hidden;
            padding-top: 5px;
            padding-bottom: 5px;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .form .gender {
            width: 410px;
        }

        .buttom {
            background: #4b8df9;
            display: inline-block;
            padding: 5px 10px 6px;
            color: #fbf7f7;
            text-decoration: none;
            font-weight: bold;
            line-height: 1;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            -moz-box-shadow: 0 1px 3px #999;
            -webkit-box-shadow: 0 1px 3px #999;
            box-shadow: 0 1px 3px #999;
            text-shadow: 0 -1px 1px #222;
            border: none;
            position: relative;
            cursor: pointer;
            font-size: 14px;
            font-family: Verdana, Geneva, sans-serif;
        }

            .buttom:hover {
                background-color: #2a78f6;
            }
    </style>

</head>
<body>
    <form runat="server" id="form1" method="get">
        <ajax:ToolkitScriptManager ID="toolkit1" runat="server"></ajax:ToolkitScriptManager>
        <main>
    <header>     
      <div class="wrap"><a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>
        <div class="inner_logo"><a>CUSTOMER REGISTRATION<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn" >
            <a href="Login.aspx" class="bt_Logout1" style="padding-top:25px;" onclick="return confirm('Are you sure to logout?');" ><i  class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>

<div id="middle">
    
	 <div class="wrap form" >
     <div class="cuslist">
		              <p class="contact" style="padding-top:10px; padding-bottom:30px; margin-right:85px; text-align:center; "><label for="name" style="font-size:20px;color:#8A0886;" >CUSTOMER REGISTRATION</label></p>
			                  
                      <p class="contact"><label for="name">DISTRIBUTOR NAME :</label></p>
                    <asp:TextBox id="txtDistributorName"  runat="server" placeholder="DISTRIBUTOR NAME" required="" tabindex="1"/>     
                     
                     <p class="contact"><label for="name">DATE :</label></p>
                    <asp:TextBox ID="txtDate"  placeholder="dd/MM/yyyy" runat="server" style="width:162px;" required="" tabindex="2" ></asp:TextBox>      
                      <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate" Format="dd/MM/yyyy" runat="server">
                      </ajax:CalendarExtender>
                     <p class="contact " style="padding-top:20px;"><label for="name" style="font-size:15px;" class="borderbottom">1. NAME AND ADDRESS OF THE CUSTOMER</label></p>

                      <p class="contact" style="padding-top:10px;"><label for="name">CUSTOMER NAME :</label></p>
                    <asp:TextBox id="txtCustomerName"  runat="server" placeholder="CUSTOMER NAME" required="" tabindex="3"/> 

                     <p class="contact" ><label for="name">ADDRESS :</label></p>
                    <asp:TextBox id="txtAddress"  runat="server" placeholder="Address" required="" tabindex="4"/> 

                      <p class="contact" ><label for="name">ADDRESS 2 :</label></p>
                    <asp:TextBox id="txtAddress2"  runat="server" placeholder="Address 2" required="" tabindex="5"/> 

                     <p class="contact" ><label for="name">CITY :</label></p>
                    <asp:TextBox id="txtCity"  runat="server" placeholder="City" required="" tabindex="6"/> 

                     <p class="contact" ><label for="name">PIN CODE :</label></p>
                    <asp:TextBox id="txtPinCode"  runat="server" placeholder="Pin Code (Exp. : 380060)" required="" tabindex="7"/>

                     <p class="contact" ><label for="name">TELEPHONE LANDLINE :</label></p>
                    <asp:TextBox id="txtTelephoneLandline"  runat="server" placeholder=" Telephone LandLine" required="" tabindex="8"/>

                    <p class="contact" ><label for="name">MOBILE NO :</label></p>
                    <asp:TextBox id="txtMobile"  runat="server" placeholder="Mobile NO (Exp. : 9865654585)" required="" tabindex="9"/>

                     <p class="contact" ><label for="name">FAX :</label></p>
                    <asp:TextBox id="txtFax"  runat="server" placeholder="FAX" required="" tabindex="10"/>

                     <p class="contact" ><label for="name">E-MAIL ID :</label></p>
                    <asp:TextBox id="txtEMail" runat="server" style="text-transform:lowercase;" placeholder="E-MAIL ID  (Exp. : Example@domain.com)" required="" tabindex="11"/>

                     <p class="contact" ><label for="name">ESTABLISHED IN :</label></p>
                    <asp:TextBox id="txtEstablishedIn" runat="server"  placeholder="Established In" required="" tabindex="12"/>

                     <p class="contact" ><label for="name">MAIN CONTACT PERSON :</label></p>
                    <asp:TextBox id="txtMainContactPerson" runat="server"  placeholder="Main Contact person" required="" tabindex="13"/>
                     		
                     <p class="contact" ><label for="name">SISTER CONCERN NAMES(IF ANY) :</label></p>
                    <asp:TextBox id="txtSisterConcern" runat="server"  placeholder="SISTER CONCERN NAMES(IF ANY) " required="" tabindex="14"/>

                     <p class="contact " style="padding-top:20px;"><label for="name" style="font-size:15px;" class="borderbottom">2. LINE OF BUSINESS</label></p>

                      <p class="contact" style="padding-top:10px;"><label for="name"> TYPE: DYEING / PRINTING :</label></p>
                     <asp:CheckBoxList id="CheckType" AutoPostBack="false" RepeatDirection="Horizontal" tabindex="15" runat="server">
                                        <asp:ListItem Value="1" >&nbsp;&nbsp;&nbsp;&nbsp;DYEING&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2" >&nbsp;&nbsp;&nbsp;&nbsp;PRINTING</asp:ListItem>                                        
                     </asp:CheckBoxList>
                    

                     <p class="contact" ><label for="name">PRODUCTION CAPACITY :</label></p>
                    <asp:TextBox id="txtProdCapacity" runat="server"  placeholder="Production Capacity" required="" tabindex="16"/>
                     	
                    <p class="contact" ><label for="name">BUSINEESS VOLUME: P/m :</label></p>
                    <asp:TextBox id="txtbusinessVolume" runat="server"  placeholder="Business Volume:P/m" required="" tabindex="17"/>
                     	
                     <p class="contact" ><label for="name">COMPETITION NAME & PRODUCT RANGE :</label></p>
                    <asp:TextBox id="txtCompetitionNameAndProductRange" runat="server"  placeholder="Competition Name & Product Range" required="" tabindex="18"/>
                     
                     <p class="contact" ><label for="name">EXPECTED BUSINESS VOLUME P/m :</label></p>
                    <asp:TextBox id="txtExpectedBusinessVolume" runat="server"  placeholder="Expected Business Volume P/M" required="" tabindex="19"/>
                     	
                      <p class="contact" ><label for="name">REFERENCE (IF AVAILABLE) :</label></p>
                    <asp:TextBox id="txtReference" runat="server"  placeholder="Reference (if available)" required="" tabindex="20"/>
                     	
                      <p class="contact" ><label for="name">INTERESTED IN JCIL PRODUCT :</label></p>
                                        <asp:CheckBoxList id="CheckInterestedJCILProduct" AutoPostBack="false" RepeatDirection="Horizontal"   runat="server">
                                        <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;&nbsp;REACTIVE DYES&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;&nbsp;DISPERSE&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value ="3">&nbsp;&nbsp;&nbsp;&nbsp;AUXILIARY&nbsp;&nbsp;&nbsp;</asp:ListItem>                                      
                                        </asp:CheckBoxList>                     

                                                       
                <div style="text-align:left; padding-top:20px; padding-bottom:20px;"  >
                <asp:LinkButton ID="Btn_submit" CssClass="buttom"  OnClientClick="javascript: return validateCustRefNo();" OnClick="Btn_submit_Click" Text="Submit" runat="server" />                											
</div></div>
</div>
</div>

</main>
    </form>
</body>
</html>
