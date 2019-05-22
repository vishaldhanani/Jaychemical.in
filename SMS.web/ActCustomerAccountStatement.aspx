<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActCustomerAccountStatement.aspx.cs" Inherits="ActCustomerAccountStatement" %>

<!DOCTYPE html>
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
    <script type="text/javascript" src="js/FancyBoxStyle.js"></script>
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function OpenFullScreenWindow(code) {


            var caption = "Download PDF";
            var url = "../PrintAccountStatementReceipt.aspx?CustomerNo=" + code + "";
            return GB_showCenter(caption, url, 450, 300)
        }
    </script>

    <script type="text/javascript">
        function OpenFullScreenWindowMail(code) {


            var caption = "Mail";
            var url = "../SendMail_Customers.aspx?CustomerNo=" + code + "";
            return GB_showCenter(caption, url, 450, 300)
        }
    </script>

    <script lang="JavaScript" type="text/javascript">
        document.onkeypress = function noNumbers2(e) {
            e = e || window.event;
            var keynum = e.keyCode || e.which;
            if (keynum == 27) {
                AJS.AEV(document, "keypress", GB_hide);
            }
        }
    </script>
</head>

<body>
    <script type="text/javascript">
        var GB_ROOT_DIR = '<%= this.ResolveClientUrl("~/greybox/")%>';
    </script>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS_fx.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/gb_scripts.js") %>'></script>


    <form runat="server" id="frm_Customer">
        <main>            
        <header>
            <div class="wrap">
                <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
                <div class="inner_logo">
                    <a >Account Statement<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>  
                </div>
                 <div class="logout-btn"><a  class="bt_Logout" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>
        <div id="middle" class="tz-middle-pad">
            <div class="wrap">                                                 
                <div class="cuslist tz-ship-list">
                <div class="block_1">                	                                   	
                    <div class="cuslist_listing">
                    <div class="idbox">                    	
                        <div class="blk_1"><h3>Customer</h3></div>
                        <div class="blk_1" style="text-align:right;"><h3> Balance (<i class="fa fa-inr" aria-hidden="true"></i>)</h3></div>
                    </div>
                    <asp:Repeater runat="server" ID="rpt_Customer" OnItemDataBound="rpt_Customer_ItemDataBound" >  
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                    <div class="Account"><span> <%# Eval("Name") %></span></div>
                                    <div class="Accountnew"><span style="text-align:right; margin-right:0; font-weight:600; font-size:13px;">&nbsp;(<i class="fa fa-inr" aria-hidden="true"></i>)&nbsp;<%# Eval("Custbalance", "{0:0,00}") %></span>
                                       <a runat="server" style="text-decoration:none;" id="a_link" >
                                       <a class="addtocart"  onclick='<%# string.Format("javascript:return OpenFullScreenWindowMail(\"{0}\")", Eval("CustomerNo")) %>' >Mail</a> 
                                       <a class="tz-pdf-icon" onclick='<%# string.Format("javascript:return OpenFullScreenWindow(\"{0}\")", Eval("CustomerNo")) %>'><i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                                       </a>   
                                       </a>
                                    </div>                                  
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                        </div>
                </div>
                    <br /><asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            </div>
        </div>
        <footer>           
            <div class="fl_icon"><a href="AccountStatement.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a>
    	    <div class="wrap">
                &nbsp;</div></div>
        </footer>
    </main>
    </form>
</body>
</html>
