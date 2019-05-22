<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActCustomerTotalOutStanding.aspx.cs" Inherits="ActCustomerTotalOutStanding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="js/html5.js"></script>

    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script src="js/AutoComplete.js" type="text/javascript"></script>
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form runat="server" id="frm_Customer">
        <main>           
        <header>
            <div class="wrap">
                <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
                <div class="inner_logo" >
                    <a >Total Outstanding <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>
                </div>
                 <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>
        <div id="middle" class="tz-middle-pad">
            <div class="wrap">                 
                <div class="cuslist tz-ship-list">
                <div class="block_1">                	
                    <div class="cuslist_listing">
                    <div class="idbox">                    	
                        <div class="blk_1"><h3>Customer</h3></div>
                        <div class="blk_1" style="text-align:right;"><h3>Balance (<i class="fa fa-inr" aria-hidden="true"></i>)</h3></div>
                    </div>
                    <asp:Repeater runat="server" ID="rpt_Customer" OnItemDataBound="rpt_Customer_TotalOutStanding_ItemDataBound">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a runat="server" style="text-decoration:none;" id="a_link" >                                 
                                    <div class="Overdue"><span> <%# Eval("Name") %></span></div>
                                    <div class="Overduenew" style="text-align:right; font-weight:600;"><span>(<i class="fa fa-inr" aria-hidden="true"></i>)&nbsp;<%# Eval("Custbalance","{0:0,00}") %></span></div>
                                    <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("CustomerNo") %>' />
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                    <br /><asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            </div>
        </div></div>
        <footer>
            <div class="fl_icon"><a href="AccountStatement.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	    <div class="wrap">
                &nbsp;</div>
        </footer>
    </main>
    </form>
</body>
</html>
