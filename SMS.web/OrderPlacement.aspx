<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPlacement.aspx.cs" Inherits="OrderPlacement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.fancybox.css" media="screen" />
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script src="js/Common.js"></script>
    <script type="text/javascript" src="js/FancyBoxStyle.js"></script>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>
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
            height: 400px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">
        // added new method for Preventing Multiple Button Click - Raj Shah 07/07/2016
        var isSubmitted = false;
        function preventMultipleSubmissions() {
            if (!isSubmitted) {
                $('#<%=btn_Submit.ClientID %>').val('Submitting.. Plz Wait..');
                isSubmitted = true;
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        // added new method for Preventing Multiple Button Click - Raj Shah 07/07/2016
        function preventMultipleSubmissions() {
            $('#<%=btn_Submit.ClientID %>').prop('disabled', true);
     }
     var inputs = document.getElementsByTagName("INPUT");
     for (var i in inputs) {
         if (inputs[i].type == "button" || inputs[i].type == "submit") {
             inputs[i].disabled = true;
         }
     }
     window.onbeforeunload = preventMultipleSubmissions;
    </script>

    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>

    <script type="text/javascript">
        function Hide(id) {
            document.getElementById(id).style.visibility = 'hidden';
            document.getElementById("btn_back").style.visibility = "hidden";

        }
    </script>

    <script type="text/javascript">

        function validateCustRefNo() {
            if (!form1.txtDelivery_Comment.value) {
                alert("Please update delivery comment as per below. \nSELF DELIVERY = SELF \nOR \nCONSIGNEE DELIVERY = CONSIGNEE \nOR \nprovide your Consignee details.");
                var textbox = document.getElementById("txtDelivery_Comment");
                textbox.focus();
                return (false);
            }
           else if (document.getElementById('cbTerms').checked == false) {
                alert("Please agree to terms and conditions.");
                return (false);

            }
            else {
                document.getElementById("btn_Submit").style.visibility = "hidden";
                document.getElementById("btn_back").style.visibility = "hidden";
                return (true);
            }
        }
    </script>

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

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <main>          
    <header>       
      <div class="wrap">
        <a href="DashBoard.aspx"><img class="inner-logo"  src="images/logo.png" /></a>
        <div class="inner_logo"><a>Order Placement<br /> <asp:Label runat="server" style="margin-top:10px; vertical-align:top; font-size:10px;"  ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
      </div>
    </header>
    <div id="middle" style="padding-top:1px;">
      <div class="wrap">
        <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        <div class="addres_box" >
          <div class="odr_place_address">
            <table border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Customer :</strong></td>
                <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustName"></asp:Label></td>
              </tr>
              <tr>
                <td width="30%" height="41" align="left" valign="top" style=" font-size:13px;"><strong>Address :</strong></td>
                <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_CustAdd"></asp:Label></td>
              </tr>
              <tr style="height:10px">
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td width="30%" align="left" valign="top" style=" font-size:13px;"><strong>Consignee:</strong></td>
                <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConName"></asp:Label></td>
              </tr>
              <tr>
                <td width="30%" height="41" align="left" valign="top" style=" font-size:13px;"><strong>Address :</strong></td>
                <td width="70%" align="left" valign="top" style=" font-size:13px;"><asp:Label runat="server" ID="l_ConAdd"></asp:Label></td>
              </tr>
            </table>
          </div>
          <div class="tz-order-address">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td align="left" valign="middle"><asp:TextBox runat="server" CssClass="u_oder_fill_white_label13"  ID="txtCustomerReferenceNo" TabIndex="1" CausesValidation="true" MaxLength="40" PlaceHolder="Purchase Order No"></asp:TextBox></td>
              </tr>
              <tr style="height:10px">
                <td></td>
              </tr>
              <tr>
                <td align="left" valign="middle"><asp:TextBox runat="server"  valign="middle"  ID="txtDelivery_Comment" CssClass="u_oder_fill_white_label13" PlaceHolder="Delivery Comment"  MaxLength="800"  TabIndex="2" Height="40px"></asp:TextBox></td>
              </tr>
            </table>
          </div>
        </div>
        <div>
          <asp:Repeater runat="server" ID="rpt_FinalCart" OnItemDataBound="rpt_FinalCart_ItemDataBound">
            <HeaderTemplate>
              <table width="100%" border="1" class="odr_tbl_list1">
              <tr>
                <th width="48%" align="Left" valign="middle">PRODUCT</th>
                <th width="18%" align="center" valign="middle">QTY.</th>
                <th width="25%" align="center" valign="middle">VALUE (Excl.Tax)&nbsp;(&#x20B9;)</th>
                <th  width="16%" align="center" valign="middle">Edit<br />
                  Del.</th>
              </tr>
            
            </HeaderTemplate>
            <ItemTemplate>
              <tr>
                <td align="Left" valign="LEFT"><strong><%# Container.ItemIndex + 1 %>. &nbsp;<%# Eval("ProductName") %></strong> <br />
                  <span style="font-size:10px;"> - (<%# Eval("VariantCode") %>) </span></td>
                <td align="right" valign="top"><%# Eval("Quantity") %> <br />
                  <span style="font-size:10px;">(<%# Eval("UOM") %>)</span></td>
                <td align="right" valign="top"><%# Convert.ToString(Eval("SellingPrice")) %></td>
                <td align="center" valign="middle"><asp:LinkButton runat="server" ID="btn_edit" CommandName="edit" OnCommand="btn_edit_Command" CommandArgument='<%#Eval("ProductCode")%>'><i class="fa fa-lg fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                  <asp:HiddenField runat="server" ID="hf_ProductCode" Value='<%# Eval("ProductCode") %>' />
                  <asp:HiddenField runat="server" ID="hf_VariantCode" Value='<%# Eval("VariantCode") %>' />
                  <asp:HiddenField runat="server" ID="hdn_Nofield" Value='<%# Eval("noField") %>' />
                  <asp:LinkButton runat="server" ID="btn_delete" CommandName="delete" OnClientClick="return confirm('Do you want to delete this Item ?');" OnCommand="btn_delete_Command" CommandArgument='<%#Eval("ProductCode")%>'><i class="fa fa-lg fa-times" aria-hidden="true"></i></asp:LinkButton></td>
              </tr>
             
            </ItemTemplate>
            <FooterTemplate>
              <tr>
                <th width="39%" align="left" valign="middle">TOTAL</th>
                <th width="11%" align="right" valign="middle"><asp:Label runat="server" ID="l_TotalQty"></asp:Label></th>
                <th width="15%" align="right" valign="middle"  colspan="1"><asp:Label runat="server" ID="l_TotalPrice"></asp:Label></th>
                <th width="39%" align="center" valign="middle"></th>
              </tr>
             
              </table>
            </FooterTemplate>
          </asp:Repeater>
          <br />
          <asp:CheckBox runat="server" ID="cbTerms" TabIndex="3" class="paddingClass" /> <label for="cbTerms">I agree to
          <asp:LinkButton id="lnk_Terms" runat="server" Text="Terms & Conditions."></asp:LinkButton></label>
        </div>
      </div>
    </div>
    <footer>
      <div class="fl_icon" ><a id="btn_back" runat="server" href="ProductItemNew.aspx"><i class="fa fa-2x fa-arrow-circle-o-left" aria-hidden="true"></i></a></div>
      <div class="wrap"> 
      <div class="links">
        <asp:LinkButton ID="btn_Submit" runat="server" CausesValidation="true" TabIndex="4" OnClientClick="javascript: return validateCustRefNo();  return preventMultipleSubmissions();"  ValidationGroup="Save"  Text="SUBMIT" class="u_f_btn" OnClick="btn_Submit_Click" />
      </div>
      </div>
    </footer>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="lnk_Terms" CancelControlID="Button2" BackgroundCssClass="Background"> </cc1:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
      <iframe style=" width: 290px; height: 370px;" id="irm1" src="TermsAndConditions.html" runat="server"></iframe>
      <br/>
      <asp:Button ID="Button2" runat="server" Text="Close" />
      </asp:Panel>
  </main>
    </form>
</body>
</html>
