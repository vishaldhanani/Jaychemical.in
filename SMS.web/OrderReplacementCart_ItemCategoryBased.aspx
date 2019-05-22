<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderReplacementCart_ItemCategoryBased.aspx.cs" Inherits="OrderReplacementCart_ItemCategoryBased" %>

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
		<div class="inner_logo"><a>Order Placement<br /> <asp:Label runat="server"  CssClass="lblUser" style="margin-top:10px; vertical-align:top; font-size:10px;" ID="lblUserName"></asp:Label></a> </div>
		<div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
	  </div>
	</header>
	<div id="middle">
	  <div class="wrap">
		<asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
		<%--<div class="odr_place"><input name="input" type="text" class="u_fill" readonly="true" id="tb_CustName" runat="server" visible="false" value=""/></div>--%>
		<div class="cuslist tz-cuslists">
		  <div class="block_1">
			<div class="cuslist_listing">
			  <asp:Repeater runat="server" ID="rpt_Cart" OnItemDataBound="rpt_Cart_ItemDataBound" >
				<HeaderTemplate>
				  <ul>
				</HeaderTemplate>
				<ItemTemplate>
				  <li>
					<%--<div class="odr_no"><%# Container.ItemIndex + 1 %></div>--%>
					<div class="idbox">
					  <div><span>
						<asp:TextBox runat="server" CssClass="page-heading" readonly="true" ID="tb_ProductName"  Text='<%# Eval("ProductName") %>'></asp:TextBox>
						</span></div>
					</div>
					<div>
					  <table width="100%" border="0">
						<tr>
						  <!--<td width="18%" align="left" valign="top" ><div class="blk_one"> <span id="Span4">Packs</span> </div></td>
						  <td width="2%" align="center" valign="top"  ></td>-->
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span5">
                          	<asp:DropDownList runat="server" ID="dd_Variant" TabIndex="1" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="u_oder_fill_white_label order_pack" > </asp:DropDownList>
							</span> </div>
							<asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ProductCode") %>' />
							<asp:HiddenField runat="server" ID="hf_UOM"  Value='<%# Convert.ToString(Eval("UOM")).Equals("")?"":Eval("UOM") %>' />
							<%--<asp:HiddenField runat="server" ID="hf_Price" Value='<%# Convert.ToDecimal(Eval("Rate")) %>' />--%>
                            <asp:HiddenField runat="server" ID="tb_Price" Value='<%# Convert.ToDecimal(Eval("Rate")) %>' />
                            <asp:HiddenField runat="server" ID="txt_AgentPrice" Value='<%# Convert.ToString(Eval("CSellingPrice")).Equals("0")?"":Convert.ToString(((Convert.ToDecimal(Eval("CSellingPrice"))))) %>' />

						  </td>
						</tr>
						<tr >
						  <!--<td width="18%" align="left" valign="top"><div class="blk_one" > <span id="Span6">Qty (Kgs)</span> </div></td>
						  <td width="2%" align="center" valign="top" ></td>-->
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span12">
                          	<label class="t-lable">Qty (Kgs)</label>
							<asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="tb_Qty_TextChanged" Style="text-align:right;font-size:small;" MaxLength="16" ID="tb_Qty" BackColor="White" ToolTip="Qty" onkeyup="checkDec(this);" TabIndex="2" Placeholder="Qty" CssClass="u_oder_fill_white_label"  Text='<%# Convert.ToString(Eval("Quantity")).Equals("1")?"":Eval("Quantity") %>'> </asp:TextBox>
							</span> </div></td>
						</tr>
						<%--<tr>						  
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span13">
                          <label class="t-lable">Cons. Price Per Kg</label> <b>
						  <asp:TextBox runat="server" ID="tb_Price" BackColor="#efefef" Style="text-align:right;font-size:small;"  MaxLength="16" Placeholder="Price"  ReadOnly="true" CssClass="u_oder_fill_white_label" Text='<%# (Convert.ToDecimal(Eval("Rate"))) %>'></asp:TextBox>
						  </b> </span> </div></td>
						</tr>--%>
						<%--<tr>						  
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span15">
                          	<label class="t-lable">Agent Price</label>
							<asp:TextBox runat="server" ID="txt_AgentPrice" MaxLength="16" BackColor="#efefef" ReadOnly="true" Style="text-align:right;" onkeyup="checkDec(this);"   Placeholder="Cust. Price" CssClass="u_oder_fill_white_label" Text='<%# Convert.ToString(Eval("CSellingPrice")).Equals("0")?"":Convert.ToString(((Convert.ToDecimal(Eval("CSellingPrice"))))) %>'></asp:TextBox>
							</span>							  
							</div></td>
						</tr>--%>
						<tr>						
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span17">
                          <label class="t-lable">Price</label> <b>
						  <asp:TextBox runat="server" CausesValidation="true" ClientIDMode="Static" ID="txtPrice"  Placeholder="Price" ReadOnly="true"  AutoPostBack="true"  Text='<%# (Convert.ToDecimal(Eval("Rate"))).ToString("0.00") %>'    onkeyup="checkDec(this); "     BackColor="#efefef" Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>

                          <tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span1">
                          <label class="t-lable">Bill Price</label> <b>
						  <asp:TextBox runat="server" CausesValidation="true"  ID="txtSellingPrice"  ClientIDMode="Static" Placeholder="Bill Price"    AutoPostBack="true" OnTextChanged="txtSellingPrice_TextChanged" Text='<%# (Convert.ToDecimal(Eval("CSellingPrice"))).ToString("0.00") %>'   onkeyup="checkDec(this); "   TabIndex="3"   Style="text-align:right;font-size:small;" MaxLength="16" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>

						<%--<tr>						
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span2">
                          <label class="t-lable">Discount(%)</label> <b>
						  <asp:TextBox class="percent" ClientIDMode="Static" runat="server" BackColor="#efefef" CausesValidation="true" AutoPostBack="true"  ReadOnly="true"  ID="txt_discount_per" onkeyup="return checkDec(this);"   Text='<%# (Convert.ToDecimal(Eval("DiscPercentage"))).ToString("0.00") %>' TabIndex="4"  Style="text-align:right;font-size:small;" MaxLength="16" OnTextChanged="txt_discount_per_TextChanged" CssClass="u_oder_fill_white_label" ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>--%>
						<%--<tr>						  
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span8">
                          <label class="t-lable">Discount (Rs.)</label> <b>
						  <asp:TextBox runat="server" AutoPostBack="true" onfocus="this.select()" ID="txt_disc_amt" Text='<%# (Convert.ToDecimal(Eval("DyesDiscPrice"))).ToString("0.00") %>'  TabIndex="5" BackColor="#efefef" ReadOnly="true" Style="text-align:right;font-size:small;" MaxLength="16" Placeholder="Price"  CssClass="u_oder_fill_white_label" onkeyup="checkDec(this);" OnTextChanged="txt_disc_amt_TextChanged"  ></asp:TextBox>
						  </b> </span> </div></td>
						</tr>--%>
						<tr>						  
						  <td align="left" valign="top"><div class="blk_1"> <span id="Span19">
                          <label class="t-lable">VALUE (Excl.Tax)</label> <b>
						  <asp:TextBox runat="server" Style="text-align:right;"  ID="txt_netPrice" BackColor="#efefef"  MaxLength="16" Placeholder="VALUE (Excl.Tax)" CssClass="u_oder_fill_white_label" ReadOnly="true" Text='<%# (Convert.ToDecimal(Eval("ExtField_SellingPrice"))).ToString("0.00") %>'></asp:TextBox>
						  </b> </span> </div></td>
						</tr>

						<%--<tr runat="server" id="tr_condition" >
						  <td id="Td1" width="18%"  align="left" runat="server" valign="top"><div id="Div1" runat="server" class="blk_one">
							  <asp:Label id="lbl_discount"  runat="server" Text="Discount(%)"></asp:Label>
							</div></td>
						  <td id="Td2" width="2%" runat="server" align="center" valign="top" ></td>
						  <td id="Td3" runat="server" align="left" valign="top"><div id="Div2" class="blk_1" runat="server"> <span id="Span14" runat="server">
							<asp:TextBox runat="server" ID="tb_Discount" Style="text-align:right;" MaxLength="16" Text='<%# System.Math.Round(Convert.ToDecimal(Eval("DiscPercentage"))) %>' placeholder="Discount" ReadOnly="true" CssClass="u_oder_fill_white_label"></asp:TextBox>
							</span> </div></td>
						</tr>--%>

						<tr>						 
						  <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span16">
							<asp:TextBox runat="server" BackColor="White"  onkeypress="return this.value.length<=200" TabIndex="4"  Placeholder="Remark" ID="tb_Remark" CssClass="u_oder_fill_white_label" Text='<%# Eval("Remark") %>' ></asp:TextBox>
							</span> </div></td>
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
	  <div class="wrap">
	  <div class="links">
		<asp:LinkButton   runat="server" ID="LinkButton1" CssClass="u_f_btn12"    OnClick="lb_AddProduct_Click"  Text="Add Product"    TabIndex="8" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>
		<asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"   OnClick="lb_Finish_Click" Text="Finish Order" TabIndex="7" OnClientClick="javascript: return OrederReplacementCartForDyes();"></asp:LinkButton>
	  </div></div>
	</footer>
  </main>
</form>
</body>
</html>
