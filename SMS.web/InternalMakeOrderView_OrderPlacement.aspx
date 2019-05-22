<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternalMakeOrderView_OrderPlacement.aspx.cs" Inherits="InternalMakeOrderView_OrderPlacement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
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
    <script src="js/AutoComplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/FancyBoxStyle.js"></script>
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
<link href="css/font-awesome.min.css" rel="stylesheet" />

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
        $(document).ready(function () {
            $("#tb_Discount").keyup(function (event) {

                if (event.keyCode == 13) {
                    $("#tb_Discount").click();
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

    <script type="text/javascript">
        function OpenFullScreenWindow(SalesOrder) {
            debugger;

            var caption = "Statistic";
            var url = "../InternalSalesOrder_Statistic.aspx?SalesOrder=" + SalesOrder + "&#one" + "";
            return GB_showCenter(caption, url, 167, 410)
        }
    </script>

    <script type="text/javascript">
        function OpenFullScreenWindowPreview(SalesOrder) {
            debugger;

            var caption = "Download PDF";
            var url = "../InternalSalesOrder_Preview.aspx?SalesOrder=" + SalesOrder + "";
            return GB_showCenter(caption, url, 500, 1000)
        }
    </script>

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

        /*body{
background:#d0d0d5;
}*/


        /*  Basic stucture
=====================*/
        #accordion {
            margin: 50px auto;
        }

            #accordion ul {
                list-style: none;
                margin: 0;
                padding: 0;
            }

        .accordion {
            display: none;
        }

            .accordion:target {
                display: block;
            }

        #accordion ul li a {
            text-decoration: none;
            display: block;
            padding: 10px;
            visibility: visible;
        }

        .accordion {
            padding: 4px;
        }


        /*  Colors 
====================*/
        #accordion ul {
            /*box-shadow*/
            -webkit-box-shadow: 0 4px 10px #BDBDBD;
            -moz-box-shadow: 0 4px 10px #BDBDBD;
            box-shadow: 0 4px 10px #BDBDBD;
            /*border-radius*/
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

            #accordion ul li a {
                background: #fff;
                border-bottom: 1px solid Black; /*Black ----->>>> #E0E0E0 Changed by Vishal*/
                color: #000000;
                visibility: visible;
            }

        .accordion {
            background: #fdfdfd;
            color: #000000; /*#999    changed by vishal  */
        }

            .accordion:target {
                border-top: 3px solid Black; /* Black --->>> #FFCDCD   changed by vishal*/
            }
    </style>
</head>
<body>
    <script type="text/javascript">
        var GB_ROOT_DIR = '<%= this.ResolveClientUrl("~/greybox/")%>';
    </script>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS_fx.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/gb_scripts.js") %>'></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <main>
        <div style="float:right; right:10px; margin-top:-12px; position: absolute;"><a href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><img src="images/log3.png" style="height:24px; width:19px; margin-top:-10px; " /></a></div>

    <header>
        <div class="wrap">
        <a id="lbl_home_logo"  runat="server"  href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
                    <div class="inner_logo"><a>Make Order<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>                        
                    </div>
                   <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        	<div class="addres_box">
            <div class="odr_place_address_Make_Order">                
                  
 <div id="accordion" style="margin-top:5px; margin-left:0; width:100%; margin-bottom:0;">
  <ul style="box-shadow:none;">
    <li>
       <a href="#one" runat="server" id="one_General"  style="text-decoration-color:black; font-weight:bold; background-color:#ddd; font-size:15px; border-bottom:none;"> <asp:Label ID="lblOrderNo" runat="server"  ></asp:Label><asp:Label  runat="server" Visible="false" ID="lbl_SalesOrderNo"></asp:Label> <br><asp:Label style="margin-left:0;" runat="server" ID="l_CustName"></asp:Label> </a> <%--style="margin-left:20%; text-align:end;"--%>
      <div id="one" style="background-color:#000000;  background: #fff; padding:5px 0;">
        <table width="100%" border="0" style="text-decoration-color:black;" class="internal-option">
                  <tr id="blanketOrder_tr" runat="server" visible="false">
                    <td width="30%" height="26" style="font-weight:bold;" align="left" valign="top">Blanket Order No:</td>
                    <td width="70%" align="left" style="font-weight:bold;" valign="top"><asp:Label runat="server" ID="lbl_blk_No"></asp:Label></td>
                  </tr>  
                  <tr>
                    <td width="30%" height="26" align="left" valign="top" style="font-weight:bold;">Address:</td>
                    <td width="70%" align="left" valign="top"><asp:Label runat="server" ID="l_CustAdd"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="30%" height="25" align="left" valign="top" style="font-weight:bold;">Contact No:</td>
                    <td width="70%" align="left" valign="top"><asp:Label runat="server" ID="l_CustConNo"></asp:Label></td>
                       <asp:HiddenField  ID="hf_ShiptoCode" runat="server" Value="" />
                      <asp:HiddenField ID="hf_SelltoCustomerNo" runat="server" Value="" />
                  </tr>
                <tr>
                    <td width="30%" height="25" align="left" valign="top" style="font-weight:bold;">Consignee:</td>
                    <td width="70%" align="left" valign="top"><asp:Label runat="server" ID="l_Consignee"></asp:Label></td>
                  </tr>
                     <tr>
                    <td width="30%" align="left" valign="middle" style="font-weight:bold;">Location Code:</td>
                    <td width="70%" align="left" valign="top"><asp:DropDownList ID="drplocationcode" AutoPostBack="true" OnSelectedIndexChanged="drplocationcode_SelectedIndexChanged" tabindex="7"  style="background-color:#ffffff;" runat="server" Width="102%" CssClass="u_oder_fill_white_label order_pack">
                    <asp:ListItem  Text="-Select-" Value="0"></asp:ListItem></asp:DropDownList> </td>
                                                             
                  </tr>
                    <tr>
                    <td width="30%" align="left"  valign="middle" style="font-weight:bold;">Sales Order No Series:</td>
                    <td width="70%" align="left" valign="top"><asp:DropDownList ID="drpNoseries" tabindex="1" style="background-color:#ffffff;" Width="102%" runat="server" CssClass="u_oder_fill_white_label order_pack"> 
                        <asp:ListItem  Text="-Select-" Value="0"></asp:ListItem></asp:DropDownList> </td>                                                             
                  </tr>
                  <tr>
                    <td width="30%" align="left" valign="middle" style="font-weight:bold;">Structure:</td>
                    <td width="70%" align="left" valign="top"><asp:DropDownList ID="drpStructure" tabindex="2"  style="background-color:#ffffff;" Width="102%" runat="server" CssClass="u_oder_fill_white_label order_pack"> 
                        <asp:ListItem  Text="-Select-" Value="0"></asp:ListItem></asp:DropDownList></td>                                                               
                  </tr>

                  <tr>
                    <td width="30%" align="left" valign="middle" style="font-weight:bold;">Product Code:</td>
                    <td width="70%" align="left" valign="top"><asp:DropDownList ID="drpProductCode"  tabindex="4" style="background-color:#ffffff;" Width="102%" runat="server" CssClass="u_oder_fill_white_label order_pack">
                        <asp:ListItem  Text="-Select-" Value="0"></asp:ListItem> </asp:DropDownList> </td>                                                              
                  </tr>
                  <tr>
                    <td width="30%" align="left"  valign="middle" style="font-weight:bold;">Transporter Name:</td>
                    <td width="70%" align="left" valign="top"><asp:TextBox ID="txtTransporterName" tabindex="5" style="background-color:#ffffff; font-weight:normal;" runat="server" CssClass="u_oder_fill_white_label"></asp:TextBox></td>
                  </tr>

                     <tr>
                    <td width="30%" align="left" valign="middle" style="font-weight:bold;">Destination:</td>
                    <td width="70%" align="left" valign="top"><asp:TextBox ID="txtDestination"  tabindex="6" style="background-color:#ffffff; font-weight:normal;" runat="server" CssClass="u_oder_fill_white_label"></asp:TextBox></td>
                        <asp:HiddenField runat="server" ID="hf_SalesOrder" Value="" />
                 
                          </tr>    
                 
                    <tr runat="server"  id="tr_CForm" style="visibility:hidden">
                    <td width="30%" align="left"   valign="middle" style="font-weight:bold;">Form Code:</td>
                    <td width="70%" align="left" valign="top"><asp:DropDownList ID="drpFormCode" tabindex="8" style="background-color:#ffffff; font-weight:normal;" runat="server" CssClass="u_oder_fill_white_label">
                    <asp:ListItem  Text="-Select-" Value="0"></asp:ListItem></asp:DropDownList> </td>
                   </tr>          

                </table>
      </div>
    </li>    
  </ul>
</div>
            </div>            	
                <div >                   
                </div>
            </div>
            <div style="margin-top:0;">
                <asp:Repeater runat="server" ID="rpt_FinalCart" OnItemDataBound="rpt_FinalCart_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list1">
                            <tr>
                                <th width="11%" align="center" valign="middle">No</th>
                                <th width="31%" align="Left" valign="middle">Product Name</th>
                                <th width="11%" align="center" valign="middle">QTY.</th>
                                <th width="16%" align="center" valign="middle">Unit Price&nbsp; (&#x20B9;)</th>
                                <th width="24%" align="center" valign="middle">Discount %</th>                                                                
                                <th width="18%" align="center" valign="middle">QTY To Ship</th>                                
                                <th width="24%" align="center" valign="middle">Tax Group Code</th> 
                                  <th width="35%" align="center" valign="middle">Excise Group Code</th>                                                                                                                              
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" valign="middle"><%# Container.ItemIndex + 1 %>.</td>
                            <td align="Left" valign="LEFT" style="font-size:11px; font-weight:700;"><%# Eval("Description") %>  <br /> <span style="font-size:9px; text-align:left; font-weight:600;">- (<%# Eval("VariantCode") %>)</span> </td>   
                            <td align="right" valign="top"><asp:Label id="lbl_QTY" runat="server" Text='<%# Eval("Quantity") %>'> </asp:Label></td>    
                            <td align="right" valign="top"><asp:Label id="Label1" runat="server" Text='<%# Eval("UnitPrice") %>'> </asp:Label></td>   
                            <td align="center"  valign="top"><asp:TextBox runat="server"  ID="tb_discount" MaxLength="6"  Style="text-align:center; background-color:#EFEFEF" TabIndex="9" Width="90%" Height="20px" onkeydown = "return (event.keyCode!=13);" onkeyup="checkDec(this);" Text='<%# System.Math.Round(Convert.ToDecimal(Eval("Discount"))).ToString("0") %>'></asp:TextBox></td>                                              
                            <td align="right"  valign="top"><asp:TextBox runat="server" tabindex="10" ID="Qty_to_Ship" MaxLength="16" onkeydown = "return (event.keyCode!=13);" onkeyup="checkDec(this);"  Style="text-align:center; background-color:#EFEFEF;" Width="90%" Height="20px" Text="" ></asp:TextBox></td>                                                                                                                                                                                                                                                                                                                        
                            <td align="right"  valign="top"><asp:TextBox runat="server" ReadOnly="true" tabindex="12" ID="dd_TaxGroupCode" Style="text-align:center; " Width="90%" Height="20px" ></asp:TextBox></td>
                            <td align="right"  valign="top"><asp:DropDownList runat="server" tabindex="13" ID="dd_Excise_PostingGrpCode" Style="text-align:center; background-color:#EFEFEF;" Width="90%" Height="20px" ></asp:DropDownList></td>                                                                                                                                              
                            <asp:HiddenField runat="server" ID="hf_ItemNo" Value='<%# Eval("ItemNo") %>' />
                            <asp:HiddenField runat="server" ID="hf_LineNo" Value='<%# Eval("LineNo") %>' />                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>                             
                        </table>
                    </FooterTemplate>
                </asp:Repeater>              
            </div>
      </div>
    </div>
    <footer>        
    	<div class="wrap">        	
        
        <div class="links">          
            <asp:LinkButton runat="server" ID="lb_Update"  CssClass="u_f_btn" OnClick="btn_Update"   Text="Update"  TabIndex="13" ></asp:LinkButton>
             <asp:LinkButton runat="server" ID="lb_SalesOrder_Released" OnClick="lb_SalesOrder_Released_Click" Visible="false"  CssClass="u_f_btn"    Text="Release"  TabIndex="16" ></asp:LinkButton>                                         
            <asp:LinkButton runat="server" ID="lb_Preview" OnClick="lb_Preview_Click" CssClass="u_f_btn" Visible="false"   Text="Preview"  TabIndex="15" ></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lb_Satistic" OnClick="lb_Satistic_Click" Visible="false"  CssClass="u_f_btn"  Text="Statistic"  TabIndex="14" ></asp:LinkButton>   
        	</div>  </div>     
    </footer>         
</main>
    </form>
</body>
</html>
