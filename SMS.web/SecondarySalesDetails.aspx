<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondarySalesDetails.aspx.cs" Inherits="SecondarySalesDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= Constant.Title %></title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/html5.js"></script>
    <link href="css/media.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    
    <link href="SearchScript/chosen.css" rel="stylesheet" />
    <script src="SearchScript/chosen.jquery.min.js"></script>
    <link href="SearchScript/chosen.min.css" rel="stylesheet" />
    <script src="SearchScript/chosen.proto.js"></script>
    <script src="SearchScript/chosen.proto.min.js"></script>
    <link href="SearchScript/select2-bootstrap.css" rel="stylesheet" />

    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.min.css" rel="stylesheet" />

    <style type="text/css">
        .Grid3left {
            font-size: 12px;
            text-align: left;
            width: 100%;
            /*font-weight: 900;*/
            opacity: 1;
            font-weight:normal;
            text-transform: uppercase;
        }

        .Grid3Right {
            font-size: 12px;
            text-align: right;
            width: 100%;
            /*font-weight: 900;*/
            opacity: 1;
            text-transform: uppercase;
        }

        .Grid3Center {
            font-size: 12px;
            text-align: center;
            width: 100%;
            opacity: 1;
            text-transform: uppercase;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        .textsize {
            font-size: 15px;
            padding-top: -3px;
        }

        .Addressfont {
            font-size: 14px;
        }

        .textmode {
            font-size: 15px;
            text-align: left;
            color: blue;
        }
    </style>

    <script type="text/javascript">
        document.onkeypress = function noNumbers2(e) {
            e = e || window.event;
            var keynum = e.keyCode || e.which;
            if (keynum == 27) {
                AJS.AEV(document, "keypress", GB_hide);
            }
        }
    </script>

    <script type="text/javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {   
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>
   
    <%--<link href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" rel="stylesheet" />--%>   
    <%--<script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();                          
        }
    </script>
     --%>
</head>

