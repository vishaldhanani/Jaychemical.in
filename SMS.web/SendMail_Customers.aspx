<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail_Customers.aspx.cs" Culture="en-GB" Inherits="SendMail_Customers" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1258px;
        }
    </style>

    <script type="text/javascript">                
        function CompareDate() {
           
            var dateOne = new Date(document.getElementById("txtStartDate").value); //Year, Month, Date
            var dateTwo = new Date(document.getElementById("txtEndDate").value); //Year, Month, Date
            if (dateOne > dateTwo) {
                alert("End Date should not be less then Start Date.");
                return false;
            }
            else {
                
                return true;
            }
        }

    </script>  
</head>
<body>
    <form id="form1" runat="server">


         <ajax:ToolkitScriptManager ID="toolkit1" runat="server"></ajax:ToolkitScriptManager>
        <div id="tblDate" runat="server">
        <table style="width: 100%;" id="tblDate">
            <tr>
                <td colspan="2" style="text-align: center">Period for Statement</td>
            </tr>
            <tr>
                <td>Start Date:</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Placeholder="dd/mm/yyyy"></asp:TextBox> 
                   
                     <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="dd/MM/yyyy" runat="server">
                        </ajax:CalendarExtender>  
                    <asp:RequiredFieldValidator ID="rqfval1" runat="server"  ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"  ControlToValidate="txtStartDate" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                    
                    
                </td>
            </tr>
            <tr>
                <td>End Date:</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Placeholder="dd/mm/yyyy"></asp:TextBox>
                    
                    <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="dd/MM/yyyy"  runat="server" />
                    <asp:RequiredFieldValidator ID="rqfval2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtEndDate" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                    
                    

                </td>
            </tr>
            <tr>
                <td>Mail ID:</td>
                <td>
                    <asp:TextBox ID="txtMail" runat="server"  Placeholder="abc@abc.com"></asp:TextBox>
                    
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtMail" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgvexpressvalidator" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"  ControlToValidate="txtMail" SetFocusOnError="true"  ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$"></asp:RegularExpressionValidator>
                    

                </td>
            </tr>
             <tr>
                <td>Optional MailID:</td>
                <td>
                    <asp:TextBox ID="txtMail1" runat="server" Placeholder="abc@abc.com"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"  ControlToValidate="txtMail" SetFocusOnError="true"  ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnmail" runat="server"  Text="PDF Mail" OnClick="btnmail_Click"   CausesValidation="true" ValidationGroup="Save"  />&nbsp;
                    <asp:Button ID="btnExcel_Mail" runat="server"  Text="Excel Mail" OnClick="btnExcel_Mail_Click"  CausesValidation="true"  ValidationGroup="Save" />&nbsp;            
                      <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />&nbsp;

                </td>
            </tr>
        </table>
            </div>


        <div id="tblMessage" runat="server" visible="false">
    <table style="width: 100%;" align="center" id="tblmail" >
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">
    <asp:Label style="text-align:justify; font-weight:bold;" ID="lblmessage" runat="server" Text="Mail sent successfully.Please check your inbox for account statment.If you find any query then just contact us." ></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
    </div>
    </form>
</body>
</html>
