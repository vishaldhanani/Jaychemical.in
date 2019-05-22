<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductItemNew.aspx.cs" Inherits="ProductItemNew" %>

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
    <script type="text/javascript" src="js/BindProducts_1.js"></script>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />

    <script type="text/javascript">
        function DisableImg(index) {
            var conID = ("#dv_" + index);
            document.getElementById(conID).style.display = "none";
            document.getElementById(conID).disabled = true;
            $(conID).addClass('disabled');
        }
    </script>
    
</head>
<body>
    <form runat="server" id="frm_Customer">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <main> 


    <div id="nav" class="container"><a class="menu_res" href="#"> <span></span> <span></span> <span></span></a>
      <div class="nav_inner" >
        <asp:Repeater runat="server" ID="rpt_ProductCat">
          <HeaderTemplate>
            <ul>
          </HeaderTemplate>
          <ItemTemplate>
            <li><a>
              <asp:LinkButton ID="lb_Categories"  runat="server" OnCommand="lb_Categories_Command" CommandArgument='<%# Eval("Code") %>'> <%# Eval("Name") %>    </asp:LinkButton>                            
               </a>                
            </li>                                        
          </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>          
        </asp:Repeater>                       
      </div>
    </div>
         
    <header>
      <div class="wrap">
        <div class="Header">
            
          <%--<div class="home_icon_while_has_slide_menu"></div>--%>
          <div class="cart_icon" >
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="CartCount_Click" OnClientClick="javascript:validationAddtoCart();"  ><img height="128" width="128" src="images/cart_icon.png"></asp:LinkButton>
            <div class="item_count" >
              <asp:Label runat="server"  ID="l_CartCount"></asp:Label>
            </div>
          </div>
          <div class="inner_logo"><a >Order Placement <br /> <asp:Label runat="server" style="margin-top:-10px; vertical-align:bottom; font-size:10px;" ID="lblUserName"></asp:Label></a> </div>
          <div class="logout-btn"><a style="margin-top:-11px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
        </div>
      </div>
    </header>
    <div id="middle" class="tz-middle-pad">
      <div class="wrap">
        <div class="cuslist tz-cust-lists" style="padding-top:8px;">
          <div class="block_1">
            <div class="title_1" style="text-transform:uppercase;" >
              <asp:TextBox runat="server" ID="tb_Search" Visible="true" Style="text-transform:uppercase;" CssClass="textbox_1" OnTextChanged="tb_Search_TextChanged1"  Placeholder="Product Info" ></asp:TextBox>
              <asp:Button runat="server" ID="btn_SearchProduct" OnClick="btn_SearchProduct_Click" Visible="true" CssClass="search_button"></asp:Button>
            </div>
            <div class="cuslist_listing">             
              <asp:Repeater runat="server" ID="rpt_Item" OnItemDataBound="rpt_Item_ItemDataBound" >
                <HeaderTemplate>
                  <table width="100%"  border="1" class="odr_tbl_list_ProductItem">
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                   <td  valign="middle" class="product-name" width="75%" style="border-right-style:hidden; text-decoration:none;"><label for="rpt_Item_cb_Products_<%# ((RepeaterItem)Container).ItemIndex %>" class="css-label radGroup1"></label>
                     <a href="#" style="text-decoration:none;" onclick="AddToCart('<%# Eval("MainItemNo") %>')"><h4 style="color:#111; text-decoration:none;"><%# Eval("Description") %></h4> <%--<h4><%# Eval("Description") %> </h4>--%>
                      <span id="Span1" style="text-decoration:none; font-size:12px; color:#000;"><%# Eval("ItemNo") %></span> </td>
                    <td align="right"  valign="top"  width="25%" ><a id='<%# "dv_"+Container.ItemIndex %>' onblur='DisableImg(<%# Container.ItemIndex %>)'  href="#" onclick="AddToCart('<%# Eval("MainItemNo") %>')"><i class="fa fa-2x fa-arrow-circle-o-right" aria-hidden="true"></i></a></td>
                    <asp:Label runat="server" ID="l_ProName" Visible="false" Text='<%# Eval("Description") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Variant" Visible="false" Text='<%# Eval("VariantCode") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_UOM" Visible="false" Text='<%# Eval("UOMCode") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Price" Visible="false" Text='<%# Convert.ToDecimal(Eval("UnitPrice")).ToString("0") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Discount" Visible="false" Text='<%# Convert.ToDecimal(Eval("DicPercentage")).ToString("0") %>'></asp:Label>
                  </tr>
                </ItemTemplate>
                <FooterTemplate>
                  </table>
                  <div style="text-align:center;"><img id="loader" alt="" src="images/loader-color.gif" style="display:none;" /></div>
                </FooterTemplate>
              </asp:Repeater>             
              <asp:Repeater runat="server" ID="rpt_Item_BasedOnDefaultItemCategory" OnItemDataBound="rpt_Item_BasedOnDefaultItemCategory_ItemDataBound">
                <HeaderTemplate>
                  <table width="100%"  border="1" class="odr_tbl_list_ProductItem">
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td align="left" valign="middle"  width="75%" style="border-right-style:hidden; text-decoration:none;"><label for="rpt_Item_cb_Products_<%# ((RepeaterItem)Container).ItemIndex %>" class="css-label radGroup1"></label>
                     <%-- <h4><%# Eval("Description") %></h4>--%>
                         <a href="#" style="text-decoration:none;" onclick="AddToCart_ByItemCategory('<%# Eval("MainItemNo") %>')"><h4 style="color:#111;text-decoration:none; "><%# Eval("Description") %></h4>
                      <span id="Span1" style="text-decoration:none; font-size:12px; color:#000;"><%# Eval("ItemNo") %></span> </td>
                    <td align="right"  valign="top"  width="25%" ><a id='<%# "dv_"+Container.ItemIndex %>' onblur='DisableImg(<%# Container.ItemIndex %>)' href="#" onclick="AddToCart_ByItemCategory('<%# Eval("MainItemNo") %>')"><i class="fa fa-2x fa-arrow-circle-o-right" aria-hidden="true"></i></a></td>
                    <asp:Label runat="server" ID="l_ProName" Visible="false" Text='<%# Eval("Description") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Variant" Visible="false" Text='<%# Eval("VariantCode") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_UOM" Visible="false" Text='<%# Eval("UOMCode") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Price" Visible="false" Text='<%# Convert.ToDecimal(Eval("UnitPrice")).ToString("0") %>'></asp:Label>
                    <asp:Label runat="server" ID="l_Discount" Visible="false" Text='<%# Convert.ToDecimal(Eval("DicPercentage")).ToString("0") %>'></asp:Label>
                  </tr>
                </ItemTemplate>
                <FooterTemplate>
                  </table>
                  <div style="text-align:center;"><img id="loader" alt="" src="images/loader-color.gif" style="display:none;" /></div>
                </FooterTemplate>
              </asp:Repeater>
            </div>
            <br />
            <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
          </div>
        </div>
      </div>
    </div>
    <footer>
      <div class="fl_icon"><a href="CustomerInfo.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
      <div class="wrap">
        <div class="links" style="display:none;">
          <div style="background-image:url('images/icon_1.png'); ">
            <asp:CheckBox ID="cb_Black" runat="server" AutoPostBack="true"  OnCheckedChanged="cb_Color_CheckedChanged" />
            <br />
            <label for="cb_Black">BLK</label>
          </div>
          <div style="background-image:url('images/icon_2.png'); ">
            <asp:CheckBox ID="cb_Blue" runat="server" AutoPostBack="true"  OnCheckedChanged="cb_Color_CheckedChanged" />
            <br />
            <label for="cb_Blue">BLU</label>
          </div>
          <div style="background-image:url('images/icon_3.png'); ">
            <asp:CheckBox ID="cb_RedYellow" AutoPostBack="true"  runat="server" OnCheckedChanged="cb_Color_CheckedChanged" />
            <br />
            <label for="cb_RedYellow">R/Y</label>
          </div>
          <div style="background-image:url('images/icon_4.png'); ">
            <asp:CheckBox ID="cb_Other" AutoPostBack="true"  runat="server" OnCheckedChanged="cb_Color_CheckedChanged" />
            <br />
            <label for="cb_Other">OTR</label>
          </div>
        </div>
      </div>
    </footer>
  </main>

    </form>
</body>
</html>