<body>
    <form id="form1" runat="server">  
        
        <%--<div class="container">
        <div class="row">
            <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                data-toggle="modal"  data-target="#myModal">
                Launch demo modal
            </button>          
            <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
            <!-- Include all compiled plugins (below), or include individual files as needed -->
            <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
            <div class="modal fade" id="myModal" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">
                                Validation Message</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMessage" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                OK</button>
                            
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->
        </div>
    </div>--%>
         
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <main> 
       <header>      
      <a href="DashBoard.aspx" ><img class="inner-logo" style="padding-left:15px;"  src="images/logo.png" /></a>        
        <div class="inner_logo"><a>Secondary Sales<br /> <asp:Label runat="server" style="margin-top:10px; vertical-align:top; font-size:10px;"  ID="lblUserName"></asp:Label></a> </div>
        <div class="logout-btn" style="margin-right:2px;"><a style="margin-top:-18px;" href="Login.aspx" onclick="return confirm('Are you sure to logout?');" ><i class="fa fa-2x fa-sign-out" aria-hidden="true"></i></a></div>      
    </header>                     

    <div style="margin-top:62px; text-align:center;"> 
       <asp:Label runat="server" ID="lb_CompanyName" Text="" style="text-align:center;" ></asp:Label>                             
    </div>

        <div style="padding-left:10px; padding-right:10px;">                   
        <div class="container">           
               <table border="0" width="100%">
                   <tr>
    <asp:gridview ID="Gridview3" runat="server"  Width="100%" AutoGenerateColumns="false"    EmptyDataText="No Data Found." OnRowDataBound="Gridview3_RowDataBound"  BorderWidth="1px">
        <Columns>                    
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="No."   ItemStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"  ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true">
            <ItemTemplate>               
                 <asp:TextBox ID="lb_CustomerName" runat="server" Text='<%# Eval("CustomerName") %>' Placeholder="Customer Name" CssClass="Grid3left form-control"> 
                 </asp:TextBox>                                 
            </ItemTemplate> 
        </asp:TemplateField>  
        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true">
            <ItemTemplate>               
                 <asp:TextBox ID="lb_Location" Placeholder="Location" runat="server" Text='<%# Eval("Location") %>' CssClass="Grid3left form-control"> 
                 </asp:TextBox>                                 
            </ItemTemplate> 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true">
            <ItemTemplate>               
                 <asp:DropDownList ID="ddl_ItemList" runat="server"  CssClass="Grid3left form-control chosen-select"    AutoPostBack="true"    OnSelectedIndexChanged="ddl_ItemList_SelectedIndexChanged"> 
                </asp:DropDownList>              
                <asp:HiddenField ID="hf_ItemNo" runat="server" Value="" />                             
            </ItemTemplate> 
        </asp:TemplateField>          
        <asp:TemplateField HeaderText="Product Category"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:TextBox ID="lb_ProductCategory" CssClass="Grid3Center form-control" ReadOnly="true" Text='<%# Eval("ProductCategory") %>'  runat="server"></asp:TextBox>           
            </ItemTemplate>         
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Range"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="9%" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:TextBox ID="lb_Range" CssClass="Grid3Center form-control" Text='<%# Eval("Range") %>' ReadOnly="true"  runat="server"></asp:TextBox>           
            </ItemTemplate>         
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Month"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlMonth"  CssClass="Grid3Center form-control chosen-select"  AutoPostBack="true" OnTextChanged="ddlMonth_TextChanged"    runat="server" >
                        <asp:ListItem Text="-Select Month-"  Value="0"></asp:ListItem>
                        <asp:ListItem Text="January"  Value="1"></asp:ListItem>
                        <asp:ListItem Text="February"  Value="2"></asp:ListItem>
                        <asp:ListItem Text="March"  Value="3"></asp:ListItem>
                        <asp:ListItem Text="April"  Value="4"></asp:ListItem>
                        <asp:ListItem Text="May"  Value="5"></asp:ListItem>
                        <asp:ListItem Text="June"  Value="6"></asp:ListItem>
                        <asp:ListItem Text="July"  Value="7"></asp:ListItem>
                        <asp:ListItem Text="August"  Value="8"></asp:ListItem>
                        <asp:ListItem Text="September"  Value="9"></asp:ListItem>
                        <asp:ListItem Text="October"  Value="10"></asp:ListItem>
                        <asp:ListItem Text="November"  Value="11"></asp:ListItem>
                        <asp:ListItem Text="December"  Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity"   ItemStyle-HorizontalAlign="Right"  FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="95px">
            <ItemTemplate>
                 <asp:TextBox ID="lb_Quantity"   CssClass="Grid3Center"  AutoPostBack="true" Text='<%#  Eval("Quantity") %>' OnTextChanged="lb_Quantity_TextChanged"   onkeyup="checkDec(this);"  runat="server" ></asp:TextBox>
            </ItemTemplate>                      
        </asp:TemplateField>

       <%-- <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="8%">
            <ItemTemplate> 
                 <asp:TextBox ID="lb_Date"  CssClass="Grid3Center disable_future_dates" AutoPostBack="true" OnTextChanged="lb_Date_TextChanged"   runat="server" Text='<%# Eval("Date") %>'></asp:TextBox>             
                 <ajax:CalendarExtender ID="CalendarExtender1" CssClass= " cal_Theme1"  runat="server" BehaviorID="calendar1"    Format="dd/MM/yyyy" ClientIDMode="Static" OnClientShown="calendarShown" DefaultView="Months" TargetControlID="lb_Date"  >
                 </ajax:CalendarExtender>                             
            </ItemTemplate>            
        </asp:TemplateField>--%>
            
                   
         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                 <asp:ImageButton ID="bt_Edit" runat="server" Style="text-align:center;" CommandArgument='<%# Container.DataItemIndex + 1 %>' OnClick="bt_Edit_Click" ImageUrl="~/images/edt.jpg"  />
            </ItemTemplate>
         </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                 <span onclick="return confirm('Do you want to delete this Item ?')">
                   <asp:ImageButton ID="bt_Delete" AlternateText="Delete"  OnClick="bt_Delete_Click" Style="text-align:center; " CommandArgument='<%# Container.DataItemIndex + 1 %>' ImageUrl="~/images/del.png" runat="server"  CommandName="DeleteRow" />                             
                 </span> 
              </ItemTemplate>                    
             </asp:TemplateField>
        </Columns>          
            <HeaderStyle BackColor="#8A0886" Font-Size="13px" Height="26px" Font-Bold="True" ForeColor="White"></HeaderStyle>
            <PagerStyle  ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" BorderWidth="1" Font-Size="9px"  Height="12px" ForeColor="Black" BorderColor="Black" />        
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:gridview>
               </tr>
              </table>            

        </div>                                                                     	                              
        </div>          
        <div>
            <table runat="server" border="0">
                <tr style="height: 20px;">
                    <td></td>
                    <td></td>
                </tr>
                <tr style="height: 20px;">
                    <td></td>
                    <td></td>
                </tr>
                <tr style="height: 20px;">
                    <td></td>
                    <td></td>
                </tr>
                <tr style="height: 20px;">
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <footer>
            <div class="fl_icon">
            </div>
            <div class="wrap">
                <div class="links">
                    <asp:LinkButton ID="ButtonAdd" runat="server" class="u_f_btn12" Text="Add Lines" OnClick="ButtonAdd_Click"></asp:LinkButton>
                    <asp:LinkButton ID="btn_Submit" runat="server" CausesValidation="true" OnClientClick="javascript: return validateCustRefNo();" ValidationGroup="gridview" Style="text-decoration: none;" Text="Send Email" class="u_f_btn" OnClick="btn_Submit_Click" />                  
                </div>
            </div>
        </footer>

        </main>
    </form>
    <script type="text/javascript">
        function ShowLoading(e) {
            var div = document.createElement('div');
            var img = document.createElement('img');
            img.src = 'http://www.oppenheim.com.au/wp-content/uploads/2007/08/ajax-loader-1.gif';
            div.innerHTML = "Loading...<br />";
            div.style.cssText = 'position: fixed; top: 30%; left: 40%; z-index: 5000; width: 222px; text-align: center; background: #fff; border: 1px solid #000';
            div.appendChild(img);
            document.body.appendChild(div);

            // These 2 lines cancel form submission, so only use if needed.
            window.event.cancelBubble = true;
            e.stopPropagation();
        }
    </script>
    
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js" type="text/javascript"></script>

</body>
<script src="SearchScript/jquery.min.js"></script>
<script src="SearchScript/chosen.jquery.js"></script>
<script type="text/javascript">
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
</script>
</html>

