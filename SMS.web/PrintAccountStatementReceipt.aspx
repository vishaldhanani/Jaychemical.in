<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintAccountStatementReceipt.aspx.cs" Culture="en-GB" Inherits="PrintAccountStatementReceipt" %>

<%@ Import Namespace="System.IO" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/html5.js"></script>
    <script type="text/javascript">
        function CompareDate() {
            //Note: 00 is month i.e. January
            var dateOne = new Date(document.getElementById("txtStartDate").value); //Year, Month, Date
            var dateTwo = new Date(document.getElementById("txtEndDate").value); //Year, Month, Date
            if (dateOne > dateTwo) {
                alert("End Date should not be less than Start Date.");
                return false;
            }
            else {
                //alert("Date Two is greather then Date One.");
                return true;
            }
        }
    </script>

    <script lang="JavaScript" type="text/javascript">
        function CloseAndRefresh() {
            top.window.location.href = 'ActCustomerAccountStatement.aspx';
            return parent.parent.GB_hide();
        }
    </script>

    <script lang="JavaScript" type="text/javascript">
        $(document).keydown(function (e) {
            // ESCAPE key pressed
            if (e.keyCode == 27) {
                window.close();
            }
        });
    </script>
        
    <title></title>
</head>
<body>
    <script type="text/javascript">
        var GB_ROOT_DIR = '<%= this.ResolveClientUrl("~/greybox/")%>';
    </script>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS.js") %>'></script>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/AJS_fx.js") %>'></script>
    <script type="text/javascript" src='<%= this.ResolveClientUrl("~/greybox/gb_scripts.js") %>'></script>

    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="toolkit1" runat="server"></ajax:ToolkitScriptManager>
        <table style="width: 100%;">
            <tr>
                <td colspan="2" style="text-align: center">Period for Statement</td>
            </tr>
            <tr>
                <td>Start Date:</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Placeholder="dd/mm/yyyy"></asp:TextBox>
                    <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="dd/MM/yyyy" runat="server">
                    </ajax:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rqfval1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtStartDate" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>End Date:</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Placeholder="dd/mm/yyyy"></asp:TextBox>
                    <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="dd/MM/yyyy" runat="server" />
                    <asp:RequiredFieldValidator ID="rqfval2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtEndDate" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hf_PDF" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Excel" runat="server" Value="" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text="PDF" OnClick="btnDownload_Click" CausesValidation="true" ValidationGroup="Save" />&nbsp;
                    <asp:Button ID="btnExcel_Download" runat="server" Text="Excel" OnClick="btnExcel_Download_Click" CausesValidation="true" ValidationGroup="Save" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" />&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
