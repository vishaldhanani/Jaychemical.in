<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCategory.aspx.cs" Inherits="ProductCategory" %>

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
     <link rel="shortcut icon" type="image/x-icon" href="images/JayChem.jpg" />
</head>
<body>
    <form runat="server" id="frm_ProductCat">
        <main>
        <header>
            <div class="wrap">
                <div class="inner_logo"><a href="#"><img src="images/logo.png" /></a></div>
            </div>
        </header>
        <div id="middle">
            <div class="wrap">
                <asp:Label runat="server" ID="l_Error" Visible="false" ForeColor="Red"></asp:Label>
        	    <div class="tab">
                    <asp:Repeater runat="server" ID="rpt_ProductCat">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                            <a href='ProductItem.aspx?Code=<%# Eval("Code") %>'>
                        	    <div class="tab_name"><%# Eval("Name") %></div>
                                <asp:HiddenField runat="server" ID="hf_CustNo" Value='<%# Eval("Code") %>' />
                        	    <div class="icon"><img src="images/arrow.png" /></div>
                            </a>
                        </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <footer>
    	    <div class="wrap">&nbsp;</div>
        </footer>
    </main>
    </form>
</body>
</html>
