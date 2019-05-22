<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentCustomers.aspx.cs" Inherits="AgentCustomers" %>

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
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
</head>
<body>
    <form runat="server" id="frm_Customer">
        <main>
    <header>
      <div class="wrap">
        <a href="DashBoard.aspx"><img class="inner-logo" src="images/logo.png" /></a>
        <div class="inner_logo"><a >Order Placement<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn"><a class="bt_Logout1"  href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>
    <div id="middle" class="tz-middle-pad">
      <div class="wrap">
        <div class="cuslist" style="padding-top:8px;">
          <div class="block_1">
            <div class="title_1">
              <asp:TextBox runat="server" ID="tb_Search" ClientIDMode="Static" style="text-transform:uppercase;" CssClass="textbox_1" PlaceHolder="Customer Info" OnTextChanged="tb_Search_TextChanged"   Visible="true"></asp:TextBox>
              <asp:Button runat="server" ID="btn_Search" OnClick="btn_Search_Click" Visible="true" CssClass="search_button" />
            </div>
            <div class="cuslist_listing">
            <div class="idbox">
              <h3>Customer</h3>
            </div>           
            <asp:Repeater runat="server" ID="rpt_Customer" OnItemDataBound="rpt_Customer_ItemDataBound">
              <HeaderTemplate>
                <ul>
              </HeaderTemplate>
              <ItemTemplate>
                <li> <a runat="server" style="text-decoration:none;" id="a_link" ><div class="blk12a">
                  <div class="Cust"><%# Eval("Name") %> </div>
                  <small> <%# Eval("CustomerNo") %></small>
                  <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("CustomerNo") %>' />
                  </div></a> </li>
              </ItemTemplate>
              <FooterTemplate>
                </ul>
                </div>
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
      <div class="fl_icon"><a href="AgentCompany.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
      <div class="wrap"> &nbsp;</div>
    </footer>
  </main>
    </form>
</body>
</html>
