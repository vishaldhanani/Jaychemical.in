<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankingInfoAccountwise.aspx.cs" Culture="en-GB" Inherits="BankingInfoAccountwise" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.fancybox.css" media="screen" />
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script src="js/Common.js"></script>
    <script type="text/javascript" src="js/FancyBoxStyle.js"></script>
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.gif" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
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

                var IsBankeBranch = false;
                $('input[id*=txt_BankBranch]').each(function () {
                    if ($(this).val() == '') {
                        IsBankeBranch = true;
                        $(this).focus();
                    }
                    if ($(this).val() <= 0) {
                        IsBankeBranch = true;
                        $(this).focus();

                    }
                });
                if (IsBankeBranch == true) {
                    alert('Please enter Bank Branch');
                    return false;
                }
                var IsAccountNo = false;
                $('input[id*=txtAccountNo]').each(function () {
                    if ($(this).val() == '') {
                        IsAccountNo = true;
                        $(this).focus();
                    }
                    if ($(this).val() <= 0) {
                        IsAccountNo = true;
                        $(this).focus();

                    }
                });
                if (IsAccountNo == true) {
                    alert('Please enter Account Number');
                    return false;
                }

                var IsDate = false;
                $('input[id*=txtDate]').each(function () {
                    if ($(this).val() == '') {
                        IsDate = true;
                        $(this).focus();
                    }
                    if ($(this).val() <= 0) {
                        IsDate = true;
                        $(this).focus();

                    }
                });
                if (IsDate == true) {
                    alert('Please enter Date');
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
                var Ok = confirm('Are you sure you want to process for payment?');
                if (Ok)
                    return true;
                else
                    return false;
                



            } catch (e) {
                alert(e);
            }
        }
    </script>
 <script type="text/javascript">

     
  

        function copyvalue() {
           

           

            //var txtMsgVal = $(this).parent().parent().find("#rpt_Account_Stmt_tdAmt_1").text();
            //alert(txtMsgVal);

        }
    </script>
  <style type="text/css">
      .form{
	background:#f1f1f1; width:470px; margin:0 auto; padding-left:50px; padding-top:20px;
}
.form fieldset{border:0px; padding:0px; margin:0px;}
.form p.contact { font-size: 12px; margin:0px 0px 10px 0;line-height: 14px; font-family:Arial, Helvetica;}

