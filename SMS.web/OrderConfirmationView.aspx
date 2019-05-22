<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderConfirmationView.aspx.cs" Inherits="OrderConfirmationView" %>

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
    <script type="text/javascript" src="js/FancyBoxStyle.js"></script>
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>           
    <header>
        <div class="wrap">
        <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo" /></a>
            <div class="inner_logo">
                <a >Order Confirmation<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>                            	
            </div>
           <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
            
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        	<div class="addres_box" style="margin-top:-2px;">
        		<div class="odr_place_address">                                      
                <table width="100%" border="0">
                  
                     <tr>
                       <td><asp:Label Style="text-align:left; font-weight:bold; font-size:13px;"    ID="Label3" runat="server" Text="Booking No:"  ></asp:Label></td>
                       <td  align="left"  valign="top"><asp:Label Style="text-align:left; font-weight:normal;  font-size:13px;"  ID="lblorderno" runat="server" Text=""  ></asp:Label>
                       </td>                                              
                     </tr>

                     <tr>
                     <td style="width:47%"><asp:Label Style="text-align:left; font-weight:bold; font-size:13px;"    ID="Label1" runat="server" Text="Confirmation No:"  ></asp:Label></td>
                     <td align="left" valign="top"><asp:Label Style="text-align:left; font-weight:normal; font-size:13px;"    ID="lblSalesOrderNo" runat="server" Text=""  ></asp:Label> </td>    
                    </tr>
                    
                    <tr>
                    <td><asp:Label Style="text-align:left; font-weight:500; font-weight:bold; font-size:13px;"    ID="Label2" runat="server" Text="Date:"  ></asp:Label></td>
                    <td align="left" valign="top">
                    <asp:Label ID="lblpostingdate"  runat="server" Text="" Style="text-align:left; font-weight:500; font-size:13px;"></asp:Label>
                    </td>  
                    </tr>
                    
                  <tr>
                    <td width="47%" align="left" valign="top" style="font-weight:bold; font-size:13px;">Customer:</td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustName"></asp:Label> </td>
                  </tr>
                  <tr>
                    <td width="47%" height="41" align="left" valign="top" style="font-weight:bold; font-size:13px;">Address :</td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustAdd"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="47%" align="left" valign="top" style="font-weight:bold; font-size:13px;">Contact No. :</td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustConNo"></asp:Label></td>
                  </tr>
                </table>
            </div>
            	<div class="odr_place_address">
                <table width="100%" border="0">
                  <tr>
                    <td width="47%" align="left" valign="top" style="font-weight:bold; font-size:13px;">Consignee: </td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConName"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="47%" height="41" align="left" valign="top" style="font-weight:bold; font-size:13px;">Address :</td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConAdd"></asp:Label></td>
                  </tr>
                  <tr>
                    <td width="47%" align="left" valign="top" style="font-weight:bold; font-size:13px;">Contact No. :</td>
                    <td width="53%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConNo"></asp:Label></td>
                  </tr>
                </table>
            </div>
            </div>
            <div>
                <asp:Repeater runat="server" ID="rpt_FinalCart" OnItemDataBound="rpt_FinalCart_ItemDataBound" >                  
                    
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list1">
                            <tr>
                                <th width="48%" align="left" valign="middle">PRODUCT</th>
                                <th width="22%" align="center" valign="middle">QTY.</th>
                                <th width="22%" align="center" valign="middle">RATE (<i class="fa fa-inr" aria-hidden="true"></i>)</th>
                                <th width="26%" align="center" valign="middle">Discount %</th>
                                <th width="26%" align="center" valign="middle">VALUE (Excl. Tax) &nbsp;(<i class="fa fa-inr" aria-hidden="true"></i>) </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                             <td align="Left" valign="middle" style="font-size:11px; font-weight:700;"><%# Container.ItemIndex + 1 %>. &nbsp;<%# Eval("Description") %> <br /> <span style="font-size:9px; text-align:left; font-weight:600;"> - (<%# Eval("Variant_Code") %>) </span></td>
                            <td align="right" valign="top"><%# Convert.ToDecimal(Eval("Quantity")).ToString("0") %> <br /> </td>   
                            <td align="right" valign="top"><%# Convert.ToDecimal(Eval("Unit_Cost")).ToString("0") %></td>
                            <td align="right" valign="top"><%# Convert.ToDecimal(Eval("Discount")).ToString("0") %></td>
                            <td align="right" valign="top"><%# Convert.ToDecimal(Eval("Unit_Price")).ToString("0") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>
                                <th width="39%" align="left" valign="middle">TOTAL</th>
                                <th width="11%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalQty"></asp:Label></th>
                                <th width="18%" align="center" valign="middle"></th>
                                 <th width="18%" align="center" valign="middle"></th>
                                <th width="15%" align="right" valign="middle"  colspan="3"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
            </div>
      </div>
    </div>
    <footer>
        <div class="fl_icon"><a href="OrderConfirmation.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	<div class="wrap">        	
        </div>       
    </footer>
</main>
    </form>
</body>
</html>
