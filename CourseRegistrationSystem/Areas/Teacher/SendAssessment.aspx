<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendAssessment.aspx.cs" Inherits="CourseRegistrationSystem.Areas.Teacher.SendAssessment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="lblSelectClass" runat="server" Text="Select Class"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpDwnClassList" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnGetEligibleStudList" runat="server" OnClick="btnGetEligibleStudList_Click" Text="Get Eligible student List" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
