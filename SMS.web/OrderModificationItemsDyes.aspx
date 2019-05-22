<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderModificationItemsDyes.aspx.cs" Inherits="OrderModificationItemsDyes" %>

<!DOCTYPE html>

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
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
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
                debugger;
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
                $('input[id*=tb_Qty]').each(function () {
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
                    alert('Please enter Qty.');
                    return false;
                }

                var IsPriceNull = false;
                $('input[id*=txtSellingPrice]').each(function () {
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
		<div class="inner_logo"><a>ORDER MODIFICATION<br /> <asp:Label runat="server"  CssClass="lblUser" style="margin-top:10px; vertical-align:top; font-size:10px;" ID="lblUserName"></asp:Label></a> </div>
		<div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
	  </div>
	</header>
	<div id="middle">
	  <div class="wrap">
		<asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>	
		<div class="cuslist tz-cuslists">
		  <div class="block_1">
			<div class="cuslist_listing">
			  <asp:Repeater runat="server" ID="rpt_Cart" OnItemDataBound="rpt_Cart_ItemDataBound" >
				<HeaderTemplate>
				  <ul>
				</HeaderTemplate>
				<ItemTemplate>
				  <li>					
					<div class="idbox">
					  <div><span>
						<asp:TextBox runat="server" CssClass="page-heading" readonly="true" ID="tb_ProductName"  Text='<%# Eval("Description") %>'></asp:TextBox>
						</span></div>
					</div>
					<div>
					  <table width="100%" border="0">
						<tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span5">
                          	<asp:DropDownList runat="server" ID="dd_Variant" TabIndex="1" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="u_oder_fill_white_label order_pack" > </asp:DropDownList>
							</span> </div>
							<asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ItemNo") %>' />
							<asp:HiddenField runat="server" ID="hf_UOM"  Value='<%# Convert.ToString(Eval("UOM")).Equals("")?"":Eval("UOM") %>' />
							<asp:HiddenField runat="server" ID="hf_QuantityShipped" Value='<%# Convert.ToDecimal(Eval("QuantityShipped")) %>' />
                            <asp:HiddenField runat="server" ID="hf_OutStandingQty" Value='<%# Convert.ToDecimal(Eval("OutStandingQty")) %>' /> 
                            <asp:HiddenField runat="server" ID="tb_Price" Value='<%# Convert.ToDecimal(Eval("UnitPrice")) %>' />
                            <asp:HiddenField runat="server" ID="hf_SelltoCustomerNo" Value='<%# Eval("SelltoCustomerNo") %>' />
                            <asp:HiddenField runat="server" ID="hf_ShiptoCode" Value='<%# Eval("ShiptoCode") %>' />
                            <asp:HiddenField runat="server" ID="hf_CustomerPriceGroup" Value='<%# Eval("CustomerPriceGroup") %>' />                            

						  </td>
						</tr>
						<tr >						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span12">
                          	<label class="t-lable">Qty (Kgs)</label>
							<asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="tb_Qty_TextChanged" Style="text-align:right;font-size:small;" MaxLength="16" ID="tb_Qty" BackColor="White" ToolTip="Qty" onkeyup="checkDec(this);" TabIndex="2" Placeholder="Qty" CssClass="u_oder_fill_white_label"  Text='<%# Convert.ToString(Eval("Quantity")).Equals("1")?"":Eval("Quantity") %>'> </asp:TextBox>
							</span> </div></td>
						</tr>
					
						<tr>						
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span17">
                          <label class="t-lable">Price</label> <b>
						  <asp:TextBox runat="server" CausesValidation="true" ClientIDMode="Static" ID="txtPrice"  Placeholder="Price" ReadOnly="true"  AutoPostBack="true"  Text='<%# (Convert.ToDecimal(Eval("UnitPrice"))).ToString("0.00") %>'    onkeyup="checkDec(this); "     BackColor="#efefef" Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>

                              <tr runat="server" id="tr_condition" >                                                                                    
                                        <td width="100%" align="left" valign="top">
                                        <div class="blk_1">                               
                                        <span id="Span14">
                                        	<label class="t-lable">Discount (%)</label>
                                            <asp:TextBox runat="server" ID="tb_Discount"  Style="text-align:right;"  placeholder="Discount" Text='<%# System.Math.Round(Convert.ToDecimal(Eval("Discount"))).ToString("0") %>'  ReadOnly="true" CssClass="u_oder_fill_white_label" BackColor="#efefef"></asp:TextBox>                                        
                                        </span>
                                        </div>
                                        </td>
                                   </tr>


                          <tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span1">
                          <label class="t-lable">Bill Price</label> <b>
						  <asp:TextBox runat="server" CausesValidation="true"  ID="txtSellingPrice"  ClientIDMode="Static" Placeholder="Bill Price"    AutoPostBack="true" OnTextChanged="txtSellingPrice_TextChanged" Text='<%# (Convert.ToDecimal(Eval("UnitPrice"))).ToString("0.00") %>'   onkeyup="checkDec(this); "   TabIndex="3"   Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>						                        					
						<tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span16">
							<asp:TextBox runat="server" BackColor="White"  onkeypress="return this.value.length<=200" TabIndex="4"  Placeholder="Remark" ID="tb_Remark" CssClass="u_oder_fill_white_label" Text='<%# Eval("Remark") %>' ></asp:TextBox>
							</span> </div></td>
						</tr>
                          <tr>                                       
                           <td width="100%" align="left" valign="top">
                              <div class="blk_1">                               
                               <span id="Span2">
                               <label class="t-lable">Total</label>
                               <asp:TextBox runat="server" ID="tb_SellPrice" ReadOnly="true"  Text='<%# System.Math.Round((Convert.ToDecimal(Eval("Amount")))) %>'  CssClass="u_oder_fill_white_label u_oder_fill" Style="text-align:right;" ></asp:TextBox>                                        
                               </span>
                               </div>
                           </td>
                          </tr>  
						  </tr>						
					  </table>
					</div>
				  </li>
				</ItemTemplate>
				<FooterTemplate>
				  </ul>
				</FooterTemplate>
			  </asp:Repeater>
			</div>
		  </div>
		</div>
	  </div>
	</div>
	<footer>
	 <div class="fl_icon"><a href="OrderModificationView.aspx?OrderNo=<%=Request.QueryString["OrderNo"]%> &BlanketOrderNo= <%= Convert.ToString(Request.QueryString["BlanketOrderNo"])%> &PostingDate=<%= Convert.ToString(Request.QueryString["PostingDate"]) %>"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>                 
    	<div class="wrap">                
        <div class="links">           
            <asp:LinkButton runat="server" ID="lb_Modify" CssClass="u_f_btn" OnClick="lb_Modify_Click" Text="Modify" TabIndex="5" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>                                         
        	</div></div>
	</footer>
  </main>
    </form>
</body>
</html>
