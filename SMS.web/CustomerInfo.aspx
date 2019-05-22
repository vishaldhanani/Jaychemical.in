<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerInfo.aspx.cs" Inherits="CustomerInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script src="js/AutoComplete.js" type="text/javascript"></script>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
<link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
</head>

<body>
    <form runat="server" id="frm_Compny">
        <main>
    <header>
      <div class="wrap">
      <a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>        
        <div class="inner_logo"><a >Order Placement<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>    
    <div id="middle" class="tz-middle-pad">
      <div class="wrap">
        <div class="custo_info">
          <div class="custo_info">
            <div class="block_1">
            <h3>Customer Info</h3>
              <div class="title_1">
                <div class="idbox">
                  <div class="blk_1"><b>Code :</b>
                    <asp:Label runat="server" ID="l_CustCode" ></asp:Label>
                    <br/>
                    <b> Customer :</b>
                    <asp:Label runat="server" ID="l_Name"></asp:Label>
                    <br />
                    <b> Address : </b>
                    <asp:Label runat="server" ID="l_Address"></asp:Label>
                    <asp:Label runat="server" ID="l_Address2"></asp:Label>
                    <asp:Label runat="server" ID="l_City"></asp:Label>
                  </div>
                </div>
              </div>
            </div>
            <div class="block_1">
           <h3> Consignee Info&nbsp;&nbsp; <asp:Button runat="server" href="#" ID="btn_AddConsignee" OnClick="btn_AddConsignee_Click" style="height:25px;font-size:15px;  background-color:#8A0886; color:white; "   Text="Change Consignee"/></h3>                              
              <div class="title_1">
                <div class="idbox">
                  <div class="blk_1"><b>Code :</b>
                    <asp:Label runat="server" ID="l_CsCustCode" ></asp:Label>
                    <br />
                    <b>Consignee :</b>
                    <asp:Label runat="server" ID="l_CsName" ></asp:Label>
                    <br />
                    <b>Address :</b>
                    <asp:Label runat="server" ID="l_CsAddress"></asp:Label>
                    <asp:Label runat="server" ID="l_CsAddress2"></asp:Label>
                    <asp:Label runat="server" ID="l_CsCity"></asp:Label>
                  </div>
                </div>
              </div>
              <div class="idbox"> <a ID="btnNewConsignee" runat="server" visible="false" href="NewConsignee.aspx" class="addtocart" style="height:25px;  font-size:10px; padding-top:7px;" >New Consignee</a> </div>
              <br />
              <asp:Label runat="server" ID="l_Error" Visible="false" Text="test" ForeColor="Red"></asp:Label>
            </div>
          </div>
        </div>
      </div>
    </div>
    <footer>
      <div class="fl_icon">
        <asp:LinkButton runat="server" ID="btn_back" OnClick="btn_back_Click"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></asp:LinkButton>
      </div>
      <div class="wrap">
        <div class="nextbtn"><a href="ProductItemNew.aspx">Product Selection <i class="fa fa-lg fa-angle-double-right" aria-hidden="true"></i></a></div>
      </div>
    </footer>
  </main>
    </form>
</body>
</html>
