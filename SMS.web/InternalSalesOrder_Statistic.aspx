<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternalSalesOrder_Statistic.aspx.cs" Inherits="InternalSalesOrder_Statistic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
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
    <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#tb_Discount").keyup(function (event) {

                if (event.keyCode == 13) {
                    $("#tb_Discount").click();
                }
            });
        });
    </script>
    <script type="text/javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

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

        /*body{
background:#d0d0d5;
}*/
        /*  Basic stucture
=====================*/
        #accordion {
            margin: 50px auto;
        }

            #accordion ul {
                list-style: none;
                margin: 0;
                padding: 0;
            }

        .accordion {
            display: none;
        }

            .accordion:target {
                display: block;
            }

        #accordion ul li a {
            text-decoration: none;
            display: block;
            padding: 10px;
            visibility: visible;
        }

        .accordion {
            padding: 4px;
        }

        /*  Colors 
====================*/
        #accordion ul {
            -webkit-box-shadow: 0 4px 10px #BDBDBD;
            -moz-box-shadow: 0 4px 10px #BDBDBD;
            box-shadow: 0 4px 10px #BDBDBD;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

            #accordion ul li a {
                background: #fff;
                border-bottom: 1px solid Black;
                color: #000000;
            }

        .accordion {
            background: #fdfdfd;
            color: #000000;
        }

            .accordion:target {
                border-top: 3px solid Black;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        <div>
            <div id="accordion" style="margin-top: -1%; height: 115px;">
                <ul>
                    <li>
                        <a href="#one" runat="server" id="one_General" style="text-decoration-color: black; vertical-align: bottom; font-weight: bold; background-color: #f5edd0; font-size: 15px">
                            <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                            <asp:Label Style="margin-left: 57%; vertical-align: bottom;" runat="server" ID="l_CustName"></asp:Label></a>
                        <div id="one" class="accordion">
                            <table width="90%" border="0" style="text-decoration-color: black;">
                                <tr>
                                    <td width="50%" align="left" valign="top" style="font-size: 14px;">&nbsp;Amount Excl.VAT :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right;" ID="lbl_Amt_Excl_VAT"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td width="30%" align="left" valign="top" style="font-size: 14px;">&nbsp;Total Excl. VAT :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right" ID="lbl_Total_Excl_VAT"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td width="29%" align="left" valign="top" style="font-size: 14px;">&nbsp;Excise Amount :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right;" ID="lbl_Excise_Amt"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td width="30%" align="left" valign="top" style="font-size: 14px;">&nbsp;Tax Amount :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right;" ID="lbl_Tax_Amt"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td width="29%" align="left" valign="top" style="font-size: 14px;">&nbsp;Charges Amount :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right;" ID="lbl_Charges_Amt"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td width="30%" align="left" valign="top" style="font-size: 14px;">&nbsp;Net Total :</td>
                                    <td width="79%" align="left" valign="top" style="font-size: 14px;">
                                        <asp:Label runat="server" Style="margin-right: 58%; float: right;" ID="lbl_Net_total"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
