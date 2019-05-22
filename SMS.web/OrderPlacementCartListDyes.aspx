<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPlacementCartListDyes.aspx.cs" Inherits="OrderPlacementCartListDyes" %>

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


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <main>
            
    <header>
        <div class="wrap">
        <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo" /></a>
            <div class="inner_logo" ><a>Order Placement<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>            	
            </div>
            <div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>        	
            <div class="addres_box">
        		<div class="odr_place_address" style=" background-color:#8A0886; color:#ffffff;">
                <table width="100%" border="0">
                    <tr>
                    <td colspan="2" style="font-size:large;" align="Center" valign="top">Item Cart Summary<br /></td>
                  </tr>
                    </table></div></div>
            <div>
                <asp:Repeater runat="server" ID="rpt_FinalCart" OnItemDataBound="rpt_FinalCart_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list1 table-striped">
                            <tr>                                
                                <th width="48%" align="Left" valign="middle">PRODUCT</th>
                                <th width="18%" align="center" valign="middle">QTY.</th>   
                                <th width="18%" align="center" valign="middle">UNIT PRICE&nbsp; (<i class="fa fa-inr" aria-hidden="true"></i>)</th>                                
                                <th width="33%" align="center" valign="middle">VALUE (Excl.Tax)&nbsp; (<i class="fa fa-inr" aria-hidden="true"></i>)</th>
                                <th  width="16%" align="center" valign="middle">Edit<br />Del.</th>                                                                                           
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                            
                            <td align="Left" valign="LEFT" style="font-size:11px; font-weight:700;"><%# Container.ItemIndex + 1 %>. &nbsp;<%# Eval("ProductName") %> <br /> <a style="font-size:9px; text-align:left; color:#000;"> - (<%# Eval("VariantCode") %>) </a></td>
                              <td align="right" valign="top">
                                <%# Eval("Quantity") %>                                 
                               <br /><a style="font-size:9px; text-align:right; padding:3px 0 3px; color:#000;">(<%# Eval("UOM") %>)</a></td>                         
                                <td align="right"  valign="top"><%# Math.Round(Convert.ToDecimal(Eval("DyesNetAmount"))) %>                                                                                                                                                                                                                          
                                <td align="right"  valign="top"><%# (Convert.ToString(Eval("SellingPrice"))) %>
                                <td align="center" valign="middle"><asp:LinkButton runat="server" ID="btn_edit" CommandName="edit" OnCommand="btn_edit_Command" CommandArgument='<%#Eval("ProductCode")%>'><i class="fa fa-lg fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                  <asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ProductCode") %>' />
                                 <asp:HiddenField runat="server" ID="hf_VariantCode" Value='<%# Eval("VariantCode") %>' />
                                <asp:HiddenField runat="server" ID="hdn_Nofield" Value='<%# Eval("noField") %>' />
                                <asp:LinkButton runat="server" ID="btn_delete" CommandName="delete" OnClientClick="return confirm('Do you want to delete this Item ?');" OnCommand="btn_delete_Command" CommandArgument='<%#Eval("ProductCode")%>'><i class="fa fa-lg fa-times" aria-hidden="true"></i></asp:LinkButton></td>

                            </td>                           
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>                                
                                <th width="39%" align="left" valign="middle">TOTAL</th>
                                <th width="11%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalQty"></asp:Label></th>   
                                 <th width="15%" align="right" valign="middle"  colspan="1"><asp:Label runat="server" ID="l_TotalUnitPrice"></asp:Label></th>                          
                                <th width="15%" align="right" valign="middle"  colspan="1"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>
                <th width="39%" align="center" valign="middle"></th>

                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                
               <br />
            </div>
      </div>
    </div>
    <footer>
           	<div class="wrap">        	         
         <div class="links">
          <a id="btn_back"  runat="server" class="u_f_btn12" href="ProductItemNew.aspx">Add Product</a>     
            <asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"   OnClick="lb_Finish_Click" Text="Finish Order" TabIndex="5" ></asp:LinkButton>                                         
        </div>       
          </div>      
    </footer>            
</main>
    </form>
</body>
</html>
