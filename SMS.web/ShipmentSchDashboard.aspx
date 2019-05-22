<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipmentSchDashboard.aspx.cs" Inherits="ShipmentSchDashboard" %>

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
                <a href="DashBoard.aspx"><img src="images/logo.png" class="inner-logo" /></a>
                <div class="inner_logo" >           
                     <a >Shipment Schedule<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>              
                </div>
                <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>
       <div id="middle">
        
        <div class="wrap">
            <div class="cuslist tz-ship-list">
                <div class="block_1">                	                                                   
        	<div class="tab">
                <div class="tz-blk">
                   <asp:DropDownList ID="drpSelection"  runat="server"   CssClass="u_oder_fill_dd_white" OnSelectedIndexChanged="drpSelection_SelectedIndexChanged" AutoPostBack="true" >
                       <asp:ListItem Text="Customer wise Summary" Value="1" ></asp:ListItem>
                       <asp:ListItem Text="Item wise Summary" Value="2" ></asp:ListItem>
                       <asp:ListItem Text="Consignee wise Summary" Value="3" ></asp:ListItem>
                   </asp:DropDownList>
                </div>            	
        	</div>

            <div id="div_Customer" class="cuslist_listing" runat="server" >
                    <div class="idbox">                    	
                        <div class="blk_1"><h3>Name</h3></div>
                        <div class="blk_1" style="text-align:right;"><h3>Qty.</h3></div>
                    </div>
                    <asp:Repeater runat="server" ID="rpt_Customer" OnItemDataBound="rpt_Customer_ItemDataBound" >
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a runat="server" style="text-decoration:none;" id="a_link" >
                                    <div class="Schedule"><span> <%# Eval("Name") %></span> </div>                                 
                                    <div class="Schedulenew" style="text-align:right; font-weight:600;"><%# Eval("Qty") %></div>
                                    <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("Code") %>' />
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                               
            <div id="div_Item" class="cuslist_listing" runat="server" visible="false" >
                    <div class="idbox">                    	
                        <div class="blk_1"><h3>Item Name</h3></div>
                        <div class="blk_1" style="text-align:right;"><h3>Qty.</h3></div>
                    </div>
                    <asp:Repeater runat="server" ID="Rpt_Item" OnItemDataBound="rpt_Item_ItemDataBound">  <%--OnItemDataBound="rpt_Item_ItemDataBound"--%>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a runat="server" style="text-decoration:none;" id="a_link" >
                                    <div class="Schedule"><span><%# Eval("Description") %></span></div>                                  
                                    <div class="Schedulenew" style="text-align:right;"><%# Eval("qty1") %></div>
                                    <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("itemno") %>' />
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>

                    <div id="div_consignee" class="cuslist_listing" runat="server" visible="false" >
                    <div class="idbox">                    	
                        <div class="blk_1"><h3>Name</h3></div>
                        <div class="blk_1" style="text-align:right;"><h3>Qty.</h3></div>
                    </div>
                    <asp:Repeater runat="server" ID="rpt_Consignee" OnItemDataBound="rpt_rpt_Consignee_ItemDataBound">  <%--OnItemDataBound="rpt_Item_ItemDataBound"--%>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a runat="server" style="text-decoration:none;" id="a_link" >
                                    <div class="Schedule"><span></span> <%# Eval("Name") %></span> </div>                                    
                                    <div class="Schedulenew" style="text-align:right;"><%# Eval("Qty") %></div>
                                    <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("Code") %>' />
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
                     </div>

                </div>
            <br /><asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
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
