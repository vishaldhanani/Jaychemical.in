<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderConfirmation.aspx.cs"
    Inherits="OrderConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <link href="css/minimal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/1.8.3.jquery.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <link href="fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery.icheck.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function OpenFullScreenWindow(code) {

            var caption = "Download PDF";
            var url = "../PrintOrderConfirmationReceipt.aspx?OrderNo=" + code + "";
            return GB_showCenter(caption, url, 450, 300)
        }
    </script>


    <script language="JavaScript" type="text/javascript">
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
    <%--Include grybox javascript files--%>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS_fx.js") %>'></script>

    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/gb_scripts.js") %>'></script>
    <form runat="server" id="frm_Customer">
        <main>
        <header>
            <div class="wrap">
                <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo" /></a>
                <div class="inner_logo">
                 <a >Order Confirmation <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>    
                </div>
                <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>

        <div id="middle" class="tz-middle-pad">
            <div class="wrap">
                
                <div class="cuslist tz-ship-list" style="padding-top:8px;">
                    <div class="block_1">
                        <div class="title_1">
                            <asp:TextBox runat="server" ID="tb_Search" Visible="true" CssClass="textbox_1"  Placeholder="Order Confirmation" OnTextChanged="tb_Search_TextChanged"></asp:TextBox>
                            <asp:Button runat="server" ID="btn_OrderProduct" OnClick="btn_SearchOrder_Click" Visible="true" CssClass="search_button"></asp:Button>
                        </div>
                        <div class="cuslist_listing">
                        <div class="idbox">
                            <div class="Header" style="text-align:left;"><h3>Booking No</h3></div>
                            <div class="Header1" style="text-align:right;"><h3> Confirmation No</h3></div>
                        </div>
                        <asp:Repeater runat="server" ID="rpt_Order" OnItemDataBound="rpt_Item_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>                              
           				                <div class="OrderData"><%# Eval("OrderNo") %></div>     
                                        <div class="OrderData1" style="text-align:right"><span> <%# Eval("SalesOrderNo") %> </span> 
                                                                       
                                        <a runat="server" style="text-decoration:none;" id="a_link" >
                                         <a style="text-align:center;" class="tz-view-details"  href='OrderConfirmationView.aspx?OrderNo=<%# Eval("OrderNo") %> &PostingDate= <%#Convert.ToDateTime(Eval("PostingDate"))%> &SalesOrderNo= <%#Convert.ToString(Eval("SalesOrderNo"))%> ' >VIEW<!--<img style="width:45px; padding-top:4px; height:auto;" class="icon12" src="images/view-button.png">--> </a>
                                          <a href="#" class="tz-pdf-icon"  style="text-decoration:none;" onclick='<%# string.Format("javascript:return OpenFullScreenWindow(\"{0}\")", Eval("OrderNo")) %>' ><i class="fa fa-file-pdf-o" aria-hidden="true"></i><!--<img style="width:25px; padding-top:3px; height:auto; " class="icon" src="images/pdfp.png">--></a> 
                                             </a>
                                      </div>  
                                </li>

                            </ItemTemplate>

                            <FooterTemplate>
                                
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                            </div>
                        <br /><asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
       <footer>
        <div class="fl_icon"><a href="DashBoard.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
    	<div class="wrap">
        	
        </div>
       
    </footer>
        </main>
    </form>
</body>
</html>
