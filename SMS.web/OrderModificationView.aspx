<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderModificationView.aspx.cs" Inherits="OrderModificationView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
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
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 400px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">

        function validateCustRefNo() {
            if (!form1.txtCustomerReferenceNo.value) {
                alert("Please Enter Purchase Order No.");
                return (false);
            }
            else if (form1.txtCustomerReferenceNo.value && document.getElementById('cbTerms').checked == false) {
                alert("Please agree to terms and conditions.");
                return (false);

            }
            return (true);
        }
    </script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                $addHandler(document, "keydown", onKeyDown);
            }
        }

        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mp1').hide();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
        	<div class="addres_box">
        		<div class="odr_place_address">
                <table width="100%" border="0">
                     <tr>
                    <td colspan="2" style="font-size:18px; font-weight:bold; padding-bottom:3px;" align="Center" valign="top">Sales Order Modification<br /></td>
                    </tr>
                    <tr>
                    <td style="font-size:15px;" align="left" valign="top"><asp:Label Style="text-align:left; font-weight:bold;" ID="lblorderno" runat="server" Text=""  ></asp:Label> </td>
                        <td style="font-size:15px;" align="right" valign="top">
                            <asp:Label ID="lblpostingdate"  runat="server" Text="" Style="text-align:right;  font-weight:bold;"  ></asp:Label>
                        </td>                        
                  </tr>
                </table>
                </div>  
                  
                  
                  <div class="odr_place_address">
                <table width="100%" border="0">
                      <tr>
                       <td width="30%" align="left" valign="top" style=" font-size:13px;"><asp:Label Style="text-align:left; font-weight:bold;"    ID="Label3" runat="server" Text="Blanket Order No:"  ></asp:Label></td>
                       <td width="70%" style="font-size:13px" align="left" valign="top"><asp:Label Style="text-align:left; font-weight:bold;"  ID="lbl_blanketOrderNo" runat="server" Text=""  ></asp:Label>
                       </td>                                              
                     </tr>          

                  <tr>
                    <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Customer: </strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustName"></asp:Label> </td>
                  </tr>
                  <tr>
                    <td width="30%" height="41" align="left" valign="top" style=" font-size:13px;"><strong>Address: </strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustAdd"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Contact No: </strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustConNo"></asp:Label></td>
                  </tr>
                </table>
            </div>
            	<div class="odr_place_address">
                <table width="100%" border="0">
                  <tr>
                    <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Consignee:</strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConName"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="30%" height="41" align="left" valign="top" style=" font-size:13px;"><strong>Address:</strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConAdd"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Contact No:</strong></td>
                    <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConNo"></asp:Label></td>
                  </tr>
                </table>
            </div>

                <div style="background-color:#fff;">
                    <table width="100%" border="0">
                        <tr>
                    <td width="100%" align="left" valign="top"><asp:TextBox runat="server" CssClass="u_oder_fill_white_label13" ID="txtCustomerReferenceNo" ReadOnly="true" TabIndex="1" CausesValidation="true" PlaceHolder="Purchase Order No" Style="width:94%; background-color:#EFEFEF;"></asp:TextBox>                                              
                    </td>
                            
                  </tr>
                        </table>
                </div>               
            </div>
            <div>
                <asp:Repeater runat="server" ID="rpt_FinalCart" OnItemDataBound="rpt_FinalCart_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list1">
                            <tr>
                                <th width="12%" align="center" valign="middle">No</th>
                                <th width="32%" align="Left" valign="middle">PRODUCT NAME</th>
                                <th width="18%" align="center" valign="middle">QTY.</th>
                                 <th width="18%" align="center" valign="middle">O/S QTY.</th>                                                                                                
                                <th width="30%" align="center" valign="middle">VALUE (Excl.Tax)&nbsp; (<i class="fa fa-inr" aria-hidden="true"></i>)</th>
                                <th  width="16%" align="center" valign="middle">Edit</th>
                                <th  width="23%" align="center" valign="middle">Delete</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" valign="top"><%# Container.ItemIndex + 1 %>.</td>
                            <td align="Left" valign="top" style="font-size:11px; font-weight:700;"><%# Eval("Description") %>  <br /> <span style="font-size:9px; font-weight:600;">- ( <%# Eval("VariantCode") %> )</span> </td>   
                            <td align="right" valign="top"><%# Eval("Quantity") %> <br/> <span style="font-size:9px;"><%# Eval("UOM") %> </span>  </td> 
                            <td align="right" valign="top"><%# Eval("OutStandingQty") %> </td>                                                                                                                                                                                                                                                                   
                            <td align="right"  valign="top"><%# Convert.ToString(Eval("Amount")) %></td>
                            <asp:HiddenField runat="server" ID="hf_ItemNo" Value='<%# Eval("ItemNo") %>' />
                            <asp:HiddenField runat="server" ID="hf_LineNo" Value='<%# Eval("LineNo") %>' />
                            <asp:HiddenField runat="server" ID="hf_QuantityShipped" Value='<%# Convert.ToDecimal(Eval("QuantityShipped")) %>' />

                            <td align="center" valign="middle">
                                <asp:HiddenField ID="hf_ItemCategoryCode" runat="server" Value='<%# Eval("ItemCategoryCode") %>'/>
                                 <asp:LinkButton runat="server" ID="btn_edit" CommandName="edit" OnCommand="btn_edit_Command" Style="padding:0;"><i class="fa fa-2x fa-pencil" aria-hidden="true"></i></asp:LinkButton>                                                              
                            </td>
                            <td align="center" valign="middle">                                
                                <asp:LinkButton runat="server" ID="btn_delete" CommandName="delete" OnClientClick="return confirm('Do you want to delete this Item ?');" OnCommand="btn_delete_Command" ><i class="fa fa-2x fa-times" aria-hidden="true"></i></asp:LinkButton>                                                                
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>
                                <th width="8%" align="center" valign="middle"></th>
                                  <th width="8%" align="center" valign="middle">TOTAL</th>
                                <th width="39%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalQty"></asp:Label></th>
                                <th width="11%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalOSQTY"></asp:Label></th>                                
                                <th width="15%" align="right" valign="middle"  colspan="1"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>
                                <th width="15%" align="right" valign="middle"  colspan="1">&nbsp;</th>
                                <th width="15%" align="right" valign="middle"  colspan="1">&nbsp;</th>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>                                             
            </div>
      </div>
    </div>
    <footer>
        <div class="fl_icon"><a href="OrderModification.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	<div class="wrap">
        	
        </div>                                
    </footer>            
</main>
    </form>
</body>
</html>
