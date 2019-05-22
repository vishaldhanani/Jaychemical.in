<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActTotalOutStandingSummaryNew.aspx.cs" Culture="en-GB" Inherits="ActTotalOutStandingSummaryNew" %>

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
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.gif" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>           
    <header>
        <div class="wrap">
        <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
            <div class="inner_logo" >
            	 <a >Total Outstanding <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>
            </div>
            <div class="logout-btn"><a class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
    </header>
          
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>                      
        	<div class="tz-ship-list">
        		<div style="margin-top:-1%">                                    
                <table width="100%" border="0">
                    <tr style="text-align:center; font-size:14px; font-weight:500; text-decoration:none; line-height:17px; ">
                    <td>
                       <h3> <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h3>
                    </td>
                  </tr>                 
                </table>        
            </div>
            <div>
                <asp:Repeater runat="server" ID="rpt_Account_Stmt" OnItemDataBound="rpt_TotalOutStandingSumm_ItemDataBound" >
                       <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list table-striped">
                            <tr>                                    
                                <th width="50%" align="center" valign="middle"><div align="left">Invoice No.<br /> Posting Date</div></th>             
                                <th width="50%" align="center" valign="middle"><div align="right">O/S. Amount<br /> Due Date </div></th>                                                                                                                                                 
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                              
                            <td align="left" valign="middle"  width="50%" style="border-right-style:hidden;"> <div  style="font-size:13px; width:160%; font-weight:bold; "> <%# Eval("InvoiceNo") %> </div> <a style="font-size:12px; color:#000; text-decoration:none;"> <%# Convert.ToDateTime(Eval("PostingDate")).ToString("dd/MM/yy") %> </a></td>                                                        
                            <td align="right" valign="top"  width="50%" style="font-size:13px; font-weight:600;" > <asp:Label style="font-size:12px; font-weight:bold;" Text='<%# Eval("Amount", "{0:0,00}") %>' id="tdAmt" runat="server">   </asp:Label><br /> <asp:Label Text='<%# Convert.ToDateTime(Eval("DueDate")).ToString("dd/MM/yy") %>' style="font-size:12px; font-weight:normal;" id="tdduedate" runat="server"> </asp:Label></td>                                                                                                         
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>
                                 <th width="50%" align="left" valign="middle" style="font-size:14px;">TOTAL (<i class="fa fa-inr" aria-hidden="true"></i>)</th>                                                                
                                <th width="15%" align="right" valign="middle" style="font-size:14px;"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>                               
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
            </div>
      </div>
    </div></div>
    <footer>
        <div class="fl_icon"><a href="ActCustomerTotalOutStanding.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	<div class="wrap">
        	
        </div>       
    </footer>
</main>
    </form>
</body>
</html>
