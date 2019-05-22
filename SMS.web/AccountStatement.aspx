<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountStatement.aspx.cs" Inherits="AccountStatement" %>

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

    <script type="text/javascript">
        $.fn.digits = function () {
            return this.each(function () {
                $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
        $("span.numbers").digits();
    </script>
</head>

<body>
    <form runat="server" id="frm_Customer">
        <main>
            
        <header>
            <div class="wrap">
                <a href="DashBoard.aspx"><img src="images/logo.png"  class="inner-logo"/></a>
                <div class="inner_logo">
                       <a >Account Statement <br /> <asp:Label runat="server" CssClass="lblUser1" ID="lblUserName"></asp:Label></a>    
                </div>
                 <div class="logout-btn"><a class="bt_Logout1" href="Login.aspx"  onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>
            </div>
        </header>
       <div id="middle" class="tz-middle-pad">        
        <div class="wrap">
            <div class="cuslist">
                    <div class="block_1">
                        <div class="cuslist_listing">        	                
                             <div class="idbox">
                             <div class="blk_0" style="text-align:center;"><h3>Account Statement</h3></div>
                             </div>
            	<ul>                    
                     <li>
                        <a style="text-decoration:none;" href="ActCustomerOverDue.aspx">                        	
                            <div class="blk_0">Overdue <span class="icon"> (<i class="fa fa-inr" aria-hidden="true"></i>)&nbsp;<asp:Label ID="lbl_Over_Due" CssClass="numbers"   runat="server" ></asp:Label></span></div>                        	
                            
                        </a>
                     </li>
                	<li>
                       <a style="text-decoration:none;" href="ActCustomerDueInNext7Days.aspx">
                        	<div class="blk_0">Due in next 7 Days <span class="icon">(<i class="fa fa-inr" aria-hidden="true"></i>)&nbsp;<asp:Label ID="lbl_Due_Next7Days"   runat="server" ></asp:Label></span> </div>                        	
                            
                       </a>
                    </li>
                   
                    <li>
                        <a style="text-decoration:none;" href="ActCustomerTotalOutStanding.aspx">
                        	<div class="blk_0">Total Outstanding <span class="icon">(<i class="fa fa-inr" aria-hidden="true"></i>)&nbsp;<asp:Label ID="lbl_Total_Outstanding"  runat="server" ></asp:Label></span></div>                        	 
                        </a>
                    </li>
                    <li>
                        <a style="text-decoration:none;" href="ActCustomerAccountStatement.aspx">
                        	<div class="blk_0">Account Statement </div>                        	
                        </a>
                    </li>
                    
                </ul>
                     </div>
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
