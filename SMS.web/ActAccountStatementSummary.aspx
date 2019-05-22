<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActAccountStatementSummary.aspx.cs" Inherits="ActAccountStatementSummary" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <a href="Login.aspx" class="logout" onclick="return confirm('Are you sure to logout?');"></a>
    <header>
        <div class="wrap">
        <div class="home_icon"><a href="DashBoard.aspx"><img src="images/home-icon.png" width="128" height="128" /></a></div>
            <div class="inner_logo"><a href="#"><img src="images/logo.png" /></a>
            	<div class="module_header"><asp:Label ID="l_headerText" runat="server" ></asp:Label></div>
            </div>
        </div>
    </header>
          
    <div id="middle">
        <div class="wrap">
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>                      
        	<div class="addres_box">
        		<div class="odr_place_address">
                <table width="100%" border="0">
                    <tr>
                    <td colspan="2" style="font-size:large" align="Center" valign="top">Account Statement Details<br /><br /></td>
                  </tr>                 
                </table>          
            </div>
            <div>
                <asp:Repeater runat="server" ID="rpt_Account_Stmt" OnItemDataBound="rpt_Account_Stmt_ItemDataBound" >                                         
                    <HeaderTemplate>
                        <table width="100%" border="1" class="odr_tbl_list">
                            <tr>                               
                                <th width="38%" align="center" valign="middle">Invoice No.</th>                             
                                <th width="50%" align="center" valign="middle">Posting Date</th>
                                <th width="50%" align="center" valign="middle">Due Date</th>                               
                                <th width="49%" align="right" valign="middle">O/S. Amount</th>                              
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                          
                            <td align="center" valign="middle"><%# Eval("InvoiceNo") %></td>                           
                            <td align="center" valign="middle"><%# Convert.ToDateTime(Eval("PostingDate")).ToString("dd/MM/yy") %> </td>
                            <td align="center" valign="middle"><%# Convert.ToDateTime(Eval("DueDate")).ToString("dd/MM/yy") %> </td>
                            <td align="right" valign="middle"><%# Convert.ToDecimal(Eval("Amount")).ToString("0") %></td>                           
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                             <tr>
                                <th width="8%" align="center" valign="middle"></th>
                                <th width="39%" align="center" valign="middle"></th>                              
                                <th width="18%" align="center" valign="middle">TOTAL (&#x20B9;)</th>
                                <th width="15%" align="right" valign="middle"  colspan="3"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
            </div>
      </div>
    </div>
        </div>
    <footer>
        <div class="fl_icon"><a  href="ActCustomerAccountStatement.aspx"><img src="images/leftr.png" /></a></div>
    	<div class="wrap">        	
        </div>        
    </footer>
</main>
    </form>
</body>
</html>
