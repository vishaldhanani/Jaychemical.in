<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternalMakeOrderModification.aspx.cs"
    Inherits="InternalOrderModification" %>

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
</head>

<body>
    <form runat="server" id="frm_Customer">
        <main>                         
        <header>
            <div class="wrap">               
                 <a id="lbl_home_logo"  runat="server"  href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo"/></a>
                    <div class="inner_logo">
                         <a >Make Order<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>                                                
                    </div>
                   <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>

        <div id="middle">
            <div class="wrap">                 
                <div class="cuslist tz-ship-list" style="padding-top:8px;">
                    <div class="block_1">   
                         <div class="title_1">
                            <asp:TextBox runat="server" ID="tb_Search" Visible="true" CssClass="textbox_1" OnTextChanged="tb_Search_TextChanged"  Placeholder="Search Order" ></asp:TextBox>
                            <asp:Button runat="server" ID="btn_OrderSearch" OnClick="btn_OrderSearch_Click"  Visible="true" CssClass="search_button"></asp:Button>
                        </div>                    
                        <div class="cuslist_listing">
                        <div class="idbox">
                            <div class="blk_1" style="width:100%;"><h3>Order No</h3></div>                            
                        </div>
                        <asp:Repeater runat="server" ID="rpt_Order" OnItemDataBound="rpt_Item_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>                              
           				                <div class="blk_1"><span><%# Eval("OrderNo") %></span></div>                                        
                                        <div class="blk_198 edit-view">
                                      <span runat="server" id="a_link">
                                         <a  class="addtocart" href='InternalMakeOrderView.aspx?OrderNo=<%# Eval("OrderNo") %>'>View</a>                                        
                                            <a  class="addtocart" href='BlanketOrderModificationView.aspx?OrderNo=<%# Eval("OrderNo") %> &PostingDate=<%# Eval("PostingDate") %>'>Edit</a>
                                             </span>                                       
                                    </div>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>                                
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>

                            <div class="idbox">
                            <%--<div class="blk_1" style="width:100%;"><h3><asp:Label runat="server" id="lblBlockOrder" Text="Blocked Orders" ></asp:Label> </h3></div>  --%>                          
                        </div>
                        <%--<asp:Repeater runat="server" ID="rpt_OrderBlock" OnItemDataBound="rpt_OrderBlock_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>                              
           				                <div class="blk_1"><span><%# Eval("OrderNo") %></span></div>                                        
                                        <div class="blk_198 edit-view">
                                      <span runat="server" id="a_link">
                                         <a  class="addtocart" href='InternalMakeOrderViewBlocked.aspx?OrderNo=<%# Eval("OrderNo") %>'>View</a>                                                                                    
                                             </span>                                       
                                    </div>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>                                
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>--%>

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
