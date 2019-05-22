<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipmentScheduleConsigneeSummaryNew.aspx.cs" Inherits="ShipmentScheduleConsigneeSummaryNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
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
     <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
<link href="css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>
        <header>
        <div class="wrap">
        <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
            <div class="inner_logo" >
            	<a>Shipment Schedule<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>
            </div>
            <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
          
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>                      
        	<div>
        		<div style="margin-top:-1%">                                  
                <table width="100%" border="0">
                    <tr style="text-align:center; font-size:13px; text-decoration:underline; font-weight:500; ">
                    <td><asp:Label ID="lblName" runat="server" Text="" ></asp:Label></td>
                  </tr>
                 
                </table>          
            </div>
            <div>
                <asp:Repeater runat="server" ID="rpt_Q1_Cust_Invoice" OnItemDataBound="rpt_ShipmentSch_Consignee_ItemDataBound" >                                        
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list_Schedule">
                            <tr>                                 
                                <th width="50%" align="center" valign="middle"><div align="left">Name <br /> Order No</div></th>
                                <th width="50%" align="center" valign="middle"><div align="right"> Quantity<br />Planned Date</div></th>                                                                                                                                                          
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                             <td align="left" valign="middle"  width="50%" style="border-right-style:hidden;"> <div  style="font-size:11px; width:160%; font-weight:bold; "><%#  Eval("Consigneename") %> </div> <a style="font-size:9px;   "> <%# Eval("Code") %></a> <br /> <a style="font-size:9px;"><%# Convert.ToDateTime(Eval("OrderDate")).ToString("dd/MM/yy") %> </a>   </td>
                             <td align="right"  valign="top"  width="50%" > <div style="font-size:12px; font-weight:bold; "> <%# Convert.ToDecimal(Eval("qty")).ToString("0") %> </div><br /> <asp:Label runat="server" id="tddate" style="font-size:9px;" Text='<%#  Convert.ToDateTime(Eval("PlannedDate")).ToString("dd/MM/yy").Equals("01-01-53") || Convert.ToDateTime(Eval("PlannedDate")).ToString("dd/MM/yy").Equals("01/01/53") ?"":Convert.ToDateTime(Eval("PlannedDate")).ToString("dd/MM/yy") %>'></asp:Label></td>                                                                                                                                                                                                   
                             <td align="right" runat="server" id="tdCounter" style="visibility:hidden;" valign="middle"><%# Convert.ToInt32(Eval("count")).ToString("0") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>  
                                 <th width="50%" align="center" valign="middle">TOTAL</th>
                                 <th width="50%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>                                                                                              
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
            </div>
      </div>
    </div>
    <footer>
        <div class="fl_icon"><asp:LinkButton ID="BackLink" runat="server" OnClick="BackLink_Click"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></asp:LinkButton></div>
    	<div class="wrap">
        	
        </div>       
    </footer>
</main>
    </form>
</body>
</html>
