<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderModificationItems.aspx.cs" Inherits="OrderModificationItems" %>

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
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">

        function fnShowMessage() {
            alert("Don't enter more than 15% price.");
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

</head>
<body>
    <form id="frm_ViewCart" runat="server">

        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <main>
            
    <header>
        <div class="wrap">

             <a id="lbl_home_logo"  runat="server"  href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo" /></a>
                    <div class="inner_logo"><a>Order Modification<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>                        
                    </div>
                   <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
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
                        <div><span ><asp:TextBox runat="server" width="100%" Height="25px" CssClass="page-heading" readonly="true" ID="tb_ProductName"  Text='<%# Eval("Description") %>'></asp:TextBox></span></div>
                        </div>                        
                        <div>
                            <table width="100%" border="0" >                                  
                                             <tr>                                              
                                                <td width="100%"  align="left" valign="top">
                                                        <div class="blk_1 tz-blk">                               
                                                        <span id="Span5"> 
                                                            <asp:DropDownList runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ID="dd_Variant" TabIndex="1" Width="100%"    AutoPostBack="true"  CssClass="u_oder_fill_white_label order_pack" >                                                            
                                                            </asp:DropDownList></span></div>
                                                             <asp:HiddenField runat="server" ID="hf_UOM"  Value='<%# Convert.ToString(Eval("UOM")).Equals("")?"":Eval("UOM") %>' />
                                                            <asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ItemNo") %>' />
                                                            <asp:HiddenField runat="server" ID="hf_Price" Value='<%# Convert.ToDecimal(Eval("UnitPrice")) %>' />
                                                           <asp:HiddenField runat="server" ID="hf_QuantityShipped" Value='<%# Convert.ToDecimal(Eval("QuantityShipped")) %>' />
                                                           <asp:HiddenField runat="server" ID="hf_OutStandingQty" Value='<%# Convert.ToDecimal(Eval("OutStandingQty")) %>' /> 
                                                             <asp:HiddenField runat="server" ID="hf_SelltoCustomerNo" Value='<%# Eval("SelltoCustomerNo") %>' />
                                                            <asp:HiddenField runat="server" ID="hf_ShiptoCode" Value='<%# Eval("ShiptoCode") %>' />
                                                             <asp:HiddenField runat="server" ID="hf_CustomerPriceGroup" Value='<%# Eval("CustomerPriceGroup") %>' />                                                                                                                                                              
                                                  </td>
                                                </tr>                                                               
                                    <tr>                                                                                       
                                        <td width="100%" align="left" valign="top">
                                            <div class="blk_1 tz-blk">                               
                                               <span id="Span12">
                                               <label class="t-lable">No. of Packs</label>
                                               <asp:TextBox runat="server" ID="tb_Qty" BackColor="white" ToolTip="Qty" AutoPostBack="true" Style="text-align:right;font-size:small;" OnTextChanged="tb_Qty_TextChanged"  onkeyup="checkDec(this);" TabIndex="2" onkeydown = "return (event.keyCode!=13);" Placeholder="Qty" CssClass="u_oder_fill_white_label"  Text='<%# Convert.ToString(Eval("Quantity")).Equals("1")?"":Eval("Quantity") %>'>
                                                 </asp:TextBox> 
                                                   </span>
                                             </div>                                           
                                          </td>
                                     </tr>

                                    <tr>                                                                                        
                                        <td width="100%" align="left" valign="top">
                                            <div class="blk_1">                               
                                            <span id="Span13">
                                            <label class="t-lable">Price per Pack</label>
                                            <b><asp:TextBox runat="server" ID="tb_Price" BackColor="#efefef" Placeholder="Price" CssClass="u_oder_fill_white_label" Style="text-align:right;" ReadOnly="true" Text='<%# (Convert.ToDecimal(Eval("UnitPrice"))).ToString("0.00") %>'></asp:TextBox>                                                               </b>                             
                                            </span>
                                            </div>
                                        </td>
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
                                        <td width="100%" align="left" valign="top">
                                         <div class="blk_1">                               
                                            <span id="Span15">
                                            <label class="t-lable">Bill Price</label>
                                            <asp:TextBox runat="server" ID="txt_Customerprice" Text='<%# (Convert.ToDecimal(Eval("UnitPrice"))).ToString("0.00") %>' onkeyup="checkDec(this);" Style="text-align:right;"  TabIndex="3" Placeholder="Cust. Price" CssClass="u_oder_fill_white_label" ></asp:TextBox>                                                                                                                                     
                                                  </span>
                                         </div>
                                         </td>
                                     </tr>

                                    <tr>                                        
                                        <td width="100%" align="left" valign="top">
                                        <div class="blk_1 tz-blk">                               
                                        <span id="Span16">
                                        <asp:TextBox runat="server" BackColor="White" TabIndex="4" Text='<%# Eval("Remark") %>' Placeholder="Remark" ID="tb_Remark" CssClass="u_oder_fill_white_label" ></asp:TextBox>                                          
                                        </span>
                                        </div>
                                        </td>
                                    </tr>

                                    <tr>                                       
                                        <td width="100%" align="left" valign="top">
                                         <div class="blk_1">                               
                                         <span id="Span17">
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
                        </div></div>
                </div>
            </div>        	
      </div>               
    <footer>
        <div class="fl_icon"><a href="OrderModificationView.aspx?OrderNo=<%=Request.QueryString["OrderNo"]%> &BlanketOrderNo= <%= Convert.ToString(Request.QueryString["BlanketOrderNo"])%> &PostingDate=<%= Convert.ToString(Request.QueryString["PostingDate"]) %>"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>                 
    	<div class="wrap">                
        <div class="links">           
            <asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"   OnClick="lb_Modify_Click" Text="Modify" TabIndex="5" OnClientClick="javascript: return OrederReplacementCart();"></asp:LinkButton>                                         
        	</div></div>
    </footer>
</main>

    </form>
</body>
</html>
