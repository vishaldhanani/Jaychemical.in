<%@ Page Language="C#"  Title="" CodeFile="AgentCompanySecondarySales.aspx.cs" Inherits="AgentCompanySecondarySales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1">
<title><%= Constant.Title %></title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="js/html5.js"></script>
<link href="css/media.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
<link href="css/font-awesome.min.css" rel="stylesheet" />
   
</head>
<body>
<form runat="server" id="frm_Compny">
  <main>   
    <header>
      <div class="wrap">
      <a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>        
        <div class="inner_logo"><a >Secondary Sales<br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn"><a class="bt_Logout1"  href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>
 
    <div  class="tz-middle-pad"  >  
      <div class="wrap" style="padding-top:60px;">
        <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        <div class="cuslist" >
          <div class="block_1" >            
            <h3>Agent Companies</h3>
            <asp:Repeater runat="server" ID="rpt_Company"  OnItemDataBound="rpt_Company_ItemDataBound" >
              <HeaderTemplate>
                <ul>
              </HeaderTemplate>
              <ItemTemplate>
                <li > <a id="a_link" style="text-decoration:none;" runat="server" >
                  <div class="blk12a"><%# Eval("Name") %></div>
                  <asp:HiddenField runat="server" ID="hf_SubType" Value='<%# Eval("AgentSubType") %>' />
                  </a> </li>
              </ItemTemplate>
              <FooterTemplate>
                </ul>
              </FooterTemplate>
            </asp:Repeater>
          </div>
        </div>
      </div>
    </div>

    <footer>
      <div class="fl_icon"><a href="DashBoard.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
      <div class="wrap">
      <div class="links">
        <%--<asp:LinkButton runat="server" ID="lb_Finish" CssClass="u_f_btn"  Text="Skip" TabIndex="5" ></asp:LinkButton>--%>
      </div>
      </div>
    </footer>
  </main>
</form>
</body>
</html>