.form input[type="text"] { width: 400px; }
.form input[type="email"] { width: 400px; }
.forminput[type="password"] { width: 400px; }
.form input.birthday{width:60px;}
.form input.birthyear{width:120px;}
.form label { color: #000; font-weight:bold;font-size: 12px;font-family:Arial, Helvetica; }
.form label.month {width: 135px;}
.form input, textarea { background-color: rgba(255, 255, 255, 0.4); border: 1px solid rgba(122, 192, 0, 0.15); padding: 7px; font-family: Keffeesatz, Arial; color: #4b4b4b; font-size: 14px; -webkit-border-radius: 5px; margin-bottom: 15px; margin-top: -10px; }
.form input:focus, textarea:focus { border: 1px solid #ff5400; background-color: rgba(255, 255, 255, 1); }
.form .select-style {
  -webkit-appearance: button;
  -webkit-border-radius: 2px;
  -webkit-box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);
  -webkit-padding-end: 20px;
  -webkit-padding-start: 2px;
  -webkit-user-select: none;
  background-image: url(images/select-arrow.png), 
    -webkit-linear-gradient(#FAFAFA, #F4F4F4 40%, #E5E5E5);
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
  white-space: nowrap;}
.form .gender {
  width:410px;
  }
.form input.buttom{ background: #4b8df9; display: inline-block; padding: 5px 10px 6px; color: #fbf7f7; text-decoration: none; font-weight: bold; line-height: 1; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px; -moz-box-shadow: 0 1px 3px #999; -webkit-box-shadow: 0 1px 3px #999; box-shadow: 0 1px 3px #999; text-shadow: 0 -1px 1px #222; border: none; position: relative; cursor: pointer; font-size: 14px; font-family:Verdana, Geneva, sans-serif;}
.form input.buttom:hover	{ background-color: #2a78f6; }

  </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <main>           
    <header>
        <div class="wrap">
        <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
            <div class="inner_logo" >
            	 <a >Payment Process <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>
            </div>
            <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
          
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>                      
        	
        		<div style="margin-top:-1%">                                    
                <table width="100%" border="0">
                    <tr style="text-align:center; font-size:14px; font-weight:500; text-decoration:none; line-height:17px; ">
                    <td>
                       <h3> <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h3>
                    </td>
                  </tr>                 
                </table>        
            </div>
                <div class="block_1">
			<div class="cuslist_listing">
                <div style="text-align:center;"><b> RTGS / NEFT - Payment Summary</b></div>
			  <table width="100%" border="0">
                  
						<tr >
					
						

                               <td align="left" valign="top"><div class="blk_1"> <span id="Span19">
                          
						  <asp:DropDownList ID="drpModeofPayment" runat="server" CssClass="u_oder_fill_white_label order_pack" TabIndex="5">
                              <asp:ListItem Value="1" Text="RTGS" Selected="True"></asp:ListItem>
                              <asp:ListItem Value="2" Text="NEFT"></asp:ListItem>
                              <asp:ListItem Value="3" Text="Cheque"></asp:ListItem>
						  </asp:DropDownList>
						  </b> </span> </div></td>
                            <td>

                            </td>
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
                             <td>

                            </td>
                             <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span17">
                          <label class="t-lable">Bank A/C No. </label> <b>
						  <asp:TextBox runat="server" CausesValidation="true" ClientIDMode="Static" ID="txtAccountNo" TabIndex="3"  Placeholder="A/C No."        Style="text-align:right;font-size:small;" MaxLength="20" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>
					
					

                          <tr>						 
						        <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span1">
                              <label class="t-lable">Date </label>
                           <b>
						  <asp:TextBox runat="server" CausesValidation="true"  ID="txtDate"  ClientIDMode="Static" Placeholder="Date"       TabIndex="4"   Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
                              <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate" Format="dd/MM/yyyy" runat="server" />
						  </b> </span> </div></td>
                                <td>

                            </td>
                               <td >
                            <label class="t-lable">Payment Amount</label>  
                              <asp:TextBox runat="server"  Style="text-align:right;font-size:small;font:bold;" ForeColor="Green" Enabled="false" MaxLength="16" ID="txt_PaymentAmount" BackColor="White" ToolTip="Payment Amount" onkeyup="checkDec(this);" TabIndex="1" Placeholder="Payment Amount" CssClass="u_oder_fill_white_label"  > 

                              </asp:TextBox>

						  </td>
						</tr>

					

					

						
						  
						
					  </table>
			</div>
                    <div style="text-align:left;"><center> 
                        <b>
                       Apply Against Invoice</b></center>
                    </div>
		  </div>
            <div class="tz-ship-list">
                <asp:Repeater runat="server" ID="rpt_Account_Stmt" OnItemDataBound="rpt_TotalOutStandingSumm_ItemDataBound" >
                       <HeaderTemplate>
                        <table id="tbdata" width="100%" border="1" class="odr_tbl_list table-striped">
                            <tr>                         
                                           <th width="10%" align="center" valign="middle"><div align="left"></div></th>
                                <th width="30%" align="center" valign="middle"><div align="left">Invoice No.<br /> Posting Date</div></th>             
                                <th width="30%" align="center" valign="middle"><div align="right">O/S. Amount<br /> Due Date </div></th>   
                                <th width="30%" align="center" valign="middle"><div align="right">Net Payment<br /> </div></th>                                                                                                                                                 
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                              
                            <td align="left" valign="middle"  width="10%" style="border-right-style:hidden;" > 
                                <asp:CheckBox runat="server" ID="chk_Select" onclick="copyvalue(this.value)" AutoPostBack="true" OnCheckedChanged="chk_Select_CheckedChanged"   /> </td>
                            <td align="left" valign="middle"  width="30%" style="border-right-style:hidden;"> <div  style="font-size:13px; width:160%; font-weight:bold; "> <%# Eval("InvoiceNo") %> </div> <a style="font-size:12px; color:#000; text-decoration:none;"> <%# Convert.ToDateTime(Eval("PostingDate")).ToString("dd/MM/yy") %> </a></td>                                                        
                            <td align="right" valign="top"  width="30%" style="font-size:13px; font-weight:600;" > 
                                <asp:Label style="font-size:12px; font-weight:bold;" Text='<%# Eval("Amount", "{0:0,00}") %>' id="tdAmt" runat="server">   </asp:Label><br /> 
                                <asp:Label Text='<%# Convert.ToDateTime(Eval("DueDate")).ToString("dd/MM/yy") %>' style="font-size:12px; font-weight:normal;" id="tdduedate" runat="server"> </asp:Label>

                            </td>                                      
                            <td align="right" valign="top"  width="30%" style="font-size:13px; font-weight:600;">
                                <asp:TextBox style="text-align:right;font-size:small;" CssClass="u_oder_fill_white_label" AutoPostBack="true" OnTextChanged="txtAmounttoVerify_TextChanged"  id="txtAmounttoVerify" runat="server">   </asp:TextBox><br /> 

                            </td>                                                                   
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                           
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
            
      </div>
    </div></div>
    <footer>
        <div class="fl_icon"><a href="ActCustomerTotalOutStandingPaymentProcessing.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	<div class="wrap">
        		  <div class="links">
		
		<asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"    Text="Generate Form" TabIndex="6" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>
	  </div>
        </div>       
    </footer>
</main>
    </form>
</body>
</html>
