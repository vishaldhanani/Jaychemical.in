<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderReplacementCartNew.aspx.cs" Inherits="OrderReplacementCartNew" %>

<!DOCTYPE html>

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
    <script type="text/javascript">

        function fnShowMessage() {
            alert("Customer Price exceeds maximum Margin Limit(15%).");
        }

    </script>
    <script>       

    </script>
    <script type="text/javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

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
        $(function () {
            $('#tb_Qty').focus(function () {
                $(this).attr("placeholder", $(this).attr('data-placeholder'));
            });
        });
    </script>
</head>
<body>
    <form id="frm_ViewCart" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
        <main>
	<header>
	  <div class="wrap">
		<a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>
		<div  class="inner_logo"><a>Order Placement<br /> <asp:Label runat="server" style="margin-top:10px; vertical-align:top; font-size:10px;" CssClass="lblUser" ID="lblUserName"></asp:Label></a> </div>
		<div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
	  </div>
	</header>
	<div id="middle">
	  <div class="wrap">
		<asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>		
		<div class="cuslist">
		  <div class="block_1">
			<div class="cuslist_listing">
			  <asp:Repeater runat="server" ID="rpt_Cart" OnItemDataBound="rpt_Cart_ItemDataBound" >
				<HeaderTemplate>
				</HeaderTemplate>
				<ItemTemplate>
				  <div class="idbox">
				    <div><span>
					  <asp:TextBox runat="server" CssClass="page-heading" readonly="true" ID="tb_ProductName"  Text='<%# Eval("ProductName") %>'></asp:TextBox>
					  </span></div>
				  </div>
				  <div>
				    <table width="100%" border="0" >
					
					  <tr>
					    <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span5">
						    <asp:DropDownList runat="server" ID="dd_Variant" TabIndex="1" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="u_oder_fill_white_label select" > </asp:DropDownList>
						    </span></div>
					      <asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ProductCode") %>' />
					      <asp:HiddenField runat="server" ID="hf_UOM"  Value='<%# Convert.ToString(Eval("UOM")).Equals("")?"":Eval("UOM") %>' />
					      <asp:HiddenField runat="server" ID="hf_Price" Value='<%# Convert.ToDecimal(Eval("Rate")) %>' /></td>
				      </tr>
					  <tr style="height:10px">
					    <td></td>
				      </tr>
					  <tr>
					    <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span12">
                        <label class="t-lable">No. of Packs</label>
						    <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="tb_Qty_TextChanged" Style="text-align:right;font-size:small;" MaxLength="16" ID="tb_Qty" BackColor="White" ToolTip="Qty" onkeyup="checkDec(this);" TabIndex="2" Placeholder="Qty" CssClass="u_oder_fill_white_label"  Text='<%# Convert.ToString(Eval("Quantity")).Equals("1")?"":Eval("Quantity") %>'> </asp:TextBox>
					      </span></div></td>
				      </tr>
					  <tr style="height:10px">
					    <td></td>
				      </tr>
					  <tr style="height:0px">
					    <td align="left" valign="top"><div class="blk_1"> <span id="Span13">
                        <label class="t-lable">Price Per Pack</label>
					      <asp:TextBox runat="server"  Style="text-align:right;" BackColor="#efefef" ID="tb_Price" MaxLength="16" Placeholder="Price" CssClass="u_oder_fill_white_label" ReadOnly="true" Text='<%# (Convert.ToDecimal(Eval("Rate"))) %>'></asp:TextBox>
				        </span></div></td>
				      </tr>
					  <tr style="height:0px">
					    <td></td>
				      </tr>
					  <tr runat="server" id="tr_condition" visible="false" >
					    <td id="Td1"  align="left" runat="server" valign="top"><div id="Div1" runat="server" class="blk_one">
						    <asp:Label id="lbl_discount"  runat="server" Text="Discount(%)"></asp:Label>
						    
						  </div></td>
				      </tr>
					  <tr style="height:10px">
					    <td id="Td2" runat="server" align="left" valign="top"><div id="Div2" class="blk_1" runat="server"> <span id="Span14" runat="server">
						    <asp:TextBox runat="server" ID="tb_Discount" Style="text-align:right;" MaxLength="16" Text='<%# System.Math.Round(Convert.ToDecimal(Eval("DiscPercentage"))) %>' placeholder="Discount" ReadOnly="true" CssClass="u_oder_fill_white_label"></asp:TextBox>
					      </span></div></td>
				      </tr>
					  <tr style="height:10px">
					    <td align="left" valign="top"><div class="blk_1"> <span id="Span15">
                        <label class="t-lable">Bill Price(Per Pack)</label>
						    <asp:TextBox runat="server" ID="txt_Customerprice" MaxLength="16" Style="text-align:right; background-color:#efefef;" onkeyup="checkDec(this);" BackColor="White" TabIndex="3" Placeholder="Cust. Price" CssClass="u_oder_fill_white_label" Text='<%# Convert.ToString(Eval("CSellingPrice")).Equals("0")?"":Convert.ToString(((Convert.ToDecimal(Eval("CSellingPrice"))))) %>'></asp:TextBox>
						    </span>
						    
					      </div></td>
				      </tr>
					  <tr style="height:10px">
					    <td></td>
				      </tr>
					  <tr style="height:10px">
					    <td align="left" valign="top"><div class="blk_1 tz-blk"> <span id="Span16">
						    <asp:TextBox runat="server" BackColor="White"  onkeypress="return this.value.length<=200"  TabIndex="4" Placeholder="Remark" ID="tb_Remark" CssClass="u_oder_fill_white_label" Text='<%# Eval("Remark") %>' ></asp:TextBox>
					      </span></div></td>
				      </tr>
					  <tr style="height:10px">
					    <td></td>
				      </tr>
					  <tr style="height:10px">
					    <td align="left" valign="top"><div class="blk_1"> <span id="Span17">
                        <label class="t-lable">Total</label>
						    <asp:TextBox runat="server" ID="tb_SellPrice" MaxLength="16" ReadOnly="true"  CssClass="u_oder_fill numeric price" Text='<%# System.Math.Round((Convert.ToDecimal(Eval("ExtField_SellingPrice")))) %>'></asp:TextBox>
					      </span></div></td>
				      </tr>
					  <tr style="height:10px">
					    <td></td>
				      </tr>
				    </table>
				  </div>
				</ItemTemplate>
				<FooterTemplate>
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
		<asp:LinkButton runat="server" ID="LinkButton1" align="left" CssClass="u_f_btn12" OnClick="lb_AddProduct_Click" Text="Add Product" TabIndex="6" OnClientClick="javascript: return OrederReplacementCart();"></asp:LinkButton>
		<asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn" OnClick="lb_Finish_Click" Text="Finish Order" TabIndex="5" OnClientClick="javascript: return OrederReplacementCart();"></asp:LinkButton>
	  </div>
      </div>
	</footer>
  </main>
    </form>
</body>
</html>
