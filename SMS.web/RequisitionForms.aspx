<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionForms.aspx.cs" Inherits="RequisitionForms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <script type="text/javascript" src="../js/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <%--<script type="text/javascript" src="js/FancyBoxStyle.js"></script>--%>
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />

    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                $addHandler(document, "keydown", onKeyDown);
            }
        }

        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mp1').hide();

            }
        }
    </script>
    <script type="text/javascript">

        $('.box').magnificPopup({
            //$("[class=box]").magnificPopup({
            type: 'inline',
            modal: false,

        });

        $(document).on('click', '.closePopup', function (e) {
            e.preventDefault();
            $.magnificPopup.close();
        });


    </script>
    <style type="text/css">
        .box {
            display: none;
            width: 100%;
            flex-align: center;
            margin-top: -5px;
        }

            a:hover + .box, .box:hover {
                display: block;
                position: absolute;
                z-index: 100;
            }
    </style>
    <script type="text/javascript">
        function close_window() {
            //netscape.security.PrivilegeManager.enablePrivilege("UniversalBrowserWrite");
            if (confirm("Are you sure to logout?")) {
                top.parent.window.close();
                this.parent.window.close();
            }
        }
        //function close_window() {        
        //if (confirm("Are you sure to logout?")) {

        //    self.close();
        //    top.Window.close();
        //    this.close();
        //}
        //}



    </script>
</head>
<body style="overflow: hidden; position: relative;">
    <form runat="server" id="frm_dashboard" method="get">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <main>
    <header>     
      <div class="wrap"> 
          <a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>
        <div class="inner_logo"><a >Dashboard <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn" >
            <a href="Login.aspx" class="bt_Logout1"  onclick="return confirm('Are you sure to logout?');" ><i  class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>
    <div id="middle" style="margin-top:70px;">
      <%--<a runat="server" id="Lnk_Info" href="#" class="Information"  style="margin-right:10%; font-weight:bold;  vertical-align:bottom; color:black; text-align:left;" >?</a>--%>
      <%--<div class="box" style="margin-top:-25px;"><iframe  src="PDF/K2_CLiCK_User_Manual.pdf" align="right"  width = "380px" height = "405px"></iframe></div>--%>
      <div class="wrap"  >
        <div class="cuslist">
          <div class="block_1" >
            <div id="div_Dashboard" class="cuslist_listing" runat="server" >          
            <asp:Repeater runat="server"  ID="rpt_Dashboard" OnItemDataBound="rpt_Dashboard_ItemDataBound" >
              <HeaderTemplate>
                <ul>
              </HeaderTemplate>
              <ItemTemplate>
                <li> <a runat="server" style="text-decoration:none;"  id="a_link">
                  <div class="blk_0"><%# Eval("DynamicMenu") %> <i class="fa fa-2x fa-arrow-circle-o-right" aria-hidden="true"></i> </div>
                  </a> </li>
              </ItemTemplate>
              <FooterTemplate>
                </ul>
                </div>
              </FooterTemplate>
            </asp:Repeater>

          </div>
        </div>
      </div>
    </div>
    </div>
    <footer>
      <div style="font-size:18px; color:#FFF; text-align:center; vertical-align:middle; margin-top:15px; font-style:italic; ">We are just one click away</div>
    </footer>
    <%--<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Lnk_Info" CancelControlID="Button2" BackgroundCssClass="Background">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
<iframe style=" width: 295px; height: 375px;" id="irm1" src="PDF/K2_CLiCK_User_Manual.pdf" runat="server"></iframe>
<br/>
<asp:Button ID="Button2" runat="server" Text="Close" />
</asp:Panel>--%>
  </main>
    </form>
</body>
</html>
