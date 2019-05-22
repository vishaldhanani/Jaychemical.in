<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankingInfoCustomer.aspx.cs" Inherits="BankingInfoCustomer" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="True">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1">
<title><%= Constant.Title %></title>
<link href="css/fonts.css" rel="stylesheet" type="text/css" />
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/Custom.css" rel="stylesheet" />
<script src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/html5.js"></script>
<link href="css/media.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/Common.js"></script>
<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
<link href="css/font-awesome.min.css" rel="stylesheet" />
<link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />

<script type="text/javascript">
    function checkDec(el) {
        var ex = /^[0-9]+\.?[0-9]*$/;
        if (ex.test(el.value) == false) {
            el.value = el.value.substring(0, el.value.length - 1);
        }
    }

</script>
<%--<script>

    $('#txt_discount_per').change(function () {
        var a = $('#txt_billPrice').val();
        var b = $(this).val();
        $("#txt_netPrice").text((parseInt(a) * parseInt(b)) / 100 + parseInt(a));
    });
</script>--%>
<script type="text/javascript">

    (function ($, global) {

        var _hash = "!",
        noBackPlease = function () {
            global.location.href += "#";

            setTimeout(function () {
                global.location.href += "!";
            }, 50);
        };

        global.setInterval(function () {
            if (global.location.hash != _hash) {
                global.location.hash = _hash;
            }
        }, 100);

        global.onload = function () {
            noBackPlease();

            // disables backspace on page except on input fields and textarea..
            $(document.body).keydown(function (e) {
                var elm = e.target.nodeName.toLowerCase();
                if (e.which == 8 && elm !== 'input' && elm !== 'textarea') {
                    e.preventDefault();
                }
                // stopping event bubbling up the DOM tree..
                e.stopPropagation();
            });
        }

    })(jQuery, window);

</script>

<script type="text/javascript">
    function onlyNos(e, t) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        catch (err) {
            alert(err.Description);
        }
    }
</script>

<script>
    function OrederReplacementCartForDyes() {
        try {
            
            var IsInvalidVCode = false;
            $('select[id*=dd_Variant]').each(function () {
                if ($(this).val() == '0') {
                    IsInvalidVCode = true;
                    $(this).focus();
                }
            });
            if (IsInvalidVCode == true) {
                alert("Please select product variant.");
                return false;
            }

            var isZeroQty = false;
            $('input[id*=txt_PaymentAmount]').each(function () {
                if ($(this).val() <= 0) {
                    isZeroQty = true;
                    $(this).focus();
                }
                if ($(this).val() == '') {
                    isZeroQty = true;
                    $(this).focus();
                }
            });
            if (isZeroQty == true) {
                alert('Please enter Payment Amount');
                return false;
            }

            var IsPriceNull = false;
            $('input[id*=txt_billPrice]').each(function () {
                if ($(this).val() == '') {
                    IsPriceNull = true;
                    $(this).focus();
                }
                if ($(this).val() <= 0) {
                    IsPriceNull = true;
                    $(this).focus();

                }
            });
            if (IsPriceNull == true) {
                alert('Please enter Bill Price.');
                return false;
            }
            var IsBankeName = false;
            $('input[id*=tb_BankName]').each(function () {
                if ($(this).val() == '') {
                    IsBankeName = true;
                    $(this).focus();
                }
                if ($(this).val() <= 0) {
                    IsBankeName = true;
                    $(this).focus();

                }
            });
            if (IsBankeName == true) {
                alert('Please enter Bank Name');
                return false;
            }
                                  
            var IsInvalidUOM = false;
            $('select[id*=dd_UOM]').each(function () {
                if ($(this).val() == '0') {
                    IsInvalidUOM = true;
                    $(this).focus();
                }
            });
            if (IsInvalidUOM == true) {
                alert("Please select UOM Code");
                return false;
            }
            

        } catch (e) {
            alert(e);
        }
    }
</script>
</head>

<body>
<form id="frm_ViewCart" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
  <main>
	<header>
	  <div class="wrap">
		<a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>
		<div class="inner_logo"><a>Payment Process<br /> <asp:Label runat="server"  CssClass="lblUser" style="margin-top:10px; vertical-align:top; font-size:10px;" ID="lblUserName"></asp:Label></a> </div>
		<div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
	  </div>
	</header>
	<div id="middle">
	  <div class="wrap">
		<asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
		
		<div class="cuslist tz-cuslists">
		  <div class="block_1">
			<div class="cuslist_listing">
                <div style="text-align:center;"><b> RTGS / NEFT - Payment Summary</b></div>
			  <table width="100%" border="0">
                  
						<tr>
					
						  <td >
                            <label class="t-lable">Payment Amount</label>  
                              <asp:TextBox runat="server"  Style="text-align:right;font-size:small;" MaxLength="16" ID="txt_PaymentAmount" BackColor="White" ToolTip="Payment Amount" onkeyup="checkDec(this);" TabIndex="1" Placeholder="Payment Amount" CssClass="u_oder_fill_white_label"  > 

                              </asp:TextBox>

						  </td>
						</tr>
						<tr >
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span12">
                          	<label class="t-lable">Bank Name</label>
							<asp:TextBox runat="server"  Style="text-align:right;font-size:small;" MaxLength="50" ID="tb_BankName" BackColor="White" ToolTip="Qty"  TabIndex="2" Placeholder="Bank Name" CssClass="u_oder_fill_white_label"  > </asp:TextBox>
							</span> </div></td>
						</tr>
                        <tr >
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span2">
                          	<label class="t-lable">Bank Branch</label>
							<asp:TextBox runat="server"  Style="text-align:right;font-size:small;" MaxLength="50" ID="txt_BankBranch" BackColor="White" ToolTip="Branch"  TabIndex="2" Placeholder="Bank Branch" CssClass="u_oder_fill_white_label"  > </asp:TextBox>
							</span> </div></td>
						</tr>
					
						<tr>						
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span17">
                          <label class="t-lable">Bank A/C No. </label> <b>
						  <asp:TextBox runat="server" CausesValidation="true" ClientIDMode="Static" ID="txtAccountNo" TabIndex="3"  Placeholder="A/C No."        Style="text-align:right;font-size:small;" MaxLength="20" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>
                        

                          <tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span1">
                           <b>
						  <asp:TextBox runat="server" CausesValidation="true"  ID="txtDate"  ClientIDMode="Static" Placeholder="Date"       TabIndex="4"   Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
                              <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate" Format="dd/MM/yyyy" runat="server" />
						  </b> </span> </div></td>
						</tr>

					
						<tr>						  
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span19">
                          
						  <asp:DropDownList ID="drpModeofPayment" runat="server" CssClass="u_oder_fill_white_label order_pack" TabIndex="5">
                              <asp:ListItem Value="1" Text="RTGS" Selected="True"></asp:ListItem>
                              <asp:ListItem Value="2" Text="NEFT"></asp:ListItem>
                              <asp:ListItem Value="3" Text="Cheque"></asp:ListItem>
						  </asp:DropDownList>
						  </b> </span> </div></td>
						</tr>

					

						
						  
						
					  </table>
			</div>
		  </div>
		</div>
	  </div>
	</div>
	<footer>
	  <div class="wrap">
	  <div class="links">
		<%--<asp:LinkButton   runat="server" ID="LinkButton1" CssClass="u_f_btn12"    OnClick="lb_AddProduct_Click"  Text="Add Product"    TabIndex="8" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>--%>
		<asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"   OnClick="lb_Finish_Click" Text="Next" TabIndex="6" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>
	  </div></div>
	</footer>
  </main>
</form>
</body>
</html>
